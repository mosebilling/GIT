<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddRoomsFrm
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtestNumber = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.dtpcheckin = New System.Windows.Forms.DateTimePicker
        Me.lblstatecode = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblestimated = New System.Windows.Forms.Label
        Me.txtdescription = New System.Windows.Forms.TextBox
        Me.btnupdate = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.grdPack = New System.Windows.Forms.DataGridView
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.cmbSearch = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblDetails = New System.Windows.Forms.Label
        Me.lblstatus = New System.Windows.Forms.Label
        Me.rdoac = New System.Windows.Forms.RadioButton
        Me.rdononac = New System.Windows.Forms.RadioButton
        Me.rdoall = New System.Windows.Forms.RadioButton
        Me.rdovacent = New System.Windows.Forms.RadioButton
        Me.lblroom = New System.Windows.Forms.Label
        Me.lblrent = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lbltaxprice = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblnotacTaxprice = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblnonacrent = New System.Windows.Forms.Label
        Me.plnonac = New System.Windows.Forms.Panel
        Me.lblcategory = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.NumSalesPrice = New System.Windows.Forms.TextBox
        Me.txtpriceWtax = New System.Windows.Forms.TextBox
        Me.lbltax = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblgstamt = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdPack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plnonac.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtestNumber)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.dtpcheckin)
        Me.GroupBox1.Controls.Add(Me.lblstatecode)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblestimated)
        Me.GroupBox1.Controls.Add(Me.txtdescription)
        Me.GroupBox1.Location = New System.Drawing.Point(765, 44)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(428, 144)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Room Details"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 81)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 15)
        Me.Label1.TabIndex = 345483
        Me.Label1.Text = "Description"
        '
        'txtestNumber
        '
        Me.txtestNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtestNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtestNumber.Location = New System.Drawing.Point(119, 51)
        Me.txtestNumber.Name = "txtestNumber"
        Me.txtestNumber.Size = New System.Drawing.Size(60, 26)
        Me.txtestNumber.TabIndex = 1
        Me.txtestNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(10, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(106, 15)
        Me.Label8.TabIndex = 345482
        Me.Label8.Text = "Estimated Days"
        '
        'dtpcheckin
        '
        Me.dtpcheckin.CalendarMonthBackground = System.Drawing.Color.IndianRed
        Me.dtpcheckin.CustomFormat = "dd/MMM/yyyy hh:mm:ss tt"
        Me.dtpcheckin.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpcheckin.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpcheckin.Location = New System.Drawing.Point(119, 17)
        Me.dtpcheckin.Name = "dtpcheckin"
        Me.dtpcheckin.Size = New System.Drawing.Size(296, 26)
        Me.dtpcheckin.TabIndex = 0
        '
        'lblstatecode
        '
        Me.lblstatecode.AutoSize = True
        Me.lblstatecode.BackColor = System.Drawing.Color.Transparent
        Me.lblstatecode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatecode.ForeColor = System.Drawing.Color.Maroon
        Me.lblstatecode.Location = New System.Drawing.Point(104, 16)
        Me.lblstatecode.Name = "lblstatecode"
        Me.lblstatecode.Size = New System.Drawing.Size(67, 15)
        Me.lblstatecode.TabIndex = 345479
        Me.lblstatecode.Text = "State Code"
        Me.lblstatecode.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(10, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 18)
        Me.Label5.TabIndex = 345371
        Me.Label5.Text = "Check In Date"
        '
        'lblestimated
        '
        Me.lblestimated.AutoSize = True
        Me.lblestimated.BackColor = System.Drawing.Color.Transparent
        Me.lblestimated.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblestimated.ForeColor = System.Drawing.Color.Green
        Me.lblestimated.Location = New System.Drawing.Point(196, 56)
        Me.lblestimated.Name = "lblestimated"
        Me.lblestimated.Size = New System.Drawing.Size(114, 16)
        Me.lblestimated.TabIndex = 345369
        Me.lblestimated.Text = "Estimated Date"
        '
        'txtdescription
        '
        Me.txtdescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtdescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdescription.Location = New System.Drawing.Point(119, 81)
        Me.txtdescription.MaxLength = 15
        Me.txtdescription.Multiline = True
        Me.txtdescription.Name = "txtdescription"
        Me.txtdescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtdescription.Size = New System.Drawing.Size(296, 56)
        Me.txtdescription.TabIndex = 2
        '
        'btnupdate
        '
        Me.btnupdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnupdate.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnupdate.Location = New System.Drawing.Point(999, 493)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(93, 35)
        Me.btnupdate.TabIndex = 14
        Me.btnupdate.Text = "&Add"
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
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(1094, 493)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(93, 35)
        Me.btnExit.TabIndex = 345472
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'grdPack
        '
        Me.grdPack.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdPack.BackgroundColor = System.Drawing.Color.White
        Me.grdPack.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdPack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPack.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdPack.Location = New System.Drawing.Point(12, 52)
        Me.grdPack.MultiSelect = False
        Me.grdPack.Name = "grdPack"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPack.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.grdPack.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdPack.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdPack.RowTemplate.Height = 30
        Me.grdPack.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPack.Size = New System.Drawing.Size(742, 460)
        Me.grdPack.StandardTab = True
        Me.grdPack.TabIndex = 345473
        '
        'chkSearch
        '
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(661, 119)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 88
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
        Me.chkSearch.Visible = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(104, 12)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(546, 31)
        Me.txtSearch.TabIndex = 3
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.SteelBlue
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(589, 208)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(61, 25)
        Me.btnSearch.TabIndex = 86
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        Me.btnSearch.Visible = False
        '
        'cmbSearch
        '
        Me.cmbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSearch.FormattingEnabled = True
        Me.cmbSearch.Location = New System.Drawing.Point(594, 145)
        Me.cmbSearch.Name = "cmbSearch"
        Me.cmbSearch.Size = New System.Drawing.Size(220, 21)
        Me.cmbSearch.TabIndex = 87
        Me.cmbSearch.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 15)
        Me.Label2.TabIndex = 345484
        Me.Label2.Text = "Room Number"
        '
        'lblDetails
        '
        Me.lblDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetails.Location = New System.Drawing.Point(765, 231)
        Me.lblDetails.Name = "lblDetails"
        Me.lblDetails.Size = New System.Drawing.Size(415, 97)
        Me.lblDetails.TabIndex = 345485
        Me.lblDetails.Text = "Selected Room Detials : "
        '
        'lblstatus
        '
        Me.lblstatus.AutoSize = True
        Me.lblstatus.BackColor = System.Drawing.Color.Transparent
        Me.lblstatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.ForeColor = System.Drawing.Color.Green
        Me.lblstatus.Location = New System.Drawing.Point(765, 214)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(49, 16)
        Me.lblstatus.TabIndex = 345486
        Me.lblstatus.Text = "status"
        '
        'rdoac
        '
        Me.rdoac.AutoSize = True
        Me.rdoac.BackColor = System.Drawing.Color.Transparent
        Me.rdoac.Location = New System.Drawing.Point(80, 6)
        Me.rdoac.Name = "rdoac"
        Me.rdoac.Size = New System.Drawing.Size(44, 17)
        Me.rdoac.TabIndex = 345487
        Me.rdoac.Text = "A/C"
        Me.rdoac.UseVisualStyleBackColor = False
        '
        'rdononac
        '
        Me.rdononac.AutoSize = True
        Me.rdononac.BackColor = System.Drawing.Color.Transparent
        Me.rdononac.Checked = True
        Me.rdononac.Location = New System.Drawing.Point(3, 6)
        Me.rdononac.Name = "rdononac"
        Me.rdononac.Size = New System.Drawing.Size(71, 17)
        Me.rdononac.TabIndex = 345488
        Me.rdononac.TabStop = True
        Me.rdononac.Text = "NON A/C"
        Me.rdononac.UseVisualStyleBackColor = False
        '
        'rdoall
        '
        Me.rdoall.AutoSize = True
        Me.rdoall.BackColor = System.Drawing.Color.Transparent
        Me.rdoall.Location = New System.Drawing.Point(716, 21)
        Me.rdoall.Name = "rdoall"
        Me.rdoall.Size = New System.Drawing.Size(36, 17)
        Me.rdoall.TabIndex = 345489
        Me.rdoall.Text = "All"
        Me.rdoall.UseVisualStyleBackColor = False
        '
        'rdovacent
        '
        Me.rdovacent.AutoSize = True
        Me.rdovacent.BackColor = System.Drawing.Color.Transparent
        Me.rdovacent.Checked = True
        Me.rdovacent.Location = New System.Drawing.Point(656, 21)
        Me.rdovacent.Name = "rdovacent"
        Me.rdovacent.Size = New System.Drawing.Size(59, 17)
        Me.rdovacent.TabIndex = 345490
        Me.rdovacent.TabStop = True
        Me.rdovacent.Text = "Vacent"
        Me.rdovacent.UseVisualStyleBackColor = False
        '
        'lblroom
        '
        Me.lblroom.AutoSize = True
        Me.lblroom.BackColor = System.Drawing.Color.Transparent
        Me.lblroom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblroom.Location = New System.Drawing.Point(765, 198)
        Me.lblroom.Name = "lblroom"
        Me.lblroom.Size = New System.Drawing.Size(117, 15)
        Me.lblroom.TabIndex = 345491
        Me.lblroom.Text = "Selected Room : "
        '
        'lblrent
        '
        Me.lblrent.BackColor = System.Drawing.Color.Transparent
        Me.lblrent.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblrent.Location = New System.Drawing.Point(859, 404)
        Me.lblrent.Name = "lblrent"
        Me.lblrent.Size = New System.Drawing.Size(119, 29)
        Me.lblrent.TabIndex = 345492
        Me.lblrent.Text = "0.00"
        Me.lblrent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(772, 414)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 15)
        Me.Label3.TabIndex = 345493
        Me.Label3.Text = "Rent"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(987, 414)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 15)
        Me.Label4.TabIndex = 345495
        Me.Label4.Text = "Rent + Tax"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbltaxprice
        '
        Me.lbltaxprice.BackColor = System.Drawing.Color.Transparent
        Me.lbltaxprice.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltaxprice.Location = New System.Drawing.Point(1071, 404)
        Me.lbltaxprice.Name = "lbltaxprice"
        Me.lbltaxprice.Size = New System.Drawing.Size(119, 29)
        Me.lbltaxprice.TabIndex = 345494
        Me.lbltaxprice.Text = "0.00"
        Me.lbltaxprice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(227, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 15)
        Me.Label7.TabIndex = 345499
        Me.Label7.Text = "Rent + Tax"
        '
        'lblnotacTaxprice
        '
        Me.lblnotacTaxprice.BackColor = System.Drawing.Color.Transparent
        Me.lblnotacTaxprice.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnotacTaxprice.Location = New System.Drawing.Point(305, 4)
        Me.lblnotacTaxprice.Name = "lblnotacTaxprice"
        Me.lblnotacTaxprice.Size = New System.Drawing.Size(119, 29)
        Me.lblnotacTaxprice.TabIndex = 345498
        Me.lblnotacTaxprice.Text = "0.00"
        Me.lblnotacTaxprice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 19)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 15)
        Me.Label10.TabIndex = 345497
        Me.Label10.Text = "Rent NON A/C"
        '
        'lblnonacrent
        '
        Me.lblnonacrent.BackColor = System.Drawing.Color.Transparent
        Me.lblnonacrent.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnonacrent.Location = New System.Drawing.Point(99, 4)
        Me.lblnonacrent.Name = "lblnonacrent"
        Me.lblnonacrent.Size = New System.Drawing.Size(119, 29)
        Me.lblnonacrent.TabIndex = 345496
        Me.lblnonacrent.Text = "0.00"
        Me.lblnonacrent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'plnonac
        '
        Me.plnonac.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.plnonac.Controls.Add(Me.lblnonacrent)
        Me.plnonac.Controls.Add(Me.Label7)
        Me.plnonac.Controls.Add(Me.Label10)
        Me.plnonac.Controls.Add(Me.lblnotacTaxprice)
        Me.plnonac.Location = New System.Drawing.Point(763, 448)
        Me.plnonac.Name = "plnonac"
        Me.plnonac.Size = New System.Drawing.Size(427, 38)
        Me.plnonac.TabIndex = 345500
        Me.plnonac.Visible = False
        '
        'lblcategory
        '
        Me.lblcategory.AutoSize = True
        Me.lblcategory.BackColor = System.Drawing.Color.Transparent
        Me.lblcategory.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcategory.ForeColor = System.Drawing.Color.Green
        Me.lblcategory.Location = New System.Drawing.Point(772, 338)
        Me.lblcategory.Name = "lblcategory"
        Me.lblcategory.Size = New System.Drawing.Size(83, 16)
        Me.lblcategory.TabIndex = 345501
        Me.lblcategory.Text = "Category : "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(772, 380)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 13)
        Me.Label6.TabIndex = 345504
        Me.Label6.Text = "Rent"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(987, 374)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(68, 15)
        Me.Label21.TabIndex = 345505
        Me.Label21.Text = "Price + Tax"
        '
        'NumSalesPrice
        '
        Me.NumSalesPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumSalesPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumSalesPrice.Location = New System.Drawing.Point(855, 367)
        Me.NumSalesPrice.MaxLength = 30
        Me.NumSalesPrice.Name = "NumSalesPrice"
        Me.NumSalesPrice.Size = New System.Drawing.Size(123, 26)
        Me.NumSalesPrice.TabIndex = 345502
        Me.NumSalesPrice.Text = "0.00"
        Me.NumSalesPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtpriceWtax
        '
        Me.txtpriceWtax.BackColor = System.Drawing.Color.White
        Me.txtpriceWtax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtpriceWtax.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpriceWtax.Location = New System.Drawing.Point(1067, 367)
        Me.txtpriceWtax.MaxLength = 60
        Me.txtpriceWtax.Name = "txtpriceWtax"
        Me.txtpriceWtax.Size = New System.Drawing.Size(123, 26)
        Me.txtpriceWtax.TabIndex = 345503
        Me.txtpriceWtax.TabStop = False
        Me.txtpriceWtax.Text = "0.00"
        Me.txtpriceWtax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbltax
        '
        Me.lbltax.BackColor = System.Drawing.Color.Transparent
        Me.lbltax.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltax.ForeColor = System.Drawing.Color.Green
        Me.lbltax.Location = New System.Drawing.Point(990, 338)
        Me.lbltax.Name = "lbltax"
        Me.lbltax.Size = New System.Drawing.Size(197, 16)
        Me.lbltax.TabIndex = 345506
        Me.lbltax.Text = "Tax : "
        Me.lbltax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.rdononac)
        Me.Panel1.Controls.Add(Me.rdoac)
        Me.Panel1.Location = New System.Drawing.Point(765, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(241, 29)
        Me.Panel1.TabIndex = 345507
        '
        'lblgstamt
        '
        Me.lblgstamt.AutoSize = True
        Me.lblgstamt.BackColor = System.Drawing.Color.Transparent
        Me.lblgstamt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgstamt.ForeColor = System.Drawing.Color.Maroon
        Me.lblgstamt.Location = New System.Drawing.Point(765, 492)
        Me.lblgstamt.Name = "lblgstamt"
        Me.lblgstamt.Size = New System.Drawing.Size(61, 15)
        Me.lblgstamt.TabIndex = 345508
        Me.lblgstamt.Text = "GST Amt: "
        '
        'AddRoomsFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1197, 540)
        Me.Controls.Add(Me.lblgstamt)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lbltax)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.NumSalesPrice)
        Me.Controls.Add(Me.txtpriceWtax)
        Me.Controls.Add(Me.lblcategory)
        Me.Controls.Add(Me.plnonac)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lbltaxprice)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblrent)
        Me.Controls.Add(Me.lblroom)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.rdoall)
        Me.Controls.Add(Me.rdovacent)
        Me.Controls.Add(Me.lblstatus)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.grdPack)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnupdate)
        Me.Controls.Add(Me.cmbSearch)
        Me.Controls.Add(Me.lblDetails)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddRoomsFrm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Rooms "
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdPack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plnonac.ResumeLayout(False)
        Me.plnonac.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtestNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpcheckin As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblstatecode As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblestimated As System.Windows.Forms.Label
    Friend WithEvents txtdescription As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents grdPack As System.Windows.Forms.DataGridView
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents cmbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblDetails As System.Windows.Forms.Label
    Friend WithEvents lblstatus As System.Windows.Forms.Label
    Friend WithEvents rdoac As System.Windows.Forms.RadioButton
    Friend WithEvents rdononac As System.Windows.Forms.RadioButton
    Friend WithEvents rdoall As System.Windows.Forms.RadioButton
    Friend WithEvents rdovacent As System.Windows.Forms.RadioButton
    Friend WithEvents lblroom As System.Windows.Forms.Label
    Friend WithEvents lblrent As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lbltaxprice As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblnotacTaxprice As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblnonacrent As System.Windows.Forms.Label
    Friend WithEvents plnonac As System.Windows.Forms.Panel
    Friend WithEvents lblcategory As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents NumSalesPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtpriceWtax As System.Windows.Forms.TextBox
    Friend WithEvents lbltax As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblgstamt As System.Windows.Forms.Label
End Class
