<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AvailableSerialNumberListFrm
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblcap = New System.Windows.Forms.Label
        Me.rdoAvailable = New System.Windows.Forms.RadioButton
        Me.rdosold = New System.Windows.Forms.RadioButton
        Me.btnload = New System.Windows.Forms.Button
        Me.btnexit = New System.Windows.Forms.Button
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.cmbcolms = New System.Windows.Forms.ComboBox
        Me.rdoboth = New System.Windows.Forms.RadioButton
        Me.rdosales = New System.Windows.Forms.RadioButton
        Me.rdopurchase = New System.Windows.Forms.RadioButton
        Me.rdosreturn = New System.Windows.Forms.RadioButton
        Me.rdoPreturn = New System.Windows.Forms.RadioButton
        Me.rdoIn = New System.Windows.Forms.RadioButton
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.rdoall = New System.Windows.Forms.RadioButton
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdVoucher
        '
        Me.grdVoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdVoucher.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdVoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdVoucher.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdVoucher.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdVoucher.Location = New System.Drawing.Point(5, 36)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(864, 216)
        Me.grdVoucher.TabIndex = 138
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.lblcap)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(873, 33)
        Me.Panel1.TabIndex = 345453
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(37, 23)
        Me.PictureBox1.TabIndex = 345460
        Me.PictureBox1.TabStop = False
        '
        'lblcap
        '
        Me.lblcap.AutoSize = True
        Me.lblcap.BackColor = System.Drawing.Color.Transparent
        Me.lblcap.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcap.ForeColor = System.Drawing.Color.Black
        Me.lblcap.Location = New System.Drawing.Point(41, 6)
        Me.lblcap.Name = "lblcap"
        Me.lblcap.Size = New System.Drawing.Size(129, 18)
        Me.lblcap.TabIndex = 6
        Me.lblcap.Text = "Serial Number List"
        '
        'rdoAvailable
        '
        Me.rdoAvailable.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdoAvailable.AutoSize = True
        Me.rdoAvailable.Checked = True
        Me.rdoAvailable.Location = New System.Drawing.Point(6, 285)
        Me.rdoAvailable.Name = "rdoAvailable"
        Me.rdoAvailable.Size = New System.Drawing.Size(152, 17)
        Me.rdoAvailable.TabIndex = 345454
        Me.rdoAvailable.TabStop = True
        Me.rdoAvailable.Text = "Available IMEI Number List"
        Me.rdoAvailable.UseVisualStyleBackColor = True
        '
        'rdosold
        '
        Me.rdosold.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdosold.AutoSize = True
        Me.rdosold.Location = New System.Drawing.Point(6, 306)
        Me.rdosold.Name = "rdosold"
        Me.rdosold.Size = New System.Drawing.Size(130, 17)
        Me.rdosold.TabIndex = 345455
        Me.rdosold.Text = "Sold IMEI Number List"
        Me.rdosold.UseVisualStyleBackColor = True
        '
        'btnload
        '
        Me.btnload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnload.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnload.ForeColor = System.Drawing.Color.White
        Me.btnload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnload.Location = New System.Drawing.Point(710, 365)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(79, 30)
        Me.btnload.TabIndex = 345474
        Me.btnload.Text = "Show Tr."
        Me.btnload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnload.UseVisualStyleBackColor = False
        '
        'btnexit
        '
        Me.btnexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnexit.Location = New System.Drawing.Point(790, 365)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(79, 30)
        Me.btnexit.TabIndex = 345475
        Me.btnexit.Text = "E&xit"
        Me.btnexit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.AcceptsReturn = True
        Me.txtSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.BackColor = System.Drawing.SystemColors.Window
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSearch.Location = New System.Drawing.Point(137, 259)
        Me.txtSearch.MaxLength = 50
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSearch.Size = New System.Drawing.Size(262, 20)
        Me.txtSearch.TabIndex = 345477
        '
        'cmbcolms
        '
        Me.cmbcolms.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbcolms.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcolms.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbcolms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcolms.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcolms.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcolms.Location = New System.Drawing.Point(6, 257)
        Me.cmbcolms.Name = "cmbcolms"
        Me.cmbcolms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbcolms.Size = New System.Drawing.Size(127, 22)
        Me.cmbcolms.TabIndex = 345476
        Me.cmbcolms.TabStop = False
        '
        'rdoboth
        '
        Me.rdoboth.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdoboth.AutoSize = True
        Me.rdoboth.Location = New System.Drawing.Point(6, 327)
        Me.rdoboth.Name = "rdoboth"
        Me.rdoboth.Size = New System.Drawing.Size(47, 17)
        Me.rdoboth.TabIndex = 345478
        Me.rdoboth.Text = "Both"
        Me.rdoboth.UseVisualStyleBackColor = True
        '
        'rdosales
        '
        Me.rdosales.AutoSize = True
        Me.rdosales.Checked = True
        Me.rdosales.Location = New System.Drawing.Point(3, 4)
        Me.rdosales.Name = "rdosales"
        Me.rdosales.Size = New System.Drawing.Size(51, 17)
        Me.rdosales.TabIndex = 345455
        Me.rdosales.TabStop = True
        Me.rdosales.Text = "Sales"
        Me.rdosales.UseVisualStyleBackColor = True
        '
        'rdopurchase
        '
        Me.rdopurchase.AutoSize = True
        Me.rdopurchase.Location = New System.Drawing.Point(95, 4)
        Me.rdopurchase.Name = "rdopurchase"
        Me.rdopurchase.Size = New System.Drawing.Size(70, 17)
        Me.rdopurchase.TabIndex = 345456
        Me.rdopurchase.Text = "Purchase"
        Me.rdopurchase.UseVisualStyleBackColor = True
        '
        'rdosreturn
        '
        Me.rdosreturn.AutoSize = True
        Me.rdosreturn.Location = New System.Drawing.Point(3, 24)
        Me.rdosreturn.Name = "rdosreturn"
        Me.rdosreturn.Size = New System.Drawing.Size(86, 17)
        Me.rdosreturn.TabIndex = 345457
        Me.rdosreturn.Text = "Sales Return"
        Me.rdosreturn.UseVisualStyleBackColor = True
        '
        'rdoPreturn
        '
        Me.rdoPreturn.AutoSize = True
        Me.rdoPreturn.Location = New System.Drawing.Point(95, 24)
        Me.rdoPreturn.Name = "rdoPreturn"
        Me.rdoPreturn.Size = New System.Drawing.Size(105, 17)
        Me.rdoPreturn.TabIndex = 345458
        Me.rdoPreturn.Text = "Purchase Return"
        Me.rdoPreturn.UseVisualStyleBackColor = True
        '
        'rdoIn
        '
        Me.rdoIn.AutoSize = True
        Me.rdoIn.Location = New System.Drawing.Point(206, 2)
        Me.rdoIn.Name = "rdoIn"
        Me.rdoIn.Size = New System.Drawing.Size(81, 17)
        Me.rdoIn.TabIndex = 345459
        Me.rdoIn.Text = "Stock In Tr."
        Me.rdoIn.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(206, 22)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(89, 17)
        Me.RadioButton1.TabIndex = 345460
        Me.RadioButton1.Text = "Stock Out Tr."
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.rdoall)
        Me.Panel2.Controls.Add(Me.RadioButton1)
        Me.Panel2.Controls.Add(Me.rdosales)
        Me.Panel2.Controls.Add(Me.rdoIn)
        Me.Panel2.Controls.Add(Me.rdopurchase)
        Me.Panel2.Controls.Add(Me.rdoPreturn)
        Me.Panel2.Controls.Add(Me.rdosreturn)
        Me.Panel2.Location = New System.Drawing.Point(571, 299)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(298, 66)
        Me.Panel2.TabIndex = 345481
        Me.Panel2.Visible = False
        '
        'rdoall
        '
        Me.rdoall.AutoSize = True
        Me.rdoall.Location = New System.Drawing.Point(3, 45)
        Me.rdoall.Name = "rdoall"
        Me.rdoall.Size = New System.Drawing.Size(36, 17)
        Me.rdoall.TabIndex = 345461
        Me.rdoall.Text = "All"
        Me.rdoall.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'AvailableSerialNumberListFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(873, 400)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.rdoboth)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.cmbcolms)
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.btnload)
        Me.Controls.Add(Me.rdosold)
        Me.Controls.Add(Me.rdoAvailable)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grdVoucher)
        Me.Name = "AvailableSerialNumberListFrm"
        Me.Text = "AvailableSerialNumberListFrm"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblcap As System.Windows.Forms.Label
    Friend WithEvents rdoAvailable As System.Windows.Forms.RadioButton
    Friend WithEvents rdosold As System.Windows.Forms.RadioButton
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Public WithEvents txtSearch As System.Windows.Forms.TextBox
    Public WithEvents cmbcolms As System.Windows.Forms.ComboBox
    Friend WithEvents rdoboth As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIn As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPreturn As System.Windows.Forms.RadioButton
    Friend WithEvents rdosreturn As System.Windows.Forms.RadioButton
    Friend WithEvents rdopurchase As System.Windows.Forms.RadioButton
    Friend WithEvents rdosales As System.Windows.Forms.RadioButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rdoall As System.Windows.Forms.RadioButton
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
