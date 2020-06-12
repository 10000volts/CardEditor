Imports System.Collections.Generic

Public Enum ECardType
    None
    Leader
    Employee
    Strategy
End Enum

Public Enum EEmployeeType
    None
    Common
    Contract
    Inherit = 4
    Partnership = 8
    Secret = 16
End Enum

Public Enum EStrategy
    None
    Common
    Lasting
    Attachment = 4
    Counter = 8
    CounterLasting = 10
    Contract = 16
    Background = 32
End Enum

Public Enum ERank
    None
    Common
    Good
    Trump
    Changeable
End Enum

Public Enum ELimit
    None
    Unlimited
    Limited
    Forbidden
    Derived
End Enum

Public Class Card
    Public Property ID As String
        Get
            Return m_ID
        End Get
        Set(value As String)
            If value = "" Then Throw New Exception("ID只能是长度8位的纯数字~")
            If Val(value) <> value OrElse value.Length <> 8 Then
                Throw New Exception("ID只能是长度8位的纯数字~" & vbCrLf &
                                    "提示: 可以点击""ID:""来快速生成随机ID哦~")
            End If
            m_ID = value
        End Set
    End Property
    Private m_ID As String
    Public Property Name As String
        Get
            Return mName
        End Get
        Set(value As String)
            If value = "" Then Throw New Exception("卡名不能为空~")
            If InStr(value, "(官方)") Then
                Throw New Exception("无效卡名~")
            End If
            mName = value
        End Set
    End Property
    Private mName As String
    Public Property Type As ECardType
        Get
            Return mType
        End Get
        Set(value As ECardType)
            If value < 1 Or value > ECardType.Strategy Then
                Throw New Exception("卡片种类不太对劲~")
            End If
            mType = value
        End Set
    End Property
    Private mType As ECardType
    Public Shared SummonConditionName As String() = {"", "常规", "契约", "继承", "合约", "秘密"}
    Public HeroSummonCondition As Integer
    Public Shared SpellTypeName As String() = {"", "常规", "持续", "单人", "反制", "契约", "场地"}
    Public SpellType As Integer
    Public Shared RankName As String() = {"", "普通", "优质", "王牌"}
    Public Property Rank As ERank
        Get
            Return mRank
        End Get
        Set(value As ERank)
            If value < 1 Or value > ERank.Changeable Then
                Throw New Exception("卡片等级不太对劲~")
            End If
            mRank = value
        End Set
    End Property
    Private mRank As ERank
    Public Property Official As Boolean
        Get
            Return mOfficial
        End Get
        Set(value As Boolean)
            mOfficial = value
        End Set
    End Property
    Private mOfficial As Boolean = Admin
    Public Property Limit As ELimit
        Get
            Return mLimit
        End Get
        Set(value As ELimit)
            If value < 1 Or value > ELimit.Derived Then
                Throw New Exception("卡片限制等级不太对劲~")
            End If
            mLimit = value
        End Set
    End Property
    Private mLimit As ELimit
    ''' 攻击力/效力。
    Public ATK_EFF As Integer
    ''' 防御力。
    Public DEF As Integer
    Public Camp As List(Of String)
    Public Pool As String = ""
    Public Effects As String
    Public Labels As List(Of UShort)
    Public Series As List(Of String)
    Public Description As String

    Public Shared Function GetSubtype(t As ECardType, st As Integer) As String
        Dim res As String = ""
        If st = 0 Then Return ""
        Select Case t
            Case ECardType.Employee
                Dim l = UBound(SummonConditionName) - 1
                For i = 0 To l
                    If (st And 2 ^ i) > 0 Then
                        res += SummonConditionName(i + 1) + "|"
                    End If
                Next
            Case ECardType.Strategy
                Dim l = UBound(SpellTypeName) - 1
                For i = 0 To l
                    If (st And 2 ^ i) > 0 Then
                        res += SpellTypeName(i + 1) + "|"
                    End If
                Next
            Case Else
                Return ""
        End Select
        If res = "" Then Return ""
        Return Mid(res, 1, res.Length - 1)
    End Function
    Public Sub CheckValid()
        If mType = ECardType.None Then
            Throw New Exception("未选择卡片种类~")
        End If

        Select Case mType
            Case ECardType.Employee
                If HeroSummonCondition < 1 Then
                    Throw New Exception("未选择该雇员的上场方式~")
                End If
                If Rank = ERank.None Then
                    Throw New Exception("未选择卡片等级~")
                End If
            Case ECardType.Strategy
                If SpellType < 1 Then
                    Throw New Exception("未选择该策略的类型~")
                End If
                If Rank = ERank.None Then
                    Throw New Exception("未选择卡片等级~")
                End If
        End Select
    End Sub
    Public Overrides Function ToString() As String
        If mType = ECardType.Leader Then Return "领袖 " + Name + IIf(Official, "(官方)", "")
        Dim prefix = ""
        Select Case mLimit
            Case ELimit.Limited
                prefix = "(限制)"
            Case ELimit.Forbidden
                prefix = "(禁止)"
            Case ELimit.Derived
                prefix = "(衍生)"
        End Select
        Return RankName(Rank) + " " + prefix + Name + IIf(Official, "(官方)", "")
    End Function
    Private Shared empT As Dictionary(Of Integer, Integer)
    Private Shared straT As Dictionary(Of Integer, Integer)
    Shared Sub New()
        empT = New Dictionary(Of Integer, Integer)
        straT = New Dictionary(Of Integer, Integer)
        empT(0) = 0
        empT(1) = 1
        empT(2) = 2
        empT(3) = 4
        empT(4) = 8
        empT(5) = 16
        straT(0) = 0
        straT(1) = 1
        straT(2) = 2
        straT(3) = 4
        straT(4) = 8
        straT(5) = 10
        straT(8) = 16
        straT(9) = 32
    End Sub
    Public Function Serialize() As String
        Dim r As String = mOfficial.ToString() + "|" + m_ID.ToString() + "|" +
            mName + "|" + Pool + "|" +
            Description + "|" + CInt(mType).ToString() + "|"
        If mType = ECardType.Employee Or mType = ECardType.Strategy Then
            r += CInt(Rank).ToString() + "|" + CInt(mLimit).ToString() + "|"
            If Camp IsNot Nothing AndAlso Camp.Count > 0 Then
                For Each one In Camp
                    r += one + "_"
                Next
                r = Mid(r, 1, r.Length - 1)
            End If
            r += "|"
            If Series IsNot Nothing AndAlso Series.Count > 0 Then
                For Each one In Series
                    r += one + "_"
                Next
                r = Mid(r, 1, r.Length - 1)
            End If
            r += "|"
            If mType = ECardType.Employee Then
                r += HeroSummonCondition.ToString() + "|" + ATK_EFF.ToString() + "|" + DEF.ToString() + "|"
                ' r += empT(HeroSummonCondition).ToString() + "|" + ATK_EFF.ToString() + "|" + DEF.ToString() + "|"
            ElseIf mType = ECardType.Strategy Then
                r += SpellType.ToString() + "|" + ATK_EFF.ToString() + "|"
                ' r += straT(SpellType).ToString() + "|" + ATK_EFF.ToString() + "|"
            End If
        End If
        If Labels IsNot Nothing AndAlso Labels.Count > 0 Then
            For Each one In Labels
                r += one.ToString() + "}"
            Next
            r = Mid(r, 1, r.Length - 1)
        End If
        r += "|"
        r += Effects
        Return r
    End Function
End Class
