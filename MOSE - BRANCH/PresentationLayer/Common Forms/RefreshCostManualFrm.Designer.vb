<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RefreshCostManualFrm
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
        Me.chkapplydate = New System.Windows.Forms.CheckBox
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.lblcap = New System.Windows.Forms.Label
        Me.Worker = New System.ComponentModel.BackgroundWorker
        Me.btnexit = New System.Windows.Forms.Button
        Me.btnTransactionItems = New System.Windows.Forms.Button
        Me.lblinvoice = New System.Windows.Forms.Label
        Me.lblVal = New System.Windows.Forms.Label
        Me.pb = New System.Windows.Forms.ProgressBar
        Me.cldrstartdate = New System.Windows.Forms.DateTimePicker
        Me.cldrendate = New System.Windows.Forms.DateTimePicker
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblitemcount = New System.Windows.Forms.Label
        Me.lblinvoicecount = New System.Windows.Forms.Label
        Me.txtinno = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkapplydate
        '
        Me.chkapplydate.AutoSize = True
        Me.chkapplydate.BackColor = System.Drawing.Color.Transparent
        Me.chkapplydate.Location = New System.Drawing.Point(244, 42)
        Me.chkapplydate.Name = "chkapplydate"
        Me.chkapplydate.Size = New System.Drawing.Size(78, 17)
        Me.chkapplydate.TabIndex = 345460
        Me.chkapplydate.Text = "Apply Date"
        Me.chkapplydate.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.White
        Me.Panel4.Controls.Add(Me.PictureBox2)
        Me.Panel4.Controls.Add(Me.lblcap)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(563, 33)
        Me.Panel4.TabIndex = 345459
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(46, 29)
        Me.PictureBox2.TabIndex = 345460
        Me.PictureBox2.TabStop = False
        '
        'lblcap
        '
        Me.lblcap.AutoSize = True
        Me.lblcap.BackColor = System.Drawing.Color.Transparent
        Me.lblcap.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcap.ForeColor = System.Drawing.Color.Black
        Me.lblcap.Location = New System.Drawing.Point(53, 9)
        Me.lblcap.Name = "lblcap"
        Me.lblcap.Size = New System.Drawing.Size(162, 18)
        Me.lblcap.TabIndex = 6
        Me.lblcap.Text = "Calculate Cost Average"
        '
        'Worker
        '
        '
        'btnexit
        '
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.FlatAppearance.BorderSize = 0
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.Location = New System.Drawing.Point(444, 142)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(113, 38)
        Me.btnexit.TabIndex = 345458
        Me.btnexit.Text = "E&xit"
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'btnTransactionItems
        '
        Me.btnTransactionItems.BackColor = System.Drawing.Color.SteelBlue
        Me.btnTransactionItems.FlatAppearance.BorderSize = 0
        Me.btnTransactionItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTransactionItems.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransactionItems.ForeColor = System.Drawing.Color.White
        Me.btnTransactionItems.Location = New System.Drawing.Point(234, 142)
        Me.btnTransactionItems.Name = "btnTransactionItems"
        Me.btnTransactionItems.Size = New System.Drawing.Size(207, 38)
        Me.btnTransactionItems.TabIndex = 345457
        Me.btnTransactionItems.Text = "Refresh Transaction Items Cost"
        Me.btnTransactionItems.UseVisualStyleBackColor = False
        '
        'lblinvoice
        '
        Me.lblinvoice.AutoSize = True
        Me.lblinvoice.BackColor = System.Drawing.Color.Transparent
        Me.lblinvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblinvoice.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblinvoice.Location = New System.Drawing.Point(8, 117)
        Me.lblinvoice.Name = "lblinvoice"
        Me.lblinvoice.Size = New System.Drawing.Size(35, 20)
        Me.lblinvoice.TabIndex = 345456
        Me.lblinvoice.Text = "lable"
        Me.lblinvoice.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.lblinvoice.UseCompatibleTextRendering = True
        '
        'lblVal
        '
        Me.lblVal.AutoSize = True
        Me.lblVal.BackColor = System.Drawing.Color.Transparent
        Me.lblVal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVal.ForeColor = System.Drawing.Color.Green
        Me.lblVal.Location = New System.Drawing.Point(8, 75)
        Me.lblVal.Name = "lblVal"
        Me.lblVal.Size = New System.Drawing.Size(35, 20)
        Me.lblVal.TabIndex = 345455
        Me.lblVal.Text = "lable"
        Me.lblVal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.lblVal.UseCompatibleTextRendering = True
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(8, 101)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(549, 13)
        Me.pb.TabIndex = 345454
        '
        'cldrstartdate
        '
        Me.cldrstartdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrstartdate.Location = New System.Drawing.Point(2, 3)
        Me.cldrstartdate.Name = "cldrstartdate"
        Me.cldrstartdate.Size = New System.Drawing.Size(110, 20)
        Me.cldrstartdate.TabIndex = 345461
        '
        'cldrendate
        '
        Me.cldrendate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrendate.Location = New System.Drawing.Point(118, 3)
        Me.cldrendate.Name = "cldrendate"
        Me.cldrendate.Size = New System.Drawing.Size(108, 20)
        Me.cldrendate.TabIndex = 345463
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cldrendate)
        Me.Panel1.Controls.Add(Me.cldrstartdate)
        Me.Panel1.Enabled = False
        Me.Panel1.Location = New System.Drawing.Point(326, 39)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(231, 28)
        Me.Panel1.TabIndex = 345464
        '
        'lblitemcount
        '
        Me.lblitemcount.AutoSize = True
        Me.lblitemcount.BackColor = System.Drawing.Color.Transparent
        Me.lblitemcount.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblitemcount.ForeColor = System.Drawing.Color.Green
        Me.lblitemcount.Location = New System.Drawing.Point(8, 47)
        Me.lblitemcount.Name = "lblitemcount"
        Me.lblitemcount.Size = New System.Drawing.Size(35, 20)
        Me.lblitemcount.TabIndex = 345465
        Me.lblitemcount.Text = "lable"
        Me.lblitemcount.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.lblitemcount.UseCompatibleTextRendering = True
        '
        'lblinvoicecount
        '
        Me.lblinvoicecount.AutoSize = True
        Me.lblinvoicecount.BackColor = System.Drawing.Color.Transparent
        Me.lblinvoicecount.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblinvoicecount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblinvoicecount.Location = New System.Drawing.Point(8, 142)
        Me.lblinvoicecount.Name = "lblinvoicecount"
        Me.lblinvoicecount.Size = New System.Drawing.Size(35, 20)
        Me.lblinvoicecount.TabIndex = 345466
        Me.lblinvoicecount.Text = "lable"
        Me.lblinvoicecount.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.lblinvoicecount.UseCompatibleTextRendering = True
        '
        'txtinno
        '
        Me.txtinno.Location = New System.Drawing.Point(452, 74)
        Me.txtinno.Name = "txtinno"
        Me.txtinno.Size = New System.Drawing.Size(100, 20)
        Me.txtinno.TabIndex = 345467
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(407, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 345468
        Me.Label1.Text = "Inv No"
        '
        'RefreshCostManualFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(563, 192)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtinno)
        Me.Controls.Add(Me.lblinvoicecount)
        Me.Controls.Add(Me.lblitemcount)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.chkapplydate)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.btnTransactionItems)
        Me.Controls.Add(Me.lblinvoice)
        Me.Controls.Add(Me.lblVal)
        Me.Controls.Add(Me.pb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "RefreshCostManualFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Refresh Cost"
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkapplydate As System.Windows.Forms.CheckBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents lblcap As System.Windows.Forms.Label
    Friend WithEvents Worker As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents btnTransactionItems As System.Windows.Forms.Button
    Friend WithEvents lblinvoice As System.Windows.Forms.Label
    Friend WithEvents lblVal As System.Windows.Forms.Label
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents cldrstartdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cldrendate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblitemcount As System.Windows.Forms.Label
    Friend WithEvents lblinvoicecount As System.Windows.Forms.Label
    Friend WithEvents txtinno As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
