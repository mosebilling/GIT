<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserSettingsFrm
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
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.btnBrowseDocFolder = New System.Windows.Forms.Button
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label10 = New System.Windows.Forms.Label
        Me.cmbType = New System.Windows.Forms.ComboBox
        Me.lstUser = New System.Windows.Forms.ListBox
        Me.cmdmodi = New System.Windows.Forms.Button
        Me.txtDocumentPath = New System.Windows.Forms.TextBox
        Me.cmdadd = New System.Windows.Forms.Button
        Me.Label20 = New System.Windows.Forms.Label
        Me.cmdremove = New System.Windows.Forms.Button
        Me.cmdCreate = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtsalesdiscount = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.plrest = New System.Windows.Forms.Panel
        Me.chkReception = New System.Windows.Forms.CheckBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtusercode = New System.Windows.Forms.TextBox
        Me.rdouser = New System.Windows.Forms.RadioButton
        Me.cmbcounter = New System.Windows.Forms.ComboBox
        Me.rdomaster = New System.Windows.Forms.RadioButton
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbLocation = New System.Windows.Forms.ComboBox
        Me.chklst = New System.Windows.Forms.CheckedListBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmbbranch = New System.Windows.Forms.ComboBox
        Me.txtretype = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtpassword = New System.Windows.Forms.TextBox
        Me.txtUsername = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbuser = New System.Windows.Forms.ComboBox
        Me.btnpaste = New System.Windows.Forms.Button
        Me.btnCopy = New System.Windows.Forms.Button
        Me.TvPermission = New System.Windows.Forms.TreeView
        Me.cmdApply = New System.Windows.Forms.Button
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label26 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.cmdprotect = New System.Windows.Forms.Button
        Me.cmdexit = New System.Windows.Forms.Button
        Me.lblstatus = New System.Windows.Forms.Label
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.plrest.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 47)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(672, 379)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btnBrowseDocFolder)
        Me.TabPage1.Controls.Add(Me.Panel3)
        Me.TabPage1.Controls.Add(Me.cmdmodi)
        Me.TabPage1.Controls.Add(Me.txtDocumentPath)
        Me.TabPage1.Controls.Add(Me.cmdadd)
        Me.TabPage1.Controls.Add(Me.Label20)
        Me.TabPage1.Controls.Add(Me.cmdremove)
        Me.TabPage1.Controls.Add(Me.cmdCreate)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(664, 353)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "User "
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btnBrowseDocFolder
        '
        Me.btnBrowseDocFolder.BackColor = System.Drawing.Color.SteelBlue
        Me.btnBrowseDocFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseDocFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseDocFolder.ForeColor = System.Drawing.Color.White
        Me.btnBrowseDocFolder.Location = New System.Drawing.Point(582, 324)
        Me.btnBrowseDocFolder.Name = "btnBrowseDocFolder"
        Me.btnBrowseDocFolder.Size = New System.Drawing.Size(72, 25)
        Me.btnBrowseDocFolder.TabIndex = 9
        Me.btnBrowseDocFolder.Text = "Choose"
        Me.btnBrowseDocFolder.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnBrowseDocFolder.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.cmbType)
        Me.Panel3.Controls.Add(Me.lstUser)
        Me.Panel3.Location = New System.Drawing.Point(445, 6)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(220, 304)
        Me.Panel3.TabIndex = 16
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(56, 8)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(52, 13)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "User type"
        '
        'cmbType
        '
        Me.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.Items.AddRange(New Object() {"Master", "User"})
        Me.cmbType.Location = New System.Drawing.Point(114, 5)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(95, 21)
        Me.cmbType.TabIndex = 0
        '
        'lstUser
        '
        Me.lstUser.FormattingEnabled = True
        Me.lstUser.Location = New System.Drawing.Point(10, 32)
        Me.lstUser.Name = "lstUser"
        Me.lstUser.Size = New System.Drawing.Size(199, 264)
        Me.lstUser.TabIndex = 14
        '
        'cmdmodi
        '
        Me.cmdmodi.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdmodi.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdmodi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdmodi.ForeColor = System.Drawing.Color.White
        Me.cmdmodi.Location = New System.Drawing.Point(367, 77)
        Me.cmdmodi.Name = "cmdmodi"
        Me.cmdmodi.Size = New System.Drawing.Size(78, 35)
        Me.cmdmodi.TabIndex = 13
        Me.cmdmodi.Text = "&Modify"
        Me.cmdmodi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdmodi.UseVisualStyleBackColor = False
        '
        'txtDocumentPath
        '
        Me.txtDocumentPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(253, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.txtDocumentPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocumentPath.Location = New System.Drawing.Point(131, 326)
        Me.txtDocumentPath.Name = "txtDocumentPath"
        Me.txtDocumentPath.ReadOnly = True
        Me.txtDocumentPath.Size = New System.Drawing.Size(451, 21)
        Me.txtDocumentPath.TabIndex = 8
        '
        'cmdadd
        '
        Me.cmdadd.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdadd.ForeColor = System.Drawing.Color.White
        Me.cmdadd.Location = New System.Drawing.Point(367, 42)
        Me.cmdadd.Name = "cmdadd"
        Me.cmdadd.Size = New System.Drawing.Size(78, 35)
        Me.cmdadd.TabIndex = 12
        Me.cmdadd.Text = "&Add"
        Me.cmdadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdadd.UseVisualStyleBackColor = False
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(4, 323)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(133, 23)
        Me.Label20.TabIndex = 10
        Me.Label20.Text = "Document Data Path"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdremove
        '
        Me.cmdremove.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdremove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdremove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdremove.ForeColor = System.Drawing.Color.White
        Me.cmdremove.Location = New System.Drawing.Point(367, 112)
        Me.cmdremove.Name = "cmdremove"
        Me.cmdremove.Size = New System.Drawing.Size(78, 35)
        Me.cmdremove.TabIndex = 11
        Me.cmdremove.Text = "Remove"
        Me.cmdremove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdremove.UseVisualStyleBackColor = False
        '
        'cmdCreate
        '
        Me.cmdCreate.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCreate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCreate.ForeColor = System.Drawing.Color.White
        Me.cmdCreate.Location = New System.Drawing.Point(367, 146)
        Me.cmdCreate.Name = "cmdCreate"
        Me.cmdCreate.Size = New System.Drawing.Size(78, 35)
        Me.cmdCreate.TabIndex = 4
        Me.cmdCreate.Text = "&Update"
        Me.cmdCreate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdCreate.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtsalesdiscount)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.plrest)
        Me.GroupBox1.Controls.Add(Me.rdouser)
        Me.GroupBox1.Controls.Add(Me.cmbcounter)
        Me.GroupBox1.Controls.Add(Me.rdomaster)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbLocation)
        Me.GroupBox1.Controls.Add(Me.chklst)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.cmbbranch)
        Me.GroupBox1.Controls.Add(Me.txtretype)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtpassword)
        Me.GroupBox1.Controls.Add(Me.txtUsername)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 36)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(357, 266)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "User details"
        '
        'txtsalesdiscount
        '
        Me.txtsalesdiscount.Location = New System.Drawing.Point(302, 155)
        Me.txtsalesdiscount.Name = "txtsalesdiscount"
        Me.txtsalesdiscount.Size = New System.Drawing.Size(48, 20)
        Me.txtsalesdiscount.TabIndex = 26
        Me.txtsalesdiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(196, 157)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(107, 13)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "Max Sales Dicount %"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(7, 154)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(44, 13)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "Counter"
        '
        'plrest
        '
        Me.plrest.Controls.Add(Me.chkReception)
        Me.plrest.Controls.Add(Me.Label6)
        Me.plrest.Controls.Add(Me.txtusercode)
        Me.plrest.Location = New System.Drawing.Point(5, 115)
        Me.plrest.Name = "plrest"
        Me.plrest.Size = New System.Drawing.Size(346, 33)
        Me.plrest.TabIndex = 21
        '
        'chkReception
        '
        Me.chkReception.AutoSize = True
        Me.chkReception.Location = New System.Drawing.Point(196, 10)
        Me.chkReception.Name = "chkReception"
        Me.chkReception.Size = New System.Drawing.Size(111, 17)
        Me.chkReception.TabIndex = 26
        Me.chkReception.Text = "Is Reception User"
        Me.chkReception.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "User Code"
        '
        'txtusercode
        '
        Me.txtusercode.Location = New System.Drawing.Point(95, 8)
        Me.txtusercode.Name = "txtusercode"
        Me.txtusercode.Size = New System.Drawing.Size(95, 20)
        Me.txtusercode.TabIndex = 21
        '
        'rdouser
        '
        Me.rdouser.AutoSize = True
        Me.rdouser.Location = New System.Drawing.Point(162, 18)
        Me.rdouser.Name = "rdouser"
        Me.rdouser.Size = New System.Drawing.Size(47, 17)
        Me.rdouser.TabIndex = 23
        Me.rdouser.Text = "User"
        Me.rdouser.UseVisualStyleBackColor = True
        '
        'cmbcounter
        '
        Me.cmbcounter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcounter.FormattingEnabled = True
        Me.cmbcounter.Items.AddRange(New Object() {"", "C1", "C2", "C3", "C4", "C5"})
        Me.cmbcounter.Location = New System.Drawing.Point(100, 154)
        Me.cmbcounter.Name = "cmbcounter"
        Me.cmbcounter.Size = New System.Drawing.Size(95, 21)
        Me.cmbcounter.TabIndex = 24
        '
        'rdomaster
        '
        Me.rdomaster.AutoSize = True
        Me.rdomaster.Location = New System.Drawing.Point(99, 18)
        Me.rdomaster.Name = "rdomaster"
        Me.rdomaster.Size = New System.Drawing.Size(57, 17)
        Me.rdomaster.TabIndex = 22
        Me.rdomaster.TabStop = True
        Me.rdomaster.Text = "Master"
        Me.rdomaster.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 184)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Branches"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 239)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Default Location"
        Me.Label2.Visible = False
        '
        'cmbLocation
        '
        Me.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Items.AddRange(New Object() {"Master", "User"})
        Me.cmbLocation.Location = New System.Drawing.Point(99, 239)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(74, 21)
        Me.cmbLocation.TabIndex = 17
        Me.cmbLocation.Visible = False
        '
        'chklst
        '
        Me.chklst.FormattingEnabled = True
        Me.chklst.Location = New System.Drawing.Point(99, 184)
        Me.chklst.Name = "chklst"
        Me.chklst.Size = New System.Drawing.Size(250, 49)
        Me.chklst.TabIndex = 16
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(190, 239)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(78, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Default Branch"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "User type"
        '
        'cmbbranch
        '
        Me.cmbbranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbbranch.FormattingEnabled = True
        Me.cmbbranch.Items.AddRange(New Object() {"Master", "User"})
        Me.cmbbranch.Location = New System.Drawing.Point(274, 239)
        Me.cmbbranch.Name = "cmbbranch"
        Me.cmbbranch.Size = New System.Drawing.Size(75, 21)
        Me.cmbbranch.TabIndex = 20
        '
        'txtretype
        '
        Me.txtretype.Location = New System.Drawing.Point(99, 92)
        Me.txtretype.Name = "txtretype"
        Me.txtretype.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtretype.Size = New System.Drawing.Size(250, 20)
        Me.txtretype.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "User Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 92)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Re type Password"
        '
        'txtpassword
        '
        Me.txtpassword.Location = New System.Drawing.Point(99, 66)
        Me.txtpassword.Name = "txtpassword"
        Me.txtpassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpassword.Size = New System.Drawing.Size(250, 20)
        Me.txtpassword.TabIndex = 2
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(99, 42)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(250, 20)
        Me.txtUsername.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Password"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Panel2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(664, 353)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Permission"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.cmbuser)
        Me.Panel2.Controls.Add(Me.btnpaste)
        Me.Panel2.Controls.Add(Me.btnCopy)
        Me.Panel2.Controls.Add(Me.TvPermission)
        Me.Panel2.Controls.Add(Me.cmdApply)
        Me.Panel2.Location = New System.Drawing.Point(8, 6)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(549, 341)
        Me.Panel2.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "User Name"
        '
        'cmbuser
        '
        Me.cmbuser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbuser.FormattingEnabled = True
        Me.cmbuser.Items.AddRange(New Object() {"Master", "User"})
        Me.cmbuser.Location = New System.Drawing.Point(79, 22)
        Me.cmbuser.Name = "cmbuser"
        Me.cmbuser.Size = New System.Drawing.Size(168, 21)
        Me.cmbuser.TabIndex = 17
        '
        'btnpaste
        '
        Me.btnpaste.BackColor = System.Drawing.Color.SteelBlue
        Me.btnpaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnpaste.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnpaste.ForeColor = System.Drawing.Color.White
        Me.btnpaste.Location = New System.Drawing.Point(449, 121)
        Me.btnpaste.Name = "btnpaste"
        Me.btnpaste.Size = New System.Drawing.Size(92, 35)
        Me.btnpaste.TabIndex = 12
        Me.btnpaste.Text = "&Paste Rights"
        Me.btnpaste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnpaste.UseVisualStyleBackColor = False
        '
        'btnCopy
        '
        Me.btnCopy.BackColor = System.Drawing.Color.SteelBlue
        Me.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCopy.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCopy.ForeColor = System.Drawing.Color.White
        Me.btnCopy.Location = New System.Drawing.Point(449, 85)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(92, 35)
        Me.btnCopy.TabIndex = 11
        Me.btnCopy.Text = "&Copy Rights"
        Me.btnCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCopy.UseVisualStyleBackColor = False
        '
        'TvPermission
        '
        Me.TvPermission.CheckBoxes = True
        Me.TvPermission.FullRowSelect = True
        Me.TvPermission.Location = New System.Drawing.Point(16, 49)
        Me.TvPermission.Name = "TvPermission"
        Me.TvPermission.Size = New System.Drawing.Size(427, 289)
        Me.TvPermission.TabIndex = 10
        '
        'cmdApply
        '
        Me.cmdApply.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdApply.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdApply.ForeColor = System.Drawing.Color.White
        Me.cmdApply.Location = New System.Drawing.Point(449, 49)
        Me.cmdApply.Name = "cmdApply"
        Me.cmdApply.Size = New System.Drawing.Size(92, 35)
        Me.cmdApply.TabIndex = 7
        Me.cmdApply.Text = "&Apply"
        Me.cmdApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdApply.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(695, 41)
        Me.Panel1.TabIndex = 345404
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Font = New System.Drawing.Font("Goudy Old Style", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(43, 9)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(232, 21)
        Me.Label26.TabIndex = 345459
        Me.Label26.Text = "USER/PERMISSION SETTINGS"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.user_search
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(8, 7)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(29, 26)
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'cmdprotect
        '
        Me.cmdprotect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdprotect.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdprotect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdprotect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdprotect.ForeColor = System.Drawing.Color.White
        Me.cmdprotect.Location = New System.Drawing.Point(501, 432)
        Me.cmdprotect.Name = "cmdprotect"
        Me.cmdprotect.Size = New System.Drawing.Size(90, 35)
        Me.cmdprotect.TabIndex = 15
        Me.cmdprotect.Text = "&Protect"
        Me.cmdprotect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdprotect.UseVisualStyleBackColor = False
        Me.cmdprotect.Visible = False
        '
        'cmdexit
        '
        Me.cmdexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdexit.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.White
        Me.cmdexit.Location = New System.Drawing.Point(594, 432)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(90, 35)
        Me.cmdexit.TabIndex = 1
        Me.cmdexit.Text = "Exit"
        Me.cmdexit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'lblstatus
        '
        Me.lblstatus.AutoSize = True
        Me.lblstatus.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblstatus.Location = New System.Drawing.Point(13, 429)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(71, 14)
        Me.lblstatus.TabIndex = 345406
        Me.lblstatus.Text = "User type"
        Me.lblstatus.Visible = False
        '
        'UserSettingsFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(695, 488)
        Me.Controls.Add(Me.lblstatus)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cmdprotect)
        Me.Controls.Add(Me.cmdexit)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UserSettingsFrm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.plrest.ResumeLayout(False)
        Me.plrest.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents txtpassword As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtretype As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdCreate As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdApply As System.Windows.Forms.Button
    Friend WithEvents cmdremove As System.Windows.Forms.Button
    Friend WithEvents TvPermission As System.Windows.Forms.TreeView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents cmdmodi As System.Windows.Forms.Button
    Friend WithEvents cmdadd As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents cmdprotect As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnpaste As System.Windows.Forms.Button
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox
    Friend WithEvents chklst As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbbranch As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lstUser As System.Windows.Forms.ListBox
    Friend WithEvents rdouser As System.Windows.Forms.RadioButton
    Friend WithEvents rdomaster As System.Windows.Forms.RadioButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents txtDocumentPath As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents btnBrowseDocFolder As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbuser As System.Windows.Forms.ComboBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblstatus As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtusercode As System.Windows.Forms.TextBox
    Friend WithEvents plrest As System.Windows.Forms.Panel
    Friend WithEvents chkReception As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbcounter As System.Windows.Forms.ComboBox
    Friend WithEvents txtsalesdiscount As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
End Class
