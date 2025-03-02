<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AmcList
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.amcpanel = New System.Windows.Forms.Panel
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtcustomer = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cldrdate = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtinvoicenmr = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnamcremove = New System.Windows.Forms.Button
        Me.btnamcadd = New System.Windows.Forms.Button
        Me.drgv = New System.Windows.Forms.DataGridView
        Me.txtitem = New System.Windows.Forms.TextBox
        Me.txtstmpddate = New System.Windows.Forms.TextBox
        Me.txtnextstmpddate = New System.Windows.Forms.TextBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cmbSearch = New System.Windows.Forms.ComboBox
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.btnld = New System.Windows.Forms.Button
        Me.btnclr = New System.Windows.Forms.Button
        Me.btnadd = New System.Windows.Forms.Button
        Me.btnPreview = New System.Windows.Forms.Button
        Me.chkFormat = New System.Windows.Forms.CheckBox
        Me.btnhide = New System.Windows.Forms.Button
        Me.rdoactive = New System.Windows.Forms.RadioButton
        Me.rdohide = New System.Windows.Forms.RadioButton
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtdescr = New System.Windows.Forms.TextBox
        Me.Amt = New System.Windows.Forms.Label
        Me.txtamt = New System.Windows.Forms.TextBox
        Me.amcpanel.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.drgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'amcpanel
        '
        Me.amcpanel.BackColor = System.Drawing.Color.White
        Me.amcpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.amcpanel.Controls.Add(Me.PictureBox2)
        Me.amcpanel.Controls.Add(Me.Label1)
        Me.amcpanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.amcpanel.Location = New System.Drawing.Point(0, 0)
        Me.amcpanel.Name = "amcpanel"
        Me.amcpanel.Size = New System.Drawing.Size(1222, 38)
        Me.amcpanel.TabIndex = 345461
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(3, 4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(36, 23)
        Me.PictureBox2.TabIndex = 345461
        Me.PictureBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(45, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(155, 18)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "AMC Customer List"
        '
        'txtcustomer
        '
        Me.txtcustomer.AcceptsReturn = True
        Me.txtcustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtcustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtcustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustomer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtcustomer.Location = New System.Drawing.Point(126, 73)
        Me.txtcustomer.MaxLength = 500
        Me.txtcustomer.Name = "txtcustomer"
        Me.txtcustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtcustomer.Size = New System.Drawing.Size(238, 20)
        Me.txtcustomer.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(41, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 345498
        Me.Label4.Text = "Customer"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(411, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 345497
        Me.Label2.Text = "Item"
        '
        'cldrdate
        '
        Me.cldrdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrdate.Location = New System.Drawing.Point(261, 45)
        Me.cldrdate.Name = "cldrdate"
        Me.cldrdate.Size = New System.Drawing.Size(85, 20)
        Me.cldrdate.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(225, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 345496
        Me.Label3.Text = "Date"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(369, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 13)
        Me.Label5.TabIndex = 345500
        Me.Label5.Text = "Stamped Date"
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(576, 45)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 13)
        Me.Label6.TabIndex = 345502
        Me.Label6.Text = "Next Stamping Date"
        '
        'txtinvoicenmr
        '
        Me.txtinvoicenmr.Location = New System.Drawing.Point(126, 45)
        Me.txtinvoicenmr.Name = "txtinvoicenmr"
        Me.txtinvoicenmr.Size = New System.Drawing.Size(85, 20)
        Me.txtinvoicenmr.TabIndex = 100
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(41, 45)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(82, 13)
        Me.Label9.TabIndex = 345509
        Me.Label9.Text = "Invoice Number"
        '
        'btnamcremove
        '
        Me.btnamcremove.BackColor = System.Drawing.Color.SteelBlue
        Me.btnamcremove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnamcremove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnamcremove.ForeColor = System.Drawing.Color.White
        Me.btnamcremove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnamcremove.Location = New System.Drawing.Point(1085, 45)
        Me.btnamcremove.Name = "btnamcremove"
        Me.btnamcremove.Size = New System.Drawing.Size(69, 26)
        Me.btnamcremove.TabIndex = 345512
        Me.btnamcremove.Text = "Remove"
        Me.btnamcremove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnamcremove.UseVisualStyleBackColor = False
        '
        'btnamcadd
        '
        Me.btnamcadd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnamcadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnamcadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnamcadd.ForeColor = System.Drawing.Color.White
        Me.btnamcadd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnamcadd.Location = New System.Drawing.Point(872, 45)
        Me.btnamcadd.Name = "btnamcadd"
        Me.btnamcadd.Size = New System.Drawing.Size(69, 26)
        Me.btnamcadd.TabIndex = 6
        Me.btnamcadd.Text = "Add"
        Me.btnamcadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnamcadd.UseVisualStyleBackColor = False
        '
        'drgv
        '
        Me.drgv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.drgv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.drgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.drgv.DefaultCellStyle = DataGridViewCellStyle5
        Me.drgv.Location = New System.Drawing.Point(9, 104)
        Me.drgv.Name = "drgv"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.drgv.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.drgv.Size = New System.Drawing.Size(1201, 352)
        Me.drgv.TabIndex = 345513
        '
        'txtitem
        '
        Me.txtitem.Location = New System.Drawing.Point(453, 73)
        Me.txtitem.Name = "txtitem"
        Me.txtitem.Size = New System.Drawing.Size(210, 20)
        Me.txtitem.TabIndex = 4
        '
        'txtstmpddate
        '
        Me.txtstmpddate.Location = New System.Drawing.Point(453, 45)
        Me.txtstmpddate.Name = "txtstmpddate"
        Me.txtstmpddate.Size = New System.Drawing.Size(100, 20)
        Me.txtstmpddate.TabIndex = 1
        '
        'txtnextstmpddate
        '
        Me.txtnextstmpddate.Location = New System.Drawing.Point(682, 45)
        Me.txtnextstmpddate.Name = "txtnextstmpddate"
        Me.txtnextstmpddate.Size = New System.Drawing.Size(100, 20)
        Me.txtnextstmpddate.TabIndex = 2
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(1117, 463)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(93, 35)
        Me.btnExit.TabIndex = 345517
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        '
        'cmbSearch
        '
        Me.cmbSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSearch.FormattingEnabled = True
        Me.cmbSearch.Location = New System.Drawing.Point(10, 462)
        Me.cmbSearch.Name = "cmbSearch"
        Me.cmbSearch.Size = New System.Drawing.Size(143, 21)
        Me.cmbSearch.TabIndex = 345519
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(159, 462)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(373, 20)
        Me.txtSearch.TabIndex = 345518
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(538, 463)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345520
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
        '
        'btnld
        '
        Me.btnld.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnld.BackColor = System.Drawing.Color.SteelBlue
        Me.btnld.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnld.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnld.ForeColor = System.Drawing.Color.White
        Me.btnld.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnld.Location = New System.Drawing.Point(1015, 463)
        Me.btnld.Name = "btnld"
        Me.btnld.Size = New System.Drawing.Size(93, 35)
        Me.btnld.TabIndex = 345521
        Me.btnld.Text = "Load"
        Me.btnld.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnld.UseVisualStyleBackColor = False
        '
        'btnclr
        '
        Me.btnclr.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclr.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclr.ForeColor = System.Drawing.Color.White
        Me.btnclr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnclr.Location = New System.Drawing.Point(943, 45)
        Me.btnclr.Name = "btnclr"
        Me.btnclr.Size = New System.Drawing.Size(69, 26)
        Me.btnclr.TabIndex = 345522
        Me.btnclr.Text = "Clear"
        Me.btnclr.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclr.UseVisualStyleBackColor = False
        '
        'btnadd
        '
        Me.btnadd.Image = Global.SMSMP.My.Resources.Resources.button_edit
        Me.btnadd.Location = New System.Drawing.Point(372, 68)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(28, 25)
        Me.btnadd.TabIndex = 345523
        Me.btnadd.TabStop = False
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.BackColor = System.Drawing.Color.SteelBlue
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.White
        Me.btnPreview.Location = New System.Drawing.Point(849, 463)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(93, 35)
        Me.btnPreview.TabIndex = 345524
        Me.btnPreview.TabStop = False
        Me.btnPreview.Text = "Pre&view"
        Me.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'chkFormat
        '
        Me.chkFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(948, 463)
        Me.chkFormat.Name = "chkFormat"
        Me.chkFormat.Size = New System.Drawing.Size(65, 19)
        Me.chkFormat.TabIndex = 345525
        Me.chkFormat.Text = "Format"
        Me.chkFormat.UseVisualStyleBackColor = False
        '
        'btnhide
        '
        Me.btnhide.BackColor = System.Drawing.Color.SteelBlue
        Me.btnhide.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnhide.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnhide.ForeColor = System.Drawing.Color.White
        Me.btnhide.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnhide.Location = New System.Drawing.Point(1014, 45)
        Me.btnhide.Name = "btnhide"
        Me.btnhide.Size = New System.Drawing.Size(69, 26)
        Me.btnhide.TabIndex = 345526
        Me.btnhide.Text = "Hide"
        Me.btnhide.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnhide.UseVisualStyleBackColor = False
        '
        'rdoactive
        '
        Me.rdoactive.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdoactive.AutoSize = True
        Me.rdoactive.Checked = True
        Me.rdoactive.Location = New System.Drawing.Point(696, 463)
        Me.rdoactive.Name = "rdoactive"
        Me.rdoactive.Size = New System.Drawing.Size(55, 17)
        Me.rdoactive.TabIndex = 345527
        Me.rdoactive.TabStop = True
        Me.rdoactive.Text = "Active"
        Me.rdoactive.UseVisualStyleBackColor = True
        '
        'rdohide
        '
        Me.rdohide.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdohide.AutoSize = True
        Me.rdohide.Location = New System.Drawing.Point(755, 463)
        Me.rdohide.Name = "rdohide"
        Me.rdohide.Size = New System.Drawing.Size(47, 17)
        Me.rdohide.TabIndex = 345528
        Me.rdohide.Text = "Hide"
        Me.rdohide.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(680, 75)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 345529
        Me.Label7.Text = "Desription"
        '
        'txtdescr
        '
        Me.txtdescr.Location = New System.Drawing.Point(740, 73)
        Me.txtdescr.Name = "txtdescr"
        Me.txtdescr.Size = New System.Drawing.Size(201, 20)
        Me.txtdescr.TabIndex = 5
        '
        'Amt
        '
        Me.Amt.AutoSize = True
        Me.Amt.Location = New System.Drawing.Point(950, 75)
        Me.Amt.Name = "Amt"
        Me.Amt.Size = New System.Drawing.Size(43, 13)
        Me.Amt.TabIndex = 345503
        Me.Amt.Text = "Amount"
        Me.Amt.Visible = False
        '
        'txtamt
        '
        Me.txtamt.Location = New System.Drawing.Point(1002, 73)
        Me.txtamt.Name = "txtamt"
        Me.txtamt.Size = New System.Drawing.Size(144, 20)
        Me.txtamt.TabIndex = 7
        Me.txtamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtamt.Visible = False
        '
        'AmcList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(1222, 503)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtdescr)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.rdohide)
        Me.Controls.Add(Me.rdoactive)
        Me.Controls.Add(Me.btnhide)
        Me.Controls.Add(Me.chkFormat)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btnadd)
        Me.Controls.Add(Me.btnclr)
        Me.Controls.Add(Me.btnld)
        Me.Controls.Add(Me.cmbSearch)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.txtamt)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.txtnextstmpddate)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtitem)
        Me.Controls.Add(Me.txtstmpddate)
        Me.Controls.Add(Me.drgv)
        Me.Controls.Add(Me.Amt)
        Me.Controls.Add(Me.cldrdate)
        Me.Controls.Add(Me.btnamcremove)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnamcadd)
        Me.Controls.Add(Me.txtinvoicenmr)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtcustomer)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.amcpanel)
        Me.Name = "AmcList"
        Me.Text = "AmcListFrm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.amcpanel.ResumeLayout(False)
        Me.amcpanel.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.drgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents amcpanel As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtcustomer As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cldrdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtinvoicenmr As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnamcremove As System.Windows.Forms.Button
    Friend WithEvents btnamcadd As System.Windows.Forms.Button
    Friend WithEvents drgv As System.Windows.Forms.DataGridView
    Friend WithEvents txtitem As System.Windows.Forms.TextBox
    Friend WithEvents txtstmpddate As System.Windows.Forms.TextBox
    Friend WithEvents txtnextstmpddate As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents cmbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents btnld As System.Windows.Forms.Button
    Friend WithEvents btnclr As System.Windows.Forms.Button
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents chkFormat As System.Windows.Forms.CheckBox
    Friend WithEvents btnhide As System.Windows.Forms.Button
    Friend WithEvents rdoactive As System.Windows.Forms.RadioButton
    Friend WithEvents rdohide As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtdescr As System.Windows.Forms.TextBox
    Friend WithEvents Amt As System.Windows.Forms.Label
    Friend WithEvents txtamt As System.Windows.Forms.TextBox
End Class
