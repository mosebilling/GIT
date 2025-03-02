<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LastRenewedDateFrm
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
        Me.clrrenewed = New System.Windows.Forms.DateTimePicker
        Me.Label21 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.lblamount = New System.Windows.Forms.Label
        Me.lblamt = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'clrrenewed
        '
        Me.clrrenewed.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.clrrenewed.Location = New System.Drawing.Point(109, 18)
        Me.clrrenewed.Name = "clrrenewed"
        Me.clrrenewed.Size = New System.Drawing.Size(104, 20)
        Me.clrrenewed.TabIndex = 345475
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Location = New System.Drawing.Point(28, 18)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(76, 13)
        Me.Label21.TabIndex = 345476
        Me.Label21.Text = "Last Renewed"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(74, 88)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(101, 35)
        Me.btnExit.TabIndex = 345477
        Me.btnExit.Text = "OK"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'lblamount
        '
        Me.lblamount.AutoSize = True
        Me.lblamount.BackColor = System.Drawing.Color.Transparent
        Me.lblamount.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblamount.Location = New System.Drawing.Point(62, 53)
        Me.lblamount.Name = "lblamount"
        Me.lblamount.Size = New System.Drawing.Size(71, 20)
        Me.lblamount.TabIndex = 345478
        Me.lblamount.Text = "Amount"
        '
        'lblamt
        '
        Me.lblamt.AutoSize = True
        Me.lblamt.BackColor = System.Drawing.Color.Transparent
        Me.lblamt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblamt.Location = New System.Drawing.Point(140, 53)
        Me.lblamt.Name = "lblamt"
        Me.lblamt.Size = New System.Drawing.Size(44, 20)
        Me.lblamt.TabIndex = 345479
        Me.lblamt.Text = "0.00"
        '
        'LastRenewedDateFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(252, 135)
        Me.Controls.Add(Me.lblamt)
        Me.Controls.Add(Me.lblamount)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.clrrenewed)
        Me.Controls.Add(Me.Label21)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LastRenewedDateFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Renewed Date"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents clrrenewed As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblamount As System.Windows.Forms.Label
    Friend WithEvents lblamt As System.Windows.Forms.Label
End Class
