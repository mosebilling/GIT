<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LodgeCheckInFrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LodgeCheckInFrm))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.chktotal = New System.Windows.Forms.CheckBox
        Me.btninvoice = New System.Windows.Forms.Button
        Me.rdoserviceinv = New System.Windows.Forms.RadioButton
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.btnitmadd = New System.Windows.Forms.Button
        Me.lbldays = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblrent = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.btncheckout = New System.Windows.Forms.Button
        Me.btnroomedit = New System.Windows.Forms.Button
        Me.grdRooms = New System.Windows.Forms.DataGridView
        Me.btnrem = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblserviceGridTotal = New System.Windows.Forms.Label
        Me.btnsaveService = New System.Windows.Forms.Button
        Me.chkcal = New System.Windows.Forms.CheckBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.grdroomAdded = New System.Windows.Forms.DataGridView
        Me.Label24 = New System.Windows.Forms.Label
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.btnaddservice = New System.Windows.Forms.Button
        Me.btnremoveservice = New System.Windows.Forms.Button
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.Label18 = New System.Windows.Forms.Label
        Me.grdrvlist = New System.Windows.Forms.DataGridView
        Me.grdinvList = New System.Windows.Forms.DataGridView
        Me.cmbreceipttype = New System.Windows.Forms.ComboBox
        Me.btnrefreshVoucher = New System.Windows.Forms.Button
        Me.btncreateRV = New System.Windows.Forms.Button
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.rdoall = New System.Windows.Forms.RadioButton
        Me.btnundo = New System.Windows.Forms.Button
        Me.btnload = New System.Windows.Forms.Button
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.rdonone = New System.Windows.Forms.RadioButton
        Me.rdocheckout = New System.Windows.Forms.RadioButton
        Me.rdocheckin = New System.Windows.Forms.RadioButton
        Me.rdoactive = New System.Windows.Forms.RadioButton
        Me.rdoclosed = New System.Windows.Forms.RadioButton
        Me.cldrEnddate = New System.Windows.Forms.DateTimePicker
        Me.Label13 = New System.Windows.Forms.Label
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.grdItem = New System.Windows.Forms.DataGridView
        Me.chkwS = New System.Windows.Forms.CheckBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label16 = New System.Windows.Forms.Label
        Me.lbltotalRent = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnSlct = New System.Windows.Forms.Button
        Me.txtbookRef = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblgstn = New System.Windows.Forms.Label
        Me.dtpdate = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtjobcode = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtaddress = New System.Windows.Forms.TextBox
        Me.btnadd = New System.Windows.Forms.Button
        Me.txtcustomer = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.lblinvamt = New System.Windows.Forms.Label
        Me.lblRv = New System.Windows.Forms.Label
        Me.lblbalance = New System.Windows.Forms.Label
        Me.grpinvoice = New System.Windows.Forms.GroupBox
        Me.chkconsolidateservice = New System.Windows.Forms.CheckBox
        Me.rdoinvoice = New System.Windows.Forms.RadioButton
        Me.Label19 = New System.Windows.Forms.Label
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.txtkids = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmbidentityproof = New System.Windows.Forms.ComboBox
        Me.btnattach = New System.Windows.Forms.Button
        Me.txtvechicledetails = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtidentityProofNumber = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtfemaleGust = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtmaleGust = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtnumberofGust = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblserviceCharge = New System.Windows.Forms.Label
        Me.lblJobvalue = New System.Windows.Forms.Label
        Me.chkFormat = New System.Windows.Forms.CheckBox
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btndelete = New System.Windows.Forms.Button
        Me.btnupdate = New System.Windows.Forms.Button
        Me.Label26 = New System.Windows.Forms.Label
        Me.TabControl2 = New System.Windows.Forms.TabControl
        Me.txtprintjob = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.grdSrch = New System.Windows.Forms.DataGridView
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label11 = New System.Windows.Forms.Label
        Me.picCloseProd = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.plsrch = New System.Windows.Forms.Panel
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.lblcreatedinfo = New System.Windows.Forms.Label
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdRooms, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        CType(Me.grdroomAdded, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.grdrvlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdinvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.grpinvoice.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.grdSrch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.picCloseProd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plsrch.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gray
        Me.Panel1.Location = New System.Drawing.Point(1405, 263)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(199, 24)
        Me.Panel1.TabIndex = 345465
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(3, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(35, 22)
        Me.PictureBox1.TabIndex = 345457
        Me.PictureBox1.TabStop = False
        '
        'chktotal
        '
        Me.chktotal.AutoSize = True
        Me.chktotal.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chktotal.Location = New System.Drawing.Point(27, 101)
        Me.chktotal.Name = "chktotal"
        Me.chktotal.Size = New System.Drawing.Size(141, 17)
        Me.chktotal.TabIndex = 345483
        Me.chktotal.TabStop = False
        Me.chktotal.Text = "Consolidate Amount"
        Me.chktotal.UseVisualStyleBackColor = True
        '
        'btninvoice
        '
        Me.btninvoice.BackColor = System.Drawing.Color.SteelBlue
        Me.btninvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btninvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btninvoice.ForeColor = System.Drawing.Color.White
        Me.btninvoice.Location = New System.Drawing.Point(6, 119)
        Me.btninvoice.Name = "btninvoice"
        Me.btninvoice.Size = New System.Drawing.Size(102, 33)
        Me.btninvoice.TabIndex = 345482
        Me.btninvoice.TabStop = False
        Me.btninvoice.Text = "Create &Invoice"
        Me.btninvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btninvoice.UseVisualStyleBackColor = False
        '
        'rdoserviceinv
        '
        Me.rdoserviceinv.AutoSize = True
        Me.rdoserviceinv.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoserviceinv.Location = New System.Drawing.Point(6, 81)
        Me.rdoserviceinv.Name = "rdoserviceinv"
        Me.rdoserviceinv.Size = New System.Drawing.Size(114, 17)
        Me.rdoserviceinv.TabIndex = 345481
        Me.rdoserviceinv.Text = "Service Invoice"
        Me.rdoserviceinv.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btnitmadd)
        Me.TabPage2.Controls.Add(Me.lbldays)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.lblrent)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.btncheckout)
        Me.TabPage2.Controls.Add(Me.btnroomedit)
        Me.TabPage2.Controls.Add(Me.grdRooms)
        Me.TabPage2.Controls.Add(Me.btnrem)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1199, 194)
        Me.TabPage2.TabIndex = 0
        Me.TabPage2.Text = "Rooms"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnitmadd
        '
        Me.btnitmadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnitmadd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnitmadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnitmadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnitmadd.ForeColor = System.Drawing.Color.White
        Me.btnitmadd.Location = New System.Drawing.Point(3, 162)
        Me.btnitmadd.Name = "btnitmadd"
        Me.btnitmadd.Size = New System.Drawing.Size(55, 24)
        Me.btnitmadd.TabIndex = 345486
        Me.btnitmadd.Text = "Add"
        Me.btnitmadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnitmadd.UseVisualStyleBackColor = False
        '
        'lbldays
        '
        Me.lbldays.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbldays.BackColor = System.Drawing.Color.Transparent
        Me.lbldays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbldays.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lbldays.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldays.Location = New System.Drawing.Point(917, 161)
        Me.lbldays.Name = "lbldays"
        Me.lbldays.Size = New System.Drawing.Size(113, 27)
        Me.lbldays.TabIndex = 345485
        Me.lbldays.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(876, 169)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 13)
        Me.Label9.TabIndex = 345484
        Me.Label9.Text = "Days"
        '
        'lblrent
        '
        Me.lblrent.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblrent.BackColor = System.Drawing.Color.Transparent
        Me.lblrent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblrent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblrent.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblrent.Location = New System.Drawing.Point(1072, 161)
        Me.lblrent.Name = "lblrent"
        Me.lblrent.Size = New System.Drawing.Size(113, 27)
        Me.lblrent.TabIndex = 345483
        Me.lblrent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(1031, 169)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(36, 13)
        Me.Label8.TabIndex = 345482
        Me.Label8.Text = "Rent"
        '
        'btncheckout
        '
        Me.btncheckout.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btncheckout.BackColor = System.Drawing.Color.MediumSeaGreen
        Me.btncheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncheckout.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncheckout.ForeColor = System.Drawing.Color.White
        Me.btncheckout.Location = New System.Drawing.Point(192, 162)
        Me.btncheckout.Name = "btncheckout"
        Me.btncheckout.Size = New System.Drawing.Size(116, 24)
        Me.btncheckout.TabIndex = 345448
        Me.btncheckout.Text = "Check &Out"
        Me.btncheckout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncheckout.UseVisualStyleBackColor = False
        Me.btncheckout.Visible = False
        '
        'btnroomedit
        '
        Me.btnroomedit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnroomedit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnroomedit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnroomedit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnroomedit.ForeColor = System.Drawing.Color.White
        Me.btnroomedit.Location = New System.Drawing.Point(60, 162)
        Me.btnroomedit.Name = "btnroomedit"
        Me.btnroomedit.Size = New System.Drawing.Size(55, 24)
        Me.btnroomedit.TabIndex = 345447
        Me.btnroomedit.Text = "Edit"
        Me.btnroomedit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnroomedit.UseVisualStyleBackColor = False
        '
        'grdRooms
        '
        Me.grdRooms.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdRooms.BackgroundColor = System.Drawing.Color.White
        Me.grdRooms.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdRooms.Location = New System.Drawing.Point(3, 6)
        Me.grdRooms.Name = "grdRooms"
        Me.grdRooms.Size = New System.Drawing.Size(1182, 150)
        Me.grdRooms.TabIndex = 345444
        '
        'btnrem
        '
        Me.btnrem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnrem.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrem.ForeColor = System.Drawing.Color.White
        Me.btnrem.Location = New System.Drawing.Point(117, 162)
        Me.btnrem.Name = "btnrem"
        Me.btnrem.Size = New System.Drawing.Size(73, 24)
        Me.btnrem.TabIndex = 345446
        Me.btnrem.Text = "Remove"
        Me.btnrem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrem.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(6, 248)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1207, 220)
        Me.TabControl1.TabIndex = 345476
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.Label5)
        Me.TabPage6.Controls.Add(Me.lblserviceGridTotal)
        Me.TabPage6.Controls.Add(Me.btnsaveService)
        Me.TabPage6.Controls.Add(Me.chkcal)
        Me.TabPage6.Controls.Add(Me.Label6)
        Me.TabPage6.Controls.Add(Me.grdroomAdded)
        Me.TabPage6.Controls.Add(Me.Label24)
        Me.TabPage6.Controls.Add(Me.grdVoucher)
        Me.TabPage6.Controls.Add(Me.btnaddservice)
        Me.TabPage6.Controls.Add(Me.btnremoveservice)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(1199, 194)
        Me.TabPage6.TabIndex = 2
        Me.TabPage6.Text = "Services"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(912, 164)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 345483
        Me.Label5.Text = "Total"
        '
        'lblserviceGridTotal
        '
        Me.lblserviceGridTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblserviceGridTotal.BackColor = System.Drawing.Color.Transparent
        Me.lblserviceGridTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblserviceGridTotal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblserviceGridTotal.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblserviceGridTotal.Location = New System.Drawing.Point(958, 162)
        Me.lblserviceGridTotal.Name = "lblserviceGridTotal"
        Me.lblserviceGridTotal.Size = New System.Drawing.Size(142, 27)
        Me.lblserviceGridTotal.TabIndex = 345482
        Me.lblserviceGridTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnsaveService
        '
        Me.btnsaveService.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnsaveService.BackColor = System.Drawing.Color.SteelBlue
        Me.btnsaveService.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsaveService.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsaveService.ForeColor = System.Drawing.Color.White
        Me.btnsaveService.Location = New System.Drawing.Point(1106, 160)
        Me.btnsaveService.Name = "btnsaveService"
        Me.btnsaveService.Size = New System.Drawing.Size(76, 29)
        Me.btnsaveService.TabIndex = 345480
        Me.btnsaveService.Text = "&Save"
        Me.btnsaveService.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsaveService.UseVisualStyleBackColor = False
        '
        'chkcal
        '
        Me.chkcal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkcal.AutoSize = True
        Me.chkcal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkcal.Location = New System.Drawing.Point(334, 160)
        Me.chkcal.Name = "chkcal"
        Me.chkcal.Size = New System.Drawing.Size(144, 17)
        Me.chkcal.TabIndex = 345479
        Me.chkcal.Text = "Calculate Tax From Price"
        Me.chkcal.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 5)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 15)
        Me.Label6.TabIndex = 345478
        Me.Label6.Text = "Rooms Added"
        '
        'grdroomAdded
        '
        Me.grdroomAdded.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdroomAdded.BackgroundColor = System.Drawing.Color.White
        Me.grdroomAdded.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdroomAdded.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdroomAdded.Location = New System.Drawing.Point(6, 23)
        Me.grdroomAdded.Name = "grdroomAdded"
        Me.grdroomAdded.Size = New System.Drawing.Size(203, 136)
        Me.grdroomAdded.TabIndex = 345477
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(216, 5)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(53, 15)
        Me.Label24.TabIndex = 345476
        Me.Label24.Text = "Services"
        '
        'grdVoucher
        '
        Me.grdVoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdVoucher.BackgroundColor = System.Drawing.Color.White
        Me.grdVoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdVoucher.Location = New System.Drawing.Point(215, 23)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(967, 136)
        Me.grdVoucher.TabIndex = 345474
        '
        'btnaddservice
        '
        Me.btnaddservice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnaddservice.BackColor = System.Drawing.Color.SteelBlue
        Me.btnaddservice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnaddservice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnaddservice.ForeColor = System.Drawing.Color.White
        Me.btnaddservice.Location = New System.Drawing.Point(215, 160)
        Me.btnaddservice.Name = "btnaddservice"
        Me.btnaddservice.Size = New System.Drawing.Size(55, 29)
        Me.btnaddservice.TabIndex = 345473
        Me.btnaddservice.Text = "Add"
        Me.btnaddservice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnaddservice.UseVisualStyleBackColor = False
        '
        'btnremoveservice
        '
        Me.btnremoveservice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnremoveservice.BackColor = System.Drawing.Color.SteelBlue
        Me.btnremoveservice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnremoveservice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnremoveservice.ForeColor = System.Drawing.Color.White
        Me.btnremoveservice.Location = New System.Drawing.Point(273, 160)
        Me.btnremoveservice.Name = "btnremoveservice"
        Me.btnremoveservice.Size = New System.Drawing.Size(55, 29)
        Me.btnremoveservice.TabIndex = 345475
        Me.btnremoveservice.Text = "Rem"
        Me.btnremoveservice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnremoveservice.UseVisualStyleBackColor = False
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Label18)
        Me.TabPage4.Controls.Add(Me.grdrvlist)
        Me.TabPage4.Controls.Add(Me.grdinvList)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(1199, 194)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Voucher List"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(9, 83)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(50, 13)
        Me.Label18.TabIndex = 345494
        Me.Label18.Text = "RV List"
        '
        'grdrvlist
        '
        Me.grdrvlist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdrvlist.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdrvlist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdrvlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdrvlist.Location = New System.Drawing.Point(7, 100)
        Me.grdrvlist.Name = "grdrvlist"
        Me.grdrvlist.Size = New System.Drawing.Size(1186, 90)
        Me.grdrvlist.TabIndex = 345492
        '
        'grdinvList
        '
        Me.grdinvList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdinvList.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdinvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdinvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdinvList.Location = New System.Drawing.Point(7, 5)
        Me.grdinvList.Name = "grdinvList"
        Me.grdinvList.Size = New System.Drawing.Size(1186, 73)
        Me.grdinvList.TabIndex = 345491
        '
        'cmbreceipttype
        '
        Me.cmbreceipttype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbreceipttype.FormattingEnabled = True
        Me.cmbreceipttype.Items.AddRange(New Object() {"Advance Receipt", "Invoice Receipt"})
        Me.cmbreceipttype.Location = New System.Drawing.Point(1046, 191)
        Me.cmbreceipttype.Name = "cmbreceipttype"
        Me.cmbreceipttype.Size = New System.Drawing.Size(161, 21)
        Me.cmbreceipttype.TabIndex = 345496
        '
        'btnrefreshVoucher
        '
        Me.btnrefreshVoucher.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrefreshVoucher.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrefreshVoucher.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrefreshVoucher.ForeColor = System.Drawing.Color.White
        Me.btnrefreshVoucher.Location = New System.Drawing.Point(1128, 218)
        Me.btnrefreshVoucher.Name = "btnrefreshVoucher"
        Me.btnrefreshVoucher.Size = New System.Drawing.Size(79, 33)
        Me.btnrefreshVoucher.TabIndex = 345495
        Me.btnrefreshVoucher.TabStop = False
        Me.btnrefreshVoucher.Text = "Refresh"
        Me.btnrefreshVoucher.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrefreshVoucher.UseVisualStyleBackColor = False
        '
        'btncreateRV
        '
        Me.btncreateRV.BackColor = System.Drawing.Color.SteelBlue
        Me.btncreateRV.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncreateRV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncreateRV.ForeColor = System.Drawing.Color.White
        Me.btncreateRV.Location = New System.Drawing.Point(1046, 218)
        Me.btncreateRV.Name = "btncreateRV"
        Me.btncreateRV.Size = New System.Drawing.Size(79, 33)
        Me.btncreateRV.TabIndex = 345493
        Me.btncreateRV.TabStop = False
        Me.btncreateRV.Text = "Create RV"
        Me.btncreateRV.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncreateRV.UseVisualStyleBackColor = False
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
        Me.txtSeq.Location = New System.Drawing.Point(179, 440)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(374, 20)
        Me.txtSeq.TabIndex = 345480
        '
        'cmbOrder
        '
        Me.cmbOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOrder.Location = New System.Drawing.Point(7, 438)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOrder.Size = New System.Drawing.Size(166, 22)
        Me.cmbOrder.TabIndex = 345479
        Me.cmbOrder.TabStop = False
        '
        'rdoall
        '
        Me.rdoall.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdoall.AutoSize = True
        Me.rdoall.Location = New System.Drawing.Point(560, 442)
        Me.rdoall.Name = "rdoall"
        Me.rdoall.Size = New System.Drawing.Size(36, 17)
        Me.rdoall.TabIndex = 345475
        Me.rdoall.Text = "All"
        Me.rdoall.UseVisualStyleBackColor = True
        '
        'btnundo
        '
        Me.btnundo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnundo.BackColor = System.Drawing.Color.SteelBlue
        Me.btnundo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnundo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnundo.ForeColor = System.Drawing.Color.White
        Me.btnundo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnundo.Location = New System.Drawing.Point(7, 537)
        Me.btnundo.Name = "btnundo"
        Me.btnundo.Size = New System.Drawing.Size(93, 35)
        Me.btnundo.TabIndex = 345479
        Me.btnundo.Text = "Clear"
        Me.btnundo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnundo.UseVisualStyleBackColor = False
        '
        'btnload
        '
        Me.btnload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.Location = New System.Drawing.Point(1150, 436)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(63, 28)
        Me.btnload.TabIndex = 345478
        Me.btnload.Text = "Load"
        Me.btnload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnload.UseVisualStyleBackColor = False
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Panel5)
        Me.TabPage5.Controls.Add(Me.txtSeq)
        Me.TabPage5.Controls.Add(Me.cmbOrder)
        Me.TabPage5.Controls.Add(Me.btnload)
        Me.TabPage5.Controls.Add(Me.rdoall)
        Me.TabPage5.Controls.Add(Me.rdoactive)
        Me.TabPage5.Controls.Add(Me.rdoclosed)
        Me.TabPage5.Controls.Add(Me.cldrEnddate)
        Me.TabPage5.Controls.Add(Me.Label13)
        Me.TabPage5.Controls.Add(Me.cldrStartDate)
        Me.TabPage5.Controls.Add(Me.grdItem)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(1219, 468)
        Me.TabPage5.TabIndex = 1
        Me.TabPage5.Text = "Check In List"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel5.Controls.Add(Me.rdonone)
        Me.Panel5.Controls.Add(Me.rdocheckout)
        Me.Panel5.Controls.Add(Me.rdocheckin)
        Me.Panel5.Location = New System.Drawing.Point(712, 424)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(188, 43)
        Me.Panel5.TabIndex = 345481
        '
        'rdonone
        '
        Me.rdonone.AutoSize = True
        Me.rdonone.Checked = True
        Me.rdonone.Location = New System.Drawing.Point(129, 4)
        Me.rdonone.Name = "rdonone"
        Me.rdonone.Size = New System.Drawing.Size(51, 17)
        Me.rdonone.TabIndex = 345479
        Me.rdonone.TabStop = True
        Me.rdonone.Text = "None"
        Me.rdonone.UseVisualStyleBackColor = True
        '
        'rdocheckout
        '
        Me.rdocheckout.AutoSize = True
        Me.rdocheckout.Enabled = False
        Me.rdocheckout.Location = New System.Drawing.Point(3, 23)
        Me.rdocheckout.Name = "rdocheckout"
        Me.rdocheckout.Size = New System.Drawing.Size(122, 17)
        Me.rdocheckout.TabIndex = 345478
        Me.rdocheckout.Text = "With Checkout Date"
        Me.rdocheckout.UseVisualStyleBackColor = True
        '
        'rdocheckin
        '
        Me.rdocheckin.AutoSize = True
        Me.rdocheckin.Location = New System.Drawing.Point(3, 3)
        Me.rdocheckin.Name = "rdocheckin"
        Me.rdocheckin.Size = New System.Drawing.Size(119, 17)
        Me.rdocheckin.TabIndex = 345477
        Me.rdocheckin.Text = "With Check In Date"
        Me.rdocheckin.UseVisualStyleBackColor = True
        '
        'rdoactive
        '
        Me.rdoactive.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdoactive.AutoSize = True
        Me.rdoactive.Checked = True
        Me.rdoactive.Location = New System.Drawing.Point(598, 442)
        Me.rdoactive.Name = "rdoactive"
        Me.rdoactive.Size = New System.Drawing.Size(55, 17)
        Me.rdoactive.TabIndex = 345476
        Me.rdoactive.TabStop = True
        Me.rdoactive.Text = "Active"
        Me.rdoactive.UseVisualStyleBackColor = True
        '
        'rdoclosed
        '
        Me.rdoclosed.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdoclosed.AutoSize = True
        Me.rdoclosed.Location = New System.Drawing.Point(653, 442)
        Me.rdoclosed.Name = "rdoclosed"
        Me.rdoclosed.Size = New System.Drawing.Size(57, 17)
        Me.rdoclosed.TabIndex = 345477
        Me.rdoclosed.Text = "Closed"
        Me.rdoclosed.UseVisualStyleBackColor = True
        '
        'cldrEnddate
        '
        Me.cldrEnddate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cldrEnddate.CustomFormat = "dd/MMM/yyyy"
        Me.cldrEnddate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.cldrEnddate.Location = New System.Drawing.Point(1057, 442)
        Me.cldrEnddate.Name = "cldrEnddate"
        Me.cldrEnddate.Size = New System.Drawing.Size(87, 20)
        Me.cldrEnddate.TabIndex = 345457
        Me.cldrEnddate.TabStop = False
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(899, 444)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(65, 13)
        Me.Label13.TabIndex = 345456
        Me.Label13.Text = "Date Range"
        '
        'cldrStartDate
        '
        Me.cldrStartDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cldrStartDate.CustomFormat = "dd/MMM/yyyy"
        Me.cldrStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.cldrStartDate.Location = New System.Drawing.Point(964, 442)
        Me.cldrStartDate.Name = "cldrStartDate"
        Me.cldrStartDate.Size = New System.Drawing.Size(87, 20)
        Me.cldrStartDate.TabIndex = 345455
        Me.cldrStartDate.TabStop = False
        '
        'grdItem
        '
        Me.grdItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItem.BackgroundColor = System.Drawing.Color.White
        Me.grdItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItem.Location = New System.Drawing.Point(6, 6)
        Me.grdItem.Name = "grdItem"
        Me.grdItem.Size = New System.Drawing.Size(1207, 416)
        Me.grdItem.TabIndex = 345447
        '
        'chkwS
        '
        Me.chkwS.AutoSize = True
        Me.chkwS.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkwS.Location = New System.Drawing.Point(27, 37)
        Me.chkwS.Name = "chkwS"
        Me.chkwS.Size = New System.Drawing.Size(116, 17)
        Me.chkwS.TabIndex = 0
        Me.chkwS.TabStop = False
        Me.chkwS.Text = "Without Service"
        Me.chkwS.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Gray
        Me.Panel2.Location = New System.Drawing.Point(1407, 273)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(199, 24)
        Me.Panel2.TabIndex = 345468
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(798, 69)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(106, 13)
        Me.Label16.TabIndex = 345450
        Me.Label16.Text = "Service Charge"
        '
        'lbltotalRent
        '
        Me.lbltotalRent.BackColor = System.Drawing.Color.Transparent
        Me.lbltotalRent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbltotalRent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lbltotalRent.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalRent.Location = New System.Drawing.Point(906, 29)
        Me.lbltotalRent.Name = "lbltotalRent"
        Me.lbltotalRent.Size = New System.Drawing.Size(127, 32)
        Me.lbltotalRent.TabIndex = 345454
        Me.lbltotalRent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(798, 101)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(80, 13)
        Me.Label15.TabIndex = 345449
        Me.Label15.Text = "Total Value"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSlct)
        Me.GroupBox1.Controls.Add(Me.txtbookRef)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.lblgstn)
        Me.GroupBox1.Controls.Add(Me.dtpdate)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtjobcode)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtaddress)
        Me.GroupBox1.Controls.Add(Me.btnadd)
        Me.GroupBox1.Controls.Add(Me.txtcustomer)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(428, 236)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Check In Details"
        '
        'btnSlct
        '
        Me.btnSlct.BackColor = System.Drawing.SystemColors.Control
        Me.btnSlct.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnSlct.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSlct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSlct.Image = CType(resources.GetObject("btnSlct.Image"), System.Drawing.Image)
        Me.btnSlct.Location = New System.Drawing.Point(279, 192)
        Me.btnSlct.Name = "btnSlct"
        Me.btnSlct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnSlct.Size = New System.Drawing.Size(30, 26)
        Me.btnSlct.TabIndex = 345490
        Me.btnSlct.TabStop = False
        Me.btnSlct.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSlct.UseVisualStyleBackColor = False
        '
        'txtbookRef
        '
        Me.txtbookRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtbookRef.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbookRef.Location = New System.Drawing.Point(108, 195)
        Me.txtbookRef.Name = "txtbookRef"
        Me.txtbookRef.ReadOnly = True
        Me.txtbookRef.Size = New System.Drawing.Size(165, 21)
        Me.txtbookRef.TabIndex = 345488
        Me.txtbookRef.TabStop = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(6, 198)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(77, 15)
        Me.Label10.TabIndex = 345489
        Me.Label10.Text = "Booking Ref."
        '
        'lblgstn
        '
        Me.lblgstn.AutoSize = True
        Me.lblgstn.BackColor = System.Drawing.Color.Transparent
        Me.lblgstn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgstn.ForeColor = System.Drawing.Color.Green
        Me.lblgstn.Location = New System.Drawing.Point(105, 174)
        Me.lblgstn.Name = "lblgstn"
        Me.lblgstn.Size = New System.Drawing.Size(41, 13)
        Me.lblgstn.TabIndex = 345487
        Me.lblgstn.Text = "GSTN"
        Me.lblgstn.Visible = False
        '
        'dtpdate
        '
        Me.dtpdate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpdate.Location = New System.Drawing.Point(317, 21)
        Me.dtpdate.Name = "dtpdate"
        Me.dtpdate.Size = New System.Drawing.Size(91, 20)
        Me.dtpdate.TabIndex = 1
        Me.dtpdate.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(279, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 15)
        Me.Label3.TabIndex = 345367
        Me.Label3.Text = "Date"
        '
        'txtjobcode
        '
        Me.txtjobcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtjobcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtjobcode.Location = New System.Drawing.Point(108, 19)
        Me.txtjobcode.Name = "txtjobcode"
        Me.txtjobcode.Size = New System.Drawing.Size(165, 21)
        Me.txtjobcode.TabIndex = 0
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(6, 22)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(89, 15)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "Check In Code."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 15)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Customer Name"
        '
        'txtaddress
        '
        Me.txtaddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtaddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaddress.Location = New System.Drawing.Point(108, 80)
        Me.txtaddress.MaxLength = 15
        Me.txtaddress.Multiline = True
        Me.txtaddress.Name = "txtaddress"
        Me.txtaddress.ReadOnly = True
        Me.txtaddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtaddress.Size = New System.Drawing.Size(296, 88)
        Me.txtaddress.TabIndex = 33
        Me.txtaddress.TabStop = False
        '
        'btnadd
        '
        Me.btnadd.Image = Global.SMSMP.My.Resources.Resources.button_edit
        Me.btnadd.Location = New System.Drawing.Point(388, 48)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(28, 25)
        Me.btnadd.TabIndex = 32
        Me.btnadd.TabStop = False
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'txtcustomer
        '
        Me.txtcustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtcustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustomer.Location = New System.Drawing.Point(108, 51)
        Me.txtcustomer.MaxLength = 15
        Me.txtcustomer.Name = "txtcustomer"
        Me.txtcustomer.Size = New System.Drawing.Size(277, 21)
        Me.txtcustomer.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(438, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 15)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "Description"
        '
        'txtDescription
        '
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.Location = New System.Drawing.Point(539, 15)
        Me.txtDescription.MaxLength = 150
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescription.Size = New System.Drawing.Size(251, 34)
        Me.txtDescription.TabIndex = 6
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label22)
        Me.GroupBox3.Controls.Add(Me.Label20)
        Me.GroupBox3.Controls.Add(Me.Label28)
        Me.GroupBox3.Controls.Add(Me.lblinvamt)
        Me.GroupBox3.Controls.Add(Me.lblRv)
        Me.GroupBox3.Controls.Add(Me.lblbalance)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.Green
        Me.GroupBox3.Location = New System.Drawing.Point(795, 138)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(241, 126)
        Me.GroupBox3.TabIndex = 345480
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Invoice Status"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(6, 19)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(92, 16)
        Me.Label22.TabIndex = 345478
        Me.Label22.Text = "Invoice Amt."
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(6, 54)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(67, 16)
        Me.Label20.TabIndex = 345476
        Me.Label20.Text = "Received"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(6, 86)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(59, 16)
        Me.Label28.TabIndex = 345474
        Me.Label28.Text = "Balance"
        '
        'lblinvamt
        '
        Me.lblinvamt.BackColor = System.Drawing.Color.Transparent
        Me.lblinvamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblinvamt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblinvamt.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblinvamt.Location = New System.Drawing.Point(111, 16)
        Me.lblinvamt.Name = "lblinvamt"
        Me.lblinvamt.Size = New System.Drawing.Size(127, 32)
        Me.lblinvamt.TabIndex = 345479
        Me.lblinvamt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRv
        '
        Me.lblRv.BackColor = System.Drawing.Color.Transparent
        Me.lblRv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblRv.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRv.Location = New System.Drawing.Point(111, 51)
        Me.lblRv.Name = "lblRv"
        Me.lblRv.Size = New System.Drawing.Size(127, 32)
        Me.lblRv.TabIndex = 345477
        Me.lblRv.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblbalance
        '
        Me.lblbalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblbalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblbalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblbalance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbalance.Location = New System.Drawing.Point(111, 87)
        Me.lblbalance.Name = "lblbalance"
        Me.lblbalance.Size = New System.Drawing.Size(127, 32)
        Me.lblbalance.TabIndex = 345478
        Me.lblbalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpinvoice
        '
        Me.grpinvoice.Controls.Add(Me.btninvoice)
        Me.grpinvoice.Controls.Add(Me.chkconsolidateservice)
        Me.grpinvoice.Controls.Add(Me.chktotal)
        Me.grpinvoice.Controls.Add(Me.rdoserviceinv)
        Me.grpinvoice.Controls.Add(Me.rdoinvoice)
        Me.grpinvoice.Controls.Add(Me.chkwS)
        Me.grpinvoice.Enabled = False
        Me.grpinvoice.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpinvoice.ForeColor = System.Drawing.Color.Maroon
        Me.grpinvoice.Location = New System.Drawing.Point(1044, 27)
        Me.grpinvoice.Name = "grpinvoice"
        Me.grpinvoice.Size = New System.Drawing.Size(168, 159)
        Me.grpinvoice.TabIndex = 345475
        Me.grpinvoice.TabStop = False
        Me.grpinvoice.Text = "Invoice"
        '
        'chkconsolidateservice
        '
        Me.chkconsolidateservice.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkconsolidateservice.Location = New System.Drawing.Point(27, 54)
        Me.chkconsolidateservice.Name = "chkconsolidateservice"
        Me.chkconsolidateservice.Size = New System.Drawing.Size(142, 30)
        Me.chkconsolidateservice.TabIndex = 345484
        Me.chkconsolidateservice.TabStop = False
        Me.chkconsolidateservice.Text = "Service Consolidate Amt."
        Me.chkconsolidateservice.UseVisualStyleBackColor = True
        '
        'rdoinvoice
        '
        Me.rdoinvoice.AutoSize = True
        Me.rdoinvoice.Checked = True
        Me.rdoinvoice.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoinvoice.Location = New System.Drawing.Point(6, 17)
        Me.rdoinvoice.Name = "rdoinvoice"
        Me.rdoinvoice.Size = New System.Drawing.Size(67, 17)
        Me.rdoinvoice.TabIndex = 345480
        Me.rdoinvoice.TabStop = True
        Me.rdoinvoice.Text = "Invoice"
        Me.rdoinvoice.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(798, 29)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(73, 13)
        Me.Label19.TabIndex = 345453
        Me.Label19.Text = "Total Rent"
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(894, 534)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345480
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
        Me.chkSearch.Visible = False
        '
        'TabPage3
        '
        Me.TabPage3.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.TabPage3.Controls.Add(Me.txtkids)
        Me.TabPage3.Controls.Add(Me.Label7)
        Me.TabPage3.Controls.Add(Me.cmbidentityproof)
        Me.TabPage3.Controls.Add(Me.cmbreceipttype)
        Me.TabPage3.Controls.Add(Me.btnrefreshVoucher)
        Me.TabPage3.Controls.Add(Me.btnattach)
        Me.TabPage3.Controls.Add(Me.txtvechicledetails)
        Me.TabPage3.Controls.Add(Me.btncreateRV)
        Me.TabPage3.Controls.Add(Me.Label30)
        Me.TabPage3.Controls.Add(Me.txtidentityProofNumber)
        Me.TabPage3.Controls.Add(Me.Label29)
        Me.TabPage3.Controls.Add(Me.Label21)
        Me.TabPage3.Controls.Add(Me.txtfemaleGust)
        Me.TabPage3.Controls.Add(Me.grpinvoice)
        Me.TabPage3.Controls.Add(Me.Label17)
        Me.TabPage3.Controls.Add(Me.txtmaleGust)
        Me.TabPage3.Controls.Add(Me.Label12)
        Me.TabPage3.Controls.Add(Me.txtnumberofGust)
        Me.TabPage3.Controls.Add(Me.Label2)
        Me.TabPage3.Controls.Add(Me.GroupBox3)
        Me.TabPage3.Controls.Add(Me.lblserviceCharge)
        Me.TabPage3.Controls.Add(Me.Label19)
        Me.TabPage3.Controls.Add(Me.Label16)
        Me.TabPage3.Controls.Add(Me.Label1)
        Me.TabPage3.Controls.Add(Me.txtDescription)
        Me.TabPage3.Controls.Add(Me.lbltotalRent)
        Me.TabPage3.Controls.Add(Me.Label15)
        Me.TabPage3.Controls.Add(Me.GroupBox1)
        Me.TabPage3.Controls.Add(Me.lblJobvalue)
        Me.TabPage3.Controls.Add(Me.TabControl1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(1219, 468)
        Me.TabPage3.TabIndex = 0
        Me.TabPage3.Text = "General"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'txtkids
        '
        Me.txtkids.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtkids.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtkids.Location = New System.Drawing.Point(539, 102)
        Me.txtkids.Name = "txtkids"
        Me.txtkids.Size = New System.Drawing.Size(103, 21)
        Me.txtkids.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(442, 102)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 15)
        Me.Label7.TabIndex = 345498
        Me.Label7.Text = "Kids"
        '
        'cmbidentityproof
        '
        Me.cmbidentityproof.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbidentityproof.FormattingEnabled = True
        Me.cmbidentityproof.Items.AddRange(New Object() {"Driving License", "Adhar Card", "Voter ID", "Passport", "Other"})
        Me.cmbidentityproof.Location = New System.Drawing.Point(539, 153)
        Me.cmbidentityproof.Name = "cmbidentityproof"
        Me.cmbidentityproof.Size = New System.Drawing.Size(251, 21)
        Me.cmbidentityproof.TabIndex = 11
        '
        'btnattach
        '
        Me.btnattach.BackColor = System.Drawing.Color.SteelBlue
        Me.btnattach.Enabled = False
        Me.btnattach.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnattach.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnattach.ForeColor = System.Drawing.Color.White
        Me.btnattach.Location = New System.Drawing.Point(539, 229)
        Me.btnattach.Name = "btnattach"
        Me.btnattach.Size = New System.Drawing.Size(251, 33)
        Me.btnattach.TabIndex = 345494
        Me.btnattach.TabStop = False
        Me.btnattach.Text = "Attach Document"
        Me.btnattach.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnattach.UseVisualStyleBackColor = False
        '
        'txtvechicledetails
        '
        Me.txtvechicledetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtvechicledetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtvechicledetails.Location = New System.Drawing.Point(539, 203)
        Me.txtvechicledetails.Name = "txtvechicledetails"
        Me.txtvechicledetails.Size = New System.Drawing.Size(251, 21)
        Me.txtvechicledetails.TabIndex = 13
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(442, 202)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(88, 15)
        Me.Label30.TabIndex = 345492
        Me.Label30.Text = "Vehicle Details"
        '
        'txtidentityProofNumber
        '
        Me.txtidentityProofNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtidentityProofNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtidentityProofNumber.Location = New System.Drawing.Point(539, 178)
        Me.txtidentityProofNumber.Name = "txtidentityProofNumber"
        Me.txtidentityProofNumber.Size = New System.Drawing.Size(251, 21)
        Me.txtidentityProofNumber.TabIndex = 12
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(442, 178)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(93, 15)
        Me.Label29.TabIndex = 345491
        Me.Label29.Text = "Identity Number"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(442, 153)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(77, 15)
        Me.Label21.TabIndex = 345489
        Me.Label21.Text = "Identity Proof"
        '
        'txtfemaleGust
        '
        Me.txtfemaleGust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtfemaleGust.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfemaleGust.Location = New System.Drawing.Point(539, 78)
        Me.txtfemaleGust.Name = "txtfemaleGust"
        Me.txtfemaleGust.Size = New System.Drawing.Size(103, 21)
        Me.txtfemaleGust.TabIndex = 9
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(442, 78)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(52, 15)
        Me.Label17.TabIndex = 345487
        Me.Label17.Text = "Female "
        '
        'txtmaleGust
        '
        Me.txtmaleGust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtmaleGust.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmaleGust.Location = New System.Drawing.Point(539, 53)
        Me.txtmaleGust.Name = "txtmaleGust"
        Me.txtmaleGust.Size = New System.Drawing.Size(103, 21)
        Me.txtmaleGust.TabIndex = 8
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(442, 53)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(35, 15)
        Me.Label12.TabIndex = 345485
        Me.Label12.Text = "Male"
        '
        'txtnumberofGust
        '
        Me.txtnumberofGust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtnumberofGust.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnumberofGust.Location = New System.Drawing.Point(539, 126)
        Me.txtnumberofGust.Name = "txtnumberofGust"
        Me.txtnumberofGust.ReadOnly = True
        Me.txtnumberofGust.Size = New System.Drawing.Size(103, 21)
        Me.txtnumberofGust.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(442, 126)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 15)
        Me.Label2.TabIndex = 345483
        Me.Label2.Text = "Total Gusts"
        '
        'lblserviceCharge
        '
        Me.lblserviceCharge.BackColor = System.Drawing.Color.Transparent
        Me.lblserviceCharge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblserviceCharge.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblserviceCharge.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblserviceCharge.Location = New System.Drawing.Point(906, 65)
        Me.lblserviceCharge.Name = "lblserviceCharge"
        Me.lblserviceCharge.Size = New System.Drawing.Size(127, 32)
        Me.lblserviceCharge.TabIndex = 345481
        Me.lblserviceCharge.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblJobvalue
        '
        Me.lblJobvalue.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblJobvalue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblJobvalue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblJobvalue.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJobvalue.Location = New System.Drawing.Point(906, 101)
        Me.lblJobvalue.Name = "lblJobvalue"
        Me.lblJobvalue.Size = New System.Drawing.Size(127, 32)
        Me.lblJobvalue.TabIndex = 345455
        Me.lblJobvalue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkFormat
        '
        Me.chkFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(833, 544)
        Me.chkFormat.Name = "chkFormat"
        Me.chkFormat.Size = New System.Drawing.Size(106, 19)
        Me.chkFormat.TabIndex = 345474
        Me.chkFormat.Text = "Select Preview"
        Me.chkFormat.UseVisualStyleBackColor = False
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.BackColor = System.Drawing.Color.SteelBlue
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.White
        Me.btnPreview.Location = New System.Drawing.Point(735, 537)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(93, 35)
        Me.btnPreview.TabIndex = 345473
        Me.btnPreview.TabStop = False
        Me.btnPreview.Text = "Pre&view"
        Me.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(1141, 538)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(93, 35)
        Me.btnExit.TabIndex = 345471
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.BackColor = System.Drawing.Color.SteelBlue
        Me.btndelete.Enabled = False
        Me.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.ForeColor = System.Drawing.Color.White
        Me.btndelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btndelete.Location = New System.Drawing.Point(103, 537)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(93, 35)
        Me.btndelete.TabIndex = 345470
        Me.btndelete.Text = "&Delete"
        Me.btndelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndelete.UseVisualStyleBackColor = False
        '
        'btnupdate
        '
        Me.btnupdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnupdate.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupdate.Enabled = False
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnupdate.Location = New System.Drawing.Point(1043, 537)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(93, 35)
        Me.btnupdate.TabIndex = 14
        Me.btnupdate.Text = "Update [F8]"
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(41, 5)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(80, 20)
        Me.Label26.TabIndex = 345458
        Me.Label26.Text = "Check In"
        '
        'TabControl2
        '
        Me.TabControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl2.Controls.Add(Me.TabPage3)
        Me.TabControl2.Controls.Add(Me.TabPage5)
        Me.TabControl2.Location = New System.Drawing.Point(7, 38)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(1227, 494)
        Me.TabControl2.TabIndex = 345477
        '
        'txtprintjob
        '
        Me.txtprintjob.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtprintjob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtprintjob.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprintjob.Location = New System.Drawing.Point(665, 543)
        Me.txtprintjob.Name = "txtprintjob"
        Me.txtprintjob.Size = New System.Drawing.Size(64, 21)
        Me.txtprintjob.TabIndex = 345475
        '
        'Label27
        '
        Me.Label27.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(578, 545)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(90, 15)
        Me.Label27.TabIndex = 345476
        Me.Label27.Text = "Print Job Code."
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label26)
        Me.Panel4.Controls.Add(Me.PictureBox1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1244, 32)
        Me.Panel4.TabIndex = 345472
        '
        'Timer1
        '
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
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.picCloseProd)
        Me.Panel3.Controls.Add(Me.PictureBox2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(477, 32)
        Me.Panel3.TabIndex = 345445
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.White
        Me.Label11.Font = New System.Drawing.Font("Goudy Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(31, 5)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(91, 21)
        Me.Label11.TabIndex = 27
        Me.Label11.Text = "Select Item.."
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
        'plsrch
        '
        Me.plsrch.Controls.Add(Me.Panel3)
        Me.plsrch.Controls.Add(Me.grdSrch)
        Me.plsrch.Location = New System.Drawing.Point(412, 230)
        Me.plsrch.Name = "plsrch"
        Me.plsrch.Size = New System.Drawing.Size(477, 264)
        Me.plsrch.TabIndex = 345478
        Me.plsrch.Visible = False
        '
        'Timer2
        '
        '
        'lblcreatedinfo
        '
        Me.lblcreatedinfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblcreatedinfo.AutoSize = True
        Me.lblcreatedinfo.BackColor = System.Drawing.Color.Transparent
        Me.lblcreatedinfo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcreatedinfo.Location = New System.Drawing.Point(210, 538)
        Me.lblcreatedinfo.Name = "lblcreatedinfo"
        Me.lblcreatedinfo.Size = New System.Drawing.Size(86, 13)
        Me.lblcreatedinfo.TabIndex = 345481
        Me.lblcreatedinfo.Text = "Created on :"
        '
        'LodgeCheckInFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1244, 575)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblcreatedinfo)
        Me.Controls.Add(Me.plsrch)
        Me.Controls.Add(Me.btnundo)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.chkFormat)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.btnupdate)
        Me.Controls.Add(Me.TabControl2)
        Me.Controls.Add(Me.txtprintjob)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "LodgeCheckInFrm"
        Me.Text = "Check in"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.grdRooms, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage6.PerformLayout()
        CType(Me.grdroomAdded, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        CType(Me.grdrvlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdinvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.grpinvoice.ResumeLayout(False)
        Me.grpinvoice.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabControl2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.grdSrch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.picCloseProd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plsrch.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chktotal As System.Windows.Forms.CheckBox
    Friend WithEvents btninvoice As System.Windows.Forms.Button
    Friend WithEvents rdoserviceinv As System.Windows.Forms.RadioButton
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents grdRooms As System.Windows.Forms.DataGridView
    Friend WithEvents btnrem As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents rdoall As System.Windows.Forms.RadioButton
    Friend WithEvents btnundo As System.Windows.Forms.Button
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents rdoactive As System.Windows.Forms.RadioButton
    Friend WithEvents rdoclosed As System.Windows.Forms.RadioButton
    Friend WithEvents cldrEnddate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents grdItem As System.Windows.Forms.DataGridView
    Friend WithEvents chkwS As System.Windows.Forms.CheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lbltotalRent As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtjobcode As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblRv As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lblbalance As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lblinvamt As System.Windows.Forms.Label
    Friend WithEvents grpinvoice As System.Windows.Forms.GroupBox
    Friend WithEvents rdoinvoice As System.Windows.Forms.RadioButton
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents lblJobvalue As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtcustomer As System.Windows.Forms.TextBox
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents txtaddress As System.Windows.Forms.TextBox
    Friend WithEvents chkFormat As System.Windows.Forms.CheckBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btndelete As System.Windows.Forms.Button
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents txtprintjob As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents lblserviceCharge As System.Windows.Forms.Label
    Friend WithEvents txtvechicledetails As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtidentityProofNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtfemaleGust As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtmaleGust As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtnumberofGust As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnattach As System.Windows.Forms.Button
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents grdVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents btnaddservice As System.Windows.Forms.Button
    Friend WithEvents btnremoveservice As System.Windows.Forms.Button
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents btnrefreshVoucher As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btncreateRV As System.Windows.Forms.Button
    Friend WithEvents grdrvlist As System.Windows.Forms.DataGridView
    Friend WithEvents grdinvList As System.Windows.Forms.DataGridView
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents grdroomAdded As System.Windows.Forms.DataGridView
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents grdSrch As System.Windows.Forms.DataGridView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents picCloseProd As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents plsrch As System.Windows.Forms.Panel
    Friend WithEvents btnroomedit As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents rdocheckout As System.Windows.Forms.RadioButton
    Friend WithEvents rdocheckin As System.Windows.Forms.RadioButton
    Friend WithEvents rdonone As System.Windows.Forms.RadioButton
    Friend WithEvents chkcal As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblserviceGridTotal As System.Windows.Forms.Label
    Friend WithEvents btnsaveService As System.Windows.Forms.Button
    Friend WithEvents cmbreceipttype As System.Windows.Forms.ComboBox
    Friend WithEvents btncheckout As System.Windows.Forms.Button
    Friend WithEvents lbldays As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblrent As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents chkconsolidateservice As System.Windows.Forms.CheckBox
    Friend WithEvents btnitmadd As System.Windows.Forms.Button
    Friend WithEvents cmbidentityproof As System.Windows.Forms.ComboBox
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents txtkids As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblcreatedinfo As System.Windows.Forms.Label
    Friend WithEvents lblgstn As System.Windows.Forms.Label
    Friend WithEvents txtbookRef As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents btnSlct As System.Windows.Forms.Button
End Class
