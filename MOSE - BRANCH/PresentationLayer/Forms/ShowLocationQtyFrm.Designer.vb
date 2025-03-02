<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ShowLocationQtyFrm
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
        Me.grdLocation = New System.Windows.Forms.DataGridView
        Me.btnExit = New System.Windows.Forms.Button
        Me.lbltotalValue = New System.Windows.Forms.Label
        Me.btnupdateopening = New System.Windows.Forms.Button
        CType(Me.grdLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdLocation
        '
        Me.grdLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLocation.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdLocation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdLocation.Location = New System.Drawing.Point(0, 50)
        Me.grdLocation.Name = "grdLocation"
        Me.grdLocation.Size = New System.Drawing.Size(455, 161)
        Me.grdLocation.TabIndex = 345428
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.AutoEllipsis = True
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(359, 214)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(96, 35)
        Me.btnExit.TabIndex = 345429
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'lbltotalValue
        '
        Me.lbltotalValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbltotalValue.AutoSize = True
        Me.lbltotalValue.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalValue.ForeColor = System.Drawing.Color.Black
        Me.lbltotalValue.Location = New System.Drawing.Point(0, 4)
        Me.lbltotalValue.Name = "lbltotalValue"
        Me.lbltotalValue.Size = New System.Drawing.Size(50, 16)
        Me.lbltotalValue.TabIndex = 345449
        Me.lbltotalValue.Text = "Name"
        '
        'btnupdateopening
        '
        Me.btnupdateopening.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnupdateopening.AutoEllipsis = True
        Me.btnupdateopening.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupdateopening.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnupdateopening.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdateopening.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdateopening.ForeColor = System.Drawing.Color.White
        Me.btnupdateopening.Location = New System.Drawing.Point(232, 214)
        Me.btnupdateopening.Name = "btnupdateopening"
        Me.btnupdateopening.Size = New System.Drawing.Size(124, 35)
        Me.btnupdateopening.TabIndex = 345450
        Me.btnupdateopening.Text = "Update Opening"
        Me.btnupdateopening.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdateopening.UseVisualStyleBackColor = False
        '
        'ShowLocationQtyFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(458, 261)
        Me.Controls.Add(Me.btnupdateopening)
        Me.Controls.Add(Me.lbltotalValue)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.grdLocation)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ShowLocationQtyFrm"
        Me.ShowInTaskbar = False
        Me.Text = "Location Qty"
        Me.TopMost = True
        CType(Me.grdLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdLocation As System.Windows.Forms.DataGridView
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lbltotalValue As System.Windows.Forms.Label
    Friend WithEvents btnupdateopening As System.Windows.Forms.Button
End Class
