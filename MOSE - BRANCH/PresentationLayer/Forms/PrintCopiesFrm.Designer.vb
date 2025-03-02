<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrintCopiesFrm
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
        Me.chkduplicate = New System.Windows.Forms.CheckBox
        Me.chktriplicate = New System.Windows.Forms.CheckBox
        Me.btnAddgst = New System.Windows.Forms.Button
        Me.btncancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'chkduplicate
        '
        Me.chkduplicate.AutoSize = True
        Me.chkduplicate.Checked = True
        Me.chkduplicate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkduplicate.Location = New System.Drawing.Point(12, 12)
        Me.chkduplicate.Name = "chkduplicate"
        Me.chkduplicate.Size = New System.Drawing.Size(71, 17)
        Me.chkduplicate.TabIndex = 0
        Me.chkduplicate.Text = "Duplicate"
        Me.chkduplicate.UseVisualStyleBackColor = True
        '
        'chktriplicate
        '
        Me.chktriplicate.AutoSize = True
        Me.chktriplicate.Location = New System.Drawing.Point(12, 35)
        Me.chktriplicate.Name = "chktriplicate"
        Me.chktriplicate.Size = New System.Drawing.Size(69, 17)
        Me.chktriplicate.TabIndex = 1
        Me.chktriplicate.Text = "Triplicate"
        Me.chktriplicate.UseVisualStyleBackColor = True
        '
        'btnAddgst
        '
        Me.btnAddgst.BackColor = System.Drawing.Color.SteelBlue
        Me.btnAddgst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddgst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddgst.ForeColor = System.Drawing.Color.White
        Me.btnAddgst.Location = New System.Drawing.Point(12, 67)
        Me.btnAddgst.Name = "btnAddgst"
        Me.btnAddgst.Size = New System.Drawing.Size(93, 29)
        Me.btnAddgst.TabIndex = 4
        Me.btnAddgst.Text = "OK"
        Me.btnAddgst.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAddgst.UseVisualStyleBackColor = False
        '
        'btncancel
        '
        Me.btncancel.BackColor = System.Drawing.Color.SteelBlue
        Me.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancel.ForeColor = System.Drawing.Color.White
        Me.btncancel.Location = New System.Drawing.Point(122, 67)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(93, 29)
        Me.btncancel.TabIndex = 5
        Me.btncancel.Text = "Cancel"
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = False
        '
        'PrintCopiesFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(227, 100)
        Me.ControlBox = False
        Me.Controls.Add(Me.btncancel)
        Me.Controls.Add(Me.btnAddgst)
        Me.Controls.Add(Me.chktriplicate)
        Me.Controls.Add(Me.chkduplicate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "PrintCopiesFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Print Copies"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkduplicate As System.Windows.Forms.CheckBox
    Friend WithEvents chktriplicate As System.Windows.Forms.CheckBox
    Friend WithEvents btnAddgst As System.Windows.Forms.Button
    Friend WithEvents btncancel As System.Windows.Forms.Button
End Class
