<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QtyReport
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
    Public Sub New()
        IsInitializing = True
        InitializeComponent()
        IsInitializing = False
    End Sub
    Private IsInitializing As Boolean
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QtyReport))
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.chkGlobalsearch = New System.Windows.Forms.CheckBox
        Me.cmbSearch = New System.Windows.Forms.ComboBox
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.cmbQIH = New System.Windows.Forms.ComboBox
        Me.chkLevelWise = New System.Windows.Forms.CheckBox
        Me.rdAll = New System.Windows.Forms.RadioButton
        Me.rdGridlist = New System.Windows.Forms.RadioButton
        Me.rdTag = New System.Windows.Forms.RadioButton
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.btnTo = New System.Windows.Forms.Button
        Me.btnFP = New System.Windows.Forms.Button
        Me.rdBoth = New System.Windows.Forms.RadioButton
        Me.rdService = New System.Windows.Forms.RadioButton
        Me.rdStock = New System.Windows.Forms.RadioButton
        Me.grdvoucher = New System.Windows.Forms.DataGridView
        Me.ldTimer = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpto = New System.Windows.Forms.DateTimePicker
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.rdoselect = New System.Windows.Forms.RadioButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.rdomenu = New System.Windows.Forms.RadioButton
        Me.lstVouchers = New System.Windows.Forms.ListBox
        Me.chknoncost = New System.Windows.Forms.CheckBox
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdLevel = New System.Windows.Forms.DataGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkcsv = New System.Windows.Forms.CheckBox
        Me.chkFormat = New System.Windows.Forms.CheckBox
        Me.chkbillingmech = New System.Windows.Forms.CheckBox
        Me.btnexport = New System.Windows.Forms.Button
        Me.lbltotalValue = New System.Windows.Forms.Label
        Me.btnLedger = New System.Windows.Forms.Button
        Me.grdstockLedger = New System.Windows.Forms.DataGridView
        Me.pnlstockledger = New System.Windows.Forms.Panel
        Me.lblIN = New System.Windows.Forms.Label
        Me.btnclose = New System.Windows.Forms.Button
        Me.panellevel = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label7 = New System.Windows.Forms.Label
        Me.picCloseProd = New System.Windows.Forms.PictureBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblName = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.chkbatchwise = New System.Windows.Forms.CheckBox
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.chklocationwise = New System.Windows.Forms.CheckBox
        Me.pllocation = New System.Windows.Forms.Panel
        Me.chklocations = New System.Windows.Forms.CheckedListBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.PictureBox4 = New System.Windows.Forms.PictureBox
        Me.PictureBox5 = New System.Windows.Forms.PictureBox
        Me.PictureBox6 = New System.Windows.Forms.PictureBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.cmbitemtype = New System.Windows.Forms.ComboBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.grdsupersed = New System.Windows.Forms.DataGridView
        Me.grdLocation = New System.Windows.Forms.DataGridView
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblbranch = New System.Windows.Forms.Label
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel6.SuspendLayout()
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.grdLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.grdstockLedger, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlstockledger.SuspendLayout()
        Me.panellevel.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.picCloseProd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pllocation.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdsupersed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel6.BackColor = System.Drawing.Color.Transparent
        Me.Panel6.Controls.Add(Me.chkGlobalsearch)
        Me.Panel6.Controls.Add(Me.cmbSearch)
        Me.Panel6.Controls.Add(Me.txtSearch)
        Me.Panel6.Controls.Add(Me.chkSearch)
        Me.Panel6.Location = New System.Drawing.Point(193, 416)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(641, 23)
        Me.Panel6.TabIndex = 345360
        '
        'chkGlobalsearch
        '
        Me.chkGlobalsearch.AutoSize = True
        Me.chkGlobalsearch.BackColor = System.Drawing.Color.Transparent
        Me.chkGlobalsearch.ForeColor = System.Drawing.Color.Black
        Me.chkGlobalsearch.Location = New System.Drawing.Point(359, 33)
        Me.chkGlobalsearch.Name = "chkGlobalsearch"
        Me.chkGlobalsearch.Size = New System.Drawing.Size(259, 17)
        Me.chkGlobalsearch.TabIndex = 108
        Me.chkGlobalsearch.Text = "Global Search for[Item Code,Item Name,Barcode]"
        Me.chkGlobalsearch.UseVisualStyleBackColor = False
        Me.chkGlobalsearch.Visible = False
        '
        'cmbSearch
        '
        Me.cmbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSearch.FormattingEnabled = True
        Me.cmbSearch.Location = New System.Drawing.Point(0, 0)
        Me.cmbSearch.Name = "cmbSearch"
        Me.cmbSearch.Size = New System.Drawing.Size(203, 21)
        Me.cmbSearch.TabIndex = 91
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(209, 1)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(261, 20)
        Me.txtSearch.TabIndex = 89
        '
        'chkSearch
        '
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(475, 2)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 92
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
        '
        'cmbQIH
        '
        Me.cmbQIH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbQIH.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbQIH.FormattingEnabled = True
        Me.cmbQIH.Items.AddRange(New Object() {"ALL", "POSITIVE", "NEGATIVE", "ZERO", "ZERO+POSITIVE", "ZERO+NEGATIVE", "NON ZERO"})
        Me.cmbQIH.Location = New System.Drawing.Point(18, 297)
        Me.cmbQIH.Name = "cmbQIH"
        Me.cmbQIH.Size = New System.Drawing.Size(127, 21)
        Me.cmbQIH.TabIndex = 93
        '
        'chkLevelWise
        '
        Me.chkLevelWise.AutoSize = True
        Me.chkLevelWise.BackColor = System.Drawing.Color.Transparent
        Me.chkLevelWise.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLevelWise.ForeColor = System.Drawing.Color.Black
        Me.chkLevelWise.Location = New System.Drawing.Point(12, 465)
        Me.chkLevelWise.Name = "chkLevelWise"
        Me.chkLevelWise.Size = New System.Drawing.Size(85, 19)
        Me.chkLevelWise.TabIndex = 345356
        Me.chkLevelWise.Text = "&Level Wise"
        Me.chkLevelWise.UseVisualStyleBackColor = False
        '
        'rdAll
        '
        Me.rdAll.AutoSize = True
        Me.rdAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdAll.Location = New System.Drawing.Point(6, 18)
        Me.rdAll.Name = "rdAll"
        Me.rdAll.Size = New System.Drawing.Size(38, 19)
        Me.rdAll.TabIndex = 345367
        Me.rdAll.Tag = ""
        Me.rdAll.Text = "All"
        Me.rdAll.UseVisualStyleBackColor = True
        '
        'rdGridlist
        '
        Me.rdGridlist.AutoSize = True
        Me.rdGridlist.Checked = True
        Me.rdGridlist.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdGridlist.Location = New System.Drawing.Point(6, 38)
        Me.rdGridlist.Name = "rdGridlist"
        Me.rdGridlist.Size = New System.Drawing.Size(102, 19)
        Me.rdGridlist.TabIndex = 345366
        Me.rdGridlist.TabStop = True
        Me.rdGridlist.Tag = ""
        Me.rdGridlist.Text = "Apply Grid List"
        Me.rdGridlist.UseVisualStyleBackColor = True
        '
        'rdTag
        '
        Me.rdTag.AutoSize = True
        Me.rdTag.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdTag.Location = New System.Drawing.Point(45, 18)
        Me.rdTag.Name = "rdTag"
        Me.rdTag.Size = New System.Drawing.Size(46, 19)
        Me.rdTag.TabIndex = 345365
        Me.rdTag.Tag = ""
        Me.rdTag.Text = "Tag"
        Me.rdTag.UseVisualStyleBackColor = True
        '
        'cldrStartDate
        '
        Me.cldrStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrStartDate.Location = New System.Drawing.Point(11, 30)
        Me.cldrStartDate.Name = "cldrStartDate"
        Me.cldrStartDate.Size = New System.Drawing.Size(95, 20)
        Me.cldrStartDate.TabIndex = 345395
        Me.cldrStartDate.TabStop = False
        '
        'btnTo
        '
        Me.btnTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTo.Location = New System.Drawing.Point(118, 36)
        Me.btnTo.Name = "btnTo"
        Me.btnTo.Size = New System.Drawing.Size(30, 23)
        Me.btnTo.TabIndex = 345365
        Me.btnTo.Text = "TD"
        Me.ToolTip1.SetToolTip(Me.btnTo, "Current Date")
        Me.btnTo.UseVisualStyleBackColor = True
        Me.btnTo.Visible = False
        '
        'btnFP
        '
        Me.btnFP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFP.Location = New System.Drawing.Point(118, 14)
        Me.btnFP.Name = "btnFP"
        Me.btnFP.Size = New System.Drawing.Size(30, 23)
        Me.btnFP.TabIndex = 345364
        Me.btnFP.Text = "FP"
        Me.ToolTip1.SetToolTip(Me.btnFP, "Starting Date")
        Me.btnFP.UseVisualStyleBackColor = True
        Me.btnFP.Visible = False
        '
        'rdBoth
        '
        Me.rdBoth.AutoSize = True
        Me.rdBoth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdBoth.Location = New System.Drawing.Point(96, 35)
        Me.rdBoth.Name = "rdBoth"
        Me.rdBoth.Size = New System.Drawing.Size(38, 19)
        Me.rdBoth.TabIndex = 345370
        Me.rdBoth.Tag = ""
        Me.rdBoth.Text = "All"
        Me.rdBoth.UseVisualStyleBackColor = True
        Me.rdBoth.Visible = False
        '
        'rdService
        '
        Me.rdService.AutoSize = True
        Me.rdService.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdService.Location = New System.Drawing.Point(59, 16)
        Me.rdService.Name = "rdService"
        Me.rdService.Size = New System.Drawing.Size(65, 19)
        Me.rdService.TabIndex = 345369
        Me.rdService.Tag = ""
        Me.rdService.Text = "Service"
        Me.rdService.UseVisualStyleBackColor = True
        '
        'rdStock
        '
        Me.rdStock.AutoSize = True
        Me.rdStock.Checked = True
        Me.rdStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdStock.Location = New System.Drawing.Point(6, 16)
        Me.rdStock.Name = "rdStock"
        Me.rdStock.Size = New System.Drawing.Size(58, 19)
        Me.rdStock.TabIndex = 345368
        Me.rdStock.TabStop = True
        Me.rdStock.Tag = ""
        Me.rdStock.Text = "Stock "
        Me.rdStock.UseVisualStyleBackColor = True
        '
        'grdvoucher
        '
        Me.grdvoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdvoucher.BackgroundColor = System.Drawing.Color.FloralWhite
        Me.grdvoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdvoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdvoucher.Location = New System.Drawing.Point(193, 43)
        Me.grdvoucher.Name = "grdvoucher"
        Me.grdvoucher.Size = New System.Drawing.Size(686, 370)
        Me.grdvoucher.TabIndex = 345357
        '
        'ldTimer
        '
        Me.ldTimer.Interval = 500
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpto)
        Me.GroupBox1.Controls.Add(Me.btnTo)
        Me.GroupBox1.Controls.Add(Me.cldrStartDate)
        Me.GroupBox1.Controls.Add(Me.btnFP)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 187)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(175, 91)
        Me.GroupBox1.TabIndex = 345361
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Parameter"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 345428
        Me.Label3.Text = "Date From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 13)
        Me.Label2.TabIndex = 345427
        Me.Label2.Text = "As On Date / Date To"
        '
        'dtpto
        '
        Me.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpto.Location = New System.Drawing.Point(11, 67)
        Me.dtpto.Name = "dtpto"
        Me.dtpto.Size = New System.Drawing.Size(95, 20)
        Me.dtpto.TabIndex = 345396
        Me.dtpto.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.rdoselect)
        Me.GroupBox2.Controls.Add(Me.rdGridlist)
        Me.GroupBox2.Controls.Add(Me.rdAll)
        Me.GroupBox2.Controls.Add(Me.rdTag)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 376)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(162, 83)
        Me.GroupBox2.TabIndex = 345369
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Search By"
        '
        'rdoselect
        '
        Me.rdoselect.AutoSize = True
        Me.rdoselect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoselect.Location = New System.Drawing.Point(6, 57)
        Me.rdoselect.Name = "rdoselect"
        Me.rdoselect.Size = New System.Drawing.Size(76, 19)
        Me.rdoselect.TabIndex = 345368
        Me.rdoselect.Tag = ""
        Me.rdoselect.Text = "Selection"
        Me.rdoselect.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.rdomenu)
        Me.GroupBox3.Controls.Add(Me.rdBoth)
        Me.GroupBox3.Controls.Add(Me.rdService)
        Me.GroupBox3.Controls.Add(Me.rdStock)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 126)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(176, 58)
        Me.GroupBox3.TabIndex = 345370
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Items"
        '
        'rdomenu
        '
        Me.rdomenu.AutoSize = True
        Me.rdomenu.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdomenu.Location = New System.Drawing.Point(6, 35)
        Me.rdomenu.Name = "rdomenu"
        Me.rdomenu.Size = New System.Drawing.Size(84, 19)
        Me.rdomenu.TabIndex = 345371
        Me.rdomenu.Tag = ""
        Me.rdomenu.Text = "Menu Item"
        Me.rdomenu.UseVisualStyleBackColor = True
        Me.rdomenu.Visible = False
        '
        'lstVouchers
        '
        Me.lstVouchers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstVouchers.FormattingEnabled = True
        Me.lstVouchers.ItemHeight = 16
        Me.lstVouchers.Items.AddRange(New Object() {"Quantity In Hand as on Date", "Opening Quantity", "Re-Order List", "Item Price List"})
        Me.lstVouchers.Location = New System.Drawing.Point(8, 43)
        Me.lstVouchers.Name = "lstVouchers"
        Me.lstVouchers.Size = New System.Drawing.Size(180, 84)
        Me.lstVouchers.TabIndex = 345371
        '
        'chknoncost
        '
        Me.chknoncost.AutoSize = True
        Me.chknoncost.BackColor = System.Drawing.Color.Transparent
        Me.chknoncost.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chknoncost.ForeColor = System.Drawing.Color.Black
        Me.chknoncost.Location = New System.Drawing.Point(12, 525)
        Me.chknoncost.Name = "chknoncost"
        Me.chknoncost.Size = New System.Drawing.Size(109, 19)
        Me.chknoncost.TabIndex = 345375
        Me.chknoncost.Text = "Non Cost Items"
        Me.chknoncost.UseVisualStyleBackColor = False
        Me.chknoncost.Visible = False
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.AutoEllipsis = True
        Me.btnPreview.BackColor = System.Drawing.Color.SteelBlue
        Me.btnPreview.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.White
        Me.btnPreview.Location = New System.Drawing.Point(686, 1)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(96, 35)
        Me.btnPreview.TabIndex = 345373
        Me.btnPreview.Text = "&Preview"
        Me.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPreview.UseVisualStyleBackColor = False
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
        Me.btnExit.Location = New System.Drawing.Point(783, 1)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(96, 35)
        Me.btnExit.TabIndex = 345364
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.AutoEllipsis = True
        Me.btnRefresh.BackColor = System.Drawing.Color.SteelBlue
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(514, 1)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(96, 35)
        Me.btnRefresh.TabIndex = 345366
        Me.btnRefresh.Text = "&Load"
        Me.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(15, 281)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 345426
        Me.Label1.Text = "Qty With"
        '
        'grdLevel
        '
        Me.grdLevel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLevel.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdLevel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdLevel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdLevel.Location = New System.Drawing.Point(7, 34)
        Me.grdLevel.Name = "grdLevel"
        Me.grdLevel.Size = New System.Drawing.Size(289, 103)
        Me.grdLevel.TabIndex = 345427
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.chkcsv)
        Me.Panel1.Controls.Add(Me.chkFormat)
        Me.Panel1.Controls.Add(Me.chkbillingmech)
        Me.Panel1.Controls.Add(Me.btnexport)
        Me.Panel1.Controls.Add(Me.lbltotalValue)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.btnLedger)
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 629)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(882, 38)
        Me.Panel1.TabIndex = 345444
        '
        'chkcsv
        '
        Me.chkcsv.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkcsv.AutoSize = True
        Me.chkcsv.BackColor = System.Drawing.Color.Transparent
        Me.chkcsv.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkcsv.ForeColor = System.Drawing.Color.Black
        Me.chkcsv.Location = New System.Drawing.Point(277, 16)
        Me.chkcsv.Name = "chkcsv"
        Me.chkcsv.Size = New System.Drawing.Size(66, 19)
        Me.chkcsv.TabIndex = 345452
        Me.chkcsv.Text = "To CSV"
        Me.chkcsv.UseVisualStyleBackColor = False
        '
        'chkFormat
        '
        Me.chkFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(620, 6)
        Me.chkFormat.Name = "chkFormat"
        Me.chkFormat.Size = New System.Drawing.Size(65, 19)
        Me.chkFormat.TabIndex = 345451
        Me.chkFormat.Text = "&Format"
        Me.chkFormat.UseVisualStyleBackColor = False
        '
        'chkbillingmech
        '
        Me.chkbillingmech.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkbillingmech.AutoSize = True
        Me.chkbillingmech.BackColor = System.Drawing.Color.Transparent
        Me.chkbillingmech.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbillingmech.ForeColor = System.Drawing.Color.Black
        Me.chkbillingmech.Location = New System.Drawing.Point(277, 0)
        Me.chkbillingmech.Name = "chkbillingmech"
        Me.chkbillingmech.Size = New System.Drawing.Size(73, 19)
        Me.chkbillingmech.TabIndex = 345450
        Me.chkbillingmech.Text = "To Excel"
        Me.chkbillingmech.UseVisualStyleBackColor = False
        '
        'btnexport
        '
        Me.btnexport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexport.AutoEllipsis = True
        Me.btnexport.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexport.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnexport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexport.ForeColor = System.Drawing.Color.White
        Me.btnexport.Location = New System.Drawing.Point(349, 1)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(67, 35)
        Me.btnexport.TabIndex = 345449
        Me.btnexport.Text = "Export"
        Me.btnexport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexport.UseVisualStyleBackColor = False
        '
        'lbltotalValue
        '
        Me.lbltotalValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbltotalValue.AutoSize = True
        Me.lbltotalValue.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalValue.ForeColor = System.Drawing.Color.Black
        Me.lbltotalValue.Location = New System.Drawing.Point(15, 11)
        Me.lbltotalValue.Name = "lbltotalValue"
        Me.lbltotalValue.Size = New System.Drawing.Size(40, 16)
        Me.lbltotalValue.TabIndex = 345448
        Me.lbltotalValue.Text = "0.00"
        '
        'btnLedger
        '
        Me.btnLedger.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLedger.AutoEllipsis = True
        Me.btnLedger.BackColor = System.Drawing.Color.SteelBlue
        Me.btnLedger.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLedger.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLedger.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLedger.ForeColor = System.Drawing.Color.White
        Me.btnLedger.Location = New System.Drawing.Point(417, 1)
        Me.btnLedger.Name = "btnLedger"
        Me.btnLedger.Size = New System.Drawing.Size(96, 35)
        Me.btnLedger.TabIndex = 345447
        Me.btnLedger.Text = "Stock Ledger"
        Me.btnLedger.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLedger.UseVisualStyleBackColor = False
        '
        'grdstockLedger
        '
        Me.grdstockLedger.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdstockLedger.BackgroundColor = System.Drawing.Color.FloralWhite
        Me.grdstockLedger.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdstockLedger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdstockLedger.Location = New System.Drawing.Point(11, 7)
        Me.grdstockLedger.Name = "grdstockLedger"
        Me.grdstockLedger.Size = New System.Drawing.Size(665, 155)
        Me.grdstockLedger.TabIndex = 345445
        '
        'pnlstockledger
        '
        Me.pnlstockledger.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlstockledger.BackColor = System.Drawing.Color.Transparent
        Me.pnlstockledger.Controls.Add(Me.lblIN)
        Me.pnlstockledger.Controls.Add(Me.btnclose)
        Me.pnlstockledger.Controls.Add(Me.grdstockLedger)
        Me.pnlstockledger.Location = New System.Drawing.Point(193, 38)
        Me.pnlstockledger.Name = "pnlstockledger"
        Me.pnlstockledger.Size = New System.Drawing.Size(687, 203)
        Me.pnlstockledger.TabIndex = 345446
        Me.pnlstockledger.Visible = False
        '
        'lblIN
        '
        Me.lblIN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblIN.AutoSize = True
        Me.lblIN.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIN.ForeColor = System.Drawing.Color.Black
        Me.lblIN.Location = New System.Drawing.Point(14, 180)
        Me.lblIN.Name = "lblIN"
        Me.lblIN.Size = New System.Drawing.Size(40, 16)
        Me.lblIN.TabIndex = 345447
        Me.lblIN.Text = "0.00"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.AutoEllipsis = True
        Me.btnclose.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ForeColor = System.Drawing.Color.White
        Me.btnclose.Location = New System.Drawing.Point(618, 165)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(58, 35)
        Me.btnclose.TabIndex = 345446
        Me.btnclose.Text = "Close"
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'panellevel
        '
        Me.panellevel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.panellevel.Controls.Add(Me.Panel3)
        Me.panellevel.Controls.Add(Me.grdLevel)
        Me.panellevel.Location = New System.Drawing.Point(193, 479)
        Me.panellevel.Name = "panellevel"
        Me.panellevel.Size = New System.Drawing.Size(299, 144)
        Me.panellevel.TabIndex = 345448
        Me.panellevel.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.BackgroundImage = Global.SMSMP.My.Resources.Resources.top
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.picCloseProd)
        Me.Panel3.Controls.Add(Me.PictureBox1)
        Me.Panel3.Controls.Add(Me.PictureBox3)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(299, 32)
        Me.Panel3.TabIndex = 345446
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.DimGray
        Me.Label7.Location = New System.Drawing.Point(31, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(98, 16)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Select Group"
        '
        'picCloseProd
        '
        Me.picCloseProd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picCloseProd.BackColor = System.Drawing.Color.Transparent
        Me.picCloseProd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picCloseProd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picCloseProd.Image = CType(resources.GetObject("picCloseProd.Image"), System.Drawing.Image)
        Me.picCloseProd.Location = New System.Drawing.Point(272, 8)
        Me.picCloseProd.Name = "picCloseProd"
        Me.picCloseProd.Size = New System.Drawing.Size(19, 17)
        Me.picCloseProd.TabIndex = 345357
        Me.picCloseProd.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = Global.SMSMP.My.Resources.Resources.CloseButton
        Me.PictureBox1.Location = New System.Drawing.Point(460, 9)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(12, 12)
        Me.PictureBox1.TabIndex = 345356
        Me.PictureBox1.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox3.BackgroundImage = Global.SMSMP.My.Resources.Resources.search
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox3.Location = New System.Drawing.Point(4, 5)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(27, 23)
        Me.PictureBox3.TabIndex = 27
        Me.PictureBox3.TabStop = False
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
        Me.Panel2.Size = New System.Drawing.Size(882, 32)
        Me.Panel2.TabIndex = 345464
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.White
        Me.lblName.Font = New System.Drawing.Font("Goudy Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblName.Location = New System.Drawing.Point(41, 5)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(161, 21)
        Me.lblName.TabIndex = 345458
        Me.lblName.Text = "QUANTITY REPORT"
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
        'chkbatchwise
        '
        Me.chkbatchwise.AutoSize = True
        Me.chkbatchwise.BackColor = System.Drawing.Color.Transparent
        Me.chkbatchwise.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkbatchwise.ForeColor = System.Drawing.Color.Black
        Me.chkbatchwise.Location = New System.Drawing.Point(12, 505)
        Me.chkbatchwise.Name = "chkbatchwise"
        Me.chkbatchwise.Size = New System.Drawing.Size(87, 19)
        Me.chkbatchwise.TabIndex = 345465
        Me.chkbatchwise.Text = "Batch Wise"
        Me.chkbatchwise.UseVisualStyleBackColor = False
        Me.chkbatchwise.Visible = False
        '
        'chklocationwise
        '
        Me.chklocationwise.AutoSize = True
        Me.chklocationwise.BackColor = System.Drawing.Color.Transparent
        Me.chklocationwise.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chklocationwise.ForeColor = System.Drawing.Color.Black
        Me.chklocationwise.Location = New System.Drawing.Point(12, 485)
        Me.chklocationwise.Name = "chklocationwise"
        Me.chklocationwise.Size = New System.Drawing.Size(103, 19)
        Me.chklocationwise.TabIndex = 345466
        Me.chklocationwise.Text = "Location Wise"
        Me.chklocationwise.UseVisualStyleBackColor = False
        '
        'pllocation
        '
        Me.pllocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pllocation.Controls.Add(Me.chklocations)
        Me.pllocation.Controls.Add(Me.Panel5)
        Me.pllocation.Location = New System.Drawing.Point(193, 265)
        Me.pllocation.Name = "pllocation"
        Me.pllocation.Size = New System.Drawing.Size(299, 210)
        Me.pllocation.TabIndex = 345467
        Me.pllocation.Visible = False
        '
        'chklocations
        '
        Me.chklocations.CheckOnClick = True
        Me.chklocations.FormattingEnabled = True
        Me.chklocations.Location = New System.Drawing.Point(6, 37)
        Me.chklocations.Name = "chklocations"
        Me.chklocations.Size = New System.Drawing.Size(289, 169)
        Me.chklocations.TabIndex = 345447
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.BackgroundImage = Global.SMSMP.My.Resources.Resources.top
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Controls.Add(Me.PictureBox4)
        Me.Panel5.Controls.Add(Me.PictureBox5)
        Me.Panel5.Controls.Add(Me.PictureBox6)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(299, 32)
        Me.Panel5.TabIndex = 345446
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.DimGray
        Me.Label4.Location = New System.Drawing.Point(31, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 16)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Select Locations"
        '
        'PictureBox4
        '
        Me.PictureBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox4.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(272, 8)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(19, 17)
        Me.PictureBox4.TabIndex = 345357
        Me.PictureBox4.TabStop = False
        '
        'PictureBox5
        '
        Me.PictureBox5.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox5.Image = Global.SMSMP.My.Resources.Resources.CloseButton
        Me.PictureBox5.Location = New System.Drawing.Point(460, 9)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(12, 12)
        Me.PictureBox5.TabIndex = 345356
        Me.PictureBox5.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox6.BackgroundImage = Global.SMSMP.My.Resources.Resources.search
        Me.PictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox6.Location = New System.Drawing.Point(4, 5)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(27, 23)
        Me.PictureBox6.TabIndex = 27
        Me.PictureBox6.TabStop = False
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Location = New System.Drawing.Point(15, 321)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(31, 13)
        Me.Label32.TabIndex = 345471
        Me.Label32.Text = "Type"
        '
        'cmbitemtype
        '
        Me.cmbitemtype.BackColor = System.Drawing.SystemColors.Window
        Me.cmbitemtype.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbitemtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbitemtype.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbitemtype.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbitemtype.Items.AddRange(New Object() {"Finished Goods", "Raw Material", "All"})
        Me.cmbitemtype.Location = New System.Drawing.Point(18, 337)
        Me.cmbitemtype.Name = "cmbitemtype"
        Me.cmbitemtype.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbitemtype.Size = New System.Drawing.Size(127, 22)
        Me.cmbitemtype.TabIndex = 345470
        Me.cmbitemtype.TabStop = False
        '
        'Timer1
        '
        '
        'grdsupersed
        '
        Me.grdsupersed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdsupersed.BackgroundColor = System.Drawing.Color.FloralWhite
        Me.grdsupersed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdsupersed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdsupersed.Location = New System.Drawing.Point(193, 465)
        Me.grdsupersed.Name = "grdsupersed"
        Me.grdsupersed.Size = New System.Drawing.Size(299, 159)
        Me.grdsupersed.TabIndex = 345472
        '
        'grdLocation
        '
        Me.grdLocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLocation.BackgroundColor = System.Drawing.Color.FloralWhite
        Me.grdLocation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdLocation.Location = New System.Drawing.Point(498, 465)
        Me.grdLocation.Name = "grdLocation"
        Me.grdLocation.Size = New System.Drawing.Size(382, 159)
        Me.grdLocation.TabIndex = 345473
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(190, 446)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 13)
        Me.Label5.TabIndex = 345474
        Me.Label5.Text = "Supersed Item"
        '
        'lblbranch
        '
        Me.lblbranch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblbranch.AutoSize = True
        Me.lblbranch.BackColor = System.Drawing.Color.Transparent
        Me.lblbranch.Location = New System.Drawing.Point(580, 446)
        Me.lblbranch.Name = "lblbranch"
        Me.lblbranch.Size = New System.Drawing.Size(112, 13)
        Me.lblbranch.TabIndex = 345475
        Me.lblbranch.Text = "Branch / Location Qty"
        '
        'Timer2
        '
        Me.Timer2.Interval = 200
        '
        'QtyReport
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(882, 667)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlstockledger)
        Me.Controls.Add(Me.panellevel)
        Me.Controls.Add(Me.pllocation)
        Me.Controls.Add(Me.lblbranch)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.grdLocation)
        Me.Controls.Add(Me.grdsupersed)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.cmbitemtype)
        Me.Controls.Add(Me.chklocationwise)
        Me.Controls.Add(Me.chkbatchwise)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.chkLevelWise)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbQIH)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.chknoncost)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.grdvoucher)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.lstVouchers)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "QtyReport"
        Me.Text = "Qty Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.grdLevel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.grdstockLedger, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlstockledger.ResumeLayout(False)
        Me.pnlstockledger.PerformLayout()
        Me.panellevel.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.picCloseProd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pllocation.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdsupersed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents cmbQIH As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents chkLevelWise As System.Windows.Forms.CheckBox
    Friend WithEvents rdAll As System.Windows.Forms.RadioButton
    Friend WithEvents rdGridlist As System.Windows.Forms.RadioButton
    Friend WithEvents rdTag As System.Windows.Forms.RadioButton
    Friend WithEvents btnTo As System.Windows.Forms.Button
    Friend WithEvents btnFP As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents rdBoth As System.Windows.Forms.RadioButton
    Friend WithEvents rdService As System.Windows.Forms.RadioButton
    Friend WithEvents rdStock As System.Windows.Forms.RadioButton
    Friend WithEvents grdvoucher As System.Windows.Forms.DataGridView
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkGlobalsearch As System.Windows.Forms.CheckBox
    Friend WithEvents ldTimer As System.Windows.Forms.Timer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lstVouchers As System.Windows.Forms.ListBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents chknoncost As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdLevel As System.Windows.Forms.DataGridView
    Friend WithEvents dtpto As System.Windows.Forms.DateTimePicker
    Friend WithEvents rdoselect As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grdstockLedger As System.Windows.Forms.DataGridView
    Friend WithEvents pnlstockledger As System.Windows.Forms.Panel
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents btnLedger As System.Windows.Forms.Button
    Friend WithEvents panellevel As System.Windows.Forms.Panel
    Friend WithEvents picCloseProd As System.Windows.Forms.PictureBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents lblIN As System.Windows.Forms.Label
    Friend WithEvents lbltotalValue As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rdomenu As System.Windows.Forms.RadioButton
    Friend WithEvents chkbatchwise As System.Windows.Forms.CheckBox
    Friend WithEvents btnexport As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents chkbillingmech As System.Windows.Forms.CheckBox
    Friend WithEvents chklocationwise As System.Windows.Forms.CheckBox
    Friend WithEvents pllocation As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents chklocations As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents cmbitemtype As System.Windows.Forms.ComboBox
    Friend WithEvents chkFormat As System.Windows.Forms.CheckBox
    Friend WithEvents chkcsv As System.Windows.Forms.CheckBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents grdsupersed As System.Windows.Forms.DataGridView
    Friend WithEvents grdLocation As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblbranch As System.Windows.Forms.Label
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
End Class
