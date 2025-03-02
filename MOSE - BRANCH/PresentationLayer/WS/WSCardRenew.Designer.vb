<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WSCardRenew
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtledger = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtaddress = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtcustomer = New System.Windows.Forms.TextBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.lblcap = New System.Windows.Forms.Label
        Me.txtprefix = New System.Windows.Forms.TextBox
        Me.cldrdate = New System.Windows.Forms.DateTimePicker
        Me.numVchrNo = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtReference = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.cmbcard = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.btnprint = New System.Windows.Forms.Button
        Me.txtPPrefix = New System.Windows.Forms.TextBox
        Me.numPrintVchr = New System.Windows.Forms.TextBox
        Me.btnclear = New System.Windows.Forms.Button
        Me.btnupdate = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtAmount = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtdebit = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtcredit = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.chkcredit = New System.Windows.Forms.CheckBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtgst = New System.Windows.Forms.TextBox
        Me.txtnetamount = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtdiscount = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblRservice = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.lbllastplatenumber = New System.Windows.Forms.Label
        Me.lbllastservicedate = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblservice = New System.Windows.Forms.Label
        Me.lblhsncode = New System.Windows.Forms.Label
        Me.lblgstper = New System.Windows.Forms.Label
        Me.lblcardtype = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.txtcustomer)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtaddress)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 71)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(395, 158)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Customer Details"
        '
        'txtledger
        '
        Me.txtledger.BackColor = System.Drawing.Color.White
        Me.txtledger.Location = New System.Drawing.Point(75, 49)
        Me.txtledger.MaxLength = 50
        Me.txtledger.Name = "txtledger"
        Me.txtledger.ReadOnly = True
        Me.txtledger.Size = New System.Drawing.Size(308, 20)
        Me.txtledger.TabIndex = 345470
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(6, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 15)
        Me.Label2.TabIndex = 345469
        Me.Label2.Text = "Address"
        '
        'txtaddress
        '
        Me.txtaddress.Location = New System.Drawing.Point(90, 59)
        Me.txtaddress.MaxLength = 50
        Me.txtaddress.Multiline = True
        Me.txtaddress.Name = "txtaddress"
        Me.txtaddress.ReadOnly = True
        Me.txtaddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtaddress.Size = New System.Drawing.Size(293, 94)
        Me.txtaddress.TabIndex = 345468
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(6, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 15)
        Me.Label1.TabIndex = 345467
        Me.Label1.Text = "Customer (F2)"
        '
        'txtcustomer
        '
        Me.txtcustomer.BackColor = System.Drawing.Color.White
        Me.txtcustomer.Location = New System.Drawing.Point(90, 32)
        Me.txtcustomer.MaxLength = 50
        Me.txtcustomer.Name = "txtcustomer"
        Me.txtcustomer.ReadOnly = True
        Me.txtcustomer.Size = New System.Drawing.Size(293, 20)
        Me.txtcustomer.TabIndex = 345466
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.White
        Me.Panel4.Controls.Add(Me.PictureBox2)
        Me.Panel4.Controls.Add(Me.lblcap)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(851, 33)
        Me.Panel4.TabIndex = 345456
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(2, 8)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(37, 22)
        Me.PictureBox2.TabIndex = 345459
        Me.PictureBox2.TabStop = False
        '
        'lblcap
        '
        Me.lblcap.AutoSize = True
        Me.lblcap.BackColor = System.Drawing.Color.Transparent
        Me.lblcap.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcap.ForeColor = System.Drawing.Color.Black
        Me.lblcap.Location = New System.Drawing.Point(41, 9)
        Me.lblcap.Name = "lblcap"
        Me.lblcap.Size = New System.Drawing.Size(164, 20)
        Me.lblcap.TabIndex = 6
        Me.lblcap.Text = "Discount Card Renew"
        '
        'txtprefix
        '
        Me.txtprefix.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprefix.Location = New System.Drawing.Point(98, 43)
        Me.txtprefix.MaxLength = 15
        Me.txtprefix.Name = "txtprefix"
        Me.txtprefix.Size = New System.Drawing.Size(59, 21)
        Me.txtprefix.TabIndex = 345465
        '
        'cldrdate
        '
        Me.cldrdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrdate.Location = New System.Drawing.Point(513, 45)
        Me.cldrdate.Name = "cldrdate"
        Me.cldrdate.Size = New System.Drawing.Size(106, 20)
        Me.cldrdate.TabIndex = 345460
        '
        'numVchrNo
        '
        Me.numVchrNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numVchrNo.Location = New System.Drawing.Point(160, 43)
        Me.numVchrNo.Name = "numVchrNo"
        Me.numVchrNo.Size = New System.Drawing.Size(76, 21)
        Me.numVchrNo.TabIndex = 345458
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(18, 45)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 15)
        Me.Label14.TabIndex = 345461
        Me.Label14.Text = "Voucher No."
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(456, 45)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 15)
        Me.Label8.TabIndex = 345463
        Me.Label8.Text = "Vr.Date"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(242, 43)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 15)
        Me.Label9.TabIndex = 345462
        Me.Label9.Text = "Reference"
        '
        'txtReference
        '
        Me.txtReference.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReference.Location = New System.Drawing.Point(312, 44)
        Me.txtReference.MaxLength = 50
        Me.txtReference.Name = "txtReference"
        Me.txtReference.Size = New System.Drawing.Size(141, 21)
        Me.txtReference.TabIndex = 345459
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(19, 254)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(81, 15)
        Me.Label10.TabIndex = 345469
        Me.Label10.Text = "Card Number"
        '
        'cmbcard
        '
        Me.cmbcard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcard.FormattingEnabled = True
        Me.cmbcard.Location = New System.Drawing.Point(118, 251)
        Me.cmbcard.Name = "cmbcard"
        Me.cmbcard.Size = New System.Drawing.Size(289, 21)
        Me.cmbcard.TabIndex = 345468
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(452, 178)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(85, 15)
        Me.Label11.TabIndex = 345471
        Me.Label11.Text = "No of Services"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.Controls.Add(Me.btnprint)
        Me.Panel7.Controls.Add(Me.txtPPrefix)
        Me.Panel7.Controls.Add(Me.numPrintVchr)
        Me.Panel7.Controls.Add(Me.btnclear)
        Me.Panel7.Controls.Add(Me.btnupdate)
        Me.Panel7.Controls.Add(Me.btnExit)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel7.Location = New System.Drawing.Point(0, 501)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(851, 38)
        Me.Panel7.TabIndex = 345472
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnprint.BackColor = System.Drawing.Color.SteelBlue
        Me.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.ForeColor = System.Drawing.Color.White
        Me.btnprint.Location = New System.Drawing.Point(205, 2)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(100, 33)
        Me.btnprint.TabIndex = 345501
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
        Me.txtPPrefix.Location = New System.Drawing.Point(102, 3)
        Me.txtPPrefix.Name = "txtPPrefix"
        Me.txtPPrefix.Size = New System.Drawing.Size(37, 21)
        Me.txtPPrefix.TabIndex = 345455
        '
        'numPrintVchr
        '
        Me.numPrintVchr.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numPrintVchr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPrintVchr.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numPrintVchr.Location = New System.Drawing.Point(140, 3)
        Me.numPrintVchr.Name = "numPrintVchr"
        Me.numPrintVchr.Size = New System.Drawing.Size(61, 21)
        Me.numPrintVchr.TabIndex = 345454
        '
        'btnclear
        '
        Me.btnclear.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.ForeColor = System.Drawing.Color.White
        Me.btnclear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnclear.Location = New System.Drawing.Point(3, 2)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(93, 33)
        Me.btnclear.TabIndex = 81
        Me.btnclear.Text = "Clear"
        Me.btnclear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclear.UseVisualStyleBackColor = False
        '
        'btnupdate
        '
        Me.btnupdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnupdate.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnupdate.Location = New System.Drawing.Point(659, 2)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(93, 33)
        Me.btnupdate.TabIndex = 2
        Me.btnupdate.Text = "&Update"
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(755, 2)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(93, 33)
        Me.btnExit.TabIndex = 75
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
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
        Me.grdVoucher.Location = New System.Drawing.Point(21, 322)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(386, 175)
        Me.grdVoucher.TabIndex = 345473
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(19, 302)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 15)
        Me.Label12.TabIndex = 345474
        Me.Label12.Text = "Services"
        '
        'txtAmount
        '
        Me.txtAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.Location = New System.Drawing.Point(694, 362)
        Me.txtAmount.MaxLength = 50
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ReadOnly = True
        Me.txtAmount.Size = New System.Drawing.Size(133, 26)
        Me.txtAmount.TabIndex = 345477
        Me.txtAmount.Text = "0.00"
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(450, 362)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 15)
        Me.Label7.TabIndex = 345478
        Me.Label7.Text = "Amount"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(6, 23)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(57, 15)
        Me.Label13.TabIndex = 345480
        Me.Label13.Text = "Debit A/C"
        '
        'txtdebit
        '
        Me.txtdebit.BackColor = System.Drawing.Color.White
        Me.txtdebit.Location = New System.Drawing.Point(75, 23)
        Me.txtdebit.MaxLength = 50
        Me.txtdebit.Name = "txtdebit"
        Me.txtdebit.ReadOnly = True
        Me.txtdebit.Size = New System.Drawing.Size(308, 20)
        Me.txtdebit.TabIndex = 345479
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(6, 50)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(60, 15)
        Me.Label15.TabIndex = 345482
        Me.Label15.Text = "Credit A/C"
        '
        'txtcredit
        '
        Me.txtcredit.BackColor = System.Drawing.Color.White
        Me.txtcredit.Location = New System.Drawing.Point(513, 246)
        Me.txtcredit.MaxLength = 50
        Me.txtcredit.Name = "txtcredit"
        Me.txtcredit.ReadOnly = True
        Me.txtcredit.Size = New System.Drawing.Size(63, 20)
        Me.txtcredit.TabIndex = 345481
        Me.txtcredit.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.txtledger)
        Me.GroupBox3.Controls.Add(Me.txtdebit)
        Me.GroupBox3.Location = New System.Drawing.Point(444, 272)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(395, 80)
        Me.GroupBox3.TabIndex = 345483
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Ledger Entry"
        '
        'chkcredit
        '
        Me.chkcredit.AutoSize = True
        Me.chkcredit.BackColor = System.Drawing.Color.Transparent
        Me.chkcredit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkcredit.ForeColor = System.Drawing.Color.Maroon
        Me.chkcredit.Location = New System.Drawing.Point(735, 42)
        Me.chkcredit.Name = "chkcredit"
        Me.chkcredit.Size = New System.Drawing.Size(104, 20)
        Me.chkcredit.TabIndex = 345484
        Me.chkcredit.Text = "Credit Sale"
        Me.chkcredit.UseVisualStyleBackColor = False
        Me.chkcredit.Visible = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(450, 240)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(31, 15)
        Me.Label16.TabIndex = 345485
        Me.Label16.Text = "GST"
        Me.Label16.Visible = False
        '
        'txtgst
        '
        Me.txtgst.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtgst.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtgst.Location = New System.Drawing.Point(694, 240)
        Me.txtgst.MaxLength = 50
        Me.txtgst.Name = "txtgst"
        Me.txtgst.ReadOnly = True
        Me.txtgst.Size = New System.Drawing.Size(133, 26)
        Me.txtgst.TabIndex = 345486
        Me.txtgst.Text = "0.00"
        Me.txtgst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtgst.Visible = False
        '
        'txtnetamount
        '
        Me.txtnetamount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtnetamount.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnetamount.Location = New System.Drawing.Point(694, 449)
        Me.txtnetamount.MaxLength = 50
        Me.txtnetamount.Name = "txtnetamount"
        Me.txtnetamount.ReadOnly = True
        Me.txtnetamount.Size = New System.Drawing.Size(133, 29)
        Me.txtnetamount.TabIndex = 345487
        Me.txtnetamount.Text = "0.00"
        Me.txtnetamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(450, 449)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(86, 15)
        Me.Label17.TabIndex = 345488
        Me.Label17.Text = "NET Amount"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(452, 203)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(65, 15)
        Me.Label18.TabIndex = 345489
        Me.Label18.Text = "HSN Code"
        '
        'txtdiscount
        '
        Me.txtdiscount.BackColor = System.Drawing.Color.White
        Me.txtdiscount.Location = New System.Drawing.Point(694, 393)
        Me.txtdiscount.MaxLength = 50
        Me.txtdiscount.Name = "txtdiscount"
        Me.txtdiscount.ReadOnly = True
        Me.txtdiscount.Size = New System.Drawing.Size(133, 20)
        Me.txtdiscount.TabIndex = 345491
        Me.txtdiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(450, 393)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 15)
        Me.Label4.TabIndex = 345492
        Me.Label4.Text = "Discount"
        '
        'lblRservice
        '
        Me.lblRservice.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblRservice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRservice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRservice.ForeColor = System.Drawing.Color.Maroon
        Me.lblRservice.Location = New System.Drawing.Point(592, 103)
        Me.lblRservice.Name = "lblRservice"
        Me.lblRservice.Size = New System.Drawing.Size(123, 21)
        Me.lblRservice.TabIndex = 345510
        Me.lblRservice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(452, 103)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(135, 15)
        Me.Label36.TabIndex = 345509
        Me.Label36.Text = "Remaining Services"
        '
        'lbllastplatenumber
        '
        Me.lbllastplatenumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lbllastplatenumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbllastplatenumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbllastplatenumber.ForeColor = System.Drawing.Color.Maroon
        Me.lbllastplatenumber.Location = New System.Drawing.Point(592, 153)
        Me.lbllastplatenumber.Name = "lbllastplatenumber"
        Me.lbllastplatenumber.Size = New System.Drawing.Size(123, 21)
        Me.lbllastplatenumber.TabIndex = 345514
        Me.lbllastplatenumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbllastservicedate
        '
        Me.lbllastservicedate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lbllastservicedate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbllastservicedate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbllastservicedate.ForeColor = System.Drawing.Color.Maroon
        Me.lbllastservicedate.Location = New System.Drawing.Point(592, 128)
        Me.lbllastservicedate.Name = "lbllastservicedate"
        Me.lbllastservicedate.Size = New System.Drawing.Size(123, 21)
        Me.lbllastservicedate.TabIndex = 345513
        Me.lbllastservicedate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(452, 153)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(57, 15)
        Me.Label38.TabIndex = 345512
        Me.Label38.Text = "Plate No."
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(452, 128)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(59, 15)
        Me.Label37.TabIndex = 345511
        Me.Label37.Text = "Last Date"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(19, 280)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 15)
        Me.Label5.TabIndex = 345515
        Me.Label5.Text = "Card Type"
        '
        'lblservice
        '
        Me.lblservice.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblservice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblservice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblservice.ForeColor = System.Drawing.Color.Maroon
        Me.lblservice.Location = New System.Drawing.Point(592, 178)
        Me.lblservice.Name = "lblservice"
        Me.lblservice.Size = New System.Drawing.Size(123, 21)
        Me.lblservice.TabIndex = 345517
        Me.lblservice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblhsncode
        '
        Me.lblhsncode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblhsncode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblhsncode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblhsncode.ForeColor = System.Drawing.Color.Maroon
        Me.lblhsncode.Location = New System.Drawing.Point(592, 203)
        Me.lblhsncode.Name = "lblhsncode"
        Me.lblhsncode.Size = New System.Drawing.Size(123, 21)
        Me.lblhsncode.TabIndex = 345518
        Me.lblhsncode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblgstper
        '
        Me.lblgstper.AutoSize = True
        Me.lblgstper.BackColor = System.Drawing.Color.Transparent
        Me.lblgstper.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lblgstper.Location = New System.Drawing.Point(657, 246)
        Me.lblgstper.Name = "lblgstper"
        Me.lblgstper.Size = New System.Drawing.Size(31, 15)
        Me.lblgstper.TabIndex = 345519
        Me.lblgstper.Text = "0.00"
        '
        'lblcardtype
        '
        Me.lblcardtype.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblcardtype.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblcardtype.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcardtype.ForeColor = System.Drawing.Color.Maroon
        Me.lblcardtype.Location = New System.Drawing.Point(118, 280)
        Me.lblcardtype.Name = "lblcardtype"
        Me.lblcardtype.Size = New System.Drawing.Size(289, 21)
        Me.lblcardtype.TabIndex = 345516
        Me.lblcardtype.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WSCardRenew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(851, 539)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblgstper)
        Me.Controls.Add(Me.lblhsncode)
        Me.Controls.Add(Me.lblservice)
        Me.Controls.Add(Me.txtcredit)
        Me.Controls.Add(Me.lblcardtype)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lbllastplatenumber)
        Me.Controls.Add(Me.lbllastservicedate)
        Me.Controls.Add(Me.Label38)
        Me.Controls.Add(Me.Label37)
        Me.Controls.Add(Me.lblRservice)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtdiscount)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtnetamount)
        Me.Controls.Add(Me.txtgst)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.chkcredit)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.grdVoucher)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cmbcard)
        Me.Controls.Add(Me.txtprefix)
        Me.Controls.Add(Me.cldrdate)
        Me.Controls.Add(Me.numVchrNo)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtReference)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "WSCardRenew"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Card Sale"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents lblcap As System.Windows.Forms.Label
    Friend WithEvents txtledger As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtaddress As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtcustomer As System.Windows.Forms.TextBox
    Friend WithEvents txtprefix As System.Windows.Forms.TextBox
    Friend WithEvents cldrdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents numVchrNo As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtReference As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbcard As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents grdVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtPPrefix As System.Windows.Forms.TextBox
    Friend WithEvents numPrintVchr As System.Windows.Forms.TextBox
    Friend WithEvents btnprint As System.Windows.Forms.Button
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtdebit As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtcredit As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkcredit As System.Windows.Forms.CheckBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtgst As System.Windows.Forms.TextBox
    Friend WithEvents txtnetamount As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtdiscount As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblRservice As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents lbllastplatenumber As System.Windows.Forms.Label
    Friend WithEvents lbllastservicedate As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblservice As System.Windows.Forms.Label
    Friend WithEvents lblhsncode As System.Windows.Forms.Label
    Friend WithEvents lblgstper As System.Windows.Forms.Label
    Friend WithEvents lblcardtype As System.Windows.Forms.Label
End Class
