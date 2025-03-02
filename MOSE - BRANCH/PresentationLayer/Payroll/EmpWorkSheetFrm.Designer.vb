<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EmpworksheetFrm
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
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.cldrdateFrom = New System.Windows.Forms.DateTimePicker
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label26 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.lblName = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lblrate = New System.Windows.Forms.Label
        Me.chkloadsalescommission = New System.Windows.Forms.CheckBox
        Me.lblsaleman = New System.Windows.Forms.Label
        Me.lblsalarytype = New System.Windows.Forms.Label
        Me.lbldesignation = New System.Windows.Forms.Label
        Me.lblemployee = New System.Windows.Forms.Label
        Me.lbllastdate = New System.Windows.Forms.Label
        Me.chkselectall = New System.Windows.Forms.CheckBox
        Me.btnload = New System.Windows.Forms.Button
        Me.btnclose = New System.Windows.Forms.Button
        Me.BtnUpdate = New System.Windows.Forms.Button
        Me.btndelete = New System.Windows.Forms.Button
        Me.cmbcolms = New System.Windows.Forms.ComboBox
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.btnfind = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblNetAmt = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.btnpreview = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpdateTo = New System.Windows.Forms.DateTimePicker
        Me.cmbemployee = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblpay = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblcommission = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
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
        Me.grdVoucher.Location = New System.Drawing.Point(247, 42)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(671, 421)
        Me.grdVoucher.TabIndex = 137
        '
        'cldrdateFrom
        '
        Me.cldrdateFrom.CustomFormat = "dd/MM/yyyy"
        Me.cldrdateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.cldrdateFrom.Location = New System.Drawing.Point(59, 69)
        Me.cldrdateFrom.Name = "cldrdateFrom"
        Me.cldrdateFrom.Size = New System.Drawing.Size(101, 20)
        Me.cldrdateFrom.TabIndex = 138
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
        Me.Panel2.Size = New System.Drawing.Size(930, 34)
        Me.Panel2.TabIndex = 345422
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(50, 5)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(218, 20)
        Me.Label26.TabIndex = 345461
        Me.Label26.Text = "Employee wise Worksheet"
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(4, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 345423
        Me.Label1.Text = "From"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.MistyRose
        Me.Panel3.Controls.Add(Me.lblrate)
        Me.Panel3.Controls.Add(Me.chkloadsalescommission)
        Me.Panel3.Controls.Add(Me.lblsaleman)
        Me.Panel3.Controls.Add(Me.lblsalarytype)
        Me.Panel3.Controls.Add(Me.lbldesignation)
        Me.Panel3.Controls.Add(Me.lblemployee)
        Me.Panel3.Controls.Add(Me.lbllastdate)
        Me.Panel3.Location = New System.Drawing.Point(7, 138)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(230, 156)
        Me.Panel3.TabIndex = 345492
        '
        'lblrate
        '
        Me.lblrate.AutoSize = True
        Me.lblrate.BackColor = System.Drawing.Color.Transparent
        Me.lblrate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblrate.ForeColor = System.Drawing.Color.Black
        Me.lblrate.Location = New System.Drawing.Point(7, 59)
        Me.lblrate.Name = "lblrate"
        Me.lblrate.Size = New System.Drawing.Size(37, 15)
        Me.lblrate.TabIndex = 345518
        Me.lblrate.Text = "Rate"
        '
        'chkloadsalescommission
        '
        Me.chkloadsalescommission.AutoSize = True
        Me.chkloadsalescommission.BackColor = System.Drawing.Color.Transparent
        Me.chkloadsalescommission.Location = New System.Drawing.Point(3, 136)
        Me.chkloadsalescommission.Name = "chkloadsalescommission"
        Me.chkloadsalescommission.Size = New System.Drawing.Size(191, 17)
        Me.chkloadsalescommission.TabIndex = 345517
        Me.chkloadsalescommission.Text = "Load Sales && Commssion Datewise"
        Me.chkloadsalescommission.UseVisualStyleBackColor = False
        '
        'lblsaleman
        '
        Me.lblsaleman.AutoSize = True
        Me.lblsaleman.BackColor = System.Drawing.Color.Transparent
        Me.lblsaleman.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsaleman.ForeColor = System.Drawing.Color.Black
        Me.lblsaleman.Location = New System.Drawing.Point(7, 80)
        Me.lblsaleman.Name = "lblsaleman"
        Me.lblsaleman.Size = New System.Drawing.Size(71, 15)
        Me.lblsaleman.TabIndex = 345516
        Me.lblsaleman.Text = "Salesman"
        '
        'lblsalarytype
        '
        Me.lblsalarytype.AutoSize = True
        Me.lblsalarytype.BackColor = System.Drawing.Color.Transparent
        Me.lblsalarytype.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsalarytype.ForeColor = System.Drawing.Color.Black
        Me.lblsalarytype.Location = New System.Drawing.Point(7, 40)
        Me.lblsalarytype.Name = "lblsalarytype"
        Me.lblsalarytype.Size = New System.Drawing.Size(74, 13)
        Me.lblsalarytype.TabIndex = 345515
        Me.lblsalarytype.Text = "Salary Type"
        '
        'lbldesignation
        '
        Me.lbldesignation.AutoSize = True
        Me.lbldesignation.BackColor = System.Drawing.Color.Transparent
        Me.lbldesignation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldesignation.ForeColor = System.Drawing.Color.Black
        Me.lbldesignation.Location = New System.Drawing.Point(7, 21)
        Me.lbldesignation.Name = "lbldesignation"
        Me.lbldesignation.Size = New System.Drawing.Size(74, 13)
        Me.lbldesignation.TabIndex = 345514
        Me.lbldesignation.Text = "Designation"
        '
        'lblemployee
        '
        Me.lblemployee.AutoSize = True
        Me.lblemployee.BackColor = System.Drawing.Color.Transparent
        Me.lblemployee.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblemployee.ForeColor = System.Drawing.Color.Black
        Me.lblemployee.Location = New System.Drawing.Point(7, 2)
        Me.lblemployee.Name = "lblemployee"
        Me.lblemployee.Size = New System.Drawing.Size(39, 13)
        Me.lblemployee.TabIndex = 345513
        Me.lblemployee.Text = "Name"
        '
        'lbllastdate
        '
        Me.lbllastdate.AutoSize = True
        Me.lbllastdate.BackColor = System.Drawing.Color.Transparent
        Me.lbllastdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbllastdate.ForeColor = System.Drawing.Color.Black
        Me.lbllastdate.Location = New System.Drawing.Point(7, 101)
        Me.lbllastdate.Name = "lbllastdate"
        Me.lbllastdate.Size = New System.Drawing.Size(66, 15)
        Me.lbllastdate.TabIndex = 345513
        Me.lbllastdate.Text = "Last date"
        '
        'chkselectall
        '
        Me.chkselectall.AutoSize = True
        Me.chkselectall.BackColor = System.Drawing.Color.Transparent
        Me.chkselectall.Location = New System.Drawing.Point(167, 119)
        Me.chkselectall.Name = "chkselectall"
        Me.chkselectall.Size = New System.Drawing.Size(70, 17)
        Me.chkselectall.TabIndex = 345508
        Me.chkselectall.Text = "Select All"
        Me.chkselectall.UseVisualStyleBackColor = False
        '
        'btnload
        '
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.Location = New System.Drawing.Point(167, 88)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(70, 25)
        Me.btnload.TabIndex = 345500
        Me.btnload.Text = "Load"
        Me.btnload.UseVisualStyleBackColor = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ForeColor = System.Drawing.Color.White
        Me.btnclose.Location = New System.Drawing.Point(836, 467)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(82, 35)
        Me.btnclose.TabIndex = 345501
        Me.btnclose.Text = "E&xit"
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'BtnUpdate
        '
        Me.BtnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnUpdate.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdate.ForeColor = System.Drawing.Color.White
        Me.BtnUpdate.Location = New System.Drawing.Point(751, 467)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnUpdate.Size = New System.Drawing.Size(83, 35)
        Me.BtnUpdate.TabIndex = 345504
        Me.BtnUpdate.Tag = "56"
        Me.BtnUpdate.Text = "&Update"
        Me.BtnUpdate.UseVisualStyleBackColor = False
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.BackColor = System.Drawing.Color.SteelBlue
        Me.btndelete.Enabled = False
        Me.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.ForeColor = System.Drawing.Color.White
        Me.btndelete.Location = New System.Drawing.Point(3, 467)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(83, 35)
        Me.btndelete.TabIndex = 345505
        Me.btndelete.Text = "&Delete"
        Me.btndelete.UseVisualStyleBackColor = False
        '
        'cmbcolms
        '
        Me.cmbcolms.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbcolms.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcolms.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbcolms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcolms.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcolms.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcolms.Location = New System.Drawing.Point(247, 467)
        Me.cmbcolms.Name = "cmbcolms"
        Me.cmbcolms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbcolms.Size = New System.Drawing.Size(123, 22)
        Me.cmbcolms.TabIndex = 345506
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
        Me.txtSeq.Location = New System.Drawing.Point(376, 467)
        Me.txtSeq.MaxLength = 50
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(197, 20)
        Me.txtSeq.TabIndex = 345507
        '
        'btnfind
        '
        Me.btnfind.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnfind.BackColor = System.Drawing.Color.SteelBlue
        Me.btnfind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnfind.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnfind.ForeColor = System.Drawing.Color.White
        Me.btnfind.Location = New System.Drawing.Point(579, 466)
        Me.btnfind.Name = "btnfind"
        Me.btnfind.Size = New System.Drawing.Size(66, 23)
        Me.btnfind.TabIndex = 345508
        Me.btnfind.Text = "Search"
        Me.btnfind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnfind.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        '
        'lblNetAmt
        '
        Me.lblNetAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblNetAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNetAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmt.ForeColor = System.Drawing.Color.Red
        Me.lblNetAmt.Location = New System.Drawing.Point(88, 421)
        Me.lblNetAmt.Name = "lblNetAmt"
        Me.lblNetAmt.Size = New System.Drawing.Size(149, 42)
        Me.lblNetAmt.TabIndex = 345510
        Me.lblNetAmt.Text = "0.00"
        Me.lblNetAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label27
        '
        Me.Label27.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(2, 421)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(65, 25)
        Me.Label27.TabIndex = 345511
        Me.Label27.Text = "Total"
        '
        'btnpreview
        '
        Me.btnpreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnpreview.BackColor = System.Drawing.Color.SteelBlue
        Me.btnpreview.Enabled = False
        Me.btnpreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnpreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnpreview.ForeColor = System.Drawing.Color.White
        Me.btnpreview.Location = New System.Drawing.Point(666, 467)
        Me.btnpreview.Name = "btnpreview"
        Me.btnpreview.Size = New System.Drawing.Size(83, 35)
        Me.btnpreview.TabIndex = 345514
        Me.btnpreview.Text = "Preview"
        Me.btnpreview.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(4, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(20, 13)
        Me.Label2.TabIndex = 345516
        Me.Label2.Text = "To"
        '
        'dtpdateTo
        '
        Me.dtpdateTo.CustomFormat = "dd/MM/yyyy"
        Me.dtpdateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpdateTo.Location = New System.Drawing.Point(59, 93)
        Me.dtpdateTo.Name = "dtpdateTo"
        Me.dtpdateTo.Size = New System.Drawing.Size(101, 20)
        Me.dtpdateTo.TabIndex = 345515
        '
        'cmbemployee
        '
        Me.cmbemployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbemployee.FormattingEnabled = True
        Me.cmbemployee.Location = New System.Drawing.Point(59, 42)
        Me.cmbemployee.Name = "cmbemployee"
        Me.cmbemployee.Size = New System.Drawing.Size(178, 21)
        Me.cmbemployee.TabIndex = 345517
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(4, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 345518
        Me.Label3.Text = "Employee"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(1, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 15)
        Me.Label4.TabIndex = 345520
        Me.Label4.Text = "Total Pay"
        '
        'lblpay
        '
        Me.lblpay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblpay.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblpay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblpay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpay.ForeColor = System.Drawing.Color.Red
        Me.lblpay.Location = New System.Drawing.Point(114, 4)
        Me.lblpay.Name = "lblpay"
        Me.lblpay.Size = New System.Drawing.Size(118, 23)
        Me.lblpay.TabIndex = 345519
        Me.lblpay.Text = "0.00"
        Me.lblpay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(1, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(113, 15)
        Me.Label5.TabIndex = 345522
        Me.Label5.Text = "Commission Pay"
        '
        'lblcommission
        '
        Me.lblcommission.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblcommission.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblcommission.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblcommission.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcommission.ForeColor = System.Drawing.Color.Red
        Me.lblcommission.Location = New System.Drawing.Point(114, 31)
        Me.lblcommission.Name = "lblcommission"
        Me.lblcommission.Size = New System.Drawing.Size(118, 23)
        Me.lblcommission.TabIndex = 345521
        Me.lblcommission.Text = "0.00"
        Me.lblcommission.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblcommission)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.lblpay)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Location = New System.Drawing.Point(2, 298)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(245, 70)
        Me.Panel1.TabIndex = 345523
        '
        'EmpworksheetFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(930, 508)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbemployee)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtpdateTo)
        Me.Controls.Add(Me.btnpreview)
        Me.Controls.Add(Me.chkselectall)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.lblNetAmt)
        Me.Controls.Add(Me.btnfind)
        Me.Controls.Add(Me.cmbcolms)
        Me.Controls.Add(Me.txtSeq)
        Me.Controls.Add(Me.BtnUpdate)
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.btnload)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.cldrdateFrom)
        Me.Controls.Add(Me.grdVoucher)
        Me.Name = "EmpworksheetFrm"
        Me.Text = "DailyWorkSheetFrm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents cldrdateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Public WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents btndelete As System.Windows.Forms.Button
    Public WithEvents cmbcolms As System.Windows.Forms.ComboBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Friend WithEvents btnfind As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblNetAmt As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents chkselectall As System.Windows.Forms.CheckBox
    Friend WithEvents lbllastdate As System.Windows.Forms.Label
    Friend WithEvents btnpreview As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpdateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbemployee As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblemployee As System.Windows.Forms.Label
    Friend WithEvents lblsalarytype As System.Windows.Forms.Label
    Friend WithEvents lbldesignation As System.Windows.Forms.Label
    Friend WithEvents lblsaleman As System.Windows.Forms.Label
    Friend WithEvents chkloadsalescommission As System.Windows.Forms.CheckBox
    Friend WithEvents lblrate As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblpay As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblcommission As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
