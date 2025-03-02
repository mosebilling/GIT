<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReprtviewNEWfrm
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
        Me.crView = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.btnprint = New System.Windows.Forms.Button
        Me.btnBack = New System.Windows.Forms.Button
        Me.btnSetup = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        Me.btnwhatsapp = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'crView
        '
        Me.crView.ActiveViewIndex = -1
        Me.crView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.crView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.crView.Location = New System.Drawing.Point(3, -1)
        Me.crView.Name = "crView"
        Me.crView.SelectionFormula = ""
        Me.crView.Size = New System.Drawing.Size(689, 366)
        Me.crView.TabIndex = 0
        Me.crView.ViewTimeSelectionFormula = ""
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnprint.AutoEllipsis = True
        Me.btnprint.BackColor = System.Drawing.Color.SteelBlue
        Me.btnprint.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.ForeColor = System.Drawing.Color.White
        Me.btnprint.Location = New System.Drawing.Point(504, 372)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(92, 35)
        Me.btnprint.TabIndex = 345374
        Me.btnprint.Text = "&Print"
        Me.btnprint.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.AutoEllipsis = True
        Me.btnBack.BackColor = System.Drawing.Color.SteelBlue
        Me.btnBack.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(12, 372)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(92, 35)
        Me.btnBack.TabIndex = 345373
        Me.btnBack.Text = "&Back"
        Me.btnBack.UseVisualStyleBackColor = False
        Me.btnBack.Visible = False
        '
        'btnSetup
        '
        Me.btnSetup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSetup.AutoEllipsis = True
        Me.btnSetup.BackColor = System.Drawing.Color.SteelBlue
        Me.btnSetup.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSetup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetup.ForeColor = System.Drawing.Color.White
        Me.btnSetup.Location = New System.Drawing.Point(410, 372)
        Me.btnSetup.Name = "btnSetup"
        Me.btnSetup.Size = New System.Drawing.Size(92, 35)
        Me.btnSetup.TabIndex = 345372
        Me.btnSetup.Text = "&Setup"
        Me.btnSetup.UseVisualStyleBackColor = False
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
        Me.btnExit.Location = New System.Drawing.Point(598, 372)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(92, 35)
        Me.btnExit.TabIndex = 345371
        Me.btnExit.Text = "E&xit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'btnwhatsapp
        '
        Me.btnwhatsapp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnwhatsapp.AutoEllipsis = True
        Me.btnwhatsapp.BackColor = System.Drawing.Color.SteelBlue
        Me.btnwhatsapp.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnwhatsapp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnwhatsapp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnwhatsapp.ForeColor = System.Drawing.Color.White
        Me.btnwhatsapp.Location = New System.Drawing.Point(110, 371)
        Me.btnwhatsapp.Name = "btnwhatsapp"
        Me.btnwhatsapp.Size = New System.Drawing.Size(92, 35)
        Me.btnwhatsapp.TabIndex = 345375
        Me.btnwhatsapp.Text = "WhatsApp"
        Me.btnwhatsapp.UseVisualStyleBackColor = False
        Me.btnwhatsapp.Visible = False
        '
        'ReprtviewNEWfrm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(694, 410)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnwhatsapp)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnSetup)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.crView)
        Me.Name = "ReprtviewNEWfrm"
        Me.ShowInTaskbar = False
        Me.Text = "Reprtviewfrm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents crView As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents btnprint As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnSetup As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents btnwhatsapp As System.Windows.Forms.Button
End Class
