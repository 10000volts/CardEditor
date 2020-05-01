<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmChoose
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
        Me.ListMain = New System.Windows.Forms.ListBox()
        Me.ButNew = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ListMain
        '
        Me.ListMain.FormattingEnabled = True
        Me.ListMain.HorizontalScrollbar = True
        Me.ListMain.IntegralHeight = False
        Me.ListMain.ItemHeight = 15
        Me.ListMain.Location = New System.Drawing.Point(16, 15)
        Me.ListMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ListMain.Name = "ListMain"
        Me.ListMain.ScrollAlwaysVisible = True
        Me.ListMain.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListMain.Size = New System.Drawing.Size(279, 574)
        Me.ListMain.TabIndex = 47
        '
        'ButNew
        '
        Me.ButNew.Location = New System.Drawing.Point(16, 598)
        Me.ButNew.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ButNew.Name = "ButNew"
        Me.ButNew.Size = New System.Drawing.Size(280, 89)
        Me.ButNew.TabIndex = 48
        Me.ButNew.Text = "确 认 导 出"
        Me.ButNew.UseVisualStyleBackColor = True
        '
        'FrmChoose
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(312, 701)
        Me.Controls.Add(Me.ButNew)
        Me.Controls.Add(Me.ListMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "FrmChoose"
        Me.Text = "FrmChoose"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListMain As System.Windows.Forms.ListBox
    Friend WithEvents ButNew As System.Windows.Forms.Button
End Class
