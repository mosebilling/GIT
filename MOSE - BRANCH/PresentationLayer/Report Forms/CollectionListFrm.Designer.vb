<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CollectionListFrm
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
        Me.btnrv = New System.Windows.Forms.Button
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.btnload = New System.Windows.Forms.Button
        Me.dvData = New System.Windows.Forms.DataGridView
        Me.dtpdate = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkFormat = New System.Windows.Forms.CheckBox
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btnexit = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.lblcap = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lbltodaycollection = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblcollection = New System.Windows.Forms.Label
        Me.lbltotalbalance = New System.Windows.Forms.Label
        Me.lblreceived = New System.Windows.Forms.Label
        Me.lbltotalInvoice = New System.Windows.Forms.Label
        Me.rdocollection = New System.Windows.Forms.RadioButton
        Me.rdoall = New System.Windows.Forms.RadioButton
        Me.cmbtype = New System.Windows.Forms.ComboBox
        Me.chkrvnot = New System.Windows.Forms.CheckBox
        Me.rdoinvoice = New System.Windows.Forms.RadioButton
        Me.chkinstallment = New System.Windows.Forms.CheckBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmbsalesman = New System.Windows.Forms.ComboBox
        Me.lblusr = New System.Windows.Forms.Label
        Me.cmbuser = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbstatus = New System.Windows.Forms.ComboBox
        Me.chkcollectionwise = New System.Windows.Forms.CheckBox
        CType(Me.dvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnrv
        '
        Me.btnrv.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnrv.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrv.FlatAppearance.BorderSize = 0
        Me.btnrv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrv.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrv.ForeColor = System.Drawing.Color.White
        Me.btnrv.Location = New System.Drawing.Point(584, 485)
        Me.btnrv.Name = "btnrv"
        Me.btnrv.Size = New System.Drawing.Size(97, 33)
        Me.btnrv.TabIndex = 345488
        Me.btnrv.Text = "Creative RV"
        Me.btnrv.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrv.UseVisualStyleBackColor = False
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.Checked = True
        Me.chkSearch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(420, 485)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345486
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
        Me.txtSeq.Location = New System.Drawing.Point(179, 486)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(235, 20)
        Me.txtSeq.TabIndex = 345485
        '
        'cmbOrder
        '
        Me.cmbOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOrder.Location = New System.Drawing.Point(9, 485)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOrder.Size = New System.Drawing.Size(166, 22)
        Me.cmbOrder.TabIndex = 345484
        Me.cmbOrder.TabStop = False
        '
        'btnload
        '
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.FlatAppearance.BorderSize = 0
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.Location = New System.Drawing.Point(143, 42)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(63, 30)
        Me.btnload.TabIndex = 345483
        Me.btnload.Text = "Load"
        Me.btnload.UseVisualStyleBackColor = False
        '
        'dvData
        '
        Me.dvData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dvData.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dvData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dvData.Location = New System.Drawing.Point(12, 103)
        Me.dvData.Name = "dvData"
        Me.dvData.Size = New System.Drawing.Size(1095, 376)
        Me.dvData.TabIndex = 345482
        '
        'dtpdate
        '
        Me.dtpdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpdate.Location = New System.Drawing.Point(42, 47)
        Me.dtpdate.Name = "dtpdate"
        Me.dtpdate.Size = New System.Drawing.Size(95, 20)
        Me.dtpdate.TabIndex = 345481
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 345480
        Me.Label1.Text = "Date"
        '
        'chkFormat
        '
        Me.chkFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(847, 554)
        Me.chkFormat.Name = "chkFormat"
        Me.chkFormat.Size = New System.Drawing.Size(65, 19)
        Me.chkFormat.TabIndex = 345491
        Me.chkFormat.Text = "Format"
        Me.chkFormat.UseVisualStyleBackColor = False
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.BackColor = System.Drawing.Color.SteelBlue
        Me.btnPreview.Enabled = False
        Me.btnPreview.FlatAppearance.BorderSize = 0
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.White
        Me.btnPreview.Location = New System.Drawing.Point(918, 540)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(93, 33)
        Me.btnPreview.TabIndex = 345490
        Me.btnPreview.TabStop = False
        Me.btnPreview.Text = "Pre&view"
        Me.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'btnexit
        '
        Me.btnexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.FlatAppearance.BorderSize = 0
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.Location = New System.Drawing.Point(1014, 540)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(93, 33)
        Me.btnexit.TabIndex = 345489
        Me.btnexit.Text = "E&xit"
        Me.btnexit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PictureBox3)
        Me.Panel1.Controls.Add(Me.lblcap)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1119, 33)
        Me.Panel1.TabIndex = 345492
        '
        'PictureBox3
        '
        Me.PictureBox3.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox3.Location = New System.Drawing.Point(7, 5)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(34, 20)
        Me.PictureBox3.TabIndex = 345460
        Me.PictureBox3.TabStop = False
        '
        'lblcap
        '
        Me.lblcap.AutoSize = True
        Me.lblcap.BackColor = System.Drawing.Color.Transparent
        Me.lblcap.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcap.ForeColor = System.Drawing.Color.Black
        Me.lblcap.Location = New System.Drawing.Point(41, 6)
        Me.lblcap.Name = "lblcap"
        Me.lblcap.Size = New System.Drawing.Size(101, 18)
        Me.lblcap.TabIndex = 6
        Me.lblcap.Text = "Collection List"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(246, 560)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(119, 16)
        Me.Label8.TabIndex = 345506
        Me.Label8.Tag = ""
        Me.Label8.Text = "Today Collection : "
        '
        'lbltodaycollection
        '
        Me.lbltodaycollection.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbltodaycollection.BackColor = System.Drawing.Color.Transparent
        Me.lbltodaycollection.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltodaycollection.Location = New System.Drawing.Point(356, 560)
        Me.lbltodaycollection.Name = "lbltodaycollection"
        Me.lbltodaycollection.Size = New System.Drawing.Size(109, 16)
        Me.lbltodaycollection.TabIndex = 345505
        Me.lbltodaycollection.Tag = "Installment Amt.  :"
        Me.lbltodaycollection.Text = "0.00"
        Me.lbltodaycollection.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(247, 538)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 16)
        Me.Label6.TabIndex = 345503
        Me.Label6.Tag = ""
        Me.Label6.Text = "Today RV.   :"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 560)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 16)
        Me.Label2.TabIndex = 345501
        Me.Label2.Tag = "Balance Amt.    :"
        Me.Label2.Text = "Total Pending"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 538)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 16)
        Me.Label3.TabIndex = 345500
        Me.Label3.Tag = "Received Amt. :"
        Me.Label3.Text = "Total Online"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(11, 517)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 16)
        Me.Label4.TabIndex = 345499
        Me.Label4.Tag = "Invoice Amt.      :"
        Me.Label4.Text = "Total Cash"
        '
        'lblcollection
        '
        Me.lblcollection.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblcollection.BackColor = System.Drawing.Color.Transparent
        Me.lblcollection.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcollection.Location = New System.Drawing.Point(357, 538)
        Me.lblcollection.Name = "lblcollection"
        Me.lblcollection.Size = New System.Drawing.Size(108, 16)
        Me.lblcollection.TabIndex = 345497
        Me.lblcollection.Tag = ""
        Me.lblcollection.Text = "0.00"
        Me.lblcollection.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbltotalbalance
        '
        Me.lbltotalbalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbltotalbalance.BackColor = System.Drawing.Color.Transparent
        Me.lbltotalbalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalbalance.Location = New System.Drawing.Point(118, 560)
        Me.lbltotalbalance.Name = "lbltotalbalance"
        Me.lbltotalbalance.Size = New System.Drawing.Size(124, 16)
        Me.lbltotalbalance.TabIndex = 345495
        Me.lbltotalbalance.Tag = "Balance Amt.    :"
        Me.lbltotalbalance.Text = "0.00"
        Me.lbltotalbalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblreceived
        '
        Me.lblreceived.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblreceived.BackColor = System.Drawing.Color.Transparent
        Me.lblreceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblreceived.Location = New System.Drawing.Point(118, 538)
        Me.lblreceived.Name = "lblreceived"
        Me.lblreceived.Size = New System.Drawing.Size(124, 16)
        Me.lblreceived.TabIndex = 345494
        Me.lblreceived.Tag = "Received Amt. :"
        Me.lblreceived.Text = "0.00"
        Me.lblreceived.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbltotalInvoice
        '
        Me.lbltotalInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbltotalInvoice.BackColor = System.Drawing.Color.Transparent
        Me.lbltotalInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalInvoice.Location = New System.Drawing.Point(118, 517)
        Me.lbltotalInvoice.Name = "lbltotalInvoice"
        Me.lbltotalInvoice.Size = New System.Drawing.Size(123, 16)
        Me.lbltotalInvoice.TabIndex = 345493
        Me.lbltotalInvoice.Tag = "Invoice Amt.      :"
        Me.lbltotalInvoice.Text = "0.00"
        Me.lbltotalInvoice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rdocollection
        '
        Me.rdocollection.AutoSize = True
        Me.rdocollection.BackColor = System.Drawing.Color.Transparent
        Me.rdocollection.Checked = True
        Me.rdocollection.Location = New System.Drawing.Point(212, 51)
        Me.rdocollection.Name = "rdocollection"
        Me.rdocollection.Size = New System.Drawing.Size(71, 17)
        Me.rdocollection.TabIndex = 345507
        Me.rdocollection.TabStop = True
        Me.rdocollection.Text = "Collection"
        Me.rdocollection.UseVisualStyleBackColor = False
        '
        'rdoall
        '
        Me.rdoall.AutoSize = True
        Me.rdoall.BackColor = System.Drawing.Color.Transparent
        Me.rdoall.Location = New System.Drawing.Point(284, 51)
        Me.rdoall.Name = "rdoall"
        Me.rdoall.Size = New System.Drawing.Size(83, 17)
        Me.rdoall.TabIndex = 345508
        Me.rdoall.Text = "All Customer"
        Me.rdoall.UseVisualStyleBackColor = False
        '
        'cmbtype
        '
        Me.cmbtype.BackColor = System.Drawing.SystemColors.Window
        Me.cmbtype.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtype.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbtype.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbtype.Items.AddRange(New Object() {"Active", "Hidden"})
        Me.cmbtype.Location = New System.Drawing.Point(1005, 50)
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbtype.Size = New System.Drawing.Size(102, 22)
        Me.cmbtype.TabIndex = 345509
        Me.cmbtype.TabStop = False
        Me.cmbtype.Visible = False
        '
        'chkrvnot
        '
        Me.chkrvnot.AutoSize = True
        Me.chkrvnot.BackColor = System.Drawing.Color.Transparent
        Me.chkrvnot.Checked = True
        Me.chkrvnot.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkrvnot.ForeColor = System.Drawing.Color.Black
        Me.chkrvnot.Location = New System.Drawing.Point(42, 73)
        Me.chkrvnot.Name = "chkrvnot"
        Me.chkrvnot.Size = New System.Drawing.Size(107, 17)
        Me.chkrvnot.TabIndex = 345510
        Me.chkrvnot.Text = "Only RV Pending"
        Me.chkrvnot.UseVisualStyleBackColor = False
        '
        'rdoinvoice
        '
        Me.rdoinvoice.AutoSize = True
        Me.rdoinvoice.BackColor = System.Drawing.Color.Transparent
        Me.rdoinvoice.Location = New System.Drawing.Point(373, 51)
        Me.rdoinvoice.Name = "rdoinvoice"
        Me.rdoinvoice.Size = New System.Drawing.Size(87, 17)
        Me.rdoinvoice.TabIndex = 345511
        Me.rdoinvoice.Text = "Invoice Wise"
        Me.rdoinvoice.UseVisualStyleBackColor = False
        '
        'chkinstallment
        '
        Me.chkinstallment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkinstallment.AutoSize = True
        Me.chkinstallment.BackColor = System.Drawing.Color.Transparent
        Me.chkinstallment.ForeColor = System.Drawing.Color.Black
        Me.chkinstallment.Location = New System.Drawing.Point(996, 485)
        Me.chkinstallment.Name = "chkinstallment"
        Me.chkinstallment.Size = New System.Drawing.Size(111, 17)
        Me.chkinstallment.TabIndex = 345512
        Me.chkinstallment.Text = "Installment History"
        Me.chkinstallment.UseVisualStyleBackColor = False
        Me.chkinstallment.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.cmbsalesman)
        Me.Panel2.Controls.Add(Me.lblusr)
        Me.Panel2.Controls.Add(Me.cmbuser)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.cmbstatus)
        Me.Panel2.Location = New System.Drawing.Point(503, 38)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(392, 51)
        Me.Panel2.TabIndex = 345513
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(263, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 13)
        Me.Label7.TabIndex = 345515
        Me.Label7.Text = "Salesman"
        '
        'cmbsalesman
        '
        Me.cmbsalesman.BackColor = System.Drawing.SystemColors.Window
        Me.cmbsalesman.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbsalesman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsalesman.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbsalesman.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbsalesman.Location = New System.Drawing.Point(263, 26)
        Me.cmbsalesman.Name = "cmbsalesman"
        Me.cmbsalesman.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbsalesman.Size = New System.Drawing.Size(123, 22)
        Me.cmbsalesman.TabIndex = 345514
        Me.cmbsalesman.TabStop = False
        '
        'lblusr
        '
        Me.lblusr.AutoSize = True
        Me.lblusr.Location = New System.Drawing.Point(133, 9)
        Me.lblusr.Name = "lblusr"
        Me.lblusr.Size = New System.Drawing.Size(29, 13)
        Me.lblusr.TabIndex = 345513
        Me.lblusr.Text = "User"
        '
        'cmbuser
        '
        Me.cmbuser.BackColor = System.Drawing.SystemColors.Window
        Me.cmbuser.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbuser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbuser.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbuser.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbuser.Location = New System.Drawing.Point(133, 26)
        Me.cmbuser.Name = "cmbuser"
        Me.cmbuser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbuser.Size = New System.Drawing.Size(123, 22)
        Me.cmbuser.TabIndex = 345512
        Me.cmbuser.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 13)
        Me.Label5.TabIndex = 345511
        Me.Label5.Text = "Type"
        '
        'cmbstatus
        '
        Me.cmbstatus.BackColor = System.Drawing.SystemColors.Window
        Me.cmbstatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbstatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbstatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbstatus.Items.AddRange(New Object() {"All", "Paid", "Not Paid", "Pending"})
        Me.cmbstatus.Location = New System.Drawing.Point(3, 26)
        Me.cmbstatus.Name = "cmbstatus"
        Me.cmbstatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbstatus.Size = New System.Drawing.Size(123, 22)
        Me.cmbstatus.TabIndex = 345510
        Me.cmbstatus.TabStop = False
        '
        'chkcollectionwise
        '
        Me.chkcollectionwise.AutoSize = True
        Me.chkcollectionwise.BackColor = System.Drawing.Color.Transparent
        Me.chkcollectionwise.Checked = True
        Me.chkcollectionwise.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkcollectionwise.ForeColor = System.Drawing.Color.Black
        Me.chkcollectionwise.Location = New System.Drawing.Point(284, 72)
        Me.chkcollectionwise.Name = "chkcollectionwise"
        Me.chkcollectionwise.Size = New System.Drawing.Size(93, 17)
        Me.chkcollectionwise.TabIndex = 345514
        Me.chkcollectionwise.Text = "Collectionwise"
        Me.chkcollectionwise.UseVisualStyleBackColor = False
        '
        'CollectionListFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1119, 585)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkcollectionwise)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.chkinstallment)
        Me.Controls.Add(Me.rdoinvoice)
        Me.Controls.Add(Me.chkrvnot)
        Me.Controls.Add(Me.cmbtype)
        Me.Controls.Add(Me.rdoall)
        Me.Controls.Add(Me.rdocollection)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lbltodaycollection)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblcollection)
        Me.Controls.Add(Me.lbltotalbalance)
        Me.Controls.Add(Me.lblreceived)
        Me.Controls.Add(Me.lbltotalInvoice)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.chkFormat)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.btnrv)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.txtSeq)
        Me.Controls.Add(Me.cmbOrder)
        Me.Controls.Add(Me.btnload)
        Me.Controls.Add(Me.dvData)
        Me.Controls.Add(Me.dtpdate)
        Me.Controls.Add(Me.Label1)
        Me.Name = "CollectionListFrm"
        Me.Text = "CollectionListFrm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnrv As System.Windows.Forms.Button
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents dvData As System.Windows.Forms.DataGridView
    Friend WithEvents dtpdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkFormat As System.Windows.Forms.CheckBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents lblcap As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lbltodaycollection As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblcollection As System.Windows.Forms.Label
    Friend WithEvents lbltotalbalance As System.Windows.Forms.Label
    Friend WithEvents lblreceived As System.Windows.Forms.Label
    Friend WithEvents lbltotalInvoice As System.Windows.Forms.Label
    Friend WithEvents rdocollection As System.Windows.Forms.RadioButton
    Friend WithEvents rdoall As System.Windows.Forms.RadioButton
    Public WithEvents cmbtype As System.Windows.Forms.ComboBox
    Friend WithEvents chkrvnot As System.Windows.Forms.CheckBox
    Friend WithEvents rdoinvoice As System.Windows.Forms.RadioButton
    Friend WithEvents chkinstallment As System.Windows.Forms.CheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Public WithEvents cmbstatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents cmbsalesman As System.Windows.Forms.ComboBox
    Friend WithEvents lblusr As System.Windows.Forms.Label
    Public WithEvents cmbuser As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkcollectionwise As System.Windows.Forms.CheckBox
End Class
