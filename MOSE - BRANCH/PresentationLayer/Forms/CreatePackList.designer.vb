<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CreatePackList
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtbu = New System.Windows.Forms.TextBox
        Me.lblbu = New System.Windows.Forms.Label
        Me.txtpu = New System.Windows.Forms.TextBox
        Me.lblunit = New System.Windows.Forms.Label
        Me.cmbUnit = New System.Windows.Forms.ComboBox
        Me.numWprice = New System.Windows.Forms.TextBox
        Me.numPrice = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.BtnExit = New System.Windows.Forms.Button
        Me.BtnUpdate = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label26 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.GroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.txtbu)
        Me.GroupBox2.Controls.Add(Me.lblbu)
        Me.GroupBox2.Controls.Add(Me.txtpu)
        Me.GroupBox2.Controls.Add(Me.lblunit)
        Me.GroupBox2.Controls.Add(Me.cmbUnit)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 44)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(270, 59)
        Me.GroupBox2.TabIndex = 167
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Set Pack Info"
        '
        'txtbu
        '
        Me.txtbu.BackColor = System.Drawing.Color.White
        Me.txtbu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtbu.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbu.Location = New System.Drawing.Point(192, 17)
        Me.txtbu.MaxLength = 60
        Me.txtbu.Name = "txtbu"
        Me.txtbu.Size = New System.Drawing.Size(40, 21)
        Me.txtbu.TabIndex = 163
        Me.txtbu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblbu
        '
        Me.lblbu.AutoSize = True
        Me.lblbu.BackColor = System.Drawing.Color.Transparent
        Me.lblbu.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbu.Location = New System.Drawing.Point(237, 19)
        Me.lblbu.Name = "lblbu"
        Me.lblbu.Size = New System.Drawing.Size(24, 15)
        Me.lblbu.TabIndex = 162
        Me.lblbu.Text = "BU"
        '
        'txtpu
        '
        Me.txtpu.BackColor = System.Drawing.Color.White
        Me.txtpu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtpu.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpu.Location = New System.Drawing.Point(148, 17)
        Me.txtpu.MaxLength = 60
        Me.txtpu.Name = "txtpu"
        Me.txtpu.ReadOnly = True
        Me.txtpu.Size = New System.Drawing.Size(40, 21)
        Me.txtpu.TabIndex = 160
        Me.txtpu.Text = "1"
        Me.txtpu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblunit
        '
        Me.lblunit.AutoSize = True
        Me.lblunit.BackColor = System.Drawing.Color.Transparent
        Me.lblunit.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblunit.Location = New System.Drawing.Point(16, 19)
        Me.lblunit.Name = "lblunit"
        Me.lblunit.Size = New System.Drawing.Size(33, 17)
        Me.lblunit.TabIndex = 157
        Me.lblunit.Text = "Unit"
        '
        'cmbUnit
        '
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUnit.FormattingEnabled = True
        Me.cmbUnit.Location = New System.Drawing.Point(55, 16)
        Me.cmbUnit.MaxLength = 10
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.Size = New System.Drawing.Size(90, 24)
        Me.cmbUnit.TabIndex = 3
        '
        'numWprice
        '
        Me.numWprice.BackColor = System.Drawing.Color.White
        Me.numWprice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numWprice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numWprice.Location = New System.Drawing.Point(126, 174)
        Me.numWprice.MaxLength = 60
        Me.numWprice.Name = "numWprice"
        Me.numWprice.Size = New System.Drawing.Size(123, 21)
        Me.numWprice.TabIndex = 5
        Me.numWprice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'numPrice
        '
        Me.numPrice.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.numPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numPrice.ForeColor = System.Drawing.Color.Black
        Me.numPrice.Location = New System.Drawing.Point(126, 149)
        Me.numPrice.MaxLength = 60
        Me.numPrice.Name = "numPrice"
        Me.numPrice.Size = New System.Drawing.Size(123, 21)
        Me.numPrice.TabIndex = 4
        Me.numPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(42, 176)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(63, 15)
        Me.Label12.TabIndex = 159
        Me.Label12.Text = "W.S. Price"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(42, 148)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 15)
        Me.Label3.TabIndex = 151
        Me.Label3.Text = "Sales Price"
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExit.ForeColor = System.Drawing.Color.White
        Me.BtnExit.Location = New System.Drawing.Point(213, 108)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(66, 35)
        Me.BtnExit.TabIndex = 169
        Me.BtnExit.Text = "Exit"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'BtnUpdate
        '
        Me.BtnUpdate.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdate.ForeColor = System.Drawing.Color.White
        Me.BtnUpdate.Location = New System.Drawing.Point(144, 108)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnUpdate.Size = New System.Drawing.Size(66, 35)
        Me.BtnUpdate.TabIndex = 6
        Me.BtnUpdate.Tag = "56"
        Me.BtnUpdate.Text = "&Update"
        Me.BtnUpdate.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label26)
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(288, 32)
        Me.Panel2.TabIndex = 345461
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Font = New System.Drawing.Font("Goudy Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(41, 5)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(130, 21)
        Me.Label26.TabIndex = 345458
        Me.Label26.Text = "ADD PACK LIST"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(3, -1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 35)
        Me.PictureBox1.TabIndex = 345457
        Me.PictureBox1.TabStop = False
        '
        'CreatePackList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(288, 145)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.BtnUpdate)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.numPrice)
        Me.Controls.Add(Me.numWprice)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label12)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CreatePackList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents numWprice As System.Windows.Forms.TextBox
    Friend WithEvents numPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblunit As System.Windows.Forms.Label
    Friend WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents BtnExit As System.Windows.Forms.Button
    Public WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents txtbu As System.Windows.Forms.TextBox
    Friend WithEvents lblbu As System.Windows.Forms.Label
    Friend WithEvents txtpu As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
