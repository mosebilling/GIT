<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TransferiItemsFromExcel
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
        Me.lblrec = New System.Windows.Forms.Label
        Me.lblmodule = New System.Windows.Forms.Label
        Me.pb = New System.Windows.Forms.ProgressBar
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnok = New System.Windows.Forms.Button
        Me.txtpath = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnbrowse = New System.Windows.Forms.Button
        Me.opfSelectFile = New System.Windows.Forms.OpenFileDialog
        Me.chkappend = New System.Windows.Forms.CheckBox
        Me.Worker = New System.ComponentModel.BackgroundWorker
        Me.lstmose = New System.Windows.Forms.ListBox
        Me.lstexcel = New System.Windows.Forms.ListBox
        Me.lstvw = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnadd = New System.Windows.Forms.Button
        Me.btnremove = New System.Windows.Forms.Button
        Me.rdoPdcIssdEx = New System.Windows.Forms.RadioButton
        Me.rdocashcustomerEx = New System.Windows.Forms.RadioButton
        Me.rdoitemlistExcel = New System.Windows.Forms.RadioButton
        Me.rdoexcel = New System.Windows.Forms.RadioButton
        Me.rdomose = New System.Windows.Forms.RadioButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rdopurchase = New System.Windows.Forms.RadioButton
        Me.cmbformat = New System.Windows.Forms.ComboBox
        Me.plformat = New System.Windows.Forms.Panel
        Me.btnsetformat = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.rdocreditcustomer = New System.Windows.Forms.RadioButton
        Me.rdosupplier = New System.Windows.Forms.RadioButton
        Me.rdosalesTransferFromMchn = New System.Windows.Forms.RadioButton
        Me.numVchrNo = New System.Windows.Forms.TextBox
        Me.chkmdb = New System.Windows.Forms.CheckBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmbsalesman = New System.Windows.Forms.ComboBox
        Me.btnrefreshmechinedata = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.clrend = New System.Windows.Forms.DateTimePicker
        Me.cldrstart = New System.Windows.Forms.DateTimePicker
        Me.rdoreceipts = New System.Windows.Forms.RadioButton
        Me.rdosales = New System.Windows.Forms.RadioButton
        Me.chkopeningOnly = New System.Windows.Forms.CheckBox
        Me.chkskip = New System.Windows.Forms.CheckBox
        Me.chklocqty = New System.Windows.Forms.CheckBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmblocation = New System.Windows.Forms.ComboBox
        Me.pllocation = New System.Windows.Forms.Panel
        Me.chkskipzero = New System.Windows.Forms.CheckBox
        Me.Panel1.SuspendLayout()
        Me.plformat.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pllocation.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblrec
        '
        Me.lblrec.AutoSize = True
        Me.lblrec.BackColor = System.Drawing.Color.Transparent
        Me.lblrec.Location = New System.Drawing.Point(376, 128)
        Me.lblrec.Name = "lblrec"
        Me.lblrec.Size = New System.Drawing.Size(42, 13)
        Me.lblrec.TabIndex = 36
        Me.lblrec.Text = "Record"
        '
        'lblmodule
        '
        Me.lblmodule.AutoSize = True
        Me.lblmodule.BackColor = System.Drawing.Color.Transparent
        Me.lblmodule.Location = New System.Drawing.Point(376, 89)
        Me.lblmodule.Name = "lblmodule"
        Me.lblmodule.Size = New System.Drawing.Size(42, 13)
        Me.lblmodule.TabIndex = 35
        Me.lblmodule.Text = "Module"
        '
        'pb
        '
        Me.pb.Location = New System.Drawing.Point(376, 105)
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(396, 20)
        Me.pb.TabIndex = 26
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.SteelBlue
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(707, 498)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(65, 28)
        Me.btnCancel.TabIndex = 25
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnok
        '
        Me.btnok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnok.BackColor = System.Drawing.Color.SteelBlue
        Me.btnok.FlatAppearance.BorderSize = 0
        Me.btnok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnok.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnok.ForeColor = System.Drawing.Color.White
        Me.btnok.Location = New System.Drawing.Point(638, 498)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(65, 28)
        Me.btnok.TabIndex = 24
        Me.btnok.Text = "Transfer"
        Me.btnok.UseVisualStyleBackColor = False
        '
        'txtpath
        '
        Me.txtpath.Location = New System.Drawing.Point(15, 104)
        Me.txtpath.Name = "txtpath"
        Me.txtpath.Size = New System.Drawing.Size(276, 20)
        Me.txtpath.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(12, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Select Valid File Path"
        '
        'btnbrowse
        '
        Me.btnbrowse.BackColor = System.Drawing.Color.SteelBlue
        Me.btnbrowse.FlatAppearance.BorderSize = 0
        Me.btnbrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnbrowse.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbrowse.ForeColor = System.Drawing.Color.White
        Me.btnbrowse.Location = New System.Drawing.Point(294, 99)
        Me.btnbrowse.Name = "btnbrowse"
        Me.btnbrowse.Size = New System.Drawing.Size(76, 28)
        Me.btnbrowse.TabIndex = 21
        Me.btnbrowse.Text = "Browse"
        Me.btnbrowse.UseVisualStyleBackColor = False
        '
        'opfSelectFile
        '
        Me.opfSelectFile.FileName = "OpenFileDialog1"
        '
        'chkappend
        '
        Me.chkappend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkappend.AutoSize = True
        Me.chkappend.BackColor = System.Drawing.Color.Transparent
        Me.chkappend.Checked = True
        Me.chkappend.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkappend.Location = New System.Drawing.Point(709, 85)
        Me.chkappend.Name = "chkappend"
        Me.chkappend.Size = New System.Drawing.Size(63, 17)
        Me.chkappend.TabIndex = 37
        Me.chkappend.Text = "Append"
        Me.chkappend.UseVisualStyleBackColor = False
        Me.chkappend.Visible = False
        '
        'Worker
        '
        '
        'lstmose
        '
        Me.lstmose.FormattingEnabled = True
        Me.lstmose.Location = New System.Drawing.Point(15, 177)
        Me.lstmose.Name = "lstmose"
        Me.lstmose.Size = New System.Drawing.Size(193, 316)
        Me.lstmose.TabIndex = 38
        '
        'lstexcel
        '
        Me.lstexcel.FormattingEnabled = True
        Me.lstexcel.Location = New System.Drawing.Point(211, 177)
        Me.lstexcel.Name = "lstexcel"
        Me.lstexcel.Size = New System.Drawing.Size(193, 316)
        Me.lstexcel.TabIndex = 39
        '
        'lstvw
        '
        Me.lstvw.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lstvw.Location = New System.Drawing.Point(458, 179)
        Me.lstvw.Name = "lstvw"
        Me.lstvw.Size = New System.Drawing.Size(314, 313)
        Me.lstvw.TabIndex = 40
        Me.lstvw.UseCompatibleStateImageBehavior = False
        Me.lstvw.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Mose Column"
        Me.ColumnHeader1.Width = 151
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Excel Column"
        Me.ColumnHeader2.Width = 141
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 161)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "Mose Column"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(208, 161)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 13)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "Excel Column"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(455, 161)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 13)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Mapped Columns"
        '
        'btnadd
        '
        Me.btnadd.BackColor = System.Drawing.Color.SteelBlue
        Me.btnadd.FlatAppearance.BorderSize = 0
        Me.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnadd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.ForeColor = System.Drawing.Color.White
        Me.btnadd.Location = New System.Drawing.Point(410, 247)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(42, 28)
        Me.btnadd.TabIndex = 44
        Me.btnadd.Text = ">"
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'btnremove
        '
        Me.btnremove.BackColor = System.Drawing.Color.SteelBlue
        Me.btnremove.FlatAppearance.BorderSize = 0
        Me.btnremove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnremove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnremove.ForeColor = System.Drawing.Color.White
        Me.btnremove.Location = New System.Drawing.Point(410, 281)
        Me.btnremove.Name = "btnremove"
        Me.btnremove.Size = New System.Drawing.Size(42, 28)
        Me.btnremove.TabIndex = 45
        Me.btnremove.Text = "<"
        Me.btnremove.UseVisualStyleBackColor = False
        '
        'rdoPdcIssdEx
        '
        Me.rdoPdcIssdEx.AutoSize = True
        Me.rdoPdcIssdEx.BackColor = System.Drawing.Color.Transparent
        Me.rdoPdcIssdEx.Location = New System.Drawing.Point(232, 49)
        Me.rdoPdcIssdEx.Name = "rdoPdcIssdEx"
        Me.rdoPdcIssdEx.Size = New System.Drawing.Size(81, 17)
        Me.rdoPdcIssdEx.TabIndex = 46
        Me.rdoPdcIssdEx.Text = "PDC Issued"
        Me.rdoPdcIssdEx.UseVisualStyleBackColor = False
        Me.rdoPdcIssdEx.Visible = False
        '
        'rdocashcustomerEx
        '
        Me.rdocashcustomerEx.AutoSize = True
        Me.rdocashcustomerEx.BackColor = System.Drawing.Color.Transparent
        Me.rdocashcustomerEx.Location = New System.Drawing.Point(9, 65)
        Me.rdocashcustomerEx.Name = "rdocashcustomerEx"
        Me.rdocashcustomerEx.Size = New System.Drawing.Size(115, 17)
        Me.rdocashcustomerEx.TabIndex = 49
        Me.rdocashcustomerEx.Text = "Cash Customer List"
        Me.rdocashcustomerEx.UseVisualStyleBackColor = False
        '
        'rdoitemlistExcel
        '
        Me.rdoitemlistExcel.AutoSize = True
        Me.rdoitemlistExcel.BackColor = System.Drawing.Color.Transparent
        Me.rdoitemlistExcel.Checked = True
        Me.rdoitemlistExcel.Location = New System.Drawing.Point(9, 12)
        Me.rdoitemlistExcel.Name = "rdoitemlistExcel"
        Me.rdoitemlistExcel.Size = New System.Drawing.Size(64, 17)
        Me.rdoitemlistExcel.TabIndex = 48
        Me.rdoitemlistExcel.TabStop = True
        Me.rdoitemlistExcel.Text = "Item List"
        Me.rdoitemlistExcel.UseVisualStyleBackColor = False
        '
        'rdoexcel
        '
        Me.rdoexcel.AutoSize = True
        Me.rdoexcel.BackColor = System.Drawing.Color.Transparent
        Me.rdoexcel.Checked = True
        Me.rdoexcel.Location = New System.Drawing.Point(2, 4)
        Me.rdoexcel.Name = "rdoexcel"
        Me.rdoexcel.Size = New System.Drawing.Size(77, 17)
        Me.rdoexcel.TabIndex = 49
        Me.rdoexcel.TabStop = True
        Me.rdoexcel.Text = "From Excel"
        Me.rdoexcel.UseVisualStyleBackColor = False
        '
        'rdomose
        '
        Me.rdomose.AutoSize = True
        Me.rdomose.BackColor = System.Drawing.Color.Transparent
        Me.rdomose.Location = New System.Drawing.Point(85, 4)
        Me.rdomose.Name = "rdomose"
        Me.rdomose.Size = New System.Drawing.Size(77, 17)
        Me.rdomose.TabIndex = 50
        Me.rdomose.Text = "From Mose"
        Me.rdomose.UseVisualStyleBackColor = False
        Me.rdomose.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.rdomose)
        Me.Panel1.Controls.Add(Me.rdoexcel)
        Me.Panel1.Location = New System.Drawing.Point(12, 130)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(254, 24)
        Me.Panel1.TabIndex = 51
        '
        'rdopurchase
        '
        Me.rdopurchase.AutoSize = True
        Me.rdopurchase.BackColor = System.Drawing.Color.Transparent
        Me.rdopurchase.Location = New System.Drawing.Point(232, 12)
        Me.rdopurchase.Name = "rdopurchase"
        Me.rdopurchase.Size = New System.Drawing.Size(112, 17)
        Me.rdopurchase.TabIndex = 52
        Me.rdopurchase.Text = "Purchase Transfer"
        Me.rdopurchase.UseVisualStyleBackColor = False
        Me.rdopurchase.Visible = False
        '
        'cmbformat
        '
        Me.cmbformat.FormattingEnabled = True
        Me.cmbformat.Location = New System.Drawing.Point(81, 3)
        Me.cmbformat.Name = "cmbformat"
        Me.cmbformat.Size = New System.Drawing.Size(139, 21)
        Me.cmbformat.TabIndex = 53
        '
        'plformat
        '
        Me.plformat.BackColor = System.Drawing.Color.Transparent
        Me.plformat.Controls.Add(Me.btnsetformat)
        Me.plformat.Controls.Add(Me.Label5)
        Me.plformat.Controls.Add(Me.cmbformat)
        Me.plformat.Location = New System.Drawing.Point(458, 128)
        Me.plformat.Name = "plformat"
        Me.plformat.Size = New System.Drawing.Size(314, 31)
        Me.plformat.TabIndex = 54
        '
        'btnsetformat
        '
        Me.btnsetformat.BackColor = System.Drawing.Color.SteelBlue
        Me.btnsetformat.FlatAppearance.BorderSize = 0
        Me.btnsetformat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsetformat.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsetformat.ForeColor = System.Drawing.Color.White
        Me.btnsetformat.Location = New System.Drawing.Point(226, 0)
        Me.btnsetformat.Name = "btnsetformat"
        Me.btnsetformat.Size = New System.Drawing.Size(88, 31)
        Me.btnsetformat.TabIndex = 55
        Me.btnsetformat.Text = "Set Format"
        Me.btnsetformat.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(3, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 13)
        Me.Label5.TabIndex = 54
        Me.Label5.Text = "Select Format"
        '
        'rdocreditcustomer
        '
        Me.rdocreditcustomer.AutoSize = True
        Me.rdocreditcustomer.BackColor = System.Drawing.Color.Transparent
        Me.rdocreditcustomer.Location = New System.Drawing.Point(127, 12)
        Me.rdocreditcustomer.Name = "rdocreditcustomer"
        Me.rdocreditcustomer.Size = New System.Drawing.Size(99, 17)
        Me.rdocreditcustomer.TabIndex = 55
        Me.rdocreditcustomer.Text = "Credit Customer"
        Me.rdocreditcustomer.UseVisualStyleBackColor = False
        '
        'rdosupplier
        '
        Me.rdosupplier.AutoSize = True
        Me.rdosupplier.BackColor = System.Drawing.Color.Transparent
        Me.rdosupplier.Location = New System.Drawing.Point(127, 49)
        Me.rdosupplier.Name = "rdosupplier"
        Me.rdosupplier.Size = New System.Drawing.Size(93, 17)
        Me.rdosupplier.TabIndex = 56
        Me.rdosupplier.Text = "Credit Supplier"
        Me.rdosupplier.UseVisualStyleBackColor = False
        '
        'rdosalesTransferFromMchn
        '
        Me.rdosalesTransferFromMchn.AutoSize = True
        Me.rdosalesTransferFromMchn.BackColor = System.Drawing.Color.Transparent
        Me.rdosalesTransferFromMchn.Location = New System.Drawing.Point(320, 12)
        Me.rdosalesTransferFromMchn.Name = "rdosalesTransferFromMchn"
        Me.rdosalesTransferFromMchn.Size = New System.Drawing.Size(163, 17)
        Me.rdosalesTransferFromMchn.TabIndex = 57
        Me.rdosalesTransferFromMchn.Text = "Sales Transfer From Mechine"
        Me.rdosalesTransferFromMchn.UseVisualStyleBackColor = False
        Me.rdosalesTransferFromMchn.Visible = False
        '
        'numVchrNo
        '
        Me.numVchrNo.Location = New System.Drawing.Point(685, 9)
        Me.numVchrNo.Name = "numVchrNo"
        Me.numVchrNo.Size = New System.Drawing.Size(89, 20)
        Me.numVchrNo.TabIndex = 58
        Me.numVchrNo.Visible = False
        '
        'chkmdb
        '
        Me.chkmdb.AutoSize = True
        Me.chkmdb.BackColor = System.Drawing.Color.Transparent
        Me.chkmdb.Location = New System.Drawing.Point(3, 4)
        Me.chkmdb.Name = "chkmdb"
        Me.chkmdb.Size = New System.Drawing.Size(115, 17)
        Me.chkmdb.TabIndex = 59
        Me.chkmdb.Text = "Transfer from MDB"
        Me.chkmdb.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.cmbsalesman)
        Me.Panel2.Controls.Add(Me.btnrefreshmechinedata)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.clrend)
        Me.Panel2.Controls.Add(Me.cldrstart)
        Me.Panel2.Controls.Add(Me.rdoreceipts)
        Me.Panel2.Controls.Add(Me.rdosales)
        Me.Panel2.Controls.Add(Me.chkmdb)
        Me.Panel2.Location = New System.Drawing.Point(143, 28)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(642, 43)
        Me.Panel2.TabIndex = 60
        Me.Panel2.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(377, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 13)
        Me.Label7.TabIndex = 345484
        Me.Label7.Text = "Sales Man"
        '
        'cmbsalesman
        '
        Me.cmbsalesman.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsalesman.FormattingEnabled = True
        Me.cmbsalesman.Location = New System.Drawing.Point(437, 16)
        Me.cmbsalesman.Name = "cmbsalesman"
        Me.cmbsalesman.Size = New System.Drawing.Size(123, 21)
        Me.cmbsalesman.TabIndex = 345483
        '
        'btnrefreshmechinedata
        '
        Me.btnrefreshmechinedata.BackColor = System.Drawing.Color.SteelBlue
        Me.btnrefreshmechinedata.FlatAppearance.BorderSize = 0
        Me.btnrefreshmechinedata.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnrefreshmechinedata.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnrefreshmechinedata.ForeColor = System.Drawing.Color.White
        Me.btnrefreshmechinedata.Location = New System.Drawing.Point(566, 10)
        Me.btnrefreshmechinedata.Name = "btnrefreshmechinedata"
        Me.btnrefreshmechinedata.Size = New System.Drawing.Size(74, 28)
        Me.btnrefreshmechinedata.TabIndex = 61
        Me.btnrefreshmechinedata.Text = "Refresh"
        Me.btnrefreshmechinedata.UseVisualStyleBackColor = False
        Me.btnrefreshmechinedata.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(151, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 13)
        Me.Label6.TabIndex = 345482
        Me.Label6.Text = "Date Range"
        '
        'clrend
        '
        Me.clrend.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.clrend.Location = New System.Drawing.Point(263, 17)
        Me.clrend.Name = "clrend"
        Me.clrend.Size = New System.Drawing.Size(107, 20)
        Me.clrend.TabIndex = 345481
        '
        'cldrstart
        '
        Me.cldrstart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.cldrstart.Location = New System.Drawing.Point(154, 17)
        Me.cldrstart.Name = "cldrstart"
        Me.cldrstart.Size = New System.Drawing.Size(106, 20)
        Me.cldrstart.TabIndex = 345480
        '
        'rdoreceipts
        '
        Me.rdoreceipts.AutoSize = True
        Me.rdoreceipts.BackColor = System.Drawing.Color.Transparent
        Me.rdoreceipts.Location = New System.Drawing.Point(82, 21)
        Me.rdoreceipts.Name = "rdoreceipts"
        Me.rdoreceipts.Size = New System.Drawing.Size(67, 17)
        Me.rdoreceipts.TabIndex = 61
        Me.rdoreceipts.Text = "Receipts"
        Me.rdoreceipts.UseVisualStyleBackColor = False
        '
        'rdosales
        '
        Me.rdosales.AutoSize = True
        Me.rdosales.BackColor = System.Drawing.Color.Transparent
        Me.rdosales.Checked = True
        Me.rdosales.Location = New System.Drawing.Point(25, 21)
        Me.rdosales.Name = "rdosales"
        Me.rdosales.Size = New System.Drawing.Size(51, 17)
        Me.rdosales.TabIndex = 60
        Me.rdosales.TabStop = True
        Me.rdosales.Text = "Sales"
        Me.rdosales.UseVisualStyleBackColor = False
        '
        'chkopeningOnly
        '
        Me.chkopeningOnly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkopeningOnly.AutoSize = True
        Me.chkopeningOnly.BackColor = System.Drawing.Color.Transparent
        Me.chkopeningOnly.Checked = True
        Me.chkopeningOnly.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkopeningOnly.Location = New System.Drawing.Point(9, 31)
        Me.chkopeningOnly.Name = "chkopeningOnly"
        Me.chkopeningOnly.Size = New System.Drawing.Size(128, 17)
        Me.chkopeningOnly.TabIndex = 61
        Me.chkopeningOnly.Text = "Update Opening Only"
        Me.chkopeningOnly.UseVisualStyleBackColor = False
        Me.chkopeningOnly.Visible = False
        '
        'chkskip
        '
        Me.chkskip.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkskip.AutoSize = True
        Me.chkskip.BackColor = System.Drawing.Color.Transparent
        Me.chkskip.Checked = True
        Me.chkskip.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkskip.Location = New System.Drawing.Point(618, 84)
        Me.chkskip.Name = "chkskip"
        Me.chkskip.Size = New System.Drawing.Size(86, 17)
        Me.chkskip.TabIndex = 62
        Me.chkskip.Text = "Skip Existing"
        Me.chkskip.UseVisualStyleBackColor = False
        '
        'chklocqty
        '
        Me.chklocqty.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chklocqty.AutoSize = True
        Me.chklocqty.BackColor = System.Drawing.Color.Transparent
        Me.chklocqty.Location = New System.Drawing.Point(9, 47)
        Me.chklocqty.Name = "chklocqty"
        Me.chklocqty.Size = New System.Drawing.Size(86, 17)
        Me.chklocqty.TabIndex = 63
        Me.chklocqty.Text = "Location Qty"
        Me.chklocqty.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(12, 6)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(48, 13)
        Me.Label8.TabIndex = 345486
        Me.Label8.Text = "Location"
        '
        'cmblocation
        '
        Me.cmblocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmblocation.FormattingEnabled = True
        Me.cmblocation.Location = New System.Drawing.Point(72, 3)
        Me.cmblocation.Name = "cmblocation"
        Me.cmblocation.Size = New System.Drawing.Size(123, 21)
        Me.cmblocation.TabIndex = 345485
        '
        'pllocation
        '
        Me.pllocation.BackColor = System.Drawing.Color.Transparent
        Me.pllocation.Controls.Add(Me.cmblocation)
        Me.pllocation.Controls.Add(Me.Label8)
        Me.pllocation.Location = New System.Drawing.Point(565, 51)
        Me.pllocation.Name = "pllocation"
        Me.pllocation.Size = New System.Drawing.Size(209, 33)
        Me.pllocation.TabIndex = 345487
        '
        'chkskipzero
        '
        Me.chkskipzero.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkskipzero.AutoSize = True
        Me.chkskipzero.BackColor = System.Drawing.Color.Transparent
        Me.chkskipzero.Checked = True
        Me.chkskipzero.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkskipzero.Location = New System.Drawing.Point(521, 84)
        Me.chkskipzero.Name = "chkskipzero"
        Me.chkskipzero.Size = New System.Drawing.Size(91, 17)
        Me.chkskipzero.TabIndex = 345488
        Me.chkskipzero.Text = "Skip Zero Qty"
        Me.chkskipzero.UseVisualStyleBackColor = False
        '
        'TransferiItemsFromExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(786, 543)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkskipzero)
        Me.Controls.Add(Me.pllocation)
        Me.Controls.Add(Me.chklocqty)
        Me.Controls.Add(Me.chkskip)
        Me.Controls.Add(Me.chkopeningOnly)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.numVchrNo)
        Me.Controls.Add(Me.rdosalesTransferFromMchn)
        Me.Controls.Add(Me.rdosupplier)
        Me.Controls.Add(Me.rdocreditcustomer)
        Me.Controls.Add(Me.plformat)
        Me.Controls.Add(Me.rdopurchase)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.rdocashcustomerEx)
        Me.Controls.Add(Me.rdoitemlistExcel)
        Me.Controls.Add(Me.rdoPdcIssdEx)
        Me.Controls.Add(Me.btnremove)
        Me.Controls.Add(Me.btnadd)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lstvw)
        Me.Controls.Add(Me.lstexcel)
        Me.Controls.Add(Me.lstmose)
        Me.Controls.Add(Me.chkappend)
        Me.Controls.Add(Me.lblrec)
        Me.Controls.Add(Me.lblmodule)
        Me.Controls.Add(Me.pb)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.txtpath)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnbrowse)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "TransferiItemsFromExcel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Transfer Items From Excel"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.plformat.ResumeLayout(False)
        Me.plformat.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pllocation.ResumeLayout(False)
        Me.pllocation.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblrec As System.Windows.Forms.Label
    Friend WithEvents lblmodule As System.Windows.Forms.Label
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnok As System.Windows.Forms.Button
    Friend WithEvents txtpath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnbrowse As System.Windows.Forms.Button
    Friend WithEvents opfSelectFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents chkappend As System.Windows.Forms.CheckBox
    Friend WithEvents Worker As System.ComponentModel.BackgroundWorker
    Friend WithEvents lstmose As System.Windows.Forms.ListBox
    Friend WithEvents lstexcel As System.Windows.Forms.ListBox
    Friend WithEvents lstvw As System.Windows.Forms.ListView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents btnremove As System.Windows.Forms.Button
    Friend WithEvents rdoPdcIssdEx As System.Windows.Forms.RadioButton
    Friend WithEvents rdocashcustomerEx As System.Windows.Forms.RadioButton
    Friend WithEvents rdoitemlistExcel As System.Windows.Forms.RadioButton
    Friend WithEvents rdoexcel As System.Windows.Forms.RadioButton
    Friend WithEvents rdomose As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdopurchase As System.Windows.Forms.RadioButton
    Friend WithEvents cmbformat As System.Windows.Forms.ComboBox
    Friend WithEvents plformat As System.Windows.Forms.Panel
    Friend WithEvents btnsetformat As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents rdocreditcustomer As System.Windows.Forms.RadioButton
    Friend WithEvents rdosupplier As System.Windows.Forms.RadioButton
    Friend WithEvents rdosalesTransferFromMchn As System.Windows.Forms.RadioButton
    Friend WithEvents numVchrNo As System.Windows.Forms.TextBox
    Friend WithEvents chkmdb As System.Windows.Forms.CheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rdoreceipts As System.Windows.Forms.RadioButton
    Friend WithEvents rdosales As System.Windows.Forms.RadioButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents clrend As System.Windows.Forms.DateTimePicker
    Friend WithEvents cldrstart As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnrefreshmechinedata As System.Windows.Forms.Button
    Friend WithEvents chkopeningOnly As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbsalesman As System.Windows.Forms.ComboBox
    Friend WithEvents chkskip As System.Windows.Forms.CheckBox
    Friend WithEvents chklocqty As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmblocation As System.Windows.Forms.ComboBox
    Friend WithEvents pllocation As System.Windows.Forms.Panel
    Friend WithEvents chkskipzero As System.Windows.Forms.CheckBox
End Class
