<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FinancialStatements
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
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.grdvoucher = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblarea = New System.Windows.Forms.Label
        Me.cmbcategory = New System.Windows.Forms.ComboBox
        Me.cmbarea = New System.Windows.Forms.ComboBox
        Me.optroth = New System.Windows.Forms.RadioButton
        Me.optpoth = New System.Windows.Forms.RadioButton
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbcolms = New System.Windows.Forms.ComboBox
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.btnLoad = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cldrEnddate = New System.Windows.Forms.DateTimePicker
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.btnApply = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.chkremoveadvance = New System.Windows.Forms.CheckBox
        Me.rdovazhipadurate = New System.Windows.Forms.RadioButton
        Me.rdopdclist = New System.Windows.Forms.RadioButton
        Me.chkageing = New System.Windows.Forms.CheckBox
        Me.rdoAddress = New System.Windows.Forms.RadioButton
        Me.rdoAccountBalance = New System.Windows.Forms.RadioButton
        Me.rdostatement = New System.Windows.Forms.RadioButton
        Me.rdooutstanding = New System.Windows.Forms.RadioButton
        Me.chkdeliveredBy = New System.Windows.Forms.CheckBox
        Me.cmbdeliveredBy = New System.Windows.Forms.ComboBox
        Me.btnreconciliation = New System.Windows.Forms.Button
        Me.btnsms = New System.Windows.Forms.Button
        Me.chkFormat = New System.Windows.Forms.CheckBox
        Me.btnrv = New System.Windows.Forms.Button
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.PictureBox2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(895, 32)
        Me.Panel2.TabIndex = 345464
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(41, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 18)
        Me.Label1.TabIndex = 345458
        Me.Label1.Text = "Financial Reports"
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
        'grdvoucher
        '
        Me.grdvoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdvoucher.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdvoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdvoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdvoucher.GridColor = System.Drawing.Color.Gainsboro
        Me.grdvoucher.Location = New System.Drawing.Point(4, 35)
        Me.grdvoucher.Name = "grdvoucher"
        Me.grdvoucher.Size = New System.Drawing.Size(888, 237)
        Me.grdvoucher.TabIndex = 345465
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.lblarea)
        Me.GroupBox1.Controls.Add(Me.cmbcategory)
        Me.GroupBox1.Controls.Add(Me.cmbarea)
        Me.GroupBox1.Controls.Add(Me.optroth)
        Me.GroupBox1.Controls.Add(Me.optpoth)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 298)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(146, 159)
        Me.GroupBox1.TabIndex = 345466
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select Category"
        '
        'lblarea
        '
        Me.lblarea.AutoSize = True
        Me.lblarea.BackColor = System.Drawing.Color.Transparent
        Me.lblarea.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblarea.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblarea.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblarea.Location = New System.Drawing.Point(6, 110)
        Me.lblarea.Name = "lblarea"
        Me.lblarea.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblarea.Size = New System.Drawing.Size(65, 14)
        Me.lblarea.TabIndex = 345496
        Me.lblarea.Text = "Sales Route"
        '
        'cmbcategory
        '
        Me.cmbcategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcategory.FormattingEnabled = True
        Me.cmbcategory.Items.AddRange(New Object() {"Customer", "Supplier", "Bank", "Cash", "P.D.C.(R)", "P.D.C.(I)", "Vazhipadu", "ALL"})
        Me.cmbcategory.Location = New System.Drawing.Point(8, 18)
        Me.cmbcategory.Name = "cmbcategory"
        Me.cmbcategory.Size = New System.Drawing.Size(132, 21)
        Me.cmbcategory.TabIndex = 345491
        '
        'cmbarea
        '
        Me.cmbarea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbarea.FormattingEnabled = True
        Me.cmbarea.Location = New System.Drawing.Point(8, 127)
        Me.cmbarea.Name = "cmbarea"
        Me.cmbarea.Size = New System.Drawing.Size(109, 21)
        Me.cmbarea.TabIndex = 345495
        '
        'optroth
        '
        Me.optroth.BackColor = System.Drawing.Color.Transparent
        Me.optroth.Cursor = System.Windows.Forms.Cursors.Default
        Me.optroth.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optroth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optroth.Location = New System.Drawing.Point(8, 65)
        Me.optroth.Name = "optroth"
        Me.optroth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optroth.Size = New System.Drawing.Size(58, 18)
        Me.optroth.TabIndex = 20
        Me.optroth.Tag = "1"
        Me.optroth.Text = "ROTH"
        Me.optroth.UseVisualStyleBackColor = False
        Me.optroth.Visible = False
        '
        'optpoth
        '
        Me.optpoth.BackColor = System.Drawing.Color.Transparent
        Me.optpoth.Cursor = System.Windows.Forms.Cursors.Default
        Me.optpoth.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optpoth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optpoth.Location = New System.Drawing.Point(8, 45)
        Me.optpoth.Name = "optpoth"
        Me.optpoth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optpoth.Size = New System.Drawing.Size(64, 18)
        Me.optpoth.TabIndex = 19
        Me.optpoth.Tag = "1"
        Me.optpoth.Text = "POTH"
        Me.optpoth.UseVisualStyleBackColor = False
        Me.optpoth.Visible = False
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
        Me.txtSeq.Location = New System.Drawing.Point(175, 274)
        Me.txtSeq.MaxLength = 50
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(203, 20)
        Me.txtSeq.TabIndex = 345468
        '
        'cmbcolms
        '
        Me.cmbcolms.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbcolms.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcolms.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbcolms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcolms.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcolms.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcolms.Location = New System.Drawing.Point(3, 273)
        Me.cmbcolms.Name = "cmbcolms"
        Me.cmbcolms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbcolms.Size = New System.Drawing.Size(166, 22)
        Me.cmbcolms.TabIndex = 345467
        Me.cmbcolms.TabStop = False
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(384, 277)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345469
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.BackColor = System.Drawing.Color.SteelBlue
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.ForeColor = System.Drawing.Color.White
        Me.btnLoad.Location = New System.Drawing.Point(560, 422)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(85, 35)
        Me.btnLoad.TabIndex = 345470
        Me.btnLoad.Text = "&Load"
        Me.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLoad.UseVisualStyleBackColor = False
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
        Me.btnExit.Location = New System.Drawing.Point(798, 425)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(85, 35)
        Me.btnExit.TabIndex = 345472
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.cldrEnddate)
        Me.GroupBox2.Controls.Add(Me.cldrStartDate)
        Me.GroupBox2.Location = New System.Drawing.Point(668, 368)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(215, 51)
        Me.GroupBox2.TabIndex = 345471
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
        Me.btnApply.Location = New System.Drawing.Point(711, 425)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(85, 35)
        Me.btnApply.TabIndex = 345473
        Me.btnApply.Text = "Preview"
        Me.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnApply.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.chkremoveadvance)
        Me.GroupBox3.Controls.Add(Me.rdovazhipadurate)
        Me.GroupBox3.Controls.Add(Me.rdopdclist)
        Me.GroupBox3.Controls.Add(Me.chkageing)
        Me.GroupBox3.Controls.Add(Me.rdoAddress)
        Me.GroupBox3.Controls.Add(Me.rdoAccountBalance)
        Me.GroupBox3.Controls.Add(Me.rdostatement)
        Me.GroupBox3.Controls.Add(Me.rdooutstanding)
        Me.GroupBox3.Location = New System.Drawing.Point(175, 298)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(268, 159)
        Me.GroupBox3.TabIndex = 345474
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Select Report"
        '
        'chkremoveadvance
        '
        Me.chkremoveadvance.AutoSize = True
        Me.chkremoveadvance.Checked = True
        Me.chkremoveadvance.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkremoveadvance.Location = New System.Drawing.Point(141, 62)
        Me.chkremoveadvance.Name = "chkremoveadvance"
        Me.chkremoveadvance.Size = New System.Drawing.Size(112, 17)
        Me.chkremoveadvance.TabIndex = 345493
        Me.chkremoveadvance.Text = "Remove Advance"
        Me.chkremoveadvance.UseVisualStyleBackColor = True
        Me.chkremoveadvance.Visible = False
        '
        'rdovazhipadurate
        '
        Me.rdovazhipadurate.AutoSize = True
        Me.rdovazhipadurate.BackColor = System.Drawing.Color.Transparent
        Me.rdovazhipadurate.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdovazhipadurate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdovazhipadurate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdovazhipadurate.Location = New System.Drawing.Point(6, 113)
        Me.rdovazhipadurate.Name = "rdovazhipadurate"
        Me.rdovazhipadurate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdovazhipadurate.Size = New System.Drawing.Size(125, 17)
        Me.rdovazhipadurate.TabIndex = 345492
        Me.rdovazhipadurate.Text = "Print Vazhipadu Rate"
        Me.rdovazhipadurate.UseVisualStyleBackColor = False
        Me.rdovazhipadurate.Visible = False
        '
        'rdopdclist
        '
        Me.rdopdclist.AutoSize = True
        Me.rdopdclist.BackColor = System.Drawing.Color.Transparent
        Me.rdopdclist.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdopdclist.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdopdclist.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdopdclist.Location = New System.Drawing.Point(6, 96)
        Me.rdopdclist.Name = "rdopdclist"
        Me.rdopdclist.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdopdclist.Size = New System.Drawing.Size(153, 17)
        Me.rdopdclist.TabIndex = 345477
        Me.rdopdclist.Text = "PDC List Cheque Datewise"
        Me.rdopdclist.UseVisualStyleBackColor = False
        Me.rdopdclist.Visible = False
        '
        'chkageing
        '
        Me.chkageing.AutoSize = True
        Me.chkageing.Location = New System.Drawing.Point(141, 39)
        Me.chkageing.Name = "chkageing"
        Me.chkageing.Size = New System.Drawing.Size(123, 17)
        Me.chkageing.TabIndex = 345476
        Me.chkageing.Text = "Aeging on Due Date"
        Me.chkageing.UseVisualStyleBackColor = True
        '
        'rdoAddress
        '
        Me.rdoAddress.AutoSize = True
        Me.rdoAddress.BackColor = System.Drawing.Color.Transparent
        Me.rdoAddress.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdoAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoAddress.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdoAddress.Location = New System.Drawing.Point(6, 77)
        Me.rdoAddress.Name = "rdoAddress"
        Me.rdoAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdoAddress.Size = New System.Drawing.Size(82, 17)
        Me.rdoAddress.TabIndex = 17
        Me.rdoAddress.Text = "Address List"
        Me.rdoAddress.UseVisualStyleBackColor = False
        '
        'rdoAccountBalance
        '
        Me.rdoAccountBalance.AutoSize = True
        Me.rdoAccountBalance.BackColor = System.Drawing.Color.Transparent
        Me.rdoAccountBalance.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdoAccountBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoAccountBalance.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdoAccountBalance.Location = New System.Drawing.Point(6, 58)
        Me.rdoAccountBalance.Name = "rdoAccountBalance"
        Me.rdoAccountBalance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdoAccountBalance.Size = New System.Drawing.Size(107, 17)
        Me.rdoAccountBalance.TabIndex = 16
        Me.rdoAccountBalance.Text = "Account Balance"
        Me.rdoAccountBalance.UseVisualStyleBackColor = False
        '
        'rdostatement
        '
        Me.rdostatement.AutoSize = True
        Me.rdostatement.BackColor = System.Drawing.Color.Transparent
        Me.rdostatement.Checked = True
        Me.rdostatement.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdostatement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdostatement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdostatement.Location = New System.Drawing.Point(6, 19)
        Me.rdostatement.Name = "rdostatement"
        Me.rdostatement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdostatement.Size = New System.Drawing.Size(130, 17)
        Me.rdostatement.TabIndex = 15
        Me.rdostatement.TabStop = True
        Me.rdostatement.Text = "Statement Of Account"
        Me.rdostatement.UseVisualStyleBackColor = False
        '
        'rdooutstanding
        '
        Me.rdooutstanding.AutoSize = True
        Me.rdooutstanding.BackColor = System.Drawing.Color.Transparent
        Me.rdooutstanding.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdooutstanding.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdooutstanding.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdooutstanding.Location = New System.Drawing.Point(6, 37)
        Me.rdooutstanding.Name = "rdooutstanding"
        Me.rdooutstanding.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdooutstanding.Size = New System.Drawing.Size(133, 17)
        Me.rdooutstanding.TabIndex = 14
        Me.rdooutstanding.Text = "Outstanding Statement"
        Me.rdooutstanding.UseVisualStyleBackColor = False
        '
        'chkdeliveredBy
        '
        Me.chkdeliveredBy.AutoSize = True
        Me.chkdeliveredBy.Location = New System.Drawing.Point(515, 319)
        Me.chkdeliveredBy.Name = "chkdeliveredBy"
        Me.chkdeliveredBy.Size = New System.Drawing.Size(86, 17)
        Me.chkdeliveredBy.TabIndex = 345491
        Me.chkdeliveredBy.Text = "Delivered By"
        Me.chkdeliveredBy.UseVisualStyleBackColor = True
        Me.chkdeliveredBy.Visible = False
        '
        'cmbdeliveredBy
        '
        Me.cmbdeliveredBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbdeliveredBy.FormattingEnabled = True
        Me.cmbdeliveredBy.Location = New System.Drawing.Point(604, 317)
        Me.cmbdeliveredBy.Name = "cmbdeliveredBy"
        Me.cmbdeliveredBy.Size = New System.Drawing.Size(109, 21)
        Me.cmbdeliveredBy.TabIndex = 345490
        Me.cmbdeliveredBy.Visible = False
        '
        'btnreconciliation
        '
        Me.btnreconciliation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnreconciliation.BackColor = System.Drawing.Color.SteelBlue
        Me.btnreconciliation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnreconciliation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnreconciliation.ForeColor = System.Drawing.Color.White
        Me.btnreconciliation.Location = New System.Drawing.Point(436, 422)
        Me.btnreconciliation.Name = "btnreconciliation"
        Me.btnreconciliation.Size = New System.Drawing.Size(121, 35)
        Me.btnreconciliation.TabIndex = 345475
        Me.btnreconciliation.Text = "Reconciliation"
        Me.btnreconciliation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnreconciliation.UseVisualStyleBackColor = False
        '
        'btnsms
        '
        Me.btnsms.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnsms.BackColor = System.Drawing.Color.SteelBlue
        Me.btnsms.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsms.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsms.ForeColor = System.Drawing.Color.White
        Me.btnsms.Location = New System.Drawing.Point(798, 327)
        Me.btnsms.Name = "btnsms"
        Me.btnsms.Size = New System.Drawing.Size(85, 35)
        Me.btnsms.TabIndex = 345476
        Me.btnsms.Text = "Send SMS"
        Me.btnsms.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsms.UseVisualStyleBackColor = False
        Me.btnsms.Visible = False
        '
        'chkFormat
        '
        Me.chkFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(648, 432)
        Me.chkFormat.Name = "chkFormat"
        Me.chkFormat.Size = New System.Drawing.Size(65, 19)
        Me.chkFormat.TabIndex = 345477
        Me.chkFormat.Text = "&Format"
        Me.chkFormat.UseVisualStyleBackColor = False
        '
        'btnrv
        '
        Me.btnrv.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnrv.AutoEllipsis = True
        Me.btnrv.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrv.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnrv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrv.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrv.ForeColor = System.Drawing.Color.White
        Me.btnrv.Location = New System.Drawing.Point(798, 274)
        Me.btnrv.Name = "btnrv"
        Me.btnrv.Size = New System.Drawing.Size(85, 35)
        Me.btnrv.TabIndex = 345492
        Me.btnrv.Text = "Create RV"
        Me.btnrv.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrv.UseVisualStyleBackColor = False
        '
        'FinancialStatements
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(895, 466)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnrv)
        Me.Controls.Add(Me.btnsms)
        Me.Controls.Add(Me.chkdeliveredBy)
        Me.Controls.Add(Me.btnreconciliation)
        Me.Controls.Add(Me.cmbdeliveredBy)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.txtSeq)
        Me.Controls.Add(Me.cmbcolms)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grdvoucher)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.chkFormat)
        Me.Name = "FinancialStatements"
        Me.Text = "AccountEnquiry"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdvoucher As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbcolms As System.Windows.Forms.ComboBox
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cldrEnddate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Public WithEvents rdostatement As System.Windows.Forms.RadioButton
    Public WithEvents rdooutstanding As System.Windows.Forms.RadioButton
    Public WithEvents rdoAccountBalance As System.Windows.Forms.RadioButton
    Public WithEvents rdoAddress As System.Windows.Forms.RadioButton
    Friend WithEvents btnreconciliation As System.Windows.Forms.Button
    Friend WithEvents chkageing As System.Windows.Forms.CheckBox
    Public WithEvents rdopdclist As System.Windows.Forms.RadioButton
    Public WithEvents optroth As System.Windows.Forms.RadioButton
    Public WithEvents optpoth As System.Windows.Forms.RadioButton
    Friend WithEvents chkdeliveredBy As System.Windows.Forms.CheckBox
    Friend WithEvents cmbdeliveredBy As System.Windows.Forms.ComboBox
    Friend WithEvents cmbcategory As System.Windows.Forms.ComboBox
    Public WithEvents rdovazhipadurate As System.Windows.Forms.RadioButton
    Friend WithEvents btnsms As System.Windows.Forms.Button
    Friend WithEvents chkFormat As System.Windows.Forms.CheckBox
    Friend WithEvents chkremoveadvance As System.Windows.Forms.CheckBox
    Public WithEvents lblarea As System.Windows.Forms.Label
    Friend WithEvents cmbarea As System.Windows.Forms.ComboBox
    Friend WithEvents btnrv As System.Windows.Forms.Button
End Class
