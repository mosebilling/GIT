<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockAdjustmentFrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StockAdjustmentFrm))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.pnlCmn = New System.Windows.Forms.Panel
        Me.btnlocqty = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.cmbtolocation = New System.Windows.Forms.ComboBox
        Me.chkforlocation = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmbimporttr = New System.Windows.Forms.ComboBox
        Me.btnimport = New System.Windows.Forms.Button
        Me.cmblocation = New System.Windows.Forms.ComboBox
        Me.Label44 = New System.Windows.Forms.Label
        Me.chkcal = New System.Windows.Forms.CheckBox
        Me.lblstatecode = New System.Windows.Forms.Label
        Me.chkTaxbill = New System.Windows.Forms.CheckBox
        Me.txtfcrt = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtPPrefix = New System.Windows.Forms.TextBox
        Me.cmbfc = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.numDisc = New System.Windows.Forms.TextBox
        Me.cmbVoucherTp = New System.Windows.Forms.ComboBox
        Me.txtPurchaseName = New System.Windows.Forms.TextBox
        Me.txtPurchAlias = New System.Windows.Forms.TextBox
        Me.txtprefix = New System.Windows.Forms.TextBox
        Me.btnSlct = New System.Windows.Forms.Button
        Me.cldrdate = New System.Windows.Forms.DateTimePicker
        Me.numVchrNo = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.btnNext = New System.Windows.Forms.Button
        Me.txtDescr = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtSuppName = New System.Windows.Forms.TextBox
        Me.txtSuppAlias = New System.Windows.Forms.TextBox
        Me.txtReference = New System.Windows.Forms.TextBox
        Me.btnrem = New System.Windows.Forms.Button
        Me.btnadd = New System.Windows.Forms.Button
        Me.txtjobname = New System.Windows.Forms.TextBox
        Me.txtJob = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.lblTotAmt = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.lblNetAmt = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.btnModify = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.lbltrdate = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.lbladd7 = New System.Windows.Forms.Label
        Me.lbladd1 = New System.Windows.Forms.Label
        Me.lbladd5 = New System.Windows.Forms.Label
        Me.lbladd4 = New System.Windows.Forms.Label
        Me.lbladd3 = New System.Windows.Forms.Label
        Me.lbladd6 = New System.Windows.Forms.Label
        Me.lbladd2 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.lblCap4 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.lblCap7 = New System.Windows.Forms.Label
        Me.lblCap6 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.lblCap5 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.numPrintVchr = New System.Windows.Forms.TextBox
        Me.chkFormat = New System.Windows.Forms.CheckBox
        Me.plsrch = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.picCloseProd = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.grdSrch = New System.Windows.Forms.DataGridView
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btndelete = New System.Windows.Forms.Button
        Me.btnupdate = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btntax = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.lbltax = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblOthCost = New System.Windows.Forms.Label
        Me.btnothercost = New System.Windows.Forms.Button
        Me.tbothercost = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.btnothadd = New System.Windows.Forms.Button
        Me.btnOthrOk = New System.Windows.Forms.Button
        Me.btnothcancel = New System.Windows.Forms.Button
        Me.btnothRemove = New System.Windows.Forms.Button
        Me.Label24 = New System.Windows.Forms.Label
        Me.numOtherAmt = New System.Windows.Forms.TextBox
        Me.txtOthrDescription = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtOthrRef = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtdebit = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtcredit = New System.Windows.Forms.TextBox
        Me.grdOtherCost = New System.Windows.Forms.DataGridView
        Me.ldtimer = New System.Windows.Forms.Timer(Me.components)
        Me.pnlCmn.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.plsrch.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.picCloseProd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSrch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.tbothercost.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.grdOtherCost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlCmn
        '
        Me.pnlCmn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlCmn.BackColor = System.Drawing.Color.Transparent
        Me.pnlCmn.Controls.Add(Me.txtDescr)
        Me.pnlCmn.Controls.Add(Me.btnlocqty)
        Me.pnlCmn.Controls.Add(Me.TabControl1)
        Me.pnlCmn.Controls.Add(Me.cmblocation)
        Me.pnlCmn.Controls.Add(Me.Label44)
        Me.pnlCmn.Controls.Add(Me.chkcal)
        Me.pnlCmn.Controls.Add(Me.lblstatecode)
        Me.pnlCmn.Controls.Add(Me.chkTaxbill)
        Me.pnlCmn.Controls.Add(Me.Label10)
        Me.pnlCmn.Controls.Add(Me.cmbfc)
        Me.pnlCmn.Controls.Add(Me.Label7)
        Me.pnlCmn.Controls.Add(Me.Label5)
        Me.pnlCmn.Controls.Add(Me.cmbVoucherTp)
        Me.pnlCmn.Controls.Add(Me.txtPurchaseName)
        Me.pnlCmn.Controls.Add(Me.txtPurchAlias)
        Me.pnlCmn.Controls.Add(Me.txtprefix)
        Me.pnlCmn.Controls.Add(Me.btnSlct)
        Me.pnlCmn.Controls.Add(Me.cldrdate)
        Me.pnlCmn.Controls.Add(Me.numVchrNo)
        Me.pnlCmn.Controls.Add(Me.Label14)
        Me.pnlCmn.Controls.Add(Me.btnNext)
        Me.pnlCmn.Controls.Add(Me.Label6)
        Me.pnlCmn.Controls.Add(Me.Label8)
        Me.pnlCmn.Controls.Add(Me.Label3)
        Me.pnlCmn.Controls.Add(Me.Label2)
        Me.pnlCmn.Controls.Add(Me.txtSuppName)
        Me.pnlCmn.Controls.Add(Me.txtSuppAlias)
        Me.pnlCmn.Controls.Add(Me.txtReference)
        Me.pnlCmn.Location = New System.Drawing.Point(4, 36)
        Me.pnlCmn.Name = "pnlCmn"
        Me.pnlCmn.Size = New System.Drawing.Size(1136, 110)
        Me.pnlCmn.TabIndex = 0
        Me.pnlCmn.TabStop = True
        '
        'btnlocqty
        '
        Me.btnlocqty.BackColor = System.Drawing.Color.SteelBlue
        Me.btnlocqty.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnlocqty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnlocqty.ForeColor = System.Drawing.Color.White
        Me.btnlocqty.Location = New System.Drawing.Point(707, 74)
        Me.btnlocqty.Name = "btnlocqty"
        Me.btnlocqty.Size = New System.Drawing.Size(113, 32)
        Me.btnlocqty.TabIndex = 345512
        Me.btnlocqty.Text = "Location QTY"
        Me.btnlocqty.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnlocqty.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(826, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(242, 103)
        Me.TabControl1.TabIndex = 345507
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.cmbtolocation)
        Me.TabPage4.Controls.Add(Me.chkforlocation)
        Me.TabPage4.Controls.Add(Me.GroupBox2)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(234, 77)
        Me.TabPage4.TabIndex = 1
        Me.TabPage4.Text = "Import"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'cmbtolocation
        '
        Me.cmbtolocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtolocation.FormattingEnabled = True
        Me.cmbtolocation.Location = New System.Drawing.Point(128, 52)
        Me.cmbtolocation.Name = "cmbtolocation"
        Me.cmbtolocation.Size = New System.Drawing.Size(97, 21)
        Me.cmbtolocation.TabIndex = 345513
        '
        'chkforlocation
        '
        Me.chkforlocation.AutoSize = True
        Me.chkforlocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkforlocation.Location = New System.Drawing.Point(15, 52)
        Me.chkforlocation.Name = "chkforlocation"
        Me.chkforlocation.Size = New System.Drawing.Size(109, 17)
        Me.chkforlocation.TabIndex = 345513
        Me.chkforlocation.Text = "Location Transfer"
        Me.chkforlocation.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.cmbimporttr)
        Me.GroupBox2.Controls.Add(Me.btnimport)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(210, 48)
        Me.GroupBox2.TabIndex = 345508
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Inventory"
        '
        'cmbimporttr
        '
        Me.cmbimporttr.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbimporttr.BackColor = System.Drawing.SystemColors.Window
        Me.cmbimporttr.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbimporttr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbimporttr.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbimporttr.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbimporttr.Items.AddRange(New Object() {"TO"})
        Me.cmbimporttr.Location = New System.Drawing.Point(69, 15)
        Me.cmbimporttr.Name = "cmbimporttr"
        Me.cmbimporttr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbimporttr.Size = New System.Drawing.Size(135, 27)
        Me.cmbimporttr.TabIndex = 345504
        Me.cmbimporttr.TabStop = False
        '
        'btnimport
        '
        Me.btnimport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnimport.BackColor = System.Drawing.Color.SteelBlue
        Me.btnimport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnimport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnimport.ForeColor = System.Drawing.Color.White
        Me.btnimport.Location = New System.Drawing.Point(9, 15)
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Size = New System.Drawing.Size(58, 28)
        Me.btnimport.TabIndex = 345445
        Me.btnimport.Text = "Import"
        Me.btnimport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnimport.UseVisualStyleBackColor = False
        '
        'cmblocation
        '
        Me.cmblocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmblocation.FormattingEnabled = True
        Me.cmblocation.Location = New System.Drawing.Point(452, 38)
        Me.cmblocation.Name = "cmblocation"
        Me.cmblocation.Size = New System.Drawing.Size(249, 21)
        Me.cmblocation.TabIndex = 345492
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(385, 40)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(48, 13)
        Me.Label44.TabIndex = 345493
        Me.Label44.Text = "Location"
        '
        'chkcal
        '
        Me.chkcal.AutoSize = True
        Me.chkcal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkcal.Location = New System.Drawing.Point(720, 5)
        Me.chkcal.Name = "chkcal"
        Me.chkcal.Size = New System.Drawing.Size(177, 17)
        Me.chkcal.TabIndex = 345476
        Me.chkcal.Text = "Calculate Price From [Tax Price]"
        Me.chkcal.UseVisualStyleBackColor = True
        Me.chkcal.Visible = False
        '
        'lblstatecode
        '
        Me.lblstatecode.AutoSize = True
        Me.lblstatecode.BackColor = System.Drawing.Color.Transparent
        Me.lblstatecode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatecode.Location = New System.Drawing.Point(717, 55)
        Me.lblstatecode.Name = "lblstatecode"
        Me.lblstatecode.Size = New System.Drawing.Size(67, 15)
        Me.lblstatecode.TabIndex = 345474
        Me.lblstatecode.Text = "State Code"
        Me.lblstatecode.Visible = False
        '
        'chkTaxbill
        '
        Me.chkTaxbill.AutoSize = True
        Me.chkTaxbill.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTaxbill.ForeColor = System.Drawing.Color.Green
        Me.chkTaxbill.Location = New System.Drawing.Point(720, 27)
        Me.chkTaxbill.Name = "chkTaxbill"
        Me.chkTaxbill.Size = New System.Drawing.Size(93, 17)
        Me.chkTaxbill.TabIndex = 345473
        Me.chkTaxbill.Text = "Tax Invoice"
        Me.chkTaxbill.UseVisualStyleBackColor = True
        Me.chkTaxbill.Visible = False
        '
        'txtfcrt
        '
        Me.txtfcrt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtfcrt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfcrt.Location = New System.Drawing.Point(1149, 92)
        Me.txtfcrt.Name = "txtfcrt"
        Me.txtfcrt.Size = New System.Drawing.Size(95, 21)
        Me.txtfcrt.TabIndex = 345465
        Me.txtfcrt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtfcrt.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(770, 11)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(20, 13)
        Me.Label10.TabIndex = 345464
        Me.Label10.Text = "FC"
        Me.Label10.Visible = False
        '
        'txtPPrefix
        '
        Me.txtPPrefix.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPPrefix.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPPrefix.Location = New System.Drawing.Point(1160, 68)
        Me.txtPPrefix.MaxLength = 15
        Me.txtPPrefix.Name = "txtPPrefix"
        Me.txtPPrefix.Size = New System.Drawing.Size(38, 21)
        Me.txtPPrefix.TabIndex = 345447
        Me.txtPPrefix.Visible = False
        '
        'cmbfc
        '
        Me.cmbfc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbfc.FormattingEnabled = True
        Me.cmbfc.Location = New System.Drawing.Point(796, 8)
        Me.cmbfc.Name = "cmbfc"
        Me.cmbfc.Size = New System.Drawing.Size(56, 21)
        Me.cmbfc.TabIndex = 345463
        Me.cmbfc.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 36)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 15)
        Me.Label7.TabIndex = 345436
        Me.Label7.Text = "Debit A/C"
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(1167, 111)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(49, 13)
        Me.Label15.TabIndex = 96
        Me.Label15.Text = "Discount"
        Me.Label15.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 345435
        Me.Label5.Text = "Voucher Type"
        '
        'numDisc
        '
        Me.numDisc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numDisc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numDisc.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numDisc.Location = New System.Drawing.Point(1147, 43)
        Me.numDisc.Name = "numDisc"
        Me.numDisc.Size = New System.Drawing.Size(123, 23)
        Me.numDisc.TabIndex = 345429
        Me.numDisc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numDisc.Visible = False
        '
        'cmbVoucherTp
        '
        Me.cmbVoucherTp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVoucherTp.FormattingEnabled = True
        Me.cmbVoucherTp.Items.AddRange(New Object() {"TI", "TO"})
        Me.cmbVoucherTp.Location = New System.Drawing.Point(88, 7)
        Me.cmbVoucherTp.Name = "cmbVoucherTp"
        Me.cmbVoucherTp.Size = New System.Drawing.Size(56, 21)
        Me.cmbVoucherTp.TabIndex = 345434
        '
        'txtPurchaseName
        '
        Me.txtPurchaseName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchaseName.Location = New System.Drawing.Point(88, 35)
        Me.txtPurchaseName.MaxLength = 15
        Me.txtPurchaseName.Name = "txtPurchaseName"
        Me.txtPurchaseName.ReadOnly = True
        Me.txtPurchaseName.Size = New System.Drawing.Size(291, 21)
        Me.txtPurchaseName.TabIndex = 345400
        '
        'txtPurchAlias
        '
        Me.txtPurchAlias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchAlias.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchAlias.Location = New System.Drawing.Point(1023, 36)
        Me.txtPurchAlias.MaxLength = 15
        Me.txtPurchAlias.Name = "txtPurchAlias"
        Me.txtPurchAlias.Size = New System.Drawing.Size(103, 21)
        Me.txtPurchAlias.TabIndex = 345399
        Me.txtPurchAlias.Visible = False
        '
        'txtprefix
        '
        Me.txtprefix.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprefix.Location = New System.Drawing.Point(227, 7)
        Me.txtprefix.MaxLength = 15
        Me.txtprefix.Name = "txtprefix"
        Me.txtprefix.Size = New System.Drawing.Size(45, 21)
        Me.txtprefix.TabIndex = 345398
        '
        'btnSlct
        '
        Me.btnSlct.BackColor = System.Drawing.SystemColors.Control
        Me.btnSlct.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnSlct.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSlct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSlct.Image = CType(resources.GetObject("btnSlct.Image"), System.Drawing.Image)
        Me.btnSlct.Location = New System.Drawing.Point(349, 5)
        Me.btnSlct.Name = "btnSlct"
        Me.btnSlct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnSlct.Size = New System.Drawing.Size(30, 26)
        Me.btnSlct.TabIndex = 345369
        Me.btnSlct.TabStop = False
        Me.btnSlct.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSlct.UseVisualStyleBackColor = False
        Me.btnSlct.Visible = False
        '
        'cldrdate
        '
        Me.cldrdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrdate.Location = New System.Drawing.Point(610, 11)
        Me.cldrdate.Name = "cldrdate"
        Me.cldrdate.Size = New System.Drawing.Size(91, 20)
        Me.cldrdate.TabIndex = 3
        '
        'numVchrNo
        '
        Me.numVchrNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numVchrNo.Location = New System.Drawing.Point(276, 7)
        Me.numVchrNo.Name = "numVchrNo"
        Me.numVchrNo.Size = New System.Drawing.Size(69, 21)
        Me.numVchrNo.TabIndex = 0
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(149, 8)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 15)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "Voucher No."
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnNext.Location = New System.Drawing.Point(1095, 8)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(30, 26)
        Me.btnNext.TabIndex = 115
        Me.btnNext.TabStop = False
        Me.btnNext.Text = ">"
        Me.btnNext.UseVisualStyleBackColor = False
        Me.btnNext.Visible = False
        '
        'txtDescr
        '
        Me.txtDescr.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescr.Location = New System.Drawing.Point(88, 85)
        Me.txtDescr.Name = "txtDescr"
        Me.txtDescr.Size = New System.Drawing.Size(613, 21)
        Me.txtDescr.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 85)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 15)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Description"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 60)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(60, 15)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "Credit A/C"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(557, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 15)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Vr.Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(385, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 15)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Reference"
        '
        'txtSuppName
        '
        Me.txtSuppName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppName.Location = New System.Drawing.Point(88, 60)
        Me.txtSuppName.Name = "txtSuppName"
        Me.txtSuppName.Size = New System.Drawing.Size(613, 21)
        Me.txtSuppName.TabIndex = 5
        '
        'txtSuppAlias
        '
        Me.txtSuppAlias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppAlias.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppAlias.Location = New System.Drawing.Point(985, 60)
        Me.txtSuppAlias.Name = "txtSuppAlias"
        Me.txtSuppAlias.Size = New System.Drawing.Size(148, 21)
        Me.txtSuppAlias.TabIndex = 4
        Me.txtSuppAlias.Visible = False
        '
        'txtReference
        '
        Me.txtReference.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReference.Location = New System.Drawing.Point(452, 11)
        Me.txtReference.MaxLength = 15
        Me.txtReference.Name = "txtReference"
        Me.txtReference.Size = New System.Drawing.Size(103, 21)
        Me.txtReference.TabIndex = 2
        '
        'btnrem
        '
        Me.btnrem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnrem.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrem.ForeColor = System.Drawing.Color.White
        Me.btnrem.Location = New System.Drawing.Point(63, 602)
        Me.btnrem.Name = "btnrem"
        Me.btnrem.Size = New System.Drawing.Size(55, 28)
        Me.btnrem.TabIndex = 1
        Me.btnrem.TabStop = False
        Me.btnrem.Text = "Rem"
        Me.btnrem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrem.UseVisualStyleBackColor = False
        '
        'btnadd
        '
        Me.btnadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnadd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.ForeColor = System.Drawing.Color.White
        Me.btnadd.Location = New System.Drawing.Point(5, 602)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(55, 28)
        Me.btnadd.TabIndex = 0
        Me.btnadd.Text = "&Add"
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'txtjobname
        '
        Me.txtjobname.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtjobname.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtjobname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtjobname.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtjobname.Location = New System.Drawing.Point(322, 93)
        Me.txtjobname.Name = "txtjobname"
        Me.txtjobname.ReadOnly = True
        Me.txtjobname.Size = New System.Drawing.Size(495, 21)
        Me.txtjobname.TabIndex = 345397
        Me.txtjobname.Visible = False
        '
        'txtJob
        '
        Me.txtJob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJob.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJob.Location = New System.Drawing.Point(254, 93)
        Me.txtJob.Name = "txtJob"
        Me.txtJob.Size = New System.Drawing.Size(103, 21)
        Me.txtJob.TabIndex = 7
        Me.txtJob.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(178, 95)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(27, 15)
        Me.Label12.TabIndex = 38
        Me.Label12.Text = "Job"
        Me.Label12.Visible = False
        '
        'grdVoucher
        '
        Me.grdVoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdVoucher.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdVoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdVoucher.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdVoucher.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdVoucher.Location = New System.Drawing.Point(4, 152)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(1136, 448)
        Me.grdVoucher.TabIndex = 136
        '
        'lblTotAmt
        '
        Me.lblTotAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblTotAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotAmt.Location = New System.Drawing.Point(1147, 458)
        Me.lblTotAmt.Name = "lblTotAmt"
        Me.lblTotAmt.Size = New System.Drawing.Size(123, 24)
        Me.lblTotAmt.TabIndex = 102
        Me.lblTotAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotAmt.Visible = False
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(1146, 445)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(61, 13)
        Me.Label16.TabIndex = 95
        Me.Label16.Text = "Gross Total"
        Me.Label16.Visible = False
        '
        'lblNetAmt
        '
        Me.lblNetAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNetAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNetAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNetAmt.Location = New System.Drawing.Point(4, 25)
        Me.lblNetAmt.Name = "lblNetAmt"
        Me.lblNetAmt.Size = New System.Drawing.Size(123, 24)
        Me.lblNetAmt.TabIndex = 103
        Me.lblNetAmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(24, 9)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(72, 16)
        Me.Label17.TabIndex = 100
        Me.Label17.Text = "Net Total"
        '
        'btnModify
        '
        Me.btnModify.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnModify.BackColor = System.Drawing.Color.SteelBlue
        Me.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnModify.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModify.ForeColor = System.Drawing.Color.White
        Me.btnModify.Location = New System.Drawing.Point(1177, 269)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(93, 35)
        Me.btnModify.TabIndex = 69
        Me.btnModify.Text = "&Modify"
        Me.btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnModify.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lbltrdate)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.lbladd7)
        Me.GroupBox3.Controls.Add(Me.lbladd1)
        Me.GroupBox3.Controls.Add(Me.lbladd5)
        Me.GroupBox3.Controls.Add(Me.lbladd4)
        Me.GroupBox3.Controls.Add(Me.lbladd3)
        Me.GroupBox3.Controls.Add(Me.lbladd6)
        Me.GroupBox3.Controls.Add(Me.lbladd2)
        Me.GroupBox3.Controls.Add(Me.Label25)
        Me.GroupBox3.Controls.Add(Me.lblCap4)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.lblCap7)
        Me.GroupBox3.Controls.Add(Me.lblCap6)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.lblCap5)
        Me.GroupBox3.Location = New System.Drawing.Point(895, 72)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(58, 20)
        Me.GroupBox3.TabIndex = 345426
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Supplier Details"
        Me.GroupBox3.Visible = False
        '
        'lbltrdate
        '
        Me.lbltrdate.BackColor = System.Drawing.Color.Transparent
        Me.lbltrdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbltrdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbltrdate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltrdate.ForeColor = System.Drawing.Color.Green
        Me.lbltrdate.Location = New System.Drawing.Point(73, 169)
        Me.lbltrdate.Name = "lbltrdate"
        Me.lbltrdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbltrdate.Size = New System.Drawing.Size(161, 20)
        Me.lbltrdate.TabIndex = 78
        Me.lbltrdate.Text = "Trd.LC Date"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(9, 170)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(51, 14)
        Me.Label19.TabIndex = 77
        Me.Label19.Text = "TLC Date"
        '
        'lbladd7
        '
        Me.lbladd7.BackColor = System.Drawing.Color.Transparent
        Me.lbladd7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbladd7.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbladd7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladd7.ForeColor = System.Drawing.Color.Green
        Me.lbladd7.Location = New System.Drawing.Point(73, 147)
        Me.lbladd7.Name = "lbladd7"
        Me.lbladd7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbladd7.Size = New System.Drawing.Size(161, 20)
        Me.lbladd7.TabIndex = 75
        Me.lbladd7.Text = "Trd.LC No "
        '
        'lbladd1
        '
        Me.lbladd1.BackColor = System.Drawing.Color.Transparent
        Me.lbladd1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbladd1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbladd1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladd1.ForeColor = System.Drawing.Color.Green
        Me.lbladd1.Location = New System.Drawing.Point(73, 21)
        Me.lbladd1.Name = "lbladd1"
        Me.lbladd1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbladd1.Size = New System.Drawing.Size(200, 20)
        Me.lbladd1.TabIndex = 70
        Me.lbladd1.Text = "Address 1"
        '
        'lbladd5
        '
        Me.lbladd5.BackColor = System.Drawing.Color.Transparent
        Me.lbladd5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbladd5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbladd5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladd5.ForeColor = System.Drawing.Color.Green
        Me.lbladd5.Location = New System.Drawing.Point(73, 105)
        Me.lbladd5.Name = "lbladd5"
        Me.lbladd5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbladd5.Size = New System.Drawing.Size(161, 20)
        Me.lbladd5.TabIndex = 76
        Me.lbladd5.Text = "Phone"
        '
        'lbladd4
        '
        Me.lbladd4.BackColor = System.Drawing.Color.Transparent
        Me.lbladd4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbladd4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbladd4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladd4.ForeColor = System.Drawing.Color.Green
        Me.lbladd4.Location = New System.Drawing.Point(73, 84)
        Me.lbladd4.Name = "lbladd4"
        Me.lbladd4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbladd4.Size = New System.Drawing.Size(200, 20)
        Me.lbladd4.TabIndex = 73
        Me.lbladd4.Text = "Address 4"
        '
        'lbladd3
        '
        Me.lbladd3.BackColor = System.Drawing.Color.Transparent
        Me.lbladd3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbladd3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbladd3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladd3.ForeColor = System.Drawing.Color.Green
        Me.lbladd3.Location = New System.Drawing.Point(73, 63)
        Me.lbladd3.Name = "lbladd3"
        Me.lbladd3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbladd3.Size = New System.Drawing.Size(200, 20)
        Me.lbladd3.TabIndex = 72
        Me.lbladd3.Text = "Address 3"
        '
        'lbladd6
        '
        Me.lbladd6.BackColor = System.Drawing.Color.Transparent
        Me.lbladd6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbladd6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbladd6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladd6.ForeColor = System.Drawing.Color.Green
        Me.lbladd6.Location = New System.Drawing.Point(73, 126)
        Me.lbladd6.Name = "lbladd6"
        Me.lbladd6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbladd6.Size = New System.Drawing.Size(197, 20)
        Me.lbladd6.TabIndex = 74
        Me.lbladd6.Text = "Email"
        '
        'lbladd2
        '
        Me.lbladd2.BackColor = System.Drawing.Color.Transparent
        Me.lbladd2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbladd2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbladd2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladd2.ForeColor = System.Drawing.Color.Green
        Me.lbladd2.Location = New System.Drawing.Point(73, 42)
        Me.lbladd2.Name = "lbladd2"
        Me.lbladd2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbladd2.Size = New System.Drawing.Size(200, 20)
        Me.lbladd2.TabIndex = 71
        Me.lbladd2.Text = "Address 2"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(9, 148)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(58, 14)
        Me.Label25.TabIndex = 65
        Me.Label25.Text = "Trd.LC No "
        '
        'lblCap4
        '
        Me.lblCap4.AutoSize = True
        Me.lblCap4.BackColor = System.Drawing.Color.Transparent
        Me.lblCap4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap4.Location = New System.Drawing.Point(9, 21)
        Me.lblCap4.Name = "lblCap4"
        Me.lblCap4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap4.Size = New System.Drawing.Size(58, 14)
        Me.lblCap4.TabIndex = 46
        Me.lblCap4.Text = "Address 1"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(9, 105)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(37, 14)
        Me.Label13.TabIndex = 69
        Me.Label13.Text = "Phone"
        '
        'lblCap7
        '
        Me.lblCap7.AutoSize = True
        Me.lblCap7.BackColor = System.Drawing.Color.Transparent
        Me.lblCap7.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap7.Location = New System.Drawing.Point(9, 84)
        Me.lblCap7.Name = "lblCap7"
        Me.lblCap7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap7.Size = New System.Drawing.Size(58, 14)
        Me.lblCap7.TabIndex = 49
        Me.lblCap7.Text = "Address 4"
        '
        'lblCap6
        '
        Me.lblCap6.AutoSize = True
        Me.lblCap6.BackColor = System.Drawing.Color.Transparent
        Me.lblCap6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap6.Location = New System.Drawing.Point(9, 63)
        Me.lblCap6.Name = "lblCap6"
        Me.lblCap6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap6.Size = New System.Drawing.Size(58, 14)
        Me.lblCap6.TabIndex = 48
        Me.lblCap6.Text = "Address 3"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(8, 126)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(31, 14)
        Me.Label23.TabIndex = 57
        Me.Label23.Text = "Email"
        '
        'lblCap5
        '
        Me.lblCap5.AutoSize = True
        Me.lblCap5.BackColor = System.Drawing.Color.Transparent
        Me.lblCap5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap5.Location = New System.Drawing.Point(9, 42)
        Me.lblCap5.Name = "lblCap5"
        Me.lblCap5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap5.Size = New System.Drawing.Size(58, 14)
        Me.lblCap5.TabIndex = 47
        Me.lblCap5.Text = "Address 2"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(1180, 605)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(93, 35)
        Me.btnExit.TabIndex = 345439
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'numPrintVchr
        '
        Me.numPrintVchr.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numPrintVchr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPrintVchr.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numPrintVchr.Location = New System.Drawing.Point(1180, 346)
        Me.numPrintVchr.Name = "numPrintVchr"
        Me.numPrintVchr.Size = New System.Drawing.Size(90, 21)
        Me.numPrintVchr.TabIndex = 137
        '
        'chkFormat
        '
        Me.chkFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(1177, 407)
        Me.chkFormat.Name = "chkFormat"
        Me.chkFormat.Size = New System.Drawing.Size(65, 19)
        Me.chkFormat.TabIndex = 119
        Me.chkFormat.Text = "Format"
        Me.chkFormat.UseVisualStyleBackColor = False
        '
        'plsrch
        '
        Me.plsrch.Controls.Add(Me.Panel3)
        Me.plsrch.Controls.Add(Me.grdSrch)
        Me.plsrch.Location = New System.Drawing.Point(481, 190)
        Me.plsrch.Name = "plsrch"
        Me.plsrch.Size = New System.Drawing.Size(477, 264)
        Me.plsrch.TabIndex = 345442
        Me.plsrch.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.picCloseProd)
        Me.Panel3.Controls.Add(Me.PictureBox2)
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(477, 32)
        Me.Panel3.TabIndex = 345445
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Font = New System.Drawing.Font("Goudy Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(31, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 21)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Select Item.."
        '
        'picCloseProd
        '
        Me.picCloseProd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.grdSrch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSrch.Location = New System.Drawing.Point(7, 35)
        Me.grdSrch.Name = "grdSrch"
        Me.grdSrch.Size = New System.Drawing.Size(465, 223)
        Me.grdSrch.TabIndex = 3
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.BackColor = System.Drawing.Color.SteelBlue
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.White
        Me.btnPreview.Location = New System.Drawing.Point(1177, 371)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(93, 35)
        Me.btnPreview.TabIndex = 1
        Me.btnPreview.TabStop = False
        Me.btnPreview.Text = "Pre&view"
        Me.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btndelete.BackColor = System.Drawing.Color.SteelBlue
        Me.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.ForeColor = System.Drawing.Color.White
        Me.btndelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btndelete.Location = New System.Drawing.Point(1177, 307)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(93, 35)
        Me.btndelete.TabIndex = 80
        Me.btndelete.Text = "Delete"
        Me.btndelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndelete.UseVisualStyleBackColor = False
        '
        'btnupdate
        '
        Me.btnupdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnupdate.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnupdate.Location = New System.Drawing.Point(1177, 231)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(93, 35)
        Me.btnupdate.TabIndex = 79
        Me.btnupdate.Text = "&Update "
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1276, 36)
        Me.Panel1.TabIndex = 345444
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(36, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 18)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Stock Adustment"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.Pur
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(7, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(26, 27)
        Me.PictureBox1.TabIndex = 27
        Me.PictureBox1.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel2.Controls.Add(Me.lblNetAmt)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Location = New System.Drawing.Point(1143, 543)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(132, 57)
        Me.Panel2.TabIndex = 345445
        '
        'btntax
        '
        Me.btntax.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btntax.BackColor = System.Drawing.Color.SteelBlue
        Me.btntax.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btntax.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntax.ForeColor = System.Drawing.Color.White
        Me.btntax.Location = New System.Drawing.Point(1244, 499)
        Me.btntax.Name = "btntax"
        Me.btntax.Size = New System.Drawing.Size(28, 24)
        Me.btntax.TabIndex = 345449
        Me.btntax.TabStop = False
        Me.btntax.Text = ">"
        Me.btntax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btntax.UseVisualStyleBackColor = False
        Me.btntax.Visible = False
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(1146, 486)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 13)
        Me.Label9.TabIndex = 345430
        Me.Label9.Text = "Tax Total"
        Me.Label9.Visible = False
        '
        'lbltax
        '
        Me.lbltax.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbltax.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lbltax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbltax.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbltax.Location = New System.Drawing.Point(1147, 499)
        Me.lbltax.Name = "lbltax"
        Me.lbltax.Size = New System.Drawing.Size(97, 24)
        Me.lbltax.TabIndex = 345431
        Me.lbltax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbltax.Visible = False
        '
        'Timer1
        '
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.lblOthCost)
        Me.GroupBox1.Controls.Add(Me.btnothercost)
        Me.GroupBox1.Location = New System.Drawing.Point(1170, 141)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(104, 84)
        Me.GroupBox1.TabIndex = 345446
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Other Cost"
        Me.GroupBox1.Visible = False
        '
        'lblOthCost
        '
        Me.lblOthCost.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblOthCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOthCost.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOthCost.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblOthCost.Location = New System.Drawing.Point(6, 16)
        Me.lblOthCost.Name = "lblOthCost"
        Me.lblOthCost.Size = New System.Drawing.Size(93, 24)
        Me.lblOthCost.TabIndex = 103
        Me.lblOthCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnothercost
        '
        Me.btnothercost.BackColor = System.Drawing.Color.SteelBlue
        Me.btnothercost.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnothercost.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnothercost.ForeColor = System.Drawing.Color.White
        Me.btnothercost.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnothercost.Location = New System.Drawing.Point(6, 43)
        Me.btnothercost.Name = "btnothercost"
        Me.btnothercost.Size = New System.Drawing.Size(93, 35)
        Me.btnothercost.TabIndex = 80
        Me.btnothercost.Text = "Other Cost"
        Me.btnothercost.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnothercost.UseVisualStyleBackColor = False
        '
        'tbothercost
        '
        Me.tbothercost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbothercost.Controls.Add(Me.TabPage1)
        Me.tbothercost.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbothercost.Location = New System.Drawing.Point(4, 341)
        Me.tbothercost.Name = "tbothercost"
        Me.tbothercost.SelectedIndex = 0
        Me.tbothercost.Size = New System.Drawing.Size(855, 259)
        Me.tbothercost.TabIndex = 345448
        Me.tbothercost.Visible = False
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btnothadd)
        Me.TabPage1.Controls.Add(Me.btnOthrOk)
        Me.TabPage1.Controls.Add(Me.btnothcancel)
        Me.TabPage1.Controls.Add(Me.btnothRemove)
        Me.TabPage1.Controls.Add(Me.Label24)
        Me.TabPage1.Controls.Add(Me.numOtherAmt)
        Me.TabPage1.Controls.Add(Me.txtOthrDescription)
        Me.TabPage1.Controls.Add(Me.Label22)
        Me.TabPage1.Controls.Add(Me.Label21)
        Me.TabPage1.Controls.Add(Me.txtOthrRef)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.txtdebit)
        Me.TabPage1.Controls.Add(Me.Label20)
        Me.TabPage1.Controls.Add(Me.txtcredit)
        Me.TabPage1.Controls.Add(Me.grdOtherCost)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(847, 233)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Other Cost"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btnothadd
        '
        Me.btnothadd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnothadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnothadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnothadd.ForeColor = System.Drawing.Color.White
        Me.btnothadd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnothadd.Location = New System.Drawing.Point(664, 80)
        Me.btnothadd.Name = "btnothadd"
        Me.btnothadd.Size = New System.Drawing.Size(78, 35)
        Me.btnothadd.TabIndex = 4
        Me.btnothadd.Text = "Add"
        Me.btnothadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnothadd.UseVisualStyleBackColor = False
        '
        'btnOthrOk
        '
        Me.btnOthrOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOthrOk.BackColor = System.Drawing.Color.SteelBlue
        Me.btnOthrOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOthrOk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOthrOk.ForeColor = System.Drawing.Color.White
        Me.btnOthrOk.Location = New System.Drawing.Point(748, 155)
        Me.btnOthrOk.Name = "btnOthrOk"
        Me.btnOthrOk.Size = New System.Drawing.Size(93, 35)
        Me.btnOthrOk.TabIndex = 345447
        Me.btnOthrOk.Text = "OK"
        Me.btnOthrOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnOthrOk.UseVisualStyleBackColor = False
        '
        'btnothcancel
        '
        Me.btnothcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnothcancel.BackColor = System.Drawing.Color.SteelBlue
        Me.btnothcancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnothcancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnothcancel.ForeColor = System.Drawing.Color.White
        Me.btnothcancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnothcancel.Location = New System.Drawing.Point(748, 193)
        Me.btnothcancel.Name = "btnothcancel"
        Me.btnothcancel.Size = New System.Drawing.Size(93, 35)
        Me.btnothcancel.TabIndex = 345449
        Me.btnothcancel.Text = "Cancel"
        Me.btnothcancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnothcancel.UseVisualStyleBackColor = False
        '
        'btnothRemove
        '
        Me.btnothRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnothRemove.BackColor = System.Drawing.Color.SteelBlue
        Me.btnothRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnothRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnothRemove.ForeColor = System.Drawing.Color.White
        Me.btnothRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnothRemove.Location = New System.Drawing.Point(748, 117)
        Me.btnothRemove.Name = "btnothRemove"
        Me.btnothRemove.Size = New System.Drawing.Size(93, 35)
        Me.btnothRemove.TabIndex = 345448
        Me.btnothRemove.Text = "Remove"
        Me.btnothRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnothRemove.UseVisualStyleBackColor = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(5, 84)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(49, 15)
        Me.Label24.TabIndex = 345446
        Me.Label24.Text = "Amount"
        '
        'numOtherAmt
        '
        Me.numOtherAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numOtherAmt.Location = New System.Drawing.Point(73, 82)
        Me.numOtherAmt.MaxLength = 15
        Me.numOtherAmt.Name = "numOtherAmt"
        Me.numOtherAmt.Size = New System.Drawing.Size(103, 21)
        Me.numOtherAmt.TabIndex = 3
        Me.numOtherAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOthrDescription
        '
        Me.txtOthrDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOthrDescription.Location = New System.Drawing.Point(253, 56)
        Me.txtOthrDescription.Name = "txtOthrDescription"
        Me.txtOthrDescription.Size = New System.Drawing.Size(489, 21)
        Me.txtOthrDescription.TabIndex = 2
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(182, 56)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(69, 15)
        Me.Label22.TabIndex = 345444
        Me.Label22.Text = "Description"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(5, 56)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(64, 15)
        Me.Label21.TabIndex = 345442
        Me.Label21.Text = "Reference"
        '
        'txtOthrRef
        '
        Me.txtOthrRef.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOthrRef.Location = New System.Drawing.Point(73, 56)
        Me.txtOthrRef.MaxLength = 15
        Me.txtOthrRef.Name = "txtOthrRef"
        Me.txtOthrRef.Size = New System.Drawing.Size(103, 21)
        Me.txtOthrRef.TabIndex = 1
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(5, 7)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(57, 15)
        Me.Label18.TabIndex = 345440
        Me.Label18.Text = "Debit A/C"
        '
        'txtdebit
        '
        Me.txtdebit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdebit.Location = New System.Drawing.Point(73, 6)
        Me.txtdebit.MaxLength = 15
        Me.txtdebit.Name = "txtdebit"
        Me.txtdebit.ReadOnly = True
        Me.txtdebit.Size = New System.Drawing.Size(669, 21)
        Me.txtdebit.TabIndex = 345439
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(5, 31)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(60, 15)
        Me.Label20.TabIndex = 345438
        Me.Label20.Text = "Credit A/C"
        '
        'txtcredit
        '
        Me.txtcredit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcredit.Location = New System.Drawing.Point(73, 31)
        Me.txtcredit.Name = "txtcredit"
        Me.txtcredit.Size = New System.Drawing.Size(669, 21)
        Me.txtcredit.TabIndex = 0
        '
        'grdOtherCost
        '
        Me.grdOtherCost.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdOtherCost.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdOtherCost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdOtherCost.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdOtherCost.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdOtherCost.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdOtherCost.Location = New System.Drawing.Point(8, 118)
        Me.grdOtherCost.Name = "grdOtherCost"
        Me.grdOtherCost.Size = New System.Drawing.Size(734, 109)
        Me.grdOtherCost.TabIndex = 137
        '
        'ldtimer
        '
        Me.ldtimer.Interval = 200
        '
        'StockAdjustmentFrm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1276, 644)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btntax)
        Me.Controls.Add(Me.plsrch)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.tbothercost)
        Me.Controls.Add(Me.lbltax)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtfcrt)
        Me.Controls.Add(Me.lblTotAmt)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtPPrefix)
        Me.Controls.Add(Me.numPrintVchr)
        Me.Controls.Add(Me.chkFormat)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.pnlCmn)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.numDisc)
        Me.Controls.Add(Me.txtjobname)
        Me.Controls.Add(Me.btnModify)
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.btnrem)
        Me.Controls.Add(Me.btnadd)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnupdate)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtJob)
        Me.Controls.Add(Me.grdVoucher)
        Me.Name = "StockAdjustmentFrm"
        Me.Text = "PurchaseInvoiceFrm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlCmn.ResumeLayout(False)
        Me.pnlCmn.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.plsrch.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.picCloseProd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSrch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.tbothercost.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.grdOtherCost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnModify As System.Windows.Forms.Button
    Friend WithEvents grdVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents pnlCmn As System.Windows.Forms.Panel
    Public WithEvents btnSlct As System.Windows.Forms.Button
    Friend WithEvents cldrdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents numVchrNo As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSuppName As System.Windows.Forms.TextBox
    Friend WithEvents txtSuppAlias As System.Windows.Forms.TextBox
    Friend WithEvents txtReference As System.Windows.Forms.TextBox
    Friend WithEvents numDisc As System.Windows.Forms.TextBox
    Friend WithEvents lblTotAmt As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblNetAmt As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtDescr As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Public WithEvents lbltrdate As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents lbladd7 As System.Windows.Forms.Label
    Public WithEvents lbladd1 As System.Windows.Forms.Label
    Public WithEvents lbladd5 As System.Windows.Forms.Label
    Public WithEvents lbladd4 As System.Windows.Forms.Label
    Public WithEvents lbladd3 As System.Windows.Forms.Label
    Public WithEvents lbladd6 As System.Windows.Forms.Label
    Public WithEvents lbladd2 As System.Windows.Forms.Label
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents lblCap4 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblCap7 As System.Windows.Forms.Label
    Public WithEvents lblCap6 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents lblCap5 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents numPrintVchr As System.Windows.Forms.TextBox
    Friend WithEvents chkFormat As System.Windows.Forms.CheckBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents plsrch As System.Windows.Forms.Panel
    Friend WithEvents picCloseProd As System.Windows.Forms.PictureBox
    Friend WithEvents grdSrch As System.Windows.Forms.DataGridView
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents btnrem As System.Windows.Forms.Button
    Friend WithEvents btndelete As System.Windows.Forms.Button
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents txtjobname As System.Windows.Forms.TextBox
    Friend WithEvents txtJob As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtprefix As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchAlias As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseName As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbVoucherTp As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lbltax As System.Windows.Forms.Label
    Friend WithEvents txtfcrt As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbfc As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblOthCost As System.Windows.Forms.Label
    Friend WithEvents btnothercost As System.Windows.Forms.Button
    Friend WithEvents btntax As System.Windows.Forms.Button
    Friend WithEvents txtPPrefix As System.Windows.Forms.TextBox
    Friend WithEvents tbothercost As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtdebit As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtcredit As System.Windows.Forms.TextBox
    Friend WithEvents grdOtherCost As System.Windows.Forms.DataGridView
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents numOtherAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtOthrDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtOthrRef As System.Windows.Forms.TextBox
    Friend WithEvents btnothadd As System.Windows.Forms.Button
    Friend WithEvents btnOthrOk As System.Windows.Forms.Button
    Friend WithEvents btnothcancel As System.Windows.Forms.Button
    Friend WithEvents btnothRemove As System.Windows.Forms.Button
    Friend WithEvents chkTaxbill As System.Windows.Forms.CheckBox
    Friend WithEvents lblstatecode As System.Windows.Forms.Label
    Friend WithEvents chkcal As System.Windows.Forms.CheckBox
    Friend WithEvents cmblocation As System.Windows.Forms.ComboBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents cmbimporttr As System.Windows.Forms.ComboBox
    Friend WithEvents btnimport As System.Windows.Forms.Button
    Friend WithEvents btnlocqty As System.Windows.Forms.Button
    Friend WithEvents chkforlocation As System.Windows.Forms.CheckBox
    Friend WithEvents ldtimer As System.Windows.Forms.Timer
    Friend WithEvents cmbtolocation As System.Windows.Forms.ComboBox
End Class
