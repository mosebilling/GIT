<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemImageFrm
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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnupload = New System.Windows.Forms.Button
        Me.lblpicpath = New System.Windows.Forms.Label
        Me.btnbrowse = New System.Windows.Forms.Button
        Me.picImge = New System.Windows.Forms.PictureBox
        Me.btnupdate = New System.Windows.Forms.Button
        Me.btnexit = New System.Windows.Forms.Button
        Me.DlgOpen = New System.Windows.Forms.OpenFileDialog
        Me.lblitem = New System.Windows.Forms.Label
        Me.picloader = New System.Windows.Forms.PictureBox
        Me.Worker = New System.ComponentModel.BackgroundWorker
        Me.GroupBox3.SuspendLayout()
        CType(Me.picImge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picloader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.btnupload)
        Me.GroupBox3.Controls.Add(Me.lblpicpath)
        Me.GroupBox3.Controls.Add(Me.btnbrowse)
        Me.GroupBox3.Controls.Add(Me.picImge)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(546, 430)
        Me.GroupBox3.TabIndex = 345415
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Image"
        '
        'btnupload
        '
        Me.btnupload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnupload.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupload.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnupload.Enabled = False
        Me.btnupload.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupload.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupload.ForeColor = System.Drawing.Color.White
        Me.btnupload.Location = New System.Drawing.Point(174, 390)
        Me.btnupload.Name = "btnupload"
        Me.btnupload.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnupload.Size = New System.Drawing.Size(131, 23)
        Me.btnupload.TabIndex = 345500
        Me.btnupload.Tag = "56"
        Me.btnupload.Text = "Upload to Server"
        Me.btnupload.UseVisualStyleBackColor = False
        '
        'lblpicpath
        '
        Me.lblpicpath.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblpicpath.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblpicpath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblpicpath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblpicpath.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpicpath.Location = New System.Drawing.Point(11, 390)
        Me.lblpicpath.Name = "lblpicpath"
        Me.lblpicpath.Size = New System.Drawing.Size(157, 21)
        Me.lblpicpath.TabIndex = 345436
        Me.lblpicpath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnbrowse
        '
        Me.btnbrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnbrowse.Enabled = False
        Me.btnbrowse.Location = New System.Drawing.Point(511, 394)
        Me.btnbrowse.Name = "btnbrowse"
        Me.btnbrowse.Size = New System.Drawing.Size(29, 29)
        Me.btnbrowse.TabIndex = 345386
        Me.btnbrowse.Text = "..."
        Me.btnbrowse.UseVisualStyleBackColor = True
        '
        'picImge
        '
        Me.picImge.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picImge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picImge.ImageLocation = ""
        Me.picImge.Location = New System.Drawing.Point(11, 19)
        Me.picImge.Name = "picImge"
        Me.picImge.Size = New System.Drawing.Size(529, 368)
        Me.picImge.TabIndex = 0
        Me.picImge.TabStop = False
        '
        'btnupdate
        '
        Me.btnupdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnupdate.BackColor = System.Drawing.Color.SteelBlue
        Me.btnupdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnupdate.Enabled = False
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.Location = New System.Drawing.Point(396, 448)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnupdate.Size = New System.Drawing.Size(96, 36)
        Me.btnupdate.TabIndex = 345501
        Me.btnupdate.Tag = "56"
        Me.btnupdate.Text = "Update"
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'btnexit
        '
        Me.btnexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.Location = New System.Drawing.Point(498, 448)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnexit.Size = New System.Drawing.Size(60, 36)
        Me.btnexit.TabIndex = 345502
        Me.btnexit.Tag = "56"
        Me.btnexit.Text = "Exit"
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'DlgOpen
        '
        Me.DlgOpen.FileName = "OpenFileDialog1"
        '
        'lblitem
        '
        Me.lblitem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblitem.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblitem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblitem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblitem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblitem.Location = New System.Drawing.Point(12, 448)
        Me.lblitem.Name = "lblitem"
        Me.lblitem.Size = New System.Drawing.Size(339, 26)
        Me.lblitem.TabIndex = 345503
        Me.lblitem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'picloader
        '
        Me.picloader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picloader.ImageLocation = ""
        Me.picloader.Location = New System.Drawing.Point(244, 171)
        Me.picloader.Name = "picloader"
        Me.picloader.Size = New System.Drawing.Size(83, 73)
        Me.picloader.TabIndex = 345501
        Me.picloader.TabStop = False
        Me.picloader.Visible = False
        '
        'Worker
        '
        '
        'ItemImageFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(565, 495)
        Me.ControlBox = False
        Me.Controls.Add(Me.picloader)
        Me.Controls.Add(Me.lblitem)
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.btnupdate)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "ItemImageFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Product Image"
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.picImge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picloader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Public WithEvents btnupload As System.Windows.Forms.Button
    Friend WithEvents lblpicpath As System.Windows.Forms.Label
    Friend WithEvents btnbrowse As System.Windows.Forms.Button
    Friend WithEvents picImge As System.Windows.Forms.PictureBox
    Public WithEvents btnupdate As System.Windows.Forms.Button
    Public WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents DlgOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblitem As System.Windows.Forms.Label
    Friend WithEvents picloader As System.Windows.Forms.PictureBox
    Friend WithEvents Worker As System.ComponentModel.BackgroundWorker
End Class
