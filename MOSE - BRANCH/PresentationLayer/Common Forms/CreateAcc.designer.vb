<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CreateAcc
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
        Me.chkreconcil = New System.Windows.Forms.CheckBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtbankcode = New System.Windows.Forms.TextBox
        Me.lblbankcode = New System.Windows.Forms.Label
        Me.txtmalayalam = New System.Windows.Forms.TextBox
        Me.cmbBr = New System.Windows.Forms.ComboBox
        Me.txtphone = New System.Windows.Forms.TextBox
        Me.lblCap8 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtcontactname = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtwebsite = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtemail = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtFax = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtAddr3 = New System.Windows.Forms.TextBox
        Me.txtAddr2 = New System.Windows.Forms.TextBox
        Me.lblCap4 = New System.Windows.Forms.Label
        Me.txtAddr1 = New System.Windows.Forms.TextBox
        Me.lblCap5 = New System.Windows.Forms.Label
        Me.txtAddr0 = New System.Windows.Forms.TextBox
        Me.lblCap6 = New System.Windows.Forms.Label
        Me.lblCap7 = New System.Windows.Forms.Label
        Me.txtRec1 = New System.Windows.Forms.TextBox
        Me.txtRec0 = New System.Windows.Forms.TextBox
        Me.lblCap1 = New System.Windows.Forms.Label
        Me.cmbAccGroup = New System.Windows.Forms.ComboBox
        Me.cmdOk = New System.Windows.Forms.Button
        Me.btnaddnew = New System.Windows.Forms.Button
        Me.btnmodify = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.lblclosing = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.btnrem = New System.Windows.Forms.Button
        Me.btnitmAdd = New System.Windows.Forms.Button
        Me.btnAcSrch = New System.Windows.Forms.Button
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.cmbAcOrder = New System.Windows.Forms.ComboBox
        Me.txtAcSearch = New System.Windows.Forms.TextBox
        Me.btnadd = New System.Windows.Forms.Button
        Me.numopnBal = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnremove = New System.Windows.Forms.Button
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.grdItem = New System.Windows.Forms.DataGridView
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label7 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.btnclose = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmbtitle = New System.Windows.Forms.ComboBox
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkreconcil)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.txtbankcode)
        Me.Panel1.Controls.Add(Me.lblbankcode)
        Me.Panel1.Controls.Add(Me.txtmalayalam)
        Me.Panel1.Controls.Add(Me.cmbBr)
        Me.Panel1.Controls.Add(Me.txtphone)
        Me.Panel1.Controls.Add(Me.lblCap8)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txtcontactname)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtwebsite)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtemail)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtFax)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtAddr3)
        Me.Panel1.Controls.Add(Me.txtAddr2)
        Me.Panel1.Controls.Add(Me.lblCap4)
        Me.Panel1.Controls.Add(Me.txtAddr1)
        Me.Panel1.Controls.Add(Me.lblCap5)
        Me.Panel1.Controls.Add(Me.txtAddr0)
        Me.Panel1.Controls.Add(Me.lblCap6)
        Me.Panel1.Controls.Add(Me.lblCap7)
        Me.Panel1.Controls.Add(Me.txtRec1)
        Me.Panel1.Controls.Add(Me.txtRec0)
        Me.Panel1.Enabled = False
        Me.Panel1.Location = New System.Drawing.Point(6, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(366, 300)
        Me.Panel1.TabIndex = 0
        '
        'chkreconcil
        '
        Me.chkreconcil.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkreconcil.AutoSize = True
        Me.chkreconcil.BackColor = System.Drawing.Color.Transparent
        Me.chkreconcil.ForeColor = System.Drawing.Color.Black
        Me.chkreconcil.Location = New System.Drawing.Point(186, 273)
        Me.chkreconcil.Name = "chkreconcil"
        Me.chkreconcil.Size = New System.Drawing.Size(145, 17)
        Me.chkreconcil.TabIndex = 345471
        Me.chkreconcil.Text = "Reconcil Account [Debit]"
        Me.chkreconcil.UseVisualStyleBackColor = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(7, 48)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(57, 14)
        Me.Label17.TabIndex = 345470
        Me.Label17.Text = "Malayalam"
        '
        'txtbankcode
        '
        Me.txtbankcode.AcceptsReturn = True
        Me.txtbankcode.BackColor = System.Drawing.SystemColors.Window
        Me.txtbankcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtbankcode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtbankcode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbankcode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtbankcode.Location = New System.Drawing.Point(306, 179)
        Me.txtbankcode.MaxLength = 30
        Me.txtbankcode.Name = "txtbankcode"
        Me.txtbankcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtbankcode.Size = New System.Drawing.Size(54, 20)
        Me.txtbankcode.TabIndex = 8
        '
        'lblbankcode
        '
        Me.lblbankcode.AutoSize = True
        Me.lblbankcode.BackColor = System.Drawing.Color.Transparent
        Me.lblbankcode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblbankcode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbankcode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblbankcode.Location = New System.Drawing.Point(246, 183)
        Me.lblbankcode.Name = "lblbankcode"
        Me.lblbankcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblbankcode.Size = New System.Drawing.Size(59, 14)
        Me.lblbankcode.TabIndex = 71
        Me.lblbankcode.Text = "Bank Code"
        '
        'txtmalayalam
        '
        Me.txtmalayalam.AcceptsReturn = True
        Me.txtmalayalam.BackColor = System.Drawing.SystemColors.Window
        Me.txtmalayalam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtmalayalam.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtmalayalam.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmalayalam.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtmalayalam.Location = New System.Drawing.Point(75, 44)
        Me.txtmalayalam.MaxLength = 150
        Me.txtmalayalam.Name = "txtmalayalam"
        Me.txtmalayalam.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtmalayalam.Size = New System.Drawing.Size(285, 25)
        Me.txtmalayalam.TabIndex = 2
        '
        'cmbBr
        '
        Me.cmbBr.BackColor = System.Drawing.SystemColors.Window
        Me.cmbBr.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbBr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBr.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBr.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbBr.Location = New System.Drawing.Point(75, 270)
        Me.cmbBr.Name = "cmbBr"
        Me.cmbBr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbBr.Size = New System.Drawing.Size(105, 22)
        Me.cmbBr.TabIndex = 50
        Me.cmbBr.Visible = False
        '
        'txtphone
        '
        Me.txtphone.AcceptsReturn = True
        Me.txtphone.BackColor = System.Drawing.SystemColors.Window
        Me.txtphone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtphone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtphone.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtphone.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtphone.Location = New System.Drawing.Point(75, 157)
        Me.txtphone.MaxLength = 30
        Me.txtphone.Name = "txtphone"
        Me.txtphone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtphone.Size = New System.Drawing.Size(285, 20)
        Me.txtphone.TabIndex = 6
        '
        'lblCap8
        '
        Me.lblCap8.AutoSize = True
        Me.lblCap8.BackColor = System.Drawing.Color.Transparent
        Me.lblCap8.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap8.Location = New System.Drawing.Point(18, 272)
        Me.lblCap8.Name = "lblCap8"
        Me.lblCap8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap8.Size = New System.Drawing.Size(42, 14)
        Me.lblCap8.TabIndex = 51
        Me.lblCap8.Text = "&Branch"
        Me.lblCap8.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(14, 162)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(37, 14)
        Me.Label13.TabIndex = 69
        Me.Label13.Text = "Phone"
        '
        'txtcontactname
        '
        Me.txtcontactname.AcceptsReturn = True
        Me.txtcontactname.BackColor = System.Drawing.SystemColors.Window
        Me.txtcontactname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcontactname.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtcontactname.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcontactname.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtcontactname.Location = New System.Drawing.Point(94, 246)
        Me.txtcontactname.MaxLength = 150
        Me.txtcontactname.Name = "txtcontactname"
        Me.txtcontactname.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtcontactname.Size = New System.Drawing.Size(266, 20)
        Me.txtcontactname.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(14, 249)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(74, 14)
        Me.Label6.TabIndex = 63
        Me.Label6.Text = "Contact Name"
        '
        'txtwebsite
        '
        Me.txtwebsite.AcceptsReturn = True
        Me.txtwebsite.BackColor = System.Drawing.SystemColors.Window
        Me.txtwebsite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtwebsite.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtwebsite.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtwebsite.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtwebsite.Location = New System.Drawing.Point(75, 224)
        Me.txtwebsite.MaxLength = 150
        Me.txtwebsite.Name = "txtwebsite"
        Me.txtwebsite.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtwebsite.Size = New System.Drawing.Size(285, 20)
        Me.txtwebsite.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(14, 227)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(46, 14)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "Website"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(106, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(78, 14)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "Account Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(14, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(76, 14)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "Account Code"
        '
        'txtemail
        '
        Me.txtemail.AcceptsReturn = True
        Me.txtemail.BackColor = System.Drawing.SystemColors.Window
        Me.txtemail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtemail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtemail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtemail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtemail.Location = New System.Drawing.Point(75, 202)
        Me.txtemail.MaxLength = 150
        Me.txtemail.Name = "txtemail"
        Me.txtemail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtemail.Size = New System.Drawing.Size(285, 20)
        Me.txtemail.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(14, 207)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(31, 14)
        Me.Label2.TabIndex = 57
        Me.Label2.Text = "Email"
        '
        'txtFax
        '
        Me.txtFax.AcceptsReturn = True
        Me.txtFax.BackColor = System.Drawing.SystemColors.Window
        Me.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFax.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFax.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFax.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFax.Location = New System.Drawing.Point(75, 179)
        Me.txtFax.MaxLength = 30
        Me.txtFax.Name = "txtFax"
        Me.txtFax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFax.Size = New System.Drawing.Size(167, 20)
        Me.txtFax.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(14, 185)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(25, 14)
        Me.Label1.TabIndex = 55
        Me.Label1.Text = "Fax"
        '
        'txtAddr3
        '
        Me.txtAddr3.AcceptsReturn = True
        Me.txtAddr3.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddr3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddr3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddr3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddr3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddr3.Location = New System.Drawing.Point(75, 135)
        Me.txtAddr3.MaxLength = 150
        Me.txtAddr3.Name = "txtAddr3"
        Me.txtAddr3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddr3.Size = New System.Drawing.Size(285, 20)
        Me.txtAddr3.TabIndex = 5
        '
        'txtAddr2
        '
        Me.txtAddr2.AcceptsReturn = True
        Me.txtAddr2.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddr2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddr2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddr2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddr2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddr2.Location = New System.Drawing.Point(75, 114)
        Me.txtAddr2.MaxLength = 150
        Me.txtAddr2.Name = "txtAddr2"
        Me.txtAddr2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddr2.Size = New System.Drawing.Size(285, 20)
        Me.txtAddr2.TabIndex = 5
        '
        'lblCap4
        '
        Me.lblCap4.AutoSize = True
        Me.lblCap4.BackColor = System.Drawing.Color.Transparent
        Me.lblCap4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap4.Location = New System.Drawing.Point(14, 75)
        Me.lblCap4.Name = "lblCap4"
        Me.lblCap4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap4.Size = New System.Drawing.Size(58, 14)
        Me.lblCap4.TabIndex = 46
        Me.lblCap4.Text = "Address 1"
        '
        'txtAddr1
        '
        Me.txtAddr1.AcceptsReturn = True
        Me.txtAddr1.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddr1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddr1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddr1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddr1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddr1.Location = New System.Drawing.Point(75, 93)
        Me.txtAddr1.MaxLength = 150
        Me.txtAddr1.Name = "txtAddr1"
        Me.txtAddr1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddr1.Size = New System.Drawing.Size(285, 20)
        Me.txtAddr1.TabIndex = 4
        '
        'lblCap5
        '
        Me.lblCap5.AutoSize = True
        Me.lblCap5.BackColor = System.Drawing.Color.Transparent
        Me.lblCap5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap5.Location = New System.Drawing.Point(14, 96)
        Me.lblCap5.Name = "lblCap5"
        Me.lblCap5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap5.Size = New System.Drawing.Size(58, 14)
        Me.lblCap5.TabIndex = 47
        Me.lblCap5.Text = "Address 2"
        '
        'txtAddr0
        '
        Me.txtAddr0.AcceptsReturn = True
        Me.txtAddr0.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddr0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddr0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddr0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddr0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddr0.Location = New System.Drawing.Point(75, 72)
        Me.txtAddr0.MaxLength = 150
        Me.txtAddr0.Name = "txtAddr0"
        Me.txtAddr0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddr0.Size = New System.Drawing.Size(285, 20)
        Me.txtAddr0.TabIndex = 3
        '
        'lblCap6
        '
        Me.lblCap6.AutoSize = True
        Me.lblCap6.BackColor = System.Drawing.Color.Transparent
        Me.lblCap6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap6.Location = New System.Drawing.Point(14, 117)
        Me.lblCap6.Name = "lblCap6"
        Me.lblCap6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap6.Size = New System.Drawing.Size(58, 14)
        Me.lblCap6.TabIndex = 48
        Me.lblCap6.Text = "Address 3"
        '
        'lblCap7
        '
        Me.lblCap7.AutoSize = True
        Me.lblCap7.BackColor = System.Drawing.Color.Transparent
        Me.lblCap7.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap7.Location = New System.Drawing.Point(14, 138)
        Me.lblCap7.Name = "lblCap7"
        Me.lblCap7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap7.Size = New System.Drawing.Size(58, 14)
        Me.lblCap7.TabIndex = 49
        Me.lblCap7.Text = "Address 4"
        '
        'txtRec1
        '
        Me.txtRec1.AcceptsReturn = True
        Me.txtRec1.BackColor = System.Drawing.SystemColors.Window
        Me.txtRec1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRec1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRec1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRec1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRec1.Location = New System.Drawing.Point(108, 22)
        Me.txtRec1.MaxLength = 100
        Me.txtRec1.Name = "txtRec1"
        Me.txtRec1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRec1.Size = New System.Drawing.Size(252, 20)
        Me.txtRec1.TabIndex = 1
        '
        'txtRec0
        '
        Me.txtRec0.AcceptsReturn = True
        Me.txtRec0.BackColor = System.Drawing.SystemColors.Window
        Me.txtRec0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRec0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRec0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRec0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRec0.Location = New System.Drawing.Point(14, 22)
        Me.txtRec0.MaxLength = 10
        Me.txtRec0.Name = "txtRec0"
        Me.txtRec0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRec0.Size = New System.Drawing.Size(89, 20)
        Me.txtRec0.TabIndex = 0
        '
        'lblCap1
        '
        Me.lblCap1.AutoSize = True
        Me.lblCap1.BackColor = System.Drawing.Color.Transparent
        Me.lblCap1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap1.Location = New System.Drawing.Point(268, 42)
        Me.lblCap1.Name = "lblCap1"
        Me.lblCap1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap1.Size = New System.Drawing.Size(126, 14)
        Me.lblCap1.TabIndex = 27
        Me.lblCap1.Text = "Choose Account &Group :"
        '
        'cmbAccGroup
        '
        Me.cmbAccGroup.BackColor = System.Drawing.SystemColors.Window
        Me.cmbAccGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbAccGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAccGroup.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAccGroup.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbAccGroup.Location = New System.Drawing.Point(268, 60)
        Me.cmbAccGroup.Name = "cmbAccGroup"
        Me.cmbAccGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbAccGroup.Size = New System.Drawing.Size(345, 22)
        Me.cmbAccGroup.TabIndex = 28
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.Enabled = False
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.Color.White
        Me.cmdOk.Location = New System.Drawing.Point(285, 311)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(81, 35)
        Me.cmdOk.TabIndex = 12
        Me.cmdOk.Text = "&Update"
        Me.cmdOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'btnaddnew
        '
        Me.btnaddnew.BackColor = System.Drawing.Color.SteelBlue
        Me.btnaddnew.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnaddnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnaddnew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnaddnew.ForeColor = System.Drawing.Color.White
        Me.btnaddnew.Location = New System.Drawing.Point(37, 311)
        Me.btnaddnew.Name = "btnaddnew"
        Me.btnaddnew.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnaddnew.Size = New System.Drawing.Size(81, 35)
        Me.btnaddnew.TabIndex = 44
        Me.btnaddnew.Text = "&Add New"
        Me.btnaddnew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnaddnew.UseVisualStyleBackColor = False
        '
        'btnmodify
        '
        Me.btnmodify.BackColor = System.Drawing.Color.SteelBlue
        Me.btnmodify.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnmodify.Enabled = False
        Me.btnmodify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnmodify.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnmodify.ForeColor = System.Drawing.Color.White
        Me.btnmodify.Location = New System.Drawing.Point(120, 311)
        Me.btnmodify.Name = "btnmodify"
        Me.btnmodify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnmodify.Size = New System.Drawing.Size(81, 35)
        Me.btnmodify.TabIndex = 43
        Me.btnmodify.Text = "&Modify"
        Me.btnmodify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnmodify.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 88)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(992, 407)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lblclosing)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.Panel4)
        Me.TabPage1.Controls.Add(Me.btnadd)
        Me.TabPage1.Controls.Add(Me.numopnBal)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.btnremove)
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Controls.Add(Me.btnaddnew)
        Me.TabPage1.Controls.Add(Me.btnmodify)
        Me.TabPage1.Controls.Add(Me.cmdOk)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(984, 381)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Details"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'lblclosing
        '
        Me.lblclosing.AutoSize = True
        Me.lblclosing.BackColor = System.Drawing.Color.Transparent
        Me.lblclosing.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclosing.Location = New System.Drawing.Point(750, 12)
        Me.lblclosing.Name = "lblclosing"
        Me.lblclosing.Size = New System.Drawing.Size(35, 15)
        Me.lblclosing.TabIndex = 345468
        Me.lblclosing.Text = "0.00"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(641, 11)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(96, 15)
        Me.Label10.TabIndex = 345467
        Me.Label10.Text = "Closing Balance"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnrem)
        Me.Panel4.Controls.Add(Me.btnitmAdd)
        Me.Panel4.Controls.Add(Me.btnAcSrch)
        Me.Panel4.Controls.Add(Me.grdVoucher)
        Me.Panel4.Controls.Add(Me.cmbAcOrder)
        Me.Panel4.Controls.Add(Me.txtAcSearch)
        Me.Panel4.Location = New System.Drawing.Point(380, 37)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(598, 268)
        Me.Panel4.TabIndex = 345466
        Me.Panel4.Visible = False
        '
        'btnrem
        '
        Me.btnrem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnrem.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrem.ForeColor = System.Drawing.Color.White
        Me.btnrem.Location = New System.Drawing.Point(539, 239)
        Me.btnrem.Name = "btnrem"
        Me.btnrem.Size = New System.Drawing.Size(55, 24)
        Me.btnrem.TabIndex = 345448
        Me.btnrem.Text = "Rem"
        Me.btnrem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrem.UseVisualStyleBackColor = False
        '
        'btnitmAdd
        '
        Me.btnitmAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnitmAdd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnitmAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnitmAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnitmAdd.ForeColor = System.Drawing.Color.White
        Me.btnitmAdd.Location = New System.Drawing.Point(482, 239)
        Me.btnitmAdd.Name = "btnitmAdd"
        Me.btnitmAdd.Size = New System.Drawing.Size(55, 24)
        Me.btnitmAdd.TabIndex = 345447
        Me.btnitmAdd.Text = "Add"
        Me.btnitmAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnitmAdd.UseVisualStyleBackColor = False
        '
        'btnAcSrch
        '
        Me.btnAcSrch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAcSrch.BackColor = System.Drawing.SystemColors.Control
        Me.btnAcSrch.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAcSrch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAcSrch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAcSrch.Location = New System.Drawing.Point(345, 239)
        Me.btnAcSrch.Name = "btnAcSrch"
        Me.btnAcSrch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAcSrch.Size = New System.Drawing.Size(80, 23)
        Me.btnAcSrch.TabIndex = 510
        Me.btnAcSrch.Text = "Search"
        Me.btnAcSrch.UseVisualStyleBackColor = False
        '
        'grdVoucher
        '
        Me.grdVoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdVoucher.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.grdVoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdVoucher.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdVoucher.Location = New System.Drawing.Point(3, 6)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(589, 229)
        Me.grdVoucher.TabIndex = 502
        Me.grdVoucher.TabStop = False
        '
        'cmbAcOrder
        '
        Me.cmbAcOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbAcOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbAcOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbAcOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAcOrder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAcOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbAcOrder.Location = New System.Drawing.Point(1, 239)
        Me.cmbAcOrder.Name = "cmbAcOrder"
        Me.cmbAcOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbAcOrder.Size = New System.Drawing.Size(128, 22)
        Me.cmbAcOrder.TabIndex = 508
        '
        'txtAcSearch
        '
        Me.txtAcSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtAcSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAcSearch.Location = New System.Drawing.Point(135, 240)
        Me.txtAcSearch.Name = "txtAcSearch"
        Me.txtAcSearch.Size = New System.Drawing.Size(207, 20)
        Me.txtAcSearch.TabIndex = 509
        '
        'btnadd
        '
        Me.btnadd.Enabled = False
        Me.btnadd.Image = Global.SMSMP.My.Resources.Resources.button_edit
        Me.btnadd.Location = New System.Drawing.Point(597, 9)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(40, 24)
        Me.btnadd.TabIndex = 345465
        Me.btnadd.TabStop = False
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'numopnBal
        '
        Me.numopnBal.BackColor = System.Drawing.Color.White
        Me.numopnBal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numopnBal.Enabled = False
        Me.numopnBal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numopnBal.Location = New System.Drawing.Point(473, 10)
        Me.numopnBal.MaxLength = 60
        Me.numopnBal.Name = "numopnBal"
        Me.numopnBal.Size = New System.Drawing.Size(121, 21)
        Me.numopnBal.TabIndex = 345463
        Me.numopnBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(372, 12)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(95, 15)
        Me.Label9.TabIndex = 345464
        Me.Label9.Text = "Opning Balance"
        '
        'btnremove
        '
        Me.btnremove.BackColor = System.Drawing.Color.SteelBlue
        Me.btnremove.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnremove.Enabled = False
        Me.btnremove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnremove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnremove.ForeColor = System.Drawing.Color.White
        Me.btnremove.Location = New System.Drawing.Point(203, 311)
        Me.btnremove.Name = "btnremove"
        Me.btnremove.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnremove.Size = New System.Drawing.Size(81, 35)
        Me.btnremove.TabIndex = 52
        Me.btnremove.Text = "&Remove"
        Me.btnremove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnremove.UseVisualStyleBackColor = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.chkSearch)
        Me.TabPage2.Controls.Add(Me.grdItem)
        Me.TabPage2.Controls.Add(Me.txtSeq)
        Me.TabPage2.Controls.Add(Me.cmbOrder)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(984, 381)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Search"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.Checked = True
        Me.chkSearch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(419, 282)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345416
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
        '
        'grdItem
        '
        Me.grdItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItem.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItem.Location = New System.Drawing.Point(6, 6)
        Me.grdItem.Name = "grdItem"
        Me.grdItem.Size = New System.Drawing.Size(972, 270)
        Me.grdItem.TabIndex = 345413
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
        Me.txtSeq.Location = New System.Drawing.Point(178, 283)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(235, 20)
        Me.txtSeq.TabIndex = 345415
        '
        'cmbOrder
        '
        Me.cmbOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOrder.Location = New System.Drawing.Point(8, 282)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOrder.Size = New System.Drawing.Size(166, 22)
        Me.cmbOrder.TabIndex = 345414
        Me.cmbOrder.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1014, 41)
        Me.Panel2.TabIndex = 345403
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.Font = New System.Drawing.Font("Goudy Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(39, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(139, 21)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "LEDGER MASTER"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.tn_entryform
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(8, 7)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(29, 26)
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ForeColor = System.Drawing.Color.White
        Me.btnclose.Location = New System.Drawing.Point(922, 501)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(82, 35)
        Me.btnclose.TabIndex = 345404
        Me.btnclose.Text = "E&xit"
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(9, 44)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(127, 13)
        Me.Label8.TabIndex = 345406
        Me.Label8.Text = "Choose Account Title"
        '
        'cmbtitle
        '
        Me.cmbtitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtitle.FormattingEnabled = True
        Me.cmbtitle.Location = New System.Drawing.Point(12, 60)
        Me.cmbtitle.Name = "cmbtitle"
        Me.cmbtitle.Size = New System.Drawing.Size(252, 21)
        Me.cmbtitle.TabIndex = 345405
        '
        'CreateAcc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1014, 548)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cmbtitle)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.cmbAccGroup)
        Me.Controls.Add(Me.lblCap1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CreateAcc"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ledger Master"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents lblCap1 As System.Windows.Forms.Label
    Public WithEvents cmbBr As System.Windows.Forms.ComboBox
    Public WithEvents txtAddr3 As System.Windows.Forms.TextBox
    Public WithEvents txtAddr2 As System.Windows.Forms.TextBox
    Public WithEvents lblCap4 As System.Windows.Forms.Label
    Public WithEvents txtAddr1 As System.Windows.Forms.TextBox
    Public WithEvents lblCap5 As System.Windows.Forms.Label
    Public WithEvents txtAddr0 As System.Windows.Forms.TextBox
    Public WithEvents lblCap6 As System.Windows.Forms.Label
    Public WithEvents lblCap7 As System.Windows.Forms.Label
    Public WithEvents lblCap8 As System.Windows.Forms.Label
    Public WithEvents btnaddnew As System.Windows.Forms.Button
    Public WithEvents btnmodify As System.Windows.Forms.Button
    Public WithEvents txtRec1 As System.Windows.Forms.TextBox
    Public WithEvents txtRec0 As System.Windows.Forms.TextBox
    Public WithEvents cmbAccGroup As System.Windows.Forms.ComboBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Public WithEvents txtemail As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtFax As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents txtcontactname As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents txtwebsite As System.Windows.Forms.TextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents txtphone As System.Windows.Forms.TextBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents grdItem As System.Windows.Forms.DataGridView
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Public WithEvents btnremove As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnrem As System.Windows.Forms.Button
    Friend WithEvents btnitmAdd As System.Windows.Forms.Button
    Public WithEvents btnAcSrch As System.Windows.Forms.Button
    Public WithEvents cmbAcOrder As System.Windows.Forms.ComboBox
    Friend WithEvents txtAcSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents numopnBal As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents grdVoucher As System.Windows.Forms.DataGridView
    Public WithEvents txtbankcode As System.Windows.Forms.TextBox
    Public WithEvents lblbankcode As System.Windows.Forms.Label
    Friend WithEvents lblclosing As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbtitle As System.Windows.Forms.ComboBox
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents txtmalayalam As System.Windows.Forms.TextBox
    Friend WithEvents chkreconcil As System.Windows.Forms.CheckBox
End Class
