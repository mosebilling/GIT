<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StichingServiceMasterFrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StichingServiceMasterFrm))
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label26 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.lblName = New System.Windows.Forms.Label
        Me.grdItem = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkhide = New System.Windows.Forms.CheckBox
        Me.cmbcopyfrom = New System.Windows.Forms.ComboBox
        Me.txtimgpath = New System.Windows.Forms.TextBox
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.picImage = New System.Windows.Forms.PictureBox
        Me.btnaddline = New System.Windows.Forms.Button
        Me.lblgstamt = New System.Windows.Forms.Label
        Me.btnremline = New System.Windows.Forms.Button
        Me.grdmeasurement = New System.Windows.Forms.DataGridView
        Me.lblCode = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.txtTrDescr = New System.Windows.Forms.TextBox
        Me.lblrent = New System.Windows.Forms.Label
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.NumSalesPrice = New System.Windows.Forms.TextBox
        Me.txtpriceWtax = New System.Windows.Forms.TextBox
        Me.lblgstp = New System.Windows.Forms.Label
        Me.txthsncode = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.btnclear = New System.Windows.Forms.Button
        Me.btnRemove = New System.Windows.Forms.Button
        Me.BtnUpdate = New System.Windows.Forms.Button
        Me.btnload = New System.Windows.Forms.Button
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.btnclose = New System.Windows.Forms.Button
        Me.DlgOpen = New System.Windows.Forms.OpenFileDialog
        Me.cmbcategory = New System.Windows.Forms.ComboBox
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdmeasurement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Navy
        Me.Panel2.Controls.Add(Me.Label26)
        Me.Panel2.Controls.Add(Me.PictureBox2)
        Me.Panel2.Controls.Add(Me.lblName)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(884, 34)
        Me.Panel2.TabIndex = 345488
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.White
        Me.Label26.Location = New System.Drawing.Point(50, 5)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(128, 20)
        Me.Label26.TabIndex = 345461
        Me.Label26.Text = "Service Master"
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
        'grdItem
        '
        Me.grdItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItem.BackgroundColor = System.Drawing.Color.White
        Me.grdItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdItem.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.grdItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItem.Location = New System.Drawing.Point(6, 257)
        Me.grdItem.Name = "grdItem"
        Me.grdItem.Size = New System.Drawing.Size(849, 189)
        Me.grdItem.TabIndex = 345485
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.chkhide)
        Me.GroupBox1.Controls.Add(Me.cmbcopyfrom)
        Me.GroupBox1.Controls.Add(Me.txtimgpath)
        Me.GroupBox1.Controls.Add(Me.btnBrowse)
        Me.GroupBox1.Controls.Add(Me.picImage)
        Me.GroupBox1.Controls.Add(Me.btnaddline)
        Me.GroupBox1.Controls.Add(Me.lblgstamt)
        Me.GroupBox1.Controls.Add(Me.btnremline)
        Me.GroupBox1.Controls.Add(Me.grdmeasurement)
        Me.GroupBox1.Controls.Add(Me.lblCode)
        Me.GroupBox1.Controls.Add(Me.Label34)
        Me.GroupBox1.Controls.Add(Me.txtTrDescr)
        Me.GroupBox1.Controls.Add(Me.lblrent)
        Me.GroupBox1.Controls.Add(Me.txtCode)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.NumSalesPrice)
        Me.GroupBox1.Controls.Add(Me.txtpriceWtax)
        Me.GroupBox1.Controls.Add(Me.lblgstp)
        Me.GroupBox1.Controls.Add(Me.txthsncode)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(849, 245)
        Me.GroupBox1.TabIndex = 345486
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Service Details"
        '
        'chkhide
        '
        Me.chkhide.AutoSize = True
        Me.chkhide.Location = New System.Drawing.Point(90, 194)
        Me.chkhide.Name = "chkhide"
        Me.chkhide.Size = New System.Drawing.Size(48, 17)
        Me.chkhide.TabIndex = 345495
        Me.chkhide.Text = "Hide"
        Me.chkhide.UseVisualStyleBackColor = True
        '
        'cmbcopyfrom
        '
        Me.cmbcopyfrom.FormattingEnabled = True
        Me.cmbcopyfrom.Location = New System.Drawing.Point(443, 16)
        Me.cmbcopyfrom.Name = "cmbcopyfrom"
        Me.cmbcopyfrom.Size = New System.Drawing.Size(271, 21)
        Me.cmbcopyfrom.TabIndex = 345494
        '
        'txtimgpath
        '
        Me.txtimgpath.BackColor = System.Drawing.Color.White
        Me.txtimgpath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtimgpath.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtimgpath.Location = New System.Drawing.Point(231, 209)
        Me.txtimgpath.MaxLength = 60
        Me.txtimgpath.Name = "txtimgpath"
        Me.txtimgpath.ReadOnly = True
        Me.txtimgpath.Size = New System.Drawing.Size(171, 21)
        Me.txtimgpath.TabIndex = 345493
        Me.txtimgpath.TabStop = False
        Me.txtimgpath.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(408, 205)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(29, 29)
        Me.btnBrowse.TabIndex = 345492
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'picImage
        '
        Me.picImage.BackColor = System.Drawing.Color.Transparent
        Me.picImage.InitialImage = CType(resources.GetObject("picImage.InitialImage"), System.Drawing.Image)
        Me.picImage.Location = New System.Drawing.Point(231, 73)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(206, 134)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picImage.TabIndex = 345491
        Me.picImage.TabStop = False
        '
        'btnaddline
        '
        Me.btnaddline.BackColor = System.Drawing.Color.SteelBlue
        Me.btnaddline.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnaddline.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnaddline.ForeColor = System.Drawing.Color.White
        Me.btnaddline.Location = New System.Drawing.Point(720, 14)
        Me.btnaddline.Name = "btnaddline"
        Me.btnaddline.Size = New System.Drawing.Size(55, 23)
        Me.btnaddline.TabIndex = 345490
        Me.btnaddline.Text = "Add"
        Me.btnaddline.UseVisualStyleBackColor = False
        '
        'lblgstamt
        '
        Me.lblgstamt.AutoSize = True
        Me.lblgstamt.BackColor = System.Drawing.Color.Transparent
        Me.lblgstamt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgstamt.ForeColor = System.Drawing.Color.Maroon
        Me.lblgstamt.Location = New System.Drawing.Point(90, 140)
        Me.lblgstamt.Name = "lblgstamt"
        Me.lblgstamt.Size = New System.Drawing.Size(61, 15)
        Me.lblgstamt.TabIndex = 345488
        Me.lblgstamt.Text = "GST Amt: "
        '
        'btnremline
        '
        Me.btnremline.BackColor = System.Drawing.Color.SteelBlue
        Me.btnremline.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnremline.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnremline.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnremline.ForeColor = System.Drawing.Color.White
        Me.btnremline.Location = New System.Drawing.Point(777, 14)
        Me.btnremline.Name = "btnremline"
        Me.btnremline.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnremline.Size = New System.Drawing.Size(69, 23)
        Me.btnremline.TabIndex = 345489
        Me.btnremline.Tag = "56"
        Me.btnremline.Text = "Remove"
        Me.btnremline.UseVisualStyleBackColor = False
        '
        'grdmeasurement
        '
        Me.grdmeasurement.BackgroundColor = System.Drawing.Color.White
        Me.grdmeasurement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdmeasurement.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.Red
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SkyBlue
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdmeasurement.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdmeasurement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdmeasurement.GridColor = System.Drawing.Color.Silver
        Me.grdmeasurement.Location = New System.Drawing.Point(443, 39)
        Me.grdmeasurement.Name = "grdmeasurement"
        Me.grdmeasurement.Size = New System.Drawing.Size(400, 191)
        Me.grdmeasurement.TabIndex = 345487
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCode.Location = New System.Drawing.Point(6, 25)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(71, 13)
        Me.lblCode.TabIndex = 345412
        Me.lblCode.Text = "Service Code"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Location = New System.Drawing.Point(6, 51)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(74, 13)
        Me.Label34.TabIndex = 345416
        Me.Label34.Text = "Service Name"
        '
        'txtTrDescr
        '
        Me.txtTrDescr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTrDescr.Location = New System.Drawing.Point(90, 47)
        Me.txtTrDescr.MaxLength = 50
        Me.txtTrDescr.Name = "txtTrDescr"
        Me.txtTrDescr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTrDescr.Size = New System.Drawing.Size(237, 20)
        Me.txtTrDescr.TabIndex = 1
        '
        'lblrent
        '
        Me.lblrent.AutoSize = True
        Me.lblrent.BackColor = System.Drawing.Color.Transparent
        Me.lblrent.Location = New System.Drawing.Point(6, 73)
        Me.lblrent.Name = "lblrent"
        Me.lblrent.Size = New System.Drawing.Size(30, 13)
        Me.lblrent.TabIndex = 345413
        Me.lblrent.Text = "Rate"
        '
        'txtCode
        '
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Location = New System.Drawing.Point(90, 21)
        Me.txtCode.MaxLength = 30
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(123, 20)
        Me.txtCode.TabIndex = 0
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(6, 163)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(68, 15)
        Me.Label21.TabIndex = 345481
        Me.Label21.Text = "Price + Tax"
        '
        'NumSalesPrice
        '
        Me.NumSalesPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumSalesPrice.Location = New System.Drawing.Point(90, 73)
        Me.NumSalesPrice.MaxLength = 30
        Me.NumSalesPrice.Name = "NumSalesPrice"
        Me.NumSalesPrice.Size = New System.Drawing.Size(123, 20)
        Me.NumSalesPrice.TabIndex = 2
        Me.NumSalesPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtpriceWtax
        '
        Me.txtpriceWtax.BackColor = System.Drawing.Color.White
        Me.txtpriceWtax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtpriceWtax.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpriceWtax.Location = New System.Drawing.Point(90, 161)
        Me.txtpriceWtax.MaxLength = 60
        Me.txtpriceWtax.Name = "txtpriceWtax"
        Me.txtpriceWtax.Size = New System.Drawing.Size(123, 21)
        Me.txtpriceWtax.TabIndex = 4
        Me.txtpriceWtax.TabStop = False
        Me.txtpriceWtax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblgstp
        '
        Me.lblgstp.AutoSize = True
        Me.lblgstp.BackColor = System.Drawing.Color.Transparent
        Me.lblgstp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgstp.ForeColor = System.Drawing.Color.Maroon
        Me.lblgstp.Location = New System.Drawing.Point(90, 123)
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
        Me.txthsncode.Location = New System.Drawing.Point(90, 99)
        Me.txthsncode.MaxLength = 60
        Me.txthsncode.Name = "txthsncode"
        Me.txthsncode.Size = New System.Drawing.Size(123, 21)
        Me.txthsncode.TabIndex = 3
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(6, 101)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(33, 15)
        Me.Label19.TabIndex = 345428
        Me.Label19.Text = "HSN"
        '
        'btnclear
        '
        Me.btnclear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnclear.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.ForeColor = System.Drawing.Color.White
        Me.btnclear.Location = New System.Drawing.Point(2, 549)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(83, 35)
        Me.btnclear.TabIndex = 345484
        Me.btnclear.Text = "&Clear"
        Me.btnclear.UseVisualStyleBackColor = False
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRemove.BackColor = System.Drawing.Color.SteelBlue
        Me.btnRemove.Enabled = False
        Me.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemove.ForeColor = System.Drawing.Color.White
        Me.btnRemove.Location = New System.Drawing.Point(87, 549)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(83, 35)
        Me.btnRemove.TabIndex = 345483
        Me.btnRemove.Text = "&Delete"
        Me.btnRemove.UseVisualStyleBackColor = False
        '
        'BtnUpdate
        '
        Me.BtnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnUpdate.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnUpdate.Enabled = False
        Me.BtnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdate.ForeColor = System.Drawing.Color.White
        Me.BtnUpdate.Location = New System.Drawing.Point(172, 549)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnUpdate.Size = New System.Drawing.Size(83, 35)
        Me.BtnUpdate.TabIndex = 5
        Me.BtnUpdate.Tag = "56"
        Me.BtnUpdate.Text = "&Update"
        Me.BtnUpdate.UseVisualStyleBackColor = False
        '
        'btnload
        '
        Me.btnload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.Location = New System.Drawing.Point(793, 447)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(62, 29)
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
        Me.chkSearch.Location = New System.Drawing.Point(429, 454)
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
        Me.txtSeq.Location = New System.Drawing.Point(178, 453)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(245, 20)
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
        Me.cmbOrder.Location = New System.Drawing.Point(6, 452)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOrder.Size = New System.Drawing.Size(166, 22)
        Me.cmbOrder.TabIndex = 345486
        Me.cmbOrder.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(3, 41)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(873, 506)
        Me.TabControl1.TabIndex = 345490
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.White
        Me.TabPage1.Controls.Add(Me.cmbcategory)
        Me.TabPage1.Controls.Add(Me.btnload)
        Me.TabPage1.Controls.Add(Me.chkSearch)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.txtSeq)
        Me.TabPage1.Controls.Add(Me.cmbOrder)
        Me.TabPage1.Controls.Add(Me.grdItem)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(865, 480)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Details"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ForeColor = System.Drawing.Color.White
        Me.btnclose.Location = New System.Drawing.Point(784, 547)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(82, 35)
        Me.btnclose.TabIndex = 345489
        Me.btnclose.Text = "E&xit"
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'DlgOpen
        '
        Me.DlgOpen.FileName = "OpenFileDialog1"
        '
        'cmbcategory
        '
        Me.cmbcategory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbcategory.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbcategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcategory.Items.AddRange(New Object() {"Active", "Hidden", "All"})
        Me.cmbcategory.Location = New System.Drawing.Point(686, 449)
        Me.cmbcategory.Name = "cmbcategory"
        Me.cmbcategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbcategory.Size = New System.Drawing.Size(101, 22)
        Me.cmbcategory.TabIndex = 345489
        Me.cmbcategory.TabStop = False
        '
        'StichingServiceMasterFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(884, 590)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.BtnUpdate)
        Me.Controls.Add(Me.btnclear)
        Me.Controls.Add(Me.btnRemove)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "StichingServiceMasterFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Master"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdmeasurement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents grdItem As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblgstamt As System.Windows.Forms.Label
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtTrDescr As System.Windows.Forms.TextBox
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents lblrent As System.Windows.Forms.Label
    Public WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents NumSalesPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtpriceWtax As System.Windows.Forms.TextBox
    Friend WithEvents lblgstp As System.Windows.Forms.Label
    Friend WithEvents txthsncode As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents grdmeasurement As System.Windows.Forms.DataGridView
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents btnaddline As System.Windows.Forms.Button
    Public WithEvents btnremline As System.Windows.Forms.Button
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents DlgOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtimgpath As System.Windows.Forms.TextBox
    Friend WithEvents cmbcopyfrom As System.Windows.Forms.ComboBox
    Friend WithEvents chkhide As System.Windows.Forms.CheckBox
    Public WithEvents cmbcategory As System.Windows.Forms.ComboBox
End Class
