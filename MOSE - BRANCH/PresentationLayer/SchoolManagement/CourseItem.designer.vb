<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CourseItem
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
        Me.txtpriceWtax = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.numunitprice = New System.Windows.Forms.TextBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.grdItem = New System.Windows.Forms.DataGridView
        Me.cmbtax = New System.Windows.Forms.ComboBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtduration = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txttotalfees = New System.Windows.Forms.TextBox
        Me.txttotalTaxfees = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnclear = New System.Windows.Forms.Button
        Me.btndelete = New System.Windows.Forms.Button
        Me.chkhide = New System.Windows.Forms.CheckBox
        Me.txtsylabus = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCode
        '
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Location = New System.Drawing.Point(93, 46)
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
        Me.BtnExit.Location = New System.Drawing.Point(650, 223)
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
        Me.BtnUpdate.Location = New System.Drawing.Point(650, 161)
        Me.BtnUpdate.Name = "BtnUpdate"
        Me.BtnUpdate.Size = New System.Drawing.Size(85, 25)
        Me.BtnUpdate.TabIndex = 8
        Me.BtnUpdate.Text = "&Update"
        Me.BtnUpdate.UseVisualStyleBackColor = False
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCode.Location = New System.Drawing.Point(22, 50)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(32, 13)
        Me.lblCode.TabIndex = 345358
        Me.lblCode.Text = "Code"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(22, 212)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 345359
        Me.Label3.Text = "Fees"
        '
        'txtTrDescr
        '
        Me.txtTrDescr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTrDescr.Location = New System.Drawing.Point(93, 70)
        Me.txtTrDescr.MaxLength = 50
        Me.txtTrDescr.Name = "txtTrDescr"
        Me.txtTrDescr.Size = New System.Drawing.Size(420, 20)
        Me.txtTrDescr.TabIndex = 2
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Location = New System.Drawing.Point(22, 73)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(71, 13)
        Me.Label34.TabIndex = 345362
        Me.Label34.Text = "Course Name"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.PictureBox2)
        Me.Panel2.Controls.Add(Me.lblName)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(757, 34)
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
        Me.lblName.Size = New System.Drawing.Size(113, 20)
        Me.lblName.TabIndex = 6
        Me.lblName.Text = "Course Master"
        '
        'txtpriceWtax
        '
        Me.txtpriceWtax.BackColor = System.Drawing.Color.Linen
        Me.txtpriceWtax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtpriceWtax.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpriceWtax.Location = New System.Drawing.Point(93, 237)
        Me.txtpriceWtax.MaxLength = 60
        Me.txtpriceWtax.Name = "txtpriceWtax"
        Me.txtpriceWtax.ReadOnly = True
        Me.txtpriceWtax.Size = New System.Drawing.Size(123, 21)
        Me.txtpriceWtax.TabIndex = 6
        Me.txtpriceWtax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(22, 235)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(66, 15)
        Me.Label21.TabIndex = 345484
        Me.Label21.Text = "Fess + Tax"
        '
        'numunitprice
        '
        Me.numunitprice.BackColor = System.Drawing.Color.Linen
        Me.numunitprice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numunitprice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numunitprice.Location = New System.Drawing.Point(93, 212)
        Me.numunitprice.MaxLength = 60
        Me.numunitprice.Name = "numunitprice"
        Me.numunitprice.ReadOnly = True
        Me.numunitprice.Size = New System.Drawing.Size(123, 21)
        Me.numunitprice.TabIndex = 5
        Me.numunitprice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Timer1
        '
        '
        'grdItem
        '
        Me.grdItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItem.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItem.Location = New System.Drawing.Point(12, 335)
        Me.grdItem.Name = "grdItem"
        Me.grdItem.Size = New System.Drawing.Size(723, 196)
        Me.grdItem.TabIndex = 345485
        '
        'cmbtax
        '
        Me.cmbtax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtax.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbtax.FormattingEnabled = True
        Me.cmbtax.Location = New System.Drawing.Point(92, 119)
        Me.cmbtax.MaxLength = 10
        Me.cmbtax.Name = "cmbtax"
        Me.cmbtax.Size = New System.Drawing.Size(124, 23)
        Me.cmbtax.TabIndex = 4
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(22, 121)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(24, 15)
        Me.Label17.TabIndex = 345487
        Me.Label17.Text = "Vat"
        '
        'txtduration
        '
        Me.txtduration.BackColor = System.Drawing.Color.White
        Me.txtduration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtduration.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtduration.Location = New System.Drawing.Point(93, 94)
        Me.txtduration.MaxLength = 60
        Me.txtduration.Name = "txtduration"
        Me.txtduration.Size = New System.Drawing.Size(55, 21)
        Me.txtduration.TabIndex = 3
        Me.txtduration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(22, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 345489
        Me.Label1.Text = "Duration "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(154, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 345490
        Me.Label2.Text = "In Months"
        '
        'txttotalfees
        '
        Me.txttotalfees.BackColor = System.Drawing.Color.White
        Me.txttotalfees.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txttotalfees.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttotalfees.Location = New System.Drawing.Point(93, 148)
        Me.txttotalfees.MaxLength = 60
        Me.txttotalfees.Name = "txttotalfees"
        Me.txttotalfees.Size = New System.Drawing.Size(123, 21)
        Me.txttotalfees.TabIndex = 5
        Me.txttotalfees.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txttotalTaxfees
        '
        Me.txttotalTaxfees.BackColor = System.Drawing.Color.White
        Me.txttotalTaxfees.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txttotalTaxfees.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttotalTaxfees.Location = New System.Drawing.Point(93, 173)
        Me.txttotalTaxfees.MaxLength = 60
        Me.txttotalTaxfees.Name = "txttotalTaxfees"
        Me.txttotalTaxfees.Size = New System.Drawing.Size(123, 21)
        Me.txttotalTaxfees.TabIndex = 6
        Me.txttotalTaxfees.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(22, 171)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 15)
        Me.Label4.TabIndex = 345494
        Me.Label4.Text = "Fess + Tax"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(22, 148)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 13)
        Me.Label5.TabIndex = 345493
        Me.Label5.Text = "Fees"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(90, 196)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 13)
        Me.Label6.TabIndex = 345495
        Me.Label6.Text = "Monthly"
        '
        'btnclear
        '
        Me.btnclear.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnclear.FlatAppearance.BorderSize = 0
        Me.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.ForeColor = System.Drawing.Color.White
        Me.btnclear.Location = New System.Drawing.Point(650, 192)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(85, 25)
        Me.btnclear.TabIndex = 345496
        Me.btnclear.Text = "Clear"
        Me.btnclear.UseVisualStyleBackColor = False
        '
        'btndelete
        '
        Me.btndelete.BackColor = System.Drawing.Color.SteelBlue
        Me.btndelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btndelete.FlatAppearance.BorderSize = 0
        Me.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.ForeColor = System.Drawing.Color.White
        Me.btndelete.Location = New System.Drawing.Point(650, 70)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(85, 25)
        Me.btndelete.TabIndex = 345497
        Me.btndelete.Text = "Delete"
        Me.btndelete.UseVisualStyleBackColor = False
        '
        'chkhide
        '
        Me.chkhide.AutoSize = True
        Me.chkhide.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkhide.Location = New System.Drawing.Point(222, 239)
        Me.chkhide.Name = "chkhide"
        Me.chkhide.Size = New System.Drawing.Size(56, 19)
        Me.chkhide.TabIndex = 345498
        Me.chkhide.Text = "Hide"
        Me.chkhide.UseVisualStyleBackColor = True
        '
        'txtsylabus
        '
        Me.txtsylabus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtsylabus.Location = New System.Drawing.Point(93, 264)
        Me.txtsylabus.MaxLength = 250
        Me.txtsylabus.Multiline = True
        Me.txtsylabus.Name = "txtsylabus"
        Me.txtsylabus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtsylabus.Size = New System.Drawing.Size(420, 65)
        Me.txtsylabus.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(22, 267)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 13)
        Me.Label7.TabIndex = 345500
        Me.Label7.Text = "Sylabus"
        '
        'CourseItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.CancelButton = Me.BtnExit
        Me.ClientSize = New System.Drawing.Size(757, 543)
        Me.Controls.Add(Me.txtsylabus)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.chkhide)
        Me.Controls.Add(Me.btndelete)
        Me.Controls.Add(Me.btnclear)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txttotalfees)
        Me.Controls.Add(Me.txttotalTaxfees)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtduration)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbtax)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.grdItem)
        Me.Controls.Add(Me.numunitprice)
        Me.Controls.Add(Me.txtpriceWtax)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnUpdate)
        Me.Controls.Add(Me.lblCode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtTrDescr)
        Me.Controls.Add(Me.Label34)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "CourseItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Quick Item"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtpriceWtax As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents numunitprice As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents grdItem As System.Windows.Forms.DataGridView
    Friend WithEvents cmbtax As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtduration As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txttotalfees As System.Windows.Forms.TextBox
    Friend WithEvents txttotalTaxfees As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents btndelete As System.Windows.Forms.Button
    Friend WithEvents chkhide As System.Windows.Forms.CheckBox
    Friend WithEvents txtsylabus As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
