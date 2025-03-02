<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataFromDbFrm
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
        Me.rdoItems = New System.Windows.Forms.RadioButton
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnok = New System.Windows.Forms.Button
        Me.Worker = New System.ComponentModel.BackgroundWorker
        Me.lblrec = New System.Windows.Forms.Label
        Me.lblmodule = New System.Windows.Forms.Label
        Me.pb = New System.Windows.Forms.ProgressBar
        Me.chkappend = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'rdoItems
        '
        Me.rdoItems.AutoSize = True
        Me.rdoItems.Location = New System.Drawing.Point(12, 12)
        Me.rdoItems.Name = "rdoItems"
        Me.rdoItems.Size = New System.Drawing.Size(109, 17)
        Me.rdoItems.TabIndex = 0
        Me.rdoItems.TabStop = True
        Me.rdoItems.Text = "Transfer Products"
        Me.rdoItems.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.SteelBlue
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(333, 139)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(65, 28)
        Me.btnCancel.TabIndex = 27
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnok
        '
        Me.btnok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnok.BackColor = System.Drawing.Color.SteelBlue
        Me.btnok.FlatAppearance.BorderSize = 0
        Me.btnok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnok.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnok.ForeColor = System.Drawing.Color.White
        Me.btnok.Location = New System.Drawing.Point(264, 139)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(65, 28)
        Me.btnok.TabIndex = 26
        Me.btnok.Text = "Transfer"
        Me.btnok.UseVisualStyleBackColor = False
        '
        'lblrec
        '
        Me.lblrec.AutoSize = True
        Me.lblrec.BackColor = System.Drawing.Color.Transparent
        Me.lblrec.Location = New System.Drawing.Point(12, 106)
        Me.lblrec.Name = "lblrec"
        Me.lblrec.Size = New System.Drawing.Size(42, 13)
        Me.lblrec.TabIndex = 39
        Me.lblrec.Text = "Record"
        '
        'lblmodule
        '
        Me.lblmodule.AutoSize = True
        Me.lblmodule.BackColor = System.Drawing.Color.Transparent
        Me.lblmodule.Location = New System.Drawing.Point(12, 67)
        Me.lblmodule.Name = "lblmodule"
        Me.lblmodule.Size = New System.Drawing.Size(42, 13)
        Me.lblmodule.TabIndex = 38
        Me.lblmodule.Text = "Module"
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(12, 83)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(396, 20)
        Me.pb.TabIndex = 37
        '
        'chkappend
        '
        Me.chkappend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkappend.AutoSize = True
        Me.chkappend.BackColor = System.Drawing.Color.Transparent
        Me.chkappend.Checked = True
        Me.chkappend.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkappend.Location = New System.Drawing.Point(345, 60)
        Me.chkappend.Name = "chkappend"
        Me.chkappend.Size = New System.Drawing.Size(63, 17)
        Me.chkappend.TabIndex = 40
        Me.chkappend.Text = "Append"
        Me.chkappend.UseVisualStyleBackColor = False
        Me.chkappend.Visible = False
        '
        'DataFromDbFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 179)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkappend)
        Me.Controls.Add(Me.lblrec)
        Me.Controls.Add(Me.lblmodule)
        Me.Controls.Add(Me.pb)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.rdoItems)
        Me.Name = "DataFromDbFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Data From DB"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdoItems As System.Windows.Forms.RadioButton
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnok As System.Windows.Forms.Button
    Friend WithEvents Worker As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblrec As System.Windows.Forms.Label
    Friend WithEvents lblmodule As System.Windows.Forms.Label
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents chkappend As System.Windows.Forms.CheckBox
End Class
