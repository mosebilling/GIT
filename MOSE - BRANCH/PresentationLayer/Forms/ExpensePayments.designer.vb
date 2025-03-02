<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExpensePayments
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
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.txtprintprefix = New System.Windows.Forms.TextBox
        Me.chkFormat = New System.Windows.Forms.CheckBox
        Me.numPrintVchr = New System.Windows.Forms.TextBox
        Me.btndelete = New System.Windows.Forms.Button
        Me.btnexit = New System.Windows.Forms.Button
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btnupdate = New System.Windows.Forms.Button
        Me.btnModify = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.lbltaxable = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.lblvat = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.lblaccountbalance = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.cmbdeliveredBy = New System.Windows.Forms.ComboBox
        Me.lblpayroll = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lbljobname = New System.Windows.Forms.Label
        Me.txtjobcode = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.btnremoveexpense = New System.Windows.Forms.Button
        Me.btnaddexpense = New System.Windows.Forms.Button
        Me.txtfcrt = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.cmbfc = New System.Windows.Forms.ComboBox
        Me.txtPreFix = New System.Windows.Forms.TextBox
        Me.lbldiff = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblcredit = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.lblTlDebit = New System.Windows.Forms.Label
        Me.grdpayment = New System.Windows.Forms.DataGridView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtBank = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.dtpchequedate = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtCr = New System.Windows.Forms.TextBox
        Me.txtchequeno = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.btnrem = New System.Windows.Forms.Button
        Me.btnadd = New System.Windows.Forms.Button
        Me.dtpdate = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me._lblCap_3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.numVchrNo = New System.Windows.Forms.TextBox
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.cmbVoucherTp = New System.Windows.Forms.ComboBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.btnedit = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.dtpto = New System.Windows.Forms.DateTimePicker
        Me.btnTo = New System.Windows.Forms.Button
        Me.dtpstart = New System.Windows.Forms.DateTimePicker
        Me.btnFP = New System.Windows.Forms.Button
        Me.btnload = New System.Windows.Forms.Button
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.dvData = New System.Windows.Forms.DataGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblcap = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.plsrch = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label14 = New System.Windows.Forms.Label
        Me.picCloseProd = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.grdSrch = New System.Windows.Forms.DataGridView
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.chkcal = New System.Windows.Forms.CheckBox
        Me.Panel6.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.grdpayment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plsrch.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.picCloseProd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSrch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Transparent
        Me.Panel6.Controls.Add(Me.txtprintprefix)
        Me.Panel6.Controls.Add(Me.chkFormat)
        Me.Panel6.Controls.Add(Me.numPrintVchr)
        Me.Panel6.Controls.Add(Me.btndelete)
        Me.Panel6.Controls.Add(Me.btnexit)
        Me.Panel6.Controls.Add(Me.btnPreview)
        Me.Panel6.Controls.Add(Me.btnupdate)
        Me.Panel6.Controls.Add(Me.btnModify)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(0, 538)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1095, 38)
        Me.Panel6.TabIndex = 345445
        '
        'txtprintprefix
        '
        Me.txtprintprefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtprintprefix.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprintprefix.Location = New System.Drawing.Point(9, 7)
        Me.txtprintprefix.Name = "txtprintprefix"
        Me.txtprintprefix.Size = New System.Drawing.Size(51, 21)
        Me.txtprintprefix.TabIndex = 345459
        '
        'chkFormat
        '
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(232, 10)
        Me.chkFormat.Name = "chkFormat"
        Me.chkFormat.Size = New System.Drawing.Size(65, 19)
        Me.chkFormat.TabIndex = 119
        Me.chkFormat.Text = "Format"
        Me.chkFormat.UseVisualStyleBackColor = False
        '
        'numPrintVchr
        '
        Me.numPrintVchr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPrintVchr.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numPrintVchr.Location = New System.Drawing.Point(63, 8)
        Me.numPrintVchr.Name = "numPrintVchr"
        Me.numPrintVchr.Size = New System.Drawing.Size(75, 21)
        Me.numPrintVchr.TabIndex = 137
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btndelete.BackColor = System.Drawing.Color.SteelBlue
        Me.btndelete.Enabled = False
        Me.btndelete.FlatAppearance.BorderSize = 0
        Me.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.ForeColor = System.Drawing.Color.White
        Me.btndelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btndelete.Location = New System.Drawing.Point(902, 3)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(93, 31)
        Me.btndelete.TabIndex = 80
        Me.btndelete.Text = "Delete"
        Me.btndelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndelete.UseVisualStyleBackColor = False
        '
        'btnexit
        '
        Me.btnexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.FlatAppearance.BorderSize = 0
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.Location = New System.Drawing.Point(999, 2)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(93, 33)
        Me.btnexit.TabIndex = 345439
        Me.btnexit.Text = "E&xit"
        Me.btnexit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'btnPreview
        '
        Me.btnPreview.BackColor = System.Drawing.Color.SteelBlue
        Me.btnPreview.FlatAppearance.BorderSize = 0
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.White
        Me.btnPreview.Location = New System.Drawing.Point(144, 3)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(87, 31)
        Me.btnPreview.TabIndex = 1
        Me.btnPreview.TabStop = False
        Me.btnPreview.Text = "Pre&view"
        Me.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'btnupdate
        '
        Me.btnupdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnupdate.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupdate.FlatAppearance.BorderSize = 0
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnupdate.Location = New System.Drawing.Point(807, 3)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(93, 31)
        Me.btnupdate.TabIndex = 79
        Me.btnupdate.Text = "&Update"
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'btnModify
        '
        Me.btnModify.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnModify.BackColor = System.Drawing.Color.SteelBlue
        Me.btnModify.FlatAppearance.BorderSize = 0
        Me.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnModify.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModify.ForeColor = System.Drawing.Color.White
        Me.btnModify.Location = New System.Drawing.Point(712, 3)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(93, 31)
        Me.btnModify.TabIndex = 69
        Me.btnModify.Text = "&Clear"
        Me.btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnModify.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(3, 45)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1089, 487)
        Me.TabControl1.TabIndex = 345449
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.chkcal)
        Me.TabPage1.Controls.Add(Me.lbltaxable)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.lblvat)
        Me.TabPage1.Controls.Add(Me.Label17)
        Me.TabPage1.Controls.Add(Me.lblaccountbalance)
        Me.TabPage1.Controls.Add(Me.Label28)
        Me.TabPage1.Controls.Add(Me.cmbdeliveredBy)
        Me.TabPage1.Controls.Add(Me.lblpayroll)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.lbljobname)
        Me.TabPage1.Controls.Add(Me.txtjobcode)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Panel5)
        Me.TabPage1.Controls.Add(Me.txtfcrt)
        Me.TabPage1.Controls.Add(Me.Label15)
        Me.TabPage1.Controls.Add(Me.cmbfc)
        Me.TabPage1.Controls.Add(Me.txtPreFix)
        Me.TabPage1.Controls.Add(Me.lbldiff)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.lblcredit)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.Label13)
        Me.TabPage1.Controls.Add(Me.lblTlDebit)
        Me.TabPage1.Controls.Add(Me.grdpayment)
        Me.TabPage1.Controls.Add(Me.Panel2)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Panel4)
        Me.TabPage1.Controls.Add(Me.dtpdate)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me._lblCap_3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.numVchrNo)
        Me.TabPage1.Controls.Add(Me.grdVoucher)
        Me.TabPage1.Controls.Add(Me.cmbVoucherTp)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1081, 461)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Entry"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'lbltaxable
        '
        Me.lbltaxable.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbltaxable.BackColor = System.Drawing.Color.Transparent
        Me.lbltaxable.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbltaxable.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltaxable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbltaxable.Location = New System.Drawing.Point(727, 422)
        Me.lbltaxable.Name = "lbltaxable"
        Me.lbltaxable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbltaxable.Size = New System.Drawing.Size(128, 23)
        Me.lbltaxable.TabIndex = 345500
        Me.lbltaxable.Text = "0.000"
        Me.lbltaxable.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(656, 423)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(66, 18)
        Me.Label18.TabIndex = 345499
        Me.Label18.Text = "Taxable"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblvat
        '
        Me.lblvat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblvat.BackColor = System.Drawing.Color.Transparent
        Me.lblvat.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblvat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblvat.Location = New System.Drawing.Point(947, 379)
        Me.lblvat.Name = "lblvat"
        Me.lblvat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblvat.Size = New System.Drawing.Size(128, 23)
        Me.lblvat.TabIndex = 345498
        Me.lblvat.Text = "0.000"
        Me.lblvat.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(874, 380)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(66, 18)
        Me.Label17.TabIndex = 345497
        Me.Label17.Text = "Dr. VAT"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblaccountbalance
        '
        Me.lblaccountbalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblaccountbalance.BackColor = System.Drawing.Color.Transparent
        Me.lblaccountbalance.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblaccountbalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblaccountbalance.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblaccountbalance.Location = New System.Drawing.Point(798, 118)
        Me.lblaccountbalance.Name = "lblaccountbalance"
        Me.lblaccountbalance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblaccountbalance.Size = New System.Drawing.Size(275, 23)
        Me.lblaccountbalance.TabIndex = 345496
        Me.lblaccountbalance.Text = "Balance : 0.00"
        Me.lblaccountbalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(736, 10)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(63, 13)
        Me.Label28.TabIndex = 345495
        Me.Label28.Text = "Expense By"
        '
        'cmbdeliveredBy
        '
        Me.cmbdeliveredBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbdeliveredBy.FormattingEnabled = True
        Me.cmbdeliveredBy.Location = New System.Drawing.Point(802, 6)
        Me.cmbdeliveredBy.Name = "cmbdeliveredBy"
        Me.cmbdeliveredBy.Size = New System.Drawing.Size(109, 21)
        Me.cmbdeliveredBy.TabIndex = 345494
        '
        'lblpayroll
        '
        Me.lblpayroll.AutoSize = True
        Me.lblpayroll.BackColor = System.Drawing.Color.Transparent
        Me.lblpayroll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpayroll.ForeColor = System.Drawing.Color.Black
        Me.lblpayroll.Location = New System.Drawing.Point(920, 9)
        Me.lblpayroll.Name = "lblpayroll"
        Me.lblpayroll.Size = New System.Drawing.Size(50, 16)
        Me.lblpayroll.TabIndex = 345474
        Me.lblpayroll.Text = "Payroll"
        Me.lblpayroll.Visible = False
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(604, 156)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 345473
        Me.Label5.Text = "Job Code"
        '
        'lbljobname
        '
        Me.lbljobname.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbljobname.BackColor = System.Drawing.Color.Transparent
        Me.lbljobname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbljobname.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbljobname.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbljobname.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbljobname.Location = New System.Drawing.Point(756, 153)
        Me.lbljobname.Name = "lbljobname"
        Me.lbljobname.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbljobname.Size = New System.Drawing.Size(320, 21)
        Me.lbljobname.TabIndex = 345472
        Me.lbljobname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtjobcode
        '
        Me.txtjobcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtjobcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtjobcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtjobcode.Location = New System.Drawing.Point(659, 153)
        Me.txtjobcode.Name = "txtjobcode"
        Me.txtjobcode.Size = New System.Drawing.Size(95, 21)
        Me.txtjobcode.TabIndex = 345471
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(8, 157)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(107, 16)
        Me.Label3.TabIndex = 345467
        Me.Label3.Text = "Expense Entry"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel5.Controls.Add(Me.btnremoveexpense)
        Me.Panel5.Controls.Add(Me.btnaddexpense)
        Me.Panel5.Location = New System.Drawing.Point(7, 423)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(126, 32)
        Me.Panel5.TabIndex = 345466
        '
        'btnremoveexpense
        '
        Me.btnremoveexpense.BackColor = System.Drawing.Color.SteelBlue
        Me.btnremoveexpense.FlatAppearance.BorderSize = 0
        Me.btnremoveexpense.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnremoveexpense.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnremoveexpense.ForeColor = System.Drawing.Color.White
        Me.btnremoveexpense.Location = New System.Drawing.Point(64, 3)
        Me.btnremoveexpense.Name = "btnremoveexpense"
        Me.btnremoveexpense.Size = New System.Drawing.Size(60, 27)
        Me.btnremoveexpense.TabIndex = 9
        Me.btnremoveexpense.Text = "Rem"
        Me.btnremoveexpense.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnremoveexpense.UseVisualStyleBackColor = False
        '
        'btnaddexpense
        '
        Me.btnaddexpense.BackColor = System.Drawing.Color.SteelBlue
        Me.btnaddexpense.FlatAppearance.BorderSize = 0
        Me.btnaddexpense.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnaddexpense.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnaddexpense.ForeColor = System.Drawing.Color.White
        Me.btnaddexpense.Location = New System.Drawing.Point(2, 3)
        Me.btnaddexpense.Name = "btnaddexpense"
        Me.btnaddexpense.Size = New System.Drawing.Size(60, 27)
        Me.btnaddexpense.TabIndex = 8
        Me.btnaddexpense.Text = "Add"
        Me.btnaddexpense.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnaddexpense.UseVisualStyleBackColor = False
        '
        'txtfcrt
        '
        Me.txtfcrt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtfcrt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfcrt.Location = New System.Drawing.Point(634, 6)
        Me.txtfcrt.Name = "txtfcrt"
        Me.txtfcrt.Size = New System.Drawing.Size(95, 21)
        Me.txtfcrt.TabIndex = 4
        Me.txtfcrt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(550, 9)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(20, 13)
        Me.Label15.TabIndex = 345464
        Me.Label15.Text = "FC"
        '
        'cmbfc
        '
        Me.cmbfc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbfc.FormattingEnabled = True
        Me.cmbfc.Location = New System.Drawing.Point(576, 6)
        Me.cmbfc.Name = "cmbfc"
        Me.cmbfc.Size = New System.Drawing.Size(56, 21)
        Me.cmbfc.TabIndex = 3
        '
        'txtPreFix
        '
        Me.txtPreFix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPreFix.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPreFix.Location = New System.Drawing.Point(290, 7)
        Me.txtPreFix.Name = "txtPreFix"
        Me.txtPreFix.Size = New System.Drawing.Size(51, 21)
        Me.txtPreFix.TabIndex = 345458
        '
        'lbldiff
        '
        Me.lbldiff.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbldiff.BackColor = System.Drawing.Color.Transparent
        Me.lbldiff.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbldiff.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldiff.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbldiff.Location = New System.Drawing.Point(947, 427)
        Me.lbldiff.Name = "lbldiff"
        Me.lbldiff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbldiff.Size = New System.Drawing.Size(128, 23)
        Me.lbldiff.TabIndex = 345457
        Me.lbldiff.Text = "0.000"
        Me.lbldiff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(874, 429)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(39, 18)
        Me.Label10.TabIndex = 345456
        Me.Label10.Text = "Diff."
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblcredit
        '
        Me.lblcredit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblcredit.BackColor = System.Drawing.Color.Transparent
        Me.lblcredit.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblcredit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcredit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblcredit.Location = New System.Drawing.Point(947, 402)
        Me.lblcredit.Name = "lblcredit"
        Me.lblcredit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblcredit.Size = New System.Drawing.Size(128, 23)
        Me.lblcredit.TabIndex = 345455
        Me.lblcredit.Text = "0.000"
        Me.lblcredit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(874, 403)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(31, 18)
        Me.Label11.TabIndex = 345454
        Me.Label11.Text = "Cr."
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(874, 358)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(31, 18)
        Me.Label12.TabIndex = 345453
        Me.Label12.Text = "Dr."
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(783, 358)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(68, 23)
        Me.Label13.TabIndex = 345452
        Me.Label13.Text = "TOTAL"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTlDebit
        '
        Me.lblTlDebit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTlDebit.BackColor = System.Drawing.Color.Transparent
        Me.lblTlDebit.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTlDebit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTlDebit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTlDebit.Location = New System.Drawing.Point(947, 356)
        Me.lblTlDebit.Name = "lblTlDebit"
        Me.lblTlDebit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTlDebit.Size = New System.Drawing.Size(128, 23)
        Me.lblTlDebit.TabIndex = 345451
        Me.lblTlDebit.Text = "0.000"
        Me.lblTlDebit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdpayment
        '
        Me.grdpayment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdpayment.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.grdpayment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdpayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdpayment.Location = New System.Drawing.Point(9, 33)
        Me.grdpayment.Name = "grdpayment"
        Me.grdpayment.Size = New System.Drawing.Size(1066, 82)
        Me.grdpayment.TabIndex = 345446
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.txtBank)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.dtpchequedate)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.txtCr)
        Me.Panel2.Controls.Add(Me.txtchequeno)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Location = New System.Drawing.Point(798, 62)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(57, 60)
        Me.Panel2.TabIndex = 345445
        Me.Panel2.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(407, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 345445
        Me.Label9.Text = "Bank Code"
        '
        'txtBank
        '
        Me.txtBank.BackColor = System.Drawing.Color.White
        Me.txtBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBank.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBank.Location = New System.Drawing.Point(467, 5)
        Me.txtBank.Name = "txtBank"
        Me.txtBank.Size = New System.Drawing.Size(227, 21)
        Me.txtBank.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(4, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 13)
        Me.Label8.TabIndex = 345443
        Me.Label8.Text = "Credit To"
        '
        'dtpchequedate
        '
        Me.dtpchequedate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpchequedate.Location = New System.Drawing.Point(286, 32)
        Me.dtpchequedate.Name = "dtpchequedate"
        Me.dtpchequedate.Size = New System.Drawing.Size(95, 20)
        Me.dtpchequedate.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(215, 35)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 13)
        Me.Label7.TabIndex = 345441
        Me.Label7.Text = "Cheque Date"
        '
        'txtCr
        '
        Me.txtCr.BackColor = System.Drawing.Color.White
        Me.txtCr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCr.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCr.Location = New System.Drawing.Point(93, 5)
        Me.txtCr.Name = "txtCr"
        Me.txtCr.Size = New System.Drawing.Size(288, 21)
        Me.txtCr.TabIndex = 5
        '
        'txtchequeno
        '
        Me.txtchequeno.BackColor = System.Drawing.Color.White
        Me.txtchequeno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtchequeno.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtchequeno.Location = New System.Drawing.Point(93, 32)
        Me.txtchequeno.Name = "txtchequeno"
        Me.txtchequeno.Size = New System.Drawing.Size(119, 21)
        Me.txtchequeno.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(4, 35)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 13)
        Me.Label6.TabIndex = 345439
        Me.Label6.Text = "Cheque No"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 13)
        Me.Label4.TabIndex = 345433
        Me.Label4.Text = "Voucher Type"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnrem)
        Me.Panel4.Controls.Add(Me.btnadd)
        Me.Panel4.Location = New System.Drawing.Point(9, 118)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(126, 32)
        Me.Panel4.TabIndex = 8
        '
        'btnrem
        '
        Me.btnrem.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrem.FlatAppearance.BorderSize = 0
        Me.btnrem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrem.ForeColor = System.Drawing.Color.White
        Me.btnrem.Location = New System.Drawing.Point(64, 3)
        Me.btnrem.Name = "btnrem"
        Me.btnrem.Size = New System.Drawing.Size(60, 27)
        Me.btnrem.TabIndex = 9
        Me.btnrem.Text = "Rem"
        Me.btnrem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrem.UseVisualStyleBackColor = False
        '
        'btnadd
        '
        Me.btnadd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnadd.FlatAppearance.BorderSize = 0
        Me.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.ForeColor = System.Drawing.Color.White
        Me.btnadd.Location = New System.Drawing.Point(2, 3)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(60, 27)
        Me.btnadd.TabIndex = 8
        Me.btnadd.Text = "Add"
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'dtpdate
        '
        Me.dtpdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpdate.Location = New System.Drawing.Point(447, 7)
        Me.dtpdate.Name = "dtpdate"
        Me.dtpdate.Size = New System.Drawing.Size(95, 20)
        Me.dtpdate.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(414, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Date"
        '
        '_lblCap_3
        '
        Me._lblCap_3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._lblCap_3.AutoSize = True
        Me._lblCap_3.BackColor = System.Drawing.Color.Transparent
        Me._lblCap_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblCap_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblCap_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblCap_3.Location = New System.Drawing.Point(1213, 452)
        Me._lblCap_3.Name = "_lblCap_3"
        Me._lblCap_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblCap_3.Size = New System.Drawing.Size(46, 18)
        Me._lblCap_3.TabIndex = 345442
        Me._lblCap_3.Text = "Total"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(225, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 345427
        Me.Label2.Text = "Payment No"
        '
        'numVchrNo
        '
        Me.numVchrNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numVchrNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numVchrNo.Location = New System.Drawing.Point(343, 7)
        Me.numVchrNo.Name = "numVchrNo"
        Me.numVchrNo.Size = New System.Drawing.Size(65, 21)
        Me.numVchrNo.TabIndex = 1
        '
        'grdVoucher
        '
        Me.grdVoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdVoucher.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.grdVoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdVoucher.Location = New System.Drawing.Point(6, 177)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(1069, 176)
        Me.grdVoucher.TabIndex = 345438
        '
        'cmbVoucherTp
        '
        Me.cmbVoucherTp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVoucherTp.FormattingEnabled = True
        Me.cmbVoucherTp.Location = New System.Drawing.Point(95, 6)
        Me.cmbVoucherTp.Name = "cmbVoucherTp"
        Me.cmbVoucherTp.Size = New System.Drawing.Size(126, 21)
        Me.cmbVoucherTp.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btnedit)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Controls.Add(Me.btnload)
        Me.TabPage2.Controls.Add(Me.chkSearch)
        Me.TabPage2.Controls.Add(Me.txtSeq)
        Me.TabPage2.Controls.Add(Me.cmbOrder)
        Me.TabPage2.Controls.Add(Me.dvData)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(959, 461)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Listing"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnedit
        '
        Me.btnedit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnedit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnedit.FlatAppearance.BorderSize = 0
        Me.btnedit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnedit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnedit.ForeColor = System.Drawing.Color.White
        Me.btnedit.Location = New System.Drawing.Point(898, 415)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(55, 30)
        Me.btnedit.TabIndex = 345445
        Me.btnedit.Text = "Edit"
        Me.btnedit.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dtpto)
        Me.GroupBox1.Controls.Add(Me.btnTo)
        Me.GroupBox1.Controls.Add(Me.dtpstart)
        Me.GroupBox1.Controls.Add(Me.btnFP)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 381)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(209, 39)
        Me.GroupBox1.TabIndex = 345444
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Parameter"
        '
        'dtpto
        '
        Me.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpto.Location = New System.Drawing.Point(107, 15)
        Me.dtpto.Name = "dtpto"
        Me.dtpto.Size = New System.Drawing.Size(95, 20)
        Me.dtpto.TabIndex = 345396
        Me.dtpto.TabStop = False
        '
        'btnTo
        '
        Me.btnTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTo.Location = New System.Drawing.Point(244, 36)
        Me.btnTo.Name = "btnTo"
        Me.btnTo.Size = New System.Drawing.Size(30, 23)
        Me.btnTo.TabIndex = 345365
        Me.btnTo.Text = "TD"
        Me.btnTo.UseVisualStyleBackColor = True
        Me.btnTo.Visible = False
        '
        'dtpstart
        '
        Me.dtpstart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpstart.Location = New System.Drawing.Point(6, 15)
        Me.dtpstart.Name = "dtpstart"
        Me.dtpstart.Size = New System.Drawing.Size(95, 20)
        Me.dtpstart.TabIndex = 345395
        Me.dtpstart.TabStop = False
        '
        'btnFP
        '
        Me.btnFP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFP.Location = New System.Drawing.Point(244, 12)
        Me.btnFP.Name = "btnFP"
        Me.btnFP.Size = New System.Drawing.Size(30, 23)
        Me.btnFP.TabIndex = 345364
        Me.btnFP.Text = "FP"
        Me.btnFP.UseVisualStyleBackColor = True
        Me.btnFP.Visible = False
        '
        'btnload
        '
        Me.btnload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.FlatAppearance.BorderSize = 0
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.Location = New System.Drawing.Point(560, 415)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(63, 30)
        Me.btnload.TabIndex = 345443
        Me.btnload.Text = "Load"
        Me.btnload.UseVisualStyleBackColor = False
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.Checked = True
        Me.chkSearch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(414, 425)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345442
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
        Me.txtSeq.Location = New System.Drawing.Point(173, 426)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(235, 20)
        Me.txtSeq.TabIndex = 345441
        '
        'cmbOrder
        '
        Me.cmbOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOrder.Location = New System.Drawing.Point(3, 425)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOrder.Size = New System.Drawing.Size(166, 22)
        Me.cmbOrder.TabIndex = 345440
        Me.cmbOrder.TabStop = False
        '
        'dvData
        '
        Me.dvData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dvData.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dvData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dvData.Location = New System.Drawing.Point(3, 6)
        Me.dvData.Name = "dvData"
        Me.dvData.Size = New System.Drawing.Size(950, 369)
        Me.dvData.TabIndex = 345439
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.lblcap)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1095, 33)
        Me.Panel1.TabIndex = 345452
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(37, 23)
        Me.PictureBox1.TabIndex = 345460
        Me.PictureBox1.TabStop = False
        '
        'lblcap
        '
        Me.lblcap.AutoSize = True
        Me.lblcap.BackColor = System.Drawing.Color.Transparent
        Me.lblcap.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcap.ForeColor = System.Drawing.Color.Black
        Me.lblcap.Location = New System.Drawing.Point(41, 6)
        Me.lblcap.Name = "lblcap"
        Me.lblcap.Size = New System.Drawing.Size(174, 18)
        Me.lblcap.TabIndex = 6
        Me.lblcap.Text = "Payment Voucher [Other]"
        '
        'Timer1
        '
        '
        'plsrch
        '
        Me.plsrch.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.plsrch.Controls.Add(Me.Panel3)
        Me.plsrch.Controls.Add(Me.grdSrch)
        Me.plsrch.Location = New System.Drawing.Point(235, 265)
        Me.plsrch.Name = "plsrch"
        Me.plsrch.Size = New System.Drawing.Size(477, 264)
        Me.plsrch.TabIndex = 345455
        Me.plsrch.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.BackgroundImage = Global.SMSMP.My.Resources.Resources.top
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Controls.Add(Me.picCloseProd)
        Me.Panel3.Controls.Add(Me.PictureBox2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(477, 32)
        Me.Panel3.TabIndex = 345445
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.DimGray
        Me.Label14.Location = New System.Drawing.Point(31, 9)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(111, 16)
        Me.Label14.TabIndex = 27
        Me.Label14.Text = "Select Account"
        '
        'picCloseProd
        '
        Me.picCloseProd.BackColor = System.Drawing.Color.Transparent
        Me.picCloseProd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picCloseProd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picCloseProd.Image = Global.SMSMP.My.Resources.Resources.CloseButton
        Me.picCloseProd.Location = New System.Drawing.Point(460, 9)
        Me.picCloseProd.Name = "picCloseProd"
        Me.picCloseProd.Size = New System.Drawing.Size(12, 12)
        Me.picCloseProd.TabIndex = 345356
        Me.picCloseProd.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.search
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(4, 5)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(27, 23)
        Me.PictureBox2.TabIndex = 27
        Me.PictureBox2.TabStop = False
        '
        'grdSrch
        '
        Me.grdSrch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSrch.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.grdSrch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdSrch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSrch.Location = New System.Drawing.Point(0, 32)
        Me.grdSrch.Name = "grdSrch"
        Me.grdSrch.Size = New System.Drawing.Size(469, 232)
        Me.grdSrch.TabIndex = 3
        '
        'Timer2
        '
        '
        'chkcal
        '
        Me.chkcal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkcal.AutoSize = True
        Me.chkcal.Checked = True
        Me.chkcal.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkcal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkcal.Location = New System.Drawing.Point(659, 132)
        Me.chkcal.Name = "chkcal"
        Me.chkcal.Size = New System.Drawing.Size(144, 17)
        Me.chkcal.TabIndex = 345501
        Me.chkcal.Text = "Calculate Tax From Price"
        Me.chkcal.UseVisualStyleBackColor = True
        '
        'ExpensePayments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1095, 576)
        Me.ControlBox = False
        Me.Controls.Add(Me.plsrch)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel6)
        Me.Name = "ExpensePayments"
        Me.Text = "Supplier Payments"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        CType(Me.grdpayment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plsrch.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.picCloseProd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSrch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chkFormat As System.Windows.Forms.CheckBox
    Friend WithEvents numPrintVchr As System.Windows.Forms.TextBox
    Friend WithEvents btndelete As System.Windows.Forms.Button
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents btnModify As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents dvData As System.Windows.Forms.DataGridView
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpto As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnTo As System.Windows.Forms.Button
    Friend WithEvents dtpstart As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnFP As System.Windows.Forms.Button
    Friend WithEvents btnedit As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblcap As System.Windows.Forms.Label
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents grdpayment As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtBank As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpchequedate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCr As System.Windows.Forms.TextBox
    Friend WithEvents txtchequeno As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnrem As System.Windows.Forms.Button
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents dtpdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents _lblCap_3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents numVchrNo As System.Windows.Forms.TextBox
    Friend WithEvents grdVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents cmbVoucherTp As System.Windows.Forms.ComboBox
    Public WithEvents lbldiff As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents lblcredit As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblTlDebit As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents txtPreFix As System.Windows.Forms.TextBox
    Friend WithEvents plsrch As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents picCloseProd As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents grdSrch As System.Windows.Forms.DataGridView
    Friend WithEvents txtprintprefix As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents txtfcrt As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmbfc As System.Windows.Forms.ComboBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnremoveexpense As System.Windows.Forms.Button
    Friend WithEvents btnaddexpense As System.Windows.Forms.Button
    Public WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents lbljobname As System.Windows.Forms.Label
    Friend WithEvents txtjobcode As System.Windows.Forms.TextBox
    Friend WithEvents lblpayroll As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents cmbdeliveredBy As System.Windows.Forms.ComboBox
    Public WithEvents lblaccountbalance As System.Windows.Forms.Label
    Public WithEvents lbltaxable As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents lblvat As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents chkcal As System.Windows.Forms.CheckBox
End Class
