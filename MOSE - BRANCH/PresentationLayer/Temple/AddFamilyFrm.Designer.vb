<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddFamilyFrm
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
        Me.lblname = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cmblives = New System.Windows.Forms.ComboBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.chkwu = New System.Windows.Forms.CheckBox
        Me.rdofemale = New System.Windows.Forms.RadioButton
        Me.rdomale = New System.Windows.Forms.RadioButton
        Me.cmbgroup = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtroll = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtblood = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtintute = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtstandared = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtoccupation = New System.Windows.Forms.TextBox
        Me.lblCap4 = New System.Windows.Forms.Label
        Me.lblCap5 = New System.Windows.Forms.Label
        Me.txtrelation = New System.Windows.Forms.TextBox
        Me.lblCap7 = New System.Windows.Forms.Label
        Me.txtname = New System.Windows.Forms.TextBox
        Me.btnclose = New System.Windows.Forms.Button
        Me.cmdOk = New System.Windows.Forms.Button
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.lblname)
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(395, 32)
        Me.Panel2.TabIndex = 345462
        '
        'lblname
        '
        Me.lblname.AutoSize = True
        Me.lblname.BackColor = System.Drawing.Color.White
        Me.lblname.Font = New System.Drawing.Font("Goudy Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblname.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblname.Location = New System.Drawing.Point(41, 5)
        Me.lblname.Name = "lblname"
        Me.lblname.Size = New System.Drawing.Size(188, 21)
        Me.lblname.TabIndex = 345458
        Me.lblname.Text = "ADD FAMILY MEMBERS"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 20)
        Me.PictureBox1.TabIndex = 345457
        Me.PictureBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.cmblives)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.chkwu)
        Me.Panel1.Controls.Add(Me.rdofemale)
        Me.Panel1.Controls.Add(Me.rdomale)
        Me.Panel1.Controls.Add(Me.cmbgroup)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.txtroll)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txtblood)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtintute)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtstandared)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtoccupation)
        Me.Panel1.Controls.Add(Me.lblCap4)
        Me.Panel1.Controls.Add(Me.lblCap5)
        Me.Panel1.Controls.Add(Me.txtrelation)
        Me.Panel1.Controls.Add(Me.lblCap7)
        Me.Panel1.Controls.Add(Me.txtname)
        Me.Panel1.Location = New System.Drawing.Point(12, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(368, 268)
        Me.Panel1.TabIndex = 345463
        '
        'cmblives
        '
        Me.cmblives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmblives.FormattingEnabled = True
        Me.cmblives.Items.AddRange(New Object() {"Kerala", "Outside Kerala", "Outside India"})
        Me.cmblives.Location = New System.Drawing.Point(69, 238)
        Me.cmblives.Name = "cmblives"
        Me.cmblives.Size = New System.Drawing.Size(149, 21)
        Me.cmblives.TabIndex = 9
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(4, 238)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(44, 14)
        Me.Label18.TabIndex = 345473
        Me.Label18.Text = "Lives In"
        '
        'chkwu
        '
        Me.chkwu.AutoSize = True
        Me.chkwu.Location = New System.Drawing.Point(70, 174)
        Me.chkwu.Name = "chkwu"
        Me.chkwu.Size = New System.Drawing.Size(157, 17)
        Me.chkwu.TabIndex = 345471
        Me.chkwu.Text = "Is Womens Union Member?"
        Me.chkwu.UseVisualStyleBackColor = True
        '
        'rdofemale
        '
        Me.rdofemale.AutoSize = True
        Me.rdofemale.Location = New System.Drawing.Point(115, 60)
        Me.rdofemale.Name = "rdofemale"
        Me.rdofemale.Size = New System.Drawing.Size(59, 17)
        Me.rdofemale.TabIndex = 345469
        Me.rdofemale.Text = "Female"
        Me.rdofemale.UseVisualStyleBackColor = True
        '
        'rdomale
        '
        Me.rdomale.AutoSize = True
        Me.rdomale.Checked = True
        Me.rdomale.Location = New System.Drawing.Point(70, 60)
        Me.rdomale.Name = "rdomale"
        Me.rdomale.Size = New System.Drawing.Size(48, 17)
        Me.rdomale.TabIndex = 345468
        Me.rdomale.TabStop = True
        Me.rdomale.Text = "Male"
        Me.rdomale.UseVisualStyleBackColor = True
        '
        'cmbgroup
        '
        Me.cmbgroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbgroup.FormattingEnabled = True
        Me.cmbgroup.Items.AddRange(New Object() {"None", "Student"})
        Me.cmbgroup.Location = New System.Drawing.Point(70, 82)
        Me.cmbgroup.Name = "cmbgroup"
        Me.cmbgroup.Size = New System.Drawing.Size(149, 21)
        Me.cmbgroup.TabIndex = 3
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(4, 82)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(51, 14)
        Me.Label10.TabIndex = 345467
        Me.Label10.Text = "Category"
        '
        'txtroll
        '
        Me.txtroll.AcceptsReturn = True
        Me.txtroll.BackColor = System.Drawing.SystemColors.Window
        Me.txtroll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtroll.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtroll.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtroll.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtroll.Location = New System.Drawing.Point(70, 193)
        Me.txtroll.MaxLength = 30
        Me.txtroll.Name = "txtroll"
        Me.txtroll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtroll.Size = New System.Drawing.Size(285, 20)
        Me.txtroll.TabIndex = 7
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(4, 196)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(44, 14)
        Me.Label13.TabIndex = 69
        Me.Label13.Text = "WU Roll"
        '
        'txtblood
        '
        Me.txtblood.AcceptsReturn = True
        Me.txtblood.BackColor = System.Drawing.SystemColors.Window
        Me.txtblood.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtblood.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtblood.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtblood.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtblood.Location = New System.Drawing.Point(70, 215)
        Me.txtblood.MaxLength = 150
        Me.txtblood.Name = "txtblood"
        Me.txtblood.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtblood.Size = New System.Drawing.Size(285, 20)
        Me.txtblood.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(4, 218)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(67, 14)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "Blood Group"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(4, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(34, 14)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "Name"
        '
        'txtintute
        '
        Me.txtintute.AcceptsReturn = True
        Me.txtintute.BackColor = System.Drawing.SystemColors.Window
        Me.txtintute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtintute.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtintute.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtintute.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtintute.Location = New System.Drawing.Point(70, 128)
        Me.txtintute.MaxLength = 150
        Me.txtintute.Name = "txtintute"
        Me.txtintute.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtintute.Size = New System.Drawing.Size(285, 20)
        Me.txtintute.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(4, 131)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(39, 14)
        Me.Label2.TabIndex = 57
        Me.Label2.Text = "Instute"
        '
        'txtstandared
        '
        Me.txtstandared.AcceptsReturn = True
        Me.txtstandared.BackColor = System.Drawing.SystemColors.Window
        Me.txtstandared.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtstandared.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtstandared.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstandared.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtstandared.Location = New System.Drawing.Point(70, 106)
        Me.txtstandared.MaxLength = 30
        Me.txtstandared.Name = "txtstandared"
        Me.txtstandared.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtstandared.Size = New System.Drawing.Size(285, 20)
        Me.txtstandared.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(4, 109)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(66, 14)
        Me.Label1.TabIndex = 55
        Me.Label1.Text = "Student Std."
        '
        'txtoccupation
        '
        Me.txtoccupation.AcceptsReturn = True
        Me.txtoccupation.BackColor = System.Drawing.SystemColors.Window
        Me.txtoccupation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtoccupation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtoccupation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtoccupation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtoccupation.Location = New System.Drawing.Point(70, 151)
        Me.txtoccupation.MaxLength = 150
        Me.txtoccupation.Name = "txtoccupation"
        Me.txtoccupation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtoccupation.Size = New System.Drawing.Size(285, 20)
        Me.txtoccupation.TabIndex = 6
        '
        'lblCap4
        '
        Me.lblCap4.AutoSize = True
        Me.lblCap4.BackColor = System.Drawing.Color.Transparent
        Me.lblCap4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap4.Location = New System.Drawing.Point(4, 37)
        Me.lblCap4.Name = "lblCap4"
        Me.lblCap4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap4.Size = New System.Drawing.Size(45, 14)
        Me.lblCap4.TabIndex = 46
        Me.lblCap4.Text = "Relation"
        '
        'lblCap5
        '
        Me.lblCap5.AutoSize = True
        Me.lblCap5.BackColor = System.Drawing.Color.Transparent
        Me.lblCap5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap5.Location = New System.Drawing.Point(4, 58)
        Me.lblCap5.Name = "lblCap5"
        Me.lblCap5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap5.Size = New System.Drawing.Size(43, 14)
        Me.lblCap5.TabIndex = 47
        Me.lblCap5.Text = "Gender"
        '
        'txtrelation
        '
        Me.txtrelation.AcceptsReturn = True
        Me.txtrelation.BackColor = System.Drawing.SystemColors.Window
        Me.txtrelation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtrelation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtrelation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrelation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtrelation.Location = New System.Drawing.Point(70, 34)
        Me.txtrelation.MaxLength = 150
        Me.txtrelation.Name = "txtrelation"
        Me.txtrelation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtrelation.Size = New System.Drawing.Size(285, 20)
        Me.txtrelation.TabIndex = 2
        '
        'lblCap7
        '
        Me.lblCap7.AutoSize = True
        Me.lblCap7.BackColor = System.Drawing.Color.Transparent
        Me.lblCap7.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap7.Location = New System.Drawing.Point(4, 154)
        Me.lblCap7.Name = "lblCap7"
        Me.lblCap7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap7.Size = New System.Drawing.Size(62, 14)
        Me.lblCap7.TabIndex = 49
        Me.lblCap7.Text = "Occupation"
        '
        'txtname
        '
        Me.txtname.AcceptsReturn = True
        Me.txtname.BackColor = System.Drawing.SystemColors.Window
        Me.txtname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtname.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtname.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtname.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtname.Location = New System.Drawing.Point(70, 12)
        Me.txtname.MaxLength = 100
        Me.txtname.Name = "txtname"
        Me.txtname.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtname.Size = New System.Drawing.Size(285, 20)
        Me.txtname.TabIndex = 1
        '
        'btnclose
        '
        Me.btnclose.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ForeColor = System.Drawing.Color.White
        Me.btnclose.Location = New System.Drawing.Point(298, 321)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(82, 35)
        Me.btnclose.TabIndex = 345464
        Me.btnclose.Text = "E&xit"
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.Color.White
        Me.cmdOk.Location = New System.Drawing.Point(211, 321)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(81, 35)
        Me.cmdOk.TabIndex = 10
        Me.cmdOk.Text = "&Update"
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'AddFamilyFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(395, 367)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddFamilyFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Family Members"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblname As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents txtroll As System.Windows.Forms.TextBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents txtblood As System.Windows.Forms.TextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents txtintute As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtstandared As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtoccupation As System.Windows.Forms.TextBox
    Public WithEvents lblCap4 As System.Windows.Forms.Label
    Public WithEvents lblCap5 As System.Windows.Forms.Label
    Public WithEvents txtrelation As System.Windows.Forms.TextBox
    Public WithEvents lblCap7 As System.Windows.Forms.Label
    Public WithEvents txtname As System.Windows.Forms.TextBox
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents rdofemale As System.Windows.Forms.RadioButton
    Friend WithEvents rdomale As System.Windows.Forms.RadioButton
    Friend WithEvents cmbgroup As System.Windows.Forms.ComboBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents chkwu As System.Windows.Forms.CheckBox
    Friend WithEvents cmblives As System.Windows.Forms.ComboBox
    Public WithEvents Label18 As System.Windows.Forms.Label
End Class
