<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RVCollectionListFrm
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
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.lblcap = New System.Windows.Forms.Label
        Me.dtpdate = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.dvData = New System.Windows.Forms.DataGridView
        Me.btnload = New System.Windows.Forms.Button
        Me.btnexit = New System.Windows.Forms.Button
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lbltotalInvoice = New System.Windows.Forms.Label
        Me.lblreceived = New System.Windows.Forms.Label
        Me.lbltotalbalance = New System.Windows.Forms.Label
        Me.lblbalancecollection = New System.Windows.Forms.Label
        Me.lblcollection = New System.Windows.Forms.Label
        Me.lblinstallment = New System.Windows.Forms.Label
        Me.chkpending = New System.Windows.Forms.CheckBox
        Me.btncomparison = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lbltodaycollection = New System.Windows.Forms.Label
        Me.btnhide = New System.Windows.Forms.Button
        Me.btnrv = New System.Windows.Forms.Button
        Me.cmbtype = New System.Windows.Forms.ComboBox
        Me.btnPreview = New System.Windows.Forms.Button
        Me.chkFormat = New System.Windows.Forms.CheckBox
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.PictureBox3)
        Me.Panel1.Controls.Add(Me.lblcap)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1025, 33)
        Me.Panel1.TabIndex = 345453
        '
        'PictureBox3
        '
        Me.PictureBox3.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox3.Location = New System.Drawing.Point(1, 5)
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
        Me.lblcap.Size = New System.Drawing.Size(222, 18)
        Me.lblcap.TabIndex = 6
        Me.lblcap.Text = "Receipt Voucher List [Customer]"
        '
        'dtpdate
        '
        Me.dtpdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpdate.Location = New System.Drawing.Point(44, 43)
        Me.dtpdate.Name = "dtpdate"
        Me.dtpdate.Size = New System.Drawing.Size(95, 20)
        Me.dtpdate.TabIndex = 345455
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 345454
        Me.Label1.Text = "Date"
        '
        'dvData
        '
        Me.dvData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dvData.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dvData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dvData.Location = New System.Drawing.Point(14, 74)
        Me.dvData.Name = "dvData"
        Me.dvData.Size = New System.Drawing.Size(999, 400)
        Me.dvData.TabIndex = 345456
        '
        'btnload
        '
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.FlatAppearance.BorderSize = 0
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.Location = New System.Drawing.Point(145, 38)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(63, 30)
        Me.btnload.TabIndex = 345458
        Me.btnload.Text = "Load"
        Me.btnload.UseVisualStyleBackColor = False
        '
        'btnexit
        '
        Me.btnexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.FlatAppearance.BorderSize = 0
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.Location = New System.Drawing.Point(920, 547)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(93, 33)
        Me.btnexit.TabIndex = 345457
        Me.btnexit.Text = "E&xit"
        Me.btnexit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'chkSearch
        '
        Me.chkSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.Checked = True
        Me.chkSearch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(425, 480)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 345461
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
        Me.txtSeq.Location = New System.Drawing.Point(184, 481)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(235, 20)
        Me.txtSeq.TabIndex = 345460
        '
        'cmbOrder
        '
        Me.cmbOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOrder.Location = New System.Drawing.Point(14, 480)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOrder.Size = New System.Drawing.Size(166, 22)
        Me.cmbOrder.TabIndex = 345459
        Me.cmbOrder.TabStop = False
        '
        'Timer1
        '
        '
        'lbltotalInvoice
        '
        Me.lbltotalInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbltotalInvoice.BackColor = System.Drawing.Color.Transparent
        Me.lbltotalInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalInvoice.Location = New System.Drawing.Point(123, 512)
        Me.lbltotalInvoice.Name = "lbltotalInvoice"
        Me.lbltotalInvoice.Size = New System.Drawing.Size(123, 16)
        Me.lbltotalInvoice.TabIndex = 345462
        Me.lbltotalInvoice.Tag = "Invoice Amt.      :"
        Me.lbltotalInvoice.Text = "0.00"
        Me.lbltotalInvoice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblreceived
        '
        Me.lblreceived.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblreceived.BackColor = System.Drawing.Color.Transparent
        Me.lblreceived.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblreceived.Location = New System.Drawing.Point(123, 533)
        Me.lblreceived.Name = "lblreceived"
        Me.lblreceived.Size = New System.Drawing.Size(124, 16)
        Me.lblreceived.TabIndex = 345463
        Me.lblreceived.Tag = "Received Amt. :"
        Me.lblreceived.Text = "0.00"
        Me.lblreceived.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbltotalbalance
        '
        Me.lbltotalbalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbltotalbalance.BackColor = System.Drawing.Color.Transparent
        Me.lbltotalbalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalbalance.Location = New System.Drawing.Point(123, 555)
        Me.lbltotalbalance.Name = "lbltotalbalance"
        Me.lbltotalbalance.Size = New System.Drawing.Size(124, 16)
        Me.lbltotalbalance.TabIndex = 345464
        Me.lbltotalbalance.Tag = "Balance Amt.    :"
        Me.lbltotalbalance.Text = "0.00"
        Me.lbltotalbalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblbalancecollection
        '
        Me.lblbalancecollection.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblbalancecollection.BackColor = System.Drawing.Color.Transparent
        Me.lblbalancecollection.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbalancecollection.Location = New System.Drawing.Point(362, 555)
        Me.lblbalancecollection.Name = "lblbalancecollection"
        Me.lblbalancecollection.Size = New System.Drawing.Size(108, 16)
        Me.lblbalancecollection.TabIndex = 345467
        Me.lblbalancecollection.Tag = "Balance Amt.      :"
        Me.lblbalancecollection.Text = "0.00"
        Me.lblbalancecollection.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblcollection
        '
        Me.lblcollection.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblcollection.BackColor = System.Drawing.Color.Transparent
        Me.lblcollection.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcollection.Location = New System.Drawing.Point(362, 533)
        Me.lblcollection.Name = "lblcollection"
        Me.lblcollection.Size = New System.Drawing.Size(108, 16)
        Me.lblcollection.TabIndex = 345466
        Me.lblcollection.Tag = ""
        Me.lblcollection.Text = "0.00"
        Me.lblcollection.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblinstallment
        '
        Me.lblinstallment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblinstallment.BackColor = System.Drawing.Color.Transparent
        Me.lblinstallment.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblinstallment.Location = New System.Drawing.Point(362, 512)
        Me.lblinstallment.Name = "lblinstallment"
        Me.lblinstallment.Size = New System.Drawing.Size(109, 16)
        Me.lblinstallment.TabIndex = 345465
        Me.lblinstallment.Tag = "Installment Amt.  :"
        Me.lblinstallment.Text = "0.00"
        Me.lblinstallment.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkpending
        '
        Me.chkpending.AutoSize = True
        Me.chkpending.BackColor = System.Drawing.Color.Transparent
        Me.chkpending.ForeColor = System.Drawing.Color.Black
        Me.chkpending.Location = New System.Drawing.Point(214, 46)
        Me.chkpending.Name = "chkpending"
        Me.chkpending.Size = New System.Drawing.Size(89, 17)
        Me.chkpending.TabIndex = 345468
        Me.chkpending.Text = "Pending Only"
        Me.chkpending.UseVisualStyleBackColor = False
        '
        'btncomparison
        '
        Me.btncomparison.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btncomparison.BackColor = System.Drawing.Color.SteelBlue
        Me.btncomparison.FlatAppearance.BorderSize = 0
        Me.btncomparison.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncomparison.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncomparison.ForeColor = System.Drawing.Color.White
        Me.btncomparison.Location = New System.Drawing.Point(920, 480)
        Me.btncomparison.Name = "btncomparison"
        Me.btncomparison.Size = New System.Drawing.Size(93, 33)
        Me.btncomparison.TabIndex = 345469
        Me.btncomparison.Text = "Comparison"
        Me.btncomparison.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncomparison.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 555)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 16)
        Me.Label2.TabIndex = 345472
        Me.Label2.Tag = "Balance Amt.    :"
        Me.Label2.Text = "Balance Amt.    :"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 533)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 16)
        Me.Label3.TabIndex = 345471
        Me.Label3.Tag = "Received Amt. :"
        Me.Label3.Text = "Received Amt. :"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 512)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 16)
        Me.Label4.TabIndex = 345470
        Me.Label4.Tag = "Invoice Amt.      :"
        Me.Label4.Text = "Invoice Amt.      :"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(252, 555)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(107, 16)
        Me.Label5.TabIndex = 345475
        Me.Label5.Tag = ""
        Me.Label5.Text = "Today Balance :"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(252, 533)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 16)
        Me.Label6.TabIndex = 345474
        Me.Label6.Tag = ""
        Me.Label6.Text = "Today RV.   :"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(252, 512)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(109, 16)
        Me.Label7.TabIndex = 345473
        Me.Label7.Tag = "Installment Amt.  :"
        Me.Label7.Text = "Installment Amt.  :"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(480, 555)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(119, 16)
        Me.Label8.TabIndex = 345477
        Me.Label8.Tag = ""
        Me.Label8.Text = "Today Collection : "
        '
        'lbltodaycollection
        '
        Me.lbltodaycollection.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbltodaycollection.BackColor = System.Drawing.Color.Transparent
        Me.lbltodaycollection.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltodaycollection.Location = New System.Drawing.Point(590, 555)
        Me.lbltodaycollection.Name = "lbltodaycollection"
        Me.lbltodaycollection.Size = New System.Drawing.Size(109, 16)
        Me.lbltodaycollection.TabIndex = 345476
        Me.lbltodaycollection.Tag = "Installment Amt.  :"
        Me.lbltodaycollection.Text = "0.00"
        Me.lbltodaycollection.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnhide
        '
        Me.btnhide.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnhide.BackColor = System.Drawing.Color.SteelBlue
        Me.btnhide.FlatAppearance.BorderSize = 0
        Me.btnhide.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnhide.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnhide.ForeColor = System.Drawing.Color.White
        Me.btnhide.Location = New System.Drawing.Point(817, 480)
        Me.btnhide.Name = "btnhide"
        Me.btnhide.Size = New System.Drawing.Size(99, 33)
        Me.btnhide.TabIndex = 345478
        Me.btnhide.Text = "Hide"
        Me.btnhide.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnhide.UseVisualStyleBackColor = False
        '
        'btnrv
        '
        Me.btnrv.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnrv.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrv.FlatAppearance.BorderSize = 0
        Me.btnrv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrv.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrv.ForeColor = System.Drawing.Color.White
        Me.btnrv.Location = New System.Drawing.Point(717, 480)
        Me.btnrv.Name = "btnrv"
        Me.btnrv.Size = New System.Drawing.Size(97, 33)
        Me.btnrv.TabIndex = 345479
        Me.btnrv.Text = "Creative RV"
        Me.btnrv.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrv.UseVisualStyleBackColor = False
        '
        'cmbtype
        '
        Me.cmbtype.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbtype.BackColor = System.Drawing.SystemColors.Window
        Me.cmbtype.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtype.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbtype.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbtype.Items.AddRange(New Object() {"Active", "Hidden"})
        Me.cmbtype.Location = New System.Drawing.Point(817, 517)
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbtype.Size = New System.Drawing.Size(102, 22)
        Me.cmbtype.TabIndex = 345480
        Me.cmbtype.TabStop = False
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
        Me.btnPreview.Location = New System.Drawing.Point(824, 547)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(93, 33)
        Me.btnPreview.TabIndex = 345481
        Me.btnPreview.TabStop = False
        Me.btnPreview.Text = "Pre&view"
        Me.btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'chkFormat
        '
        Me.chkFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFormat.AutoSize = True
        Me.chkFormat.BackColor = System.Drawing.Color.Transparent
        Me.chkFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFormat.Location = New System.Drawing.Point(753, 561)
        Me.chkFormat.Name = "chkFormat"
        Me.chkFormat.Size = New System.Drawing.Size(65, 19)
        Me.chkFormat.TabIndex = 345482
        Me.chkFormat.Text = "Format"
        Me.chkFormat.UseVisualStyleBackColor = False
        '
        'RVCollectionListFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1025, 592)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkFormat)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.cmbtype)
        Me.Controls.Add(Me.btnrv)
        Me.Controls.Add(Me.btnhide)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lbltodaycollection)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btncomparison)
        Me.Controls.Add(Me.chkpending)
        Me.Controls.Add(Me.lblbalancecollection)
        Me.Controls.Add(Me.lblcollection)
        Me.Controls.Add(Me.lblinstallment)
        Me.Controls.Add(Me.lbltotalbalance)
        Me.Controls.Add(Me.lblreceived)
        Me.Controls.Add(Me.lbltotalInvoice)
        Me.Controls.Add(Me.chkSearch)
        Me.Controls.Add(Me.txtSeq)
        Me.Controls.Add(Me.cmbOrder)
        Me.Controls.Add(Me.btnload)
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.dvData)
        Me.Controls.Add(Me.dtpdate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "RVCollectionListFrm"
        Me.Text = "RVCollectionListFrm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents lblcap As System.Windows.Forms.Label
    Friend WithEvents dtpdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dvData As System.Windows.Forms.DataGridView
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lbltotalInvoice As System.Windows.Forms.Label
    Friend WithEvents lblreceived As System.Windows.Forms.Label
    Friend WithEvents lbltotalbalance As System.Windows.Forms.Label
    Friend WithEvents lblbalancecollection As System.Windows.Forms.Label
    Friend WithEvents lblcollection As System.Windows.Forms.Label
    Friend WithEvents lblinstallment As System.Windows.Forms.Label
    Friend WithEvents chkpending As System.Windows.Forms.CheckBox
    Friend WithEvents btncomparison As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lbltodaycollection As System.Windows.Forms.Label
    Friend WithEvents btnhide As System.Windows.Forms.Button
    Friend WithEvents btnrv As System.Windows.Forms.Button
    Public WithEvents cmbtype As System.Windows.Forms.ComboBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents chkFormat As System.Windows.Forms.CheckBox
End Class
