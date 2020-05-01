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
End Enum

Public Enum EStrategy
    None
    Common
    Lasting
    Attachment
    Counter
    CounterLasting
    Instant
    InstantLasting
    Contract
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
    Public Shared SummonConditionName As String() = {"", "常规", "契约"}
    Public HeroSummonCondition As EEmployeeType
    Public Shared SpellTypeName As String() = {"", "常规", "持续", "单人", "反制", "反制|持续", "立即", "立即|持续", "契约"}
    Public SpellType As EStrategy
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

    Public Sub CheckValid()
        If mType = ECardType.None Then
            Throw New Exception("未选择卡片种类~")
        End If

        Select Case mType
            Case ECardType.Employee
                If HeroSummonCondition < 1 Or HeroSummonCondition > EEmployeeType.Contract Then
                    Throw New Exception("未选择该雇员的上场方式~")
                End If
                If Rank = ERank.None Then
                    Throw New Exception("未选择卡片等级~")
                End If
            Case ECardType.Strategy
                If SpellType < 1 Or SpellType > EStrategy.Contract Then
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
                r += CInt(HeroSummonCondition).ToString() + "|" + ATK_EFF.ToString() + "|" + DEF.ToString() + "|"
            ElseIf mType = ECardType.Strategy Then
                r += CInt(SpellType).ToString() + "|" + ATK_EFF.ToString() + "|"
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
