<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TreatmentItem
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
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.BtnExit = New System.Windows.Forms.Button
        Me.BtnUpdate = New System.Windows.Forms.Button
        Me.lblCode = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtTrDescr = New System.Windows.Forms.TextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.lblName = New System.Windows.Forms.Label
        Me.lblgstp = New System.Windows.Forms.Label
        Me.txthsncode = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtpriceWtax = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtmrp = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtws = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.numunitprice = New System.Windows.Forms.TextBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtamount = New System.Windows.Forms.TextBox
        Me.txtpercentage = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.rdowithtax = New System.Windows.Forms.RadioButton
        Me.rdowithouttax = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnset = New System.Windows.Forms.Button
        Me.grdItem = New System.Windows.Forms.DataGridView
        Me.btnclear = New System.Windows.Forms.Button
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCode
        '
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Location = New System.Drawing.Point(91, 46)
        Me.txtCode.MaxLength = 30
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(123, 20)
        Me.txtCode.TabIndex = 0
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnExit.FlatAppearance.BorderSize = 0
        Me.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExit.ForeColor = System.Drawing.Color.White
        Me.BtnExit.Location = New System.Drawing.Point(426, 148)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(85, 25)
        Me.BtnExit.TabIndex = 345363
        Me.BtnExit.Text = "E&xit"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'BtnUpdate
        '
        Me.BtnUpdate.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnUpdate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnUpdate.Enabled = False
        Me.BtnUpdate.FlatAppearance.BorderSize = 0
        Me.BtnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdate.ForeColor = System.Drawing.Color.White
        Me.BtnUpdate.Location = New System.Drawing.Point(335, 148)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(85, 25)
        Me.BtnUpdate.TabIndex = 9
        Me.BtnUpdate.Text = "&Update"
        Me.BtnUpdate.UseVisualStyleBackColor = False
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCode.Location = New System.Drawing.Point(10, 50)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(32, 13)
        Me.lblCode.TabIndex = 345358
        Me.lblCode.Text = "Code"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(9, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 345359
        Me.Label3.Text = "Sales Price"
        '
        'txtTrDescr
        '
        Me.txtTrDescr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTrDescr.Location = New System.Drawing.Point(91, 69)
        Me.txtTrDescr.MaxLength = 50
        Me.txtTrDescr.Name = "txtTrDescr"
        Me.txtTrDescr.Size = New System.Drawing.Size(420, 20)
        Me.txtTrDescr.TabIndex = 2
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Location = New System.Drawing.Point(10, 72)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(58, 13)
        Me.Label34.TabIndex = 345362
        Me.Label34.Text = "Item Name"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.PictureBox2)
        Me.Panel2.Controls.Add(Me.lblName)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(523, 34)
        Me.Panel2.TabIndex = 345404
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.about
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(3, 6)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(40, 22)
        Me.PictureBox2.TabIndex = 345460
        Me.PictureBox2.TabStop = False
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.Black
        Me.lblName.Location = New System.Drawing.Point(47, 6)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(166, 20)
        Me.lblName.TabIndex = 6
        Me.lblName.Text = "Create/Edit Treatment"
        '
        'lblgstp
        '
        Me.lblgstp.AutoSize = True
        Me.lblgstp.BackColor = System.Drawing.Color.Transparent
        Me.lblgstp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgstp.ForeColor = System.Drawing.Color.Maroon
        Me.lblgstp.Location = New System.Drawing.Point(385, 119)
        Me.lblgstp.Name = "lblgstp"
        Me.lblgstp.Size = New System.Drawing.Size(40, 15)
        Me.lblgstp.TabIndex = 345430
        Me.lblgstp.Text = "GST : "
        '
        'txthsncode
        '
        Me.txthsncode.BackColor = System.Drawing.Color.White
        Me.txthsncode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txthsncode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txthsncode.Location = New System.Drawing.Point(388, 95)
        Me.txthsncode.MaxLength = 60
        Me.txthsncode.Name = "txthsncode"
        Me.txthsncode.Size = New System.Drawing.Size(123, 21)
        Me.txthsncode.TabIndex = 3
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(307, 97)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(80, 15)
        Me.Label19.TabIndex = 345429
        Me.Label19.Text = "Set Tax Code"
        '
        'txtpriceWtax
        '
        Me.txtpriceWtax.BackColor = System.Drawing.Color.White
        Me.txtpriceWtax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtpriceWtax.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpriceWtax.Location = New System.Drawing.Point(90, 116)
        Me.txtpriceWtax.MaxLength = 60
        Me.txtpriceWtax.Name = "txtpriceWtax"
        Me.txtpriceWtax.Size = New System.Drawing.Size(123, 21)
        Me.txtpriceWtax.TabIndex = 6
        Me.txtpriceWtax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(9, 116)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(68, 15)
        Me.Label21.TabIndex = 345484
        Me.Label21.Text = "Price + Tax"
        '
        'txtmrp
        '
        Me.txtmrp.BackColor = System.Drawing.Color.White
        Me.txtmrp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtmrp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmrp.Location = New System.Drawing.Point(174, 44)
        Me.txtmrp.MaxLength = 60
        Me.txtmrp.Name = "txtmrp"
        Me.txtmrp.Size = New System.Drawing.Size(123, 21)
        Me.txtmrp.TabIndex = 8
        Me.txtmrp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtmrp.Visible = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(93, 44)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(41, 15)
        Me.Label20.TabIndex = 345483
        Me.Label20.Text = "M.R.P"
        Me.Label20.Visible = False
        '
        'txtws
        '
        Me.txtws.BackColor = System.Drawing.Color.White
        Me.txtws.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtws.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtws.Location = New System.Drawing.Point(174, 20)
        Me.txtws.MaxLength = 60
        Me.txtws.Name = "txtws"
        Me.txtws.Size = New System.Drawing.Size(123, 21)
        Me.txtws.TabIndex = 7
        Me.txtws.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtws.Visible = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(93, 20)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(60, 15)
        Me.Label18.TabIndex = 345482
        Me.Label18.Text = "WS. Price"
        Me.Label18.Visible = False
        '
        'numunitprice
        '
        Me.numunitprice.BackColor = System.Drawing.Color.White
        Me.numunitprice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numunitprice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numunitprice.Location = New System.Drawing.Point(90, 92)
        Me.numunitprice.MaxLength = 60
        Me.numunitprice.Name = "numunitprice"
        Me.numunitprice.Size = New System.Drawing.Size(123, 21)
        Me.numunitprice.TabIndex = 5
        Me.numunitprice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Timer1
        '
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(12, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 345488
        Me.Label2.Text = "Amount"
        '
        'txtamount
        '
        Me.txtamount.BackColor = System.Drawing.Color.White
        Me.txtamount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtamount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtamount.Location = New System.Drawing.Point(15, 38)
        Me.txtamount.MaxLength = 60
        Me.txtamount.Name = "txtamount"
        Me.txtamount.Size = New System.Drawing.Size(123, 21)
        Me.txtamount.TabIndex = 345489
        Me.txtamount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtpercentage
        '
        Me.txtpercentage.BackColor = System.Drawing.Color.White
        Me.txtpercentage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtpercentage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpercentage.Location = New System.Drawing.Point(153, 38)
        Me.txtpercentage.MaxLength = 60
        Me.txtpercentage.Name = "txtpercentage"
        Me.txtpercentage.Size = New System.Drawing.Size(54, 21)
        Me.txtpercentage.TabIndex = 345490
        Me.txtpercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(138, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(15, 13)
        Me.Label4.TabIndex = 345491
        Me.Label4.Text = "%"
        '
        'rdowithtax
        '
        Me.rdowithtax.AutoSize = True
        Me.rdowithtax.Checked = True
        Me.rdowithtax.Location = New System.Drawing.Point(15, 67)
        Me.rdowithtax.Name = "rdowithtax"
        Me.rdowithtax.Size = New System.Drawing.Size(95, 17)
        Me.rdowithtax.TabIndex = 345492
        Me.rdowithtax.TabStop = True
        Me.rdowithtax.Text = "With Tax Price"
        Me.rdowithtax.UseVisualStyleBackColor = True
        '
        'rdowithouttax
        '
        Me.rdowithouttax.AutoSize = True
        Me.rdowithouttax.Location = New System.Drawing.Point(116, 67)
        Me.rdowithouttax.Name = "rdowithouttax"
        Me.rdowithouttax.Size = New System.Drawing.Size(110, 17)
        Me.rdowithouttax.TabIndex = 345493
        Me.rdowithouttax.Text = "Without Tax Price"
        Me.rdowithouttax.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.btnset)
        Me.GroupBox1.Controls.Add(Me.rdowithouttax)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.rdowithtax)
        Me.GroupBox1.Controls.Add(Me.txtamount)
        Me.GroupBox1.Controls.Add(Me.txtmrp)
        Me.GroupBox1.Controls.Add(Me.txtpercentage)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtws)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Location = New System.Drawing.Point(178, 299)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(291, 96)
        Me.GroupBox1.TabIndex = 345494
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Calculate Tax from"
        Me.GroupBox1.Visible = False
        '
        'btnset
        '
        Me.btnset.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnset.Location = New System.Drawing.Point(213, 35)
        Me.btnset.Name = "btnset"
        Me.btnset.Size = New System.Drawing.Size(71, 26)
        Me.btnset.TabIndex = 345492
        Me.btnset.Text = "Set"
        Me.btnset.UseVisualStyleBackColor = True
        '
        'grdItem
        '
        Me.grdItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItem.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItem.Location = New System.Drawing.Point(13, 179)
        Me.grdItem.Name = "grdItem"
        Me.grdItem.Size = New System.Drawing.Size(498, 227)
        Me.grdItem.TabIndex = 345495
        '
        'btnclear
        '
        Me.btnclear.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnclear.FlatAppearance.BorderSize = 0
        Me.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.ForeColor = System.Drawing.Color.White
        Me.btnclear.Location = New System.Drawing.Point(13, 148)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(85, 25)
        Me.btnclear.TabIndex = 345496
        Me.btnclear.Text = "Clear"
        Me.btnclear.UseVisualStyleBackColor = False
        '
        'TreatmentItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.CancelButton = Me.BtnExit
        Me.ClientSize = New System.Drawing.Size(523, 423)
        Me.Controls.Add(Me.btnclear)
        Me.Controls.Add(Me.grdItem)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.numunitprice)
        Me.Controls.Add(Me.txtpriceWtax)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.lblgstp)
        Me.Controls.Add(Me.txthsncode)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnUpdate)
        Me.Controls.Add(Me.lblCode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtTrDescr)
        Me.Controls.Add(Me.Label34)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "TreatmentItem"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Treatment Item"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents BtnExit As System.Windows.Forms.Button
    Friend WithEvents BtnUpdate As System.Windows.Forms.Button
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTrDescr As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents lblgstp As System.Windows.Forms.Label
    Friend WithEvents txthsncode As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtpriceWtax As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtmrp As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtws As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents numunitprice As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtamount As System.Windows.Forms.TextBox
    Friend WithEvents txtpercentage As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rdowithtax As System.Windows.Forms.RadioButton
    Friend WithEvents rdowithouttax As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnset As System.Windows.Forms.Button
    Friend WithEvents grdItem As System.Windows.Forms.DataGridView
    Friend WithEvents btnclear As System.Windows.Forms.Button
End Class
