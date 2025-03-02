<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QuickPatientCreateFrm
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnnextnumber = New System.Windows.Forms.Button
        Me.cmbdoctor = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtphone = New System.Windows.Forms.TextBox
        Me.txtadd3 = New System.Windows.Forms.TextBox
        Me.txtadd2 = New System.Windows.Forms.TextBox
        Me.txtadd1 = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblclosing = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblCap4 = New System.Windows.Forms.Label
        Me.txtRec1 = New System.Windows.Forms.TextBox
        Me.txtRec0 = New System.Windows.Forms.TextBox
        Me.cmdOk = New System.Windows.Forms.Button
        Me.btnexit = New System.Windows.Forms.Button
        Me.cmbAccGroup = New System.Windows.Forms.ComboBox
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(450, 33)
        Me.Panel1.TabIndex = 345448
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
        Me.Label1.Size = New System.Drawing.Size(117, 18)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Patient Master"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.btnnextnumber)
        Me.Panel2.Controls.Add(Me.cmbdoctor)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.txtphone)
        Me.Panel2.Controls.Add(Me.txtadd3)
        Me.Panel2.Controls.Add(Me.txtadd2)
        Me.Panel2.Controls.Add(Me.txtadd1)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.lblclosing)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.lblCap4)
        Me.Panel2.Controls.Add(Me.txtRec1)
        Me.Panel2.Controls.Add(Me.txtRec0)
        Me.Panel2.Location = New System.Drawing.Point(43, 39)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(365, 174)
        Me.Panel2.TabIndex = 345449
        '
        'btnnextnumber
        '
        Me.btnnextnumber.BackColor = System.Drawing.Color.SteelBlue
        Me.btnnextnumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnnextnumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnnextnumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnextnumber.ForeColor = System.Drawing.Color.White
        Me.btnnextnumber.Location = New System.Drawing.Point(327, 2)
        Me.btnnextnumber.Name = "btnnextnumber"
        Me.btnnextnumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnnextnumber.Size = New System.Drawing.Size(31, 28)
        Me.btnnextnumber.TabIndex = 345453
        Me.btnnextnumber.Text = ">>"
        Me.btnnextnumber.UseVisualStyleBackColor = False
        '
        'cmbdoctor
        '
        Me.cmbdoctor.BackColor = System.Drawing.SystemColors.Window
        Me.cmbdoctor.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbdoctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbdoctor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbdoctor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbdoctor.Location = New System.Drawing.Point(77, 147)
        Me.cmbdoctor.Name = "cmbdoctor"
        Me.cmbdoctor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbdoctor.Size = New System.Drawing.Size(284, 22)
        Me.cmbdoctor.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(3, 150)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(39, 14)
        Me.Label8.TabIndex = 345482
        Me.Label8.Text = "Doctor"
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
        Me.txtRec0.Size = New System.Drawing.Size(244, 20)
        Me.txtRec0.TabIndex = 0
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.Color.White
        Me.cmdOk.Location = New System.Drawing.Point(240, 218)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(81, 35)
        Me.cmdOk.TabIndex = 7
        Me.cmdOk.Text = "&Update"
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'btnexit
        '
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.Location = New System.Drawing.Point(327, 218)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnexit.Size = New System.Drawing.Size(81, 35)
        Me.btnexit.TabIndex = 345451
        Me.btnexit.Text = "E&xit"
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'cmbAccGroup
        '
        Me.cmbAccGroup.BackColor = System.Drawing.SystemColors.Window
        Me.cmbAccGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbAccGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAccGroup.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAccGroup.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbAccGroup.Location = New System.Drawing.Point(43, 231)
        Me.cmbAccGroup.Name = "cmbAccGroup"
        Me.cmbAccGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbAccGroup.Size = New System.Drawing.Size(163, 22)
        Me.cmbAccGroup.TabIndex = 345452
        Me.cmbAccGroup.Visible = False
        '
        'QuickPatientCreateFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(450, 275)
        Me.Controls.Add(Me.cmbAccGroup)
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "QuickPatientCreateFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generate OP Number "
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Public WithEvents cmbdoctor As System.Windows.Forms.ComboBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents txtphone As System.Windows.Forms.TextBox
    Public WithEvents txtadd3 As System.Windows.Forms.TextBox
    Public WithEvents txtadd2 As System.Windows.Forms.TextBox
    Public WithEvents txtadd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblclosing As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblCap4 As System.Windows.Forms.Label
    Public WithEvents txtRec1 As System.Windows.Forms.TextBox
    Public WithEvents txtRec0 As System.Windows.Forms.TextBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents btnexit As System.Windows.Forms.Button
    Public WithEvents cmbAccGroup As System.Windows.Forms.ComboBox
    Public WithEvents btnnextnumber As System.Windows.Forms.Button
End Class
