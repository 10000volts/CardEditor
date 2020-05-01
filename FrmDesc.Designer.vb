<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDesc
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
        Me.TxtDesc = New System.Windows.Forms.TextBox()
        Me.butOK = New System.Windows.Forms.Button()
        Me.ButCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TxtDesc
        '
        Me.TxtDesc.Location = New System.Drawing.Point(12, 12)
        Me.TxtDesc.Multiline = True
        Me.TxtDesc.Name = "TxtDesc"
        Me.TxtDesc.Size = New System.Drawing.Size(605, 426)
        Me.TxtDesc.TabIndex = 0
        '
        'butOK
        '
        Me.butOK.Location = New System.Drawing.Point(638, 12)
        Me.butOK.Name = "butOK"
        Me.butOK.Size = New System.Drawing.Size(150, 79)
        Me.butOK.TabIndex = 2
        Me.butOK.Text = "完 成"
        Me.butOK.UseVisualStyleBackColor = True
        '
        'ButCancel
        '
        Me.ButCancel.Location = New System.Drawing.Point(638, 112)
        Me.ButCancel.Name = "ButCancel"
        Me.ButCancel.Size = New System.Drawing.Size(150, 79)
        Me.ButCancel.TabIndex = 3
        Me.ButCancel.Text = "取 消"
        Me.ButCancel.UseVisualStyleBackColor = True
        '
        'FrmDesc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.ButCancel)
        Me.Controls.Add(Me.butOK)
        Me.Controls.Add(Me.TxtDesc)
        Me.Name = "FrmDesc"
        Me.Text = "卡片描述 (:3[▓▓]"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtDesc As Windows.Forms.TextBox
    Friend WithEvents butOK As Windows.Forms.Button
    Friend WithEvents ButCancel As Windows.Forms.Button
End Class
