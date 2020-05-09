Imports System.Data.SqlClient
Public Class Registration_1_2
    Inherits System.Web.UI.Page
    Public Const STRNDate = "2019/10/01"            'A2019/08/31

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try

            '入力チェック
            If DetailInputCheck() = False Then Exit Sub

            Dim con As New SqlConnection

            'DB接続
            con.ConnectionString = ConfigurationManager.ConnectionStrings("OnPremisesDBConnection").ConnectionString


            'コース選択処理-------------------------------------------------------------------------------
            '新しいテーブルの生成ストアドプロシージャ
            Dim strSql As String = "SELECT * FROM Ｍ商品 WHERE 店舗ＣＤ = @店舗ＣＤ AND 画像パス = @画像パス"
            'SQLCommand設定
            Dim sqlcom As SqlCommand = New SqlCommand(strSql, con)

            'パラメータ設定
            sqlcom.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.VarChar)).Value = Session("facility_id")
            sqlcom.Parameters.Add(New SqlParameter("画像パス", SqlDbType.VarChar)).Value = hdnGetsrc_P.Value

            'SQLCommand実行
            Dim dt As New DataTable()
            Dim sda As New SqlDataAdapter(sqlcom)
            sda.Fill(dt)

            '商品名表示列の導入により消す予定　
            'If dt.Rows(0)("商品名").ToString.IndexOf("tﾏｽﾀｰ") <> -1 Then '明示的にStringに変換し、IndexOfで目的の文字列が見つからないとき-1を返す＝見つかったときは-1以外を返す
            '    Session("plan_name") = "マスター会員"
            'ElseIf dt.Rows(0)("商品名").ToString.IndexOf("fmﾏｽﾀｰ") <> -1 Then
            '    Session("plan_name") = "マスター会員"
            'ElseIf dt.Rows(0)("商品名").ToString.IndexOf("esﾏｽﾀｰ") <> -1 Then
            '    Session("plan_name") = "フィットネス会員"
            'ElseIf dt.Rows(0)("商品名").ToString.IndexOf("ﾃﾞｨ") <> -1 Then
            '    Session("plan_name") = "デイタイム会員"
            'ElseIf dt.Rows(0)("商品名").ToString.IndexOf("ﾌｧﾐ2") <> -1 Then
            '    Session("plan_name") = "ファミリー会員(２人)"
            'ElseIf dt.Rows(0)("商品名").ToString.IndexOf("ﾌｧﾐ3") <> -1 Then
            '    Session("plan_name") = "ファミリー会員(３人以上)"
            'ElseIf dt.Rows(0)("商品名").ToString.IndexOf("ｺﾞｰﾙﾄﾞ") <> -1 Then
            '    Session("plan_name") = "ゴールド会員"
            'ElseIf dt.Rows(0)("商品名").ToString.IndexOf("GD") <> -1 Then
            '    Session("plan_name") = "ゴールドデイタイム会員"
            'End If
            Session("plan_name") = dt.Rows(0)("商品名表示")

            Session("plan_monthlydues") = dt.Rows(0)("売上単価")
            Session("plan_monthlydues2") = dt.Rows(0)("売上単価2")      'A2019/08/31

            Session("plan_img") = hdnGetsrc_P.Value
            Session("plan_imgid") = hdnGetid_P.Value

            'ADD_start-----2017/08/17_TOS163
            Session("plan_productcd") = dt.Rows(0)("商品ＣＤ")
            Session("plan_monthlydues_t") = dt.Rows(0)("売上単価税抜")
            'ADD_end-------2017/08/17_TOS163

            Session("plan_syubetu") = dt.Rows(0)("会員種別") 'ADD_201710/26 TOS163

            ''オプション選択処理-------------------------------------------------------------------------------
            'strSql = "SELECT * FROM GymPlanOption WHERE StoreID = @StoreID AND OptionImg = @OptionImg"
            'sqlcom = New SqlCommand(strSql, con)

            'sqlcom.Parameters.Add(New SqlParameter("StoreID", SqlDbType.VarChar)).Value = Session("facility_id")
            'sqlcom.Parameters.Add(New SqlParameter("OptionImg", SqlDbType.VarChar)).Value = hdnGetsrc_O.Value

            'Dim dt2 As New DataTable()
            'Dim sda2 As New SqlDataAdapter(sqlcom)
            'sda2.Fill(dt2)

            'If dt2.Rows.Count = 0 Then
            Session("option_name") = "オプションなし"
            Session("option_monthlydues") = 0
            'Else
            '    Session("option_name") = dt2.Rows(0)("OptionName")
            '    Session("option_monthlydues") = dt2.Rows(0)("MonthlyDues")
            'End If

            Session("option_img") = hdnGetsrc_O.Value
            Session("option_imgid") = hdnGetid_O.Value

            'Session("date_visit") = datepicker.Text
            Session("date_visit") = txtDatepicker.Text

            If Session("date_visit") >= STRNDate Then                               'A2019/08/31
                Session("plan_monthlydues") = dt.Rows(0)("売上単価2")                   'A2019/08/31
            End If                                                                      'A2019/08/31


            'ADD_start-------------------------------------------------2018/05/31 TOS163
            Dim localNow As DateTime = DateTime.Now
            Dim toUtc As DateTime = localNow.ToUniversalTime()
            toUtc = toUtc.AddHours(9)
            Dim HonzituMonth As String = toUtc.ToString("MM")
            Dim NextMonth As String = toUtc.AddMonths(1).ToString("MM")

            '来店日の月が来月の場合、入会日が1日付になる
            If CInt(NextMonth) = CInt(Mid(txtDatepicker.Text, 6, 2)) Then
                Session("date_join") = Mid(txtDatepicker.Text, 1, 8) & "01"
            Else
                Session("date_join") = txtDatepicker.Text
            End If
            'ADD_start-------------------------------------------------2018/05/31 TOS163




            LogInsert() 'renaissDBに登録　ADD_2017/09/29 TOS163

        Catch ex As Exception
            MsgBox("システムエラーが起きました。")
            Exit Sub
        End Try

        'Server.Transfer("Registration_3.aspx")
        Response.Redirect("Registration_3.aspx")
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            '店舗ＣＤ取得
            Session("ori_facility_id") = ddlFacilityName.SelectedItem.Value '越前・鯖江の本来の店舗ＣＤを保存
            If ddlFacilityName.SelectedItem.Value = 1 Or ddlFacilityName.SelectedItem.Value = 2 Then
                Session("facility_id") = 999  '越前・鯖江の店舗ＣＤは999に集約(Ｍ商品テーブルへの照合のため)
            Else
                Session("facility_id") = ddlFacilityName.SelectedItem.Value 'その他は選択されたままの店舗ＣＤ
            End If
            Session("facility_name") = ddlFacilityName.SelectedItem.Text '店舗名取得

            Dim con1 As New SqlConnection
            Dim con2 As New SqlConnection
            Dim con3 As New SqlConnection
            'DB接続
            '            con.ConnectionString = "Data Source=hokutos-tester.database.windows.net;Initial Catalog=hokutos-tester;Persist Security Info=True;User ID=Komaikou;Password=Tanstaafl2045"
            con1.ConnectionString = ConfigurationManager.ConnectionStrings("OnPremisesDBConnection").ConnectionString
            con2.ConnectionString = ConfigurationManager.ConnectionStrings("OnPremisesDBConnection").ConnectionString
            con3.ConnectionString = ConfigurationManager.ConnectionStrings("renaissDBConnectionString").ConnectionString

            '新しいテーブルの生成ストアドプロシージャ
            'Dim strSql1 As String = "SELECT * FROM Ｍ商品 WHERE 商品名 = 'フィットネス登録料' ORDER BY 商品ＣＤ" 'フィットネス登録料取得、複数あるので商品ＣＤの数字が小さい方=0016
            Dim strSql1 As String
            Select Case Session("facility_id")
                Case "5"
                    strSql1 = "SELECT * FROM Ｍ商品 WHERE 商品名 = 'フィットネス登録料(福井南)'" 'ADD_2018/03/16 TOS163
                Case "6"
                    strSql1 = "SELECT * FROM Ｍ商品 WHERE 商品名 = 'フィットネス登録料(福井西)'" 'ADD_2018/03/16 TOS163
                Case Else
                    strSql1 = "SELECT * FROM Ｍ商品 WHERE 商品名 = 'フィットネス登録料' ORDER BY 商品ＣＤ" 'ADD_2018/03/16 TOS163
            End Select

            Dim strSql2 As String = "SELECT * FROM Ｍ商品 WHERE 商品名 LIKE '大人フィットネス入会金%' AND 店舗ＣＤ = @店舗ＣＤ" '大人フィットネス入会金取得 


            'SQLCommand設定
            Dim sqlcom1 As SqlCommand = New SqlCommand(strSql1, con1)
            Dim sqlcom2 As SqlCommand = New SqlCommand(strSql2, con2)
            'パラメータ設定
            sqlcom2.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.VarChar)).Value = Session("facility_id")
            'SQLCommand実行
            Dim dt1 As New DataTable()
            Dim dt2 As New DataTable()
            Dim sda1 As New SqlDataAdapter(sqlcom1)
            Dim sda2 As New SqlDataAdapter(sqlcom2)
            sda1.Fill(dt1)
            sda2.Fill(dt2)

            'ADD_start----------------------------------------------------------------------------------------------------2018/05/17 TOS163
            '登録手数料キャンペーン金額
            Dim strSql3 As String = "SELECT 売上単価 FROM Ｍ商品ＷＥＢ WHERE 商品ＣＤ = @商品ＣＤ AND 開始日 <= @本日 AND (終了日 >= @本日 OR 終了日 is null)"
            Dim sqlcom3 As SqlCommand = New SqlCommand(strSql3, con3)
            'パラメータ設定
            Dim localNow As DateTime = DateTime.Now
            Dim toUtc As DateTime = localNow.ToUniversalTime()
            toUtc = toUtc.AddHours(9)
            sqlcom3.Parameters.Add(New SqlParameter("本日", SqlDbType.DateTime)).Value = toUtc.ToString("yyyy/MM/dd HH:mm:ss")
            sqlcom3.Parameters.Add(New SqlParameter("商品ＣＤ", SqlDbType.NVarChar)).Value = dt1.Rows(0)("商品ＣＤ").ToString

            Dim dt3 As New DataTable()
            Dim sda3 As New SqlDataAdapter(sqlcom3)
            sda3.Fill(dt3)

            Dim WEB登録料 As String
            If dt3.Rows.Count = 0 Then
                WEB登録料 = ""
            Else
                WEB登録料 = dt3.Rows(0)("売上単価").ToString

            End If

            '入会金キャンペーン金額
            strSql3 = "SELECT 売上単価 FROM Ｍ商品ＷＥＢ WHERE 商品ＣＤ = @商品ＣＤ"
            sqlcom3 = New SqlCommand(strSql3, con3)
            'パラメータ設定
            sqlcom3.Parameters.Add(New SqlParameter("商品ＣＤ", SqlDbType.NVarChar)).Value = dt2.Rows(0)("商品ＣＤ").ToString
            sda3 = New SqlDataAdapter(sqlcom3)
            sda3.Fill(dt3)

            Dim WEB入会金 As String
            If dt3.Rows.Count = 0 Then
                WEB入会金 = ""
            Else
                WEB入会金 = dt3.Rows(0)("売上単価").ToString
            End If
            'ADD_end------------------------------------------------------------------------------------------------------2018/05/17 TOS163


            '消すSession("facility_name") = dt.Rows(0)("StoreName")
            'Session("facility_admissionfee") = dt2.Rows(0)("売上単価")

            '入会金 'ADD_2018/03/16 TOS63
            If WEB入会金 = "" Then
                If Session("date_visit") >= STRNDate Then                   'A2019/08/31
                    Session("facility_admissionfee") = dt2.Rows(0)("売上単価2")
                Else
                    Session("facility_admissionfee") = dt2.Rows(0)("売上単価")
                End If
            Else
                    Session("facility_admissionfee") = WEB入会金
            End If

            'DEL_start-------------------------------------------------------------------------------2018/03/16 TOS163
            'If Session("facility_id") = "5" Then
            '    Session("facility_registrationfee") = 0 '福井南店は登録料０円 ADD_2017/12/26 TOS163
            'Else
            '    'Session("facility_registrationfee") = dt1.Rows(0)("売上単価")'DEL_2018/03/05 TOS163
            '    Session("facility_registrationfee") = 0 'ADD_2018/03/05 TOS163
            'End If
            'DEL_end---------------------------------------------------------------------------------2018/03/16 TOS163

            '登録料 'ADD_2018/03/16 TOS63
            If WEB登録料 = "" Then
                If Session("date_visit") >= STRNDate Then                   'A2019/08/31
                    Session("facility_registrationfee") = dt1.Rows(0)("売上単価2")
                Else
                    Session("facility_registrationfee") = dt1.Rows(0)("売上単価")
                End If
            Else
                    Session("facility_registrationfee") = WEB登録料
            End If

            lblAdmissionFee.Text = CInt(Session("facility_admissionfee").ToString).ToString("#,0") + "円"
            lblRegistrationFee.Text = CInt(Session("facility_registrationfee").ToString).ToString("#,0") + "円"

            dltPlan.Visible = True
            dltOption.Visible = True
            'datepicker.Visible = True
            pnlDatepicker.Visible = True

            '敦賀店ではメッセージを表示 ADD_2017/12/26 TOS163
            If Session("facility_id") = "4" Then
                lblTsurugaMsg.Visible = True
            Else
                lblTsurugaMsg.Visible = False
            End If


        Catch ex As Exception
            MsgBox("システムエラーが起きました。")
            Exit Sub
        End Try

    End Sub

    Private Function DetailInputCheck() As Boolean

        '--- 店舗選択(必須) ---
        If ddlFacilityName.Text = "" Then
            MsgBox("店舗を選択してください。")
            'ddlFacilityName.BackColor = System.Drawing.Color.FromName("Pink")
            ddlFacilityName.Focus()
            Return False
        End If

        '--- コース選択(必須) ---
        If hdnGetsrc_P.Value = "0" Then
            'MsgBox("コースを選択してください。")
            lblerr_plan.Text = "※コースを選択してください。"
            'ddlFacilityName.BackColor = System.Drawing.Color.FromName("Pink")
            dltPlan.Focus()
            Return False
        Else
            lblerr_plan.Text = ""
        End If

        ''--- オプション選択(必須) ---
        'If hdnGetsrc_O.Value = "0" Then
        '    'MsgBox("オプションを選択してください。")
        '    lblerr_option.Text = "※オプションを選択してください。"
        '    'ddlFacilityName.BackColor = System.Drawing.Color.FromName("Pink")
        '    dltOption.Focus()
        '    Return False
        'Else
        '    lblerr_option.Text = ""
        'End If

        '--- ご来店予定日(必須) ---
        If txtDatepicker.Text.ToString = "" Then
            'MsgBox("ご来店予定日を入力してください。")
            lblerr_datepicker.Text = "※ご来店予定日を入力してください。"
            'ddlFacilityName.BackColor = System.Drawing.Color.FromName("Pink")
            dltOption.Focus()
            Return False
        Else
            'lblerr_datepicker.Text = ""
            Dim str As String = txtDatepicker.Text
            'DateTimeに変換できるか確かめる
            Dim dt As DateTime
            If Not DateTime.TryParse(str, dt) Then
                lblerr_datepicker.Text = "※正しい日付を入力してください。"
                dltOption.Focus()
                Return False
            Else
                Dim T_day As Date = DateTime.UtcNow.AddHours(9)
                Dim S_day As Date = CDate(txtDatepicker.Text)
                If T_day > S_day Then
                    lblerr_datepicker.Text = "※今日の日付以降の日付を入力してください。"
                    dltOption.Focus()
                    Return False
                End If
            End If

            lblerr_datepicker.Text = ""
        End If

        'DEL_start-----------------------------------------2018/05/08 TOS163
        'Add_start-----------------------------------------2018/03/10 TOS163
        ''春のキャンペーン2018　日付選択制限設定
        'Dim day As DateTime = DateTime.UtcNow.AddHours(9)
        'Dim dayMonth As String = day.ToString("MM")
        'Dim MarchSeigen As DateTime = "2018/04/02 00:00:00"
        'Dim SpringSeigen As DateTime = "2018/05/02 00:00:00"

        'If dayMonth = "03" Then
        '    If CDate(txtDatepicker.Text) >= MarchSeigen Then
        '        lblerr_datepicker.Text = "※4/1以降選択することができません。"
        '        dltOption.Focus()
        '        Return False
        '    Else
        '        lblerr_datepicker.Text = ""
        '    End If

        'Else
        '    If CDate(txtDatepicker.Text) >= SpringSeigen Then
        '        lblerr_datepicker.Text = "※5/1以降選択することができません。"
        '        dltOption.Focus()
        '        Return False
        '    Else
        '        lblerr_datepicker.Text = ""
        '    End If
        'End If
        ''Add_end-------------------------------------------2018/03/10 TOS163
        'DEL_end-------------------------------------------2018/05/08 TOS163


        'If datepicker.Text = "" Then
        '    'MsgBox("ご来店予定日を入力してください。")
        '    lblerr_datepicker.Text = "※ご来店予定日を入力してください。"
        '    'ddlFacilityName.BackColor = System.Drawing.Color.FromName("Pink")
        '    dltOption.Focus()
        '    Return False
        'Else
        '    Dim str As String = datepicker.Text
        '    'DateTimeに変換できるか確かめる
        '    Dim dt As DateTime
        '    If Not DateTime.TryParse(str, dt) Then
        '        'MsgBox("正しい日付を入力してください。")
        '        lblerr_datepicker.Text = "※正しい日付を入力してください。"
        '        dltOption.Focus()
        '        Return False
        '    Else
        '        Dim T_day As Date = DateTime.Today
        '        Dim S_day As Date = CDate(datepicker.Text)
        '        If T_day > S_day Then
        '            'MsgBox("今日の日付以降の日付を入力してください。")
        '            lblerr_datepicker.Text = "※今日の日付以降の日付を入力してください。"
        '            dltOption.Focus()
        '            Return False
        '        End If
        '    End If

        '    lblerr_datepicker.Text = ""
        'End If

        Return True

    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'SqlDataSource1.ConnectionString = ConfigurationManager.ConnectionStrings("OnPremisesDBConnection").ConnectionString
        SqlDataSource2.ConnectionString = ConfigurationManager.ConnectionStrings("OnPremisesDBConnection").ConnectionString
        SqlDataSource3.ConnectionString = ConfigurationManager.ConnectionStrings("OnPremisesDBConnection").ConnectionString

        If Not Me.IsPostBack Then
            'ADD_start--------------------------------------2018/05/17 TOS163
            Dim con As New SqlConnection

            'DB接続
            con.ConnectionString = ConfigurationManager.ConnectionStrings("renaissDBConnectionString").ConnectionString

            'コース選択処理-------------------------------------------------------------------------------
            '新しいテーブルの生成ストアドプロシージャ
            Dim strSql As String = "SELECT M.店舗ＣＤ, M.店舗名 FROM WebsiteReleasePeriod W, Ｍ店舗 M WHERE W.開始日 <= @本日 AND (W.終了日 >= @本日 OR W.終了日 is null) AND W.店舗ＣＤ = M.店舗ＣＤ"
            'SQLCommand設定
            Dim sqlcom As SqlCommand = New SqlCommand(strSql, con)

            'パラメータ設定
            Dim localNow As DateTime = DateTime.Now
            Dim toUtc As DateTime = localNow.ToUniversalTime()
            toUtc = toUtc.AddHours(9)
            sqlcom.Parameters.Add(New SqlParameter("本日", SqlDbType.DateTime)).Value = toUtc.ToString("yyyy/MM/dd HH:mm:ss")

            'SQLCommand実行
            Dim dt As New DataTable()
            Dim sda As New SqlDataAdapter(sqlcom)
            sda.Fill(dt)

            ddlFacilityName.DataSource = dt
            ddlFacilityName.DataBind()
            'ADD_end----------------------------------------2018/05/17 TOS163


            Select Case Session("facility_id").ToString

                Case "999"
                    If Session("facility_name").ToString = "越前" Then
                        ddlFacilityName.SelectedValue = "1"
                    Else
                        ddlFacilityName.SelectedValue = "2"
                    End If

                Case Else
                    ddlFacilityName.SelectedValue = Session("facility_id").ToString

            End Select

            lblAdmissionFee.Text = CInt(Session("facility_admissionfee").ToString).ToString("#,0") + "円"
            lblRegistrationFee.Text = CInt(Session("facility_registrationfee").ToString).ToString("#,0") + "円"

            hdnGetsrc_P.Value = Session("plan_img").ToString
            hdnGetid_P.Value = Session("plan_imgid").ToString
            'hdnGetsrc_O.Value = Session("option_img").ToString
            'hdnGetid_O.Value = Session("option_imgid").ToString
            'datepicker.Text = Session("date_visit").ToString
            txtDatepicker.Text = Session("date_visit").ToString
        End If


    End Sub


    Private Sub LogInsert() 'ADD_2017/09/29 TOS163

        Dim con As New SqlConnection

        'DB接続
        con.ConnectionString = ConfigurationManager.ConnectionStrings("renaissDBConnectionString").ConnectionString
        con.Open()

        'Session("LOGid")と同じID行を消す
        'If Session("LOGid").ToString <> "" Then

        '    Dim strSqlDel As String = ""
        '    strSqlDel = strSqlDel & " DELETE FROM LOG2_Ｍ会員_FITNESS WHERE ID = @ID"

        '    'SQLCommand設定
        '    Dim sqlcomdel As SqlCommand = New SqlCommand(strSqlDel, con)

        '    'パラメータ設定
        '    sqlcomdel.Parameters.Add(New SqlParameter("ID", SqlDbType.Int)).Value = Session("LOGid")

        '    'SQLCommand実行
        '    Dim resultdel As String
        '    resultdel = sqlcomdel.ExecuteNonQuery()

        'End If


        '===Ｍ会員_FITNESSのINSERT文==========================================================================================================
        'コース選択処理-------------------------------------------------------------------------------
        '新しいテーブルの生成ストアドプロシージャ
        Dim strSql As String = ""
        strSql = strSql & " INSERT INTO LOG2_Ｍ会員_FITNESS (会員種別, 会員名, 会員カナ名, 誕生日, 性別, 郵便番号, 住所, 住所1, 住所2, 住所3, 住所4, 携帯番号, ＰＣＭＡＩＬ, 入会日, 画像, 会員区分, 職業, 職業ＣＤ, 店舗ＣＤ, 申込日, 基本入金額, 入会金, 登録区分, 支払区分, 審査パスＦ, 現金Ｆ, バス利用Ｆ, 利用区分, 申込担当者ＣＤ, 携帯番号１, 携帯番号２, 携帯番号３, Timestamp)"
        strSql = strSql & " VALUES (@会員種別, @会員名, @会員カナ名, @誕生日, @性別, @郵便番号, @住所, @住所1, @住所2, @住所3, @住所4, @携帯番号, @ＰＣＭＡＩＬ, @入会日, @画像, @会員区分, @職業, @職業ＣＤ, @店舗ＣＤ, @申込日, @基本入金額, 0, @登録区分, @支払区分, @審査パスＦ, @現金Ｆ, @バス利用Ｆ, @利用区分, @申込担当者ＣＤ, @携帯番号１, @携帯番号２, @携帯番号３, DATEADD(hour, 9, GETUTCDATE()))" '@Head3Numと0埋めした@MAXをともに文字列化してから連結"


        'SQLCommand設定
        Dim sqlcom As SqlCommand = New SqlCommand(strSql, con)

        'パラメータ設定
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
        'sqlcom.Parameters.Add(New SqlParameter("性別", SqlDbType.NVarChar)).Value = Session("sex").ToString.Replace("性", "")
        sqlcom.Parameters.Add(New SqlParameter("郵便番号", SqlDbType.Char)).Value = Session("zipcode")
        sqlcom.Parameters.Add(New SqlParameter("住所", SqlDbType.NVarChar)).Value = Session("address")
        sqlcom.Parameters.Add(New SqlParameter("住所1", SqlDbType.NVarChar)).Value = Session("address0")
        sqlcom.Parameters.Add(New SqlParameter("住所2", SqlDbType.NVarChar)).Value = Session("address1")
        sqlcom.Parameters.Add(New SqlParameter("住所3", SqlDbType.NVarChar)).Value = Session("address2")
        sqlcom.Parameters.Add(New SqlParameter("住所4", SqlDbType.NVarChar)).Value = Session("address3")
        sqlcom.Parameters.Add(New SqlParameter("携帯番号", SqlDbType.VarChar)).Value = Session("phonenumber")
        sqlcom.Parameters.Add(New SqlParameter("ＰＣＭＡＩＬ", SqlDbType.VarChar)).Value = Session("email")
        sqlcom.Parameters.Add(New SqlParameter("入会日", SqlDbType.Date)).Value = Session("date_visit")
        sqlcom.Parameters.Add(New SqlParameter("画像", SqlDbType.VarBinary)).Value = System.Convert.FromBase64String(Session("photodata3")) '切り取り後バイナリデータ a2017/08/07_TOS163
        sqlcom.Parameters.Add(New SqlParameter("会員区分", SqlDbType.Int)).Value = 1 'とりあえず1=フィットネスでよいとのこと
        sqlcom.Parameters.Add(New SqlParameter("職業", SqlDbType.NVarChar)).Value = Session("job")
        sqlcom.Parameters.Add(New SqlParameter("職業ＣＤ", SqlDbType.Int)).Value = Session("job_cd") 'ダミー、あとで置き換え
        sqlcom.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.Int)).Value = Session("ori_facility_id") '本来の店舗ＣＤの方
        sqlcom.Parameters.Add(New SqlParameter("申込日", SqlDbType.Char)).Value = Today.ToString("yyyyMMdd")
        sqlcom.Parameters.Add(New SqlParameter("基本入金額", SqlDbType.Decimal)).Value = Session("plan_monthlydues") 'A2017/09/14 セッションの売上単価に変更
        'sqlcom.Parameters.Add(New SqlParameter("入会金", SqlDbType.Decimal)).Value = Session("celAdmissionFee") 'A2017/09/14
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


        'SQLCommand実行
        Dim result As String
        result = sqlcom.ExecuteNonQuery()

    End Sub

End Class