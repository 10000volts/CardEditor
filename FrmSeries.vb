Imports System.Collections.Generic
Imports System.Windows.Forms

Public Class FrmSeries
    Sub New(ByVal _lss As List(Of String))
        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        If _lss IsNot Nothing Then
            For Each s In _lss
                lbxName.Items.Add(s.Clone())
            Next
            lss = _lss
        End If
    End Sub

    Friend lss As New List(Of String)
    Private Sub lbxName_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lbxName.MouseDoubleClick
        Try
            If lbxName.SelectedIndex = 0 Then
                Dim name = InputBox("请输入所属系列名称。")
                If String.IsNullOrWhiteSpace(name) Then MsgBox("非法名称！") : Exit Sub
                lss.Add(name)
                lbxName.Items.Add(name)
            Else
                lss.RemoveAt(lbxName.SelectedIndex - 1)
                lbxName.Items.RemoveAt(lbxName.SelectedIndex)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub lbxName_MouseUp(sender As Object, e As MouseEventArgs) Handles lbxName.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right AndAlso Not lbxName.SelectedIndex = 0 Then
            Dim name = InputBox("请输入要更改成的效果名称。")
            If String.IsNullOrWhiteSpace(name) Then MsgBox("非法名称！") : Exit Sub
            lss(lbxName.SelectedIndex - 1) = name
            lbxName.Items(lbxName.SelectedIndex) = name
        End If
    End Sub

    Private Sub FrmSeries_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class