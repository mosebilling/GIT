<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewImageFrm
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
        Me.picimagefromlist = New System.Windows.Forms.PictureBox
        CType(Me.picimagefromlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picimagefromlist
        '
        Me.picimagefromlist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picimagefromlist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picimagefromlist.ImageLocation = ""
        Me.picimagefromlist.Location = New System.Drawing.Point(0, 0)
        Me.picimagefromlist.Name = "picimagefromlist"
        Me.picimagefromlist.Size = New System.Drawing.Size(284, 261)
        Me.picimagefromlist.TabIndex = 1
        Me.picimagefromlist.TabStop = False
        '
        'ViewImageFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.picimagefromlist)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ViewImageFrm"
        Me.ShowInTaskbar = False
        Me.Text = "Image"
        Me.TopMost = True
        CType(Me.picimagefromlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picimagefromlist As System.Windows.Forms.PictureBox
End Class
