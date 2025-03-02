<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InvoicePercentage
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
        Me.numAmt = New System.Windows.Forms.TextBox
        Me.rdoamount = New System.Windows.Forms.RadioButton
        Me.rdoPer = New System.Windows.Forms.RadioButton
        Me.btnok = New System.Windows.Forms.Button
        Me.Label36 = New System.Windows.Forms.Label
        Me.lblInvoiced = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.lbltobeinvoiced = New System.Windows.Forms.Label
        Me.lblinvoice = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'numAmt
        '
        Me.numAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.numAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numAmt.Location = New System.Drawing.Point(115, 70)
        Me.numAmt.Name = "numAmt"
        Me.numAmt.Size = New System.Drawing.Size(125, 21)
        Me.numAmt.TabIndex = 13
        Me.numAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'rdoamount
        '
        Me.rdoamount.AutoSize = True
        Me.rdoamount.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoamount.Location = New System.Drawing.Point(4, 59)
        Me.rdoamount.Name = "rdoamount"
        Me.rdoamount.Size = New System.Drawing.Size(69, 17)
        Me.rdoamount.TabIndex = 345483
        Me.rdoamount.Text = "Amount"
        Me.rdoamount.UseVisualStyleBackColor = True
        '
        'rdoPer
        '
        Me.rdoPer.AutoSize = True
        Me.rdoPer.Checked = True
        Me.rdoPer.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoPer.Location = New System.Drawing.Point(4, 82)
        Me.rdoPer.Name = "rdoPer"
        Me.rdoPer.Size = New System.Drawing.Size(82, 17)
        Me.rdoPer.TabIndex = 345482
        Me.rdoPer.TabStop = True
        Me.rdoPer.Text = "% Of QTN"
        Me.rdoPer.UseVisualStyleBackColor = True
        '
        'btnok
        '
        Me.btnok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnok.BackColor = System.Drawing.Color.SteelBlue
        Me.btnok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnok.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnok.ForeColor = System.Drawing.Color.White
        Me.btnok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnok.Location = New System.Drawing.Point(191, 153)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(87, 32)
        Me.btnok.TabIndex = 345485
        Me.btnok.Text = "OK"
        Me.btnok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnok.UseVisualStyleBackColor = False
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(4, 33)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(92, 13)
        Me.Label36.TabIndex = 345488
        Me.Label36.Text = "To be Invoiced"
        '
        'lblInvoiced
        '
        Me.lblInvoiced.BackColor = System.Drawing.Color.Transparent
        Me.lblInvoiced.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInvoiced.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblInvoiced.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiced.Location = New System.Drawing.Point(100, 105)
        Me.lblInvoiced.Name = "lblInvoiced"
        Me.lblInvoiced.Size = New System.Drawing.Size(154, 21)
        Me.lblInvoiced.TabIndex = 345489
        Me.lblInvoiced.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(6, 105)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(51, 13)
        Me.Label22.TabIndex = 345486
        Me.Label22.Text = "Amount"
        '
        'lbltobeinvoiced
        '
        Me.lbltobeinvoiced.BackColor = System.Drawing.Color.Transparent
        Me.lbltobeinvoiced.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbltobeinvoiced.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lbltobeinvoiced.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltobeinvoiced.Location = New System.Drawing.Point(100, 33)
        Me.lbltobeinvoiced.Name = "lbltobeinvoiced"
        Me.lbltobeinvoiced.Size = New System.Drawing.Size(154, 21)
        Me.lbltobeinvoiced.TabIndex = 345487
        Me.lbltobeinvoiced.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblinvoice
        '
        Me.lblinvoice.BackColor = System.Drawing.Color.Transparent
        Me.lblinvoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblinvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblinvoice.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblinvoice.Location = New System.Drawing.Point(100, 9)
        Me.lblinvoice.Name = "lblinvoice"
        Me.lblinvoice.Size = New System.Drawing.Size(154, 21)
        Me.lblinvoice.TabIndex = 345491
        Me.lblinvoice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 345490
        Me.Label2.Text = "Inv. Amount"
        '
        'InvoicePercentage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(280, 189)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblinvoice)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.lblInvoiced)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.lbltobeinvoiced)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.rdoamount)
        Me.Controls.Add(Me.rdoPer)
        Me.Controls.Add(Me.numAmt)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "InvoicePercentage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "InvoicePercentage"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents numAmt As System.Windows.Forms.TextBox
    Friend WithEvents rdoamount As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPer As System.Windows.Forms.RadioButton
    Friend WithEvents btnok As System.Windows.Forms.Button
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents lblInvoiced As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lbltobeinvoiced As System.Windows.Forms.Label
    Friend WithEvents lblinvoice As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
