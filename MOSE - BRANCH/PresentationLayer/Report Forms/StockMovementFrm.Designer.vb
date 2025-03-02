<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockmovementFrm
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
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.grdItem = New System.Windows.Forms.DataGridView
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label26 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.grdSup = New System.Windows.Forms.DataGridView
        Me.grdTransactions = New System.Windows.Forms.DataGridView
        Me.btnclose = New System.Windows.Forms.Button
        Me.txtsupsearch = New System.Windows.Forms.TextBox
        Me.cmbsup = New System.Windows.Forms.ComboBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.dtpto = New System.Windows.Forms.DateTimePicker
        Me.rdofastmove = New System.Windows.Forms.RadioButton
        Me.rdoslow = New System.Windows.Forms.RadioButton
        Me.btnload = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.rdoprofit = New System.Windows.Forms.RadioButton
        Me.rdovalue = New System.Windows.Forms.RadioButton
        Me.rdoqty = New System.Windows.Forms.RadioButton
        Me.chkdate = New System.Windows.Forms.CheckBox
        Me.pldate = New System.Windows.Forms.Panel
        Me.chksuppllierwise = New System.Windows.Forms.CheckBox
        Me.btnApply = New System.Windows.Forms.Button
        Me.chkallsupp = New System.Windows.Forms.CheckBox
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTransactions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.pldate.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.Checked = True
        Me.chkSearch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(609, 308)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345416
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
        '
        'grdItem
        '
        Me.grdItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItem.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItem.Location = New System.Drawing.Point(157, 33)
        Me.grdItem.Name = "grdItem"
        Me.grdItem.Size = New System.Drawing.Size(851, 266)
        Me.grdItem.TabIndex = 345413
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
        Me.txtSeq.Location = New System.Drawing.Point(329, 307)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(274, 20)
        Me.txtSeq.TabIndex = 345415
        '
        'cmbOrder
        '
        Me.cmbOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOrder.Location = New System.Drawing.Point(157, 305)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOrder.Size = New System.Drawing.Size(166, 22)
        Me.cmbOrder.TabIndex = 345414
        Me.cmbOrder.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1011, 32)
        Me.Panel1.TabIndex = 345464
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(37, 6)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(154, 18)
        Me.Label26.TabIndex = 345458
        Me.Label26.Text = "STOCK MOVEMENT"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.application_icon
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(3, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(30, 27)
        Me.PictureBox2.TabIndex = 345457
        Me.PictureBox2.TabStop = False
        '
        'grdSup
        '
        Me.grdSup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdSup.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdSup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdSup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSup.Location = New System.Drawing.Point(4, 333)
        Me.grdSup.Name = "grdSup"
        Me.grdSup.Size = New System.Drawing.Size(592, 198)
        Me.grdSup.TabIndex = 345465
        '
        'grdTransactions
        '
        Me.grdTransactions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdTransactions.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdTransactions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTransactions.Location = New System.Drawing.Point(605, 333)
        Me.grdTransactions.Name = "grdTransactions"
        Me.grdTransactions.Size = New System.Drawing.Size(403, 198)
        Me.grdTransactions.TabIndex = 345466
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ForeColor = System.Drawing.Color.White
        Me.btnclose.Location = New System.Drawing.Point(926, 535)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(82, 35)
        Me.btnclose.TabIndex = 345467
        Me.btnclose.Text = "E&xit"
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'txtsupsearch
        '
        Me.txtsupsearch.AcceptsReturn = True
        Me.txtsupsearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtsupsearch.BackColor = System.Drawing.SystemColors.Window
        Me.txtsupsearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtsupsearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtsupsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsupsearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtsupsearch.Location = New System.Drawing.Point(176, 535)
        Me.txtsupsearch.MaxLength = 500
        Me.txtsupsearch.Name = "txtsupsearch"
        Me.txtsupsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtsupsearch.Size = New System.Drawing.Size(274, 20)
        Me.txtsupsearch.TabIndex = 345469
        Me.txtsupsearch.Visible = False
        '
        'cmbsup
        '
        Me.cmbsup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbsup.BackColor = System.Drawing.SystemColors.Window
        Me.cmbsup.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbsup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsup.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsup.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbsup.Location = New System.Drawing.Point(4, 533)
        Me.cmbsup.Name = "cmbsup"
        Me.cmbsup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbsup.Size = New System.Drawing.Size(166, 22)
        Me.cmbsup.TabIndex = 345468
        Me.cmbsup.TabStop = False
        Me.cmbsup.Visible = False
        '
        'Timer1
        '
        '
        'cldrStartDate
        '
        Me.cldrStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrStartDate.Location = New System.Drawing.Point(3, 3)
        Me.cldrStartDate.Name = "cldrStartDate"
        Me.cldrStartDate.Size = New System.Drawing.Size(95, 20)
        Me.cldrStartDate.TabIndex = 345470
        Me.cldrStartDate.TabStop = False
        '
        'dtpto
        '
        Me.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpto.Location = New System.Drawing.Point(3, 29)
        Me.dtpto.Name = "dtpto"
        Me.dtpto.Size = New System.Drawing.Size(95, 20)
        Me.dtpto.TabIndex = 345471
        Me.dtpto.TabStop = False
        '
        'rdofastmove
        '
        Me.rdofastmove.AutoSize = True
        Me.rdofastmove.BackColor = System.Drawing.Color.Transparent
        Me.rdofastmove.Checked = True
        Me.rdofastmove.Location = New System.Drawing.Point(15, 104)
        Me.rdofastmove.Name = "rdofastmove"
        Me.rdofastmove.Size = New System.Drawing.Size(98, 17)
        Me.rdofastmove.TabIndex = 345473
        Me.rdofastmove.TabStop = True
        Me.rdofastmove.Text = "Fast Movement"
        Me.rdofastmove.UseVisualStyleBackColor = False
        '
        'rdoslow
        '
        Me.rdoslow.AutoSize = True
        Me.rdoslow.BackColor = System.Drawing.Color.Transparent
        Me.rdoslow.Location = New System.Drawing.Point(15, 122)
        Me.rdoslow.Name = "rdoslow"
        Me.rdoslow.Size = New System.Drawing.Size(101, 17)
        Me.rdoslow.TabIndex = 345474
        Me.rdoslow.Text = "Slow Movement"
        Me.rdoslow.UseVisualStyleBackColor = False
        '
        'btnload
        '
        Me.btnload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.Location = New System.Drawing.Point(4, 293)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(138, 35)
        Me.btnload.TabIndex = 345475
        Me.btnload.Text = "Load"
        Me.btnload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnload.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.rdoprofit)
        Me.Panel2.Controls.Add(Me.rdovalue)
        Me.Panel2.Controls.Add(Me.rdoqty)
        Me.Panel2.Location = New System.Drawing.Point(41, 138)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(83, 58)
        Me.Panel2.TabIndex = 345476
        '
        'rdoprofit
        '
        Me.rdoprofit.AutoSize = True
        Me.rdoprofit.BackColor = System.Drawing.Color.Transparent
        Me.rdoprofit.Location = New System.Drawing.Point(8, 38)
        Me.rdoprofit.Name = "rdoprofit"
        Me.rdoprofit.Size = New System.Drawing.Size(64, 17)
        Me.rdoprofit.TabIndex = 345477
        Me.rdoprofit.Text = "By Profit"
        Me.rdoprofit.UseVisualStyleBackColor = False
        '
        'rdovalue
        '
        Me.rdovalue.AutoSize = True
        Me.rdovalue.BackColor = System.Drawing.Color.Transparent
        Me.rdovalue.Location = New System.Drawing.Point(7, 20)
        Me.rdovalue.Name = "rdovalue"
        Me.rdovalue.Size = New System.Drawing.Size(67, 17)
        Me.rdovalue.TabIndex = 345476
        Me.rdovalue.Text = "By Value"
        Me.rdovalue.UseVisualStyleBackColor = False
        '
        'rdoqty
        '
        Me.rdoqty.AutoSize = True
        Me.rdoqty.BackColor = System.Drawing.Color.Transparent
        Me.rdoqty.Checked = True
        Me.rdoqty.Location = New System.Drawing.Point(7, 3)
        Me.rdoqty.Name = "rdoqty"
        Me.rdoqty.Size = New System.Drawing.Size(56, 17)
        Me.rdoqty.TabIndex = 345475
        Me.rdoqty.TabStop = True
        Me.rdoqty.Text = "By Qty"
        Me.rdoqty.UseVisualStyleBackColor = False
        '
        'chkdate
        '
        Me.chkdate.AutoSize = True
        Me.chkdate.BackColor = System.Drawing.Color.Transparent
        Me.chkdate.Location = New System.Drawing.Point(17, 35)
        Me.chkdate.Name = "chkdate"
        Me.chkdate.Size = New System.Drawing.Size(73, 17)
        Me.chkdate.TabIndex = 345477
        Me.chkdate.Text = "Date wise"
        Me.chkdate.UseVisualStyleBackColor = False
        '
        'pldate
        '
        Me.pldate.BackColor = System.Drawing.Color.Transparent
        Me.pldate.Controls.Add(Me.cldrStartDate)
        Me.pldate.Controls.Add(Me.dtpto)
        Me.pldate.Enabled = False
        Me.pldate.Location = New System.Drawing.Point(14, 52)
        Me.pldate.Name = "pldate"
        Me.pldate.Size = New System.Drawing.Size(138, 53)
        Me.pldate.TabIndex = 345478
        '
        'chksuppllierwise
        '
        Me.chksuppllierwise.AutoSize = True
        Me.chksuppllierwise.BackColor = System.Drawing.Color.Transparent
        Me.chksuppllierwise.Location = New System.Drawing.Point(14, 212)
        Me.chksuppllierwise.Name = "chksuppllierwise"
        Me.chksuppllierwise.Size = New System.Drawing.Size(85, 17)
        Me.chksuppllierwise.TabIndex = 345479
        Me.chksuppllierwise.Text = "Supplierwise"
        Me.chksuppllierwise.UseVisualStyleBackColor = False
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
        Me.btnApply.Location = New System.Drawing.Point(838, 535)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(85, 35)
        Me.btnApply.TabIndex = 345496
        Me.btnApply.Text = "Preview"
        Me.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnApply.UseVisualStyleBackColor = False
        '
        'chkallsupp
        '
        Me.chkallsupp.AutoSize = True
        Me.chkallsupp.BackColor = System.Drawing.Color.Transparent
        Me.chkallsupp.Location = New System.Drawing.Point(14, 231)
        Me.chkallsupp.Name = "chkallsupp"
        Me.chkallsupp.Size = New System.Drawing.Size(96, 17)
        Me.chkallsupp.TabIndex = 345497
        Me.chkallsupp.Text = "For All Supplier"
        Me.chkallsupp.UseVisualStyleBackColor = False
        '
        'StockmovementFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1011, 572)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkallsupp)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.chksuppllierwise)
        Me.Controls.Add(Me.pldate)
        Me.Controls.Add(Me.chkdate)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnload)
        Me.Controls.Add(Me.rdoslow)
        Me.Controls.Add(Me.rdofastmove)
        Me.Controls.Add(Me.txtsupsearch)
        Me.Controls.Add(Me.cmbsup)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.grdTransactions)
        Me.Controls.Add(Me.grdSup)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.grdItem)
        Me.Controls.Add(Me.txtSeq)
        Me.Controls.Add(Me.cmbOrder)
        Me.Name = "StockmovementFrm"
        Me.Text = "Stock Movement"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTransactions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pldate.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents grdItem As System.Windows.Forms.DataGridView
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents grdSup As System.Windows.Forms.DataGridView
    Friend WithEvents grdTransactions As System.Windows.Forms.DataGridView
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Public WithEvents txtsupsearch As System.Windows.Forms.TextBox
    Public WithEvents cmbsup As System.Windows.Forms.ComboBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpto As System.Windows.Forms.DateTimePicker
    Friend WithEvents rdofastmove As System.Windows.Forms.RadioButton
    Friend WithEvents rdoslow As System.Windows.Forms.RadioButton
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rdoprofit As System.Windows.Forms.RadioButton
    Friend WithEvents rdovalue As System.Windows.Forms.RadioButton
    Friend WithEvents rdoqty As System.Windows.Forms.RadioButton
    Friend WithEvents chkdate As System.Windows.Forms.CheckBox
    Friend WithEvents pldate As System.Windows.Forms.Panel
    Friend WithEvents chksuppllierwise As System.Windows.Forms.CheckBox
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents chkallsupp As System.Windows.Forms.CheckBox
End Class
