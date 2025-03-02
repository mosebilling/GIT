<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LodgeRoomFrm
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
        Me.Label26 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.lblName = New System.Windows.Forms.Label
        Me.NumSalesPrice = New System.Windows.Forms.TextBox
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.lblCode = New System.Windows.Forms.Label
        Me.lblrent = New System.Windows.Forms.Label
        Me.txtTrDescr = New System.Windows.Forms.TextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbtype = New System.Windows.Forms.ComboBox
        Me.rdoac = New System.Windows.Forms.RadioButton
        Me.rdononac = New System.Windows.Forms.RadioButton
        Me.lblgstp = New System.Windows.Forms.Label
        Me.txthsncode = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtpriceWtax = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.btnRemove = New System.Windows.Forms.Button
        Me.BtnUpdate = New System.Windows.Forms.Button
        Me.btnclose = New System.Windows.Forms.Button
        Me.grdItem = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.rdoready = New System.Windows.Forms.RadioButton
        Me.rdonotavailable = New System.Windows.Forms.RadioButton
        Me.rdocleaning = New System.Windows.Forms.RadioButton
        Me.lblgstamt = New System.Windows.Forms.Label
        Me.cmbtax = New System.Windows.Forms.ComboBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.lblstatus = New System.Windows.Forms.Label
        Me.btnclear = New System.Windows.Forms.Button
        Me.plwithoutac = New System.Windows.Forms.Panel
        Me.lblnonacGst = New System.Windows.Forms.Label
        Me.lblwithoutacRent = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtwithoutac = New System.Windows.Forms.TextBox
        Me.txtpricetaxwithoutac = New System.Windows.Forms.TextBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.Label3 = New System.Windows.Forms.Label
        Me.grdroomhistory = New System.Windows.Forms.DataGridView
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.btnload = New System.Windows.Forms.Button
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.lblremarks = New System.Windows.Forms.Label
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.plwithoutac.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.grdroomhistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.Label26)
        Me.Panel2.Controls.Add(Me.PictureBox2)
        Me.Panel2.Controls.Add(Me.lblName)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1100, 34)
        Me.Panel2.TabIndex = 345420
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(50, 5)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(116, 20)
        Me.Label26.TabIndex = 345461
        Me.Label26.Text = "Room Master"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(3, 5)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(41, 22)
        Me.PictureBox2.TabIndex = 345460
        Me.PictureBox2.TabStop = False
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.White
        Me.lblName.Location = New System.Drawing.Point(41, 9)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(94, 20)
        Me.lblName.TabIndex = 6
        Me.lblName.Text = "Item Master"
        '
        'NumSalesPrice
        '
        Me.NumSalesPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumSalesPrice.Location = New System.Drawing.Point(90, 184)
        Me.NumSalesPrice.MaxLength = 30
        Me.NumSalesPrice.Name = "NumSalesPrice"
        Me.NumSalesPrice.Size = New System.Drawing.Size(123, 20)
        Me.NumSalesPrice.TabIndex = 3
        Me.NumSalesPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCode
        '
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Location = New System.Drawing.Point(90, 21)
        Me.txtCode.MaxLength = 30
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(140, 20)
        Me.txtCode.TabIndex = 0
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCode.Location = New System.Drawing.Point(6, 25)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(75, 13)
        Me.lblCode.TabIndex = 345412
        Me.lblCode.Text = "Room Number"
        '
        'lblrent
        '
        Me.lblrent.AutoSize = True
        Me.lblrent.BackColor = System.Drawing.Color.Transparent
        Me.lblrent.Location = New System.Drawing.Point(6, 184)
        Me.lblrent.Name = "lblrent"
        Me.lblrent.Size = New System.Drawing.Size(30, 13)
        Me.lblrent.TabIndex = 345413
        Me.lblrent.Text = "Rent"
        '
        'txtTrDescr
        '
        Me.txtTrDescr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTrDescr.Location = New System.Drawing.Point(90, 97)
        Me.txtTrDescr.MaxLength = 50
        Me.txtTrDescr.Multiline = True
        Me.txtTrDescr.Name = "txtTrDescr"
        Me.txtTrDescr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTrDescr.Size = New System.Drawing.Size(369, 74)
        Me.txtTrDescr.TabIndex = 2
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Location = New System.Drawing.Point(6, 101)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(60, 13)
        Me.Label34.TabIndex = 345416
        Me.Label34.Text = "Description"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(6, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 345421
        Me.Label1.Text = "Room Type"
        '
        'cmbtype
        '
        Me.cmbtype.FormattingEnabled = True
        Me.cmbtype.Location = New System.Drawing.Point(90, 47)
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.Size = New System.Drawing.Size(369, 21)
        Me.cmbtype.TabIndex = 1
        '
        'rdoac
        '
        Me.rdoac.AutoSize = True
        Me.rdoac.Location = New System.Drawing.Point(169, 74)
        Me.rdoac.Name = "rdoac"
        Me.rdoac.Size = New System.Drawing.Size(44, 17)
        Me.rdoac.TabIndex = 345423
        Me.rdoac.Text = "A/C"
        Me.rdoac.UseVisualStyleBackColor = True
        '
        'rdononac
        '
        Me.rdononac.AutoSize = True
        Me.rdononac.Checked = True
        Me.rdononac.Location = New System.Drawing.Point(90, 74)
        Me.rdononac.Name = "rdononac"
        Me.rdononac.Size = New System.Drawing.Size(71, 17)
        Me.rdononac.TabIndex = 345424
        Me.rdononac.TabStop = True
        Me.rdononac.Text = "NON A/C"
        Me.rdononac.UseVisualStyleBackColor = True
        '
        'lblgstp
        '
        Me.lblgstp.AutoSize = True
        Me.lblgstp.BackColor = System.Drawing.Color.Transparent
        Me.lblgstp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgstp.ForeColor = System.Drawing.Color.Maroon
        Me.lblgstp.Location = New System.Drawing.Point(90, 234)
        Me.lblgstp.Name = "lblgstp"
        Me.lblgstp.Size = New System.Drawing.Size(40, 15)
        Me.lblgstp.TabIndex = 345430
        Me.lblgstp.Text = "GST : "
        '
        'txthsncode
        '
        Me.txthsncode.BackColor = System.Drawing.Color.White
        Me.txthsncode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txthsncode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txthsncode.Location = New System.Drawing.Point(90, 210)
        Me.txthsncode.MaxLength = 60
        Me.txthsncode.Name = "txthsncode"
        Me.txthsncode.Size = New System.Drawing.Size(123, 21)
        Me.txthsncode.TabIndex = 4
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(6, 212)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(80, 15)
        Me.Label19.TabIndex = 345428
        Me.Label19.Text = "Set Tax Code"
        '
        'txtpriceWtax
        '
        Me.txtpriceWtax.BackColor = System.Drawing.Color.White
        Me.txtpriceWtax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtpriceWtax.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpriceWtax.Location = New System.Drawing.Point(90, 348)
        Me.txtpriceWtax.MaxLength = 60
        Me.txtpriceWtax.Name = "txtpriceWtax"
        Me.txtpriceWtax.Size = New System.Drawing.Size(123, 21)
        Me.txtpriceWtax.TabIndex = 5
        Me.txtpriceWtax.TabStop = False
        Me.txtpriceWtax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(6, 350)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(68, 15)
        Me.Label21.TabIndex = 345481
        Me.Label21.Text = "Price + Tax"
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemove.BackColor = System.Drawing.Color.SteelBlue
        Me.btnRemove.Enabled = False
        Me.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemove.ForeColor = System.Drawing.Color.White
        Me.btnRemove.Location = New System.Drawing.Point(300, 370)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(83, 35)
        Me.btnRemove.TabIndex = 345483
        Me.btnRemove.Text = "&Delete"
        Me.btnRemove.UseVisualStyleBackColor = False
        '
        'BtnUpdate
        '
        Me.BtnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnUpdate.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnUpdate.Enabled = False
        Me.BtnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdate.ForeColor = System.Drawing.Color.White
        Me.BtnUpdate.Location = New System.Drawing.Point(385, 370)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnUpdate.Size = New System.Drawing.Size(83, 35)
        Me.BtnUpdate.TabIndex = 5
        Me.BtnUpdate.Tag = "56"
        Me.BtnUpdate.Text = "&Update"
        Me.BtnUpdate.UseVisualStyleBackColor = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ForeColor = System.Drawing.Color.White
        Me.btnclose.Location = New System.Drawing.Point(1000, 501)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(82, 35)
        Me.btnclose.TabIndex = 345484
        Me.btnclose.Text = "E&xit"
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'grdItem
        '
        Me.grdItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItem.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItem.Location = New System.Drawing.Point(6, 6)
        Me.grdItem.Name = "grdItem"
        Me.grdItem.Size = New System.Drawing.Size(1069, 389)
        Me.grdItem.TabIndex = 345485
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.lblremarks)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.lblgstamt)
        Me.GroupBox1.Controls.Add(Me.cmbtax)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.lblstatus)
        Me.GroupBox1.Controls.Add(Me.btnclear)
        Me.GroupBox1.Controls.Add(Me.lblCode)
        Me.GroupBox1.Controls.Add(Me.Label34)
        Me.GroupBox1.Controls.Add(Me.txtTrDescr)
        Me.GroupBox1.Controls.Add(Me.btnRemove)
        Me.GroupBox1.Controls.Add(Me.lblrent)
        Me.GroupBox1.Controls.Add(Me.BtnUpdate)
        Me.GroupBox1.Controls.Add(Me.txtCode)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.NumSalesPrice)
        Me.GroupBox1.Controls.Add(Me.txtpriceWtax)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblgstp)
        Me.GroupBox1.Controls.Add(Me.cmbtype)
        Me.GroupBox1.Controls.Add(Me.txthsncode)
        Me.GroupBox1.Controls.Add(Me.rdoac)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.rdononac)
        Me.GroupBox1.Controls.Add(Me.plwithoutac)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(474, 415)
        Me.GroupBox1.TabIndex = 345486
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Room Details"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rdoready)
        Me.GroupBox2.Controls.Add(Me.rdonotavailable)
        Me.GroupBox2.Controls.Add(Me.rdocleaning)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(219, 326)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(245, 39)
        Me.GroupBox2.TabIndex = 345489
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Mark As.."
        '
        'rdoready
        '
        Me.rdoready.AutoSize = True
        Me.rdoready.ForeColor = System.Drawing.Color.Green
        Me.rdoready.Location = New System.Drawing.Point(182, 16)
        Me.rdoready.Name = "rdoready"
        Me.rdoready.Size = New System.Drawing.Size(61, 17)
        Me.rdoready.TabIndex = 2
        Me.rdoready.TabStop = True
        Me.rdoready.Text = "Ready"
        Me.rdoready.UseVisualStyleBackColor = True
        '
        'rdonotavailable
        '
        Me.rdonotavailable.AutoSize = True
        Me.rdonotavailable.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.rdonotavailable.Location = New System.Drawing.Point(81, 16)
        Me.rdonotavailable.Name = "rdonotavailable"
        Me.rdonotavailable.Size = New System.Drawing.Size(101, 17)
        Me.rdonotavailable.TabIndex = 1
        Me.rdonotavailable.TabStop = True
        Me.rdonotavailable.Text = "Not Available"
        Me.rdonotavailable.UseVisualStyleBackColor = True
        '
        'rdocleaning
        '
        Me.rdocleaning.AutoSize = True
        Me.rdocleaning.ForeColor = System.Drawing.Color.Olive
        Me.rdocleaning.Location = New System.Drawing.Point(7, 16)
        Me.rdocleaning.Name = "rdocleaning"
        Me.rdocleaning.Size = New System.Drawing.Size(74, 17)
        Me.rdocleaning.TabIndex = 0
        Me.rdocleaning.TabStop = True
        Me.rdocleaning.Text = "Cleaning"
        Me.rdocleaning.UseVisualStyleBackColor = True
        '
        'lblgstamt
        '
        Me.lblgstamt.AutoSize = True
        Me.lblgstamt.BackColor = System.Drawing.Color.Transparent
        Me.lblgstamt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgstamt.ForeColor = System.Drawing.Color.Maroon
        Me.lblgstamt.Location = New System.Drawing.Point(90, 251)
        Me.lblgstamt.Name = "lblgstamt"
        Me.lblgstamt.Size = New System.Drawing.Size(61, 15)
        Me.lblgstamt.TabIndex = 345488
        Me.lblgstamt.Text = "GST Amt: "
        '
        'cmbtax
        '
        Me.cmbtax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtax.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbtax.FormattingEnabled = True
        Me.cmbtax.Location = New System.Drawing.Point(90, 319)
        Me.cmbtax.MaxLength = 10
        Me.cmbtax.Name = "cmbtax"
        Me.cmbtax.Size = New System.Drawing.Size(94, 23)
        Me.cmbtax.TabIndex = 345486
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(6, 319)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(68, 15)
        Me.Label17.TabIndex = 345487
        Me.Label17.Text = "Flood Cess"
        '
        'lblstatus
        '
        Me.lblstatus.AutoSize = True
        Me.lblstatus.BackColor = System.Drawing.Color.Transparent
        Me.lblstatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.Location = New System.Drawing.Point(6, 376)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(62, 20)
        Me.lblstatus.TabIndex = 345485
        Me.lblstatus.Text = "Status"
        '
        'btnclear
        '
        Me.btnclear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclear.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.ForeColor = System.Drawing.Color.White
        Me.btnclear.Location = New System.Drawing.Point(215, 370)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(83, 35)
        Me.btnclear.TabIndex = 345484
        Me.btnclear.Text = "&Clear"
        Me.btnclear.UseVisualStyleBackColor = False
        '
        'plwithoutac
        '
        Me.plwithoutac.Controls.Add(Me.lblnonacGst)
        Me.plwithoutac.Controls.Add(Me.lblwithoutacRent)
        Me.plwithoutac.Controls.Add(Me.Label2)
        Me.plwithoutac.Controls.Add(Me.txtwithoutac)
        Me.plwithoutac.Controls.Add(Me.txtpricetaxwithoutac)
        Me.plwithoutac.Location = New System.Drawing.Point(219, 174)
        Me.plwithoutac.Name = "plwithoutac"
        Me.plwithoutac.Size = New System.Drawing.Size(185, 106)
        Me.plwithoutac.TabIndex = 6
        Me.plwithoutac.Visible = False
        '
        'lblnonacGst
        '
        Me.lblnonacGst.AutoSize = True
        Me.lblnonacGst.BackColor = System.Drawing.Color.Transparent
        Me.lblnonacGst.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnonacGst.ForeColor = System.Drawing.Color.Maroon
        Me.lblnonacGst.Location = New System.Drawing.Point(3, 63)
        Me.lblnonacGst.Name = "lblnonacGst"
        Me.lblnonacGst.Size = New System.Drawing.Size(61, 15)
        Me.lblnonacGst.TabIndex = 345489
        Me.lblnonacGst.Text = "GST Amt: "
        '
        'lblwithoutacRent
        '
        Me.lblwithoutacRent.AutoSize = True
        Me.lblwithoutacRent.BackColor = System.Drawing.Color.Transparent
        Me.lblwithoutacRent.Location = New System.Drawing.Point(3, 14)
        Me.lblwithoutacRent.Name = "lblwithoutacRent"
        Me.lblwithoutacRent.Size = New System.Drawing.Size(66, 13)
        Me.lblwithoutacRent.TabIndex = 345486
        Me.lblwithoutacRent.Text = "Without A/C"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(3, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 345488
        Me.Label2.Text = "Without A/C"
        '
        'txtwithoutac
        '
        Me.txtwithoutac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtwithoutac.Location = New System.Drawing.Point(75, 11)
        Me.txtwithoutac.MaxLength = 30
        Me.txtwithoutac.Name = "txtwithoutac"
        Me.txtwithoutac.Size = New System.Drawing.Size(101, 20)
        Me.txtwithoutac.TabIndex = 6
        Me.txtwithoutac.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtpricetaxwithoutac
        '
        Me.txtpricetaxwithoutac.BackColor = System.Drawing.Color.White
        Me.txtpricetaxwithoutac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtpricetaxwithoutac.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpricetaxwithoutac.Location = New System.Drawing.Point(75, 38)
        Me.txtpricetaxwithoutac.MaxLength = 60
        Me.txtpricetaxwithoutac.Name = "txtpricetaxwithoutac"
        Me.txtpricetaxwithoutac.Size = New System.Drawing.Size(101, 21)
        Me.txtpricetaxwithoutac.TabIndex = 7
        Me.txtpricetaxwithoutac.TabStop = False
        Me.txtpricetaxwithoutac.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(3, 40)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1089, 455)
        Me.TabControl1.TabIndex = 345487
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.White
        Me.TabPage1.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.grdroomhistory)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1081, 429)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Room Details"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(486, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 16)
        Me.Label3.TabIndex = 345485
        Me.Label3.Text = "History"
        '
        'grdroomhistory
        '
        Me.grdroomhistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdroomhistory.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdroomhistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdroomhistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdroomhistory.Location = New System.Drawing.Point(486, 31)
        Me.grdroomhistory.Name = "grdroomhistory"
        Me.grdroomhistory.Size = New System.Drawing.Size(589, 392)
        Me.grdroomhistory.TabIndex = 345487
        '
        'TabPage2
        '
        Me.TabPage2.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.TabPage2.Controls.Add(Me.btnload)
        Me.TabPage2.Controls.Add(Me.chkSearch)
        Me.TabPage2.Controls.Add(Me.txtSeq)
        Me.TabPage2.Controls.Add(Me.cmbOrder)
        Me.TabPage2.Controls.Add(Me.grdItem)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1081, 429)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Room List"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnload
        '
        Me.btnload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.Location = New System.Drawing.Point(993, 397)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(82, 29)
        Me.btnload.TabIndex = 345488
        Me.btnload.Text = "Load"
        Me.btnload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnload.UseVisualStyleBackColor = False
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(558, 406)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345488
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
        Me.chkSearch.Visible = False
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
        Me.txtSeq.Location = New System.Drawing.Point(178, 403)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(374, 20)
        Me.txtSeq.TabIndex = 345487
        '
        'cmbOrder
        '
        Me.cmbOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOrder.Location = New System.Drawing.Point(6, 401)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOrder.Size = New System.Drawing.Size(166, 22)
        Me.cmbOrder.TabIndex = 345486
        Me.cmbOrder.TabStop = False
        '
        'lblremarks
        '
        Me.lblremarks.AutoSize = True
        Me.lblremarks.BackColor = System.Drawing.Color.Transparent
        Me.lblremarks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblremarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblremarks.Location = New System.Drawing.Point(216, 285)
        Me.lblremarks.Name = "lblremarks"
        Me.lblremarks.Size = New System.Drawing.Size(51, 15)
        Me.lblremarks.TabIndex = 345491
        Me.lblremarks.Text = "Remark"
        '
        'LodgeRoomFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1100, 539)
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "LodgeRoomFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Room Master"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.plwithoutac.ResumeLayout(False)
        Me.plwithoutac.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.grdroomhistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents NumSalesPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents lblrent As System.Windows.Forms.Label
    Friend WithEvents txtTrDescr As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbtype As System.Windows.Forms.ComboBox
    Friend WithEvents rdoac As System.Windows.Forms.RadioButton
    Friend WithEvents rdononac As System.Windows.Forms.RadioButton
    Friend WithEvents lblgstp As System.Windows.Forms.Label
    Friend WithEvents txthsncode As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtpriceWtax As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Public WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents grdItem As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents lblwithoutacRent As System.Windows.Forms.Label
    Friend WithEvents txtwithoutac As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtpricetaxwithoutac As System.Windows.Forms.TextBox
    Friend WithEvents plwithoutac As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grdroomhistory As System.Windows.Forms.DataGridView
    Friend WithEvents lblstatus As System.Windows.Forms.Label
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents cmbtax As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblgstamt As System.Windows.Forms.Label
    Friend WithEvents lblnonacGst As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoready As System.Windows.Forms.RadioButton
    Friend WithEvents rdonotavailable As System.Windows.Forms.RadioButton
    Friend WithEvents rdocleaning As System.Windows.Forms.RadioButton
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents lblremarks As System.Windows.Forms.Label
End Class
