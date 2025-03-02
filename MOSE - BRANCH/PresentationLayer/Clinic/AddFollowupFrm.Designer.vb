<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddFollowupFrm
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
        Me.grpcallupdate = New System.Windows.Forms.GroupBox
        Me.chkcreatenew = New System.Windows.Forms.CheckBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.dtpcallnextdate = New System.Windows.Forms.DateTimePicker
        Me.txtremark = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtphonenumber = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtopnumber = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtcompanyname = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtcustAddress = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtpurpose = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnupdate = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.dtpcalldate = New System.Windows.Forms.DateTimePicker
        Me.cmbsalesman = New System.Windows.Forms.ComboBox
        Me.grpcallupdate.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpcallupdate
        '
        Me.grpcallupdate.Controls.Add(Me.chkcreatenew)
        Me.grpcallupdate.Controls.Add(Me.Label7)
        Me.grpcallupdate.Controls.Add(Me.dtpcallnextdate)
        Me.grpcallupdate.Controls.Add(Me.txtremark)
        Me.grpcallupdate.Controls.Add(Me.Label5)
        Me.grpcallupdate.Enabled = False
        Me.grpcallupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpcallupdate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.grpcallupdate.Location = New System.Drawing.Point(9, 290)
        Me.grpcallupdate.Name = "grpcallupdate"
        Me.grpcallupdate.Size = New System.Drawing.Size(423, 123)
        Me.grpcallupdate.TabIndex = 28
        Me.grpcallupdate.TabStop = False
        Me.grpcallupdate.Text = "Call Update"
        '
        'chkcreatenew
        '
        Me.chkcreatenew.AutoSize = True
        Me.chkcreatenew.BackColor = System.Drawing.Color.Transparent
        Me.chkcreatenew.Location = New System.Drawing.Point(186, 95)
        Me.chkcreatenew.Name = "chkcreatenew"
        Me.chkcreatenew.Size = New System.Drawing.Size(118, 17)
        Me.chkcreatenew.TabIndex = 345469
        Me.chkcreatenew.Text = "Create Next Call"
        Me.chkcreatenew.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(5, 92)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Next Call Date"
        '
        'dtpcallnextdate
        '
        Me.dtpcallnextdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpcallnextdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpcallnextdate.Location = New System.Drawing.Point(95, 92)
        Me.dtpcallnextdate.Name = "dtpcallnextdate"
        Me.dtpcallnextdate.Size = New System.Drawing.Size(85, 20)
        Me.dtpcallnextdate.TabIndex = 9
        '
        'txtremark
        '
        Me.txtremark.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremark.Location = New System.Drawing.Point(95, 19)
        Me.txtremark.Multiline = True
        Me.txtremark.Name = "txtremark"
        Me.txtremark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtremark.Size = New System.Drawing.Size(322, 66)
        Me.txtremark.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(5, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Remark"
        '
        'txtphonenumber
        '
        Me.txtphonenumber.Location = New System.Drawing.Point(105, 97)
        Me.txtphonenumber.Name = "txtphonenumber"
        Me.txtphonenumber.ReadOnly = True
        Me.txtphonenumber.Size = New System.Drawing.Size(217, 20)
        Me.txtphonenumber.TabIndex = 22
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(13, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 13)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Phone Number"
        '
        'txtopnumber
        '
        Me.txtopnumber.Location = New System.Drawing.Point(105, 45)
        Me.txtopnumber.Name = "txtopnumber"
        Me.txtopnumber.Size = New System.Drawing.Size(217, 20)
        Me.txtopnumber.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(13, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "OP Number"
        '
        'txtcompanyname
        '
        Me.txtcompanyname.Location = New System.Drawing.Point(105, 71)
        Me.txtcompanyname.Name = "txtcompanyname"
        Me.txtcompanyname.ReadOnly = True
        Me.txtcompanyname.Size = New System.Drawing.Size(217, 20)
        Me.txtcompanyname.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(13, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Patient Name"
        '
        'txtcustAddress
        '
        Me.txtcustAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustAddress.Location = New System.Drawing.Point(103, 123)
        Me.txtcustAddress.Multiline = True
        Me.txtcustAddress.Name = "txtcustAddress"
        Me.txtcustAddress.ReadOnly = True
        Me.txtcustAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtcustAddress.Size = New System.Drawing.Size(219, 66)
        Me.txtcustAddress.TabIndex = 31
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(13, 123)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Address"
        '
        'txtpurpose
        '
        Me.txtpurpose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpurpose.Location = New System.Drawing.Point(103, 217)
        Me.txtpurpose.Multiline = True
        Me.txtpurpose.Name = "txtpurpose"
        Me.txtpurpose.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtpurpose.Size = New System.Drawing.Size(329, 66)
        Me.txtpurpose.TabIndex = 33
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(15, 217)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 13)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Purpose"
        '
        'btnupdate
        '
        Me.btnupdate.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.Location = New System.Drawing.Point(227, 421)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(101, 35)
        Me.btnupdate.TabIndex = 345464
        Me.btnupdate.Text = "&Update"
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(331, 421)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(101, 35)
        Me.btnExit.TabIndex = 345463
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(15, 193)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 13)
        Me.Label8.TabIndex = 345465
        Me.Label8.Text = "Doctor"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(13, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(50, 13)
        Me.Label9.TabIndex = 345468
        Me.Label9.Text = "Call Date"
        '
        'dtpcalldate
        '
        Me.dtpcalldate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpcalldate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpcalldate.Location = New System.Drawing.Point(103, 19)
        Me.dtpcalldate.Name = "dtpcalldate"
        Me.dtpcalldate.Size = New System.Drawing.Size(85, 20)
        Me.dtpcalldate.TabIndex = 345467
        '
        'cmbsalesman
        '
        Me.cmbsalesman.BackColor = System.Drawing.SystemColors.Window
        Me.cmbsalesman.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbsalesman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsalesman.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsalesman.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbsalesman.Location = New System.Drawing.Point(103, 192)
        Me.cmbsalesman.Name = "cmbsalesman"
        Me.cmbsalesman.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbsalesman.Size = New System.Drawing.Size(271, 22)
        Me.cmbsalesman.TabIndex = 345469
        '
        'AddFollowupFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(445, 465)
        Me.Controls.Add(Me.cmbsalesman)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.dtpcalldate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnupdate)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.txtpurpose)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtcustAddress)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grpcallupdate)
        Me.Controls.Add(Me.txtphonenumber)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtopnumber)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtcompanyname)
        Me.Controls.Add(Me.Label2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddFollowupFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Followup"
        Me.grpcallupdate.ResumeLayout(False)
        Me.grpcallupdate.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpcallupdate As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpcallnextdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtremark As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtphonenumber As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtopnumber As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtcompanyname As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtcustAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtpurpose As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpcalldate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkcreatenew As System.Windows.Forms.CheckBox
    Public WithEvents cmbsalesman As System.Windows.Forms.ComboBox
End Class
