<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptToXmlFrm
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
        Me.btnconvert = New System.Windows.Forms.Button
        Me.txtpath = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnbrowse = New System.Windows.Forms.Button
        Me.opfSelectFile = New System.Windows.Forms.OpenFileDialog
        Me.btnexit = New System.Windows.Forms.Button
        Me.lblfilename = New System.Windows.Forms.Label
        Me.chkxmltorpt = New System.Windows.Forms.CheckBox
        Me.chkfiles = New System.Windows.Forms.CheckedListBox
        Me.chkeselectall = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'btnconvert
        '
        Me.btnconvert.Location = New System.Drawing.Point(282, 333)
        Me.btnconvert.Name = "btnconvert"
        Me.btnconvert.Size = New System.Drawing.Size(71, 27)
        Me.btnconvert.TabIndex = 0
        Me.btnconvert.Text = "Convert"
        Me.btnconvert.UseVisualStyleBackColor = True
        '
        'txtpath
        '
        Me.txtpath.Location = New System.Drawing.Point(15, 25)
        Me.txtpath.Name = "txtpath"
        Me.txtpath.Size = New System.Drawing.Size(356, 20)
        Me.txtpath.TabIndex = 26
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 13)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Select Valid File Path"
        '
        'btnbrowse
        '
        Me.btnbrowse.Location = New System.Drawing.Point(377, 20)
        Me.btnbrowse.Name = "btnbrowse"
        Me.btnbrowse.Size = New System.Drawing.Size(53, 28)
        Me.btnbrowse.TabIndex = 24
        Me.btnbrowse.Text = "Browse"
        Me.btnbrowse.UseVisualStyleBackColor = True
        '
        'opfSelectFile
        '
        Me.opfSelectFile.FileName = "OpenFileDialog1"
        '
        'btnexit
        '
        Me.btnexit.Location = New System.Drawing.Point(359, 333)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(71, 27)
        Me.btnexit.TabIndex = 27
        Me.btnexit.Text = "Exit"
        Me.btnexit.UseVisualStyleBackColor = True
        '
        'lblfilename
        '
        Me.lblfilename.AutoSize = True
        Me.lblfilename.Location = New System.Drawing.Point(12, 62)
        Me.lblfilename.Name = "lblfilename"
        Me.lblfilename.Size = New System.Drawing.Size(54, 13)
        Me.lblfilename.TabIndex = 28
        Me.lblfilename.Text = "File Name"
        '
        'chkxmltorpt
        '
        Me.chkxmltorpt.AutoSize = True
        Me.chkxmltorpt.Location = New System.Drawing.Point(15, 345)
        Me.chkxmltorpt.Name = "chkxmltorpt"
        Me.chkxmltorpt.Size = New System.Drawing.Size(81, 17)
        Me.chkxmltorpt.TabIndex = 29
        Me.chkxmltorpt.Text = ".Xml to .Rpt"
        Me.chkxmltorpt.UseVisualStyleBackColor = True
        '
        'chkfiles
        '
        Me.chkfiles.CheckOnClick = True
        Me.chkfiles.FormattingEnabled = True
        Me.chkfiles.Location = New System.Drawing.Point(19, 86)
        Me.chkfiles.Name = "chkfiles"
        Me.chkfiles.Size = New System.Drawing.Size(368, 229)
        Me.chkfiles.TabIndex = 30
        '
        'chkeselectall
        '
        Me.chkeselectall.AutoSize = True
        Me.chkeselectall.Location = New System.Drawing.Point(317, 62)
        Me.chkeselectall.Name = "chkeselectall"
        Me.chkeselectall.Size = New System.Drawing.Size(70, 17)
        Me.chkeselectall.TabIndex = 31
        Me.chkeselectall.Text = "Select All"
        Me.chkeselectall.UseVisualStyleBackColor = True
        '
        'RptToXmlFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(442, 372)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkeselectall)
        Me.Controls.Add(Me.chkfiles)
        Me.Controls.Add(Me.chkxmltorpt)
        Me.Controls.Add(Me.lblfilename)
        Me.Controls.Add(Me.btnexit)
        Me.Controls.Add(Me.txtpath)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnbrowse)
        Me.Controls.Add(Me.btnconvert)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "RptToXmlFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ".Rpt To .Xml"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnconvert As System.Windows.Forms.Button
    Friend WithEvents txtpath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnbrowse As System.Windows.Forms.Button
    Friend WithEvents opfSelectFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnexit As System.Windows.Forms.Button
    Friend WithEvents lblfilename As System.Windows.Forms.Label
    Friend WithEvents chkxmltorpt As System.Windows.Forms.CheckBox
    Friend WithEvents chkfiles As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkeselectall As System.Windows.Forms.CheckBox
End Class
