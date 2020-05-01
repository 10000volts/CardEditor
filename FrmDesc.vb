Public Class FrmDesc
    Sub New(ByVal desc As String)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        If desc IsNot Nothing Then
            TempDesc = desc.Clone()
            TxtDesc.Text = TempDesc
        End If
    End Sub

    Friend ResultValid As Boolean = False
    Friend TempDesc As String = ""

    Private Sub butOK_Click(sender As Object, e As EventArgs) Handles butOK.Click
        TempDesc = TxtDesc.Text
        ResultValid = True
        Close()
    End Sub

    Private Sub ButCancel_Click(sender As Object, e As EventArgs) Handles ButCancel.Click
        Close()
    End Sub

    Private Sub FrmDesc_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class