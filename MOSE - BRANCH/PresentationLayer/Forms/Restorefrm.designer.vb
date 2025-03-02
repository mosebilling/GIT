<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Restorefrm
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
        Me.cmdRestore = New System.Windows.Forms.Button
        Me.cmdFileFrom = New System.Windows.Forms.Button
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtFirleFrom = New System.Windows.Forms.TextBox
        Me.btnexit = New System.Windows.Forms.Button
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblcap = New System.Windows.Forms.Label
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdRestore
        '
        Me.cmdRestore.Image = Global.SMSMP.My.Resources.Resources.button_edit
        Me.cmdRestore.Location = New System.Drawing.Point(268, 67)
        Me.cmdRestore.Name = "cmdRestore"
        Me.cmdRestore.Size = New System.Drawing.Size(79, 28)
        Me.cmdRestore.TabIndex = 22
        Me.cmdRestore.Text = "Restore"
        Me.cmdRestore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdRestore.UseVisualStyleBackColor = True
        '
        'cmdFileFrom
        '
        Me.cmdFileFrom.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFileFrom.Location = New System.Drawing.Point(398, 37)
        Me.cmdFileFrom.Name = "cmdFileFrom"
        Me.cmdFileFrom.Size = New System.Drawing.Size(31, 22)
        Me.cmdFileFrom.TabIndex = 25
        Me.cmdFileFrom.Text = ">>"
        Me.cmdFileFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdFileFrom.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(8, 42)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(49, 13)
        Me.Label19.TabIndex = 23
        Me.Label19.Text = "File From"
        '
        'txtFirleFrom
        '
        Me.txtFirleFrom.BackColor = System.Drawing.Color.White
        Me.txtFirleFrom.Location = New System.Drawing.Point(62, 39)
        Me.txtFirleFrom.MaxLength = 500
        Me.txtFirleFrom.Name = "txtFirleFrom"
        Me.txtFirleFrom.ReadOnly = True
        Me.txtFirleFrom.Size = New System.Drawing.Size(330, 20)
        Me.txtFirleFrom.TabIndex = 24
        '
        'btnexit
        '
        Me.btnexit.Image = Global.SMSMP.My.Resources.Resources.button_cancel
        Me.btnexit.Location = New System.Drawing.Point(350, 67)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(79, 28)
        Me.btnexit.TabIndex = 30
        Me.btnexit.Text = "E&xit"
        Me.btnexit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexit.UseVisualStyleBackColor = True
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
        Me.Panel4.TabIndex = 345447
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.backup_restore
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
        Me.lblcap.Size = New System.Drawing.Size(115, 20)
        Me.lblcap.TabIndex = 6
        Me.lblcap.Text = "Restore From.."
        '
        'Restorefrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(441, 103)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.cmdRestore)
        Me.Controls.Add(Me.cmdFileFrom)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtFirleFrom)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Restorefrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdRestore As System.Windows.Forms.Button
    Friend WithEvents cmdFileFrom As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtFirleFrom As System.Windows.Forms.TextBox
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblcap As System.Windows.Forms.Label
End Class
