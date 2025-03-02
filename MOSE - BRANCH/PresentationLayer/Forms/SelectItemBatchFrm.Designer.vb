<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectItemBatchFrm
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
        Me.grdSrch = New System.Windows.Forms.DataGridView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label26 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.btnadd = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.txtname = New System.Windows.Forms.TextBox
        Me.rdobatch = New System.Windows.Forms.RadioButton
        Me.rdomrp = New System.Windows.Forms.RadioButton
        Me.rdoprice = New System.Windows.Forms.RadioButton
        Me.lblitem = New System.Windows.Forms.Label
        CType(Me.grdSrch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdSrch
        '
        Me.grdSrch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSrch.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.grdSrch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSrch.Location = New System.Drawing.Point(4, 85)
        Me.grdSrch.Name = "grdSrch"
        Me.grdSrch.Size = New System.Drawing.Size(464, 154)
        Me.grdSrch.TabIndex = 4
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label26)
        Me.Panel2.Controls.Add(Me.PictureBox2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(472, 32)
        Me.Panel2.TabIndex = 345463
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(41, 5)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(145, 20)
        Me.Label26.TabIndex = 345458
        Me.Label26.Text = "SELECT ITEM BATCH"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(3, 5)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(37, 20)
        Me.PictureBox2.TabIndex = 345457
        Me.PictureBox2.TabStop = False
        '
        'btnadd
        '
        Me.btnadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnadd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.ForeColor = System.Drawing.Color.White
        Me.btnadd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnadd.Location = New System.Drawing.Point(260, 245)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(113, 35)
        Me.btnadd.TabIndex = 345464
        Me.btnadd.Text = "&Select [Alt+S]"
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(375, 245)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(93, 35)
        Me.btnExit.TabIndex = 345465
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'txtname
        '
        Me.txtname.Location = New System.Drawing.Point(3, 61)
        Me.txtname.MaxLength = 30
        Me.txtname.Name = "txtname"
        Me.txtname.Size = New System.Drawing.Size(234, 20)
        Me.txtname.TabIndex = 345466
        '
        'rdobatch
        '
        Me.rdobatch.AutoSize = True
        Me.rdobatch.BackColor = System.Drawing.Color.Transparent
        Me.rdobatch.Checked = True
        Me.rdobatch.Location = New System.Drawing.Point(243, 62)
        Me.rdobatch.Name = "rdobatch"
        Me.rdobatch.Size = New System.Drawing.Size(53, 17)
        Me.rdobatch.TabIndex = 345467
        Me.rdobatch.TabStop = True
        Me.rdobatch.Text = "Batch"
        Me.rdobatch.UseVisualStyleBackColor = False
        '
        'rdomrp
        '
        Me.rdomrp.AutoSize = True
        Me.rdomrp.BackColor = System.Drawing.Color.Transparent
        Me.rdomrp.Location = New System.Drawing.Point(302, 62)
        Me.rdomrp.Name = "rdomrp"
        Me.rdomrp.Size = New System.Drawing.Size(49, 17)
        Me.rdomrp.TabIndex = 345468
        Me.rdomrp.Text = "MRP"
        Me.rdomrp.UseVisualStyleBackColor = False
        '
        'rdoprice
        '
        Me.rdoprice.AutoSize = True
        Me.rdoprice.BackColor = System.Drawing.Color.Transparent
        Me.rdoprice.Location = New System.Drawing.Point(357, 62)
        Me.rdoprice.Name = "rdoprice"
        Me.rdoprice.Size = New System.Drawing.Size(71, 17)
        Me.rdoprice.TabIndex = 345469
        Me.rdoprice.TabStop = True
        Me.rdoprice.Text = "Unit Price"
        Me.rdoprice.UseVisualStyleBackColor = False
        '
        'lblitem
        '
        Me.lblitem.AutoSize = True
        Me.lblitem.BackColor = System.Drawing.Color.White
        Me.lblitem.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblitem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblitem.Location = New System.Drawing.Point(0, 38)
        Me.lblitem.Name = "lblitem"
        Me.lblitem.Size = New System.Drawing.Size(36, 20)
        Me.lblitem.TabIndex = 345470
        Me.lblitem.Text = "item"
        '
        'SelectItemBatchFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(472, 282)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblitem)
        Me.Controls.Add(Me.rdoprice)
        Me.Controls.Add(Me.rdomrp)
        Me.Controls.Add(Me.rdobatch)
        Me.Controls.Add(Me.txtname)
        Me.Controls.Add(Me.btnadd)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.grdSrch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "SelectItemBatchFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Item Batch "
        CType(Me.grdSrch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdSrch As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents txtname As System.Windows.Forms.TextBox
    Friend WithEvents rdobatch As System.Windows.Forms.RadioButton
    Friend WithEvents rdomrp As System.Windows.Forms.RadioButton
    Friend WithEvents rdoprice As System.Windows.Forms.RadioButton
    Friend WithEvents lblitem As System.Windows.Forms.Label
End Class
