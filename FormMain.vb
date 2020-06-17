Imports TheSoulHunterEditor.Card
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports TheSoulHunterEditor.Helper
Imports System.Math

Public Class FormMain
#If DEBUG Then
    Friend Const admin = True
#Else
    Friend Const admin = False
#End If
    Sub New()
        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        MsgBox("·HP、MP、ATK、DEF、范围不能作为搜索依据。" & vbCrLf &
               "·点击ID可以方便的创建ID。" & vbCrLf &
               "·编辑卡片效果时，双击名称列表可以将该效果移除。" & vbCrLf &
               "·使用<>、[]括进数字进行范围简写可以快速创建范围。" & vbCrLf &
               "·在范围编辑时，右键会尝试添加直线。" & vbCrLf &
               "·右键导出按钮可以快速导出。右键导入按钮可以仅从新库中更新。" & vbCrLf &
               "                                                          ——车万伏特") '"·在范围编辑时，右键会尝试添加直线。" & vbCrLf &
        Cards = New List(Of Card)
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UnitControlList = {CbxSummonCondition, LblHP, LblMP, TbxHP, TbxMP, TbxMPMax, LblATK,
                 TbxATK, LblAR, LblMR}
        CommonControlList = {LblCOST_Occupy, TbxCOST_Occupy}
    End Sub
    Private CurrentCard As New Card
    Friend UnitControlList As Control()
    Friend CommonControlList As Control()
    Private CurrentFile As String

    Friend Shared Cards As List(Of Card)
    Friend SearchCards As New List(Of Card)
    Friend TempAR As Range
    Friend TempARAbb As String
    Friend TempMR As Range
    Friend TempMRAbb As String
    Friend TempCSR As Range
    Friend TempCMR As Range
    Friend TempEf As List(Of SEffect)
    Friend TempLabel As List(Of UInt16)
    Friend TempSeries As List(Of String)
    Private Searching As Boolean = False
    Private Sub RemoveCard(ByVal id As UInteger)
        Dim i = Cards.FindIndex(Function(c As Card) As Boolean
                                    Return c.ID = id
                                End Function)
        If i > -1 Then
            Cards.RemoveAt(i)
        End If
    End Sub
    Private Function FindCard(ByVal id As UInteger) As Card
        Dim i = Cards.FindIndex(Function(c As Card) As Boolean
                                    Return c.ID = id
                                End Function)
        If i > -1 Then Return Cards(i) Else Return Nothing
    End Function
    Private Function ExistCard(ByVal id As UInteger) As Boolean
        Return Cards.FindIndex(Function(c As Card) As Boolean
                                   Return c.ID = id
                               End Function) > -1
    End Function
    Private Function FuncRead(ByVal file As String) As List(Of Card)
        Dim result As New List(Of Card)
        Dim fs As New FileStream(file, FileMode.Open, FileAccess.Read)
        Dim bytes(fs.Length - 1) As Byte
        fs.Read(bytes, 0, fs.Length)
        Dim str As String = DESDecrypt(bytes)  'Encoding.Default.GetString(bytes) 'DESDecrypt(bytes) 
        Dim cs = str.Split("!")
        fs.Close()
        For Each Card In cs
            Dim briefings = Card.Split("|")
            Dim c As New Card With {
                .Official = briefings(0),
                .ID = briefings(1),
                .Name = briefings(2),
                .Type = briefings(3)
            }
            If Not briefings(4) = " " Then
                c.Series = New List(Of String)
                Dim s = briefings(4).Split(",")
                For Each one In s
                    c.Series.Add(one)
                Next
            End If
            If c.Type = EType.Unit Then
                c.Species = briefings(5)
                c.SummonCondition.SummonCondition = briefings(6)
                If c.SummonCondition.SummonCondition = ESummonCondition.Ceremony Then
                    FromStringToRange(briefings(7), c.CeremoryFormationSummon)
                    FromStringToRange(briefings(8), c.CeremoryFormationMaterial)
                    c.SummonCondition.Condition = briefings(9)
                    c.HP = briefings(10)
                    c.MP = briefings(11)
                    c.MPMax = briefings(12)
                    c.ATK = briefings(13)
                    If InStr(briefings(14), "=") Then
                        FromStringToRange(briefings(14), c.AR)
                    Else
                        c.EquivalentAR = briefings(14)
                    End If
                    If InStr(briefings(15), "=") Then
                        FromStringToRange(briefings(15), c.MR)
                    Else
                        c.EquivalentMR = briefings(15)
                    End If
                    If briefings(16) <> " " Then
                        c.Label = New List(Of UInt16)
                        Dim s = briefings(16).Split(",")
                        For Each one In s
                            c.Label.Add(one)
                        Next
                    End If
                    If briefings.Count > 17 Then
                        c.Effect = New List(Of SEffect)
                        For i = 17 To UBound(briefings)
                            Dim ef As SEffect
                            ef.Description = briefings(i)
                            c.Effect.Add(ef)
                        Next
                    End If
                Else
                    c.SummonCondition.Condition = briefings(7)
                    c.HP = briefings(8)
                    c.MP = briefings(9)
                    c.MPMax = briefings(10)
                    c.ATK = briefings(11)
                    If InStr(briefings(12), "=") Then
                        FromStringToRange(briefings(12), c.AR)
                    Else
                        c.EquivalentAR = briefings(12)
                    End If
                    If InStr(briefings(13), "=") Then
                        FromStringToRange(briefings(13), c.MR)
                    Else
                        c.EquivalentMR = briefings(13)
                    End If
                    If briefings(14) <> " " Then
                        c.Label = New List(Of UInt16)
                        Dim s = briefings(14).Split(",")
                        For Each one In s
                            c.Label.Add(one)
                        Next
                    End If
                    If briefings.Count > 15 Then
                        c.Effect = New List(Of SEffect)
                        For i = 15 To UBound(briefings)
                            Dim ef As SEffect
                            ef.Description = briefings(i)
                            c.Effect.Add(ef)
                        Next
                    End If
                End If
                result.Add(c)
            Else
                c.StraregyType = briefings(5)
                c.Occupy = briefings(6)
                If briefings(7) <> " " Then
                    c.Label = New List(Of UInt16)
                    Dim s = briefings(7).Split(",")
                    For Each one In s
                        c.Label.Add(one)
                    Next
                End If
                If briefings.Count > 8 Then
                    c.Effect = New List(Of SEffect)
                    For i = 8 To UBound(briefings)
                        Dim ef As SEffect
                        ef.Description = briefings(i)
                        c.Effect.Add(ef)
                    Next
                End If
                result.Add(c)
            End If
        Next
        Return result
    End Function
   
    Private Sub ButRead_Click(sendejr As Object, e As EventArgs) Handles ButRead.Click
        ListMain.SelectedIndex = -1
        Clear()
        Cards = New List(Of Card)
        Dim ofd As New OpenFileDialog With {
            .Filter = "The Soul Hunter Card Database(*.shcdb)|*.shcdb",
            .InitialDirectory = Application.StartupPath,
            .RestoreDirectory = True
        }
        ofd.ShowDialog()
        If FileIO.FileSystem.FileExists(ofd.FileName) Then
            Cards = FuncRead(ofd.FileName)
            If Cards.Any Then
                RenewList(Cards)
                CurrentFile = ofd.FileName
            End If
        Else
            MsgBox("指定的文件不存在！")
        End If
    End Sub
    Private Sub SaveTo(ByRef cs As List(Of Card), ByVal file As String)
        If Not cs.Any Then MsgBox("当前卡片库不包含任何卡！") : Exit Sub
        If file = "" Then Exit Sub
        If FileIO.FileSystem.FileExists(file) Then FileIO.FileSystem.CopyFile(file, file & "temp.shcdb")
        Dim fs As New FileStream(file, FileMode.Create)
        Try
            Dim content As String = ""
            For Each strCard In cs
                content &= strCard.ToStr & "!"
            Next
            content = Mid(content, 1, content.Length - 1)
            Dim bytes As Byte()
            If Not admin OrElse MsgBox("加密吗？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                bytes = Encrypt(content)
            Else
                bytes = Encoding.Default.GetBytes(content)
            End If
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()
            FileIO.FileSystem.DeleteFile(file & "temp.shcdb")
            CurrentFile = file
        Catch ex As Exception
            fs.Close()
        End Try
    End Sub
    Private Sub ButOut_Click(sender As Object, e As EventArgs) Handles ButOut.Click
        Dim icards = FrmChoose.CallFrmChoose(Cards)
        Dim cs As New List(Of Card)
        For Each one In icards
            cs.Add(one)
        Next
        Dim sfd As New SaveFileDialog With {
            .Filter = "The Soul Hunter Card Database(*.shcdb)|*.shcdb",
            .AddExtension = True,
            .InitialDirectory = Application.StartupPath,
            .RestoreDirectory = True
        }
        sfd.ShowDialog()
        SaveTo(cs, sfd.FileName)
    End Sub
    Private Sub ButOut_MouseUp(sender As Object, e As MouseEventArgs) Handles ButOut.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If ListMain.Items.Count > 0 Then
                Dim cs As New List(Of Card)
                For Each one In ListMain.Items
                    cs.Add(one)
                Next
                If ListMain.Items.Count > 1 Then
                    Dim sfd As New SaveFileDialog With {
                        .Filter = "The Soul Hunter Card Database(*.shcdb)|*.shcdb",
                        .AddExtension = True,
                        .InitialDirectory = Application.StartupPath,
                        .RestoreDirectory = True
                    }
                    sfd.ShowDialog()
                    SaveTo(cs, sfd.FileName)
                Else
                    SaveTo(cs, Application.StartupPath & "\\" & CType(ListMain.Items(0), Card).Name & ".shcdb")
                End If
            Else
                MsgBox("搜索列表中不含任何项！")
            End If
        End If
    End Sub
    Private Sub ButNew_Click(sender As Object, e As EventArgs) Handles ButNew.Click
        CurrentFile = ""
        ListMain.SelectedIndex = -1
        Clear()
        Cards = New List(Of Card)
    End Sub
    Friend Sub Clear()
        CurrentCard = New Card
        TempAR = Nothing
        TempMR = Nothing
        TempEf = Nothing
        TempLabel = Nothing
        TempSeries = Nothing
        TempCMR = Nothing
        TempCSR = Nothing
        For Each one In CommonControlList
            Dim tb = TryCast(one, TextBox)
            If tb IsNot Nothing Then tb.Text = ""
            Dim cb = TryCast(one, ComboBox)
            If cb IsNot Nothing Then cb.SelectedIndex = -1
            one.Visible = False
        Next
        For Each one In UnitControlList
            Dim tb = TryCast(one, TextBox)
            If tb IsNot Nothing Then tb.Text = ""
            Dim cb = TryCast(one, ComboBox)
            If cb IsNot Nothing Then cb.SelectedIndex = -1
            one.Visible = False
        Next
        TbxName.Text = ""
        TbxID.Text = ""
        CbxSpecies.SelectedIndex = -1
        CbxType.SelectedIndex = -1
    End Sub
    Private Sub ButIn_MouseUp(sender As Object, e As MouseEventArgs) Handles ButIn.MouseUp
        Select Case e.Button
            Case Windows.Forms.MouseButtons.Left
                Dim ofd As New OpenFileDialog With {
                    .Filter = "The Soul Hunter Card Database(*.shcdb)|*.shcdb",
                    .InitialDirectory = Application.StartupPath,
                    .RestoreDirectory = True
                }
                ofd.ShowDialog()
                If FileIO.FileSystem.FileExists(ofd.FileName) Then
                    Dim cs = FuncRead(ofd.FileName)
                    If cs.Any Then
                        For Each Card In cs
                            Dim cc = FindCard(Card.ID)
                            If cc.ID > 0 Then
                                If Card.Official AndAlso Not cc.Official Then MsgBox("导入源中的""" & Card.Name &
                                    """与当前库中的""" & cc.Name & """的ID发生冲突，但当前库中的卡为官方版本。") : Continue For
                                If MsgBox("导入源中的""" & Card.Name & """与当前库中的""" & cc.Name &
                                                                      """的ID发生冲突，将其覆盖吗？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                                    Dim lci = Cards.FindIndex(Function(c As Card) As Boolean
                                                                  Return Card.ID = c.ID
                                                              End Function)
                                    Cards(lci) = Card
                                End If
                            Else
                                Cards.Add(Card)
                            End If
                        Next
                        RenewList(Cards)
                    End If
                Else
                    MsgBox("指定的文件不存在！")
                End If
            Case Windows.Forms.MouseButtons.Right
                Dim ofd As New OpenFileDialog With {
                    .Filter = "The Soul Hunter Card Database(*.shcdb)|*.shcdb",
                    .InitialDirectory = Application.StartupPath,
                    .RestoreDirectory = True
                }
                ofd.ShowDialog()
                If FileIO.FileSystem.FileExists(ofd.FileName) Then
                    Dim css = FuncRead(ofd.FileName)
                    If css.Any Then
                        For i = 0 To Cards.Count - 1
                            Dim ii = i
                            Dim csupd = css.Find(Function(c As Card) As Boolean
                                                     Return c.ID = Cards(ii).ID
                                                 End Function)
                            If csupd.Name <> "" Then
                                Cards(i) = csupd
                            End If
                        Next
                        RenewList(Cards)
                    End If
                Else
                    MsgBox("指定的文件不存在！")
                End If
        End Select
    End Sub
    Private Sub ButSave_Click(sender As Object, e As EventArgs) Handles ButSave.Click
        If admin OrElse Not InStr(TbxName.Text, "(官方)") Then
            If Val(TbxID.Text) <> TbxID.Text OrElse TbxID.Text.Length <> 8 Then MsgBox("ID只能是八位长度的数字。") : Exit Sub
            Try
                Dim cardid = Cards.FindIndex(Function(c As Card) As Boolean
                                                 Return c.ID = TbxID.Text
                                             End Function)
                If cardid = -1 Then
                    SetCard(CurrentCard)
                    Cards.Add(CurrentCard)
                    If Not Searching Then
                        ListMain.Items.Add(CurrentCard)
                        ListMain.SelectedIndex = ListMain.Items.Count - 1
                    End If
                ElseIf admin OrElse Not Cards(cardid).Official Then
                    If Cards(cardid).Name = TbxName.Text OrElse MsgBox("该ID已存在不同名的卡，是否继续？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                        SetCard(CurrentCard)
                        Cards(cardid) = CurrentCard
                        If Not Searching Then
                            ListMain.Items(cardid) = CurrentCard
                        Else
                            Dim c As Card = ListMain.SelectedItem
                            If c.ID = TbxID.Text Then
                                ListMain.SelectedItem = CurrentCard
                            Else
                                For i = 0 To ListMain.Items.Count - 1
                                    If c.Name = TbxID.Text Then
                                        ListMain.Items(i) = CurrentCard
                                    End If
                                Next
                            End If
                        End If
                    End If
                Else
                    MsgBox("权限不足，无法修改！")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("创建失败。")
        End If
    End Sub
    Private Sub SetCard(ByRef c As Card)
        If admin Then c.Official = True
        c.Name = TbxName.Text
        c.ID = TbxID.Text
        c.Type = Max(0, CbxType.SelectedIndex)
        Select Case c.Type
            Case EType.Unit
                c.Species = Max(0, CbxSpecies.SelectedIndex)
                c.SummonCondition.SummonCondition = 2 ^ Max(0, CbxSummonCondition.SelectedIndex)
                c.SummonCondition.Condition = TbxCOST_Occupy.Text
                c.HP = Val(TbxHP.Text)
                c.MP = Val(TbxMP.Text)
                c.MPMax = Val(TbxMPMax.Text)
                c.ATK = Val(TbxATK.Text)
                If TempARAbb <> "" Then c.EquivalentAR = TempARAbb Else c.AR = TempAR : c.EquivalentAR = ""
                If TempMRAbb <> "" Then c.EquivalentMR = TempMRAbb Else c.MR = TempMR : c.EquivalentMR = ""
                If CbxSummonCondition.SelectedIndex = 4 Then
                    c.CeremoryFormationSummon = TempCSR
                    c.CeremoryFormationMaterial = TempCMR
                End If
            Case EType.Strategy
                c.StraregyType = Max(0, CbxSpecies.SelectedIndex)
                c.Occupy = Val(TbxCOST_Occupy.Text)
        End Select
        c.Effect = TempEf
        c.Label = TempLabel
        c.Series = TempSeries
        'TempARAbb = ""
        'TempMRAbb = ""
    End Sub
    Private Sub ButSaveIn_Click(sender As Object, e As EventArgs) Handles ButSaveIn.Click
        SaveTo(Cards, CurrentFile)
    End Sub
    Private Sub LblAR_Click(sender As Object, e As EventArgs) Handles LblAR.Click
        Dim rfe As FrmRangeEdit = Nothing
        If TempARAbb <> "" Then
            rfe = New FrmRangeEdit(TempARAbb)
        Else
            rfe = New FrmRangeEdit(TempAR)
        End If
        rfe.ShowDialog()
        If rfe.TargetAbb <> "" Then
            TempARAbb = rfe.TargetAbb
        Else
            TempAR = rfe.ResultRange
            TempARAbb = ""
        End If
    End Sub
    Private Sub LblMR_Click(sender As Object, e As EventArgs) Handles LblMR.Click
        Dim rfe As FrmRangeEdit = Nothing
        If TempMRAbb <> "" Then
            rfe = New FrmRangeEdit(TempMRAbb)
        Else
            rfe = New FrmRangeEdit(TempMR)
        End If
        rfe.ShowDialog()
        If rfe.TargetAbb <> "" Then
            TempMRAbb = rfe.TargetAbb
        Else
            TempMR = rfe.ResultRange
            TempMRAbb = ""
        End If
    End Sub
    Private Sub LblCR_Click(sender As Object, e As EventArgs) Handles LblCR.Click
        Dim rce As New FrmCeremonyEdit(TempCSR, TempCMR)
        rce.ShowDialog()
        TempCSR = rce.TargetCSR
        TempCMR = rce.TargetCMR
    End Sub
    Private Sub CbxSummonCondition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbxSummonCondition.SelectedIndexChanged
        If CbxSummonCondition.SelectedIndex = 4 Then
            LblCR.Visible = True
        Else
            LblCR.Visible = False
        End If
    End Sub
    Private Sub LblEffect_Click(sender As Object, e As EventArgs) Handles LblEffect.Click
        Dim fed As New FrmEffectDescription(TempEf, TempLabel)
        fed.ShowDialog()
        TempEf = fed.ResultEfs
        TempLabel = fed.ResultLabels
    End Sub
    Private Sub ButRemove_Click(sender As Object, e As EventArgs) Handles ButRemove.Click
        If ListMain.SelectedIndex > -1 Then
            Dim c = CType(ListMain.SelectedItem, Card)
            If admin OrElse Not c.Official Then
                Cards.Remove(c)
                ListMain.Items.Remove(c)
            Else
                MsgBox("权限不足！")
            End If
        End If
    End Sub
    Private Sub ButCreate_Click(sender As Object, e As EventArgs) Handles ButCreate.Click
        ListMain.SelectedIndex = -1
        Clear()
    End Sub
    Private Sub CbxType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbxType.SelectedIndexChanged
        For Each one In CommonControlList
            one.Visible = True
        Next
        Select Case CbxType.SelectedIndex
            Case 0
                For Each one In CommonControlList
                    one.Visible = False
                Next
                For Each one In UnitControlList
                    one.Visible = False
                Next
            Case 1
                LblCOST_Occupy.Text = "降临条件："
                CbxSpecies.Items.Clear()
                For Each one In SpeciesName
                    CbxSpecies.Items.Add(one)
                Next
                For Each one In UnitControlList
                    one.Visible = True
                Next
            Case 2
                LblCOST_Occupy.Text = "占用空间："
                CbxSpecies.Items.Clear()
                For Each one In StrategyTypeName
                    CbxSpecies.Items.Add(one)
                Next
                For Each one In UnitControlList
                    one.Visible = False
                Next
                LblCR.Visible = False
        End Select
    End Sub
    Private Sub ButSearch_MouseUp(sender As Object, e As MouseEventArgs) Handles ButSearch.MouseUp
        Select Case e.Button
            Case Windows.Forms.MouseButtons.Left
                Dim c As New Card
                SetCard(c)
                ListMain.SelectedIndex = -1
                Clear()
                Searching = True
                RenewList(Cards.FindAll(Function(cc As Card) As Boolean
                                            If InStr(cc.Name, c.Name) = 0 OrElse InStr(cc.ID, c.ID) = 0 OrElse
        (c.Type <> 0 AndAlso cc.Type <> c.Type) Then Return False
                                            If c.Label IsNot Nothing Then
                                                If cc.Label Is Nothing Then Return False
                                                For Each l In c.Label
                                                    If Not cc.Label.Contains(l) Then Return False
                                                Next
                                            End If
                                            If c.Effect IsNot Nothing Then
                                                If cc.Effect Is Nothing Then Return False
                                                For Each ef In c.Effect
                                                    If cc.Effect.FindIndex(Function(_c As SEffect) As Boolean
                                                                               Return InStr(_c.Description, ef.Description)
                                                                           End Function) = -1 Then Return False
                                                Next
                                            End If
                                            If c.Type = EType.None Then Return True
                                            If c.Type = EType.Unit Then
                                                If c.SummonCondition.SummonCondition = ESummonCondition.None Then
                                                    If c.SummonCondition.Condition = "(传说)" Then
                                                        Return (c.Species = 0 OrElse cc.Species = c.Species) AndAlso
                                                           InStr(cc.SummonCondition.Condition, "(传说)")
                                                    End If
                                                    Return (c.Species = 0 OrElse cc.Species = c.Species)
                                                ElseIf c.SummonCondition.Condition = "(传说)" Then
                                                    Return (c.Species = 0 OrElse cc.Species = c.Species) AndAlso
     cc.SummonCondition.SummonCondition = c.SummonCondition.SummonCondition AndAlso
     InStr(cc.SummonCondition.Condition, "(传说)")
                                                Else
                                                    Return (c.Species = 0 OrElse cc.Species = c.Species) AndAlso
     cc.SummonCondition.SummonCondition = c.SummonCondition.SummonCondition AndAlso
     (c.SummonCondition.Condition = "" OrElse c.SummonCondition.Condition = cc.SummonCondition.Condition)
                                                End If
                                            Else
                                                Return (c.StraregyType = 0 OrElse cc.StraregyType = c.StraregyType) AndAlso
  (c.Occupy = -1 OrElse cc.Occupy = c.Occupy)
                                            End If
                                        End Function))
                MsgBox(ListMain.Items.Count & "个搜索结果。")
            Case Windows.Forms.MouseButtons.Right
                ListMain.SelectedIndex = -1
                Clear()
                Searching = False
                RenewList(Cards)
        End Select
    End Sub
    Private Sub butHelp_Click(sender As Object, e As EventArgs) Handles butHelp.Click
        Dim _frmhelp As New FrmHelp
        _frmhelp.Show()
    End Sub
    Friend Sub RenewList(ByRef cl As List(Of Card))
        ListMain.Items.Clear()
        For Each Card In cl
            ListMain.Items.Add(Card)
        Next
    End Sub
    Private Sub ListMain_DoubleClick(sender As Object, e As EventArgs) Handles ListMain.DoubleClick
        If CurrentCard.Name <> "" Then
            If _FrmDeck.Visible Then
                _FrmDeck.AddCard(CurrentCard)
            End If
        End If
    End Sub
    Private Sub CardList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListMain.SelectedIndexChanged
        If ListMain.SelectedIndex > -1 Then
            Clear()
            Dim ca = CType(ListMain.SelectedItem, Card)
            If Not Searching Then CurrentCard = ca Else
            CurrentCard = Cards.Find(Function(c As Card) As Boolean
                                         Return c.ID = ca.ID
                                     End Function)
            TbxName.Text = CurrentCard.Name
            TbxID.Text = CurrentCard.ID
            CbxType.SelectedIndex = CurrentCard.Type
            For Each one In CommonControlList
                one.Visible = True
            Next
            Select Case CurrentCard.Type
                Case 1
                    LblCOST_Occupy.Text = "降临条件："
                    CbxSpecies.Items.Clear()
                    For Each one In SpeciesName
                        CbxSpecies.Items.Add(one)
                    Next
                    For Each one In UnitControlList
                        one.Visible = True
                    Next
                Case 2
                    LblCOST_Occupy.Text = "占用空间："
                    CbxSpecies.Items.Clear()
                    For Each one In StrategyTypeName
                        CbxSpecies.Items.Add(one)
                    Next
                    For Each one In UnitControlList
                        one.Visible = False
                    Next
                    LblCR.Visible = False
            End Select
            Select Case CurrentCard.Type
                Case 1
                    CbxSpecies.SelectedIndex = CurrentCard.Species
                    CbxSummonCondition.SelectedIndex = Log(CurrentCard.SummonCondition.SummonCondition, 2)
                    If CurrentCard.SummonCondition.SummonCondition = ESummonCondition.Ceremony Then
                        LblCR.Visible = True
                        TempCSR = CurrentCard.CeremoryFormationSummon
                        TempCMR = CurrentCard.CeremoryFormationMaterial
                    End If
                    TbxCOST_Occupy.Text = CurrentCard.SummonCondition.Condition
                    TbxHP.Text = CurrentCard.HP
                    TbxMP.Text = CurrentCard.MP
                    TbxMPMax.Text = CurrentCard.MPMax
                    TbxATK.Text = CurrentCard.ATK
                    If CurrentCard.EquivalentAR <> "" Then
                        TempARAbb = CurrentCard.EquivalentAR
                    Else
                        TempAR = CurrentCard.AR
                        TempARAbb = ""
                    End If
                    If CurrentCard.EquivalentMR <> "" Then
                        TempMRAbb = CurrentCard.EquivalentMR
                    Else
                        TempMR = CurrentCard.MR
                        TempMRAbb = ""
                    End If
                Case 2
                    CbxSpecies.SelectedIndex = CurrentCard.StraregyType
                    TbxCOST_Occupy.Text = CurrentCard.Occupy
            End Select
            If CurrentCard.Effect IsNot Nothing Then TempEf = CurrentCard.Effect.Clone Else TempEf = Nothing
            If CurrentCard.Label IsNot Nothing Then TempLabel = CurrentCard.Label.Clone Else TempLabel = Nothing
            If CurrentCard.Series IsNot Nothing Then TempSeries = CurrentCard.Series.Clone Else TempSeries = Nothing
        End If
    End Sub
    Private Sub LblID_Click(sender As Object, e As EventArgs) Handles LblID.Click
        Randomize()
        'Dim i As String = CStr(ComplementaryIDList(Rnd() * (ComplementaryIDList.Count - 1) + 1)).PadLeft(8, "0")
        Dim r As New Random
        Dim i As Integer = r.Next(100000000)
        While ExistCard(i)
            Randomize()
            i = r.Next(100000000)
        End While
        TbxID.Text = CStr(i).PadLeft(8, "0")
    End Sub
    Private Sub lblSeries_Click(sender As Object, e As EventArgs) Handles lblSeries.Click
        Dim fs As New FrmSeries(TempSeries)
        fs.ShowDialog()
        TempSeries = fs.lss
    End Sub
    Private Sub ButEX_Click(sender As Object, e As EventArgs) Handles ButEx.Click
        _FrmDeck.Show()
    End Sub
    Private Const iv As String = "JiLiGuaLa"
    Private Const key As String = "jlgljlgl"
    Friend Shared Function DESDecrypt(encryptedString As Byte()) As String
        Dim btKey As Byte() = Encoding.Default.GetBytes(key)
        Dim btIV As Byte() = Encoding.Default.GetBytes(iv)
        Dim des As New DESCryptoServiceProvider With {
            .Mode = CipherMode.CBC,
            .Padding = PaddingMode.PKCS7
        }
        Dim ms As New MemoryStream()
        Try
            Try
                Dim cs As New CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write)
                Try
                    cs.Write(encryptedString, 0, encryptedString.Length)
                    cs.FlushFinalBlock()
                Finally
                    cs.Dispose()
                End Try

                Return Encoding.Default.GetString(ms.ToArray())
            Catch
            End Try
        Finally
            ms.Dispose()
        End Try
        Return ""
    End Function
    Friend Shared Function Encrypt(sourceString As String) As Byte()
        Dim btKey As Byte() = Encoding.Default.GetBytes(key)
        Dim btIV As Byte() = Encoding.Default.GetBytes(iv)
        Dim des As New DESCryptoServiceProvider With {
            .Mode = CipherMode.CBC,
            .Padding = PaddingMode.PKCS7
        }
        Dim ms As New MemoryStream()
        Try
            Dim inData As Byte() = Encoding.Default.GetBytes(sourceString) 'ANSI
            Try
                Dim cs As New CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write)
                Try
                    cs.Write(inData, 0, inData.Length)
                    cs.FlushFinalBlock()
                Finally
                    cs.Dispose()
                End Try
                Return ms.ToArray
            Catch
            End Try
        Finally
            ms.Dispose()
        End Try
        Return Nothing
    End Function 'Encrypt
    Private Shared Sub FromStringToRange(ByVal content As String, ByRef r As Range)
        Dim lp = content.Split("=")
        If InStr(content, "-") Then
            r.Lines = New List(Of Line)
            For Each l In lp(0).Split("-")
                If l = "" Then Exit For
                Dim ld = l.Split(",")
                Dim _l As Line
                _l.Direction = Val(ld(0))
                _l.StartPosition = Val(ld(1))
                r.Lines.Add(_l)
            Next
        End If
        If InStr(content, ".") Then
            r.Points = New List(Of RelativePosition)
            For Each l In lp(1).Split(".")
                If l = "" Then Exit Sub
                Dim ld = l.Split(",")
                Dim _p As RelativePosition
                _p.X = Val(ld(0))
                _p.Y = Val(ld(1))
                r.Points.Add(_p)
            Next
        End If
    End Sub
    Friend Class FrmCeremonyEdit
        Inherits Form
        Friend TargetCSR As Range, TargetCMR As Range
        Private rls(RowCount ^ 2 - 1) As SignalLabel
        Private WithEvents butOK As New Button() With {.Text = "确定", .Left = 640, .Top = 80, .Width = 80, .Height = 40}
        ''' <summary>
        ''' 创建一个新的降灵仪式示意窗体。
        ''' </summary>
        ''' <param name="rs">降临位置。</param>
        ''' <param name="rm">降灵素材位置。</param>
        ''' <remarks></remarks>
        Sub New(ByVal rs As Range, ByVal rm As Range)
            Width = 800
            Height = 600
            Controls.Add(butOK)
            For i = 0 To RowCount ^ 2 - 1
                Dim x As UInt16 = (i Mod RowCount) * LabelWidth + 80, y As UInt16 = (i \ RowCount) * LabelWidth + 80
                rls(i) = New SignalLabel("△", "×") With {
                    .Left = x,
                    .Top = y
                }
                Controls.Add(rls(i))
            Next
            If rs.Points IsNot Nothing Then
                For Each p In rs.Points
                    rls(p.Y * RowCount + p.X + (RowCount ^ 2 - 1) / 2).State = SignalLabel.EState.B
                    rls(p.Y * RowCount + p.X + (RowCount ^ 2 - 1) / 2).Text = "×"
                Next
            End If
            If rm.Points IsNot Nothing Then
                For Each p In rm.Points
                    rls(p.Y * RowCount + p.X + (RowCount ^ 2 - 1) / 2).State = SignalLabel.EState.A
                    rls(p.Y * RowCount + p.X + (RowCount ^ 2 - 1) / 2).Text = "△"
                Next
            End If
        End Sub
        Private Sub butOK_Click(sender As Object, e As EventArgs) Handles butOK.Click
            TargetCSR.Points = New List(Of RelativePosition)
            TargetCMR.Points = New List(Of RelativePosition)
            For i = 0 To RowCount ^ 2 - 1
                Select Case rls(i).State
                    Case SignalLabel.EState.A
                        TargetCMR.Points.Add(New RelativePosition((i Mod RowCount) - (RowCount - 1) / 2, (i \ RowCount) - (RowCount - 1) / 2))
                    Case SignalLabel.EState.B
                        TargetCSR.Points.Add(New RelativePosition((i Mod RowCount) - (RowCount - 1) / 2, (i \ RowCount) - (RowCount - 1) / 2))
                End Select
            Next
            Me.Close()
        End Sub
    End Class
    Friend Class FrmRangeEdit
        Inherits Form
        Private TargetRange As Range, TempLines As New List(Of Line)
        Friend TargetAbb As String = ""
        Private rls(RowCount ^ 2 - 1) As RangeLabel, LblSelf As New Label() With {.Width = LabelWidth, .Height = LabelWidth, .Font = New Font("宋体",
                                                                     LabelWidth / 2), .Text = "△", .BorderStyle = BorderStyle.FixedSingle}
        Private WithEvents butOK As New Button() With {.Text = "确定", .Left = 640, .Top = 80, .Width = 80, .Height = 40}
        Private WithEvents butQuick As New Button() With {.Text = "使用简写", .Left = 640, .Top = 160, .Width = 80, .Height = 40}
        Private lblAbb As New Label() With {.Text = "等效简写：", .Left = 560, .Top = 240, .AutoSize = True}
        ''' <summary>
        ''' 创建一个新的范围编辑窗体。
        ''' </summary>
        ''' <param name="r">范围初始量。</param>
        ''' <remarks></remarks>
        Sub New(ByVal r As Range)
            Width = 800
            Height = 600
            TargetRange = r
            Controls.Add(butOK)
            Controls.Add(butQuick)
            Controls.Add(lblAbb)
            For i = 0 To RowCount ^ 2 - 1
                rls(i) = New RangeLabel(i)
                Dim x As UInt16 = (i Mod RowCount) * LabelWidth + 80, y As UInt16 = (i \ RowCount) * LabelWidth + 80
                If i = (RowCount ^ 2 - 1) / 2 Then
                    LblSelf.Left = x
                    LblSelf.Top = y
                    Controls.Add(LblSelf)
                Else
                    With rls(i)
                        .Left = x
                        .Top = y
                        AddHandler .TrySetDirection, AddressOf TrySetDirection
                        AddHandler .DirectionInvalidated, AddressOf DirectionInvalidated
                        AddHandler .StateChanged, AddressOf StateChanged
                    End With
                    Controls.Add(rls(i))
                End If
            Next
            SetFromRange(r)
        End Sub
        Sub New(ByVal abb As String)
            Width = 800
            Height = 600
            TargetAbb = abb
            Controls.Add(butOK)
            Controls.Add(butQuick)
            Controls.Add(lblAbb)
            For i = 0 To RowCount ^ 2 - 1
                rls(i) = New RangeLabel(i)
                Dim x As UInt16 = (i Mod RowCount) * LabelWidth + 80, y As UInt16 = (i \ RowCount) * LabelWidth + 80
                If i = (RowCount ^ 2 - 1) / 2 Then
                    LblSelf.Left = x
                    LblSelf.Top = y
                    Controls.Add(LblSelf)
                Else
                    With rls(i)
                        .Left = x
                        .Top = y
                        AddHandler .TrySetDirection, AddressOf TrySetDirection
                        AddHandler .DirectionInvalidated, AddressOf DirectionInvalidated
                        AddHandler .StateChanged, AddressOf StateChanged
                    End With
                    Controls.Add(rls(i))
                End If
            Next
            SetFromAbb()
        End Sub
        Private Sub TrySetDirection(ByVal index As UInt16)
            Dim x As Int16 = (index Mod RowCount) - (RowCount - 1) / 2
            Dim y As Int16 = (index \ RowCount) - (RowCount - 1) / 2
            Dim l As New Line
            If x = 0 Then
                If y > 0 Then
                    l.Direction = EDirection.South
                    DirectionInvalidated(l.Direction)
                    l.StartPosition = y - 1
                    For i = y - 1 To (RowCount - 3) / 2
                        rls((i + 1) * RowCount + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.South)
                    Next
                Else
                    l.Direction = EDirection.North
                    DirectionInvalidated(l.Direction)
                    l.StartPosition = -y - 1
                    For i = -y - 1 To (RowCount - 3) / 2
                        rls((-i - 1) * RowCount + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.North)
                    Next
                End If
            ElseIf y = 0 Then
                If x > 0 Then
                    l.Direction = EDirection.East
                    DirectionInvalidated(l.Direction)
                    l.StartPosition = x - 1
                    For i = x - 1 To (RowCount - 3) / 2
                        rls(i + 1 + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.East)
                    Next
                Else
                    l.Direction = EDirection.West
                    DirectionInvalidated(l.Direction)
                    l.StartPosition = -x - 1
                    For i = -x - 1 To (RowCount - 3) / 2
                        rls(-i - 1 + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.West)
                    Next
                End If
            ElseIf x = y Then
                If x > 0 Then
                    l.Direction = EDirection.SouthEast
                    DirectionInvalidated(l.Direction)
                    l.StartPosition = x - 1
                    For i = x - 1 To (RowCount - 3) / 2
                        rls((i + 1) * RowCount + i + 1 + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.SouthEast)
                    Next
                Else
                    l.Direction = EDirection.NorthWest
                    DirectionInvalidated(l.Direction)
                    l.StartPosition = -x - 1
                    For i = -x - 1 To (RowCount - 3) / 2
                        rls((-i - 1) * RowCount - i - 1 + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.NorthWest)
                    Next
                End If
            ElseIf x = -y Then
                If x > 0 Then
                    l.Direction = EDirection.NorthEast
                    DirectionInvalidated(l.Direction)
                    l.StartPosition = x - 1
                    For i = x - 1 To (RowCount - 3) / 2
                        rls((-i - 1) * RowCount + i + 1 + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.NorthEast)
                    Next
                Else
                    l.Direction = EDirection.SouthWest
                    DirectionInvalidated(l.Direction)
                    l.StartPosition = -x - 1
                    For i = -x - 1 To (RowCount - 3) / 2
                        rls((i + 1) * RowCount - i - 1 + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.SouthWest)
                    Next
                End If
            Else
                Exit Sub
            End If
            TempLines.Add(l)
            StateChanged()
        End Sub
        Private Sub DirectionInvalidated(ByVal d As UShort)
            Dim index = TempLines.FindIndex(Function(_l As Line) As Boolean
                                                Return _l.Direction = d
                                            End Function)
            If index > -1 Then
                Dim l = TempLines(index)
                Select Case l.Direction
                    Case EDirection.North
                        For i = l.StartPosition To (RowCount - 3) / 2
                            rls((-i - 1) * RowCount + (RowCount ^ 2 - 1) / 2).RemoveDirection()
                        Next
                    Case EDirection.NorthEast
                        For i = l.StartPosition To (RowCount - 3) / 2
                            rls((-i - 1) * RowCount + i + 1 + (RowCount ^ 2 - 1) / 2).RemoveDirection()
                        Next
                    Case EDirection.East
                        For i = l.StartPosition To (RowCount - 3) / 2
                            rls(i + 1 + (RowCount ^ 2 - 1) / 2).RemoveDirection()
                        Next
                    Case EDirection.SouthEast
                        For i = l.StartPosition To (RowCount - 3) / 2
                            rls((i + 1) * RowCount + i + 1 + (RowCount ^ 2 - 1) / 2).RemoveDirection()
                        Next
                    Case EDirection.South
                        For i = l.StartPosition To (RowCount - 3) / 2
                            rls((i + 1) * RowCount + (RowCount ^ 2 - 1) / 2).RemoveDirection()
                        Next
                    Case EDirection.SouthWest
                        For i = l.StartPosition To (RowCount - 3) / 2
                            rls((i + 1) * RowCount - i - 1 + (RowCount ^ 2 - 1) / 2).RemoveDirection()
                        Next
                    Case EDirection.West
                        For i = l.StartPosition To (RowCount - 3) / 2
                            rls(-i - 1 + (RowCount ^ 2 - 1) / 2).RemoveDirection()
                        Next
                    Case EDirection.NorthWest
                        For i = l.StartPosition To (RowCount - 3) / 2
                            rls((-i - 1) * RowCount - i - 1 + (RowCount ^ 2 - 1) / 2).RemoveDirection()
                        Next
                End Select
                TempLines.RemoveAt(index)
                TargetAbb = ""
                lblAbb.Text = "等效简写："
            End If
        End Sub
        Private Sub StateChanged()
            TargetAbb = ""
            lblAbb.Text = "等效简写："
        End Sub
        Friend ReadOnly Property ResultRange As Range
            Get
                Return TargetRange
            End Get
        End Property
        Private Sub butOK_Click(sender As Object, e As EventArgs) Handles butOK.Click
            If TargetAbb = "" Then
                TargetRange.Points = New List(Of RelativePosition)
                TargetRange.Lines = TempLines
                For i = 0 To RowCount ^ 2 - 1
                    If rls(i).Accessible And rls(i).Direction = 0 Then
                        TargetRange.Points.Add(New RelativePosition((i Mod RowCount) - (RowCount - 1) / 2, (i \ RowCount) - (RowCount - 1) / 2))
                    End If
                Next
            End If
            Me.Close()
        End Sub
        Private Sub butQuick_Click(sender As Object, e As EventArgs) Handles butQuick.Click
            FromAbbreviation(InputBox("输入范围简写。"), TargetAbb, TargetRange)
            SetFromAbb()
        End Sub
        Private Sub SetFromAbb()
            If TargetAbb <> "" Then
                lblAbb.Text = "等效简写：" & TargetAbb
                For Each one In rls
                    one.RemoveDirection()
                Next
                SetFromRange(FromAbbreviationToRange(TargetAbb))
            End If
        End Sub
        Private Sub SetFromRange(ByVal r As Range)
            If r.Points IsNot Nothing Then
                For Each p In r.Points
                    Dim i As Int16 = p.Y * RowCount + p.X + (RowCount ^ 2 - 1) / 2
                    If i < 0 OrElse i > RowCount ^ 2 - 1 Then
                        MsgBox("超出界面显示范围，暂时只能以简写形式显示。")
                        Exit For
                    End If
                    rls(i).Accessible = True
                Next
            End If
            If r.Lines IsNot Nothing Then
                TempLines = r.Lines
                For Each l In r.Lines
                    Select Case l.Direction
                        Case EDirection.North
                            For i = l.StartPosition To (RowCount - 3) / 2
                                rls((-i - 1) * RowCount + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.North)
                            Next
                        Case EDirection.NorthEast
                            For i = l.StartPosition To (RowCount - 3) / 2
                                rls((-i - 1) * RowCount + i + 1 + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.NorthEast)
                            Next
                        Case EDirection.East
                            For i = l.StartPosition To (RowCount - 3) / 2
                                rls(i + 1 + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.East)
                            Next
                        Case EDirection.SouthEast
                            For i = l.StartPosition To (RowCount - 3) / 2
                                rls((i + 1) * RowCount + i + 1 + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.SouthEast)
                            Next
                        Case EDirection.South
                            For i = l.StartPosition To (RowCount - 3) / 2
                                rls((i + 1) * RowCount + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.South)
                            Next
                        Case EDirection.SouthWest
                            For i = l.StartPosition To (RowCount - 3) / 2
                                rls((i + 1) * RowCount - i - 1 + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.SouthWest)
                            Next
                        Case EDirection.West
                            For i = l.StartPosition To (RowCount - 3) / 2
                                rls(-i - 1 + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.West)
                            Next
                        Case EDirection.NorthWest
                            For i = l.StartPosition To (RowCount - 3) / 2
                                rls((-i - 1) * RowCount - i - 1 + (RowCount ^ 2 - 1) / 2).SetDirection(EDirection.NorthWest)
                            Next
                    End Select
                Next
            End If
        End Sub
        ''' <summary>
        ''' 用来指示范围是否可达的标签控件。
        ''' </summary>
        ''' <remarks></remarks>
        Friend Class RangeLabel
            Inherits SignalLabel
            Protected _a As Boolean
            Private _d As UInt16
            Private index As UInt16
            Friend Property Accessible As Boolean
                Get
                    Return _a
                End Get
                Set(value As Boolean)
                    _a = value
                    Select Case _a
                        Case False
                            Text = ""
                        Case True
                            Text = SignA
                    End Select
                End Set
            End Property
            Friend ReadOnly Property Direction As UInt16
                Get
                    Return _d
                End Get
            End Property
            Sub New(ByVal _i As UInt16)
                MyBase.New("×", "—")
                Width = LabelWidth
                Height = LabelWidth
                Font = New Font("宋体", LabelWidth / 2)
                index = _i
            End Sub
            Protected Overrides Sub RangeLabel_MouseUp(sender As Object, e As MouseEventArgs)
                Select Case e.Button
                    Case Windows.Forms.MouseButtons.Left
                        Accessible = Not Accessible
                        If Direction Then RaiseEvent DirectionInvalidated(Direction - 1)
                        RaiseEvent StateChanged()
                    Case Windows.Forms.MouseButtons.Right
                        If Direction = 0 Then RaiseEvent TrySetDirection(index)
                End Select
            End Sub
            Friend Event TrySetDirection(ByVal i As UInt16)
            Friend Event DirectionInvalidated(ByVal d As UInt16)
            Friend Event StateChanged()
            Friend Sub SetDirection(ByVal d As UInt16)
                _a = True
                Text = "—"
                _d = d + 1
            End Sub
            Friend Sub RemoveDirection()
                Accessible = False
                _d = 0
            End Sub
        End Class
    End Class

    Private Sub ButSearch_Click(sender As Object, e As EventArgs) Handles ButSearch.Click

    End Sub
End Class
