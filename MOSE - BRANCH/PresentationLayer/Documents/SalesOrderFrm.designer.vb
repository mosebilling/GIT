<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SalesOrderFrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SalesOrderFrm))
        Me.pnlCmn = New System.Windows.Forms.Panel
        Me.rdoother = New System.Windows.Forms.RadioButton
        Me.rdodealer = New System.Windows.Forms.RadioButton
        Me.rdows = New System.Windows.Forms.RadioButton
        Me.rdoretail = New System.Windows.Forms.RadioButton
        Me.btninvoice = New System.Windows.Forms.Button
        Me.dtpduedate = New System.Windows.Forms.DateTimePicker
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtDescr = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.chkautoroundOff = New System.Windows.Forms.CheckBox
        Me.btnrem = New System.Windows.Forms.Button
        Me.btnadd = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtReference = New System.Windows.Forms.TextBox
        Me.lblgstn = New System.Windows.Forms.Label
        Me.chktaxInv = New System.Windows.Forms.CheckBox
        Me.lblstatecode = New System.Windows.Forms.Label
        Me.chkcal = New System.Windows.Forms.CheckBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtcustAddress = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmbsalesman = New System.Windows.Forms.ComboBox
        Me.txtfcrt = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.cmbfc = New System.Windows.Forms.ComboBox
        Me.chkremovealert = New System.Windows.Forms.CheckBox
        Me.txtPurchAlias = New System.Windows.Forms.TextBox
        Me.txtprefix = New System.Windows.Forms.TextBox
        Me.txtjobname = New System.Windows.Forms.TextBox
        Me.btnSlct = New System.Windows.Forms.Button
        Me.cldrdate = New System.Windows.Forms.DateTimePicker
        Me.numVchrNo = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.btnNext = New System.Windows.Forms.Button
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtJob = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtSuppName = New System.Windows.Forms.TextBox
        Me.txtSuppAlias = New System.Windows.Forms.TextBox
        Me.btnimport = New System.Windows.Forms.Button
        Me.txtsubject = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtAttn = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.numDisc = New System.Windows.Forms.TextBox
        Me.lblTotAmt = New System.Windows.Forms.Label
        Me.lblNetAmt = New System.Windows.Forms.Label
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
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lbltax = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.lblcess = New System.Windows.Forms.Label
        Me.txtroundOff = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtdp = New System.Windows.Forms.TextBox
        Me.cmbsign = New System.Windows.Forms.ComboBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.btntax = New System.Windows.Forms.Button
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtPPrefix = New System.Windows.Forms.TextBox
        Me.lblbalance = New System.Windows.Forms.Label
        Me.lbllimit = New System.Windows.Forms.Label
        Me.lblInvoices = New System.Windows.Forms.Label
        Me.tbgst = New System.Windows.Forms.TabControl
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.txtIgstAmt = New System.Windows.Forms.TextBox
        Me.txtSgstAmt = New System.Windows.Forms.TextBox
        Me.txtCgstAmt = New System.Windows.Forms.TextBox
        Me.txtIgst = New System.Windows.Forms.TextBox
        Me.lblIgst = New System.Windows.Forms.Label
        Me.txtSgst = New System.Windows.Forms.TextBox
        Me.lblSgst = New System.Windows.Forms.Label
        Me.btnAddgst = New System.Windows.Forms.Button
        Me.btncancelgst = New System.Windows.Forms.Button
        Me.txtCgst = New System.Windows.Forms.TextBox
        Me.lblCGST = New System.Windows.Forms.Label
        Me.btnfind = New System.Windows.Forms.Button
        Me.cmbcolms = New System.Windows.Forms.ComboBox
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.grdItemInfo = New System.Windows.Forms.DataGridView
        Me.btnprint = New System.Windows.Forms.Button
        Me.cmbDos = New System.Windows.Forms.ComboBox
        Me.txtDOLst = New System.Windows.Forms.TextBox
        Me.btnshow = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btncancel = New System.Windows.Forms.Button
        Me.txtinvno = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.ldtimer = New System.Windows.Forms.Timer(Me.components)
        Me.pnlCmn.SuspendLayout()
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
        Me.tbgst.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdItemInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlCmn
        '
        Me.pnlCmn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlCmn.BackColor = System.Drawing.Color.Transparent
        Me.pnlCmn.Controls.Add(Me.rdoother)
        Me.pnlCmn.Controls.Add(Me.rdodealer)
        Me.pnlCmn.Controls.Add(Me.rdows)
        Me.pnlCmn.Controls.Add(Me.rdoretail)
        Me.pnlCmn.Controls.Add(Me.btninvoice)
        Me.pnlCmn.Controls.Add(Me.dtpduedate)
        Me.pnlCmn.Controls.Add(Me.Label11)
        Me.pnlCmn.Controls.Add(Me.txtDescr)
        Me.pnlCmn.Controls.Add(Me.Label6)
        Me.pnlCmn.Controls.Add(Me.chkautoroundOff)
        Me.pnlCmn.Controls.Add(Me.btnrem)
        Me.pnlCmn.Controls.Add(Me.btnadd)
        Me.pnlCmn.Controls.Add(Me.Label2)
        Me.pnlCmn.Controls.Add(Me.txtReference)
        Me.pnlCmn.Controls.Add(Me.lblgstn)
        Me.pnlCmn.Controls.Add(Me.chktaxInv)
        Me.pnlCmn.Controls.Add(Me.lblstatecode)
        Me.pnlCmn.Controls.Add(Me.chkcal)
        Me.pnlCmn.Controls.Add(Me.Label18)
        Me.pnlCmn.Controls.Add(Me.txtcustAddress)
        Me.pnlCmn.Controls.Add(Me.Label9)
        Me.pnlCmn.Controls.Add(Me.cmbsalesman)
        Me.pnlCmn.Controls.Add(Me.txtfcrt)
        Me.pnlCmn.Controls.Add(Me.Label10)
        Me.pnlCmn.Controls.Add(Me.cmbfc)
        Me.pnlCmn.Controls.Add(Me.chkremovealert)
        Me.pnlCmn.Controls.Add(Me.txtPurchAlias)
        Me.pnlCmn.Controls.Add(Me.txtprefix)
        Me.pnlCmn.Controls.Add(Me.txtjobname)
        Me.pnlCmn.Controls.Add(Me.btnSlct)
        Me.pnlCmn.Controls.Add(Me.cldrdate)
        Me.pnlCmn.Controls.Add(Me.numVchrNo)
        Me.pnlCmn.Controls.Add(Me.Label14)
        Me.pnlCmn.Controls.Add(Me.btnNext)
        Me.pnlCmn.Controls.Add(Me.Label12)
        Me.pnlCmn.Controls.Add(Me.txtJob)
        Me.pnlCmn.Controls.Add(Me.Label8)
        Me.pnlCmn.Controls.Add(Me.Label3)
        Me.pnlCmn.Controls.Add(Me.txtSuppName)
        Me.pnlCmn.Controls.Add(Me.txtSuppAlias)
        Me.pnlCmn.Location = New System.Drawing.Point(4, 44)
        Me.pnlCmn.Name = "pnlCmn"
        Me.pnlCmn.Size = New System.Drawing.Size(1136, 149)
        Me.pnlCmn.TabIndex = 0
        Me.pnlCmn.TabStop = True
        '
        'rdoother
        '
        Me.rdoother.AutoSize = True
        Me.rdoother.Checked = True
        Me.rdoother.Location = New System.Drawing.Point(772, 4)
        Me.rdoother.Name = "rdoother"
        Me.rdoother.Size = New System.Drawing.Size(78, 17)
        Me.rdoother.TabIndex = 345518
        Me.rdoother.TabStop = True
        Me.rdoother.Text = "Other Price"
        Me.rdoother.UseVisualStyleBackColor = True
        '
        'rdodealer
        '
        Me.rdodealer.AutoSize = True
        Me.rdodealer.Location = New System.Drawing.Point(772, 25)
        Me.rdodealer.Name = "rdodealer"
        Me.rdodealer.Size = New System.Drawing.Size(83, 17)
        Me.rdodealer.TabIndex = 345517
        Me.rdodealer.TabStop = True
        Me.rdodealer.Text = "Dealer Price"
        Me.rdodealer.UseVisualStyleBackColor = True
        '
        'rdows
        '
        Me.rdows.AutoSize = True
        Me.rdows.Location = New System.Drawing.Point(693, 25)
        Me.rdows.Name = "rdows"
        Me.rdows.Size = New System.Drawing.Size(70, 17)
        Me.rdows.TabIndex = 345516
        Me.rdows.TabStop = True
        Me.rdows.Text = "WS Price"
        Me.rdows.UseVisualStyleBackColor = True
        '
        'rdoretail
        '
        Me.rdoretail.AutoSize = True
        Me.rdoretail.Checked = True
        Me.rdoretail.Location = New System.Drawing.Point(693, 3)
        Me.rdoretail.Name = "rdoretail"
        Me.rdoretail.Size = New System.Drawing.Size(79, 17)
        Me.rdoretail.TabIndex = 345515
        Me.rdoretail.TabStop = True
        Me.rdoretail.Text = "Retail Price"
        Me.rdoretail.UseVisualStyleBackColor = True
        '
        'btninvoice
        '
        Me.btninvoice.BackColor = System.Drawing.Color.SteelBlue
        Me.btninvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btninvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btninvoice.ForeColor = System.Drawing.Color.White
        Me.btninvoice.Location = New System.Drawing.Point(482, 110)
        Me.btninvoice.Name = "btninvoice"
        Me.btninvoice.Size = New System.Drawing.Size(188, 32)
        Me.btninvoice.TabIndex = 345514
        Me.btninvoice.Text = "Troansfer to Invoice"
        Me.btninvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btninvoice.UseVisualStyleBackColor = False
        '
        'dtpduedate
        '
        Me.dtpduedate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpduedate.Location = New System.Drawing.Point(544, 86)
        Me.dtpduedate.Name = "dtpduedate"
        Me.dtpduedate.Size = New System.Drawing.Size(106, 20)
        Me.dtpduedate.TabIndex = 345512
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(481, 86)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(59, 15)
        Me.Label11.TabIndex = 345513
        Me.Label11.Text = "Due Date"
        '
        'txtDescr
        '
        Me.txtDescr.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescr.Location = New System.Drawing.Point(691, 63)
        Me.txtDescr.Multiline = True
        Me.txtDescr.Name = "txtDescr"
        Me.txtDescr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescr.Size = New System.Drawing.Size(320, 69)
        Me.txtDescr.TabIndex = 345510
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(690, 46)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(121, 15)
        Me.Label6.TabIndex = 345511
        Me.Label6.Text = "Tearms && Conditions"
        '
        'chkautoroundOff
        '
        Me.chkautoroundOff.AutoSize = True
        Me.chkautoroundOff.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkautoroundOff.Location = New System.Drawing.Point(908, 44)
        Me.chkautoroundOff.Name = "chkautoroundOff"
        Me.chkautoroundOff.Size = New System.Drawing.Size(97, 17)
        Me.chkautoroundOff.TabIndex = 345509
        Me.chkautoroundOff.Text = "Auto RoundOff"
        Me.chkautoroundOff.UseVisualStyleBackColor = True
        '
        'btnrem
        '
        Me.btnrem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnrem.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrem.ForeColor = System.Drawing.Color.White
        Me.btnrem.Location = New System.Drawing.Point(1078, 114)
        Me.btnrem.Name = "btnrem"
        Me.btnrem.Size = New System.Drawing.Size(55, 32)
        Me.btnrem.TabIndex = 1
        Me.btnrem.TabStop = False
        Me.btnrem.Text = "Rem"
        Me.btnrem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrem.UseVisualStyleBackColor = False
        '
        'btnadd
        '
        Me.btnadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnadd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.ForeColor = System.Drawing.Color.White
        Me.btnadd.Location = New System.Drawing.Point(1020, 114)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(55, 32)
        Me.btnadd.TabIndex = 0
        Me.btnadd.Text = "&Add"
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(262, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 15)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Reference"
        '
        'txtReference
        '
        Me.txtReference.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReference.Location = New System.Drawing.Point(330, 5)
        Me.txtReference.MaxLength = 50
        Me.txtReference.Name = "txtReference"
        Me.txtReference.Size = New System.Drawing.Size(144, 21)
        Me.txtReference.TabIndex = 2
        '
        'lblgstn
        '
        Me.lblgstn.AutoSize = True
        Me.lblgstn.BackColor = System.Drawing.Color.Transparent
        Me.lblgstn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgstn.ForeColor = System.Drawing.Color.Green
        Me.lblgstn.Location = New System.Drawing.Point(100, 126)
        Me.lblgstn.Name = "lblgstn"
        Me.lblgstn.Size = New System.Drawing.Size(44, 15)
        Me.lblgstn.TabIndex = 345486
        Me.lblgstn.Text = "GSTN"
        '
        'chktaxInv
        '
        Me.chktaxInv.AutoSize = True
        Me.chktaxInv.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chktaxInv.ForeColor = System.Drawing.Color.Green
        Me.chktaxInv.Location = New System.Drawing.Point(9, 126)
        Me.chktaxInv.Name = "chktaxInv"
        Me.chktaxInv.Size = New System.Drawing.Size(93, 17)
        Me.chktaxInv.TabIndex = 345485
        Me.chktaxInv.Text = "Tax Invoice"
        Me.chktaxInv.UseVisualStyleBackColor = True
        '
        'lblstatecode
        '
        Me.lblstatecode.AutoSize = True
        Me.lblstatecode.BackColor = System.Drawing.Color.Transparent
        Me.lblstatecode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatecode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblstatecode.Location = New System.Drawing.Point(397, 129)
        Me.lblstatecode.Name = "lblstatecode"
        Me.lblstatecode.Size = New System.Drawing.Size(77, 15)
        Me.lblstatecode.TabIndex = 345478
        Me.lblstatecode.Text = "State Code"
        '
        'chkcal
        '
        Me.chkcal.AutoSize = True
        Me.chkcal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkcal.Location = New System.Drawing.Point(908, 25)
        Me.chkcal.Name = "chkcal"
        Me.chkcal.Size = New System.Drawing.Size(144, 17)
        Me.chkcal.TabIndex = 345475
        Me.chkcal.Text = "Calculate Tax From Price"
        Me.chkcal.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(11, 55)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(51, 15)
        Me.Label18.TabIndex = 345477
        Me.Label18.Text = "Address"
        '
        'txtcustAddress
        '
        Me.txtcustAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustAddress.Location = New System.Drawing.Point(90, 55)
        Me.txtcustAddress.Multiline = True
        Me.txtcustAddress.Name = "txtcustAddress"
        Me.txtcustAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtcustAddress.Size = New System.Drawing.Size(384, 63)
        Me.txtcustAddress.TabIndex = 345476
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(483, 38)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 345470
        Me.Label9.Text = "Sales Man"
        '
        'cmbsalesman
        '
        Me.cmbsalesman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsalesman.FormattingEnabled = True
        Me.cmbsalesman.Location = New System.Drawing.Point(543, 35)
        Me.cmbsalesman.Name = "cmbsalesman"
        Me.cmbsalesman.Size = New System.Drawing.Size(130, 21)
        Me.cmbsalesman.TabIndex = 345469
        '
        'txtfcrt
        '
        Me.txtfcrt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtfcrt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfcrt.Location = New System.Drawing.Point(603, 59)
        Me.txtfcrt.Name = "txtfcrt"
        Me.txtfcrt.Size = New System.Drawing.Size(70, 21)
        Me.txtfcrt.TabIndex = 345468
        Me.txtfcrt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(483, 60)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(20, 13)
        Me.Label10.TabIndex = 345467
        Me.Label10.Text = "FC"
        '
        'cmbfc
        '
        Me.cmbfc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbfc.FormattingEnabled = True
        Me.cmbfc.Location = New System.Drawing.Point(544, 59)
        Me.cmbfc.Name = "cmbfc"
        Me.cmbfc.Size = New System.Drawing.Size(56, 21)
        Me.cmbfc.TabIndex = 345466
        '
        'chkremovealert
        '
        Me.chkremovealert.AutoSize = True
        Me.chkremovealert.Location = New System.Drawing.Point(908, 6)
        Me.chkremovealert.Name = "chkremovealert"
        Me.chkremovealert.Size = New System.Drawing.Size(90, 17)
        Me.chkremovealert.TabIndex = 345401
        Me.chkremovealert.Text = "Remove Alert"
        Me.chkremovealert.UseVisualStyleBackColor = True
        '
        'txtPurchAlias
        '
        Me.txtPurchAlias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchAlias.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchAlias.Location = New System.Drawing.Point(1140, 7)
        Me.txtPurchAlias.MaxLength = 15
        Me.txtPurchAlias.Name = "txtPurchAlias"
        Me.txtPurchAlias.Size = New System.Drawing.Size(103, 21)
        Me.txtPurchAlias.TabIndex = 345399
        Me.txtPurchAlias.Visible = False
        '
        'txtprefix
        '
        Me.txtprefix.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprefix.Location = New System.Drawing.Point(90, 4)
        Me.txtprefix.MaxLength = 15
        Me.txtprefix.Name = "txtprefix"
        Me.txtprefix.Size = New System.Drawing.Size(59, 21)
        Me.txtprefix.TabIndex = 345398
        '
        'txtjobname
        '
        Me.txtjobname.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtjobname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtjobname.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtjobname.Location = New System.Drawing.Point(735, 120)
        Me.txtjobname.Name = "txtjobname"
        Me.txtjobname.ReadOnly = True
        Me.txtjobname.Size = New System.Drawing.Size(276, 21)
        Me.txtjobname.TabIndex = 345397
        Me.txtjobname.Visible = False
        '
        'btnSlct
        '
        Me.btnSlct.BackColor = System.Drawing.SystemColors.Control
        Me.btnSlct.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnSlct.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSlct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSlct.Image = CType(resources.GetObject("btnSlct.Image"), System.Drawing.Image)
        Me.btnSlct.Location = New System.Drawing.Point(229, 1)
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
        Me.cldrdate.Location = New System.Drawing.Point(543, 7)
        Me.cldrdate.Name = "cldrdate"
        Me.cldrdate.Size = New System.Drawing.Size(106, 20)
        Me.cldrdate.TabIndex = 3
        '
        'numVchrNo
        '
        Me.numVchrNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numVchrNo.Location = New System.Drawing.Point(152, 4)
        Me.numVchrNo.Name = "numVchrNo"
        Me.numVchrNo.Size = New System.Drawing.Size(76, 21)
        Me.numVchrNo.TabIndex = 0
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(11, 6)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(46, 15)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "SO No."
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnNext.Location = New System.Drawing.Point(1249, 26)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(30, 26)
        Me.btnNext.TabIndex = 115
        Me.btnNext.TabStop = False
        Me.btnNext.Text = ">"
        Me.btnNext.UseVisualStyleBackColor = False
        Me.btnNext.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(593, 122)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(27, 15)
        Me.Label12.TabIndex = 38
        Me.Label12.Text = "Job"
        Me.Label12.Visible = False
        '
        'txtJob
        '
        Me.txtJob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJob.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJob.Location = New System.Drawing.Point(626, 120)
        Me.txtJob.Name = "txtJob"
        Me.txtJob.Size = New System.Drawing.Size(103, 21)
        Me.txtJob.TabIndex = 7
        Me.txtJob.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(11, 30)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(60, 15)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "Customer"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(480, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 15)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Vr.Date"
        '
        'txtSuppName
        '
        Me.txtSuppName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppName.Location = New System.Drawing.Point(90, 30)
        Me.txtSuppName.Name = "txtSuppName"
        Me.txtSuppName.Size = New System.Drawing.Size(384, 21)
        Me.txtSuppName.TabIndex = 3
        '
        'txtSuppAlias
        '
        Me.txtSuppAlias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppAlias.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppAlias.Location = New System.Drawing.Point(1140, 30)
        Me.txtSuppAlias.Name = "txtSuppAlias"
        Me.txtSuppAlias.Size = New System.Drawing.Size(103, 21)
        Me.txtSuppAlias.TabIndex = 4
        Me.txtSuppAlias.Visible = False
        '
        'btnimport
        '
        Me.btnimport.BackColor = System.Drawing.Color.SteelBlue
        Me.btnimport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnimport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnimport.ForeColor = System.Drawing.Color.White
        Me.btnimport.Location = New System.Drawing.Point(167, 44)
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Size = New System.Drawing.Size(112, 32)
        Me.btnimport.TabIndex = 345445
        Me.btnimport.Text = "Imported To"
        Me.btnimport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnimport.UseVisualStyleBackColor = False
        Me.btnimport.Visible = False
        '
        'txtsubject
        '
        Me.txtsubject.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsubject.Location = New System.Drawing.Point(1227, 78)
        Me.txtsubject.Name = "txtsubject"
        Me.txtsubject.Size = New System.Drawing.Size(385, 21)
        Me.txtsubject.TabIndex = 5
        Me.txtsubject.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1148, 81)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 15)
        Me.Label5.TabIndex = 345490
        Me.Label5.Text = "Subject"
        Me.Label5.Visible = False
        '
        'txtAttn
        '
        Me.txtAttn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAttn.Location = New System.Drawing.Point(1227, 53)
        Me.txtAttn.Name = "txtAttn"
        Me.txtAttn.Size = New System.Drawing.Size(194, 21)
        Me.txtAttn.TabIndex = 4
        Me.txtAttn.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(1148, 55)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 15)
        Me.Label7.TabIndex = 345488
        Me.Label7.Text = "Attention To."
        Me.Label7.Visible = False
        '
        'grdVoucher
        '
        Me.grdVoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdVoucher.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdVoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdVoucher.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        Me.grdVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdVoucher.Location = New System.Drawing.Point(4, 194)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(1136, 331)
        Me.grdVoucher.TabIndex = 136
        '
        'numDisc
        '
        Me.numDisc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numDisc.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numDisc.Location = New System.Drawing.Point(46, 70)
        Me.numDisc.Name = "numDisc"
        Me.numDisc.Size = New System.Drawing.Size(83, 23)
        Me.numDisc.TabIndex = 345429
        Me.numDisc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTotAmt
        '
        Me.lblTotAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblTotAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotAmt.Location = New System.Drawing.Point(4, 26)
        Me.lblTotAmt.Name = "lblTotAmt"
        Me.lblTotAmt.Size = New System.Drawing.Size(123, 24)
        Me.lblTotAmt.TabIndex = 102
        Me.lblTotAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNetAmt
        '
        Me.lblNetAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNetAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNetAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmt.ForeColor = System.Drawing.Color.Red
        Me.lblNetAmt.Location = New System.Drawing.Point(1124, 561)
        Me.lblNetAmt.Name = "lblNetAmt"
        Me.lblNetAmt.Size = New System.Drawing.Size(149, 42)
        Me.lblNetAmt.TabIndex = 103
        Me.lblNetAmt.Text = "9999999.99"
        Me.lblNetAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnModify
        '
        Me.btnModify.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnModify.BackColor = System.Drawing.Color.SteelBlue
        Me.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnModify.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModify.ForeColor = System.Drawing.Color.White
        Me.btnModify.Location = New System.Drawing.Point(1171, 169)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(100, 35)
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
        Me.btnExit.Location = New System.Drawing.Point(1172, 605)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(101, 35)
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
        Me.numPrintVchr.Location = New System.Drawing.Point(1209, 207)
        Me.numPrintVchr.Name = "numPrintVchr"
        Me.numPrintVchr.Size = New System.Drawing.Size(61, 21)
        Me.numPrintVchr.TabIndex = 137
        '
        'chkFormat
        '
        Me.chkFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(1178, 306)
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
        Me.plsrch.Location = New System.Drawing.Point(282, 219)
        Me.plsrch.Name = "plsrch"
        Me.plsrch.Size = New System.Drawing.Size(707, 306)
        Me.plsrch.TabIndex = 345442
        Me.plsrch.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.picCloseProd)
        Me.Panel3.Controls.Add(Me.PictureBox2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(707, 32)
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
        Me.picCloseProd.Location = New System.Drawing.Point(689, 9)
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
        Me.grdSrch.Size = New System.Drawing.Size(695, 265)
        Me.grdSrch.TabIndex = 3
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.BackColor = System.Drawing.Color.SteelBlue
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.White
        Me.btnPreview.Location = New System.Drawing.Point(1171, 232)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(100, 35)
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
        Me.btndelete.Location = New System.Drawing.Point(1069, 605)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(100, 35)
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
        Me.btnupdate.Location = New System.Drawing.Point(1171, 132)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(100, 35)
        Me.btnupdate.TabIndex = 79
        Me.btnupdate.Text = "&Update "
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1276, 36)
        Me.Panel1.TabIndex = 345444
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(3, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(37, 18)
        Me.PictureBox1.TabIndex = 345458
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(39, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 18)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Sales Order"
        '
        'Timer1
        '
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel2.Controls.Add(Me.lbltax)
        Me.Panel2.Controls.Add(Me.Label31)
        Me.Panel2.Controls.Add(Me.lblcess)
        Me.Panel2.Controls.Add(Me.txtroundOff)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.txtdp)
        Me.Panel2.Controls.Add(Me.cmbsign)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.btntax)
        Me.Panel2.Controls.Add(Me.Label20)
        Me.Panel2.Controls.Add(Me.Label21)
        Me.Panel2.Controls.Add(Me.Label24)
        Me.Panel2.Controls.Add(Me.numDisc)
        Me.Panel2.Controls.Add(Me.lblTotAmt)
        Me.Panel2.Location = New System.Drawing.Point(1144, 333)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(132, 224)
        Me.Panel2.TabIndex = 345446
        '
        'lbltax
        '
        Me.lbltax.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lbltax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbltax.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbltax.Location = New System.Drawing.Point(3, 113)
        Me.lbltax.Name = "lbltax"
        Me.lbltax.Size = New System.Drawing.Size(123, 24)
        Me.lbltax.TabIndex = 345431
        Me.lbltax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Black
        Me.Label31.Location = New System.Drawing.Point(4, 139)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(30, 13)
        Me.Label31.TabIndex = 345455
        Me.Label31.Text = "Cess"
        '
        'lblcess
        '
        Me.lblcess.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblcess.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcess.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblcess.Location = New System.Drawing.Point(4, 155)
        Me.lblcess.Name = "lblcess"
        Me.lblcess.Size = New System.Drawing.Size(123, 24)
        Me.lblcess.TabIndex = 345456
        Me.lblcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtroundOff
        '
        Me.txtroundOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtroundOff.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtroundOff.Location = New System.Drawing.Point(55, 192)
        Me.txtroundOff.Name = "txtroundOff"
        Me.txtroundOff.Size = New System.Drawing.Size(73, 23)
        Me.txtroundOff.TabIndex = 345454
        Me.txtroundOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(14, 54)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(15, 13)
        Me.Label15.TabIndex = 345451
        Me.Label15.Text = "%"
        '
        'txtdp
        '
        Me.txtdp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtdp.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdp.Location = New System.Drawing.Point(4, 70)
        Me.txtdp.Name = "txtdp"
        Me.txtdp.Size = New System.Drawing.Size(39, 23)
        Me.txtdp.TabIndex = 345450
        Me.txtdp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbsign
        '
        Me.cmbsign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsign.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsign.FormattingEnabled = True
        Me.cmbsign.Items.AddRange(New Object() {"+", "-"})
        Me.cmbsign.Location = New System.Drawing.Point(3, 193)
        Me.cmbsign.Name = "cmbsign"
        Me.cmbsign.Size = New System.Drawing.Size(50, 21)
        Me.cmbsign.TabIndex = 345452
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(3, 179)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(64, 13)
        Me.Label17.TabIndex = 345453
        Me.Label17.Text = "Round Off"
        '
        'btntax
        '
        Me.btntax.BackColor = System.Drawing.Color.SteelBlue
        Me.btntax.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btntax.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntax.ForeColor = System.Drawing.Color.White
        Me.btntax.Location = New System.Drawing.Point(99, 113)
        Me.btntax.Name = "btntax"
        Me.btntax.Size = New System.Drawing.Size(28, 24)
        Me.btntax.TabIndex = 345449
        Me.btntax.TabStop = False
        Me.btntax.Text = ">"
        Me.btntax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btntax.UseVisualStyleBackColor = False
        Me.btntax.Visible = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(2, 97)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(56, 13)
        Me.Label20.TabIndex = 345430
        Me.Label20.Text = "GST Total"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(3, 9)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(61, 13)
        Me.Label21.TabIndex = 95
        Me.Label21.Text = "Gross Total"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(41, 54)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(49, 13)
        Me.Label24.TabIndex = 96
        Me.Label24.Text = "Discount"
        '
        'Label27
        '
        Me.Label27.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(1014, 564)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(108, 25)
        Me.Label27.TabIndex = 100
        Me.Label27.Text = "Net Total"
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label16.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(3, 594)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(63, 14)
        Me.Label16.TabIndex = 345447
        Me.Label16.Text = "Balance "
        '
        'Label22
        '
        Me.Label22.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label22.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label22.Location = New System.Drawing.Point(3, 610)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(59, 14)
        Me.Label22.TabIndex = 345449
        Me.Label22.Text = "Cr Limit"
        '
        'Label26
        '
        Me.Label26.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label26.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(3, 626)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(128, 14)
        Me.Label26.TabIndex = 345451
        Me.Label26.Text = "Due Invoice Count"
        '
        'txtPPrefix
        '
        Me.txtPPrefix.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPPrefix.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPPrefix.Location = New System.Drawing.Point(1171, 207)
        Me.txtPPrefix.Name = "txtPPrefix"
        Me.txtPPrefix.Size = New System.Drawing.Size(37, 21)
        Me.txtPPrefix.TabIndex = 345453
        '
        'lblbalance
        '
        Me.lblbalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblbalance.BackColor = System.Drawing.Color.Transparent
        Me.lblbalance.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lblbalance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbalance.ForeColor = System.Drawing.Color.Green
        Me.lblbalance.Location = New System.Drawing.Point(86, 594)
        Me.lblbalance.Name = "lblbalance"
        Me.lblbalance.Size = New System.Drawing.Size(177, 16)
        Me.lblbalance.TabIndex = 345454
        Me.lblbalance.Text = "0.00"
        Me.lblbalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbllimit
        '
        Me.lbllimit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbllimit.BackColor = System.Drawing.Color.Transparent
        Me.lbllimit.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lbllimit.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbllimit.ForeColor = System.Drawing.Color.Green
        Me.lbllimit.Location = New System.Drawing.Point(86, 610)
        Me.lbllimit.Name = "lbllimit"
        Me.lbllimit.Size = New System.Drawing.Size(177, 16)
        Me.lbllimit.TabIndex = 345455
        Me.lbllimit.Text = "0.00"
        Me.lbllimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblInvoices
        '
        Me.lblInvoices.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblInvoices.BackColor = System.Drawing.Color.Transparent
        Me.lblInvoices.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lblInvoices.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoices.ForeColor = System.Drawing.Color.Green
        Me.lblInvoices.Location = New System.Drawing.Point(183, 626)
        Me.lblInvoices.Name = "lblInvoices"
        Me.lblInvoices.Size = New System.Drawing.Size(80, 16)
        Me.lblInvoices.TabIndex = 345456
        Me.lblInvoices.Text = "0.00"
        Me.lblInvoices.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbgst
        '
        Me.tbgst.Controls.Add(Me.TabPage2)
        Me.tbgst.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbgst.Location = New System.Drawing.Point(330, 235)
        Me.tbgst.Name = "tbgst"
        Me.tbgst.SelectedIndex = 0
        Me.tbgst.Size = New System.Drawing.Size(359, 119)
        Me.tbgst.TabIndex = 345464
        Me.tbgst.Visible = False
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.txtIgstAmt)
        Me.TabPage2.Controls.Add(Me.txtSgstAmt)
        Me.TabPage2.Controls.Add(Me.txtCgstAmt)
        Me.TabPage2.Controls.Add(Me.txtIgst)
        Me.TabPage2.Controls.Add(Me.lblIgst)
        Me.TabPage2.Controls.Add(Me.txtSgst)
        Me.TabPage2.Controls.Add(Me.lblSgst)
        Me.TabPage2.Controls.Add(Me.btnAddgst)
        Me.TabPage2.Controls.Add(Me.btncancelgst)
        Me.TabPage2.Controls.Add(Me.txtCgst)
        Me.TabPage2.Controls.Add(Me.lblCGST)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(351, 93)
        Me.TabPage2.TabIndex = 0
        Me.TabPage2.Text = "GST"
        '
        'txtIgstAmt
        '
        Me.txtIgstAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIgstAmt.Location = New System.Drawing.Point(115, 61)
        Me.txtIgstAmt.MaxLength = 15
        Me.txtIgstAmt.Name = "txtIgstAmt"
        Me.txtIgstAmt.ReadOnly = True
        Me.txtIgstAmt.Size = New System.Drawing.Size(95, 21)
        Me.txtIgstAmt.TabIndex = 345456
        Me.txtIgstAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSgstAmt
        '
        Me.txtSgstAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSgstAmt.Location = New System.Drawing.Point(115, 35)
        Me.txtSgstAmt.MaxLength = 15
        Me.txtSgstAmt.Name = "txtSgstAmt"
        Me.txtSgstAmt.ReadOnly = True
        Me.txtSgstAmt.Size = New System.Drawing.Size(95, 21)
        Me.txtSgstAmt.TabIndex = 345455
        Me.txtSgstAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCgstAmt
        '
        Me.txtCgstAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCgstAmt.Location = New System.Drawing.Point(115, 9)
        Me.txtCgstAmt.MaxLength = 15
        Me.txtCgstAmt.Name = "txtCgstAmt"
        Me.txtCgstAmt.ReadOnly = True
        Me.txtCgstAmt.Size = New System.Drawing.Size(95, 21)
        Me.txtCgstAmt.TabIndex = 345454
        Me.txtCgstAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIgst
        '
        Me.txtIgst.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIgst.Location = New System.Drawing.Point(60, 61)
        Me.txtIgst.MaxLength = 15
        Me.txtIgst.Name = "txtIgst"
        Me.txtIgst.Size = New System.Drawing.Size(51, 21)
        Me.txtIgst.TabIndex = 2
        Me.txtIgst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblIgst
        '
        Me.lblIgst.AutoSize = True
        Me.lblIgst.BackColor = System.Drawing.Color.Transparent
        Me.lblIgst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIgst.ForeColor = System.Drawing.Color.White
        Me.lblIgst.Location = New System.Drawing.Point(15, 61)
        Me.lblIgst.Name = "lblIgst"
        Me.lblIgst.Size = New System.Drawing.Size(36, 13)
        Me.lblIgst.TabIndex = 345453
        Me.lblIgst.Text = "IGST"
        '
        'txtSgst
        '
        Me.txtSgst.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSgst.Location = New System.Drawing.Point(60, 35)
        Me.txtSgst.MaxLength = 15
        Me.txtSgst.Name = "txtSgst"
        Me.txtSgst.Size = New System.Drawing.Size(51, 21)
        Me.txtSgst.TabIndex = 1
        Me.txtSgst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSgst
        '
        Me.lblSgst.AutoSize = True
        Me.lblSgst.BackColor = System.Drawing.Color.Transparent
        Me.lblSgst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSgst.ForeColor = System.Drawing.Color.White
        Me.lblSgst.Location = New System.Drawing.Point(15, 35)
        Me.lblSgst.Name = "lblSgst"
        Me.lblSgst.Size = New System.Drawing.Size(40, 13)
        Me.lblSgst.TabIndex = 345451
        Me.lblSgst.Text = "SGST"
        '
        'btnAddgst
        '
        Me.btnAddgst.BackColor = System.Drawing.Color.SteelBlue
        Me.btnAddgst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddgst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddgst.ForeColor = System.Drawing.Color.White
        Me.btnAddgst.Location = New System.Drawing.Point(255, 9)
        Me.btnAddgst.Name = "btnAddgst"
        Me.btnAddgst.Size = New System.Drawing.Size(93, 35)
        Me.btnAddgst.TabIndex = 3
        Me.btnAddgst.Text = "OK"
        Me.btnAddgst.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAddgst.UseVisualStyleBackColor = False
        '
        'btncancelgst
        '
        Me.btncancelgst.BackColor = System.Drawing.Color.SteelBlue
        Me.btncancelgst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncancelgst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancelgst.ForeColor = System.Drawing.Color.White
        Me.btncancelgst.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btncancelgst.Location = New System.Drawing.Point(255, 47)
        Me.btncancelgst.Name = "btncancelgst"
        Me.btncancelgst.Size = New System.Drawing.Size(93, 35)
        Me.btncancelgst.TabIndex = 345449
        Me.btncancelgst.Text = "Cancel"
        Me.btncancelgst.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancelgst.UseVisualStyleBackColor = False
        '
        'txtCgst
        '
        Me.txtCgst.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCgst.Location = New System.Drawing.Point(60, 10)
        Me.txtCgst.MaxLength = 15
        Me.txtCgst.Name = "txtCgst"
        Me.txtCgst.Size = New System.Drawing.Size(51, 21)
        Me.txtCgst.TabIndex = 0
        Me.txtCgst.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCGST
        '
        Me.lblCGST.AutoSize = True
        Me.lblCGST.BackColor = System.Drawing.Color.Transparent
        Me.lblCGST.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCGST.ForeColor = System.Drawing.Color.White
        Me.lblCGST.Location = New System.Drawing.Point(15, 10)
        Me.lblCGST.Name = "lblCGST"
        Me.lblCGST.Size = New System.Drawing.Size(40, 13)
        Me.lblCGST.TabIndex = 345440
        Me.lblCGST.Text = "CGST"
        '
        'btnfind
        '
        Me.btnfind.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnfind.BackColor = System.Drawing.Color.SteelBlue
        Me.btnfind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnfind.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnfind.ForeColor = System.Drawing.Color.White
        Me.btnfind.Location = New System.Drawing.Point(394, 616)
        Me.btnfind.Name = "btnfind"
        Me.btnfind.Size = New System.Drawing.Size(66, 23)
        Me.btnfind.TabIndex = 345497
        Me.btnfind.Text = "Search"
        Me.btnfind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnfind.UseVisualStyleBackColor = False
        '
        'cmbcolms
        '
        Me.cmbcolms.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbcolms.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcolms.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbcolms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcolms.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcolms.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcolms.Location = New System.Drawing.Point(265, 595)
        Me.cmbcolms.Name = "cmbcolms"
        Me.cmbcolms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbcolms.Size = New System.Drawing.Size(123, 22)
        Me.cmbcolms.TabIndex = 345495
        Me.cmbcolms.TabStop = False
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
        Me.txtSeq.Location = New System.Drawing.Point(265, 619)
        Me.txtSeq.MaxLength = 50
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(123, 20)
        Me.txtSeq.TabIndex = 345496
        '
        'grdItemInfo
        '
        Me.grdItemInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItemInfo.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.grdItemInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItemInfo.Location = New System.Drawing.Point(6, 6)
        Me.grdItemInfo.Name = "grdItemInfo"
        Me.grdItemInfo.Size = New System.Drawing.Size(534, 79)
        Me.grdItemInfo.TabIndex = 345498
        '
        'btnprint
        '
        Me.btnprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnprint.BackColor = System.Drawing.Color.SteelBlue
        Me.btnprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprint.ForeColor = System.Drawing.Color.White
        Me.btnprint.Location = New System.Drawing.Point(1170, 270)
        Me.btnprint.Name = "btnprint"
        Me.btnprint.Size = New System.Drawing.Size(100, 35)
        Me.btnprint.TabIndex = 345500
        Me.btnprint.TabStop = False
        Me.btnprint.Text = "Print"
        Me.btnprint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnprint.UseVisualStyleBackColor = False
        '
        'cmbDos
        '
        Me.cmbDos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDos.FormattingEnabled = True
        Me.cmbDos.Items.AddRange(New Object() {"QTI"})
        Me.cmbDos.Location = New System.Drawing.Point(8, 16)
        Me.cmbDos.Name = "cmbDos"
        Me.cmbDos.Size = New System.Drawing.Size(68, 23)
        Me.cmbDos.TabIndex = 123
        Me.cmbDos.TabStop = False
        '
        'txtDOLst
        '
        Me.txtDOLst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDOLst.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDOLst.Location = New System.Drawing.Point(8, 43)
        Me.txtDOLst.Name = "txtDOLst"
        Me.txtDOLst.Size = New System.Drawing.Size(134, 23)
        Me.txtDOLst.TabIndex = 116
        '
        'btnshow
        '
        Me.btnshow.BackColor = System.Drawing.SystemColors.Control
        Me.btnshow.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnshow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnshow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnshow.Image = CType(resources.GetObject("btnshow.Image"), System.Drawing.Image)
        Me.btnshow.Location = New System.Drawing.Point(80, 13)
        Me.btnshow.Name = "btnshow"
        Me.btnshow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnshow.Size = New System.Drawing.Size(30, 26)
        Me.btnshow.TabIndex = 121
        Me.btnshow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnshow.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(465, 528)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(552, 113)
        Me.TabControl1.TabIndex = 345504
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.grdItemInfo)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(544, 87)
        Me.TabPage3.TabIndex = 0
        Me.TabPage3.Text = "Item Info"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.GroupBox1)
        Me.TabPage4.Controls.Add(Me.btnimport)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(544, 87)
        Me.TabPage4.TabIndex = 1
        Me.TabPage4.Text = "Import"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbDos)
        Me.GroupBox1.Controls.Add(Me.txtDOLst)
        Me.GroupBox1.Controls.Add(Me.btnshow)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(150, 71)
        Me.GroupBox1.TabIndex = 345508
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Document"
        '
        'btncancel
        '
        Me.btncancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btncancel.BackColor = System.Drawing.Color.SteelBlue
        Me.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancel.ForeColor = System.Drawing.Color.White
        Me.btncancel.Location = New System.Drawing.Point(6, 528)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(117, 32)
        Me.btncancel.TabIndex = 345505
        Me.btncancel.Text = "Cancel"
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = False
        '
        'txtinvno
        '
        Me.txtinvno.AcceptsReturn = True
        Me.txtinvno.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtinvno.BackColor = System.Drawing.SystemColors.Window
        Me.txtinvno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtinvno.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtinvno.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtinvno.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtinvno.Location = New System.Drawing.Point(206, 540)
        Me.txtinvno.MaxLength = 50
        Me.txtinvno.Name = "txtinvno"
        Me.txtinvno.ReadOnly = True
        Me.txtinvno.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtinvno.Size = New System.Drawing.Size(145, 20)
        Me.txtinvno.TabIndex = 345506
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(129, 544)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(74, 13)
        Me.Label28.TabIndex = 345507
        Me.Label28.Text = "Transfered To"
        '
        'ldtimer
        '
        Me.ldtimer.Interval = 200
        '
        'SalesOrderFrm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1276, 644)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.txtinvno)
        Me.Controls.Add(Me.btncancel)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.plsrch)
        Me.Controls.Add(Me.txtsubject)
        Me.Controls.Add(Me.tbgst)
        Me.Controls.Add(Me.btnprint)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblNetAmt)
        Me.Controls.Add(Me.btnfind)
        Me.Controls.Add(Me.txtAttn)
        Me.Controls.Add(Me.cmbcolms)
        Me.Controls.Add(Me.txtSeq)
        Me.Controls.Add(Me.lblInvoices)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.lbllimit)
        Me.Controls.Add(Me.lblbalance)
        Me.Controls.Add(Me.txtPPrefix)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.numPrintVchr)
        Me.Controls.Add(Me.chkFormat)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.pnlCmn)
        Me.Controls.Add(Me.btnModify)
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnupdate)
        Me.Controls.Add(Me.grdVoucher)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "SalesOrderFrm"
        Me.Text = "Sales Order"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlCmn.ResumeLayout(False)
        Me.pnlCmn.PerformLayout()
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
        Me.tbgst.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.grdItemInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
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
    Friend WithEvents lblNetAmt As System.Windows.Forms.Label
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents btnimport As System.Windows.Forms.Button
    Friend WithEvents chkremovealert As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents txtfcrt As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbfc As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbsalesman As System.Windows.Forms.ComboBox
    Friend WithEvents chkcal As System.Windows.Forms.CheckBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtcustAddress As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtdp As System.Windows.Forms.TextBox
    Friend WithEvents btntax As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lbltax As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtPPrefix As System.Windows.Forms.TextBox
    Friend WithEvents lblbalance As System.Windows.Forms.Label
    Friend WithEvents lbllimit As System.Windows.Forms.Label
    Friend WithEvents lblInvoices As System.Windows.Forms.Label
    Friend WithEvents lblstatecode As System.Windows.Forms.Label
    Friend WithEvents tbgst As System.Windows.Forms.TabControl
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents txtIgstAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtSgstAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtCgstAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtIgst As System.Windows.Forms.TextBox
    Friend WithEvents lblIgst As System.Windows.Forms.Label
    Friend WithEvents txtSgst As System.Windows.Forms.TextBox
    Friend WithEvents lblSgst As System.Windows.Forms.Label
    Friend WithEvents btnAddgst As System.Windows.Forms.Button
    Friend WithEvents btncancelgst As System.Windows.Forms.Button
    Friend WithEvents txtCgst As System.Windows.Forms.TextBox
    Friend WithEvents lblCGST As System.Windows.Forms.Label
    Friend WithEvents txtroundOff As System.Windows.Forms.TextBox
    Friend WithEvents cmbsign As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnfind As System.Windows.Forms.Button
    Public WithEvents cmbcolms As System.Windows.Forms.ComboBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Friend WithEvents grdItemInfo As System.Windows.Forms.DataGridView
    Friend WithEvents btnprint As System.Windows.Forms.Button
    Friend WithEvents cmbDos As System.Windows.Forms.ComboBox
    Friend WithEvents txtDOLst As System.Windows.Forms.TextBox
    Public WithEvents btnshow As System.Windows.Forms.Button
    Friend WithEvents chktaxInv As System.Windows.Forms.CheckBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents lblcess As System.Windows.Forms.Label
    Friend WithEvents lblgstn As System.Windows.Forms.Label
    Friend WithEvents txtAttn As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtsubject As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chkautoroundOff As System.Windows.Forms.CheckBox
    Friend WithEvents txtDescr As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpduedate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btninvoice As System.Windows.Forms.Button
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Friend WithEvents rdodealer As System.Windows.Forms.RadioButton
    Friend WithEvents rdows As System.Windows.Forms.RadioButton
    Friend WithEvents rdoretail As System.Windows.Forms.RadioButton
    Public WithEvents txtinvno As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents ldtimer As System.Windows.Forms.Timer
    Friend WithEvents rdoother As System.Windows.Forms.RadioButton
End Class
