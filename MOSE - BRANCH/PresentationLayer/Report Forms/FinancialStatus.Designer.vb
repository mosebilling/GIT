<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FinancialStatus
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.rdoDaybook = New System.Windows.Forms.RadioButton
        Me._optRpt_4 = New System.Windows.Forms.RadioButton
        Me._optRpt_2 = New System.Windows.Forms.RadioButton
        Me._optRpt_1 = New System.Windows.Forms.RadioButton
        Me._optRpt_7 = New System.Windows.Forms.RadioButton
        Me.cldrstartdate = New System.Windows.Forms.DateTimePicker
        Me._lblAcc_1 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnApply = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me._optReport_5 = New System.Windows.Forms.RadioButton
        Me._optReport_6 = New System.Windows.Forms.RadioButton
        Me._optReport_8 = New System.Windows.Forms.RadioButton
        Me._optReport_7 = New System.Windows.Forms.RadioButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.cldateto = New System.Windows.Forms.DateTimePicker
        Me._chkOpt_1 = New System.Windows.Forms.CheckBox
        Me._chkOpt_0 = New System.Windows.Forms.CheckBox
        Me.dtmonth = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnrefresh = New System.Windows.Forms.Button
        Me.grdvoucher = New System.Windows.Forms.DataGridView
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnweb = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkupdatedate = New System.Windows.Forms.CheckBox
        Me.chkprofitloss = New System.Windows.Forms.CheckBox
        Me.chkbalance = New System.Windows.Forms.CheckBox
        Me.chkfsatatus = New System.Windows.Forms.CheckBox
        Me.chkdailyreports = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtend = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.dtstart = New System.Windows.Forms.DateTimePicker
        Me.Worker = New System.ComponentModel.BackgroundWorker
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.PictureBox2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(815, 32)
        Me.Panel2.TabIndex = 345465
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(41, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(128, 18)
        Me.Label1.TabIndex = 345458
        Me.Label1.Text = "Financial Status"
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
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.Color.Transparent
        Me.Frame2.Controls.Add(Me.rdoDaybook)
        Me.Frame2.Controls.Add(Me._optRpt_4)
        Me.Frame2.Controls.Add(Me._optRpt_2)
        Me.Frame2.Controls.Add(Me._optRpt_1)
        Me.Frame2.Controls.Add(Me._optRpt_7)
        Me.Frame2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(12, 40)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(221, 131)
        Me.Frame2.TabIndex = 345466
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Trial Balance"
        '
        'rdoDaybook
        '
        Me.rdoDaybook.BackColor = System.Drawing.Color.Transparent
        Me.rdoDaybook.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdoDaybook.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoDaybook.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdoDaybook.Location = New System.Drawing.Point(8, 106)
        Me.rdoDaybook.Name = "rdoDaybook"
        Me.rdoDaybook.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdoDaybook.Size = New System.Drawing.Size(165, 17)
        Me.rdoDaybook.TabIndex = 14
        Me.rdoDaybook.Tag = "4"
        Me.rdoDaybook.Text = "Day Book"
        Me.rdoDaybook.UseVisualStyleBackColor = False
        Me.rdoDaybook.Visible = False
        '
        '_optRpt_4
        '
        Me._optRpt_4.BackColor = System.Drawing.Color.Transparent
        Me._optRpt_4.Checked = True
        Me._optRpt_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._optRpt_4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optRpt_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._optRpt_4.Location = New System.Drawing.Point(8, 83)
        Me._optRpt_4.Name = "_optRpt_4"
        Me._optRpt_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optRpt_4.Size = New System.Drawing.Size(165, 17)
        Me._optRpt_4.TabIndex = 13
        Me._optRpt_4.TabStop = True
        Me._optRpt_4.Tag = "4"
        Me._optRpt_4.Text = "Summar&y as on Date"
        Me._optRpt_4.UseVisualStyleBackColor = False
        '
        '_optRpt_2
        '
        Me._optRpt_2.BackColor = System.Drawing.Color.Transparent
        Me._optRpt_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optRpt_2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optRpt_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._optRpt_2.Location = New System.Drawing.Point(8, 39)
        Me._optRpt_2.Name = "_optRpt_2"
        Me._optRpt_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optRpt_2.Size = New System.Drawing.Size(197, 17)
        Me._optRpt_2.TabIndex = 11
        Me._optRpt_2.Tag = "2"
        Me._optRpt_2.Text = "Opening - Su&mmary"
        Me._optRpt_2.UseVisualStyleBackColor = False
        '
        '_optRpt_1
        '
        Me._optRpt_1.BackColor = System.Drawing.Color.Transparent
        Me._optRpt_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optRpt_1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optRpt_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._optRpt_1.Location = New System.Drawing.Point(8, 17)
        Me._optRpt_1.Name = "_optRpt_1"
        Me._optRpt_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optRpt_1.Size = New System.Drawing.Size(237, 17)
        Me._optRpt_1.TabIndex = 10
        Me._optRpt_1.Tag = "1"
        Me._optRpt_1.Text = "Op&ening - Detail (Groupwise)"
        Me._optRpt_1.UseVisualStyleBackColor = False
        '
        '_optRpt_7
        '
        Me._optRpt_7.BackColor = System.Drawing.Color.Transparent
        Me._optRpt_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._optRpt_7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optRpt_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._optRpt_7.Location = New System.Drawing.Point(8, 61)
        Me._optRpt_7.Name = "_optRpt_7"
        Me._optRpt_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optRpt_7.Size = New System.Drawing.Size(205, 17)
        Me._optRpt_7.TabIndex = 8
        Me._optRpt_7.Tag = "7"
        Me._optRpt_7.Text = "De&tail as on Date (Groupwise)"
        Me._optRpt_7.UseVisualStyleBackColor = False
        '
        'cldrstartdate
        '
        Me.cldrstartdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrstartdate.Location = New System.Drawing.Point(89, 291)
        Me.cldrstartdate.Name = "cldrstartdate"
        Me.cldrstartdate.Size = New System.Drawing.Size(96, 20)
        Me.cldrstartdate.TabIndex = 345467
        Me.cldrstartdate.TabStop = False
        '
        '_lblAcc_1
        '
        Me._lblAcc_1.AutoSize = True
        Me._lblAcc_1.BackColor = System.Drawing.Color.Transparent
        Me._lblAcc_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblAcc_1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblAcc_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblAcc_1.Location = New System.Drawing.Point(17, 291)
        Me._lblAcc_1.Name = "_lblAcc_1"
        Me._lblAcc_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblAcc_1.Size = New System.Drawing.Size(70, 13)
        Me._lblAcc_1.TabIndex = 345468
        Me._lblAcc_1.Text = "As on Date"
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
        Me.btnExit.Location = New System.Drawing.Point(718, 348)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(85, 35)
        Me.btnExit.TabIndex = 345474
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
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
        Me.btnApply.Location = New System.Drawing.Point(631, 348)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(85, 35)
        Me.btnApply.TabIndex = 345475
        Me.btnApply.Text = "Preview"
        Me.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnApply.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me._optReport_5)
        Me.GroupBox1.Controls.Add(Me._optReport_6)
        Me.GroupBox1.Controls.Add(Me._optReport_8)
        Me.GroupBox1.Controls.Add(Me._optReport_7)
        Me.GroupBox1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(12, 180)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(221, 105)
        Me.GroupBox1.TabIndex = 345476
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Financial Position"
        '
        '_optReport_5
        '
        Me._optReport_5.BackColor = System.Drawing.Color.Transparent
        Me._optReport_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._optReport_5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optReport_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._optReport_5.Location = New System.Drawing.Point(7, 17)
        Me._optReport_5.Name = "_optReport_5"
        Me._optReport_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optReport_5.Size = New System.Drawing.Size(166, 18)
        Me._optReport_5.TabIndex = 72
        Me._optReport_5.TabStop = True
        Me._optReport_5.Tag = "AMM"
        Me._optReport_5.Text = "Profit && Loss (Summary)"
        Me._optReport_5.UseVisualStyleBackColor = False
        '
        '_optReport_6
        '
        Me._optReport_6.BackColor = System.Drawing.Color.Transparent
        Me._optReport_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._optReport_6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optReport_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._optReport_6.Location = New System.Drawing.Point(7, 38)
        Me._optReport_6.Name = "_optReport_6"
        Me._optReport_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optReport_6.Size = New System.Drawing.Size(150, 18)
        Me._optReport_6.TabIndex = 71
        Me._optReport_6.TabStop = True
        Me._optReport_6.Tag = "AMM"
        Me._optReport_6.Text = "Profit && Loss (Detail)"
        Me._optReport_6.UseVisualStyleBackColor = False
        '
        '_optReport_8
        '
        Me._optReport_8.BackColor = System.Drawing.Color.Transparent
        Me._optReport_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._optReport_8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optReport_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._optReport_8.Location = New System.Drawing.Point(7, 80)
        Me._optReport_8.Name = "_optReport_8"
        Me._optReport_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optReport_8.Size = New System.Drawing.Size(174, 18)
        Me._optReport_8.TabIndex = 70
        Me._optReport_8.TabStop = True
        Me._optReport_8.Tag = "AMM"
        Me._optReport_8.Text = "Balance Sheet (Detail)"
        Me._optReport_8.UseVisualStyleBackColor = False
        '
        '_optReport_7
        '
        Me._optReport_7.AutoSize = True
        Me._optReport_7.BackColor = System.Drawing.Color.Transparent
        Me._optReport_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._optReport_7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optReport_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._optReport_7.Location = New System.Drawing.Point(7, 59)
        Me._optReport_7.Name = "_optReport_7"
        Me._optReport_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optReport_7.Size = New System.Drawing.Size(177, 17)
        Me._optReport_7.TabIndex = 69
        Me._optReport_7.TabStop = True
        Me._optReport_7.Tag = "AMM"
        Me._optReport_7.Text = "Balance Sheet (Summary)"
        Me._optReport_7.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(17, 317)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 345478
        Me.Label2.Text = "Date To"
        '
        'cldateto
        '
        Me.cldateto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldateto.Location = New System.Drawing.Point(89, 317)
        Me.cldateto.Name = "cldateto"
        Me.cldateto.Size = New System.Drawing.Size(96, 20)
        Me.cldateto.TabIndex = 345477
        Me.cldateto.TabStop = False
        '
        '_chkOpt_1
        '
        Me._chkOpt_1.AutoSize = True
        Me._chkOpt_1.BackColor = System.Drawing.Color.Transparent
        Me._chkOpt_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkOpt_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkOpt_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._chkOpt_1.Location = New System.Drawing.Point(19, 362)
        Me._chkOpt_1.Name = "_chkOpt_1"
        Me._chkOpt_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkOpt_1.Size = New System.Drawing.Size(114, 18)
        Me._chkOpt_1.TabIndex = 345480
        Me._chkOpt_1.Text = "Incl. Zero Bal. A/c."
        Me._chkOpt_1.UseVisualStyleBackColor = False
        '
        '_chkOpt_0
        '
        Me._chkOpt_0.AutoSize = True
        Me._chkOpt_0.BackColor = System.Drawing.Color.Transparent
        Me._chkOpt_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkOpt_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkOpt_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._chkOpt_0.Location = New System.Drawing.Point(19, 343)
        Me._chkOpt_0.Name = "_chkOpt_0"
        Me._chkOpt_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkOpt_0.Size = New System.Drawing.Size(90, 18)
        Me._chkOpt_0.TabIndex = 345479
        Me._chkOpt_0.Text = "Excl. Opn Bal"
        Me._chkOpt_0.UseVisualStyleBackColor = False
        '
        'dtmonth
        '
        Me.dtmonth.CustomFormat = "MMM/yyyy"
        Me.dtmonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtmonth.Location = New System.Drawing.Point(329, 40)
        Me.dtmonth.Name = "dtmonth"
        Me.dtmonth.Size = New System.Drawing.Size(87, 20)
        Me.dtmonth.TabIndex = 345481
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(284, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 345482
        Me.Label3.Text = "Month"
        '
        'btnrefresh
        '
        Me.btnrefresh.AutoEllipsis = True
        Me.btnrefresh.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnrefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrefresh.ForeColor = System.Drawing.Color.White
        Me.btnrefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnrefresh.Location = New System.Drawing.Point(422, 38)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(85, 27)
        Me.btnrefresh.TabIndex = 345483
        Me.btnrefresh.Text = "Load"
        Me.btnrefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrefresh.UseVisualStyleBackColor = False
        '
        'grdvoucher
        '
        Me.grdvoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdvoucher.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdvoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdvoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdvoucher.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdvoucher.GridColor = System.Drawing.Color.Gainsboro
        Me.grdvoucher.Location = New System.Drawing.Point(287, 71)
        Me.grdvoucher.Name = "grdvoucher"
        Me.grdvoucher.Size = New System.Drawing.Size(333, 312)
        Me.grdvoucher.TabIndex = 345484
        '
        'Timer1
        '
        '
        'btnweb
        '
        Me.btnweb.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnweb.AutoEllipsis = True
        Me.btnweb.BackColor = System.Drawing.Color.SteelBlue
        Me.btnweb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnweb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnweb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnweb.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnweb.ForeColor = System.Drawing.Color.White
        Me.btnweb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnweb.Location = New System.Drawing.Point(3, 149)
        Me.btnweb.Name = "btnweb"
        Me.btnweb.Size = New System.Drawing.Size(172, 35)
        Me.btnweb.TabIndex = 345485
        Me.btnweb.Text = "Upload to Web"
        Me.btnweb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnweb.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.chkupdatedate)
        Me.Panel1.Controls.Add(Me.chkprofitloss)
        Me.Panel1.Controls.Add(Me.btnweb)
        Me.Panel1.Controls.Add(Me.chkbalance)
        Me.Panel1.Controls.Add(Me.chkfsatatus)
        Me.Panel1.Controls.Add(Me.chkdailyreports)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.dtend)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.dtstart)
        Me.Panel1.Location = New System.Drawing.Point(626, 158)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(176, 188)
        Me.Panel1.TabIndex = 345486
        '
        'chkupdatedate
        '
        Me.chkupdatedate.AutoSize = True
        Me.chkupdatedate.BackColor = System.Drawing.Color.Transparent
        Me.chkupdatedate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkupdatedate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkupdatedate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkupdatedate.Location = New System.Drawing.Point(8, 3)
        Me.chkupdatedate.Name = "chkupdatedate"
        Me.chkupdatedate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkupdatedate.Size = New System.Drawing.Size(116, 18)
        Me.chkupdatedate.TabIndex = 345487
        Me.chkupdatedate.Text = "Update Date range"
        Me.chkupdatedate.UseVisualStyleBackColor = False
        '
        'chkprofitloss
        '
        Me.chkprofitloss.BackColor = System.Drawing.Color.Transparent
        Me.chkprofitloss.Checked = True
        Me.chkprofitloss.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkprofitloss.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkprofitloss.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkprofitloss.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkprofitloss.Location = New System.Drawing.Point(8, 131)
        Me.chkprofitloss.Name = "chkprofitloss"
        Me.chkprofitloss.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkprofitloss.Size = New System.Drawing.Size(117, 17)
        Me.chkprofitloss.TabIndex = 345486
        Me.chkprofitloss.Text = "Profit && Loss"
        Me.chkprofitloss.UseVisualStyleBackColor = False
        '
        'chkbalance
        '
        Me.chkbalance.AutoSize = True
        Me.chkbalance.BackColor = System.Drawing.Color.Transparent
        Me.chkbalance.Checked = True
        Me.chkbalance.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbalance.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkbalance.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbalance.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkbalance.Location = New System.Drawing.Point(8, 113)
        Me.chkbalance.Name = "chkbalance"
        Me.chkbalance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkbalance.Size = New System.Drawing.Size(109, 18)
        Me.chkbalance.TabIndex = 345485
        Me.chkbalance.Text = "Account Balance"
        Me.chkbalance.UseVisualStyleBackColor = False
        '
        'chkfsatatus
        '
        Me.chkfsatatus.AutoSize = True
        Me.chkfsatatus.BackColor = System.Drawing.Color.Transparent
        Me.chkfsatatus.Checked = True
        Me.chkfsatatus.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkfsatatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkfsatatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkfsatatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkfsatatus.Location = New System.Drawing.Point(8, 95)
        Me.chkfsatatus.Name = "chkfsatatus"
        Me.chkfsatatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkfsatatus.Size = New System.Drawing.Size(102, 18)
        Me.chkfsatatus.TabIndex = 345484
        Me.chkfsatatus.Text = "Financial Status"
        Me.chkfsatatus.UseVisualStyleBackColor = False
        '
        'chkdailyreports
        '
        Me.chkdailyreports.AutoSize = True
        Me.chkdailyreports.BackColor = System.Drawing.Color.Transparent
        Me.chkdailyreports.Checked = True
        Me.chkdailyreports.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkdailyreports.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkdailyreports.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkdailyreports.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkdailyreports.Location = New System.Drawing.Point(8, 77)
        Me.chkdailyreports.Name = "chkdailyreports"
        Me.chkdailyreports.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkdailyreports.Size = New System.Drawing.Size(72, 18)
        Me.chkdailyreports.TabIndex = 345483
        Me.chkdailyreports.Text = "Day Book"
        Me.chkdailyreports.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(5, 49)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 345482
        Me.Label4.Text = "Date To"
        '
        'dtend
        '
        Me.dtend.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtend.Location = New System.Drawing.Point(77, 49)
        Me.dtend.Name = "dtend"
        Me.dtend.Size = New System.Drawing.Size(96, 20)
        Me.dtend.TabIndex = 345481
        Me.dtend.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Enabled = False
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(5, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 345480
        Me.Label5.Text = "Date From"
        '
        'dtstart
        '
        Me.dtstart.Enabled = False
        Me.dtstart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtstart.Location = New System.Drawing.Point(77, 26)
        Me.dtstart.Name = "dtstart"
        Me.dtstart.Size = New System.Drawing.Size(96, 20)
        Me.dtstart.TabIndex = 345479
        Me.dtstart.TabStop = False
        '
        'Worker
        '
        '
        'FinancialStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(815, 395)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grdvoucher)
        Me.Controls.Add(Me.btnrefresh)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtmonth)
        Me.Controls.Add(Me._chkOpt_1)
        Me.Controls.Add(Me._chkOpt_0)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cldateto)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me._lblAcc_1)
        Me.Controls.Add(Me.cldrstartdate)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "FinancialStatus"
        Me.Text = "FinancialStatus"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _optRpt_4 As System.Windows.Forms.RadioButton
    Public WithEvents _optRpt_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optRpt_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optRpt_7 As System.Windows.Forms.RadioButton
    Friend WithEvents cldrstartdate As System.Windows.Forms.DateTimePicker
    Public WithEvents _lblAcc_1 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents _optReport_5 As System.Windows.Forms.RadioButton
    Public WithEvents _optReport_6 As System.Windows.Forms.RadioButton
    Public WithEvents _optReport_8 As System.Windows.Forms.RadioButton
    Public WithEvents _optReport_7 As System.Windows.Forms.RadioButton
    Public WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cldateto As System.Windows.Forms.DateTimePicker
    Public WithEvents _chkOpt_1 As System.Windows.Forms.CheckBox
    Public WithEvents _chkOpt_0 As System.Windows.Forms.CheckBox
    Friend WithEvents dtmonth As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnrefresh As System.Windows.Forms.Button
    Friend WithEvents grdvoucher As System.Windows.Forms.DataGridView
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents rdoDaybook As System.Windows.Forms.RadioButton
    Friend WithEvents btnweb As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtend As System.Windows.Forms.DateTimePicker
    Public WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtstart As System.Windows.Forms.DateTimePicker
    Public WithEvents chkfsatatus As System.Windows.Forms.CheckBox
    Public WithEvents chkdailyreports As System.Windows.Forms.CheckBox
    Public WithEvents chkbalance As System.Windows.Forms.CheckBox
    Public WithEvents chkprofitloss As System.Windows.Forms.CheckBox
    Public WithEvents chkupdatedate As System.Windows.Forms.CheckBox
    Friend WithEvents Worker As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
End Class
