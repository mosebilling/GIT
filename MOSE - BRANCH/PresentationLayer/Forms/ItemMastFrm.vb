Imports System.IO
Imports System.Net

Public Class ItemMastFrm
    Dim chgbyprg As Boolean
    Dim chgbyprgAmt As Boolean
    Dim _vDtable As New DataTable
    Private _gridItems As DataTable
    Dim _vItemid As New DataTable
    Dim bDatatable As New DataTable
    Dim _vRecPosition As Integer
    Dim IsModi As Boolean
    Dim PreitemId As Long
    Dim bChgOpnQty As Boolean
    Dim bChgOpnCost As Boolean
    Dim _srchOnce As Boolean
    Dim _srchTxtId As Byte
    Private lstKey As Keys
    Private WithEvents fwait As WaitMessageFrm
    Private WithEvents fshowlocationqty As ShowLocationQtyFrm
    Private dtsuspend As DataTable

    Public IsFromEnqry As Boolean
    Public EnqId As Long
    Public Event updateItemCode(ByVal ItemCode As String)
    'object variable
    Dim _objcmnbLayer As New clsCommon_BL
    Dim _objItmMast As New clsItemMast_BL
    'numeric text
    Dim idx As Integer
    Dim str1 As String
    Dim str2 As String
    Dim str3 As String
    Dim SelStart As Integer
    Private isRawMetChange As Boolean
    Dim numCtrl As TextBox

    Private Const PackCode = 0
    Private Const PackUnit = 1
    Private Const OpenQty = 2
    Private Const OpenCost = 3
    Private Const QIH = 4
    Private Const costAvg = 5
    Private Const LastPCost = 6
    Private Const RcvdQty = 7
    Private Const IssdQty = 8
    Private Const PUpD = 9
    Private Const PDownD = 10

    Private Const ItemCode = 0
    Private Const TrDescr = 1
    Private Const Unit = 2
    Private Const QtyInHand = 3
    Private Const CostAverage = 4
    Private Const ConstUnitPrice = 5
    Private Const ConstTaxPrice = 6
    Private Const ConstMrp = 7
    Private Const Constsecondprice = 8
    Private Const Constwsprice = 9
    Private Const Constlcost = 10
    Private Const intReceivedQty = 11
    Private Const intIssuedQty = 12
    Private Const OpnCost = 13
    Private Const OpnQty = 14
    Private Const Rack = 15
    Private Const ConstItemId = 16

    Private Const RawItemCode = 0
    Private Const RawTrDescr = 1
    Private Const RawUnit = 2
    Private Const RawQtyInHand = 3
    Private Const RawItemid = 4
    Private Const RawPffraction = 5
    Private Const RawMid = 6

    Private MyActiveControl As New Object
    Private WithEvents fHistory As New SelectHistory
    Private WithEvents fProductEnquiry As ItmEnqry
    Private WithEvents fMList As Mlistfrm
    Private WithEvents fimage As ViewImageFrm
    Private activecontrolname As String
    Private chgItm As Boolean
    Private SrchText As String
    Private strGridSrchString As String
    Private lnumformat As String
    Private chgamt As Boolean
    Private WithEvents fhsncode As HSNCodeMaster
    'ftp://ftp.upload.mosebilling.com/pImages/

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub ItemMastFrm_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If chgbyprg Then Exit Sub
        If IsFromEnqry = True Then
            txtCode.Focus()
        Else
            btnNew.Focus()
        End If
    End Sub

    Private Sub ItemMastFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not fProductEnquiry Is Nothing Then fProductEnquiry.Close() : fProductEnquiry = Nothing
        If Not fHistory Is Nothing Then fHistory.Close() : fHistory = Nothing
    End Sub
    Private Sub loadMasters()
        Dim strqry As String = "Select Units From UnitsTb Order by IsDefault desc,Units"
        strqry = strqry & " SELECT LName,LCode from LevelTb Order by LCode"
        strqry = strqry & " SELECT LevelTb.LCode, LName, GrpItmCode, UnqGrpId FROM LevelTb LEFT JOIN " & _
                          "(SELECT GrpItmCode, LCode, UnqGrpId FROM GrpItmTb) Q ON Q.LCode = LevelTb.LCode ORDER BY LevelTb.LCode"
        strqry = strqry & " SELECT vatcode FROM VatMasterTb"
        strqry = strqry & " select HSNCode from GSTTb"
        Dim dtset As DataSet
        dtset = _objcmnbLayer._ldDataset(strqry, False)
        ldUnits(dtset.Tables(0))
        ldLevel(dtset.Tables(1), dtset.Tables(2))
        ldVat(dtset.Tables(3))
        Dim ObjLocationlist As New List(Of String)
        ObjLocationlist = toLoadAutoFillListItemsFromDt(dtset.Tables(4))
        Call toAssignDownListToText(txthsncode, ObjLocationlist) '
        If EnableFruitsSales Then
            AddtoCombo(cmbcarrier, "SELECT carriername FROM CarrierTb", True, False)
            cmbcarrier.Visible = True
            Label35.Visible = True
        Else
            cmbcarrier.Visible = False
            Label35.Visible = False
        End If
    End Sub

    Private Sub ItemMastFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
        SetLevelGrid()
        lblgstp.Text = ""
        setRawGrid()
        loadMasters()
        'SetGridHeadPackDet()
        If enableWoodSale Then
            plwoodsale.Visible = True
            setWoodQtyDiscount()
        End If
        Me.Top = Me.Top + 25
        'LocknUnLockControls(True)
        FillGrid()
        If userType = 0 Or userType = 2 Then
            btnAddNext.Tag = 1
            btnModify.Tag = 1
            btnRemove.Tag = 1
        Else
            btnAddNext.Tag = IIf(getRight(4, CurrentUser), 1, 0)
            btnModify.Tag = IIf(getRight(2, CurrentUser), 1, 0)
            btnRemove.Tag = IIf(getRight(3, CurrentUser), 1, 0)
            numopnCost.Visible = getRight(200, CurrentUser)
            Label13.Visible = getRight(200, CurrentUser)
        End If
        cmbCategory.SelectedIndex = 0
        If enableRestuarent Then
            cmbCategory.Items.Add("Menu Item")
        End If
        If enableFloodCess Then
            Label17.Visible = True
            cmbtax.Visible = True
        Else
            Label17.Visible = False
            cmbtax.Visible = False
        End If
        If enablecess Then
            Label31.Visible = True
            Label30.Visible = True
            cmbregularcess.Visible = True
            txtadditionalcess.Visible = True
        Else
            Label31.Visible = False
            Label30.Visible = False
            cmbregularcess.Visible = False
            txtadditionalcess.Visible = False
        End If
        If enableGCC Then
            Label17.Visible = True
            cmbtax.Visible = True
            Label19.Visible = False
            txthsncode.Visible = False
            lblgstp.Visible = False
            Label17.Text = "Set Vat"
            Label24.Text = "VAT"
            Label23.Visible = False
            lbltaxAmt.Visible = False
            Label27.Text = "Part Number"
        End If
        lnumformat = numFormat
        If enableRestuarent Then
            lblwsprice.Text = "Second Price"
            lblwsprice.Left = lblwsprice.Left - 12
        End If
        'cmbSrchType.SelectedIndex = 2
        If enableCreditPrice Then
            Label18.Text = "Credit Price"
        End If
        If IsFromEnqry = True Then
            addnew()
            Timer1.Enabled = True

        End If
        'cmbitemtype.SelectedIndex = 3
        If cessenddate <= DateValue(Date.Now) Then
            lblcessAmt.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub ShowCurrentRecord()
        chgbyprg = True
        chgbyprgAmt = True
        _vDtable.Clear()
        _vDtable = _objcmnbLayer._fldDatatable("SELECT InvItm.*,InvItmPropertiesTb.*,IGST,vatcode,rgcode,itemlistorder FROM InvItm " & _
                                               "left join InvItmPropertiesTb on InvItmPropertiesTb.itemid=invitm.itemid " & _
                                               " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                                               " LEFT JOIN (SELECT vatcode,vatid FROM VatMasterTb)vattb ON vattb.vatid=InvItm.vatid " & _
                                               " LEFT JOIN (SELECT vatcode rgcode,vatid rgid FROM VatMasterTb)regularcess ON regularcess.rgid=InvItm.regularcessid  " & _
                                               "WHERE InvItm.Itemid=" & Val(PreitemId))
        If _vDtable.Rows.Count = 0 Then
            clearControls()
        Else
            txtCode.Text = _vDtable(0)("Item Code")
            txtDescr.Text = Trim(_vDtable(0)("Description") & "")
            txthsncode.Text = Trim(_vDtable(0)("HSNCode") & "")
            PreitemId = _vDtable(0)("ItemID")
            cmbUnit.Text = Trim(_vDtable(0)("Unit") & "")
            getUnitFraction()
            lblcreatedby.Text = _vDtable(0)("CreatedBy") & ""
            lblmodified.Text = _vDtable(0)("ModiBy") & ""
            txtmake.Text = Trim(_vDtable(0)("Make") & "")
            txtmodel.Text = Trim(_vDtable(0)("Model") & "")
            txtrack.Text = Trim(_vDtable(0)("Rack") & "")
            txtmitemcode.Text = _vDtable(0)("mechineItemcode") & ""
            txtdescription.Text = _vDtable(0)("otherDescr") & ""
            numOpnQty.Text = Format(IIf(IsDBNull(_vDtable(0)("opQty")), 0, _vDtable(0)("opQty")), lnumformat)
            numopnCost.Text = Format(IIf(IsDBNull(_vDtable(0)("opcost")), 0, _vDtable(0)("opcost")), numFormat)
            lblcostAvg.Text = Format(IIf(IsDBNull(_vDtable(0)("CostAvg")), 0, _vDtable(0)("CostAvg")), numFormat)
            cmbCategory.Text = IIf(IsDBNull(_vDtable(0)("itemCategory")), "Stock", _vDtable(0)("itemCategory"))
            lbllpcost.Text = Format(IIf(IsDBNull(_vDtable(0)("LastPurchCost")), 0, _vDtable(0)("LastPurchCost")), numFormat)
            lblissd.Text = Format(IIf(IsDBNull(_vDtable(0)("IssdQty")), 0, _vDtable(0)("IssdQty")), lnumformat)
            lblrcvd.Text = Format(IIf(IsDBNull(_vDtable(0)("RcvdQty")), 0, _vDtable(0)("RcvdQty")), lnumformat)
            numMinQty.Text = Format(IIf(IsDBNull(_vDtable(0)("MinQty")), 0, _vDtable(0)("MinQty")), lnumformat)
            numunitprice.Text = Format(IIf(IsDBNull(_vDtable(0)("UnitPrice")), 0, _vDtable(0)("UnitPrice")), numFormat)
            numunitprice.Tag = IIf(IsDBNull(_vDtable(0)("UnitPrice")), 0, _vDtable(0)("UnitPrice"))

            txtperton.Text = Format(IIf(IsDBNull(_vDtable(0)("aboveTenTon")), 0, _vDtable(0)("aboveTenTon")), lnumformat)
            txtsecondprice.Tag = IIf(IsDBNull(_vDtable(0)("secondPrice")), 0, _vDtable(0)("secondPrice"))
            txtsecondprice.Text = Format(IIf(IsDBNull(_vDtable(0)("secondPrice")), 0, _vDtable(0)("secondPrice")), numFormat)
            txtws.Tag = IIf(IsDBNull(_vDtable(0)("UnitPriceWS")), 0, _vDtable(0)("UnitPriceWS"))
            txtws.Text = Format(IIf(IsDBNull(_vDtable(0)("UnitPriceWS")), 0, _vDtable(0)("UnitPriceWS")), numFormat)
            txtmrp.Text = Format(IIf(IsDBNull(_vDtable(0)("mrp")), 0, _vDtable(0)("mrp")), numFormat)
            txtadditionalcess.Text = Format(Val(_vDtable(0)("additionalcess") & ""), numFormat)
            txtwmcalculation.Text = Format(Val(_vDtable(0)("wmcalculation") & ""), numFormat)
            txtcount.Text = IIf(IsDBNull(_vDtable(0)("salescountForPoint")), 0, _vDtable(0)("salescountForPoint"))
            txtpoint.Text = IIf(IsDBNull(_vDtable(0)("salesPontOncount")), 0, _vDtable(0)("salesPontOncount"))
            txtorprice1.Text = Format(IIf(IsDBNull(_vDtable(0)("Price1")), 0, _vDtable(0)("Price1")), numFormat)
            txtorprice2.Text = Format(IIf(IsDBNull(_vDtable(0)("Price2")), 0, _vDtable(0)("Price2")), numFormat)
            txtorprice3.Text = Format(IIf(IsDBNull(_vDtable(0)("Price3")), 0, _vDtable(0)("Price3")), numFormat)
            txtorprice4.Text = Format(IIf(IsDBNull(_vDtable(0)("Price4")), 0, _vDtable(0)("Price4")), numFormat)
            txtorprice5.Text = Format(IIf(IsDBNull(_vDtable(0)("Price5")), 0, _vDtable(0)("Price5")), numFormat)
            txtorprice6.Text = Format(IIf(IsDBNull(_vDtable(0)("Price6")), 0, _vDtable(0)("Price6")), numFormat)
            txtorprice7.Text = Format(IIf(IsDBNull(_vDtable(0)("Price7")), 0, _vDtable(0)("Price7")), numFormat)
            txtorprice8.Text = Format(IIf(IsDBNull(_vDtable(0)("Price8")), 0, _vDtable(0)("Price8")), numFormat)
            txtorprice9.Text = Format(IIf(IsDBNull(_vDtable(0)("Price9")), 0, _vDtable(0)("Price9")), numFormat)
            txtorprice10.Text = Format(IIf(IsDBNull(_vDtable(0)("Price10")), 0, _vDtable(0)("Price10")), numFormat)
            If Val(_vDtable(0)("IGST") & "") > 0 Then
                txtpriceWtax.Text = Format(CDbl(numunitprice.Text) + ((CDbl(numunitprice.Text) * CDbl(_vDtable(0)("IGST"))) / 100), numFormat)
                txtpriceWtax.Tag = CDbl(_vDtable(0)("IGST"))
                lbldptax.Text = Format(CDbl(txtsecondprice.Text) + ((CDbl(txtsecondprice.Text) * CDbl(_vDtable(0)("IGST"))) / 100), numFormat)
                lblwstax.Text = Format(CDbl(txtws.Text) + ((CDbl(txtws.Text) * CDbl(_vDtable(0)("IGST"))) / 100), numFormat)
                lbp1tax.Text = Format(CDec(txtorprice1.Text) + ((CDbl(txtorprice1.Text) * CDbl(_vDtable(0)("IGST"))) / 100), numFormat)
                lbp2tax.Text = Format(CDec(txtorprice2.Text) + ((CDbl(txtorprice2.Text) * CDbl(_vDtable(0)("IGST"))) / 100), numFormat)
                lbp3tax.Text = Format(CDec(txtorprice3.Text) + ((CDbl(txtorprice3.Text) * CDbl(_vDtable(0)("IGST"))) / 100), numFormat)
                lbp4tax.Text = Format(CDec(txtorprice4.Text) + ((CDbl(txtorprice4.Text) * CDbl(_vDtable(0)("IGST"))) / 100), numFormat)
                lbp5tax.Text = Format(CDec(txtorprice5.Text) + ((CDbl(txtorprice5.Text) * CDbl(_vDtable(0)("IGST"))) / 100), numFormat)
                lbp6tax.Text = Format(CDec(txtorprice6.Text) + ((CDbl(txtorprice6.Text) * CDbl(_vDtable(0)("IGST"))) / 100), numFormat)
                lbp7tax.Text = Format(CDec(txtorprice7.Text) + ((CDbl(txtorprice7.Text) * CDbl(_vDtable(0)("IGST"))) / 100), numFormat)
                lbp8tax.Text = Format(CDec(txtorprice8.Text) + ((CDbl(txtorprice8.Text) * CDbl(_vDtable(0)("IGST"))) / 100), numFormat)
                lbp9tax.Text = Format(CDec(txtorprice9.Text) + ((CDbl(txtorprice9.Text) * CDbl(_vDtable(0)("IGST"))) / 100), numFormat)
                lbp10tax.Text = Format(CDec(txtorprice10.Text) + ((CDbl(txtorprice10.Text) * CDbl(_vDtable(0)("IGST"))) / 100), numFormat)



            Else
                txtpriceWtax.Tag = 0
            End If
            If txthsncode.Text = "" Then
                lblgstp.Text = ""
            Else
                lblgstp.Text = "GST : " & Val(txtpriceWtax.Tag) & "%"
            End If


            lblQIH.Text = Format(IIf(IsDBNull(_vDtable(0)("opQty")), 0, _vDtable(0)("opQty")) + IIf(IsDBNull(_vDtable(0)("RcvdQty")), 0, _vDtable(0)("RcvdQty")) - IIf(IsDBNull(_vDtable(0)("IssdQty")), 0, _vDtable(0)("IssdQty")), lnumformat)
            If Not IsDBNull(_vDtable(0)("isSerialNo")) Then
                chkserial.Checked = _vDtable(0)("isSerialNo")
            End If
            If Not IsDBNull(_vDtable(0)("isDuealSerialNo")) Then
                chkdualsim.Checked = _vDtable(0)("isDuealSerialNo")
            End If
            If Not IsDBNull(_vDtable(0)("ismanufacturing")) Then
                chkmanufacturing.Checked = _vDtable(0)("ismanufacturing")
            End If
            If Not IsDBNull(_vDtable(0)("ishide")) Then
                chkhide.Checked = _vDtable(0)("ishide")
            End If
            cmbtax.Text = Trim(_vDtable(0)("vatcode") & "")
            cmbregularcess.Text = Trim(_vDtable(0)("rgcode") & "")

            cmbagegroup.Text = _vDtable(0)("agegroup") & ""
            cmbgender.Text = _vDtable(0)("gender") & ""
            txtcolor.Text = Trim(_vDtable(0)("color") & "")
            txtsize.Text = Trim(_vDtable(0)("itmsize") & "")
            If Not IsDBNull(_vDtable(0)("showinweb")) Then
                chkshowinweb.Checked = _vDtable(0)("showinweb")
            End If
            txtitemord.Text = Trim(_vDtable(0)("itemlistorder") & "")
            If Not IsDBNull(_vDtable(0)("ishidefrommilksales")) Then
                ckbxhidemilk.Checked = _vDtable(0)("ishidefrommilksales")
            End If


            'Dim dt As DataTable
            'dt = _objcmnbLayer._fldDatatable("SELECT vatcode FROM VatMasterTb WHERE vatid=" & Val(_vDtable(0)("vatid") & ""))
            'If dt.Rows.Count > 0 Then
            '    cmbtax.Text = dt(0)("vatcode")
            'Else
            '    cmbtax.Text = ""
            'End If
            cmbcarrier.Text = Trim(_vDtable(0)("carrier") & "")
            ldItemLevel()
            FillGrid()
            getRawmaterial()
            If enableWoodSale Then loadWoodQtyDisc()
            btnRemove.Enabled = True
            If EnableBarcode Then btnbarcode.Visible = True
            chgamt = True
            calculateTaxFromUnitPrice(True)
            loadimage()
        End If

            chgbyprg = False
            chgbyprgAmt = False
            BtnUpdate.Enabled = False
    End Sub

    Private Sub ldVat(ByVal dt As DataTable)
        Dim i As Integer
        'dt = _objcmnbLayer._fldDatatable("SELECT vatcode FROM VatMasterTb")
        cmbtax.Items.Clear()
        cmbregularcess.Items.Clear()
        cmbregularcess.Items.Add("")
        cmbtax.Items.Add("")
        For i = 0 To dt.Rows.Count - 1
            cmbtax.Items.Add(dt(i)("vatcode"))
            cmbregularcess.Items.Add(dt(i)("vatcode"))
        Next
        If cmbtax.Items.Count > 1 Then cmbtax.SelectedIndex = 0
    End Sub
    Private Sub ldHSN()
        'Dim dt As DataTable
        'Dim i As Integer
        'dt = _objcmnbLayer._fldDatatable("SELECT HSNCode FROM GSTTb")
        'cmbhsncode.Items.Clear()
        'cmbhsncode.Items.Add("")
        'For i = 0 To dt.Rows.Count - 1
        '    cmbhsncode.Items.Add(dt(i)("HSNCode"))
        'Next
        Dim ObjLocationlist As New List(Of String)
        ObjLocationlist = toLoadAutoFillListItems("HSNCode", "GSTTb")
        Call toAssignDownListToText(txthsncode, ObjLocationlist) '
    End Sub
    Private Sub ldUnits(ByVal dtTable As DataTable)
        Dim i As Integer
        'dtTable = _objcmnbLayer._fldDatatable("Select Units From UnitsTb Order by IsDefault desc,Units")
        cmbUnit.Items.Clear()
        If dtTable.Rows.Count > 0 Then
            For i = 0 To dtTable.Rows.Count - 1
                cmbUnit.Items.Add(dtTable(i)("Units"))
            Next
        End If
        If cmbUnit.Items.Count > 0 Then cmbUnit.SelectedIndex = 0
    End Sub
    Private Sub txtCode_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyDown, _
                                                                                                                    numOpnQty.KeyDown, numopnCost.KeyDown, _
                                                                                                                    numMinQty.KeyDown, cmbUnit.KeyDown, _
                                                                                                                    cmbCategory.KeyDown, numunitprice.KeyDown, _
                                                                                                                    txtmake.KeyDown, txtmodel.KeyDown, txtws.KeyDown, txtDescr.KeyDown, _
                                                                                                                    txtsecondprice.KeyDown, txtadditionalcess.KeyDown, txtorprice1.KeyDown, txtorprice2.KeyDown, txtorprice3.KeyDown, _
                                                                                                                    txtorprice4.KeyDown, txtorprice5.KeyDown, txtorprice6.KeyDown, txtorprice7.KeyDown, txtorprice8.KeyDown, _
                                                                                                                    txtorprice9.KeyDown, txtorprice10.KeyDown
        Dim myCtrl As Control = sender
        lstKey = e.KeyCode
        If e.KeyCode = Keys.Enter Then
            If myCtrl.Name = "numunitprice" Then
                txtpriceWtax.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If

            If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveUp(myCtrl.Text)
                    Exit Sub
                End If
            End If

        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(myCtrl.Text)
                    Exit Sub
                End If
            End If
        End If

    End Sub
    'Private Sub packCreations()
    '    With grdpackDet
    '        .Rows.Add(1)
    '        .Item(PackCode, .Rows.Count - 1).Value = "P1"
    '        .Item(PackUnit, .Rows.Count - 1).Value = ""
    '        .Item(OpenQty, .Rows.Count - 1).Value = "0.00"
    '        .Item(OpenCost, .Rows.Count - 1).Value = ""
    '        .Item(QIH, .Rows.Count - 1).Value = "0.00"
    '        .Item(costAvg, .Rows.Count - 1).Value = "0.00"
    '        .Item(LastPCost, .Rows.Count - 1).Value = "0.00"
    '        .Item(RcvdQty, .Rows.Count - 1).Value = "0.00"
    '        .Item(IssdQty, .Rows.Count - 1).Value = "0.00"
    '        .Item(PUpD, .Rows.Count - 1).Value = 1
    '        .Item(PDownD, .Rows.Count - 1).Value = 1

    '        .Rows.Add(1)
    '        .Item(PackCode, .Rows.Count - 1).Value = "P2"
    '        .Item(PackUnit, .Rows.Count - 1).Value = ""
    '        .Item(OpenQty, .Rows.Count - 1).Value = "0.00"
    '        .Item(OpenCost, .Rows.Count - 1).Value = ""
    '        .Item(QIH, .Rows.Count - 1).Value = "0.00"
    '        .Item(costAvg, .Rows.Count - 1).Value = "0.00"
    '        .Item(LastPCost, .Rows.Count - 1).Value = "0.00"
    '        .Item(RcvdQty, .Rows.Count - 1).Value = "0.00"
    '        .Item(IssdQty, .Rows.Count - 1).Value = "0.00"
    '        .Item(PUpD, .Rows.Count - 1).Value = 1
    '        .Item(PDownD, .Rows.Count - 1).Value = 1
    '    End With
    'End Sub
    Private Sub addnew()
        btnNew.Text = "Undo"
        IsModi = False
        chgbyprg = True
        chgbyprgAmt = True
        clearControls()
        chgbyprg = True
        txtCode.Text = GenerateNext(txtCode.Text)
        chgbyprgAmt = False
        chgbyprg = False
        LocknUnLockControls(False)
        EnaDisBtn("EDIT")
        txtCode.Focus()
        grdLevel.Columns(1).ReadOnly = False
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If Val(btnAddNext.Tag) = 0 Then
            MsgBox("This user do not have permission to add new entry", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If btnNew.Text = "Add &New" Then
            addnew()
        Else  ' Modify
            If BtnUpdate.Enabled Then
                If MsgBox("You are going to Undo New ? " & vbCrLf & " Continue ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Undo Modify ?") = MsgBoxResult.No Then Exit Sub
            End If
            btnNew.Text = "Add &New"
            chgbyprg = True
            chgbyprgAmt = True
            clearControls()
            'LdRec(PreitemId)
            chgbyprgAmt = False
            chgbyprg = False
            LocknUnLockControls(True)
            bChgOpnQty = False
            bChgOpnCost = False
            IsModi = False
            EnaDisBtn("UNDO")
            grdLevel.Columns(1).ReadOnly = True
        End If
    End Sub
    Sub LocknUnLockControls(ByVal bLock As Boolean)
        LockControlsInDetailTab(bLock)
        grdLevel.ReadOnly = bLock
        grdLevel.ReadOnly = bLock
        cmbCategory.Enabled = Not bLock
        'cmbtax.Enabled = Not bLock
        cmbUnit.Enabled = Not bLock
        cmbregularcess.Enabled = Not bLock
        plwoodsale.Enabled = Not bLock
        If EnableGST Then
            txthsncode.ReadOnly = bLock
        End If
        btnSet.Enabled = False
        Panel3.Enabled = bLock
        cmbcarrier.Enabled = Not bLock
    End Sub
    Private Sub LockControlsInDetailTab(ByVal bLock As Boolean)
        On Error Resume Next
        For Each Control In Panel2.Controls
            If Control.name = "" Then Exit Sub
            If TypeOf (Control) Is TextBox Or TypeOf (Control) Is CheckBox Or TypeOf (Control) Is ComboBox Then
                Control.readonly = bLock
            End If
        Next
        For Each Control In Panel2.Controls
            If TypeOf (Control) Is TextBox Or TypeOf (Control) Is CheckBox Or TypeOf (Control) Is ComboBox Then
                Control.readonly = bLock
            End If
        Next
        For Each Control In GroupBox3.Controls
            If TypeOf (Control) Is TextBox Or TypeOf (Control) Is CheckBox Or TypeOf (Control) Is ComboBox Then
                Control.readonly = bLock
            End If
        Next
        txtorprice1.ReadOnly = bLock
        txtorprice2.ReadOnly = bLock
        txtorprice3.ReadOnly = bLock
        txtorprice4.ReadOnly = bLock
        txtorprice5.ReadOnly = bLock
        txtorprice6.ReadOnly = bLock
        txtorprice7.ReadOnly = bLock
        txtorprice8.ReadOnly = bLock
        txtorprice9.ReadOnly = bLock
        txtorprice10.ReadOnly = bLock
        chkserial.Enabled = Not bLock
        chkdualsim.Enabled = Not bLock
        chkmanufacturing.Enabled = Not bLock
        chkshowinweb.Enabled = Not bLock
        cmbagegroup.Enabled = Not bLock
        cmbgender.Enabled = Not bLock
        txtcolor.Enabled = Not bLock
        txtsize.Enabled = Not bLock
        chkhide.Enabled = Not bLock
        ckbxhidemilk.Enabled = Not bLock
        If EnableGST Then
            txthsncode.ReadOnly = bLock
        End If
        txtitemord.ReadOnly = bLock
        txtperton.ReadOnly = bLock
        txtadditionalcess.ReadOnly = bLock
        plrawmeterial.Enabled = Not bLock
        txtwmcalculation.ReadOnly = bLock
        If cessenddate <= DateValue(Date.Now) And EnableGST Then
            'Label17.Enabled = False
            cmbtax.Enabled = False
            lblcessAmt.Enabled = False
        Else
            'Label17.Enabled = True
            cmbtax.Enabled = Not bLock
            lblcessAmt.Enabled = Not bLock
        End If
        txtdescription.ReadOnly = bLock
    End Sub
    Private Sub clearControls()
        SetControlsInDetailTab()
        grdpackDet.Rows.Clear()
        _vDtable.Rows.Clear()
        Dim i As Integer
        For i = 0 To grdLevel.Rows.Count - 1
            With grdLevel
                .Item(1, i).Value = ""
            End With
        Next
        FillGrid()
        lblgstp.Text = ""
        lblpicpath.Text = ""
        grdLevel.Tag = ""
        chgbyprg = True
        btnupload.Enabled = False
        'cmbtax.Tag = 0
        'cmbtax.Text = ""
        If cmbtax.Items.Count > 1 Then cmbtax.SelectedIndex = 1
        txtdescription.Text = ""
        isRawMetChange = False
        grdmanufacturing.Rows.Clear()
        grdwoodQtyDisc.Rows.Clear()
        btnbarcode.Visible = False
        lblcessAmt.Text = Format(0, numFormat)
        lbltaxAmt.Text = Format(0, numFormat)
        txtpriceWtax.Tag = 0
        PreitemId = 0
        chkhide.Checked = False
        If Not dtsuspend Is Nothing Then
            If dtsuspend.Rows.Count > 0 Then
                dtsuspend.Rows.Clear()
            End If
        End If
        cmbagegroup.Text = ""
        cmbgender.Text = ""
        txtcolor.Text = ""
        txtsize.Text = ""
        chkshowinweb.Checked = False
        ckbxhidemilk.Checked = False
        chgbyprg = False
        txtorprice1.Text = ""
        txtorprice2.Text = ""
        txtorprice3.Text = ""
        txtorprice4.Text = ""
        txtorprice5.Text = ""
        txtorprice6.Text = ""
        txtorprice7.Text = ""
        txtorprice8.Text = ""
        txtorprice9.Text = ""
        txtorprice10.Text = ""
        lbp1tax.Text = "0.00"
        lbp2tax.Text = "0.00"
        lbp3tax.Text = "0.00"
        lbp4tax.Text = "0.00"
        lbp5tax.Text = "0.00"
        lbp6tax.Text = "0.00"
        lbp7tax.Text = "0.00"
        lbp8tax.Text = "0.00"
        lbp9tax.Text = "0.00"
        lbp10tax.Text = "0.00"
        lbldptax.Text = "0.00"
        txtitemord.Text = 0

        'SetLevelGrid()
    End Sub
    Private Sub ldLevel(ByVal LevelTb As DataTable, ByVal LevelGrpTb As DataTable)
        Dim dtGroup As DataTable
        Dim cmb As New DataGridViewComboBoxCell
        Dim itmlevel As New DataTable
        'grdLevel.Rows.Clear()
        'LevelTb = _objcmnbLayer._fldDatatable("SELECT LName,LCode from LevelTb Order by LCode")
        'LevelGrpTb = _objcmnbLayer._fldDatatable("SELECT LevelTb.LCode, LName, GrpItmCode, UnqGrpId FROM LevelTb LEFT JOIN " & _
        '  "(SELECT GrpItmCode, LCode, UnqGrpId FROM GrpItmTb) Q ON Q.LCode = LevelTb.LCode ORDER BY LevelTb.LCode")
        chgbyprg = True
        Dim found As Boolean
        With grdLevel
            .Rows.Clear()
            Dim i As Integer
            '.RowCount = 0
            If LevelTb.Rows.Count > 0 Then
                For i = 0 To LevelTb.Rows.Count - 1
                    If LevelTb.Rows.Count > .RowCount Then .Rows.Add()
                    .Item(0, i).Value = LevelTb(i)("LName")
                    cmb = .Rows(i).Cells(1)
                    If cmb.Items.Count = 0 Then
                        cmb.Items.Clear()
                        cmb.Items.Add("")
                        dtGroup = SearchGrid(LevelGrpTb, LevelTb(i)("LName"), 1)
                        found = False
                        For j = 0 To dtGroup.Rows.Count - 1
                            cmb.Items.Add(dtGroup(j)("GrpItmCode"))
                            If itmlevel.Rows.Count > 0 Then
                                If Trim(itmlevel(0)(0) & "") = Trim(dtGroup(j)("GrpItmCode") & "") Then found = True
                            End If
                        Next
                    End If
                Next
            End If
            .Refresh()
        End With
        chgbyprg = False
    End Sub
    Private Sub ldItemLevel()
        Dim i As Integer
        Dim itmlevel As DataTable
        With grdLevel
            For i = 0 To .RowCount - 1
                If Not _vDtable Is Nothing Then
                    If _vDtable.Rows.Count > 0 Then
                        itmlevel = _objcmnbLayer._fldDatatable("SELECT GrpItmCode,UnqGrpId FROM GrpItmTb WHERE UnqGrpId=" & Val(_vDtable(0)("Level" & i + 1) & ""))
                        If itmlevel.Rows.Count > 0 Then
                            .Item(1, i).Value = itmlevel(0)(0)
                        End If
                    End If
                End If
            Next
        End With

    End Sub
    Private Sub SetControlsInDetailTab()
        chgbyprg = True
        chgbyprgAmt = True
        For Each Control In Panel2.Controls
            If TypeOf (Control) Is TextBox Then
                Control.text = ""
                Control.tag = ""
            End If
            If TypeOf (Control) Is CheckBox Then
                Control.checked = False
                Control.tag = ""
            End If
        Next
        For Each Control In Panel2.Controls
            If TypeOf (Control) Is TextBox Then
                Control.text = ""
                Control.tag = ""
            End If
            If TypeOf (Control) Is CheckBox Then
                Control.checked = False
                Control.tag = ""
            End If
        Next

        numMinQty.Text = Format(0, numFormat)
        txtadditionalcess.Text = Format(0, numFormat)
        txtwmcalculation.Text = Format(0, numFormat)
        numopnCost.Text = Format(0, numFormat)
        numunitprice.Text = Format(0, numFormat)
        txtws.Text = Format(0, numFormat)
        txtmrp.Text = Format(0, numFormat)
        numOpnQty.Text = Format(0, numFormat)
        txtpriceWtax.Text = Format(0, numFormat)
        txtperton.Text = Format(0, numFormat)
        txtsecondprice.Text = Format(0, numFormat)
        lblrcvd.Text = "0.00"
        lblissd.Text = "0.00"
        lblQIH.Text = "0.00"
        lblcostAvg.Text = "0.00"
        lbllpcost.Text = "0.00"
        lblcreatedby.Text = ""
        lblmodified.Text = ""
        txthsncode.Text = ""
        chgbyprgAmt = False
        chgbyprg = False
        chkserial.Checked = False
        chkdualsim.Checked = False
        chkmanufacturing.Checked = False
        btnaddhsn.Enabled = False
        If cmbcarrier.Text <> "" Then cmbcarrier.Text = ""
    End Sub
    Private Sub SetLevelGrid()
        'Dim dtLevel As DataTable
        'Dim dtGrp As DataTable
        chgbyprg = True
        grdLevel.Columns.Clear()
        Dim headert(2) As String
        With grdLevel
            .ReadOnly = True
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = True
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            .AutoResizeColumns()
            .StandardTab = False
            '.Location = New Point(1, 1)
            .ColumnCount = 1
            '.Width = tbPanelLevel.Width - 2
            '.Height = tbPanelLevel.Height - 2
            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.Olive
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("arial", 8.5!, FontStyle.Regular)

            .Columns(0).HeaderText = "Level"
            .Columns(0).Width = 150
            .Columns(0).ReadOnly = True

            Dim cmb As New DataGridViewComboBoxColumn
            cmb.HeaderText = "Item Group"
            cmb.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            cmb.DropDownWidth = 150
            .Columns.Add(cmb)
            .Columns(1).ReadOnly = True
        End With
        chgbyprg = False
    End Sub
    Sub EnaDisBtn(ByVal keyStr As String)
        Select Case keyStr
            Case "EDIT"
                btnAddNext.Enabled = False
                btnRemove.Enabled = False
                BtnUpdate.Enabled = False
                btnModify.Enabled = False
                btnNew.Enabled = True
                btnaddhsn.Enabled = True
            Case "UPDATE", "UNDO", "LOAD"
                BtnUpdate.Enabled = False
                btnNew.Enabled = True
                btnNew.Text = "Add &New"
                btnAddNext.Enabled = True
                btnModify.Enabled = True
                btnModify.Text = "&Modify"
                btnRemove.Enabled = True
                btnaddhsn.Enabled = False
        End Select
    End Sub
    Private Function GenerateNext(ByVal strCode As String) As String
        Dim N As Double
        Dim NewCode As String
        Dim i As Byte
        Dim f As Byte
        Dim tmp As String
        If strCode = "" Then
            strCode = _objItmMast.returnLastItemCode
        End If
        If strCode = "" Then
            strCode = "ITM000"
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
    Private Sub btnAddNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNext.Click
        'If Trim(txtCode.Text) = "" Then Exit Sub

        'Dim strCode As String
        'strCode = Trim(txtCode.Text)
        'btnNew.Text = "Undo&New"
        'IsModi = False
        'clearControls()
        'chgbyprg = True
        'txtCode.Text = GenerateNext(strCode)
        'chgbyprg = False
        'LocknUnLockControls(False)
        'EnaDisBtn("EDIT")
        'txtDescr.Focus()
        'chgbyprg = False
        Dim frm As New RefreshcostAverage
        frm.itemid = Val(PreitemId)
        frm.ShowDialog()
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        If Val(btnModify.Tag) = 0 Then
            MsgBox("This user do not have permission to modify", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Val(PreitemId) = 0 Then
            MsgBox("Select an Item to Modify !", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Cannot Modify !")
            Exit Sub
        End If
        btnNew.Text = "Undo"
        IsModi = True
        LocknUnLockControls(False)
        EnaDisBtn("EDIT")
        btnSet.Enabled = False
        txtDescr.Focus()
        grdLevel.Columns(1).ReadOnly = False
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Val(btnRemove.Tag) = 0 Then
            MsgBox("This user do not have permission to remove", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim dttable As DataTable
        dttable = _objcmnbLayer._fldDatatable("SELECT itemid FROM INVITM WHERE opQty >0 AND itemid =" & Val(PreitemId))
        If dttable.Rows.Count > 0 Then
            MsgBox("OPENING QTY Exist !", MsgBoxStyle.Information, "Cannot Delete !")
            Exit Sub
        End If
        dttable.Clear()
        'dttable = _objcmnbLayer._fldDatatable("SELECT itemid FROM INVITM WHERE ITEMID<>BASEID AND itemid =" & BaseID)
        'If dttable.Rows.Count > 0 Then
        '    MsgBox("PACK ITEMS EXISTS !", MsgBoxStyle.Information, "Cannot Delete !")
        '    Exit Sub
        'End If
        'dttable.Clear()
        dttable = _objcmnbLayer._fldDatatable("SELECT ITEMID FROM ITMINVTRTB WHERE itemid =" & Val(PreitemId) & " UNION ALL SELECT ITEMID FROM JobitemTb WHERE itemid =" & Val(PreitemId))
        If dttable.Rows.Count > 0 Then
            MsgBox("Transaction Exist !", MsgBoxStyle.Information, "Cannot Delete !")
        Else
            If MsgBox("Are You Sure to Remove The Item!", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.No Then Exit Sub
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM INVITM WHERE itemid=" & Val(PreitemId))
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM ItemRawMeterialTb WHERE itemid=" & Val(PreitemId))
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM InvItmPropertiesTb WHERE itemid=" & Val(PreitemId))
            _objcmnbLayer._saveDatawithOutParm("delete from BatchTb where Itemid=" & Val(PreitemId) & " and batchTrid=0")
            If _vRecPosition > 0 And _vRecPosition < _vItemid.Rows.Count - 1 Then
                _vRecPosition = _vRecPosition + 1
            ElseIf _vRecPosition = _vItemid.Rows.Count - 1 Then
                _vRecPosition = _vRecPosition - 1
            End If
            clearControls()
        End If
        tbitem.SelectedIndex = 1
        btnrefresh_Click(btnrefresh, New System.EventArgs)
    End Sub
    Private Function chkFields() As Boolean
        Dim _vdtTable As DataTable
        chkFields = True
        If Trim(txtCode.Text) = "" Then
            MsgBox("'Item Code' Cannot be Blank !", MsgBoxStyle.Information)
            txtCode.Focus()
            chkFields = False
            Exit Function
        End If
        If Trim(txtDescr.Text) = "" Then
            MsgBox("'Item Name' Cannot be Blank !", MsgBoxStyle.Information)
            txtDescr.Focus()
            chkFields = False
            Exit Function
        End If
        If IsModi Then
            _vdtTable = _objcmnbLayer._fldDatatable("Select ItemId from InvItm Where ItemId= " & Val(PreitemId))
            If _vdtTable.Rows.Count = 0 Then
                MsgBox("Selected Base Item Not Found !", MsgBoxStyle.Information)
                chkFields = False
            End If
        End If
        _vdtTable = _objcmnbLayer._fldDatatable("Select ItemId from InvItm Where [Item Code] ='" & MkDbSrchStr(txtCode.Text) & "'" & IIf(IsModi, " and ItemId <>" & Val(PreitemId), ""))
        If _vdtTable.Rows.Count > 0 Then
            If _vdtTable(0)("ItemId") <> Val(PreitemId) Then
                MsgBox("Item Code Already Exist as [Item Code] or [Barcode] !", MsgBoxStyle.Information)
                txtCode.Focus()
                chkFields = False
                Exit Function
            End If
        End If
    End Function

    Private Sub setValue()
        _objItmMast.ItemId = Val(PreitemId)
        _objItmMast.ItemId = Val(PreitemId)
        _objItmMast.ItemCode = Trim(txtCode.Text)
        _objItmMast.Descr = Trim(txtDescr.Text)
        _objItmMast.Category = Trim(cmbCategory.Text)
        _objItmMast.Unit = cmbUnit.Text
        If numOpnQty.Text = "" Then numOpnQty.Text = 0
        _objItmMast.opnQty = CDbl(numOpnQty.Text)
        If numopnCost.Text = "" Then numopnCost.Text = 0
        _objItmMast.OpnCost = CDbl(numopnCost.Text)
        If numMinQty.Text = "" Then numMinQty.Text = 0
        If Val(numunitprice.Tag) = 0 Then numunitprice.Tag = 0
        If Val(txtws.Text) = 0 Then txtws.Text = 0
        _objItmMast.MinQty = Val(numMinQty.Text)
        _objItmMast.salesPrice = CDbl(numunitprice.Tag)
        _objItmMast.Make = txtmake.Text
        _objItmMast.Model = txtmodel.Text
        If enableFloodCess Or enableGCC Then
            _objItmMast.vat = cmbtax.Text
        End If
        If Val(txtws.Tag & "") = 0 Then txtws.Tag = CDbl(txtws.Text)
        If Val(txtws.Tag) = 0 Then txtws.Tag = 0
        _objItmMast.WSalesPrice = CDbl(txtws.Tag)
        _objItmMast.hsncode = txthsncode.Text
        _objItmMast.Rack = txtrack.Text
        If Val(txtmrp.Text) = 0 Then txtmrp.Text = Format(0, numFormat)
        _objItmMast.MRP = CDbl(txtmrp.Text)
        With grdLevel
            Dim i As Integer
            For i = 0 To .Rows.Count - 1
                Select Case i
                    Case 0
                        _objItmMast.Level1 = .Item(1, 0).Value
                    Case 1
                        _objItmMast.Level2 = .Item(1, 1).Value
                    Case 2
                        _objItmMast.Level3 = .Item(1, 2).Value
                    Case 3
                        _objItmMast.Level4 = .Item(1, 3).Value
                    Case 4
                        _objItmMast.Level5 = .Item(1, 4).Value
                    Case 5
                        _objItmMast.Level6 = .Item(1, 5).Value
                    Case 6
                        _objItmMast.Level7 = .Item(1, 6).Value
                    Case 7
                        _objItmMast.Level8 = .Item(1, 7).Value
                    Case 8
                        _objItmMast.Level9 = .Item(1, 8).Value
                    Case 9
                        _objItmMast.Level10 = .Item(1, 9).Value
                End Select
            Next
        End With
        _objItmMast.MinQty = CDbl(numMinQty.Text)
        If IsModi Then
            _objItmMast.LstModiBy = CurrentUser
            _objItmMast.LstModiDt = Date.Now
            _objItmMast.CreatedDt = Date.Now
            _objItmMast.Createdby = Trim(CurrentUser)
        Else
            _objItmMast.LstModiBy = ""
            _objItmMast.Createdby = Trim(CurrentUser)
            _objItmMast.CreatedDt = Date.Now
            _objItmMast.LstModiDt = Date.Now
        End If
        _objItmMast.Ismodi = IsModi
        _objItmMast.isDuealSerialNo = chkdualsim.Checked
        _objItmMast.isSerialNo = chkserial.Checked
        _objItmMast.ismanufacturing = chkmanufacturing.Checked
        If Val(txtperton.Text) = 0 Then txtperton.Text = Format(0, lnumformat)
        _objItmMast.aboveTenTon = CDbl(txtperton.Text)
        If Val(txtsecondprice.Text) = 0 Then txtsecondprice.Text = 0
        If Val(txtsecondprice.Tag & "") = 0 Then txtsecondprice.Tag = CDbl(txtsecondprice.Text)
        _objItmMast.secondPrice = CDbl(txtsecondprice.Tag)
        _objItmMast.mechineItemcode = txtmitemcode.Text
        If enablecess Then
            _objItmMast.regularcess = cmbregularcess.Text
            _objItmMast.additionalcess = CDbl(txtadditionalcess.Text)
        End If


        _objItmMast.wmcalculation = CDbl(txtwmcalculation.Text)
        _objItmMast.carrier = cmbcarrier.Text
        _objItmMast.salescountForPoint = Val(txtcount.Text)
        _objItmMast.salesPontOncount = Val(txtpoint.Text)
        If Val(txtorprice1.Text) = 0 Then txtorprice1.Text = Format(0, lnumformat)
        _objItmMast.Price1 = CDbl(txtorprice1.Text)
        If Val(txtorprice2.Text) = 0 Then txtorprice2.Text = Format(0, lnumformat)
        _objItmMast.Price2 = CDbl(txtorprice2.Text)
        If Val(txtorprice3.Text) = 0 Then txtorprice3.Text = Format(0, lnumformat)
        _objItmMast.Price3 = CDbl(txtorprice3.Text)
        If Val(txtorprice4.Text) = 0 Then txtorprice4.Text = Format(0, lnumformat)
        _objItmMast.Price4 = CDbl(txtorprice4.Text)
        If Val(txtorprice5.Text) = 0 Then txtorprice5.Text = Format(0, lnumformat)
        _objItmMast.Price5 = CDbl(txtorprice5.Text)
        If Val(txtorprice6.Text) = 0 Then txtorprice6.Text = Format(0, lnumformat)
        _objItmMast.Price6 = CDbl(txtorprice6.Text)
        If Val(txtorprice7.Text) = 0 Then txtorprice7.Text = Format(0, lnumformat)
        _objItmMast.Price7 = CDbl(txtorprice7.Text)
        If Val(txtorprice8.Text) = 0 Then txtorprice8.Text = Format(0, lnumformat)
        _objItmMast.Price8 = CDbl(txtorprice8.Text)
        If Val(txtorprice9.Text) = 0 Then txtorprice9.Text = Format(0, lnumformat)
        _objItmMast.Price9 = CDbl(txtorprice9.Text)
        If Val(txtorprice10.Text) = 0 Then txtorprice10.Text = Format(0, lnumformat)
        _objItmMast.Price10 = CDbl(txtorprice10.Text)
        _objItmMast.itemlistorder = Val(txtitemord.Text)
    End Sub
    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        If Val(btnAddNext.Tag) = 0 And Val(btnModify.Tag) = 0 Then
            MsgBox("This user do not have rights to update", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If Not chkFields() Then Exit Sub
        loadWaite(2)
    End Sub
    Private Sub saveProduct()
        setValue()
        Dim itemid As Long
        itemid = _objItmMast._saveItemMast()
        _objItmMast.ItemId = itemid
        _objItmMast.saveItemProperties()
        Dim str As String
        str = "Update Invitm Set gender='" & cmbgender.Text & "',agegroup='" & cmbagegroup.Text & "'," & _
                                            "color='" & txtcolor.Text & "',itmsize='" & txtsize.Text & "',otherDescr='" & MkDbSrchStr(txtdescription.Text) & "'" & _
                                           " Where  itemid=" & itemid
        str = str & " update InvItmPropertiesTb set showinweb=" & IIf(chkshowinweb.Checked, 1, 0) & ",ishidefrommilksales=" & IIf(ckbxhidemilk.Checked, 1, 0) & " where itemid=" & itemid
        _objcmnbLayer._saveDatawithOutParm(str)
        If isRawMetChange Then
            setRawmeterial(itemid, False)
        End If
        If enableWoodSale Then
            saveWoodQtyDisc(itemid)
        End If
        saveImage(itemid)
        LocknUnLockControls(True)
        EnaDisBtn("UPDATE")
        Dim UItmCode As String
        UItmCode = txtCode.Text
        'If IsModi Then
        '    tbitem.SelectedIndex = 1
        '    clearControls()
        'Else
        '    clearControls()
        '    'ShowCurrentRecord()
        'End If
        PreitemId = itemid
        ShowCurrentRecord()
        isnewItemcreated = True
        btnrefresh_Click(btnrefresh, New System.EventArgs)
        fwait.Close()
        MsgBox("Item Successfully Saved", MsgBoxStyle.Information)
        'If BaseID <> 0 Then RfrshSingle(BaseID)

        btnNew.Focus()
        RaiseEvent updateItemCode(UItmCode)
    End Sub
    Private Sub saveWoodQtyDisc(ByVal itemid As Long)
        Dim i As Integer
        _objcmnbLayer._saveDatawithOutParm("Delete from WoodQtyDiscountTb where itemid=" & itemid)
        For i = 0 To grdwoodQtyDisc.RowCount - 1
            If Val(grdwoodQtyDisc.Item(0, i).Value & "") > 0 And Val(grdwoodQtyDisc.Item(1, i).Value & "") > 0 Then
                _objcmnbLayer._saveDatawithOutParm("Insert into WoodQtyDiscountTb (itemid,qty,dqty) values(" & itemid & "," & CDbl(grdwoodQtyDisc.Item(0, i).Value) & "," & CDbl(grdwoodQtyDisc.Item(1, i).Value) & ")")
            End If
        Next
    End Sub
    Private Sub loadWoodQtyDisc()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("SELECT qty,dqty from WoodQtyDiscountTb where itemid=" & Val(PreitemId))
        Dim tnumformat As String
        Dim Ndec As Integer = 3
        tnumformat = "#,##0" & IIf(Ndec = 0, "", "." & Strings.StrDup(Ndec, "0"))
        'chgbyprg = True
        For i = 0 To dt.Rows.Count - 1
            With grdwoodQtyDisc
                .Rows.Add()
                .Item(0, i).Value = Format(dt(i)("qty"), tnumformat)
                .Item(1, i).Value = Format(dt(i)("dqty"), tnumformat)
            End With
        Next
        grdwoodQtyDisc.Columns(0).HeaderText = "Qty Upto [" & cmbUnit.Text & "]"
        'chgbyprg = False
    End Sub
    Private Sub setWoodQtyDiscount()
        chgbyprg = True
        With grdwoodQtyDisc
            SetGridEditProperty(grdwoodQtyDisc)
            .ColumnCount = 2
            .Columns(0).HeaderText = "Qty Upto [" & cmbUnit.Text & "]"
            .Columns(0).Width = 150
            .Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(0).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(1).HeaderText = "Disc Qty [KG]"
            .Columns(1).Width = 150
            .Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(1).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        End With
        chgbyprg = False
    End Sub
    Private Sub SetGridHeadPackDet()

        With grdpackDet
            .ReadOnly = True
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True

            .ColumnCount = 11

            .ColumnHeadersHeight = 100
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("arial", 8.5!, FontStyle.Regular)


            .Columns(PackCode).HeaderText = "Pack Code"
            .Columns(PackCode).Width = 75
            .Columns(PackCode).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(PackUnit).HeaderText = "Pack Unit"
            .Columns(PackUnit).Width = 75
            .Columns(PackUnit).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstBarcode).Resizable = DataGridViewTriState.False


            .Columns(OpenQty).HeaderText = "Open Qty"
            .Columns(OpenQty).Width = (Me.Width / 2) * 14 / 100 '50
            .Columns(OpenQty).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(OpenQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(OpenQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(OpenQty).Visible = False
            '.Columns(OpenQty).DefaultCellStyle.Font = New System.Drawing.Font("arial", 15.0!, FontStyle.Bold)


            .Columns(OpenCost).HeaderText = "Open Cost"
            .Columns(OpenCost).Width = (Me.Width / 2) * 14 / 100 '50
            .Columns(OpenCost).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(OpenCost).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(OpenCost).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(OpenCost).Visible = False

            .Columns(costAvg).HeaderText = "Cost Avg."
            .Columns(costAvg).Width = (Me.Width / 2) * 14 / 100 '50
            .Columns(costAvg).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(costAvg).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(costAvg).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(costAvg).DefaultCellStyle.Font = New System.Drawing.Font("arial", 10.0!, FontStyle.Bold)
            .Columns(costAvg).Visible = False

            .Columns(LastPCost).HeaderText = "Last P.Cost"
            .Columns(LastPCost).Width = (Me.Width / 2) * 14 / 100 '50
            .Columns(LastPCost).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(LastPCost).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(LastPCost).DefaultCellStyle.Font = New System.Drawing.Font("arial", 10.0!, FontStyle.Bold)
            .Columns(LastPCost).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(LastPCost).Visible = False

            .Columns(QIH).HeaderText = "Quantity"
            .Columns(QIH).Width = (Me.Width / 2) * 14 / 100 '50
            .Columns(QIH).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(QIH).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(QIH).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(QIH).DefaultCellStyle.Font = New System.Drawing.Font("arial", 10.0!, FontStyle.Bold)
            .Columns(QIH).DefaultCellStyle.ForeColor = Color.Red
            .Columns(QIH).DefaultCellStyle.BackColor = Color.Lime

            .Columns(RcvdQty).HeaderText = "Receive Qty"
            .Columns(RcvdQty).Width = (Me.Width / 2) * 14 / 100 '50
            .Columns(RcvdQty).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(RcvdQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(RcvdQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(RcvdQty).Visible = False
            '.Columns(RcvdQty).DefaultCellStyle.Font = New System.Drawing.Font("arial", 15.0!, FontStyle.Bold)

            .Columns(IssdQty).HeaderText = "Issued Qty"
            .Columns(IssdQty).Width = (Me.Width / 2) * 14 / 100 '50
            .Columns(IssdQty).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(IssdQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(IssdQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(IssdQty).Visible = False
            '.Columns(IssdQty).DefaultCellStyle.Font = New System.Drawing.Font("arial", 15.0!, FontStyle.Bold)

            .Columns(PUpD).HeaderText = "pUp"
            .Columns(PUpD).Visible = False

            .Columns(PDownD).HeaderText = "pDown"
            .Columns(PDownD).Visible = False

        End With
    End Sub
    Private Sub FillGrid()
        SetGridHeadPackDet()
        Dim i As Integer
        Dim PackFra As Double
        With grdpackDet
            If .Rows.Count = 0 Then .Rows.Add(3)
            For i = 0 To 2

                If i = 0 Then
                    .Item(PackCode, i).Value = "Small Unit" 'Small Unit
                    If _vDtable.Rows.Count > 0 Then
                        .Item(PackUnit, i).Value = _vDtable(0)("Unit")
                    Else
                        .Item(PackUnit, i).Value = ""
                    End If
                    .Item(PUpD, i).Value = 1
                    .Item(PDownD, i).Value = 1
                ElseIf i = 1 Then
                    .Item(PackCode, i).Value = "Medium Unit" 'Medium Unit
                    If _vDtable.Rows.Count > 0 Then
                        .Item(PackUnit, i).Value = _vDtable(0)("P1Unit")
                        .Item(PDownD, i).Value = _vDtable(0)("P1Fra")
                    Else
                        .Item(PackUnit, i).Value = ""
                        .Item(PDownD, i).Value = "1"
                    End If
                    .Item(PUpD, i).Value = 1
                Else
                    If _vDtable.Rows.Count > 0 Then
                        .Item(PackUnit, i).Value = _vDtable(0)("P2Unit")
                        .Item(PDownD, i).Value = _vDtable(0)("P2Fra")
                    Else
                        .Item(PackUnit, i).Value = ""
                        .Item(PDownD, i).Value = 1
                    End If
                    .Item(PackCode, i).Value = "Upper Unit" 'Upper Unit
                    .Item(PUpD, i).Value = 1
                End If
                If Val(grdpackDet.Item(PDownD, i).Value & "") = 0 Then grdpackDet.Item(PDownD, i).Value = 1
                PackFra = Val(grdpackDet.Item(PUpD, i).Value) / Val(grdpackDet.Item(PDownD, i).Value)
                If Val(numOpnQty.Text) > 0 Then
                    .Item(OpenQty, i).Value = CDbl(numOpnQty.Text) * PackFra
                Else
                    .Item(OpenQty, i).Value = "0.00"
                End If
                If Val(numopnCost.Text) > 0 Then
                    .Item(OpenCost, i).Value = CDbl(numopnCost.Text) * Val(grdpackDet.Item(PDownD, i).Value)
                Else
                    .Item(OpenCost, i).Value = "0.00"
                End If
                If Val(lblQIH.Text) > 0 Then
                    .Item(QIH, i).Value = CDbl(lblQIH.Text) * PackFra
                Else
                    .Item(QIH, i).Value = "0.00"
                End If
                If Val(lblcostAvg.Text) > 0 Then
                    .Item(costAvg, i).Value = CDbl(lblcostAvg.Text) * Val(grdpackDet.Item(PDownD, i).Value)
                Else
                    .Item(costAvg, i).Value = "0.00"
                End If
                If Val(lbllpcost.Text) > 0 Then
                    .Item(LastPCost, i).Value = CDbl(lbllpcost.Text) * Val(grdpackDet.Item(PDownD, i).Value)
                Else
                    .Item(LastPCost, i).Value = "0.00"
                End If
                If Val(lblrcvd.Text) > 0 Then
                    .Item(RcvdQty, i).Value = CDbl(lblrcvd.Text) * PackFra
                Else
                    .Item(RcvdQty, i).Value = "0.00"
                End If
                If Val(lblissd.Text) > 0 Then
                    .Item(IssdQty, i).Value = CDbl(lblissd.Text) * PackFra
                Else
                    .Item(IssdQty, i).Value = "0.00"
                End If
            Next
        End With

    End Sub

    Private Sub txtmrp_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtmrp.KeyDown
        If e.KeyCode = Keys.Return Then
            txtpoint.Focus()
        End If
    End Sub

    Private Sub txtpriceWtax_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpriceWtax.KeyDown
        If e.KeyCode = Keys.Return Then
            txtws.Focus()
        End If

    End Sub

    Private Sub txtwmcalculation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtwmcalculation.KeyDown
        If e.KeyCode = Keys.Return Then
            BtnUpdate.Focus()
        End If

    End Sub


    Private Sub numMinQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles numMinQty.KeyPress, numopnCost.KeyPress, numOpnQty.KeyPress, _
                                                                                                                      numunitprice.KeyPress, txtws.KeyPress, txtmrp.KeyPress, _
                                                                                                                      txtpriceWtax.KeyPress, txtsecondprice.KeyPress, txtadditionalcess.KeyPress, _
                                                                                                                      txtwmcalculation.KeyPress
        Dim myctr As TextBox
        myctr = sender
        If myctr.ReadOnly Then Exit Sub
        NumericTextOnKeypress(sender, e, chgbyprgAmt, numFormat)
    End Sub

    Private Sub txtmitemcode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtmitemcode.KeyDown

    End Sub

    Private Sub txtsupersed_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtsupersed.KeyDown
        lstKey = e.KeyCode
        Dim myCtrl As TextBox = sender
        If e.KeyCode = Keys.Return Then
            'btnaddsupersed.Focus()
            addsupersed()
        ElseIf e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveUp(myCtrl.Text)
                    Exit Sub
                End If
            End If

        ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.PageDown Then
            If Not fMList Is Nothing Then
                If fMList.Visible Then
                    fMList.MoveDown(myCtrl.Text)
                    Exit Sub
                End If
            End If
        End If

    End Sub

    Private Sub txtCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescr.TextChanged, txtCode.TextChanged, _
                                                                                                        numOpnQty.TextChanged, numopnCost.TextChanged, _
                                                                                                        numMinQty.TextChanged, numunitprice.TextChanged, _
                                                                                                        txtmake.TextChanged, txtmodel.TextChanged, _
                                                                                                        txtmrp.TextChanged, txtmitemcode.TextChanged, txtsupersed.TextChanged, txtorprice1.TextChanged, _
                                                                                                        txtorprice2.TextChanged, txtorprice3.TextChanged, txtorprice4.TextChanged, txtorprice5.TextChanged, _
                                                                                                      txtorprice6.TextChanged, txtorprice7.TextChanged, txtorprice8.TextChanged, txtorprice9.TextChanged, txtorprice10.TextChanged, txtitemord.TextChanged



        If chgbyprg Then Exit Sub
        Dim myCtrl As Control = sender
        Select Case myCtrl.Name
            Case "txtCode"
                _srchTxtId = 1
                _srchOnce = False
                ShowFmlist(sender)
            Case "txtDescr"
                _srchTxtId = 2
                _srchOnce = False
                ShowFmlist(sender)
            Case "txtsupersed"
                _srchTxtId = 3
                _srchOnce = False
                ShowFmlist(sender)
            Case "numunitprice"
                If Val(numunitprice.Text) > 0 Then
                    numunitprice.Tag = CDbl(numunitprice.Text)
                Else
                    numunitprice.Tag = 0
                End If
                chgamt = True
            Case "txtorprice1", "txtorprice2", "txtorprice3", "txtorprice4", "txtorprice5", "txtorprice6", "txtorprice7", "txtorprice8", "txtorprice9", "txtorprice10"
                chgamt = True
        End Select
        If myCtrl.Name <> "txtsupersed" Then BtnUpdate.Enabled = True
    End Sub
    Private Sub ShowFmlist(ByVal myCtrl As Control)
        If chgbyprg Then Exit Sub
        MyActiveControl = myCtrl
        If Not _srchOnce Then
            chgbyprg = True
            If fMList Is Nothing Then
                fMList = New Mlistfrm
            End If
            Dim PopupLoc As Point
            Dim x As Integer = Me.Width - fMList.Width - 100
            Dim y As Integer = Me.Height - fMList.Height - 100
            PopupLoc = New Point(x, y)
            If fMList.Visible = False Then
                Select Case _srchTxtId
                    Case 1, 3
                        SetFmlist(fMList, 26)
                    Case 2
                        SetFmlist(fMList, 31)
                End Select
                fMList.loc = PopupLoc
                If IsFromEnqry Then
                    fMList.Show(Me)
                Else
                    fMList.Show(fMainForm)
                End If

                fMList.Visible = True
            End If
        End If
        Select Case _srchTxtId
            Case 1   'Item Code
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 0
                fMList_doFocus()
                fMList.Search(txtCode.Text)
                fMList.AssignList(txtCode, lstKey, chgbyprg)
            Case 2   'Item Name
                fMList.SearchIndex = 1
                fMList.SearchIndexDescr = 1
                fMList_doFocus()
                fMList.Search(txtDescr.Text)
                fMList.AssignList(txtDescr, lstKey, chgbyprg)
            Case 3   'Item code for supersed
                fMList.SearchIndex = 0
                fMList.SearchIndexDescr = 0
                fMList_doFocus()
                fMList.Search(txtsupersed.Text)
                fMList.AssignList(txtsupersed, lstKey, chgbyprg)
        End Select
        _srchOnce = True
        chgbyprg = False
    End Sub

    Private Sub fMList_doClose() Handles fMList.doClose
        fMList = Nothing
    End Sub

    Private Sub fMList_doFocus() Handles fMList.doFocus
        If TypeOf (MyActiveControl) Is TextBox Then
            MyActiveControl.focus()
        End If
    End Sub

    Private Sub fMList_doSelect(ByVal ItmFlds() As String) Handles fMList.doSelect
        chgbyprg = True
        Select Case _srchTxtId
            Case 3
                txtsupersed.Text = ItmFlds(0)
        End Select
        chgbyprg = False
    End Sub
    Private Sub grdLevel_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLevel.CellClick
        If e.ColumnIndex <> 1 Then Exit Sub
        grdLevel.BeginEdit(True)
        BtnUpdate.Enabled = True
    End Sub


    Private Sub grdLevel_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdLevel.Enter
        If grdLevel.CurrentCell.ColumnIndex = 1 Then
            grdLevel.BeginEdit(True)
        End If
    End Sub
    Private Sub lditemdetails()
        Dim creditpriceCap As String
        If enableCreditPrice Then
            creditpriceCap = "Tax Price-CR"
        Else
            creditpriceCap = "Tax Price-DP"
        End If
        Dim manufacturingFlag As String = ""
        If cmbitemtype.SelectedIndex = 1 Then
            manufacturingFlag = " And isnull(ismanufacturing,0)=1  And isnull(ishide,0)=0"
        ElseIf cmbitemtype.SelectedIndex = 2 Then
            manufacturingFlag = " And isnull(ismanufacturing,0)<>1  And isnull(ishide,0)=0"
        ElseIf cmbitemtype.SelectedIndex = 3 Then
            manufacturingFlag = " And isnull(ishide,0)=1"
        Else
            manufacturingFlag = " And isnull(ishide,0)=0"
        End If
        _gridItems = _objcmnbLayer._fldDatatable("SELECT [Item Code],Description,Unit,QIH,CostAvg,UnitPrice," & _
                                                 "(((isnull(IGST,0)+ISNULL(vat,0)+isnull(rgcess,0))*UnitPrice)/100)+UnitPrice+isnull(additionalcess,0) [Tax Price],mrp," & _
                                                 "(((isnull(IGST,0)+ISNULL(vat,0)+isnull(rgcess,0))*secondPrice)/100)+secondPrice+isnull(additionalcess,0)  [" & creditpriceCap & "]," & _
                                                 "(((isnull(IGST,0)+ISNULL(vat,0)+isnull(rgcess,0))*UnitPriceWS)/100)+UnitPriceWS+isnull(additionalcess,0)  [Tax Price-WS]" & _
                                                 ",LastPurchCost," & _
                                                 "RcvdQty,IssdQty,opQty,opcost,Rack,invitm.itemid FROM InvItm " & _
                                                  "left join InvItmPropertiesTb on InvItmPropertiesTb.itemid=invitm.itemid " & _
                                                 " LEFT JOIN GSTTb ON GSTTb.HSNCode=InvItm.HSNCode " & _
                                                 " LEFT JOIN VatMasterTb ON VatMasterTb.vatid=InvItm.vatid " & _
                                                 " LEFT JOIN (SELECT vatcode rgcode,vatid rgid,vat rgcess FROM VatMasterTb)regularcess ON regularcess.rgid=InvItm.regularcessid  " & _
                                                 "where itemCategory not in ('room','')" & manufacturingFlag)
        If txtSeq.Text <> "" Then
            grdItem.DataSource = SearchGrid(_gridItems, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
        Else
            grdItem.DataSource = _gridItems
        End If
        SetGridHead()
        setComboGrid()
    End Sub
    Private Sub setComboGrid()
        chgbyprg = True
        cmbOrder.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To grdItem.ColumnCount - 1
            cmbOrder.Items.Add(grdItem.Columns(i).HeaderText)
        Next
        If cmbOrder.Items.Count > 0 Then
            If searchByCodeInInventory Then
                cmbOrder.SelectedIndex = 0
            Else
                cmbOrder.SelectedIndex = 1
            End If
        End If
        chgbyprg = False
    End Sub
    Private Sub SetGridHead()
        With grdItem
            SetGridProperty(grdItem)

            .Columns(ItemCode).HeaderText = "Item Code"
            .Columns(ItemCode).Width = 150

            .Columns(TrDescr).HeaderText = "Description"
            .Columns(TrDescr).Width = 300
            .Columns(TrDescr).DefaultCellStyle.BackColor = Color.LightSteelBlue
            '.Columns(TrDescr).SortMode = DataGridViewColumnSortMode.NotSortable

            .Columns(Unit).HeaderText = "Unit"
            .Columns(Unit).Width = 50
            '.Columns(Unit).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(Unit).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .Columns(QtyInHand).HeaderText = "QIH"
            .Columns(QtyInHand).Width = 75
            '.Columns(QtyInHand).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ConstUPrice).Resizable = DataGridViewTriState.False
            .Columns(QtyInHand).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(QtyInHand).DefaultCellStyle.BackColor = Color.LightYellow
            .Columns(QtyInHand).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(CostAverage).HeaderText = "CostAvg"
            .Columns(CostAverage).Width = 75
            .Columns(CostAverage).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(CostAverage).DefaultCellStyle.BackColor = Color.LightPink
            .Columns(CostAverage).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(CostAverage).Visible = getRight(200, CurrentUser)

            .Columns(ConstUnitPrice).HeaderText = "Unit Price"
            .Columns(ConstUnitPrice).Width = 85
            .Columns(ConstUnitPrice).DefaultCellStyle.Font = New System.Drawing.Font("arial", 10.0!, FontStyle.Bold)
            .Columns(ConstUnitPrice).DefaultCellStyle.ForeColor = Color.Red
            .Columns(ConstUnitPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstUnitPrice).DefaultCellStyle.BackColor = Color.Lime
            .Columns(ConstUnitPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(ConstTaxPrice).HeaderText = "Tax Price"
            .Columns(ConstTaxPrice).Width = 85
            .Columns(ConstTaxPrice).DefaultCellStyle.Font = New System.Drawing.Font("arial", 10.0!, FontStyle.Bold)
            .Columns(ConstTaxPrice).DefaultCellStyle.ForeColor = Color.Red
            .Columns(ConstTaxPrice).DefaultCellStyle.Format = "N" & NoOfDecimal
            '.Columns(ConstTaxPrice).DefaultCellStyle.BackColor = Color.Lime
            .Columns(ConstTaxPrice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(ConstMrp).HeaderText = "MRP"
            .Columns(ConstMrp).Width = 85
            .Columns(ConstMrp).DefaultCellStyle.Font = New System.Drawing.Font("arial", 10.0!, FontStyle.Bold)
            .Columns(ConstMrp).DefaultCellStyle.ForeColor = Color.Red
            .Columns(ConstMrp).DefaultCellStyle.Format = "N" & NoOfDecimal
            '.Columns(ConstTaxPrice).DefaultCellStyle.BackColor = Color.Lime
            .Columns(ConstMrp).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(Constsecondprice).Width = 100
            .Columns(Constsecondprice).DefaultCellStyle.Font = New System.Drawing.Font("arial", 10.0!, FontStyle.Bold)
            .Columns(Constsecondprice).DefaultCellStyle.ForeColor = Color.Red
            .Columns(Constsecondprice).DefaultCellStyle.Format = "N" & NoOfDecimal
            '.Columns(ConstTaxPrice).DefaultCellStyle.BackColor = Color.Lime
            .Columns(Constsecondprice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(Constwsprice).Width = 110
            .Columns(Constwsprice).DefaultCellStyle.Font = New System.Drawing.Font("arial", 10.0!, FontStyle.Bold)
            .Columns(Constwsprice).DefaultCellStyle.ForeColor = Color.Red
            .Columns(Constwsprice).DefaultCellStyle.Format = "N" & NoOfDecimal
            '.Columns(ConstTaxPrice).DefaultCellStyle.BackColor = Color.Lime
            .Columns(Constwsprice).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(Constlcost).HeaderText = "Last P.Cost"
            .Columns(Constlcost).Width = 100
            .Columns(Constlcost).DefaultCellStyle.Font = New System.Drawing.Font("arial", 10.0!, FontStyle.Bold)
            .Columns(Constlcost).DefaultCellStyle.ForeColor = Color.Red
            .Columns(Constlcost).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(Constlcost).DefaultCellStyle.BackColor = Color.Lime
            .Columns(Constlcost).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(Constlcost).Visible = getRight(200, CurrentUser)



            .Columns(intReceivedQty).HeaderText = "Rec. Qty"
            .Columns(intReceivedQty).Width = 90
            .Columns(intReceivedQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(intReceivedQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(intIssuedQty).HeaderText = "Issu. Qty"
            .Columns(intIssuedQty).Width = 90
            .Columns(intIssuedQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(intIssuedQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            .Columns(OpnQty).HeaderText = "Opn. Qty"
            .Columns(OpnQty).Width = 80
            .Columns(OpnQty).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(OpnQty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(OpnCost).HeaderText = "Opn.Cost "
            .Columns(OpnCost).Width = 75
            .Columns(OpnCost).SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns(OpnCost).DefaultCellStyle.BackColor = Color.LightGreen
            '.Columns(ConstUPrice).Resizable = DataGridViewTriState.False
            .Columns(OpnCost).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(OpnCost).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(OpnCost).Visible = getRight(200, CurrentUser)


            .Columns(Rack).HeaderText = "Rack"
            .Columns(Rack).Width = 50

            .Columns(ConstItemId).HeaderText = "ItemId"
            .Columns(ConstItemId).Visible = False

        End With
    End Sub

    Private Sub grdItem_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.CellClick
        If fHistory Is Nothing Then Exit Sub
        If fHistory.Visible Then btnloadtr_Click(btnloadtr, New System.EventArgs)
    End Sub

    Private Sub txtSeq_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSeq.TextChanged
        grdItem.DataSource = SearchGrid(_gridItems, Trim(txtSeq.Text), cmbOrder.SelectedIndex, Not chkSearch.Checked)
    End Sub

    Private Sub grdItem_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.CellDoubleClick
        loadWaite(3)
    End Sub
    Public Sub loadFromTransactions()
        'clearControls()
        'txtCode.Tag = Val(grdItem.Item(ConstItemId, grdItem.CurrentCell.RowIndex).Value)
        ShowCurrentRecord()
        tbitem.SelectedIndex = 0
        btnModify.Focus()
    End Sub

    Private Sub grdpackDet_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdpackDet.DoubleClick
        Try
            Dim fPck As New CreatePackList
            If grdpackDet.Item(0, grdpackDet.CurrentCell.RowIndex).Value <> "BU" Then
                If Val(PreitemId) = 0 Then MsgBox("Select item to set pack details", MsgBoxStyle.Exclamation) : Exit Sub
                fPck.cmbUnit.Tag = Val(PreitemId)
                fPck.lblunit.Tag = Trim(grdpackDet.Item(1, grdpackDet.CurrentCell.RowIndex).Value & "")
                fPck.lblbu.Text = cmbUnit.Text
                fPck.lblbu.Tag = Trim(grdpackDet.Item(0, grdpackDet.CurrentCell.RowIndex).Value & "")
                fPck.txtbu.Text = Val(grdpackDet.Item(PDownD, grdpackDet.CurrentCell.RowIndex).Value & "")
                fPck.ShowDialog()
                _vDtable = _objcmnbLayer._fldDatatable("SELECT * FROM InvItm WHERE Itemid=" & Val(PreitemId))
                FillGrid()
            Else
                MsgBox("You can't set denomination to Basic Unit", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        lditemdetails()
    End Sub
    Private Sub btncost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncost.Click
        Dim fCost As New ItemCostFrm
        If tbitem.SelectedIndex = 1 Then
            If Val(grdItem.Item(ConstItemId, grdItem.CurrentCell.RowIndex).Value) > 0 Then
                fCost.isSupp = True
                fCost.lblName.Text = "Cost Settings For Item :" & grdItem.Item(TrDescr, grdItem.CurrentCell.RowIndex).Value
                fCost.lblName.Tag = Val(grdItem.Item(ConstItemId, grdItem.CurrentCell.RowIndex).Value)
                fCost.ShowDialog()
            End If
        End If

    End Sub

    Private Sub setHistory(ByVal ItemId As Long, ByVal strCode As String, Optional ByVal PartyId As Long = 0, Optional ByVal strType As String = "")
        If fHistory Is Nothing Then
            fHistory = New SelectHistory
        End If
        With fHistory
            If strType <> "" Then
                .strType = strType
            Else
                Exit Sub
            End If
            .Itemid = ItemId
            .ItemCode = strCode
            .PartyId = PartyId
            .jobcode = ""
            If fHistory.Visible Then
                .setEnquiryLoad()
            Else
                .Show()
                .Top = Me.Top + Screen.PrimaryScreen.WorkingArea.Height - .Height + 20
            End If
        End With
    End Sub

    Private Sub btnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnload.Click
        With grdItem
            If Val(.Item(ConstItemId, .CurrentRow.Index).Value) > 0 Then
                If rdopurcahseReturn.Checked Then
                    setHistory(Val(.Item(ConstItemId, .CurrentRow.Index).Value), .Item(ItemCode, .CurrentRow.Index).Value.ToString, 0, "PO")
                ElseIf rdopurchase.Checked Then
                    setHistory(Val(.Item(ConstItemId, .CurrentRow.Index).Value), .Item(ItemCode, .CurrentRow.Index).Value.ToString, 0, "IP")
                ElseIf rdosales.Checked Then
                    setHistory(Val(.Item(ConstItemId, .CurrentRow.Index).Value), .Item(ItemCode, .CurrentRow.Index).Value.ToString, 0, "IS")
                ElseIf rdosalesreturn.Checked Then
                    setHistory(Val(.Item(ConstItemId, .CurrentRow.Index).Value), .Item(ItemCode, .CurrentRow.Index).Value.ToString, 0, "SR")
                End If
            End If
        End With
    End Sub

    Private Sub rdoLocalPurchaseOrder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdopurcahseReturn.CheckedChanged

    End Sub

    Private Sub btnloadtr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnloadtr.Click
        With grdItem
            If rdopurcahseReturn.Checked Then
                setHistory(Val(.Item(ConstItemId, .CurrentRow.Index).Value), .Item(ItemCode, .CurrentRow.Index).Value.ToString, 0, "PR")
            ElseIf rdopurchase.Checked Then
                setHistory(Val(.Item(ConstItemId, .CurrentRow.Index).Value), .Item(ItemCode, .CurrentRow.Index).Value.ToString, 0, "IP")
            ElseIf rdosalesreturn.Checked Then
                setHistory(Val(.Item(ConstItemId, .CurrentRow.Index).Value), .Item(ItemCode, .CurrentRow.Index).Value.ToString, 0, "SR")
            ElseIf rdoPin.Checked Then
                setHistory(Val(.Item(ConstItemId, .CurrentRow.Index).Value), .Item(ItemCode, .CurrentRow.Index).Value.ToString, 0, "MI")
            ElseIf rdoPout.Checked Then
                setHistory(Val(.Item(ConstItemId, .CurrentRow.Index).Value), .Item(ItemCode, .CurrentRow.Index).Value.ToString, 0, "MO")
            Else
                setHistory(Val(.Item(ConstItemId, .CurrentRow.Index).Value), .Item(ItemCode, .CurrentRow.Index).Value.ToString, 0, "IS")
            End If
        End With
    End Sub


    Private Sub rdopurchase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdopurchase.Click, rdopurcahseReturn.Click, rdosales.Click, rdosalesreturn.Click, rdoPin.Click, rdoPout.Click
        If Not fHistory Is Nothing Then
            If fHistory.Visible Then
                btnloadtr_Click(btnloadtr, New System.EventArgs)
            End If
        End If
    End Sub


    Private Sub fHistory_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fHistory.FormClosed
        fHistory = Nothing
    End Sub

    Private Sub cmbtax_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbtax.KeyDown, cmbregularcess.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub


    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        If btnNew.Text = "Undo" Then
            BtnUpdate.Enabled = True
        End If
        getUnitFraction()
    End Sub

    Private Sub chkserial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkserial.Click, chkdualsim.Click, chkmanufacturing.Click
        If btnNew.Text = "Undo" Then
            BtnUpdate.Enabled = True
        End If

    End Sub

    Private Sub bntrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntrefresh.Click
        btnrefresh.Tag = 1
        loadWaite(1)
    End Sub

    Private Sub txtpriceWtax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpriceWtax.TextChanged
        If chgbyprg Then Exit Sub
        If btnNew.Text = "Undo" Then
            BtnUpdate.Enabled = True
        End If
        chgamt = True
    End Sub

    Private Sub calculateTaxFromUnitPrice(ByVal fromUnitpice As Boolean, Optional ByVal priceType As Integer = 0)
        If Not chgamt Then Exit Sub
        chgbyprgAmt = True
        Dim price As Double
        If Val(txtpriceWtax.Tag & "") = 0 Then txtpriceWtax.Tag = 0
        If Val(cmbtax.Tag & "") = 0 Then cmbtax.Tag = 0
        If Val(cmbregularcess.Tag & "") = 0 Then cmbregularcess.Tag = 0
        If Val(txtadditionalcess.Text) = 0 Then txtadditionalcess.Text = 0
        Dim disableFc As Boolean
        If cessenddate <= DateValue(Date.Now) And enableGCC = False Then disableFc = True
        Dim totalTax As Double
        If disableFc Then cmbtax.Tag = 0
        totalTax = CDbl(txtpriceWtax.Tag) + IIf(disableFc = False, CDbl(cmbtax.Tag), 0) + CDbl(cmbregularcess.Tag)
        If Not fromUnitpice Then
            If priceType = 0 Then
                price = CDbl(txtpriceWtax.Text) - CDbl(txtadditionalcess.Text)
                numunitprice.Tag = ((CDbl(price) * 100) / (CDbl(totalTax) + 100))
                numunitprice.Text = Format(CDbl(numunitprice.Tag), numFormat)
            ElseIf priceType = 1 Then
                If Val(txtsecondprice.Text) = 0 Then txtsecondprice.Text = 0
                If Val(txtsecondprice.Text) > 0 Then
                    price = CDbl(txtsecondprice.Text) - CDbl(txtadditionalcess.Text)
                    txtsecondprice.Tag = ((CDbl(price) * 100) / (CDbl(totalTax) + 100))
                    txtsecondprice.Text = Format(CDbl(txtsecondprice.Tag), numFormat)
                    lbldptax.Text = Format((CDbl(txtsecondprice.Tag) + ((CDbl(txtsecondprice.Tag) * CDbl(totalTax)) / 100)) + CDbl(txtadditionalcess.Text), numFormat)
                Else
                    price = 0
                    txtsecondprice.Tag = 0
                    lbldptax.Text = 0
                    Format(0, numFormat)
                End If
            ElseIf priceType = 2 Then
                If Val(txtws.Text) = 0 Then txtws.Text = 0
                If Val(txtws.Text) > 0 Then
                    price = CDbl(txtws.Text) - CDbl(txtadditionalcess.Text)
                    txtws.Tag = ((CDbl(price) * 100) / (CDbl(totalTax) + 100))
                    txtws.Text = Format(CDbl(txtws.Tag), numFormat)
                    lblwstax.Text = Format((CDbl(txtws.Tag) + ((CDbl(txtws.Tag) * CDbl(totalTax)) / 100)) + CDbl(txtadditionalcess.Text), numFormat)
                Else
                    lblwstax.Text = Format(0, numFormat)
                    txtws.Tag = 0
                    price = 0
                End If
            ElseIf priceType = 3 Then
                If Val(txtorprice1.Text) = 0 Then txtorprice1.Text = 0
                If Val(txtorprice1.Text) > 0 Then
                    price = CDbl(txtorprice1.Text) - CDbl(txtadditionalcess.Text)
                    txtorprice1.Tag = ((CDbl(price) * 100) / (CDbl(totalTax) + 100))
                    txtorprice1.Text = Format(CDbl(txtorprice1.Tag), numFormat)
                    lbp1tax.Text = Format((CDbl(txtorprice1.Tag) + ((CDbl(txtorprice1.Tag) * CDbl(totalTax)) / 100)) + CDbl(txtadditionalcess.Text), numFormat)
                Else
                    lbp1tax.Text = Format(0, numFormat)
                    txtorprice1.Tag = 0
                    price = 0

                End If
            ElseIf priceType = 4 Then
                If Val(txtorprice2.Text) = 0 Then txtorprice2.Text = 0
                If Val(txtorprice2.Text) > 0 Then
                    price = CDbl(txtorprice2.Text) - CDbl(txtadditionalcess.Text)
                    txtorprice2.Tag = ((CDbl(price) * 100) / (CDbl(totalTax) + 100))
                    txtorprice2.Text = Format(CDbl(txtorprice2.Tag), numFormat)
                    lbp2tax.Text = Format((CDbl(txtorprice2.Tag) + ((CDbl(txtorprice2.Tag) * CDbl(totalTax)) / 100)) + CDbl(txtadditionalcess.Text), numFormat)
                Else
                    lbp2tax.Text = Format(0, numFormat)
                    txtorprice2.Tag = 0
                    price = 0

                End If
            ElseIf priceType = 5 Then
                If Val(txtorprice3.Text) = 0 Then txtorprice3.Text = 0
                If Val(txtorprice3.Text) > 0 Then
                    price = CDbl(txtorprice3.Text) - CDbl(txtadditionalcess.Text)
                    txtorprice3.Tag = ((CDbl(price) * 100) / (CDbl(totalTax) + 100))
                    txtorprice3.Text = Format(CDbl(txtorprice3.Tag), numFormat)
                    lbp3tax.Text = Format((CDbl(txtorprice3.Tag) + ((CDbl(txtorprice3.Tag) * CDbl(totalTax)) / 100)) + CDbl(txtadditionalcess.Text), numFormat)
                Else
                    lbp3tax.Text = Format(0, numFormat)
                    txtorprice3.Tag = 0
                    price = 0

                End If
            ElseIf priceType = 6 Then
                If Val(txtorprice4.Text) = 0 Then txtorprice4.Text = 0
                If Val(txtorprice4.Text) > 0 Then
                    price = CDbl(txtorprice4.Text) - CDbl(txtadditionalcess.Text)
                    txtorprice4.Tag = ((CDbl(price) * 100) / (CDbl(totalTax) + 100))
                    txtorprice4.Text = Format(CDbl(txtorprice4.Tag), numFormat)
                    lbp4tax.Text = Format((CDbl(txtorprice4.Tag) + ((CDbl(txtorprice4.Tag) * CDbl(totalTax)) / 100)) + CDbl(txtadditionalcess.Text), numFormat)
                Else
                    lbp4tax.Text = Format(0, numFormat)
                    txtorprice4.Tag = 0
                    price = 0

                End If
            ElseIf priceType = 7 Then
                If Val(txtorprice5.Text) = 0 Then txtorprice5.Text = 0
                If Val(txtorprice5.Text) > 0 Then
                    price = CDbl(txtorprice5.Text) - CDbl(txtadditionalcess.Text)
                    txtorprice5.Tag = ((CDbl(price) * 100) / (CDbl(totalTax) + 100))
                    txtorprice5.Text = Format(CDbl(txtorprice5.Tag), numFormat)
                    lbp5tax.Text = Format((CDbl(txtorprice5.Tag) + ((CDbl(txtorprice5.Tag) * CDbl(totalTax)) / 100)) + CDbl(txtadditionalcess.Text), numFormat)
                Else
                    lbp5tax.Text = Format(0, numFormat)
                    txtorprice5.Tag = 0
                    price = 0

                End If
            ElseIf priceType = 8 Then
                If Val(txtorprice6.Text) = 0 Then txtorprice6.Text = 0
                If Val(txtorprice6.Text) > 0 Then
                    price = CDbl(txtorprice6.Text) - CDbl(txtadditionalcess.Text)
                    txtorprice6.Tag = ((CDbl(price) * 100) / (CDbl(totalTax) + 100))
                    txtorprice6.Text = Format(CDbl(txtorprice6.Tag), numFormat)
                    lbp6tax.Text = Format((CDbl(txtorprice6.Tag) + ((CDbl(txtorprice6.Tag) * CDbl(totalTax)) / 100)) + CDbl(txtadditionalcess.Text), numFormat)
                Else
                    lbp6tax.Text = Format(0, numFormat)
                    txtorprice6.Tag = 0
                    price = 0

                End If
            ElseIf priceType = 9 Then
                If Val(txtorprice7.Text) = 0 Then txtorprice7.Text = 0
                If Val(txtorprice7.Text) > 0 Then
                    price = CDbl(txtorprice7.Text) - CDbl(txtadditionalcess.Text)
                    txtorprice7.Tag = ((CDbl(price) * 100) / (CDbl(totalTax) + 100))
                    txtorprice7.Text = Format(CDbl(txtorprice7.Tag), numFormat)
                    lbp7tax.Text = Format((CDbl(txtorprice7.Tag) + ((CDbl(txtorprice7.Tag) * CDbl(totalTax)) / 100)) + CDbl(txtadditionalcess.Text), numFormat)
                Else
                    lbp7tax.Text = Format(0, numFormat)
                    txtorprice7.Tag = 0
                    price = 0

                End If
            ElseIf priceType = 10 Then
                If Val(txtorprice8.Text) = 0 Then txtorprice8.Text = 0
                If Val(txtorprice8.Text) > 0 Then
                    price = CDbl(txtorprice8.Text) - CDbl(txtadditionalcess.Text)
                    txtorprice8.Tag = ((CDbl(price) * 100) / (CDbl(totalTax) + 100))
                    txtorprice8.Text = Format(CDbl(txtorprice8.Tag), numFormat)
                    lbp8tax.Text = Format((CDbl(txtorprice8.Tag) + ((CDbl(txtorprice8.Tag) * CDbl(totalTax)) / 100)) + CDbl(txtadditionalcess.Text), numFormat)
                Else
                    lbp8tax.Text = Format(0, numFormat)
                    txtorprice8.Tag = 0
                    price = 0

                End If
            ElseIf priceType = 11 Then
                If Val(txtorprice9.Text) = 0 Then txtorprice9.Text = 0
                If Val(txtorprice9.Text) > 0 Then
                    price = CDbl(txtorprice9.Text) - CDbl(txtadditionalcess.Text)
                    txtorprice9.Tag = ((CDbl(price) * 100) / (CDbl(totalTax) + 100))
                    txtorprice9.Text = Format(CDbl(txtorprice9.Tag), numFormat)
                    lbp9tax.Text = Format((CDbl(txtorprice9.Tag) + ((CDbl(txtorprice9.Tag) * CDbl(totalTax)) / 100)) + CDbl(txtadditionalcess.Text), numFormat)
                Else
                    lbp9tax.Text = Format(0, numFormat)
                    txtorprice9.Tag = 0
                    price = 0

                End If
            ElseIf priceType = 12 Then
                If Val(txtorprice10.Text) = 0 Then txtorprice10.Text = 0
                If Val(txtorprice10.Text) > 0 Then
                    price = CDbl(txtorprice10.Text) - CDbl(txtadditionalcess.Text)
                    txtorprice10.Tag = ((CDbl(price) * 100) / (CDbl(totalTax) + 100))
                    txtorprice10.Text = Format(CDbl(txtorprice10.Tag), numFormat)
                    lbp10tax.Text = Format((CDbl(txtorprice10.Tag) + ((CDbl(txtorprice10.Tag) * CDbl(totalTax)) / 100)) + CDbl(txtadditionalcess.Text), numFormat)
                Else
                    lbp10tax.Text = Format(0, numFormat)
                    txtorprice10.Tag = 0
                    price = 0

                End If
                

            End If
        Else
            If Val(numunitprice.Text) = 0 Then numunitprice.Text = 0
            If Val(numunitprice.Tag) = 0 Then numunitprice.Tag = CDbl(numunitprice.Text)
            txtpriceWtax.Text = Format((CDbl(numunitprice.Text) + ((CDbl(numunitprice.Text) * CDbl(totalTax)) / 100)) + CDbl(txtadditionalcess.Text), numFormat)
            If Val(txtsecondprice.Tag) > 0 Then
                lbldptax.Text = Format((CDbl(txtsecondprice.Tag) + ((CDbl(txtsecondprice.Tag) * CDbl(totalTax)) / 100)) + CDbl(txtadditionalcess.Text), numFormat)
            End If
            If Val(txtws.Tag) > 0 Then
                lblwstax.Text = Format((CDbl(txtws.Tag) + ((CDbl(txtws.Tag) * CDbl(totalTax)) / 100)) + CDbl(txtadditionalcess.Text), numFormat)
            End If

        End If


            If Val(numunitprice.Text) = 0 Then numunitprice.Text = 0
            lbltaxAmt.Text = Format(CDbl(numunitprice.Text) * CDbl(txtpriceWtax.Tag) / 100, numFormat)
            lblcessAmt.Text = Format((CDbl(numunitprice.Text) * (CDbl(cmbtax.Tag) + CDbl(cmbregularcess.Tag)) / 100) + CDbl(txtadditionalcess.Text), numFormat)
            chgbyprgAmt = False
            chgamt = False
    End Sub

    Private Sub numunitprice_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles numunitprice.Validated
        If Val(numunitprice.Text) > 0 Then calculateTaxFromUnitPrice(True)
    End Sub

    Private Sub txtpriceWtax_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpriceWtax.Validated
        If Val(txtpriceWtax.Text) > 0 Then calculateTaxFromUnitPrice(False)
    End Sub

    Private Sub btnrefreshmast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefreshmast.Click
        cmbUnit.Tag = cmbUnit.Text
        cmbtax.Tag = cmbtax.Text
        txthsncode.Tag = txthsncode.Text
        loadMasters()
        cmbUnit.Text = cmbUnit.Tag
        cmbtax.Text = cmbtax.Tag
        txthsncode.Text = txthsncode.Tag
    End Sub

    Private Sub txthsncode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txthsncode.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub txthsncode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txthsncode.TextChanged
        If chgbyprg Or btnModify.Enabled Then Exit Sub
        BtnUpdate.Enabled = True
    End Sub

    Private Sub txthsncode_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txthsncode.Validated
        hsncodevalidate()
    End Sub
    Private Sub hsncodevalidate()
        Try
            If chgbyprg Then Exit Sub
            If btnNew.Text = "Undo" Then
                BtnUpdate.Enabled = True
            End If
            Dim dt As DataTable
            dt = _objcmnbLayer._fldDatatable("SELECT IGST FROM GSTTB WHERE HSNCode='" & txthsncode.Text & "'")
            If dt.Rows.Count > 0 Then
                txtpriceWtax.Tag = Format(dt(0)("IGST"), numFormat)
                lblgstp.Text = "GST : " & Val(txtpriceWtax.Tag) & "%"
                If Val(numunitprice.Text) > 0 Then
                    calculateTaxFromUnitPrice(True)
                ElseIf Val(txtpriceWtax.Text) > 0 Then
                    calculateTaxFromUnitPrice(False)
                End If
            Else
                txtpriceWtax.Tag = 0
                lblgstp.Text = ""
                txthsncode.Tag = ""
                txtpriceWtax.Tag = 0
                If Val(numunitprice.Text) > 0 Then
                    calculateTaxFromUnitPrice(True)
                ElseIf Val(txtpriceWtax.Text) > 0 Then
                    calculateTaxFromUnitPrice(False)
                End If
                If txthsncode.Text <> "" Then
                    MsgBox("Invalid HSN Code", MsgBoxStyle.Exclamation)
                    txthsncode.Text = ""
                    txthsncode.Focus()
                End If

                '
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub AddManuRow()
        Dim i As Integer
        With grdmanufacturing
            activecontrolname = "grdmanufacturing"
            i = .RowCount '- 1
            .Rows.Add(1)
            .CurrentCell = .Item(RawItemCode, i)
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
        End With
    End Sub
    Private Sub AddDiscRow()
        Dim i As Integer
        With grdwoodQtyDisc
            activecontrolname = "grdwoodQtyDisc"
            i = .RowCount '- 1
            .Rows.Add(1)
            .CurrentCell = .Item(0, i)
            chgbyprg = True
            .BeginEdit(True)
            chgbyprg = False
        End With
        BtnUpdate.Enabled = True
    End Sub
    Private Sub RemoveDiscRow()
        If grdwoodQtyDisc.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdwoodQtyDisc
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
            End With
        End If
        BtnUpdate.Enabled = True
    End Sub
    Private Sub RemoveRawRow()
        If grdmanufacturing.RowCount > 0 Then
            If MsgBox("You Are Going to Remove the Row." & vbCrLf & "Are You Sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                Exit Sub
            End If
            With grdmanufacturing
                .Rows.RemoveAt(.CurrentRow.Index)
                .ClearSelection()
            End With
        End If

    End Sub

    Private Sub btnaddraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddraw.Click
        AddManuRow()
        enableset()
    End Sub

    Private Sub btnremoveRaw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremoveRaw.Click
        RemoveRawRow()
        enableset()
    End Sub
    Private Sub setRawGrid()
        chgbyprg = True
        SetGridEditProperty(grdmanufacturing)
        With grdmanufacturing
            .ColumnCount = 7
            .Columns(RawItemCode).HeaderText = "Item Code"
            .Columns(RawItemCode).Width = 100

            .Columns(RawTrDescr).HeaderText = "Description"
            .Columns(RawTrDescr).Width = 200
            .Columns(RawTrDescr).DefaultCellStyle.BackColor = Color.LightSteelBlue

            .Columns(RawUnit).HeaderText = "Unit"
            .Columns(RawUnit).Width = 50
            .Columns(RawUnit).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns(RawUnit).ReadOnly = True

            .Columns(RawQtyInHand).HeaderText = "QIH"
            .Columns(RawQtyInHand).Width = 50
            .Columns(RawQtyInHand).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(RawQtyInHand).DefaultCellStyle.BackColor = Color.LightYellow
            .Columns(RawQtyInHand).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(RawItemid).Visible = False
            .Columns(RawPffraction).Visible = False
            .Columns(RawMid).Visible = False
        End With
        chgbyprg = False
    End Sub
    Private Sub grdmanufacturing_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdmanufacturing.CellClick
        chgbyprg = True
        grdmanufacturing.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdmanufacturing_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdmanufacturing.CellEndEdit
        With grdmanufacturing
            Dim col As Integer = e.ColumnIndex
            Dim ndec1 As Integer
            If col = RawQtyInHand Then
                If col = RawQtyInHand Then
                    ndec1 = .Item(RawPffraction, .CurrentRow.Index).Value
                Else
                    ndec1 = 2
                End If
                .Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
            End If
        End With
        enableset()
    End Sub
    Private Sub Valid(ByVal RowIndex As Integer, ByVal ColIndex As Integer)
        With grdmanufacturing
            Select Case ColIndex
                Case RawItemCode
                    If Not chgItm Then Exit Sub
                    If Trim(.Item(ColIndex, RowIndex).Value) = "" Then Exit Sub
                    Dim dtItms As DataTable
                    Dim found As Boolean
                    dtItms = getItmDtls(3, SrchText, True)
                    If dtItms.Rows.Count > 0 Then
                        'found = True
                        found = AddRawmeterials(dtItms)
                    End If
                    chgItm = False
                    If Not found Then
                        .Item(RawItemCode, RowIndex).Value = ""
                        .Item(RawTrDescr, RowIndex).Value = ""
                        .Item(RawUnit, RowIndex).Value = ""
                        .Item(RawItemid, RowIndex).Value = 0
                        .Item(RawPffraction, RowIndex).Value = 2
                        chgItm = False
                    End If
            End Select
        End With
    End Sub
    Private Function AddRawmeterials(ByVal DR As DataTable) As Boolean
        If Val(PreitemId) = Val(DR(0)("ItemId")) Then
            MsgBox("Cannot add same item as rawmaterial", MsgBoxStyle.Exclamation)
            Return False
        End If
        Dim i As Integer
        Dim PMult As Double
        With grdmanufacturing
            chgbyprg = True
            PMult = 1
            i = .CurrentRow.Index ' .RowCount - 1
            .Item(RawItemCode, i).Value = IIf(IsDBNull(DR(0)("Item Code")), Trim(DR(0)("Item Code") & ""), DR(0)("Item Code"))
            .Item(RawTrDescr, i).Value = DR(0)("Description")
            .Item(RawPffraction, i).Value = IIf(IsDBNull(DR(0)("FraCount")), "2", DR(0)("FraCount"))
            .Item(RawQtyInHand, i).Value = Format(1, "#,##0" & IIf(Val(.Item(RawPffraction, i).Value) = 0, "", "." & Strings.StrDup(CByte(.Item(RawPffraction, i).Value), "0"))) ' Format(1, numFormat) 'IIf(IsReturn, -1, 1)
            .Item(RawUnit, i).Value = DR(0)("Unit")
            .Item(RawItemid, i).Value = DR(0)("ItemId")
            chgItm = False
            .ClearSelection()
        End With
        chgbyprg = False
        Return True
    End Function

    Private Sub grdmanufacturing_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdmanufacturing.CellValidated
        Valid(e.RowIndex, e.ColumnIndex)
    End Sub

    Private Sub grdmanufacturing_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdmanufacturing.CellValueChanged
        If chgbyprg Then Exit Sub
        With grdmanufacturing
            Dim i As Integer = e.RowIndex
            Select Case e.ColumnIndex
                Case ConstItemCode, ConstBarcode
                    chgItm = True
            End Select
        End With
    End Sub

    Private Sub grdmanufacturing_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdmanufacturing.EditingControlShowing
        Dim Col As Integer
        Col = grdmanufacturing.CurrentCell.ColumnIndex
        If Col = RawItemCode Or Col = RawQtyInHand Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            RemoveHandler tb.KeyPress, AddressOf Textbox_KeyPress
            AddHandler tb.KeyPress, AddressOf Textbox_KeyPress
        End If
    End Sub
    Private Sub Textbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            Dim col As Integer
            col = grdmanufacturing.CurrentCell.ColumnIndex
            If col = RawQtyInHand Then
                If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And Not e.KeyChar = Convert.ToChar(Keys.Back) Then e.Handled = True
            ElseIf col = RawItemCode Then
                e.Handled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub grdmanufacturing_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdmanufacturing.GotFocus
        activecontrolname = "grdmanufacturing"
    End Sub

    Private Sub grdmanufacturing_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdmanufacturing.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdmanufacturing.RowCount = 0 Then Exit Sub
                If SrchText = "" And grdmanufacturing.CurrentCell.ColumnIndex = RawItemCode Then
                    GoTo nxt
                End If
                If FindNextCell(grdmanufacturing, grdmanufacturing.CurrentCell.RowIndex, grdmanufacturing.CurrentCell.ColumnIndex + 1) Then
                    AddManuRow()
                End If
nxt:
                chgbyprg = True
                grdmanufacturing.BeginEdit(True)
                chgbyprg = False
            ElseIf e.KeyCode = Keys.F2 Then
                If grdmanufacturing.RowCount = 0 Then Exit Sub
                Select Case grdmanufacturing.CurrentCell.ColumnIndex
                    Case RawItemCode
                        fProductEnquiry = New ItmEnqry
                        fProductEnquiry.ShowDialog()
                End Select
            ElseIf e.KeyCode = Keys.F3 Then
                AddManuRow()
            ElseIf e.KeyCode = Keys.F4 Then
                If grdmanufacturing.RowCount = 0 Then Exit Sub
                grdmanufacturing.Rows.RemoveAt(grdmanufacturing.CurrentCell.RowIndex)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdmanufacturing_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdmanufacturing.Leave
        activecontrolname = ""
    End Sub

    Private Sub fProductEnquiry_CreateItem() Handles fProductEnquiry.CreateItem

    End Sub


    Private Sub fProductEnquiry_getSelected(ByVal ItemcODE As String, ByVal isacc As Integer) Handles fProductEnquiry.getSelected
        chgbyprg = True
        fProductEnquiry.Close()
        SrchText = ItemcODE
        grdmanufacturing.Item(RawItemCode, grdmanufacturing.CurrentRow.Index).Value = ItemcODE
        chgItm = True
        Valid(grdmanufacturing.CurrentRow.Index, RawItemCode)
        grdmanufacturing.CurrentCell = grdmanufacturing.Item(RawTrDescr, grdmanufacturing.CurrentRow.Index)
        grdmanufacturing.BeginEdit(True)
        chgbyprg = False
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            Const WM_KEYDOWN As Integer = &H100
            Const WM_KEYUP As Integer = &H101
            'msg.ToString()
            If msg.Msg = WM_KEYDOWN Or msg.Msg = WM_KEYUP Then
                If activecontrolname = "grdmanufacturing" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        If (msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up)) Then GoTo ctn
                        grdmanufacturing_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                ElseIf activecontrolname = "grdwoodQtyDisc" Then
                    If msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up) Or msg.WParam.ToInt32() = CInt(Keys.Enter) Or msg.WParam.ToInt32() = CInt(Keys.F2) Or msg.WParam.ToInt32() = CInt(Keys.F3) Or msg.WParam.ToInt32() = CInt(Keys.F4) Then
                        If (msg.WParam.ToInt32() = CInt(Keys.Down) Or msg.WParam.ToInt32() = CInt(Keys.Up)) Then GoTo ctn
                        grdwoodQtyDisc_KeyDown(Nothing, New KeyEventArgs(keyData))
                        Return True
                    End If
                End If
            End If
ctn:
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub btnSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSet.Click
        setRawmeterial(Val(PreitemId), True)
    End Sub
    Private Sub setRawmeterial(ByVal itemid As Long, ByVal isfromset As Boolean)
        _objcmnbLayer._saveDatawithOutParm("update ItemRawMeterialTb set setremove=1 where itemid=" & itemid)
        Dim i As Integer
        With grdmanufacturing
            For i = 0 To .RowCount - 1
                If Val(.Item(RawItemid, i).Value) > 0 Then
                    _objItmMast.rawmetid = Val(.Item(RawMid, i).Value)
                    _objItmMast.Ritemid = Val(.Item(RawItemid, i).Value)
                    _objItmMast.Rawqty = CDbl(.Item(RawQtyInHand, i).Value)
                    _objItmMast.ItemId = itemid
                    _objItmMast.saveRawMeterial()
                End If
            Next
        End With
        _objcmnbLayer._saveDatawithOutParm("delete from ItemRawMeterialTb where setremove=1 and itemid=" & itemid)
        If isfromset Then MsgBox("Rawmaterials added successfully", MsgBoxStyle.Information)
        btnSet.Enabled = False
        If isfromset Then getRawmaterial()
    End Sub
    Private Sub enableset()
        isRawMetChange = True
        If Val(PreitemId) > 0 Then
            btnSet.Enabled = True
        Else
            btnSet.Enabled = False
        End If
    End Sub
    Private Sub getRawmaterial()
        Dim dt As DataTable
        '_objItmMast = New clsItemMast_BL
        dt = _objItmMast.returnRawMaterial(Val(PreitemId))
        grdmanufacturing.Rows.Clear()
        For i = 0 To dt.Rows.Count - 1
            With grdmanufacturing
                .Rows.Add()
                .Item(RawItemCode, i).Value = dt(i)("Item Code")
                .Item(RawTrDescr, i).Value = dt(i)("Description")
                .Item(RawUnit, i).Value = dt(i)("Unit")
                .Item(RawQtyInHand, i).Value = dt(i)("Rawqty")
                .Item(RawPffraction, i).Value = dt(i)("FraCount")
                If Val(.Item(RawPffraction, i).Value & "") = 0 Then .Item(RawPffraction, i).Value = 0
                .Item(RawQtyInHand, i).Value = Format(CDbl(.Item(RawQtyInHand, i).Value), "#,##0" & IIf(Val(.Item(RawPffraction, i).Value & "") = 0, "", "." & Strings.StrDup(CByte(.Item(RawPffraction, i).Value), "0"))) ' 
                .Item(RawItemid, i).Value = dt(i)("Ritemid")
                .Item(RawMid, i).Value = dt(i)("rawmetid")
            End With
        Next
    End Sub

    Private Sub chkmanufacturing_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkmanufacturing.CheckedChanged
        If chkmanufacturing.Checked Then
            plrawmeterial.Visible = True
        Else
            plrawmeterial.Visible = False
        End If
    End Sub

    Private Sub getUnitFraction()
        Dim dt As DataTable
        Dim Ndec As Integer
        dt = _objcmnbLayer._fldDatatable("SELECT FraCount from  UnitsTb WHERE Units='" & cmbUnit.Text & "'")
        If dt.Rows.Count > 0 Then
            Ndec = Val(dt(0)("FraCount") & "")
            lnumformat = "#,##0" & IIf(Ndec = 0, "", "." & Strings.StrDup(Ndec, "0"))
        End If

    End Sub

    Private Sub btnaddQtyDisc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddQtyDisc.Click
        AddDiscRow()

    End Sub

    Private Sub btnRemQtyDisc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemQtyDisc.Click
        RemoveDiscRow()
    End Sub

    Private Sub grdmanufacturing_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdmanufacturing.CellContentClick

    End Sub

    Private Sub grdwoodQtyDisc_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdwoodQtyDisc.CellClick
        chgbyprg = True
        grdwoodQtyDisc.BeginEdit(True)
        chgbyprg = False
    End Sub

    Private Sub grdwoodQtyDisc_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdwoodQtyDisc.CellContentClick

    End Sub

    Private Sub grdwoodQtyDisc_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdwoodQtyDisc.CellEndEdit
        With grdwoodQtyDisc
            Dim col As Integer = e.ColumnIndex
            Dim ndec1 As Integer
            If col = 0 Or col = 1 Then
                ndec1 = 3
                .Item(col, e.RowIndex).Value = Format(Convert.ToDecimal(.Item(col, e.RowIndex).Value), "#,##0" & IIf(ndec1 = 0, "", "." & Strings.StrDup(ndec1, "0")))
            End If
        End With
    End Sub

    Private Sub grdwoodQtyDisc_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdwoodQtyDisc.CellValueChanged
        If chgbyprg Then Exit Sub
        BtnUpdate.Enabled = True
    End Sub

    Private Sub grdwoodQtyDisc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdwoodQtyDisc.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If grdwoodQtyDisc.RowCount = 0 Then Exit Sub
                If FindNextCell(grdwoodQtyDisc, grdwoodQtyDisc.CurrentCell.RowIndex, grdwoodQtyDisc.CurrentCell.ColumnIndex + 1) Then
                    AddDiscRow()
                End If
nxt:
                chgbyprg = True
                grdwoodQtyDisc.BeginEdit(True)
                chgbyprg = False

            ElseIf e.KeyCode = Keys.F3 Then
                AddDiscRow()
            ElseIf e.KeyCode = Keys.F4 Then
                RemoveDiscRow()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grdwoodQtyDisc_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdwoodQtyDisc.Leave
        activecontrolname = ""
    End Sub

    Private Sub txtperton_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtperton.KeyDown
        If e.KeyCode = Keys.Return Then
            BtnUpdate.Focus()
        End If
    End Sub

    Private Sub txtperton_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtperton.KeyPress
        NumericTextOnKeypress(txtperton, e, chgbyprgAmt, lnumformat)
    End Sub

    Private Sub txtperton_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtperton.TextChanged
        If chgbyprg Then Exit Sub
        BtnUpdate.Enabled = True
    End Sub

    Private Sub cmbtax_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtax.SelectedIndexChanged

        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select isnull(vat,0)vat from VatMasterTb where vatcode='" & cmbtax.Text & "'")
        If dt.Rows.Count > 0 Then
            cmbtax.Tag = dt(0)("vat")
        Else
            cmbtax.Tag = 0
        End If
        If chgbyprg Then Exit Sub
        If btnNew.Text = "Undo" Then
            BtnUpdate.Enabled = True
        End If
        chgamt = True
        If Val(numunitprice.Text) > 0 Then calculateTaxFromUnitPrice(True)
    End Sub

    Private Sub btnbarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbarcode.Click
        Dim frm As New BarCodeFrm
        With frm
            .lblbarcode.Text = txtCode.Text
            .lblitem.Text = txtDescr.Text
            .lblprice.Text = Format(CDbl(numunitprice.Text), numFormat)
            .lbltaxprice.Text = Format(CDbl(txtpriceWtax.Text), numFormat)
            .lblmrp.Text = Format(CDbl(txtmrp.Text), numFormat)
            .txtqty.Text = 1
            .ShowDialog()
        End With
    End Sub

    Private Sub txtCode_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCode.Validated
        If txtCode.Text = "" Or BtnUpdate.Enabled = False Then Exit Sub
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select itemid from invitm where [item code]='" & txtCode.Text & "' and itemid<>" & Val(PreitemId))
        If dt.Rows.Count > 0 Then
            MsgBox("Product code already exist", MsgBoxStyle.Exclamation)
            txtCode.Focus()
        End If
        chgbyprg = True
        txtCode.Text = UCase(txtCode.Text)
        chgbyprg = False
    End Sub

    Private Sub grdItem_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.CellContentClick

    End Sub


    Private Sub txtws_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtws.TextChanged, txtsecondprice.TextChanged
        If chgbyprg Then Exit Sub
        BtnUpdate.Enabled = True
        chgamt = True
    End Sub

    Private Sub txtrack_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtrack.TextChanged
        If chgbyprg Then Exit Sub
        BtnUpdate.Enabled = True
    End Sub

    Private Sub TabPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboxprtax.Click

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        If IsFromEnqry = True Then
            txtCode.Focus()
        Else
            btnNew.Focus()
        End If
    End Sub

    Private Sub txtsecondprice_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsecondprice.Validated
        'If Val(txtsecondprice.Text) > 0 Then calculateTaxFromUnitPrice(False, 1)
        If Val(txtpriceWtax.Text) > 0 Then calculateTaxFromUnitPrice(False)
    End Sub

    Private Sub txtws_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtws.Validated
        'If Val(txtws.Text) > 0 Then calculateTaxFromUnitPrice(False, 2)
        calculateTaxFromUnitPrice(False, 2)
    End Sub

    Private Sub cmbfc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbregularcess.SelectedIndexChanged

        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select isnull(vat,0)vat from VatMasterTb where vatcode='" & cmbregularcess.Text & "'")
        If dt.Rows.Count > 0 Then
            cmbregularcess.Tag = dt(0)("vat")
        Else
            cmbregularcess.Tag = 0
        End If
        If chgbyprg Then Exit Sub
        If btnNew.Text = "Undo" Then
            BtnUpdate.Enabled = True
        End If
        chgamt = True
        If Val(numunitprice.Text) > 0 Then calculateTaxFromUnitPrice(True)
    End Sub

    Private Sub txtadditionalcess_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtadditionalcess.TextChanged
        If chgbyprg Then Exit Sub
        If btnNew.Text = "Undo" Then
            BtnUpdate.Enabled = True
        End If
        chgamt = True
    End Sub

    Private Sub txtadditionalcess_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtadditionalcess.Validated
        If Val(numunitprice.Text) > 0 Then calculateTaxFromUnitPrice(True)
    End Sub

    Private Sub cmbitemtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbitemtype.SelectedIndexChanged
        lditemdetails()
    End Sub

    Private Sub txtDescr_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescr.Validated
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        chgbyprg = True
        txtDescr.Text = UCase(txtDescr.Text)
        chgbyprg = False
    End Sub


    Private Sub chkhide_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkhide.Click
        If chkhide.Checked Then
            If MsgBox("Do you want to hide this item?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Else
            If MsgBox("Do you want to visible this item?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        End If
        _objcmnbLayer._saveDatawithOutParm("Update invitm set ishide=" & IIf(chkhide.Checked, 1, 0) & " where itemid=" & PreitemId)
        MsgBox("Updated", MsgBoxStyle.Information)
        ShowCurrentRecord()
    End Sub

    Private Sub chkhide_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkhide.CheckedChanged

    End Sub

    Private Sub txtwmcalculation_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwmcalculation.TextChanged
        If chgbyprg Then Exit Sub
        BtnUpdate.Enabled = True
    End Sub

    Private Sub fwait_triggerEvent() Handles fwait.triggerEvent
        Select Case fwait.triggerType
            Case 1
                If tbitem.SelectedIndex = 1 And btnNew.Text = "Undo" Then
                    MsgBox("Do Undo/Update before moving to Enquiry", MsgBoxStyle.Exclamation)
                    tbitem.SelectedIndex = 0
                    GoTo ext
                End If
                If tbitem.SelectedIndex = 1 Then
                    If grdItem.Rows.Count = 0 Or Val(btnrefresh.Tag) = 1 Then lditemdetails()
                End If
                btnrefresh.Tag = ""
            Case 2
                saveProduct()
            Case 3
                clearControls()
                PreitemId = Val(grdItem.Item(ConstItemId, grdItem.CurrentCell.RowIndex).Value)
                ShowCurrentRecord()
                tbitem.SelectedIndex = 0
                btnModify.Focus()
        End Select
ext:
        If Not fwait Is Nothing Then
            fwait.Close()
            fwait = Nothing
        End If

    End Sub

    Private Sub tbitem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbitem.Click
        loadWaite(1)
    End Sub
    Private Sub loadWaite(ByVal triggerType As Integer)
        If Not fwait Is Nothing Then fwait.Close() : fwait = Nothing
        fwait = New WaitMessageFrm
        With fwait
            .triggerType = triggerType
            .Show()
        End With
    End Sub

    Private Sub btnlocqty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlocqty.Click
        If tbitem.SelectedIndex = 1 Then
            If grdItem.CurrentRow Is Nothing Then Exit Sub
            showlocationwise(grdItem.CurrentRow.Index)
        Else
            showlocationwise(0)
        End If
    End Sub
    Private Sub showlocationwise(ByVal rowindex As Integer)
        If chgbyprg Then Exit Sub
        Dim litemid As Long
        If tbitem.SelectedIndex = 1 Then
            If grdItem.CurrentRow Is Nothing Then Exit Sub
            litemid = Val(grdItem.Item(ConstItemId, rowindex).Value)
        Else
            litemid = PreitemId
        End If
        If fshowlocationqty Is Nothing Then fshowlocationqty = New ShowLocationQtyFrm
        With fshowlocationqty
            .loadLOCQty(litemid)
            .Show()
            .Top = Me.Top + Screen.PrimaryScreen.WorkingArea.Height - .Height - 40
            .Left = Me.Left + Screen.PrimaryScreen.WorkingArea.Width - .Width - 10
        End With
    End Sub

    Private Sub grdItem_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem.RowEnter
        If Not fshowlocationqty Is Nothing Then
            showlocationwise(e.RowIndex)
        ElseIf Not fimage Is Nothing Then
            loadimagefromList(grdItem.Item(ConstItemId, e.RowIndex).Value, grdItem.Item(TrDescr, e.RowIndex).Value)
        End If
    End Sub

    Private Sub fshowlocationqty_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fshowlocationqty.FormClosed
        fshowlocationqty = Nothing
    End Sub

    Private Sub btnaddsupersed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddsupersed.Click
        addsupersed()
    End Sub
    Private Sub addsupersed()
        Dim dt As DataTable
        dt = _objcmnbLayer._fldDatatable("Select itemid from invitm where [item code]='" & Trim(MkDbSrchStr(txtsupersed.Text)) & "'")
        If dt.Rows.Count > 0 Then
            txtsupersed.Tag = dt(0)("itemid")
        End If
        If Not fMList Is Nothing Then fMList.Close() : fMList = Nothing
        If Val(txtsupersed.Tag) = 0 Then Exit Sub
        If PreitemId = 0 Then Exit Sub
        _objcmnbLayer._saveDatawithOutParm("insert into  SupersedItemsTb (Parentitemid,ChildItemid) values(" & PreitemId & "," & Val(txtsupersed.Tag) & ")")
        loadSupersed()
        chgbyprg = True
        txtsupersed.Text = ""
        txtsupersed.Tag = ""
        chgbyprg = False
        txtsupersed.Focus()
    End Sub
    Private Sub loadSupersed()

        dtsuspend = _objcmnbLayer._fldDatatable("Select " & _
                                         "case when " & PreitemId & "=Parentitemid then childcode else Parentcode end Code, " & _
                                          "case when " & PreitemId & "=Parentitemid then childname else Parentname end Name," & _
                                         "supersedid from SupersedItemsTb " & _
                                         "left join (select [Item Code] childcode,Description childname,itemid childid from invitm)child on child.childid=SupersedItemsTb.ChildItemid " & _
                                         "left join (select [Item Code] Parentcode,Description Parentname,itemid Parentid from invitm)Parent on Parent.Parentid=SupersedItemsTb.Parentitemid " & _
                                         "where Parentitemid=" & PreitemId & " OR ChildItemid=" & PreitemId)
        grdsupersed.DataSource = dtsuspend
        SetGridProperty(grdsupersed)
        With grdsupersed
            .Columns("supersedid").Visible = False
        End With
        resizeGridColumn(grdsupersed, 1)
        If PreitemId > 0 Then
            Panel3.Enabled = True
            txtCode.ReadOnly = False
        Else
            Panel3.Enabled = False
        End If
    End Sub

    Private Sub TabControl2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Tab.Click
        If Tab.SelectedIndex = 3 Then
            loadSupersed()
        End If
    End Sub

    Private Sub btnremovesupersed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnremovesupersed.Click
        If MsgBox("Do you want to remove supersed Item", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        Dim ssid As Long
        If grdsupersed.CurrentRow Is Nothing Then Exit Sub
        ssid = Val(grdsupersed.Item("supersedid", grdsupersed.CurrentRow.Index).Value)
        _objcmnbLayer._saveDatawithOutParm("Delete from SupersedItemsTb where supersedid=" & ssid)
        loadSupersed()
    End Sub

    Private Sub cmbOrder_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbOrder.SelectedIndexChanged
        txtSeq.Focus()
    End Sub

    Private Sub fMList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fMList.FormClosed
        fMList = Nothing
    End Sub

    Private Sub fhsncode_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles fhsncode.FormClosed
        fhsncode = Nothing
    End Sub

    Private Sub btnaddhsn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddhsn.Click
        If fhsncode Is Nothing Then
            fhsncode = New HSNCodeMaster
            fhsncode.isfromexternal = True
            fhsncode.ShowDialog()
            txthsncode.Focus()
            fhsncode = Nothing
        End If
    End Sub

    Private Sub fhsncode_returnHsn(ByVal hsncode As String) Handles fhsncode.returnHsn
        chgbyprg = TrDescr
        txthsncode.Text = hsncode
        chgbyprg = False
        hsncodevalidate()

    End Sub

    Private Sub txtpoint_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtpoint.KeyDown
        If e.KeyCode = Keys.Return Then
            BtnUpdate.Focus()
        End If
    End Sub

    Private Sub txtpoint_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpoint.KeyPress, txtcount.KeyPress
        NumericTextOnKeypress(sender, e, chgbyprgAmt, "0")
    End Sub

    Private Sub txtpoint_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpoint.TextChanged
        If chgbyprg Then Exit Sub
        BtnUpdate.Enabled = True
    End Sub

    Private Sub cmbCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategory.SelectedIndexChanged
        If chgbyprg Then Exit Sub
        BtnUpdate.Enabled = True
    End Sub

    Private Sub btnbrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbrowse.Click
        If btnNew.Text <> "Undo" Then Exit Sub
        With DlgOpen
            .Filter = "All Picture Files|*.bmp;*.dib;*.gif;*.wmf;*.emf;*.jpg;*.ico;*.cur|" & _
                     "Bitmaps(*.bmp,*.dib)|*.bmp;*.dib|" & _
                     "Gif Images(*.gif)|*.gif|" & _
                     "JPEG Images(*.jpg)|*.jpg|" & _
                     "Matafiles(*.wmf,*.emf)|*.wmf;*.emf|" & _
                     "Icons(*.ico,*.cur)|*.ico;*.cur|" & _
                     "All Files(*.*)|*.*"
            .Title = "Select an Image file"
            .FileName = ""
            .ShowDialog()
            If .FileName <> "" Then
                Err.Clear()
                On Error Resume Next
                Dim bm As New Bitmap(.FileName)
                picImge.SizeMode = PictureBoxSizeMode.StretchImage
                picImge.Image = bm
                If Err.Number Then
                    MsgBox(Err.Description)
                Else
                    picImge.Tag = .FileName
                    'lblpicpath.Text = .FileName
                    BtnUpdate.Enabled = True
                End If
            End If
            ChDir(Application.StartupPath)
        End With
    End Sub
    Private Sub saveImage(ByVal itemid As Long)
        If picImge.Tag <> "" Then
            'On Error Resume Next
            If Directory.Exists(DPath & "\Photos") = False Then
                Directory.CreateDirectory(DPath & "\Photos")
            End If
            Dim imagename As String = "ITM-" & itemid & ".png"
            If DPath <> "" Then
                imagename = DPath & "Photos\" & imagename
                If FileExists(imagename) Then
                    System.IO.File.Delete(imagename)
                End If
                FileCopy(picImge.Tag, imagename)
                If Err.Number Then
                    If MsgBox(Err.Description, vbRetryCancel + vbExclamation) = vbRetry Then Exit Sub
                End If
            End If
            uploadtoServer(itemid)
        End If
    End Sub
    Private Sub loadimage()
        Try
            If ftpurl <> "" And ftpusername <> "" And ftppassword <> "" Then
                btnupload.Enabled = False
                picImge.SizeMode = PictureBoxSizeMode.CenterImage
                picImge.ImageLocation = Application.StartupPath & "\loader.gif"
                'LdPic(picImge, Application.StartupPath & "\loader.gif", Me)
                Dim fname As String = ftpurl & "/ITM-" & PreitemId & ".png"
                Dim MyWebClient As New System.Net.WebClient
                AddHandler MyWebClient.DownloadDataCompleted, AddressOf DownloadDataCompleted
                MyWebClient.Credentials = New NetworkCredential(ftpusername, ftppassword)
                MyWebClient.DownloadDataAsync(New Uri(fname))
                'Dim ImageInBytes() As Byte = MyWebClient.DownloadData(fname)
                'Dim ImageStream As New IO.MemoryStream(ImageInBytes)
                'picImge.Image = New System.Drawing.Bitmap(ImageStream)
                'lblpicpath.Text = "ITM-" & PreitemId & ".png"
                'lblpicpath.Tag = DPath & "Photos\ITM-" & PreitemId & ".png"
            Else
                GoTo loadlocalimage
            End If
        Catch ex As Exception
            GoTo loadlocalimage
        End Try
        Exit Sub
loadlocalimage:
        LdPic(picImge, DPath & "Photos\ITM-" & PreitemId & ".png", Me)
        lblpicpath.Text = "ITM-" & PreitemId & ".png"
        lblpicpath.Tag = DPath & "Photos\ITM-" & PreitemId & ".png"
        btnupload.Enabled = True
    End Sub
    Private Sub uploadtoServer(ByVal itemid As Long)
        If ftpurl <> "" And ftpusername <> "" And ftppassword <> "" Then
            Dim imagename As String = DPath & "Photos\ITM-" & itemid & ".png"
            If FileExists(imagename) Then
                If MsgBox("Do you want update image to webserver?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Exit Sub

                Dim request As FtpWebRequest = DirectCast(WebRequest.Create(New Uri(ftpurl & "/ITM-" & itemid & ".png")), System.Net.FtpWebRequest)
                request.Method = WebRequestMethods.Ftp.UploadFile
                request.Credentials = New NetworkCredential(ftpusername, ftppassword)
                request.UseBinary = True
                request.UsePassive = True

                Dim buffer(1023) As Byte
                Dim bytesIn As Long = 1
                Dim totalBytesIn As Long = 0

                Dim filepath As System.IO.FileInfo = New System.IO.FileInfo(imagename)
                Dim ftpstream As System.IO.FileStream = filepath.OpenRead()
                Dim flLength As Long = ftpstream.Length
                Dim reqfile As System.IO.Stream = request.GetRequestStream()

                Do Until bytesIn < 1
                    bytesIn = ftpstream.Read(buffer, 0, 1024)
                    If bytesIn > 0 Then
                        reqfile.Write(buffer, 0, bytesIn)
                        totalBytesIn += bytesIn
                    End If
                Loop
                reqfile.Close()
                ftpstream.Close()
            End If
        End If
    End Sub
    Sub DownloadDataCompleted(ByVal sender As Object, ByVal e As DownloadDataCompletedEventArgs)
        If e.Cancelled = False AndAlso e.Error Is Nothing Then
            picImge.SizeMode = PictureBoxSizeMode.StretchImage
            picImge.Image = New Bitmap(New IO.MemoryStream(e.Result))
            lblpicpath.Text = "ITM-" & PreitemId & ".png"
            lblpicpath.Tag = DPath & "Photos\ITM-" & PreitemId & ".png"
        Else
            picImge.Image = Nothing
            picImge.ImageLocation = ""
            LdPic(picImge, DPath & "Photos\ITM-" & PreitemId & ".png", Me, True)
            lblpicpath.Text = "ITM-" & PreitemId & ".png"
            lblpicpath.Tag = DPath & "Photos\ITM-" & PreitemId & ".png"
            btnupload.Enabled = True
        End If
    End Sub
    Private Sub btnupload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupload.Click
        If PreitemId = 0 Then Exit Sub
        uploadtoServer(PreitemId)

    End Sub

    Private Sub grdLevel_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdLevel.DataError

    End Sub

    Private Sub btnviewimage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnviewimage.Click
        If btnviewimage.Text = "Hide Image" Then
            If Not fimage Is Nothing Then
                fimage.Close()
                fimage = Nothing
            End If
            btnviewimage.Text = "View Image"
        Else
            btnviewimage.Text = "Hide Image"
            If grdItem.RowCount > 0 Then
                If Not grdItem.CurrentRow Is Nothing Then
                    loadimagefromList(grdItem.Item(ConstItemId, grdItem.CurrentRow.Index).Value, grdItem.Item(TrDescr, grdItem.CurrentRow.Index).Value)
                End If
            End If
        End If
    End Sub
    Private Sub loadimagefromList(ByVal listitemid As Long, ByVal itemname As String)
        If fimage Is Nothing Then
            fimage = New ViewImageFrm
            fimage.Show()
        Else
            fimage.Focus()
        End If
        With fimage
            .listitemid = listitemid
            .Text = itemname
            .loadimage()
            .Top = Me.Top + 100
            .Left = Me.Left + Screen.PrimaryScreen.WorkingArea.Width - (.Width + 20)
        End With
    End Sub

    Private Sub cmbgender_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbgender.KeyDown
        If e.KeyCode = Keys.Return Then
            txtCode.Focus()
        End If
    End Sub
    Private Sub cmbagegroup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbagegroup.KeyDown, txtcolor.KeyDown, txtsize.KeyDown
        If e.KeyCode = Keys.Return Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub cmbagegroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbagegroup.SelectedIndexChanged
        If btnNew.Text = "Undo" Then
            BtnUpdate.Enabled = True
        End If
    End Sub

    Private Sub txtcolor_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcolor.TextChanged, txtsize.TextChanged
        If chgbyprg Then Exit Sub
        If btnNew.Text = "Undo" Then
            BtnUpdate.Enabled = True
        End If
    End Sub

    Private Sub cmbgender_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbgender.SelectedIndexChanged
        If btnNew.Text = "Undo" Then
            BtnUpdate.Enabled = True
        End If
    End Sub

    Private Sub chkshowinweb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkshowinweb.Click
        If btnNew.Text = "Undo" Then
            BtnUpdate.Enabled = True
        End If
    End Sub

    Private Sub txtdescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdescription.TextChanged
        If btnNew.Text = "Undo" Then
            BtnUpdate.Enabled = True
        End If
    End Sub


    Private Sub hh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles hh.Click

    End Sub

    Private Sub TabPage9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles otherprce.Click

    End Sub

    Private Sub Label51_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label51.Click

    End Sub

    Private Sub TabPage3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage3.Click

    End Sub

    Private Sub Label28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label28.Click

    End Sub

    Private Sub ToolTip1_Popup(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PopupEventArgs) Handles ToolTip1.Popup

    End Sub

    Private Sub Label52_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label52.Click

    End Sub

    Private Sub Label49_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label49.Click

    End Sub

    Private Sub Label48_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label48.Click

    End Sub

    Private Sub lblgstp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblgstp.Click

    End Sub

    Private Sub Label65_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbp8tax.Click

    End Sub

    Private Sub lbp1tax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbp1tax.Click

    End Sub

  
    Private Sub txtorprice1_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtorprice1.Validated
        calculateTaxFromUnitPrice(False, 3)
    End Sub

    Private Sub txtorprice2_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtorprice2.Validated
        calculateTaxFromUnitPrice(False, 4)
    End Sub

    Private Sub txtorprice3_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtorprice3.Validated
        calculateTaxFromUnitPrice(False, 5)
    End Sub

    Private Sub txtorprice4_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtorprice4.Validated
        calculateTaxFromUnitPrice(False, 6)
    End Sub

    Private Sub txtorprice5_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtorprice5.Validated
        calculateTaxFromUnitPrice(False, 7)
    End Sub

    Private Sub txtorprice6_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtorprice6.Validated
        calculateTaxFromUnitPrice(False, 8)
    End Sub

    Private Sub txtorprice7_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtorprice7.Validated
        calculateTaxFromUnitPrice(False, 9)
    End Sub

    Private Sub txtorprice8_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtorprice8.Validated
        calculateTaxFromUnitPrice(False, 10)
    End Sub

    Private Sub txtorprice9_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtorprice9.Validated
        calculateTaxFromUnitPrice(False, 11)
    End Sub

    Private Sub txtorprice10_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtorprice10.Validated
        calculateTaxFromUnitPrice(False, 12)
    End Sub


    Private Sub ckbxhidemilk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckbxhidemilk.CheckedChanged

    End Sub

    Private Sub ckbxhidemilk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckbxhidemilk.Click
        If btnNew.Text = "Undo" Then
            BtnUpdate.Enabled = True
        End If
    End Sub
End Class