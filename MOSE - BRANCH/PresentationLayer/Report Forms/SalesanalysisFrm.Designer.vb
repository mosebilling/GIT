<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SalesanalysisFrm
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
        Me.btnload = New System.Windows.Forms.Button
        Me.dtpto = New System.Windows.Forms.DateTimePicker
        Me.grdItem = New System.Windows.Forms.DataGridView
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label26 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.btnApply = New System.Windows.Forms.Button
        Me.btnclose = New System.Windows.Forms.Button
        Me.grdTransactions = New System.Windows.Forms.DataGridView
        Me.cmboptions = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.chksolditem = New System.Windows.Forms.CheckBox
        Me.chkFormat = New System.Windows.Forms.CheckBox
        Me.chkoutstanding = New System.Windows.Forms.CheckBox
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTransactions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnload
        '
        Me.btnload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.Location = New System.Drawing.Point(4, 294)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(138, 35)
        Me.btnload.TabIndex = 345509
        Me.btnload.Text = "Load"
        Me.btnload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnload.UseVisualStyleBackColor = False
        '
        'dtpto
        '
        Me.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpto.Location = New System.Drawing.Point(12, 82)
        Me.dtpto.Name = "dtpto"
        Me.dtpto.Size = New System.Drawing.Size(95, 20)
        Me.dtpto.TabIndex = 345471
        Me.dtpto.TabStop = False
        '
        'grdItem
        '
        Me.grdItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItem.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItem.Location = New System.Drawing.Point(157, 34)
        Me.grdItem.Name = "grdItem"
        Me.grdItem.Size = New System.Drawing.Size(851, 266)
        Me.grdItem.TabIndex = 345497
        '
        'cldrStartDate
        '
        Me.cldrStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrStartDate.Location = New System.Drawing.Point(12, 56)
        Me.cldrStartDate.Name = "cldrStartDate"
        Me.cldrStartDate.Size = New System.Drawing.Size(95, 20)
        Me.cldrStartDate.TabIndex = 345470
        Me.cldrStartDate.TabStop = False
        '
        'Timer1
        '
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
        Me.Panel1.Size = New System.Drawing.Size(1011, 32)
        Me.Panel1.TabIndex = 345501
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(37, 6)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(130, 18)
        Me.Label26.TabIndex = 345458
        Me.Label26.Text = "SALES ANALYSIS"
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
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.Checked = True
        Me.chkSearch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(609, 309)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345500
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
        Me.txtSeq.Location = New System.Drawing.Point(329, 308)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(274, 20)
        Me.txtSeq.TabIndex = 345499
        '
        'cmbOrder
        '
        Me.cmbOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOrder.Location = New System.Drawing.Point(157, 306)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOrder.Size = New System.Drawing.Size(166, 22)
        Me.cmbOrder.TabIndex = 345498
        Me.cmbOrder.TabStop = False
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
        Me.btnApply.Location = New System.Drawing.Point(838, 536)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(85, 35)
        Me.btnApply.TabIndex = 345514
        Me.btnApply.Text = "Preview"
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
        Me.btnclose.Location = New System.Drawing.Point(926, 536)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(82, 35)
        Me.btnclose.TabIndex = 345504
        Me.btnclose.Text = "E&xit"
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'grdTransactions
        '
        Me.grdTransactions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdTransactions.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdTransactions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTransactions.Location = New System.Drawing.Point(4, 334)
        Me.grdTransactions.Name = "grdTransactions"
        Me.grdTransactions.Size = New System.Drawing.Size(1004, 198)
        Me.grdTransactions.TabIndex = 345503
        '
        'cmboptions
        '
        Me.cmboptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmboptions.FormattingEnabled = True
        Me.cmboptions.Items.AddRange(New Object() {"Customer wise", "Item wise", "Invoice wise", "Day wise", "Month wise", "Year wise"})
        Me.cmboptions.Location = New System.Drawing.Point(12, 125)
        Me.cmboptions.Name = "cmboptions"
        Me.cmboptions.Size = New System.Drawing.Size(137, 21)
        Me.cmboptions.TabIndex = 345515
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(12, 107)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 345516
        Me.Label1.Text = "Options"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(12, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 345517
        Me.Label2.Text = "Date Range"
        '
        'chksolditem
        '
        Me.chksolditem.AutoSize = True
        Me.chksolditem.BackColor = System.Drawing.Color.Transparent
        Me.chksolditem.Location = New System.Drawing.Point(12, 155)
        Me.chksolditem.Name = "chksolditem"
        Me.chksolditem.Size = New System.Drawing.Size(99, 17)
        Me.chksolditem.TabIndex = 345518
        Me.chksolditem.Text = "Sold Items Only"
        Me.chksolditem.UseVisualStyleBackColor = False
        Me.chksolditem.Visible = False
        '
        'chkFormat
        '
        Me.chkFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(767, 544)
        Me.chkFormat.Name = "chkFormat"
        Me.chkFormat.Size = New System.Drawing.Size(65, 19)
        Me.chkFormat.TabIndex = 345519
        Me.chkFormat.Text = "&Format"
        Me.chkFormat.UseVisualStyleBackColor = False
        '
        'chkoutstanding
        '
        Me.chkoutstanding.AutoSize = True
        Me.chkoutstanding.BackColor = System.Drawing.Color.Transparent
        Me.chkoutstanding.Location = New System.Drawing.Point(12, 178)
        Me.chkoutstanding.Name = "chkoutstanding"
        Me.chkoutstanding.Size = New System.Drawing.Size(112, 17)
        Me.chkoutstanding.TabIndex = 345520
        Me.chkoutstanding.Text = "Sales Outstanding"
        Me.chkoutstanding.UseVisualStyleBackColor = False
        Me.chkoutstanding.Visible = False
        '
        'SalesanalysisFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1011, 572)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkoutstanding)
        Me.Controls.Add(Me.chkFormat)
        Me.Controls.Add(Me.chksolditem)
        Me.Controls.Add(Me.cldrStartDate)
        Me.Controls.Add(Me.dtpto)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmboptions)
        Me.Controls.Add(Me.btnload)
        Me.Controls.Add(Me.grdItem)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.txtSeq)
        Me.Controls.Add(Me.cmbOrder)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.grdTransactions)
        Me.Name = "SalesanalysisFrm"
        Me.Text = "SalesanalysisFrm"
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTransactions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents dtpto As System.Windows.Forms.DateTimePicker
    Friend WithEvents grdItem As System.Windows.Forms.DataGridView
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents grdTransactions As System.Windows.Forms.DataGridView
    Friend WithEvents cmboptions As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chksolditem As System.Windows.Forms.CheckBox
    Friend WithEvents chkFormat As System.Windows.Forms.CheckBox
    Friend WithEvents chkoutstanding As System.Windows.Forms.CheckBox
End Class
