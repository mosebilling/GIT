<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Homefrm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Homefrm))
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtserialno = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblsupplier = New System.Windows.Forms.Label
        Me.lblpurno = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblpurdate = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblsalesDate = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblSalesNo = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblcustomer = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblitem = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblsupno = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.lblSupExdate = New System.Windows.Forms.Label
        Me.lblisupwarrentySatatus = New System.Windows.Forms.Label
        Me.lblexpiry = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblremark = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.lblWstatuscp = New System.Windows.Forms.Label
        Me.lblWstatus = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblwarrenty = New System.Windows.Forms.Label
        Me.lstContent = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.chkwarrenty = New System.Windows.Forms.CheckBox
        Me.lbltime = New System.Windows.Forms.Label
        Me.lbluser = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.btnwaarentyCancel = New System.Windows.Forms.Button
        Me.btnaddwarrenty = New System.Windows.Forms.Button
        Me.cmdAddnew = New System.Windows.Forms.Button
        Me.btnclear = New System.Windows.Forms.Button
        Me.grptracking = New System.Windows.Forms.GroupBox
        Me.chkdualsim = New System.Windows.Forms.CheckBox
        Me.lblcompanyname = New System.Windows.Forms.Label
        Me.lbladdress1 = New System.Windows.Forms.Label
        Me.lbladdress2 = New System.Windows.Forms.Label
        Me.lbladdress3 = New System.Windows.Forms.Label
        Me.lblphone = New System.Windows.Forms.Label
        Me.lblfax = New System.Windows.Forms.Label
        Me.lblemail = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.lbldatabase = New System.Windows.Forms.Label
        Me.lblserver = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.lblaccountp = New System.Windows.Forms.Label
        Me.btnTracking = New System.Windows.Forms.Button
        Me.btninvoicelist = New System.Windows.Forms.Button
        Me.btnfinStatus = New System.Windows.Forms.Button
        Me.btnvoucherlist = New System.Windows.Forms.Button
        Me.btnquantitylist = New System.Windows.Forms.Button
        Me.Label19 = New System.Windows.Forms.Label
        Me.picLogo = New System.Windows.Forms.PictureBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.lblpath = New System.Windows.Forms.Label
        Me.worker = New System.ComponentModel.BackgroundWorker
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.plitbin = New System.Windows.Forms.Panel
        Me.Label21 = New System.Windows.Forms.Label
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.Timer4 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.grptracking.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plitbin.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Green
        Me.Label1.Location = New System.Drawing.Point(53, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Enter Serial No"
        '
        'txtserialno
        '
        Me.txtserialno.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtserialno.Location = New System.Drawing.Point(170, 24)
        Me.txtserialno.MaxLength = 50
        Me.txtserialno.Name = "txtserialno"
        Me.txtserialno.Size = New System.Drawing.Size(381, 33)
        Me.txtserialno.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(7, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Supplier Name "
        '
        'lblsupplier
        '
        Me.lblsupplier.AutoSize = True
        Me.lblsupplier.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsupplier.ForeColor = System.Drawing.Color.Black
        Me.lblsupplier.Location = New System.Drawing.Point(149, 21)
        Me.lblsupplier.Name = "lblsupplier"
        Me.lblsupplier.Size = New System.Drawing.Size(15, 13)
        Me.lblsupplier.TabIndex = 4
        Me.lblsupplier.Text = ": "
        '
        'lblpurno
        '
        Me.lblpurno.AutoSize = True
        Me.lblpurno.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpurno.ForeColor = System.Drawing.Color.Black
        Me.lblpurno.Location = New System.Drawing.Point(149, 46)
        Me.lblpurno.Name = "lblpurno"
        Me.lblpurno.Size = New System.Drawing.Size(15, 13)
        Me.lblpurno.TabIndex = 6
        Me.lblpurno.Text = ": "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(7, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Purchase No    "
        '
        'lblpurdate
        '
        Me.lblpurdate.AutoSize = True
        Me.lblpurdate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpurdate.ForeColor = System.Drawing.Color.Black
        Me.lblpurdate.Location = New System.Drawing.Point(515, 46)
        Me.lblpurdate.Name = "lblpurdate"
        Me.lblpurdate.Size = New System.Drawing.Size(15, 13)
        Me.lblpurdate.TabIndex = 8
        Me.lblpurdate.Text = ": "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(360, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(125, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Purchase Date      "
        '
        'lblsalesDate
        '
        Me.lblsalesDate.AutoSize = True
        Me.lblsalesDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsalesDate.ForeColor = System.Drawing.Color.Black
        Me.lblsalesDate.Location = New System.Drawing.Point(514, 49)
        Me.lblsalesDate.Name = "lblsalesDate"
        Me.lblsalesDate.Size = New System.Drawing.Size(15, 13)
        Me.lblsalesDate.TabIndex = 14
        Me.lblsalesDate.Text = ": "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(360, 49)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Sales Date     "
        '
        'lblSalesNo
        '
        Me.lblSalesNo.AutoSize = True
        Me.lblSalesNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesNo.ForeColor = System.Drawing.Color.Black
        Me.lblSalesNo.Location = New System.Drawing.Point(148, 49)
        Me.lblSalesNo.Name = "lblSalesNo"
        Me.lblSalesNo.Size = New System.Drawing.Size(15, 13)
        Me.lblSalesNo.TabIndex = 12
        Me.lblSalesNo.Text = ": "
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(6, 49)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(67, 13)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Sales No "
        '
        'lblcustomer
        '
        Me.lblcustomer.AutoSize = True
        Me.lblcustomer.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcustomer.ForeColor = System.Drawing.Color.Black
        Me.lblcustomer.Location = New System.Drawing.Point(148, 24)
        Me.lblcustomer.Name = "lblcustomer"
        Me.lblcustomer.Size = New System.Drawing.Size(15, 13)
        Me.lblcustomer.TabIndex = 10
        Me.lblcustomer.Text = ": "
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(6, 24)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(114, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Customer Name "
        '
        'lblitem
        '
        Me.lblitem.AutoSize = True
        Me.lblitem.BackColor = System.Drawing.Color.Transparent
        Me.lblitem.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblitem.ForeColor = System.Drawing.Color.Black
        Me.lblitem.Location = New System.Drawing.Point(167, 65)
        Me.lblitem.Name = "lblitem"
        Me.lblitem.Size = New System.Drawing.Size(15, 13)
        Me.lblitem.TabIndex = 16
        Me.lblitem.Text = ": "
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(25, 65)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(99, 13)
        Me.Label11.TabIndex = 15
        Me.Label11.Text = "Item Name     "
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.lblsupno)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lblsupplier)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lblpurno)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblpurdate)
        Me.GroupBox1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Navy
        Me.GroupBox1.Location = New System.Drawing.Point(18, 90)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(648, 73)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Purchase Details"
        '
        'lblsupno
        '
        Me.lblsupno.AutoSize = True
        Me.lblsupno.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsupno.ForeColor = System.Drawing.Color.Black
        Me.lblsupno.Location = New System.Drawing.Point(515, 18)
        Me.lblsupno.Name = "lblsupno"
        Me.lblsupno.Size = New System.Drawing.Size(17, 16)
        Me.lblsupno.TabIndex = 10
        Me.lblsupno.Text = ": "
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(360, 18)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(151, 13)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "Supplier Invoice No    "
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.lblcustomer)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.lblSalesNo)
        Me.GroupBox2.Controls.Add(Me.lblsalesDate)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Maroon
        Me.GroupBox2.Location = New System.Drawing.Point(18, 170)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(648, 71)
        Me.GroupBox2.TabIndex = 18
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Sales Details"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.lblSupExdate)
        Me.GroupBox3.Controls.Add(Me.lblisupwarrentySatatus)
        Me.GroupBox3.Controls.Add(Me.lblexpiry)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.lblremark)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.lblWstatuscp)
        Me.GroupBox3.Controls.Add(Me.lblWstatus)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.lblwarrenty)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.Purple
        Me.GroupBox3.Location = New System.Drawing.Point(18, 247)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(648, 116)
        Me.GroupBox3.TabIndex = 19
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Warranty Details"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(7, 65)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(103, 13)
        Me.Label15.TabIndex = 21
        Me.Label15.Text = "Expiry Date     "
        '
        'lblSupExdate
        '
        Me.lblSupExdate.AutoSize = True
        Me.lblSupExdate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupExdate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSupExdate.Location = New System.Drawing.Point(149, 65)
        Me.lblSupExdate.Name = "lblSupExdate"
        Me.lblSupExdate.Size = New System.Drawing.Size(11, 13)
        Me.lblSupExdate.TabIndex = 20
        Me.lblSupExdate.Text = ":"
        '
        'lblisupwarrentySatatus
        '
        Me.lblisupwarrentySatatus.AutoSize = True
        Me.lblisupwarrentySatatus.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblisupwarrentySatatus.ForeColor = System.Drawing.Color.Black
        Me.lblisupwarrentySatatus.Location = New System.Drawing.Point(149, 47)
        Me.lblisupwarrentySatatus.Name = "lblisupwarrentySatatus"
        Me.lblisupwarrentySatatus.Size = New System.Drawing.Size(11, 13)
        Me.lblisupwarrentySatatus.TabIndex = 19
        Me.lblisupwarrentySatatus.Text = ":"
        '
        'lblexpiry
        '
        Me.lblexpiry.AutoSize = True
        Me.lblexpiry.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblexpiry.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblexpiry.Location = New System.Drawing.Point(511, 47)
        Me.lblexpiry.Name = "lblexpiry"
        Me.lblexpiry.Size = New System.Drawing.Size(15, 13)
        Me.lblexpiry.TabIndex = 18
        Me.lblexpiry.Text = ": "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(360, 47)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(103, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Expiry Date     "
        '
        'lblremark
        '
        Me.lblremark.AutoSize = True
        Me.lblremark.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblremark.ForeColor = System.Drawing.Color.Black
        Me.lblremark.Location = New System.Drawing.Point(149, 90)
        Me.lblremark.Name = "lblremark"
        Me.lblremark.Size = New System.Drawing.Size(15, 13)
        Me.lblremark.TabIndex = 16
        Me.lblremark.Text = ": "
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(7, 90)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 13)
        Me.Label12.TabIndex = 15
        Me.Label12.Text = "Remarks"
        '
        'lblWstatuscp
        '
        Me.lblWstatuscp.AutoSize = True
        Me.lblWstatuscp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWstatuscp.ForeColor = System.Drawing.Color.Black
        Me.lblWstatuscp.Location = New System.Drawing.Point(360, 27)
        Me.lblWstatuscp.Name = "lblWstatuscp"
        Me.lblWstatuscp.Size = New System.Drawing.Size(134, 13)
        Me.lblWstatuscp.TabIndex = 13
        Me.lblWstatuscp.Text = "Customer Warranty"
        '
        'lblWstatus
        '
        Me.lblWstatus.AutoSize = True
        Me.lblWstatus.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWstatus.ForeColor = System.Drawing.Color.Green
        Me.lblWstatus.Location = New System.Drawing.Point(511, 27)
        Me.lblWstatus.Name = "lblWstatus"
        Me.lblWstatus.Size = New System.Drawing.Size(15, 13)
        Me.lblWstatus.TabIndex = 14
        Me.lblWstatus.Text = ": "
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(7, 27)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(126, 13)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Supplier Warranty"
        '
        'lblwarrenty
        '
        Me.lblwarrenty.AutoSize = True
        Me.lblwarrenty.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblwarrenty.ForeColor = System.Drawing.Color.Black
        Me.lblwarrenty.Location = New System.Drawing.Point(149, 27)
        Me.lblwarrenty.Name = "lblwarrenty"
        Me.lblwarrenty.Size = New System.Drawing.Size(15, 13)
        Me.lblwarrenty.TabIndex = 12
        Me.lblwarrenty.Text = ": "
        '
        'lstContent
        '
        Me.lstContent.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.lstContent.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader5, Me.ColumnHeader4, Me.ColumnHeader7, Me.ColumnHeader6})
        Me.lstContent.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstContent.FullRowSelect = True
        Me.lstContent.GridLines = True
        Me.lstContent.Location = New System.Drawing.Point(18, 366)
        Me.lstContent.Name = "lstContent"
        Me.lstContent.Size = New System.Drawing.Size(610, 126)
        Me.lstContent.TabIndex = 345448
        Me.lstContent.UseCompatibleStateImageBehavior = False
        Me.lstContent.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Job No"
        Me.ColumnHeader1.Width = 81
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Job Date"
        Me.ColumnHeader2.Width = 85
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Description"
        Me.ColumnHeader3.Width = 172
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Warranty?"
        Me.ColumnHeader5.Width = 81
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Observation"
        Me.ColumnHeader4.Width = 193
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.DisplayIndex = 6
        Me.ColumnHeader7.Text = "Tech Remark"
        Me.ColumnHeader7.Width = 195
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.DisplayIndex = 5
        Me.ColumnHeader6.Text = "jbid"
        Me.ColumnHeader6.Width = 0
        '
        'chkwarrenty
        '
        Me.chkwarrenty.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkwarrenty.AutoSize = True
        Me.chkwarrenty.Checked = True
        Me.chkwarrenty.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkwarrenty.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkwarrenty.Location = New System.Drawing.Point(18, 88)
        Me.chkwarrenty.Name = "chkwarrenty"
        Me.chkwarrenty.Size = New System.Drawing.Size(107, 17)
        Me.chkwarrenty.TabIndex = 345450
        Me.chkwarrenty.Text = "With Warranty"
        Me.chkwarrenty.UseVisualStyleBackColor = True
        '
        'lbltime
        '
        Me.lbltime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbltime.BackColor = System.Drawing.Color.Transparent
        Me.lbltime.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbltime.Location = New System.Drawing.Point(106, 512)
        Me.lbltime.Name = "lbltime"
        Me.lbltime.Size = New System.Drawing.Size(283, 19)
        Me.lbltime.TabIndex = 345454
        Me.lbltime.Text = "Date && Time"
        Me.lbltime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbluser
        '
        Me.lbluser.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbluser.BackColor = System.Drawing.Color.Transparent
        Me.lbluser.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbluser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbluser.Location = New System.Drawing.Point(106, 489)
        Me.lbluser.Name = "lbluser"
        Me.lbluser.Size = New System.Drawing.Size(283, 19)
        Me.lbluser.TabIndex = 345453
        Me.lbluser.Text = "User"
        Me.lbluser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(6, 489)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 16)
        Me.Label3.TabIndex = 345455
        Me.Label3.Text = "User"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(6, 512)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(96, 16)
        Me.Label13.TabIndex = 345456
        Me.Label13.Text = "Date && Time"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnwaarentyCancel
        '
        Me.btnwaarentyCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnwaarentyCancel.BackColor = System.Drawing.Color.SteelBlue
        Me.btnwaarentyCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnwaarentyCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnwaarentyCancel.ForeColor = System.Drawing.Color.White
        Me.btnwaarentyCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnwaarentyCancel.Location = New System.Drawing.Point(269, 88)
        Me.btnwaarentyCancel.Name = "btnwaarentyCancel"
        Me.btnwaarentyCancel.Size = New System.Drawing.Size(131, 35)
        Me.btnwaarentyCancel.TabIndex = 345457
        Me.btnwaarentyCancel.Text = "Cancel Warranty"
        Me.btnwaarentyCancel.UseVisualStyleBackColor = False
        '
        'btnaddwarrenty
        '
        Me.btnaddwarrenty.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnaddwarrenty.BackColor = System.Drawing.Color.SteelBlue
        Me.btnaddwarrenty.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnaddwarrenty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnaddwarrenty.ForeColor = System.Drawing.Color.White
        Me.btnaddwarrenty.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnaddwarrenty.Location = New System.Drawing.Point(402, 88)
        Me.btnaddwarrenty.Name = "btnaddwarrenty"
        Me.btnaddwarrenty.Size = New System.Drawing.Size(116, 35)
        Me.btnaddwarrenty.TabIndex = 345452
        Me.btnaddwarrenty.Text = "Add  Warranty"
        Me.btnaddwarrenty.UseVisualStyleBackColor = False
        '
        'cmdAddnew
        '
        Me.cmdAddnew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAddnew.BackColor = System.Drawing.Color.SteelBlue
        Me.cmdAddnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAddnew.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAddnew.ForeColor = System.Drawing.Color.White
        Me.cmdAddnew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdAddnew.Location = New System.Drawing.Point(520, 88)
        Me.cmdAddnew.Name = "cmdAddnew"
        Me.cmdAddnew.Size = New System.Drawing.Size(99, 35)
        Me.cmdAddnew.TabIndex = 345449
        Me.cmdAddnew.Text = "&Add To Job"
        Me.cmdAddnew.UseVisualStyleBackColor = False
        '
        'btnclear
        '
        Me.btnclear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclear.BackColor = System.Drawing.Color.SteelBlue
        Me.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclear.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclear.ForeColor = System.Drawing.Color.White
        Me.btnclear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnclear.Location = New System.Drawing.Point(168, 88)
        Me.btnclear.Name = "btnclear"
        Me.btnclear.Size = New System.Drawing.Size(99, 35)
        Me.btnclear.TabIndex = 345458
        Me.btnclear.Text = "Clear All"
        Me.btnclear.UseVisualStyleBackColor = False
        '
        'grptracking
        '
        Me.grptracking.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grptracking.BackColor = System.Drawing.Color.Transparent
        Me.grptracking.Controls.Add(Me.chkdualsim)
        Me.grptracking.Controls.Add(Me.btnclear)
        Me.grptracking.Controls.Add(Me.btnwaarentyCancel)
        Me.grptracking.Controls.Add(Me.btnaddwarrenty)
        Me.grptracking.Controls.Add(Me.cmdAddnew)
        Me.grptracking.Controls.Add(Me.Label1)
        Me.grptracking.Controls.Add(Me.Label11)
        Me.grptracking.Controls.Add(Me.lblitem)
        Me.grptracking.Controls.Add(Me.chkwarrenty)
        Me.grptracking.Controls.Add(Me.GroupBox1)
        Me.grptracking.Controls.Add(Me.GroupBox2)
        Me.grptracking.Controls.Add(Me.GroupBox3)
        Me.grptracking.Controls.Add(Me.lstContent)
        Me.grptracking.Controls.Add(Me.txtserialno)
        Me.grptracking.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grptracking.Location = New System.Drawing.Point(454, 5)
        Me.grptracking.Name = "grptracking"
        Me.grptracking.Size = New System.Drawing.Size(628, 132)
        Me.grptracking.TabIndex = 345460
        Me.grptracking.TabStop = False
        Me.grptracking.Visible = False
        '
        'chkdualsim
        '
        Me.chkdualsim.AutoSize = True
        Me.chkdualsim.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkdualsim.Location = New System.Drawing.Point(554, 25)
        Me.chkdualsim.Name = "chkdualsim"
        Me.chkdualsim.Size = New System.Drawing.Size(117, 17)
        Me.chkdualsim.TabIndex = 345459
        Me.chkdualsim.Text = "Dual Serial Number"
        Me.chkdualsim.UseVisualStyleBackColor = True
        '
        'lblcompanyname
        '
        Me.lblcompanyname.AutoSize = True
        Me.lblcompanyname.BackColor = System.Drawing.Color.Transparent
        Me.lblcompanyname.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcompanyname.ForeColor = System.Drawing.Color.Black
        Me.lblcompanyname.Location = New System.Drawing.Point(13, 15)
        Me.lblcompanyname.Name = "lblcompanyname"
        Me.lblcompanyname.Size = New System.Drawing.Size(127, 16)
        Me.lblcompanyname.TabIndex = 11
        Me.lblcompanyname.Text = "Company Name "
        '
        'lbladdress1
        '
        Me.lbladdress1.AutoSize = True
        Me.lbladdress1.BackColor = System.Drawing.Color.Transparent
        Me.lbladdress1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladdress1.ForeColor = System.Drawing.Color.Black
        Me.lbladdress1.Location = New System.Drawing.Point(13, 38)
        Me.lbladdress1.Name = "lbladdress1"
        Me.lbladdress1.Size = New System.Drawing.Size(70, 14)
        Me.lbladdress1.TabIndex = 345461
        Me.lbladdress1.Text = "Address 1"
        '
        'lbladdress2
        '
        Me.lbladdress2.AutoSize = True
        Me.lbladdress2.BackColor = System.Drawing.Color.Transparent
        Me.lbladdress2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladdress2.ForeColor = System.Drawing.Color.Black
        Me.lbladdress2.Location = New System.Drawing.Point(13, 54)
        Me.lbladdress2.Name = "lbladdress2"
        Me.lbladdress2.Size = New System.Drawing.Size(70, 14)
        Me.lbladdress2.TabIndex = 345462
        Me.lbladdress2.Text = "Address 2"
        '
        'lbladdress3
        '
        Me.lbladdress3.AutoSize = True
        Me.lbladdress3.BackColor = System.Drawing.Color.Transparent
        Me.lbladdress3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladdress3.ForeColor = System.Drawing.Color.Black
        Me.lbladdress3.Location = New System.Drawing.Point(13, 70)
        Me.lbladdress3.Name = "lbladdress3"
        Me.lbladdress3.Size = New System.Drawing.Size(70, 14)
        Me.lbladdress3.TabIndex = 345463
        Me.lbladdress3.Text = "Address 3"
        '
        'lblphone
        '
        Me.lblphone.AutoSize = True
        Me.lblphone.BackColor = System.Drawing.Color.Transparent
        Me.lblphone.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblphone.Location = New System.Drawing.Point(13, 86)
        Me.lblphone.Name = "lblphone"
        Me.lblphone.Size = New System.Drawing.Size(47, 14)
        Me.lblphone.TabIndex = 345464
        Me.lblphone.Text = "Phone"
        '
        'lblfax
        '
        Me.lblfax.AutoSize = True
        Me.lblfax.BackColor = System.Drawing.Color.Transparent
        Me.lblfax.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfax.Location = New System.Drawing.Point(191, 86)
        Me.lblfax.Name = "lblfax"
        Me.lblfax.Size = New System.Drawing.Size(28, 14)
        Me.lblfax.TabIndex = 345465
        Me.lblfax.Text = "Fax"
        '
        'lblemail
        '
        Me.lblemail.AutoSize = True
        Me.lblemail.BackColor = System.Drawing.Color.Transparent
        Me.lblemail.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblemail.Location = New System.Drawing.Point(13, 102)
        Me.lblemail.Name = "lblemail"
        Me.lblemail.Size = New System.Drawing.Size(40, 14)
        Me.lblemail.TabIndex = 345466
        Me.lblemail.Text = "Email"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = Global.SMSMP.My.Resources.Resources._4T9zXypLc
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.lblcompanyname)
        Me.Panel1.Controls.Add(Me.lbladdress1)
        Me.Panel1.Controls.Add(Me.lblphone)
        Me.Panel1.Controls.Add(Me.lbladdress2)
        Me.Panel1.Controls.Add(Me.lblfax)
        Me.Panel1.Controls.Add(Me.lbladdress3)
        Me.Panel1.Controls.Add(Me.lblemail)
        Me.Panel1.Location = New System.Drawing.Point(9, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(596, 142)
        Me.Panel1.TabIndex = 345468
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(6, 466)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(77, 16)
        Me.Label16.TabIndex = 345472
        Me.Label16.Text = "Database"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(6, 443)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(56, 16)
        Me.Label17.TabIndex = 345471
        Me.Label17.Text = "Server"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbldatabase
        '
        Me.lbldatabase.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbldatabase.BackColor = System.Drawing.Color.Transparent
        Me.lbldatabase.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldatabase.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbldatabase.Location = New System.Drawing.Point(106, 466)
        Me.lbldatabase.Name = "lbldatabase"
        Me.lbldatabase.Size = New System.Drawing.Size(283, 19)
        Me.lbldatabase.TabIndex = 345470
        Me.lbldatabase.Text = "Date && Time"
        Me.lbldatabase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblserver
        '
        Me.lblserver.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblserver.BackColor = System.Drawing.Color.Transparent
        Me.lblserver.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblserver.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblserver.Location = New System.Drawing.Point(106, 443)
        Me.lblserver.Name = "lblserver"
        Me.lblserver.Size = New System.Drawing.Size(283, 19)
        Me.lblserver.TabIndex = 345469
        Me.lblserver.Text = "User"
        Me.lblserver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(6, 420)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(87, 16)
        Me.Label18.TabIndex = 345473
        Me.Label18.Text = "A/C Period"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblaccountp
        '
        Me.lblaccountp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblaccountp.BackColor = System.Drawing.Color.Transparent
        Me.lblaccountp.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblaccountp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblaccountp.Location = New System.Drawing.Point(106, 420)
        Me.lblaccountp.Name = "lblaccountp"
        Me.lblaccountp.Size = New System.Drawing.Size(283, 19)
        Me.lblaccountp.TabIndex = 345474
        Me.lblaccountp.Text = "User"
        Me.lblaccountp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnTracking
        '
        Me.btnTracking.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTracking.BackColor = System.Drawing.Color.SteelBlue
        Me.btnTracking.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTracking.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTracking.ForeColor = System.Drawing.Color.White
        Me.btnTracking.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTracking.Location = New System.Drawing.Point(871, 0)
        Me.btnTracking.Name = "btnTracking"
        Me.btnTracking.Size = New System.Drawing.Size(212, 27)
        Me.btnTracking.TabIndex = 345475
        Me.btnTracking.Text = "Serial Number Tracking.."
        Me.btnTracking.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnTracking.UseVisualStyleBackColor = False
        Me.btnTracking.Visible = False
        '
        'btninvoicelist
        '
        Me.btninvoicelist.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btninvoicelist.BackColor = System.Drawing.Color.SteelBlue
        Me.btninvoicelist.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btninvoicelist.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btninvoicelist.ForeColor = System.Drawing.Color.White
        Me.btninvoicelist.Image = Global.SMSMP.My.Resources.Resources.ODRFNote
        Me.btninvoicelist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btninvoicelist.Location = New System.Drawing.Point(513, 129)
        Me.btninvoicelist.Name = "btninvoicelist"
        Me.btninvoicelist.Size = New System.Drawing.Size(208, 79)
        Me.btninvoicelist.TabIndex = 345479
        Me.btninvoicelist.Text = "Invoice List"
        Me.btninvoicelist.UseVisualStyleBackColor = False
        '
        'btnfinStatus
        '
        Me.btnfinStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnfinStatus.BackColor = System.Drawing.Color.SteelBlue
        Me.btnfinStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnfinStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnfinStatus.ForeColor = System.Drawing.Color.White
        Me.btnfinStatus.Image = Global.SMSMP.My.Resources.Resources.job
        Me.btnfinStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnfinStatus.Location = New System.Drawing.Point(723, 129)
        Me.btnfinStatus.Name = "btnfinStatus"
        Me.btnfinStatus.Size = New System.Drawing.Size(208, 79)
        Me.btnfinStatus.TabIndex = 345480
        Me.btnfinStatus.Text = "Financial Status"
        Me.btnfinStatus.UseVisualStyleBackColor = False
        '
        'btnvoucherlist
        '
        Me.btnvoucherlist.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnvoucherlist.BackColor = System.Drawing.Color.SteelBlue
        Me.btnvoucherlist.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnvoucherlist.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnvoucherlist.ForeColor = System.Drawing.Color.White
        Me.btnvoucherlist.Image = Global.SMSMP.My.Resources.Resources.OMR
        Me.btnvoucherlist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnvoucherlist.Location = New System.Drawing.Point(513, 210)
        Me.btnvoucherlist.Name = "btnvoucherlist"
        Me.btnvoucherlist.Size = New System.Drawing.Size(208, 79)
        Me.btnvoucherlist.TabIndex = 345481
        Me.btnvoucherlist.Text = "Voucher List"
        Me.btnvoucherlist.UseVisualStyleBackColor = False
        '
        'btnquantitylist
        '
        Me.btnquantitylist.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnquantitylist.BackColor = System.Drawing.Color.SteelBlue
        Me.btnquantitylist.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnquantitylist.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnquantitylist.ForeColor = System.Drawing.Color.White
        Me.btnquantitylist.Image = Global.SMSMP.My.Resources.Resources.OIP
        Me.btnquantitylist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnquantitylist.Location = New System.Drawing.Point(723, 210)
        Me.btnquantitylist.Name = "btnquantitylist"
        Me.btnquantitylist.Size = New System.Drawing.Size(208, 79)
        Me.btnquantitylist.TabIndex = 345482
        Me.btnquantitylist.Text = "Quantity List"
        Me.btnquantitylist.UseVisualStyleBackColor = False
        '
        'Label19
        '
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(733, 473)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(350, 76)
        Me.Label19.TabIndex = 345483
        Me.Label19.Text = "Email-sales@mosebilling.com" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Mob : +91 9526794529,8590963465" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "web: www.mosebillin" & _
            "g.com" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "---------------------------------------------" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "*Chat support available at" & _
            " www.mosebilling.com*"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'picLogo
        '
        Me.picLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picLogo.BackColor = System.Drawing.Color.Transparent
        Me.picLogo.Location = New System.Drawing.Point(490, 46)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(458, 64)
        Me.picLogo.TabIndex = 345484
        Me.picLogo.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.BackgroundImage = Global.SMSMP.My.Resources.Resources.mose_final
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(93, 178)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(162, 152)
        Me.PictureBox1.TabIndex = 345485
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.BackgroundImage = Global.SMSMP.My.Resources.Resources.Name_with_side_logo
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(734, 437)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(214, 38)
        Me.PictureBox2.TabIndex = 345486
        Me.PictureBox2.TabStop = False
        '
        'Label20
        '
        Me.Label20.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(6, 533)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(72, 16)
        Me.Label20.TabIndex = 345488
        Me.Label20.Text = "Doc Path"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblpath
        '
        Me.lblpath.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblpath.BackColor = System.Drawing.Color.Transparent
        Me.lblpath.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblpath.Location = New System.Drawing.Point(106, 533)
        Me.lblpath.Name = "lblpath"
        Me.lblpath.Size = New System.Drawing.Size(283, 19)
        Me.lblpath.TabIndex = 345487
        Me.lblpath.Text = "Docpath"
        Me.lblpath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'worker
        '
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        '
        'Timer3
        '
        Me.Timer3.Interval = 2000
        '
        'plitbin
        '
        Me.plitbin.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.plitbin.BackColor = System.Drawing.Color.Transparent
        Me.plitbin.Controls.Add(Me.Label21)
        Me.plitbin.Controls.Add(Me.PictureBox3)
        Me.plitbin.Controls.Add(Me.Label22)
        Me.plitbin.Location = New System.Drawing.Point(389, 437)
        Me.plitbin.Name = "plitbin"
        Me.plitbin.Size = New System.Drawing.Size(343, 120)
        Me.plitbin.TabIndex = 345490
        Me.plitbin.Visible = False
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(118, 7)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(108, 13)
        Me.Label21.TabIndex = 345489
        Me.Label21.Text = "Marketing Partner"
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox3.BackgroundImage = CType(resources.GetObject("PictureBox3.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox3.Location = New System.Drawing.Point(50, 7)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(62, 50)
        Me.PictureBox3.TabIndex = 345488
        Me.PictureBox3.TabStop = False
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label22.Location = New System.Drawing.Point(47, 57)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(291, 62)
        Me.Label22.TabIndex = 345487
        Me.Label22.Text = "Prinsof IT Bin solutions (opc) private limited," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Kakkanad, Ernakulam,Kerala, 6820" & _
            "21" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Phone: 0484 2388029,  Mobile: 8086607648" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Email : enquiriesitbin@gmail.com"
        '
        'Timer4
        '
        Me.Timer4.Enabled = True
        Me.Timer4.Interval = 1000
        '
        'Homefrm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.SMSMP.My.Resources.Resources.bg_pattern
        Me.ClientSize = New System.Drawing.Size(1089, 558)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.plitbin)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.lblpath)
        Me.Controls.Add(Me.btnTracking)
        Me.Controls.Add(Me.grptracking)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.btnquantitylist)
        Me.Controls.Add(Me.btnvoucherlist)
        Me.Controls.Add(Me.btnfinStatus)
        Me.Controls.Add(Me.btninvoicelist)
        Me.Controls.Add(Me.lblaccountp)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.lbldatabase)
        Me.Controls.Add(Me.lblserver)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lbltime)
        Me.Controls.Add(Me.lbluser)
        Me.Controls.Add(Me.picLogo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Homefrm"
        Me.Text = "Home"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.grptracking.ResumeLayout(False)
        Me.grptracking.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plitbin.ResumeLayout(False)
        Me.plitbin.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbltime As System.Windows.Forms.Label
    Friend WithEvents lbluser As System.Windows.Forms.Label
    Friend WithEvents txtserialno As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblsupplier As System.Windows.Forms.Label
    Friend WithEvents lblpurno As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblpurdate As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblsalesDate As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblSalesNo As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblcustomer As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblitem As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblWstatuscp As System.Windows.Forms.Label
    Friend WithEvents lblWstatus As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblwarrenty As System.Windows.Forms.Label
    Friend WithEvents lblremark As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lstContent As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdAddnew As System.Windows.Forms.Button
    Friend WithEvents chkwarrenty As System.Windows.Forms.CheckBox
    Friend WithEvents btnaddwarrenty As System.Windows.Forms.Button
    Friend WithEvents lblexpiry As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblsupno As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblisupwarrentySatatus As System.Windows.Forms.Label
    Friend WithEvents lblSupExdate As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnwaarentyCancel As System.Windows.Forms.Button
    Friend WithEvents btnclear As System.Windows.Forms.Button
    Friend WithEvents grptracking As System.Windows.Forms.GroupBox
    Friend WithEvents lblcompanyname As System.Windows.Forms.Label
    Friend WithEvents lbladdress1 As System.Windows.Forms.Label
    Friend WithEvents lbladdress2 As System.Windows.Forms.Label
    Friend WithEvents lbladdress3 As System.Windows.Forms.Label
    Friend WithEvents lblphone As System.Windows.Forms.Label
    Friend WithEvents lblfax As System.Windows.Forms.Label
    Friend WithEvents lblemail As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lbldatabase As System.Windows.Forms.Label
    Friend WithEvents lblserver As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblaccountp As System.Windows.Forms.Label
    Friend WithEvents btnTracking As System.Windows.Forms.Button
    Friend WithEvents btninvoicelist As System.Windows.Forms.Button
    Friend WithEvents btnfinStatus As System.Windows.Forms.Button
    Friend WithEvents btnvoucherlist As System.Windows.Forms.Button
    Friend WithEvents btnquantitylist As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents picLogo As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents chkdualsim As System.Windows.Forms.CheckBox
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblpath As System.Windows.Forms.Label
    Friend WithEvents worker As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
    Friend WithEvents plitbin As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Timer4 As System.Windows.Forms.Timer
End Class
