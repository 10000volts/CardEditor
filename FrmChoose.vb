Imports System.Collections.Generic

Friend Class FrmChoose
    Friend Shared Function CallFrmChoose(ByRef cl As List(Of Card)) As List(Of Card)
        Dim f As New FrmChoose(cl)
        f.ShowDialog()
        Return f.Result
    End Function
    Private Result As New List(Of Card)
    Sub New(ByRef cl As List(Of Card))
        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        For Each one In cl
            ListMain.Items.Add(one)
        Next
    End Sub
    Private Sub ButNew_Click(sender As Object, e As EventArgs) Handles ButNew.Click
        For Each one In ListMain.SelectedItems
            Result.Add(one)
        Next
        Close()
    End Sub

    Private Sub FrmChoose_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class