Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms

Public Class FrmDeck
    Private myFrmMain As FrmMain
    Public Sub New(ByRef clist As List(Of Card), ByRef frmMain As FrmMain)
        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Cards = clist
        myFrmMain = frmMain
    End Sub

    Private Cards As List(Of Card)
    Private CurrentDeckFile As String = ""

    ' 是否正在操作副卡组。
    Private isSide As Boolean = False
    ' 总数目，普通卡数目，优质卡数目，王牌卡数目
    Private deckMainCount As Integer() = {0, 0, 0, 0}
    Private deckMain As List(Of CardAndCount)() = {
        New List(Of CardAndCount),
        New List(Of CardAndCount),
        New List(Of CardAndCount)
    }
    Private deckSideCount As Integer = 0
    Private deckSide As List(Of CardAndCount)() = {
        New List(Of CardAndCount),
        New List(Of CardAndCount),
        New List(Of CardAndCount)
    }

    Private Sub LoadDeck(s As String)
        If s.Length < 8 Then Exit Sub
        Dim decs = s.Split("#")
        Dim dm = decs(0), ds = decs(1)
        Dim cs = dm.Split("|")
        For Each cac In cs
            If cac.Length < 2 Then Continue For
            Dim count = cac.Split("_")(0)
            Dim cid = cac.Split("_")(1)
            isSide = False
            For i = 1 To Val(count)
                TryAdd(Cards.Find(Function(c As Card)
                                      Return c.ID = cid
                                  End Function))
            Next
        Next
        cs = ds.Split("|")
        For Each cac In cs
            If cac.Length < 2 Then Continue For
            Dim count = cac.Split("_")(0)
            Dim cid = cac.Split("_")(1)
            isSide = True
            For i = 1 To Val(count)
                TryAdd(Cards.Find(Function(c As Card)
                                      Return c.ID = cid
                                  End Function))
            Next
        Next
    End Sub

    Private Sub LoadDeckFromFile()
        Using fs As New FileStream(CurrentDeckFile, FileMode.Open)
            Dim b(fs.Length) As Byte
            fs.Read(b, 0, fs.Length)
            LoadDeck(Encoding.UTF8.GetString(b))
        End Using
    End Sub

    ''' <summary>
    ''' 生成用于保存成文件的字符串。
    ''' </summary>
    Private Function MakeSaveString() As String
        Dim s As String = ""

        For Each one In deckMain
            For Each cac In one
                s += cac.Serialize()
            Next
        Next
        s += "#"
        For Each one In deckSide
            For Each cac In one
                s += cac.Serialize()
            Next
        Next
        Return s
    End Function
    Private Function MakeDeckString() As String
        Dim s As String = "主卡组：" + vbCrLf

        For i = 2 To 0 Step -1
            For Each cac In deckMain(i)
                s += cac.ToString() + vbCrLf
            Next
            s += "----" + vbCrLf
        Next

        s += "副卡组：" + vbCrLf

        For i = 2 To 0 Step -1
            For Each cac In deckSide(i)
                s += cac.ToString() + vbCrLf
            Next
            s += "----" + vbCrLf
        Next

        Return s
    End Function

    Private Sub RemoveCard(ByRef c As Card)
        Dim cid = c.ID
        If isSide Then
            Dim cac = deckSide(c.Rank - 1).Find(Function(arg_cac As CardAndCount)
                                                    Return arg_cac.Card.ID = cid
                                                End Function)
            If cac.Count = 1 Then
                If deckSide(c.Rank - 1).Remove(cac) Then deckSideCount -= 1
            Else
                cac.Count -= 1
                deckSideCount -= 1
            End If
            UpdateLbx(LbxSide, deckSide)
        Else
            Dim cac = deckMain(c.Rank - 1).Find(Function(arg_cac As CardAndCount)
                                                    Return arg_cac.Card.ID = cid
                                                End Function)
            If cac.Count = 1 Then
                If deckMain(c.Rank - 1).Remove(cac) Then deckMainCount(c.Rank) -= 1 : deckMainCount(0) -= 1
            Else
                cac.Count -= 1
                deckMainCount(0) -= 1
                deckMainCount(c.Rank) -= 1
            End If
            UpdateLbx(LbxMain, deckMain)
        End If
    End Sub

    Public Sub TryAdd(ByRef c As Card)
        Dim cid = c.ID
        Try
            AddCheck(c)
            If isSide Then
                Dim cc = deckSide(c.Rank - 1).Find(Function(cac As CardAndCount)
                                                       Return cac.Card.ID = cid
                                                   End Function)
                If cc Is Nothing Then
                    Dim cac As New CardAndCount With {.Card = c, .Count = 1}
                    deckSide(c.Rank - 1).Add(cac)
                Else
                    cc.Count += 1
                End If
                deckSideCount += 1
                UpdateLbx(LbxSide, deckSide)
            Else
                Dim cc = deckMain(c.Rank - 1).Find(Function(cac As CardAndCount)
                                                       Return cac.Card.ID = cid
                                                   End Function)
                If cc Is Nothing Then
                    Dim cac As New CardAndCount With {.Card = c, .Count = 1}
                    deckMain(c.Rank - 1).Add(cac)
                Else
                    cc.Count += 1
                End If
                deckMainCount(0) += 1
                deckMainCount(c.Rank) += 1
                UpdateLbx(LbxMain, deckMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function AddCheck(ByRef c As Card) As Boolean
        If c.Limit = ELimit.Forbidden Then Throw New Exception("不能携带禁止卡orz")
        If c.Limit = ELimit.Derived Then Throw New Exception("衍生卡是效果产生的，不能事先放入卡组orz")
        If isSide Then
            If deckSideCount >= 10 Then Throw New Exception("副卡组最多放10张卡orz")
        Else
            If deckMainCount(0) >= 18 Then Throw New Exception("主卡组最多放18张卡orz")
            Select Case c.Rank
                Case ERank.Common
                    If deckMainCount(1) >= 9 Then Throw New Exception("主卡组最多放9张普通卡orz")
                Case ERank.Good
                    If deckMainCount(2) >= 6 Then Throw New Exception("主卡组最多放6张优质卡orz")
                Case ERank.Trump
                    If deckMainCount(3) >= 3 Then Throw New Exception("主卡组最多放3张王牌卡orz")
            End Select
        End If
        If isSide Then
            If c.Effects.Contains("不能放入副卡组") Then
                Throw New Exception("这张卡不能事先放入副卡组orz")
            End If
        End If
        Dim cid = c.ID
        Dim count = 0
        Dim tcac = deckMain(c.Rank - 1).Find(Function(cac As CardAndCount)
                                                 Return cid = cac.Card.ID
                                             End Function)
        If Not tcac Is Nothing Then
            count += tcac.Count
        End If
        tcac = deckSide(c.Rank - 1).Find(Function(cac As CardAndCount)
                                             Return cid = cac.Card.ID
                                         End Function)
        If Not tcac Is Nothing Then
            count += tcac.Count
        End If
        If (c.Rank = ERank.Trump Or c.Limit = ELimit.Limited) And count > 0 Then Throw New Exception("携带数量超过限制orz")
        If count > 1 Then Throw New Exception("同名卡至多合计放2张orz")
        Return True
    End Function

    Private Sub UpdateLbx(ByRef lbx As ListBox, ByRef clists As List(Of CardAndCount)())
        With lbx
            Dim tmpIndex As Integer = .SelectedIndex

            .Items.Clear()
            For i = 2 To 0 Step -1
                For Each cac In clists(i)
                    lbx.Items.Add(cac)

                Next
            Next

            If tmpIndex < .Items.Count Then
                .SelectedIndex = tmpIndex
            Else
                .SelectedIndex = .Items.Count - 1
            End If
        End With
    End Sub

    Private Sub LbxSide_MouseDown(sender As Object, e As MouseEventArgs) Handles LbxSide.MouseDown
        isSide = True
        LbxSide.BackColor = Color.FromArgb(255, 255, 255, 128)
        LbxMain.BackColor = Color.FromArgb(255, 255, 255, 255)
    End Sub

    Private Sub LbxSide_DoubleClick(sender As Object, e As EventArgs) Handles LbxSide.DoubleClick
        If LbxSide.SelectedIndex >= 0 Then
            RemoveCard(LbxSide.SelectedItem.Card)
            UpdateLbx(LbxSide, deckSide)
        End If
    End Sub

    Private Sub LbxMain_MouseDown(sender As Object, e As MouseEventArgs) Handles LbxMain.MouseDown
        isSide = False
        LbxMain.BackColor = Color.FromArgb(255, 255, 255, 128)
        LbxSide.BackColor = Color.FromArgb(255, 255, 255, 255)
    End Sub

    Private Sub LbxMain_DoubleClick(sender As Object, e As EventArgs) Handles LbxMain.DoubleClick
        If LbxMain.SelectedIndex >= 0 Then
            RemoveCard(LbxMain.SelectedItem.Card)
            UpdateLbx(LbxMain, deckMain)
        End If
    End Sub

    Private Sub ButCopy_Click(sender As Object, e As EventArgs) Handles ButCopy.Click
        Clipboard.SetText(MakeDeckString())
    End Sub

    Private Sub ButOpen_Click(sender As Object, e As EventArgs) Handles ButOpen.Click
        For Each d In deckMain
            d.Clear()
        Next
        For Each d In deckSide
            d.Clear()
        Next
        deckSideCount = 0
        For i = 0 To 3
            deckMainCount(i) = 0
        Next
        Dim sfd As New OpenFileDialog With {
            .Filter = "Fair Deal Deck Files(*.fddec)|*.fddec",
            .AddExtension = True,
            .InitialDirectory = Application.StartupPath,
            .RestoreDirectory = True
            }
        sfd.ShowDialog()
        CurrentDeckFile = sfd.FileName
        LoadDeckFromFile()
    End Sub

    Private Sub Save()
        If CurrentDeckFile = "" Then Return
        Dim s = MakeSaveString()
        Dim fs As New FileStream(CurrentDeckFile, FileMode.Create)
        Dim b As Byte() = Encoding.UTF8.GetBytes(s)
        fs.Write(b, 0, b.Length)
        fs.Close()
    End Sub

    Private Sub ButSave_Click(sender As Object, e As EventArgs) Handles ButSave.Click
        If Not FileIO.FileSystem.FileExists(CurrentDeckFile) Then ChooseSaveFile()
        Save()
    End Sub

    Private Sub ChooseSaveFile()
        Dim sfd As New SaveFileDialog With {
       .Filter = "Fair Deal Deck Files(*.fddec)|*.fddec",
       .AddExtension = True,
       .InitialDirectory = Application.StartupPath,
       .RestoreDirectory = True
    }
        sfd.ShowDialog()
        CurrentDeckFile = sfd.FileName
    End Sub
    Private Sub ButSaveAs_Click(sender As Object, e As EventArgs) Handles ButSaveAs.Click
        ChooseSaveFile()
        Save()
    End Sub

    Friend Class CardAndCount
        Public Card As Card
        Public Count As Integer

        Public Overrides Function ToString() As String
            Return Card.Name + ": " + Count.ToString()
        End Function

        Public Function Serialize() As String
            Return Count.ToString + "_" + Card.ID + "|"
        End Function
    End Class

    Private Sub LbxMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbxMain.SelectedIndexChanged
        If LbxMain.SelectedIndex >= 0 Then
            myFrmMain.LbxCards.SelectedItem = LbxMain.SelectedItem.Card
        End If
    End Sub

    Private Sub LbxSide_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbxSide.SelectedIndexChanged
        If LbxSide.SelectedIndex >= 0 Then
            myFrmMain.LbxCards.SelectedItem = LbxSide.SelectedItem.Card
        End If
    End Sub
End Class