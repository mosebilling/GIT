<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RefreshcostAverage
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
        Me.Worker = New System.ComponentModel.BackgroundWorker
        Me.lblitem = New System.Windows.Forms.Label
        Me.lblVal = New System.Windows.Forms.Label
        Me.pb = New System.Windows.Forms.ProgressBar
        Me.btnTransactionItems = New System.Windows.Forms.Button
        Me.btnexit = New System.Windows.Forms.Button
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.lblcap = New System.Windows.Forms.Label
        Me.chkremove = New System.Windows.Forms.CheckBox
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Worker
        '
        '
        'lblitem
        '
        Me.lblitem.AutoSize = True
        Me.lblitem.BackColor = System.Drawing.Color.Transparent
        Me.lblitem.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblitem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblitem.Location = New System.Drawing.Point(12, 116)
        Me.lblitem.Name = "lblitem"
        Me.lblitem.Size = New System.Drawing.Size(35, 20)
        Me.lblitem.TabIndex = 345374
        Me.lblitem.Text = "lable"
        Me.lblitem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.lblitem.UseCompatibleTextRendering = True
        '
        'lblVal
        '
        Me.lblVal.AutoSize = True
        Me.lblVal.BackColor = System.Drawing.Color.Transparent
        Me.lblVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVal.ForeColor = System.Drawing.Color.Green
        Me.lblVal.Location = New System.Drawing.Point(12, 74)
        Me.lblVal.Name = "lblVal"
        Me.lblVal.Size = New System.Drawing.Size(35, 20)
        Me.lblVal.TabIndex = 345373
        Me.lblVal.Text = "lable"
        Me.lblVal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.lblVal.UseCompatibleTextRendering = True
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(12, 100)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(549, 13)
        Me.pb.TabIndex = 345372
        '
        'btnTransactionItems
        '
        Me.btnTransactionItems.BackColor = System.Drawing.Color.SteelBlue
        Me.btnTransactionItems.FlatAppearance.BorderSize = 0
        Me.btnTransactionItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTransactionItems.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransactionItems.ForeColor = System.Drawing.Color.White
        Me.btnTransactionItems.Location = New System.Drawing.Point(238, 152)
        Me.btnTransactionItems.Name = "btnTransactionItems"
        Me.btnTransactionItems.Size = New System.Drawing.Size(207, 38)
        Me.btnTransactionItems.TabIndex = 345375
        Me.btnTransactionItems.Text = "Refresh Transaction Items Cost"
        Me.btnTransactionItems.UseVisualStyleBackColor = False
        '
        'btnexit
        '
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.FlatAppearance.BorderSize = 0
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.Location = New System.Drawing.Point(448, 152)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(113, 38)
        Me.btnexit.TabIndex = 345376
        Me.btnexit.Text = "E&xit"
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.White
        Me.Panel4.Controls.Add(Me.PictureBox2)
        Me.Panel4.Controls.Add(Me.lblcap)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(573, 33)
        Me.Panel4.TabIndex = 345452
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(32, 30)
        Me.PictureBox2.TabIndex = 345460
        Me.PictureBox2.TabStop = False
        '
        'lblcap
        '
        Me.lblcap.AutoSize = True
        Me.lblcap.BackColor = System.Drawing.Color.Transparent
        Me.lblcap.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcap.ForeColor = System.Drawing.Color.Black
        Me.lblcap.Location = New System.Drawing.Point(41, 6)
        Me.lblcap.Name = "lblcap"
        Me.lblcap.Size = New System.Drawing.Size(162, 18)
        Me.lblcap.TabIndex = 6
        Me.lblcap.Text = "Calculate Cost Average"
        '
        'chkremove
        '
        Me.chkremove.AutoSize = True
        Me.chkremove.BackColor = System.Drawing.Color.Transparent
        Me.chkremove.Location = New System.Drawing.Point(480, 77)
        Me.chkremove.Name = "chkremove"
        Me.chkremove.Size = New System.Drawing.Size(90, 17)
        Me.chkremove.TabIndex = 345453
        Me.chkremove.Text = "Remove Cost"
        Me.chkremove.UseVisualStyleBackColor = False
        '
        'RefreshcostAverage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(573, 202)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkremove)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.btnTransactionItems)
        Me.Controls.Add(Me.lblitem)
        Me.Controls.Add(Me.lblVal)
        Me.Controls.Add(Me.pb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "RefreshcostAverage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Calculate Cost Average"
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Worker As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblitem As System.Windows.Forms.Label
    Friend WithEvents lblVal As System.Windows.Forms.Label
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents btnTransactionItems As System.Windows.Forms.Button
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblcap As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents chkremove As System.Windows.Forms.CheckBox
End Class
