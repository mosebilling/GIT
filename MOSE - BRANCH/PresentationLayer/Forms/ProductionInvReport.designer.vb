<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProductionInvReport
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
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.cmbSearch = New System.Windows.Forms.ComboBox
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.rdAll = New System.Windows.Forms.RadioButton
        Me.rdGridlist = New System.Windows.Forms.RadioButton
        Me.rdTag = New System.Windows.Forms.RadioButton
        Me.chkPending = New System.Windows.Forms.CheckBox
        Me.chkDetails = New System.Windows.Forms.CheckBox
        Me.chkFormat = New System.Windows.Forms.CheckBox
        Me.cldrEnddate = New System.Windows.Forms.DateTimePicker
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.btnLoad = New System.Windows.Forms.Button
        Me.grdvoucher = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.grdItemView = New System.Windows.Forms.DataGridView
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.grdItemsOut = New System.Windows.Forms.DataGridView
        Me.cmbsalesman = New System.Windows.Forms.ComboBox
        Me.chkserviceby = New System.Windows.Forms.CheckBox
        Me.chkItemtotal = New System.Windows.Forms.CheckBox
        Me.grpDocument = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.rdosummary = New System.Windows.Forms.RadioButton
        Me.rdoitemwise = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbbranch = New System.Windows.Forms.ComboBox
        Me.btnApply = New System.Windows.Forms.Button
        Me.btnedit = New System.Windows.Forms.Button
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.cmbitmOrder = New System.Windows.Forms.ComboBox
        Me.txtitemSearch = New System.Windows.Forms.TextBox
        Me.chkitemSearchOnly = New System.Windows.Forms.CheckBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblName = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.txtitem = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.plitem = New System.Windows.Forms.Panel
        Me.Panel6.SuspendLayout()
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.grdItemView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdItemsOut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDocument.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plitem.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel6.BackColor = System.Drawing.Color.Transparent
        Me.Panel6.Controls.Add(Me.cmbSearch)
        Me.Panel6.Controls.Add(Me.txtSearch)
        Me.Panel6.Controls.Add(Me.chkSearch)
        Me.Panel6.Location = New System.Drawing.Point(3, 267)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(493, 29)
        Me.Panel6.TabIndex = 345359
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
        Me.txtSearch.TabIndex = 89
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
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.AutoEllipsis = True
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(1021, 159)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(85, 35)
        Me.btnExit.TabIndex = 345364
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'rdAll
        '
        Me.rdAll.AutoSize = True
        Me.rdAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdAll.Location = New System.Drawing.Point(6, 19)
        Me.rdAll.Name = "rdAll"
        Me.rdAll.Size = New System.Drawing.Size(36, 17)
        Me.rdAll.TabIndex = 345367
        Me.rdAll.Tag = ""
        Me.rdAll.Text = "All"
        Me.rdAll.UseVisualStyleBackColor = True
        '
        'rdGridlist
        '
        Me.rdGridlist.AutoSize = True
        Me.rdGridlist.Checked = True
        Me.rdGridlist.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdGridlist.Location = New System.Drawing.Point(6, 55)
        Me.rdGridlist.Name = "rdGridlist"
        Me.rdGridlist.Size = New System.Drawing.Size(73, 17)
        Me.rdGridlist.TabIndex = 345366
        Me.rdGridlist.TabStop = True
        Me.rdGridlist.Tag = ""
        Me.rdGridlist.Text = "Apply  List"
        Me.rdGridlist.UseVisualStyleBackColor = True
        '
        'rdTag
        '
        Me.rdTag.AutoSize = True
        Me.rdTag.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdTag.Location = New System.Drawing.Point(6, 38)
        Me.rdTag.Name = "rdTag"
        Me.rdTag.Size = New System.Drawing.Size(44, 17)
        Me.rdTag.TabIndex = 345365
        Me.rdTag.Tag = ""
        Me.rdTag.Text = "Tag"
        Me.rdTag.UseVisualStyleBackColor = True
        '
        'chkPending
        '
        Me.chkPending.AutoSize = True
        Me.chkPending.BackColor = System.Drawing.Color.Transparent
        Me.chkPending.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPending.ForeColor = System.Drawing.Color.Black
        Me.chkPending.Location = New System.Drawing.Point(6, 19)
        Me.chkPending.Name = "chkPending"
        Me.chkPending.Size = New System.Drawing.Size(106, 19)
        Me.chkPending.TabIndex = 345356
        Me.chkPending.Text = "Show Pending"
        Me.chkPending.UseVisualStyleBackColor = False
        '
        'chkDetails
        '
        Me.chkDetails.AutoSize = True
        Me.chkDetails.BackColor = System.Drawing.Color.Transparent
        Me.chkDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDetails.Location = New System.Drawing.Point(6, 38)
        Me.chkDetails.Name = "chkDetails"
        Me.chkDetails.Size = New System.Drawing.Size(140, 19)
        Me.chkDetails.TabIndex = 345361
        Me.chkDetails.Text = "Pending With Details"
        Me.chkDetails.UseVisualStyleBackColor = False
        Me.chkDetails.Visible = False
        '
        'chkFormat
        '
        Me.chkFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(776, 165)
        Me.chkFormat.Name = "chkFormat"
        Me.chkFormat.Size = New System.Drawing.Size(65, 19)
        Me.chkFormat.TabIndex = 345358
        Me.chkFormat.Text = "&Format"
        Me.chkFormat.UseVisualStyleBackColor = False
        Me.chkFormat.Visible = False
        '
        'cldrEnddate
        '
        Me.cldrEnddate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrEnddate.Location = New System.Drawing.Point(110, 22)
        Me.cldrEnddate.Name = "cldrEnddate"
        Me.cldrEnddate.Size = New System.Drawing.Size(96, 20)
        Me.cldrEnddate.TabIndex = 345395
        Me.cldrEnddate.TabStop = False
        '
        'cldrStartDate
        '
        Me.cldrStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrStartDate.Location = New System.Drawing.Point(9, 22)
        Me.cldrStartDate.Name = "cldrStartDate"
        Me.cldrStartDate.Size = New System.Drawing.Size(95, 20)
        Me.cldrStartDate.TabIndex = 345393
        Me.cldrStartDate.TabStop = False
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.BackColor = System.Drawing.Color.SteelBlue
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.ForeColor = System.Drawing.Color.White
        Me.btnLoad.Location = New System.Drawing.Point(847, 159)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(85, 35)
        Me.btnLoad.TabIndex = 345357
        Me.btnLoad.Text = "&Load"
        Me.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLoad.UseVisualStyleBackColor = False
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
        Me.grdvoucher.Location = New System.Drawing.Point(3, 33)
        Me.grdvoucher.Name = "grdvoucher"
        Me.grdvoucher.Size = New System.Drawing.Size(1103, 230)
        Me.grdvoucher.TabIndex = 345357
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cldrEnddate)
        Me.GroupBox1.Controls.Add(Me.cldrStartDate)
        Me.GroupBox1.Location = New System.Drawing.Point(786, 96)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(215, 51)
        Me.GroupBox1.TabIndex = 345360
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Parameter"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.cmbsalesman)
        Me.Panel1.Controls.Add(Me.chkserviceby)
        Me.Panel1.Controls.Add(Me.chkItemtotal)
        Me.Panel1.Controls.Add(Me.grpDocument)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.btnLoad)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cmbbranch)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.btnApply)
        Me.Panel1.Controls.Add(Me.chkFormat)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 297)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1110, 196)
        Me.Panel1.TabIndex = 345378
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(10, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(766, 161)
        Me.TabControl1.TabIndex = 345433
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.grdItemView)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(758, 135)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Items IN"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'grdItemView
        '
        Me.grdItemView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItemView.BackgroundColor = System.Drawing.Color.White
        Me.grdItemView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdItemView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItemView.GridColor = System.Drawing.Color.Gainsboro
        Me.grdItemView.Location = New System.Drawing.Point(6, 6)
        Me.grdItemView.Name = "grdItemView"
        Me.grdItemView.Size = New System.Drawing.Size(746, 123)
        Me.grdItemView.TabIndex = 345425
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.grdItemsOut)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(758, 135)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Item Out"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'grdItemsOut
        '
        Me.grdItemsOut.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItemsOut.BackgroundColor = System.Drawing.Color.White
        Me.grdItemsOut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdItemsOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItemsOut.GridColor = System.Drawing.Color.Gainsboro
        Me.grdItemsOut.Location = New System.Drawing.Point(6, 6)
        Me.grdItemsOut.Name = "grdItemsOut"
        Me.grdItemsOut.Size = New System.Drawing.Size(746, 123)
        Me.grdItemsOut.TabIndex = 345426
        '
        'cmbsalesman
        '
        Me.cmbsalesman.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbsalesman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsalesman.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsalesman.FormattingEnabled = True
        Me.cmbsalesman.Location = New System.Drawing.Point(809, 42)
        Me.cmbsalesman.Name = "cmbsalesman"
        Me.cmbsalesman.Size = New System.Drawing.Size(110, 21)
        Me.cmbsalesman.TabIndex = 345432
        '
        'chkserviceby
        '
        Me.chkserviceby.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkserviceby.BackColor = System.Drawing.Color.Transparent
        Me.chkserviceby.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkserviceby.ForeColor = System.Drawing.Color.Black
        Me.chkserviceby.Location = New System.Drawing.Point(786, 23)
        Me.chkserviceby.Name = "chkserviceby"
        Me.chkserviceby.Size = New System.Drawing.Size(116, 19)
        Me.chkserviceby.TabIndex = 345431
        Me.chkserviceby.Text = "Sales Man wise "
        Me.chkserviceby.UseVisualStyleBackColor = False
        '
        'chkItemtotal
        '
        Me.chkItemtotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkItemtotal.BackColor = System.Drawing.Color.Transparent
        Me.chkItemtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemtotal.ForeColor = System.Drawing.Color.Black
        Me.chkItemtotal.Location = New System.Drawing.Point(786, 3)
        Me.chkItemtotal.Name = "chkItemtotal"
        Me.chkItemtotal.Size = New System.Drawing.Size(133, 19)
        Me.chkItemtotal.TabIndex = 345430
        Me.chkItemtotal.Text = "Party Wise Item Qty "
        Me.chkItemtotal.UseVisualStyleBackColor = False
        Me.chkItemtotal.Visible = False
        '
        'grpDocument
        '
        Me.grpDocument.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpDocument.Controls.Add(Me.chkPending)
        Me.grpDocument.Controls.Add(Me.chkDetails)
        Me.grpDocument.Location = New System.Drawing.Point(952, 4)
        Me.grpDocument.Name = "grpDocument"
        Me.grpDocument.Size = New System.Drawing.Size(150, 66)
        Me.grpDocument.TabIndex = 345429
        Me.grpDocument.TabStop = False
        Me.grpDocument.Text = "Document Options"
        Me.grpDocument.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.rdosummary)
        Me.GroupBox3.Controls.Add(Me.rdoitemwise)
        Me.GroupBox3.Location = New System.Drawing.Point(1009, 97)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(93, 53)
        Me.GroupBox3.TabIndex = 345428
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Report Options"
        '
        'rdosummary
        '
        Me.rdosummary.AutoSize = True
        Me.rdosummary.Checked = True
        Me.rdosummary.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdosummary.Location = New System.Drawing.Point(6, 17)
        Me.rdosummary.Name = "rdosummary"
        Me.rdosummary.Size = New System.Drawing.Size(68, 17)
        Me.rdosummary.TabIndex = 345367
        Me.rdosummary.TabStop = True
        Me.rdosummary.Tag = ""
        Me.rdosummary.Text = "Summary"
        Me.rdosummary.UseVisualStyleBackColor = True
        '
        'rdoitemwise
        '
        Me.rdoitemwise.AutoSize = True
        Me.rdoitemwise.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoitemwise.Location = New System.Drawing.Point(6, 32)
        Me.rdoitemwise.Name = "rdoitemwise"
        Me.rdoitemwise.Size = New System.Drawing.Size(69, 17)
        Me.rdoitemwise.TabIndex = 345427
        Me.rdoitemwise.Tag = ""
        Me.rdoitemwise.Text = "Item wise"
        Me.rdoitemwise.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.rdGridlist)
        Me.GroupBox2.Controls.Add(Me.rdAll)
        Me.GroupBox2.Controls.Add(Me.rdTag)
        Me.GroupBox2.Location = New System.Drawing.Point(1009, 81)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(93, 76)
        Me.GroupBox2.TabIndex = 345384
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Options By"
        Me.GroupBox2.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(990, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 345380
        Me.Label1.Text = "Branch"
        Me.Label1.Visible = False
        '
        'cmbbranch
        '
        Me.cmbbranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbbranch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbbranch.FormattingEnabled = True
        Me.cmbbranch.Location = New System.Drawing.Point(1040, 76)
        Me.cmbbranch.Name = "cmbbranch"
        Me.cmbbranch.Size = New System.Drawing.Size(119, 21)
        Me.cmbbranch.TabIndex = 345379
        Me.cmbbranch.Visible = False
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
        Me.btnApply.Location = New System.Drawing.Point(934, 159)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(85, 35)
        Me.btnApply.TabIndex = 345365
        Me.btnApply.Text = "Preview"
        Me.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnApply.UseVisualStyleBackColor = False
        '
        'btnedit
        '
        Me.btnedit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnedit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnedit.Location = New System.Drawing.Point(1045, 267)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(61, 29)
        Me.btnedit.TabIndex = 93
        Me.btnedit.Text = "Edit"
        Me.btnedit.UseVisualStyleBackColor = True
        Me.btnedit.Visible = False
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel4.Controls.Add(Me.cmbitmOrder)
        Me.Panel4.Controls.Add(Me.txtitemSearch)
        Me.Panel4.Controls.Add(Me.chkitemSearchOnly)
        Me.Panel4.Location = New System.Drawing.Point(8, 464)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(493, 29)
        Me.Panel4.TabIndex = 345425
        '
        'cmbitmOrder
        '
        Me.cmbitmOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbitmOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbitmOrder.FormattingEnabled = True
        Me.cmbitmOrder.Location = New System.Drawing.Point(5, 3)
        Me.cmbitmOrder.Name = "cmbitmOrder"
        Me.cmbitmOrder.Size = New System.Drawing.Size(143, 21)
        Me.cmbitmOrder.TabIndex = 91
        '
        'txtitemSearch
        '
        Me.txtitemSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtitemSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtitemSearch.Location = New System.Drawing.Point(154, 3)
        Me.txtitemSearch.Name = "txtitemSearch"
        Me.txtitemSearch.Size = New System.Drawing.Size(191, 20)
        Me.txtitemSearch.TabIndex = 89
        '
        'chkitemSearchOnly
        '
        Me.chkitemSearchOnly.AutoSize = True
        Me.chkitemSearchOnly.BackColor = System.Drawing.Color.Transparent
        Me.chkitemSearchOnly.ForeColor = System.Drawing.Color.Black
        Me.chkitemSearchOnly.Location = New System.Drawing.Point(351, 4)
        Me.chkitemSearchOnly.Name = "chkitemSearchOnly"
        Me.chkitemSearchOnly.Size = New System.Drawing.Size(143, 17)
        Me.chkitemSearchOnly.TabIndex = 92
        Me.chkitemSearchOnly.Text = "Search 'Starts With' Only"
        Me.chkitemSearchOnly.UseVisualStyleBackColor = False
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
        Me.Panel2.Size = New System.Drawing.Size(1110, 32)
        Me.Panel2.TabIndex = 345463
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.White
        Me.lblName.Font = New System.Drawing.Font("Goudy Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblName.Location = New System.Drawing.Point(41, 5)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(181, 21)
        Me.lblName.TabIndex = 345458
        Me.lblName.Text = "ACCESSORIES MASTER"
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
        'Timer1
        '
        Me.Timer1.Interval = 200
        '
        'txtitem
        '
        Me.txtitem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtitem.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtitem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtitem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtitem.Location = New System.Drawing.Point(71, 5)
        Me.txtitem.Name = "txtitem"
        Me.txtitem.ReadOnly = True
        Me.txtitem.Size = New System.Drawing.Size(300, 20)
        Me.txtitem.TabIndex = 345464
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(4, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 345465
        Me.Label2.Text = "Choose Item"
        '
        'plitem
        '
        Me.plitem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.plitem.BackColor = System.Drawing.Color.Transparent
        Me.plitem.Controls.Add(Me.Label2)
        Me.plitem.Controls.Add(Me.txtitem)
        Me.plitem.Location = New System.Drawing.Point(730, 264)
        Me.plitem.Name = "plitem"
        Me.plitem.Size = New System.Drawing.Size(376, 31)
        Me.plitem.TabIndex = 345466
        Me.plitem.Visible = False
        '
        'ProductionInvReport
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1110, 493)
        Me.ControlBox = False
        Me.Controls.Add(Me.plitem)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.btnedit)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grdvoucher)
        Me.Controls.Add(Me.Panel6)
        Me.Name = "ProductionInvReport"
        Me.Text = "Invoice Summary"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.grdItemView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.grdItemsOut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDocument.ResumeLayout(False)
        Me.grpDocument.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plitem.ResumeLayout(False)
        Me.plitem.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents cmbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents rdAll As System.Windows.Forms.RadioButton
    Friend WithEvents rdGridlist As System.Windows.Forms.RadioButton
    Friend WithEvents rdTag As System.Windows.Forms.RadioButton
    Friend WithEvents chkPending As System.Windows.Forms.CheckBox
    Friend WithEvents chkDetails As System.Windows.Forms.CheckBox
    Friend WithEvents chkFormat As System.Windows.Forms.CheckBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents grdvoucher As System.Windows.Forms.DataGridView
    Friend WithEvents cldrEnddate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbbranch As System.Windows.Forms.ComboBox
    Friend WithEvents btnedit As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdItemView As System.Windows.Forms.DataGridView
    Friend WithEvents rdosummary As System.Windows.Forms.RadioButton
    Friend WithEvents grpDocument As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoitemwise As System.Windows.Forms.RadioButton
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents cmbitmOrder As System.Windows.Forms.ComboBox
    Friend WithEvents txtitemSearch As System.Windows.Forms.TextBox
    Friend WithEvents chkitemSearchOnly As System.Windows.Forms.CheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents chkItemtotal As System.Windows.Forms.CheckBox
    Friend WithEvents chkserviceby As System.Windows.Forms.CheckBox
    Friend WithEvents cmbsalesman As System.Windows.Forms.ComboBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents txtitem As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents plitem As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents grdItemsOut As System.Windows.Forms.DataGridView
End Class
