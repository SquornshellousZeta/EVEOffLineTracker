<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.APIInformationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabCharSheet = New System.Windows.Forms.TabPage()
        Me.imgAlliance = New System.Windows.Forms.PictureBox()
        Me.imgCorp = New System.Windows.Forms.PictureBox()
        Me.picImage = New System.Windows.Forms.PictureBox()
        Me.ssCharSheet = New System.Windows.Forms.StatusStrip()
        Me.tsslCharSheet = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslCachedUntilTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lbSkillLevel = New System.Windows.Forms.ListBox()
        Me.lbSkillTimeRemaining = New System.Windows.Forms.ListBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lbSkillQueue = New System.Windows.Forms.ListBox()
        Me.lblChaImplant = New System.Windows.Forms.Label()
        Me.lblWillImplant = New System.Windows.Forms.Label()
        Me.lblPerImplant = New System.Windows.Forms.Label()
        Me.lblMemImplant = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblIntImplant = New System.Windows.Forms.Label()
        Me.lblCha = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblWill = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblPer = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblMem = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblInt = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblBalance = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblCloneInfo = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblSkillpoints = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblGender = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblCorporation = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblRace = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.Label0 = New System.Windows.Forms.Label()
        Me.tabAssets = New System.Windows.Forms.TabPage()
        Me.ssAssets = New System.Windows.Forms.StatusStrip()
        Me.tsslAssets = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.dgvAssets = New System.Windows.Forms.DataGridView()
        Me.tabJournal = New System.Windows.Forms.TabPage()
        Me.ssJournal = New System.Windows.Forms.StatusStrip()
        Me.tsslJournal = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.dgvJournal = New System.Windows.Forms.DataGridView()
        Me.tabOrders = New System.Windows.Forms.TabPage()
        Me.cbActiveOrdersOnly = New System.Windows.Forms.CheckBox()
        Me.lblNumOrders = New System.Windows.Forms.Label()
        Me.ssOrders = New System.Windows.Forms.StatusStrip()
        Me.tsslOrders = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel5 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblSellOrdersTitle = New System.Windows.Forms.Label()
        Me.dgvOrdersSell = New System.Windows.Forms.DataGridView()
        Me.lblBuyOrdersTitle = New System.Windows.Forms.Label()
        Me.dgvOrdersBuy = New System.Windows.Forms.DataGridView()
        Me.tabTransactions = New System.Windows.Forms.TabPage()
        Me.ssTransactions = New System.Windows.Forms.StatusStrip()
        Me.tsslTransactions = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.dgvTransactions = New System.Windows.Forms.DataGridView()
        Me.tabIndustry = New System.Windows.Forms.TabPage()
        Me.chkActiveOnly = New System.Windows.Forms.CheckBox()
        Me.dgvIndustry = New System.Windows.Forms.DataGridView()
        Me.ssIndustry = New System.Windows.Forms.StatusStrip()
        Me.tsslIndustry = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel8 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tabBlueprints = New System.Windows.Forms.TabPage()
        Me.ssNotifications = New System.Windows.Forms.StatusStrip()
        Me.tsslNotifications = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel6 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.dgvBlueprints = New System.Windows.Forms.DataGridView()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label99 = New System.Windows.Forms.Label()
        Me.lblPaidUntil = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tabCharSheet.SuspendLayout()
        CType(Me.imgAlliance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgCorp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ssCharSheet.SuspendLayout()
        Me.tabAssets.SuspendLayout()
        Me.ssAssets.SuspendLayout()
        CType(Me.dgvAssets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabJournal.SuspendLayout()
        Me.ssJournal.SuspendLayout()
        CType(Me.dgvJournal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabOrders.SuspendLayout()
        Me.ssOrders.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgvOrdersSell, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvOrdersBuy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabTransactions.SuspendLayout()
        Me.ssTransactions.SuspendLayout()
        CType(Me.dgvTransactions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabIndustry.SuspendLayout()
        CType(Me.dgvIndustry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ssIndustry.SuspendLayout()
        Me.tabBlueprints.SuspendLayout()
        Me.ssNotifications.SuspendLayout()
        CType(Me.dgvBlueprints, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(825, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(92, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.APIInformationToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'APIInformationToolStripMenuItem
        '
        Me.APIInformationToolStripMenuItem.Name = "APIInformationToolStripMenuItem"
        Me.APIInformationToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.APIInformationToolStripMenuItem.Text = "API Information..."
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.AboutToolStripMenuItem.Text = "About..."
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tabCharSheet)
        Me.TabControl1.Controls.Add(Me.tabAssets)
        Me.TabControl1.Controls.Add(Me.tabJournal)
        Me.TabControl1.Controls.Add(Me.tabOrders)
        Me.TabControl1.Controls.Add(Me.tabTransactions)
        Me.TabControl1.Controls.Add(Me.tabIndustry)
        Me.TabControl1.Controls.Add(Me.tabBlueprints)
        Me.TabControl1.Location = New System.Drawing.Point(0, 24)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(825, 526)
        Me.TabControl1.TabIndex = 1
        '
        'tabCharSheet
        '
        Me.tabCharSheet.Controls.Add(Me.lblPaidUntil)
        Me.tabCharSheet.Controls.Add(Me.Label99)
        Me.tabCharSheet.Controls.Add(Me.imgAlliance)
        Me.tabCharSheet.Controls.Add(Me.imgCorp)
        Me.tabCharSheet.Controls.Add(Me.picImage)
        Me.tabCharSheet.Controls.Add(Me.ssCharSheet)
        Me.tabCharSheet.Controls.Add(Me.Label18)
        Me.tabCharSheet.Controls.Add(Me.Label17)
        Me.tabCharSheet.Controls.Add(Me.Label15)
        Me.tabCharSheet.Controls.Add(Me.lbSkillLevel)
        Me.tabCharSheet.Controls.Add(Me.lbSkillTimeRemaining)
        Me.tabCharSheet.Controls.Add(Me.Label13)
        Me.tabCharSheet.Controls.Add(Me.lbSkillQueue)
        Me.tabCharSheet.Controls.Add(Me.lblChaImplant)
        Me.tabCharSheet.Controls.Add(Me.lblWillImplant)
        Me.tabCharSheet.Controls.Add(Me.lblPerImplant)
        Me.tabCharSheet.Controls.Add(Me.lblMemImplant)
        Me.tabCharSheet.Controls.Add(Me.Label1)
        Me.tabCharSheet.Controls.Add(Me.lblIntImplant)
        Me.tabCharSheet.Controls.Add(Me.lblCha)
        Me.tabCharSheet.Controls.Add(Me.Label16)
        Me.tabCharSheet.Controls.Add(Me.lblWill)
        Me.tabCharSheet.Controls.Add(Me.Label12)
        Me.tabCharSheet.Controls.Add(Me.lblPer)
        Me.tabCharSheet.Controls.Add(Me.Label14)
        Me.tabCharSheet.Controls.Add(Me.lblMem)
        Me.tabCharSheet.Controls.Add(Me.Label11)
        Me.tabCharSheet.Controls.Add(Me.Label10)
        Me.tabCharSheet.Controls.Add(Me.Label9)
        Me.tabCharSheet.Controls.Add(Me.lblInt)
        Me.tabCharSheet.Controls.Add(Me.Label8)
        Me.tabCharSheet.Controls.Add(Me.lblBalance)
        Me.tabCharSheet.Controls.Add(Me.Label7)
        Me.tabCharSheet.Controls.Add(Me.lblCloneInfo)
        Me.tabCharSheet.Controls.Add(Me.Label6)
        Me.tabCharSheet.Controls.Add(Me.lblSkillpoints)
        Me.tabCharSheet.Controls.Add(Me.Label5)
        Me.tabCharSheet.Controls.Add(Me.lblGender)
        Me.tabCharSheet.Controls.Add(Me.Label2)
        Me.tabCharSheet.Controls.Add(Me.lblCorporation)
        Me.tabCharSheet.Controls.Add(Me.Label4)
        Me.tabCharSheet.Controls.Add(Me.lblRace)
        Me.tabCharSheet.Controls.Add(Me.Label3)
        Me.tabCharSheet.Controls.Add(Me.lblName)
        Me.tabCharSheet.Controls.Add(Me.Label0)
        Me.tabCharSheet.Location = New System.Drawing.Point(4, 22)
        Me.tabCharSheet.Name = "tabCharSheet"
        Me.tabCharSheet.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCharSheet.Size = New System.Drawing.Size(817, 500)
        Me.tabCharSheet.TabIndex = 0
        Me.tabCharSheet.Text = "Character Sheet"
        Me.tabCharSheet.UseVisualStyleBackColor = True
        '
        'imgAlliance
        '
        Me.imgAlliance.Location = New System.Drawing.Point(273, 285)
        Me.imgAlliance.Name = "imgAlliance"
        Me.imgAlliance.Size = New System.Drawing.Size(64, 64)
        Me.imgAlliance.TabIndex = 43
        Me.imgAlliance.TabStop = False
        '
        'imgCorp
        '
        Me.imgCorp.Location = New System.Drawing.Point(273, 215)
        Me.imgCorp.Name = "imgCorp"
        Me.imgCorp.Size = New System.Drawing.Size(64, 64)
        Me.imgCorp.TabIndex = 42
        Me.imgCorp.TabStop = False
        '
        'picImage
        '
        Me.picImage.Location = New System.Drawing.Point(11, 215)
        Me.picImage.Name = "picImage"
        Me.picImage.Size = New System.Drawing.Size(256, 256)
        Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picImage.TabIndex = 41
        Me.picImage.TabStop = False
        '
        'ssCharSheet
        '
        Me.ssCharSheet.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslCharSheet, Me.tsslCachedUntilTime})
        Me.ssCharSheet.Location = New System.Drawing.Point(3, 475)
        Me.ssCharSheet.Name = "ssCharSheet"
        Me.ssCharSheet.Size = New System.Drawing.Size(811, 22)
        Me.ssCharSheet.SizingGrip = False
        Me.ssCharSheet.TabIndex = 40
        '
        'tsslCharSheet
        '
        Me.tsslCharSheet.Name = "tsslCharSheet"
        Me.tsslCharSheet.Size = New System.Drawing.Size(78, 17)
        Me.tsslCharSheet.Text = "Cached Until:"
        '
        'tsslCachedUntilTime
        '
        Me.tsslCachedUntilTime.Name = "tsslCachedUntilTime"
        Me.tsslCachedUntilTime.Size = New System.Drawing.Size(0, 17)
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(656, 201)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(101, 16)
        Me.Label18.TabIndex = 39
        Me.Label18.Text = "Time Remaining"
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(632, 201)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(23, 16)
        Me.Label17.TabIndex = 38
        Me.Label17.Text = "Lvl"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(400, 201)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(71, 16)
        Me.Label15.TabIndex = 37
        Me.Label15.Text = "Skill Name"
        '
        'lbSkillLevel
        '
        Me.lbSkillLevel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbSkillLevel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbSkillLevel.FormattingEnabled = True
        Me.lbSkillLevel.ItemHeight = 19
        Me.lbSkillLevel.Location = New System.Drawing.Point(634, 220)
        Me.lbSkillLevel.Name = "lbSkillLevel"
        Me.lbSkillLevel.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.lbSkillLevel.Size = New System.Drawing.Size(22, 251)
        Me.lbSkillLevel.TabIndex = 36
        '
        'lbSkillTimeRemaining
        '
        Me.lbSkillTimeRemaining.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbSkillTimeRemaining.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbSkillTimeRemaining.FormattingEnabled = True
        Me.lbSkillTimeRemaining.ItemHeight = 19
        Me.lbSkillTimeRemaining.Location = New System.Drawing.Point(656, 220)
        Me.lbSkillTimeRemaining.Name = "lbSkillTimeRemaining"
        Me.lbSkillTimeRemaining.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.lbSkillTimeRemaining.Size = New System.Drawing.Size(152, 251)
        Me.lbSkillTimeRemaining.TabIndex = 35
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(400, 182)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(87, 18)
        Me.Label13.TabIndex = 34
        Me.Label13.Text = "Skill Queue"
        '
        'lbSkillQueue
        '
        Me.lbSkillQueue.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbSkillQueue.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbSkillQueue.FormattingEnabled = True
        Me.lbSkillQueue.ItemHeight = 19
        Me.lbSkillQueue.Location = New System.Drawing.Point(403, 220)
        Me.lbSkillQueue.Name = "lbSkillQueue"
        Me.lbSkillQueue.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.lbSkillQueue.Size = New System.Drawing.Size(231, 251)
        Me.lbSkillQueue.TabIndex = 33
        '
        'lblChaImplant
        '
        Me.lblChaImplant.AutoSize = True
        Me.lblChaImplant.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChaImplant.Location = New System.Drawing.Point(686, 111)
        Me.lblChaImplant.Name = "lblChaImplant"
        Me.lblChaImplant.Size = New System.Drawing.Size(60, 19)
        Me.lblChaImplant.TabIndex = 32
        Me.lblChaImplant.Text = "Label2"
        '
        'lblWillImplant
        '
        Me.lblWillImplant.AutoSize = True
        Me.lblWillImplant.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWillImplant.Location = New System.Drawing.Point(686, 93)
        Me.lblWillImplant.Name = "lblWillImplant"
        Me.lblWillImplant.Size = New System.Drawing.Size(60, 19)
        Me.lblWillImplant.TabIndex = 31
        Me.lblWillImplant.Text = "Label2"
        '
        'lblPerImplant
        '
        Me.lblPerImplant.AutoSize = True
        Me.lblPerImplant.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPerImplant.Location = New System.Drawing.Point(686, 75)
        Me.lblPerImplant.Name = "lblPerImplant"
        Me.lblPerImplant.Size = New System.Drawing.Size(60, 19)
        Me.lblPerImplant.TabIndex = 30
        Me.lblPerImplant.Text = "Label2"
        '
        'lblMemImplant
        '
        Me.lblMemImplant.AutoSize = True
        Me.lblMemImplant.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMemImplant.Location = New System.Drawing.Point(686, 57)
        Me.lblMemImplant.Name = "lblMemImplant"
        Me.lblMemImplant.Size = New System.Drawing.Size(60, 19)
        Me.lblMemImplant.TabIndex = 29
        Me.lblMemImplant.Text = "Label2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(673, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 18)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Implant"
        '
        'lblIntImplant
        '
        Me.lblIntImplant.AutoSize = True
        Me.lblIntImplant.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIntImplant.Location = New System.Drawing.Point(686, 39)
        Me.lblIntImplant.Name = "lblIntImplant"
        Me.lblIntImplant.Size = New System.Drawing.Size(27, 19)
        Me.lblIntImplant.TabIndex = 27
        Me.lblIntImplant.Text = "+4"
        '
        'lblCha
        '
        Me.lblCha.AutoSize = True
        Me.lblCha.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCha.Location = New System.Drawing.Point(531, 111)
        Me.lblCha.Name = "lblCha"
        Me.lblCha.Size = New System.Drawing.Size(60, 19)
        Me.lblCha.TabIndex = 26
        Me.lblCha.Text = "Label2"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(402, 109)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(76, 18)
        Me.Label16.TabIndex = 25
        Me.Label16.Text = "Charisma"
        '
        'lblWill
        '
        Me.lblWill.AutoSize = True
        Me.lblWill.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWill.Location = New System.Drawing.Point(531, 93)
        Me.lblWill.Name = "lblWill"
        Me.lblWill.Size = New System.Drawing.Size(60, 19)
        Me.lblWill.TabIndex = 24
        Me.lblWill.Text = "Label2"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(402, 91)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(76, 18)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Willpower"
        '
        'lblPer
        '
        Me.lblPer.AutoSize = True
        Me.lblPer.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPer.Location = New System.Drawing.Point(531, 75)
        Me.lblPer.Name = "lblPer"
        Me.lblPer.Size = New System.Drawing.Size(60, 19)
        Me.lblPer.TabIndex = 22
        Me.lblPer.Text = "Label2"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(402, 73)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(84, 18)
        Me.Label14.TabIndex = 21
        Me.Label14.Text = "Perception"
        '
        'lblMem
        '
        Me.lblMem.AutoSize = True
        Me.lblMem.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMem.Location = New System.Drawing.Point(531, 57)
        Me.lblMem.Name = "lblMem"
        Me.lblMem.Size = New System.Drawing.Size(60, 19)
        Me.lblMem.TabIndex = 20
        Me.lblMem.Text = "Label2"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(402, 55)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(64, 18)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "Memory"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(532, 19)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(120, 18)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Modified (Base)"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(402, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 18)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Attributes"
        '
        'lblInt
        '
        Me.lblInt.AutoSize = True
        Me.lblInt.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInt.Location = New System.Drawing.Point(531, 39)
        Me.lblInt.Name = "lblInt"
        Me.lblInt.Size = New System.Drawing.Size(60, 19)
        Me.lblInt.TabIndex = 16
        Me.lblInt.Text = "Label2"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(402, 37)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(85, 18)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Intelligence"
        '
        'lblBalance
        '
        Me.lblBalance.AutoSize = True
        Me.lblBalance.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalance.Location = New System.Drawing.Point(137, 161)
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(60, 19)
        Me.lblBalance.TabIndex = 14
        Me.lblBalance.Text = "Label2"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 159)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(111, 18)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Wallet Balance"
        '
        'lblCloneInfo
        '
        Me.lblCloneInfo.AutoSize = True
        Me.lblCloneInfo.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCloneInfo.Location = New System.Drawing.Point(137, 130)
        Me.lblCloneInfo.Name = "lblCloneInfo"
        Me.lblCloneInfo.Size = New System.Drawing.Size(60, 19)
        Me.lblCloneInfo.TabIndex = 12
        Me.lblCloneInfo.Text = "Label2"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 130)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 18)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Clone"
        '
        'lblSkillpoints
        '
        Me.lblSkillpoints.AutoSize = True
        Me.lblSkillpoints.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSkillpoints.Location = New System.Drawing.Point(137, 112)
        Me.lblSkillpoints.Name = "lblSkillpoints"
        Me.lblSkillpoints.Size = New System.Drawing.Size(60, 19)
        Me.lblSkillpoints.TabIndex = 10
        Me.lblSkillpoints.Text = "Label2"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(107, 18)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Skillpoint Total"
        '
        'lblGender
        '
        Me.lblGender.AutoSize = True
        Me.lblGender.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGender.Location = New System.Drawing.Point(137, 39)
        Me.lblGender.Name = "lblGender"
        Me.lblGender.Size = New System.Drawing.Size(60, 19)
        Me.lblGender.TabIndex = 8
        Me.lblGender.Text = "Label2"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 18)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Gender"
        '
        'lblCorporation
        '
        Me.lblCorporation.AutoSize = True
        Me.lblCorporation.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCorporation.Location = New System.Drawing.Point(137, 84)
        Me.lblCorporation.Name = "lblCorporation"
        Me.lblCorporation.Size = New System.Drawing.Size(60, 19)
        Me.lblCorporation.TabIndex = 6
        Me.lblCorporation.Text = "Label2"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 18)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Corporation"
        '
        'lblRace
        '
        Me.lblRace.AutoSize = True
        Me.lblRace.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRace.Location = New System.Drawing.Point(137, 58)
        Me.lblRace.Name = "lblRace"
        Me.lblRace.Size = New System.Drawing.Size(60, 19)
        Me.lblRace.TabIndex = 4
        Me.lblRace.Text = "Label2"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 18)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Character Race"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(137, 20)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(60, 19)
        Me.lblName.TabIndex = 2
        Me.lblName.Text = "Label1"
        '
        'Label0
        '
        Me.Label0.AutoSize = True
        Me.Label0.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label0.Location = New System.Drawing.Point(8, 20)
        Me.Label0.Name = "Label0"
        Me.Label0.Size = New System.Drawing.Size(123, 18)
        Me.Label0.TabIndex = 1
        Me.Label0.Text = "Character Name"
        '
        'tabAssets
        '
        Me.tabAssets.Controls.Add(Me.ssAssets)
        Me.tabAssets.Controls.Add(Me.dgvAssets)
        Me.tabAssets.Location = New System.Drawing.Point(4, 22)
        Me.tabAssets.Name = "tabAssets"
        Me.tabAssets.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAssets.Size = New System.Drawing.Size(817, 500)
        Me.tabAssets.TabIndex = 1
        Me.tabAssets.Text = "Assets"
        Me.tabAssets.UseVisualStyleBackColor = True
        '
        'ssAssets
        '
        Me.ssAssets.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslAssets, Me.ToolStripStatusLabel3})
        Me.ssAssets.Location = New System.Drawing.Point(3, 475)
        Me.ssAssets.Name = "ssAssets"
        Me.ssAssets.Size = New System.Drawing.Size(811, 22)
        Me.ssAssets.SizingGrip = False
        Me.ssAssets.TabIndex = 3
        '
        'tsslAssets
        '
        Me.tsslAssets.Name = "tsslAssets"
        Me.tsslAssets.Size = New System.Drawing.Size(78, 17)
        Me.tsslAssets.Text = "Cached Until:"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(0, 17)
        '
        'dgvAssets
        '
        Me.dgvAssets.AllowUserToAddRows = False
        Me.dgvAssets.AllowUserToDeleteRows = False
        Me.dgvAssets.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure
        Me.dgvAssets.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvAssets.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvAssets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvAssets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAssets.Location = New System.Drawing.Point(3, 3)
        Me.dgvAssets.Name = "dgvAssets"
        Me.dgvAssets.ReadOnly = True
        Me.dgvAssets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAssets.Size = New System.Drawing.Size(811, 469)
        Me.dgvAssets.TabIndex = 0
        '
        'tabJournal
        '
        Me.tabJournal.Controls.Add(Me.ssJournal)
        Me.tabJournal.Controls.Add(Me.dgvJournal)
        Me.tabJournal.Location = New System.Drawing.Point(4, 22)
        Me.tabJournal.Name = "tabJournal"
        Me.tabJournal.Padding = New System.Windows.Forms.Padding(3)
        Me.tabJournal.Size = New System.Drawing.Size(817, 500)
        Me.tabJournal.TabIndex = 3
        Me.tabJournal.Text = "Journal"
        Me.tabJournal.UseVisualStyleBackColor = True
        '
        'ssJournal
        '
        Me.ssJournal.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslJournal, Me.ToolStripStatusLabel2})
        Me.ssJournal.Location = New System.Drawing.Point(3, 475)
        Me.ssJournal.Name = "ssJournal"
        Me.ssJournal.Size = New System.Drawing.Size(811, 22)
        Me.ssJournal.SizingGrip = False
        Me.ssJournal.TabIndex = 6
        '
        'tsslJournal
        '
        Me.tsslJournal.Name = "tsslJournal"
        Me.tsslJournal.Size = New System.Drawing.Size(78, 17)
        Me.tsslJournal.Text = "Cached Until:"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(0, 17)
        '
        'dgvJournal
        '
        Me.dgvJournal.AllowUserToAddRows = False
        Me.dgvJournal.AllowUserToDeleteRows = False
        Me.dgvJournal.AllowUserToOrderColumns = True
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Azure
        Me.dgvJournal.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvJournal.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvJournal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvJournal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvJournal.Location = New System.Drawing.Point(3, 3)
        Me.dgvJournal.Name = "dgvJournal"
        Me.dgvJournal.ReadOnly = True
        Me.dgvJournal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvJournal.Size = New System.Drawing.Size(811, 469)
        Me.dgvJournal.TabIndex = 1
        '
        'tabOrders
        '
        Me.tabOrders.Controls.Add(Me.cbActiveOrdersOnly)
        Me.tabOrders.Controls.Add(Me.lblNumOrders)
        Me.tabOrders.Controls.Add(Me.ssOrders)
        Me.tabOrders.Controls.Add(Me.SplitContainer1)
        Me.tabOrders.Location = New System.Drawing.Point(4, 22)
        Me.tabOrders.Name = "tabOrders"
        Me.tabOrders.Padding = New System.Windows.Forms.Padding(3)
        Me.tabOrders.Size = New System.Drawing.Size(817, 500)
        Me.tabOrders.TabIndex = 2
        Me.tabOrders.Text = "Market Orders"
        Me.tabOrders.UseVisualStyleBackColor = True
        '
        'cbActiveOrdersOnly
        '
        Me.cbActiveOrdersOnly.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbActiveOrdersOnly.AutoSize = True
        Me.cbActiveOrdersOnly.Checked = True
        Me.cbActiveOrdersOnly.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbActiveOrdersOnly.Location = New System.Drawing.Point(682, 4)
        Me.cbActiveOrdersOnly.Name = "cbActiveOrdersOnly"
        Me.cbActiveOrdersOnly.Size = New System.Drawing.Size(139, 17)
        Me.cbActiveOrdersOnly.TabIndex = 6
        Me.cbActiveOrdersOnly.Text = "Show active orders only"
        Me.cbActiveOrdersOnly.UseVisualStyleBackColor = True
        '
        'lblNumOrders
        '
        Me.lblNumOrders.AutoSize = True
        Me.lblNumOrders.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumOrders.Location = New System.Drawing.Point(3, 3)
        Me.lblNumOrders.Name = "lblNumOrders"
        Me.lblNumOrders.Size = New System.Drawing.Size(56, 16)
        Me.lblNumOrders.TabIndex = 5
        Me.lblNumOrders.Text = "Label21"
        '
        'ssOrders
        '
        Me.ssOrders.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslOrders, Me.ToolStripStatusLabel5})
        Me.ssOrders.Location = New System.Drawing.Point(3, 475)
        Me.ssOrders.Name = "ssOrders"
        Me.ssOrders.Size = New System.Drawing.Size(811, 22)
        Me.ssOrders.SizingGrip = False
        Me.ssOrders.TabIndex = 4
        '
        'tsslOrders
        '
        Me.tsslOrders.Name = "tsslOrders"
        Me.tsslOrders.Size = New System.Drawing.Size(78, 17)
        Me.tsslOrders.Text = "Cached Until:"
        '
        'ToolStripStatusLabel5
        '
        Me.ToolStripStatusLabel5.Name = "ToolStripStatusLabel5"
        Me.ToolStripStatusLabel5.Size = New System.Drawing.Size(0, 17)
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 22)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSellOrdersTitle)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvOrdersSell)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblBuyOrdersTitle)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvOrdersBuy)
        Me.SplitContainer1.Size = New System.Drawing.Size(811, 461)
        Me.SplitContainer1.SplitterDistance = 216
        Me.SplitContainer1.TabIndex = 3
        '
        'lblSellOrdersTitle
        '
        Me.lblSellOrdersTitle.AutoSize = True
        Me.lblSellOrdersTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSellOrdersTitle.Location = New System.Drawing.Point(0, 1)
        Me.lblSellOrdersTitle.Name = "lblSellOrdersTitle"
        Me.lblSellOrdersTitle.Size = New System.Drawing.Size(86, 16)
        Me.lblSellOrdersTitle.TabIndex = 4
        Me.lblSellOrdersTitle.Text = "Sell Orders"
        '
        'dgvOrdersSell
        '
        Me.dgvOrdersSell.AllowUserToAddRows = False
        Me.dgvOrdersSell.AllowUserToDeleteRows = False
        Me.dgvOrdersSell.AllowUserToOrderColumns = True
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.Azure
        Me.dgvOrdersSell.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvOrdersSell.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvOrdersSell.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvOrdersSell.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOrdersSell.Location = New System.Drawing.Point(0, 17)
        Me.dgvOrdersSell.Name = "dgvOrdersSell"
        Me.dgvOrdersSell.ReadOnly = True
        Me.dgvOrdersSell.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvOrdersSell.Size = New System.Drawing.Size(811, 199)
        Me.dgvOrdersSell.TabIndex = 3
        '
        'lblBuyOrdersTitle
        '
        Me.lblBuyOrdersTitle.AutoSize = True
        Me.lblBuyOrdersTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBuyOrdersTitle.Location = New System.Drawing.Point(5, 0)
        Me.lblBuyOrdersTitle.Name = "lblBuyOrdersTitle"
        Me.lblBuyOrdersTitle.Size = New System.Drawing.Size(85, 16)
        Me.lblBuyOrdersTitle.TabIndex = 5
        Me.lblBuyOrdersTitle.Text = "Buy Orders"
        '
        'dgvOrdersBuy
        '
        Me.dgvOrdersBuy.AllowUserToAddRows = False
        Me.dgvOrdersBuy.AllowUserToDeleteRows = False
        Me.dgvOrdersBuy.AllowUserToOrderColumns = True
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.Azure
        Me.dgvOrdersBuy.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvOrdersBuy.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvOrdersBuy.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvOrdersBuy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOrdersBuy.Location = New System.Drawing.Point(0, 16)
        Me.dgvOrdersBuy.Name = "dgvOrdersBuy"
        Me.dgvOrdersBuy.ReadOnly = True
        Me.dgvOrdersBuy.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvOrdersBuy.Size = New System.Drawing.Size(811, 214)
        Me.dgvOrdersBuy.TabIndex = 2
        '
        'tabTransactions
        '
        Me.tabTransactions.Controls.Add(Me.ssTransactions)
        Me.tabTransactions.Controls.Add(Me.dgvTransactions)
        Me.tabTransactions.Location = New System.Drawing.Point(4, 22)
        Me.tabTransactions.Name = "tabTransactions"
        Me.tabTransactions.Padding = New System.Windows.Forms.Padding(3)
        Me.tabTransactions.Size = New System.Drawing.Size(817, 500)
        Me.tabTransactions.TabIndex = 4
        Me.tabTransactions.Text = "Transactions"
        Me.tabTransactions.UseVisualStyleBackColor = True
        '
        'ssTransactions
        '
        Me.ssTransactions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslTransactions, Me.ToolStripStatusLabel4})
        Me.ssTransactions.Location = New System.Drawing.Point(3, 475)
        Me.ssTransactions.Name = "ssTransactions"
        Me.ssTransactions.Size = New System.Drawing.Size(811, 22)
        Me.ssTransactions.SizingGrip = False
        Me.ssTransactions.TabIndex = 9
        '
        'tsslTransactions
        '
        Me.tsslTransactions.Name = "tsslTransactions"
        Me.tsslTransactions.Size = New System.Drawing.Size(78, 17)
        Me.tsslTransactions.Text = "Cached Until:"
        '
        'ToolStripStatusLabel4
        '
        Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(0, 17)
        '
        'dgvTransactions
        '
        Me.dgvTransactions.AllowUserToAddRows = False
        Me.dgvTransactions.AllowUserToDeleteRows = False
        Me.dgvTransactions.AllowUserToOrderColumns = True
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.Azure
        Me.dgvTransactions.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvTransactions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTransactions.Location = New System.Drawing.Point(3, 3)
        Me.dgvTransactions.Name = "dgvTransactions"
        Me.dgvTransactions.ReadOnly = True
        Me.dgvTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTransactions.Size = New System.Drawing.Size(811, 469)
        Me.dgvTransactions.TabIndex = 7
        '
        'tabIndustry
        '
        Me.tabIndustry.Controls.Add(Me.chkActiveOnly)
        Me.tabIndustry.Controls.Add(Me.dgvIndustry)
        Me.tabIndustry.Controls.Add(Me.ssIndustry)
        Me.tabIndustry.Location = New System.Drawing.Point(4, 22)
        Me.tabIndustry.Name = "tabIndustry"
        Me.tabIndustry.Padding = New System.Windows.Forms.Padding(3)
        Me.tabIndustry.Size = New System.Drawing.Size(817, 500)
        Me.tabIndustry.TabIndex = 5
        Me.tabIndustry.Text = "Industry"
        Me.tabIndustry.UseVisualStyleBackColor = True
        '
        'chkActiveOnly
        '
        Me.chkActiveOnly.AutoSize = True
        Me.chkActiveOnly.Location = New System.Drawing.Point(8, 6)
        Me.chkActiveOnly.Name = "chkActiveOnly"
        Me.chkActiveOnly.Size = New System.Drawing.Size(133, 17)
        Me.chkActiveOnly.TabIndex = 13
        Me.chkActiveOnly.Text = "Show current jobs only"
        Me.chkActiveOnly.UseVisualStyleBackColor = True
        '
        'dgvIndustry
        '
        Me.dgvIndustry.AllowUserToAddRows = False
        Me.dgvIndustry.AllowUserToDeleteRows = False
        Me.dgvIndustry.AllowUserToOrderColumns = True
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.Azure
        Me.dgvIndustry.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvIndustry.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvIndustry.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvIndustry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvIndustry.Location = New System.Drawing.Point(3, 29)
        Me.dgvIndustry.Name = "dgvIndustry"
        Me.dgvIndustry.ReadOnly = True
        Me.dgvIndustry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvIndustry.Size = New System.Drawing.Size(811, 443)
        Me.dgvIndustry.TabIndex = 12
        '
        'ssIndustry
        '
        Me.ssIndustry.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslIndustry, Me.ToolStripStatusLabel8})
        Me.ssIndustry.Location = New System.Drawing.Point(3, 475)
        Me.ssIndustry.Name = "ssIndustry"
        Me.ssIndustry.Size = New System.Drawing.Size(811, 22)
        Me.ssIndustry.SizingGrip = False
        Me.ssIndustry.TabIndex = 11
        '
        'tsslIndustry
        '
        Me.tsslIndustry.Name = "tsslIndustry"
        Me.tsslIndustry.Size = New System.Drawing.Size(78, 17)
        Me.tsslIndustry.Text = "Cached Until:"
        '
        'ToolStripStatusLabel8
        '
        Me.ToolStripStatusLabel8.Name = "ToolStripStatusLabel8"
        Me.ToolStripStatusLabel8.Size = New System.Drawing.Size(0, 17)
        '
        'tabBlueprints
        '
        Me.tabBlueprints.Controls.Add(Me.ssNotifications)
        Me.tabBlueprints.Controls.Add(Me.dgvBlueprints)
        Me.tabBlueprints.Location = New System.Drawing.Point(4, 22)
        Me.tabBlueprints.Name = "tabBlueprints"
        Me.tabBlueprints.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBlueprints.Size = New System.Drawing.Size(817, 500)
        Me.tabBlueprints.TabIndex = 6
        Me.tabBlueprints.Text = "Blueprints"
        Me.tabBlueprints.UseVisualStyleBackColor = True
        '
        'ssNotifications
        '
        Me.ssNotifications.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslNotifications, Me.ToolStripStatusLabel6})
        Me.ssNotifications.Location = New System.Drawing.Point(3, 475)
        Me.ssNotifications.Name = "ssNotifications"
        Me.ssNotifications.Size = New System.Drawing.Size(811, 22)
        Me.ssNotifications.SizingGrip = False
        Me.ssNotifications.TabIndex = 10
        '
        'tsslNotifications
        '
        Me.tsslNotifications.Name = "tsslNotifications"
        Me.tsslNotifications.Size = New System.Drawing.Size(78, 17)
        Me.tsslNotifications.Text = "Cached Until:"
        '
        'ToolStripStatusLabel6
        '
        Me.ToolStripStatusLabel6.Name = "ToolStripStatusLabel6"
        Me.ToolStripStatusLabel6.Size = New System.Drawing.Size(0, 17)
        '
        'dgvBlueprints
        '
        Me.dgvBlueprints.AllowUserToAddRows = False
        Me.dgvBlueprints.AllowUserToDeleteRows = False
        Me.dgvBlueprints.AllowUserToOrderColumns = True
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.Azure
        Me.dgvBlueprints.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvBlueprints.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvBlueprints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvBlueprints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBlueprints.Location = New System.Drawing.Point(3, 3)
        Me.dgvBlueprints.Name = "dgvBlueprints"
        Me.dgvBlueprints.ReadOnly = True
        Me.dgvBlueprints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvBlueprints.Size = New System.Drawing.Size(811, 469)
        Me.dgvBlueprints.TabIndex = 8
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnRefresh.Location = New System.Drawing.Point(375, 556)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 40
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'Label99
        '
        Me.Label99.AutoSize = True
        Me.Label99.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label99.Location = New System.Drawing.Point(8, 182)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(75, 18)
        Me.Label99.TabIndex = 44
        Me.Label99.Text = "Paid Until"
        '
        'lblPaidUntil
        '
        Me.lblPaidUntil.AutoSize = True
        Me.lblPaidUntil.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaidUntil.Location = New System.Drawing.Point(137, 182)
        Me.lblPaidUntil.Name = "lblPaidUntil"
        Me.lblPaidUntil.Size = New System.Drawing.Size(60, 19)
        Me.lblPaidUntil.TabIndex = 45
        Me.lblPaidUntil.Text = "Label2"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(825, 591)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(833, 625)
        Me.Name = "frmMain"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EVE Offline Tracker"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tabCharSheet.ResumeLayout(False)
        Me.tabCharSheet.PerformLayout()
        CType(Me.imgAlliance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgCorp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ssCharSheet.ResumeLayout(False)
        Me.ssCharSheet.PerformLayout()
        Me.tabAssets.ResumeLayout(False)
        Me.tabAssets.PerformLayout()
        Me.ssAssets.ResumeLayout(False)
        Me.ssAssets.PerformLayout()
        CType(Me.dgvAssets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabJournal.ResumeLayout(False)
        Me.tabJournal.PerformLayout()
        Me.ssJournal.ResumeLayout(False)
        Me.ssJournal.PerformLayout()
        CType(Me.dgvJournal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabOrders.ResumeLayout(False)
        Me.tabOrders.PerformLayout()
        Me.ssOrders.ResumeLayout(False)
        Me.ssOrders.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgvOrdersSell, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvOrdersBuy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabTransactions.ResumeLayout(False)
        Me.tabTransactions.PerformLayout()
        Me.ssTransactions.ResumeLayout(False)
        Me.ssTransactions.PerformLayout()
        CType(Me.dgvTransactions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabIndustry.ResumeLayout(False)
        Me.tabIndustry.PerformLayout()
        CType(Me.dgvIndustry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ssIndustry.ResumeLayout(False)
        Me.ssIndustry.PerformLayout()
        Me.tabBlueprints.ResumeLayout(False)
        Me.tabBlueprints.PerformLayout()
        Me.ssNotifications.ResumeLayout(False)
        Me.ssNotifications.PerformLayout()
        CType(Me.dgvBlueprints, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents APIInformationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabCharSheet As System.Windows.Forms.TabPage
    Friend WithEvents tabAssets As System.Windows.Forms.TabPage
    Friend WithEvents Label0 As System.Windows.Forms.Label
    Friend WithEvents lblRace As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblCorporation As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblSkillpoints As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblGender As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblCloneInfo As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblCha As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblWill As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblPer As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblMem As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblInt As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblBalance As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblIntImplant As System.Windows.Forms.Label
    Friend WithEvents lblChaImplant As System.Windows.Forms.Label
    Friend WithEvents lblWillImplant As System.Windows.Forms.Label
    Friend WithEvents lblPerImplant As System.Windows.Forms.Label
    Friend WithEvents lblMemImplant As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lbSkillQueue As System.Windows.Forms.ListBox
    Friend WithEvents lbSkillTimeRemaining As System.Windows.Forms.ListBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lbSkillLevel As System.Windows.Forms.ListBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents dgvAssets As System.Windows.Forms.DataGridView
    Friend WithEvents tabOrders As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblSellOrdersTitle As System.Windows.Forms.Label
    Friend WithEvents dgvOrdersSell As System.Windows.Forms.DataGridView
    Friend WithEvents lblBuyOrdersTitle As System.Windows.Forms.Label
    Friend WithEvents dgvOrdersBuy As System.Windows.Forms.DataGridView
    Friend WithEvents ssCharSheet As System.Windows.Forms.StatusStrip
    Friend WithEvents tsslCharSheet As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsslCachedUntilTime As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ssAssets As System.Windows.Forms.StatusStrip
    Friend WithEvents tsslAssets As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ssOrders As System.Windows.Forms.StatusStrip
    Friend WithEvents tsslOrders As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel5 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tabJournal As System.Windows.Forms.TabPage
    Friend WithEvents dgvJournal As System.Windows.Forms.DataGridView
    Friend WithEvents ssJournal As System.Windows.Forms.StatusStrip
    Friend WithEvents tsslJournal As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents picImage As System.Windows.Forms.PictureBox
    Friend WithEvents tabTransactions As System.Windows.Forms.TabPage
    Friend WithEvents dgvTransactions As System.Windows.Forms.DataGridView
    Friend WithEvents ssTransactions As System.Windows.Forms.StatusStrip
    Friend WithEvents tsslTransactions As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tabIndustry As System.Windows.Forms.TabPage
    Friend WithEvents dgvIndustry As System.Windows.Forms.DataGridView
    Friend WithEvents ssIndustry As System.Windows.Forms.StatusStrip
    Friend WithEvents tsslIndustry As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel8 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tabBlueprints As System.Windows.Forms.TabPage
    Friend WithEvents ssNotifications As System.Windows.Forms.StatusStrip
    Friend WithEvents tsslNotifications As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel6 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents dgvBlueprints As System.Windows.Forms.DataGridView
    Friend WithEvents chkActiveOnly As System.Windows.Forms.CheckBox
    Friend WithEvents lblNumOrders As System.Windows.Forms.Label
    Friend WithEvents cbActiveOrdersOnly As System.Windows.Forms.CheckBox
    Friend WithEvents imgAlliance As System.Windows.Forms.PictureBox
    Friend WithEvents imgCorp As System.Windows.Forms.PictureBox
    Friend WithEvents lblPaidUntil As System.Windows.Forms.Label
    Friend WithEvents Label99 As System.Windows.Forms.Label

End Class
