<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DocumentView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DocumentView))
        Me.plrow = New System.Windows.Forms.Panel
        Me.btndecsave = New System.Windows.Forms.Button
        Me.txtimgDesc = New System.Windows.Forms.TextBox
        Me.btnclose = New System.Windows.Forms.Button
        Me.btnopen = New System.Windows.Forms.Button
        Me.txtpath = New System.Windows.Forms.TextBox
        Me.btnremove = New System.Windows.Forms.Button
        Me.btnadd = New System.Windows.Forms.Button
        Me.btnLast = New System.Windows.Forms.Button
        Me.btnNext = New System.Windows.Forms.Button
        Me.btnPre = New System.Windows.Forms.Button
        Me.btnFirst = New System.Windows.Forms.Button
        Me.opfSelectFile = New System.Windows.Forms.OpenFileDialog
        Me.picImage = New System.Windows.Forms.PictureBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.plrow.SuspendLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'plrow
        '
        Me.plrow.Controls.Add(Me.btndecsave)
        Me.plrow.Controls.Add(Me.txtimgDesc)
        Me.plrow.Controls.Add(Me.btnclose)
        Me.plrow.Controls.Add(Me.btnopen)
        Me.plrow.Controls.Add(Me.txtpath)
        Me.plrow.Controls.Add(Me.btnremove)
        Me.plrow.Controls.Add(Me.btnadd)
        Me.plrow.Controls.Add(Me.btnLast)
        Me.plrow.Controls.Add(Me.btnNext)
        Me.plrow.Controls.Add(Me.btnPre)
        Me.plrow.Controls.Add(Me.btnFirst)
        Me.plrow.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.plrow.Location = New System.Drawing.Point(0, 383)
        Me.plrow.Name = "plrow"
        Me.plrow.Size = New System.Drawing.Size(674, 37)
        Me.plrow.TabIndex = 0
        '
        'btndecsave
        '
        Me.btndecsave.BackColor = System.Drawing.SystemColors.Control
        Me.btndecsave.BackgroundImage = Global.SMSMP.My.Resources.Resources.button_save
        Me.btndecsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btndecsave.Cursor = System.Windows.Forms.Cursors.Default
        Me.btndecsave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndecsave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btndecsave.Location = New System.Drawing.Point(385, 7)
        Me.btndecsave.Name = "btndecsave"
        Me.btndecsave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btndecsave.Size = New System.Drawing.Size(26, 25)
        Me.btndecsave.TabIndex = 86
        Me.btndecsave.Tag = "4"
        Me.btndecsave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btndecsave.UseVisualStyleBackColor = False
        '
        'txtimgDesc
        '
        Me.txtimgDesc.Location = New System.Drawing.Point(175, 8)
        Me.txtimgDesc.Name = "txtimgDesc"
        Me.txtimgDesc.Size = New System.Drawing.Size(204, 20)
        Me.txtimgDesc.TabIndex = 85
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.BackColor = System.Drawing.SystemColors.Control
        Me.btnclose.BackgroundImage = Global.SMSMP.My.Resources.Resources.button_cancel
        Me.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnclose.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnclose.Location = New System.Drawing.Point(639, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnclose.Size = New System.Drawing.Size(32, 25)
        Me.btnclose.TabIndex = 84
        Me.btnclose.Tag = "4"
        Me.btnclose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'btnopen
        '
        Me.btnopen.BackColor = System.Drawing.SystemColors.Control
        Me.btnopen.BackgroundImage = Global.SMSMP.My.Resources.Resources.folder_search
        Me.btnopen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnopen.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnopen.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnopen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnopen.Location = New System.Drawing.Point(590, 4)
        Me.btnopen.Name = "btnopen"
        Me.btnopen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnopen.Size = New System.Drawing.Size(25, 25)
        Me.btnopen.TabIndex = 83
        Me.btnopen.Tag = "4"
        Me.btnopen.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnopen.UseVisualStyleBackColor = False
        '
        'txtpath
        '
        Me.txtpath.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtpath.Location = New System.Drawing.Point(432, 7)
        Me.txtpath.Name = "txtpath"
        Me.txtpath.ReadOnly = True
        Me.txtpath.Size = New System.Drawing.Size(152, 20)
        Me.txtpath.TabIndex = 82
        '
        'btnremove
        '
        Me.btnremove.BackColor = System.Drawing.SystemColors.Control
        Me.btnremove.BackgroundImage = Global.SMSMP.My.Resources.Resources.button_delete
        Me.btnremove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnremove.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnremove.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnremove.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnremove.Location = New System.Drawing.Point(143, 4)
        Me.btnremove.Name = "btnremove"
        Me.btnremove.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnremove.Size = New System.Drawing.Size(26, 25)
        Me.btnremove.TabIndex = 81
        Me.btnremove.Tag = "4"
        Me.btnremove.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnremove.UseVisualStyleBackColor = False
        '
        'btnadd
        '
        Me.btnadd.BackColor = System.Drawing.SystemColors.Control
        Me.btnadd.BackgroundImage = Global.SMSMP.My.Resources.Resources.add
        Me.btnadd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnadd.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnadd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnadd.Location = New System.Drawing.Point(114, 4)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnadd.Size = New System.Drawing.Size(26, 25)
        Me.btnadd.TabIndex = 80
        Me.btnadd.Tag = "4"
        Me.btnadd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'btnLast
        '
        Me.btnLast.BackColor = System.Drawing.SystemColors.Control
        Me.btnLast.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnLast.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLast.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnLast.Image = CType(resources.GetObject("btnLast.Image"), System.Drawing.Image)
        Me.btnLast.Location = New System.Drawing.Point(85, 3)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnLast.Size = New System.Drawing.Size(25, 25)
        Me.btnLast.TabIndex = 79
        Me.btnLast.Tag = "4"
        Me.btnLast.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLast.UseVisualStyleBackColor = False
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.SystemColors.Control
        Me.btnNext.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnNext.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnNext.Image = CType(resources.GetObject("btnNext.Image"), System.Drawing.Image)
        Me.btnNext.Location = New System.Drawing.Point(58, 3)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnNext.Size = New System.Drawing.Size(25, 25)
        Me.btnNext.TabIndex = 78
        Me.btnNext.Tag = "3"
        Me.btnNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'btnPre
        '
        Me.btnPre.BackColor = System.Drawing.SystemColors.Control
        Me.btnPre.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnPre.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPre.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnPre.Image = CType(resources.GetObject("btnPre.Image"), System.Drawing.Image)
        Me.btnPre.Location = New System.Drawing.Point(31, 3)
        Me.btnPre.Name = "btnPre"
        Me.btnPre.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnPre.Size = New System.Drawing.Size(25, 25)
        Me.btnPre.TabIndex = 77
        Me.btnPre.Tag = "2"
        Me.btnPre.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnPre.UseVisualStyleBackColor = False
        '
        'btnFirst
        '
        Me.btnFirst.BackColor = System.Drawing.SystemColors.Control
        Me.btnFirst.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnFirst.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFirst.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnFirst.Image = CType(resources.GetObject("btnFirst.Image"), System.Drawing.Image)
        Me.btnFirst.Location = New System.Drawing.Point(4, 3)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnFirst.Size = New System.Drawing.Size(25, 25)
        Me.btnFirst.TabIndex = 76
        Me.btnFirst.Tag = "1"
        Me.btnFirst.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnFirst.UseVisualStyleBackColor = False
        '
        'opfSelectFile
        '
        Me.opfSelectFile.FileName = "OpenFileDialog1"
        '
        'picImage
        '
        Me.picImage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picImage.BackColor = System.Drawing.Color.Transparent
        Me.picImage.InitialImage = CType(resources.GetObject("picImage.InitialImage"), System.Drawing.Image)
        Me.picImage.Location = New System.Drawing.Point(299, 147)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(80, 68)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picImage.TabIndex = 345380
        Me.picImage.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(65, 383)
        Me.Panel1.TabIndex = 345381
        '
        'DocumentView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(674, 420)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.picImage)
        Me.Controls.Add(Me.plrow)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DocumentView"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Document View"
        Me.plrow.ResumeLayout(False)
        Me.plrow.PerformLayout()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents plrow As System.Windows.Forms.Panel
    Public WithEvents btnLast As System.Windows.Forms.Button
    Public WithEvents btnNext As System.Windows.Forms.Button
    Public WithEvents btnPre As System.Windows.Forms.Button
    Public WithEvents btnFirst As System.Windows.Forms.Button
    Public WithEvents btnadd As System.Windows.Forms.Button
    Public WithEvents btnremove As System.Windows.Forms.Button
    Friend WithEvents txtpath As System.Windows.Forms.TextBox
    Friend WithEvents opfSelectFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Public WithEvents btnopen As System.Windows.Forms.Button
    Public WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtimgDesc As System.Windows.Forms.TextBox
    Public WithEvents btndecsave As System.Windows.Forms.Button
End Class
