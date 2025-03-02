<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerFrm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtServer = New System.Windows.Forms.TextBox
        Me.cmbservertype = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmdcreateDb = New System.Windows.Forms.Button
        Me.btntestconnection = New System.Windows.Forms.Button
        Me.btnsearchDatabase = New System.Windows.Forms.Button
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtpassword = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtusername = New System.Windows.Forms.TextBox
        Me.cmbdatabases = New System.Windows.Forms.ComboBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtpath = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmdBrowse = New System.Windows.Forms.Button
        Me.Label11 = New System.Windows.Forms.Label
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label12 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lbldata = New System.Windows.Forms.Label
        Me.cmdok = New System.Windows.Forms.Button
        Me.cmdcancel = New System.Windows.Forms.Button
        Me.btnSql = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblstatus = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Server name"
        '
        'txtServer
        '
        Me.txtServer.Location = New System.Drawing.Point(79, 46)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(283, 20)
        Me.txtServer.TabIndex = 1
        '
        'cmbservertype
        '
        Me.cmbservertype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbservertype.FormattingEnabled = True
        Me.cmbservertype.Items.AddRange(New Object() {"<Select Server Type>", "Local system", "SQLEXPRESS", "Network server"})
        Me.cmbservertype.Location = New System.Drawing.Point(79, 19)
        Me.cmbservertype.Name = "cmbservertype"
        Me.cmbservertype.Size = New System.Drawing.Size(171, 21)
        Me.cmbservertype.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Server type"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(438, 29)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Specify server type and name which you use  to keep data"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmdcreateDb)
        Me.GroupBox1.Controls.Add(Me.btntestconnection)
        Me.GroupBox1.Controls.Add(Me.btnsearchDatabase)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.txtpassword)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtusername)
        Me.GroupBox1.Controls.Add(Me.cmbdatabases)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.cmbservertype)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtServer)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 96)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(516, 217)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Server type and name"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(79, 133)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(410, 13)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "To create new database,  Type your database name && click 'Create Database' Butto" & _
            "n"
        '
        'cmdcreateDb
        '
        Me.cmdcreateDb.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdcreateDb.FlatAppearance.BorderSize = 0
        Me.cmdcreateDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdcreateDb.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcreateDb.ForeColor = System.Drawing.Color.White
        Me.cmdcreateDb.Location = New System.Drawing.Point(299, 150)
        Me.cmdcreateDb.Name = "cmdcreateDb"
        Me.cmdcreateDb.Size = New System.Drawing.Size(137, 30)
        Me.cmdcreateDb.TabIndex = 28
        Me.cmdcreateDb.Text = "Create Database"
        Me.cmdcreateDb.UseVisualStyleBackColor = False
        '
        'btntestconnection
        '
        Me.btntestconnection.Enabled = False
        Me.btntestconnection.Image = Global.SMSMP.My.Resources.Resources.testconnection
        Me.btntestconnection.Location = New System.Drawing.Point(77, 179)
        Me.btntestconnection.Name = "btntestconnection"
        Me.btntestconnection.Size = New System.Drawing.Size(149, 30)
        Me.btntestconnection.TabIndex = 27
        Me.btntestconnection.Text = "Test Connection"
        Me.btntestconnection.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btntestconnection.UseVisualStyleBackColor = True
        '
        'btnsearchDatabase
        '
        Me.btnsearchDatabase.Image = Global.SMSMP.My.Resources.Resources.searchbtn
        Me.btnsearchDatabase.Location = New System.Drawing.Point(79, 95)
        Me.btnsearchDatabase.Name = "btnsearchDatabase"
        Me.btnsearchDatabase.Size = New System.Drawing.Size(149, 30)
        Me.btnsearchDatabase.TabIndex = 26
        Me.btnsearchDatabase.Text = "Search DataBase"
        Me.btnsearchDatabase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnsearchDatabase.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(151, 71)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(53, 13)
        Me.Label21.TabIndex = 23
        Me.Label21.Text = "Password"
        '
        'txtpassword
        '
        Me.txtpassword.Location = New System.Drawing.Point(210, 68)
        Me.txtpassword.Name = "txtpassword"
        Me.txtpassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpassword.Size = New System.Drawing.Size(152, 20)
        Me.txtpassword.TabIndex = 24
        Me.txtpassword.Text = "mosesft"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(9, 71)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(58, 13)
        Me.Label20.TabIndex = 21
        Me.Label20.Text = "User name"
        '
        'txtusername
        '
        Me.txtusername.Location = New System.Drawing.Point(79, 68)
        Me.txtusername.Name = "txtusername"
        Me.txtusername.Size = New System.Drawing.Size(69, 20)
        Me.txtusername.TabIndex = 22
        Me.txtusername.Text = "sa"
        '
        'cmbdatabases
        '
        Me.cmbdatabases.Enabled = False
        Me.cmbdatabases.FormattingEnabled = True
        Me.cmbdatabases.Location = New System.Drawing.Point(77, 154)
        Me.cmbdatabases.Name = "cmbdatabases"
        Me.cmbdatabases.Size = New System.Drawing.Size(216, 21)
        Me.cmbdatabases.TabIndex = 8
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(7, 157)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(56, 13)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "Database "
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(7, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(54, 13)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "Data path"
        '
        'txtpath
        '
        Me.txtpath.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpath.Location = New System.Drawing.Point(80, 17)
        Me.txtpath.Name = "txtpath"
        Me.txtpath.Size = New System.Drawing.Size(392, 24)
        Me.txtpath.TabIndex = 15
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.cmdBrowse)
        Me.GroupBox2.Controls.Add(Me.txtpath)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 358)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(514, 46)
        Me.GroupBox2.TabIndex = 16
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Set data path"
        '
        'cmdBrowse
        '
        Me.cmdBrowse.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdBrowse.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBrowse.ForeColor = System.Drawing.Color.White
        Me.cmdBrowse.Location = New System.Drawing.Point(471, 16)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(37, 25)
        Me.cmdBrowse.TabIndex = 19
        Me.cmdBrowse.Text = ">>"
        Me.cmdBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdBrowse.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(12, 338)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(214, 13)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Data path should be set to secure your data"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(528, 48)
        Me.Panel1.TabIndex = 24
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(4, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(116, 13)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "Server Settings.."
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.Link_icon
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Image = Global.SMSMP.My.Resources.Resources.paper_money_icon1
        Me.PictureBox1.Location = New System.Drawing.Point(475, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(46, 41)
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'lbldata
        '
        Me.lbldata.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbldata.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldata.Location = New System.Drawing.Point(7, 441)
        Me.lbldata.Name = "lbldata"
        Me.lbldata.Size = New System.Drawing.Size(509, 18)
        Me.lbldata.TabIndex = 25
        Me.lbldata.Text = "Note:"
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdok.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.White
        Me.cmdok.Location = New System.Drawing.Point(342, 408)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(88, 35)
        Me.cmdok.TabIndex = 2
        Me.cmdok.Text = "OK"
        Me.cmdok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdcancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdcancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdcancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.Color.White
        Me.cmdcancel.Location = New System.Drawing.Point(433, 408)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(88, 35)
        Me.cmdcancel.TabIndex = 3
        Me.cmdcancel.Text = "E&xit"
        Me.cmdcancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'btnSql
        '
        Me.btnSql.BackColor = System.Drawing.Color.SteelBlue
        Me.btnSql.FlatAppearance.BorderSize = 0
        Me.btnSql.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSql.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSql.ForeColor = System.Drawing.Color.White
        Me.btnSql.Location = New System.Drawing.Point(106, 54)
        Me.btnSql.Name = "btnSql"
        Me.btnSql.Size = New System.Drawing.Size(137, 36)
        Me.btnSql.TabIndex = 26
        Me.btnSql.Text = "SQL Server 2014"
        Me.btnSql.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 66)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 13)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Database Engine"
        '
        'lblstatus
        '
        Me.lblstatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblstatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.ForeColor = System.Drawing.Color.Green
        Me.lblstatus.Location = New System.Drawing.Point(12, 316)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(509, 18)
        Me.lblstatus.TabIndex = 29
        Me.lblstatus.Text = "status"
        '
        'ServerFrm
        '
        Me.AcceptButton = Me.cmdok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdcancel
        Me.ClientSize = New System.Drawing.Size(528, 470)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblstatus)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnSql)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdcancel)
        Me.Controls.Add(Me.cmdok)
        Me.Controls.Add(Me.lbldata)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "ServerFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents cmdok As System.Windows.Forms.Button
    Friend WithEvents cmdcancel As System.Windows.Forms.Button
    Friend WithEvents cmbservertype As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtpath As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmbdatabases As System.Windows.Forms.ComboBox
    Friend WithEvents lbldata As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtusername As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtpassword As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnsearchDatabase As System.Windows.Forms.Button
    Friend WithEvents btntestconnection As System.Windows.Forms.Button
    Friend WithEvents btnSql As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblstatus As System.Windows.Forms.Label
    Friend WithEvents cmdcreateDb As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
