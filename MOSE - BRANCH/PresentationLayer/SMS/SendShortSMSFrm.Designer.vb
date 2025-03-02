<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SendShortSMSFrm
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
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtphone = New System.Windows.Forms.TextBox
        Me.txtcontent = New System.Windows.Forms.TextBox
        Me.lblcharector = New System.Windows.Forms.Label
        Me.lblsmsremaining = New System.Windows.Forms.Label
        Me.btnsend = New System.Windows.Forms.Button
        Me.lblstaus = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtparty = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(9, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 345511
        Me.Label2.Text = "Phone Number"
        '
        'txtphone
        '
        Me.txtphone.Location = New System.Drawing.Point(96, 9)
        Me.txtphone.MaxLength = 10
        Me.txtphone.Name = "txtphone"
        Me.txtphone.Size = New System.Drawing.Size(181, 20)
        Me.txtphone.TabIndex = 0
        '
        'txtcontent
        '
        Me.txtcontent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtcontent.Location = New System.Drawing.Point(1, 85)
        Me.txtcontent.MaxLength = 120
        Me.txtcontent.Multiline = True
        Me.txtcontent.Name = "txtcontent"
        Me.txtcontent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtcontent.Size = New System.Drawing.Size(299, 180)
        Me.txtcontent.TabIndex = 2
        '
        'lblcharector
        '
        Me.lblcharector.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblcharector.AutoSize = True
        Me.lblcharector.BackColor = System.Drawing.Color.Transparent
        Me.lblcharector.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcharector.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblcharector.Location = New System.Drawing.Point(0, 67)
        Me.lblcharector.Name = "lblcharector"
        Me.lblcharector.Size = New System.Drawing.Size(41, 15)
        Me.lblcharector.TabIndex = 345513
        Me.lblcharector.Text = "Status"
        '
        'lblsmsremaining
        '
        Me.lblsmsremaining.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblsmsremaining.AutoSize = True
        Me.lblsmsremaining.BackColor = System.Drawing.Color.Transparent
        Me.lblsmsremaining.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsmsremaining.ForeColor = System.Drawing.Color.Green
        Me.lblsmsremaining.Location = New System.Drawing.Point(3, 303)
        Me.lblsmsremaining.Name = "lblsmsremaining"
        Me.lblsmsremaining.Size = New System.Drawing.Size(94, 16)
        Me.lblsmsremaining.TabIndex = 345515
        Me.lblsmsremaining.Text = "Remaining : "
        '
        'btnsend
        '
        Me.btnsend.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnsend.BackColor = System.Drawing.Color.SteelBlue
        Me.btnsend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsend.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsend.ForeColor = System.Drawing.Color.White
        Me.btnsend.Location = New System.Drawing.Point(215, 294)
        Me.btnsend.Name = "btnsend"
        Me.btnsend.Size = New System.Drawing.Size(85, 35)
        Me.btnsend.TabIndex = 345514
        Me.btnsend.Text = "&Send"
        Me.btnsend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsend.UseVisualStyleBackColor = False
        '
        'lblstaus
        '
        Me.lblstaus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblstaus.AutoSize = True
        Me.lblstaus.BackColor = System.Drawing.Color.Transparent
        Me.lblstaus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstaus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblstaus.Location = New System.Drawing.Point(0, 268)
        Me.lblstaus.Name = "lblstaus"
        Me.lblstaus.Size = New System.Drawing.Size(51, 16)
        Me.lblstaus.TabIndex = 345516
        Me.lblstaus.Text = "Status"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(9, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 345517
        Me.Label1.Text = "Party Name"
        '
        'txtparty
        '
        Me.txtparty.Location = New System.Drawing.Point(96, 35)
        Me.txtparty.MaxLength = 50
        Me.txtparty.Name = "txtparty"
        Me.txtparty.Size = New System.Drawing.Size(181, 20)
        Me.txtparty.TabIndex = 1
        '
        'SendShortSMSFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(300, 330)
        Me.Controls.Add(Me.txtparty)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblsmsremaining)
        Me.Controls.Add(Me.btnsend)
        Me.Controls.Add(Me.lblstaus)
        Me.Controls.Add(Me.txtcontent)
        Me.Controls.Add(Me.lblcharector)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtphone)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SendShortSMSFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Send SMS"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtphone As System.Windows.Forms.TextBox
    Friend WithEvents txtcontent As System.Windows.Forms.TextBox
    Friend WithEvents lblcharector As System.Windows.Forms.Label
    Friend WithEvents lblsmsremaining As System.Windows.Forms.Label
    Friend WithEvents btnsend As System.Windows.Forms.Button
    Friend WithEvents lblstaus As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtparty As System.Windows.Forms.TextBox
End Class
