Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports Newtonsoft.Json.Linq

Public Class FrmMain
    Private CurrCard As Card
    Friend Shared Cards As New List(Of Card)
    Friend SearchCards As New List(Of Card)
    Private CurrentFile As String = ""
    Private LibName As String = ""
    Friend TempCamp As List(Of String)
    Friend TempPool As String = ""
    Friend TempEf As String
    Friend TempLabel As List(Of UShort)
    Friend TempDesc As String = ""
    Friend TempSeries As List(Of String)
    Friend TempPicPath As String = ""
    Private Searching As Boolean = False
    ' 开发者模式/浏览者模式 浏览者模式优先显示卡图
    Private BrowseMode As Boolean

    Private RandomEngine As New Random()

    Private HeroSpellControls As New List(Of Control)

    Private myFrmDeck As FrmDeck

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        HeroSpellControls.Add(lblSpecies)
        HeroSpellControls.Add(CbxSpecies)
        HeroSpellControls.Add(lblRarity)
        HeroSpellControls.Add(CbxRank)
        HeroSpellControls.Add(lblRank)
        HeroSpellControls.Add(CbxLimit)
        HeroSpellControls.Add(LblCamp)
        HeroSpellControls.Add(LblSeries)
        HeroSpellControls.Add(lblATK)
        HeroSpellControls.Add(TxtAtk)
        HeroSpellControls.Add(lblDef)
        HeroSpellControls.Add(TxtDef)

        MsgBox(UserHint)
    End Sub
    Private Sub DisplaySeletedCard()
        If LbxCards.SelectedIndex >= 0 Then
            Dim dir = Path.GetDirectoryName(CurrentFile) + "\"
            If FileIO.FileSystem.FileExists(dir + LibName + "_pic\" + LbxCards.SelectedItem.ID + ".png") Then
                PicCard.ImageLocation = dir + LibName + "_pic\" + LbxCards.SelectedItem.ID + ".png"
                PicCard.Visible = True
                lblCurtain.Visible = True
                ButDeveloperMode.Visible = True
            Else
                PicCard.Visible = False
                lblCurtain.Visible = False
                ButDeveloperMode.Visible = False
            End If
        End If
    End Sub

    Private Sub ChangeFile(fullPath As String)
        CurrentFile = fullPath
        LibName = Path.GetFileNameWithoutExtension(fullPath)
    End Sub
    Private Function ExistCard(ByVal id As UInteger) As Boolean
        Return Cards.FindIndex(Function(c As Card) As Boolean
                                   Return c.ID = id
                               End Function) > -1
    End Function
    Private Sub LblID_Click(sender As Object, e As EventArgs) Handles LblID.Click
        Dim i As Integer
        Do
            i = RandomEngine.Next(100000000)
        Loop While ExistCard(i)
        TxtID.Text = CStr(i).PadLeft(8, "0")
    End Sub
    Private Sub CbxType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbxType.SelectedIndexChanged
        Select Case CbxType.SelectedIndex
            Case 0, -1 ' 用于查找
                For Each one In HeroSpellControls
                    one.Visible = True
                Next
                CbxSpecies.Items.Clear()
            Case 1 ' 领袖
                For Each one In HeroSpellControls
                    one.Visible = False
                Next
            Case 2 ' 雇员
                For Each one In HeroSpellControls
                    one.Visible = True
                Next
                CbxSpecies.Items.Clear()
                For Each one In Card.SummonConditionName
                    CbxSpecies.Items.Add(one)
                Next
            Case 3 ' 策略
                For Each one In HeroSpellControls
                    one.Visible = True
                Next
                lblDef.Visible = False
                TxtDef.Visible = False
                CbxSpecies.Items.Clear()
                For Each one In Card.SpellTypeName
                    CbxSpecies.Items.Add(one)
                Next
            Case Else
                For Each one In HeroSpellControls
                    one.Visible = False
                Next
        End Select
    End Sub
    Private Sub SetCard(ByRef c As Card)
        c.Name = TxtName.Text
        c.ID = TxtID.Text
        c.Type = CbxType.SelectedIndex
        If c.Type = ECardType.Employee Or c.Type = ECardType.Strategy Then
            If c.Type = ECardType.Employee Then
                c.HeroSummonCondition = CbxSpecies.SelectedIndex
            ElseIf c.Type = ECardType.Strategy Then
                c.SpellType = CbxSpecies.SelectedIndex
            End If
            c.Rank = CbxRank.SelectedIndex
            If CbxLimit.SelectedIndex <= 0 Then CbxLimit.SelectedIndex = 1
            c.Limit = CbxLimit.SelectedIndex
            c.Camp = TempCamp
            c.Series = TempSeries
            c.ATK_EFF = Val(TxtAtk.Text)
            If c.Type = ECardType.Employee Then
                c.DEF = Val(TxtDef.Text)
            End If
        End If
        c.Pool = TempPool
        c.Effects = TempEf
        c.Labels = TempLabel
        c.Description = TempDesc
        c.CheckValid()
    End Sub
    Private Sub ButSaveCard_Click(sender As Object, e As EventArgs) Handles ButSaveCard.Click
        Dim cardid = Cards.FindIndex(Function(c As Card) As Boolean
                                         Return c.ID = TxtID.Text
                                     End Function)
        Try
            If cardid = -1 Then
                CurrCard = New Card()
                SetCard(CurrCard)
                Cards.Add(CurrCard)
                If Not Searching Then
                    LbxCards.Items.Add(CurrCard)
                    LbxCards.SelectedIndex = LbxCards.Items.Count - 1
                End If
            ElseIf Admin OrElse Not Cards(cardid).Official Then
                If Cards(cardid).Name = TxtName.Text OrElse MsgBox("该ID已存在不同名的卡，是否继续？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                    SetCard(CurrCard)
                    Cards(cardid) = CurrCard
                    If Not Searching Then
                        LbxCards.Items(cardid) = CurrCard
                    Else
                        Dim c As Card = LbxCards.SelectedItem
                        If c.ID = TxtID.Text Then
                            LbxCards.SelectedItem = CurrCard
                        Else
                            For i = 0 To LbxCards.Items.Count - 1
                                If c.Name = TxtID.Text Then
                                    LbxCards.Items(i) = CurrCard
                                End If
                            Next
                        End If
                    End If
                End If
            Else
                MsgBox("权限不足，无法修改！")
            End If
        Catch ex As Exception
            If cardid = -1 Then
                CurrCard = New Card()
            Else
                CurrCard = Cards(cardid)
            End If
            MsgBox(ex.Message)
        End Try

        GC.Collect()
    End Sub
    Private Sub Clear()
        CurrCard = New Card()
        TempCamp = Nothing
        TempPool = ""
        TempEf = Nothing
        TempLabel = Nothing
        TempDesc = ""
        TempSeries = Nothing
        For Each one In HeroSpellControls
            Dim tb = TryCast(one, TextBox)
            If tb IsNot Nothing Then tb.Text = ""
            Dim cb = TryCast(one, ComboBox)
            If cb IsNot Nothing Then cb.SelectedIndex = -1
        Next
        TxtName.Text = ""
        TxtID.Text = ""
        CbxSpecies.SelectedIndex = -1
        CbxType.SelectedIndex = -1
    End Sub
    Private Sub ButClear_Click(sender As Object, e As EventArgs) Handles ButClear.Click
        LbxCards.SelectedIndex = -1
        Clear()
    End Sub
    Private Sub LblDesc_Click(sender As Object, e As EventArgs) Handles LblDesc.Click
        Dim f As New FrmDesc(TempDesc)
        f.ShowDialog()
        If f.ResultValid Then
            TempDesc = f.TempDesc
        End If
    End Sub
    Private Sub LblPool_Click(sender As Object, e As EventArgs) Handles LblPool.Click
        TempPool = InputBox("输入该卡片所属的卡池~", , TempPool)
    End Sub
    Private Sub LblSeries_Click(sender As Object, e As EventArgs) Handles LblSeries.Click
        Dim f As New FrmSeries(TempSeries)
        f.ShowDialog()
        TempSeries = f.lss
    End Sub
    Private Sub LblCamp_Click(sender As Object, e As EventArgs) Handles LblCamp.Click
        Dim f As New FrmSeries(TempCamp)
        f.ShowDialog()
        TempCamp = f.lss
    End Sub
    Private Sub LblEffect_Click(sender As Object, e As EventArgs) Handles LblEffect.Click
        Dim fed As New FrmEffectDescription(TempEf, TempLabel)
        fed.ShowDialog()
        TempEf = fed.ResultEfs
        TempLabel = fed.ResultLabels
    End Sub
    Private Sub ButRemove_Click(sender As Object, e As EventArgs) Handles ButRemove.Click
        If LbxCards.SelectedIndex > -1 Then
            Dim c As Card = LbxCards.SelectedItem
            If Admin OrElse Not c.Official Then
                Cards.Remove(c)
                LbxCards.Items.Remove(c)
            Else
                MsgBox("权限不足！")
            End If
        End If
    End Sub
    Private Sub ButNew_Click(sender As Object, e As EventArgs) Handles ButNew.Click
        CurrentFile = ""
        LibName = ""
        LbxCards.Items.Clear()
        LbxCards.SelectedIndex = -1
        Clear()
        Cards = New List(Of Card)
    End Sub
    Private Sub SaveTo(ByRef cs As List(Of Card), ByVal file As String)
        If cs.Count = 0 Then MsgBox("当前卡片库不包含任何卡！") : Exit Sub
        If file = "" Then Exit Sub
        Dim dir = Path.GetDirectoryName(file)
        If FileIO.FileSystem.FileExists(file) Then FileIO.FileSystem.CopyFile(file, file & ".tmp") ' 有漏洞但懒得改了2333
        Dim fs As New FileStream(file, FileMode.Create)
        Dim download As Boolean = False
        Try
            Dim content As String = ""
            For Each card In cs
                If download Then

                End If
                content &= card.Serialize() & "#"
            Next
            content = Mid(content, 1, content.Length - 1)
            Dim bytes As Byte()
            bytes = DESEncrypt(content)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()
            FileIO.FileSystem.DeleteFile(file & ".tmp")
        Catch ex As Exception
            fs.Close()
        End Try
    End Sub
    Private Sub ButSaveAll_Click(sender As Object, e As EventArgs) Handles ButSaveAll.Click
        If Not FileIO.FileSystem.FileExists(CurrentFile) Then
            Dim sfd As New SaveFileDialog With {
           .Filter = "Fair Deal Card Database(*.fdcdb)|*.fdcdb",
           .AddExtension = True,
           .InitialDirectory = Application.StartupPath,
           .RestoreDirectory = True
        }
            sfd.ShowDialog()
            ChangeFile(sfd.FileName)
        End If
        SaveTo(Cards, CurrentFile)
    End Sub
    Private Function FuncRead(ByVal file As String) As List(Of Card)
        Dim result As New List(Of Card)
        Dim fs As New FileStream(file, FileMode.Open, FileAccess.Read)
        Dim bytes(fs.Length - 1) As Byte
        fs.Read(bytes, 0, fs.Length)
        Dim str As String = DESDecrypt(bytes)  'Encoding.Default.GetString(bytes) 'DESDecrypt(bytes) 
        Dim cs = str.Split("#")
        fs.Close()
        For Each Card In cs
            Dim briefings = Card.Split("|")
            Dim c As New Card() With {
                .Official = briefings(0),
                .ID = briefings(1),
                .Name = briefings(2),
                .Pool = briefings(3),
                .Description = briefings(4),
                .Type = Val(briefings(5))
            }
            If c.Type = ECardType.Leader Then
                c.Labels = New List(Of UShort)
                SplitAndAddToList(c.Labels, briefings(6), "}")
                c.Effects = briefings(7)
                result.Add(c)
            Else
                c.Rank = Val(briefings(6))
                c.Limit = Val(briefings(7))
                c.Camp = New List(Of String)
                SplitAndAddToList(c.Camp, briefings(8), "_")
                c.Series = New List(Of String)
                SplitAndAddToList(c.Series, briefings(9), "_")
                If c.Type = ECardType.Employee Then
                    c.HeroSummonCondition = Val(briefings(10))
                    c.ATK_EFF = Val(briefings(11))
                    c.DEF = Val(briefings(12))
                    c.Labels = New List(Of UShort)
                    SplitAndAddToList(c.Labels, briefings(13), "}")
                    c.Effects = briefings(14)
                    result.Add(c)
                ElseIf c.Type = ECardType.Strategy Then
                    c.SpellType = Val(briefings(10))
                    c.ATK_EFF = Val(briefings(11))
                    c.Labels = New List(Of UShort)
                    SplitAndAddToList(c.Labels, briefings(12), "}")
                    c.Effects = briefings(13)
                    result.Add(c)
                End If
            End If
        Next
        Return result
    End Function
    Friend Sub RenewList(ByRef cl As List(Of Card))
        LbxCards.Items.Clear()
        For Each Card In cl
            LbxCards.Items.Add(Card)
        Next
    End Sub
    Private Sub ButLoad_Click(sender As Object, e As EventArgs) Handles ButLoad.Click
        Clear()
        LbxCards.SelectedIndex = -1
        Cards = New List(Of Card)
        Dim ofd As New OpenFileDialog With {
            .Filter = "Fair Deal Card Database(*.fdcdb)|*.fdcdb",
            .InitialDirectory = Application.StartupPath,
            .RestoreDirectory = True
        }
        ofd.ShowDialog()
        If FileIO.FileSystem.FileExists(ofd.FileName) Then
            Cards = FuncRead(ofd.FileName)
            If Cards.Count > 0 Then
                RenewList(Cards)
                ChangeFile(ofd.FileName)
            End If
        Else
            MsgBox("指定的文件不存在~")
        End If
    End Sub
    Private Sub LbxCards_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbxCards.SelectedIndexChanged
        If LbxCards.SelectedIndex > -1 Then
            Clear()
            Dim ca = CType(LbxCards.SelectedItem, Card)
            If BrowseMode Then
                DisplaySeletedCard()
            End If
            If Not Searching Then CurrCard = ca Else
            CurrCard = Cards.Find(Function(c As Card) As Boolean
                                      Return c.ID = ca.ID
                                  End Function)
            TxtName.Text = CurrCard.Name
            TxtID.Text = CurrCard.ID
            CbxType.SelectedIndex = CurrCard.Type
            Select Case CurrCard.Type
                Case ECardType.Employee
                    CbxSpecies.SelectedIndex = CurrCard.HeroSummonCondition
                    TxtAtk.Text = CurrCard.ATK_EFF
                    TxtDef.Text = CurrCard.DEF
                Case ECardType.Strategy
                    CbxSpecies.SelectedIndex = CurrCard.SpellType
                    TxtAtk.Text = CurrCard.ATK_EFF
            End Select
            If CurrCard.Type = ECardType.Employee Or CurrCard.Type = ECardType.Strategy Then
                CbxRank.SelectedIndex = CurrCard.Rank
                CbxLimit.SelectedIndex = CurrCard.Limit
                TempCamp = CurrCard.Camp
                TempSeries = CurrCard.Series.Clone
            End If
            TempPool = CurrCard.Pool
            TempEf = CurrCard.Effects
            TempLabel = CurrCard.Labels.Clone
            TempDesc = CurrCard.Description
        End If
    End Sub
    Private Sub ButSearch_MouseUp(sender As Object, e As MouseEventArgs) Handles ButSearch.MouseUp
        Select Case e.Button
            Case Windows.Forms.MouseButtons.Left
                LbxCards.SelectedIndex = -1
                Searching = True
                RenewList(Cards.FindAll(Function(cc As Card) As Boolean
                                            If InStr(cc.Name, TxtName.Text) = 0 Or InStr(cc.ID, TxtID.Text) = 0 Or
        (CbxType.SelectedIndex > ECardType.None And cc.Type <> CbxType.SelectedIndex) Or
        (CbxRank.SelectedIndex > ERank.None And CbxRank.SelectedIndex <> cc.Rank) Or
        (Not CbxLimit.SelectedIndex = -1 And Not CbxLimit.SelectedIndex = cc.Limit) Or
        (TxtAtk.Text <> "" And cc.Type = ECardType.Leader) Or
        (TxtDef.Text <> "" And cc.Type <> ECardType.Employee) Or
        (Not TempPool = "" And Not TempPool = cc.Pool) Then Return False
                                            If TempLabel IsNot Nothing Then
                                                If cc.Labels Is Nothing Then Return False
                                                For Each l In TempLabel
                                                    If Not cc.Labels.Contains(l) Then Return False
                                                Next
                                            End If
                                            If TempCamp IsNot Nothing Then
                                                If cc.Camp Is Nothing Then Return False
                                                For Each l In TempCamp
                                                    If Not cc.Camp.Contains(l) Then Return False
                                                Next
                                            End If
                                            If TempSeries IsNot Nothing Then
                                                If cc.Series Is Nothing Then Return False
                                                For Each s In TempSeries
                                                    If Not cc.Series.Contains(s) Then Return False
                                                Next
                                            End If
                                            If TempEf IsNot Nothing Then
                                                If cc.Effects Is Nothing Then Return False
                                                Dim efs = TempEf.Split("|")
                                                For Each ef In efs
                                                    If Not cc.Effects.Contains(ef) Then Return False
                                                Next
                                            End If
                                            If TxtAtk.Text <> "" Then
                                                If TxtAtk.Text.EndsWith("+") Then
                                                    If cc.ATK_EFF < Val(TxtAtk.Text) Then Return False
                                                ElseIf TxtAtk.Text.EndsWith("-") Then
                                                    If cc.ATK_EFF > Val(TxtAtk.Text) Then Return False
                                                Else
                                                    If cc.ATK_EFF <> Val(TxtAtk.Text) Then Return False
                                                End If
                                            End If
                                            If TxtDef.Text <> "" Then
                                                If TxtDef.Text.EndsWith("+") Then
                                                    If cc.DEF < Val(TxtDef.Text) Then Return False
                                                ElseIf TxtDef.Text.EndsWith("-") Then
                                                    If cc.DEF > Val(TxtDef.Text) Then Return False
                                                Else
                                                    If cc.DEF <> Val(TxtDef.Text) Then Return False
                                                End If
                                            End If
                                            If CbxType.SelectedIndex = ECardType.Employee Then
                                                If CbxSpecies.SelectedIndex > EEmployeeType.None And CbxSpecies.SelectedIndex <>
                                                    cc.HeroSummonCondition Then Return False
                                            ElseIf CbxType.SelectedIndex = ECardType.Strategy Then
                                                If CbxSpecies.SelectedIndex > EStrategy.None And CbxSpecies.SelectedIndex <>
                                                    cc.SpellType Then Return False
                                            End If
                                            Return True
                                        End Function))

                Clear()
                MsgBox(LbxCards.Items.Count & "个搜索结果。")

                ButSearch.Text = "右键复原233"
            Case Windows.Forms.MouseButtons.Right
                LbxCards.SelectedIndex = -1
                Clear()
                Searching = False
                RenewList(Cards)

                ButSearch.Text = "搜索满足条件的卡片..."
        End Select
    End Sub
    Private Function FindCard(ByVal id As UInteger) As Card
        Dim i = Cards.FindIndex(Function(c As Card) As Boolean
                                    Return c.ID = id
                                End Function)
        If i > -1 Then Return Cards(i) Else Return Nothing
    End Function
    Private Sub ButImport_MouseUp(sender As Object, e As MouseEventArgs) Handles ButImport.MouseUp
        Select Case e.Button
            Case Windows.Forms.MouseButtons.Left
                Dim ofd As New OpenFileDialog With {
                    .Filter = "The Soul Hunter Card Database(*.fdcdb)|*.fdcdb",
                    .InitialDirectory = Application.StartupPath,
                    .RestoreDirectory = True
                }
                ofd.ShowDialog()
                If FileIO.FileSystem.FileExists(ofd.FileName) Then
                    Dim cs = FuncRead(ofd.FileName)
                    If cs.Any Then
                        For Each Card In cs
                            Dim cc = FindCard(Card.ID)
                            If cc IsNot Nothing Then
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
                    .Filter = "The Soul Hunter Card Database(*.fdcdb)|*.fdcdb",
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
    Private Sub ButExport_MouseUp(sender As Object, e As MouseEventArgs) Handles ButExport.MouseUp
        Select Case e.Button
            Case MouseButtons.Left
                Dim icards = FrmChoose.CallFrmChoose(Cards)
                Dim cs As New List(Of Card)
                For Each one In icards
                    cs.Add(one)
                Next
                Dim sfd As New SaveFileDialog With {
                    .Filter = "The Soul Hunter Card Database(*.fdcdb)|*.fdcdb",
                    .AddExtension = True,
                    .InitialDirectory = Application.StartupPath,
                    .RestoreDirectory = True
                }
                sfd.ShowDialog()
                SaveTo(cs, sfd.FileName)
            Case MouseButtons.Right
                Dim cs As New List(Of Card)
                For Each card In LbxCards.Items
                    cs.Add(card)
                Next
                Dim sfd As New SaveFileDialog With {
                    .Filter = "The Soul Hunter Card Database(*.fdcdb)|*.fdcdb",
                    .AddExtension = True,
                    .InitialDirectory = Application.StartupPath,
                    .RestoreDirectory = True
                }
                sfd.ShowDialog()
                SaveTo(cs, sfd.FileName)
        End Select
    End Sub
    Private Sub ButDeck_Click(sender As Object, e As EventArgs) Handles ButDeck.Click
        If Not myFrmDeck Is Nothing AndAlso myFrmDeck.Visible = True Then myFrmDeck.Close()
        myFrmDeck = New FrmDeck(Cards, Me)
        myFrmDeck.Show()
        GC.Collect()
    End Sub
    Private Sub LbxCards_DoubleClick(sender As Object, e As EventArgs) Handles LbxCards.DoubleClick
        If Not myFrmDeck Is Nothing AndAlso myFrmDeck.Visible = True And LbxCards.SelectedIndex >= 0 Then
            myFrmDeck.TryAdd(LbxCards.SelectedItem)
        End If
    End Sub

    Private Sub ButNet_Click(sender As Object, e As EventArgs) Handles ButNet.Click
        Dim ja As New JArray()
        For Each c In Cards
            Dim j As New JObject()
            j.Add("id", c.ID)
            j.Add("name", c.Name)
            j.Add("rank", c.Rank - 1)
            Dim l As Integer = 3 - c.Limit
            If c.Rank = ERank.Trump Then l = 1
            If c.Limit >= ELimit.Forbidden Then l = 0
            j.Add("limit", l)
            j.Add("pool", 1)
            ja.Add(j)
        Next
        Dim fs As New FileStream("fxxk.txt", FileMode.Create)
        Dim b As Byte() = Encoding.UTF8.GetBytes(ja.ToString())
        Dim bc = b.Length()
        fs.Write(b, 0, bc)
        fs.Close()
    End Sub

    Private Sub ButModeSwitch_Click(sender As Object, e As EventArgs) Handles ButModeSwitch.Click
        BrowseMode = True
        DisplaySeletedCard()
    End Sub

    Private Sub ButDeveloperMode_MouseUp(sender As Object, e As MouseEventArgs) Handles ButDeveloperMode.MouseUp
        Select Case e.Button
            Case MouseButtons.Left
                BrowseMode = False
                PicCard.Visible = False
                lblCurtain.Visible = False
                ButDeveloperMode.Visible = False
            Case MouseButtons.Right
                PicCard.Visible = False
                lblCurtain.Visible = False
                ButDeveloperMode.Visible = False
        End Select
    End Sub

    Private Sub LblForbiddenGroup_Click(sender As Object, e As EventArgs) Handles LblForbiddenGroup.Click

    End Sub
End Class