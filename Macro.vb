Imports System.Runtime.CompilerServices
Imports System.Collections.Generic

Module Macro
#If DEBUG Then
    Friend Const Admin = True
#Else
    Friend Const Admin = False
#End If

    Public Const MapMaxLength As Integer = 7
    Public Const LabelWidth = 40

    <Extension()>
    Friend Function Clone(Of T)(ByRef l As List(Of T)) As List(Of T)
        If l Is Nothing Then Return Nothing
        Dim result As New List(Of T)
        For Each one In l
            Dim i As T = one
            result.Add(i)
        Next
        Return result
    End Function

    Friend LabelContents As String() = {"生命回复", "效果伤害", "生命损失", "生命支付", "情报", "特殊胜利", "伤害增减",
                                        "属性变化", "站场", "铺场", "雇员摧毁", "策略摧毁", "姿态相关",
                                        "保护", "控制权", "场上移除", "特殊入场", "离场效果",
                                        "附加值", "属性修改",
                                        "墓地利用", "墓地移除", "移除回归",
                                        "额外资源", "丢弃手牌", "移除手牌", "加入手牌",
                                        "无效化", "封锁", "延迟起效",
                                        "额外机会",
                                        "陷阱", "白板", "回合相关", "组件", "构筑相关",
                                        "属性突变", "群体效果", "全局效果", "替代", "容错", "roll池外", "特殊roll",
                                        "特殊放取", "加入卡组", "移除卡组", "卡组堆墓"}

    Friend UserHint As String = "欢迎使用公平交易卡片编辑器~希望亲能花59秒稍微阅读一下下面的操作提示~" & vbCrLf &
        "1、可以点击 ""ID:"" 快速创建唯一ID~" & vbCrLf &
        "2、不可以使用的符号：|_}#" & vbCrLf &
        "3、在编辑卡组时，双击卡片编辑界面中卡片列表内的项目可以将相应的卡添加到卡组。" & vbCrLf &
        "4、搜索属性时，您可以在数值的末尾添加+或-以表示至少或至多，否则将进行精确搜索。" & vbCrLf &
        "5、如果您想要搜索效果，您可以在效果编辑页面用"“|”"把不同的搜索条件分开，比如"“穿透|入场”"" & vbCrLf &
        "6、如果您不需要使用编辑卡片的功能，您可以切换成浏览者模式以获得更好的体验。"
End Module
