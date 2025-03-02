<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VisitNoteFrm
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
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cldrdate = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbdoctor = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtCashCustomer = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.txtcomment = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtobservation = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtdoctornote = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmdOk = New System.Windows.Forms.Button
        Me.btnclose = New System.Windows.Forms.Button
        Me.lblvisitnumber = New System.Windows.Forms.Label
        Me.dtpfollowup = New System.Windows.Forms.DateTimePicker
        Me.chkfollowup = New System.Windows.Forms.CheckBox
        Me.txtfollowup = New System.Windows.Forms.TextBox
        Me.txtremark = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.txttreatmentdescription = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.btnclearmedicine = New System.Windows.Forms.Button
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtdays = New System.Windows.Forms.TextBox
        Me.txtusage = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtqty = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtdescription = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtitemname = New System.Windows.Forms.TextBox
        Me.btnrem = New System.Windows.Forms.Button
        Me.btnitmAdd = New System.Windows.Forms.Button
        Me.grdmedicine = New System.Windows.Forms.DataGridView
        Me.btndelete = New System.Windows.Forms.Button
        Me.btnclear = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdmedicine, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(1111, 33)
        Me.Panel1.TabIndex = 345445
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 20)
        Me.PictureBox1.TabIndex = 345458
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(39, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 18)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Visit Note"
        '
        'cldrdate
        '
        Me.cldrdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrdate.Location = New System.Drawing.Point(91, 3)
        Me.cldrdate.Name = "cldrdate"
        Me.cldrdate.Size = New System.Drawing.Size(106, 20)
        Me.cldrdate.TabIndex = 345446
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 15)
        Me.Label3.TabIndex = 345447
        Me.Label3.Text = "Date"
        '
        'cmbdoctor
        '
        Me.cmbdoctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbdoctor.FormattingEnabled = True
        Me.cmbdoctor.Location = New System.Drawing.Point(91, 29)
        Me.cmbdoctor.Name = "cmbdoctor"
        Me.cmbdoctor.Size = New System.Drawing.Size(196, 21)
        Me.cmbdoctor.TabIndex = 345471
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(6, 33)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 13)
        Me.Label9.TabIndex = 345472
        Me.Label9.Text = "Doctor"
        '
        'txtCashCustomer
        '
        Me.txtCashCustomer.BackColor = System.Drawing.Color.White
        Me.txtCashCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCashCustomer.Location = New System.Drawing.Point(91, 56)
        Me.txtCashCustomer.Name = "txtCashCustomer"
        Me.txtCashCustomer.ReadOnly = True
        Me.txtCashCustomer.Size = New System.Drawing.Size(313, 21)
        Me.txtCashCustomer.TabIndex = 345483
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(6, 57)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(52, 15)
        Me.Label32.TabIndex = 345484
        Me.Label32.Text = "Patient"
        '
        'txtcomment
        '
        Me.txtcomment.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcomment.Location = New System.Drawing.Point(91, 83)
        Me.txtcomment.Multiline = True
        Me.txtcomment.Name = "txtcomment"
        Me.txtcomment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtcomment.Size = New System.Drawing.Size(313, 127)
        Me.txtcomment.TabIndex = 345485
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(6, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 15)
        Me.Label2.TabIndex = 345486
        Me.Label2.Text = "Comment"
        '
        'txtobservation
        '
        Me.txtobservation.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtobservation.Location = New System.Drawing.Point(91, 216)
        Me.txtobservation.Multiline = True
        Me.txtobservation.Name = "txtobservation"
        Me.txtobservation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtobservation.Size = New System.Drawing.Size(313, 141)
        Me.txtobservation.TabIndex = 345487
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(6, 217)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 15)
        Me.Label4.TabIndex = 345488
        Me.Label4.Text = "Observation"
        '
        'txtdoctornote
        '
        Me.txtdoctornote.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdoctornote.Location = New System.Drawing.Point(418, 26)
        Me.txtdoctornote.Multiline = True
        Me.txtdoctornote.Name = "txtdoctornote"
        Me.txtdoctornote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtdoctornote.Size = New System.Drawing.Size(666, 151)
        Me.txtdoctornote.TabIndex = 345489
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(415, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 15)
        Me.Label5.TabIndex = 345490
        Me.Label5.Text = "Doctor's Note"
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOk.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.Color.White
        Me.cmdOk.Location = New System.Drawing.Point(931, 570)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(81, 35)
        Me.cmdOk.TabIndex = 345491
        Me.cmdOk.Text = "&Update"
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ForeColor = System.Drawing.Color.White
        Me.btnclose.Location = New System.Drawing.Point(1016, 570)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(82, 35)
        Me.btnclose.TabIndex = 345492
        Me.btnclose.Text = "E&xit"
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'lblvisitnumber
        '
        Me.lblvisitnumber.AutoSize = True
        Me.lblvisitnumber.BackColor = System.Drawing.Color.Transparent
        Me.lblvisitnumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvisitnumber.ForeColor = System.Drawing.Color.Black
        Me.lblvisitnumber.Location = New System.Drawing.Point(203, 3)
        Me.lblvisitnumber.Name = "lblvisitnumber"
        Me.lblvisitnumber.Size = New System.Drawing.Size(89, 15)
        Me.lblvisitnumber.TabIndex = 345493
        Me.lblvisitnumber.Text = "Visit Number"
        '
        'dtpfollowup
        '
        Me.dtpfollowup.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfollowup.Location = New System.Drawing.Point(91, 363)
        Me.dtpfollowup.Name = "dtpfollowup"
        Me.dtpfollowup.Size = New System.Drawing.Size(106, 20)
        Me.dtpfollowup.TabIndex = 345494
        Me.dtpfollowup.Visible = False
        '
        'chkfollowup
        '
        Me.chkfollowup.AutoSize = True
        Me.chkfollowup.BackColor = System.Drawing.Color.Transparent
        Me.chkfollowup.Location = New System.Drawing.Point(9, 363)
        Me.chkfollowup.Name = "chkfollowup"
        Me.chkfollowup.Size = New System.Drawing.Size(73, 17)
        Me.chkfollowup.TabIndex = 345496
        Me.chkfollowup.Text = "Follow Up"
        Me.chkfollowup.UseVisualStyleBackColor = False
        Me.chkfollowup.Visible = False
        '
        'txtfollowup
        '
        Me.txtfollowup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfollowup.Location = New System.Drawing.Point(91, 387)
        Me.txtfollowup.Multiline = True
        Me.txtfollowup.Name = "txtfollowup"
        Me.txtfollowup.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtfollowup.Size = New System.Drawing.Size(313, 103)
        Me.txtfollowup.TabIndex = 345497
        Me.txtfollowup.Visible = False
        '
        'txtremark
        '
        Me.txtremark.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremark.Location = New System.Drawing.Point(415, 363)
        Me.txtremark.Multiline = True
        Me.txtremark.Name = "txtremark"
        Me.txtremark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtremark.Size = New System.Drawing.Size(660, 127)
        Me.txtremark.TabIndex = 345498
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(418, 342)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 15)
        Me.Label6.TabIndex = 345499
        Me.Label6.Text = "Other Remark"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(9, 39)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1095, 525)
        Me.TabControl1.TabIndex = 345500
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txttreatmentdescription)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.txtremark)
        Me.TabPage1.Controls.Add(Me.cldrdate)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.txtfollowup)
        Me.TabPage1.Controls.Add(Me.cmbdoctor)
        Me.TabPage1.Controls.Add(Me.txtdoctornote)
        Me.TabPage1.Controls.Add(Me.chkfollowup)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label32)
        Me.TabPage1.Controls.Add(Me.dtpfollowup)
        Me.TabPage1.Controls.Add(Me.txtCashCustomer)
        Me.TabPage1.Controls.Add(Me.lblvisitnumber)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.txtcomment)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.txtobservation)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1087, 499)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Note"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'txttreatmentdescription
        '
        Me.txttreatmentdescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttreatmentdescription.Location = New System.Drawing.Point(418, 200)
        Me.txttreatmentdescription.Multiline = True
        Me.txttreatmentdescription.Name = "txttreatmentdescription"
        Me.txttreatmentdescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txttreatmentdescription.Size = New System.Drawing.Size(660, 139)
        Me.txttreatmentdescription.TabIndex = 345500
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(418, 180)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(128, 15)
        Me.Label7.TabIndex = 345501
        Me.Label7.Text = "Treatment Description"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btnclearmedicine)
        Me.TabPage2.Controls.Add(Me.Label13)
        Me.TabPage2.Controls.Add(Me.txtdays)
        Me.TabPage2.Controls.Add(Me.txtusage)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.txtqty)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.txtdescription)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.txtitemname)
        Me.TabPage2.Controls.Add(Me.btnrem)
        Me.TabPage2.Controls.Add(Me.btnitmAdd)
        Me.TabPage2.Controls.Add(Me.grdmedicine)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1087, 499)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Medicine"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnclearmedicine
        '
        Me.btnclearmedicine.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclearmedicine.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclearmedicine.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclearmedicine.ForeColor = System.Drawing.Color.White
        Me.btnclearmedicine.Location = New System.Drawing.Point(127, 172)
        Me.btnclearmedicine.Name = "btnclearmedicine"
        Me.btnclearmedicine.Size = New System.Drawing.Size(55, 24)
        Me.btnclearmedicine.TabIndex = 345495
        Me.btnclearmedicine.Text = "Clear"
        Me.btnclearmedicine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclearmedicine.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(8, 61)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(34, 15)
        Me.Label13.TabIndex = 345494
        Me.Label13.Text = "Days"
        '
        'txtdays
        '
        Me.txtdays.BackColor = System.Drawing.Color.White
        Me.txtdays.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdays.Location = New System.Drawing.Point(85, 61)
        Me.txtdays.Name = "txtdays"
        Me.txtdays.Size = New System.Drawing.Size(87, 21)
        Me.txtdays.TabIndex = 4
        Me.txtdays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtusage
        '
        Me.txtusage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtusage.Location = New System.Drawing.Point(525, 7)
        Me.txtusage.Multiline = True
        Me.txtusage.Name = "txtusage"
        Me.txtusage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtusage.Size = New System.Drawing.Size(556, 189)
        Me.txtusage.TabIndex = 5
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(476, 10)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(43, 15)
        Me.Label12.TabIndex = 345492
        Me.Label12.Text = "Usage"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(8, 34)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(24, 15)
        Me.Label11.TabIndex = 345490
        Me.Label11.Text = "Qty"
        '
        'txtqty
        '
        Me.txtqty.BackColor = System.Drawing.Color.White
        Me.txtqty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtqty.Location = New System.Drawing.Point(85, 34)
        Me.txtqty.Name = "txtqty"
        Me.txtqty.Size = New System.Drawing.Size(87, 21)
        Me.txtqty.TabIndex = 3
        Me.txtqty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(8, 115)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 15)
        Me.Label10.TabIndex = 345488
        Me.Label10.Text = "Description"
        Me.Label10.Visible = False
        '
        'txtdescription
        '
        Me.txtdescription.BackColor = System.Drawing.Color.White
        Me.txtdescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdescription.Location = New System.Drawing.Point(85, 115)
        Me.txtdescription.Name = "txtdescription"
        Me.txtdescription.Size = New System.Drawing.Size(358, 21)
        Me.txtdescription.TabIndex = 2
        Me.txtdescription.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(8, 7)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(58, 15)
        Me.Label8.TabIndex = 345486
        Me.Label8.Text = "Medicine"
        '
        'txtitemname
        '
        Me.txtitemname.BackColor = System.Drawing.Color.White
        Me.txtitemname.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtitemname.Location = New System.Drawing.Point(85, 7)
        Me.txtitemname.Name = "txtitemname"
        Me.txtitemname.Size = New System.Drawing.Size(358, 21)
        Me.txtitemname.TabIndex = 1
        '
        'btnrem
        '
        Me.btnrem.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrem.ForeColor = System.Drawing.Color.White
        Me.btnrem.Location = New System.Drawing.Point(68, 172)
        Me.btnrem.Name = "btnrem"
        Me.btnrem.Size = New System.Drawing.Size(55, 24)
        Me.btnrem.TabIndex = 345450
        Me.btnrem.Text = "Rem"
        Me.btnrem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrem.UseVisualStyleBackColor = False
        '
        'btnitmAdd
        '
        Me.btnitmAdd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnitmAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnitmAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnitmAdd.ForeColor = System.Drawing.Color.White
        Me.btnitmAdd.Location = New System.Drawing.Point(9, 172)
        Me.btnitmAdd.Name = "btnitmAdd"
        Me.btnitmAdd.Size = New System.Drawing.Size(55, 24)
        Me.btnitmAdd.TabIndex = 6
        Me.btnitmAdd.Text = "Add"
        Me.btnitmAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnitmAdd.UseVisualStyleBackColor = False
        '
        'grdmedicine
        '
        Me.grdmedicine.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdmedicine.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.grdmedicine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdmedicine.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdmedicine.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdmedicine.Location = New System.Drawing.Point(6, 202)
        Me.grdmedicine.Name = "grdmedicine"
        Me.grdmedicine.Size = New System.Drawing.Size(1075, 291)
        Me.grdmedicine.TabIndex = 503
        Me.grdmedicine.TabStop = False
        '
        'btndelete
        '
        Me.btndelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btndelete.BackColor = System.Drawing.Color.SteelBlue
        Me.btndelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.ForeColor = System.Drawing.Color.White
        Me.btndelete.Location = New System.Drawing.Point(14, 566)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btndelete.Size = New System.Drawing.Size(81, 35)
        Me.btndelete.TabIndex = 345501
        Me.btndelete.Text = "Delete"
        Me.btndelete.UseVisualStyleBackColor = False
        '
        'btnclear
        '
        Me.btnclear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclear.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclear.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.ForeColor = System.Drawing.Color.White
        Me.btnclear.Location = New System.Drawing.Point(846, 570)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnclear.Size = New System.Drawing.Size(81, 35)
        Me.btnclear.TabIndex = 345502
        Me.btnclear.Text = "Clear"
        Me.btnclear.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        '
        'VisitNoteFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1111, 619)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnclear)
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnclose)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "VisitNoteFrm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Visit Note"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.grdmedicine, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cldrdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbdoctor As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCashCustomer As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtcomment As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtobservation As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtdoctornote As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents lblvisitnumber As System.Windows.Forms.Label
    Friend WithEvents dtpfollowup As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkfollowup As System.Windows.Forms.CheckBox
    Friend WithEvents txtfollowup As System.Windows.Forms.TextBox
    Friend WithEvents txtremark As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents grdmedicine As System.Windows.Forms.DataGridView
    Friend WithEvents btnrem As System.Windows.Forms.Button
    Friend WithEvents btnitmAdd As System.Windows.Forms.Button
    Friend WithEvents txttreatmentdescription As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtqty As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtdescription As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtitemname As System.Windows.Forms.TextBox
    Friend WithEvents txtusage As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents btndelete As System.Windows.Forms.Button
    Public WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtdays As System.Windows.Forms.TextBox
    Friend WithEvents btnclearmedicine As System.Windows.Forms.Button
End Class
