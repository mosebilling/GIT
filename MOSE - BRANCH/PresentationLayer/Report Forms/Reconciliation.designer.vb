<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reconciliation
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lbl_Trl = New System.Windows.Forms.Label
        Me.txtBankCode = New System.Windows.Forms.TextBox
        Me.btnupdate = New System.Windows.Forms.Button
        Me.btnRefresh1 = New System.Windows.Forms.Button
        Me.chkPrtDlg = New System.Windows.Forms.CheckBox
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btn_Exit = New System.Windows.Forms.Button
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtbankname = New System.Windows.Forms.TextBox
        Me.cldrdateFrom = New System.Windows.Forms.DateTimePicker
        Me.clrDateTo = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtProjectionSearch = New System.Windows.Forms.TextBox
        Me.cmbSearch = New System.Windows.Forms.ComboBox
        Me.txtBalance = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.chkall = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.lblOpening = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblbalance = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.lblcredit = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lbldebit = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.lblAdd = New System.Windows.Forms.Label
        Me.lblCBText = New System.Windows.Forms.Label
        Me.lblLess = New System.Windows.Forms.Label
        Me.lblDbText = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.lblBankAmount = New System.Windows.Forms.Label
        Me.rdUncleared = New System.Windows.Forms.RadioButton
        Me.rdCleared = New System.Windows.Forms.RadioButton
        Me.rdAll = New System.Windows.Forms.RadioButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl_Trl
        '
        Me.lbl_Trl.AutoSize = True
        Me.lbl_Trl.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Trl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Trl.ForeColor = System.Drawing.Color.White
        Me.lbl_Trl.Location = New System.Drawing.Point(451, 2)
        Me.lbl_Trl.Name = "lbl_Trl"
        Me.lbl_Trl.Size = New System.Drawing.Size(18, 20)
        Me.lbl_Trl.TabIndex = 8
        Me.lbl_Trl.Text = "0"
        '
        'txtBankCode
        '
        Me.txtBankCode.Location = New System.Drawing.Point(1167, 2)
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.Size = New System.Drawing.Size(70, 20)
        Me.txtBankCode.TabIndex = 345444
        Me.txtBankCode.Visible = False
        '
        'btnupdate
        '
        Me.btnupdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnupdate.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupdate.FlatAppearance.BorderSize = 0
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnupdate.Location = New System.Drawing.Point(1041, 521)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(95, 33)
        Me.btnupdate.TabIndex = 345394
        Me.btnupdate.Text = "Update [F8]"
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'btnRefresh1
        '
        Me.btnRefresh1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh1.BackColor = System.Drawing.Color.SteelBlue
        Me.btnRefresh1.FlatAppearance.BorderSize = 0
        Me.btnRefresh1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh1.ForeColor = System.Drawing.Color.White
        Me.btnRefresh1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefresh1.Location = New System.Drawing.Point(946, 521)
        Me.btnRefresh1.Name = "btnRefresh1"
        Me.btnRefresh1.Size = New System.Drawing.Size(93, 33)
        Me.btnRefresh1.TabIndex = 345393
        Me.btnRefresh1.Text = "&Refresh"
        Me.btnRefresh1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnRefresh1.UseVisualStyleBackColor = False
        '
        'chkPrtDlg
        '
        Me.chkPrtDlg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkPrtDlg.BackColor = System.Drawing.Color.Transparent
        Me.chkPrtDlg.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrtDlg.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrtDlg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrtDlg.Location = New System.Drawing.Point(145, 508)
        Me.chkPrtDlg.Name = "chkPrtDlg"
        Me.chkPrtDlg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrtDlg.Size = New System.Drawing.Size(122, 16)
        Me.chkPrtDlg.TabIndex = 345380
        Me.chkPrtDlg.Text = "Choose  Format"
        Me.chkPrtDlg.UseVisualStyleBackColor = False
        Me.chkPrtDlg.Visible = False
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.BackColor = System.Drawing.Color.SteelBlue
        Me.btnPreview.FlatAppearance.BorderSize = 0
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.White
        Me.btnPreview.Location = New System.Drawing.Point(843, 521)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(101, 33)
        Me.btnPreview.TabIndex = 138
        Me.btnPreview.Text = "Pre&view"
        Me.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'btn_Exit
        '
        Me.btn_Exit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Exit.BackColor = System.Drawing.Color.SteelBlue
        Me.btn_Exit.FlatAppearance.BorderSize = 0
        Me.btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Exit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Exit.ForeColor = System.Drawing.Color.White
        Me.btn_Exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_Exit.Location = New System.Drawing.Point(1138, 521)
        Me.btn_Exit.Name = "btn_Exit"
        Me.btn_Exit.Size = New System.Drawing.Size(93, 33)
        Me.btn_Exit.TabIndex = 75
        Me.btn_Exit.Text = "   E&xit"
        Me.btn_Exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btn_Exit.UseVisualStyleBackColor = False
        '
        'grdVoucher
        '
        Me.grdVoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdVoucher.BackgroundColor = System.Drawing.Color.AliceBlue
        Me.grdVoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdVoucher.Location = New System.Drawing.Point(8, 69)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(906, 411)
        Me.grdVoucher.TabIndex = 345441
        Me.grdVoucher.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(286, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 345442
        Me.Label1.Text = "Bank"
        '
        'txtbankname
        '
        Me.txtbankname.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtbankname.Location = New System.Drawing.Point(321, 41)
        Me.txtbankname.Name = "txtbankname"
        Me.txtbankname.Size = New System.Drawing.Size(593, 20)
        Me.txtbankname.TabIndex = 345443
        '
        'cldrdateFrom
        '
        Me.cldrdateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrdateFrom.Location = New System.Drawing.Point(75, 41)
        Me.cldrdateFrom.Name = "cldrdateFrom"
        Me.cldrdateFrom.Size = New System.Drawing.Size(101, 20)
        Me.cldrdateFrom.TabIndex = 345446
        Me.cldrdateFrom.TabStop = False
        '
        'clrDateTo
        '
        Me.clrDateTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.clrDateTo.Location = New System.Drawing.Point(179, 41)
        Me.clrDateTo.Name = "clrDateTo"
        Me.clrDateTo.Size = New System.Drawing.Size(101, 20)
        Me.clrDateTo.TabIndex = 345448
        Me.clrDateTo.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(5, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 345449
        Me.Label2.Text = "Date Range"
        '
        'txtProjectionSearch
        '
        Me.txtProjectionSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtProjectionSearch.BackColor = System.Drawing.Color.PaleGreen
        Me.txtProjectionSearch.Location = New System.Drawing.Point(145, 487)
        Me.txtProjectionSearch.Name = "txtProjectionSearch"
        Me.txtProjectionSearch.Size = New System.Drawing.Size(651, 20)
        Me.txtProjectionSearch.TabIndex = 345452
        '
        'cmbSearch
        '
        Me.cmbSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearch.FormattingEnabled = True
        Me.cmbSearch.Location = New System.Drawing.Point(8, 486)
        Me.cmbSearch.Name = "cmbSearch"
        Me.cmbSearch.Size = New System.Drawing.Size(133, 21)
        Me.cmbSearch.TabIndex = 345451
        '
        'txtBalance
        '
        Me.txtBalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBalance.Location = New System.Drawing.Point(1104, 486)
        Me.txtBalance.Name = "txtBalance"
        Me.txtBalance.ReadOnly = True
        Me.txtBalance.Size = New System.Drawing.Size(127, 20)
        Me.txtBalance.TabIndex = 345459
        Me.txtBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBalance.Visible = False
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(1012, 490)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(86, 13)
        Me.Label7.TabIndex = 345460
        Me.Label7.Text = "Total Balance"
        Me.Label7.Visible = False
        '
        'chkall
        '
        Me.chkall.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkall.AutoSize = True
        Me.chkall.BackColor = System.Drawing.Color.Transparent
        Me.chkall.Location = New System.Drawing.Point(8, 508)
        Me.chkall.Name = "chkall"
        Me.chkall.Size = New System.Drawing.Size(70, 17)
        Me.chkall.TabIndex = 345461
        Me.chkall.Text = "Select All"
        Me.chkall.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(11, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(306, 13)
        Me.Label4.TabIndex = 345465
        Me.Label4.Text = "Balance Amount as per our books of Accounts"
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.lblOpening)
        Me.Panel5.Controls.Add(Me.Label8)
        Me.Panel5.Controls.Add(Me.lblbalance)
        Me.Panel5.Controls.Add(Me.Label11)
        Me.Panel5.Controls.Add(Me.lblcredit)
        Me.Panel5.Controls.Add(Me.Label9)
        Me.Panel5.Controls.Add(Me.lbldebit)
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Location = New System.Drawing.Point(920, 36)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(320, 148)
        Me.Panel5.TabIndex = 345466
        '
        'lblOpening
        '
        Me.lblOpening.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOpening.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpening.Location = New System.Drawing.Point(172, 32)
        Me.lblOpening.Name = "lblOpening"
        Me.lblOpening.Size = New System.Drawing.Size(138, 23)
        Me.lblOpening.TabIndex = 345471
        Me.lblOpening.Text = "0.00"
        Me.lblOpening.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(34, 36)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(118, 13)
        Me.Label8.TabIndex = 345470
        Me.Label8.Text = "Opening  Amount"
        '
        'lblbalance
        '
        Me.lblbalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblbalance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbalance.Location = New System.Drawing.Point(172, 111)
        Me.lblbalance.Name = "lblbalance"
        Me.lblbalance.Size = New System.Drawing.Size(138, 23)
        Me.lblbalance.TabIndex = 345469
        Me.lblbalance.Text = "0.00"
        Me.lblbalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(36, 116)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(112, 13)
        Me.Label11.TabIndex = 345468
        Me.Label11.Text = "Balance Amount"
        '
        'lblcredit
        '
        Me.lblcredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblcredit.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcredit.Location = New System.Drawing.Point(172, 85)
        Me.lblcredit.Name = "lblcredit"
        Me.lblcredit.Size = New System.Drawing.Size(138, 23)
        Me.lblcredit.TabIndex = 345469
        Me.lblcredit.Text = "0.00"
        Me.lblcredit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(34, 88)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(137, 13)
        Me.Label9.TabIndex = 345468
        Me.Label9.Text = "Total Credit Amount"
        '
        'lbldebit
        '
        Me.lbldebit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbldebit.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldebit.Location = New System.Drawing.Point(172, 59)
        Me.lbldebit.Name = "lbldebit"
        Me.lbldebit.Size = New System.Drawing.Size(138, 23)
        Me.lbldebit.TabIndex = 345467
        Me.lbldebit.Text = "0.00"
        Me.lbldebit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(34, 61)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(132, 13)
        Me.Label5.TabIndex = 345466
        Me.Label5.Text = "Total Debit Amount"
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(159, Byte), Integer))
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.lblAdd)
        Me.Panel6.Controls.Add(Me.lblCBText)
        Me.Panel6.Controls.Add(Me.lblLess)
        Me.Panel6.Controls.Add(Me.lblDbText)
        Me.Panel6.Controls.Add(Me.Label18)
        Me.Panel6.Location = New System.Drawing.Point(920, 190)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(320, 87)
        Me.Panel6.TabIndex = 345467
        '
        'lblAdd
        '
        Me.lblAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAdd.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdd.Location = New System.Drawing.Point(172, 55)
        Me.lblAdd.Name = "lblAdd"
        Me.lblAdd.Size = New System.Drawing.Size(138, 23)
        Me.lblAdd.TabIndex = 345469
        Me.lblAdd.Text = "0.00"
        Me.lblAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCBText
        '
        Me.lblCBText.AutoSize = True
        Me.lblCBText.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCBText.Location = New System.Drawing.Point(34, 57)
        Me.lblCBText.Name = "lblCBText"
        Me.lblCBText.Size = New System.Drawing.Size(128, 13)
        Me.lblCBText.TabIndex = 345468
        Me.lblCBText.Text = "Total Credit (Less)"
        '
        'lblLess
        '
        Me.lblLess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLess.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLess.Location = New System.Drawing.Point(172, 26)
        Me.lblLess.Name = "lblLess"
        Me.lblLess.Size = New System.Drawing.Size(138, 23)
        Me.lblLess.TabIndex = 345467
        Me.lblLess.Text = "0.00"
        Me.lblLess.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDbText
        '
        Me.lblDbText.AutoSize = True
        Me.lblDbText.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDbText.Location = New System.Drawing.Point(34, 31)
        Me.lblDbText.Name = "lblDbText"
        Me.lblDbText.Size = New System.Drawing.Size(119, 13)
        Me.lblDbText.TabIndex = 345466
        Me.lblDbText.Text = "Total Debit (Add)"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(11, 9)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(127, 13)
        Me.Label18.TabIndex = 345465
        Me.Label18.Text = "Uncleared Amount"
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(928, 284)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(288, 13)
        Me.Label12.TabIndex = 345468
        Me.Label12.Text = "Balance Amount as per the Bank statement"
        '
        'lblBankAmount
        '
        Me.lblBankAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBankAmount.BackColor = System.Drawing.Color.SeaShell
        Me.lblBankAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBankAmount.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblBankAmount.Location = New System.Drawing.Point(1015, 313)
        Me.lblBankAmount.Name = "lblBankAmount"
        Me.lblBankAmount.Size = New System.Drawing.Size(216, 23)
        Me.lblBankAmount.TabIndex = 345469
        Me.lblBankAmount.Text = "0.00"
        Me.lblBankAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rdUncleared
        '
        Me.rdUncleared.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdUncleared.AutoSize = True
        Me.rdUncleared.BackColor = System.Drawing.Color.Transparent
        Me.rdUncleared.Location = New System.Drawing.Point(803, 486)
        Me.rdUncleared.Name = "rdUncleared"
        Me.rdUncleared.Size = New System.Drawing.Size(78, 17)
        Me.rdUncleared.TabIndex = 345470
        Me.rdUncleared.TabStop = True
        Me.rdUncleared.Text = "Un-Cleared"
        Me.rdUncleared.UseVisualStyleBackColor = False
        '
        'rdCleared
        '
        Me.rdCleared.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdCleared.AutoSize = True
        Me.rdCleared.BackColor = System.Drawing.Color.Transparent
        Me.rdCleared.Location = New System.Drawing.Point(881, 486)
        Me.rdCleared.Name = "rdCleared"
        Me.rdCleared.Size = New System.Drawing.Size(61, 17)
        Me.rdCleared.TabIndex = 345471
        Me.rdCleared.TabStop = True
        Me.rdCleared.Text = "Cleared"
        Me.rdCleared.UseVisualStyleBackColor = False
        '
        'rdAll
        '
        Me.rdAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdAll.AutoSize = True
        Me.rdAll.BackColor = System.Drawing.Color.Transparent
        Me.rdAll.Location = New System.Drawing.Point(945, 486)
        Me.rdAll.Name = "rdAll"
        Me.rdAll.Size = New System.Drawing.Size(36, 17)
        Me.rdAll.TabIndex = 345472
        Me.rdAll.TabStop = True
        Me.rdAll.Text = "All"
        Me.rdAll.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.lbl_Trl)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.txtBankCode)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1243, 32)
        Me.Panel1.TabIndex = 345473
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(41, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 18)
        Me.Label3.TabIndex = 345458
        Me.Label3.Text = "Reconciliation"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.PendingLPO
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(3, -1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(32, 29)
        Me.PictureBox2.TabIndex = 345457
        Me.PictureBox2.TabStop = False
        '
        'Reconciliation
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.AutoSize = True
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1243, 564)
        Me.Controls.Add(Me.chkPrtDlg)
        Me.Controls.Add(Me.btnupdate)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnRefresh1)
        Me.Controls.Add(Me.clrDateTo)
        Me.Controls.Add(Me.cldrdateFrom)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.rdAll)
        Me.Controls.Add(Me.btn_Exit)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.rdCleared)
        Me.Controls.Add(Me.rdUncleared)
        Me.Controls.Add(Me.lblBankAmount)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.chkall)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtBalance)
        Me.Controls.Add(Me.txtProjectionSearch)
        Me.Controls.Add(Me.cmbSearch)
        Me.Controls.Add(Me.txtbankname)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grdVoucher)
        Me.KeyPreview = True
        Me.Name = "Reconciliation"
        Me.Text = "Projection Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_Trl As System.Windows.Forms.Label
    Friend WithEvents btnRefresh1 As System.Windows.Forms.Button
    Public WithEvents chkPrtDlg As System.Windows.Forms.CheckBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btn_Exit As System.Windows.Forms.Button
    Friend WithEvents grdVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtbankname As System.Windows.Forms.TextBox
    Friend WithEvents txtBankCode As System.Windows.Forms.TextBox
    Friend WithEvents cldrdateFrom As System.Windows.Forms.DateTimePicker
    'Friend WithEvents dtpdateFrom As AxMSMask.AxMaskEdBox
    Friend WithEvents clrDateTo As System.Windows.Forms.DateTimePicker
    'Friend WithEvents dtpDateTo As AxMSMask.AxMaskEdBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtProjectionSearch As System.Windows.Forms.TextBox
    Friend WithEvents cmbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents txtBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents chkall As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lbldebit As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblbalance As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblcredit As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents lblAdd As System.Windows.Forms.Label
    Friend WithEvents lblCBText As System.Windows.Forms.Label
    Friend WithEvents lblLess As System.Windows.Forms.Label
    Friend WithEvents lblDbText As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblBankAmount As System.Windows.Forms.Label
    Friend WithEvents rdUncleared As System.Windows.Forms.RadioButton
    Friend WithEvents rdCleared As System.Windows.Forms.RadioButton
    Friend WithEvents rdAll As System.Windows.Forms.RadioButton
    Friend WithEvents lblOpening As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
End Class
