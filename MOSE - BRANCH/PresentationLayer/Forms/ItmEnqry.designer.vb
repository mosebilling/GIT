<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItmEnqry
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
        Me.grdPack = New System.Windows.Forms.DataGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkSearchBycode = New System.Windows.Forms.CheckBox
        Me.chkfulltext = New System.Windows.Forms.CheckBox
        Me.cmbQIH = New System.Windows.Forms.ComboBox
        Me.chkSearch = New System.Windows.Forms.CheckBox
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.cmbSearch = New System.Windows.Forms.ComboBox
        Me.BtnSelect = New System.Windows.Forms.Button
        Me.BtnRefresh = New System.Windows.Forms.Button
        Me.BtnExit = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ldTimer = New System.Windows.Forms.Timer(Me.components)
        Me.btnCreate = New System.Windows.Forms.Button
        Me.grdsupersed = New System.Windows.Forms.DataGridView
        Me.grdLocation = New System.Windows.Forms.DataGridView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.grdPack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.grdsupersed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdPack
        '
        Me.grdPack.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdPack.BackgroundColor = System.Drawing.Color.White
        Me.grdPack.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdPack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPack.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.grdPack.Location = New System.Drawing.Point(0, 39)
        Me.grdPack.MultiSelect = False
        Me.grdPack.Name = "grdPack"
        Me.grdPack.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPack.Size = New System.Drawing.Size(872, 423)
        Me.grdPack.StandardTab = True
        Me.grdPack.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.Panel1.Controls.Add(Me.chkSearchBycode)
        Me.Panel1.Controls.Add(Me.chkfulltext)
        Me.Panel1.Controls.Add(Me.cmbQIH)
        Me.Panel1.Controls.Add(Me.chkSearch)
        Me.Panel1.Controls.Add(Me.txtSearch)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Controls.Add(Me.cmbSearch)
        Me.Panel1.Location = New System.Drawing.Point(103, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1102, 30)
        Me.Panel1.TabIndex = 7
        '
        'chkSearchBycode
        '
        Me.chkSearchBycode.AutoSize = True
        Me.chkSearchBycode.BackColor = System.Drawing.Color.Transparent
        Me.chkSearchBycode.ForeColor = System.Drawing.Color.Black
        Me.chkSearchBycode.Location = New System.Drawing.Point(808, 5)
        Me.chkSearchBycode.Name = "chkSearchBycode"
        Me.chkSearchBycode.Size = New System.Drawing.Size(103, 17)
        Me.chkSearchBycode.TabIndex = 107
        Me.chkSearchBycode.Text = "Search By Code"
        Me.chkSearchBycode.UseVisualStyleBackColor = False
        '
        'chkfulltext
        '
        Me.chkfulltext.AutoSize = True
        Me.chkfulltext.BackColor = System.Drawing.Color.Transparent
        Me.chkfulltext.ForeColor = System.Drawing.Color.Black
        Me.chkfulltext.Location = New System.Drawing.Point(699, 5)
        Me.chkfulltext.Name = "chkfulltext"
        Me.chkfulltext.Size = New System.Drawing.Size(103, 17)
        Me.chkfulltext.TabIndex = 106
        Me.chkfulltext.Text = "Search Full Text"
        Me.chkfulltext.UseVisualStyleBackColor = False
        '
        'cmbQIH
        '
        Me.cmbQIH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbQIH.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbQIH.FormattingEnabled = True
        Me.cmbQIH.Items.AddRange(New Object() {"ALL", "POSITIVE", "NEGATIVE", "ZERO", "ZERO+POSITIVE", "ZERO+NEGATIVE", "NON ZERO"})
        Me.cmbQIH.Location = New System.Drawing.Point(760, 22)
        Me.cmbQIH.Name = "cmbQIH"
        Me.cmbQIH.Size = New System.Drawing.Size(53, 21)
        Me.cmbQIH.TabIndex = 89
        Me.cmbQIH.Visible = False
        '
        'chkSearch
        '
        Me.chkSearch.AutoSize = True
        Me.chkSearch.BackColor = System.Drawing.Color.Transparent
        Me.chkSearch.ForeColor = System.Drawing.Color.Black
        Me.chkSearch.Location = New System.Drawing.Point(541, 6)
        Me.chkSearch.Name = "chkSearch"
        Me.chkSearch.Size = New System.Drawing.Size(143, 17)
        Me.chkSearch.TabIndex = 88
        Me.chkSearch.Text = "Search 'Starts With' Only"
        Me.chkSearch.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(251, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(225, 20)
        Me.txtSearch.TabIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.SteelBlue
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(478, 2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(61, 25)
        Me.btnSearch.TabIndex = 86
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'cmbSearch
        '
        Me.cmbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSearch.FormattingEnabled = True
        Me.cmbSearch.Location = New System.Drawing.Point(25, 3)
        Me.cmbSearch.Name = "cmbSearch"
        Me.cmbSearch.Size = New System.Drawing.Size(220, 21)
        Me.cmbSearch.TabIndex = 87
        '
        'BtnSelect
        '
        Me.BtnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelect.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnSelect.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelect.ForeColor = System.Drawing.Color.White
        Me.BtnSelect.Location = New System.Drawing.Point(957, 468)
        Me.BtnSelect.Name = "BtnSelect"
        Me.BtnSelect.Size = New System.Drawing.Size(82, 35)
        Me.BtnSelect.TabIndex = 30
        Me.BtnSelect.Text = "&Select"
        Me.BtnSelect.UseVisualStyleBackColor = False
        '
        'BtnRefresh
        '
        Me.BtnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnRefresh.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnRefresh.ForeColor = System.Drawing.Color.White
        Me.BtnRefresh.Location = New System.Drawing.Point(1040, 468)
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(82, 35)
        Me.BtnRefresh.TabIndex = 26
        Me.BtnRefresh.Text = "Refresh"
        Me.BtnRefresh.UseVisualStyleBackColor = False
        '
        'BtnExit
        '
        Me.BtnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExit.ForeColor = System.Drawing.Color.White
        Me.BtnExit.Location = New System.Drawing.Point(1123, 468)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(82, 35)
        Me.BtnExit.TabIndex = 29
        Me.BtnExit.Text = "Exit"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        '
        'ldTimer
        '
        '
        'btnCreate
        '
        Me.btnCreate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCreate.BackColor = System.Drawing.Color.SteelBlue
        Me.btnCreate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreate.ForeColor = System.Drawing.Color.White
        Me.btnCreate.Location = New System.Drawing.Point(840, 468)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(115, 35)
        Me.btnCreate.TabIndex = 345464
        Me.btnCreate.Text = "Create New Item"
        Me.btnCreate.UseVisualStyleBackColor = False
        '
        'grdsupersed
        '
        Me.grdsupersed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdsupersed.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdsupersed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdsupersed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdsupersed.Location = New System.Drawing.Point(878, 39)
        Me.grdsupersed.Name = "grdsupersed"
        Me.grdsupersed.Size = New System.Drawing.Size(327, 219)
        Me.grdsupersed.TabIndex = 345465
        '
        'grdLocation
        '
        Me.grdLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLocation.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdLocation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdLocation.Location = New System.Drawing.Point(3, 32)
        Me.grdLocation.Name = "grdLocation"
        Me.grdLocation.Size = New System.Drawing.Size(322, 150)
        Me.grdLocation.TabIndex = 345466
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.grdLocation)
        Me.Panel2.Location = New System.Drawing.Point(878, 261)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(327, 195)
        Me.Panel2.TabIndex = 345467
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 16)
        Me.Label1.TabIndex = 345467
        Me.Label1.Text = "Location QTY"
        '
        'Timer2
        '
        Me.Timer2.Interval = 200
        '
        'ItmEnqry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.CancelButton = Me.BtnExit
        Me.ClientSize = New System.Drawing.Size(1215, 506)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.grdsupersed)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.grdPack)
        Me.Controls.Add(Me.BtnSelect)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnRefresh)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "ItmEnqry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Item"
        CType(Me.grdPack, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.grdsupersed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdPack As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmbQIH As System.Windows.Forms.ComboBox
    Friend WithEvents chkSearch As System.Windows.Forms.CheckBox
    Friend WithEvents BtnSelect As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents BtnRefresh As System.Windows.Forms.Button
    Friend WithEvents BtnExit As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents cmbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents chkfulltext As System.Windows.Forms.CheckBox
    Friend WithEvents ldTimer As System.Windows.Forms.Timer
    Friend WithEvents btnCreate As System.Windows.Forms.Button
    Friend WithEvents grdsupersed As System.Windows.Forms.DataGridView
    Friend WithEvents grdLocation As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents chkSearchBycode As System.Windows.Forms.CheckBox
End Class
