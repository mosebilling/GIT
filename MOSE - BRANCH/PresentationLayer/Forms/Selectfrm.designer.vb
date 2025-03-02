<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Selectfrm
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.btnSelect = New System.Windows.Forms.Button
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.cmbSearch = New System.Windows.Forms.ComboBox
        Me.pnlRadios = New System.Windows.Forms.Panel
        Me.rdostaff = New System.Windows.Forms.RadioButton
        Me.rbPDCRcvd = New System.Windows.Forms.RadioButton
        Me.rbPDCIssued = New System.Windows.Forms.RadioButton
        Me.rbPurchase = New System.Windows.Forms.RadioButton
        Me.rbCustSupp = New System.Windows.Forms.RadioButton
        Me.rbAllAc = New System.Windows.Forms.RadioButton
        Me.rbSales = New System.Windows.Forms.RadioButton
        Me.rbSupplier = New System.Windows.Forms.RadioButton
        Me.rbExpence = New System.Windows.Forms.RadioButton
        Me.rbCashBank = New System.Windows.Forms.RadioButton
        Me.rbCustomer = New System.Windows.Forms.RadioButton
        Me.dvData = New System.Windows.Forms.DataGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel3.SuspendLayout()
        Me.pnlRadios.SuspendLayout()
        CType(Me.dvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.btnSelect)
        Me.Panel3.Controls.Add(Me.btnRefresh)
        Me.Panel3.Controls.Add(Me.btnSearch)
        Me.Panel3.Controls.Add(Me.btnExit)
        Me.Panel3.Location = New System.Drawing.Point(417, 427)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(315, 48)
        Me.Panel3.TabIndex = 62
        '
        'btnSelect
        '
        Me.btnSelect.BackColor = System.Drawing.Color.SteelBlue
        Me.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelect.ForeColor = System.Drawing.Color.White
        Me.btnSelect.Location = New System.Drawing.Point(136, 3)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(87, 35)
        Me.btnSelect.TabIndex = 78
        Me.btnSelect.Text = "&Select"
        Me.btnSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSelect.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.SteelBlue
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(47, 3)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(87, 35)
        Me.btnRefresh.TabIndex = 77
        Me.btnRefresh.Text = "&Refresh"
        Me.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Image = Global.SMSMP.My.Resources.Resources.searchbtn
        Me.btnSearch.Location = New System.Drawing.Point(3, 3)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(38, 32)
        Me.btnSearch.TabIndex = 76
        Me.btnSearch.Text = "Search"
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSearch.UseVisualStyleBackColor = True
        Me.btnSearch.Visible = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(224, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(87, 35)
        Me.btnExit.TabIndex = 75
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.Location = New System.Drawing.Point(589, 407)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 84
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.BackColor = System.Drawing.Color.White
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(211, 7)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(522, 20)
        Me.txtSearch.TabIndex = 0
        '
        'cmbSearch
        '
        Me.cmbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSearch.FormattingEnabled = True
        Me.cmbSearch.Location = New System.Drawing.Point(46, 6)
        Me.cmbSearch.Name = "cmbSearch"
        Me.cmbSearch.Size = New System.Drawing.Size(159, 21)
        Me.cmbSearch.TabIndex = 81
        '
        'pnlRadios
        '
        Me.pnlRadios.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlRadios.Controls.Add(Me.rdostaff)
        Me.pnlRadios.Controls.Add(Me.rbPDCRcvd)
        Me.pnlRadios.Controls.Add(Me.rbPDCIssued)
        Me.pnlRadios.Controls.Add(Me.rbPurchase)
        Me.pnlRadios.Controls.Add(Me.rbCustSupp)
        Me.pnlRadios.Controls.Add(Me.rbAllAc)
        Me.pnlRadios.Controls.Add(Me.rbSales)
        Me.pnlRadios.Controls.Add(Me.rbSupplier)
        Me.pnlRadios.Controls.Add(Me.rbExpence)
        Me.pnlRadios.Controls.Add(Me.rbCashBank)
        Me.pnlRadios.Controls.Add(Me.rbCustomer)
        Me.pnlRadios.Location = New System.Drawing.Point(5, 415)
        Me.pnlRadios.Name = "pnlRadios"
        Me.pnlRadios.Size = New System.Drawing.Size(406, 61)
        Me.pnlRadios.TabIndex = 83
        '
        'rdostaff
        '
        Me.rdostaff.AutoSize = True
        Me.rdostaff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdostaff.Location = New System.Drawing.Point(305, 3)
        Me.rdostaff.Name = "rdostaff"
        Me.rdostaff.Size = New System.Drawing.Size(47, 17)
        Me.rdostaff.TabIndex = 10
        Me.rdostaff.TabStop = True
        Me.rdostaff.Text = "Staff"
        Me.rdostaff.UseVisualStyleBackColor = True
        '
        'rbPDCRcvd
        '
        Me.rbPDCRcvd.AutoSize = True
        Me.rbPDCRcvd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbPDCRcvd.Location = New System.Drawing.Point(101, 22)
        Me.rbPDCRcvd.Name = "rbPDCRcvd"
        Me.rbPDCRcvd.Size = New System.Drawing.Size(64, 17)
        Me.rbPDCRcvd.TabIndex = 9
        Me.rbPDCRcvd.TabStop = True
        Me.rbPDCRcvd.Text = "PDC (R)"
        Me.rbPDCRcvd.UseVisualStyleBackColor = True
        '
        'rbPDCIssued
        '
        Me.rbPDCIssued.AutoSize = True
        Me.rbPDCIssued.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbPDCIssued.Location = New System.Drawing.Point(185, 22)
        Me.rbPDCIssued.Name = "rbPDCIssued"
        Me.rbPDCIssued.Size = New System.Drawing.Size(59, 17)
        Me.rbPDCIssued.TabIndex = 8
        Me.rbPDCIssued.TabStop = True
        Me.rbPDCIssued.Text = "PDC (I)"
        Me.rbPDCIssued.UseVisualStyleBackColor = True
        '
        'rbPurchase
        '
        Me.rbPurchase.AutoSize = True
        Me.rbPurchase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbPurchase.Location = New System.Drawing.Point(185, 41)
        Me.rbPurchase.Name = "rbPurchase"
        Me.rbPurchase.Size = New System.Drawing.Size(121, 17)
        Me.rbPurchase.TabIndex = 7
        Me.rbPurchase.TabStop = True
        Me.rbPurchase.Text = "Purchase (Expence)"
        Me.rbPurchase.UseVisualStyleBackColor = True
        Me.rbPurchase.Visible = False
        '
        'rbCustSupp
        '
        Me.rbCustSupp.AutoSize = True
        Me.rbCustSupp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCustSupp.Location = New System.Drawing.Point(185, 2)
        Me.rbCustSupp.Name = "rbCustSupp"
        Me.rbCustSupp.Size = New System.Drawing.Size(114, 17)
        Me.rbCustSupp.TabIndex = 6
        Me.rbCustSupp.TabStop = True
        Me.rbCustSupp.Text = "Cutomer && Supplier"
        Me.rbCustSupp.UseVisualStyleBackColor = True
        '
        'rbAllAc
        '
        Me.rbAllAc.AutoSize = True
        Me.rbAllAc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbAllAc.Location = New System.Drawing.Point(13, 41)
        Me.rbAllAc.Name = "rbAllAc"
        Me.rbAllAc.Size = New System.Drawing.Size(58, 17)
        Me.rbAllAc.TabIndex = 5
        Me.rbAllAc.TabStop = True
        Me.rbAllAc.Text = "All A/C"
        Me.rbAllAc.UseVisualStyleBackColor = True
        '
        'rbSales
        '
        Me.rbSales.AutoSize = True
        Me.rbSales.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSales.Location = New System.Drawing.Point(13, 77)
        Me.rbSales.Name = "rbSales"
        Me.rbSales.Size = New System.Drawing.Size(108, 19)
        Me.rbSales.TabIndex = 4
        Me.rbSales.TabStop = True
        Me.rbSales.Text = "Sales (Income)"
        Me.rbSales.UseVisualStyleBackColor = True
        Me.rbSales.Visible = False
        '
        'rbSupplier
        '
        Me.rbSupplier.AutoSize = True
        Me.rbSupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSupplier.Location = New System.Drawing.Point(101, 1)
        Me.rbSupplier.Name = "rbSupplier"
        Me.rbSupplier.Size = New System.Drawing.Size(63, 17)
        Me.rbSupplier.TabIndex = 3
        Me.rbSupplier.TabStop = True
        Me.rbSupplier.Text = "Supplier"
        Me.rbSupplier.UseVisualStyleBackColor = True
        '
        'rbExpence
        '
        Me.rbExpence.AutoSize = True
        Me.rbExpence.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbExpence.Location = New System.Drawing.Point(101, 40)
        Me.rbExpence.Name = "rbExpence"
        Me.rbExpence.Size = New System.Drawing.Size(81, 17)
        Me.rbExpence.TabIndex = 2
        Me.rbExpence.TabStop = True
        Me.rbExpence.Text = "&9.Expences"
        Me.rbExpence.UseVisualStyleBackColor = True
        '
        'rbCashBank
        '
        Me.rbCashBank.AutoSize = True
        Me.rbCashBank.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCashBank.Location = New System.Drawing.Point(13, 22)
        Me.rbCashBank.Name = "rbCashBank"
        Me.rbCashBank.Size = New System.Drawing.Size(86, 17)
        Me.rbCashBank.TabIndex = 1
        Me.rbCashBank.TabStop = True
        Me.rbCashBank.Text = "Cash && Bank"
        Me.rbCashBank.UseVisualStyleBackColor = True
        '
        'rbCustomer
        '
        Me.rbCustomer.AutoSize = True
        Me.rbCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCustomer.Location = New System.Drawing.Point(13, 1)
        Me.rbCustomer.Name = "rbCustomer"
        Me.rbCustomer.Size = New System.Drawing.Size(69, 17)
        Me.rbCustomer.TabIndex = 0
        Me.rbCustomer.TabStop = True
        Me.rbCustomer.Text = "Customer"
        Me.rbCustomer.UseVisualStyleBackColor = True
        '
        'dvData
        '
        Me.dvData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dvData.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dvData.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dvData.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dvData.DefaultCellStyle = DataGridViewCellStyle2
        Me.dvData.Location = New System.Drawing.Point(0, 34)
        Me.dvData.Name = "dvData"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dvData.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dvData.Size = New System.Drawing.Size(736, 370)
        Me.dvData.TabIndex = 86
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.txtSearch)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.cmbSearch)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(736, 30)
        Me.Panel1.TabIndex = 87
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.search
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(1, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(39, 32)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Selectfrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(736, 478)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.dvData)
        Me.Controls.Add(Me.pnlRadios)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Selectfrm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select .."
        Me.Panel3.ResumeLayout(False)
        Me.pnlRadios.ResumeLayout(False)
        Me.pnlRadios.PerformLayout()
        CType(Me.dvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents cmbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents pnlRadios As System.Windows.Forms.Panel
    Friend WithEvents rbPDCRcvd As System.Windows.Forms.RadioButton
    Friend WithEvents rbPDCIssued As System.Windows.Forms.RadioButton
    Friend WithEvents rbPurchase As System.Windows.Forms.RadioButton
    Friend WithEvents rbCustSupp As System.Windows.Forms.RadioButton
    Friend WithEvents rbAllAc As System.Windows.Forms.RadioButton
    Friend WithEvents rbSales As System.Windows.Forms.RadioButton
    Friend WithEvents rbSupplier As System.Windows.Forms.RadioButton
    Friend WithEvents rbExpence As System.Windows.Forms.RadioButton
    Friend WithEvents rbCashBank As System.Windows.Forms.RadioButton
    Friend WithEvents rbCustomer As System.Windows.Forms.RadioButton
    Friend WithEvents dvData As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents rdostaff As System.Windows.Forms.RadioButton
End Class
