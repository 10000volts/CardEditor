Imports System.Collections.Generic
Imports System.Windows.Forms
Friend Class FrmEffectDescription
    Private efs As String, labels As List(Of UInt16)
    Friend ReadOnly Property ResultEfs As String
        Get
            Return efs
        End Get
    End Property
    Friend ResultLabels As List(Of UInt16)
    Sub New(ByVal _efs As String, ByVal ls As List(Of UInt16))
        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        If _efs IsNot Nothing Then
            tbxContent.Text = _efs
            efs = _efs
        End If
        labels = ls
    End Sub
    Private Sub ButSave_Click(sender As Object, e As EventArgs) Handles ButSave.Click
        efs = tbxContent.Text
        ResultLabels = labels
    End Sub
    Private Sub ButLabel_Click(sender As Object, e As EventArgs) Handles ButLabel.Click
        Dim fl As New FrmLabel(labels)
        fl.ShowDialog()
        labels = fl.Result
    End Sub

    Private Sub FrmEffectDescription_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Friend Class FrmLabel
        Inherits Form
        Friend Result As New List(Of UInt16)
        Private cbs(UBound(LabelContents)) As CheckBox
        Sub New(ByRef ls As List(Of UInt16))
            Width = 800
            Height = 600
            For i = 0 To UBound(cbs)
                cbs(i) = New CheckBox With {.Left = (i Mod 6) * 120 + 25, .Top = (i \ 6) * 30 + 20,
                                            .Text = LabelContents(i), .Width = 120}
                Controls.Add(cbs(i))
            Next
            If ls IsNot Nothing Then
                For Each i In ls
                    If cbs.Length > i Then
                        cbs(i).Checked = True
                    End If
                Next
            End If
        End Sub
        Private Sub FrmLabel_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
            For i = 0 To UBound(cbs)
                If cbs(i).Checked Then
                    Result.Add(i)
                End If
            Next
            If Result.Count = 0 Then Result = Nothing
        End Sub
    End Class
End Class