<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProcessFormFrm
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
        Me.lblwithimage = New System.Windows.Forms.Label
        Me.lblrec = New System.Windows.Forms.Label
        Me.lblmodule = New System.Windows.Forms.Label
        Me.pb = New System.Windows.Forms.ProgressBar
        Me.Worker = New System.ComponentModel.BackgroundWorker
        Me.SuspendLayout()
        '
        'lblwithimage
        '
        Me.lblwithimage.AutoSize = True
        Me.lblwithimage.BackColor = System.Drawing.Color.Transparent
        Me.lblwithimage.Location = New System.Drawing.Point(17, 109)
        Me.lblwithimage.Name = "lblwithimage"
        Me.lblwithimage.Size = New System.Drawing.Size(42, 13)
        Me.lblwithimage.TabIndex = 22
        Me.lblwithimage.Text = "Module"
        '
        'lblrec
        '
        Me.lblrec.AutoSize = True
        Me.lblrec.BackColor = System.Drawing.Color.Transparent
        Me.lblrec.Location = New System.Drawing.Point(17, 67)
        Me.lblrec.Name = "lblrec"
        Me.lblrec.Size = New System.Drawing.Size(42, 13)
        Me.lblrec.TabIndex = 21
        Me.lblrec.Text = "Record"
        '
        'lblmodule
        '
        Me.lblmodule.AutoSize = True
        Me.lblmodule.BackColor = System.Drawing.Color.Transparent
        Me.lblmodule.Location = New System.Drawing.Point(17, 28)
        Me.lblmodule.Name = "lblmodule"
        Me.lblmodule.Size = New System.Drawing.Size(42, 13)
        Me.lblmodule.TabIndex = 20
        Me.lblmodule.Text = "Module"
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(12, 44)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(412, 20)
        Me.pb.TabIndex = 19
        '
        'Worker
        '
        '
        'ProcessFormFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(440, 139)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblwithimage)
        Me.Controls.Add(Me.lblrec)
        Me.Controls.Add(Me.lblmodule)
        Me.Controls.Add(Me.pb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "ProcessFormFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Process"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblwithimage As System.Windows.Forms.Label
    Friend WithEvents lblrec As System.Windows.Forms.Label
    Friend WithEvents lblmodule As System.Windows.Forms.Label
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents Worker As System.ComponentModel.BackgroundWorker
End Class
