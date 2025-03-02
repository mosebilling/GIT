<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ShowAlert
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.btnothcancel = New System.Windows.Forms.Button
        Me.btnpreview = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtpto = New System.Windows.Forms.DateTimePicker
        Me.dtpfrom = New System.Windows.Forms.DateTimePicker
        Me.plmain = New System.Windows.Forms.Panel
        Me.pldate = New System.Windows.Forms.Panel
        Me.btnload = New System.Windows.Forms.Button
        Me.chkdate = New System.Windows.Forms.CheckBox
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.cmbcolms = New System.Windows.Forms.ComboBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblcap = New System.Windows.Forms.Label
        Me.chkdontshow = New System.Windows.Forms.CheckBox
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plmain.SuspendLayout()
        Me.pldate.SuspendLayout()
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
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdVoucher.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdVoucher.Location = New System.Drawing.Point(7, 34)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(840, 292)
        Me.grdVoucher.TabIndex = 137
        '
        'btnothcancel
        '
        Me.btnothcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnothcancel.BackColor = System.Drawing.Color.SteelBlue
        Me.btnothcancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnothcancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnothcancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnothcancel.ForeColor = System.Drawing.Color.White
        Me.btnothcancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnothcancel.Location = New System.Drawing.Point(754, 332)
        Me.btnothcancel.Name = "btnothcancel"
        Me.btnothcancel.Size = New System.Drawing.Size(93, 35)
        Me.btnothcancel.TabIndex = 345450
        Me.btnothcancel.Text = "Cancel"
        Me.btnothcancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnothcancel.UseVisualStyleBackColor = False
        '
        'btnpreview
        '
        Me.btnpreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnpreview.BackColor = System.Drawing.Color.SteelBlue
        Me.btnpreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnpreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnpreview.ForeColor = System.Drawing.Color.White
        Me.btnpreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnpreview.Location = New System.Drawing.Point(1, 3)
        Me.btnpreview.Name = "btnpreview"
        Me.btnpreview.Size = New System.Drawing.Size(93, 35)
        Me.btnpreview.TabIndex = 345451
        Me.btnpreview.Text = "Preview"
        Me.btnpreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnpreview.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 345428
        Me.Label3.Text = "Date Parameter"
        '
        'dtpto
        '
        Me.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpto.Location = New System.Drawing.Point(108, 14)
        Me.dtpto.Name = "dtpto"
        Me.dtpto.Size = New System.Drawing.Size(95, 20)
        Me.dtpto.TabIndex = 345396
        Me.dtpto.TabStop = False
        '
        'dtpfrom
        '
        Me.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfrom.Location = New System.Drawing.Point(8, 14)
        Me.dtpfrom.Name = "dtpfrom"
        Me.dtpfrom.Size = New System.Drawing.Size(95, 20)
        Me.dtpfrom.TabIndex = 345395
        Me.dtpfrom.TabStop = False
        '
        'plmain
        '
        Me.plmain.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.plmain.BackColor = System.Drawing.Color.Transparent
        Me.plmain.Controls.Add(Me.pldate)
        Me.plmain.Controls.Add(Me.chkdate)
        Me.plmain.Controls.Add(Me.txtSearch)
        Me.plmain.Controls.Add(Me.cmbcolms)
        Me.plmain.Controls.Add(Me.btnpreview)
        Me.plmain.Location = New System.Drawing.Point(7, 328)
        Me.plmain.Name = "plmain"
        Me.plmain.Size = New System.Drawing.Size(835, 42)
        Me.plmain.TabIndex = 345453
        Me.plmain.Visible = False
        '
        'pldate
        '
        Me.pldate.BackColor = System.Drawing.Color.Transparent
        Me.pldate.Controls.Add(Me.btnload)
        Me.pldate.Controls.Add(Me.dtpto)
        Me.pldate.Controls.Add(Me.dtpfrom)
        Me.pldate.Controls.Add(Me.Label3)
        Me.pldate.Enabled = False
        Me.pldate.Location = New System.Drawing.Point(521, 2)
        Me.pldate.Name = "pldate"
        Me.pldate.Size = New System.Drawing.Size(302, 36)
        Me.pldate.TabIndex = 345455
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
        Me.btnload.Location = New System.Drawing.Point(209, 3)
        Me.btnload.Name = "btnload"
        Me.btnload.Size = New System.Drawing.Size(79, 30)
        Me.btnload.TabIndex = 345473
        Me.btnload.Text = "Load"
        Me.btnload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnload.UseVisualStyleBackColor = False
        '
        'chkdate
        '
        Me.chkdate.AutoSize = True
        Me.chkdate.Location = New System.Drawing.Point(437, 19)
        Me.chkdate.Name = "chkdate"
        Me.chkdate.Size = New System.Drawing.Size(78, 17)
        Me.chkdate.TabIndex = 345474
        Me.chkdate.Text = "Apply Date"
        Me.chkdate.UseVisualStyleBackColor = True
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
        Me.txtSearch.Location = New System.Drawing.Point(227, 8)
        Me.txtSearch.MaxLength = 50
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSearch.Size = New System.Drawing.Size(203, 20)
        Me.txtSearch.TabIndex = 345472
        '
        'cmbcolms
        '
        Me.cmbcolms.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbcolms.BackColor = System.Drawing.SystemColors.Window
        Me.cmbcolms.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbcolms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcolms.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcolms.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbcolms.Location = New System.Drawing.Point(99, 6)
        Me.cmbcolms.Name = "cmbcolms"
        Me.cmbcolms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbcolms.Size = New System.Drawing.Size(124, 22)
        Me.cmbcolms.TabIndex = 345471
        Me.cmbcolms.TabStop = False
        '
        'Timer1
        '
        '
        'lblcap
        '
        Me.lblcap.AutoSize = True
        Me.lblcap.BackColor = System.Drawing.Color.Transparent
        Me.lblcap.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcap.ForeColor = System.Drawing.Color.Maroon
        Me.lblcap.Location = New System.Drawing.Point(12, 9)
        Me.lblcap.Name = "lblcap"
        Me.lblcap.Size = New System.Drawing.Size(49, 16)
        Me.lblcap.TabIndex = 345454
        Me.lblcap.Text = "Label1"
        '
        'chkdontshow
        '
        Me.chkdontshow.AutoSize = True
        Me.chkdontshow.BackColor = System.Drawing.Color.Transparent
        Me.chkdontshow.Location = New System.Drawing.Point(726, 8)
        Me.chkdontshow.Name = "chkdontshow"
        Me.chkdontshow.Size = New System.Drawing.Size(121, 17)
        Me.chkdontshow.TabIndex = 345475
        Me.chkdontshow.Text = "Do not Show Today"
        Me.chkdontshow.UseVisualStyleBackColor = False
        '
        'ShowAlert
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.CancelButton = Me.btnothcancel
        Me.ClientSize = New System.Drawing.Size(855, 374)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkdontshow)
        Me.Controls.Add(Me.btnothcancel)
        Me.Controls.Add(Me.lblcap)
        Me.Controls.Add(Me.plmain)
        Me.Controls.Add(Me.grdVoucher)
        Me.Name = "ShowAlert"
        Me.ShowInTaskbar = False
        Me.Text = "Show Alert"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plmain.ResumeLayout(False)
        Me.plmain.PerformLayout()
        Me.pldate.ResumeLayout(False)
        Me.pldate.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents btnothcancel As System.Windows.Forms.Button
    Friend WithEvents btnpreview As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpto As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpfrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents plmain As System.Windows.Forms.Panel
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblcap As System.Windows.Forms.Label
    Public WithEvents txtSearch As System.Windows.Forms.TextBox
    Public WithEvents cmbcolms As System.Windows.Forms.ComboBox
    Friend WithEvents btnload As System.Windows.Forms.Button
    Friend WithEvents chkdate As System.Windows.Forms.CheckBox
    Friend WithEvents pldate As System.Windows.Forms.Panel
    Friend WithEvents chkdontshow As System.Windows.Forms.CheckBox
End Class
