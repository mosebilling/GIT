<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Backupfrm
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
        Me.cmdfileto = New System.Windows.Forms.Button
        Me.txtFileto = New System.Windows.Forms.TextBox
        Me.cmdbackup = New System.Windows.Forms.Button
        Me.Label20 = New System.Windows.Forms.Label
        Me.btnexit = New System.Windows.Forms.Button
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblcap = New System.Windows.Forms.Label
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdfileto
        '
        Me.cmdfileto.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdfileto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdfileto.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdfileto.ForeColor = System.Drawing.Color.White
        Me.cmdfileto.Location = New System.Drawing.Point(392, 38)
        Me.cmdfileto.Name = "cmdfileto"
        Me.cmdfileto.Size = New System.Drawing.Size(41, 25)
        Me.cmdfileto.TabIndex = 28
        Me.cmdfileto.Text = ">>"
        Me.cmdfileto.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.cmdfileto.UseVisualStyleBackColor = False
        '
        'txtFileto
        '
        Me.txtFileto.BackColor = System.Drawing.Color.White
        Me.txtFileto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileto.Location = New System.Drawing.Point(50, 38)
        Me.txtFileto.MaxLength = 500
        Me.txtFileto.Name = "txtFileto"
        Me.txtFileto.ReadOnly = True
        Me.txtFileto.Size = New System.Drawing.Size(336, 20)
        Me.txtFileto.TabIndex = 27
        '
        'cmdbackup
        '
        Me.cmdbackup.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdbackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdbackup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdbackup.ForeColor = System.Drawing.Color.White
        Me.cmdbackup.Location = New System.Drawing.Point(272, 64)
        Me.cmdbackup.Name = "cmdbackup"
        Me.cmdbackup.Size = New System.Drawing.Size(79, 35)
        Me.cmdbackup.TabIndex = 25
        Me.cmdbackup.Text = "Backup"
        Me.cmdbackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdbackup.UseVisualStyleBackColor = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(9, 41)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(35, 13)
        Me.Label20.TabIndex = 26
        Me.Label20.Text = "File to"
        '
        'btnexit
        '
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.Location = New System.Drawing.Point(354, 64)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(79, 35)
        Me.btnexit.TabIndex = 29
        Me.btnexit.Text = "E&xit"
        Me.btnexit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.White
        Me.Panel4.Controls.Add(Me.PictureBox1)
        Me.Panel4.Controls.Add(Me.lblcap)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(441, 33)
        Me.Panel4.TabIndex = 345446
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.open_file_icon
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Image = Global.SMSMP.My.Resources.Resources.paper_money_icon1
        Me.PictureBox1.Location = New System.Drawing.Point(8, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(29, 26)
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'lblcap
        '
        Me.lblcap.AutoSize = True
        Me.lblcap.BackColor = System.Drawing.Color.Transparent
        Me.lblcap.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcap.ForeColor = System.Drawing.Color.Black
        Me.lblcap.Location = New System.Drawing.Point(41, 6)
        Me.lblcap.Name = "lblcap"
        Me.lblcap.Size = New System.Drawing.Size(93, 20)
        Me.lblcap.TabIndex = 6
        Me.lblcap.Text = "Backup To.."
        '
        'Backupfrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(441, 102)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.cmdfileto)
        Me.Controls.Add(Me.txtFileto)
        Me.Controls.Add(Me.cmdbackup)
        Me.Controls.Add(Me.Label20)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Backupfrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdfileto As System.Windows.Forms.Button
    Friend WithEvents txtFileto As System.Windows.Forms.TextBox
    Friend WithEvents cmdbackup As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblcap As System.Windows.Forms.Label
End Class
