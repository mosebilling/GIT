<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectBarcodeFormat
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
        Me.btndefault = New System.Windows.Forms.Button
        Me.btnadd = New System.Windows.Forms.Button
        Me.cmbformat = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnapply = New System.Windows.Forms.Button
        Me.dlgOpen = New System.Windows.Forms.OpenFileDialog
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtcopy = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'btndefault
        '
        Me.btndefault.BackColor = System.Drawing.Color.SteelBlue
        Me.btndefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndefault.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndefault.ForeColor = System.Drawing.Color.White
        Me.btndefault.Location = New System.Drawing.Point(8, 78)
        Me.btndefault.Name = "btndefault"
        Me.btndefault.Size = New System.Drawing.Size(102, 35)
        Me.btndefault.TabIndex = 345414
        Me.btndefault.Text = "Set as Default"
        Me.btndefault.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btndefault.UseVisualStyleBackColor = False
        '
        'btnadd
        '
        Me.btnadd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.ForeColor = System.Drawing.Color.White
        Me.btnadd.Location = New System.Drawing.Point(113, 78)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(82, 35)
        Me.btnadd.TabIndex = 345413
        Me.btnadd.Text = "Add Format"
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'cmbformat
        '
        Me.cmbformat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbformat.FormattingEnabled = True
        Me.cmbformat.Location = New System.Drawing.Point(12, 25)
        Me.cmbformat.Name = "cmbformat"
        Me.cmbformat.Size = New System.Drawing.Size(188, 21)
        Me.cmbformat.TabIndex = 345412
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 345411
        Me.Label4.Text = "Formats"
        '
        'btnapply
        '
        Me.btnapply.BackColor = System.Drawing.Color.SteelBlue
        Me.btnapply.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnapply.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnapply.ForeColor = System.Drawing.Color.White
        Me.btnapply.Location = New System.Drawing.Point(197, 78)
        Me.btnapply.Name = "btnapply"
        Me.btnapply.Size = New System.Drawing.Size(82, 35)
        Me.btnapply.TabIndex = 345415
        Me.btnapply.Text = "Apply"
        Me.btnapply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnapply.UseVisualStyleBackColor = False
        '
        'dlgOpen
        '
        Me.dlgOpen.FileName = "OpenFileDialog1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 345416
        Me.Label1.Text = "Copies"
        '
        'txtcopy
        '
        Me.txtcopy.Location = New System.Drawing.Point(100, 49)
        Me.txtcopy.Name = "txtcopy"
        Me.txtcopy.Size = New System.Drawing.Size(100, 20)
        Me.txtcopy.TabIndex = 345417
        Me.txtcopy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SelectBarcodeFormat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(291, 114)
        Me.Controls.Add(Me.txtcopy)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnapply)
        Me.Controls.Add(Me.btndefault)
        Me.Controls.Add(Me.btnadd)
        Me.Controls.Add(Me.cmbformat)
        Me.Controls.Add(Me.Label4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SelectBarcodeFormat"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Barcode "
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btndefault As System.Windows.Forms.Button
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents cmbformat As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnapply As System.Windows.Forms.Button
    Friend WithEvents dlgOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtcopy As System.Windows.Forms.TextBox
End Class
