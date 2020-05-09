Imports System.Data.SqlClient
Public Class Registration_0
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Redirect("Registration_1.aspx")

    End Sub

    'ADD----------------------------------------------2018/03/06 TOS163
    Private Sub btnNishi_Click(sender As Object, e As EventArgs) Handles btnNishi.Click

        Dim con As New SqlConnection
        con.ConnectionString = ConfigurationManager.ConnectionStrings("renaissDBConnectionString").ConnectionString
        con.Open()

        '
        Dim strSql As String = ""
        strSql = strSql & " SELECT 開始日, 終了日"
        strSql = strSql & " FROM WebsiteReleasePeriod "
        strSql = strSql & " WHERE 店舗ＣＤ = @店舗ＣＤ"

        Dim sqlcom As SqlCommand = New SqlCommand(strSql, con)
        sqlcom.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.Char)).Value = 6

        Dim dt As New DataTable()
        Dim sda As New SqlDataAdapter(sqlcom)
        sda.Fill(dt)

        If dt.Rows.Count <> 0 Then

            Dim StrtTime As DateTime = dt.Rows(0).Item(0)
            Dim EndTime As DateTime
            If dt.Rows(0).Item(1).ToString = "" Then
                EndTime = "9999/01/31 22:59:59"
            Else
                EndTime = dt.Rows(0).Item(1)
            End If
            Dim day As DateTime = DateTime.UtcNow.AddHours(9)

            Dim hiduke As String = StrtTime.ToString("MM月dd日")
            Dim zikan As DateTime = StrtTime.AddSeconds(1)
            Dim szikan As String = zikan.ToString("tthh時")

            '開始日までは予約不可
            If StrtTime > day Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alertscript5", "window.alert('" & hiduke & "の" & szikan & "から予約ができるようになります。');", True)
                Exit Sub
            End If
            '終了日で予約一時終了
            If EndTime < day Then
                Response.Redirect("https://www.renaiss-news.com/")
                Exit Sub
            End If

        End If



        Response.Redirect("Registration-ns_1.aspx")

    End Sub

    'ADD----------------------------------------------2017/12/14 TOS163
    Private Sub btnEchisaba_Click(sender As Object, e As EventArgs) Handles btnEchisaba.Click


        Dim con As New SqlConnection
        con.ConnectionString = ConfigurationManager.ConnectionStrings("renaissDBConnectionString").ConnectionString
        con.Open()

        '
        Dim strSql As String = ""
        strSql = strSql & " SELECT 開始日, 終了日"
        strSql = strSql & " FROM WebsiteReleasePeriod "
        strSql = strSql & " WHERE 店舗ＣＤ = @店舗ＣＤ"

        Dim sqlcom As SqlCommand = New SqlCommand(strSql, con)
        sqlcom.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.Char)).Value = 1

        Dim dt As New DataTable()
        Dim sda As New SqlDataAdapter(sqlcom)
        sda.Fill(dt)

        If dt.Rows.Count <> 0 Then

            Dim StrtTime As DateTime = dt.Rows(0).Item(0)
            Dim EndTime As DateTime
            If dt.Rows(0).Item(1).ToString = "" Then
                EndTime = "9999/01/31 22:59:59"
            Else
                EndTime = dt.Rows(0).Item(1)
            End If

            Dim day As DateTime = DateTime.UtcNow.AddHours(9)

            Dim hiduke As String = StrtTime.ToString("MM月dd日")
            Dim zikan As DateTime = StrtTime.AddSeconds(1)
            Dim szikan As String = zikan.ToString("tthh時")

            '開始日までは予約不可
            If StrtTime > day Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alertscript5", "window.alert('" & hiduke & "の" & szikan & "から予約ができるようになります。');", True)
                Exit Sub
            End If
            '終了日で予約一時終了
            If EndTime < day Then
                Response.Redirect("https://www.renaiss-news.com/")
                Exit Sub
            End If

        End If

        Response.Redirect("Registration_1.aspx")

    End Sub

End Class