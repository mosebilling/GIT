<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GoodsTransferFrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GoodsTransferFrm))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnrem = New System.Windows.Forms.Button
        Me.btnadd = New System.Windows.Forms.Button
        Me.txtDescr = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtjobname = New System.Windows.Forms.TextBox
        Me.txtJob = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtjobnamefrom = New System.Windows.Forms.TextBox
        Me.txtjobfrom = New System.Windows.Forms.TextBox
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.Label14 = New System.Windows.Forms.Label
        Me.btnSlct = New System.Windows.Forms.Button
        Me.cldrdate = New System.Windows.Forms.DateTimePicker
        Me.numVchrNo = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtReference = New System.Windows.Forms.TextBox
        Me.numPrintVchr = New System.Windows.Forms.TextBox
        Me.chkFormat = New System.Windows.Forms.CheckBox
        Me.btnPreview = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblNetAmt = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.btnModify = New System.Windows.Forms.Button
        Me.btndelete = New System.Windows.Forms.Button
        Me.btnupdate = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.lblLocFrom = New System.Windows.Forms.Label
        Me.lbllocto = New System.Windows.Forms.Label
        Me.plsrch = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.picCloseProd = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.grdSrch = New System.Windows.Forms.DataGridView
        Me.txtcustomer = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmblocationFrom = New System.Windows.Forms.ComboBox
        Me.cmblocationTo = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plsrch.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.picCloseProd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSrch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1018, 36)
        Me.Panel1.TabIndex = 345445
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(3, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(38, 21)
        Me.PictureBox1.TabIndex = 345461
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(41, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 18)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Goods Issued Note"
        '
        'btnrem
        '
        Me.btnrem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnrem.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrem.ForeColor = System.Drawing.Color.White
        Me.btnrem.Location = New System.Drawing.Point(845, 139)
        Me.btnrem.Name = "btnrem"
        Me.btnrem.Size = New System.Drawing.Size(55, 32)
        Me.btnrem.TabIndex = 345450
        Me.btnrem.TabStop = False
        Me.btnrem.Text = "Rem"
        Me.btnrem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrem.UseVisualStyleBackColor = False
        '
        'btnadd
        '
        Me.btnadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnadd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.ForeColor = System.Drawing.Color.White
        Me.btnadd.Location = New System.Drawing.Point(787, 139)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(55, 32)
        Me.btnadd.TabIndex = 6
        Me.btnadd.Text = "Add"
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'txtDescr
        '
        Me.txtDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescr.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescr.Location = New System.Drawing.Point(91, 150)
        Me.txtDescr.Name = "txtDescr"
        Me.txtDescr.Size = New System.Drawing.Size(690, 21)
        Me.txtDescr.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 152)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 15)
        Me.Label6.TabIndex = 345444
        Me.Label6.Text = "Description"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(202, 100)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 15)
        Me.Label9.TabIndex = 345442
        Me.Label9.Text = "Job To"
        '
        'txtjobname
        '
        Me.txtjobname.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtjobname.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtjobname.Location = New System.Drawing.Point(399, 99)
        Me.txtjobname.Name = "txtjobname"
        Me.txtjobname.ReadOnly = True
        Me.txtjobname.Size = New System.Drawing.Size(320, 21)
        Me.txtjobname.TabIndex = 345441
        Me.txtjobname.TabStop = False
        '
        'txtJob
        '
        Me.txtJob.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJob.Location = New System.Drawing.Point(268, 99)
        Me.txtJob.Name = "txtJob"
        Me.txtJob.Size = New System.Drawing.Size(126, 21)
        Me.txtJob.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(202, 77)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 345386
        Me.Label8.Text = "Job From"
        '
        'txtjobnamefrom
        '
        Me.txtjobnamefrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtjobnamefrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtjobnamefrom.Location = New System.Drawing.Point(399, 74)
        Me.txtjobnamefrom.Name = "txtjobnamefrom"
        Me.txtjobnamefrom.ReadOnly = True
        Me.txtjobnamefrom.Size = New System.Drawing.Size(320, 21)
        Me.txtjobnamefrom.TabIndex = 345385
        Me.txtjobnamefrom.TabStop = False
        '
        'txtjobfrom
        '
        Me.txtjobfrom.BackColor = System.Drawing.Color.White
        Me.txtjobfrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtjobfrom.Location = New System.Drawing.Point(268, 73)
        Me.txtjobfrom.Name = "txtjobfrom"
        Me.txtjobfrom.Size = New System.Drawing.Size(126, 21)
        Me.txtjobfrom.TabIndex = 3
        '
        'grdVoucher
        '
        Me.grdVoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdVoucher.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdVoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdVoucher.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        Me.grdVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdVoucher.Location = New System.Drawing.Point(15, 177)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(885, 327)
        Me.grdVoucher.TabIndex = 345383
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(12, 48)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 13)
        Me.Label14.TabIndex = 345376
        Me.Label14.Text = "Voucher No."
        '
        'btnSlct
        '
        Me.btnSlct.BackColor = System.Drawing.SystemColors.Control
        Me.btnSlct.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnSlct.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSlct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSlct.Image = CType(resources.GetObject("btnSlct.Image"), System.Drawing.Image)
        Me.btnSlct.Location = New System.Drawing.Point(168, 44)
        Me.btnSlct.Name = "btnSlct"
        Me.btnSlct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnSlct.Size = New System.Drawing.Size(30, 26)
        Me.btnSlct.TabIndex = 345375
        Me.btnSlct.TabStop = False
        Me.btnSlct.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSlct.UseVisualStyleBackColor = False
        Me.btnSlct.Visible = False
        '
        'cldrdate
        '
        Me.cldrdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrdate.Location = New System.Drawing.Point(435, 45)
        Me.cldrdate.Name = "cldrdate"
        Me.cldrdate.Size = New System.Drawing.Size(106, 20)
        Me.cldrdate.TabIndex = 2
        '
        'numVchrNo
        '
        Me.numVchrNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numVchrNo.Location = New System.Drawing.Point(91, 45)
        Me.numVchrNo.Name = "numVchrNo"
        Me.numVchrNo.Size = New System.Drawing.Size(76, 21)
        Me.numVchrNo.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(383, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 345374
        Me.Label3.Text = "Vr.Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(202, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 345373
        Me.Label2.Text = "Reference"
        '
        'txtReference
        '
        Me.txtReference.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReference.Location = New System.Drawing.Point(268, 45)
        Me.txtReference.MaxLength = 15
        Me.txtReference.Name = "txtReference"
        Me.txtReference.Size = New System.Drawing.Size(109, 21)
        Me.txtReference.TabIndex = 1
        '
        'numPrintVchr
        '
        Me.numPrintVchr.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numPrintVchr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPrintVchr.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numPrintVchr.Location = New System.Drawing.Point(910, 389)
        Me.numPrintVchr.Name = "numPrintVchr"
        Me.numPrintVchr.Size = New System.Drawing.Size(100, 21)
        Me.numPrintVchr.TabIndex = 345453
        '
        'chkFormat
        '
        Me.chkFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(910, 373)
        Me.chkFormat.Name = "chkFormat"
        Me.chkFormat.Size = New System.Drawing.Size(65, 19)
        Me.chkFormat.TabIndex = 345452
        Me.chkFormat.Text = "Format"
        Me.chkFormat.UseVisualStyleBackColor = False
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.BackColor = System.Drawing.Color.SteelBlue
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.White
        Me.btnPreview.Location = New System.Drawing.Point(910, 411)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(100, 35)
        Me.btnPreview.TabIndex = 345451
        Me.btnPreview.TabStop = False
        Me.btnPreview.Text = "Pre&view"
        Me.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        '
        'lblNetAmt
        '
        Me.lblNetAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNetAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNetAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmt.ForeColor = System.Drawing.Color.Red
        Me.lblNetAmt.Location = New System.Drawing.Point(780, 480)
        Me.lblNetAmt.Name = "lblNetAmt"
        Me.lblNetAmt.Size = New System.Drawing.Size(123, 24)
        Me.lblNetAmt.TabIndex = 103
        Me.lblNetAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblNetAmt.Visible = False
        '
        'Label27
        '
        Me.Label27.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(777, 465)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(51, 13)
        Me.Label27.TabIndex = 100
        Me.Label27.Text = "Net Total"
        Me.Label27.Visible = False
        '
        'btnModify
        '
        Me.btnModify.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnModify.BackColor = System.Drawing.Color.SteelBlue
        Me.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnModify.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModify.ForeColor = System.Drawing.Color.White
        Me.btnModify.Location = New System.Drawing.Point(910, 298)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(100, 35)
        Me.btnModify.TabIndex = 345455
        Me.btnModify.Text = "&Modify"
        Me.btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnModify.UseVisualStyleBackColor = False
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btndelete.BackColor = System.Drawing.Color.SteelBlue
        Me.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.ForeColor = System.Drawing.Color.White
        Me.btndelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btndelete.Location = New System.Drawing.Point(910, 335)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(100, 35)
        Me.btndelete.TabIndex = 345457
        Me.btndelete.Text = "Delete"
        Me.btndelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndelete.UseVisualStyleBackColor = False
        '
        'btnupdate
        '
        Me.btnupdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnupdate.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnupdate.Location = New System.Drawing.Point(910, 260)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(100, 35)
        Me.btnupdate.TabIndex = 345456
        Me.btnupdate.Text = "&Update "
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(909, 467)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(101, 35)
        Me.btnExit.TabIndex = 345463
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'lblLocFrom
        '
        Me.lblLocFrom.AutoSize = True
        Me.lblLocFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblLocFrom.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocFrom.Location = New System.Drawing.Point(726, 72)
        Me.lblLocFrom.Name = "lblLocFrom"
        Me.lblLocFrom.Size = New System.Drawing.Size(59, 13)
        Me.lblLocFrom.TabIndex = 345464
        Me.lblLocFrom.Text = "Loc From"
        Me.lblLocFrom.Visible = False
        '
        'lbllocto
        '
        Me.lbllocto.AutoSize = True
        Me.lbllocto.BackColor = System.Drawing.Color.Transparent
        Me.lbllocto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbllocto.Location = New System.Drawing.Point(726, 98)
        Me.lbllocto.Name = "lbllocto"
        Me.lbllocto.Size = New System.Drawing.Size(44, 15)
        Me.lbllocto.TabIndex = 345465
        Me.lbllocto.Text = "Loc To"
        Me.lbllocto.Visible = False
        '
        'plsrch
        '
        Me.plsrch.Controls.Add(Me.Panel3)
        Me.plsrch.Controls.Add(Me.grdSrch)
        Me.plsrch.Location = New System.Drawing.Point(423, 240)
        Me.plsrch.Name = "plsrch"
        Me.plsrch.Size = New System.Drawing.Size(477, 264)
        Me.plsrch.TabIndex = 345466
        Me.plsrch.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.picCloseProd)
        Me.Panel3.Controls.Add(Me.PictureBox2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(477, 32)
        Me.Panel3.TabIndex = 345445
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Goudy Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(31, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 21)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Select Item.."
        '
        'picCloseProd
        '
        Me.picCloseProd.BackColor = System.Drawing.Color.Transparent
        Me.picCloseProd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picCloseProd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picCloseProd.Image = Global.SMSMP.My.Resources.Resources.CloseButton
        Me.picCloseProd.Location = New System.Drawing.Point(460, 9)
        Me.picCloseProd.Name = "picCloseProd"
        Me.picCloseProd.Size = New System.Drawing.Size(12, 12)
        Me.picCloseProd.TabIndex = 345356
        Me.picCloseProd.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.search
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(4, 5)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(27, 23)
        Me.PictureBox2.TabIndex = 27
        Me.PictureBox2.TabStop = False
        '
        'grdSrch
        '
        Me.grdSrch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSrch.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.grdSrch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSrch.Location = New System.Drawing.Point(7, 35)
        Me.grdSrch.Name = "grdSrch"
        Me.grdSrch.Size = New System.Drawing.Size(465, 223)
        Me.grdSrch.TabIndex = 3
        '
        'txtcustomer
        '
        Me.txtcustomer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtcustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtcustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustomer.Location = New System.Drawing.Point(91, 123)
        Me.txtcustomer.Name = "txtcustomer"
        Me.txtcustomer.ReadOnly = True
        Me.txtcustomer.Size = New System.Drawing.Size(690, 21)
        Me.txtcustomer.TabIndex = 345467
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 125)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 15)
        Me.Label5.TabIndex = 345468
        Me.Label5.Text = "Customer"
        '
        'cmblocationFrom
        '
        Me.cmblocationFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmblocationFrom.FormattingEnabled = True
        Me.cmblocationFrom.Location = New System.Drawing.Point(91, 77)
        Me.cmblocationFrom.Name = "cmblocationFrom"
        Me.cmblocationFrom.Size = New System.Drawing.Size(103, 21)
        Me.cmblocationFrom.TabIndex = 345469
        '
        'cmblocationTo
        '
        Me.cmblocationTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmblocationTo.FormattingEnabled = True
        Me.cmblocationTo.Location = New System.Drawing.Point(91, 101)
        Me.cmblocationTo.Name = "cmblocationTo"
        Me.cmblocationTo.Size = New System.Drawing.Size(103, 21)
        Me.cmblocationTo.TabIndex = 345470
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 100)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 15)
        Me.Label7.TabIndex = 345472
        Me.Label7.Text = "LOC. To"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 77)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 13)
        Me.Label10.TabIndex = 345471
        Me.Label10.Text = "LOC. From"
        '
        'GoodsTransferFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1018, 509)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cmblocationTo)
        Me.Controls.Add(Me.cmblocationFrom)
        Me.Controls.Add(Me.txtcustomer)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.plsrch)
        Me.Controls.Add(Me.lbllocto)
        Me.Controls.Add(Me.lblLocFrom)
        Me.Controls.Add(Me.lblNetAmt)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnModify)
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.btnupdate)
        Me.Controls.Add(Me.cldrdate)
        Me.Controls.Add(Me.btnrem)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.numPrintVchr)
        Me.Controls.Add(Me.btnadd)
        Me.Controls.Add(Me.chkFormat)
        Me.Controls.Add(Me.txtDescr)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtjobname)
        Me.Controls.Add(Me.txtJob)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtjobnamefrom)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtjobfrom)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtReference)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnSlct)
        Me.Controls.Add(Me.numVchrNo)
        Me.Controls.Add(Me.grdVoucher)
        Me.Name = "GoodsTransferFrm"
        Me.Text = "SalesOrderFrm"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plsrch.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.picCloseProd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSrch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents btnSlct As System.Windows.Forms.Button
    Friend WithEvents cldrdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents numVchrNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtReference As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtjobnamefrom As System.Windows.Forms.TextBox
    Friend WithEvents txtjobfrom As System.Windows.Forms.TextBox
    Friend WithEvents grdVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtjobname As System.Windows.Forms.TextBox
    Friend WithEvents txtJob As System.Windows.Forms.TextBox
    Friend WithEvents txtDescr As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnrem As System.Windows.Forms.Button
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents numPrintVchr As System.Windows.Forms.TextBox
    Friend WithEvents chkFormat As System.Windows.Forms.CheckBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblNetAmt As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents btnModify As System.Windows.Forms.Button
    Friend WithEvents btndelete As System.Windows.Forms.Button
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblLocFrom As System.Windows.Forms.Label
    Friend WithEvents lbllocto As System.Windows.Forms.Label
    Friend WithEvents plsrch As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents picCloseProd As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents grdSrch As System.Windows.Forms.DataGridView
    Friend WithEvents txtcustomer As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmblocationFrom As System.Windows.Forms.ComboBox
    Friend WithEvents cmblocationTo As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
