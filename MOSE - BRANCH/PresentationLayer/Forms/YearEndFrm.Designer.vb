<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class YearEndFrm
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label26 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.btnApply = New System.Windows.Forms.Button
        Me.btnclose = New System.Windows.Forms.Button
        Me.grdaccounts = New System.Windows.Forms.DataGridView
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbcategory = New System.Windows.Forms.ComboBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.btntransfer = New System.Windows.Forms.Button
        Me.pb = New System.Windows.Forms.ProgressBar
        Me.lblrec = New System.Windows.Forms.Label
        Me.lblmodule = New System.Windows.Forms.Label
        Me.cmbseconddb = New System.Windows.Forms.ComboBox
        Me.Worker = New System.ComponentModel.BackgroundWorker
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdaccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1117, 32)
        Me.Panel1.TabIndex = 345502
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(37, 6)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(84, 18)
        Me.Label26.TabIndex = 345458
        Me.Label26.Text = "YEAR END"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.application_icon
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(3, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(30, 27)
        Me.PictureBox2.TabIndex = 345457
        Me.PictureBox2.TabStop = False
        '
        'btnApply
        '
        Me.btnApply.AutoEllipsis = True
        Me.btnApply.BackColor = System.Drawing.Color.SteelBlue
        Me.btnApply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnApply.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnApply.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApply.ForeColor = System.Drawing.Color.White
        Me.btnApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnApply.Location = New System.Drawing.Point(113, 45)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(85, 35)
        Me.btnApply.TabIndex = 345516
        Me.btnApply.Text = "Process"
        Me.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnApply.UseVisualStyleBackColor = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ForeColor = System.Drawing.Color.White
        Me.btnclose.Location = New System.Drawing.Point(1023, 439)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(82, 35)
        Me.btnclose.TabIndex = 345515
        Me.btnclose.Text = "E&xit"
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'grdaccounts
        '
        Me.grdaccounts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdaccounts.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdaccounts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdaccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdaccounts.Location = New System.Drawing.Point(12, 86)
        Me.grdaccounts.Name = "grdaccounts"
        Me.grdaccounts.Size = New System.Drawing.Size(466, 388)
        Me.grdaccounts.TabIndex = 345517
        '
        'cldrStartDate
        '
        Me.cldrStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrStartDate.Location = New System.Drawing.Point(12, 60)
        Me.cldrStartDate.Name = "cldrStartDate"
        Me.cldrStartDate.Size = New System.Drawing.Size(95, 20)
        Me.cldrStartDate.TabIndex = 345518
        Me.cldrStartDate.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(12, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 345519
        Me.Label2.Text = "Date"
        '
        'cmbcategory
        '
        Me.cmbcategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcategory.FormattingEnabled = True
        Me.cmbcategory.Items.AddRange(New Object() {"Customer", "Supplier", "Bank", "Cash", "P.D.C.(R)", "P.D.C.(I)", "Vazhipadu", "ALL"})
        Me.cmbcategory.Location = New System.Drawing.Point(204, 45)
        Me.cmbcategory.Name = "cmbcategory"
        Me.cmbcategory.Size = New System.Drawing.Size(132, 21)
        Me.cmbcategory.TabIndex = 345520
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(494, 86)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(611, 347)
        Me.DataGridView1.TabIndex = 345521
        '
        'btntransfer
        '
        Me.btntransfer.AutoEllipsis = True
        Me.btntransfer.BackColor = System.Drawing.Color.SteelBlue
        Me.btntransfer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btntransfer.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btntransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btntransfer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntransfer.ForeColor = System.Drawing.Color.White
        Me.btntransfer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btntransfer.Location = New System.Drawing.Point(494, 439)
        Me.btntransfer.Name = "btntransfer"
        Me.btntransfer.Size = New System.Drawing.Size(85, 35)
        Me.btntransfer.TabIndex = 345522
        Me.btntransfer.Text = "Transfer"
        Me.btntransfer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btntransfer.UseVisualStyleBackColor = False
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(494, 60)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(611, 20)
        Me.pb.TabIndex = 345523
        '
        'lblrec
        '
        Me.lblrec.AutoSize = True
        Me.lblrec.BackColor = System.Drawing.Color.Transparent
        Me.lblrec.Location = New System.Drawing.Point(1063, 44)
        Me.lblrec.Name = "lblrec"
        Me.lblrec.Size = New System.Drawing.Size(42, 13)
        Me.lblrec.TabIndex = 345525
        Me.lblrec.Text = "Record"
        '
        'lblmodule
        '
        Me.lblmodule.AutoSize = True
        Me.lblmodule.BackColor = System.Drawing.Color.Transparent
        Me.lblmodule.Location = New System.Drawing.Point(491, 45)
        Me.lblmodule.Name = "lblmodule"
        Me.lblmodule.Size = New System.Drawing.Size(42, 13)
        Me.lblmodule.TabIndex = 345524
        Me.lblmodule.Text = "Module"
        '
        'cmbseconddb
        '
        Me.cmbseconddb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbseconddb.FormattingEnabled = True
        Me.cmbseconddb.Items.AddRange(New Object() {"Customer", "Supplier", "Bank", "Cash", "P.D.C.(R)", "P.D.C.(I)", "Vazhipadu", "ALL"})
        Me.cmbseconddb.Location = New System.Drawing.Point(585, 439)
        Me.cmbseconddb.Name = "cmbseconddb"
        Me.cmbseconddb.Size = New System.Drawing.Size(132, 21)
        Me.cmbseconddb.TabIndex = 345526
        '
        'YearEndFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1117, 486)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmbseconddb)
        Me.Controls.Add(Me.lblrec)
        Me.Controls.Add(Me.lblmodule)
        Me.Controls.Add(Me.pb)
        Me.Controls.Add(Me.btntransfer)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.cmbcategory)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cldrStartDate)
        Me.Controls.Add(Me.grdaccounts)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "YearEndFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "YearEndFrm"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdaccounts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents grdaccounts As System.Windows.Forms.DataGridView
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbcategory As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btntransfer As System.Windows.Forms.Button
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents lblrec As System.Windows.Forms.Label
    Friend WithEvents lblmodule As System.Windows.Forms.Label
    Friend WithEvents cmbseconddb As System.Windows.Forms.ComboBox
    Friend WithEvents Worker As System.ComponentModel.BackgroundWorker
End Class
