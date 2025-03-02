Imports System.IO
Public Class RptFormatfrm
    Public RptType As String
    Public slctSQL As String
    Public slctFormat As String
    Public VrNoIsNumeric As Boolean
    Public isFromMenu As Boolean
    Private RptCaption As String
    Public Event PrvPrnRpt(ByVal RptFlName As String, ByVal RptCaption As String, ByVal forPrint As Boolean)
    Public Event Cancel()
    Const ConstRptNo = 0
    Const ConstRptType = 1
    Const ConstRptCaption = 2
    Const ConstRptTypeName = 3
    Const ConstRptDefault = 4
    Const ConstCustRpt = 5
    Const ConstRptBase = 6
    Const ConstRptName = 7
    Const Constprintername = 8
    Const ConstHidden = 9
    'object declarations
    Dim _objcmnbLayer As New clsCommon_BL
    Private Sub RptFormatfrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If userType = 0 Then
            btnadd.Visible = True
        Else
            btnadd.Visible = False
        End If
        If isFromMenu Then
            ldType()
            cmbtype.Visible = True
        Else
            cmbtype.Visible = False
        End If
        LdReportfiles()
        SetGridHead()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If btnPrint.Tag = "" Then Exit Sub
        Dim RptFlName As String
        RptFlName = IIf(Val(btnView.Tag) = 1, Application.StartupPath, DPath) & btnPrint.Tag
        If Not File.Exists(RptFlName) Then
            RptFlName = ""
            MsgBox("Selected Format File not Found !!", vbCritical)
            Exit Sub
        End If
        If RptFlName <> "" Then
            Close()
            RaiseEvent PrvPrnRpt(RptFlName, RptCaption, True)
        End If
    End Sub
    Private Sub SetGridHead()
        With grdRpt
            SetGridProperty(grdRpt)

            .Columns(ConstRptNo).HeaderText = "RptNo"
            .Columns(ConstRptNo).Visible = False

            .Columns(ConstRptCaption).HeaderText = "Description"
            .Columns(ConstRptCaption).Width = 250 ' IIf(MastId, .Width * 75 / 100, .Width * 95 / 100)
            .Columns(ConstRptCaption).SortMode = DataGridViewColumnSortMode.NotSortable
            '.Columns(ItmCode).DefaultCellStyle.Format = "N" & NoOfDecimal
            .Columns(ConstRptType).HeaderText = "Type"
            .Columns(ConstRptType).Width = 50

            .Columns(ConstRptTypeName).HeaderText = "TypeName"
            .Columns(ConstRptTypeName).Visible = False

            .Columns(ConstRptDefault).HeaderText = "Default"
            .Columns(ConstRptDefault).Visible = False

            .Columns(ConstCustRpt).Visible = False

            .Columns(ConstRptBase).HeaderText = "Base"
            .Columns(ConstRptBase).Visible = False

            .Columns(ConstRptName).HeaderText = "File Name"
            .Columns(ConstRptName).Visible = True ' IIf(MastId = 2, True, False)
            .Columns(ConstRptName).Width = .Width * 25 / 100

            .Columns(ConstHidden).Width = 50
            .Columns(ConstHidden).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        End With
    End Sub
    Private Sub LdReportfiles()
        Dim Rptfls As New DataTable
        Dim Selected As Integer
        Dim _vRptfls As String
        '_vRptfls = "(SELECT RptType, RptName, RptCaption, RptNo, OnlySaveAs FROM RptDetTb UNION ALL SELECT RptType, RptName, RptCaption, 8000 + RptNo, OnlySaveAs FROM RptCustDetTb) RptFls "
        _vRptfls = ""
        Rptfls = _objcmnbLayer._fldDatatable("select * from(SELECT RPTNO,RptCmnTb.RptType,RptCaption, RptTypeName, " & _
                                             "case when RPTNO=DefaultRpt then RPTNO else 0 end DefaultRpt, " & _
                                             "case when RPTNO>=8000 then 1 else 0 end CustRpt, " & _
                                             "OnlySaveAs, RptName,Printername,case when isnull(ishide,0)=0 then '' else 'Yes' end Hidden FROM RptCmnTb " & _
                                             "LEFT JOIN RptFls ON RptFls.RptType = RptCmnTb.RptType " & _
                                             "WHERE RptCmnTb.RptType = '" & RptType & "'" & IIf(chkshowhidden.Checked, "", " AND isnull(ishide,0)=0") & ")tr ORDER BY DefaultRpt DESC,CustRpt DESC")
        Dim i As Integer
        grdRpt.DataSource = Rptfls
        txtRptCode.Text = RptType
        If Rptfls.Rows.Count = 0 Then Exit Sub
        txtRptTypeName.Text = Trim(Rptfls(0)("RptTypeName") & "")
        For i = 0 To Rptfls.Rows.Count - 1
            If Val(grdRpt.Item(ConstRptNo, i).Value) = Val(grdRpt.Item(ConstRptDefault, i).Value) Then
                txtSelected.Text = Trim(Rptfls(i)("RptCaption") & "")
                txtSelected.Tag = Rptfls(i)("rptno")
                btnPrint.Tag = Rptfls(i)("RptName")
                btnView.Tag = Rptfls(i)("OnlySaveAs")
                Selected = i
                Exit For
            End If
        Next
        grdRpt.Rows(Selected).Selected = True
        grdRpt.FirstDisplayedScrollingRowIndex = Selected
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        If btnPrint.Tag = "" Then Exit Sub
        Dim RptFlName As String
        RptFlName = IIf(Val(btnView.Tag) = 1, Application.StartupPath & "\", DPath) & btnPrint.Tag
        RptCaption = grdRpt.Item(ConstRptCaption, grdRpt.CurrentRow.Index).Value
        If Not FileExists(RptFlName) Then
            RptFlName = ""
            MsgBox("Selected Format File not Found !!", vbCritical)
            Exit Sub
        End If
        If RptFlName <> "" Then
            RaiseEvent PrvPrnRpt(RptFlName, RptCaption, False)
            Close()
        End If
    End Sub

    Private Sub grdRpt_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRpt.CellClick
        btnPrint.Tag = grdRpt.Item(ConstRptName, grdRpt.CurrentRow.Index).Value
        RptCaption = grdRpt.Item(ConstRptCaption, grdRpt.CurrentRow.Index).Value
        btnView.Tag = grdRpt.Item(ConstRptBase, grdRpt.CurrentRow.Index).Value
    End Sub

    Private Sub btnDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefault.Click
        With grdRpt
            If Val(.Item(ConstRptBase, .CurrentRow.Index).Value) = 1 Then
                _objcmnbLayer._saveDatawithOutParm("UPDATE RPTCMNTB SET DEFAULTRPT=" & Val(.Item(ConstRptNo, .CurrentRow.Index).Value) & " WHERE RPTTYPE='" & RptType & "'")
            Else
                _objcmnbLayer._saveDatawithOutParm("UPDATE RPTCMNTB SET DEFAULTRPT=" & Val(.Item(ConstRptNo, .CurrentRow.Index).Value) & ",IsCustom=1 WHERE RPTTYPE='" & RptType & "'")
            End If
            LdReportfiles()
            SetGridHead()
        End With
    End Sub

    Private Sub grdRpt_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRpt.CellDoubleClick

        Dim r As Integer = e.RowIndex
        If r < 0 Then Exit Sub
        If Trim(grdRpt.Item(ConstRptName, r).Value) = "" Then Exit Sub
        Dim RptFlName As String
        RptFlName = IIf(Val(grdRpt.Item(ConstRptBase, r).Value) = 1, APath, DPath) & Trim(grdRpt.Item(ConstRptName, r).Value)
        If Not FileExists(RptFlName) Then
            RptFlName = ""
            MsgBox("Selected Format File not Found !!", vbCritical)
            Exit Sub
        End If
        If RptFlName <> "" Then
            RaiseEvent PrvPrnRpt(RptFlName, RptCaption, True)
        End If
    End Sub

    Private Sub addFiles()
        Try
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
            If Not flag Then
                Dim str2 As String = InputBox("Enter Report Name", "Adding Customized Report", "", -1, -1)
                If (str2 = "") Then
                    str2 = "ISUNTITLED REPORT"
                End If
                _objcmnbLayer._saveDatawithOutParm("Insert Into RptCustTb( CustRptType, CustRptName, CustRptCaption) values('" & RptType & "','" & fileName & "','" & str2 & "')")
            End If
            LdReportfiles()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Sub



    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        addFiles()
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveToolStripMenuItem.Click
        If grdRpt.Item(ConstRptBase, grdRpt.CurrentRow.Index).Value = 1 Then
            MsgBox("You cannot remove base report", MsgBoxStyle.Exclamation)
        ElseIf grdRpt.Item(ConstRptDefault, grdRpt.CurrentRow.Index).Value > 0 Then
            MsgBox("You Cannot Remove Default Report", MsgBoxStyle.Exclamation, Nothing)
        ElseIf MsgBox("Do you want to remove the report " & grdRpt.Item(ConstRptCaption, grdRpt.CurrentRow.Index).Value, MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim str As String = grdRpt.Item(ConstRptName, grdRpt.CurrentRow.Index).Value
            _objcmnbLayer._saveDatawithOutParm("DELETE FROM RptCustTb WHERE CustRptNo=" & Val(grdRpt.Item(ConstRptNo, grdRpt.CurrentRow.Index).Value) - 8000)
            Dim filePath As String = DPath
            If FileExists(filePath) Then
                File.SetAttributes(filePath, System.IO.File.GetAttributes(filePath) Xor FileAttributes.ReadOnly Or FileAttributes.Hidden)
                File.Delete(filePath)
            End If
            LdReportfiles()
        End If

    End Sub
    Private Sub ldType()
        Dim dt As DataTable = _objcmnbLayer._fldDatatable("Select RptType from RptCmnTb", False)
        Dim num2 As Integer = (dt.Rows.Count - 1)
        Dim i As Integer = 0
        cmbtype.Items.Clear()
        Do While (i <= num2)
            cmbtype.Items.Add(dt(i)("RptType"))
            i += 1
        Loop
        If (cmbtype.Items.Count > 0) Then
            cmbtype.SelectedIndex = 0
        End If
    End Sub

    Private Sub cmbtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbtype.SelectedIndexChanged
        RptType = cmbtype.Text
        LdReportfiles()
    End Sub

    Private Sub grdRpt_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRpt.CellContentClick

    End Sub

    Private Sub mnusetprinter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnusetprinter.Click
        Dim str2 As String = InputBox("Enter Printer Name", "Adding Customized Report", "", -1, -1)
        Dim rptno As Integer = Val(grdRpt.Item(ConstRptNo, grdRpt.CurrentRow.Index).Value)
        If str2 <> "" Then
            If rptno > 8000 Then
                _objcmnbLayer._saveDatawithOutParm("Update RptCustTb set Printername='" & str2 & "' where CustRptNo=" & rptno - 8000)
            Else
                _objcmnbLayer._saveDatawithOutParm("Update RptDetTb set Printername='" & str2 & "' where RptNo=" & rptno)
            End If

            MsgBox("Updated", MsgBoxStyle.Information)
        End If

    End Sub

    Private Sub mnuhide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuhide.Click
        If grdRpt.Item(ConstRptBase, grdRpt.CurrentRow.Index).Value <> 1 Then
            MsgBox("You cannot hide custom report", MsgBoxStyle.Exclamation)
        ElseIf grdRpt.Item(ConstRptDefault, grdRpt.CurrentRow.Index).Value > 0 Then
            MsgBox("You Cannot Remove Default Report", MsgBoxStyle.Exclamation, Nothing)
        Else
            Dim rptno As Integer = Val(grdRpt.Item(ConstRptNo, grdRpt.CurrentRow.Index).Value)
            Dim hide As Integer
            If mnuhide.Text = "Hide" Then
                hide = 1
            Else
                hide = 0
            End If
            _objcmnbLayer._saveDatawithOutParm("Update RptDetTb set ishide=" & hide & " where RptNo=" & rptno)
        End If
        LdReportfiles()
    End Sub

    Private Sub chkshowhidden_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkshowhidden.CheckedChanged
        LdReportfiles()
    End Sub

    Private Sub grdRpt_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRpt.RowEnter
        If grdRpt.Item("hidden", e.RowIndex).Value = "Yes" Then
            mnuhide.Text = "Undo Hide"
        Else
            mnuhide.Text = "Hide"
        End If
    End Sub
End Class