<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MembershipAttendanceFrm
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.cldrdate = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.grdattendancelist = New System.Windows.Forms.DataGridView
        Me.cmbpackage = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtcustomer = New System.Windows.Forms.TextBox
        Me.btnadd = New System.Windows.Forms.Button
        Me.btnremove = New System.Windows.Forms.Button
        Me.dtpcheckin = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.dtpcheckout = New System.Windows.Forms.DateTimePicker
        Me.txtremarks = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtnos = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblclasses = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.lblattendance = New System.Windows.Forms.Label
        Me.lblexpirydate = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.btnload = New System.Windows.Forms.Button
        Me.txthour = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdattendancelist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1189, 36)
        Me.Panel1.TabIndex = 345460
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
        Me.Label1.Location = New System.Drawing.Point(39, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 18)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Attendance"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(1076, 512)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(101, 35)
        Me.btnExit.TabIndex = 345462
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'cldrdate
        '
        Me.cldrdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrdate.Location = New System.Drawing.Point(43, 42)
        Me.cldrdate.Name = "cldrdate"
        Me.cldrdate.Size = New System.Drawing.Size(90, 20)
        Me.cldrdate.TabIndex = 345467
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(10, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 345468
        Me.Label3.Text = "Date"
        '
        'grdattendancelist
        '
        Me.grdattendancelist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdattendancelist.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdattendancelist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdattendancelist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdattendancelist.Location = New System.Drawing.Point(13, 121)
        Me.grdattendancelist.Name = "grdattendancelist"
        Me.grdattendancelist.Size = New System.Drawing.Size(1164, 385)
        Me.grdattendancelist.TabIndex = 345469
        '
        'cmbpackage
        '
        Me.cmbpackage.BackColor = System.Drawing.SystemColors.Window
        Me.cmbpackage.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbpackage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbpackage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbpackage.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbpackage.Location = New System.Drawing.Point(663, 42)
        Me.cmbpackage.Name = "cmbpackage"
        Me.cmbpackage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbpackage.Size = New System.Drawing.Size(181, 22)
        Me.cmbpackage.TabIndex = 1
        Me.cmbpackage.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(606, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 345471
        Me.Label2.Text = "Package"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(215, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 345472
        Me.Label4.Text = "Customer"
        '
        'txtcustomer
        '
        Me.txtcustomer.AcceptsReturn = True
        Me.txtcustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtcustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtcustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustomer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtcustomer.Location = New System.Drawing.Point(272, 42)
        Me.txtcustomer.MaxLength = 500
        Me.txtcustomer.Name = "txtcustomer"
        Me.txtcustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtcustomer.Size = New System.Drawing.Size(331, 20)
        Me.txtcustomer.TabIndex = 0
        '
        'btnadd
        '
        Me.btnadd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.ForeColor = System.Drawing.Color.White
        Me.btnadd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnadd.Location = New System.Drawing.Point(852, 85)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(56, 26)
        Me.btnadd.TabIndex = 6
        Me.btnadd.Text = "Add"
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'btnremove
        '
        Me.btnremove.BackColor = System.Drawing.Color.SteelBlue
        Me.btnremove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnremove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnremove.ForeColor = System.Drawing.Color.White
        Me.btnremove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnremove.Location = New System.Drawing.Point(910, 85)
        Me.btnremove.Name = "btnremove"
        Me.btnremove.Size = New System.Drawing.Size(69, 26)
        Me.btnremove.TabIndex = 345475
        Me.btnremove.Text = "Remove"
        Me.btnremove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnremove.UseVisualStyleBackColor = False
        '
        'dtpcheckin
        '
        Me.dtpcheckin.CalendarMonthBackground = System.Drawing.Color.IndianRed
        Me.dtpcheckin.CustomFormat = "hh:mm:ss tt"
        Me.dtpcheckin.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpcheckin.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpcheckin.Location = New System.Drawing.Point(10, 89)
        Me.dtpcheckin.Name = "dtpcheckin"
        Me.dtpcheckin.Size = New System.Drawing.Size(128, 22)
        Me.dtpcheckin.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(10, 73)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 13)
        Me.Label5.TabIndex = 345477
        Me.Label5.Text = "Check IN Time"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(199, 73)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 13)
        Me.Label6.TabIndex = 345479
        Me.Label6.Text = "Check OUT Time"
        '
        'dtpcheckout
        '
        Me.dtpcheckout.CalendarMonthBackground = System.Drawing.Color.IndianRed
        Me.dtpcheckout.CustomFormat = "hh:mm:ss tt"
        Me.dtpcheckout.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpcheckout.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpcheckout.Location = New System.Drawing.Point(199, 89)
        Me.dtpcheckout.Name = "dtpcheckout"
        Me.dtpcheckout.Size = New System.Drawing.Size(126, 22)
        Me.dtpcheckout.TabIndex = 3
        '
        'txtremarks
        '
        Me.txtremarks.AcceptsReturn = True
        Me.txtremarks.BackColor = System.Drawing.Color.White
        Me.txtremarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtremarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtremarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtremarks.Location = New System.Drawing.Point(406, 89)
        Me.txtremarks.MaxLength = 500
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtremarks.Size = New System.Drawing.Size(331, 20)
        Me.txtremarks.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(349, 92)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 345480
        Me.Label7.Text = "Remarks"
        '
        'txtnos
        '
        Me.txtnos.AcceptsReturn = True
        Me.txtnos.BackColor = System.Drawing.Color.White
        Me.txtnos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtnos.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtnos.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnos.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtnos.Location = New System.Drawing.Point(793, 89)
        Me.txtnos.MaxLength = 500
        Me.txtnos.Name = "txtnos"
        Me.txtnos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtnos.Size = New System.Drawing.Size(53, 20)
        Me.txtnos.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(742, 92)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(45, 13)
        Me.Label8.TabIndex = 345482
        Me.Label8.Text = "Persons"
        '
        'Timer1
        '
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.Checked = True
        Me.chkSearch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(530, 513)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345485
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
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
        Me.txtSeq.Location = New System.Drawing.Point(185, 512)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(331, 20)
        Me.txtSeq.TabIndex = 345484
        '
        'cmbOrder
        '
        Me.cmbOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOrder.Location = New System.Drawing.Point(13, 512)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOrder.Size = New System.Drawing.Size(166, 22)
        Me.cmbOrder.TabIndex = 345483
        Me.cmbOrder.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(982, 68)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 345486
        Me.Label9.Text = "Total Classes"
        '
        'lblclasses
        '
        Me.lblclasses.BackColor = System.Drawing.Color.Transparent
        Me.lblclasses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblclasses.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclasses.ForeColor = System.Drawing.Color.Green
        Me.lblclasses.Location = New System.Drawing.Point(1071, 68)
        Me.lblclasses.Name = "lblclasses"
        Me.lblclasses.Size = New System.Drawing.Size(106, 20)
        Me.lblclasses.TabIndex = 345487
        Me.lblclasses.Text = "0"
        Me.lblclasses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(982, 96)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(89, 13)
        Me.Label11.TabIndex = 345488
        Me.Label11.Text = "Total Attendance"
        '
        'lblattendance
        '
        Me.lblattendance.BackColor = System.Drawing.Color.Transparent
        Me.lblattendance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblattendance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblattendance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblattendance.Location = New System.Drawing.Point(1071, 96)
        Me.lblattendance.Name = "lblattendance"
        Me.lblattendance.Size = New System.Drawing.Size(106, 20)
        Me.lblattendance.TabIndex = 345489
        Me.lblattendance.Text = "0"
        Me.lblattendance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblexpirydate
        '
        Me.lblexpirydate.BackColor = System.Drawing.Color.Transparent
        Me.lblexpirydate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblexpirydate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblexpirydate.ForeColor = System.Drawing.Color.Green
        Me.lblexpirydate.Location = New System.Drawing.Point(1071, 41)
        Me.lblexpirydate.Name = "lblexpirydate"
        Me.lblexpirydate.Size = New System.Drawing.Size(106, 20)
        Me.lblexpirydate.TabIndex = 345491
        Me.lblexpirydate.Text = "0"
        Me.lblexpirydate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(982, 41)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(61, 13)
        Me.Label12.TabIndex = 345490
        Me.Label12.Text = "Expiry Date"
        '
        'btnload
        '
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnload.Location = New System.Drawing.Point(139, 38)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(56, 26)
        Me.btnload.TabIndex = 345492
        Me.btnload.Text = "Load"
        Me.btnload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnload.UseVisualStyleBackColor = False
        '
        'txthour
        '
        Me.txthour.AcceptsReturn = True
        Me.txthour.BackColor = System.Drawing.Color.White
        Me.txthour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txthour.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txthour.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txthour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txthour.Location = New System.Drawing.Point(142, 90)
        Me.txthour.MaxLength = 500
        Me.txthour.Name = "txthour"
        Me.txthour.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txthour.Size = New System.Drawing.Size(53, 20)
        Me.txthour.TabIndex = 345493
        Me.txthour.Text = "1"
        Me.txthour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(139, 73)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(30, 13)
        Me.Label10.TabIndex = 345494
        Me.Label10.Text = "Hour"
        '
        'MembershipAttendanceFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1189, 553)
        Me.ControlBox = False
        Me.Controls.Add(Me.txthour)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.btnload)
        Me.Controls.Add(Me.lblexpirydate)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.lblattendance)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblclasses)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.txtSeq)
        Me.Controls.Add(Me.cmbOrder)
        Me.Controls.Add(Me.txtnos)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtremarks)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.dtpcheckout)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dtpcheckin)
        Me.Controls.Add(Me.btnremove)
        Me.Controls.Add(Me.btnadd)
        Me.Controls.Add(Me.txtcustomer)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbpackage)
        Me.Controls.Add(Me.grdattendancelist)
        Me.Controls.Add(Me.cldrdate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "MembershipAttendanceFrm"
        Me.Text = "MembershipAttendanceFrm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdattendancelist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents cldrdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grdattendancelist As System.Windows.Forms.DataGridView
    Public WithEvents cmbpackage As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents txtcustomer As System.Windows.Forms.TextBox
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents btnremove As System.Windows.Forms.Button
    Friend WithEvents dtpcheckin As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpcheckout As System.Windows.Forms.DateTimePicker
    Public WithEvents txtremarks As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents txtnos As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblclasses As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblattendance As System.Windows.Forms.Label
    Friend WithEvents lblexpirydate As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnload As System.Windows.Forms.Button
    Public WithEvents txthour As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
End Class
