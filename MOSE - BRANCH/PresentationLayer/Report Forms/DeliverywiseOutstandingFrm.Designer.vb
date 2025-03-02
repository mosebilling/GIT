<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DeliverywiseOutstandingFrm
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
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblName = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.grdvoucher = New System.Windows.Forms.DataGridView
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.cmbSearch = New System.Windows.Forms.ComboBox
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.cmbdeliveredBy = New System.Windows.Forms.ComboBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.chknotupdated = New System.Windows.Forms.CheckBox
        Me.rdocollectionlist = New System.Windows.Forms.RadioButton
        Me.rdosummary = New System.Windows.Forms.RadioButton
        Me.rdoinvoicewise = New System.Windows.Forms.RadioButton
        Me.btnLoad = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnApply = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cldrEnddate = New System.Windows.Forms.DateTimePicker
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnupdate = New System.Windows.Forms.Button
        Me.grpReceipt = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbVoucherTp = New System.Windows.Forms.ComboBox
        Me.chksinglerv = New System.Windows.Forms.CheckBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.txtPcash = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cldrdate = New System.Windows.Forms.DateTimePicker
        Me.txtrvprefix = New System.Windows.Forms.TextBox
        Me.txtrvnumber = New System.Windows.Forms.TextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.chkob = New System.Windows.Forms.CheckBox
        Me.lblTotal = New System.Windows.Forms.Label
        Me.chkall = New System.Windows.Forms.CheckBox
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grpReceipt.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.lblName)
        Me.Panel2.Controls.Add(Me.PictureBox2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(906, 32)
        Me.Panel2.TabIndex = 345466
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.White
        Me.lblName.Font = New System.Drawing.Font("Goudy Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblName.Location = New System.Drawing.Point(41, 5)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(252, 21)
        Me.lblName.TabIndex = 345458
        Me.lblName.Text = "DELIVERY WISE OUTSTANIDNG"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.button_reports1
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(3, -1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(32, 35)
        Me.PictureBox2.TabIndex = 345457
        Me.PictureBox2.TabStop = False
        '
        'grdvoucher
        '
        Me.grdvoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdvoucher.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdvoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdvoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdvoucher.GridColor = System.Drawing.Color.Gainsboro
        Me.grdvoucher.Location = New System.Drawing.Point(4, 189)
        Me.grdvoucher.Name = "grdvoucher"
        Me.grdvoucher.Size = New System.Drawing.Size(898, 261)
        Me.grdvoucher.TabIndex = 345464
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel6.Controls.Add(Me.cmbSearch)
        Me.Panel6.Controls.Add(Me.txtSearch)
        Me.Panel6.Controls.Add(Me.chkSearch)
        Me.Panel6.Location = New System.Drawing.Point(4, 452)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(493, 29)
        Me.Panel6.TabIndex = 345465
        '
        'cmbSearch
        '
        Me.cmbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSearch.FormattingEnabled = True
        Me.cmbSearch.Location = New System.Drawing.Point(5, 3)
        Me.cmbSearch.Name = "cmbSearch"
        Me.cmbSearch.Size = New System.Drawing.Size(143, 21)
        Me.cmbSearch.TabIndex = 91
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(154, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(191, 20)
        Me.txtSearch.TabIndex = 0
        '
        'chkSearch
        '
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(351, 4)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 92
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Location = New System.Drawing.Point(8, 136)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(68, 13)
        Me.Label28.TabIndex = 345491
        Me.Label28.Text = "Collection By"
        '
        'cmbdeliveredBy
        '
        Me.cmbdeliveredBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbdeliveredBy.FormattingEnabled = True
        Me.cmbdeliveredBy.Location = New System.Drawing.Point(77, 132)
        Me.cmbdeliveredBy.Name = "cmbdeliveredBy"
        Me.cmbdeliveredBy.Size = New System.Drawing.Size(135, 21)
        Me.cmbdeliveredBy.TabIndex = 345490
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.chknotupdated)
        Me.GroupBox3.Controls.Add(Me.rdocollectionlist)
        Me.GroupBox3.Controls.Add(Me.rdosummary)
        Me.GroupBox3.Controls.Add(Me.rdoinvoicewise)
        Me.GroupBox3.Location = New System.Drawing.Point(9, 36)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(307, 90)
        Me.GroupBox3.TabIndex = 345492
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Options"
        '
        'chknotupdated
        '
        Me.chknotupdated.AutoSize = True
        Me.chknotupdated.BackColor = System.Drawing.Color.Transparent
        Me.chknotupdated.ForeColor = System.Drawing.Color.Maroon
        Me.chknotupdated.Location = New System.Drawing.Point(149, 42)
        Me.chknotupdated.Name = "chknotupdated"
        Me.chknotupdated.Size = New System.Drawing.Size(154, 17)
        Me.chknotupdated.TabIndex = 345498
        Me.chknotupdated.Text = "Not Updated For Collection"
        Me.chknotupdated.UseVisualStyleBackColor = False
        Me.chknotupdated.Visible = False
        '
        'rdocollectionlist
        '
        Me.rdocollectionlist.AutoSize = True
        Me.rdocollectionlist.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdocollectionlist.Location = New System.Drawing.Point(6, 63)
        Me.rdocollectionlist.Name = "rdocollectionlist"
        Me.rdocollectionlist.Size = New System.Drawing.Size(88, 17)
        Me.rdocollectionlist.TabIndex = 345428
        Me.rdocollectionlist.Tag = ""
        Me.rdocollectionlist.Text = "Collecton List"
        Me.rdocollectionlist.UseVisualStyleBackColor = True
        '
        'rdosummary
        '
        Me.rdosummary.AutoSize = True
        Me.rdosummary.Checked = True
        Me.rdosummary.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdosummary.Location = New System.Drawing.Point(6, 17)
        Me.rdosummary.Name = "rdosummary"
        Me.rdosummary.Size = New System.Drawing.Size(197, 17)
        Me.rdosummary.TabIndex = 345367
        Me.rdosummary.TabStop = True
        Me.rdosummary.Tag = ""
        Me.rdosummary.Text = "Customer wise Outstanding summary"
        Me.rdosummary.UseVisualStyleBackColor = True
        '
        'rdoinvoicewise
        '
        Me.rdoinvoicewise.AutoSize = True
        Me.rdoinvoicewise.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoinvoicewise.Location = New System.Drawing.Point(6, 40)
        Me.rdoinvoicewise.Name = "rdoinvoicewise"
        Me.rdoinvoicewise.Size = New System.Drawing.Size(142, 17)
        Me.rdoinvoicewise.TabIndex = 345427
        Me.rdoinvoicewise.Tag = ""
        Me.rdoinvoicewise.Text = "Invoice wise outstanding"
        Me.rdoinvoicewise.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.BackColor = System.Drawing.Color.SteelBlue
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.ForeColor = System.Drawing.Color.White
        Me.btnLoad.Location = New System.Drawing.Point(539, 45)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(85, 35)
        Me.btnLoad.TabIndex = 345493
        Me.btnLoad.Text = "&Load"
        Me.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLoad.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.AutoEllipsis = True
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(817, 455)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(85, 35)
        Me.btnExit.TabIndex = 345494
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
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
        Me.btnApply.Location = New System.Drawing.Point(730, 455)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(85, 35)
        Me.btnApply.TabIndex = 345495
        Me.btnApply.Text = "Preview"
        Me.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnApply.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.cldrEnddate)
        Me.GroupBox2.Controls.Add(Me.cldrStartDate)
        Me.GroupBox2.Location = New System.Drawing.Point(318, 38)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(215, 42)
        Me.GroupBox2.TabIndex = 345496
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Date Parameter"
        '
        'cldrEnddate
        '
        Me.cldrEnddate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrEnddate.Location = New System.Drawing.Point(110, 17)
        Me.cldrEnddate.Name = "cldrEnddate"
        Me.cldrEnddate.Size = New System.Drawing.Size(96, 20)
        Me.cldrEnddate.TabIndex = 345395
        Me.cldrEnddate.TabStop = False
        '
        'cldrStartDate
        '
        Me.cldrStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrStartDate.Location = New System.Drawing.Point(9, 17)
        Me.cldrStartDate.Name = "cldrStartDate"
        Me.cldrStartDate.Size = New System.Drawing.Size(95, 20)
        Me.cldrStartDate.TabIndex = 345393
        Me.cldrStartDate.TabStop = False
        '
        'Timer1
        '
        '
        'btnupdate
        '
        Me.btnupdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnupdate.AutoEllipsis = True
        Me.btnupdate.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnupdate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnupdate.Location = New System.Drawing.Point(578, 455)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(150, 35)
        Me.btnupdate.TabIndex = 345497
        Me.btnupdate.Text = "Update For Collection"
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'grpReceipt
        '
        Me.grpReceipt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpReceipt.BackColor = System.Drawing.Color.MistyRose
        Me.grpReceipt.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.grpReceipt.Controls.Add(Me.Label4)
        Me.grpReceipt.Controls.Add(Me.cmbVoucherTp)
        Me.grpReceipt.Controls.Add(Me.chksinglerv)
        Me.grpReceipt.Controls.Add(Me.Label33)
        Me.grpReceipt.Controls.Add(Me.txtPcash)
        Me.grpReceipt.Controls.Add(Me.Label1)
        Me.grpReceipt.Controls.Add(Me.cldrdate)
        Me.grpReceipt.Controls.Add(Me.txtrvprefix)
        Me.grpReceipt.Controls.Add(Me.txtrvnumber)
        Me.grpReceipt.Controls.Add(Me.Label31)
        Me.grpReceipt.Location = New System.Drawing.Point(637, 38)
        Me.grpReceipt.Name = "grpReceipt"
        Me.grpReceipt.Size = New System.Drawing.Size(264, 145)
        Me.grpReceipt.TabIndex = 345498
        Me.grpReceipt.TabStop = False
        Me.grpReceipt.Text = "Receipt"
        Me.grpReceipt.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(10, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 13)
        Me.Label4.TabIndex = 345503
        Me.Label4.Text = "Mode"
        '
        'cmbVoucherTp
        '
        Me.cmbVoucherTp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVoucherTp.FormattingEnabled = True
        Me.cmbVoucherTp.Location = New System.Drawing.Point(76, 17)
        Me.cmbVoucherTp.Name = "cmbVoucherTp"
        Me.cmbVoucherTp.Size = New System.Drawing.Size(182, 21)
        Me.cmbVoucherTp.TabIndex = 345502
        '
        'chksinglerv
        '
        Me.chksinglerv.AutoSize = True
        Me.chksinglerv.BackColor = System.Drawing.Color.Transparent
        Me.chksinglerv.Checked = True
        Me.chksinglerv.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chksinglerv.ForeColor = System.Drawing.Color.Maroon
        Me.chksinglerv.Location = New System.Drawing.Point(76, 125)
        Me.chksinglerv.Name = "chksinglerv"
        Me.chksinglerv.Size = New System.Drawing.Size(121, 17)
        Me.chksinglerv.TabIndex = 345501
        Me.chksinglerv.Text = "Create as Single RV"
        Me.chksinglerv.UseVisualStyleBackColor = False
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(10, 96)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(53, 15)
        Me.Label33.TabIndex = 345408
        Me.Label33.Text = "Debit To"
        '
        'txtPcash
        '
        Me.txtPcash.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPcash.Location = New System.Drawing.Point(76, 93)
        Me.txtPcash.Name = "txtPcash"
        Me.txtPcash.ReadOnly = True
        Me.txtPcash.Size = New System.Drawing.Size(182, 26)
        Me.txtPcash.TabIndex = 345407
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 15)
        Me.Label1.TabIndex = 345406
        Me.Label1.Text = "Date"
        '
        'cldrdate
        '
        Me.cldrdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrdate.Location = New System.Drawing.Point(76, 70)
        Me.cldrdate.Name = "cldrdate"
        Me.cldrdate.Size = New System.Drawing.Size(95, 20)
        Me.cldrdate.TabIndex = 345405
        Me.cldrdate.TabStop = False
        '
        'txtrvprefix
        '
        Me.txtrvprefix.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrvprefix.Location = New System.Drawing.Point(76, 45)
        Me.txtrvprefix.MaxLength = 15
        Me.txtrvprefix.Name = "txtrvprefix"
        Me.txtrvprefix.ReadOnly = True
        Me.txtrvprefix.Size = New System.Drawing.Size(59, 21)
        Me.txtrvprefix.TabIndex = 345404
        '
        'txtrvnumber
        '
        Me.txtrvnumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrvnumber.Location = New System.Drawing.Point(137, 45)
        Me.txtrvnumber.Name = "txtrvnumber"
        Me.txtrvnumber.ReadOnly = True
        Me.txtrvnumber.Size = New System.Drawing.Size(76, 21)
        Me.txtrvnumber.TabIndex = 345402
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(11, 48)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(25, 15)
        Me.Label31.TabIndex = 345403
        Me.Label31.Text = "Vr#"
        '
        'chkob
        '
        Me.chkob.AutoSize = True
        Me.chkob.BackColor = System.Drawing.Color.Transparent
        Me.chkob.ForeColor = System.Drawing.Color.Maroon
        Me.chkob.Location = New System.Drawing.Point(218, 132)
        Me.chkob.Name = "chkob"
        Me.chkob.Size = New System.Drawing.Size(102, 17)
        Me.chkob.TabIndex = 345499
        Me.chkob.Text = "Update OB Also"
        Me.chkob.UseVisualStyleBackColor = False
        Me.chkob.Visible = False
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotal.BackColor = System.Drawing.Color.Transparent
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(410, 462)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(162, 21)
        Me.lblTotal.TabIndex = 345500
        Me.lblTotal.Text = "Total : 0.00"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotal.Visible = False
        '
        'chkall
        '
        Me.chkall.AutoSize = True
        Me.chkall.BackColor = System.Drawing.Color.Transparent
        Me.chkall.ForeColor = System.Drawing.Color.Maroon
        Me.chkall.Location = New System.Drawing.Point(77, 159)
        Me.chkall.Name = "chkall"
        Me.chkall.Size = New System.Drawing.Size(75, 17)
        Me.chkall.TabIndex = 345501
        Me.chkall.Text = "Update All"
        Me.chkall.UseVisualStyleBackColor = False
        '
        'DeliverywiseOutstandingFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(906, 493)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkall)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.chkob)
        Me.Controls.Add(Me.grpReceipt)
        Me.Controls.Add(Me.btnupdate)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.cmbdeliveredBy)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.grdvoucher)
        Me.Controls.Add(Me.Panel6)
        Me.Name = "DeliverywiseOutstandingFrm"
        Me.Text = "Deliverywise Outstanding "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.grpReceipt.ResumeLayout(False)
        Me.grpReceipt.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents grdvoucher As System.Windows.Forms.DataGridView
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents cmbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents cmbdeliveredBy As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rdosummary As System.Windows.Forms.RadioButton
    Friend WithEvents rdoinvoicewise As System.Windows.Forms.RadioButton
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cldrEnddate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents chknotupdated As System.Windows.Forms.CheckBox
    Friend WithEvents rdocollectionlist As System.Windows.Forms.RadioButton
    Friend WithEvents grpReceipt As System.Windows.Forms.GroupBox
    Friend WithEvents txtrvprefix As System.Windows.Forms.TextBox
    Friend WithEvents txtrvnumber As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cldrdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtPcash As System.Windows.Forms.TextBox
    Friend WithEvents chkob As System.Windows.Forms.CheckBox
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents chksinglerv As System.Windows.Forms.CheckBox
    Friend WithEvents cmbVoucherTp As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkall As System.Windows.Forms.CheckBox
End Class
