<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddSerialnoFrm
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
        Me.grdVoucher = New System.Windows.Forms.DataGridView
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtserialno = New System.Windows.Forms.TextBox
        Me.btnrem = New System.Windows.Forms.Button
        Me.btnadd = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.cldrdate = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.chknowarrenty = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtserialNoII = New System.Windows.Forms.TextBox
        Me.lblserialType = New System.Windows.Forms.Label
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdVoucher
        '
        Me.grdVoucher.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdVoucher.BackgroundColor = System.Drawing.Color.White
        Me.grdVoucher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdVoucher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdVoucher.Location = New System.Drawing.Point(12, 77)
        Me.grdVoucher.Name = "grdVoucher"
        Me.grdVoucher.Size = New System.Drawing.Size(591, 315)
        Me.grdVoucher.TabIndex = 137
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(253, 30)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 15)
        Me.Label8.TabIndex = 139
        Me.Label8.Text = "IMEI No"
        '
        'txtserialno
        '
        Me.txtserialno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtserialno.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtserialno.Location = New System.Drawing.Point(324, 28)
        Me.txtserialno.MaxLength = 50
        Me.txtserialno.Name = "txtserialno"
        Me.txtserialno.Size = New System.Drawing.Size(279, 21)
        Me.txtserialno.TabIndex = 138
        '
        'btnrem
        '
        Me.btnrem.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrem.ForeColor = System.Drawing.Color.White
        Me.btnrem.Location = New System.Drawing.Point(14, 398)
        Me.btnrem.Name = "btnrem"
        Me.btnrem.Size = New System.Drawing.Size(55, 28)
        Me.btnrem.TabIndex = 141
        Me.btnrem.Text = "Rem"
        Me.btnrem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnrem.UseVisualStyleBackColor = False
        '
        'btnadd
        '
        Me.btnadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnadd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.ForeColor = System.Drawing.Color.White
        Me.btnadd.Location = New System.Drawing.Point(490, 398)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(55, 28)
        Me.btnadd.TabIndex = 140
        Me.btnadd.Text = "&Add"
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(548, 398)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(55, 28)
        Me.btnExit.TabIndex = 345440
        Me.btnExit.Text = "E&xit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'cldrdate
        '
        Me.cldrdate.CustomFormat = "dd/MMM/yyyy"
        Me.cldrdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.cldrdate.Location = New System.Drawing.Point(137, 28)
        Me.cldrdate.Name = "cldrdate"
        Me.cldrdate.Size = New System.Drawing.Size(107, 20)
        Me.cldrdate.TabIndex = 345441
        Me.cldrdate.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 15)
        Me.Label1.TabIndex = 345442
        Me.Label1.Text = "Warranty Expiry Date"
        '
        'chknowarrenty
        '
        Me.chknowarrenty.AutoSize = True
        Me.chknowarrenty.BackColor = System.Drawing.Color.Transparent
        Me.chknowarrenty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chknowarrenty.Location = New System.Drawing.Point(14, 50)
        Me.chknowarrenty.Name = "chknowarrenty"
        Me.chknowarrenty.Size = New System.Drawing.Size(153, 17)
        Me.chknowarrenty.TabIndex = 345444
        Me.chknowarrenty.Text = "01/01/1950 - No Warranty"
        Me.chknowarrenty.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(253, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 15)
        Me.Label2.TabIndex = 345446
        Me.Label2.Text = "IMEI No II"
        '
        'txtserialNoII
        '
        Me.txtserialNoII.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtserialNoII.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtserialNoII.Location = New System.Drawing.Point(324, 53)
        Me.txtserialNoII.MaxLength = 50
        Me.txtserialNoII.Name = "txtserialNoII"
        Me.txtserialNoII.Size = New System.Drawing.Size(279, 21)
        Me.txtserialNoII.TabIndex = 345445
        '
        'lblserialType
        '
        Me.lblserialType.AutoSize = True
        Me.lblserialType.BackColor = System.Drawing.Color.Transparent
        Me.lblserialType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblserialType.ForeColor = System.Drawing.Color.Green
        Me.lblserialType.Location = New System.Drawing.Point(12, 9)
        Me.lblserialType.Name = "lblserialType"
        Me.lblserialType.Size = New System.Drawing.Size(37, 15)
        Me.lblserialType.TabIndex = 345447
        Me.lblserialType.Text = "Type"
        '
        'AddSerialnoFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(615, 434)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblserialType)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtserialNoII)
        Me.Controls.Add(Me.chknowarrenty)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cldrdate)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnrem)
        Me.Controls.Add(Me.btnadd)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtserialno)
        Me.Controls.Add(Me.grdVoucher)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "AddSerialnoFrm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add IMEI Number"
        CType(Me.grdVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdVoucher As System.Windows.Forms.DataGridView
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtserialno As System.Windows.Forms.TextBox
    Friend WithEvents btnrem As System.Windows.Forms.Button
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents cldrdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chknowarrenty As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtserialNoII As System.Windows.Forms.TextBox
    Friend WithEvents lblserialType As System.Windows.Forms.Label
End Class
