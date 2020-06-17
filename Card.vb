Imports System.Text.RegularExpressions

Friend Structure Card
    Friend Name As String
    Friend ID As String
    Friend Type As EType
    Friend Series As List(Of String)
    Friend Species As ESpecies
    Friend StraregyType As EStraregyType
    Friend Occupy As Int16
    Friend SummonCondition As SSummonCondition
    Friend HP As UInt32
    Friend ATK As Int16
    Friend MP As UInt16
    Friend MPMax As UInt16
    ''' <summary>
    ''' 攻击范围。
    ''' </summary>
    ''' <remarks></remarks>
    Friend AR As Range
    ''' <summary>
    ''' 移动范围。
    ''' </summary>
    ''' <remarks></remarks>
    Friend MR As Range
    Friend EquivalentAR As String, EquivalentMR As String
    ''' <summary>
    ''' 降灵单位持有属性。降临可行范围。
    ''' </summary>
    ''' <remarks></remarks>
    Friend CeremoryFormationSummon As Range
    ''' <summary>
    ''' 降灵单位持有属性。降灵素材位置集合。
    ''' </summary>
    ''' <remarks></remarks>
    Friend CeremoryFormationMaterial As Range
    Friend Effect As List(Of SEffect)
    ''' <summary>
    ''' 卡片的标签。
    ''' </summary>
    ''' <remarks></remarks>
    Friend Label As List(Of UInt16)
    ''' <summary>
    ''' 指示该卡片是否是官方卡片。
    ''' </summary>
    ''' <remarks></remarks>
    Friend Official As Boolean
    Friend Shared SpeciesName As String() = {"", "战士", "魔使", "机械", "兽族", "虚幻"}
    Friend Enum ESpecies
        None
        Warrior
        SpellCaster
        Machine
        Beast
        Phantom
    End Enum
    Friend Shared StrategyTypeName As String() = {"", "普通", "持续", "背景"}
    Friend Enum EStraregyType
        None
        Normal
        Lasting
        Background
    End Enum
    Friend Enum EType
        None
        ''' <summary>
        ''' 单位。
        ''' </summary>
        ''' <remarks></remarks>
        Unit
        ''' <summary>
        ''' 锦囊。
        ''' </summary>
        ''' <remarks></remarks>
        Strategy
    End Enum
    Friend Structure Line
        Friend Direction As EDirection
        Friend StartPosition As UInt16
    End Structure
    Friend Structure Range
        Friend Lines As List(Of Line)
        Friend Points As List(Of RelativePosition)
        Public Overrides Function ToString() As String
            Dim result As String = ""
            If Lines IsNot Nothing Then
                For Each l In Lines
                    result &= l.Direction & "," & l.StartPosition & "-"
                Next
            End If
            result &= "="
            If Points IsNot Nothing Then
                For Each p In Points
                    result &= p.X & "," & p.Y & "."
                Next
            End If
            Return result
        End Function
        Friend Function IsNotNothing() As Boolean
            Return (Lines IsNot Nothing AndAlso Lines.Any) OrElse (Points IsNot Nothing AndAlso Points.Any)
        End Function
    End Structure
    Friend Enum EDirection
        North
        NorthEast
        East
        SouthEast
        South
        SouthWest
        West
        NorthWest
    End Enum
    ''' <summary>
    ''' 表示一个相对位置。
    ''' </summary>
    ''' <remarks></remarks>
    Friend Structure RelativePosition
        Friend X As Int16, Y As Int16
        Sub New(_x As Int16, _y As Int16)
            X = _x : Y = _y
        End Sub
    End Structure
    Friend Structure SEffect
        Friend Description As String
    End Structure
    Friend Class EffectList
        Friend Effect As List(Of SEffect)
        Friend Function Clone() As EffectList
            Return MemberwiseClone()
        End Function
    End Class
    Friend Structure SSummonCondition
        Friend SummonCondition As ESummonCondition
        Friend Condition As String
        Public Overrides Function ToString() As String
            Dim result As String = ""
            Select Case SummonCondition
                Case ESummonCondition.Summon
                    result = "COST"
                Case ESummonCondition.Sacrifice
                    result = "献"
                Case ESummonCondition.Fusion
                    result = "集"
                Case ESummonCondition.Ceremony
                    result = "临"
                Case ESummonCondition.Link
                    result = "连"
                Case ESummonCondition.Possess
                    result = "附"
                Case ESummonCondition.Echo
                    result = "唤"
            End Select
            result &= Condition
            Return result
        End Function
    End Structure
    Friend Enum ESummonCondition
        None = 1
        Summon = 2
        Sacrifice = 4
        Fusion = 8
        Ceremony = 16
        Link = 32
        Possess = 64
        Echo = 128
    End Enum
    Friend Function ToStr() As String
        Dim result As String = Official & "|" & ID & "|" & Name & "|" & Type & "|"
        Dim ef As String = "", se As String = ""
        If Series IsNot Nothing Then
            For i = 0 To Series.Count - 1
                se &= Series(i) & ","
            Next
            se = Mid(se, 1, se.Length - 1)
        Else
            se &= " "
        End If
        se &= "|"
        result &= se
        If Label IsNot Nothing Then
            For i = 0 To Label.Count - 1
                ef &= Label(i) & ","
            Next
            ef = Mid(ef, 1, ef.Length - 1)
        Else
            ef &= " "
        End If
        ef &= "|"
        If Effect IsNot Nothing Then
            For Each e In Effect
                ef &= e.Description & "|"
            Next
        End If
        ef = Mid(ef, 1, ef.Length - 1)
        If Type = EType.Unit Then result &= Species & "|" & SummonCondition.SummonCondition & "|" &
           IIf(SummonCondition.SummonCondition = ESummonCondition.Ceremony, CeremoryFormationSummon.ToString &
           "|" & CeremoryFormationMaterial.ToString & "|", "") &
           SummonCondition.Condition & "|" & HP & "|" & MP & "|" &
           MPMax & "|" & ATK & "|" & IIf(EquivalentAR <> "", EquivalentAR, AR.ToString) &
           "|" & IIf(EquivalentMR <> "", EquivalentMR, MR.ToString) & "|" & ef Else result &= StraregyType &
           "|" & Occupy & "|" & ef
        Return result
    End Function

    Friend Shared Sub FromAbbreviation(ByVal a As String, ByRef abb As String, ByRef result As Range)
        result = FromAbbreviationToRange(a)
        If result.IsNotNothing Then
            abb = a
        End If
    End Sub
    Friend Shared Function RhombusRange(ByVal r As UInt16) As Range
        Dim result As New Range
        result.Points = New List(Of RelativePosition)
        For i = 0 To 2 * r
            If i <= r Then
                For j = 0 To 2 * i
                    If Not i = j OrElse Not i = r Then
                        result.Points.Add(New RelativePosition(j - i, i - r))
                    End If
                Next
            Else
                For j = 0 To 4 * r - 2 * i
                    result.Points.Add(New RelativePosition(j - r * 2 + i, i - r))
                Next
            End If
        Next
        Return result
    End Function
    Friend Shared Function RectRange(ByVal r As UInt16) As Range
        Dim result As New Range
        result.Points = New List(Of RelativePosition)
        For i = 0 To 2 * r
            For j = 0 To 2 * r
                If Not i = j OrElse Not i = r Then
                    result.Points.Add(New RelativePosition(j - r, i - r))
                End If
            Next
        Next
        Return result
    End Function
    Private Shared Function StewRange(ByVal drc As EDirection, ByVal r As UInt16) As RelativePosition()
        r -= 1
        Dim result(r) As RelativePosition
        Select Case drc
            Case EDirection.North
                For i = 0 To r
                    result(i).Y = -i - 1
                Next
            Case EDirection.NorthEast
                For i = 0 To r
                    result(i).X = i + 1
                    result(i).Y = -i - 1
                Next
            Case EDirection.East
                For i = 0 To r
                    result(i).X = i + 1
                Next
            Case EDirection.SouthEast
                For i = 0 To r
                    result(i).X = i + 1
                    result(i).Y = i + 1
                Next
            Case EDirection.South
                For i = 0 To r
                    result(i).Y = i + 1
                Next
            Case EDirection.SouthWest
                For i = 0 To r
                    result(i).X = -i - 1
                    result(i).Y = i + 1
                Next
            Case EDirection.West
                For i = 0 To r
                    result(i).X = -i - 1
                Next
            Case EDirection.NorthWest
                For i = 0 To r
                    result(i).X = -i - 1
                    result(i).Y = -i - 1
                Next
        End Select
        Return result
    End Function
    Friend Shared Function CrossRange(ByVal r As Int16())
        Dim result As New Range
        result.Points = New List(Of RelativePosition)
        result.Lines = New List(Of Line)
        For i = 0 To 7
            If r(i) = -1 Then
                result.Lines.Add(New Line() With {.Direction = i})
            ElseIf r(i) > 0 Then
                result.Points.AddRange(StewRange(i, r(i)))
            End If
        Next
        Return result
    End Function
    Friend Shared Function FromAbbreviationToRange(ByVal a As String) As Range
        Dim r As New Regex("^<[0-9]+>$")
        Dim ran As New Range
        ran.Points = New List(Of RelativePosition)
        Dim match As Match = r.Match(a)
        If match.Success Then
            Dim m As UInt16 = a.Replace("<", "").Replace(">", "")
            For i = 0 To 2 * m
                If i <= m Then
                    For j = 0 To 2 * i
                        If Not i = j OrElse Not i = m Then
                            ran.Points.Add(New RelativePosition(j - i, i - m))
                        End If
                    Next
                Else
                    For j = 0 To 4 * m - 2 * i
                        ran.Points.Add(New RelativePosition(j - m * 2 + i, i - m))
                    Next
                End If
            Next
            Return ran
            Exit Function
        End If
        r = New Regex("^\[[0-9]+\]$")
        match = r.Match(a)
        If match.Success Then
            Dim m As UInt16 = a.Replace("[", "").Replace("]", "")
            For i = 0 To 2 * m
                For j = 0 To 2 * m
                    If Not i = j OrElse Not i = m Then
                        ran.Points.Add(New RelativePosition(j - m, i - m))
                    End If
                Next
            Next
            Return ran
            Exit Function
        End If
        r = New Regex("^[0-9~]\+$")
        match = r.Match(a)
        If match.Success Then
            Dim mstr As String = a.Replace("+", "")
            Dim _r(7) As Int16
            For i = 0 To 3
                _r(i * 2) = IIf(mstr = "~", -1, mstr)
            Next
            Return CrossRange(_r)
            Exit Function
        End If
        r = New Regex("^[0-9~] [0-9~]\*$")
        match = r.Match(a)
        If match.Success Then
            Dim mstr As String() = a.Replace("*", "").Split(" ")
            Dim _r(7) As Int16
            For i = 0 To 7
                _r(i) = IIf(mstr(i Mod 2) = "~", -1, mstr(i Mod 2))
            Next
            Return CrossRange(_r)
            Exit Function
        End If
        r = New Regex("^[0-9~]\*$")
        match = r.Match(a)
        If match.Success Then
            Dim mstr As String = a.Replace("*", "")
            Dim _r(7) As Int16
            For i = 0 To 7
                _r(i) = IIf(mstr = "~", -1, mstr)
            Next
            Return CrossRange(_r)
            Exit Function
        End If
        r = New Regex("^\+([0-9~] ){3}[0-9~]$")
        match = r.Match(a)
        If match.Success Then
            Dim mstr As String() = a.Replace("+", "").Split(" ")
            Dim _r(7) As Int16
            For i = 0 To 3
                _r(i * 2) = IIf(mstr(i) = "~", -1, mstr(i))
            Next
            Return CrossRange(_r)
            Exit Function
        End If
        r = New Regex("^\*([0-9~] ){7}[0-9~]$")
        match = r.Match(a)
        If match.Success Then
            Dim mstr As String() = a.Replace("*", "").Split(" ")
            Dim _r(7) As Int16
            For i = 0 To 7
                _r(i) = IIf(mstr(i) = "~", -1, mstr(i))
            Next
            Return CrossRange(_r)
            Exit Function
        End If
        Return Nothing
    End Function
    Friend Shared Function FromRangeToAbbreviation(ByVal r As Range) As String
        Return Nothing
    End Function

    Public Overrides Function ToString() As String
        Return ID & " " & Name & IIf(Official, "(官方)", "")
    End Function
End Structure