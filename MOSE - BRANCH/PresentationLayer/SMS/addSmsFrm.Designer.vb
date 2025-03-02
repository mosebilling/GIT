<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class addSmsFrm
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
        Me.btnadd = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtkey = New System.Windows.Forms.TextBox
        Me.txtapi = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtcount = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnadd
        '
        Me.btnadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnadd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.ForeColor = System.Drawing.Color.White
        Me.btnadd.Location = New System.Drawing.Point(14, 105)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(116, 35)
        Me.btnadd.TabIndex = 345498
        Me.btnadd.Text = "Integrate SMS"
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 13)
        Me.Label1.TabIndex = 345499
        Me.Label1.Text = "SMS Integration Key"
        '
        'txtkey
        '
        Me.txtkey.BackColor = System.Drawing.Color.White
        Me.txtkey.Location = New System.Drawing.Point(13, 34)
        Me.txtkey.Name = "txtkey"
        Me.txtkey.ReadOnly = True
        Me.txtkey.Size = New System.Drawing.Size(272, 20)
        Me.txtkey.TabIndex = 345500
        Me.txtkey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtapi
        '
        Me.txtapi.BackColor = System.Drawing.Color.White
        Me.txtapi.Location = New System.Drawing.Point(14, 79)
        Me.txtapi.Multiline = True
        Me.txtapi.Name = "txtapi"
        Me.txtapi.Size = New System.Drawing.Size(502, 20)
        Me.txtapi.TabIndex = 345502
        Me.txtapi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 345501
        Me.Label2.Text = "SMS API Key"
        '
        'txtcount
        '
        Me.txtcount.BackColor = System.Drawing.Color.White
        Me.txtcount.Location = New System.Drawing.Point(291, 34)
        Me.txtcount.Name = "txtcount"
        Me.txtcount.Size = New System.Drawing.Size(131, 20)
        Me.txtcount.TabIndex = 345503
        Me.txtcount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(288, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 345504
        Me.Label3.Text = "SMS Count"
        '
        'addSmsFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(521, 145)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtcount)
        Me.Controls.Add(Me.txtapi)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtkey)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnadd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "addSmsFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add SMS  Key"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtkey As System.Windows.Forms.TextBox
    Friend WithEvents txtapi As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtcount As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
