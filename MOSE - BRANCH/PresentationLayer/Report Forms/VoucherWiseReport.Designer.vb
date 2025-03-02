<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VoucherWiseReport
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
        Me.btnLoad = New System.Windows.Forms.Button
        Me.rdovoucher = New System.Windows.Forms.RadioButton
        Me.btnExit = New System.Windows.Forms.Button
        Me.cldrEnddate = New System.Windows.Forms.DateTimePicker
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.rdoExpense = New System.Windows.Forms.RadioButton
        Me.btnApply = New System.Windows.Forms.Button
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdvoucher = New System.Windows.Forms.DataGridView
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.cmbcolms = New System.Windows.Forms.ComboBox
        Me.plvoucher = New System.Windows.Forms.Panel
        Me.chkTally = New System.Windows.Forms.CheckBox
        Me.cmbVoucherTp = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblDiff = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblTlDebit = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblTlCredit = New System.Windows.Forms.Label
        Me.btndelete = New System.Windows.Forms.Button
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.plvoucher.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.BackColor = System.Drawing.Color.SteelBlue
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.ForeColor = System.Drawing.Color.White
        Me.btnLoad.Location = New System.Drawing.Point(824, 428)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(85, 35)
        Me.btnLoad.TabIndex = 345481
        Me.btnLoad.Text = "&Load"
        Me.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLoad.UseVisualStyleBackColor = False
        '
        'rdovoucher
        '
        Me.rdovoucher.AutoSize = True
        Me.rdovoucher.BackColor = System.Drawing.Color.Transparent
        Me.rdovoucher.Checked = True
        Me.rdovoucher.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdovoucher.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdovoucher.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdovoucher.Location = New System.Drawing.Point(6, 19)
        Me.rdovoucher.Name = "rdovoucher"
        Me.rdovoucher.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdovoucher.Size = New System.Drawing.Size(100, 17)
        Me.rdovoucher.TabIndex = 15
        Me.rdovoucher.TabStop = True
        Me.rdovoucher.Text = "Voucher Report"
        Me.rdovoucher.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.AutoEllipsis = True
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(998, 428)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(85, 35)
        Me.btnExit.TabIndex = 345483
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'cldrEnddate
        '
        Me.cldrEnddate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrEnddate.Location = New System.Drawing.Point(110, 22)
        Me.cldrEnddate.Name = "cldrEnddate"
        Me.cldrEnddate.Size = New System.Drawing.Size(96, 20)
        Me.cldrEnddate.TabIndex = 345395
        Me.cldrEnddate.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.cldrEnddate)
        Me.GroupBox2.Controls.Add(Me.cldrStartDate)
        Me.GroupBox2.Location = New System.Drawing.Point(824, 373)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(215, 51)
        Me.GroupBox2.TabIndex = 345482
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Date Parameter"
        '
        'cldrStartDate
        '
        Me.cldrStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrStartDate.Location = New System.Drawing.Point(9, 22)
        Me.cldrStartDate.Name = "cldrStartDate"
        Me.cldrStartDate.Size = New System.Drawing.Size(95, 20)
        Me.cldrStartDate.TabIndex = 345393
        Me.cldrStartDate.TabStop = False
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(386, 373)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345480
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.rdovoucher)
        Me.GroupBox3.Controls.Add(Me.rdoExpense)
        Me.GroupBox3.Location = New System.Drawing.Point(4, 404)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(219, 59)
        Me.GroupBox3.TabIndex = 345485
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Select Report"
        '
        'rdoExpense
        '
        Me.rdoExpense.AutoSize = True
        Me.rdoExpense.BackColor = System.Drawing.Color.Transparent
        Me.rdoExpense.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdoExpense.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoExpense.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdoExpense.Location = New System.Drawing.Point(6, 37)
        Me.rdoExpense.Name = "rdoExpense"
        Me.rdoExpense.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdoExpense.Size = New System.Drawing.Size(145, 17)
        Me.rdoExpense.TabIndex = 14
        Me.rdoExpense.Text = "Expense Report [Indirect]"
        Me.rdoExpense.UseVisualStyleBackColor = False
        '
        'btnApply
        '
        Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApply.AutoEllipsis = True
        Me.btnApply.BackColor = System.Drawing.Color.SteelBlue
        Me.btnApply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnApply.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnApply.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApply.ForeColor = System.Drawing.Color.White
        Me.btnApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnApply.Location = New System.Drawing.Point(911, 428)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(85, 35)
        Me.btnApply.TabIndex = 345484
        Me.btnApply.Text = "Preview"
        Me.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnApply.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.AcceptsReturn = True
        Me.txtSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.BackColor = System.Drawing.SystemColors.Window
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSearch.Location = New System.Drawing.Point(177, 371)
        Me.txtSearch.MaxLength = 50
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSearch.Size = New System.Drawing.Size(203, 20)
        Me.txtSearch.TabIndex = 345479
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(41, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(137, 18)
        Me.Label1.TabIndex = 345458
        Me.Label1.Text = "Voucher Analysis"
        '
        'grdvoucher
        '
        Me.grdvoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdvoucher.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdvoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdvoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdvoucher.GridColor = System.Drawing.Color.Gainsboro
        Me.grdvoucher.Location = New System.Drawing.Point(4, 34)
        Me.grdvoucher.Name = "grdvoucher"
        Me.grdvoucher.Size = New System.Drawing.Size(1088, 332)
        Me.grdvoucher.TabIndex = 345476
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.button_reports1
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(3, -1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(32, 35)
        Me.PictureBox2.TabIndex = 345457
        Me.PictureBox2.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.PictureBox2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1095, 32)
        Me.Panel2.TabIndex = 345475
        '
        'cmbcolms
        '
        Me.cmbcolms.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbcolms.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcolms.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbcolms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcolms.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcolms.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcolms.Location = New System.Drawing.Point(5, 370)
        Me.cmbcolms.Name = "cmbcolms"
        Me.cmbcolms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbcolms.Size = New System.Drawing.Size(166, 22)
        Me.cmbcolms.TabIndex = 345478
        Me.cmbcolms.TabStop = False
        '
        'plvoucher
        '
        Me.plvoucher.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.plvoucher.BackColor = System.Drawing.Color.Transparent
        Me.plvoucher.Controls.Add(Me.chkTally)
        Me.plvoucher.Controls.Add(Me.cmbVoucherTp)
        Me.plvoucher.Controls.Add(Me.Label4)
        Me.plvoucher.Location = New System.Drawing.Point(224, 412)
        Me.plvoucher.Name = "plvoucher"
        Me.plvoucher.Size = New System.Drawing.Size(261, 50)
        Me.plvoucher.TabIndex = 345486
        '
        'chkTally
        '
        Me.chkTally.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkTally.AutoSize = True
        Me.chkTally.BackColor = System.Drawing.Color.Transparent
        Me.chkTally.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTally.Location = New System.Drawing.Point(134, 3)
        Me.chkTally.Name = "chkTally"
        Me.chkTally.Size = New System.Drawing.Size(124, 19)
        Me.chkTally.TabIndex = 345436
        Me.chkTally.Text = "&Show Non-Tallied"
        Me.chkTally.UseVisualStyleBackColor = False
        '
        'cmbVoucherTp
        '
        Me.cmbVoucherTp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVoucherTp.FormattingEnabled = True
        Me.cmbVoucherTp.Items.AddRange(New Object() {"JV - Journal Voucher", "PI - Purchase Invoice", "IP - Purchase Invoice [Stock]", "PR - Purchase Return", "SI - Sales Invoice", "IS - Sales Invoice [Stock]", "JIS - Job Sales Invoice", "SR - Sales Return", "CN - Credit Note Voucher", "DN - Debit Note Voucher", "CV - Contra Voucher", "PV - Payment Voucher", "RV - Receipt Voucher", "TI - Transaction [IN]", "DB - Debit Voucher", "CB - Credit Voucher", "TO - Transaction [OUT]", "MA - Manufacturing Voucher", "DIS - Discount Card Sales Voucher", "SIS - WS Service Sales Voucher", "STO- Service Stock OUT"})
        Me.cmbVoucherTp.Location = New System.Drawing.Point(8, 25)
        Me.cmbVoucherTp.Name = "cmbVoucherTp"
        Me.cmbVoucherTp.Size = New System.Drawing.Size(250, 21)
        Me.cmbVoucherTp.TabIndex = 345434
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(8, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 13)
        Me.Label4.TabIndex = 345435
        Me.Label4.Text = "Voucher Type"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblDiff)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lblTlDebit)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblTlCredit)
        Me.GroupBox1.Location = New System.Drawing.Point(573, 375)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(245, 88)
        Me.GroupBox1.TabIndex = 345487
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Total"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(6, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 15)
        Me.Label3.TabIndex = 345378
        Me.Label3.Text = "Credit"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDiff
        '
        Me.lblDiff.AutoSize = True
        Me.lblDiff.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblDiff.Location = New System.Drawing.Point(151, 61)
        Me.lblDiff.Name = "lblDiff"
        Me.lblDiff.Size = New System.Drawing.Size(73, 15)
        Me.lblDiff.TabIndex = 345373
        Me.lblDiff.Text = "Difference"
        Me.lblDiff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(6, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 15)
        Me.Label2.TabIndex = 345379
        Me.Label2.Text = "Debit"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTlDebit
        '
        Me.lblTlDebit.AutoSize = True
        Me.lblTlDebit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTlDebit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblTlDebit.Location = New System.Drawing.Point(151, 21)
        Me.lblTlDebit.Name = "lblTlDebit"
        Me.lblTlDebit.Size = New System.Drawing.Size(41, 15)
        Me.lblTlDebit.TabIndex = 345372
        Me.lblTlDebit.Text = "Debit"
        Me.lblTlDebit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(6, 61)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 15)
        Me.Label5.TabIndex = 345380
        Me.Label5.Text = "Difference"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTlCredit
        '
        Me.lblTlCredit.AutoSize = True
        Me.lblTlCredit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTlCredit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblTlCredit.Location = New System.Drawing.Point(151, 41)
        Me.lblTlCredit.Name = "lblTlCredit"
        Me.lblTlCredit.Size = New System.Drawing.Size(45, 15)
        Me.lblTlCredit.TabIndex = 345371
        Me.lblTlCredit.Text = "Credit"
        Me.lblTlCredit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btndelete.BackColor = System.Drawing.Color.SteelBlue
        Me.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.ForeColor = System.Drawing.Color.White
        Me.btndelete.Location = New System.Drawing.Point(491, 428)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(85, 35)
        Me.btndelete.TabIndex = 345488
        Me.btndelete.Text = "Delete"
        Me.btndelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndelete.UseVisualStyleBackColor = False
        Me.btndelete.Visible = False
        '
        'VoucherWiseReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1095, 466)
        Me.ControlBox = False
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.grdvoucher)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.cmbcolms)
        Me.Controls.Add(Me.plvoucher)
        Me.Name = "VoucherWiseReport"
        Me.Text = "Voucher Wise Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.plvoucher.ResumeLayout(False)
        Me.plvoucher.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Public WithEvents rdovoucher As System.Windows.Forms.RadioButton
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents cldrEnddate As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Public WithEvents rdoExpense As System.Windows.Forms.RadioButton
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Public WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdvoucher As System.Windows.Forms.DataGridView
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Public WithEvents cmbcolms As System.Windows.Forms.ComboBox
    Friend WithEvents plvoucher As System.Windows.Forms.Panel
    Friend WithEvents cmbVoucherTp As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblDiff As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblTlDebit As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblTlCredit As System.Windows.Forms.Label
    Friend WithEvents chkTally As System.Windows.Forms.CheckBox
    Friend WithEvents btndelete As System.Windows.Forms.Button
End Class
