<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TransferToWebFrm
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
        Me.pb = New System.Windows.Forms.ProgressBar
        Me.lblrec = New System.Windows.Forms.Label
        Me.lblmodule = New System.Windows.Forms.Label
        Me.Worker = New System.ComponentModel.BackgroundWorker
        Me.lblwithimage = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(12, 49)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(412, 20)
        Me.pb.TabIndex = 6
        '
        'lblrec
        '
        Me.lblrec.AutoSize = True
        Me.lblrec.BackColor = System.Drawing.Color.Transparent
        Me.lblrec.Location = New System.Drawing.Point(17, 72)
        Me.lblrec.Name = "lblrec"
        Me.lblrec.Size = New System.Drawing.Size(42, 13)
        Me.lblrec.TabIndex = 17
        Me.lblrec.Text = "Record"
        '
        'lblmodule
        '
        Me.lblmodule.AutoSize = True
        Me.lblmodule.BackColor = System.Drawing.Color.Transparent
        Me.lblmodule.Location = New System.Drawing.Point(17, 33)
        Me.lblmodule.Name = "lblmodule"
        Me.lblmodule.Size = New System.Drawing.Size(42, 13)
        Me.lblmodule.TabIndex = 16
        Me.lblmodule.Text = "Module"
        '
        'Worker
        '
        '
        'lblwithimage
        '
        Me.lblwithimage.AutoSize = True
        Me.lblwithimage.BackColor = System.Drawing.Color.Transparent
        Me.lblwithimage.Location = New System.Drawing.Point(17, 114)
        Me.lblwithimage.Name = "lblwithimage"
        Me.lblwithimage.Size = New System.Drawing.Size(42, 13)
        Me.lblwithimage.TabIndex = 18
        Me.lblwithimage.Text = "Module"
        '
        'TransferToWebFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(436, 136)
        Me.Controls.Add(Me.lblwithimage)
        Me.Controls.Add(Me.lblrec)
        Me.Controls.Add(Me.lblmodule)
        Me.Controls.Add(Me.pb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TransferToWebFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transfer "
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents lblrec As System.Windows.Forms.Label
    Friend WithEvents lblmodule As System.Windows.Forms.Label
    Friend WithEvents Worker As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblwithimage As System.Windows.Forms.Label
End Class
