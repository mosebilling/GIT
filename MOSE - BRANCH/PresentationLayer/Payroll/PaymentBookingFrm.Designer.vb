<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PaymentBookingFrm
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
        Me.BtnUpdate = New System.Windows.Forms.Button
        Me.btnclose = New System.Windows.Forms.Button
        Me.rdosalary = New System.Windows.Forms.RadioButton
        Me.btnload = New System.Windows.Forms.Button
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.rdodaily = New System.Windows.Forms.RadioButton
        Me.Label26 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.lblName = New System.Windows.Forms.Label
        Me.cldrdate = New System.Windows.Forms.DateTimePicker
        Me.btndelete = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtaccount = New System.Windows.Forms.TextBox
        Me.dtpbookingdate = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.chkselectall = New System.Windows.Forms.CheckBox
        Me.rdounit = New System.Windows.Forms.RadioButton
        Me.chkloadfromdailysheet = New System.Windows.Forms.CheckBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.lblstatus = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.chkbookingdate = New System.Windows.Forms.CheckBox
        Me.btnloadbooking = New System.Windows.Forms.Button
        Me.dtpto = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpfrom = New System.Windows.Forms.DateTimePicker
        Me.grdlist = New System.Windows.Forms.DataGridView
        Me.Label27 = New System.Windows.Forms.Label
        Me.lblworkAmt = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtjv = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.dtptodate = New System.Windows.Forms.DateTimePicker
        Me.btnclear = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblnetAmt = New System.Windows.Forms.Label
        Me.btnupdateAccounts = New System.Windows.Forms.Button
        Me.btnpayment = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.btnpreview = New System.Windows.Forms.Button
        Me.chkpayslip = New System.Windows.Forms.CheckBox
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnUpdate
        '
        Me.BtnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnUpdate.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnUpdate.Enabled = False
        Me.BtnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdate.ForeColor = System.Drawing.Color.White
        Me.BtnUpdate.Location = New System.Drawing.Point(795, 521)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnUpdate.Size = New System.Drawing.Size(83, 35)
        Me.BtnUpdate.TabIndex = 345515
        Me.BtnUpdate.Tag = "56"
        Me.BtnUpdate.Text = "&Update"
        Me.BtnUpdate.UseVisualStyleBackColor = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ForeColor = System.Drawing.Color.White
        Me.btnclose.Location = New System.Drawing.Point(880, 521)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(82, 35)
        Me.btnclose.TabIndex = 345512
        Me.btnclose.Text = "E&xit"
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'rdosalary
        '
        Me.rdosalary.AutoSize = True
        Me.rdosalary.BackColor = System.Drawing.Color.Transparent
        Me.rdosalary.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdosalary.Location = New System.Drawing.Point(9, 89)
        Me.rdosalary.Name = "rdosalary"
        Me.rdosalary.Size = New System.Drawing.Size(65, 19)
        Me.rdosalary.TabIndex = 345491
        Me.rdosalary.Text = "Salary"
        Me.rdosalary.UseVisualStyleBackColor = False
        '
        'btnload
        '
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.Location = New System.Drawing.Point(16, 238)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(61, 25)
        Me.btnload.TabIndex = 345511
        Me.btnload.Text = "Load"
        Me.btnload.UseVisualStyleBackColor = False
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
        Me.grdVoucher.Location = New System.Drawing.Point(6, 28)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(750, 414)
        Me.grdVoucher.TabIndex = 345506
        '
        'rdodaily
        '
        Me.rdodaily.AutoSize = True
        Me.rdodaily.BackColor = System.Drawing.Color.Transparent
        Me.rdodaily.Checked = True
        Me.rdodaily.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdodaily.Location = New System.Drawing.Point(9, 41)
        Me.rdodaily.Name = "rdodaily"
        Me.rdodaily.Size = New System.Drawing.Size(68, 19)
        Me.rdodaily.TabIndex = 345489
        Me.rdodaily.TabStop = True
        Me.rdodaily.Text = "Wages"
        Me.rdodaily.UseVisualStyleBackColor = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(50, 5)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(148, 20)
        Me.Label26.TabIndex = 345461
        Me.Label26.Text = "Payment Booking"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.Label26)
        Me.Panel2.Controls.Add(Me.PictureBox2)
        Me.Panel2.Controls.Add(Me.lblName)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(974, 34)
        Me.Panel2.TabIndex = 345508
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(3, 5)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(41, 22)
        Me.PictureBox2.TabIndex = 345460
        Me.PictureBox2.TabStop = False
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.White
        Me.lblName.Location = New System.Drawing.Point(41, 9)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(94, 20)
        Me.lblName.TabIndex = 6
        Me.lblName.Text = "Item Master"
        '
        'cldrdate
        '
        Me.cldrdate.CustomFormat = "dd/MM/yyyy"
        Me.cldrdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.cldrdate.Location = New System.Drawing.Point(16, 170)
        Me.cldrdate.Name = "cldrdate"
        Me.cldrdate.Size = New System.Drawing.Size(106, 20)
        Me.cldrdate.TabIndex = 345507
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.BackColor = System.Drawing.Color.SteelBlue
        Me.btndelete.Enabled = False
        Me.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.ForeColor = System.Drawing.Color.White
        Me.btndelete.Location = New System.Drawing.Point(192, 517)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(83, 35)
        Me.btndelete.TabIndex = 345516
        Me.btndelete.Text = "&Delete"
        Me.btndelete.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(5, 53)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 345518
        Me.Label5.Text = "Salary A/C"
        '
        'txtaccount
        '
        Me.txtaccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtaccount.Location = New System.Drawing.Point(5, 69)
        Me.txtaccount.MaxLength = 30
        Me.txtaccount.Name = "txtaccount"
        Me.txtaccount.ReadOnly = True
        Me.txtaccount.Size = New System.Drawing.Size(149, 20)
        Me.txtaccount.TabIndex = 345517
        '
        'dtpbookingdate
        '
        Me.dtpbookingdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpbookingdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpbookingdate.Location = New System.Drawing.Point(650, 6)
        Me.dtpbookingdate.Name = "dtpbookingdate"
        Me.dtpbookingdate.Size = New System.Drawing.Size(106, 20)
        Me.dtpbookingdate.TabIndex = 345522
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(576, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 13)
        Me.Label3.TabIndex = 345523
        Me.Label3.Text = "Booking Date"
        '
        'chkselectall
        '
        Me.chkselectall.AutoSize = True
        Me.chkselectall.BackColor = System.Drawing.Color.Transparent
        Me.chkselectall.Location = New System.Drawing.Point(63, 7)
        Me.chkselectall.Name = "chkselectall"
        Me.chkselectall.Size = New System.Drawing.Size(70, 17)
        Me.chkselectall.TabIndex = 345508
        Me.chkselectall.Text = "Select All"
        Me.chkselectall.UseVisualStyleBackColor = False
        Me.chkselectall.Visible = False
        '
        'rdounit
        '
        Me.rdounit.AutoSize = True
        Me.rdounit.BackColor = System.Drawing.Color.Transparent
        Me.rdounit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdounit.Location = New System.Drawing.Point(9, 66)
        Me.rdounit.Name = "rdounit"
        Me.rdounit.Size = New System.Drawing.Size(73, 17)
        Me.rdounit.TabIndex = 345491
        Me.rdounit.Text = "Unitwise"
        Me.rdounit.UseVisualStyleBackColor = False
        '
        'chkloadfromdailysheet
        '
        Me.chkloadfromdailysheet.AutoSize = True
        Me.chkloadfromdailysheet.BackColor = System.Drawing.Color.Transparent
        Me.chkloadfromdailysheet.Location = New System.Drawing.Point(15, 125)
        Me.chkloadfromdailysheet.Name = "chkloadfromdailysheet"
        Me.chkloadfromdailysheet.Size = New System.Drawing.Size(131, 17)
        Me.chkloadfromdailysheet.TabIndex = 345524
        Me.chkloadfromdailysheet.Text = "Load from Attendance"
        Me.chkloadfromdailysheet.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(192, 41)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(770, 474)
        Me.TabControl1.TabIndex = 345525
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lblstatus)
        Me.TabPage1.Controls.Add(Me.grdVoucher)
        Me.TabPage1.Controls.Add(Me.dtpbookingdate)
        Me.TabPage1.Controls.Add(Me.chkselectall)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(762, 448)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Booking"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'lblstatus
        '
        Me.lblstatus.AutoSize = True
        Me.lblstatus.BackColor = System.Drawing.Color.Transparent
        Me.lblstatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.ForeColor = System.Drawing.Color.Green
        Me.lblstatus.Location = New System.Drawing.Point(5, 7)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(32, 13)
        Me.lblstatus.TabIndex = 345524
        Me.lblstatus.Text = "New"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.chkbookingdate)
        Me.TabPage2.Controls.Add(Me.btnloadbooking)
        Me.TabPage2.Controls.Add(Me.dtpto)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.dtpfrom)
        Me.TabPage2.Controls.Add(Me.grdlist)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(762, 448)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "List"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'chkbookingdate
        '
        Me.chkbookingdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkbookingdate.AutoSize = True
        Me.chkbookingdate.BackColor = System.Drawing.Color.Transparent
        Me.chkbookingdate.Location = New System.Drawing.Point(6, 416)
        Me.chkbookingdate.Name = "chkbookingdate"
        Me.chkbookingdate.Size = New System.Drawing.Size(115, 17)
        Me.chkbookingdate.TabIndex = 345528
        Me.chkbookingdate.Text = "Booking Date wise"
        Me.chkbookingdate.UseVisualStyleBackColor = False
        '
        'btnloadbooking
        '
        Me.btnloadbooking.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnloadbooking.BackColor = System.Drawing.Color.SteelBlue
        Me.btnloadbooking.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnloadbooking.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnloadbooking.ForeColor = System.Drawing.Color.White
        Me.btnloadbooking.Location = New System.Drawing.Point(679, 414)
        Me.btnloadbooking.Name = "btnloadbooking"
        Me.btnloadbooking.Size = New System.Drawing.Size(77, 28)
        Me.btnloadbooking.TabIndex = 345526
        Me.btnloadbooking.Text = "Load"
        Me.btnloadbooking.UseVisualStyleBackColor = False
        '
        'dtpto
        '
        Me.dtpto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpto.Location = New System.Drawing.Point(571, 422)
        Me.dtpto.Name = "dtpto"
        Me.dtpto.Size = New System.Drawing.Size(106, 20)
        Me.dtpto.TabIndex = 345527
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(388, 422)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 345526
        Me.Label2.Text = "Date Range"
        '
        'dtpfrom
        '
        Me.dtpfrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfrom.Location = New System.Drawing.Point(459, 422)
        Me.dtpfrom.Name = "dtpfrom"
        Me.dtpfrom.Size = New System.Drawing.Size(106, 20)
        Me.dtpfrom.TabIndex = 345522
        '
        'grdlist
        '
        Me.grdlist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdlist.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdlist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdlist.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken
        Me.grdlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdlist.Location = New System.Drawing.Point(6, 8)
        Me.grdlist.Name = "grdlist"
        Me.grdlist.Size = New System.Drawing.Size(750, 402)
        Me.grdlist.TabIndex = 345507
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(3, 4)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(119, 20)
        Me.Label27.TabIndex = 345527
        Me.Label27.Text = "Booking Total"
        '
        'lblworkAmt
        '
        Me.lblworkAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblworkAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblworkAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblworkAmt.ForeColor = System.Drawing.Color.Red
        Me.lblworkAmt.Location = New System.Drawing.Point(3, 24)
        Me.lblworkAmt.Name = "lblworkAmt"
        Me.lblworkAmt.Size = New System.Drawing.Size(149, 24)
        Me.lblworkAmt.TabIndex = 345526
        Me.lblworkAmt.Text = "0.00"
        Me.lblworkAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Timer1
        '
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(4, 92)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 13)
        Me.Label4.TabIndex = 345530
        Me.Label4.Text = "JV #"
        '
        'txtjv
        '
        Me.txtjv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtjv.Location = New System.Drawing.Point(5, 108)
        Me.txtjv.MaxLength = 30
        Me.txtjv.Name = "txtjv"
        Me.txtjv.ReadOnly = True
        Me.txtjv.Size = New System.Drawing.Size(90, 20)
        Me.txtjv.TabIndex = 345529
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(12, 154)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 345532
        Me.Label6.Text = "Date From"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(12, 197)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 13)
        Me.Label9.TabIndex = 345534
        Me.Label9.Text = "Date To"
        '
        'dtptodate
        '
        Me.dtptodate.CustomFormat = ""
        Me.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtptodate.Location = New System.Drawing.Point(16, 212)
        Me.dtptodate.Name = "dtptodate"
        Me.dtptodate.Size = New System.Drawing.Size(106, 20)
        Me.dtptodate.TabIndex = 345536
        '
        'btnclear
        '
        Me.btnclear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclear.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.ForeColor = System.Drawing.Color.White
        Me.btnclear.Location = New System.Drawing.Point(710, 521)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(83, 35)
        Me.btnclear.TabIndex = 345537
        Me.btnclear.Text = "Clear"
        Me.btnclear.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(3, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(123, 20)
        Me.Label7.TabIndex = 345539
        Me.Label7.Text = "Payment Total"
        '
        'lblnetAmt
        '
        Me.lblnetAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblnetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblnetAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnetAmt.ForeColor = System.Drawing.Color.Red
        Me.lblnetAmt.Location = New System.Drawing.Point(3, 34)
        Me.lblnetAmt.Name = "lblnetAmt"
        Me.lblnetAmt.Size = New System.Drawing.Size(149, 24)
        Me.lblnetAmt.TabIndex = 345538
        Me.lblnetAmt.Text = "0.00"
        Me.lblnetAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnupdateAccounts
        '
        Me.btnupdateAccounts.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupdateAccounts.Enabled = False
        Me.btnupdateAccounts.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdateAccounts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdateAccounts.ForeColor = System.Drawing.Color.White
        Me.btnupdateAccounts.Location = New System.Drawing.Point(99, 95)
        Me.btnupdateAccounts.Name = "btnupdateAccounts"
        Me.btnupdateAccounts.Size = New System.Drawing.Size(83, 45)
        Me.btnupdateAccounts.TabIndex = 345540
        Me.btnupdateAccounts.Text = "Update to Accounts"
        Me.btnupdateAccounts.UseVisualStyleBackColor = False
        '
        'btnpayment
        '
        Me.btnpayment.BackColor = System.Drawing.Color.SteelBlue
        Me.btnpayment.Enabled = False
        Me.btnpayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnpayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnpayment.ForeColor = System.Drawing.Color.White
        Me.btnpayment.Location = New System.Drawing.Point(43, 63)
        Me.btnpayment.Name = "btnpayment"
        Me.btnpayment.Size = New System.Drawing.Size(109, 31)
        Me.btnpayment.TabIndex = 345541
        Me.btnpayment.Text = "Add to Payment"
        Me.btnpayment.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label27)
        Me.Panel1.Controls.Add(Me.btnupdateAccounts)
        Me.Panel1.Controls.Add(Me.txtaccount)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.lblworkAmt)
        Me.Panel1.Controls.Add(Me.txtjv)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Location = New System.Drawing.Point(3, 284)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(187, 147)
        Me.Panel1.TabIndex = 345525
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.btnpayment)
        Me.Panel3.Controls.Add(Me.lblnetAmt)
        Me.Panel3.Location = New System.Drawing.Point(3, 434)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(187, 97)
        Me.Panel3.TabIndex = 345525
        '
        'btnpreview
        '
        Me.btnpreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnpreview.BackColor = System.Drawing.Color.SteelBlue
        Me.btnpreview.Enabled = False
        Me.btnpreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnpreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnpreview.ForeColor = System.Drawing.Color.White
        Me.btnpreview.Location = New System.Drawing.Point(625, 521)
        Me.btnpreview.Name = "btnpreview"
        Me.btnpreview.Size = New System.Drawing.Size(83, 35)
        Me.btnpreview.TabIndex = 345538
        Me.btnpreview.Text = "Preview"
        Me.btnpreview.UseVisualStyleBackColor = False
        '
        'chkpayslip
        '
        Me.chkpayslip.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkpayslip.AutoSize = True
        Me.chkpayslip.BackColor = System.Drawing.Color.Transparent
        Me.chkpayslip.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkpayslip.Location = New System.Drawing.Point(541, 531)
        Me.chkpayslip.Name = "chkpayslip"
        Me.chkpayslip.Size = New System.Drawing.Size(72, 17)
        Me.chkpayslip.TabIndex = 345539
        Me.chkpayslip.Text = "Pay Slip"
        Me.chkpayslip.UseVisualStyleBackColor = False
        Me.chkpayslip.Visible = False
        '
        'PaymentBookingFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(974, 560)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkpayslip)
        Me.Controls.Add(Me.btnpreview)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnclear)
        Me.Controls.Add(Me.dtptodate)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.chkloadfromdailysheet)
        Me.Controls.Add(Me.rdounit)
        Me.Controls.Add(Me.rdosalary)
        Me.Controls.Add(Me.rdodaily)
        Me.Controls.Add(Me.BtnUpdate)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.btnload)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.cldrdate)
        Me.Controls.Add(Me.btndelete)
        Me.Name = "PaymentBookingFrm"
        Me.Text = "PaymentBookingFrm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.grdlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents rdosalary As System.Windows.Forms.RadioButton
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents grdVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents rdodaily As System.Windows.Forms.RadioButton
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents cldrdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btndelete As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtaccount As System.Windows.Forms.TextBox
    Friend WithEvents dtpbookingdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkselectall As System.Windows.Forms.CheckBox
    Friend WithEvents rdounit As System.Windows.Forms.RadioButton
    Friend WithEvents chkloadfromdailysheet As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents btnloadbooking As System.Windows.Forms.Button
    Friend WithEvents dtpto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpfrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents grdlist As System.Windows.Forms.DataGridView
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents lblworkAmt As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtjv As System.Windows.Forms.TextBox
    Friend WithEvents lblstatus As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtptodate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkbookingdate As System.Windows.Forms.CheckBox
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblnetAmt As System.Windows.Forms.Label
    Friend WithEvents btnupdateAccounts As System.Windows.Forms.Button
    Friend WithEvents btnpayment As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnpreview As System.Windows.Forms.Button
    Friend WithEvents chkpayslip As System.Windows.Forms.CheckBox
End Class
