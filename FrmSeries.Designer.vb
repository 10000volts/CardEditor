<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSeries
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
        Me.lbxName = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'lbxName
        '
        Me.lbxName.FormattingEnabled = True
        Me.lbxName.IntegralHeight = False
        Me.lbxName.ItemHeight = 15
        Me.lbxName.Items.AddRange(New Object() {"(双击新建)"})
        Me.lbxName.Location = New System.Drawing.Point(16, 15)
        Me.lbxName.Margin = New System.Windows.Forms.Padding(4)
        Me.lbxName.Name = "lbxName"
        Me.lbxName.Size = New System.Drawing.Size(345, 295)
        Me.lbxName.TabIndex = 2
        '
        'FrmSeries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(379, 326)
        Me.Controls.Add(Me.lbxName)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FrmSeries"
        Me.Text = "势力/系列选择..."
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lbxName As System.Windows.Forms.ListBox
End Class
