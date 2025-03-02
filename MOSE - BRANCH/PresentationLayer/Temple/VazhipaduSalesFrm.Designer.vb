<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VazhipaduSalesFrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VazhipaduSalesFrm))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtSuppAlias = New System.Windows.Forms.TextBox
        Me.txtPurchAlias = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.txtCashCustomer = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbVoucherTp = New System.Windows.Forms.ComboBox
        Me.txtPurchaseName = New System.Windows.Forms.TextBox
        Me.txtprefix = New System.Windows.Forms.TextBox
        Me.numVchrNo = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtDescr = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtSuppName = New System.Windows.Forms.TextBox
        Me.cldrdate = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtReference = New System.Windows.Forms.TextBox
        Me.txtstar = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.btnrem = New System.Windows.Forms.Button
        Me.btnadd = New System.Windows.Forms.Button
        Me.btnprint = New System.Windows.Forms.Button
        Me.txtPPrefix = New System.Windows.Forms.TextBox
        Me.numPrintVchr = New System.Windows.Forms.TextBox
        Me.chkFormat = New System.Windows.Forms.CheckBox
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btnModify = New System.Windows.Forms.Button
        Me.btndelete = New System.Windows.Forms.Button
        Me.btnupdate = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.txtroundOff = New System.Windows.Forms.TextBox
        Me.cmbsign = New System.Windows.Forms.ComboBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.lblNetAmt = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.lblTotAmt = New System.Windows.Forms.Label
        Me.plsrch = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label9 = New System.Windows.Forms.Label
        Me.picCloseProd = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.grdSrch = New System.Windows.Forms.DataGridView
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtcustAddress = New System.Windows.Forms.TextBox
        Me.btnfind = New System.Windows.Forms.Button
        Me.cmbcolms = New System.Windows.Forms.ComboBox
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnSlct = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
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
        Me.Panel1.Controls.Add(Me.txtSuppAlias)
        Me.Panel1.Controls.Add(Me.txtPurchAlias)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1258, 36)
        Me.Panel1.TabIndex = 345445
        '
        'txtSuppAlias
        '
        Me.txtSuppAlias.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppAlias.Location = New System.Drawing.Point(705, 8)
        Me.txtSuppAlias.MaxLength = 15
        Me.txtSuppAlias.Name = "txtSuppAlias"
        Me.txtSuppAlias.Size = New System.Drawing.Size(156, 21)
        Me.txtSuppAlias.TabIndex = 345522
        Me.txtSuppAlias.Visible = False
        '
        'txtPurchAlias
        '
        Me.txtPurchAlias.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchAlias.Location = New System.Drawing.Point(867, 8)
        Me.txtPurchAlias.MaxLength = 15
        Me.txtPurchAlias.Name = "txtPurchAlias"
        Me.txtPurchAlias.Size = New System.Drawing.Size(156, 21)
        Me.txtPurchAlias.TabIndex = 345521
        Me.txtPurchAlias.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(39, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 18)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Vazhipadu Sales"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.SI
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(7, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(26, 27)
        Me.PictureBox1.TabIndex = 27
        Me.PictureBox1.TabStop = False
        '
        'txtCashCustomer
        '
        Me.txtCashCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCashCustomer.Location = New System.Drawing.Point(92, 94)
        Me.txtCashCustomer.Name = "txtCashCustomer"
        Me.txtCashCustomer.Size = New System.Drawing.Size(352, 21)
        Me.txtCashCustomer.TabIndex = 4
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label32.Location = New System.Drawing.Point(7, 95)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(79, 15)
        Me.Label32.TabIndex = 345495
        Me.Label32.Text = "CS Customer"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(7, 69)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 15)
        Me.Label7.TabIndex = 345493
        Me.Label7.Text = "Sales A/C"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(7, 44)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 345492
        Me.Label5.Text = "Voucher Type"
        '
        'cmbVoucherTp
        '
        Me.cmbVoucherTp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVoucherTp.FormattingEnabled = True
        Me.cmbVoucherTp.Location = New System.Drawing.Point(92, 43)
        Me.cmbVoucherTp.Name = "cmbVoucherTp"
        Me.cmbVoucherTp.Size = New System.Drawing.Size(128, 21)
        Me.cmbVoucherTp.TabIndex = 345491
        '
        'txtPurchaseName
        '
        Me.txtPurchaseName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseName.Location = New System.Drawing.Point(92, 67)
        Me.txtPurchaseName.MaxLength = 15
        Me.txtPurchaseName.Name = "txtPurchaseName"
        Me.txtPurchaseName.ReadOnly = True
        Me.txtPurchaseName.Size = New System.Drawing.Size(273, 21)
        Me.txtPurchaseName.TabIndex = 345490
        '
        'txtprefix
        '
        Me.txtprefix.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprefix.Location = New System.Drawing.Point(306, 42)
        Me.txtprefix.MaxLength = 15
        Me.txtprefix.Name = "txtprefix"
        Me.txtprefix.ReadOnly = True
        Me.txtprefix.Size = New System.Drawing.Size(59, 21)
        Me.txtprefix.TabIndex = 345489
        '
        'numVchrNo
        '
        Me.numVchrNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numVchrNo.Location = New System.Drawing.Point(368, 42)
        Me.numVchrNo.Name = "numVchrNo"
        Me.numVchrNo.ReadOnly = True
        Me.numVchrNo.Size = New System.Drawing.Size(76, 21)
        Me.numVchrNo.TabIndex = 0
        Me.numVchrNo.TabStop = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(226, 44)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 15)
        Me.Label14.TabIndex = 345484
        Me.Label14.Text = "Voucher No."
        '
        'txtDescr
        '
        Me.txtDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescr.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescr.Location = New System.Drawing.Point(92, 118)
        Me.txtDescr.Name = "txtDescr"
        Me.txtDescr.Size = New System.Drawing.Size(872, 21)
        Me.txtDescr.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(7, 119)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 15)
        Me.Label6.TabIndex = 345487
        Me.Label6.Text = "Description"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(400, 69)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(113, 15)
        Me.Label8.TabIndex = 345488
        Me.Label8.Text = "Customer/Debit A/C"
        '
        'txtSuppName
        '
        Me.txtSuppName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppName.Location = New System.Drawing.Point(521, 71)
        Me.txtSuppName.Name = "txtSuppName"
        Me.txtSuppName.ReadOnly = True
        Me.txtSuppName.Size = New System.Drawing.Size(325, 21)
        Me.txtSuppName.TabIndex = 3
        '
        'cldrdate
        '
        Me.cldrdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrdate.Location = New System.Drawing.Point(740, 44)
        Me.cldrdate.Name = "cldrdate"
        Me.cldrdate.Size = New System.Drawing.Size(106, 20)
        Me.cldrdate.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(683, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 15)
        Me.Label3.TabIndex = 345499
        Me.Label3.Text = "Vr.Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(490, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 15)
        Me.Label2.TabIndex = 345498
        Me.Label2.Text = "Reference"
        '
        'txtReference
        '
        Me.txtReference.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReference.Location = New System.Drawing.Point(557, 44)
        Me.txtReference.MaxLength = 15
        Me.txtReference.Name = "txtReference"
        Me.txtReference.Size = New System.Drawing.Size(120, 21)
        Me.txtReference.TabIndex = 1
        '
        'txtstar
        '
        Me.txtstar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstar.Location = New System.Drawing.Point(521, 95)
        Me.txtstar.Name = "txtstar"
        Me.txtstar.Size = New System.Drawing.Size(325, 21)
        Me.txtstar.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(484, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 15)
        Me.Label4.TabIndex = 345501
        Me.Label4.Text = "Star"
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
        Me.grdVoucher.Location = New System.Drawing.Point(10, 149)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(1070, 308)
        Me.grdVoucher.TabIndex = 345502
        '
        'btnrem
        '
        Me.btnrem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnrem.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrem.ForeColor = System.Drawing.Color.White
        Me.btnrem.Location = New System.Drawing.Point(1027, 118)
        Me.btnrem.Name = "btnrem"
        Me.btnrem.Size = New System.Drawing.Size(53, 29)
        Me.btnrem.TabIndex = 345504
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
        Me.btnadd.Location = New System.Drawing.Point(971, 118)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(53, 29)
        Me.btnadd.TabIndex = 345503
        Me.btnadd.Text = "&Add"
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnprint.BackColor = System.Drawing.Color.SteelBlue
        Me.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.ForeColor = System.Drawing.Color.White
        Me.btnprint.Location = New System.Drawing.Point(1145, 252)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(100, 35)
        Me.btnprint.TabIndex = 345512
        Me.btnprint.TabStop = False
        Me.btnprint.Text = "Print"
        Me.btnprint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnprint.UseVisualStyleBackColor = False
        '
        'txtPPrefix
        '
        Me.txtPPrefix.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPPrefix.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPPrefix.Location = New System.Drawing.Point(1146, 189)
        Me.txtPPrefix.Name = "txtPPrefix"
        Me.txtPPrefix.Size = New System.Drawing.Size(37, 21)
        Me.txtPPrefix.TabIndex = 345511
        '
        'numPrintVchr
        '
        Me.numPrintVchr.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numPrintVchr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPrintVchr.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numPrintVchr.Location = New System.Drawing.Point(1184, 189)
        Me.numPrintVchr.Name = "numPrintVchr"
        Me.numPrintVchr.Size = New System.Drawing.Size(61, 21)
        Me.numPrintVchr.TabIndex = 345510
        '
        'chkFormat
        '
        Me.chkFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(1153, 288)
        Me.chkFormat.Name = "chkFormat"
        Me.chkFormat.Size = New System.Drawing.Size(65, 19)
        Me.chkFormat.TabIndex = 345509
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
        Me.btnPreview.Location = New System.Drawing.Point(1146, 214)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(100, 35)
        Me.btnPreview.TabIndex = 345505
        Me.btnPreview.TabStop = False
        Me.btnPreview.Text = "Pre&view"
        Me.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'btnModify
        '
        Me.btnModify.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnModify.BackColor = System.Drawing.Color.SteelBlue
        Me.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnModify.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModify.ForeColor = System.Drawing.Color.White
        Me.btnModify.Location = New System.Drawing.Point(1146, 115)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(100, 35)
        Me.btnModify.TabIndex = 345506
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
        Me.btndelete.Location = New System.Drawing.Point(1146, 152)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(100, 35)
        Me.btndelete.TabIndex = 345508
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
        Me.btnupdate.Location = New System.Drawing.Point(1146, 77)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(100, 35)
        Me.btnupdate.TabIndex = 345507
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
        Me.btnExit.Location = New System.Drawing.Point(1151, 465)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(101, 35)
        Me.btnExit.TabIndex = 345513
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel2.Controls.Add(Me.txtroundOff)
        Me.Panel2.Controls.Add(Me.cmbsign)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.lblNetAmt)
        Me.Panel2.Controls.Add(Me.Label21)
        Me.Panel2.Controls.Add(Me.Label27)
        Me.Panel2.Controls.Add(Me.lblTotAmt)
        Me.Panel2.Location = New System.Drawing.Point(1099, 310)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(153, 147)
        Me.Panel2.TabIndex = 345514
        '
        'txtroundOff
        '
        Me.txtroundOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtroundOff.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtroundOff.Location = New System.Drawing.Point(74, 67)
        Me.txtroundOff.Name = "txtroundOff"
        Me.txtroundOff.Size = New System.Drawing.Size(73, 23)
        Me.txtroundOff.TabIndex = 345454
        Me.txtroundOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbsign
        '
        Me.cmbsign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsign.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsign.FormattingEnabled = True
        Me.cmbsign.Items.AddRange(New Object() {"+", "-"})
        Me.cmbsign.Location = New System.Drawing.Point(22, 68)
        Me.cmbsign.Name = "cmbsign"
        Me.cmbsign.Size = New System.Drawing.Size(50, 21)
        Me.cmbsign.TabIndex = 345452
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Navy
        Me.Label17.Location = New System.Drawing.Point(7, 50)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(71, 15)
        Me.Label17.TabIndex = 345453
        Me.Label17.Text = "Round Off"
        '
        'lblNetAmt
        '
        Me.lblNetAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNetAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmt.ForeColor = System.Drawing.Color.Red
        Me.lblNetAmt.Location = New System.Drawing.Point(7, 108)
        Me.lblNetAmt.Name = "lblNetAmt"
        Me.lblNetAmt.Size = New System.Drawing.Size(140, 24)
        Me.lblNetAmt.TabIndex = 103
        Me.lblNetAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(7, 7)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(80, 15)
        Me.Label21.TabIndex = 95
        Me.Label21.Text = "Gross Total"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Maroon
        Me.Label27.Location = New System.Drawing.Point(7, 93)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(60, 13)
        Me.Label27.TabIndex = 100
        Me.Label27.Text = "Net Total"
        '
        'lblTotAmt
        '
        Me.lblTotAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblTotAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotAmt.Location = New System.Drawing.Point(7, 25)
        Me.lblTotAmt.Name = "lblTotAmt"
        Me.lblTotAmt.Size = New System.Drawing.Size(140, 24)
        Me.lblTotAmt.TabIndex = 102
        Me.lblTotAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'plsrch
        '
        Me.plsrch.Controls.Add(Me.Panel3)
        Me.plsrch.Controls.Add(Me.grdSrch)
        Me.plsrch.Location = New System.Drawing.Point(391, 152)
        Me.plsrch.Name = "plsrch"
        Me.plsrch.Size = New System.Drawing.Size(477, 264)
        Me.plsrch.TabIndex = 345515
        Me.plsrch.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.picCloseProd)
        Me.Panel3.Controls.Add(Me.PictureBox2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(477, 32)
        Me.Panel3.TabIndex = 345445
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Font = New System.Drawing.Font("Goudy Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(31, 5)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(91, 21)
        Me.Label9.TabIndex = 27
        Me.Label9.Text = "Select Item.."
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
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(849, 44)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(51, 15)
        Me.Label18.TabIndex = 345517
        Me.Label18.Text = "Address"
        '
        'txtcustAddress
        '
        Me.txtcustAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtcustAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustAddress.Location = New System.Drawing.Point(906, 44)
        Me.txtcustAddress.Multiline = True
        Me.txtcustAddress.Name = "txtcustAddress"
        Me.txtcustAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtcustAddress.Size = New System.Drawing.Size(174, 65)
        Me.txtcustAddress.TabIndex = 345516
        '
        'btnfind
        '
        Me.btnfind.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnfind.BackColor = System.Drawing.Color.SteelBlue
        Me.btnfind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnfind.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnfind.ForeColor = System.Drawing.Color.White
        Me.btnfind.Location = New System.Drawing.Point(267, 463)
        Me.btnfind.Name = "btnfind"
        Me.btnfind.Size = New System.Drawing.Size(66, 23)
        Me.btnfind.TabIndex = 345520
        Me.btnfind.Text = "Search"
        Me.btnfind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnfind.UseVisualStyleBackColor = False
        '
        'cmbcolms
        '
        Me.cmbcolms.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbcolms.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcolms.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbcolms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcolms.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcolms.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcolms.Location = New System.Drawing.Point(10, 463)
        Me.cmbcolms.Name = "cmbcolms"
        Me.cmbcolms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbcolms.Size = New System.Drawing.Size(123, 22)
        Me.cmbcolms.TabIndex = 345518
        Me.cmbcolms.TabStop = False
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
        Me.txtSeq.Location = New System.Drawing.Point(138, 463)
        Me.txtSeq.MaxLength = 50
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(123, 20)
        Me.txtSeq.TabIndex = 345519
        '
        'Timer1
        '
        '
        'btnSlct
        '
        Me.btnSlct.BackColor = System.Drawing.SystemColors.Control
        Me.btnSlct.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnSlct.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSlct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSlct.Image = CType(resources.GetObject("btnSlct.Image"), System.Drawing.Image)
        Me.btnSlct.Location = New System.Drawing.Point(450, 39)
        Me.btnSlct.Name = "btnSlct"
        Me.btnSlct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnSlct.Size = New System.Drawing.Size(30, 26)
        Me.btnSlct.TabIndex = 345521
        Me.btnSlct.TabStop = False
        Me.btnSlct.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSlct.UseVisualStyleBackColor = False
        Me.btnSlct.Visible = False
        '
        'VazhipaduSalesFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1258, 502)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnSlct)
        Me.Controls.Add(Me.btnfind)
        Me.Controls.Add(Me.cmbcolms)
        Me.Controls.Add(Me.txtSeq)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtcustAddress)
        Me.Controls.Add(Me.plsrch)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.txtPPrefix)
        Me.Controls.Add(Me.numPrintVchr)
        Me.Controls.Add(Me.chkFormat)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btnModify)
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.btnupdate)
        Me.Controls.Add(Me.btnrem)
        Me.Controls.Add(Me.btnadd)
        Me.Controls.Add(Me.grdVoucher)
        Me.Controls.Add(Me.txtstar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cldrdate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtReference)
        Me.Controls.Add(Me.txtCashCustomer)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbVoucherTp)
        Me.Controls.Add(Me.txtPurchaseName)
        Me.Controls.Add(Me.txtprefix)
        Me.Controls.Add(Me.numVchrNo)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtDescr)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtSuppName)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "VazhipaduSalesFrm"
        Me.Text = "Vazhipadu Sales"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
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
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents txtCashCustomer As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbVoucherTp As System.Windows.Forms.ComboBox
    Friend WithEvents txtPurchaseName As System.Windows.Forms.TextBox
    Friend WithEvents txtprefix As System.Windows.Forms.TextBox
    Friend WithEvents numVchrNo As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtDescr As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtSuppName As System.Windows.Forms.TextBox
    Friend WithEvents cldrdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtReference As System.Windows.Forms.TextBox
    Friend WithEvents txtstar As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grdVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents btnrem As System.Windows.Forms.Button
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents btnprint As System.Windows.Forms.Button
    Friend WithEvents txtPPrefix As System.Windows.Forms.TextBox
    Friend WithEvents numPrintVchr As System.Windows.Forms.TextBox
    Friend WithEvents chkFormat As System.Windows.Forms.CheckBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnModify As System.Windows.Forms.Button
    Friend WithEvents btndelete As System.Windows.Forms.Button
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtroundOff As System.Windows.Forms.TextBox
    Friend WithEvents cmbsign As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblNetAmt As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents lblTotAmt As System.Windows.Forms.Label
    Friend WithEvents plsrch As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents picCloseProd As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents grdSrch As System.Windows.Forms.DataGridView
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtcustAddress As System.Windows.Forms.TextBox
    Friend WithEvents btnfind As System.Windows.Forms.Button
    Public WithEvents cmbcolms As System.Windows.Forms.ComboBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents txtSuppAlias As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchAlias As System.Windows.Forms.TextBox
    Public WithEvents btnSlct As System.Windows.Forms.Button
End Class
