<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JobList
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
        Me.cldrEnddate = New System.Windows.Forms.DateTimePicker
        Me.btnclose = New System.Windows.Forms.Button
        Me.btnload = New System.Windows.Forms.Button
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.grdItem = New System.Windows.Forms.DataGridView
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.btnEdit = New System.Windows.Forms.Button
        Me.btnDelivery = New System.Windows.Forms.Button
        Me.btncloseJob = New System.Windows.Forms.Button
        Me.btnPreview = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblcap = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.cmbtech = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtsearch = New System.Windows.Forms.TextBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.rdoall = New System.Windows.Forms.RadioButton
        Me.rdoactive = New System.Windows.Forms.RadioButton
        Me.rdoclosed = New System.Windows.Forms.RadioButton
        Me.plclose = New System.Windows.Forms.Panel
        Me.rdocloseddate = New System.Windows.Forms.RadioButton
        Me.rdojobdate = New System.Windows.Forms.RadioButton
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.plDelivery = New System.Windows.Forms.Panel
        Me.rdonotdelivered = New System.Windows.Forms.RadioButton
        Me.rdodelivereditems = New System.Windows.Forms.RadioButton
        Me.rdoReceivedItems = New System.Windows.Forms.RadioButton
        Me.chkdate = New System.Windows.Forms.CheckBox
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.plclose.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.plDelivery.SuspendLayout()
        Me.SuspendLayout()
        '
        'cldrEnddate
        '
        Me.cldrEnddate.CustomFormat = "dd/MMM/yyyy"
        Me.cldrEnddate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.cldrEnddate.Location = New System.Drawing.Point(8, 89)
        Me.cldrEnddate.Name = "cldrEnddate"
        Me.cldrEnddate.Size = New System.Drawing.Size(158, 20)
        Me.cldrEnddate.TabIndex = 345453
        Me.cldrEnddate.TabStop = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ForeColor = System.Drawing.Color.White
        Me.btnclose.Location = New System.Drawing.Point(1067, 474)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(82, 35)
        Me.btnclose.TabIndex = 345451
        Me.btnclose.Text = "E&xit"
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'btnload
        '
        Me.btnload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.Location = New System.Drawing.Point(4, 402)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(159, 35)
        Me.btnload.TabIndex = 345455
        Me.btnload.Text = "Load"
        Me.btnload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnload.UseVisualStyleBackColor = False
        '
        'cldrStartDate
        '
        Me.cldrStartDate.CustomFormat = "dd/MMM/yyyy"
        Me.cldrStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.cldrStartDate.Location = New System.Drawing.Point(8, 63)
        Me.cldrStartDate.Name = "cldrStartDate"
        Me.cldrStartDate.Size = New System.Drawing.Size(158, 20)
        Me.cldrStartDate.TabIndex = 345452
        Me.cldrStartDate.TabStop = False
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.Checked = True
        Me.chkSearch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(172, 478)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345449
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
        '
        'grdItem
        '
        Me.grdItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItem.BackgroundColor = System.Drawing.Color.White
        Me.grdItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItem.Location = New System.Drawing.Point(172, 34)
        Me.grdItem.Name = "grdItem"
        Me.grdItem.Size = New System.Drawing.Size(975, 411)
        Me.grdItem.TabIndex = 345446
        '
        'txtSeq
        '
        Me.txtSeq.AcceptsReturn = True
        Me.txtSeq.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSeq.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeq.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSeq.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeq.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSeq.Location = New System.Drawing.Point(344, 450)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(805, 20)
        Me.txtSeq.TabIndex = 345448
        '
        'cmbOrder
        '
        Me.cmbOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOrder.Location = New System.Drawing.Point(172, 450)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOrder.Size = New System.Drawing.Size(166, 22)
        Me.cmbOrder.TabIndex = 345447
        Me.cmbOrder.TabStop = False
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEdit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEdit.ForeColor = System.Drawing.Color.White
        Me.btnEdit.Location = New System.Drawing.Point(88, 438)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 35)
        Me.btnEdit.TabIndex = 345458
        Me.btnEdit.TabStop = False
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'btnDelivery
        '
        Me.btnDelivery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelivery.BackColor = System.Drawing.Color.SteelBlue
        Me.btnDelivery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelivery.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelivery.ForeColor = System.Drawing.Color.White
        Me.btnDelivery.Location = New System.Drawing.Point(88, 474)
        Me.btnDelivery.Name = "btnDelivery"
        Me.btnDelivery.Size = New System.Drawing.Size(75, 35)
        Me.btnDelivery.TabIndex = 345459
        Me.btnDelivery.TabStop = False
        Me.btnDelivery.Text = "Delivery"
        Me.btnDelivery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnDelivery.UseVisualStyleBackColor = False
        '
        'btncloseJob
        '
        Me.btncloseJob.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btncloseJob.BackColor = System.Drawing.Color.SteelBlue
        Me.btncloseJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncloseJob.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncloseJob.ForeColor = System.Drawing.Color.White
        Me.btncloseJob.Location = New System.Drawing.Point(4, 474)
        Me.btncloseJob.Name = "btncloseJob"
        Me.btncloseJob.Size = New System.Drawing.Size(82, 35)
        Me.btncloseJob.TabIndex = 345460
        Me.btncloseJob.TabStop = False
        Me.btncloseJob.Text = "Job Closing"
        Me.btncloseJob.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncloseJob.UseVisualStyleBackColor = False
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.BackColor = System.Drawing.Color.SteelBlue
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.White
        Me.btnPreview.Location = New System.Drawing.Point(4, 438)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(82, 35)
        Me.btnPreview.TabIndex = 345461
        Me.btnPreview.TabStop = False
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lblcap)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1151, 32)
        Me.Panel1.TabIndex = 345464
        '
        'lblcap
        '
        Me.lblcap.AutoSize = True
        Me.lblcap.BackColor = System.Drawing.Color.White
        Me.lblcap.Font = New System.Drawing.Font("Goudy Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblcap.Location = New System.Drawing.Point(35, 5)
        Me.lblcap.Name = "lblcap"
        Me.lblcap.Size = New System.Drawing.Size(112, 21)
        Me.lblcap.TabIndex = 345458
        Me.lblcap.Text = "JOB HISTORY"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.OMR
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(3, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(29, 26)
        Me.PictureBox1.TabIndex = 44
        Me.PictureBox1.TabStop = False
        '
        'cmbtech
        '
        Me.cmbtech.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtech.FormattingEnabled = True
        Me.cmbtech.Location = New System.Drawing.Point(3, 35)
        Me.cmbtech.Name = "cmbtech"
        Me.cmbtech.Size = New System.Drawing.Size(155, 21)
        Me.cmbtech.TabIndex = 345470
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 14)
        Me.Label2.TabIndex = 345469
        Me.Label2.Text = "Search Text"
        '
        'txtsearch
        '
        Me.txtsearch.Location = New System.Drawing.Point(3, 36)
        Me.txtsearch.MaxLength = 30
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.ReadOnly = True
        Me.txtsearch.Size = New System.Drawing.Size(155, 20)
        Me.txtsearch.TabIndex = 345468
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.cmbtech)
        Me.Panel2.Controls.Add(Me.txtsearch)
        Me.Panel2.Location = New System.Drawing.Point(4, 115)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(164, 75)
        Me.Panel2.TabIndex = 345471
        '
        'rdoall
        '
        Me.rdoall.AutoSize = True
        Me.rdoall.Location = New System.Drawing.Point(3, 3)
        Me.rdoall.Name = "rdoall"
        Me.rdoall.Size = New System.Drawing.Size(36, 17)
        Me.rdoall.TabIndex = 345472
        Me.rdoall.Text = "All"
        Me.rdoall.UseVisualStyleBackColor = True
        '
        'rdoactive
        '
        Me.rdoactive.AutoSize = True
        Me.rdoactive.Checked = True
        Me.rdoactive.Location = New System.Drawing.Point(3, 26)
        Me.rdoactive.Name = "rdoactive"
        Me.rdoactive.Size = New System.Drawing.Size(80, 17)
        Me.rdoactive.TabIndex = 345473
        Me.rdoactive.TabStop = True
        Me.rdoactive.Text = "Active Jobs"
        Me.rdoactive.UseVisualStyleBackColor = True
        '
        'rdoclosed
        '
        Me.rdoclosed.AutoSize = True
        Me.rdoclosed.Location = New System.Drawing.Point(3, 49)
        Me.rdoclosed.Name = "rdoclosed"
        Me.rdoclosed.Size = New System.Drawing.Size(82, 17)
        Me.rdoclosed.TabIndex = 345474
        Me.rdoclosed.Text = "Closed Jobs"
        Me.rdoclosed.UseVisualStyleBackColor = True
        '
        'plclose
        '
        Me.plclose.Controls.Add(Me.rdocloseddate)
        Me.plclose.Controls.Add(Me.rdojobdate)
        Me.plclose.Location = New System.Drawing.Point(3, 72)
        Me.plclose.Name = "plclose"
        Me.plclose.Size = New System.Drawing.Size(163, 44)
        Me.plclose.TabIndex = 345475
        Me.plclose.Visible = False
        '
        'rdocloseddate
        '
        Me.rdocloseddate.AutoSize = True
        Me.rdocloseddate.Checked = True
        Me.rdocloseddate.Location = New System.Drawing.Point(17, 24)
        Me.rdocloseddate.Name = "rdocloseddate"
        Me.rdocloseddate.Size = New System.Drawing.Size(104, 17)
        Me.rdocloseddate.TabIndex = 345476
        Me.rdocloseddate.TabStop = True
        Me.rdocloseddate.Text = "Closed Datewise"
        Me.rdocloseddate.UseVisualStyleBackColor = True
        '
        'rdojobdate
        '
        Me.rdojobdate.AutoSize = True
        Me.rdojobdate.Location = New System.Drawing.Point(17, 3)
        Me.rdojobdate.Name = "rdojobdate"
        Me.rdojobdate.Size = New System.Drawing.Size(89, 17)
        Me.rdojobdate.TabIndex = 345475
        Me.rdojobdate.Text = "Job Datewise"
        Me.rdojobdate.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.Controls.Add(Me.rdoall)
        Me.Panel3.Controls.Add(Me.plclose)
        Me.Panel3.Controls.Add(Me.rdoactive)
        Me.Panel3.Controls.Add(Me.rdoclosed)
        Me.Panel3.Location = New System.Drawing.Point(2, 193)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(167, 120)
        Me.Panel3.TabIndex = 345476
        '
        'plDelivery
        '
        Me.plDelivery.BackColor = System.Drawing.Color.Transparent
        Me.plDelivery.Controls.Add(Me.rdonotdelivered)
        Me.plDelivery.Controls.Add(Me.rdodelivereditems)
        Me.plDelivery.Controls.Add(Me.rdoReceivedItems)
        Me.plDelivery.Location = New System.Drawing.Point(5, 115)
        Me.plDelivery.Name = "plDelivery"
        Me.plDelivery.Size = New System.Drawing.Size(162, 74)
        Me.plDelivery.TabIndex = 345477
        Me.plDelivery.Visible = False
        '
        'rdonotdelivered
        '
        Me.rdonotdelivered.AutoSize = True
        Me.rdonotdelivered.Checked = True
        Me.rdonotdelivered.Location = New System.Drawing.Point(3, 3)
        Me.rdonotdelivered.Name = "rdonotdelivered"
        Me.rdonotdelivered.Size = New System.Drawing.Size(90, 17)
        Me.rdonotdelivered.TabIndex = 345475
        Me.rdonotdelivered.TabStop = True
        Me.rdonotdelivered.Text = "Not Delivered"
        Me.rdonotdelivered.UseVisualStyleBackColor = True
        '
        'rdodelivereditems
        '
        Me.rdodelivereditems.AutoSize = True
        Me.rdodelivereditems.Location = New System.Drawing.Point(3, 26)
        Me.rdodelivereditems.Name = "rdodelivereditems"
        Me.rdodelivereditems.Size = New System.Drawing.Size(154, 17)
        Me.rdodelivereditems.TabIndex = 345476
        Me.rdodelivereditems.Text = "Delivered Items [Date wise]"
        Me.rdodelivereditems.UseVisualStyleBackColor = True
        '
        'rdoReceivedItems
        '
        Me.rdoReceivedItems.AutoSize = True
        Me.rdoReceivedItems.Location = New System.Drawing.Point(3, 49)
        Me.rdoReceivedItems.Name = "rdoReceivedItems"
        Me.rdoReceivedItems.Size = New System.Drawing.Size(155, 17)
        Me.rdoReceivedItems.TabIndex = 345477
        Me.rdoReceivedItems.Text = "Received Items [Date wise]"
        Me.rdoReceivedItems.UseVisualStyleBackColor = True
        '
        'chkdate
        '
        Me.chkdate.AutoSize = True
        Me.chkdate.Location = New System.Drawing.Point(8, 43)
        Me.chkdate.Name = "chkdate"
        Me.chkdate.Size = New System.Drawing.Size(85, 17)
        Me.chkdate.TabIndex = 345478
        Me.chkdate.Text = "Enable Date"
        Me.chkdate.UseVisualStyleBackColor = True
        '
        'JobList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1151, 513)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkdate)
        Me.Controls.Add(Me.plDelivery)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btncloseJob)
        Me.Controls.Add(Me.btnDelivery)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.cldrEnddate)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.btnload)
        Me.Controls.Add(Me.cldrStartDate)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.grdItem)
        Me.Controls.Add(Me.txtSeq)
        Me.Controls.Add(Me.cmbOrder)
        Me.Name = "JobList"
        Me.Text = "Job List"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.plclose.ResumeLayout(False)
        Me.plclose.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.plDelivery.ResumeLayout(False)
        Me.plDelivery.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cldrEnddate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents grdItem As System.Windows.Forms.DataGridView
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnDelivery As System.Windows.Forms.Button
    Friend WithEvents btncloseJob As System.Windows.Forms.Button
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblcap As System.Windows.Forms.Label
    Friend WithEvents cmbtech As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtsearch As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rdoall As System.Windows.Forms.RadioButton
    Friend WithEvents rdoactive As System.Windows.Forms.RadioButton
    Friend WithEvents rdoclosed As System.Windows.Forms.RadioButton
    Friend WithEvents plclose As System.Windows.Forms.Panel
    Friend WithEvents rdocloseddate As System.Windows.Forms.RadioButton
    Friend WithEvents rdojobdate As System.Windows.Forms.RadioButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents plDelivery As System.Windows.Forms.Panel
    Friend WithEvents rdonotdelivered As System.Windows.Forms.RadioButton
    Friend WithEvents rdodelivereditems As System.Windows.Forms.RadioButton
    Friend WithEvents rdoReceivedItems As System.Windows.Forms.RadioButton
    Friend WithEvents chkdate As System.Windows.Forms.CheckBox
End Class
