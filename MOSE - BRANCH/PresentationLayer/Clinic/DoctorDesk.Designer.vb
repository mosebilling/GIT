<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DoctorDeskFrm
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.grdlist = New System.Windows.Forms.DataGridView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btncancel = New System.Windows.Forms.Button
        Me.btnclosevisit = New System.Windows.Forms.Button
        Me.btnaddnote = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.btnexit = New System.Windows.Forms.Button
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lbldob = New System.Windows.Forms.Label
        Me.lblgender = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtremarks = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtRec1 = New System.Windows.Forms.TextBox
        Me.txtRec0 = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txttoken = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.numVchrNo = New System.Windows.Forms.TextBox
        Me.btnrefresh = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.txtSeq = New System.Windows.Forms.TextBox
        Me.cmbOrder = New System.Windows.Forms.ComboBox
        Me.btnload = New System.Windows.Forms.Button
        Me.dtpto = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.dtpfrom = New System.Windows.Forms.DateTimePicker
        Me.grdclosedlist = New System.Windows.Forms.DataGridView
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.grdfollowup = New System.Windows.Forms.DataGridView
        Me.btnloadcalls = New System.Windows.Forms.Button
        Me.dtpfollowupdate2 = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpfollowupdate1 = New System.Windows.Forms.DateTimePicker
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdclosedlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.grdfollowup, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(1065, 38)
        Me.Panel1.TabIndex = 345447
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(3, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 20)
        Me.PictureBox1.TabIndex = 345458
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(39, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 18)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Doctor Desk"
        '
        'grdlist
        '
        Me.grdlist.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdlist.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.grdlist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdlist.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdlist.Location = New System.Drawing.Point(6, 6)
        Me.grdlist.Name = "grdlist"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdlist.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdlist.RowTemplate.Height = 35
        Me.grdlist.Size = New System.Drawing.Size(445, 449)
        Me.grdlist.TabIndex = 345454
        Me.grdlist.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.btncancel)
        Me.Panel2.Controls.Add(Me.btnclosevisit)
        Me.Panel2.Controls.Add(Me.btnaddnote)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.grdVoucher)
        Me.Panel2.Controls.Add(Me.btnexit)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.txttoken)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.numVchrNo)
        Me.Panel2.Location = New System.Drawing.Point(475, 41)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(588, 534)
        Me.Panel2.TabIndex = 345455
        '
        'btncancel
        '
        Me.btncancel.BackColor = System.Drawing.Color.SteelBlue
        Me.btncancel.Enabled = False
        Me.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancel.ForeColor = System.Drawing.Color.White
        Me.btncancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btncancel.Location = New System.Drawing.Point(328, 10)
        Me.btncancel.Name = "btncancel"
        Me.btncancel.Size = New System.Drawing.Size(76, 32)
        Me.btncancel.TabIndex = 345469
        Me.btncancel.Text = "Clear"
        Me.btncancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btncancel.UseVisualStyleBackColor = False
        '
        'btnclosevisit
        '
        Me.btnclosevisit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnclosevisit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclosevisit.Enabled = False
        Me.btnclosevisit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclosevisit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclosevisit.ForeColor = System.Drawing.Color.White
        Me.btnclosevisit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnclosevisit.Location = New System.Drawing.Point(119, 490)
        Me.btnclosevisit.Name = "btnclosevisit"
        Me.btnclosevisit.Size = New System.Drawing.Size(100, 35)
        Me.btnclosevisit.TabIndex = 345468
        Me.btnclosevisit.Text = "Close Visit"
        Me.btnclosevisit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclosevisit.UseVisualStyleBackColor = False
        '
        'btnaddnote
        '
        Me.btnaddnote.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnaddnote.BackColor = System.Drawing.Color.SteelBlue
        Me.btnaddnote.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnaddnote.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnaddnote.ForeColor = System.Drawing.Color.White
        Me.btnaddnote.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnaddnote.Location = New System.Drawing.Point(13, 490)
        Me.btnaddnote.Name = "btnaddnote"
        Me.btnaddnote.Size = New System.Drawing.Size(100, 35)
        Me.btnaddnote.TabIndex = 345467
        Me.btnaddnote.Text = "Add Note"
        Me.btnaddnote.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnaddnote.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(10, 202)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(94, 15)
        Me.Label6.TabIndex = 345466
        Me.Label6.Text = "Previous History"
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
        Me.grdVoucher.Location = New System.Drawing.Point(13, 220)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(563, 264)
        Me.grdVoucher.TabIndex = 345465
        Me.grdVoucher.TabStop = False
        '
        'btnexit
        '
        Me.btnexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnexit.Location = New System.Drawing.Point(476, 490)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(100, 35)
        Me.btnexit.TabIndex = 4
        Me.btnexit.Text = "E&xit"
        Me.btnexit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.Controls.Add(Me.lbldob)
        Me.Panel3.Controls.Add(Me.lblgender)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.txtremarks)
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.txtRec1)
        Me.Panel3.Controls.Add(Me.txtRec0)
        Me.Panel3.Location = New System.Drawing.Point(13, 54)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(562, 145)
        Me.Panel3.TabIndex = 345464
        '
        'lbldob
        '
        Me.lbldob.AutoSize = True
        Me.lbldob.BackColor = System.Drawing.Color.Transparent
        Me.lbldob.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbldob.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldob.ForeColor = System.Drawing.Color.Maroon
        Me.lbldob.Location = New System.Drawing.Point(405, 11)
        Me.lbldob.Name = "lbldob"
        Me.lbldob.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbldob.Size = New System.Drawing.Size(74, 14)
        Me.lbldob.TabIndex = 345486
        Me.lbldob.Text = "Date of Birth"
        '
        'lblgender
        '
        Me.lblgender.AutoSize = True
        Me.lblgender.BackColor = System.Drawing.Color.Transparent
        Me.lblgender.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblgender.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgender.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblgender.Location = New System.Drawing.Point(296, 11)
        Me.lblgender.Name = "lblgender"
        Me.lblgender.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblgender.Size = New System.Drawing.Size(22, 14)
        Me.lblgender.TabIndex = 345485
        Me.lblgender.Text = "m/f"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(335, 11)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(67, 14)
        Me.Label12.TabIndex = 345484
        Me.Label12.Text = "Date of Birth"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(247, 11)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(43, 14)
        Me.Label11.TabIndex = 345483
        Me.Label11.Text = "Gender"
        '
        'txtremarks
        '
        Me.txtremarks.AcceptsReturn = True
        Me.txtremarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtremarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtremarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtremarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtremarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtremarks.Location = New System.Drawing.Point(84, 58)
        Me.txtremarks.MaxLength = 250
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtremarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtremarks.Size = New System.Drawing.Size(469, 78)
        Me.txtremarks.TabIndex = 2
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(9, 60)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(51, 14)
        Me.Label17.TabIndex = 345466
        Me.Label17.Text = "Comment"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(9, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(69, 14)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "Patient Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(9, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(61, 14)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "OP Number"
        '
        'txtRec1
        '
        Me.txtRec1.AcceptsReturn = True
        Me.txtRec1.BackColor = System.Drawing.SystemColors.Window
        Me.txtRec1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRec1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRec1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRec1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRec1.Location = New System.Drawing.Point(84, 32)
        Me.txtRec1.MaxLength = 100
        Me.txtRec1.Name = "txtRec1"
        Me.txtRec1.ReadOnly = True
        Me.txtRec1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRec1.Size = New System.Drawing.Size(469, 20)
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
        Me.txtRec0.Location = New System.Drawing.Point(84, 9)
        Me.txtRec0.MaxLength = 10
        Me.txtRec0.Name = "txtRec0"
        Me.txtRec0.ReadOnly = True
        Me.txtRec0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRec0.Size = New System.Drawing.Size(161, 20)
        Me.txtRec0.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Green
        Me.Label5.Location = New System.Drawing.Point(200, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 20)
        Me.Label5.TabIndex = 345463
        Me.Label5.Text = "Token"
        '
        'txttoken
        '
        Me.txttoken.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttoken.Location = New System.Drawing.Point(264, 12)
        Me.txttoken.Name = "txttoken"
        Me.txttoken.ReadOnly = True
        Me.txttoken.Size = New System.Drawing.Size(58, 29)
        Me.txttoken.TabIndex = 345462
        Me.txttoken.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(10, 12)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(53, 15)
        Me.Label14.TabIndex = 345460
        Me.Label14.Text = "App. No."
        '
        'numVchrNo
        '
        Me.numVchrNo.BackColor = System.Drawing.Color.White
        Me.numVchrNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numVchrNo.Location = New System.Drawing.Point(94, 12)
        Me.numVchrNo.Name = "numVchrNo"
        Me.numVchrNo.ReadOnly = True
        Me.numVchrNo.Size = New System.Drawing.Size(95, 21)
        Me.numVchrNo.TabIndex = 345458
        '
        'btnrefresh
        '
        Me.btnrefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnrefresh.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrefresh.ForeColor = System.Drawing.Color.White
        Me.btnrefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnrefresh.Location = New System.Drawing.Point(3, 531)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(100, 35)
        Me.btnrefresh.TabIndex = 345468
        Me.btnrefresh.Text = "Refresh"
        Me.btnrefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrefresh.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(4, 44)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(465, 487)
        Me.TabControl1.TabIndex = 345469
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.grdlist)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(457, 461)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "VISIT"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.txtSeq)
        Me.TabPage2.Controls.Add(Me.cmbOrder)
        Me.TabPage2.Controls.Add(Me.btnload)
        Me.TabPage2.Controls.Add(Me.dtpto)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.dtpfrom)
        Me.TabPage2.Controls.Add(Me.grdclosedlist)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(457, 461)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Closed Visit"
        Me.TabPage2.UseVisualStyleBackColor = True
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
        Me.txtSeq.Location = New System.Drawing.Point(151, 432)
        Me.txtSeq.MaxLength = 500
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(300, 20)
        Me.txtSeq.TabIndex = 345468
        '
        'cmbOrder
        '
        Me.cmbOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbOrder.BackColor = System.Drawing.SystemColors.Window
        Me.cmbOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrder.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOrder.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbOrder.Location = New System.Drawing.Point(7, 430)
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbOrder.Size = New System.Drawing.Size(138, 22)
        Me.cmbOrder.TabIndex = 345467
        Me.cmbOrder.TabStop = False
        '
        'btnload
        '
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.Location = New System.Drawing.Point(342, 8)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(80, 23)
        Me.btnload.TabIndex = 345466
        Me.btnload.Text = "Load"
        Me.btnload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnload.UseVisualStyleBackColor = False
        '
        'dtpto
        '
        Me.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpto.Location = New System.Drawing.Point(230, 9)
        Me.dtpto.Name = "dtpto"
        Me.dtpto.Size = New System.Drawing.Size(106, 20)
        Me.dtpto.TabIndex = 345465
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(18, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(94, 15)
        Me.Label7.TabIndex = 345464
        Me.Label7.Text = "Date Parameter"
        '
        'dtpfrom
        '
        Me.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfrom.Location = New System.Drawing.Point(118, 9)
        Me.dtpfrom.Name = "dtpfrom"
        Me.dtpfrom.Size = New System.Drawing.Size(106, 20)
        Me.dtpfrom.TabIndex = 345463
        '
        'grdclosedlist
        '
        Me.grdclosedlist.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdclosedlist.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.grdclosedlist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdclosedlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdclosedlist.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdclosedlist.Location = New System.Drawing.Point(6, 40)
        Me.grdclosedlist.Name = "grdclosedlist"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdclosedlist.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdclosedlist.RowTemplate.Height = 35
        Me.grdclosedlist.Size = New System.Drawing.Size(445, 386)
        Me.grdclosedlist.TabIndex = 345455
        Me.grdclosedlist.TabStop = False
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.btnloadcalls)
        Me.TabPage3.Controls.Add(Me.dtpfollowupdate2)
        Me.TabPage3.Controls.Add(Me.Label2)
        Me.TabPage3.Controls.Add(Me.dtpfollowupdate1)
        Me.TabPage3.Controls.Add(Me.grdfollowup)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(457, 461)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Follow Up"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'grdfollowup
        '
        Me.grdfollowup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdfollowup.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.grdfollowup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdfollowup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdfollowup.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdfollowup.Location = New System.Drawing.Point(3, 32)
        Me.grdfollowup.Name = "grdfollowup"
        Me.grdfollowup.Size = New System.Drawing.Size(448, 423)
        Me.grdfollowup.TabIndex = 345454
        Me.grdfollowup.TabStop = False
        '
        'btnloadcalls
        '
        Me.btnloadcalls.BackColor = System.Drawing.Color.SteelBlue
        Me.btnloadcalls.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnloadcalls.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnloadcalls.ForeColor = System.Drawing.Color.White
        Me.btnloadcalls.Location = New System.Drawing.Point(347, 6)
        Me.btnloadcalls.Name = "btnloadcalls"
        Me.btnloadcalls.Size = New System.Drawing.Size(80, 23)
        Me.btnloadcalls.TabIndex = 345474
        Me.btnloadcalls.Text = "Load"
        Me.btnloadcalls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnloadcalls.UseVisualStyleBackColor = False
        '
        'dtpfollowupdate2
        '
        Me.dtpfollowupdate2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfollowupdate2.Location = New System.Drawing.Point(233, 6)
        Me.dtpfollowupdate2.Name = "dtpfollowupdate2"
        Me.dtpfollowupdate2.Size = New System.Drawing.Size(106, 20)
        Me.dtpfollowupdate2.TabIndex = 345473
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 15)
        Me.Label2.TabIndex = 345472
        Me.Label2.Text = "Date Parameter"
        '
        'dtpfollowupdate1
        '
        Me.dtpfollowupdate1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfollowupdate1.Location = New System.Drawing.Point(121, 6)
        Me.dtpfollowupdate1.Name = "dtpfollowupdate1"
        Me.dtpfollowupdate1.Size = New System.Drawing.Size(106, 20)
        Me.dtpfollowupdate1.TabIndex = 345471
        '
        'DoctorDeskFrm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1065, 578)
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnrefresh)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "DoctorDeskFrm"
        Me.Text = "DoctorDesk"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.grdclosedlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.grdfollowup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grdlist As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents numVchrNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txttoken As System.Windows.Forms.TextBox
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Public WithEvents txtremarks As System.Windows.Forms.TextBox
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents txtRec1 As System.Windows.Forms.TextBox
    Public WithEvents txtRec0 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents grdVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents btnaddnote As System.Windows.Forms.Button
    Friend WithEvents btnrefresh As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnclosevisit As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents grdclosedlist As System.Windows.Forms.DataGridView
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents dtpto As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpfrom As System.Windows.Forms.DateTimePicker
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmbOrder As System.Windows.Forms.ComboBox
    Friend WithEvents btncancel As System.Windows.Forms.Button
    Public WithEvents lbldob As System.Windows.Forms.Label
    Public WithEvents lblgender As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents grdfollowup As System.Windows.Forms.DataGridView
    Friend WithEvents btnloadcalls As System.Windows.Forms.Button
    Friend WithEvents dtpfollowupdate2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpfollowupdate1 As System.Windows.Forms.DateTimePicker
End Class
