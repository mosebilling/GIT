<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TenderedEntryFrm
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
        Me.txtcardnumber = New System.Windows.Forms.TextBox
        Me.lblNetAmt = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txttendered = New System.Windows.Forms.TextBox
        Me.lblchange = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnupdate = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtcustomer = New System.Windows.Forms.TextBox
        Me.btnsetcash = New System.Windows.Forms.Button
        Me.btnsetcard = New System.Windows.Forms.Button
        Me.txtcardamount = New System.Windows.Forms.TextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.txtPCard = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.txtPcash = New System.Windows.Forms.TextBox
        Me.chkcredit = New System.Windows.Forms.CheckBox
        Me.grpCredit = New System.Windows.Forms.GroupBox
        Me.lbllimit = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtcustAddress = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.btnnewcustomer = New System.Windows.Forms.Button
        Me.lblcbalance = New System.Windows.Forms.Label
        Me.lblbalance = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblcash = New System.Windows.Forms.Label
        Me.chkdupprint = New System.Windows.Forms.CheckBox
        Me.lblredeem = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.lblpointValue = New System.Windows.Forms.Label
        Me.lblpointbalance = New System.Windows.Forms.Label
        Me.lblredeemed = New System.Windows.Forms.Label
        Me.lblearned = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label40 = New System.Windows.Forms.Label
        Me.lblqty = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.btnupi = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.txtsrno = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtsramt = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.lblvouchername = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCredit.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtcardnumber
        '
        Me.txtcardnumber.BackColor = System.Drawing.Color.White
        Me.txtcardnumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcardnumber.Location = New System.Drawing.Point(12, 146)
        Me.txtcardnumber.Name = "txtcardnumber"
        Me.txtcardnumber.Size = New System.Drawing.Size(184, 26)
        Me.txtcardnumber.TabIndex = 36
        '
        'lblNetAmt
        '
        Me.lblNetAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNetAmt.BackColor = System.Drawing.Color.White
        Me.lblNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNetAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblNetAmt.Location = New System.Drawing.Point(12, 85)
        Me.lblNetAmt.Name = "lblNetAmt"
        Me.lblNetAmt.Size = New System.Drawing.Size(189, 38)
        Me.lblNetAmt.TabIndex = 104
        Me.lblNetAmt.Text = "0.00"
        Me.lblNetAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.DimGray
        Me.Label3.Location = New System.Drawing.Point(209, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(78, 18)
        Me.Label3.TabIndex = 105
        Me.Label3.Text = "Tendered"
        '
        'txttendered
        '
        Me.txttendered.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txttendered.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txttendered.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txttendered.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttendered.Location = New System.Drawing.Point(211, 85)
        Me.txttendered.Name = "txttendered"
        Me.txttendered.Size = New System.Drawing.Size(189, 38)
        Me.txttendered.TabIndex = 345430
        Me.txttendered.Text = "0.00"
        Me.txttendered.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblchange
        '
        Me.lblchange.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblchange.BackColor = System.Drawing.Color.White
        Me.lblchange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblchange.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblchange.ForeColor = System.Drawing.Color.Brown
        Me.lblchange.Location = New System.Drawing.Point(211, 126)
        Me.lblchange.Name = "lblchange"
        Me.lblchange.Size = New System.Drawing.Size(189, 38)
        Me.lblchange.TabIndex = 345432
        Me.lblchange.Text = "0.00"
        Me.lblchange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Gray
        Me.Label5.Location = New System.Drawing.Point(138, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label5.Size = New System.Drawing.Size(65, 18)
        Me.Label5.TabIndex = 345431
        Me.Label5.Text = "Change"
        '
        'btnupdate
        '
        Me.btnupdate.BackColor = System.Drawing.Color.IndianRed
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnupdate.Location = New System.Drawing.Point(584, 553)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(186, 49)
        Me.btnupdate.TabIndex = 345506
        Me.btnupdate.Text = "PAYMENT [F8]"
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(10, 553)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 49)
        Me.btnExit.TabIndex = 345509
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Location = New System.Drawing.Point(-1, -1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(782, 72)
        Me.Panel1.TabIndex = 345510
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(12, 13)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(37, 19)
        Me.PictureBox1.TabIndex = 345458
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Verdana", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(780, 70)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Payment "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(9, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(120, 15)
        Me.Label7.TabIndex = 345512
        Me.Label7.Text = "Customer Name [F2]"
        '
        'txtcustomer
        '
        Me.txtcustomer.BackColor = System.Drawing.Color.White
        Me.txtcustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustomer.Location = New System.Drawing.Point(141, 15)
        Me.txtcustomer.Name = "txtcustomer"
        Me.txtcustomer.Size = New System.Drawing.Size(259, 22)
        Me.txtcustomer.TabIndex = 345511
        '
        'btnsetcash
        '
        Me.btnsetcash.BackColor = System.Drawing.Color.SteelBlue
        Me.btnsetcash.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsetcash.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsetcash.ForeColor = System.Drawing.Color.White
        Me.btnsetcash.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnsetcash.Location = New System.Drawing.Point(363, 37)
        Me.btnsetcash.Name = "btnsetcash"
        Me.btnsetcash.Size = New System.Drawing.Size(37, 29)
        Me.btnsetcash.TabIndex = 345530
        Me.btnsetcash.Text = ">>"
        Me.btnsetcash.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsetcash.UseVisualStyleBackColor = False
        '
        'btnsetcard
        '
        Me.btnsetcard.BackColor = System.Drawing.Color.White
        Me.btnsetcard.BackgroundImage = Global.SMSMP.My.Resources.Resources.cardpng1
        Me.btnsetcard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnsetcard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsetcard.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsetcard.ForeColor = System.Drawing.Color.White
        Me.btnsetcard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnsetcard.Location = New System.Drawing.Point(209, 63)
        Me.btnsetcard.Name = "btnsetcard"
        Me.btnsetcard.Size = New System.Drawing.Size(78, 42)
        Me.btnsetcard.TabIndex = 345529
        Me.btnsetcard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsetcard.UseVisualStyleBackColor = False
        '
        'txtcardamount
        '
        Me.txtcardamount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtcardamount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcardamount.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcardamount.Location = New System.Drawing.Point(12, 83)
        Me.txtcardamount.Name = "txtcardamount"
        Me.txtcardamount.Size = New System.Drawing.Size(184, 38)
        Me.txtcardamount.TabIndex = 345523
        Me.txtcardamount.Text = "0.00"
        Me.txtcardamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Maroon
        Me.Label34.Location = New System.Drawing.Point(102, 19)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(101, 13)
        Me.Label34.TabIndex = 345527
        Me.Label34.Text = "CARD A/C [ F5 ]"
        '
        'txtPCard
        '
        Me.txtPCard.BackColor = System.Drawing.Color.White
        Me.txtPCard.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPCard.Location = New System.Drawing.Point(12, 35)
        Me.txtPCard.Name = "txtPCard"
        Me.txtPCard.ReadOnly = True
        Me.txtPCard.Size = New System.Drawing.Size(184, 26)
        Me.txtPCard.TabIndex = 345526
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Maroon
        Me.Label33.Location = New System.Drawing.Point(9, 22)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(100, 13)
        Me.Label33.TabIndex = 345525
        Me.Label33.Text = "CASH A/C [ F6 ]"
        '
        'txtPcash
        '
        Me.txtPcash.BackColor = System.Drawing.Color.White
        Me.txtPcash.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPcash.Location = New System.Drawing.Point(12, 38)
        Me.txtPcash.Name = "txtPcash"
        Me.txtPcash.ReadOnly = True
        Me.txtPcash.Size = New System.Drawing.Size(351, 26)
        Me.txtPcash.TabIndex = 345524
        '
        'chkcredit
        '
        Me.chkcredit.AutoSize = True
        Me.chkcredit.BackColor = System.Drawing.Color.Transparent
        Me.chkcredit.ForeColor = System.Drawing.Color.Black
        Me.chkcredit.Location = New System.Drawing.Point(269, 8)
        Me.chkcredit.Name = "chkcredit"
        Me.chkcredit.Size = New System.Drawing.Size(155, 17)
        Me.chkcredit.TabIndex = 345531
        Me.chkcredit.Text = "Balance Set as Credit [ F7 ]"
        Me.chkcredit.UseVisualStyleBackColor = False
        '
        'grpCredit
        '
        Me.grpCredit.BackColor = System.Drawing.Color.Transparent
        Me.grpCredit.Controls.Add(Me.lbllimit)
        Me.grpCredit.Controls.Add(Me.Label17)
        Me.grpCredit.Controls.Add(Me.txtcustAddress)
        Me.grpCredit.Controls.Add(Me.Label18)
        Me.grpCredit.Controls.Add(Me.btnnewcustomer)
        Me.grpCredit.Controls.Add(Me.txtcustomer)
        Me.grpCredit.Controls.Add(Me.Label7)
        Me.grpCredit.Controls.Add(Me.lblcbalance)
        Me.grpCredit.Controls.Add(Me.lblbalance)
        Me.grpCredit.Controls.Add(Me.Label16)
        Me.grpCredit.Controls.Add(Me.Label6)
        Me.grpCredit.Enabled = False
        Me.grpCredit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpCredit.ForeColor = System.Drawing.Color.Black
        Me.grpCredit.Location = New System.Drawing.Point(16, 26)
        Me.grpCredit.Name = "grpCredit"
        Me.grpCredit.Size = New System.Drawing.Size(406, 205)
        Me.grpCredit.TabIndex = 345532
        Me.grpCredit.TabStop = False
        Me.grpCredit.Text = "Credit Account"
        '
        'lbllimit
        '
        Me.lbllimit.BackColor = System.Drawing.Color.Transparent
        Me.lbllimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbllimit.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lbllimit.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbllimit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lbllimit.Location = New System.Drawing.Point(237, 172)
        Me.lbllimit.Name = "lbllimit"
        Me.lbllimit.Size = New System.Drawing.Size(163, 25)
        Me.lbllimit.TabIndex = 345535
        Me.lbllimit.Text = "0.00"
        Me.lbllimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label17.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(150, 177)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(78, 14)
        Me.Label17.TabIndex = 345536
        Me.Label17.Text = "Credit Limit"
        '
        'txtcustAddress
        '
        Me.txtcustAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustAddress.Location = New System.Drawing.Point(141, 43)
        Me.txtcustAddress.Multiline = True
        Me.txtcustAddress.Name = "txtcustAddress"
        Me.txtcustAddress.ReadOnly = True
        Me.txtcustAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtcustAddress.Size = New System.Drawing.Size(259, 61)
        Me.txtcustAddress.TabIndex = 345527
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(78, 43)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(51, 15)
        Me.Label18.TabIndex = 345528
        Me.Label18.Text = "Address"
        '
        'btnnewcustomer
        '
        Me.btnnewcustomer.BackColor = System.Drawing.Color.SteelBlue
        Me.btnnewcustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnnewcustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnewcustomer.ForeColor = System.Drawing.Color.White
        Me.btnnewcustomer.Location = New System.Drawing.Point(12, 69)
        Me.btnnewcustomer.Name = "btnnewcustomer"
        Me.btnnewcustomer.Size = New System.Drawing.Size(83, 57)
        Me.btnnewcustomer.TabIndex = 345526
        Me.btnnewcustomer.Text = "New &Customer [Alt+C]"
        Me.btnnewcustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnnewcustomer.UseVisualStyleBackColor = False
        '
        'lblcbalance
        '
        Me.lblcbalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblcbalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblcbalance.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lblcbalance.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcbalance.ForeColor = System.Drawing.Color.Black
        Me.lblcbalance.Location = New System.Drawing.Point(237, 140)
        Me.lblcbalance.Name = "lblcbalance"
        Me.lblcbalance.Size = New System.Drawing.Size(163, 30)
        Me.lblcbalance.TabIndex = 345532
        Me.lblcbalance.Text = "0.00"
        Me.lblcbalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblbalance
        '
        Me.lblbalance.BackColor = System.Drawing.Color.Transparent
        Me.lblbalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblbalance.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lblbalance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbalance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblbalance.Location = New System.Drawing.Point(237, 112)
        Me.lblbalance.Name = "lblbalance"
        Me.lblbalance.Size = New System.Drawing.Size(163, 25)
        Me.lblbalance.TabIndex = 345529
        Me.lblbalance.Text = "0.00"
        Me.lblbalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label16.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(113, 117)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(121, 14)
        Me.Label16.TabIndex = 345530
        Me.Label16.Text = "Previous Balance"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.Label6.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(78, 146)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(156, 18)
        Me.Label6.TabIndex = 345531
        Me.Label6.Text = "Current. Balance"
        '
        'Timer1
        '
        '
        'lblcash
        '
        Me.lblcash.BackColor = System.Drawing.Color.Transparent
        Me.lblcash.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcash.ForeColor = System.Drawing.Color.Gray
        Me.lblcash.Location = New System.Drawing.Point(267, 13)
        Me.lblcash.Name = "lblcash"
        Me.lblcash.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblcash.Size = New System.Drawing.Size(133, 22)
        Me.lblcash.TabIndex = 345533
        Me.lblcash.Text = "0.00"
        Me.lblcash.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblcash.Visible = False
        '
        'chkdupprint
        '
        Me.chkdupprint.AutoSize = True
        Me.chkdupprint.BackColor = System.Drawing.Color.Transparent
        Me.chkdupprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkdupprint.ForeColor = System.Drawing.Color.Black
        Me.chkdupprint.Location = New System.Drawing.Point(480, 553)
        Me.chkdupprint.Name = "chkdupprint"
        Me.chkdupprint.Size = New System.Drawing.Size(98, 19)
        Me.chkdupprint.TabIndex = 345534
        Me.chkdupprint.Text = "Duplicate Bill"
        Me.chkdupprint.UseVisualStyleBackColor = False
        '
        'lblredeem
        '
        Me.lblredeem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblredeem.BackColor = System.Drawing.Color.White
        Me.lblredeem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblredeem.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblredeem.ForeColor = System.Drawing.Color.DarkRed
        Me.lblredeem.Location = New System.Drawing.Point(169, 169)
        Me.lblredeem.Name = "lblredeem"
        Me.lblredeem.Size = New System.Drawing.Size(112, 33)
        Me.lblredeem.TabIndex = 345540
        Me.lblredeem.Text = "0.00"
        Me.lblredeem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.DarkRed
        Me.Label11.Location = New System.Drawing.Point(17, 169)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label11.Size = New System.Drawing.Size(142, 18)
        Me.Label11.TabIndex = 345541
        Me.Label11.Text = "Redeem Discount"
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.DimGray
        Me.Label12.Location = New System.Drawing.Point(12, 67)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label12.Size = New System.Drawing.Size(67, 18)
        Me.Label12.TabIndex = 345542
        Me.Label12.Text = "Payable"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.DimGray
        Me.Label13.Location = New System.Drawing.Point(12, 64)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label13.Size = New System.Drawing.Size(106, 18)
        Me.Label13.TabIndex = 345546
        Me.Label13.Text = "Card Amount"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label43)
        Me.GroupBox2.Controls.Add(Me.lblpointValue)
        Me.GroupBox2.Controls.Add(Me.lblpointbalance)
        Me.GroupBox2.Controls.Add(Me.lblredeemed)
        Me.GroupBox2.Controls.Add(Me.lblearned)
        Me.GroupBox2.Controls.Add(Me.Label38)
        Me.GroupBox2.Controls.Add(Me.Label37)
        Me.GroupBox2.Controls.Add(Me.Label36)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(17, 21)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(274, 142)
        Me.GroupBox2.TabIndex = 345548
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Points"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Green
        Me.Label8.Location = New System.Drawing.Point(158, 99)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(106, 32)
        Me.Label8.TabIndex = 345501
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(7, 104)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(144, 18)
        Me.Label9.TabIndex = 345500
        Me.Label9.Text = "Point Earned Now"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.Color.Transparent
        Me.Label43.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.Black
        Me.Label43.Location = New System.Drawing.Point(119, 66)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(38, 15)
        Me.Label43.TabIndex = 345499
        Me.Label43.Text = "Value"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblpointValue
        '
        Me.lblpointValue.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblpointValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblpointValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpointValue.ForeColor = System.Drawing.Color.Black
        Me.lblpointValue.Location = New System.Drawing.Point(158, 59)
        Me.lblpointValue.Name = "lblpointValue"
        Me.lblpointValue.Size = New System.Drawing.Size(106, 34)
        Me.lblpointValue.TabIndex = 345498
        Me.lblpointValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblpointbalance
        '
        Me.lblpointbalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblpointbalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblpointbalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpointbalance.ForeColor = System.Drawing.Color.Black
        Me.lblpointbalance.Location = New System.Drawing.Point(61, 60)
        Me.lblpointbalance.Name = "lblpointbalance"
        Me.lblpointbalance.Size = New System.Drawing.Size(53, 34)
        Me.lblpointbalance.TabIndex = 345497
        Me.lblpointbalance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblredeemed
        '
        Me.lblredeemed.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblredeemed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblredeemed.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblredeemed.ForeColor = System.Drawing.Color.Black
        Me.lblredeemed.Location = New System.Drawing.Point(211, 21)
        Me.lblredeemed.Name = "lblredeemed"
        Me.lblredeemed.Size = New System.Drawing.Size(53, 34)
        Me.lblredeemed.TabIndex = 345496
        Me.lblredeemed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblearned
        '
        Me.lblearned.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblearned.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblearned.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblearned.ForeColor = System.Drawing.Color.Black
        Me.lblearned.Location = New System.Drawing.Point(61, 21)
        Me.lblearned.Name = "lblearned"
        Me.lblearned.Size = New System.Drawing.Size(53, 34)
        Me.lblearned.TabIndex = 345459
        Me.lblearned.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.Black
        Me.Label38.Location = New System.Drawing.Point(7, 66)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(52, 15)
        Me.Label38.TabIndex = 345495
        Me.Label38.Text = "Balance"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Black
        Me.Label37.Location = New System.Drawing.Point(139, 27)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(69, 15)
        Me.Label37.TabIndex = 345494
        Me.Label37.Text = "Redeemed"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(7, 27)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(50, 15)
        Me.Label36.TabIndex = 345493
        Me.Label36.Text = "Earned "
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.Controls.Add(Me.Label40)
        Me.Panel5.Controls.Add(Me.lblqty)
        Me.Panel5.Controls.Add(Me.GroupBox2)
        Me.Panel5.Controls.Add(Me.lblredeem)
        Me.Panel5.Controls.Add(Me.Label11)
        Me.Panel5.Location = New System.Drawing.Point(10, 77)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(312, 245)
        Me.Panel5.TabIndex = 345549
        '
        'Label40
        '
        Me.Label40.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.Color.Black
        Me.Label40.Location = New System.Drawing.Point(17, 207)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(71, 16)
        Me.Label40.TabIndex = 345550
        Me.Label40.Text = "Total Qty"
        '
        'lblqty
        '
        Me.lblqty.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblqty.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblqty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblqty.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblqty.ForeColor = System.Drawing.Color.Red
        Me.lblqty.Location = New System.Drawing.Point(169, 205)
        Me.lblqty.Name = "lblqty"
        Me.lblqty.Size = New System.Drawing.Size(71, 36)
        Me.lblqty.TabIndex = 345549
        Me.lblqty.Text = "0"
        Me.lblqty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.White
        Me.Panel6.Controls.Add(Me.chkcredit)
        Me.Panel6.Controls.Add(Me.grpCredit)
        Me.Panel6.Location = New System.Drawing.Point(331, 77)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(439, 245)
        Me.Panel6.TabIndex = 345550
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.White
        Me.Panel7.Controls.Add(Me.GroupBox1)
        Me.Panel7.Location = New System.Drawing.Point(10, 333)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(311, 213)
        Me.Panel7.TabIndex = 345551
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.btnupi)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtPCard)
        Me.GroupBox1.Controls.Add(Me.Label34)
        Me.GroupBox1.Controls.Add(Me.btnsetcard)
        Me.GroupBox1.Controls.Add(Me.txtcardamount)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtcardnumber)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 14)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(290, 194)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Card"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Maroon
        Me.Label14.Location = New System.Drawing.Point(12, 19)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(88, 13)
        Me.Label14.TabIndex = 345551
        Me.Label14.Text = "UPI A/C [ F4 ]"
        '
        'btnupi
        '
        Me.btnupi.BackColor = System.Drawing.Color.White
        Me.btnupi.BackgroundImage = Global.SMSMP.My.Resources.Resources.upi
        Me.btnupi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnupi.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupi.ForeColor = System.Drawing.Color.White
        Me.btnupi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnupi.Location = New System.Drawing.Point(209, 19)
        Me.btnupi.Name = "btnupi"
        Me.btnupi.Size = New System.Drawing.Size(78, 42)
        Me.btnupi.TabIndex = 345550
        Me.btnupi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupi.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.DimGray
        Me.Label1.Location = New System.Drawing.Point(12, 126)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(108, 18)
        Me.Label1.TabIndex = 345547
        Me.Label1.Text = "Card Number"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.txtsrno)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.txtsramt)
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Location = New System.Drawing.Point(331, 333)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(439, 213)
        Me.Panel2.TabIndex = 345552
        '
        'txtsrno
        '
        Me.txtsrno.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsrno.Location = New System.Drawing.Point(48, 7)
        Me.txtsrno.Name = "txtsrno"
        Me.txtsrno.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtsrno.Size = New System.Drawing.Size(147, 26)
        Me.txtsrno.TabIndex = 345545
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.DimGray
        Me.Label10.Location = New System.Drawing.Point(215, 11)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label10.Size = New System.Drawing.Size(65, 18)
        Me.Label10.TabIndex = 345544
        Me.Label10.Text = "Amount"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DimGray
        Me.Label2.Location = New System.Drawing.Point(16, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(31, 18)
        Me.Label2.TabIndex = 345543
        Me.Label2.Text = "SR"
        '
        'txtsramt
        '
        Me.txtsramt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsramt.Location = New System.Drawing.Point(286, 7)
        Me.txtsramt.Name = "txtsramt"
        Me.txtsramt.ReadOnly = True
        Me.txtsramt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtsramt.Size = New System.Drawing.Size(136, 26)
        Me.txtsramt.TabIndex = 345518
        Me.txtsramt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label33)
        Me.GroupBox3.Controls.Add(Me.txtPcash)
        Me.GroupBox3.Controls.Add(Me.btnsetcash)
        Me.GroupBox3.Controls.Add(Me.lblcash)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.lblNetAmt)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.txttendered)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.lblchange)
        Me.GroupBox3.Location = New System.Drawing.Point(16, 38)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(406, 170)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Cash"
        '
        'lblvouchername
        '
        Me.lblvouchername.AutoSize = True
        Me.lblvouchername.BackColor = System.Drawing.Color.Transparent
        Me.lblvouchername.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvouchername.ForeColor = System.Drawing.Color.DimGray
        Me.lblvouchername.Location = New System.Drawing.Point(122, 554)
        Me.lblvouchername.Name = "lblvouchername"
        Me.lblvouchername.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblvouchername.Size = New System.Drawing.Size(59, 18)
        Me.lblvouchername.TabIndex = 345553
        Me.lblvouchername.Text = "Vname"
        '
        'TenderedEntryFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(778, 605)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblvouchername)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.chkdupprint)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnupdate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "TenderedEntryFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCredit.ResumeLayout(False)
        Me.grpCredit.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtcardnumber As System.Windows.Forms.TextBox
    Friend WithEvents lblNetAmt As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txttendered As System.Windows.Forms.TextBox
    Friend WithEvents lblchange As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtcustomer As System.Windows.Forms.TextBox
    Friend WithEvents btnsetcash As System.Windows.Forms.Button
    Friend WithEvents btnsetcard As System.Windows.Forms.Button
    Friend WithEvents txtcardamount As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtPCard As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtPcash As System.Windows.Forms.TextBox
    Friend WithEvents chkcredit As System.Windows.Forms.CheckBox
    Friend WithEvents grpCredit As System.Windows.Forms.GroupBox
    Friend WithEvents btnnewcustomer As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblcash As System.Windows.Forms.Label
    Friend WithEvents txtcustAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblbalance As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblcbalance As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkdupprint As System.Windows.Forms.CheckBox
    Friend WithEvents lblredeem As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents lblpointValue As System.Windows.Forms.Label
    Friend WithEvents lblpointbalance As System.Windows.Forms.Label
    Friend WithEvents lblredeemed As System.Windows.Forms.Label
    Friend WithEvents lblearned As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtsramt As System.Windows.Forms.TextBox
    Friend WithEvents txtsrno As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnupi As System.Windows.Forms.Button
    Friend WithEvents lblvouchername As System.Windows.Forms.Label
    Friend WithEvents lbllimit As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblqty As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
End Class
