<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JobPendingAssign
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
        Me.Label1 = New System.Windows.Forms.Label
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
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.grdAssigned = New System.Windows.Forms.DataGridView
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.btnremoveAttend = New System.Windows.Forms.Button
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.btnundoClosing = New System.Windows.Forms.Button
        Me.grdcompleted = New System.Windows.Forms.DataGridView
        Me.txtSearchAssign = New System.Windows.Forms.TextBox
        Me.cmbSearchAssign = New System.Windows.Forms.ComboBox
        Me.txtsearchCompleted = New System.Windows.Forms.TextBox
        Me.cmbSearchCompleted = New System.Windows.Forms.ComboBox
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.plclose.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.plDelivery.SuspendLayout()
        CType(Me.grdAssigned, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdcompleted, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(20, -4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 345454
        Me.Label1.Text = "Date Range"
        '
        'cldrEnddate
        '
        Me.cldrEnddate.CustomFormat = "dd/MMM/yyyy"
        Me.cldrEnddate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.cldrEnddate.Location = New System.Drawing.Point(20, 38)
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
        Me.btnload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.Location = New System.Drawing.Point(983, 474)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(82, 35)
        Me.btnload.TabIndex = 345455
        Me.btnload.Text = "Load"
        Me.btnload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnload.UseVisualStyleBackColor = False
        '
        'cldrStartDate
        '
        Me.cldrStartDate.CustomFormat = "dd/MMM/yyyy"
        Me.cldrStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.cldrStartDate.Location = New System.Drawing.Point(20, 12)
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
        Me.chkSearch.Location = New System.Drawing.Point(455, 249)
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
        Me.grdItem.Location = New System.Drawing.Point(12, 63)
        Me.grdItem.Name = "grdItem"
        Me.grdItem.Size = New System.Drawing.Size(1130, 180)
        Me.grdItem.TabIndex = 345446
        '
        'txtSeq
        '
        Me.txtSeq.AcceptsReturn = True
        Me.txtSeq.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSeq.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeq.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSeq.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeq.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSeq.Location = New System.Drawing.Point(181, 249)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(268, 20)
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
        Me.cmbOrder.Location = New System.Drawing.Point(9, 249)
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
        Me.btnEdit.Location = New System.Drawing.Point(1067, 386)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 35)
        Me.btnEdit.TabIndex = 345458
        Me.btnEdit.TabStop = False
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEdit.UseVisualStyleBackColor = False
        Me.btnEdit.Visible = False
        '
        'btnDelivery
        '
        Me.btnDelivery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelivery.BackColor = System.Drawing.Color.SteelBlue
        Me.btnDelivery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelivery.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelivery.ForeColor = System.Drawing.Color.White
        Me.btnDelivery.Location = New System.Drawing.Point(1067, 422)
        Me.btnDelivery.Name = "btnDelivery"
        Me.btnDelivery.Size = New System.Drawing.Size(75, 35)
        Me.btnDelivery.TabIndex = 345459
        Me.btnDelivery.TabStop = False
        Me.btnDelivery.Text = "Delivery"
        Me.btnDelivery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnDelivery.UseVisualStyleBackColor = False
        Me.btnDelivery.Visible = False
        '
        'btncloseJob
        '
        Me.btncloseJob.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btncloseJob.BackColor = System.Drawing.Color.SteelBlue
        Me.btncloseJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncloseJob.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncloseJob.ForeColor = System.Drawing.Color.White
        Me.btncloseJob.Location = New System.Drawing.Point(872, 168)
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
        Me.btnPreview.Location = New System.Drawing.Point(615, 386)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(82, 35)
        Me.btnPreview.TabIndex = 345461
        Me.btnPreview.TabStop = False
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPreview.UseVisualStyleBackColor = False
        Me.btnPreview.Visible = False
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
        Me.Panel2.Location = New System.Drawing.Point(615, 63)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(164, 75)
        Me.Panel2.TabIndex = 345471
        Me.Panel2.Visible = False
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
        Me.rdocloseddate.Location = New System.Drawing.Point(17, 24)
        Me.rdocloseddate.Name = "rdocloseddate"
        Me.rdocloseddate.Size = New System.Drawing.Size(87, 17)
        Me.rdocloseddate.TabIndex = 345476
        Me.rdocloseddate.Text = "Job datewise"
        Me.rdocloseddate.UseVisualStyleBackColor = True
        '
        'rdojobdate
        '
        Me.rdojobdate.AutoSize = True
        Me.rdojobdate.Checked = True
        Me.rdojobdate.Location = New System.Drawing.Point(17, 3)
        Me.rdojobdate.Name = "rdojobdate"
        Me.rdojobdate.Size = New System.Drawing.Size(107, 17)
        Me.rdojobdate.TabIndex = 345475
        Me.rdojobdate.TabStop = True
        Me.rdojobdate.Text = "Closed Date wise"
        Me.rdojobdate.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.Controls.Add(Me.rdoall)
        Me.Panel3.Controls.Add(Me.plclose)
        Me.Panel3.Controls.Add(Me.rdoactive)
        Me.Panel3.Controls.Add(Me.rdoclosed)
        Me.Panel3.Controls.Add(Me.cldrStartDate)
        Me.Panel3.Controls.Add(Me.cldrEnddate)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Location = New System.Drawing.Point(613, 141)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(167, 120)
        Me.Panel3.TabIndex = 345476
        Me.Panel3.Visible = False
        '
        'plDelivery
        '
        Me.plDelivery.BackColor = System.Drawing.Color.Transparent
        Me.plDelivery.Controls.Add(Me.rdonotdelivered)
        Me.plDelivery.Controls.Add(Me.rdodelivereditems)
        Me.plDelivery.Controls.Add(Me.rdoReceivedItems)
        Me.plDelivery.Location = New System.Drawing.Point(616, 63)
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
        'CheckBox1
        '
        Me.CheckBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.ForeColor = System.Drawing.Color.Black
        Me.CheckBox1.Location = New System.Drawing.Point(458, 477)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(143, 17)
        Me.CheckBox1.TabIndex = 345481
        Me.CheckBox1.Text = "Search 'Starts With' Only"
        Me.CheckBox1.UseVisualStyleBackColor = False
        Me.CheckBox1.Visible = False
        '
        'grdAssigned
        '
        Me.grdAssigned.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAssigned.BackgroundColor = System.Drawing.Color.White
        Me.grdAssigned.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdAssigned.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdAssigned.Location = New System.Drawing.Point(6, 6)
        Me.grdAssigned.Name = "grdAssigned"
        Me.grdAssigned.Size = New System.Drawing.Size(861, 170)
        Me.grdAssigned.TabIndex = 345478
        '
        'TextBox1
        '
        Me.TextBox1.AcceptsReturn = True
        Me.TextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBox1.Location = New System.Drawing.Point(181, 474)
        Me.TextBox1.MaxLength = 500
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextBox1.Size = New System.Drawing.Size(271, 20)
        Me.TextBox1.TabIndex = 345480
        Me.TextBox1.Visible = False
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.BackColor = System.Drawing.SystemColors.Window
        Me.ComboBox1.Cursor = System.Windows.Forms.Cursors.Default
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ComboBox1.Location = New System.Drawing.Point(9, 474)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ComboBox1.Size = New System.Drawing.Size(166, 22)
        Me.ComboBox1.TabIndex = 345479
        Me.ComboBox1.TabStop = False
        Me.ComboBox1.Visible = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(9, 298)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 16)
        Me.Label3.TabIndex = 345482
        Me.Label3.Text = "Assigned Job"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(9, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 16)
        Me.Label4.TabIndex = 345483
        Me.Label4.Text = "Pending Job"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(9, 277)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(968, 232)
        Me.TabControl1.TabIndex = 345484
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txtSearchAssign)
        Me.TabPage1.Controls.Add(Me.cmbSearchAssign)
        Me.TabPage1.Controls.Add(Me.btnremoveAttend)
        Me.TabPage1.Controls.Add(Me.grdAssigned)
        Me.TabPage1.Controls.Add(Me.btncloseJob)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(960, 206)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Assigned Job"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btnremoveAttend
        '
        Me.btnremoveAttend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnremoveAttend.BackColor = System.Drawing.Color.SteelBlue
        Me.btnremoveAttend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnremoveAttend.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnremoveAttend.ForeColor = System.Drawing.Color.White
        Me.btnremoveAttend.Location = New System.Drawing.Point(872, 127)
        Me.btnremoveAttend.Name = "btnremoveAttend"
        Me.btnremoveAttend.Size = New System.Drawing.Size(82, 35)
        Me.btnremoveAttend.TabIndex = 345479
        Me.btnremoveAttend.TabStop = False
        Me.btnremoveAttend.Text = "Remove"
        Me.btnremoveAttend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnremoveAttend.UseVisualStyleBackColor = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.txtsearchCompleted)
        Me.TabPage2.Controls.Add(Me.cmbSearchCompleted)
        Me.TabPage2.Controls.Add(Me.btnundoClosing)
        Me.TabPage2.Controls.Add(Me.grdcompleted)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(960, 206)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Completed Job"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnundoClosing
        '
        Me.btnundoClosing.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnundoClosing.BackColor = System.Drawing.Color.SteelBlue
        Me.btnundoClosing.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnundoClosing.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnundoClosing.ForeColor = System.Drawing.Color.White
        Me.btnundoClosing.Location = New System.Drawing.Point(872, 168)
        Me.btnundoClosing.Name = "btnundoClosing"
        Me.btnundoClosing.Size = New System.Drawing.Size(82, 35)
        Me.btnundoClosing.TabIndex = 345480
        Me.btnundoClosing.TabStop = False
        Me.btnundoClosing.Text = "Undo"
        Me.btnundoClosing.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnundoClosing.UseVisualStyleBackColor = False
        '
        'grdcompleted
        '
        Me.grdcompleted.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdcompleted.BackgroundColor = System.Drawing.Color.White
        Me.grdcompleted.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdcompleted.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdcompleted.Location = New System.Drawing.Point(6, 6)
        Me.grdcompleted.Name = "grdcompleted"
        Me.grdcompleted.Size = New System.Drawing.Size(861, 170)
        Me.grdcompleted.TabIndex = 345479
        '
        'txtSearchAssign
        '
        Me.txtSearchAssign.AcceptsReturn = True
        Me.txtSearchAssign.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSearchAssign.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSearchAssign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchAssign.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearchAssign.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchAssign.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSearchAssign.Location = New System.Drawing.Point(179, 181)
        Me.txtSearchAssign.MaxLength = 500
        Me.txtSearchAssign.Name = "txtSearchAssign"
        Me.txtSearchAssign.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSearchAssign.Size = New System.Drawing.Size(268, 20)
        Me.txtSearchAssign.TabIndex = 345481
        '
        'cmbSearchAssign
        '
        Me.cmbSearchAssign.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbSearchAssign.BackColor = System.Drawing.SystemColors.Window
        Me.cmbSearchAssign.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbSearchAssign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearchAssign.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSearchAssign.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbSearchAssign.Location = New System.Drawing.Point(7, 181)
        Me.cmbSearchAssign.Name = "cmbSearchAssign"
        Me.cmbSearchAssign.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbSearchAssign.Size = New System.Drawing.Size(166, 22)
        Me.cmbSearchAssign.TabIndex = 345480
        Me.cmbSearchAssign.TabStop = False
        '
        'txtsearchCompleted
        '
        Me.txtsearchCompleted.AcceptsReturn = True
        Me.txtsearchCompleted.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtsearchCompleted.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtsearchCompleted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtsearchCompleted.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtsearchCompleted.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearchCompleted.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtsearchCompleted.Location = New System.Drawing.Point(176, 181)
        Me.txtsearchCompleted.MaxLength = 500
        Me.txtsearchCompleted.Name = "txtsearchCompleted"
        Me.txtsearchCompleted.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtsearchCompleted.Size = New System.Drawing.Size(268, 20)
        Me.txtsearchCompleted.TabIndex = 345483
        '
        'cmbSearchCompleted
        '
        Me.cmbSearchCompleted.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbSearchCompleted.BackColor = System.Drawing.SystemColors.Window
        Me.cmbSearchCompleted.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbSearchCompleted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearchCompleted.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSearchCompleted.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbSearchCompleted.Location = New System.Drawing.Point(4, 181)
        Me.cmbSearchCompleted.Name = "cmbSearchCompleted"
        Me.cmbSearchCompleted.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbSearchCompleted.Size = New System.Drawing.Size(166, 22)
        Me.cmbSearchCompleted.TabIndex = 345482
        Me.cmbSearchCompleted.TabStop = False
        '
        'JobPendingAssign
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1151, 513)
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.grdItem)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.plDelivery)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btnDelivery)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.btnload)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.txtSeq)
        Me.Controls.Add(Me.cmbOrder)
        Me.Name = "JobPendingAssign"
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
        CType(Me.grdAssigned, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.grdcompleted, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
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
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents grdAssigned As System.Windows.Forms.DataGridView
    Public WithEvents TextBox1 As System.Windows.Forms.TextBox
    Public WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents btnundoClosing As System.Windows.Forms.Button
    Friend WithEvents grdcompleted As System.Windows.Forms.DataGridView
    Friend WithEvents btnremoveAttend As System.Windows.Forms.Button
    Public WithEvents txtSearchAssign As System.Windows.Forms.TextBox
    Public WithEvents cmbSearchAssign As System.Windows.Forms.ComboBox
    Public WithEvents txtsearchCompleted As System.Windows.Forms.TextBox
    Public WithEvents cmbSearchCompleted As System.Windows.Forms.ComboBox
End Class
