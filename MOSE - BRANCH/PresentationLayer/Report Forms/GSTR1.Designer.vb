<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GSTR1
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
        Me.components = New System.ComponentModel.Container
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.grdb2b = New System.Windows.Forms.DataGridView
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.grdb2c = New System.Windows.Forms.DataGridView
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.chkb2b = New System.Windows.Forms.CheckBox
        Me.grdHSN = New System.Windows.Forms.DataGridView
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.grdb2bSR = New System.Windows.Forms.DataGridView
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.grdexempted = New System.Windows.Forms.DataGridView
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.grddocIS = New System.Windows.Forms.DataGridView
        Me.TabPage7 = New System.Windows.Forms.TabPage
        Me.grddocSR = New System.Windows.Forms.DataGridView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblName = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.btnLoad = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnexcell = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cldrEnddate = New System.Windows.Forms.DateTimePicker
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnjson = New System.Windows.Forms.Button
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.grdb2b, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdb2c, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.grdHSN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.grdb2bSR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        CType(Me.grdexempted, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage6.SuspendLayout()
        CType(Me.grddocIS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage7.SuspendLayout()
        CType(Me.grddocSR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.Location = New System.Drawing.Point(3, 40)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(828, 442)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.grdb2b)
        Me.TabPage1.Location = New System.Drawing.Point(4, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(820, 416)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "B2B SALES"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'grdb2b
        '
        Me.grdb2b.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdb2b.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdb2b.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdb2b.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdb2b.GridColor = System.Drawing.Color.Gainsboro
        Me.grdb2b.Location = New System.Drawing.Point(6, 6)
        Me.grdb2b.Name = "grdb2b"
        Me.grdb2b.Size = New System.Drawing.Size(806, 393)
        Me.grdb2b.TabIndex = 345475
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.grdb2c)
        Me.TabPage2.Location = New System.Drawing.Point(4, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(820, 416)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "B2C SALES"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'grdb2c
        '
        Me.grdb2c.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdb2c.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdb2c.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdb2c.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdb2c.GridColor = System.Drawing.Color.Gainsboro
        Me.grdb2c.Location = New System.Drawing.Point(7, 7)
        Me.grdb2c.Name = "grdb2c"
        Me.grdb2c.Size = New System.Drawing.Size(806, 404)
        Me.grdb2c.TabIndex = 345476
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.chkb2b)
        Me.TabPage3.Controls.Add(Me.grdHSN)
        Me.TabPage3.Location = New System.Drawing.Point(4, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(820, 416)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "HSN WISE SALES"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'chkb2b
        '
        Me.chkb2b.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkb2b.AutoSize = True
        Me.chkb2b.Location = New System.Drawing.Point(7, 391)
        Me.chkb2b.Name = "chkb2b"
        Me.chkb2b.Size = New System.Drawing.Size(70, 17)
        Me.chkb2b.TabIndex = 345478
        Me.chkb2b.Text = "B2B Only"
        Me.chkb2b.UseVisualStyleBackColor = True
        '
        'grdHSN
        '
        Me.grdHSN.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdHSN.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdHSN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdHSN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdHSN.GridColor = System.Drawing.Color.Gainsboro
        Me.grdHSN.Location = New System.Drawing.Point(7, 6)
        Me.grdHSN.Name = "grdHSN"
        Me.grdHSN.Size = New System.Drawing.Size(806, 379)
        Me.grdHSN.TabIndex = 345477
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.grdb2bSR)
        Me.TabPage4.Location = New System.Drawing.Point(4, 4)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(820, 416)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "B2B SALES RETURN"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'grdb2bSR
        '
        Me.grdb2bSR.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdb2bSR.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdb2bSR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdb2bSR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdb2bSR.GridColor = System.Drawing.Color.Gainsboro
        Me.grdb2bSR.Location = New System.Drawing.Point(7, 6)
        Me.grdb2bSR.Name = "grdb2bSR"
        Me.grdb2bSR.Size = New System.Drawing.Size(806, 404)
        Me.grdb2bSR.TabIndex = 345477
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.grdexempted)
        Me.TabPage5.Location = New System.Drawing.Point(4, 4)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(820, 416)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "EXMPTED"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'grdexempted
        '
        Me.grdexempted.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdexempted.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdexempted.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdexempted.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdexempted.GridColor = System.Drawing.Color.Gainsboro
        Me.grdexempted.Location = New System.Drawing.Point(7, 6)
        Me.grdexempted.Name = "grdexempted"
        Me.grdexempted.Size = New System.Drawing.Size(806, 404)
        Me.grdexempted.TabIndex = 345477
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.grddocIS)
        Me.TabPage6.Location = New System.Drawing.Point(4, 4)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(820, 416)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Document issued (IS)"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'grddocIS
        '
        Me.grddocIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grddocIS.BackgroundColor = System.Drawing.Color.Ivory
        Me.grddocIS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grddocIS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grddocIS.GridColor = System.Drawing.Color.Gainsboro
        Me.grddocIS.Location = New System.Drawing.Point(7, 6)
        Me.grddocIS.Name = "grddocIS"
        Me.grddocIS.Size = New System.Drawing.Size(806, 404)
        Me.grddocIS.TabIndex = 345477
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.grddocSR)
        Me.TabPage7.Location = New System.Drawing.Point(4, 4)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage7.Size = New System.Drawing.Size(820, 416)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "Document issued (SR)"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'grddocSR
        '
        Me.grddocSR.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grddocSR.BackgroundColor = System.Drawing.Color.Ivory
        Me.grddocSR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grddocSR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grddocSR.GridColor = System.Drawing.Color.Gainsboro
        Me.grddocSR.Location = New System.Drawing.Point(7, 6)
        Me.grddocSR.Name = "grddocSR"
        Me.grddocSR.Size = New System.Drawing.Size(806, 404)
        Me.grddocSR.TabIndex = 345477
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.lblName)
        Me.Panel2.Controls.Add(Me.PictureBox2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(831, 32)
        Me.Panel2.TabIndex = 345466
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.White
        Me.lblName.Font = New System.Drawing.Font("Goudy Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblName.Location = New System.Drawing.Point(41, 5)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(63, 21)
        Me.lblName.TabIndex = 345458
        Me.lblName.Text = "GSTR1 "
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.button_reports1
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(3, -1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(32, 35)
        Me.PictureBox2.TabIndex = 345457
        Me.PictureBox2.TabStop = False
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.BackColor = System.Drawing.Color.SteelBlue
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.ForeColor = System.Drawing.Color.White
        Me.btnLoad.Location = New System.Drawing.Point(228, 504)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(96, 35)
        Me.btnLoad.TabIndex = 345474
        Me.btnLoad.Text = "&Load"
        Me.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLoad.UseVisualStyleBackColor = False
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
        Me.btnExit.Location = New System.Drawing.Point(731, 506)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(96, 35)
        Me.btnExit.TabIndex = 345473
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnexcell
        '
        Me.btnexcell.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexcell.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexcell.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexcell.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexcell.ForeColor = System.Drawing.Color.White
        Me.btnexcell.Location = New System.Drawing.Point(603, 506)
        Me.btnexcell.Name = "btnexcell"
        Me.btnexcell.Size = New System.Drawing.Size(126, 35)
        Me.btnexcell.TabIndex = 345475
        Me.btnexcell.Text = "Export to Excel"
        Me.btnexcell.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexcell.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.cldrEnddate)
        Me.GroupBox2.Controls.Add(Me.cldrStartDate)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 488)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(215, 51)
        Me.GroupBox2.TabIndex = 345476
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Date Parameter"
        '
        'cldrEnddate
        '
        Me.cldrEnddate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrEnddate.Location = New System.Drawing.Point(110, 22)
        Me.cldrEnddate.Name = "cldrEnddate"
        Me.cldrEnddate.Size = New System.Drawing.Size(96, 20)
        Me.cldrEnddate.TabIndex = 345395
        Me.cldrEnddate.TabStop = False
        '
        'cldrStartDate
        '
        Me.cldrStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrStartDate.Location = New System.Drawing.Point(9, 22)
        Me.cldrStartDate.Name = "cldrStartDate"
        Me.cldrStartDate.Size = New System.Drawing.Size(95, 20)
        Me.cldrStartDate.TabIndex = 345393
        Me.cldrStartDate.TabStop = False
        '
        'Timer1
        '
        '
        'btnjson
        '
        Me.btnjson.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnjson.BackColor = System.Drawing.Color.SteelBlue
        Me.btnjson.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnjson.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnjson.ForeColor = System.Drawing.Color.White
        Me.btnjson.Location = New System.Drawing.Point(475, 506)
        Me.btnjson.Name = "btnjson"
        Me.btnjson.Size = New System.Drawing.Size(126, 35)
        Me.btnjson.TabIndex = 345477
        Me.btnjson.Text = "Export to JSON"
        Me.btnjson.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnjson.UseVisualStyleBackColor = False
        '
        'GSTR1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(831, 546)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnjson)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnexcell)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "GSTR1"
        Me.Text = "GSTR1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.grdb2b, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.grdb2c, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.grdHSN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.grdb2bSR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        CType(Me.grdexempted, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage6.ResumeLayout(False)
        CType(Me.grddocIS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage7.ResumeLayout(False)
        CType(Me.grddocSR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnexcell As System.Windows.Forms.Button
    Friend WithEvents grdb2b As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cldrEnddate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents grdb2c As System.Windows.Forms.DataGridView
    Friend WithEvents grdHSN As System.Windows.Forms.DataGridView
    Friend WithEvents grdb2bSR As System.Windows.Forms.DataGridView
    Friend WithEvents grdexempted As System.Windows.Forms.DataGridView
    Friend WithEvents grddocIS As System.Windows.Forms.DataGridView
    Friend WithEvents grddocSR As System.Windows.Forms.DataGridView
    Friend WithEvents btnjson As System.Windows.Forms.Button
    Friend WithEvents chkb2b As System.Windows.Forms.CheckBox
End Class
