<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FruitsTrayAnalysisFrm
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label26 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.btnrefresh = New System.Windows.Forms.Button
        Me.grdcustomers = New System.Windows.Forms.DataGridView
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.btnexit = New System.Windows.Forms.Button
        Me.grdtray = New System.Windows.Forms.DataGridView
        Me.grdtransaction = New System.Windows.Forms.DataGridView
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.dtpto = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.rdocustomer = New System.Windows.Forms.RadioButton
        Me.rdosupplier = New System.Windows.Forms.RadioButton
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnob = New System.Windows.Forms.Button
        Me.btnreturn = New System.Windows.Forms.Button
        Me.grdadditional = New System.Windows.Forms.DataGridView
        Me.chkFormat = New System.Windows.Forms.CheckBox
        Me.btnApply = New System.Windows.Forms.Button
        Me.btnload = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdcustomers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdtray, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdtransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdadditional, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(1295, 32)
        Me.Panel1.TabIndex = 345503
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(37, 6)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(161, 18)
        Me.Label26.TabIndex = 345458
        Me.Label26.Text = "TRAY OUTSTANDING"
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
        'btnrefresh
        '
        Me.btnrefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnrefresh.AutoEllipsis = True
        Me.btnrefresh.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnrefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrefresh.ForeColor = System.Drawing.Color.White
        Me.btnrefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnrefresh.Location = New System.Drawing.Point(601, 547)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(85, 35)
        Me.btnrefresh.TabIndex = 345531
        Me.btnrefresh.Text = "Refresh"
        Me.btnrefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrefresh.UseVisualStyleBackColor = False
        '
        'grdcustomers
        '
        Me.grdcustomers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdcustomers.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdcustomers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdcustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdcustomers.Location = New System.Drawing.Point(4, 38)
        Me.grdcustomers.Name = "grdcustomers"
        Me.grdcustomers.Size = New System.Drawing.Size(591, 506)
        Me.grdcustomers.TabIndex = 345527
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.Checked = True
        Me.chkSearch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(452, 552)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345530
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
        '
        'txtSeq
        '
        Me.txtSeq.AcceptsReturn = True
        Me.txtSeq.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSeq.BackColor = System.Drawing.SystemColors.Window
        Me.txtSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeq.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSeq.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeq.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSeq.Location = New System.Drawing.Point(172, 551)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(274, 20)
        Me.txtSeq.TabIndex = 345529
        '
        'cmbOrder
        '
        Me.cmbOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOrder.Location = New System.Drawing.Point(0, 549)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOrder.Size = New System.Drawing.Size(166, 22)
        Me.cmbOrder.TabIndex = 345528
        Me.cmbOrder.TabStop = False
        '
        'btnexit
        '
        Me.btnexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexit.AutoEllipsis = True
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnexit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnexit.Location = New System.Drawing.Point(1202, 547)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(85, 35)
        Me.btnexit.TabIndex = 345532
        Me.btnexit.Text = "E&xit"
        Me.btnexit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'grdtray
        '
        Me.grdtray.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdtray.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdtray.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdtray.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdtray.Location = New System.Drawing.Point(601, 78)
        Me.grdtray.Name = "grdtray"
        Me.grdtray.Size = New System.Drawing.Size(372, 228)
        Me.grdtray.TabIndex = 345533
        '
        'grdtransaction
        '
        Me.grdtransaction.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdtransaction.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdtransaction.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdtransaction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdtransaction.Location = New System.Drawing.Point(601, 312)
        Me.grdtransaction.Name = "grdtransaction"
        Me.grdtransaction.Size = New System.Drawing.Size(686, 232)
        Me.grdtransaction.TabIndex = 345534
        '
        'cldrStartDate
        '
        Me.cldrStartDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cldrStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrStartDate.Location = New System.Drawing.Point(1010, 43)
        Me.cldrStartDate.Name = "cldrStartDate"
        Me.cldrStartDate.Size = New System.Drawing.Size(95, 20)
        Me.cldrStartDate.TabIndex = 345535
        Me.cldrStartDate.TabStop = False
        '
        'dtpto
        '
        Me.dtpto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpto.Location = New System.Drawing.Point(1112, 43)
        Me.dtpto.Name = "dtpto"
        Me.dtpto.Size = New System.Drawing.Size(95, 20)
        Me.dtpto.TabIndex = 345536
        Me.dtpto.TabStop = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(939, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 345537
        Me.Label2.Text = "Date Range"
        '
        'rdocustomer
        '
        Me.rdocustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdocustomer.AutoSize = True
        Me.rdocustomer.BackColor = System.Drawing.Color.Transparent
        Me.rdocustomer.Checked = True
        Me.rdocustomer.Location = New System.Drawing.Point(833, 37)
        Me.rdocustomer.Name = "rdocustomer"
        Me.rdocustomer.Size = New System.Drawing.Size(69, 17)
        Me.rdocustomer.TabIndex = 345539
        Me.rdocustomer.TabStop = True
        Me.rdocustomer.Text = "Customer"
        Me.rdocustomer.UseVisualStyleBackColor = False
        '
        'rdosupplier
        '
        Me.rdosupplier.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdosupplier.AutoSize = True
        Me.rdosupplier.BackColor = System.Drawing.Color.Transparent
        Me.rdosupplier.Location = New System.Drawing.Point(833, 56)
        Me.rdosupplier.Name = "rdosupplier"
        Me.rdosupplier.Size = New System.Drawing.Size(63, 17)
        Me.rdosupplier.TabIndex = 345540
        Me.rdosupplier.Text = "Supplier"
        Me.rdosupplier.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        '
        'btnob
        '
        Me.btnob.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnob.AutoEllipsis = True
        Me.btnob.BackColor = System.Drawing.Color.SteelBlue
        Me.btnob.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnob.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnob.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnob.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnob.ForeColor = System.Drawing.Color.White
        Me.btnob.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnob.Location = New System.Drawing.Point(678, 38)
        Me.btnob.Name = "btnob"
        Me.btnob.Size = New System.Drawing.Size(138, 35)
        Me.btnob.TabIndex = 345541
        Me.btnob.Text = "Opening Balance"
        Me.btnob.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnob.UseVisualStyleBackColor = False
        '
        'btnreturn
        '
        Me.btnreturn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnreturn.AutoEllipsis = True
        Me.btnreturn.BackColor = System.Drawing.Color.SteelBlue
        Me.btnreturn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnreturn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnreturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnreturn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnreturn.ForeColor = System.Drawing.Color.White
        Me.btnreturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnreturn.Location = New System.Drawing.Point(604, 38)
        Me.btnreturn.Name = "btnreturn"
        Me.btnreturn.Size = New System.Drawing.Size(68, 35)
        Me.btnreturn.TabIndex = 345542
        Me.btnreturn.Text = "Return"
        Me.btnreturn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnreturn.UseVisualStyleBackColor = False
        '
        'grdadditional
        '
        Me.grdadditional.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdadditional.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdadditional.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdadditional.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdadditional.Location = New System.Drawing.Point(979, 78)
        Me.grdadditional.Name = "grdadditional"
        Me.grdadditional.Size = New System.Drawing.Size(304, 228)
        Me.grdadditional.TabIndex = 345543
        '
        'chkFormat
        '
        Me.chkFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(1040, 557)
        Me.chkFormat.Name = "chkFormat"
        Me.chkFormat.Size = New System.Drawing.Size(65, 19)
        Me.chkFormat.TabIndex = 345545
        Me.chkFormat.Text = "&Format"
        Me.chkFormat.UseVisualStyleBackColor = False
        '
        'btnApply
        '
        Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApply.AutoEllipsis = True
        Me.btnApply.BackColor = System.Drawing.Color.SteelBlue
        Me.btnApply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnApply.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnApply.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApply.ForeColor = System.Drawing.Color.White
        Me.btnApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnApply.Location = New System.Drawing.Point(1111, 547)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(85, 35)
        Me.btnApply.TabIndex = 345544
        Me.btnApply.Text = "Preview"
        Me.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnApply.UseVisualStyleBackColor = False
        '
        'btnload
        '
        Me.btnload.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnload.AutoEllipsis = True
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnload.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnload.Location = New System.Drawing.Point(1213, 38)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(70, 35)
        Me.btnload.TabIndex = 345546
        Me.btnload.Text = "Load"
        Me.btnload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnload.UseVisualStyleBackColor = False
        '
        'FruitsTrayAnalysisFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1295, 583)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnload)
        Me.Controls.Add(Me.chkFormat)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.btnreturn)
        Me.Controls.Add(Me.btnob)
        Me.Controls.Add(Me.grdadditional)
        Me.Controls.Add(Me.rdosupplier)
        Me.Controls.Add(Me.rdocustomer)
        Me.Controls.Add(Me.cldrStartDate)
        Me.Controls.Add(Me.dtpto)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.grdtransaction)
        Me.Controls.Add(Me.grdtray)
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.btnrefresh)
        Me.Controls.Add(Me.grdcustomers)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.txtSeq)
        Me.Controls.Add(Me.cmbOrder)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FruitsTrayAnalysisFrm"
        Me.Text = "FruitsTrayAnalysisFrm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdcustomers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdtray, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdtransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdadditional, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents btnrefresh As System.Windows.Forms.Button
    Friend WithEvents grdcustomers As System.Windows.Forms.DataGridView
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents grdtray As System.Windows.Forms.DataGridView
    Friend WithEvents grdtransaction As System.Windows.Forms.DataGridView
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rdocustomer As System.Windows.Forms.RadioButton
    Friend WithEvents rdosupplier As System.Windows.Forms.RadioButton
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnob As System.Windows.Forms.Button
    Friend WithEvents btnreturn As System.Windows.Forms.Button
    Friend WithEvents grdadditional As System.Windows.Forms.DataGridView
    Friend WithEvents chkFormat As System.Windows.Forms.CheckBox
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents btnload As System.Windows.Forms.Button
End Class
