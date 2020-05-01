<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ButSearch = New System.Windows.Forms.Button()
        Me.LbxCards = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblID = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblSpecies = New System.Windows.Forms.Label()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.ButNew = New System.Windows.Forms.Button()
        Me.lblRarity = New System.Windows.Forms.Label()
        Me.lblRank = New System.Windows.Forms.Label()
        Me.LblCamp = New System.Windows.Forms.Label()
        Me.LblPool = New System.Windows.Forms.Label()
        Me.TxtID = New System.Windows.Forms.TextBox()
        Me.LblEffect = New System.Windows.Forms.Label()
        Me.LblDesc = New System.Windows.Forms.Label()
        Me.CbxType = New System.Windows.Forms.ComboBox()
        Me.CbxSpecies = New System.Windows.Forms.ComboBox()
        Me.CbxRank = New System.Windows.Forms.ComboBox()
        Me.ButSaveCard = New System.Windows.Forms.Button()
        Me.ButClear = New System.Windows.Forms.Button()
        Me.ButSaveAll = New System.Windows.Forms.Button()
        Me.ButRemove = New System.Windows.Forms.Button()
        Me.ButDeck = New System.Windows.Forms.Button()
        Me.ButHelp = New System.Windows.Forms.Button()
        Me.ButLoad = New System.Windows.Forms.Button()
        Me.ButImport = New System.Windows.Forms.Button()
        Me.ButExport = New System.Windows.Forms.Button()
        Me.LblSeries = New System.Windows.Forms.Label()
        Me.CbxLimit = New System.Windows.Forms.ComboBox()
        Me.lblATK = New System.Windows.Forms.Label()
        Me.TxtAtk = New System.Windows.Forms.TextBox()
        Me.TxtDef = New System.Windows.Forms.TextBox()
        Me.lblDef = New System.Windows.Forms.Label()
        Me.ButNet = New System.Windows.Forms.Button()
        Me.ButModeSwitch = New System.Windows.Forms.Button()
        Me.PicCard = New System.Windows.Forms.PictureBox()
        Me.ButDeveloperMode = New System.Windows.Forms.Button()
        Me.lblCurtain = New System.Windows.Forms.Label()
        Me.LblForbiddenGroup = New System.Windows.Forms.Label()
        CType(Me.PicCard, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButSearch
        '
        Me.ButSearch.Location = New System.Drawing.Point(12, 12)
        Me.ButSearch.Name = "ButSearch"
        Me.ButSearch.Size = New System.Drawing.Size(242, 25)
        Me.ButSearch.TabIndex = 0
        Me.ButSearch.Text = "搜索满足条件的卡片..."
        Me.ButSearch.UseVisualStyleBackColor = True
        '
        'LbxCards
        '
        Me.LbxCards.BackColor = System.Drawing.SystemColors.Window
        Me.LbxCards.FormattingEnabled = True
        Me.LbxCards.HorizontalScrollbar = True
        Me.LbxCards.ItemHeight = 15
        Me.LbxCards.Location = New System.Drawing.Point(12, 43)
        Me.LbxCards.Name = "LbxCards"
        Me.LbxCards.ScrollAlwaysVisible = True
        Me.LbxCards.Size = New System.Drawing.Size(242, 469)
        Me.LbxCards.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(274, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "卡名:"
        '
        'LblID
        '
        Me.LblID.AutoSize = True
        Me.LblID.Location = New System.Drawing.Point(274, 53)
        Me.LblID.Name = "LblID"
        Me.LblID.Size = New System.Drawing.Size(46, 15)
        Me.LblID.TabIndex = 3
        Me.LblID.Text = "卡ID:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(274, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "卡种类:"
        '
        'lblSpecies
        '
        Me.lblSpecies.AutoSize = True
        Me.lblSpecies.Location = New System.Drawing.Point(428, 110)
        Me.lblSpecies.Name = "lblSpecies"
        Me.lblSpecies.Size = New System.Drawing.Size(75, 15)
        Me.lblSpecies.TabIndex = 5
        Me.lblSpecies.Text = "二级种类:"
        '
        'TxtName
        '
        Me.TxtName.Location = New System.Drawing.Point(325, 12)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(258, 25)
        Me.TxtName.TabIndex = 6
        '
        'ButNew
        '
        Me.ButNew.Location = New System.Drawing.Point(601, 12)
        Me.ButNew.Name = "ButNew"
        Me.ButNew.Size = New System.Drawing.Size(139, 94)
        Me.ButNew.TabIndex = 7
        Me.ButNew.Text = "新 建 卡 片 库"
        Me.ButNew.UseVisualStyleBackColor = True
        '
        'lblRarity
        '
        Me.lblRarity.AutoSize = True
        Me.lblRarity.Location = New System.Drawing.Point(274, 153)
        Me.lblRarity.Name = "lblRarity"
        Me.lblRarity.Size = New System.Drawing.Size(45, 15)
        Me.lblRarity.TabIndex = 8
        Me.lblRarity.Text = "等级:"
        '
        'lblRank
        '
        Me.lblRank.AutoSize = True
        Me.lblRank.Location = New System.Drawing.Point(428, 153)
        Me.lblRank.Name = "lblRank"
        Me.lblRank.Size = New System.Drawing.Size(45, 15)
        Me.lblRank.TabIndex = 10
        Me.lblRank.Text = "限制:"
        '
        'LblCamp
        '
        Me.LblCamp.AutoSize = True
        Me.LblCamp.Location = New System.Drawing.Point(274, 241)
        Me.LblCamp.Name = "LblCamp"
        Me.LblCamp.Size = New System.Drawing.Size(127, 15)
        Me.LblCamp.TabIndex = 11
        Me.LblCamp.Text = "点击编辑所属势力"
        '
        'LblPool
        '
        Me.LblPool.AutoSize = True
        Me.LblPool.Location = New System.Drawing.Point(428, 241)
        Me.LblPool.Name = "LblPool"
        Me.LblPool.Size = New System.Drawing.Size(127, 15)
        Me.LblPool.TabIndex = 12
        Me.LblPool.Text = "点击编辑所属卡池"
        '
        'TxtID
        '
        Me.TxtID.Location = New System.Drawing.Point(325, 50)
        Me.TxtID.Name = "TxtID"
        Me.TxtID.Size = New System.Drawing.Size(258, 25)
        Me.TxtID.TabIndex = 13
        '
        'LblEffect
        '
        Me.LblEffect.AutoSize = True
        Me.LblEffect.Location = New System.Drawing.Point(274, 278)
        Me.LblEffect.Name = "LblEffect"
        Me.LblEffect.Size = New System.Drawing.Size(135, 15)
        Me.LblEffect.TabIndex = 15
        Me.LblEffect.Text = "点击编辑效果/标签"
        '
        'LblDesc
        '
        Me.LblDesc.AutoSize = True
        Me.LblDesc.Location = New System.Drawing.Point(274, 314)
        Me.LblDesc.Name = "LblDesc"
        Me.LblDesc.Size = New System.Drawing.Size(97, 15)
        Me.LblDesc.TabIndex = 16
        Me.LblDesc.Text = "点击编辑描述"
        '
        'CbxType
        '
        Me.CbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbxType.FormattingEnabled = True
        Me.CbxType.Items.AddRange(New Object() {" ", "领袖", "雇员", "策略"})
        Me.CbxType.Location = New System.Drawing.Point(340, 107)
        Me.CbxType.Name = "CbxType"
        Me.CbxType.Size = New System.Drawing.Size(82, 23)
        Me.CbxType.TabIndex = 17
        '
        'CbxSpecies
        '
        Me.CbxSpecies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbxSpecies.FormattingEnabled = True
        Me.CbxSpecies.Location = New System.Drawing.Point(501, 107)
        Me.CbxSpecies.Name = "CbxSpecies"
        Me.CbxSpecies.Size = New System.Drawing.Size(82, 23)
        Me.CbxSpecies.TabIndex = 18
        '
        'CbxRank
        '
        Me.CbxRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbxRank.FormattingEnabled = True
        Me.CbxRank.Items.AddRange(New Object() {" ", "普通", "优质", "王牌"})
        Me.CbxRank.Location = New System.Drawing.Point(340, 149)
        Me.CbxRank.Name = "CbxRank"
        Me.CbxRank.Size = New System.Drawing.Size(82, 23)
        Me.CbxRank.TabIndex = 19
        '
        'ButSaveCard
        '
        Me.ButSaveCard.Location = New System.Drawing.Point(277, 443)
        Me.ButSaveCard.Name = "ButSaveCard"
        Me.ButSaveCard.Size = New System.Drawing.Size(145, 25)
        Me.ButSaveCard.TabIndex = 20
        Me.ButSaveCard.Text = "保存卡片至库"
        Me.ButSaveCard.UseVisualStyleBackColor = True
        '
        'ButClear
        '
        Me.ButClear.Location = New System.Drawing.Point(438, 443)
        Me.ButClear.Name = "ButClear"
        Me.ButClear.Size = New System.Drawing.Size(145, 25)
        Me.ButClear.TabIndex = 21
        Me.ButClear.Text = "清空数据"
        Me.ButClear.UseVisualStyleBackColor = True
        '
        'ButSaveAll
        '
        Me.ButSaveAll.Location = New System.Drawing.Point(277, 483)
        Me.ButSaveAll.Name = "ButSaveAll"
        Me.ButSaveAll.Size = New System.Drawing.Size(145, 25)
        Me.ButSaveAll.TabIndex = 22
        Me.ButSaveAll.Text = "保存卡片库"
        Me.ButSaveAll.UseVisualStyleBackColor = True
        '
        'ButRemove
        '
        Me.ButRemove.Location = New System.Drawing.Point(438, 483)
        Me.ButRemove.Name = "ButRemove"
        Me.ButRemove.Size = New System.Drawing.Size(145, 25)
        Me.ButRemove.TabIndex = 23
        Me.ButRemove.Text = "从卡片库移除"
        Me.ButRemove.UseVisualStyleBackColor = True
        '
        'ButDeck
        '
        Me.ButDeck.Location = New System.Drawing.Point(277, 403)
        Me.ButDeck.Name = "ButDeck"
        Me.ButDeck.Size = New System.Drawing.Size(187, 25)
        Me.ButDeck.TabIndex = 24
        Me.ButDeck.Text = "卡 组 编 辑"
        Me.ButDeck.UseVisualStyleBackColor = True
        '
        'ButHelp
        '
        Me.ButHelp.Location = New System.Drawing.Point(479, 403)
        Me.ButHelp.Name = "ButHelp"
        Me.ButHelp.Size = New System.Drawing.Size(104, 25)
        Me.ButHelp.TabIndex = 25
        Me.ButHelp.Text = "帮 助"
        Me.ButHelp.UseVisualStyleBackColor = True
        '
        'ButLoad
        '
        Me.ButLoad.Location = New System.Drawing.Point(601, 114)
        Me.ButLoad.Name = "ButLoad"
        Me.ButLoad.Size = New System.Drawing.Size(139, 94)
        Me.ButLoad.TabIndex = 26
        Me.ButLoad.Text = "读 取 卡 片 库"
        Me.ButLoad.UseVisualStyleBackColor = True
        '
        'ButImport
        '
        Me.ButImport.Location = New System.Drawing.Point(601, 214)
        Me.ButImport.Name = "ButImport"
        Me.ButImport.Size = New System.Drawing.Size(139, 94)
        Me.ButImport.TabIndex = 27
        Me.ButImport.Text = "导 入 卡 片 库"
        Me.ButImport.UseVisualStyleBackColor = True
        '
        'ButExport
        '
        Me.ButExport.Location = New System.Drawing.Point(601, 314)
        Me.ButExport.Name = "ButExport"
        Me.ButExport.Size = New System.Drawing.Size(139, 94)
        Me.ButExport.TabIndex = 28
        Me.ButExport.Text = "导 出 卡 片 库"
        Me.ButExport.UseVisualStyleBackColor = True
        '
        'LblSeries
        '
        Me.LblSeries.AutoSize = True
        Me.LblSeries.Location = New System.Drawing.Point(428, 278)
        Me.LblSeries.Name = "LblSeries"
        Me.LblSeries.Size = New System.Drawing.Size(127, 15)
        Me.LblSeries.TabIndex = 36
        Me.LblSeries.Text = "点击编辑所属系列"
        '
        'CbxLimit
        '
        Me.CbxLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbxLimit.FormattingEnabled = True
        Me.CbxLimit.Items.AddRange(New Object() {" ", "无限制", "限制", "禁止", "衍生"})
        Me.CbxLimit.Location = New System.Drawing.Point(501, 150)
        Me.CbxLimit.Name = "CbxLimit"
        Me.CbxLimit.Size = New System.Drawing.Size(82, 23)
        Me.CbxLimit.TabIndex = 37
        '
        'lblATK
        '
        Me.lblATK.AutoSize = True
        Me.lblATK.Location = New System.Drawing.Point(274, 193)
        Me.lblATK.Name = "lblATK"
        Me.lblATK.Size = New System.Drawing.Size(63, 15)
        Me.lblATK.TabIndex = 38
        Me.lblATK.Text = "ATK/EFF"
        '
        'TxtAtk
        '
        Me.TxtAtk.Location = New System.Drawing.Point(340, 190)
        Me.TxtAtk.Name = "TxtAtk"
        Me.TxtAtk.Size = New System.Drawing.Size(82, 25)
        Me.TxtAtk.TabIndex = 39
        '
        'TxtDef
        '
        Me.TxtDef.Location = New System.Drawing.Point(501, 190)
        Me.TxtDef.Name = "TxtDef"
        Me.TxtDef.Size = New System.Drawing.Size(82, 25)
        Me.TxtDef.TabIndex = 41
        '
        'lblDef
        '
        Me.lblDef.AutoSize = True
        Me.lblDef.Location = New System.Drawing.Point(430, 193)
        Me.lblDef.Name = "lblDef"
        Me.lblDef.Size = New System.Drawing.Size(39, 15)
        Me.lblDef.TabIndex = 40
        Me.lblDef.Text = "DEF:"
        '
        'ButNet
        '
        Me.ButNet.Location = New System.Drawing.Point(601, 414)
        Me.ButNet.Name = "ButNet"
        Me.ButNet.Size = New System.Drawing.Size(139, 94)
        Me.ButNet.TabIndex = 43
        Me.ButNet.Text = "获 取 联 机 库"
        Me.ButNet.UseVisualStyleBackColor = True
        '
        'ButModeSwitch
        '
        Me.ButModeSwitch.Location = New System.Drawing.Point(277, 362)
        Me.ButModeSwitch.Name = "ButModeSwitch"
        Me.ButModeSwitch.Size = New System.Drawing.Size(306, 25)
        Me.ButModeSwitch.TabIndex = 44
        Me.ButModeSwitch.Text = "切换至浏览者模式"
        Me.ButModeSwitch.UseVisualStyleBackColor = True
        '
        'PicCard
        '
        Me.PicCard.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.PicCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicCard.Location = New System.Drawing.Point(277, 12)
        Me.PicCard.Name = "PicCard"
        Me.PicCard.Size = New System.Drawing.Size(300, 450)
        Me.PicCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicCard.TabIndex = 45
        Me.PicCard.TabStop = False
        Me.PicCard.Visible = False
        '
        'ButDeveloperMode
        '
        Me.ButDeveloperMode.Location = New System.Drawing.Point(277, 483)
        Me.ButDeveloperMode.Name = "ButDeveloperMode"
        Me.ButDeveloperMode.Size = New System.Drawing.Size(306, 25)
        Me.ButDeveloperMode.TabIndex = 46
        Me.ButDeveloperMode.Text = "切换至开发者模式(右键启用编辑)"
        Me.ButDeveloperMode.UseVisualStyleBackColor = True
        Me.ButDeveloperMode.Visible = False
        '
        'lblCurtain
        '
        Me.lblCurtain.Location = New System.Drawing.Point(274, 9)
        Me.lblCurtain.Name = "lblCurtain"
        Me.lblCurtain.Size = New System.Drawing.Size(321, 459)
        Me.lblCurtain.TabIndex = 47
        Me.lblCurtain.Visible = False
        '
        'LblForbiddenGroup
        '
        Me.LblForbiddenGroup.AutoSize = True
        Me.LblForbiddenGroup.BackColor = System.Drawing.SystemColors.Info
        Me.LblForbiddenGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblForbiddenGroup.Location = New System.Drawing.Point(428, 314)
        Me.LblForbiddenGroup.Name = "LblForbiddenGroup"
        Me.LblForbiddenGroup.Size = New System.Drawing.Size(129, 17)
        Me.LblForbiddenGroup.TabIndex = 48
        Me.LblForbiddenGroup.Text = "点击编辑禁止组合"
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(758, 520)
        Me.Controls.Add(Me.lblCurtain)
        Me.Controls.Add(Me.PicCard)
        Me.Controls.Add(Me.LblForbiddenGroup)
        Me.Controls.Add(Me.ButDeveloperMode)
        Me.Controls.Add(Me.ButModeSwitch)
        Me.Controls.Add(Me.ButNet)
        Me.Controls.Add(Me.TxtDef)
        Me.Controls.Add(Me.lblDef)
        Me.Controls.Add(Me.TxtAtk)
        Me.Controls.Add(Me.lblATK)
        Me.Controls.Add(Me.CbxLimit)
        Me.Controls.Add(Me.LblSeries)
        Me.Controls.Add(Me.ButExport)
        Me.Controls.Add(Me.ButImport)
        Me.Controls.Add(Me.ButLoad)
        Me.Controls.Add(Me.ButHelp)
        Me.Controls.Add(Me.ButDeck)
        Me.Controls.Add(Me.ButRemove)
        Me.Controls.Add(Me.ButSaveAll)
        Me.Controls.Add(Me.ButClear)
        Me.Controls.Add(Me.ButSaveCard)
        Me.Controls.Add(Me.CbxRank)
        Me.Controls.Add(Me.CbxSpecies)
        Me.Controls.Add(Me.CbxType)
        Me.Controls.Add(Me.LblDesc)
        Me.Controls.Add(Me.LblEffect)
        Me.Controls.Add(Me.TxtID)
        Me.Controls.Add(Me.LblPool)
        Me.Controls.Add(Me.LblCamp)
        Me.Controls.Add(Me.lblRank)
        Me.Controls.Add(Me.lblRarity)
        Me.Controls.Add(Me.ButNew)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.lblSpecies)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LblID)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LbxCards)
        Me.Controls.Add(Me.ButSearch)
        Me.Name = "FrmMain"
        Me.Text = "-"
        CType(Me.PicCard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ButSearch As Windows.Forms.Button
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents LblID As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents lblSpecies As Windows.Forms.Label
    Friend WithEvents TxtName As Windows.Forms.TextBox
    Friend WithEvents ButNew As Windows.Forms.Button
    Friend WithEvents lblRarity As Windows.Forms.Label
    Friend WithEvents lblRank As Windows.Forms.Label
    Friend WithEvents LblCamp As Windows.Forms.Label
    Friend WithEvents LblPool As Windows.Forms.Label
    Friend WithEvents TxtID As Windows.Forms.TextBox
    Friend WithEvents LblEffect As Windows.Forms.Label
    Friend WithEvents LblDesc As Windows.Forms.Label
    Friend WithEvents CbxType As Windows.Forms.ComboBox
    Friend WithEvents CbxSpecies As Windows.Forms.ComboBox
    Friend WithEvents CbxRank As Windows.Forms.ComboBox
    Friend WithEvents ButSaveCard As Windows.Forms.Button
    Friend WithEvents ButClear As Windows.Forms.Button
    Friend WithEvents ButSaveAll As Windows.Forms.Button
    Friend WithEvents ButRemove As Windows.Forms.Button
    Friend WithEvents ButDeck As Windows.Forms.Button
    Friend WithEvents ButHelp As Windows.Forms.Button
    Friend WithEvents ButLoad As Windows.Forms.Button
    Friend WithEvents ButImport As Windows.Forms.Button
    Friend WithEvents ButExport As Windows.Forms.Button
    Friend WithEvents LblSeries As Windows.Forms.Label
    Friend WithEvents CbxLimit As Windows.Forms.ComboBox
    Friend WithEvents lblATK As Windows.Forms.Label
    Friend WithEvents TxtAtk As Windows.Forms.TextBox
    Friend WithEvents TxtDef As Windows.Forms.TextBox
    Friend WithEvents lblDef As Windows.Forms.Label
    Friend WithEvents ButNet As Windows.Forms.Button
    Friend WithEvents ButModeSwitch As Windows.Forms.Button
    Friend WithEvents PicCard As Windows.Forms.PictureBox
    Friend WithEvents ButDeveloperMode As Windows.Forms.Button
    Friend WithEvents lblCurtain As Windows.Forms.Label
    Friend WithEvents LbxCards As Windows.Forms.ListBox
    Friend WithEvents LblForbiddenGroup As Windows.Forms.Label
End Class
