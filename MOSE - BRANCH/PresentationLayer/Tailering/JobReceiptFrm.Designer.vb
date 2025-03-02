<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JobReceiptFrm
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
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.btnupi = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.txtPcash = New System.Windows.Forms.TextBox
        Me.btnsetcash = New System.Windows.Forms.Button
        Me.lblcash = New System.Windows.Forms.Label
        Me.lblNetAmt = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txttendered = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblchange = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtPCard = New System.Windows.Forms.TextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.btnsetcard = New System.Windows.Forms.Button
        Me.txtcardamount = New System.Windows.Forms.TextBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label4 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnupdate = New System.Windows.Forms.Button
        Me.txtPreFix = New System.Windows.Forms.TextBox
        Me.dtpdate = New System.Windows.Forms.DateTimePicker
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.numVchrNo = New System.Windows.Forms.TextBox
        Me.txtcustomer = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
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
        Me.btnupi.Location = New System.Drawing.Point(204, 19)
        Me.btnupi.Name = "btnupi"
        Me.btnupi.Size = New System.Drawing.Size(78, 42)
        Me.btnupi.TabIndex = 345550
        Me.btnupi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupi.UseVisualStyleBackColor = False
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
        Me.GroupBox3.Location = New System.Drawing.Point(16, 67)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(406, 170)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Cash"
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
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.dtpdate)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.txtPreFix)
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.numVchrNo)
        Me.Panel2.Location = New System.Drawing.Point(325, 63)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(439, 244)
        Me.Panel2.TabIndex = 345561
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.White
        Me.Panel7.Controls.Add(Me.GroupBox1)
        Me.Panel7.Location = New System.Drawing.Point(4, 63)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(311, 244)
        Me.Panel7.TabIndex = 345560
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtcustomer)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.btnupi)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtPCard)
        Me.GroupBox1.Controls.Add(Me.Label34)
        Me.GroupBox1.Controls.Add(Me.btnsetcard)
        Me.GroupBox1.Controls.Add(Me.txtcardamount)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtcardnumber)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 14)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(290, 223)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Card"
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
        'btnsetcard
        '
        Me.btnsetcard.BackColor = System.Drawing.Color.White
        Me.btnsetcard.BackgroundImage = Global.SMSMP.My.Resources.Resources.cardpng1
        Me.btnsetcard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnsetcard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsetcard.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsetcard.ForeColor = System.Drawing.Color.White
        Me.btnsetcard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnsetcard.Location = New System.Drawing.Point(204, 63)
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
        Me.Label4.Text = "Receipt"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(43, 36)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(37, 19)
        Me.PictureBox1.TabIndex = 345458
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Location = New System.Drawing.Point(-7, -15)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(782, 72)
        Me.Panel1.TabIndex = 345556
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(4, 313)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 49)
        Me.btnExit.TabIndex = 345555
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnupdate
        '
        Me.btnupdate.BackColor = System.Drawing.Color.IndianRed
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnupdate.Location = New System.Drawing.Point(570, 313)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(186, 49)
        Me.btnupdate.TabIndex = 345554
        Me.btnupdate.Text = "PAYMENT [F8]"
        Me.btnupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'txtPreFix
        '
        Me.txtPreFix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPreFix.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPreFix.Location = New System.Drawing.Point(304, 11)
        Me.txtPreFix.Name = "txtPreFix"
        Me.txtPreFix.Size = New System.Drawing.Size(51, 21)
        Me.txtPreFix.TabIndex = 345563
        '
        'dtpdate
        '
        Me.dtpdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpdate.Location = New System.Drawing.Point(327, 38)
        Me.dtpdate.Name = "dtpdate"
        Me.dtpdate.Size = New System.Drawing.Size(95, 20)
        Me.dtpdate.TabIndex = 345566
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(264, 41)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 13)
        Me.Label6.TabIndex = 345565
        Me.Label6.Text = "Date"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(264, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 345567
        Me.Label7.Text = "RV No"
        '
        'numVchrNo
        '
        Me.numVchrNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numVchrNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numVchrNo.Location = New System.Drawing.Point(357, 11)
        Me.numVchrNo.Name = "numVchrNo"
        Me.numVchrNo.Size = New System.Drawing.Size(65, 21)
        Me.numVchrNo.TabIndex = 345564
        '
        'txtcustomer
        '
        Me.txtcustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtcustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustomer.Location = New System.Drawing.Point(12, 191)
        Me.txtcustomer.Name = "txtcustomer"
        Me.txtcustomer.ReadOnly = True
        Me.txtcustomer.Size = New System.Drawing.Size(270, 26)
        Me.txtcustomer.TabIndex = 345552
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 175)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 345568
        Me.Label2.Text = "Customer"
        '
        'JobReceiptFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(768, 367)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnupdate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "JobReceiptFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "JobReceiptFrm"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtcardnumber As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnupi As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtPcash As System.Windows.Forms.TextBox
    Friend WithEvents btnsetcash As System.Windows.Forms.Button
    Friend WithEvents lblcash As System.Windows.Forms.Label
    Friend WithEvents lblNetAmt As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txttendered As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblchange As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPCard As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents btnsetcard As System.Windows.Forms.Button
    Friend WithEvents txtcardamount As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents dtpdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPreFix As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents numVchrNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtcustomer As System.Windows.Forms.TextBox
End Class
