Imports System.Net

Friend Class AfterUpdate
    Public Sub AfterUpdateCALL(ByVal EMAIL As String, ByVal Name As String, ByVal Pgname As String, ByVal HDMSG As String, ByVal MMSG As String)

        '送信者
        Dim senderMail As String = "renaiss@elle-rose.co.jp"
        '宛先
        Dim recipientMail As String = EMAIL
        '件名
        Dim subject As String = HDMSG
        '本文
        Dim body As String = "こんにちは。" + vbCrLf + vbCrLf + Name & "様" + vbCrLf + vbCrLf + Pgname + MMSG

        'SmtpClientオブジェクトを作成する
        Dim sc As New System.Net.Mail.SmtpClient()
        'SMTPサーバーを指定する
        sc.Host = "www.hokutos.co.jp"
        'ポート番号を指定する（既定値は25）
        sc.Port = 587

        sc.Credentials = New NetworkCredential("Aps_admin10", "Quadkan_aps10")
        'メールを送信する
        sc.Send(senderMail, recipientMail, subject, body)
        '後始末（.NET Framework 4.0以降）
        sc.Dispose()

    End Sub
End Class
