<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClinicAppointmentFrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ClinicAppointmentFrm))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lbldob = New System.Windows.Forms.Label
        Me.lblgender = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.btnclear = New System.Windows.Forms.Button
        Me.btnaddref = New System.Windows.Forms.Button
        Me.cmbreference = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.cmbsalesman = New System.Windows.Forms.ComboBox
        Me.lblCap8 = New System.Windows.Forms.Label
        Me.lblclosing = New System.Windows.Forms.Label
        Me.btnupdate = New System.Windows.Forms.Button
        Me.txtremarks = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblCap4 = New System.Windows.Forms.Label
        Me.txtcustAddress = New System.Windows.Forms.TextBox
        Me.txtRec1 = New System.Windows.Forms.TextBox
        Me.txtRec0 = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.cldrdate = New System.Windows.Forms.DateTimePicker
        Me.numVchrNo = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnattachment = New System.Windows.Forms.Button
        Me.btninvoice = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.txttoken = New System.Windows.Forms.TextBox
        Me.btnaddnote = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.lblinvoice = New System.Windows.Forms.Label
        Me.chkmedicine = New System.Windows.Forms.CheckBox
        Me.btnSlct = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtbooking = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.btnload = New System.Windows.Forms.Button
        Me.dtpto = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.dtpfrom = New System.Windows.Forms.DateTimePicker
        Me.grdlist = New System.Windows.Forms.DataGridView
        Me.btnpreview = New System.Windows.Forms.Button
        Me.chkFormat = New System.Windows.Forms.CheckBox
        Me.btndelete = New System.Windows.Forms.Button
        Me.btnadd = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(992, 33)
        Me.Panel1.TabIndex = 345446
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
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
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(39, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 18)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Appointment"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.btnadd)
        Me.Panel2.Controls.Add(Me.lbldob)
        Me.Panel2.Controls.Add(Me.lblgender)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.btnclear)
        Me.Panel2.Controls.Add(Me.btnaddref)
        Me.Panel2.Controls.Add(Me.cmbreference)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.cmbsalesman)
        Me.Panel2.Controls.Add(Me.lblCap8)
        Me.Panel2.Controls.Add(Me.lblclosing)
        Me.Panel2.Controls.Add(Me.btnupdate)
        Me.Panel2.Controls.Add(Me.txtremarks)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.lblCap4)
        Me.Panel2.Controls.Add(Me.txtcustAddress)
        Me.Panel2.Controls.Add(Me.txtRec1)
        Me.Panel2.Controls.Add(Me.txtRec0)
        Me.Panel2.Location = New System.Drawing.Point(6, 82)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(365, 348)
        Me.Panel2.TabIndex = 345447
        '
        'lbldob
        '
        Me.lbldob.AutoSize = True
        Me.lbldob.BackColor = System.Drawing.Color.Transparent
        Me.lbldob.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbldob.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldob.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbldob.Location = New System.Drawing.Point(81, 171)
        Me.lbldob.Name = "lbldob"
        Me.lbldob.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbldob.Size = New System.Drawing.Size(67, 14)
        Me.lbldob.TabIndex = 345482
        Me.lbldob.Text = "Date of Birth"
        '
        'lblgender
        '
        Me.lblgender.AutoSize = True
        Me.lblgender.BackColor = System.Drawing.Color.Transparent
        Me.lblgender.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblgender.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgender.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblgender.Location = New System.Drawing.Point(81, 148)
        Me.lblgender.Name = "lblgender"
        Me.lblgender.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblgender.Size = New System.Drawing.Size(22, 14)
        Me.lblgender.TabIndex = 345481
        Me.lblgender.Text = "m/f"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(11, 171)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(67, 14)
        Me.Label12.TabIndex = 345480
        Me.Label12.Text = "Date of Birth"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(11, 148)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(43, 14)
        Me.Label11.TabIndex = 345479
        Me.Label11.Text = "Gender"
        '
        'btnclear
        '
        Me.btnclear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnclear.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.ForeColor = System.Drawing.Color.White
        Me.btnclear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnclear.Location = New System.Drawing.Point(5, 308)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(100, 35)
        Me.btnclear.TabIndex = 345460
        Me.btnclear.Text = "New"
        Me.btnclear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclear.UseVisualStyleBackColor = False
        '
        'btnaddref
        '
        Me.btnaddref.Image = CType(resources.GetObject("btnaddref.Image"), System.Drawing.Image)
        Me.btnaddref.Location = New System.Drawing.Point(327, 277)
        Me.btnaddref.Name = "btnaddref"
        Me.btnaddref.Size = New System.Drawing.Size(28, 25)
        Me.btnaddref.TabIndex = 345478
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
        Me.cmbreference.Location = New System.Drawing.Point(84, 280)
        Me.cmbreference.Name = "cmbreference"
        Me.cmbreference.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbreference.Size = New System.Drawing.Size(235, 22)
        Me.cmbreference.TabIndex = 345476
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(9, 283)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(58, 14)
        Me.Label9.TabIndex = 345477
        Me.Label9.Text = "Reference"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(2, 396)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 15)
        Me.Label10.TabIndex = 345475
        Me.Label10.Text = "CL. Balance"
        Me.Label10.Visible = False
        '
        'cmbsalesman
        '
        Me.cmbsalesman.BackColor = System.Drawing.SystemColors.Window
        Me.cmbsalesman.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbsalesman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsalesman.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsalesman.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbsalesman.Location = New System.Drawing.Point(84, 252)
        Me.cmbsalesman.Name = "cmbsalesman"
        Me.cmbsalesman.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbsalesman.Size = New System.Drawing.Size(271, 22)
        Me.cmbsalesman.TabIndex = 3
        '
        'lblCap8
        '
        Me.lblCap8.AutoSize = True
        Me.lblCap8.BackColor = System.Drawing.Color.Transparent
        Me.lblCap8.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap8.Location = New System.Drawing.Point(9, 255)
        Me.lblCap8.Name = "lblCap8"
        Me.lblCap8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap8.Size = New System.Drawing.Size(39, 14)
        Me.lblCap8.TabIndex = 51
        Me.lblCap8.Text = "Doctor"
        '
        'lblclosing
        '
        Me.lblclosing.BackColor = System.Drawing.Color.Transparent
        Me.lblclosing.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclosing.Location = New System.Drawing.Point(72, 396)
        Me.lblclosing.Name = "lblclosing"
        Me.lblclosing.Size = New System.Drawing.Size(95, 19)
        Me.lblclosing.TabIndex = 165
        Me.lblclosing.Text = "0.00"
        Me.lblclosing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblclosing.Visible = False
        '
        'btnupdate
        '
        Me.btnupdate.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnupdate.Location = New System.Drawing.Point(255, 305)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(100, 35)
        Me.btnupdate.TabIndex = 4
        Me.btnupdate.Text = "&Update "
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'txtremarks
        '
        Me.txtremarks.AcceptsReturn = True
        Me.txtremarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtremarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtremarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtremarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtremarks.Location = New System.Drawing.Point(84, 192)
        Me.txtremarks.MaxLength = 250
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtremarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtremarks.Size = New System.Drawing.Size(271, 54)
        Me.txtremarks.TabIndex = 2
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(9, 194)
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
        Me.Label4.Location = New System.Drawing.Point(9, 32)
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
        Me.Label3.Location = New System.Drawing.Point(9, 11)
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
        Me.lblCap4.Location = New System.Drawing.Point(9, 58)
        Me.lblCap4.Name = "lblCap4"
        Me.lblCap4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap4.Size = New System.Drawing.Size(49, 14)
        Me.lblCap4.TabIndex = 46
        Me.lblCap4.Text = "Address"
        '
        'txtcustAddress
        '
        Me.txtcustAddress.AcceptsReturn = True
        Me.txtcustAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtcustAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcustAddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtcustAddress.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustAddress.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtcustAddress.Location = New System.Drawing.Point(84, 55)
        Me.txtcustAddress.MaxLength = 150
        Me.txtcustAddress.Multiline = True
        Me.txtcustAddress.Name = "txtcustAddress"
        Me.txtcustAddress.ReadOnly = True
        Me.txtcustAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtcustAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtcustAddress.Size = New System.Drawing.Size(271, 90)
        Me.txtcustAddress.TabIndex = 2
        Me.txtcustAddress.TabStop = False
        '
        'txtRec1
        '
        Me.txtRec1.AcceptsReturn = True
        Me.txtRec1.BackColor = System.Drawing.SystemColors.Window
        Me.txtRec1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRec1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRec1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRec1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRec1.Location = New System.Drawing.Point(84, 32)
        Me.txtRec1.MaxLength = 100
        Me.txtRec1.Name = "txtRec1"
        Me.txtRec1.ReadOnly = True
        Me.txtRec1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRec1.Size = New System.Drawing.Size(271, 20)
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
        Me.txtRec0.Location = New System.Drawing.Point(84, 9)
        Me.txtRec0.MaxLength = 10
        Me.txtRec0.Name = "txtRec0"
        Me.txtRec0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRec0.Size = New System.Drawing.Size(239, 20)
        Me.txtRec0.TabIndex = 0
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(6, 5)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(53, 15)
        Me.Label14.TabIndex = 345450
        Me.Label14.Text = "App. No."
        '
        'cldrdate
        '
        Me.cldrdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrdate.Location = New System.Drawing.Point(90, 54)
        Me.cldrdate.Name = "cldrdate"
        Me.cldrdate.Size = New System.Drawing.Size(95, 20)
        Me.cldrdate.TabIndex = 345449
        '
        'numVchrNo
        '
        Me.numVchrNo.BackColor = System.Drawing.Color.White
        Me.numVchrNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numVchrNo.Location = New System.Drawing.Point(90, 5)
        Me.numVchrNo.Name = "numVchrNo"
        Me.numVchrNo.ReadOnly = True
        Me.numVchrNo.Size = New System.Drawing.Size(95, 21)
        Me.numVchrNo.TabIndex = 345448
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 15)
        Me.Label2.TabIndex = 345451
        Me.Label2.Text = "Date"
        '
        'grdVoucher
        '
        Me.grdVoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdVoucher.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.grdVoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdVoucher.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdVoucher.Location = New System.Drawing.Point(372, 24)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(596, 446)
        Me.grdVoucher.TabIndex = 345452
        Me.grdVoucher.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(879, 591)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(101, 35)
        Me.btnExit.TabIndex = 345453
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnattachment
        '
        Me.btnattachment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnattachment.BackColor = System.Drawing.Color.SteelBlue
        Me.btnattachment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnattachment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnattachment.ForeColor = System.Drawing.Color.White
        Me.btnattachment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnattachment.Location = New System.Drawing.Point(766, 476)
        Me.btnattachment.Name = "btnattachment"
        Me.btnattachment.Size = New System.Drawing.Size(100, 35)
        Me.btnattachment.TabIndex = 345455
        Me.btnattachment.Text = "Attachment"
        Me.btnattachment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnattachment.UseVisualStyleBackColor = False
        Me.btnattachment.Visible = False
        '
        'btninvoice
        '
        Me.btninvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btninvoice.BackColor = System.Drawing.Color.SteelBlue
        Me.btninvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btninvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btninvoice.ForeColor = System.Drawing.Color.White
        Me.btninvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btninvoice.Location = New System.Drawing.Point(372, 476)
        Me.btninvoice.Name = "btninvoice"
        Me.btninvoice.Size = New System.Drawing.Size(100, 35)
        Me.btninvoice.TabIndex = 345456
        Me.btninvoice.Text = "Invoice"
        Me.btninvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btninvoice.UseVisualStyleBackColor = False
        Me.btninvoice.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Green
        Me.Label5.Location = New System.Drawing.Point(239, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 20)
        Me.Label5.TabIndex = 345458
        Me.Label5.Text = "Token"
        '
        'txttoken
        '
        Me.txttoken.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttoken.Location = New System.Drawing.Point(303, 5)
        Me.txttoken.Name = "txttoken"
        Me.txttoken.ReadOnly = True
        Me.txttoken.Size = New System.Drawing.Size(58, 29)
        Me.txttoken.TabIndex = 345457
        Me.txttoken.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnaddnote
        '
        Me.btnaddnote.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnaddnote.BackColor = System.Drawing.Color.SteelBlue
        Me.btnaddnote.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnaddnote.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnaddnote.ForeColor = System.Drawing.Color.White
        Me.btnaddnote.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnaddnote.Location = New System.Drawing.Point(868, 476)
        Me.btnaddnote.Name = "btnaddnote"
        Me.btnaddnote.Size = New System.Drawing.Size(100, 35)
        Me.btnaddnote.TabIndex = 345459
        Me.btnaddnote.Text = "Add Note"
        Me.btnaddnote.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnaddnote.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(5, 39)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(984, 546)
        Me.TabControl1.TabIndex = 345461
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lblinvoice)
        Me.TabPage1.Controls.Add(Me.chkmedicine)
        Me.TabPage1.Controls.Add(Me.btnSlct)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.txtbooking)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.btnaddnote)
        Me.TabPage1.Controls.Add(Me.btninvoice)
        Me.TabPage1.Controls.Add(Me.btnattachment)
        Me.TabPage1.Controls.Add(Me.Panel2)
        Me.TabPage1.Controls.Add(Me.Label14)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.numVchrNo)
        Me.TabPage1.Controls.Add(Me.txttoken)
        Me.TabPage1.Controls.Add(Me.cldrdate)
        Me.TabPage1.Controls.Add(Me.grdVoucher)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(976, 520)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Entry"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'lblinvoice
        '
        Me.lblinvoice.AutoSize = True
        Me.lblinvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblinvoice.Location = New System.Drawing.Point(9, 433)
        Me.lblinvoice.Name = "lblinvoice"
        Me.lblinvoice.Size = New System.Drawing.Size(61, 13)
        Me.lblinvoice.TabIndex = 345481
        Me.lblinvoice.Text = "Invoice : "
        Me.lblinvoice.Visible = False
        '
        'chkmedicine
        '
        Me.chkmedicine.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkmedicine.AutoSize = True
        Me.chkmedicine.BackColor = System.Drawing.Color.Transparent
        Me.chkmedicine.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkmedicine.Location = New System.Drawing.Point(478, 492)
        Me.chkmedicine.Name = "chkmedicine"
        Me.chkmedicine.Size = New System.Drawing.Size(104, 19)
        Me.chkmedicine.TabIndex = 345480
        Me.chkmedicine.Text = "With Medicine"
        Me.chkmedicine.UseVisualStyleBackColor = False
        '
        'btnSlct
        '
        Me.btnSlct.BackColor = System.Drawing.SystemColors.Control
        Me.btnSlct.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnSlct.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSlct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSlct.Image = CType(resources.GetObject("btnSlct.Image"), System.Drawing.Image)
        Me.btnSlct.Location = New System.Drawing.Point(331, 52)
        Me.btnSlct.Name = "btnSlct"
        Me.btnSlct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnSlct.Size = New System.Drawing.Size(30, 26)
        Me.btnSlct.TabIndex = 345464
        Me.btnSlct.TabStop = False
        Me.btnSlct.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSlct.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(199, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 15)
        Me.Label8.TabIndex = 345463
        Me.Label8.Text = "Booking"
        '
        'txtbooking
        '
        Me.txtbooking.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbooking.Location = New System.Drawing.Point(256, 54)
        Me.txtbooking.Name = "txtbooking"
        Me.txtbooking.ReadOnly = True
        Me.txtbooking.Size = New System.Drawing.Size(69, 21)
        Me.txtbooking.TabIndex = 345462
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(369, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(94, 15)
        Me.Label6.TabIndex = 345461
        Me.Label6.Text = "Previous History"
        '
        'TabPage2
        '
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
        Me.TabPage2.Size = New System.Drawing.Size(976, 520)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Search"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.Checked = True
        Me.chkSearch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(151, 503)
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
        Me.txtSeq.Location = New System.Drawing.Point(151, 480)
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
        Me.cmbOrder.Location = New System.Drawing.Point(7, 478)
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
        Me.btnload.Location = New System.Drawing.Point(890, 491)
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
        Me.dtpto.Location = New System.Drawing.Point(778, 494)
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
        Me.Label7.Location = New System.Drawing.Point(566, 494)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(94, 15)
        Me.Label7.TabIndex = 345455
        Me.Label7.Text = "Date Parameter"
        '
        'dtpfrom
        '
        Me.dtpfrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfrom.Location = New System.Drawing.Point(666, 494)
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
        Me.grdlist.Size = New System.Drawing.Size(964, 467)
        Me.grdlist.TabIndex = 345453
        Me.grdlist.TabStop = False
        '
        'btnpreview
        '
        Me.btnpreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnpreview.BackColor = System.Drawing.Color.SteelBlue
        Me.btnpreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnpreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnpreview.ForeColor = System.Drawing.Color.White
        Me.btnpreview.Location = New System.Drawing.Point(775, 591)
        Me.btnpreview.Name = "btnpreview"
        Me.btnpreview.Size = New System.Drawing.Size(101, 35)
        Me.btnpreview.TabIndex = 345462
        Me.btnpreview.Text = "Preview"
        Me.btnpreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnpreview.UseVisualStyleBackColor = False
        '
        'chkFormat
        '
        Me.chkFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(704, 591)
        Me.chkFormat.Name = "chkFormat"
        Me.chkFormat.Size = New System.Drawing.Size(65, 19)
        Me.chkFormat.TabIndex = 345479
        Me.chkFormat.Text = "&Format"
        Me.chkFormat.UseVisualStyleBackColor = False
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.BackColor = System.Drawing.Color.SteelBlue
        Me.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.ForeColor = System.Drawing.Color.White
        Me.btndelete.Location = New System.Drawing.Point(15, 591)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(101, 35)
        Me.btndelete.TabIndex = 345480
        Me.btndelete.Text = "Delete"
        Me.btndelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndelete.UseVisualStyleBackColor = False
        '
        'btnadd
        '
        Me.btnadd.Image = Global.SMSMP.My.Resources.Resources.button_edit
        Me.btnadd.Location = New System.Drawing.Point(327, 5)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(28, 25)
        Me.btnadd.TabIndex = 345483
        Me.btnadd.TabStop = False
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'ClinicAppointmentFrm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(992, 638)
        Me.ControlBox = False
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.chkFormat)
        Me.Controls.Add(Me.btnpreview)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "ClinicAppointmentFrm"
        Me.Text = "ClinicAppointmentFrm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.grdlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
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
    Public WithEvents txtcustAddress As System.Windows.Forms.TextBox
    Public WithEvents txtRec1 As System.Windows.Forms.TextBox
    Public WithEvents txtRec0 As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cldrdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents numVchrNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grdVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents btnattachment As System.Windows.Forms.Button
    Friend WithEvents btninvoice As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txttoken As System.Windows.Forms.TextBox
    Friend WithEvents btnaddnote As System.Windows.Forms.Button
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents dtpto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpfrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents grdlist As System.Windows.Forms.DataGridView
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents btnpreview As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtbooking As System.Windows.Forms.TextBox
    Public WithEvents btnSlct As System.Windows.Forms.Button
    Public WithEvents cmbreference As System.Windows.Forms.ComboBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnaddref As System.Windows.Forms.Button
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents lbldob As System.Windows.Forms.Label
    Public WithEvents lblgender As System.Windows.Forms.Label
    Friend WithEvents chkFormat As System.Windows.Forms.CheckBox
    Friend WithEvents btndelete As System.Windows.Forms.Button
    Friend WithEvents chkmedicine As System.Windows.Forms.CheckBox
    Friend WithEvents lblinvoice As System.Windows.Forms.Label
    Friend WithEvents btnadd As System.Windows.Forms.Button
End Class
