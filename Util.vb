Imports System.Collections.Generic
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Module Util
    Private Const iv As String = "JiLiGuaLa"
    Private Const key As String = "jlgljlgl"
    Friend Function DESDecrypt(encryptedString As Byte()) As String
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
    Friend Function DESEncrypt(sourceString As String) As Byte()
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
    Friend Sub SplitAndAddToList(ByRef l As List(Of String), ByVal text As String, ByVal spsym As String)
        If Not text = "" Then
            For Each one In text.Split(spsym)
                l.Add(one)
            Next
        End If
    End Sub
    Friend Sub SplitAndAddToList(ByRef l As List(Of UShort), ByVal text As String, ByVal spsym As String)
        If Not text = "" Then
            For Each one In text.Split(spsym)
                l.Add(Val(one))
            Next
        End If
    End Sub
End Module
