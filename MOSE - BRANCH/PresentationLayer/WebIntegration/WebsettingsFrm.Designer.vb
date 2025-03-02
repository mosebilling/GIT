<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WebsettingsFrm
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtserver = New System.Windows.Forms.TextBox
        Me.txtuser = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtpassword = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtdb = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnexit = New System.Windows.Forms.Button
        Me.btnclear = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.btntest = New System.Windows.Forms.Button
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.chkremove = New System.Windows.Forms.CheckBox
        Me.btngst = New System.Windows.Forms.Button
        Me.btnproductlevel = New System.Windows.Forms.Button
        Me.btnproduct = New System.Windows.Forms.Button
        Me.lblwebintegrationid = New System.Windows.Forms.Label
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(6, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Server Name"
        '
        'txtserver
        '
        Me.txtserver.Location = New System.Drawing.Point(81, 18)
        Me.txtserver.Name = "txtserver"
        Me.txtserver.Size = New System.Drawing.Size(320, 20)
        Me.txtserver.TabIndex = 1
        Me.txtserver.Text = "199.79.62.22"
        '
        'txtuser
        '
        Me.txtuser.Location = New System.Drawing.Point(81, 44)
        Me.txtuser.Name = "txtuser"
        Me.txtuser.Size = New System.Drawing.Size(320, 20)
        Me.txtuser.TabIndex = 2
        Me.txtuser.Text = "vinvisin"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(6, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "User Name"
        '
        'txtpassword
        '
        Me.txtpassword.Location = New System.Drawing.Point(81, 70)
        Me.txtpassword.Name = "txtpassword"
        Me.txtpassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpassword.Size = New System.Drawing.Size(320, 20)
        Me.txtpassword.TabIndex = 3
        Me.txtpassword.Text = "0R7$8vzf"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(6, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Password"
        '
        'txtdb
        '
        Me.txtdb.Location = New System.Drawing.Point(81, 96)
        Me.txtdb.Name = "txtdb"
        Me.txtdb.Size = New System.Drawing.Size(320, 20)
        Me.txtdb.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(6, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Database"
        '
        'btnexit
        '
        Me.btnexit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnexit.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.ForeColor = System.Drawing.Color.White
        Me.btnexit.Location = New System.Drawing.Point(318, 122)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnexit.Size = New System.Drawing.Size(83, 35)
        Me.btnexit.TabIndex = 345433
        Me.btnexit.Tag = "56"
        Me.btnexit.Text = "Exit"
        Me.btnexit.UseVisualStyleBackColor = False
        '
        'btnclear
        '
        Me.btnclear.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclear.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.ForeColor = System.Drawing.Color.White
        Me.btnclear.Location = New System.Drawing.Point(229, 122)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnclear.Size = New System.Drawing.Size(83, 35)
        Me.btnclear.TabIndex = 345432
        Me.btnclear.Tag = "56"
        Me.btnclear.Text = "Clear"
        Me.btnclear.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(420, 198)
        Me.TabControl1.TabIndex = 345434
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btntest)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.btnexit)
        Me.TabPage1.Controls.Add(Me.txtserver)
        Me.TabPage1.Controls.Add(Me.btnclear)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.txtdb)
        Me.TabPage1.Controls.Add(Me.txtuser)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.txtpassword)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(412, 172)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Server Settings"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btntest
        '
        Me.btntest.BackColor = System.Drawing.Color.SteelBlue
        Me.btntest.Cursor = System.Windows.Forms.Cursors.Default
        Me.btntest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btntest.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btntest.ForeColor = System.Drawing.Color.White
        Me.btntest.Location = New System.Drawing.Point(9, 122)
        Me.btntest.Name = "btntest"
        Me.btntest.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btntest.Size = New System.Drawing.Size(125, 35)
        Me.btntest.TabIndex = 345434
        Me.btntest.Tag = "56"
        Me.btntest.Text = "Test Connection"
        Me.btntest.UseVisualStyleBackColor = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.chkremove)
        Me.TabPage2.Controls.Add(Me.btngst)
        Me.TabPage2.Controls.Add(Me.btnproductlevel)
        Me.TabPage2.Controls.Add(Me.btnproduct)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(412, 172)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Data Transfer"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'chkremove
        '
        Me.chkremove.AutoSize = True
        Me.chkremove.Location = New System.Drawing.Point(272, 16)
        Me.chkremove.Name = "chkremove"
        Me.chkremove.Size = New System.Drawing.Size(134, 17)
        Me.chkremove.TabIndex = 345435
        Me.chkremove.Text = "Remove before adding"
        Me.chkremove.UseVisualStyleBackColor = True
        '
        'btngst
        '
        Me.btngst.BackColor = System.Drawing.Color.SteelBlue
        Me.btngst.Cursor = System.Windows.Forms.Cursors.Default
        Me.btngst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btngst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btngst.ForeColor = System.Drawing.Color.White
        Me.btngst.Location = New System.Drawing.Point(3, 43)
        Me.btngst.Name = "btngst"
        Me.btngst.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btngst.Size = New System.Drawing.Size(165, 35)
        Me.btngst.TabIndex = 345435
        Me.btngst.Tag = "56"
        Me.btngst.Text = "GST Master"
        Me.btngst.UseVisualStyleBackColor = False
        '
        'btnproductlevel
        '
        Me.btnproductlevel.BackColor = System.Drawing.Color.SteelBlue
        Me.btnproductlevel.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnproductlevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnproductlevel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnproductlevel.ForeColor = System.Drawing.Color.White
        Me.btnproductlevel.Location = New System.Drawing.Point(3, 6)
        Me.btnproductlevel.Name = "btnproductlevel"
        Me.btnproductlevel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnproductlevel.Size = New System.Drawing.Size(165, 35)
        Me.btnproductlevel.TabIndex = 345434
        Me.btnproductlevel.Tag = "56"
        Me.btnproductlevel.Text = "Product Level && Group"
        Me.btnproductlevel.UseVisualStyleBackColor = False
        '
        'btnproduct
        '
        Me.btnproduct.BackColor = System.Drawing.Color.SteelBlue
        Me.btnproduct.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnproduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnproduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnproduct.ForeColor = System.Drawing.Color.White
        Me.btnproduct.Location = New System.Drawing.Point(3, 80)
        Me.btnproduct.Name = "btnproduct"
        Me.btnproduct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnproduct.Size = New System.Drawing.Size(165, 35)
        Me.btnproduct.TabIndex = 345433
        Me.btnproduct.Tag = "56"
        Me.btnproduct.Text = "Product Master"
        Me.btnproduct.UseVisualStyleBackColor = False
        '
        'lblwebintegrationid
        '
        Me.lblwebintegrationid.AutoSize = True
        Me.lblwebintegrationid.BackColor = System.Drawing.Color.Transparent
        Me.lblwebintegrationid.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblwebintegrationid.ForeColor = System.Drawing.Color.Green
        Me.lblwebintegrationid.Location = New System.Drawing.Point(12, 213)
        Me.lblwebintegrationid.Name = "lblwebintegrationid"
        Me.lblwebintegrationid.Size = New System.Drawing.Size(134, 15)
        Me.lblwebintegrationid.TabIndex = 345435
        Me.lblwebintegrationid.Text = "Web Integration ID :"
        Me.lblwebintegrationid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WebsettingsFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(458, 251)
        Me.Controls.Add(Me.lblwebintegrationid)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WebsettingsFrm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Web Settings"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtserver As System.Windows.Forms.TextBox
    Friend WithEvents txtuser As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtpassword As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtdb As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents btnexit As System.Windows.Forms.Button
    Public WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents btnproductlevel As System.Windows.Forms.Button
    Public WithEvents btnproduct As System.Windows.Forms.Button
    Public WithEvents btngst As System.Windows.Forms.Button
    Public WithEvents btntest As System.Windows.Forms.Button
    Friend WithEvents chkremove As System.Windows.Forms.CheckBox
    Friend WithEvents lblwebintegrationid As System.Windows.Forms.Label
End Class
