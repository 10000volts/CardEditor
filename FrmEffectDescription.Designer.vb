<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEffectDescription
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
        Me.tbxContent = New System.Windows.Forms.TextBox()
        Me.ButLabel = New System.Windows.Forms.Button()
        Me.ButSave = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'tbxContent
        '
        Me.tbxContent.Location = New System.Drawing.Point(16, 15)
        Me.tbxContent.Margin = New System.Windows.Forms.Padding(4)
        Me.tbxContent.Multiline = True
        Me.tbxContent.Name = "tbxContent"
        Me.tbxContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbxContent.Size = New System.Drawing.Size(345, 261)
        Me.tbxContent.TabIndex = 2
        '
        'ButLabel
        '
        Me.ButLabel.Location = New System.Drawing.Point(15, 284)
        Me.ButLabel.Margin = New System.Windows.Forms.Padding(4)
        Me.ButLabel.Name = "ButLabel"
        Me.ButLabel.Size = New System.Drawing.Size(89, 29)
        Me.ButLabel.TabIndex = 4
        Me.ButLabel.Text = "标 签"
        Me.ButLabel.UseVisualStyleBackColor = True
        '
        'ButSave
        '
        Me.ButSave.Location = New System.Drawing.Point(112, 284)
        Me.ButSave.Margin = New System.Windows.Forms.Padding(4)
        Me.ButSave.Name = "ButSave"
        Me.ButSave.Size = New System.Drawing.Size(249, 29)
        Me.ButSave.TabIndex = 5
        Me.ButSave.Text = "保 存 效 果 / 标 签"
        Me.ButSave.UseVisualStyleBackColor = True
        '
        'FrmEffectDescription
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(379, 326)
        Me.Controls.Add(Me.ButSave)
        Me.Controls.Add(Me.ButLabel)
        Me.Controls.Add(Me.tbxContent)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FrmEffectDescription"
        Me.Text = "卡片效果/标签编辑..."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbxContent As System.Windows.Forms.TextBox
    Friend WithEvents ButLabel As System.Windows.Forms.Button
    Friend WithEvents ButSave As System.Windows.Forms.Button
End Class
