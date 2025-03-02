<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectCardPackageFrm
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
        Me.Label7 = New System.Windows.Forms.Label
        Me.grdpackage = New System.Windows.Forms.DataGridView
        Me.grdfreepackage = New System.Windows.Forms.DataGridView
        Me.rdocardpackage = New System.Windows.Forms.RadioButton
        Me.rdoallpackage = New System.Windows.Forms.RadioButton
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.btnExit = New System.Windows.Forms.Button
        CType(Me.grdpackage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdfreepackage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label7.Location = New System.Drawing.Point(15, 252)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 345469
        Me.Label7.Text = "Free Packages"
        '
        'grdpackage
        '
        Me.grdpackage.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdpackage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdpackage.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        Me.grdpackage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdpackage.Location = New System.Drawing.Point(12, 40)
        Me.grdpackage.Name = "grdpackage"
        Me.grdpackage.Size = New System.Drawing.Size(547, 209)
        Me.grdpackage.TabIndex = 345468
        '
        'grdfreepackage
        '
        Me.grdfreepackage.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdfreepackage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdfreepackage.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        Me.grdfreepackage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdfreepackage.Location = New System.Drawing.Point(12, 268)
        Me.grdfreepackage.Name = "grdfreepackage"
        Me.grdfreepackage.Size = New System.Drawing.Size(547, 109)
        Me.grdfreepackage.TabIndex = 345466
        '
        'rdocardpackage
        '
        Me.rdocardpackage.AutoSize = True
        Me.rdocardpackage.BackColor = System.Drawing.Color.Transparent
        Me.rdocardpackage.Checked = True
        Me.rdocardpackage.Location = New System.Drawing.Point(12, 17)
        Me.rdocardpackage.Name = "rdocardpackage"
        Me.rdocardpackage.Size = New System.Drawing.Size(93, 17)
        Me.rdocardpackage.TabIndex = 345470
        Me.rdocardpackage.TabStop = True
        Me.rdocardpackage.Text = "Card Package"
        Me.rdocardpackage.UseVisualStyleBackColor = False
        '
        'rdoallpackage
        '
        Me.rdoallpackage.AutoSize = True
        Me.rdoallpackage.BackColor = System.Drawing.Color.Transparent
        Me.rdoallpackage.Location = New System.Drawing.Point(111, 17)
        Me.rdoallpackage.Name = "rdoallpackage"
        Me.rdoallpackage.Size = New System.Drawing.Size(87, 17)
        Me.rdoallpackage.TabIndex = 345471
        Me.rdoallpackage.TabStop = True
        Me.rdoallpackage.Text = "All Packages"
        Me.rdoallpackage.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.Controls.Add(Me.btnExit)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel7.Location = New System.Drawing.Point(0, 378)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(571, 38)
        Me.Panel7.TabIndex = 345472
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(466, 2)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(93, 33)
        Me.btnExit.TabIndex = 75
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'SelectCardPackageFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(571, 416)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.rdoallpackage)
        Me.Controls.Add(Me.rdocardpackage)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.grdpackage)
        Me.Controls.Add(Me.grdfreepackage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SelectCardPackageFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Package"
        CType(Me.grdpackage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdfreepackage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents grdpackage As System.Windows.Forms.DataGridView
    Friend WithEvents grdfreepackage As System.Windows.Forms.DataGridView
    Friend WithEvents rdocardpackage As System.Windows.Forms.RadioButton
    Friend WithEvents rdoallpackage As System.Windows.Forms.RadioButton
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents btnExit As System.Windows.Forms.Button
End Class
