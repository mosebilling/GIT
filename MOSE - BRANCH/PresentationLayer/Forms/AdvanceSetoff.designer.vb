<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdvanceSetoff
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
        Me.grdTr = New System.Windows.Forms.DataGridView
        Me._lblCap_8 = New System.Windows.Forms.Label
        Me.txtaccount = New System.Windows.Forms.TextBox
        Me.btnLoad = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.cmdClose = New System.Windows.Forms.Button
        Me.cmdUpdate = New System.Windows.Forms.Button
        Me.cmdHelp = New System.Windows.Forms.Button
        Me.lblDiff = New System.Windows.Forms.Label
        Me.lblTlDebit = New System.Windows.Forms.Label
        Me.lblTlCredit = New System.Windows.Forms.Label
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label26 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lblrecords = New System.Windows.Forms.Label
        CType(Me.grdTr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdTr
        '
        Me.grdTr.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdTr.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.grdTr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdTr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTr.Location = New System.Drawing.Point(12, 65)
        Me.grdTr.Name = "grdTr"
        Me.grdTr.Size = New System.Drawing.Size(782, 238)
        Me.grdTr.TabIndex = 345379
        Me.grdTr.TabStop = False
        '
        '_lblCap_8
        '
        Me._lblCap_8.AutoSize = True
        Me._lblCap_8.BackColor = System.Drawing.Color.Transparent
        Me._lblCap_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblCap_8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblCap_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblCap_8.Location = New System.Drawing.Point(11, 41)
        Me._lblCap_8.Name = "_lblCap_8"
        Me._lblCap_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblCap_8.Size = New System.Drawing.Size(105, 14)
        Me._lblCap_8.TabIndex = 345378
        Me._lblCap_8.Text = "Customer Name (Cr)"
        '
        'txtaccount
        '
        Me.txtaccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtaccount.Location = New System.Drawing.Point(217, 39)
        Me.txtaccount.Name = "txtaccount"
        Me.txtaccount.Size = New System.Drawing.Size(287, 20)
        Me.txtaccount.TabIndex = 345377
        '
        'btnLoad
        '
        Me.btnLoad.BackColor = System.Drawing.Color.SteelBlue
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.ForeColor = System.Drawing.Color.White
        Me.btnLoad.Location = New System.Drawing.Point(510, 34)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(151, 29)
        Me.btnLoad.TabIndex = 345380
        Me.btnLoad.Text = "&Load Un-set Invoices"
        Me.btnLoad.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.cmdClose)
        Me.Panel2.Controls.Add(Me.cmdUpdate)
        Me.Panel2.Controls.Add(Me.cmdHelp)
        Me.Panel2.Location = New System.Drawing.Point(7, 338)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(793, 39)
        Me.Panel2.TabIndex = 345381
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.Color.White
        Me.cmdClose.Location = New System.Drawing.Point(700, 3)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(93, 33)
        Me.cmdClose.TabIndex = 29
        Me.cmdClose.TabStop = False
        Me.cmdClose.Text = "E&xit"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdUpdate.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.ForeColor = System.Drawing.Color.White
        Me.cmdUpdate.Location = New System.Drawing.Point(604, 3)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUpdate.Size = New System.Drawing.Size(93, 33)
        Me.cmdUpdate.TabIndex = 11
        Me.cmdUpdate.TabStop = False
        Me.cmdUpdate.Text = "&Update"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'cmdHelp
        '
        Me.cmdHelp.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdHelp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdHelp.ForeColor = System.Drawing.Color.White
        Me.cmdHelp.Location = New System.Drawing.Point(5, 3)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdHelp.Size = New System.Drawing.Size(93, 33)
        Me.cmdHelp.TabIndex = 28
        Me.cmdHelp.TabStop = False
        Me.cmdHelp.Text = "&Clear"
        Me.cmdHelp.UseVisualStyleBackColor = False
        '
        'lblDiff
        '
        Me.lblDiff.BackColor = System.Drawing.Color.Transparent
        Me.lblDiff.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDiff.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiff.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDiff.Location = New System.Drawing.Point(591, 5)
        Me.lblDiff.Name = "lblDiff"
        Me.lblDiff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDiff.Size = New System.Drawing.Size(145, 16)
        Me.lblDiff.TabIndex = 36
        Me.lblDiff.Text = "0.000"
        Me.lblDiff.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTlDebit
        '
        Me.lblTlDebit.BackColor = System.Drawing.Color.Transparent
        Me.lblTlDebit.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTlDebit.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTlDebit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTlDebit.Location = New System.Drawing.Point(207, 5)
        Me.lblTlDebit.Name = "lblTlDebit"
        Me.lblTlDebit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTlDebit.Size = New System.Drawing.Size(145, 16)
        Me.lblTlDebit.TabIndex = 34
        Me.lblTlDebit.Text = "0.000"
        Me.lblTlDebit.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTlCredit
        '
        Me.lblTlCredit.BackColor = System.Drawing.Color.Transparent
        Me.lblTlCredit.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTlCredit.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTlCredit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTlCredit.Location = New System.Drawing.Point(395, 5)
        Me.lblTlCredit.Name = "lblTlCredit"
        Me.lblTlCredit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTlCredit.Size = New System.Drawing.Size(145, 16)
        Me.lblTlCredit.TabIndex = 33
        Me.lblTlCredit.Text = "0.000"
        Me.lblTlCredit.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtCode
        '
        Me.txtCode.AcceptsReturn = True
        Me.txtCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCode.HideSelection = False
        Me.txtCode.Location = New System.Drawing.Point(122, 39)
        Me.txtCode.MaxLength = 10
        Me.txtCode.Name = "txtCode"
        Me.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCode.Size = New System.Drawing.Size(89, 20)
        Me.txtCode.TabIndex = 345382
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(806, 32)
        Me.Panel1.TabIndex = 345502
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(37, 6)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(300, 18)
        Me.Label26.TabIndex = 345458
        Me.Label26.Text = "ADVANCE SET OFF [Customer / Supplier]"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.application_icon
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(3, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(30, 27)
        Me.PictureBox2.TabIndex = 345457
        Me.PictureBox2.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(546, 5)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(39, 18)
        Me.Label9.TabIndex = 345506
        Me.Label9.Text = "Diff."
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(358, 5)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(31, 18)
        Me.Label6.TabIndex = 345505
        Me.Label6.Text = "Cr."
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(170, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(31, 18)
        Me.Label3.TabIndex = 345504
        Me.Label3.Text = "Dr."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(96, 5)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(68, 23)
        Me.Label10.TabIndex = 345503
        Me.Label10.Text = "TOTAL"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.lblTlDebit)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.lblTlCredit)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.lblDiff)
        Me.Panel3.Location = New System.Drawing.Point(55, 309)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(739, 26)
        Me.Panel3.TabIndex = 345507
        '
        'lblrecords
        '
        Me.lblrecords.AutoSize = True
        Me.lblrecords.BackColor = System.Drawing.Color.Transparent
        Me.lblrecords.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblrecords.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblrecords.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblrecords.Location = New System.Drawing.Point(667, 42)
        Me.lblrecords.Name = "lblrecords"
        Me.lblrecords.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblrecords.Size = New System.Drawing.Size(48, 14)
        Me.lblrecords.TabIndex = 345508
        Me.lblrecords.Text = "Records"
        '
        'AdvanceSetoff
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(806, 379)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblrecords)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.grdTr)
        Me.Controls.Add(Me._lblCap_8)
        Me.Controls.Add(Me.txtaccount)
        Me.Name = "AdvanceSetoff"
        Me.Text = "AdvanceSetoff"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdTr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdTr As System.Windows.Forms.DataGridView
    Public WithEvents _lblCap_8 As System.Windows.Forms.Label
    Friend WithEvents txtaccount As System.Windows.Forms.TextBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Public WithEvents cmdUpdate As System.Windows.Forms.Button
    Public WithEvents lblDiff As System.Windows.Forms.Label
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdHelp As System.Windows.Forms.Button
    Public WithEvents lblTlDebit As System.Windows.Forms.Label
    Public WithEvents lblTlCredit As System.Windows.Forms.Label
    Public WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Public WithEvents lblrecords As System.Windows.Forms.Label
End Class
