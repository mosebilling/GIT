<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BarCodeFrm
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
        Me.btnclose = New System.Windows.Forms.Button
        Me.btnPrint = New System.Windows.Forms.Button
        Me.txtqty = New System.Windows.Forms.TextBox
        Me.lblitem = New System.Windows.Forms.Label
        Me.lblbarcode = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbprinter = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblprice = New System.Windows.Forms.Label
        Me.imgIDAutomation = New System.Windows.Forms.PictureBox
        Me.chkprintFromExt = New System.Windows.Forms.CheckBox
        Me.cmbformat = New System.Windows.Forms.ComboBox
        Me.dlgOpen = New System.Windows.Forms.OpenFileDialog
        Me.btnadd = New System.Windows.Forms.Button
        Me.btnremove = New System.Windows.Forms.Button
        Me.btndefault = New System.Windows.Forms.Button
        Me.chkIsTaxPrice = New System.Windows.Forms.CheckBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.lbltaxprice = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.dtpto = New System.Windows.Forms.DateTimePicker
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblmrp = New System.Windows.Forms.Label
        CType(Me.imgIDAutomation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclose.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.ForeColor = System.Drawing.Color.White
        Me.btnclose.Location = New System.Drawing.Point(381, 210)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(82, 35)
        Me.btnclose.TabIndex = 345391
        Me.btnclose.Text = "E&xit"
        Me.btnclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnclose.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.BackColor = System.Drawing.Color.SteelBlue
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Location = New System.Drawing.Point(296, 210)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(82, 35)
        Me.btnPrint.TabIndex = 345392
        Me.btnPrint.Text = "Apply"
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'txtqty
        '
        Me.txtqty.Location = New System.Drawing.Point(94, 133)
        Me.txtqty.Name = "txtqty"
        Me.txtqty.Size = New System.Drawing.Size(68, 20)
        Me.txtqty.TabIndex = 345394
        Me.txtqty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblitem
        '
        Me.lblitem.AutoSize = True
        Me.lblitem.BackColor = System.Drawing.Color.Transparent
        Me.lblitem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblitem.Location = New System.Drawing.Point(91, 22)
        Me.lblitem.Name = "lblitem"
        Me.lblitem.Size = New System.Drawing.Size(79, 13)
        Me.lblitem.TabIndex = 345395
        Me.lblitem.Text = "Item Name : "
        '
        'lblbarcode
        '
        Me.lblbarcode.AutoSize = True
        Me.lblbarcode.BackColor = System.Drawing.Color.Transparent
        Me.lblbarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblbarcode.Location = New System.Drawing.Point(91, 40)
        Me.lblbarcode.Name = "lblbarcode"
        Me.lblbarcode.Size = New System.Drawing.Size(71, 13)
        Me.lblbarcode.TabIndex = 345396
        Me.lblbarcode.Text = "Bar Code : "
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 136)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 345397
        Me.Label1.Text = "Copies : "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 345399
        Me.Label2.Text = "Bar Code : "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 345398
        Me.Label3.Text = "Item Name : "
        '
        'cmbprinter
        '
        Me.cmbprinter.FormattingEnabled = True
        Me.cmbprinter.Location = New System.Drawing.Point(296, 2)
        Me.cmbprinter.Name = "cmbprinter"
        Me.cmbprinter.Size = New System.Drawing.Size(170, 21)
        Me.cmbprinter.TabIndex = 345400
        Me.cmbprinter.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(15, 160)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 345401
        Me.Label4.Text = "Formats"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 13)
        Me.Label5.TabIndex = 345403
        Me.Label5.Text = "Price : "
        '
        'lblprice
        '
        Me.lblprice.AutoSize = True
        Me.lblprice.BackColor = System.Drawing.Color.Transparent
        Me.lblprice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblprice.Location = New System.Drawing.Point(91, 58)
        Me.lblprice.Name = "lblprice"
        Me.lblprice.Size = New System.Drawing.Size(36, 13)
        Me.lblprice.TabIndex = 345402
        Me.lblprice.Text = "Price"
        '
        'imgIDAutomation
        '
        Me.imgIDAutomation.BackColor = System.Drawing.Color.Transparent
        Me.imgIDAutomation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.imgIDAutomation.Location = New System.Drawing.Point(18, 203)
        Me.imgIDAutomation.Name = "imgIDAutomation"
        Me.imgIDAutomation.Size = New System.Drawing.Size(246, 68)
        Me.imgIDAutomation.TabIndex = 345404
        Me.imgIDAutomation.TabStop = False
        '
        'chkprintFromExt
        '
        Me.chkprintFromExt.AutoSize = True
        Me.chkprintFromExt.BackColor = System.Drawing.Color.Transparent
        Me.chkprintFromExt.Checked = True
        Me.chkprintFromExt.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkprintFromExt.Location = New System.Drawing.Point(181, 132)
        Me.chkprintFromExt.Name = "chkprintFromExt"
        Me.chkprintFromExt.Size = New System.Drawing.Size(126, 17)
        Me.chkprintFromExt.TabIndex = 345406
        Me.chkprintFromExt.Text = "Print from Bar Tender"
        Me.chkprintFromExt.UseVisualStyleBackColor = False
        '
        'cmbformat
        '
        Me.cmbformat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbformat.FormattingEnabled = True
        Me.cmbformat.Location = New System.Drawing.Point(94, 160)
        Me.cmbformat.Name = "cmbformat"
        Me.cmbformat.Size = New System.Drawing.Size(170, 21)
        Me.cmbformat.TabIndex = 345407
        '
        'dlgOpen
        '
        Me.dlgOpen.FileName = "OpenFileDialog1"
        '
        'btnadd
        '
        Me.btnadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnadd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.ForeColor = System.Drawing.Color.White
        Me.btnadd.Location = New System.Drawing.Point(381, 158)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(82, 35)
        Me.btnadd.TabIndex = 345408
        Me.btnadd.Text = "Add Format"
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'btnremove
        '
        Me.btnremove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnremove.BackColor = System.Drawing.Color.SteelBlue
        Me.btnremove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnremove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnremove.ForeColor = System.Drawing.Color.White
        Me.btnremove.Location = New System.Drawing.Point(32, 210)
        Me.btnremove.Name = "btnremove"
        Me.btnremove.Size = New System.Drawing.Size(107, 35)
        Me.btnremove.TabIndex = 345409
        Me.btnremove.Text = "Remove Format"
        Me.btnremove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnremove.UseVisualStyleBackColor = False
        '
        'btndefault
        '
        Me.btndefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btndefault.BackColor = System.Drawing.Color.SteelBlue
        Me.btndefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndefault.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndefault.ForeColor = System.Drawing.Color.White
        Me.btndefault.Location = New System.Drawing.Point(276, 158)
        Me.btndefault.Name = "btndefault"
        Me.btndefault.Size = New System.Drawing.Size(102, 35)
        Me.btndefault.TabIndex = 345410
        Me.btndefault.Text = "Set as Default"
        Me.btndefault.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndefault.UseVisualStyleBackColor = False
        '
        'chkIsTaxPrice
        '
        Me.chkIsTaxPrice.AutoSize = True
        Me.chkIsTaxPrice.BackColor = System.Drawing.Color.Transparent
        Me.chkIsTaxPrice.Location = New System.Drawing.Point(313, 132)
        Me.chkIsTaxPrice.Name = "chkIsTaxPrice"
        Me.chkIsTaxPrice.Size = New System.Drawing.Size(131, 17)
        Me.chkIsTaxPrice.TabIndex = 345411
        Me.chkIsTaxPrice.Text = "Set Tax Price as Price"
        Me.chkIsTaxPrice.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 76)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 13)
        Me.Label6.TabIndex = 345413
        Me.Label6.Text = "Tax Price : "
        '
        'lbltaxprice
        '
        Me.lbltaxprice.AutoSize = True
        Me.lbltaxprice.BackColor = System.Drawing.Color.Transparent
        Me.lbltaxprice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltaxprice.Location = New System.Drawing.Point(91, 76)
        Me.lbltaxprice.Name = "lbltaxprice"
        Me.lbltaxprice.Size = New System.Drawing.Size(36, 13)
        Me.lbltaxprice.TabIndex = 345412
        Me.lbltaxprice.Text = "Price"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.dtpto)
        Me.GroupBox1.Controls.Add(Me.cldrStartDate)
        Me.GroupBox1.Location = New System.Drawing.Point(288, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(175, 91)
        Me.GroupBox1.TabIndex = 345414
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Packing Date"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 13)
        Me.Label7.TabIndex = 345428
        Me.Label7.Text = "Packing Date"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 51)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(61, 13)
        Me.Label8.TabIndex = 345427
        Me.Label8.Text = "Expiry Date"
        '
        'dtpto
        '
        Me.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpto.Location = New System.Drawing.Point(11, 67)
        Me.dtpto.Name = "dtpto"
        Me.dtpto.Size = New System.Drawing.Size(95, 20)
        Me.dtpto.TabIndex = 345396
        Me.dtpto.TabStop = False
        '
        'cldrStartDate
        '
        Me.cldrStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrStartDate.Location = New System.Drawing.Point(11, 30)
        Me.cldrStartDate.Name = "cldrStartDate"
        Me.cldrStartDate.Size = New System.Drawing.Size(95, 20)
        Me.cldrStartDate.TabIndex = 345395
        Me.cldrStartDate.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(12, 94)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 13)
        Me.Label9.TabIndex = 345416
        Me.Label9.Text = "MRP : "
        '
        'lblmrp
        '
        Me.lblmrp.AutoSize = True
        Me.lblmrp.BackColor = System.Drawing.Color.Transparent
        Me.lblmrp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmrp.Location = New System.Drawing.Point(91, 94)
        Me.lblmrp.Name = "lblmrp"
        Me.lblmrp.Size = New System.Drawing.Size(36, 13)
        Me.lblmrp.TabIndex = 345415
        Me.lblmrp.Text = "Price"
        '
        'BarCodeFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(475, 257)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblmrp)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lbltaxprice)
        Me.Controls.Add(Me.chkIsTaxPrice)
        Me.Controls.Add(Me.btndefault)
        Me.Controls.Add(Me.btnremove)
        Me.Controls.Add(Me.btnadd)
        Me.Controls.Add(Me.cmbformat)
        Me.Controls.Add(Me.chkprintFromExt)
        Me.Controls.Add(Me.imgIDAutomation)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblprice)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbprinter)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblbarcode)
        Me.Controls.Add(Me.lblitem)
        Me.Controls.Add(Me.txtqty)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnclose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "BarCodeFrm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Print Bar Code"
        CType(Me.imgIDAutomation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnclose As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents txtqty As System.Windows.Forms.TextBox
    Friend WithEvents lblitem As System.Windows.Forms.Label
    Friend WithEvents lblbarcode As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbprinter As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblprice As System.Windows.Forms.Label
    Friend WithEvents imgIDAutomation As System.Windows.Forms.PictureBox
    Friend WithEvents chkprintFromExt As System.Windows.Forms.CheckBox
    Friend WithEvents cmbformat As System.Windows.Forms.ComboBox
    Friend WithEvents dlgOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents btnremove As System.Windows.Forms.Button
    Friend WithEvents btndefault As System.Windows.Forms.Button
    Friend WithEvents chkIsTaxPrice As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lbltaxprice As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpto As System.Windows.Forms.DateTimePicker
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblmrp As System.Windows.Forms.Label
End Class
