<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClinicApBookingFrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ClinicApBookingFrm))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.lbltime = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.dtptime = New System.Windows.Forms.DateTimePicker
        Me.cmbtype = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.dtpbookingfor = New System.Windows.Forms.DateTimePicker
        Me.btnclear = New System.Windows.Forms.Button
        Me.btnupdate = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnaddref = New System.Windows.Forms.Button
        Me.cmbreference = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtphone = New System.Windows.Forms.TextBox
        Me.txtadd3 = New System.Windows.Forms.TextBox
        Me.txtadd2 = New System.Windows.Forms.TextBox
        Me.txtadd1 = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblclosing = New System.Windows.Forms.Label
        Me.txtremarks = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblCap4 = New System.Windows.Forms.Label
        Me.txtRec1 = New System.Windows.Forms.TextBox
        Me.txtRec0 = New System.Windows.Forms.TextBox
        Me.cmbsalesman = New System.Windows.Forms.ComboBox
        Me.lblCap8 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.numVchrNo = New System.Windows.Forms.TextBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.rdobookingfor = New System.Windows.Forms.RadioButton
        Me.rdobooking = New System.Windows.Forms.RadioButton
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.btnload = New System.Windows.Forms.Button
        Me.dtpto = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.dtpfrom = New System.Windows.Forms.DateTimePicker
        Me.grdlist = New System.Windows.Forms.DataGridView
        Me.btnExit = New System.Windows.Forms.Button
        Me.btndelete = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(982, 33)
        Me.Panel1.TabIndex = 345447
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 20)
        Me.PictureBox1.TabIndex = 345458
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(39, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(168, 18)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Appointment Booking"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(4, 37)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(975, 443)
        Me.TabControl1.TabIndex = 345462
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lbltime)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.dtptime)
        Me.TabPage1.Controls.Add(Me.cmbtype)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.dtpbookingfor)
        Me.TabPage1.Controls.Add(Me.btnclear)
        Me.TabPage1.Controls.Add(Me.btnupdate)
        Me.TabPage1.Controls.Add(Me.Panel2)
        Me.TabPage1.Controls.Add(Me.cmbsalesman)
        Me.TabPage1.Controls.Add(Me.lblCap8)
        Me.TabPage1.Controls.Add(Me.Label14)
        Me.TabPage1.Controls.Add(Me.numVchrNo)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(967, 417)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Entry"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'lbltime
        '
        Me.lbltime.AutoSize = True
        Me.lbltime.BackColor = System.Drawing.Color.Transparent
        Me.lbltime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbltime.Location = New System.Drawing.Point(195, 8)
        Me.lbltime.Name = "lbltime"
        Me.lbltime.Size = New System.Drawing.Size(130, 15)
        Me.lbltime.TabIndex = 345487
        Me.lbltime.Text = "Last Appointment : "
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(228, 62)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(35, 15)
        Me.Label11.TabIndex = 345486
        Me.Label11.Text = "Time"
        '
        'dtptime
        '
        Me.dtptime.CustomFormat = "hh mm ss tt"
        Me.dtptime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptime.Location = New System.Drawing.Point(269, 61)
        Me.dtptime.Name = "dtptime"
        Me.dtptime.Size = New System.Drawing.Size(96, 20)
        Me.dtptime.TabIndex = 345485
        '
        'cmbtype
        '
        Me.cmbtype.BackColor = System.Drawing.SystemColors.Window
        Me.cmbtype.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtype.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbtype.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbtype.Items.AddRange(New Object() {"OP", "GP"})
        Me.cmbtype.Location = New System.Drawing.Point(86, 87)
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbtype.Size = New System.Drawing.Size(103, 22)
        Me.cmbtype.TabIndex = 345483
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(10, 90)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(30, 14)
        Me.Label9.TabIndex = 345484
        Me.Label9.Text = "Type"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(9, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 15)
        Me.Label5.TabIndex = 345462
        Me.Label5.Text = "Date"
        '
        'dtpbookingfor
        '
        Me.dtpbookingfor.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpbookingfor.Location = New System.Drawing.Point(86, 59)
        Me.dtpbookingfor.Name = "dtpbookingfor"
        Me.dtpbookingfor.Size = New System.Drawing.Size(103, 20)
        Me.dtpbookingfor.TabIndex = 345461
        '
        'btnclear
        '
        Me.btnclear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnclear.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.ForeColor = System.Drawing.Color.White
        Me.btnclear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnclear.Location = New System.Drawing.Point(9, 379)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(100, 35)
        Me.btnclear.TabIndex = 345460
        Me.btnclear.Text = "Clear"
        Me.btnclear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclear.UseVisualStyleBackColor = False
        '
        'btnupdate
        '
        Me.btnupdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnupdate.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnupdate.Location = New System.Drawing.Point(861, 379)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(100, 35)
        Me.btnupdate.TabIndex = 8
        Me.btnupdate.Text = "&Update "
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.btnaddref)
        Me.Panel2.Controls.Add(Me.cmbreference)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.txtphone)
        Me.Panel2.Controls.Add(Me.txtadd3)
        Me.Panel2.Controls.Add(Me.txtadd2)
        Me.Panel2.Controls.Add(Me.txtadd1)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.lblclosing)
        Me.Panel2.Controls.Add(Me.txtremarks)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.lblCap4)
        Me.Panel2.Controls.Add(Me.txtRec1)
        Me.Panel2.Controls.Add(Me.txtRec0)
        Me.Panel2.Location = New System.Drawing.Point(6, 113)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(365, 260)
        Me.Panel2.TabIndex = 345447
        '
        'btnaddref
        '
        Me.btnaddref.Image = CType(resources.GetObject("btnaddref.Image"), System.Drawing.Image)
        Me.btnaddref.Location = New System.Drawing.Point(333, 229)
        Me.btnaddref.Name = "btnaddref"
        Me.btnaddref.Size = New System.Drawing.Size(28, 25)
        Me.btnaddref.TabIndex = 345463
        Me.btnaddref.TabStop = False
        Me.btnaddref.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnaddref.UseVisualStyleBackColor = True
        '
        'cmbreference
        '
        Me.cmbreference.BackColor = System.Drawing.SystemColors.Window
        Me.cmbreference.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbreference.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbreference.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbreference.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbreference.Location = New System.Drawing.Point(77, 231)
        Me.cmbreference.Name = "cmbreference"
        Me.cmbreference.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbreference.Size = New System.Drawing.Size(250, 22)
        Me.cmbreference.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(3, 234)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(58, 14)
        Me.Label8.TabIndex = 345482
        Me.Label8.Text = "Reference"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(4, 124)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(37, 14)
        Me.Label6.TabIndex = 345480
        Me.Label6.Text = "Phone"
        '
        'txtphone
        '
        Me.txtphone.AcceptsReturn = True
        Me.txtphone.BackColor = System.Drawing.SystemColors.Window
        Me.txtphone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtphone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtphone.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtphone.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtphone.Location = New System.Drawing.Point(77, 122)
        Me.txtphone.MaxLength = 100
        Me.txtphone.Name = "txtphone"
        Me.txtphone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtphone.Size = New System.Drawing.Size(284, 20)
        Me.txtphone.TabIndex = 5
        '
        'txtadd3
        '
        Me.txtadd3.AcceptsReturn = True
        Me.txtadd3.BackColor = System.Drawing.SystemColors.Window
        Me.txtadd3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtadd3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtadd3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtadd3.Location = New System.Drawing.Point(77, 99)
        Me.txtadd3.MaxLength = 100
        Me.txtadd3.Name = "txtadd3"
        Me.txtadd3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtadd3.Size = New System.Drawing.Size(284, 20)
        Me.txtadd3.TabIndex = 4
        '
        'txtadd2
        '
        Me.txtadd2.AcceptsReturn = True
        Me.txtadd2.BackColor = System.Drawing.SystemColors.Window
        Me.txtadd2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtadd2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtadd2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtadd2.Location = New System.Drawing.Point(77, 76)
        Me.txtadd2.MaxLength = 100
        Me.txtadd2.Name = "txtadd2"
        Me.txtadd2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtadd2.Size = New System.Drawing.Size(284, 20)
        Me.txtadd2.TabIndex = 3
        '
        'txtadd1
        '
        Me.txtadd1.AcceptsReturn = True
        Me.txtadd1.BackColor = System.Drawing.SystemColors.Window
        Me.txtadd1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtadd1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtadd1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtadd1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtadd1.Location = New System.Drawing.Point(77, 53)
        Me.txtadd1.MaxLength = 100
        Me.txtadd1.Name = "txtadd1"
        Me.txtadd1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtadd1.Size = New System.Drawing.Size(284, 20)
        Me.txtadd1.TabIndex = 2
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(11, 319)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 15)
        Me.Label10.TabIndex = 345475
        Me.Label10.Text = "CL. Balance"
        Me.Label10.Visible = False
        '
        'lblclosing
        '
        Me.lblclosing.BackColor = System.Drawing.Color.Transparent
        Me.lblclosing.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclosing.Location = New System.Drawing.Point(81, 319)
        Me.lblclosing.Name = "lblclosing"
        Me.lblclosing.Size = New System.Drawing.Size(95, 19)
        Me.lblclosing.TabIndex = 165
        Me.lblclosing.Text = "0.00"
        Me.lblclosing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblclosing.Visible = False
        '
        'txtremarks
        '
        Me.txtremarks.AcceptsReturn = True
        Me.txtremarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtremarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtremarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtremarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtremarks.Location = New System.Drawing.Point(77, 147)
        Me.txtremarks.MaxLength = 250
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtremarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtremarks.Size = New System.Drawing.Size(284, 78)
        Me.txtremarks.TabIndex = 6
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(4, 147)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(51, 14)
        Me.Label17.TabIndex = 345466
        Me.Label17.Text = "Comment"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(3, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(69, 14)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "Patient Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(3, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(61, 14)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "OP Number"
        '
        'lblCap4
        '
        Me.lblCap4.AutoSize = True
        Me.lblCap4.BackColor = System.Drawing.Color.Transparent
        Me.lblCap4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap4.Location = New System.Drawing.Point(3, 53)
        Me.lblCap4.Name = "lblCap4"
        Me.lblCap4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap4.Size = New System.Drawing.Size(49, 14)
        Me.lblCap4.TabIndex = 46
        Me.lblCap4.Text = "Address"
        '
        'txtRec1
        '
        Me.txtRec1.AcceptsReturn = True
        Me.txtRec1.BackColor = System.Drawing.SystemColors.Window
        Me.txtRec1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRec1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRec1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRec1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRec1.Location = New System.Drawing.Point(77, 30)
        Me.txtRec1.MaxLength = 100
        Me.txtRec1.Name = "txtRec1"
        Me.txtRec1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRec1.Size = New System.Drawing.Size(284, 20)
        Me.txtRec1.TabIndex = 1
        '
        'txtRec0
        '
        Me.txtRec0.AcceptsReturn = True
        Me.txtRec0.BackColor = System.Drawing.SystemColors.Window
        Me.txtRec0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRec0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRec0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRec0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRec0.Location = New System.Drawing.Point(77, 7)
        Me.txtRec0.MaxLength = 10
        Me.txtRec0.Name = "txtRec0"
        Me.txtRec0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRec0.Size = New System.Drawing.Size(284, 20)
        Me.txtRec0.TabIndex = 0
        '
        'cmbsalesman
        '
        Me.cmbsalesman.BackColor = System.Drawing.SystemColors.Window
        Me.cmbsalesman.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbsalesman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsalesman.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsalesman.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbsalesman.Location = New System.Drawing.Point(84, 32)
        Me.cmbsalesman.Name = "cmbsalesman"
        Me.cmbsalesman.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbsalesman.Size = New System.Drawing.Size(281, 22)
        Me.cmbsalesman.TabIndex = 3
        '
        'lblCap8
        '
        Me.lblCap8.AutoSize = True
        Me.lblCap8.BackColor = System.Drawing.Color.Transparent
        Me.lblCap8.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap8.Location = New System.Drawing.Point(10, 35)
        Me.lblCap8.Name = "lblCap8"
        Me.lblCap8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap8.Size = New System.Drawing.Size(39, 14)
        Me.lblCap8.TabIndex = 51
        Me.lblCap8.Text = "Doctor"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(10, 5)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 15)
        Me.Label14.TabIndex = 345450
        Me.Label14.Text = "Booking No."
        '
        'numVchrNo
        '
        Me.numVchrNo.BackColor = System.Drawing.Color.White
        Me.numVchrNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numVchrNo.Location = New System.Drawing.Point(86, 5)
        Me.numVchrNo.Name = "numVchrNo"
        Me.numVchrNo.ReadOnly = True
        Me.numVchrNo.Size = New System.Drawing.Size(103, 21)
        Me.numVchrNo.TabIndex = 345448
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.rdobookingfor)
        Me.TabPage2.Controls.Add(Me.rdobooking)
        Me.TabPage2.Controls.Add(Me.chkSearch)
        Me.TabPage2.Controls.Add(Me.txtSeq)
        Me.TabPage2.Controls.Add(Me.cmbOrder)
        Me.TabPage2.Controls.Add(Me.btnload)
        Me.TabPage2.Controls.Add(Me.dtpto)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.dtpfrom)
        Me.TabPage2.Controls.Add(Me.grdlist)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(967, 417)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Search"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'rdobookingfor
        '
        Me.rdobookingfor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdobookingfor.AutoSize = True
        Me.rdobookingfor.Location = New System.Drawing.Point(542, 388)
        Me.rdobookingfor.Name = "rdobookingfor"
        Me.rdobookingfor.Size = New System.Drawing.Size(82, 17)
        Me.rdobookingfor.TabIndex = 345467
        Me.rdobookingfor.Text = "Booking For"
        Me.rdobookingfor.UseVisualStyleBackColor = True
        '
        'rdobooking
        '
        Me.rdobooking.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdobooking.AutoSize = True
        Me.rdobooking.Checked = True
        Me.rdobooking.Location = New System.Drawing.Point(542, 370)
        Me.rdobooking.Name = "rdobooking"
        Me.rdobooking.Size = New System.Drawing.Size(90, 17)
        Me.rdobooking.TabIndex = 345466
        Me.rdobooking.TabStop = True
        Me.rdobooking.Text = "Booking Date"
        Me.rdobooking.UseVisualStyleBackColor = True
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.Checked = True
        Me.chkSearch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(151, 395)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345465
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
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
        Me.txtSeq.Location = New System.Drawing.Point(151, 372)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(222, 20)
        Me.txtSeq.TabIndex = 345464
        '
        'cmbOrder
        '
        Me.cmbOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOrder.Location = New System.Drawing.Point(7, 370)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOrder.Size = New System.Drawing.Size(138, 22)
        Me.cmbOrder.TabIndex = 345463
        Me.cmbOrder.TabStop = False
        '
        'btnload
        '
        Me.btnload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.Location = New System.Drawing.Point(881, 383)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(80, 23)
        Me.btnload.TabIndex = 345462
        Me.btnload.Text = "Load"
        Me.btnload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnload.UseVisualStyleBackColor = False
        '
        'dtpto
        '
        Me.dtpto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpto.Location = New System.Drawing.Point(769, 386)
        Me.dtpto.Name = "dtpto"
        Me.dtpto.Size = New System.Drawing.Size(106, 20)
        Me.dtpto.TabIndex = 345456
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(654, 368)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(94, 15)
        Me.Label7.TabIndex = 345455
        Me.Label7.Text = "Date Parameter"
        '
        'dtpfrom
        '
        Me.dtpfrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfrom.Location = New System.Drawing.Point(657, 386)
        Me.dtpfrom.Name = "dtpfrom"
        Me.dtpfrom.Size = New System.Drawing.Size(106, 20)
        Me.dtpfrom.TabIndex = 345454
        '
        'grdlist
        '
        Me.grdlist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdlist.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.grdlist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdlist.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdlist.Location = New System.Drawing.Point(6, 6)
        Me.grdlist.Name = "grdlist"
        Me.grdlist.Size = New System.Drawing.Size(955, 359)
        Me.grdlist.TabIndex = 345453
        Me.grdlist.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(873, 490)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(101, 35)
        Me.btnExit.TabIndex = 345463
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btndelete.BackColor = System.Drawing.Color.SteelBlue
        Me.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.ForeColor = System.Drawing.Color.White
        Me.btndelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btndelete.Location = New System.Drawing.Point(18, 482)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(100, 35)
        Me.btndelete.TabIndex = 345488
        Me.btndelete.Text = "&Delete"
        Me.btndelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndelete.UseVisualStyleBackColor = False
        '
        'ClinicApBookingFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(982, 535)
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ClinicApBookingFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Booking"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.grdlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents cmbsalesman As System.Windows.Forms.ComboBox
    Public WithEvents lblCap8 As System.Windows.Forms.Label
    Friend WithEvents lblclosing As System.Windows.Forms.Label
    Public WithEvents txtremarks As System.Windows.Forms.TextBox
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblCap4 As System.Windows.Forms.Label
    Public WithEvents txtRec1 As System.Windows.Forms.TextBox
    Public WithEvents txtRec0 As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents numVchrNo As System.Windows.Forms.TextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents dtpto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpfrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents grdlist As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpbookingfor As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents rdobookingfor As System.Windows.Forms.RadioButton
    Friend WithEvents rdobooking As System.Windows.Forms.RadioButton
    Public WithEvents cmbreference As System.Windows.Forms.ComboBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents txtphone As System.Windows.Forms.TextBox
    Public WithEvents txtadd3 As System.Windows.Forms.TextBox
    Public WithEvents txtadd2 As System.Windows.Forms.TextBox
    Public WithEvents txtadd1 As System.Windows.Forms.TextBox
    Friend WithEvents btnaddref As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtptime As System.Windows.Forms.DateTimePicker
    Public WithEvents cmbtype As System.Windows.Forms.ComboBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lbltime As System.Windows.Forms.Label
    Friend WithEvents btndelete As System.Windows.Forms.Button
End Class
