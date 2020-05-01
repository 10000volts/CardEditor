<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDeck
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LbxMain = New System.Windows.Forms.ListBox()
        Me.LbxSide = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButOpen = New System.Windows.Forms.Button()
        Me.ButSave = New System.Windows.Forms.Button()
        Me.ButSaveAs = New System.Windows.Forms.Button()
        Me.ButCopy = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(166, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "主卡组：(双击移除1张)"
        '
        'LbxMain
        '
        Me.LbxMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.LbxMain.FormattingEnabled = True
        Me.LbxMain.HorizontalScrollbar = True
        Me.LbxMain.ItemHeight = 15
        Me.LbxMain.Location = New System.Drawing.Point(16, 44)
        Me.LbxMain.Name = "LbxMain"
        Me.LbxMain.ScrollAlwaysVisible = True
        Me.LbxMain.Size = New System.Drawing.Size(242, 469)
        Me.LbxMain.TabIndex = 2
        '
        'LbxSide
        '
        Me.LbxSide.BackColor = System.Drawing.Color.White
        Me.LbxSide.FormattingEnabled = True
        Me.LbxSide.HorizontalScrollbar = True
        Me.LbxSide.ItemHeight = 15
        Me.LbxSide.Location = New System.Drawing.Point(279, 44)
        Me.LbxSide.Name = "LbxSide"
        Me.LbxSide.ScrollAlwaysVisible = True
        Me.LbxSide.Size = New System.Drawing.Size(242, 469)
        Me.LbxSide.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(276, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(166, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "副卡组：(双击移除1张)"
        '
        'ButOpen
        '
        Me.ButOpen.Location = New System.Drawing.Point(540, 12)
        Me.ButOpen.Name = "ButOpen"
        Me.ButOpen.Size = New System.Drawing.Size(139, 94)
        Me.ButOpen.TabIndex = 8
        Me.ButOpen.Text = "打开现有卡组"
        Me.ButOpen.UseVisualStyleBackColor = True
        '
        'ButSave
        '
        Me.ButSave.Location = New System.Drawing.Point(540, 123)
        Me.ButSave.Name = "ButSave"
        Me.ButSave.Size = New System.Drawing.Size(139, 94)
        Me.ButSave.TabIndex = 9
        Me.ButSave.Text = "保存卡组"
        Me.ButSave.UseVisualStyleBackColor = True
        '
        'ButSaveAs
        '
        Me.ButSaveAs.Location = New System.Drawing.Point(540, 235)
        Me.ButSaveAs.Name = "ButSaveAs"
        Me.ButSaveAs.Size = New System.Drawing.Size(139, 94)
        Me.ButSaveAs.TabIndex = 10
        Me.ButSaveAs.Text = "卡组另存为"
        Me.ButSaveAs.UseVisualStyleBackColor = True
        '
        'ButCopy
        '
        Me.ButCopy.Location = New System.Drawing.Point(540, 419)
        Me.ButCopy.Name = "ButCopy"
        Me.ButCopy.Size = New System.Drawing.Size(139, 94)
        Me.ButCopy.TabIndex = 11
        Me.ButCopy.Text = "复制到剪贴板"
        Me.ButCopy.UseVisualStyleBackColor = True
        '
        'FrmDeck
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(699, 527)
        Me.Controls.Add(Me.ButCopy)
        Me.Controls.Add(Me.ButSaveAs)
        Me.Controls.Add(Me.ButSave)
        Me.Controls.Add(Me.ButOpen)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LbxSide)
        Me.Controls.Add(Me.LbxMain)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmDeck"
        Me.Text = "卡组编辑 ╭(●｀∀´●)╯ 单击列表可以切换主/副卡组"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents LbxMain As Windows.Forms.ListBox
    Friend WithEvents LbxSide As Windows.Forms.ListBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents ButOpen As Windows.Forms.Button
    Friend WithEvents ButSave As Windows.Forms.Button
    Friend WithEvents ButSaveAs As Windows.Forms.Button
    Friend WithEvents ButCopy As Windows.Forms.Button
End Class
