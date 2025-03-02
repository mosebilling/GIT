<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CameraFrm
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
        Me.picimage = New System.Windows.Forms.PictureBox
        Me.btncapture = New System.Windows.Forms.Button
        Me.btnexit = New System.Windows.Forms.Button
        CType(Me.picimage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picimage
        '
        Me.picimage.Location = New System.Drawing.Point(0, 0)
        Me.picimage.Name = "picimage"
        Me.picimage.Size = New System.Drawing.Size(402, 319)
        Me.picimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picimage.TabIndex = 0
        Me.picimage.TabStop = False
        '
        'btncapture
        '
        Me.btncapture.BackColor = System.Drawing.Color.SteelBlue
        Me.btncapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncapture.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncapture.ForeColor = System.Drawing.Color.White
        Me.btncapture.Location = New System.Drawing.Point(12, 323)
        Me.btncapture.Name = "btncapture"
        Me.btncapture.Size = New System.Drawing.Size(61, 45)
        Me.btncapture.TabIndex = 345451
        Me.btncapture.Text = "Capture"
        Me.btncapture.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncapture.UseVisualStyleBackColor = False
        '
        'btnexit
        '
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.Location = New System.Drawing.Point(329, 323)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(61, 45)
        Me.btnexit.TabIndex = 345452
        Me.btnexit.Text = "Exit"
        Me.btnexit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'CameraFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(402, 371)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.btncapture)
        Me.Controls.Add(Me.picimage)
        Me.Name = "CameraFrm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CameraFrm"
        CType(Me.picimage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picimage As System.Windows.Forms.PictureBox
    Friend WithEvents btncapture As System.Windows.Forms.Button
    Friend WithEvents btnexit As System.Windows.Forms.Button
End Class
