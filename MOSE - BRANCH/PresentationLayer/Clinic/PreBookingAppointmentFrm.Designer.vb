<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PreBookingAppointmentFrm
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
        Me.Label11 = New System.Windows.Forms.Label
        Me.dtptime = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnclose = New System.Windows.Forms.Button
        Me.btnbook = New System.Windows.Forms.Button
        Me.cmbsalesman = New System.Windows.Forms.ComboBox
        Me.lblCap8 = New System.Windows.Forms.Label
        Me.cmbday = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtno = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(217, 8)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(35, 15)
        Me.Label11.TabIndex = 345490
        Me.Label11.Text = "Time"
        '
        'dtptime
        '
        Me.dtptime.CustomFormat = ""
        Me.dtptime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtptime.Location = New System.Drawing.Point(258, 7)
        Me.dtptime.Name = "dtptime"
        Me.dtptime.Size = New System.Drawing.Size(108, 20)
        Me.dtptime.TabIndex = 345489
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(28, 15)
        Me.Label5.TabIndex = 345488
        Me.Label5.Text = "Day"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ForeColor = System.Drawing.Color.White
        Me.btnclose.Location = New System.Drawing.Point(284, 117)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(82, 27)
        Me.btnclose.TabIndex = 345491
        Me.btnclose.Text = "E&xit"
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'btnbook
        '
        Me.btnbook.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnbook.BackColor = System.Drawing.Color.SteelBlue
        Me.btnbook.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnbook.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnbook.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbook.ForeColor = System.Drawing.Color.White
        Me.btnbook.Location = New System.Drawing.Point(196, 117)
        Me.btnbook.Name = "btnbook"
        Me.btnbook.Size = New System.Drawing.Size(82, 27)
        Me.btnbook.TabIndex = 345492
        Me.btnbook.Text = "&Apply"
        Me.btnbook.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnbook.UseVisualStyleBackColor = False
        '
        'cmbsalesman
        '
        Me.cmbsalesman.BackColor = System.Drawing.SystemColors.Window
        Me.cmbsalesman.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbsalesman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsalesman.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsalesman.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbsalesman.Location = New System.Drawing.Point(71, 61)
        Me.cmbsalesman.Name = "cmbsalesman"
        Me.cmbsalesman.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbsalesman.Size = New System.Drawing.Size(231, 22)
        Me.cmbsalesman.TabIndex = 345494
        '
        'lblCap8
        '
        Me.lblCap8.AutoSize = True
        Me.lblCap8.BackColor = System.Drawing.Color.Transparent
        Me.lblCap8.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap8.Location = New System.Drawing.Point(5, 64)
        Me.lblCap8.Name = "lblCap8"
        Me.lblCap8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap8.Size = New System.Drawing.Size(39, 14)
        Me.lblCap8.TabIndex = 345493
        Me.lblCap8.Text = "Doctor"
        '
        'cmbday
        '
        Me.cmbday.BackColor = System.Drawing.SystemColors.Window
        Me.cmbday.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbday.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbday.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbday.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbday.Items.AddRange(New Object() {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})
        Me.cmbday.Location = New System.Drawing.Point(71, 10)
        Me.cmbday.Name = "cmbday"
        Me.cmbday.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbday.Size = New System.Drawing.Size(140, 22)
        Me.cmbday.TabIndex = 345495
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(5, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(61, 14)
        Me.Label1.TabIndex = 345496
        Me.Label1.Text = "No of Days"
        '
        'txtno
        '
        Me.txtno.AcceptsReturn = True
        Me.txtno.BackColor = System.Drawing.SystemColors.Window
        Me.txtno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtno.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtno.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtno.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtno.Location = New System.Drawing.Point(72, 36)
        Me.txtno.MaxLength = 10
        Me.txtno.Name = "txtno"
        Me.txtno.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtno.Size = New System.Drawing.Size(89, 20)
        Me.txtno.TabIndex = 345497
        '
        'PreBookingAppointmentFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(378, 156)
        Me.Controls.Add(Me.txtno)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbday)
        Me.Controls.Add(Me.cmbsalesman)
        Me.Controls.Add(Me.lblCap8)
        Me.Controls.Add(Me.btnbook)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.dtptime)
        Me.Controls.Add(Me.Label5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PreBookingAppointmentFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pre Boking"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtptime As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents btnbook As System.Windows.Forms.Button
    Public WithEvents cmbsalesman As System.Windows.Forms.ComboBox
    Public WithEvents lblCap8 As System.Windows.Forms.Label
    Public WithEvents cmbday As System.Windows.Forms.ComboBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtno As System.Windows.Forms.TextBox
End Class
