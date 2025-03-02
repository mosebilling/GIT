<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PDCTransfer
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
        Me.btnApply = New System.Windows.Forms.Button
        Me.cldrEnddate = New System.Windows.Forms.DateTimePicker
        Me.btnExit = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.rdoAll = New System.Windows.Forms.RadioButton
        Me.rdotrdate = New System.Windows.Forms.RadioButton
        Me.rdochqdate = New System.Windows.Forms.RadioButton
        Me.rdotransaction = New System.Windows.Forms.RadioButton
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.rdoclearList = New System.Windows.Forms.RadioButton
        Me.rdopdclist = New System.Windows.Forms.RadioButton
        Me.btnLoad = New System.Windows.Forms.Button
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.cmbcolms = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.optpdcIs = New System.Windows.Forms.RadioButton
        Me.optPdc = New System.Windows.Forms.RadioButton
        Me.grdvoucher = New System.Windows.Forms.DataGridView
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.dtptrdate = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.btntransfer = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnfind = New System.Windows.Forms.Button
        Me.lblTlDebit = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
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
        Me.btnApply.Location = New System.Drawing.Point(655, 472)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(85, 35)
        Me.btnApply.TabIndex = 345485
        Me.btnApply.Text = "Preview"
        Me.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnApply.UseVisualStyleBackColor = False
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
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.AutoEllipsis = True
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(742, 472)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(85, 35)
        Me.btnExit.TabIndex = 345484
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.rdoAll)
        Me.GroupBox2.Controls.Add(Me.rdotrdate)
        Me.GroupBox2.Controls.Add(Me.rdochqdate)
        Me.GroupBox2.Controls.Add(Me.rdotransaction)
        Me.GroupBox2.Controls.Add(Me.cldrEnddate)
        Me.GroupBox2.Controls.Add(Me.cldrStartDate)
        Me.GroupBox2.Location = New System.Drawing.Point(616, 372)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(211, 94)
        Me.GroupBox2.TabIndex = 345483
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Date Parameter"
        '
        'rdoAll
        '
        Me.rdoAll.AutoSize = True
        Me.rdoAll.BackColor = System.Drawing.Color.Transparent
        Me.rdoAll.Checked = True
        Me.rdoAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdoAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdoAll.Location = New System.Drawing.Point(118, 66)
        Me.rdoAll.Name = "rdoAll"
        Me.rdoAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdoAll.Size = New System.Drawing.Size(36, 17)
        Me.rdoAll.TabIndex = 345397
        Me.rdoAll.TabStop = True
        Me.rdoAll.Text = "All"
        Me.rdoAll.UseVisualStyleBackColor = False
        '
        'rdotrdate
        '
        Me.rdotrdate.AutoSize = True
        Me.rdotrdate.BackColor = System.Drawing.Color.Transparent
        Me.rdotrdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdotrdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdotrdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdotrdate.Location = New System.Drawing.Point(118, 48)
        Me.rdotrdate.Name = "rdotrdate"
        Me.rdotrdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdotrdate.Size = New System.Drawing.Size(90, 17)
        Me.rdotrdate.TabIndex = 345396
        Me.rdotrdate.Text = "Transfer Date"
        Me.rdotrdate.UseVisualStyleBackColor = False
        Me.rdotrdate.Visible = False
        '
        'rdochqdate
        '
        Me.rdochqdate.AutoSize = True
        Me.rdochqdate.BackColor = System.Drawing.Color.Transparent
        Me.rdochqdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdochqdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdochqdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdochqdate.Location = New System.Drawing.Point(9, 48)
        Me.rdochqdate.Name = "rdochqdate"
        Me.rdochqdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdochqdate.Size = New System.Drawing.Size(88, 17)
        Me.rdochqdate.TabIndex = 15
        Me.rdochqdate.Text = "Cheque Date"
        Me.rdochqdate.UseVisualStyleBackColor = False
        '
        'rdotransaction
        '
        Me.rdotransaction.AutoSize = True
        Me.rdotransaction.BackColor = System.Drawing.Color.Transparent
        Me.rdotransaction.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdotransaction.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdotransaction.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdotransaction.Location = New System.Drawing.Point(9, 66)
        Me.rdotransaction.Name = "rdotransaction"
        Me.rdotransaction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdotransaction.Size = New System.Drawing.Size(107, 17)
        Me.rdotransaction.TabIndex = 14
        Me.rdotransaction.Text = "Transaction Date"
        Me.rdotransaction.UseVisualStyleBackColor = False
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
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.rdoclearList)
        Me.GroupBox3.Controls.Add(Me.rdopdclist)
        Me.GroupBox3.Location = New System.Drawing.Point(475, 373)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(135, 65)
        Me.GroupBox3.TabIndex = 345486
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Select Report"
        '
        'rdoclearList
        '
        Me.rdoclearList.AutoSize = True
        Me.rdoclearList.BackColor = System.Drawing.Color.Transparent
        Me.rdoclearList.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdoclearList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoclearList.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdoclearList.Location = New System.Drawing.Point(6, 42)
        Me.rdoclearList.Name = "rdoclearList"
        Me.rdoclearList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdoclearList.Size = New System.Drawing.Size(105, 17)
        Me.rdoclearList.TabIndex = 345479
        Me.rdoclearList.Text = "PDC List Cleared"
        Me.rdoclearList.UseVisualStyleBackColor = False
        '
        'rdopdclist
        '
        Me.rdopdclist.AutoSize = True
        Me.rdopdclist.BackColor = System.Drawing.Color.Transparent
        Me.rdopdclist.Checked = True
        Me.rdopdclist.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdopdclist.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdopdclist.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdopdclist.Location = New System.Drawing.Point(6, 19)
        Me.rdopdclist.Name = "rdopdclist"
        Me.rdopdclist.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdopdclist.Size = New System.Drawing.Size(118, 17)
        Me.rdopdclist.TabIndex = 345478
        Me.rdopdclist.TabStop = True
        Me.rdopdclist.Text = "PDC List Uncleared"
        Me.rdopdclist.UseVisualStyleBackColor = False
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.BackColor = System.Drawing.Color.SteelBlue
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.ForeColor = System.Drawing.Color.White
        Me.btnLoad.Location = New System.Drawing.Point(568, 472)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(85, 35)
        Me.btnLoad.TabIndex = 345482
        Me.btnLoad.Text = "&Load"
        Me.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLoad.UseVisualStyleBackColor = False
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(10, 490)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345481
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
        Me.chkSearch.Visible = False
        '
        'cmbcolms
        '
        Me.cmbcolms.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbcolms.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcolms.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbcolms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcolms.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcolms.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcolms.Location = New System.Drawing.Point(3, 345)
        Me.cmbcolms.Name = "cmbcolms"
        Me.cmbcolms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbcolms.Size = New System.Drawing.Size(166, 22)
        Me.cmbcolms.TabIndex = 345479
        Me.cmbcolms.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(41, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 18)
        Me.Label1.TabIndex = 345458
        Me.Label1.Text = "PDC Transfer"
        '
        'optpdcIs
        '
        Me.optpdcIs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optpdcIs.BackColor = System.Drawing.Color.Transparent
        Me.optpdcIs.Cursor = System.Windows.Forms.Cursors.Default
        Me.optpdcIs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optpdcIs.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optpdcIs.Location = New System.Drawing.Point(4, 391)
        Me.optpdcIs.Name = "optpdcIs"
        Me.optpdcIs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optpdcIs.Size = New System.Drawing.Size(95, 18)
        Me.optpdcIs.TabIndex = 12
        Me.optpdcIs.Tag = "6"
        Me.optpdcIs.Text = "P.D.C.(I)"
        Me.optpdcIs.UseVisualStyleBackColor = False
        '
        'optPdc
        '
        Me.optPdc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optPdc.BackColor = System.Drawing.Color.Transparent
        Me.optPdc.Checked = True
        Me.optPdc.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPdc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPdc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPdc.Location = New System.Drawing.Point(4, 373)
        Me.optPdc.Name = "optPdc"
        Me.optPdc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPdc.Size = New System.Drawing.Size(101, 18)
        Me.optPdc.TabIndex = 18
        Me.optPdc.TabStop = True
        Me.optPdc.Tag = "5"
        Me.optPdc.Text = "P.D.C.(R)"
        Me.optPdc.UseVisualStyleBackColor = False
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
        Me.grdvoucher.Size = New System.Drawing.Size(823, 308)
        Me.grdvoucher.TabIndex = 345477
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
        Me.txtSeq.Location = New System.Drawing.Point(175, 346)
        Me.txtSeq.MaxLength = 50
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(203, 20)
        Me.txtSeq.TabIndex = 345480
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.PictureBox3)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(833, 32)
        Me.Panel2.TabIndex = 345476
        '
        'PictureBox3
        '
        Me.PictureBox3.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox3.Location = New System.Drawing.Point(2, 5)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(37, 18)
        Me.PictureBox3.TabIndex = 345461
        Me.PictureBox3.TabStop = False
        '
        'Timer1
        '
        '
        'dtptrdate
        '
        Me.dtptrdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtptrdate.Location = New System.Drawing.Point(80, 3)
        Me.dtptrdate.Name = "dtptrdate"
        Me.dtptrdate.Size = New System.Drawing.Size(95, 20)
        Me.dtptrdate.TabIndex = 345487
        Me.dtptrdate.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(2, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 345488
        Me.Label2.Text = "Transfer Date"
        '
        'btntransfer
        '
        Me.btntransfer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btntransfer.BackColor = System.Drawing.Color.SteelBlue
        Me.btntransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btntransfer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntransfer.ForeColor = System.Drawing.Color.White
        Me.btntransfer.Location = New System.Drawing.Point(293, 372)
        Me.btntransfer.Name = "btntransfer"
        Me.btntransfer.Size = New System.Drawing.Size(85, 35)
        Me.btntransfer.TabIndex = 345489
        Me.btntransfer.Text = "Transfer"
        Me.btntransfer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btntransfer.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.dtptrdate)
        Me.Panel1.Location = New System.Drawing.Point(107, 372)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(180, 24)
        Me.Panel1.TabIndex = 345490
        '
        'btnfind
        '
        Me.btnfind.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnfind.BackColor = System.Drawing.Color.SteelBlue
        Me.btnfind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnfind.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnfind.ForeColor = System.Drawing.Color.White
        Me.btnfind.Location = New System.Drawing.Point(384, 345)
        Me.btnfind.Name = "btnfind"
        Me.btnfind.Size = New System.Drawing.Size(66, 23)
        Me.btnfind.TabIndex = 345491
        Me.btnfind.Text = "Search"
        Me.btnfind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnfind.UseVisualStyleBackColor = False
        '
        'lblTlDebit
        '
        Me.lblTlDebit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTlDebit.AutoSize = True
        Me.lblTlDebit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTlDebit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblTlDebit.Location = New System.Drawing.Point(12, 451)
        Me.lblTlDebit.Name = "lblTlDebit"
        Me.lblTlDebit.Size = New System.Drawing.Size(41, 15)
        Me.lblTlDebit.TabIndex = 345492
        Me.lblTlDebit.Text = "Debit"
        Me.lblTlDebit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(12, 433)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 15)
        Me.Label3.TabIndex = 345493
        Me.Label3.Text = "Total"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PDCTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(833, 512)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblTlDebit)
        Me.Controls.Add(Me.btnfind)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btntransfer)
        Me.Controls.Add(Me.optpdcIs)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.optPdc)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.cmbcolms)
        Me.Controls.Add(Me.grdvoucher)
        Me.Controls.Add(Me.txtSeq)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "PDCTransfer"
        Me.Text = "PDCTransfer"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents cldrEnddate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Public WithEvents rdochqdate As System.Windows.Forms.RadioButton
    Public WithEvents rdotransaction As System.Windows.Forms.RadioButton
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Public WithEvents cmbcolms As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents optpdcIs As System.Windows.Forms.RadioButton
    Public WithEvents optPdc As System.Windows.Forms.RadioButton
    Friend WithEvents grdvoucher As System.Windows.Forms.DataGridView
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Public WithEvents rdopdclist As System.Windows.Forms.RadioButton
    Public WithEvents rdotrdate As System.Windows.Forms.RadioButton
    Public WithEvents rdoclearList As System.Windows.Forms.RadioButton
    Public WithEvents rdoAll As System.Windows.Forms.RadioButton
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents dtptrdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btntransfer As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnfind As System.Windows.Forms.Button
    Friend WithEvents lblTlDebit As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
