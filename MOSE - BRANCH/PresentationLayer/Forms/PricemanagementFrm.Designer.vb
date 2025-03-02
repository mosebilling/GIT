<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PricemanagementFrm
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
        Me.grdPack = New System.Windows.Forms.DataGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkcal = New System.Windows.Forms.CheckBox
        Me.rdomrpdisc = New System.Windows.Forms.RadioButton
        Me.btnset = New System.Windows.Forms.Button
        Me.chktag = New System.Windows.Forms.CheckBox
        Me.txtper = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.rdowprice = New System.Windows.Forms.RadioButton
        Me.rdoprice = New System.Windows.Forms.RadioButton
        Me.plWS = New System.Windows.Forms.Panel
        Me.rdoFromMRP = New System.Windows.Forms.RadioButton
        Me.rdofromPrice = New System.Windows.Forms.RadioButton
        Me.btnexit = New System.Windows.Forms.Button
        Me.chkenter = New System.Windows.Forms.CheckBox
        Me.btnupdate = New System.Windows.Forms.Button
        Me.btnrefresh = New System.Windows.Forms.Button
        Me.btnlevel = New System.Windows.Forms.Button
        Me.btnclear = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnsearch = New System.Windows.Forms.Button
        Me.lblstatus = New System.Windows.Forms.Label
        Me.btndescription = New System.Windows.Forms.Button
        Me.btnonline = New System.Windows.Forms.Button
        Me.btnimage = New System.Windows.Forms.Button
        Me.chkupdateimage = New System.Windows.Forms.CheckBox
        Me.chksort = New System.Windows.Forms.CheckBox
        Me.btnbarcode = New System.Windows.Forms.Button
        Me.btndelete = New System.Windows.Forms.Button
        Me.btntaxtoprice = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnloaddecimal = New System.Windows.Forms.Button
        Me.rdop4 = New System.Windows.Forms.RadioButton
        Me.rdop3 = New System.Windows.Forms.RadioButton
        Me.rdop2 = New System.Windows.Forms.RadioButton
        Me.Button1 = New System.Windows.Forms.Button
        Me.rdop1 = New System.Windows.Forms.RadioButton
        CType(Me.grdPack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.plWS.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdPack
        '
        Me.grdPack.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdPack.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdPack.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdPack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPack.Location = New System.Drawing.Point(3, 35)
        Me.grdPack.Name = "grdPack"
        Me.grdPack.Size = New System.Drawing.Size(1236, 317)
        Me.grdPack.TabIndex = 345407
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1243, 32)
        Me.Panel1.TabIndex = 345464
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(2, 4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(40, 21)
        Me.PictureBox2.TabIndex = 345459
        Me.PictureBox2.TabStop = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(41, 5)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(183, 18)
        Me.Label26.TabIndex = 345458
        Me.Label26.Text = "PRICE MANAGEMENT"
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
        Me.txtSeq.Location = New System.Drawing.Point(174, 358)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(274, 20)
        Me.txtSeq.TabIndex = 345466
        '
        'cmbOrder
        '
        Me.cmbOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOrder.Location = New System.Drawing.Point(2, 356)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOrder.Size = New System.Drawing.Size(166, 22)
        Me.cmbOrder.TabIndex = 345465
        Me.cmbOrder.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.chkcal)
        Me.GroupBox1.Controls.Add(Me.rdomrpdisc)
        Me.GroupBox1.Controls.Add(Me.btnset)
        Me.GroupBox1.Controls.Add(Me.chktag)
        Me.GroupBox1.Controls.Add(Me.txtper)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.rdowprice)
        Me.GroupBox1.Controls.Add(Me.rdoprice)
        Me.GroupBox1.Controls.Add(Me.plWS)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 379)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(623, 61)
        Me.GroupBox1.TabIndex = 345467
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Options"
        '
        'chkcal
        '
        Me.chkcal.AutoSize = True
        Me.chkcal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkcal.Location = New System.Drawing.Point(9, 38)
        Me.chkcal.Name = "chkcal"
        Me.chkcal.Size = New System.Drawing.Size(144, 17)
        Me.chkcal.TabIndex = 345476
        Me.chkcal.Text = "Calculate Tax From Price"
        Me.chkcal.UseVisualStyleBackColor = True
        '
        'rdomrpdisc
        '
        Me.rdomrpdisc.AutoSize = True
        Me.rdomrpdisc.Location = New System.Drawing.Point(130, 19)
        Me.rdomrpdisc.Name = "rdomrpdisc"
        Me.rdomrpdisc.Size = New System.Drawing.Size(147, 17)
        Me.rdomrpdisc.TabIndex = 345470
        Me.rdomrpdisc.TabStop = True
        Me.rdomrpdisc.Text = "Price Discount From MRP"
        Me.rdomrpdisc.UseVisualStyleBackColor = True
        '
        'btnset
        '
        Me.btnset.BackColor = System.Drawing.Color.SteelBlue
        Me.btnset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnset.ForeColor = System.Drawing.Color.White
        Me.btnset.Location = New System.Drawing.Point(555, 14)
        Me.btnset.Name = "btnset"
        Me.btnset.Size = New System.Drawing.Size(64, 27)
        Me.btnset.TabIndex = 345469
        Me.btnset.Text = "Set"
        Me.btnset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnset.UseVisualStyleBackColor = False
        '
        'chktag
        '
        Me.chktag.AutoSize = True
        Me.chktag.BackColor = System.Drawing.Color.Transparent
        Me.chktag.ForeColor = System.Drawing.Color.Black
        Me.chktag.Location = New System.Drawing.Point(454, 19)
        Me.chktag.Name = "chktag"
        Me.chktag.Size = New System.Drawing.Size(96, 17)
        Me.chktag.TabIndex = 345468
        Me.chktag.Text = "Only for Taged"
        Me.chktag.UseVisualStyleBackColor = False
        '
        'txtper
        '
        Me.txtper.AcceptsReturn = True
        Me.txtper.BackColor = System.Drawing.SystemColors.Window
        Me.txtper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtper.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtper.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtper.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtper.Location = New System.Drawing.Point(394, 19)
        Me.txtper.MaxLength = 500
        Me.txtper.Name = "txtper"
        Me.txtper.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtper.Size = New System.Drawing.Size(54, 20)
        Me.txtper.TabIndex = 345467
        Me.txtper.Text = "0.00"
        Me.txtper.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(380, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(15, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "%"
        '
        'rdowprice
        '
        Me.rdowprice.AutoSize = True
        Me.rdowprice.Location = New System.Drawing.Point(286, 19)
        Me.rdowprice.Name = "rdowprice"
        Me.rdowprice.Size = New System.Drawing.Size(66, 17)
        Me.rdowprice.TabIndex = 1
        Me.rdowprice.TabStop = True
        Me.rdowprice.Text = "W. Price"
        Me.rdowprice.UseVisualStyleBackColor = True
        '
        'rdoprice
        '
        Me.rdoprice.AutoSize = True
        Me.rdoprice.Checked = True
        Me.rdoprice.Location = New System.Drawing.Point(9, 18)
        Me.rdoprice.Name = "rdoprice"
        Me.rdoprice.Size = New System.Drawing.Size(121, 17)
        Me.rdoprice.TabIndex = 0
        Me.rdoprice.TabStop = True
        Me.rdoprice.Text = "Price From Cost Avg"
        Me.rdoprice.UseVisualStyleBackColor = True
        '
        'plWS
        '
        Me.plWS.BackColor = System.Drawing.Color.Transparent
        Me.plWS.Controls.Add(Me.rdoFromMRP)
        Me.plWS.Controls.Add(Me.rdofromPrice)
        Me.plWS.Location = New System.Drawing.Point(284, 37)
        Me.plWS.Name = "plWS"
        Me.plWS.Size = New System.Drawing.Size(161, 25)
        Me.plWS.TabIndex = 345476
        Me.plWS.Visible = False
        '
        'rdoFromMRP
        '
        Me.rdoFromMRP.AutoSize = True
        Me.rdoFromMRP.Location = New System.Drawing.Point(76, 4)
        Me.rdoFromMRP.Name = "rdoFromMRP"
        Me.rdoFromMRP.Size = New System.Drawing.Size(75, 17)
        Me.rdoFromMRP.TabIndex = 3
        Me.rdoFromMRP.Text = "From MRP"
        Me.rdoFromMRP.UseVisualStyleBackColor = True
        '
        'rdofromPrice
        '
        Me.rdofromPrice.AutoSize = True
        Me.rdofromPrice.Checked = True
        Me.rdofromPrice.Location = New System.Drawing.Point(2, 3)
        Me.rdofromPrice.Name = "rdofromPrice"
        Me.rdofromPrice.Size = New System.Drawing.Size(75, 17)
        Me.rdofromPrice.TabIndex = 2
        Me.rdofromPrice.TabStop = True
        Me.rdofromPrice.Text = "From Price"
        Me.rdofromPrice.UseVisualStyleBackColor = True
        '
        'btnexit
        '
        Me.btnexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.Location = New System.Drawing.Point(1138, 424)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(98, 35)
        Me.btnexit.TabIndex = 345468
        Me.btnexit.Text = "E&xit"
        Me.btnexit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'chkenter
        '
        Me.chkenter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkenter.AutoSize = True
        Me.chkenter.BackColor = System.Drawing.Color.Transparent
        Me.chkenter.ForeColor = System.Drawing.Color.Black
        Me.chkenter.Location = New System.Drawing.Point(12, 442)
        Me.chkenter.Name = "chkenter"
        Me.chkenter.Size = New System.Drawing.Size(139, 17)
        Me.chkenter.TabIndex = 345469
        Me.chkenter.Text = "Go Down on Key 'Enter'"
        Me.chkenter.UseVisualStyleBackColor = False
        '
        'btnupdate
        '
        Me.btnupdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnupdate.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.Location = New System.Drawing.Point(1038, 424)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(98, 35)
        Me.btnupdate.TabIndex = 345470
        Me.btnupdate.Text = "&Update"
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'btnrefresh
        '
        Me.btnrefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnrefresh.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrefresh.ForeColor = System.Drawing.Color.White
        Me.btnrefresh.Location = New System.Drawing.Point(946, 424)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(90, 35)
        Me.btnrefresh.TabIndex = 345471
        Me.btnrefresh.Text = "Refresh"
        Me.btnrefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrefresh.UseVisualStyleBackColor = False
        '
        'btnlevel
        '
        Me.btnlevel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnlevel.BackColor = System.Drawing.Color.SteelBlue
        Me.btnlevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnlevel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnlevel.ForeColor = System.Drawing.Color.White
        Me.btnlevel.Location = New System.Drawing.Point(854, 424)
        Me.btnlevel.Name = "btnlevel"
        Me.btnlevel.Size = New System.Drawing.Size(90, 35)
        Me.btnlevel.TabIndex = 345472
        Me.btnlevel.Text = "Set Level"
        Me.btnlevel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnlevel.UseVisualStyleBackColor = False
        '
        'btnclear
        '
        Me.btnclear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclear.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.ForeColor = System.Drawing.Color.White
        Me.btnclear.Location = New System.Drawing.Point(1126, 356)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(113, 27)
        Me.btnclear.TabIndex = 345473
        Me.btnclear.Text = "Clear Selection"
        Me.btnclear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclear.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        '
        'btnsearch
        '
        Me.btnsearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsearch.Location = New System.Drawing.Point(452, 356)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(64, 25)
        Me.btnsearch.TabIndex = 345474
        Me.btnsearch.Text = "Search"
        Me.btnsearch.UseVisualStyleBackColor = True
        '
        'lblstatus
        '
        Me.lblstatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblstatus.AutoSize = True
        Me.lblstatus.BackColor = System.Drawing.Color.Transparent
        Me.lblstatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.ForeColor = System.Drawing.Color.Green
        Me.lblstatus.Location = New System.Drawing.Point(522, 363)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(45, 15)
        Me.lblstatus.TabIndex = 345475
        Me.lblstatus.Text = "status"
        '
        'btndescription
        '
        Me.btndescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btndescription.BackColor = System.Drawing.Color.SteelBlue
        Me.btndescription.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndescription.ForeColor = System.Drawing.Color.White
        Me.btndescription.Location = New System.Drawing.Point(686, 424)
        Me.btndescription.Name = "btndescription"
        Me.btndescription.Size = New System.Drawing.Size(166, 35)
        Me.btndescription.TabIndex = 345476
        Me.btndescription.Text = "Set Description for Web"
        Me.btndescription.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndescription.UseVisualStyleBackColor = False
        '
        'btnonline
        '
        Me.btnonline.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnonline.BackColor = System.Drawing.Color.SteelBlue
        Me.btnonline.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnonline.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnonline.ForeColor = System.Drawing.Color.White
        Me.btnonline.Location = New System.Drawing.Point(542, 424)
        Me.btnonline.Name = "btnonline"
        Me.btnonline.Size = New System.Drawing.Size(142, 35)
        Me.btnonline.TabIndex = 345477
        Me.btnonline.Text = "Update Online Store"
        Me.btnonline.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnonline.UseVisualStyleBackColor = False
        '
        'btnimage
        '
        Me.btnimage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnimage.BackColor = System.Drawing.Color.SteelBlue
        Me.btnimage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnimage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnimage.ForeColor = System.Drawing.Color.White
        Me.btnimage.Location = New System.Drawing.Point(1044, 356)
        Me.btnimage.Name = "btnimage"
        Me.btnimage.Size = New System.Drawing.Size(79, 27)
        Me.btnimage.TabIndex = 345478
        Me.btnimage.Text = "Set Image"
        Me.btnimage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnimage.UseVisualStyleBackColor = False
        '
        'chkupdateimage
        '
        Me.chkupdateimage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkupdateimage.AutoSize = True
        Me.chkupdateimage.BackColor = System.Drawing.Color.Transparent
        Me.chkupdateimage.ForeColor = System.Drawing.Color.Black
        Me.chkupdateimage.Location = New System.Drawing.Point(393, 441)
        Me.chkupdateimage.Name = "chkupdateimage"
        Me.chkupdateimage.Size = New System.Drawing.Size(149, 17)
        Me.chkupdateimage.TabIndex = 345479
        Me.chkupdateimage.Text = "Update With Image Name"
        Me.chkupdateimage.UseVisualStyleBackColor = False
        '
        'chksort
        '
        Me.chksort.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chksort.AutoSize = True
        Me.chksort.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chksort.Location = New System.Drawing.Point(157, 443)
        Me.chksort.Name = "chksort"
        Me.chksort.Size = New System.Drawing.Size(139, 17)
        Me.chksort.TabIndex = 345480
        Me.chksort.Text = "Sort by Recenty Added "
        Me.chksort.UseVisualStyleBackColor = True
        '
        'btnbarcode
        '
        Me.btnbarcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnbarcode.BackColor = System.Drawing.Color.SteelBlue
        Me.btnbarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnbarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbarcode.ForeColor = System.Drawing.Color.White
        Me.btnbarcode.Location = New System.Drawing.Point(628, 385)
        Me.btnbarcode.Name = "btnbarcode"
        Me.btnbarcode.Size = New System.Drawing.Size(98, 35)
        Me.btnbarcode.TabIndex = 345481
        Me.btnbarcode.Text = "Bar Code"
        Me.btnbarcode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnbarcode.UseVisualStyleBackColor = False
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btndelete.BackColor = System.Drawing.Color.SteelBlue
        Me.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.ForeColor = System.Drawing.Color.White
        Me.btndelete.Location = New System.Drawing.Point(962, 356)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(79, 27)
        Me.btndelete.TabIndex = 345482
        Me.btndelete.Text = "Delete"
        Me.btndelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndelete.UseVisualStyleBackColor = False
        '
        'btntaxtoprice
        '
        Me.btntaxtoprice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btntaxtoprice.BackColor = System.Drawing.Color.SteelBlue
        Me.btntaxtoprice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btntaxtoprice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntaxtoprice.ForeColor = System.Drawing.Color.White
        Me.btntaxtoprice.Location = New System.Drawing.Point(810, 356)
        Me.btntaxtoprice.Name = "btntaxtoprice"
        Me.btntaxtoprice.Size = New System.Drawing.Size(149, 27)
        Me.btntaxtoprice.TabIndex = 345483
        Me.btntaxtoprice.Text = "Set Price From Tax Price"
        Me.btntaxtoprice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btntaxtoprice.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.btnloaddecimal)
        Me.GroupBox2.Controls.Add(Me.rdop4)
        Me.GroupBox2.Controls.Add(Me.rdop3)
        Me.GroupBox2.Controls.Add(Me.rdop2)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.rdop1)
        Me.GroupBox2.Location = New System.Drawing.Point(903, 385)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(336, 35)
        Me.GroupBox2.TabIndex = 345484
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Load With Fraction"
        '
        'btnloaddecimal
        '
        Me.btnloaddecimal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnloaddecimal.BackColor = System.Drawing.Color.SteelBlue
        Me.btnloaddecimal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnloaddecimal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnloaddecimal.ForeColor = System.Drawing.Color.White
        Me.btnloaddecimal.Location = New System.Drawing.Point(278, 10)
        Me.btnloaddecimal.Name = "btnloaddecimal"
        Me.btnloaddecimal.Size = New System.Drawing.Size(52, 22)
        Me.btnloaddecimal.TabIndex = 345473
        Me.btnloaddecimal.Text = "Load"
        Me.btnloaddecimal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnloaddecimal.UseVisualStyleBackColor = False
        '
        'rdop4
        '
        Me.rdop4.AutoSize = True
        Me.rdop4.Location = New System.Drawing.Point(207, 14)
        Me.rdop4.Name = "rdop4"
        Me.rdop4.Size = New System.Drawing.Size(70, 17)
        Me.rdop4.TabIndex = 345472
        Me.rdop4.Text = "Tax Price"
        Me.rdop4.UseVisualStyleBackColor = True
        '
        'rdop3
        '
        Me.rdop3.AutoSize = True
        Me.rdop3.Location = New System.Drawing.Point(131, 14)
        Me.rdop3.Name = "rdop3"
        Me.rdop3.Size = New System.Drawing.Size(70, 17)
        Me.rdop3.TabIndex = 345471
        Me.rdop3.Text = "WS Price"
        Me.rdop3.UseVisualStyleBackColor = True
        '
        'rdop2
        '
        Me.rdop2.AutoSize = True
        Me.rdop2.Location = New System.Drawing.Point(70, 14)
        Me.rdop2.Name = "rdop2"
        Me.rdop2.Size = New System.Drawing.Size(55, 17)
        Me.rdop2.TabIndex = 345470
        Me.rdop2.Text = "Price2"
        Me.rdop2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.SteelBlue
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(555, 14)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(64, 27)
        Me.Button1.TabIndex = 345469
        Me.Button1.Text = "Set"
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = False
        '
        'rdop1
        '
        Me.rdop1.AutoSize = True
        Me.rdop1.Checked = True
        Me.rdop1.Location = New System.Drawing.Point(9, 14)
        Me.rdop1.Name = "rdop1"
        Me.rdop1.Size = New System.Drawing.Size(55, 17)
        Me.rdop1.TabIndex = 0
        Me.rdop1.TabStop = True
        Me.rdop1.Text = "Price1"
        Me.rdop1.UseVisualStyleBackColor = True
        '
        'PricemanagementFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1243, 464)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btntaxtoprice)
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.btnbarcode)
        Me.Controls.Add(Me.chksort)
        Me.Controls.Add(Me.chkupdateimage)
        Me.Controls.Add(Me.btnimage)
        Me.Controls.Add(Me.btnonline)
        Me.Controls.Add(Me.btndescription)
        Me.Controls.Add(Me.lblstatus)
        Me.Controls.Add(Me.btnsearch)
        Me.Controls.Add(Me.btnclear)
        Me.Controls.Add(Me.btnlevel)
        Me.Controls.Add(Me.btnrefresh)
        Me.Controls.Add(Me.btnupdate)
        Me.Controls.Add(Me.chkenter)
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtSeq)
        Me.Controls.Add(Me.cmbOrder)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grdPack)
        Me.Name = "PricemanagementFrm"
        Me.Text = "Pricemanagement "
        CType(Me.grdPack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.plWS.ResumeLayout(False)
        Me.plWS.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdPack As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents txtper As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rdowprice As System.Windows.Forms.RadioButton
    Friend WithEvents rdoprice As System.Windows.Forms.RadioButton
    Friend WithEvents chktag As System.Windows.Forms.CheckBox
    Friend WithEvents btnset As System.Windows.Forms.Button
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents chkenter As System.Windows.Forms.CheckBox
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents btnrefresh As System.Windows.Forms.Button
    Friend WithEvents btnlevel As System.Windows.Forms.Button
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents lblstatus As System.Windows.Forms.Label
    Friend WithEvents rdomrpdisc As System.Windows.Forms.RadioButton
    Friend WithEvents chkcal As System.Windows.Forms.CheckBox
    Friend WithEvents plWS As System.Windows.Forms.Panel
    Friend WithEvents rdoFromMRP As System.Windows.Forms.RadioButton
    Friend WithEvents rdofromPrice As System.Windows.Forms.RadioButton
    Friend WithEvents btndescription As System.Windows.Forms.Button
    Friend WithEvents btnonline As System.Windows.Forms.Button
    Friend WithEvents btnimage As System.Windows.Forms.Button
    Friend WithEvents chkupdateimage As System.Windows.Forms.CheckBox
    Friend WithEvents chksort As System.Windows.Forms.CheckBox
    Friend WithEvents btnbarcode As System.Windows.Forms.Button
    Friend WithEvents btndelete As System.Windows.Forms.Button
    Friend WithEvents btntaxtoprice As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents rdop1 As System.Windows.Forms.RadioButton
    Friend WithEvents btnloaddecimal As System.Windows.Forms.Button
    Friend WithEvents rdop4 As System.Windows.Forms.RadioButton
    Friend WithEvents rdop3 As System.Windows.Forms.RadioButton
    Friend WithEvents rdop2 As System.Windows.Forms.RadioButton
End Class
