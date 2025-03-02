<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SendSMSFrm
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
        Me.txtcontent = New System.Windows.Forms.TextBox
        Me.grdvoucher = New System.Windows.Forms.DataGridView
        Me.cmbcategory = New System.Windows.Forms.ComboBox
        Me.btnLoad = New System.Windows.Forms.Button
        Me.btnsend = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnaddtemplate = New System.Windows.Forms.Button
        Me.lblsmsremaining = New System.Windows.Forms.Label
        Me.lblsmsTobesend = New System.Windows.Forms.Label
        Me.cmbformat = New System.Windows.Forms.ComboBox
        Me.btnexit = New System.Windows.Forms.Button
        Me.chkselectall = New System.Windows.Forms.CheckBox
        Me.lblstaus = New System.Windows.Forms.Label
        Me.lblcharector = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.cmbitmOrder = New System.Windows.Forms.ComboBox
        Me.txtitemSearch = New System.Windows.Forms.TextBox
        Me.chkitemSearchOnly = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnadd = New System.Windows.Forms.Button
        Me.txtphone = New System.Windows.Forms.TextBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.bntloadtr = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cldrEnddate = New System.Windows.Forms.DateTimePicker
        Me.cldrStartDate = New System.Windows.Forms.DateTimePicker
        Me.grdlist = New System.Windows.Forms.DataGridView
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtcontent
        '
        Me.txtcontent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtcontent.Location = New System.Drawing.Point(438, 79)
        Me.txtcontent.MaxLength = 120
        Me.txtcontent.Multiline = True
        Me.txtcontent.Name = "txtcontent"
        Me.txtcontent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtcontent.Size = New System.Drawing.Size(306, 180)
        Me.txtcontent.TabIndex = 1
        '
        'grdvoucher
        '
        Me.grdvoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdvoucher.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdvoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdvoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdvoucher.GridColor = System.Drawing.Color.Gainsboro
        Me.grdvoucher.Location = New System.Drawing.Point(6, 79)
        Me.grdvoucher.Name = "grdvoucher"
        Me.grdvoucher.Size = New System.Drawing.Size(423, 305)
        Me.grdvoucher.TabIndex = 345466
        '
        'cmbcategory
        '
        Me.cmbcategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcategory.FormattingEnabled = True
        Me.cmbcategory.Items.AddRange(New Object() {"Customer", "Supplier"})
        Me.cmbcategory.Location = New System.Drawing.Point(6, 7)
        Me.cmbcategory.Name = "cmbcategory"
        Me.cmbcategory.Size = New System.Drawing.Size(156, 21)
        Me.cmbcategory.TabIndex = 345492
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.BackColor = System.Drawing.Color.SteelBlue
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoad.ForeColor = System.Drawing.Color.White
        Me.btnLoad.Location = New System.Drawing.Point(344, 4)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(85, 35)
        Me.btnLoad.TabIndex = 345493
        Me.btnLoad.Text = "&Load"
        Me.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLoad.UseVisualStyleBackColor = False
        '
        'btnsend
        '
        Me.btnsend.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnsend.BackColor = System.Drawing.Color.SteelBlue
        Me.btnsend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsend.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsend.ForeColor = System.Drawing.Color.White
        Me.btnsend.Location = New System.Drawing.Point(661, 268)
        Me.btnsend.Name = "btnsend"
        Me.btnsend.Size = New System.Drawing.Size(85, 35)
        Me.btnsend.TabIndex = 345494
        Me.btnsend.Text = "&Send"
        Me.btnsend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsend.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(438, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 345496
        Me.Label1.Text = "Template"
        '
        'btnaddtemplate
        '
        Me.btnaddtemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnaddtemplate.BackColor = System.Drawing.Color.SteelBlue
        Me.btnaddtemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnaddtemplate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnaddtemplate.ForeColor = System.Drawing.Color.White
        Me.btnaddtemplate.Location = New System.Drawing.Point(661, 21)
        Me.btnaddtemplate.Name = "btnaddtemplate"
        Me.btnaddtemplate.Size = New System.Drawing.Size(85, 35)
        Me.btnaddtemplate.TabIndex = 345497
        Me.btnaddtemplate.Text = "Save"
        Me.btnaddtemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnaddtemplate.UseVisualStyleBackColor = False
        '
        'lblsmsremaining
        '
        Me.lblsmsremaining.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblsmsremaining.AutoSize = True
        Me.lblsmsremaining.BackColor = System.Drawing.Color.Transparent
        Me.lblsmsremaining.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsmsremaining.ForeColor = System.Drawing.Color.Green
        Me.lblsmsremaining.Location = New System.Drawing.Point(438, 308)
        Me.lblsmsremaining.Name = "lblsmsremaining"
        Me.lblsmsremaining.Size = New System.Drawing.Size(94, 16)
        Me.lblsmsremaining.TabIndex = 345498
        Me.lblsmsremaining.Text = "Remaining : "
        '
        'lblsmsTobesend
        '
        Me.lblsmsTobesend.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblsmsTobesend.AutoSize = True
        Me.lblsmsTobesend.BackColor = System.Drawing.Color.Transparent
        Me.lblsmsTobesend.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsmsTobesend.ForeColor = System.Drawing.Color.Blue
        Me.lblsmsTobesend.Location = New System.Drawing.Point(438, 335)
        Me.lblsmsTobesend.Name = "lblsmsTobesend"
        Me.lblsmsTobesend.Size = New System.Drawing.Size(121, 16)
        Me.lblsmsTobesend.TabIndex = 345499
        Me.lblsmsTobesend.Text = "Selected Count :"
        '
        'cmbformat
        '
        Me.cmbformat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbformat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbformat.FormattingEnabled = True
        Me.cmbformat.Location = New System.Drawing.Point(442, 29)
        Me.cmbformat.Name = "cmbformat"
        Me.cmbformat.Size = New System.Drawing.Size(214, 21)
        Me.cmbformat.TabIndex = 345500
        '
        'btnexit
        '
        Me.btnexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.Location = New System.Drawing.Point(680, 459)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(85, 35)
        Me.btnexit.TabIndex = 345501
        Me.btnexit.Text = "Exit"
        Me.btnexit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'chkselectall
        '
        Me.chkselectall.AutoSize = True
        Me.chkselectall.BackColor = System.Drawing.Color.Transparent
        Me.chkselectall.Location = New System.Drawing.Point(170, 10)
        Me.chkselectall.Name = "chkselectall"
        Me.chkselectall.Size = New System.Drawing.Size(70, 17)
        Me.chkselectall.TabIndex = 345502
        Me.chkselectall.Text = "Select All"
        Me.chkselectall.UseVisualStyleBackColor = False
        '
        'lblstaus
        '
        Me.lblstaus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblstaus.AutoSize = True
        Me.lblstaus.BackColor = System.Drawing.Color.Transparent
        Me.lblstaus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstaus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblstaus.Location = New System.Drawing.Point(435, 273)
        Me.lblstaus.Name = "lblstaus"
        Me.lblstaus.Size = New System.Drawing.Size(51, 16)
        Me.lblstaus.TabIndex = 345504
        Me.lblstaus.Text = "Status"
        '
        'lblcharector
        '
        Me.lblcharector.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblcharector.AutoSize = True
        Me.lblcharector.BackColor = System.Drawing.Color.Transparent
        Me.lblcharector.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcharector.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblcharector.Location = New System.Drawing.Point(438, 60)
        Me.lblcharector.Name = "lblcharector"
        Me.lblcharector.Size = New System.Drawing.Size(41, 15)
        Me.lblcharector.TabIndex = 345505
        Me.lblcharector.Text = "Status"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(8, 8)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(765, 445)
        Me.TabControl1.TabIndex = 345506
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.cmbitmOrder)
        Me.TabPage1.Controls.Add(Me.txtitemSearch)
        Me.TabPage1.Controls.Add(Me.chkitemSearchOnly)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.btnadd)
        Me.TabPage1.Controls.Add(Me.txtphone)
        Me.TabPage1.Controls.Add(Me.cmbcategory)
        Me.TabPage1.Controls.Add(Me.grdvoucher)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.lblsmsTobesend)
        Me.TabPage1.Controls.Add(Me.btnLoad)
        Me.TabPage1.Controls.Add(Me.txtcontent)
        Me.TabPage1.Controls.Add(Me.chkselectall)
        Me.TabPage1.Controls.Add(Me.cmbformat)
        Me.TabPage1.Controls.Add(Me.lblcharector)
        Me.TabPage1.Controls.Add(Me.lblsmsremaining)
        Me.TabPage1.Controls.Add(Me.btnsend)
        Me.TabPage1.Controls.Add(Me.btnaddtemplate)
        Me.TabPage1.Controls.Add(Me.lblstaus)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(757, 419)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Compose"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'cmbitmOrder
        '
        Me.cmbitmOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbitmOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbitmOrder.FormattingEnabled = True
        Me.cmbitmOrder.Location = New System.Drawing.Point(8, 390)
        Me.cmbitmOrder.Name = "cmbitmOrder"
        Me.cmbitmOrder.Size = New System.Drawing.Size(143, 21)
        Me.cmbitmOrder.TabIndex = 345511
        '
        'txtitemSearch
        '
        Me.txtitemSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtitemSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtitemSearch.Location = New System.Drawing.Point(157, 390)
        Me.txtitemSearch.Name = "txtitemSearch"
        Me.txtitemSearch.Size = New System.Drawing.Size(191, 20)
        Me.txtitemSearch.TabIndex = 345510
        '
        'chkitemSearchOnly
        '
        Me.chkitemSearchOnly.AutoSize = True
        Me.chkitemSearchOnly.BackColor = System.Drawing.Color.Transparent
        Me.chkitemSearchOnly.ForeColor = System.Drawing.Color.Black
        Me.chkitemSearchOnly.Location = New System.Drawing.Point(354, 391)
        Me.chkitemSearchOnly.Name = "chkitemSearchOnly"
        Me.chkitemSearchOnly.Size = New System.Drawing.Size(143, 17)
        Me.chkitemSearchOnly.TabIndex = 345512
        Me.chkitemSearchOnly.Text = "Search 'Starts With' Only"
        Me.chkitemSearchOnly.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 345509
        Me.Label2.Text = "Phone Number"
        '
        'btnadd
        '
        Me.btnadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnadd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.ForeColor = System.Drawing.Color.White
        Me.btnadd.Location = New System.Drawing.Point(277, 49)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(52, 26)
        Me.btnadd.TabIndex = 345508
        Me.btnadd.Text = "Add"
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'txtphone
        '
        Me.txtphone.Location = New System.Drawing.Point(90, 53)
        Me.txtphone.MaxLength = 10
        Me.txtphone.Name = "txtphone"
        Me.txtphone.Size = New System.Drawing.Size(181, 20)
        Me.txtphone.TabIndex = 345507
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.bntloadtr)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Controls.Add(Me.grdlist)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(757, 419)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Report"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'bntloadtr
        '
        Me.bntloadtr.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bntloadtr.BackColor = System.Drawing.Color.SteelBlue
        Me.bntloadtr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bntloadtr.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bntloadtr.ForeColor = System.Drawing.Color.White
        Me.bntloadtr.Location = New System.Drawing.Point(136, 73)
        Me.bntloadtr.Name = "bntloadtr"
        Me.bntloadtr.Size = New System.Drawing.Size(85, 35)
        Me.bntloadtr.TabIndex = 345502
        Me.bntloadtr.Text = "Load"
        Me.bntloadtr.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.bntloadtr.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cldrEnddate)
        Me.GroupBox1.Controls.Add(Me.cldrStartDate)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 16)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(215, 51)
        Me.GroupBox1.TabIndex = 345468
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Parameter"
        '
        'cldrEnddate
        '
        Me.cldrEnddate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrEnddate.Location = New System.Drawing.Point(110, 22)
        Me.cldrEnddate.Name = "cldrEnddate"
        Me.cldrEnddate.Size = New System.Drawing.Size(96, 20)
        Me.cldrEnddate.TabIndex = 345395
        Me.cldrEnddate.TabStop = False
        '
        'cldrStartDate
        '
        Me.cldrStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrStartDate.Location = New System.Drawing.Point(9, 22)
        Me.cldrStartDate.Name = "cldrStartDate"
        Me.cldrStartDate.Size = New System.Drawing.Size(95, 20)
        Me.cldrStartDate.TabIndex = 345393
        Me.cldrStartDate.TabStop = False
        '
        'grdlist
        '
        Me.grdlist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdlist.BackgroundColor = System.Drawing.Color.Ivory
        Me.grdlist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdlist.GridColor = System.Drawing.Color.Gainsboro
        Me.grdlist.Location = New System.Drawing.Point(227, 16)
        Me.grdlist.Name = "grdlist"
        Me.grdlist.Size = New System.Drawing.Size(524, 373)
        Me.grdlist.TabIndex = 345467
        '
        'SendSMSFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(777, 506)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnexit)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SendSMSFrm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Send SMS "
        CType(Me.grdvoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grdlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtcontent As System.Windows.Forms.TextBox
    Friend WithEvents grdvoucher As System.Windows.Forms.DataGridView
    Friend WithEvents cmbcategory As System.Windows.Forms.ComboBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnsend As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnaddtemplate As System.Windows.Forms.Button
    Friend WithEvents lblsmsremaining As System.Windows.Forms.Label
    Friend WithEvents lblsmsTobesend As System.Windows.Forms.Label
    Friend WithEvents cmbformat As System.Windows.Forms.ComboBox
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents chkselectall As System.Windows.Forms.CheckBox
    Friend WithEvents lblstaus As System.Windows.Forms.Label
    Friend WithEvents lblcharector As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents grdlist As System.Windows.Forms.DataGridView
    Friend WithEvents bntloadtr As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cldrEnddate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cldrStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents txtphone As System.Windows.Forms.TextBox
    Friend WithEvents cmbitmOrder As System.Windows.Forms.ComboBox
    Friend WithEvents txtitemSearch As System.Windows.Forms.TextBox
    Friend WithEvents chkitemSearchOnly As System.Windows.Forms.CheckBox
End Class
