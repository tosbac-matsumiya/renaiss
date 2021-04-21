Imports System.IO
Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
Imports System.Drawing

Public Class ThankyouForPayment
    Inherits System.Web.UI.Page
    Public kainCd As String
    Public systemDate As String
    Public Denno As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Me.lblName.Text = Session("name_kanji").ToString
            Me.lblTenpo.Text = Session("facility_name").ToString
            Me.lblSyubetu.Text = Session("plan_name").ToString
            Dim day As String = Session("date_visit").ToString
            'If Session("facility_id").ToString = "6" Then
            '    Dim time As String = Session("date_visit_time").ToString
            '    Me.lblRaiten.Text = Mid(day, 1, 4) + "年" + CStr(CInt(Mid(day, 6, 2))) + "月" + CStr(CInt(Mid(day, 9, 2))) + "日" + " " + Mid(time, 1, 2) + "時" + Mid(time, 4, 2) + "分"
            'Else
            Me.lblRaiten.Text = Mid(day, 1, 4) + "年" + CStr(CInt(Mid(day, 6, 2))) + "月" + CStr(CInt(Mid(day, 9, 2))) + "日"
            'End If
            Me.lblGoriyokakijo.Text = Session("facility_name").ToString + "店"

            Dim sum As Decimal = CDec(Session("CelSum").ToString)
            Me.lblUketukebi.Text = Today.ToString("yyyy年MM月dd日(ddd)")

            If Session("facility_id").ToString = "6" Then
                Me.pnlNishitenuketuke.Visible = False
            Else
                Me.pnlNishitenuketuke.Visible = False
            End If

            Dim con As New SqlConnection

            'DB接続
            con.ConnectionString = ConfigurationManager.ConnectionStrings("OnPremisesDBConnection").ConnectionString
            con.Open()

            '会員ＣＤ桁取得
            'A2017/10/22---START-------------------------------------------------------------------------------------------------------------
            '店舗の会員ＣＤ上3桁・システム日付取得
            Dim strSql2 As String = "SELECT M.会員ＣＤ上3桁, S.システム日付 FROM Ｍ店舗 M, Ｍシステム管理 S WHERE M.店舗ＣＤ = @店舗ＣＤ AND S.ＫＥＹ = @店舗ＣＤ"
            Dim sqlcom2 As SqlCommand = New SqlCommand(strSql2, con)
            sqlcom2.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.Char)).Value = Session("ori_facility_id")
            Dim dt2 As New DataTable()
            Dim sda2 As New SqlDataAdapter(sqlcom2)
            sda2.Fill(dt2)
            Dim head3Num As String = dt2.Rows(0)("会員ＣＤ上3桁")
            systemDate = dt2.Rows(0)("システム日付") 'ADD_2017/10/23 TOS163

            'Dim strSql3 As String = "DECLARE @MAX decimal(8) "
            'strSql3 = strSql3 & " SELECT @MAX = MAX(RIGHT(会員ＣＤ, 5)) FROM Ｍ会員_FITNESS WHERE LEFT(会員ＣＤ, 3) = @Head3Num" '会員ＣＤ列の左3桁が@Head3Num(下記のパラメータ設定の11行参照)に等しいものを選択し、その右5桁が最大のものを@MAXに代入
            'strSql3 = strSql3 & " Set @MAX ="
            'strSql3 = strSql3 & " Case When @MAX Is NULL Then '00001'"
            'strSql3 = strSql3 & " ELSE @MAX + 1 END"

            'DEL_satrt--------------------------------------------------------------------------------------------------------------------------2017/10/26 TOS163
            'Dim strSql3 As String = ""
            'strSql3 = strSql3 & " SELECT Case When MAX(RIGHT(会員ＣＤ, 5)) Is NULL Then '00001'" '会員ＣＤ列の左3桁が@Head3Num(下記のパラメータ設定の11行参照)に等しいものを選択し、その右5桁が最大のものを@MAXに代入
            'strSql3 = strSql3 & " ELSE  RIGHT('00000' + CONVERT(varchar,MAX(RIGHT(会員ＣＤ, 5)) + 1), 5) END as MAX"
            'strSql3 = strSql3 & " FROM [RNS].[dbo].Ｍ会員_FITNESS "
            'strSql3 = strSql3 & " WHERE LEFT(会員ＣＤ, 3) = @Head3Num"


            'Dim sqlcom3 As SqlCommand = New SqlCommand(strSql3, con2)

            'sqlcom3.Parameters.Add(New SqlParameter("Head3Num", SqlDbType.Int)).Value = head3Num

            'Dim dt3 As New DataTable()
            'Dim sda3 As New SqlDataAdapter(sqlcom3)
            'sda3.Fill(dt3)
            'kainCd = head3Num & dt3.Rows(0)("MAX")
            'DEL_end----------------------------------------------------------------------------------------------------------------------------2017/10/26 TOS163
            'A2017/10/22---END----------------------------------------------------------------------------------------------------------------------------------
            'ADD_satrt--------------------------------------------------------------------------------------------------------------------------2017/10/26 TOS163

            Dim con3 As New SqlConnection
            con3.ConnectionString = ConfigurationManager.ConnectionStrings("renaissDBConnectionString").ConnectionString
            con3.Open()

            '会員ＣＤ取得
            Dim strSql3 As String = ""
            strSql3 = strSql3 & " SELECT Case When MAX(RIGHT(会員ＣＤ, 5)) Is NULL Then '00001'" '会員ＣＤ列の左3桁が@Head3Num(下記のパラメータ設定の11行参照)に等しいものを選択し、その右5桁が最大のものを@MAXに代入
            strSql3 = strSql3 & " ELSE  RIGHT('00000' + CONVERT(varchar,MAX(RIGHT(会員ＣＤ, 5)) + 1), 5) END as MAX"
            strSql3 = strSql3 & " FROM LOG_会員ＣＤ "
            strSql3 = strSql3 & " WHERE LEFT(会員ＣＤ, 3) = @Head3Num"

            Dim sqlcom3 As SqlCommand = New SqlCommand(strSql3, con3)
            sqlcom3.Parameters.Add(New SqlParameter("Head3Num", SqlDbType.Char)).Value = head3Num

            Dim dt3 As New DataTable()
            Dim sda3 As New SqlDataAdapter(sqlcom3)
            sda3.Fill(dt3)
            kainCd = head3Num & dt3.Rows(0)("MAX")

            'LOG_会員ＣＤへ追加
            strSql3 = " INSERT INTO LOG_会員ＣＤ (店舗ＣＤ, 会員ＣＤ) VALUES (@店舗ＣＤ, @会員ＣＤ)"
            sqlcom3 = New SqlCommand(strSql3, con3)
            sqlcom3.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.Int)).Value = Session("ori_facility_id")
            sqlcom3.Parameters.Add(New SqlParameter("会員ＣＤ", SqlDbType.NVarChar)).Value = kainCd

            'SQLCommand実行 
            Dim result2 As String
            result2 = sqlcom3.ExecuteNonQuery()

            'ADD_end----------------------------------------------------------------------------------------------------------------------------2017/10/26 TOS163

            'D2017/10/22---START------------------------------------------------------------------------------------------
            ''ADD_start---2017/09/27 TOS163
            ''新しいテーブルの生成ストアドプロシージャ
            'Dim strSql As String = "DECLARE @MAX decimal(8)"
            'strSql = strSql & " SELECT @MAX = MAX(RIGHT(会員ＣＤ, 5)) FROM Ｍ会員_FITNESS WHERE LEFT(会員ＣＤ, 3) = @Head3Num" '会員ＣＤ列の左3桁が@Head3Num(下記のパラメータ設定の11行参照)に等しいものを選択し、その右5桁が最大のものを@MAXに代入
            'strSql = strSql & " SET @MAX ="
            'strSql = strSql & " CASE WHEN @MAX IS NULL THEN '00001'"
            'strSql = strSql & " ELSE @MAX + 1 END"
            'strSql = strSql & " INSERT INTO Ｍ会員_FITNESS (会員ＣＤ, 会員種別, 会員名, 会員カナ名, 誕生日, 性別, 郵便番号, 住所, 住所1, 住所2, 住所3, 住所4, 携帯番号, ＰＣＭＡＩＬ, 入会日, 画像, 会員区分, 職業, 職業ＣＤ, 店舗ＣＤ, 申込日, 基本入金額, 入会金, 登録区分, 支払区分, 審査パスＦ, 現金Ｆ, バス利用Ｆ, 利用区分, 申込担当者ＣＤ, 携帯番号１, 携帯番号２, 携帯番号３)"
            'strSql = strSql & " VALUES (CONVERT(varchar, @Head3Num) + RIGHT('00000' + CONVERT(varchar, @MAX), 5), @会員種別, @会員名, @会員カナ名, @誕生日, @性別, @郵便番号, @住所, @住所1, @住所2, @住所3, @住所4, @携帯番号, @ＰＣＭＡＩＬ, @入会日, @画像, @会員区分, @職業, @職業ＣＤ, @店舗ＣＤ, @申込日, @基本入金額, @入会金, @登録区分, @支払区分, @審査パスＦ, @現金Ｆ, @バス利用Ｆ, @利用区分, @申込担当者ＣＤ, @携帯番号１, @携帯番号２, @携帯番号３)" '@Head3Numと0埋めした@MAXをともに文字列化してから連結"
            ''ADD_end-----2017/09/27 TOS163
            'D2017/10/22---END-------------------------------------------------------------------------------------------

            'Dim strSql As String
            'Dim sqlcom As SqlCommand
            'Dim result As String


            '伝票番号の取得
            'A2017/10/22---START-------------------------------------------------------------------------------------------------------------
            'strSql3 = "DECLARE @MAXDENNO decimal(8)"
            'strSql = strSql & " SELECT @MAXDENNO = MAX(RIGHT(伝票ＮＯ, 7)) FROM Ｔ売上伝票 WHERE LEFT(伝票ＮＯ, 1) = 'W'" '伝票ＮＯ列の左1桁が'W'に等しいものを選択し、その右7桁が最大のものを@MAXに代入
            'strSql = strSql & " SET @MAXDENNO ="
            'strSql = strSql & " CASE WHEN @MAXDENNO IS NULL THEN '0000001'"
            'strSql = strSql & " ELSE @MAXDENNO + 1 END"

            'Dim strSql4 As String = ""
            'strSql4 = strSql4 & " SELECT CASE WHEN MAX(RIGHT(伝票ＮＯ, 7)) IS NULL  THEN '0000001'" '伝票ＮＯ列の左1桁が'W'に等しいものを選択し、その右7桁が最大のものを@MAXに代入
            'strSql4 = strSql4 & " ELSE RIGHT('0000000' + CONVERT(varchar,MAX(RIGHT(伝票ＮＯ, 7)) + 1), 7) END AS MAXDENNO"
            ''strSql4 = strSql4 & " FROM Ｔ売上伝票"
            'strSql4 = strSql4 & " FROM LOG_伝票ＮＯ"
            'strSql4 = strSql4 & " WHERE LEFT(伝票ＮＯ, 1) = 'W'"

            'Dim sqlcom4 As SqlCommand = New SqlCommand(strSql4, con3)
            'Dim dt4 As New DataTable()
            'Dim sda4 As New SqlDataAdapter(sqlcom4)
            'sda4.Fill(dt4)

            'Denno = "W" & dt4.Rows(0)("MAXDENNO")


            ''LOG_会員ＣＤへ追加
            'strSql4 = " INSERT INTO LOG_伝票ＮＯ (伝票ＮＯ) VALUES (@伝票ＮＯ)"
            'sqlcom4 = New SqlCommand(strSql4, con3)
            'sqlcom4.Parameters.Add(New SqlParameter("伝票ＮＯ", SqlDbType.NVarChar)).Value = Denno

            ''SQLCommand実行 
            'Dim result4 As String
            'result4 = sqlcom4.ExecuteNonQuery()


            'con3.Close()
            'A2017/10/22---END----------------------------------------------------------------------------------------------------------------------------------


            ''===Ｔ売上伝票のINSERT文==========================================================================================================
            ''D2017/10/22---START---------------------------------------------------------------------------------------------------------------
            ''strSql = "DECLARE @MAX decimal(8)"
            ''strSql = strSql & " SELECT @MAX = MAX(RIGHT(伝票ＮＯ, 7)) FROM Ｔ売上伝票 WHERE LEFT(伝票ＮＯ, 1) = 'W'" '伝票ＮＯ列の左1桁が'W'に等しいものを選択し、その右7桁が最大のものを@MAXに代入
            ''strSql = strSql & " SET @MAX ="
            ''strSql = strSql & " CASE WHEN @MAX IS NULL THEN '0000001'"
            ''strSql = strSql & " ELSE @MAX + 1 END"
            ''strSql = strSql & " INSERT INTO Ｔ売上伝票 (伝票ＮＯ, 伝票日付, 合計金額, 合計税抜金額, 合計消費税額, 登録日, 変更日, 印刷日, 店舗ＣＤ, 登録区分, 支払区分) VALUES ('W' + RIGHT('0000000' + CONVERT(varchar, @MAX), 7), @伝票日付, @合計金額, @合計税抜金額, @合計消費税額, @登録日, @変更日, @印刷日, @店舗ＣＤ, @登録区分, @支払区分)" '"W"と0埋めした@MAXをともに文字列化してから連結
            ''D2017/10/22---END-----------------------------------------------------------------------------------------------------------------


            ''===Ｔ売上明細のINSERT文==========================================================================================================
            ''D2017/10/22---START------------------------------------------------------------------------------------------------------------
            ''strSql = "DECLARE @MAX decimal(8)"
            ''strSql = strSql & " SELECT @MAX = MAX(RIGHT(伝票ＮＯ, 7)) FROM Ｔ売上伝票 WHERE LEFT(伝票ＮＯ, 1) = 'W'" '伝票ＮＯ列の左1桁が'W'に等しいものを選択し、その右7桁が最大のものを@MAXに代入
            ''strSql = strSql & " SET @MAX = @MAX"
            ''strSql = strSql & " INSERT INTO Ｔ売上明細 (伝票ＮＯ, 行ＮＯ, 商品ＣＤ, 商品名, 数量, 仕入単価, 金額, 消費税額, 粗利額, 付け込み区分, 付け込みＦ, 処理区分, 会員区分, 店舗ＣＤ) VALUES ('W' + RIGHT('0000000' + CONVERT(varchar, @MAX), 7), @行ＮＯ, @商品ＣＤ, @商品名,1, 0, @金額, @消費税額, 0, 0, 0, 1, 1, @店舗ＣＤ)" '"W"と0埋めした@MAXをともに文字列化してから連結
            ''D2017/10/22---END--------------------------------------------------------------------------------------------------------------


            ''===Ｔ売上明細詳細のINSERT文==========================================================================================================
            ''D2017/10/22---START---------------------------------------------------------------------------------------------------------------------------
            ''strSql = "Declare @dMAX Decimal(8), @kMAX Decimal(8)"
            ''strSql = strSql & " Select @dMAX = MAX(RIGHT(伝票ＮＯ, 7)) FROM Ｔ売上伝票 WHERE LEFT(伝票ＮＯ, 1) = 'W'" '伝票ＮＯ列の左1桁が'W'に等しいものを選択し、その右7桁が最大のものを@dMAXに代入
            ''strSql = strSql & " SET @dMAX = @dMAX"
            ''strSql = strSql & " SELECT @kMAX = MAX(RIGHT(会員ＣＤ, 5)) FROM Ｍ会員_FITNESS WHERE LEFT(会員ＣＤ, 3) = @Head3Num" '会員ＣＤ列の左3桁が@Head3Num(下記のパラメータ設定の11行参照)に等しいものを選択し、その右5桁が最大のものを@kMAXに代入
            ''strSql = strSql & " SET @kMAX = @kMAX"
            ''strSql = strSql & " INSERT INTO Ｔ売上明細詳細 (伝票ＮＯ, 行ＮＯ, 枝番, 商品ＣＤ, 商品名, 数量, 仕入単価, 金額, 消費税額, 粗利額, 付け込み区分, 付け込みＦ, 処理区分, 会員区分, 店舗ＣＤ, 入金会員ＣＤ, 入金年月, 入金日) VALUES ('W' + RIGHT('0000000' + CONVERT(varchar, @dMAX), 7), @行ＮＯ, 1, @商品ＣＤ, @商品名, 1, 0, @金額, @消費税額, 0, 0, 0, 1, 1, @店舗ＣＤ, CONVERT(varchar, @Head3Num) + RIGHT('00000' + CONVERT(varchar, @kMAX), 5), @入金年月, @入金日)" '"W"と0埋めした@MAXをともに文字列化してから連結
            ''D2017/10/22---END-----------------------------------------------------------------------------------------------------------------------------


            ''D2017/10/22---START------------------------------------------
            ''strSql = "DECLARE @dMAX decimal(8), @kMAX decimal(8)"
            ''strSql = strSql & " SELECT @dMAX = MAX(RIGHT(伝票ＮＯ, 7)) FROM Ｔ売上伝票 WHERE LEFT(伝票ＮＯ, 1) = 'W'" '伝票ＮＯ列の左1桁が'W'に等しいものを選択し、その右7桁が最大のものを@dMAXに代入
            ''strSql = strSql & " SET @dMAX = @dMAX"
            ''strSql = strSql & " SELECT @kMAX = MAX(RIGHT(会員ＣＤ, 5)) FROM Ｍ会員_FITNESS WHERE LEFT(会員ＣＤ, 3) = @Head3Num" '会員ＣＤ列の左3桁が@Head3Num(下記のパラメータ設定の11行参照)に等しいものを選択し、その右5桁が最大のものを@kMAXに代入
            ''strSql = strSql & " SET @kMAX = @kMAX"
            ''strSql = strSql & " INSERT INTO Ｔ売上明細詳細 (伝票ＮＯ, 行ＮＯ, 枝番, 商品ＣＤ, 商品名, 数量, 仕入単価, 金額, 消費税額, 粗利額, 付け込み区分, 付け込みＦ, 処理区分, 会員区分, 店舗ＣＤ, 入金会員ＣＤ, 入金年月, 入金日) VALUES ('W' + RIGHT('0000000' + CONVERT(varchar, @dMAX), 7), @行ＮＯ, 1, @商品ＣＤ, @商品名, 1, 0, @金額, @消費税額, 0, 0, 0, 1, 1, @店舗ＣＤ, CONVERT(varchar, @Head3Num) + RIGHT('00000' + CONVERT(varchar, @kMAX), 5), @入金年月, @入金日)" '"W"と0埋めした@MAXをともに文字列化してから連結
            ''D2017/10/22---END--------------------------------------------




            ''===Ｔ入金情報のINSERT文==========================================================================================================
            ''strSql = "DECLARE @dMAX decimal(8), @kMAX decimal(8)"
            ''strSql = strSql & " SELECT @dMAX = MAX(RIGHT(伝票ＮＯ, 7)) FROM Ｔ売上伝票 WHERE LEFT(伝票ＮＯ, 1) = 'W'"
            ''strSql = strSql & " SET @dMAX = @dMAX"
            ''strSql = strSql & " SELECT @kMAX = MAX(RIGHT(会員ＣＤ, 5)) FROM Ｍ会員_FITNESS WHERE LEFT(会員ＣＤ, 3) = @Head3Num"
            ''strSql = strSql & " SET @kMAX = @kMAX"
            ''strSql = strSql & " INSERT INTO Ｔ入金情報 (会員CD, 年月, 基本振替額, 割引合計, 振替額, 入金額, 振替データ作成済Ｆ, レシートＮＯ, 自動作成Ｆ, 入金日, 登録区分) VALUES (CONVERT(varchar, @Head3Num) + RIGHT('00000' + CONVERT(varchar, @kMAX), 5), @年月, @基本振替額, 0, 0, @入金額, 0, 'W' + RIGHT('0000000' + CONVERT(varchar, @dMAX), 7), 0, @入金日, 2)"

            ''D2017/10/22---START----------------------------------------------------------------------------------------------------
            ''strSql = "DECLARE @dMAX decimal(8), @kMAX decimal(8)"
            ''strSql = strSql & " SELECT @dMAX = MAX(RIGHT(伝票ＮＯ, 7)) FROM Ｔ売上伝票 WHERE LEFT(伝票ＮＯ, 1) = 'W'"
            ''strSql = strSql & " SET @dMAX = @dMAX"
            ''strSql = strSql & " SELECT @kMAX = MAX(RIGHT(会員ＣＤ, 5)) FROM Ｍ会員_FITNESS WHERE LEFT(会員ＣＤ, 3) = @Head3Num"
            ''strSql = strSql & " SET @kMAX = @kMAX"
            ''strSql = strSql & " INSERT INTO Ｔ入金情報 (会員CD, 年月, 基本振替額, 割引合計, 振替額, 入金額, 振替データ作成済Ｆ, レシートＮＯ, 自動作成Ｆ, 入金日, 登録区分) VALUES (CONVERT(varchar, @Head3Num) + RIGHT('00000' + CONVERT(varchar, @kMAX), 5), @年月, @基本振替額, 0, 0, @入金額, 0, 'W' + RIGHT('0000000' + CONVERT(varchar, @dMAX), 7), 0, @入金日, 2)"
            ''D2017/10/22---END------------------------------------------------------------------------------------------------------

            ''A2017/10/22---START----------------------------------------------------------------------------------------------------

            ''会員ＣＤ桁取得
            ''D2017/10/22-----START---------------------------------------------
            ''Dim strSql3 As String = "SELECT * FROM M会員_FITNESS WHERE 会員名 = @会員名 AND 誕生日 = @誕生日 AND 性別 = @性別"
            ''strSql3 = strSql3 & " AND 携帯番号 = @携帯番号 AND ＰＣＭＡＩＬ = @ＰＣＭＡＩＬ AND 入会日 = @入会日"
            ''strSql3 = strSql3 & " AND 申込日 = @申込日 AND 登録区分 = @登録区分 AND 支払区分 = @支払区分 AND 店舗ＣＤ = @店舗ＣＤ"
            ''sqlcom = New SqlCommand(strSql3, con2)

            ''sqlcom3.Parameters.Add(New SqlParameter("会員名", SqlDbType.NVarChar)).Value = Session("name_kanji")
            ''sqlcom3.Parameters.Add(New SqlParameter("誕生日", SqlDbType.Date)).Value = Session("birthday")
            ''If Session("sex").ToString = "男性" Then
            ''    sqlcom3.Parameters.Add(New SqlParameter("性別", SqlDbType.NVarChar)).Value = "男"
            ''Else
            ''    sqlcom3.Parameters.Add(New SqlParameter("性別", SqlDbType.NVarChar)).Value = "女"
            ''End If
            ''sqlcom3.Parameters.Add(New SqlParameter("携帯番号", SqlDbType.VarChar)).Value = Session("phonenumber")
            ''sqlcom3.Parameters.Add(New SqlParameter("ＰＣＭＡＩＬ", SqlDbType.VarChar)).Value = Session("email")
            ''If Session("facility_id").ToString = "6" Then
            ''    sqlcom3.Parameters.Add(New SqlParameter("入会日", SqlDbType.DateTime)).Value = Session("date_visit") & " " & Session("date_visit_time")
            ''Else
            ''    sqlcom3.Parameters.Add(New SqlParameter("入会日", SqlDbType.DateTime)).Value = Session("date_visit")
            ''End If
            ''sqlcom3.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.Int)).Value = Session("ori_facility_id") '本来の店舗ＣＤの方
            ''sqlcom3.Parameters.Add(New SqlParameter("申込日", SqlDbType.Char)).Value = Today.ToString("yyyyMMdd")
            ''sqlcom3.Parameters.Add(New SqlParameter("登録区分", SqlDbType.NVarChar)).Value = 2 'Web登録=2
            ''sqlcom3.Parameters.Add(New SqlParameter("支払区分", SqlDbType.Int)).Value = Session("payment")

            ''Dim dt3 As New DataTable()
            ''Dim sda3 As New SqlDataAdapter(sqlcom3)
            ''sda3.Fill(dt3)
            ''Dim kainCd As String = dt3.Rows(0)("会員ＣＤ")
            ''D2017/10/22---END------------------------------------------------

            Session("membercd") = kainCd


            Me.lblMemberCd.Text = Session("membercd")

            con.Close()


            'If Session("facility_id").ToString = "6" Then
            'Send_JIS_Mail()         'A2017/09/11
            'Else
            Send_JIS_Mail2()         'A2017/10/20
            'End If


            MasterInsert()  'AzureのⅯ会員_FitnessへINSERT ADD_2017/10/25 TOS163


            'DEL_start-------------------------------------------------------------------------2018/03/05 TOS163
            'If Session("facility_id").ToString = "6" Then
            '    ZaikoUpdate() 'Azureの在庫UPDATE ADD_2017/10/13 TOS163 'テスト時はコメントアウト

            '    YoyakuInsert()  'Azureの予約INSERT ADD_2017/10/23 TOS163

            'End If
            'DEL_end---------------------------------------------------------------------------2018/03/05 TOS163

            'Session("name_kanji") = "" '会員名を空白 'ADD_2017/09/27 TOS163

        Catch ex As Exception
            Response.Redirect("Thankyoupage.aspx")

        End Try


    End Sub

#Region "myEncode"
    Private Function myEncode(ByVal str As String, ByVal enc As System.Text.Encoding) As String
        Dim base64str As String = Convert.ToBase64String(enc.GetBytes(str))
        Return String.Format("=?{0}?B?{1}?=", enc.BodyName, base64str)
    End Function
#End Region

    Private Sub Send_JIS_Mail()
        Dim smtp As New SmtpClient()
        Dim msg As New MailMessage()
        Dim myEnc As Encoding = Encoding.GetEncoding("iso-2022-jp")


        'アドレス：   info-reaiss@elle-rose.co.jp
        'ユーザID：  renais100
        'パスワード：  ryJ4M7d5

        Dim UketukeDay As String = Today.ToString("yyyy年MM月dd日")
        Dim day As String = Session("date_visit").ToString
        'Dim time As String = Session("date_visit_time").ToString 'DEL_2018/03/05 TOS163
        Dim DATE_VISIT As String
        'Dim TIME_VISIT As String
        DATE_VISIT = Mid(day, 1, 4) & "年" & CStr(CInt(Mid(day, 6, 2))) & "月" & CStr(CInt(Mid(day, 9, 2))) & "日"
        'TIME_VISIT = Mid(time, 1, 2) & "時" & Mid(time, 4, 2) & "分"'DEL_2018/03/05 TOS163


        ' 送信元
        msg.From = New System.Net.Mail.MailAddress(
                        "info-reaiss@elle-rose.co.jp", myEncode("ルネッス", myEnc))
        ' 送信先
        msg.[To].Add(New System.Net.Mail.MailAddress(
                        Session("email").ToString, myEncode("", myEnc)))
        ' 件名
        msg.Subject = myEncode(myEncode("【スポーツクラブルネッス福井西店】ご入会ありがとうございます。", myEnc), myEnc)

        ' 本文
        Dim sBody As String =
            Chr(13) & "" & Chr(10) &
            Session("name_kanji").ToString & "様" & Chr(13) & "" & Chr(10) &
            Chr(13) & "" & Chr(10) &
            "初めまして！" & Chr(13) & "" & Chr(10) &
            "スポーツクラブルネッス福井西店" & Chr(13) & "" & Chr(10) &
            "店長の中島です！" & Chr(13) & "" & Chr(10) &
            "この度は、WEB入会いただき、" & Chr(13) & "" & Chr(10) &
            "誠にありがとうございます。" & Chr(13) & "" & Chr(10) &
            "ご入会を受付しました。" & Chr(13) & "" & Chr(10) &
            Chr(13) & "" & Chr(10) &
            Session("name_kanji").ToString & "様のWEB受付番号などはこちらです。" & Chr(13) & "" & Chr(10) &
            "・WEB受付番号：" & Session("membercd").ToString & Chr(13) & "" & Chr(10) &
            "・WEB受付日時：" & UketukeDay & Chr(13) & "" & Chr(10) &
            Chr(13) & "" & Chr(10) &
            Chr(13) & "" & Chr(10) &
            "――――――――――――――――――" & Chr(13) & "" & Chr(10) &
            "◆今後の手続きについて" & Chr(13) & "" & Chr(10) &
            "――――――――――――――――――" & Chr(13) & "" & Chr(10) &
            "最終のお手続きを行いますので、" & Chr(13) & "" & Chr(10) &
            "『" & DATE_VISIT.ToString & "に" & Chr(13) & "" & Chr(10) &
            "ルネッス福井西店にお越し願います。" & Chr(13) & "" & Chr(10) &
            Chr(13) & "" & Chr(10) &
            "※最終のお手続きでは、" & Chr(13) & "" & Chr(10) &
            "月会費口座振替のお手続きや、" & Chr(13) & "" & Chr(10) &
            "必須事項のご説明などを行います。" & Chr(13) & "" & Chr(10) &
            "（約15分）" & Chr(13) & "" & Chr(10) &
            Chr(13) & "" & Chr(10) &
            "※WEBで顔写真を登録されなかった場合は、ご利用説明会時に会員証の写真撮影を行わせていただきます。" & Chr(13) & "" & Chr(10) &
            "※選択した日時を変更したい場合は、お電話にて変更をお願い致します。" & Chr(13) & "" & Chr(10) &
            "※日時をご指定いただいても混雑状況により、お待ちいただく場合がございます。予めご了承願います。" & Chr(13) & "" & Chr(10) &
            Chr(13) & "" & Chr(10) &
            Chr(13) & "" & Chr(10) &
            "――――――――――――――――――――――" & Chr(13) & "" & Chr(10) &
            "◆初回来店日(最終お手続き日)のお持ち物" & Chr(13) & "" & Chr(10) &
            "――――――――――――――――――――――" & Chr(13) & "" & Chr(10) &
            "［１］受付内容を確認できるもの（以下のいずれか一つ）" & Chr(13) & "" & Chr(10) &
            "・受付番号" & Chr(13) & "" & Chr(10) &
            "・お手続き完了画面のプリントアウトまたはスクリーンショット" & Chr(13) & "" & Chr(10) &
            "・この確認メール" & Chr(13) & "" & Chr(10) &
            "［２］本人確認書類" & Chr(13) & "" & Chr(10) &
            "免許証や保険証など、氏名、住所を確認できるもの" & Chr(13) & "" & Chr(10) &
            "［３］通帳(またはキャッシュカード)、届出印" & Chr(13) & "" & Chr(10) &
            "月会費の口座振替を行う金融機関の通帳(またはキャッシュカード)と届出印" & Chr(13) & "" & Chr(10) &
            Chr(13) & "" & Chr(10) &
            Chr(13) & "" & Chr(10) &
            "◆注意事項" & Chr(13) & "" & Chr(10) &
            "※18歳未満の方のご入会には親権者の同意が必要です。" & Chr(13) & "" & Chr(10) &
            "※入会資格の条項により、入会をお断りさせていただく場合がございます。" & Chr(13) & "" & Chr(10) &
            Chr(13) & "" & Chr(10) &
            Chr(13) & "" & Chr(10) &
            Session("name_kanji").ToString & "様のご来店を" & Chr(13) & "" & Chr(10) &
            "お待ちしております。" & Chr(13) & "" & Chr(10) &
            Chr(13) & "" & Chr(10) &
            Chr(13) & "" & Chr(10) &
            "――――――――――――――――――――――" & Chr(13) & "" & Chr(10) &
            "◆ご質問、不明な点がございましたら、" & Chr(13) & "" & Chr(10) &
            "下記までご連絡ください。" & Chr(13) & "" & Chr(10) &
            Chr(13) & "" & Chr(10) &
            "スポーツクラブルネッス福井西　TEL：0776-43-1844" & Chr(13) & "" & Chr(10) &
            "――――――――――――――――――――――"

        Dim altView As AlternateView =
          AlternateView.CreateAlternateViewFromString(
            sBody, myEnc, Mime.MediaTypeNames.Text.Plain)
        altView.TransferEncoding =
          Mime.TransferEncoding.SevenBit
        msg.AlternateViews.Add(altView)

        smtp.Host = "smtp.elle-rose.co.jp" ' SMTPサーバ
        smtp.Port = "587"
        'smtp.Credentials = New NetworkCredential(“renais100”, “ryJ4M7d5”) 'サーバーへのユーザ名とパスワード'DEL---2020/11/17
        smtp.Credentials = New NetworkCredential(“renais100”, “RvHkW1R18bd9u8zZKsMk”) 'サーバーへのユーザ名とパスワード

        'smtp.Host = "www.hokutos.co.jp" ' SMTPサーバ
        'smtp.Port = "587"
        'smtp.Credentials = New NetworkCredential(“hokutos10”, “HRtos61”) 'サーバーへのユーザ名とパスワード

        smtp.Send(msg) ' メッセージを送信
    End Sub

    Private Sub Send_JIS_Mail2()
        Dim smtp As New SmtpClient()
        Dim msg As New MailMessage()
        Dim myEnc As Encoding = Encoding.GetEncoding("iso-2022-jp")


        'アドレス：   info-reaiss@elle-rose.co.jp
        'ユーザID：  renais100
        'パスワード：  ryJ4M7d5

        Dim DATE_VISIT As String
        Dim TIME_VISIT As String
        Dim UketukeDay As String = Today.ToString("yyyy年MM月dd日")
        Dim day As String = Session("date_visit").ToString
        'If Session("facility_id").ToString = "6" Then
        '    Dim time As String = Session("date_visit_time").ToString
        '    TIME_VISIT = Mid(time, 1, 2) & "時" & Mid(time, 4, 2) & "分"
        'Else
        TIME_VISIT = ""
        'End If
        DATE_VISIT = Mid(day, 1, 4) & "年" & CStr(CInt(Mid(day, 6, 2))) & "月" & CStr(CInt(Mid(day, 9, 2))) & "日"

        ' 送信元
        msg.From = New System.Net.Mail.MailAddress(
                        "info-renaiss@elle-rose.co.jp", myEncode("ルネッス", myEnc))
        ' 送信先
        msg.[To].Add(New System.Net.Mail.MailAddress(
                        Session("email").ToString, myEncode(myEncode("", myEnc), myEnc)))
        ' 件名
        msg.Subject = myEncode(myEncode("【スポーツクラブルネッス】ご入会ありがとうございます。", myEnc), myEnc)

        ' 本文
        Dim sBody As String =
          Chr(13) & "" & Chr(10) &
          Session("name_kanji").ToString & "様" & Chr(13) & "" & Chr(10) &
          Chr(13) & "" & Chr(10) &
          "この度は、WEB入会いただき、" & Chr(13) & "" & Chr(10) &
          "誠にありがとうございます。" & Chr(13) & "" & Chr(10) &
          "ご入会を受付しました。" & Chr(13) & "" & Chr(10) &
          Chr(13) & "" & Chr(10) &
          Session("name_kanji").ToString & "様のWEB受付番号などはこちらです。" & Chr(13) & "" & Chr(10) &
          "・WEB受付番号：" & Session("membercd").ToString & Chr(13) & "" & Chr(10) &
          "・WEB受付日時：" & UketukeDay & Chr(13) & "" & Chr(10) &
          Chr(13) & "" & Chr(10) &
          Chr(13) & "" & Chr(10) &
          "――――――――――――――――――" & Chr(13) & "" & Chr(10) &
          "◆今後の手続きについて" & Chr(13) & "" & Chr(10) &
          "――――――――――――――――――" & Chr(13) & "" & Chr(10) &
          DATE_VISIT.ToString & TIME_VISIT.ToString & "に" & Chr(13) & "" & Chr(10) &
          "お申込み店舗にお越し願います。" & Chr(13) & "" & Chr(10) &
          Chr(13) & "" & Chr(10) &
          "※初回ご来店時に、" & Chr(13) & "" & Chr(10) &
          "館内案内、" & Chr(13) & "" & Chr(10) &
          "月会費口座振替のためのお手続きを行います。" & Chr(13) & "" & Chr(10) &
          "（約30分）" & Chr(13) & "" & Chr(10) &
          Chr(13) & "" & Chr(10) &
          "※WEBで顔写真を登録されなかった場合は、初回ご来店時に会員証の写真撮影を行わせていただきます。" & Chr(13) & "" & Chr(10) &
          "※選択した初回ご来所予定日を変更したい場合は、お電話にて変更をお願い致します。" & Chr(13) & "" & Chr(10) &
          "※日時をご指定いただいても混雑状況により、お待ちいただく場合がございます。予めご了承願います。" & Chr(13) & "" & Chr(10) &
          Chr(13) & "" & Chr(10) &
          Chr(13) & "" & Chr(10) &
          "――――――――――――――――――――――" & Chr(13) & "" & Chr(10) &
          "◆ご利用説明会のお持ち物" & Chr(13) & "" & Chr(10) &
          "――――――――――――――――――――――" & Chr(13) & "" & Chr(10) &
          "［１］受付内容を確認できるもの（以下のいずれか一つ）" & Chr(13) & "" & Chr(10) &
          "・受付番号" & Chr(13) & "" & Chr(10) &
          "・お手続き完了画面のプリントアウトまたはスクリーンショット" & Chr(13) & "" & Chr(10) &
          "・この確認メール" & Chr(13) & "" & Chr(10) &
          "［２］本人確認書類" & Chr(13) & "" & Chr(10) &
          "免許証や保険証など、氏名、住所を確認できるもの" & Chr(13) & "" & Chr(10) &
          "［３］通帳(またはキャッシュカード)、届出印" & Chr(13) & "" & Chr(10) &
          "月会費の口座振替を行う金融機関の通帳(またはキャッシュカード)と届出印" & Chr(13) & "" & Chr(10) &
          Chr(13) & "" & Chr(10) &
          Chr(13) & "" & Chr(10) &
          "◆注意事項" & Chr(13) & "" & Chr(10) &
          "※18歳未満の方のご入会には親権者の同意が必要です。" & Chr(13) & "" & Chr(10) &
          "※入会資格の条項により、入会をお断りさせていただく場合がございます。" & Chr(13) & "" & Chr(10) &
          Chr(13) & "" & Chr(10) &
          Chr(13) & "" & Chr(10) &
          Session("name_kanji").ToString & "様のご来店を" & Chr(13) & "" & Chr(10) &
          "お待ちしております。" & Chr(13) & "" & Chr(10) &
          Chr(13) & "" & Chr(10) &
          Chr(13) & "" & Chr(10) &
          "―――――――――――――――――――――" & Chr(13) & "" & Chr(10) &
          "【お問い合わせ先】" & Chr(13) & "" & Chr(10) &
          "スポーツクラブ　ルネッス 越前" & Chr(13) & "" & Chr(10) &
          "0778-21-0001" & Chr(13) & "" & Chr(10) &
          "スポーツクラブ　ルネッス 鯖江" & Chr(13) & "" & Chr(10) &
          "0778-51-6333" & Chr(13) & "" & Chr(10) &
          "スポーツクラブ　ルネッス 敦賀" & Chr(13) & "" & Chr(10) &
          "0770-24-1117" & Chr(13) & "" & Chr(10) &
          "スポーツクラブ　ルネッス 福井南" & Chr(13) & "" & Chr(10) &
          "0776-63-5720" & Chr(13) & "" & Chr(10) &
          "スポーツクラブ　ルネッス 福井西" & Chr(13) & "" & Chr(10) &
          "0776-43-1844" & Chr(13) & "" & Chr(10) &
          "―――――――――――――――――――――"

        Dim altView As AlternateView =
          AlternateView.CreateAlternateViewFromString(
            sBody, myEnc, Mime.MediaTypeNames.Text.Plain)
        altView.TransferEncoding =
          Mime.TransferEncoding.SevenBit
        msg.AlternateViews.Add(altView)

        smtp.Host = "smtp.elle-rose.co.jp" ' SMTPサーバ
        smtp.Port = "587"
        'smtp.Credentials = New NetworkCredential(“renais100”, “ryJ4M7d5”) 'サーバーへのユーザ名とパスワード'DEL---2020/11/17
        smtp.Credentials = New NetworkCredential(“renais100”, “RvHkW1R18bd9u8zZKsMk”) 'サーバーへのユーザ名とパスワード

        'smtp.Host = "smtp.elle-rose.co.jp" ' SMTPサーバ
        'smtp.Port = "587"
        'smtp.Credentials = New NetworkCredential(“renais100”, “ryJ4M7d5”) 'サーバーへのユーザ名とパスワード

        'smtp.Host = "www.hokutos.co.jp" ' SMTPサーバ
        'smtp.Port = "587"
        'smtp.Credentials = New NetworkCredential(“hokutos10”, “HRtos61”) 'サーバーへのユーザ名とパスワード

        smtp.Send(msg) ' メッセージを送信
    End Sub

    Private Sub ZaikoUpdate() 'ADD_2017/10/13 TOS163

        Dim con As New SqlConnection

        'DB接続
        con.ConnectionString = ConfigurationManager.ConnectionStrings("renaissDBConnectionString").ConnectionString
        con.Open()

        '===MonthlyStock_NewStoreのUPDATE文==========================================================================================================
        '新しいテーブルの生成ストアドプロシージャ
        Dim strSql As String = " UPDATE MonthlyStock_NewStore SET stockOri = stockOri - 1 WHERE timeStart = @timeStart"

        'SQLCommand設定
        Dim sqlcom As SqlCommand = New SqlCommand(strSql, con)

        'パラメータ設定
        sqlcom.Parameters.Add(New SqlParameter("timeStart", SqlDbType.DateTime)).Value = Session("date_visit") & " " & Session("date_visit_time")


        'SQLCommand実行
        Dim result As String
        result = sqlcom.ExecuteNonQuery()


    End Sub

    Private Sub YoyakuInsert() 'ADD_2017/10/23 TOS163

        Dim con As New SqlConnection

        'DB接続
        con.ConnectionString = ConfigurationManager.ConnectionStrings("renaissDBConnectionString").ConnectionString
        con.Open()

        '===MonthlyStock_NewStoreのUPDATE文==========================================================================================================
        '新しいテーブルの生成ストアドプロシージャ
        Dim strSql As String = " INSERT INTO ReservationTable ( CustomerID, CustomerName, Date, GymProgramID, TimeStart, TimeEnd, ProgramName, RequiredTime, StoreID, MenuLevel, Attendance )"
        strSql = strSql & " VALUES (@CustomerID, @CustomerName, @Date, @GymProgramID, @TimeStart, @TimeEnd, @ProgramName, @RequiredTime, @StoreID, @MenuLevel, @Attendance)"


        'SQLCommand設定
        Dim sqlcom As SqlCommand = New SqlCommand(strSql, con)

        'パラメータ設定
        sqlcom.Parameters.Add(New SqlParameter("CustomerID", SqlDbType.VarChar)).Value = Session("membercd")
        sqlcom.Parameters.Add(New SqlParameter("CustomerName", SqlDbType.NVarChar)).Value = Session("name_kanji")
        sqlcom.Parameters.Add(New SqlParameter("Date", SqlDbType.DateTime)).Value = Now
        sqlcom.Parameters.Add(New SqlParameter("GymProgramID", SqlDbType.Int)).Value = 10
        Dim daytime As DateTime = Session("date_visit") & " " & Session("date_visit_time")
        sqlcom.Parameters.Add(New SqlParameter("timeStart", SqlDbType.DateTime)).Value = Session("date_visit") & " " & Session("date_visit_time")
        sqlcom.Parameters.Add(New SqlParameter("TimeEnd", SqlDbType.DateTime)).Value = daytime.AddMinutes(30)
        sqlcom.Parameters.Add(New SqlParameter("ProgramName", SqlDbType.NVarChar)).Value = "初回オリエンテーション(新店舗オープニング用)"
        sqlcom.Parameters.Add(New SqlParameter("RequiredTime", SqlDbType.Int)).Value = 30
        sqlcom.Parameters.Add(New SqlParameter("StoreID", SqlDbType.Int)).Value = Session("ori_facility_id")
        sqlcom.Parameters.Add(New SqlParameter("MenuLevel", SqlDbType.Int)).Value = 1
        sqlcom.Parameters.Add(New SqlParameter("Attendance", SqlDbType.Bit)).Value = 0


        'SQLCommand実行
        Dim result As String
        result = sqlcom.ExecuteNonQuery()


    End Sub

    Private Sub MasterInsert() 'ADD_2017/10/25 TOS163

        Dim con As New SqlConnection
        Dim strSql As String
        'If Session("ori_facility_id") = 6 Then
        con.ConnectionString = ConfigurationManager.ConnectionStrings("renaissDBConnectionString").ConnectionString
        con.Open()

        'ADD_start---2017/10/22 TOS126
        '新しいテーブルの生成ストアドプロシージャ
        strSql = "INSERT INTO WK_Ｍ会員_FITNESS (会員ＣＤ, 会員種別, 会員名, 会員カナ名, 誕生日, 性別, 郵便番号, 住所, 住所1, 住所2, 住所3, 住所4, 携帯番号, ＰＣＭＡＩＬ, 入会日, 画像, 会員区分, 職業, 職業ＣＤ, 店舗ＣＤ, 申込日, 基本入金額, 入会金, 登録区分, 支払区分, 審査パスＦ, 現金Ｆ, バス利用Ｆ, 利用区分, 申込担当者ＣＤ, 携帯番号１, 携帯番号２, 携帯番号３, 登録日, 合計金額, 合計税抜金額, 合計消費税額, 商品ＣＤ, 商品名, 入金年月1, 入金年月2, 年月1金額, 年月2金額, 入金年月3, 年月3金額, 初回来店日予定)"
        strSql = strSql & " VALUES ( @会員ＣＤ, @会員種別, @会員名, @会員カナ名, @誕生日, @性別, @郵便番号, @住所, @住所1, @住所2, @住所3, @住所4, @携帯番号, @ＰＣＭＡＩＬ, @入会日, @画像, @会員区分, @職業, @職業ＣＤ, @店舗ＣＤ, @申込日, @基本入金額, @入会金, @登録区分, @支払区分, @審査パスＦ, @現金Ｆ, @バス利用Ｆ, @利用区分, @申込担当者ＣＤ, @携帯番号１, @携帯番号２, @携帯番号３, @登録日, @合計金額, @合計税抜金額, @合計消費税額, @商品ＣＤ, @商品名, @入金年月1, @入金年月2, @年月1金額, @年月2金額, @入金年月3, @年月3金額, @初回来店日予定)" '@Head3Numと0埋めした@MAXをともに文字列化してから連結"
        'ADD_end-----2017/10/22 TOS126

        'Else
        '    'con.ConnectionString = ConfigurationManager.ConnectionStrings("OnPremisesDBConnection").ConnectionString
        '    'con.Open()

        '    ''ADD_start---2017/10/22 TOS126
        '    ''新しいテーブルの生成ストアドプロシージャ
        '    'strSql = "INSERT INTO Ｍ会員_FITNESS (会員ＣＤ, 会員種別, 会員名, 会員カナ名, 誕生日, 性別, 郵便番号, 住所, 住所1, 住所2, 住所3, 住所4, 携帯番号, ＰＣＭＡＩＬ, 入会日, 画像, 会員区分, 職業, 職業ＣＤ, 店舗ＣＤ, 申込日, 基本入金額, 入会金, 登録区分, 支払区分, 審査パスＦ, 現金Ｆ, バス利用Ｆ, 利用区分, 申込担当者ＣＤ, 携帯番号１, 携帯番号２, 携帯番号３)"
        '    'strSql = strSql & " VALUES ( @会員ＣＤ, @会員種別, @会員名, @会員カナ名, @誕生日, @性別, @郵便番号, @住所, @住所1, @住所2, @住所3, @住所4, @携帯番号, @ＰＣＭＡＩＬ, @入会日, @画像, @会員区分, @職業, @職業ＣＤ, @店舗ＣＤ, @申込日, @基本入金額, @入会金, @登録区分, @支払区分, @審査パスＦ, @現金Ｆ, @バス利用Ｆ, @利用区分, @申込担当者ＣＤ, @携帯番号１, @携帯番号２, @携帯番号３)" '@Head3Numと0埋めした@MAXをともに文字列化してから連結"
        '    ''ADD_end-----2017/10/22 TOS126
        '    strSql = ""

        'End If



        'SQLCommand設定
        Dim sqlcom As SqlCommand = New SqlCommand(strSql, con)

        'パラメータ設定
        sqlcom.Parameters.Add(New SqlParameter("会員ＣＤ", SqlDbType.VarChar)).Value = kainCd

        'sqlcom.Parameters.Add(New SqlParameter("会員種別", SqlDbType.Int)).Value = 0 'ダミー、あとで置き換え'DEL_2017/10/26 TOS163
        sqlcom.Parameters.Add(New SqlParameter("会員種別", SqlDbType.Int)).Value = Session("plan_syubetu") 'ADD_2017/10/26 TOS163
        sqlcom.Parameters.Add(New SqlParameter("会員名", SqlDbType.NVarChar)).Value = Session("name_kanji")
        sqlcom.Parameters.Add(New SqlParameter("会員カナ名", SqlDbType.NVarChar)).Value = Session("name_kana")
        sqlcom.Parameters.Add(New SqlParameter("誕生日", SqlDbType.Date)).Value = Session("birthday")
        If Session("sex").ToString = "男性" Then
            sqlcom.Parameters.Add(New SqlParameter("性別", SqlDbType.NVarChar)).Value = "男"
        Else
            sqlcom.Parameters.Add(New SqlParameter("性別", SqlDbType.NVarChar)).Value = "女"
        End If
        sqlcom.Parameters.Add(New SqlParameter("郵便番号", SqlDbType.Char)).Value = Session("zipcode")
        sqlcom.Parameters.Add(New SqlParameter("住所", SqlDbType.NVarChar)).Value = Session("address")
        sqlcom.Parameters.Add(New SqlParameter("住所1", SqlDbType.NVarChar)).Value = Session("address0")
        sqlcom.Parameters.Add(New SqlParameter("住所2", SqlDbType.NVarChar)).Value = Session("address1")
        sqlcom.Parameters.Add(New SqlParameter("住所3", SqlDbType.NVarChar)).Value = Session("address2")
        sqlcom.Parameters.Add(New SqlParameter("住所4", SqlDbType.NVarChar)).Value = Session("address3")
        sqlcom.Parameters.Add(New SqlParameter("携帯番号", SqlDbType.VarChar)).Value = Session("phonenumber")
        sqlcom.Parameters.Add(New SqlParameter("ＰＣＭＡＩＬ", SqlDbType.VarChar)).Value = Session("email")
        'DEL_start--------------------------------2018/06/01 TOS163
        'If Session("facility_id").ToString = "6" Then
        '    sqlcom.Parameters.Add(New SqlParameter("入会日", SqlDbType.DateTime)).Value = Session("date_visit") & " " & Session("date_visit_time")
        'Else
        'sqlcom.Parameters.Add(New SqlParameter("入会日", SqlDbType.DateTime)).Value = Session("date_visit")
        'End If
        'DEL_end----------------------------------2018/06/01 TOS163
        'ADD_start--------------------------------2018/06/01 TOS163
        Select Case Session("payment")
            Case 0, 1 '現金支払いの場合
                sqlcom.Parameters.Add(New SqlParameter("入会日", SqlDbType.DateTime)).Value = DBNull.Value
            Case 2, 3 'クレジット支払いの場合
                sqlcom.Parameters.Add(New SqlParameter("入会日", SqlDbType.DateTime)).Value = Session("date_join")
        End Select
        'ADD_end----------------------------------2018/06/01 TOS163
        sqlcom.Parameters.Add(New SqlParameter("画像", SqlDbType.VarBinary)).Value = System.Convert.FromBase64String(Session("photodata3")) '切り取り後バイナリデータ a2017/08/07_TOS163
        sqlcom.Parameters.Add(New SqlParameter("会員区分", SqlDbType.Int)).Value = 1 'とりあえず1=フィットネスでよいとのこと
        sqlcom.Parameters.Add(New SqlParameter("職業", SqlDbType.NVarChar)).Value = Session("job")
        sqlcom.Parameters.Add(New SqlParameter("職業ＣＤ", SqlDbType.Int)).Value = Session("job_cd") 'ダミー、あとで置き換え
        sqlcom.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.Int)).Value = Session("ori_facility_id") '本来の店舗ＣＤの方
        'sqlcom.Parameters.Add(New SqlParameter("申込日", SqlDbType.Char)).Value = Today.ToString("yyyyMMdd")'DEL_2017/10/23 TOS163
        'sqlcom.Parameters.Add(New SqlParameter("申込日", SqlDbType.Char)).Value = systemDate 'ADD_2017/10/23 TOS163'DEL_2018/06/01 TOS163
        'ADD_start--------------------------------2018/06/01 TOS163
        Select Case Session("payment")
            Case 0, 1 '現金支払いの場合
                sqlcom.Parameters.Add(New SqlParameter("申込日", SqlDbType.Char)).Value = DBNull.Value
            Case 2, 3 'クレジット支払いの場合
                sqlcom.Parameters.Add(New SqlParameter("申込日", SqlDbType.Char)).Value = systemDate
        End Select
        'ADD_end----------------------------------2018/06/01 TOS163
        sqlcom.Parameters.Add(New SqlParameter("基本入金額", SqlDbType.Decimal)).Value = Session("plan_monthlydues") 'A2017/09/14 セッションの売上単価に変更
        sqlcom.Parameters.Add(New SqlParameter("入会金", SqlDbType.Decimal)).Value = Session("celAdmissionFee") 'ADD_2017/09/27 TOS163
        sqlcom.Parameters.Add(New SqlParameter("登録区分", SqlDbType.NVarChar)).Value = 2 'Web登録=2
        sqlcom.Parameters.Add(New SqlParameter("支払区分", SqlDbType.Int)).Value = Session("payment")

        'ADD_start---2017/09/14 TOS163
        sqlcom.Parameters.Add(New SqlParameter("審査パスＦ", SqlDbType.Int)).Value = 0
        sqlcom.Parameters.Add(New SqlParameter("現金Ｆ", SqlDbType.Int)).Value = 0
        sqlcom.Parameters.Add(New SqlParameter("バス利用Ｆ", SqlDbType.Int)).Value = 0
        sqlcom.Parameters.Add(New SqlParameter("利用区分", SqlDbType.Int)).Value = 0
        sqlcom.Parameters.Add(New SqlParameter("申込担当者ＣＤ", SqlDbType.Int)).Value = 2

        If Mid(Session("phonenumber").ToString, 1, 3) = "010" OrElse
                Mid(Session("phonenumber").ToString, 1, 3) = "020" OrElse
                Mid(Session("phonenumber").ToString, 1, 3) = "030" OrElse
                Mid(Session("phonenumber").ToString, 1, 3) = "040" OrElse
                Mid(Session("phonenumber").ToString, 1, 3) = "050" OrElse
                Mid(Session("phonenumber").ToString, 1, 3) = "060" OrElse
                Mid(Session("phonenumber").ToString, 1, 3) = "070" OrElse
                Mid(Session("phonenumber").ToString, 1, 3) = "080" OrElse
                Mid(Session("phonenumber").ToString, 1, 3) = "090" Then '携帯の場合
            sqlcom.Parameters.Add(New SqlParameter("携帯番号１", SqlDbType.NVarChar)).Value = Mid(Session("phonenumber").ToString, 1, 3)
            sqlcom.Parameters.Add(New SqlParameter("携帯番号２", SqlDbType.NVarChar)).Value = Mid(Session("phonenumber").ToString, 4, 4)
            sqlcom.Parameters.Add(New SqlParameter("携帯番号３", SqlDbType.NVarChar)).Value = Mid(Session("phonenumber").ToString, 8)
        Else
            sqlcom.Parameters.Add(New SqlParameter("携帯番号１", SqlDbType.NVarChar)).Value = Mid(Session("phonenumber").ToString, 1, 4)
            sqlcom.Parameters.Add(New SqlParameter("携帯番号２", SqlDbType.NVarChar)).Value = Mid(Session("phonenumber").ToString, 5, 2)
            sqlcom.Parameters.Add(New SqlParameter("携帯番号３", SqlDbType.NVarChar)).Value = Mid(Session("phonenumber").ToString, 7)
        End If
        'ADD_end-----2017/09/14 TOS163

        'ADD_start---2017/11/07 TOS163
        'sqlcom.Parameters.Add(New SqlParameter("登録日", SqlDbType.Char)).Value = Today.ToString("yyyyMMdd") 'DEL_2017/01/05 TOS163
        'ADD_start---------------------------------------------------------------------------------------------2017/01/05 TOS163
        Dim localNow As DateTime = DateTime.Now
        Dim toUtc As DateTime = localNow.ToUniversalTime()
        toUtc = toUtc.AddHours(9)
        sqlcom.Parameters.Add(New SqlParameter("登録日", SqlDbType.Char)).Value = toUtc.ToString("yyyyMMdd")
        'ADD_end-----------------------------------------------------------------------------------------------2017/01/05 TOS163
        sqlcom.Parameters.Add(New SqlParameter("合計金額", SqlDbType.Decimal)).Value = Session("celsum")                                              'A2017/09/11    セッション変数へ変更
        sqlcom.Parameters.Add(New SqlParameter("合計税抜金額", SqlDbType.Decimal)).Value = Math.Ceiling(Session("celsum") / 1.08)                     'A2017/09/11    セッション変数へ変更
        sqlcom.Parameters.Add(New SqlParameter("合計消費税額", SqlDbType.Decimal)).Value = Session("celsum") - Math.Ceiling(Session("celsum") / 1.08) 'A2017/09/11    セッション変数へ変更
        sqlcom.Parameters.Add(New SqlParameter("商品ＣＤ", SqlDbType.Char)).Value = Session("plan_productcd").ToString
        sqlcom.Parameters.Add(New SqlParameter("商品名", SqlDbType.NVarChar)).Value = Session("plan_name").ToString
        sqlcom.Parameters.Add(New SqlParameter("入金年月1", SqlDbType.Int)).Value = Session("THISM").ToString
        sqlcom.Parameters.Add(New SqlParameter("入金年月2", SqlDbType.Int)).Value = Session("NEXTM").ToString
        sqlcom.Parameters.Add(New SqlParameter("年月1金額", SqlDbType.Decimal)).Value = CDec(Session("Monthlydues1").ToString)
        sqlcom.Parameters.Add(New SqlParameter("年月2金額", SqlDbType.Decimal)).Value = CDec(Session("Monthlydues2").ToString)
        'ADD_end-----2017/11/07 TOS163
        'ADD_start---2018/03/06 TOS163
        sqlcom.Parameters.Add(New SqlParameter("入金年月3", SqlDbType.Int)).Value = Session("NEXTNEXTM").ToString
        sqlcom.Parameters.Add(New SqlParameter("年月3金額", SqlDbType.Decimal)).Value = CDec(Session("Monthlydues3").ToString)
        'ADD_end-----2018/03/06 TOS163
        sqlcom.Parameters.Add(New SqlParameter("初回来店日予定", SqlDbType.DateTime)).Value = Session("date_visit") 'ADD_2018/06/01 TOS163


        'SQLCommand実行
        Dim result As String
        result = sqlcom.ExecuteNonQuery()


        con.Close()

    End Sub


End Class

