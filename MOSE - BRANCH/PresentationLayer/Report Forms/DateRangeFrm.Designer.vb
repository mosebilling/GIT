<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DateRangeFrm
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
        Me.pldate = New System.Windows.Forms.Panel
        Me.cldrEnddate = New System.Windows.Forms.DateTimePicker
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.btnapply = New System.Windows.Forms.Button
        Me.btnexit = New System.Windows.Forms.Button
        Me.pldate.SuspendLayout()
        Me.SuspendLayout()
        '
        'pldate
        '
        Me.pldate.Controls.Add(Me.cldrEnddate)
        Me.pldate.Controls.Add(Me.cldrStartDate)
        Me.pldate.Location = New System.Drawing.Point(6, 12)
        Me.pldate.Name = "pldate"
        Me.pldate.Size = New System.Drawing.Size(204, 27)
        Me.pldate.TabIndex = 345474
        '
        'cldrEnddate
        '
        Me.cldrEnddate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrEnddate.Location = New System.Drawing.Point(104, 3)
        Me.cldrEnddate.Name = "cldrEnddate"
        Me.cldrEnddate.Size = New System.Drawing.Size(96, 20)
        Me.cldrEnddate.TabIndex = 345395
        Me.cldrEnddate.TabStop = False
        '
        'cldrStartDate
        '
        Me.cldrStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrStartDate.Location = New System.Drawing.Point(3, 3)
        Me.cldrStartDate.Name = "cldrStartDate"
        Me.cldrStartDate.Size = New System.Drawing.Size(95, 20)
        Me.cldrStartDate.TabIndex = 345393
        Me.cldrStartDate.TabStop = False
        '
        'btnapply
        '
        Me.btnapply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnapply.BackColor = System.Drawing.Color.SteelBlue
        Me.btnapply.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnapply.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnapply.ForeColor = System.Drawing.Color.White
        Me.btnapply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnapply.Location = New System.Drawing.Point(9, 49)
        Me.btnapply.Name = "btnapply"
        Me.btnapply.Size = New System.Drawing.Size(95, 27)
        Me.btnapply.TabIndex = 345476
        Me.btnapply.Text = "Apply"
        Me.btnapply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnapply.UseVisualStyleBackColor = False
        '
        'btnexit
        '
        Me.btnexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnexit.Location = New System.Drawing.Point(110, 49)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(95, 27)
        Me.btnexit.TabIndex = 345477
        Me.btnexit.Text = "Cancel"
        Me.btnexit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'DateRangeFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(220, 88)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.btnapply)
        Me.Controls.Add(Me.pldate)
        Me.Name = "DateRangeFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Date Range"
        Me.pldate.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pldate As System.Windows.Forms.Panel
    Friend WithEvents cldrEnddate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnapply As System.Windows.Forms.Button
    Friend WithEvents btnexit As System.Windows.Forms.Button
End Class
