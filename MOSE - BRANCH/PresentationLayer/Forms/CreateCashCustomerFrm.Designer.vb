<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CreateCashCustomerFrm
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
        Me.txtphone = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtemail = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtAddr2 = New System.Windows.Forms.TextBox
        Me.lblCap4 = New System.Windows.Forms.Label
        Me.txtAddr1 = New System.Windows.Forms.TextBox
        Me.lblCap5 = New System.Windows.Forms.Label
        Me.txtAddr0 = New System.Windows.Forms.TextBox
        Me.lblCap6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtname = New System.Windows.Forms.TextBox
        Me.txtmemberid = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtgifvr = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtsearch = New System.Windows.Forms.TextBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.txtremarks = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.grpedit = New System.Windows.Forms.GroupBox
        Me.txtgstn = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblcard = New System.Windows.Forms.Label
        Me.lblcardcap = New System.Windows.Forms.Label
        Me.txtaccount = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnclear = New System.Windows.Forms.Button
        Me.btnsave = New System.Windows.Forms.Button
        Me.grdlist = New System.Windows.Forms.DataGridView
        Me.btnselect = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.lblcap = New System.Windows.Forms.Label
        Me.btnedit = New System.Windows.Forms.Button
        Me.btnnew = New System.Windows.Forms.Button
        Me.plws = New System.Windows.Forms.Panel
        Me.rdocard = New System.Windows.Forms.RadioButton
        Me.rdocustomer = New System.Windows.Forms.RadioButton
        Me.btnexport = New System.Windows.Forms.Button
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.btndelete = New System.Windows.Forms.Button
        Me.grpedit.SuspendLayout()
        CType(Me.grdlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plws.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtphone
        '
        Me.txtphone.AcceptsReturn = True
        Me.txtphone.BackColor = System.Drawing.SystemColors.Window
        Me.txtphone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtphone.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtphone.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtphone.Location = New System.Drawing.Point(105, 14)
        Me.txtphone.MaxLength = 30
        Me.txtphone.Name = "txtphone"
        Me.txtphone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtphone.Size = New System.Drawing.Size(267, 20)
        Me.txtphone.TabIndex = 0
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(57, 12)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(43, 15)
        Me.Label13.TabIndex = 345535
        Me.Label13.Text = "Phone"
        '
        'txtemail
        '
        Me.txtemail.AcceptsReturn = True
        Me.txtemail.BackColor = System.Drawing.SystemColors.Window
        Me.txtemail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtemail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtemail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtemail.Location = New System.Drawing.Point(105, 128)
        Me.txtemail.MaxLength = 150
        Me.txtemail.Name = "txtemail"
        Me.txtemail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtemail.Size = New System.Drawing.Size(267, 20)
        Me.txtemail.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(64, 126)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(39, 15)
        Me.Label6.TabIndex = 345534
        Me.Label6.Text = "Email"
        '
        'txtAddr2
        '
        Me.txtAddr2.AcceptsReturn = True
        Me.txtAddr2.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddr2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddr2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddr2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddr2.Location = New System.Drawing.Point(105, 105)
        Me.txtAddr2.MaxLength = 150
        Me.txtAddr2.Name = "txtAddr2"
        Me.txtAddr2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddr2.Size = New System.Drawing.Size(267, 20)
        Me.txtAddr2.TabIndex = 3
        '
        'lblCap4
        '
        Me.lblCap4.AutoSize = True
        Me.lblCap4.BackColor = System.Drawing.Color.Transparent
        Me.lblCap4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap4.Location = New System.Drawing.Point(40, 59)
        Me.lblCap4.Name = "lblCap4"
        Me.lblCap4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap4.Size = New System.Drawing.Size(63, 15)
        Me.lblCap4.TabIndex = 345529
        Me.lblCap4.Text = "Address 1"
        '
        'txtAddr1
        '
        Me.txtAddr1.AcceptsReturn = True
        Me.txtAddr1.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddr1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddr1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddr1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddr1.Location = New System.Drawing.Point(105, 83)
        Me.txtAddr1.MaxLength = 150
        Me.txtAddr1.Name = "txtAddr1"
        Me.txtAddr1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddr1.Size = New System.Drawing.Size(267, 20)
        Me.txtAddr1.TabIndex = 2
        '
        'lblCap5
        '
        Me.lblCap5.AutoSize = True
        Me.lblCap5.BackColor = System.Drawing.Color.Transparent
        Me.lblCap5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap5.Location = New System.Drawing.Point(40, 81)
        Me.lblCap5.Name = "lblCap5"
        Me.lblCap5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap5.Size = New System.Drawing.Size(63, 15)
        Me.lblCap5.TabIndex = 345530
        Me.lblCap5.Text = "Address 2"
        '
        'txtAddr0
        '
        Me.txtAddr0.AcceptsReturn = True
        Me.txtAddr0.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddr0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddr0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddr0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddr0.Location = New System.Drawing.Point(105, 61)
        Me.txtAddr0.MaxLength = 150
        Me.txtAddr0.Name = "txtAddr0"
        Me.txtAddr0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddr0.Size = New System.Drawing.Size(267, 20)
        Me.txtAddr0.TabIndex = 1
        '
        'lblCap6
        '
        Me.lblCap6.AutoSize = True
        Me.lblCap6.BackColor = System.Drawing.Color.Transparent
        Me.lblCap6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCap6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCap6.Location = New System.Drawing.Point(40, 103)
        Me.lblCap6.Name = "lblCap6"
        Me.lblCap6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCap6.Size = New System.Drawing.Size(63, 15)
        Me.lblCap6.TabIndex = 345531
        Me.lblCap6.Text = "Address 3"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(28, 39)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 15)
        Me.Label7.TabIndex = 345525
        Me.Label7.Text = "Party Name"
        '
        'txtname
        '
        Me.txtname.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtname.Location = New System.Drawing.Point(105, 37)
        Me.txtname.Name = "txtname"
        Me.txtname.Size = New System.Drawing.Size(267, 22)
        Me.txtname.TabIndex = 1
        '
        'txtmemberid
        '
        Me.txtmemberid.AcceptsReturn = True
        Me.txtmemberid.BackColor = System.Drawing.SystemColors.Window
        Me.txtmemberid.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtmemberid.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmemberid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtmemberid.Location = New System.Drawing.Point(105, 150)
        Me.txtmemberid.MaxLength = 150
        Me.txtmemberid.Name = "txtmemberid"
        Me.txtmemberid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtmemberid.Size = New System.Drawing.Size(267, 20)
        Me.txtmemberid.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(36, 148)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(67, 15)
        Me.Label1.TabIndex = 345537
        Me.Label1.Text = "Member ID"
        '
        'txtgifvr
        '
        Me.txtgifvr.AcceptsReturn = True
        Me.txtgifvr.BackColor = System.Drawing.SystemColors.Window
        Me.txtgifvr.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtgifvr.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtgifvr.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtgifvr.Location = New System.Drawing.Point(105, 171)
        Me.txtgifvr.MaxLength = 150
        Me.txtgifvr.Name = "txtgifvr"
        Me.txtgifvr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtgifvr.Size = New System.Drawing.Size(267, 20)
        Me.txtgifvr.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(64, 171)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(39, 15)
        Me.Label2.TabIndex = 345539
        Me.Label2.Text = "Gift Vr"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(0, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 18)
        Me.Label3.TabIndex = 345541
        Me.Label3.Text = "Serch"
        '
        'txtsearch
        '
        Me.txtsearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearch.Location = New System.Drawing.Point(3, 66)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(595, 22)
        Me.txtsearch.TabIndex = 345540
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(873, 450)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 30)
        Me.btnExit.TabIndex = 345542
        Me.btnExit.Text = "E&xit (Alt+X)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'txtremarks
        '
        Me.txtremarks.AcceptsReturn = True
        Me.txtremarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtremarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtremarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtremarks.Location = New System.Drawing.Point(105, 195)
        Me.txtremarks.MaxLength = 150
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtremarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtremarks.Size = New System.Drawing.Size(267, 45)
        Me.txtremarks.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(45, 193)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(58, 15)
        Me.Label4.TabIndex = 345544
        Me.Label4.Text = "Remarks"
        '
        'grpedit
        '
        Me.grpedit.BackColor = System.Drawing.Color.Transparent
        Me.grpedit.Controls.Add(Me.txtgstn)
        Me.grpedit.Controls.Add(Me.Label5)
        Me.grpedit.Controls.Add(Me.lblcard)
        Me.grpedit.Controls.Add(Me.lblcardcap)
        Me.grpedit.Controls.Add(Me.txtaccount)
        Me.grpedit.Controls.Add(Me.Label9)
        Me.grpedit.Controls.Add(Me.btnclear)
        Me.grpedit.Controls.Add(Me.btnsave)
        Me.grpedit.Controls.Add(Me.Label7)
        Me.grpedit.Controls.Add(Me.txtname)
        Me.grpedit.Controls.Add(Me.txtremarks)
        Me.grpedit.Controls.Add(Me.lblCap6)
        Me.grpedit.Controls.Add(Me.Label4)
        Me.grpedit.Controls.Add(Me.txtphone)
        Me.grpedit.Controls.Add(Me.Label13)
        Me.grpedit.Controls.Add(Me.txtAddr0)
        Me.grpedit.Controls.Add(Me.lblCap5)
        Me.grpedit.Controls.Add(Me.txtAddr1)
        Me.grpedit.Controls.Add(Me.txtgifvr)
        Me.grpedit.Controls.Add(Me.lblCap4)
        Me.grpedit.Controls.Add(Me.Label2)
        Me.grpedit.Controls.Add(Me.txtAddr2)
        Me.grpedit.Controls.Add(Me.txtmemberid)
        Me.grpedit.Controls.Add(Me.Label6)
        Me.grpedit.Controls.Add(Me.Label1)
        Me.grpedit.Controls.Add(Me.txtemail)
        Me.grpedit.Enabled = False
        Me.grpedit.Location = New System.Drawing.Point(602, 54)
        Me.grpedit.Name = "grpedit"
        Me.grpedit.Size = New System.Drawing.Size(378, 382)
        Me.grpedit.TabIndex = 345545
        Me.grpedit.TabStop = False
        Me.grpedit.Text = "Create / Edit"
        '
        'txtgstn
        '
        Me.txtgstn.AcceptsReturn = True
        Me.txtgstn.BackColor = System.Drawing.SystemColors.Window
        Me.txtgstn.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtgstn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtgstn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtgstn.Location = New System.Drawing.Point(105, 291)
        Me.txtgstn.MaxLength = 150
        Me.txtgstn.Name = "txtgstn"
        Me.txtgstn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtgstn.Size = New System.Drawing.Size(267, 20)
        Me.txtgstn.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(64, 291)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(40, 15)
        Me.Label5.TabIndex = 345553
        Me.Label5.Text = "GSTN"
        '
        'lblcard
        '
        Me.lblcard.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblcard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblcard.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcard.ForeColor = System.Drawing.Color.Red
        Me.lblcard.Location = New System.Drawing.Point(105, 267)
        Me.lblcard.Name = "lblcard"
        Me.lblcard.Size = New System.Drawing.Size(267, 21)
        Me.lblcard.TabIndex = 345551
        Me.lblcard.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblcardcap
        '
        Me.lblcardcap.AutoSize = True
        Me.lblcardcap.BackColor = System.Drawing.Color.Transparent
        Me.lblcardcap.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblcardcap.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcardcap.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblcardcap.Location = New System.Drawing.Point(11, 267)
        Me.lblcardcap.Name = "lblcardcap"
        Me.lblcardcap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblcardcap.Size = New System.Drawing.Size(86, 15)
        Me.lblcardcap.TabIndex = 345550
        Me.lblcardcap.Text = "Discount Card"
        '
        'txtaccount
        '
        Me.txtaccount.AcceptsReturn = True
        Me.txtaccount.BackColor = System.Drawing.Color.White
        Me.txtaccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtaccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtaccount.Location = New System.Drawing.Point(105, 244)
        Me.txtaccount.MaxLength = 150
        Me.txtaccount.Name = "txtaccount"
        Me.txtaccount.ReadOnly = True
        Me.txtaccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtaccount.Size = New System.Drawing.Size(267, 20)
        Me.txtaccount.TabIndex = 345547
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(11, 244)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(92, 15)
        Me.Label9.TabIndex = 345548
        Me.Label9.Text = "Account Ledger"
        '
        'btnclear
        '
        Me.btnclear.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.ForeColor = System.Drawing.Color.White
        Me.btnclear.Location = New System.Drawing.Point(9, 339)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(108, 35)
        Me.btnclear.TabIndex = 345546
        Me.btnclear.Text = "&Clear (Alt+C)"
        Me.btnclear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclear.UseVisualStyleBackColor = False
        '
        'btnsave
        '
        Me.btnsave.BackColor = System.Drawing.Color.SteelBlue
        Me.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsave.ForeColor = System.Drawing.Color.White
        Me.btnsave.Location = New System.Drawing.Point(299, 339)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(70, 35)
        Me.btnsave.TabIndex = 9
        Me.btnsave.Text = "Save(F8)"
        Me.btnsave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsave.UseVisualStyleBackColor = False
        '
        'grdlist
        '
        Me.grdlist.BackgroundColor = System.Drawing.Color.FloralWhite
        Me.grdlist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdlist.Location = New System.Drawing.Point(3, 92)
        Me.grdlist.Name = "grdlist"
        Me.grdlist.Size = New System.Drawing.Size(593, 306)
        Me.grdlist.TabIndex = 345546
        '
        'btnselect
        '
        Me.btnselect.BackColor = System.Drawing.Color.SteelBlue
        Me.btnselect.Enabled = False
        Me.btnselect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnselect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnselect.ForeColor = System.Drawing.Color.White
        Me.btnselect.Location = New System.Drawing.Point(505, 406)
        Me.btnselect.Name = "btnselect"
        Me.btnselect.Size = New System.Drawing.Size(91, 30)
        Me.btnselect.TabIndex = 345547
        Me.btnselect.Text = "Select(F9)"
        Me.btnselect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnselect.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(158, 572)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(135, 15)
        Me.Label8.TabIndex = 345549
        Me.Label8.Text = "F8 - For Save Customer"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.White
        Me.Panel4.Controls.Add(Me.PictureBox2)
        Me.Panel4.Controls.Add(Me.lblcap)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(991, 33)
        Me.Panel4.TabIndex = 345550
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(2, 8)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(37, 22)
        Me.PictureBox2.TabIndex = 345459
        Me.PictureBox2.TabStop = False
        '
        'lblcap
        '
        Me.lblcap.AutoSize = True
        Me.lblcap.BackColor = System.Drawing.Color.Transparent
        Me.lblcap.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcap.ForeColor = System.Drawing.Color.Black
        Me.lblcap.Location = New System.Drawing.Point(41, 8)
        Me.lblcap.Name = "lblcap"
        Me.lblcap.Size = New System.Drawing.Size(293, 20)
        Me.lblcap.TabIndex = 6
        Me.lblcap.Text = "Party Name List - For Cash Transactions"
        '
        'btnedit
        '
        Me.btnedit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnedit.Enabled = False
        Me.btnedit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnedit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnedit.ForeColor = System.Drawing.Color.White
        Me.btnedit.Location = New System.Drawing.Point(387, 406)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(116, 30)
        Me.btnedit.TabIndex = 345551
        Me.btnedit.Text = "E&dit (Alt+D)"
        Me.btnedit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnedit.UseVisualStyleBackColor = False
        '
        'btnnew
        '
        Me.btnnew.BackColor = System.Drawing.Color.SteelBlue
        Me.btnnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnnew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnew.ForeColor = System.Drawing.Color.White
        Me.btnnew.Location = New System.Drawing.Point(271, 406)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(113, 30)
        Me.btnnew.TabIndex = 345552
        Me.btnnew.Text = "&New (Alt+N)"
        Me.btnnew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnnew.UseVisualStyleBackColor = False
        '
        'plws
        '
        Me.plws.BackColor = System.Drawing.Color.Transparent
        Me.plws.Controls.Add(Me.rdocard)
        Me.plws.Controls.Add(Me.rdocustomer)
        Me.plws.Location = New System.Drawing.Point(61, 39)
        Me.plws.Name = "plws"
        Me.plws.Size = New System.Drawing.Size(265, 31)
        Me.plws.TabIndex = 345553
        '
        'rdocard
        '
        Me.rdocard.AutoSize = True
        Me.rdocard.Location = New System.Drawing.Point(148, 8)
        Me.rdocard.Name = "rdocard"
        Me.rdocard.Size = New System.Drawing.Size(108, 17)
        Me.rdocard.TabIndex = 1
        Me.rdocard.Text = "Card Number (F7)"
        Me.rdocard.UseVisualStyleBackColor = True
        '
        'rdocustomer
        '
        Me.rdocustomer.AutoSize = True
        Me.rdocustomer.Checked = True
        Me.rdocustomer.Location = New System.Drawing.Point(4, 8)
        Me.rdocustomer.Name = "rdocustomer"
        Me.rdocustomer.Size = New System.Drawing.Size(125, 17)
        Me.rdocustomer.TabIndex = 0
        Me.rdocustomer.TabStop = True
        Me.rdocustomer.Text = "Customer Details (F6)"
        Me.rdocustomer.UseVisualStyleBackColor = True
        '
        'btnexport
        '
        Me.btnexport.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexport.ForeColor = System.Drawing.Color.White
        Me.btnexport.Location = New System.Drawing.Point(3, 406)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(113, 30)
        Me.btnexport.TabIndex = 345554
        Me.btnexport.Text = "Export to Excel"
        Me.btnexport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexport.UseVisualStyleBackColor = False
        '
        'btndelete
        '
        Me.btndelete.BackColor = System.Drawing.Color.SteelBlue
        Me.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.ForeColor = System.Drawing.Color.White
        Me.btndelete.Location = New System.Drawing.Point(602, 445)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(57, 35)
        Me.btndelete.TabIndex = 345554
        Me.btndelete.Text = "Delete"
        Me.btndelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndelete.UseVisualStyleBackColor = False
        '
        'CreateCashCustomerFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(991, 492)
        Me.ControlBox = False
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.btnexport)
        Me.Controls.Add(Me.btnnew)
        Me.Controls.Add(Me.btnedit)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnselect)
        Me.Controls.Add(Me.grdlist)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.grpedit)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtsearch)
        Me.Controls.Add(Me.plws)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "CreateCashCustomerFrm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create Cash Customer"
        Me.grpedit.ResumeLayout(False)
        Me.grpedit.PerformLayout()
        CType(Me.grdlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plws.ResumeLayout(False)
        Me.plws.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents txtphone As System.Windows.Forms.TextBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents txtemail As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents txtAddr2 As System.Windows.Forms.TextBox
    Public WithEvents lblCap4 As System.Windows.Forms.Label
    Public WithEvents txtAddr1 As System.Windows.Forms.TextBox
    Public WithEvents lblCap5 As System.Windows.Forms.Label
    Public WithEvents txtAddr0 As System.Windows.Forms.TextBox
    Public WithEvents lblCap6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtname As System.Windows.Forms.TextBox
    Public WithEvents txtmemberid As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtgifvr As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtsearch As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Public WithEvents txtremarks As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grpedit As System.Windows.Forms.GroupBox
    Friend WithEvents btnsave As System.Windows.Forms.Button
    Friend WithEvents grdlist As System.Windows.Forms.DataGridView
    Friend WithEvents btnselect As System.Windows.Forms.Button
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents txtaccount As System.Windows.Forms.TextBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents lblcap As System.Windows.Forms.Label
    Friend WithEvents btnedit As System.Windows.Forms.Button
    Friend WithEvents btnnew As System.Windows.Forms.Button
    Friend WithEvents plws As System.Windows.Forms.Panel
    Friend WithEvents rdocard As System.Windows.Forms.RadioButton
    Friend WithEvents rdocustomer As System.Windows.Forms.RadioButton
    Public WithEvents lblcardcap As System.Windows.Forms.Label
    Friend WithEvents lblcard As System.Windows.Forms.Label
    Public WithEvents txtgstn As System.Windows.Forms.TextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnexport As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btndelete As System.Windows.Forms.Button
End Class
