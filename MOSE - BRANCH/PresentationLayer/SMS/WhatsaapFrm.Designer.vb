<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WhatsaapFrm
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
        Me.btnsend = New System.Windows.Forms.Button
        Me.txtcontent = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtphone = New System.Windows.Forms.TextBox
        Me.btnexit = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtparty = New System.Windows.Forms.TextBox
        Me.cmbtemplate = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnset = New System.Windows.Forms.Button
        Me.btnremove = New System.Windows.Forms.Button
        Me.picwhatsapp = New System.Windows.Forms.PictureBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtjobcode = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtamount = New System.Windows.Forms.TextBox
        Me.Amount = New System.Windows.Forms.Label
        Me.txtpreview = New System.Windows.Forms.TextBox
        Me.txtreceived = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.chkoutstanding = New System.Windows.Forms.CheckBox
        Me.chkisjobcode = New System.Windows.Forms.CheckBox
        Me.chkisamount = New System.Windows.Forms.CheckBox
        CType(Me.picwhatsapp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnsend
        '
        Me.btnsend.BackColor = System.Drawing.Color.SteelBlue
        Me.btnsend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsend.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsend.ForeColor = System.Drawing.Color.White
        Me.btnsend.Location = New System.Drawing.Point(142, 387)
        Me.btnsend.Name = "btnsend"
        Me.btnsend.Size = New System.Drawing.Size(85, 35)
        Me.btnsend.TabIndex = 345523
        Me.btnsend.Text = "&Send"
        Me.btnsend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsend.UseVisualStyleBackColor = False
        '
        'txtcontent
        '
        Me.txtcontent.Location = New System.Drawing.Point(19, 198)
        Me.txtcontent.MaxLength = 3000
        Me.txtcontent.Multiline = True
        Me.txtcontent.Name = "txtcontent"
        Me.txtcontent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtcontent.Size = New System.Drawing.Size(317, 183)
        Me.txtcontent.TabIndex = 345520
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(21, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 345521
        Me.Label2.Text = "Phone Number"
        '
        'txtphone
        '
        Me.txtphone.Location = New System.Drawing.Point(108, 68)
        Me.txtphone.MaxLength = 50
        Me.txtphone.Name = "txtphone"
        Me.txtphone.Size = New System.Drawing.Size(233, 20)
        Me.txtphone.TabIndex = 345518
        '
        'btnexit
        '
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.Location = New System.Drawing.Point(233, 387)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(85, 35)
        Me.btnexit.TabIndex = 345527
        Me.btnexit.Text = "Exit"
        Me.btnexit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(21, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 345526
        Me.Label1.Text = "Party Name"
        '
        'txtparty
        '
        Me.txtparty.Location = New System.Drawing.Point(108, 94)
        Me.txtparty.MaxLength = 50
        Me.txtparty.Name = "txtparty"
        Me.txtparty.ReadOnly = True
        Me.txtparty.Size = New System.Drawing.Size(233, 20)
        Me.txtparty.TabIndex = 345519
        '
        'cmbtemplate
        '
        Me.cmbtemplate.FormattingEnabled = True
        Me.cmbtemplate.Location = New System.Drawing.Point(108, 41)
        Me.cmbtemplate.Name = "cmbtemplate"
        Me.cmbtemplate.Size = New System.Drawing.Size(131, 21)
        Me.cmbtemplate.TabIndex = 345528
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(21, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 345529
        Me.Label3.Text = "Template"
        '
        'btnset
        '
        Me.btnset.BackColor = System.Drawing.Color.SteelBlue
        Me.btnset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnset.ForeColor = System.Drawing.Color.White
        Me.btnset.Location = New System.Drawing.Point(243, 36)
        Me.btnset.Name = "btnset"
        Me.btnset.Size = New System.Drawing.Size(46, 29)
        Me.btnset.TabIndex = 345530
        Me.btnset.Text = "Set"
        Me.btnset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnset.UseVisualStyleBackColor = False
        '
        'btnremove
        '
        Me.btnremove.BackColor = System.Drawing.Color.SteelBlue
        Me.btnremove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnremove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnremove.ForeColor = System.Drawing.Color.White
        Me.btnremove.Location = New System.Drawing.Point(290, 36)
        Me.btnremove.Name = "btnremove"
        Me.btnremove.Size = New System.Drawing.Size(46, 29)
        Me.btnremove.TabIndex = 345531
        Me.btnremove.Text = "Rem"
        Me.btnremove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnremove.UseVisualStyleBackColor = False
        '
        'picwhatsapp
        '
        Me.picwhatsapp.BackgroundImage = Global.SMSMP.My.Resources.Resources.smallwhtsapp1
        Me.picwhatsapp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picwhatsapp.Location = New System.Drawing.Point(12, 3)
        Me.picwhatsapp.Name = "picwhatsapp"
        Me.picwhatsapp.Size = New System.Drawing.Size(30, 26)
        Me.picwhatsapp.TabIndex = 345532
        Me.picwhatsapp.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Green
        Me.Label4.Location = New System.Drawing.Point(48, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 16)
        Me.Label4.TabIndex = 345533
        Me.Label4.Text = "Send Message"
        '
        'txtjobcode
        '
        Me.txtjobcode.Location = New System.Drawing.Point(108, 120)
        Me.txtjobcode.MaxLength = 50
        Me.txtjobcode.Name = "txtjobcode"
        Me.txtjobcode.ReadOnly = True
        Me.txtjobcode.Size = New System.Drawing.Size(233, 20)
        Me.txtjobcode.TabIndex = 345534
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(21, 120)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 345535
        Me.Label5.Text = "Job Code"
        '
        'txtamount
        '
        Me.txtamount.Location = New System.Drawing.Point(108, 146)
        Me.txtamount.MaxLength = 50
        Me.txtamount.Name = "txtamount"
        Me.txtamount.ReadOnly = True
        Me.txtamount.Size = New System.Drawing.Size(233, 20)
        Me.txtamount.TabIndex = 345536
        '
        'Amount
        '
        Me.Amount.AutoSize = True
        Me.Amount.BackColor = System.Drawing.Color.Transparent
        Me.Amount.Location = New System.Drawing.Point(21, 148)
        Me.Amount.Name = "Amount"
        Me.Amount.Size = New System.Drawing.Size(43, 13)
        Me.Amount.TabIndex = 345537
        Me.Amount.Text = "Amount"
        '
        'txtpreview
        '
        Me.txtpreview.BackColor = System.Drawing.Color.White
        Me.txtpreview.Location = New System.Drawing.Point(347, 36)
        Me.txtpreview.MaxLength = 3000
        Me.txtpreview.Multiline = True
        Me.txtpreview.Name = "txtpreview"
        Me.txtpreview.ReadOnly = True
        Me.txtpreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtpreview.Size = New System.Drawing.Size(356, 319)
        Me.txtpreview.TabIndex = 345538
        '
        'txtreceived
        '
        Me.txtreceived.Location = New System.Drawing.Point(108, 172)
        Me.txtreceived.MaxLength = 50
        Me.txtreceived.Name = "txtreceived"
        Me.txtreceived.ReadOnly = True
        Me.txtreceived.Size = New System.Drawing.Size(233, 20)
        Me.txtreceived.TabIndex = 345539
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(21, 174)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 13)
        Me.Label6.TabIndex = 345540
        Me.Label6.Text = "Received"
        '
        'chkoutstanding
        '
        Me.chkoutstanding.AutoSize = True
        Me.chkoutstanding.BackColor = System.Drawing.Color.Transparent
        Me.chkoutstanding.Location = New System.Drawing.Point(19, 391)
        Me.chkoutstanding.Name = "chkoutstanding"
        Me.chkoutstanding.Size = New System.Drawing.Size(107, 17)
        Me.chkoutstanding.TabIndex = 345541
        Me.chkoutstanding.Text = "Outstanding Only"
        Me.chkoutstanding.UseVisualStyleBackColor = False
        '
        'chkisjobcode
        '
        Me.chkisjobcode.AutoSize = True
        Me.chkisjobcode.Location = New System.Drawing.Point(19, 411)
        Me.chkisjobcode.Name = "chkisjobcode"
        Me.chkisjobcode.Size = New System.Drawing.Size(92, 17)
        Me.chkisjobcode.TabIndex = 345542
        Me.chkisjobcode.Text = "With Jobcode"
        Me.chkisjobcode.UseVisualStyleBackColor = True
        '
        'chkisamount
        '
        Me.chkisamount.AutoSize = True
        Me.chkisamount.Location = New System.Drawing.Point(19, 432)
        Me.chkisamount.Name = "chkisamount"
        Me.chkisamount.Size = New System.Drawing.Size(87, 17)
        Me.chkisamount.TabIndex = 345543
        Me.chkisamount.Text = "With Amount"
        Me.chkisamount.UseVisualStyleBackColor = True
        '
        'WhatsaapFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(715, 450)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkisamount)
        Me.Controls.Add(Me.chkisjobcode)
        Me.Controls.Add(Me.chkoutstanding)
        Me.Controls.Add(Me.txtreceived)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtpreview)
        Me.Controls.Add(Me.txtamount)
        Me.Controls.Add(Me.Amount)
        Me.Controls.Add(Me.txtjobcode)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.picwhatsapp)
        Me.Controls.Add(Me.btnremove)
        Me.Controls.Add(Me.btnset)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbtemplate)
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.txtparty)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnsend)
        Me.Controls.Add(Me.txtcontent)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtphone)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "WhatsaapFrm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WhatApp"
        CType(Me.picwhatsapp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnsend As System.Windows.Forms.Button
    Friend WithEvents txtcontent As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtphone As System.Windows.Forms.TextBox
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtparty As System.Windows.Forms.TextBox
    Friend WithEvents cmbtemplate As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnset As System.Windows.Forms.Button
    Friend WithEvents btnremove As System.Windows.Forms.Button
    Friend WithEvents picwhatsapp As System.Windows.Forms.PictureBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtjobcode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtamount As System.Windows.Forms.TextBox
    Friend WithEvents Amount As System.Windows.Forms.Label
    Friend WithEvents txtpreview As System.Windows.Forms.TextBox
    Friend WithEvents txtreceived As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkoutstanding As System.Windows.Forms.CheckBox
    Friend WithEvents chkisjobcode As System.Windows.Forms.CheckBox
    Friend WithEvents chkisamount As System.Windows.Forms.CheckBox
End Class
