<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TaxReportFrm
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
        Me.components = New System.ComponentModel.Container
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblName = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btnLoad = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cldrEnddate = New System.Windows.Forms.DateTimePicker
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbcolms = New System.Windows.Forms.ComboBox
        Me.grdvoucher = New System.Windows.Forms.DataGridView
        Me.rdoInvoicewise = New System.Windows.Forms.RadioButton
        Me.rdoItemwise = New System.Windows.Forms.RadioButton
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.rdosales = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbgstslab = New System.Windows.Forms.ComboBox
        Me.rdopercentagewise = New System.Windows.Forms.RadioButton
        Me.rdohsncode = New System.Windows.Forms.RadioButton
        Me.rdopurchase = New System.Windows.Forms.RadioButton
        Me.rdoboth = New System.Windows.Forms.RadioButton
        Me.chkwithgst = New System.Windows.Forms.CheckBox
        Me.chkwithoutgst = New System.Windows.Forms.CheckBox
        Me.chkb2c = New System.Windows.Forms.CheckBox
        Me.chkb2b = New System.Windows.Forms.CheckBox
        Me.rdooutputtax = New System.Windows.Forms.RadioButton
        Me.chkFormat = New System.Windows.Forms.CheckBox
        Me.rdopurchasereturn = New System.Windows.Forms.RadioButton
        Me.rdosalesreturn = New System.Windows.Forms.RadioButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.lblName)
        Me.Panel2.Controls.Add(Me.PictureBox2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1061, 32)
        Me.Panel2.TabIndex = 345465
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.White
        Me.lblName.Font = New System.Drawing.Font("Goudy Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblName.Location = New System.Drawing.Point(41, 5)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(111, 21)
        Me.lblName.TabIndex = 345458
        Me.lblName.Text = "GST REPORT "
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
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.AutoEllipsis = True
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(956, 420)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(96, 35)
        Me.btnExit.TabIndex = 345466
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.AutoEllipsis = True
        Me.btnPreview.BackColor = System.Drawing.Color.SteelBlue
        Me.btnPreview.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.White
        Me.btnPreview.Location = New System.Drawing.Point(860, 420)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(96, 35)
        Me.btnPreview.TabIndex = 345467
        Me.btnPreview.Text = "&Preview"
        Me.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.BackColor = System.Drawing.Color.SteelBlue
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.ForeColor = System.Drawing.Color.White
        Me.btnLoad.Location = New System.Drawing.Point(764, 420)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(96, 35)
        Me.btnLoad.TabIndex = 345472
        Me.btnLoad.Text = "&Load"
        Me.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLoad.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.cldrEnddate)
        Me.GroupBox2.Controls.Add(Me.cldrStartDate)
        Me.GroupBox2.Location = New System.Drawing.Point(837, 363)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(215, 51)
        Me.GroupBox2.TabIndex = 345473
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Date Parameter"
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
        Me.chkSearch.Location = New System.Drawing.Point(382, 308)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345477
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
        '
        'txtSeq
        '
        Me.txtSeq.AcceptsReturn = True
        Me.txtSeq.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSeq.BackColor = System.Drawing.SystemColors.Window
        Me.txtSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeq.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSeq.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeq.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSeq.Location = New System.Drawing.Point(173, 305)
        Me.txtSeq.MaxLength = 50
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(203, 20)
        Me.txtSeq.TabIndex = 345476
        '
        'cmbcolms
        '
        Me.cmbcolms.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbcolms.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcolms.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbcolms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcolms.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcolms.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcolms.Location = New System.Drawing.Point(1, 304)
        Me.cmbcolms.Name = "cmbcolms"
        Me.cmbcolms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbcolms.Size = New System.Drawing.Size(166, 22)
        Me.cmbcolms.TabIndex = 345475
        Me.cmbcolms.TabStop = False
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
        Me.grdvoucher.Location = New System.Drawing.Point(3, 34)
        Me.grdvoucher.Name = "grdvoucher"
        Me.grdvoucher.Size = New System.Drawing.Size(1054, 265)
        Me.grdvoucher.TabIndex = 345474
        '
        'rdoInvoicewise
        '
        Me.rdoInvoicewise.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdoInvoicewise.AutoSize = True
        Me.rdoInvoicewise.BackColor = System.Drawing.Color.Transparent
        Me.rdoInvoicewise.Checked = True
        Me.rdoInvoicewise.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdoInvoicewise.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoInvoicewise.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdoInvoicewise.Location = New System.Drawing.Point(6, 24)
        Me.rdoInvoicewise.Name = "rdoInvoicewise"
        Me.rdoInvoicewise.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdoInvoicewise.Size = New System.Drawing.Size(84, 17)
        Me.rdoInvoicewise.TabIndex = 345479
        Me.rdoInvoicewise.TabStop = True
        Me.rdoInvoicewise.Text = "Invoice wise"
        Me.rdoInvoicewise.UseVisualStyleBackColor = False
        '
        'rdoItemwise
        '
        Me.rdoItemwise.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdoItemwise.AutoSize = True
        Me.rdoItemwise.BackColor = System.Drawing.Color.Transparent
        Me.rdoItemwise.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdoItemwise.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoItemwise.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdoItemwise.Location = New System.Drawing.Point(6, 59)
        Me.rdoItemwise.Name = "rdoItemwise"
        Me.rdoItemwise.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdoItemwise.Size = New System.Drawing.Size(69, 17)
        Me.rdoItemwise.TabIndex = 345478
        Me.rdoItemwise.Text = "Item wise"
        Me.rdoItemwise.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        '
        'rdosales
        '
        Me.rdosales.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdosales.AutoSize = True
        Me.rdosales.BackColor = System.Drawing.Color.Transparent
        Me.rdosales.Checked = True
        Me.rdosales.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdosales.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdosales.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdosales.Location = New System.Drawing.Point(3, 364)
        Me.rdosales.Name = "rdosales"
        Me.rdosales.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdosales.Size = New System.Drawing.Size(105, 17)
        Me.rdosales.TabIndex = 345480
        Me.rdosales.TabStop = True
        Me.rdosales.Text = "Tax / GST Sales"
        Me.rdosales.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.cmbgstslab)
        Me.GroupBox1.Controls.Add(Me.rdopercentagewise)
        Me.GroupBox1.Controls.Add(Me.rdohsncode)
        Me.GroupBox1.Controls.Add(Me.rdoInvoicewise)
        Me.GroupBox1.Controls.Add(Me.rdoItemwise)
        Me.GroupBox1.Location = New System.Drawing.Point(179, 16)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(222, 91)
        Me.GroupBox1.TabIndex = 345481
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Options"
        '
        'cmbgstslab
        '
        Me.cmbgstslab.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbgstslab.BackColor = System.Drawing.SystemColors.Window
        Me.cmbgstslab.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbgstslab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbgstslab.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbgstslab.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbgstslab.Location = New System.Drawing.Point(115, 42)
        Me.cmbgstslab.Name = "cmbgstslab"
        Me.cmbgstslab.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbgstslab.Size = New System.Drawing.Size(101, 22)
        Me.cmbgstslab.TabIndex = 345482
        Me.cmbgstslab.TabStop = False
        Me.cmbgstslab.Visible = False
        '
        'rdopercentagewise
        '
        Me.rdopercentagewise.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdopercentagewise.AutoSize = True
        Me.rdopercentagewise.BackColor = System.Drawing.Color.Transparent
        Me.rdopercentagewise.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdopercentagewise.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdopercentagewise.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdopercentagewise.Location = New System.Drawing.Point(115, 24)
        Me.rdopercentagewise.Name = "rdopercentagewise"
        Me.rdopercentagewise.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdopercentagewise.Size = New System.Drawing.Size(104, 17)
        Me.rdopercentagewise.TabIndex = 345481
        Me.rdopercentagewise.Text = "Percentage wise"
        Me.rdopercentagewise.UseVisualStyleBackColor = False
        '
        'rdohsncode
        '
        Me.rdohsncode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdohsncode.AutoSize = True
        Me.rdohsncode.BackColor = System.Drawing.Color.Transparent
        Me.rdohsncode.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdohsncode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdohsncode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdohsncode.Location = New System.Drawing.Point(6, 41)
        Me.rdohsncode.Name = "rdohsncode"
        Me.rdohsncode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdohsncode.Size = New System.Drawing.Size(100, 17)
        Me.rdohsncode.TabIndex = 345480
        Me.rdohsncode.Text = "HSN Code wise"
        Me.rdohsncode.UseVisualStyleBackColor = False
        '
        'rdopurchase
        '
        Me.rdopurchase.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdopurchase.AutoSize = True
        Me.rdopurchase.BackColor = System.Drawing.Color.Transparent
        Me.rdopurchase.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdopurchase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdopurchase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdopurchase.Location = New System.Drawing.Point(3, 382)
        Me.rdopurchase.Name = "rdopurchase"
        Me.rdopurchase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdopurchase.Size = New System.Drawing.Size(124, 17)
        Me.rdopurchase.TabIndex = 345482
        Me.rdopurchase.Text = "Tax / GST Purchase"
        Me.rdopurchase.UseVisualStyleBackColor = False
        '
        'rdoboth
        '
        Me.rdoboth.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdoboth.AutoSize = True
        Me.rdoboth.BackColor = System.Drawing.Color.Transparent
        Me.rdoboth.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdoboth.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoboth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdoboth.Location = New System.Drawing.Point(6, 19)
        Me.rdoboth.Name = "rdoboth"
        Me.rdoboth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdoboth.Size = New System.Drawing.Size(162, 17)
        Me.rdoboth.TabIndex = 345483
        Me.rdoboth.Text = "Tax / GST Purchase && Sales"
        Me.rdoboth.UseVisualStyleBackColor = False
        '
        'chkwithgst
        '
        Me.chkwithgst.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkwithgst.AutoSize = True
        Me.chkwithgst.BackColor = System.Drawing.Color.Transparent
        Me.chkwithgst.ForeColor = System.Drawing.Color.Black
        Me.chkwithgst.Location = New System.Drawing.Point(173, 401)
        Me.chkwithgst.Name = "chkwithgst"
        Me.chkwithgst.Size = New System.Drawing.Size(81, 17)
        Me.chkwithgst.TabIndex = 345484
        Me.chkwithgst.Text = "With GSTN"
        Me.chkwithgst.UseVisualStyleBackColor = False
        '
        'chkwithoutgst
        '
        Me.chkwithoutgst.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkwithoutgst.AutoSize = True
        Me.chkwithoutgst.BackColor = System.Drawing.Color.Transparent
        Me.chkwithoutgst.ForeColor = System.Drawing.Color.Black
        Me.chkwithoutgst.Location = New System.Drawing.Point(173, 420)
        Me.chkwithoutgst.Name = "chkwithoutgst"
        Me.chkwithoutgst.Size = New System.Drawing.Size(96, 17)
        Me.chkwithoutgst.TabIndex = 345485
        Me.chkwithoutgst.Text = "Without GSTN"
        Me.chkwithoutgst.UseVisualStyleBackColor = False
        '
        'chkb2c
        '
        Me.chkb2c.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkb2c.AutoSize = True
        Me.chkb2c.BackColor = System.Drawing.Color.Transparent
        Me.chkb2c.ForeColor = System.Drawing.Color.Black
        Me.chkb2c.Location = New System.Drawing.Point(281, 420)
        Me.chkb2c.Name = "chkb2c"
        Me.chkb2c.Size = New System.Drawing.Size(46, 17)
        Me.chkb2c.TabIndex = 345487
        Me.chkb2c.Text = "B2C"
        Me.chkb2c.UseVisualStyleBackColor = False
        '
        'chkb2b
        '
        Me.chkb2b.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkb2b.AutoSize = True
        Me.chkb2b.BackColor = System.Drawing.Color.Transparent
        Me.chkb2b.ForeColor = System.Drawing.Color.Black
        Me.chkb2b.Location = New System.Drawing.Point(281, 401)
        Me.chkb2b.Name = "chkb2b"
        Me.chkb2b.Size = New System.Drawing.Size(46, 17)
        Me.chkb2b.TabIndex = 345486
        Me.chkb2b.Text = "B2B"
        Me.chkb2b.UseVisualStyleBackColor = False
        '
        'rdooutputtax
        '
        Me.rdooutputtax.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdooutputtax.AutoSize = True
        Me.rdooutputtax.BackColor = System.Drawing.Color.Transparent
        Me.rdooutputtax.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdooutputtax.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdooutputtax.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdooutputtax.Location = New System.Drawing.Point(6, 42)
        Me.rdooutputtax.Name = "rdooutputtax"
        Me.rdooutputtax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdooutputtax.Size = New System.Drawing.Size(129, 17)
        Me.rdooutputtax.TabIndex = 345488
        Me.rdooutputtax.Text = "Output Tax Statement"
        Me.rdooutputtax.UseVisualStyleBackColor = False
        '
        'chkFormat
        '
        Me.chkFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(766, 395)
        Me.chkFormat.Name = "chkFormat"
        Me.chkFormat.Size = New System.Drawing.Size(65, 19)
        Me.chkFormat.TabIndex = 345489
        Me.chkFormat.Text = "Format"
        Me.chkFormat.UseVisualStyleBackColor = False
        '
        'rdopurchasereturn
        '
        Me.rdopurchasereturn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdopurchasereturn.AutoSize = True
        Me.rdopurchasereturn.BackColor = System.Drawing.Color.Transparent
        Me.rdopurchasereturn.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdopurchasereturn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdopurchasereturn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdopurchasereturn.Location = New System.Drawing.Point(3, 401)
        Me.rdopurchasereturn.Name = "rdopurchasereturn"
        Me.rdopurchasereturn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdopurchasereturn.Size = New System.Drawing.Size(159, 17)
        Me.rdopurchasereturn.TabIndex = 345490
        Me.rdopurchasereturn.Text = "Tax / GST Purchase Return"
        Me.rdopurchasereturn.UseVisualStyleBackColor = False
        '
        'rdosalesreturn
        '
        Me.rdosalesreturn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdosalesreturn.AutoSize = True
        Me.rdosalesreturn.BackColor = System.Drawing.Color.Transparent
        Me.rdosalesreturn.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdosalesreturn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdosalesreturn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdosalesreturn.Location = New System.Drawing.Point(3, 419)
        Me.rdosalesreturn.Name = "rdosalesreturn"
        Me.rdosalesreturn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdosalesreturn.Size = New System.Drawing.Size(140, 17)
        Me.rdosalesreturn.TabIndex = 345491
        Me.rdosalesreturn.Text = "Tax / GST Sales Return"
        Me.rdosalesreturn.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.rdooutputtax)
        Me.GroupBox3.Controls.Add(Me.GroupBox1)
        Me.GroupBox3.Controls.Add(Me.rdoboth)
        Me.GroupBox3.Location = New System.Drawing.Point(333, 334)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(416, 121)
        Me.GroupBox3.TabIndex = 345492
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Preview"
        '
        'TaxReportFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1061, 464)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.rdosalesreturn)
        Me.Controls.Add(Me.rdopurchasereturn)
        Me.Controls.Add(Me.chkwithgst)
        Me.Controls.Add(Me.chkb2c)
        Me.Controls.Add(Me.chkFormat)
        Me.Controls.Add(Me.chkwithoutgst)
        Me.Controls.Add(Me.rdopurchase)
        Me.Controls.Add(Me.chkb2b)
        Me.Controls.Add(Me.rdosales)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.txtSeq)
        Me.Controls.Add(Me.cmbcolms)
        Me.Controls.Add(Me.grdvoucher)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "TaxReportFrm"
        Me.Text = "Sales Profit Analysis"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cldrEnddate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbcolms As System.Windows.Forms.ComboBox
    Friend WithEvents grdvoucher As System.Windows.Forms.DataGridView
    Public WithEvents rdoInvoicewise As System.Windows.Forms.RadioButton
    Public WithEvents rdoItemwise As System.Windows.Forms.RadioButton
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents rdosales As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents rdopurchase As System.Windows.Forms.RadioButton
    Public WithEvents rdoboth As System.Windows.Forms.RadioButton
    Public WithEvents rdohsncode As System.Windows.Forms.RadioButton
    Public WithEvents rdopercentagewise As System.Windows.Forms.RadioButton
    Friend WithEvents chkwithgst As System.Windows.Forms.CheckBox
    Friend WithEvents chkwithoutgst As System.Windows.Forms.CheckBox
    Public WithEvents cmbgstslab As System.Windows.Forms.ComboBox
    Friend WithEvents chkb2c As System.Windows.Forms.CheckBox
    Friend WithEvents chkb2b As System.Windows.Forms.CheckBox
    Public WithEvents rdooutputtax As System.Windows.Forms.RadioButton
    Friend WithEvents chkFormat As System.Windows.Forms.CheckBox
    Public WithEvents rdopurchasereturn As System.Windows.Forms.RadioButton
    Public WithEvents rdosalesreturn As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
End Class
