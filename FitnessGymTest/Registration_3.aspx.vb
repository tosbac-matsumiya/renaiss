Imports System.IO
Imports System.Data.SqlClient
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
Imports System.Drawing

Public Class Registration_3
    Inherits System.Web.UI.Page

    Public kainCd As String
    Public systemDate As String
    Public Denno As String
    Public Const STRNDate = "2019/10/01"            'A2019/08/31


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '店舗名
        lblFacilityName.Text = Session("facility_name").ToString
        'コース名
        lblPlanName.Text = Session("plan_name").ToString
        'オプション名
        lblOptionName.Text = Session("option_name").ToString
        '来店日
        Dim day As String = Session("date_visit").ToString
        lblDateVisit.Text = Mid(day, 1, 4) + "年" + CStr(CInt(Mid(day, 6, 2))) + "月" + CStr(CInt(Mid(day, 9, 2))) + "日"

        ''入会費
        'celAdmissionFee.Text = CInt(Session("facility_admissionfee").ToString).ToString("#,0")
        ''登録手数料
        'celRegistrationFee.Text = CInt(Session("facility_registrationfee").ToString).ToString("#,0")
        ''今月の月会費
        Dim s_thisM As String = Mid(day, 6, 2) 'MMを抜き出す
        'DEL_start--------------------------------------------2017/10/10 TOS163
        'Dim i_thisM As Integer = CInt(s_thisM)
        'Session("THISM") = i_thisM              'A2017/09/11
        'celMonth1.Text = CStr(i_thisM) + "月分"
        'DEL_end----------------------------------------------2017/10/10 TOS163
        'ADD_start--------------------------------------------2017/10/10 TOS163
        Session("THISM") = Mid(day, 1, 4) + Mid(day, 6, 2)
        celMonth1.Text = s_thisM + "月分"
        'ADD_end----------------------------------------------2017/10/10 TOS163
        'celMonthlyDues1.Text = DailyCalculation().ToString("#,0")
        'celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")

        '来月の月会費
        Dim d_nextM As DateTime = CDate(day).AddMonths(1)
        Dim s_nextM As String = d_nextM.ToString("yyyy/MM/dd")
        s_nextM = Mid(s_nextM, 6, 2) 'MMを抜き出す
        '消すd_nextM = d_nextM.AddMonths(1)


        'DEL_start--------------------------------------------2017/10/10 TOS163
        'Dim i_nextM As Integer = CInt(s_nextM)
        'Session("NEXTM") = i_nextM                  'A2017/09/11
        'celMonth2.Text = CStr(i_nextM) + "月分"
        'DEL_end----------------------------------------------2017/10/10 TOS163
        'ADD_start--------------------------------------------2017/10/10 TOS163
        Session("NEXTM") = d_nextM.ToString("yyyyMM")
        celMonth2.Text = s_nextM + "月分"
        'ADD_end----------------------------------------------2017/10/10 TOS163


        'celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
        'celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")

        'ADD_start--------------------------------------------2018/03/05 TOS163
        '再来月の月会費
        Dim d_nextM2 As DateTime = CDate(day).AddMonths(2)
        Dim s_nextM2 As String = d_nextM2.ToString("yyyy/MM/dd")
        s_nextM2 = Mid(s_nextM2, 6, 2) 'MMを抜き出す

        Session("NEXTNEXTM") = d_nextM2.ToString("yyyyMM")
        celMonth3.Text = s_nextM2 + "月分"
        'ADD_end----------------------------------------------2018/03/05 TOS163


        Campaign()

        '初回支払金額
        Dim nyukai As Integer = CInt(celAdmissionFee.Text)
        Dim touroku As Integer = CInt(celRegistrationFee.Text)
        Dim tukikaihi1 As Integer = CInt(celMonthlyDues1.Text)
        Dim tukikaihi2 As Integer = CInt(celMonthlyDues2.Text)
        Dim tukikaihi3 As Integer = CInt(celMonthlyDues3.Text) 'add_2018/03/05 TOS163
        Dim option1 As Integer = CInt(celMonthlyDues1_O.Text)
        Dim option2 As Integer = CInt(celMonthlyDues2_O.Text)
        Dim option3 As Integer = CInt(celMonthlyDues3_O.Text) 'add_2018/03/05 TOS163
        Dim sum As Integer = nyukai + touroku + tukikaihi1 + tukikaihi2 + option1 + option2 + tukikaihi3 + option3
        celsum.Text = sum.ToString("#,0")

        Session("celSum") = sum.ToString("#,0")         'A2017/08/03



        'お名前
        lblNameKanji.Text = Session("name_kanji").ToString
        'フリガナ
        lblNameKana.Text = Session("name_kana").ToString
        '性別
        lblSex.Text = Session("sex").ToString
        '生年月日
        Dim bday As String = Session("birthday").ToString
        'lblBirthDay.Text = Mid(bday, 1, 4) + "年" + CStr(CInt(Mid(bday, 6, 2))) + "月" + CStr(CInt(Mid(bday, 9, 2))) + "日"
        lblBirthDay.Text = bday
        '職業
        lblJob.Text = Session("job").ToString
        '電話番号
        lblPhoneNumber.Text = Session("phonenumber").ToString
        '郵便番号
        Dim zc As String = Session("zipcode").ToString
        lblZipCode.Text = "〒" + Mid(zc, 1, 3) + "-" + Mid(zc, 4, 4)
        '住所
        lblAddress.Text = Session("address").ToString
        'メールアドレス
        lblEmail.Text = Session("email").ToString
        '消す'お支払方法
        'lblPayment.Text = Session("payment").ToString

        'If Session("credit_id").ToString = "0" Then
        '    Panel1.Visible = True

        '    'クレジットカードの種類
        '    lblCreditName.Text = Session("credit_name").ToString
        '    'クレジットカード番号
        '    Dim nm As String = Session("credit_number").ToString
        '    lblCreditNumber.Text = "************" + Mid(nm, 13, 4)
        '    'クレジットカードの有効期限
        '    lblCreditDate.Text = Session("credit_date").ToString

        'End If


        '会員証画像
        imgPhoto.ImageUrl = Session("photodata2").ToString

        '--コースと年齢ﾁｪｯｸ---------------------------------------ADD_start_2017/12/19 TOS163
        '生年月日
        Dim birthDate As DateTime = Session("birthday")
        '現在の日付
        'パラメータ設定
        Dim localNow As DateTime = DateTime.Now
        Dim toUtc As DateTime = localNow.ToUniversalTime()
        toUtc = toUtc.AddHours(9)
        Dim today As DateTime = toUtc

        '年齢を計算する
        Dim age As Integer = GetAge(birthDate, today)

        If Left(Session("plan_name").ToString, 4) = "ゴールド" Then
            If age < 65 Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alertscript5", "window.alert('ゴールド会員は65歳以上の方対象商品です。【ご利用情報登録画面へ戻る】でコースを選択し直してください。');", True)
                BtnOK.Enabled = False
                btnReturn1.Focus()
                lblerr_Return1.Text = "コースを選択し直してください。"
                Exit Sub
            End If
        End If

        BtnOK.Enabled = True
        lblerr_Return1.Text = ""
        '--コースと年齢ﾁｪｯｸ---------------------------------------ADD_end___2017/12/19 TOS163

    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click

        Session("celsum") = CDec(celsum.Text)

        'fitnessindex.aspxから戻るボタンで来た場合は、処理しない
        If Session("name_kanji").ToString = "" Then
            'Server.Transfer("fitnessindex.aspx")
            HyperLink7.Visible = True
            lblItido.Visible = True
            Exit Sub
        End If

        'a2017/08/03 sh---start-----------------------------------------
        If chkDoisyo2.Checked = False Then
            lblerr_chkdoisyo2.Text = "※規約の同意チェックをしてください。"
            chkDoisyo2.Focus()
            Exit Sub
        Else
            lblerr_chkdoisyo2.Text = ""
        End If
        'a2017/08/03 sh---end-------------------------------------------

        'a2017/07/28 sh---start-----------------------------------------
        If chkDoisyo1.Checked = False Then
            lblerr_chkdoisyo1.Text = "※個人情報規約のチェックをしてください。"
            chkDoisyo1.Focus()
            Exit Sub
        Else
            lblerr_chkdoisyo1.Text = ""
        End If
        'a2017/07/28 sh---end-------------------------------------------

        '同意書のチェック
        If chkDoisyo.Checked = False Then
            lblerr_chkdoisyo.Text = "※同意書のチェックをしてください。"
            chkDoisyo.Focus()
            Exit Sub
        Else
            lblerr_chkdoisyo.Text = ""
        End If

        'sdsRegistration.InsertParameters.Add("FacilityName", Session("facility_name").ToString)
        'sdsRegistration.InsertParameters.Add("MembershipPlanName", Session("plan_Name").ToString)
        'sdsRegistration.InsertParameters.Add("PlanoptionName", Session("option_Name").ToString)
        'sdsRegistration.InsertParameters.Add("DateVisit", Session("date_visit").ToString)
        'sdsRegistration.InsertParameters.Add("NameKanji", Session("name_kanji").ToString)
        'sdsRegistration.InsertParameters.Add("NameKana", Session("name_kana").ToString)
        'sdsRegistration.InsertParameters.Add("Sex", Session("sex").ToString)
        'sdsRegistration.InsertParameters.Add("BirthDay", Session("birthday").ToString)
        'sdsRegistration.InsertParameters.Add("Job", Session("job").ToString)
        'sdsRegistration.InsertParameters.Add("PhoneNumber", Session("phonenumber").ToString)
        'sdsRegistration.InsertParameters.Add("ZipCode", Session("zipcode").ToString)
        'sdsRegistration.InsertParameters.Add("Address", Session("address").ToString)
        'sdsRegistration.InsertParameters.Add("Email", Session("email").ToString)
        'sdsRegistration.InsertParameters.Add("Payment", Session("payment").ToString)
        'sdsRegistration.InsertParameters.Add("PhotoData", Session("photodata").ToString)
        'sdsRegistration.InsertParameters.Add("DataType", Session("datatype").ToString)
        'sdsRegistration.InsertParameters.Add("PhotoData2", Session("photodata2").ToString)

        'sdsRegistration.Insert()

        LogInsert() 'renaissDBに登録　ADD_2017/09/29 TOS163


        If Session("payment") <> 0 Then             'a2017/08/03000
            Response.Redirect("Payment.aspx")                     '本番
            'Response.Redirect("ThankyouForPayment.aspx")     'テスト
        End If                                        'a2017/08/03

        'Dim con As New SqlConnection

        ''DB接続
        'con.ConnectionString = ConfigurationManager.ConnectionStrings("OnPremisesDBConnection").ConnectionString
        'con.Open()

        ''===Ｍ会員_FITNESSのINSERT文==========================================================================================================
        ''コース選択処理-------------------------------------------------------------------------------
        ''新しいテーブルの生成ストアドプロシージャ
        'Dim strSql As String = "DECLARE @MAX decimal(8)"
        'strSql = strSql & " SELECT @MAX = MAX(RIGHT(会員ＣＤ, 5)) FROM Ｍ会員_FITNESS WHERE LEFT(会員ＣＤ, 3) = @Head3Num" '会員ＣＤ列の左3桁が@Head3Num(下記のパラメータ設定の11行参照)に等しいものを選択し、その右5桁が最大のものを@MAXに代入
        'strSql = strSql & " SET @MAX ="
        'strSql = strSql & " CASE WHEN @MAX IS NULL THEN '00001'"
        'strSql = strSql & " ELSE @MAX + 1 END"
        'strSql = strSql & " INSERT INTO Ｍ会員_FITNESS (会員ＣＤ, 会員種別, 会員名, 会員カナ名, 誕生日, 性別, 郵便番号, 住所, 住所1, 住所2, 住所3, 住所4, 携帯番号, ＰＣＭＡＩＬ, 入会日, 画像, 会員区分, 職業, 職業ＣＤ, 店舗ＣＤ, 申込日, 基本入金額, 入会金, 登録区分, 支払区分, 審査パスＦ, 現金Ｆ, バス利用Ｆ, 利用区分, 申込担当者ＣＤ, 携帯番号１, 携帯番号２, 携帯番号３)"
        'strSql = strSql & " VALUES (CONVERT(varchar, @Head3Num) + RIGHT('00000' + CONVERT(varchar, @MAX), 5), @会員種別, @会員名, @会員カナ名, @誕生日, @性別, @郵便番号, @住所, @住所1, @住所2, @住所3, @住所4, @携帯番号, @ＰＣＭＡＩＬ, @入会日, @画像, @会員区分, @職業, @職業ＣＤ, @店舗ＣＤ, @申込日, @基本入金額, @入会金, @登録区分, @支払区分, @審査パスＦ, @現金Ｆ, @バス利用Ｆ, @利用区分, @申込担当者ＣＤ, @携帯番号１, @携帯番号２, @携帯番号３)" '@Head3Numと0埋めした@MAXをともに文字列化してから連結"
        ''SQLCommand設定
        'Dim sqlcom As SqlCommand = New SqlCommand(strSql, con)

        ''パラメータ設定

        ''店舗の会員ＣＤ上3桁・システム日付取得
        'Dim con2 As New SqlConnection
        'con2.ConnectionString = ConfigurationManager.ConnectionStrings("OnPremisesDBConnection").ConnectionString

        ''Dim strSql2 As String = "SELECT * FROM Ｍ店舗 WHERE 店舗ＣＤ = @店舗ＣＤ"
        ''Dim sqlcom2 As SqlCommand = New SqlCommand(strSql2, con2)
        ''sqlcom2.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.Char)).Value = Session("ori_facility_id")
        ''Dim dt2 As New DataTable()
        ''Dim sda2 As New SqlDataAdapter(sqlcom2)
        ''sda2.Fill(dt2)
        ''Dim head3Num As String = dt2.Rows(0)("会員ＣＤ上3桁")

        'Dim strSql2 As String = "SELECT M.会員ＣＤ上3桁, S.システム日付 FROM Ｍ店舗 M, Ｍシステム管理 S WHERE M.店舗ＣＤ = @店舗ＣＤ AND S.ＫＥＹ = @店舗ＣＤ"
        'Dim sqlcom2 As SqlCommand = New SqlCommand(strSql2, con2)
        'sqlcom2.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.Char)).Value = Session("ori_facility_id")
        'Dim dt2 As New DataTable()
        'Dim sda2 As New SqlDataAdapter(sqlcom2)
        'sda2.Fill(dt2)
        'Dim head3Num As String = dt2.Rows(0)("会員ＣＤ上3桁")
        'Dim systemDate As String = dt2.Rows(0)("システム日付") 'ADD_2017/10/23 TOS163


        ''消す
        ''Dim head3Num As String = ""
        ''Select Case Session("ori_facility_id")
        ''    Case 1 '越前
        ''        head3Num = "005"
        ''    Case 2 '鯖江
        ''        head3Num = "305"
        ''    Case 4 '敦賀
        ''        head3Num = "605"
        ''    Case 5 '福井南
        ''        head3Num = "705"
        ''End Select

        'sqlcom.Parameters.Add(New SqlParameter("Head3Num", SqlDbType.VarChar)).Value = head3Num

        ''sqlcom.Parameters.Add(New SqlParameter("会員種別", SqlDbType.Int)).Value = 0 'ダミー、あとで置き換え'DEL_2017/10/26 TOS163
        'sqlcom.Parameters.Add(New SqlParameter("会員種別", SqlDbType.Int)).Value = Session("plan_syubetu") 'ADD_2017/10/26 TOS163
        'sqlcom.Parameters.Add(New SqlParameter("会員名", SqlDbType.NVarChar)).Value = Session("name_kanji")
        'sqlcom.Parameters.Add(New SqlParameter("会員カナ名", SqlDbType.NVarChar)).Value = Session("name_kana")
        'sqlcom.Parameters.Add(New SqlParameter("誕生日", SqlDbType.Date)).Value = Session("birthday")
        'If Session("sex").ToString = "男性" Then
        '    sqlcom.Parameters.Add(New SqlParameter("性別", SqlDbType.NVarChar)).Value = "男"
        'Else
        '    sqlcom.Parameters.Add(New SqlParameter("性別", SqlDbType.NVarChar)).Value = "女"
        'End If
        ''sqlcom.Parameters.Add(New SqlParameter("性別", SqlDbType.NVarChar)).Value = Session("sex").ToString.Replace("性", "")
        'sqlcom.Parameters.Add(New SqlParameter("郵便番号", SqlDbType.Char)).Value = Session("zipcode")
        'sqlcom.Parameters.Add(New SqlParameter("住所", SqlDbType.NVarChar)).Value = Session("address")
        'sqlcom.Parameters.Add(New SqlParameter("住所1", SqlDbType.NVarChar)).Value = Session("address0")
        'sqlcom.Parameters.Add(New SqlParameter("住所2", SqlDbType.NVarChar)).Value = Session("address1")
        'sqlcom.Parameters.Add(New SqlParameter("住所3", SqlDbType.NVarChar)).Value = Session("address2")
        'sqlcom.Parameters.Add(New SqlParameter("住所4", SqlDbType.NVarChar)).Value = Session("address3")
        'sqlcom.Parameters.Add(New SqlParameter("携帯番号", SqlDbType.VarChar)).Value = Session("phonenumber")
        'sqlcom.Parameters.Add(New SqlParameter("ＰＣＭＡＩＬ", SqlDbType.VarChar)).Value = Session("email")
        'sqlcom.Parameters.Add(New SqlParameter("入会日", SqlDbType.Date)).Value = Session("date_visit")
        ''sqlcom.Parameters.Add(New SqlParameter("画像", SqlDbType.VarBinary)).Value = Session("photodata") '切り取り前バイナリデータ d2017/08/03_TOS163
        ''sqlcom.Parameters.Add(New SqlParameter("画像", SqlDbType.VarBinary)).Value = Session("photodata3") '切り取り後バイナリデータ a2017/08/03_TOS163 d2017/08/07_TOS163
        'sqlcom.Parameters.Add(New SqlParameter("画像", SqlDbType.VarBinary)).Value = System.Convert.FromBase64String(Session("photodata3")) '切り取り後バイナリデータ a2017/08/07_TOS163
        ''sqlcom.Parameters.Add(New SqlParameter("写真パス", SqlDbType.NVarChar)).Value = Session("photodata2") '切り取り後URL
        'sqlcom.Parameters.Add(New SqlParameter("会員区分", SqlDbType.Int)).Value = 1 'とりあえず1=フィットネスでよいとのこと
        'sqlcom.Parameters.Add(New SqlParameter("職業", SqlDbType.NVarChar)).Value = Session("job")
        'sqlcom.Parameters.Add(New SqlParameter("職業ＣＤ", SqlDbType.Int)).Value = Session("job_cd") 'ダミー、あとで置き換え
        ''消すsqlcom.Parameters.Add(New SqlParameter("店舗名", SqlDbType.NVarChar)).Value = Session("facility_name")
        'sqlcom.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.Int)).Value = Session("ori_facility_id") '本来の店舗ＣＤの方
        ''sqlcom.Parameters.Add(New SqlParameter("申込日", SqlDbType.Char)).Value = Today.ToString("yyyyMMdd")'DEL_2017/10/23 TOS163
        'sqlcom.Parameters.Add(New SqlParameter("申込日", SqlDbType.Char)).Value = systemDate 'ADD_2017/10/23 TOS163
        ''sqlcom.Parameters.Add(New SqlParameter("基本入金額", SqlDbType.Decimal)).Value = CDec(celsum.Text)      'D2017/09/11
        ''sqlcom.Parameters.Add(New SqlParameter("基本入金額", SqlDbType.Decimal)).Value = Session("celsum")       'A2017/09/11 セッション変数に変更 'D2017/09/14
        'sqlcom.Parameters.Add(New SqlParameter("基本入金額", SqlDbType.Decimal)).Value = Session("plan_monthlydues") 'A2017/09/14 セッションの売上単価に変更
        ''sqlcom.Parameters.Add(New SqlParameter("入会金", SqlDbType.Decimal)).Value = Session("facility_admissionfee") 'D2014/09/14
        'sqlcom.Parameters.Add(New SqlParameter("入会金", SqlDbType.Decimal)).Value = Session("celAdmissionFee") 'A2017/09/14
        'sqlcom.Parameters.Add(New SqlParameter("登録区分", SqlDbType.NVarChar)).Value = 2 'Web登録=2
        'sqlcom.Parameters.Add(New SqlParameter("支払区分", SqlDbType.Int)).Value = Session("payment")

        ''ADD_start---2017/09/14 TOS163
        'sqlcom.Parameters.Add(New SqlParameter("審査パスＦ", SqlDbType.Int)).Value = 0
        'sqlcom.Parameters.Add(New SqlParameter("現金Ｆ", SqlDbType.Int)).Value = 0
        'sqlcom.Parameters.Add(New SqlParameter("バス利用Ｆ", SqlDbType.Int)).Value = 0
        'sqlcom.Parameters.Add(New SqlParameter("利用区分", SqlDbType.Int)).Value = 0
        'sqlcom.Parameters.Add(New SqlParameter("申込担当者ＣＤ", SqlDbType.Int)).Value = 2

        'If Mid(Session("phonenumber").ToString, 1, 3) = "010" OrElse
        '    Mid(Session("phonenumber").ToString, 1, 3) = "020" OrElse
        '    Mid(Session("phonenumber").ToString, 1, 3) = "030" OrElse
        '    Mid(Session("phonenumber").ToString, 1, 3) = "040" OrElse
        '    Mid(Session("phonenumber").ToString, 1, 3) = "050" OrElse
        '    Mid(Session("phonenumber").ToString, 1, 3) = "060" OrElse
        '    Mid(Session("phonenumber").ToString, 1, 3) = "070" OrElse
        '    Mid(Session("phonenumber").ToString, 1, 3) = "080" OrElse
        '    Mid(Session("phonenumber").ToString, 1, 3) = "090" Then '携帯の場合
        '    sqlcom.Parameters.Add(New SqlParameter("携帯番号１", SqlDbType.NVarChar)).Value = Mid(Session("phonenumber").ToString, 1, 3)
        '    sqlcom.Parameters.Add(New SqlParameter("携帯番号２", SqlDbType.NVarChar)).Value = Mid(Session("phonenumber").ToString, 4, 4)
        '    sqlcom.Parameters.Add(New SqlParameter("携帯番号３", SqlDbType.NVarChar)).Value = Mid(Session("phonenumber").ToString, 8)
        'Else
        '    sqlcom.Parameters.Add(New SqlParameter("携帯番号１", SqlDbType.NVarChar)).Value = Mid(Session("phonenumber").ToString, 1, 4)
        '    sqlcom.Parameters.Add(New SqlParameter("携帯番号２", SqlDbType.NVarChar)).Value = Mid(Session("phonenumber").ToString, 5, 2)
        '    sqlcom.Parameters.Add(New SqlParameter("携帯番号３", SqlDbType.NVarChar)).Value = Mid(Session("phonenumber").ToString, 7)
        'End If
        ''ADD_end-----2017/09/14 TOS163

        ''sqlcom.Parameters.Add(New SqlParameter("FacilityName", SqlDbType.NVarChar)).Value = Session("facility_name")
        ''sqlcom.Parameters.Add(New SqlParameter("MembershipPlanName", SqlDbType.NVarChar)).Value = Session("plan_Name")
        ''sqlcom.Parameters.Add(New SqlParameter("PlanoptionName", SqlDbType.NVarChar)).Value = Session("option_Name")
        ''sqlcom.Parameters.Add(New SqlParameter("DateVisit", SqlDbType.Date)).Value = Session("date_visit")

        ''sqlcom.Parameters.Add(New SqlParameter("NameKanji", SqlDbType.NVarChar)).Value = Session("name_kanji")
        ''sqlcom.Parameters.Add(New SqlParameter("NameKana", SqlDbType.NVarChar)).Value = Session("name_kana")
        ''sqlcom.Parameters.Add(New SqlParameter("Sex", SqlDbType.NVarChar)).Value = Session("sex")
        ''sqlcom.Parameters.Add(New SqlParameter("BirthDay", SqlDbType.VarChar)).Value = Session("birthday")
        ''sqlcom.Parameters.Add(New SqlParameter("Job", SqlDbType.NVarChar)).Value = Session("job")
        ''sqlcom.Parameters.Add(New SqlParameter("PhoneNumber", SqlDbType.VarChar)).Value = Session("phonenumber")
        ''sqlcom.Parameters.Add(New SqlParameter("ZipCode", SqlDbType.Int)).Value = Session("zipcode")
        ''sqlcom.Parameters.Add(New SqlParameter("Address", SqlDbType.NVarChar)).Value = Session("address")
        ''sqlcom.Parameters.Add(New SqlParameter("Email", SqlDbType.VarChar)).Value = Session("email")
        ''消すsqlcom.Parameters.Add(New SqlParameter("Payment", SqlDbType.NVarChar)).Value = Session("payment")
        ''sqlcom.Parameters.Add(New SqlParameter("PhotoData", SqlDbType.VarBinary)).Value = Session("photodata")
        ''sqlcom.Parameters.Add(New SqlParameter("DataType", SqlDbType.VarChar)).Value = Session("datatype")
        ''sqlcom.Parameters.Add(New SqlParameter("PhotoData2", SqlDbType.NVarChar)).Value = Session("photodata2")

        ''消すIf Session("credit_id").ToString = "0" Then
        ''    sqlcom.Parameters.Add(New SqlParameter("CreditName", SqlDbType.NVarChar)).Value = Session("credit_name")
        ''    sqlcom.Parameters.Add(New SqlParameter("CreditNumber", SqlDbType.BigInt)).Value = Session("credit_number")
        ''    sqlcom.Parameters.Add(New SqlParameter("CreditDate", SqlDbType.NVarChar)).Value = Session("credit_date")
        ''Else
        ''    sqlcom.Parameters.Add(New SqlParameter("CreditName", SqlDbType.NVarChar)).Value = DBNull.Value
        ''    sqlcom.Parameters.Add(New SqlParameter("CreditNumber", SqlDbType.BigInt)).Value = DBNull.Value
        ''    sqlcom.Parameters.Add(New SqlParameter("CreditDate", SqlDbType.NVarChar)).Value = DBNull.Value
        ''End If

        ''sqlcom.Parameters.Add(New SqlParameter("FirstPaymentMoney", SqlDbType.NVarChar)).Value = CDec(celsum.Text)
        ''sqlcom.Parameters.Add(New SqlParameter("RegistrationDate", SqlDbType.NVarChar)).Value = Today

        ''SQLCommand実行
        'Dim result As String
        'result = sqlcom.ExecuteNonQuery()

        ''Dim dt As New DataTable()
        ''Dim sda As New SqlDataAdapter(sqlcom)
        ''sda.Fill(dt)

        ''If Session("payment") <> "0" Then '現金支払いの場合は、Ｔ売上伝票・Ｔ売上明細・Ｔ売上明細詳細にインサートしない

        ''    ===Ｔ売上伝票のINSERT文==========================================================================================================
        ''    strSql = "DECLARE @MAX decimal(8)"
        ''    strSql = strSql & " SELECT @MAX = MAX(RIGHT(伝票ＮＯ, 7)) FROM Ｔ売上伝票 WHERE LEFT(伝票ＮＯ, 1) = 'W'" '伝票ＮＯ列の左1桁が'W'に等しいものを選択し、その右7桁が最大のものを@MAXに代入
        ''    strSql = strSql & " SET @MAX ="
        ''    strSql = strSql & " CASE WHEN @MAX IS NULL THEN '0000001'"
        ''    strSql = strSql & " ELSE @MAX + 1 END"
        ''    strSql = strSql & " INSERT INTO Ｔ売上伝票 (伝票ＮＯ, 伝票日付, 合計金額, 合計税抜金額, 合計消費税額, 登録日, 変更日, 印刷日, 店舗ＣＤ, 登録区分, 支払区分) VALUES ('W' + RIGHT('0000000' + CONVERT(varchar, @MAX), 7), @伝票日付, @合計金額, @合計税抜金額, @合計消費税額, @登録日, @変更日, @印刷日, @店舗ＣＤ, @登録区分, @支払区分)" '"W"と0埋めした@MAXをともに文字列化してから連結
        ''    SQLCommand設定
        ''    sqlcom = New SqlCommand(strSql, con)

        ''    パラメータ設定
        ''    sqlcom.Parameters.Add(New SqlParameter("伝票日付", SqlDbType.Char)).Value = Today.ToString("yyyyMMdd") 'DEl_2017/10/23 TOS163
        ''    sqlcom.Parameters.Add(New SqlParameter("伝票日付", SqlDbType.Char)).Value = systemDate 'ADD_2017/10/23 TOS163
        ''    sqlcom.Parameters.Add(New SqlParameter("合計金額", SqlDbType.Decimal)).Value = CDec(celsum.Text)                                                 'D2017/09/11 
        ''    sqlcom.Parameters.Add(New SqlParameter("合計税抜金額", SqlDbType.Decimal)).Value = Math.Ceiling(CDec(celsum.Text) / 1.08)                        'D2017/09/11
        ''    sqlcom.Parameters.Add(New SqlParameter("合計消費税額", SqlDbType.Decimal)).Value = CDec(celsum.Text) - Math.Ceiling(CDec(celsum.Text) / 1.08)    'D2017/09/11

        ''    sqlcom.Parameters.Add(New SqlParameter("合計金額", SqlDbType.Decimal)).Value = Session("celsum")                                              'A2017/09/11    セッション変数へ変更
        ''    sqlcom.Parameters.Add(New SqlParameter("合計税抜金額", SqlDbType.Decimal)).Value = Math.Ceiling(Session("celsum") / 1.08)                     'A2017/09/11    セッション変数へ変更
        ''    sqlcom.Parameters.Add(New SqlParameter("合計消費税額", SqlDbType.Decimal)).Value = Session("celsum") - Math.Ceiling(Session("celsum") / 1.08) 'A2017/09/11    セッション変数へ変更

        ''    sqlcom.Parameters.Add(New SqlParameter("登録日", SqlDbType.Char)).Value = Today.ToString("yyyyMMdd")
        ''    sqlcom.Parameters.Add(New SqlParameter("変更日", SqlDbType.Char)).Value = Today.ToString("yyyyMMdd")
        ''    sqlcom.Parameters.Add(New SqlParameter("印刷日", SqlDbType.Char)).Value = Today.ToString("yyyyMMdd")
        ''    sqlcom.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.Int)).Value = Session("ori_facility_id") '本来の店舗ＣＤの方
        ''    sqlcom.Parameters.Add(New SqlParameter("登録区分", SqlDbType.NVarChar)).Value = 2 'Web登録=2
        ''    sqlcom.Parameters.Add(New SqlParameter("支払区分", SqlDbType.Int)).Value = Session("payment")

        ''    SQLCommand実行
        ''    result = sqlcom.ExecuteNonQuery()


        ''    ===Ｔ売上明細のINSERT文==========================================================================================================
        ''    strSql = "DECLARE @MAX decimal(8)"
        ''    strSql = strSql & " SELECT @MAX = MAX(RIGHT(伝票ＮＯ, 7)) FROM Ｔ売上伝票 WHERE LEFT(伝票ＮＯ, 1) = 'W'" '伝票ＮＯ列の左1桁が'W'に等しいものを選択し、その右7桁が最大のものを@MAXに代入
        ''    strSql = strSql & " SET @MAX = @MAX"
        ''    strSql = strSql & " INSERT INTO Ｔ売上明細 (伝票ＮＯ, 行ＮＯ, 商品ＣＤ, 商品名, 数量, 仕入単価, 金額, 消費税額, 粗利額, 付け込み区分, 付け込みＦ, 処理区分, 会員区分, 店舗ＣＤ) VALUES ('W' + RIGHT('0000000' + CONVERT(varchar, @MAX), 7), @行ＮＯ, @商品ＣＤ, @商品名,1, 0, @金額, @消費税額, 0, 0, 0, 1, 1, @店舗ＣＤ)" '"W"と0埋めした@MAXをともに文字列化してから連結
        ''    SQLCommand設定
        ''    sqlcom = New SqlCommand(strSql, con)

        ''    パラメータ設定
        ''    sqlcom.Parameters.Add(New SqlParameter("行ＮＯ", SqlDbType.Int)).Value = 1 'ダミー、あとで置き換え
        ''    sqlcom.Parameters.Add(New SqlParameter("商品ＣＤ", SqlDbType.Char)).Value = Session("plan_productcd").ToString
        ''    sqlcom.Parameters.Add(New SqlParameter("商品名", SqlDbType.Char)).Value = Session("plan_name").ToString
        ''    sqlcom.Parameters.Add(New SqlParameter("金額", SqlDbType.Decimal)).Value = CDec(Session("celsum").ToString)
        ''    sqlcom.Parameters.Add(New SqlParameter("消費税額", SqlDbType.Decimal)).Value = CDec(Session("celsum").ToString) - Math.Ceiling(CDec(Session("celsum").ToString) / 1.08)
        ''    sqlcom.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.Int)).Value = Session("ori_facility_id")

        ''    SQLCommand実行
        ''    result = sqlcom.ExecuteNonQuery()


        ''    ===Ｔ売上明細詳細のINSERT文==========================================================================================================
        ''    strSql = "Declare @dMAX Decimal(8), @kMAX Decimal(8)"
        ''    strSql = strSql & " Select @dMAX = MAX(RIGHT(伝票ＮＯ, 7)) FROM Ｔ売上伝票 WHERE LEFT(伝票ＮＯ, 1) = 'W'" '伝票ＮＯ列の左1桁が'W'に等しいものを選択し、その右7桁が最大のものを@dMAXに代入
        ''    strSql = strSql & " SET @dMAX = @dMAX"
        ''    strSql = strSql & " SELECT @kMAX = MAX(RIGHT(会員ＣＤ, 5)) FROM Ｍ会員_FITNESS WHERE LEFT(会員ＣＤ, 3) = @Head3Num" '会員ＣＤ列の左3桁が@Head3Num(下記のパラメータ設定の11行参照)に等しいものを選択し、その右5桁が最大のものを@kMAXに代入
        ''    strSql = strSql & " SET @kMAX = @kMAX"
        ''    strSql = strSql & " INSERT INTO Ｔ売上明細詳細 (伝票ＮＯ, 行ＮＯ, 枝番, 商品ＣＤ, 商品名, 数量, 仕入単価, 金額, 消費税額, 粗利額, 付け込み区分, 付け込みＦ, 処理区分, 会員区分, 店舗ＣＤ, 入金会員ＣＤ, 入金年月, 入金日) VALUES ('W' + RIGHT('0000000' + CONVERT(varchar, @dMAX), 7), @行ＮＯ, 1, @商品ＣＤ, @商品名, 1, 0, @金額, @消費税額, 0, 0, 0, 1, 1, @店舗ＣＤ, CONVERT(varchar, @Head3Num) + RIGHT('00000' + CONVERT(varchar, @kMAX), 5), @入金年月, @入金日)" '"W"と0埋めした@MAXをともに文字列化してから連結
        ''    SQLCommand設定
        ''    sqlcom = New SqlCommand(strSql, con)

        ''    パラメータ設定
        ''    sqlcom.Parameters.Add(New SqlParameter("Head3Num", SqlDbType.VarChar)).Value = head3Num
        ''    sqlcom.Parameters.Add(New SqlParameter("行ＮＯ", SqlDbType.Int)).Value = 1 'ダミー、あとで置き換え
        ''    sqlcom.Parameters.Add(New SqlParameter("商品ＣＤ", SqlDbType.Char)).Value = Session("plan_productcd").ToString
        ''    sqlcom.Parameters.Add(New SqlParameter("商品名", SqlDbType.Char)).Value = Session("plan_name").ToString
        ''    sqlcom.Parameters.Add(New SqlParameter("金額", SqlDbType.Decimal)).Value = CDec(Session("Monthlydues1").ToString)
        ''    sqlcom.Parameters.Add(New SqlParameter("消費税額", SqlDbType.Decimal)).Value = CDec(Session("Monthlydues1").ToString) - Math.Ceiling(CDec(Session("Monthlydues1").ToString) / 1.08)
        ''    sqlcom.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.Int)).Value = Session("ori_facility_id")
        ''    sqlcom.Parameters.Add(New SqlParameter("入金会員ＣＤ", SqlDbType.Int)).Value = Session("ori_facility_id")
        ''    sqlcom.Parameters.Add(New SqlParameter("入金年月", SqlDbType.Int)).Value = Today.ToString("yyyy").ToString & Format(Session("THISM"), "00") 'DEL_2017/10/10 TOS163
        ''    sqlcom.Parameters.Add(New SqlParameter("入金年月", SqlDbType.Int)).Value = Session("THISM").ToString 'ADD_2017/10/10 TOS163
        ''    sqlcom.Parameters.Add(New SqlParameter("入金日", SqlDbType.Int)).Value = Today.ToString("yyyyMMdd") 'DEL_2017/10/23 TOS163
        ''    sqlcom.Parameters.Add(New SqlParameter("入金日", SqlDbType.Int)).Value = systemDate 'ADD_2017/10/23 TOS163

        ''    SQLCommand実行
        ''    result = sqlcom.ExecuteNonQuery()


        ''    strSql = "DECLARE @dMAX decimal(8), @kMAX decimal(8)"
        ''    strSql = strSql & " SELECT @dMAX = MAX(RIGHT(伝票ＮＯ, 7)) FROM Ｔ売上伝票 WHERE LEFT(伝票ＮＯ, 1) = 'W'" '伝票ＮＯ列の左1桁が'W'に等しいものを選択し、その右7桁が最大のものを@dMAXに代入
        ''    strSql = strSql & " SET @dMAX = @dMAX"
        ''    strSql = strSql & " SELECT @kMAX = MAX(RIGHT(会員ＣＤ, 5)) FROM Ｍ会員_FITNESS WHERE LEFT(会員ＣＤ, 3) = @Head3Num" '会員ＣＤ列の左3桁が@Head3Num(下記のパラメータ設定の11行参照)に等しいものを選択し、その右5桁が最大のものを@kMAXに代入
        ''    strSql = strSql & " SET @kMAX = @kMAX"
        ''    strSql = strSql & " INSERT INTO Ｔ売上明細詳細 (伝票ＮＯ, 行ＮＯ, 枝番, 商品ＣＤ, 商品名, 数量, 仕入単価, 金額, 消費税額, 粗利額, 付け込み区分, 付け込みＦ, 処理区分, 会員区分, 店舗ＣＤ, 入金会員ＣＤ, 入金年月, 入金日) VALUES ('W' + RIGHT('0000000' + CONVERT(varchar, @dMAX), 7), @行ＮＯ, 1, @商品ＣＤ, @商品名, 1, 0, @金額, @消費税額, 0, 0, 0, 1, 1, @店舗ＣＤ, CONVERT(varchar, @Head3Num) + RIGHT('00000' + CONVERT(varchar, @kMAX), 5), @入金年月, @入金日)" '"W"と0埋めした@MAXをともに文字列化してから連結
        ''    SQLCommand設定
        ''    sqlcom = New SqlCommand(strSql, con)

        ''    パラメータ設定
        ''    sqlcom.Parameters.Add(New SqlParameter("Head3Num", SqlDbType.VarChar)).Value = head3Num
        ''    sqlcom.Parameters.Add(New SqlParameter("行ＮＯ", SqlDbType.Int)).Value = 2 'ダミー、あとで置き換え
        ''    sqlcom.Parameters.Add(New SqlParameter("商品ＣＤ", SqlDbType.Char)).Value = Session("plan_productcd").ToString
        ''    sqlcom.Parameters.Add(New SqlParameter("商品名", SqlDbType.Char)).Value = Session("plan_name").ToString
        ''    sqlcom.Parameters.Add(New SqlParameter("金額", SqlDbType.Decimal)).Value = CDec(Session("Monthlydues2").ToString)
        ''    sqlcom.Parameters.Add(New SqlParameter("消費税額", SqlDbType.Decimal)).Value = CDec(Session("Monthlydues2").ToString) - Math.Ceiling(CDec(Session("Monthlydues2").ToString) / 1.08)
        ''    sqlcom.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.Int)).Value = Session("ori_facility_id")
        ''    sqlcom.Parameters.Add(New SqlParameter("入金会員ＣＤ", SqlDbType.Int)).Value = Session("ori_facility_id")
        ''    sqlcom.Parameters.Add(New SqlParameter("入金年月", SqlDbType.Int)).Value = Today.ToString("yyyy").ToString & Format(Session("NEXTM"), "00") 'DEL_2017/10/10 TOS163
        ''    sqlcom.Parameters.Add(New SqlParameter("入金年月", SqlDbType.Int)).Value = Session("NEXTM").ToString 'ADD_2017/10/10 TOS163
        ''    sqlcom.Parameters.Add(New SqlParameter("入金日", SqlDbType.Int)).Value = Today.ToString("yyyyMMdd") 'DEL_2017/10/23 TOS163
        ''    sqlcom.Parameters.Add(New SqlParameter("入金日", SqlDbType.Int)).Value = systemDate 'ADD_2017/10/23 TOS163

        ''    SQLCommand実行
        ''    result = sqlcom.ExecuteNonQuery()

        ''End If


        ''===Ｔ入金情報のINSERT文==========================================================================================================
        'strSql = "DECLARE @dMAX decimal(8), @kMAX decimal(8)"
        'strSql = strSql & " SELECT @dMAX = MAX(RIGHT(伝票ＮＯ, 7)) FROM Ｔ売上伝票 WHERE LEFT(伝票ＮＯ, 1) = 'W'"
        'strSql = strSql & " SET @dMAX = @dMAX"
        'strSql = strSql & " SELECT @kMAX = MAX(RIGHT(会員ＣＤ, 5)) FROM Ｍ会員_FITNESS WHERE LEFT(会員ＣＤ, 3) = @Head3Num"
        'strSql = strSql & " SET @kMAX = @kMAX"
        'strSql = strSql & " INSERT INTO Ｔ入金情報 (会員CD, 年月, 基本振替額, 割引合計, 振替額, 入金額, 振替データ作成済Ｆ, レシートＮＯ, 自動作成Ｆ, 水素水会員金額, 入金日, 登録区分)"
        'strSql = strSql & " VALUES (CONVERT(varchar, @Head3Num) + RIGHT('00000' + CONVERT(varchar, @kMAX), 5), @年月, @基本振替額, 0, 0, @入金額, 0"
        ''レシートＮＯ振り分け
        'If Session("payment") <> "0" Then 'ｸﾚｼﾞｯﾄの場合
        '    strSql = strSql & ", 'W' + RIGHT('0000000' + CONVERT(varchar, @dMAX), 7)"
        'Else '現金の場合
        '    strSql = strSql & ", 0"
        'End If
        'strSql = strSql & ", 0, 0, @入金日, 2)"
        ''SQLCommand設定
        'sqlcom = New SqlCommand(strSql, con)

        ''パラメータ設定
        'sqlcom.Parameters.Add(New SqlParameter("Head3Num", SqlDbType.VarChar)).Value = head3Num
        ''sqlcom.Parameters.Add(New SqlParameter("年月", SqlDbType.Int)).Value = Today.ToString("yyyy").ToString & Format(Session("THISM"), "00") 'DEL_2017/10/10 TOS163
        'sqlcom.Parameters.Add(New SqlParameter("年月", SqlDbType.Int)).Value = Session("THISM").ToString 'ADD_2017/10/10 TOS163
        'sqlcom.Parameters.Add(New SqlParameter("基本振替額", SqlDbType.Decimal)).Value = CDec(Session("Monthlydues1").ToString)

        'If Session("payment") <> "0" Then
        '    'ｸﾚｼﾞｯﾄの場合
        '    sqlcom.Parameters.Add(New SqlParameter("入金額", SqlDbType.Decimal)).Value = CDec(Session("Monthlydues1").ToString)
        '    'sqlcom.Parameters.Add(New SqlParameter("入金日", SqlDbType.Int)).Value = Today.ToString("yyyyMMdd") 'DEL_2017/10/23 TOS163
        '    sqlcom.Parameters.Add(New SqlParameter("入金日", SqlDbType.Int)).Value = systemDate 'ADD_2017/10/23 TOS163

        'Else
        '    '現金の場合
        '    sqlcom.Parameters.Add(New SqlParameter("入金額", SqlDbType.Decimal)).Value = 0
        '    sqlcom.Parameters.Add(New SqlParameter("入金日", SqlDbType.NVarChar)).Value = ""

        'End If


        ''SQLCommand実行
        'result = sqlcom.ExecuteNonQuery()

        'strSql = "DECLARE @dMAX decimal(8), @kMAX decimal(8)"
        'strSql = strSql & " SELECT @dMAX = MAX(RIGHT(伝票ＮＯ, 7)) FROM Ｔ売上伝票 WHERE LEFT(伝票ＮＯ, 1) = 'W'"
        'strSql = strSql & " SET @dMAX = @dMAX"
        'strSql = strSql & " SELECT @kMAX = MAX(RIGHT(会員ＣＤ, 5)) FROM Ｍ会員_FITNESS WHERE LEFT(会員ＣＤ, 3) = @Head3Num"
        'strSql = strSql & " SET @kMAX = @kMAX"
        'strSql = strSql & " INSERT INTO Ｔ入金情報 (会員CD, 年月, 基本振替額, 割引合計, 振替額, 入金額, 振替データ作成済Ｆ, レシートＮＯ, 自動作成Ｆ, 水素水会員金額, 入金日, 登録区分)"
        'strSql = strSql & " VALUES (CONVERT(varchar, @Head3Num) + RIGHT('00000' + CONVERT(varchar, @kMAX), 5), @年月, @基本振替額, 0, 0, @入金額, 0"
        ''レシートＮＯ振り分け
        'If Session("payment") <> "0" Then 'ｸﾚｼﾞｯﾄの場合
        '    strSql = strSql & ", 'W' + RIGHT('0000000' + CONVERT(varchar, @dMAX), 7)"
        'Else '現金の場合
        '    strSql = strSql & ", 0"
        'End If
        'strSql = strSql & ", 0, 0, @入金日, 2)"

        ''SQLCommand設定
        'sqlcom = New SqlCommand(strSql, con)

        ''パラメータ設定
        'sqlcom.Parameters.Add(New SqlParameter("Head3Num", SqlDbType.VarChar)).Value = head3Num
        ''sqlcom.Parameters.Add(New SqlParameter("年月", SqlDbType.Int)).Value = Today.ToString("yyyy").ToString & Format(Session("NEXTM"), "00") 'DEL_2017/10/10 TOS163
        'sqlcom.Parameters.Add(New SqlParameter("年月", SqlDbType.Int)).Value = Session("NEXTM").ToString 'ADD_2017/10/10 TOS163
        'sqlcom.Parameters.Add(New SqlParameter("基本振替額", SqlDbType.Decimal)).Value = CDec(Session("Monthlydues2").ToString)

        'If Session("payment") <> "0" Then
        '    'ｸﾚｼﾞｯﾄの場合
        '    sqlcom.Parameters.Add(New SqlParameter("入金額", SqlDbType.Decimal)).Value = CDec(Session("Monthlydues2").ToString)
        '    'sqlcom.Parameters.Add(New SqlParameter("入金日", SqlDbType.Int)).Value = Today.ToString("yyyyMMdd") 'DEL_2017/10/23 TOS163
        '    sqlcom.Parameters.Add(New SqlParameter("入金日", SqlDbType.Int)).Value = systemDate 'ADD_2017/10/23 TOS163

        'Else
        '    '現金の場合
        '    sqlcom.Parameters.Add(New SqlParameter("入金額", SqlDbType.Decimal)).Value = 0
        '    sqlcom.Parameters.Add(New SqlParameter("入金日", SqlDbType.NVarChar)).Value = ""

        'End If



        ''SQLCommand実行
        'result = sqlcom.ExecuteNonQuery()


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

        con.Close()

        Session("membercd") = kainCd

        Send_JIS_Mail()         'A2017/09/11

        'Session.RemoveAll() 'セッションすべて削除→支払処理が完全に終了後に削除

        MasterInsert()  'AzureのⅯ会員_FitnessへINSERT ADD_2017/10/25 TOS163

        Response.Redirect("Thankyou.aspx")        'a2017/08/03


    End Sub

    Private Sub btnReturn1_Click(sender As Object, e As EventArgs) Handles btnReturn1.Click
        Response.Redirect("Registration_1_2.aspx")
    End Sub

    Private Sub btnReturn2_Click(sender As Object, e As EventArgs) Handles btnReturn2.Click
        Response.Redirect("Registration_2_2.aspx")
    End Sub

    Private Sub btnReturn3_Click(sender As Object, e As EventArgs) Handles btnReturn3.Click
        Response.Redirect("Registration_credit_2.aspx")
    End Sub

    Private Function myEncode(ByVal str As String, ByVal enc As System.Text.Encoding) As String
        Dim base64str As String = Convert.ToBase64String(enc.GetBytes(str))
        Return String.Format("=?{0}?B?{1}?=", enc.BodyName, base64str)
    End Function

    Private Sub Send_JIS_Mail()
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
                        Session("email").ToString, myEncode("", myEnc)))
        ' 件名
        msg.Subject = myEncode(myEncode("【スポーツクラブルネッス】お申込みありがとうございます。", myEnc), myEnc)

        ' 本文
        Dim sBody As String =
          Chr(13) & "" & Chr(10) &
          Session("name_kanji").ToString & "様" & Chr(13) & "" & Chr(10) &
          Chr(13) & "" & Chr(10) &
          "この度は、お申込みいただき、" & Chr(13) & "" & Chr(10) &
          "誠にありがとうございます。" & Chr(13) & "" & Chr(10) &
          "ご入会申込を受付しました。" & Chr(13) & "" & Chr(10) &
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
        smtp.Credentials = New NetworkCredential(“renais100”, “ryJ4M7d5”) 'サーバーへのユーザ名とパスワード

        'smtp.Host = "smtp.elle-rose.co.jp" ' SMTPサーバ
        'smtp.Port = "587"
        'smtp.Credentials = New NetworkCredential(“renais100”, “ryJ4M7d5”) 'サーバーへのユーザ名とパスワード

        'smtp.Host = "www.hokutos.co.jp" ' SMTPサーバ
        'smtp.Port = "587"
        'smtp.Credentials = New NetworkCredential(“hokutos10”, “HRtos61”) 'サーバーへのユーザ名とパスワード

        smtp.Send(msg) ' メッセージを送信
    End Sub

    Private Function DailyCalculation() As Integer

        Dim visit As String = Session("date_visit").ToString
        Dim visit_year As Integer = CInt(Mid(visit, 1, 4)) '例：2017/07/31の1文字目から4文字を取得=2017
        Dim visit_month As Integer = CInt(Mid(visit, 6, 2))
        Dim visit_day As Integer = CInt(Mid(visit, 9, 2))
        Dim intDaysinMonth As Integer = DateTime.DaysInMonth(visit_year, visit_month) '来所日の月の日数

        Dim kaihi As Decimal = CInt(Session("plan_monthlydues").ToString) / 1.08

        Dim day_count As Integer = intDaysinMonth - visit_day + 1 '来所日の月の日数 - 来所日 + 1 = その月の利用可能残日数
        Dim zeinuki As Decimal = Math.Round((kaihi * (day_count / intDaysinMonth)), MidpointRounding.AwayFromZero) '日割り計算(税抜き)
        Dim zeikomi As Decimal = Math.Round((zeinuki * 1.08), MidpointRounding.AwayFromZero)

        '消す
        'Select Case visit_month
        '    Case 1, 3, 5, 7, 8, 10, 12
        '        day_count = 31 - visit_day + 1
        '        zeinuki = Math.Round((kaihi * (day_count / 31)), MidpointRounding.AwayFromZero)
        '        zeikomi = Math.Round((zeinuki * 1.08), MidpointRounding.AwayFromZero)

        '    Case 4, 6, 9, 11
        '        day_count = 30 - visit_day + 1
        '        zeinuki = Math.Round((kaihi * (day_count / 30)), MidpointRounding.AwayFromZero)
        '        zeikomi = Math.Round((zeinuki * 1.08), MidpointRounding.AwayFromZero)

        '    Case 2
        '        day_count = 28 - visit_day + 1
        '        zeinuki = Math.Round((kaihi * (day_count / 28)), MidpointRounding.AwayFromZero)
        '        zeikomi = Math.Round((zeinuki * 1.08), MidpointRounding.AwayFromZero)
        'End Select



        Return CInt(zeikomi)

    End Function

    Private Function DailyCalculation2(ByVal Hiwariday As String) As Integer

        Dim visit As String = Hiwariday
        Dim visit_year As Integer = CInt(Mid(visit, 1, 4)) '例：2017/07/31の1文字目から4文字を取得=2017
        Dim visit_month As Integer = CInt(Mid(visit, 6, 2))
        Dim visit_day As Integer = CInt(Mid(visit, 9, 2))
        Dim intDaysinMonth As Integer = DateTime.DaysInMonth(visit_year, visit_month) '来所日の月の日数

        Dim kaihi As Decimal = CInt(Session("plan_monthlydues").ToString) / 1.08

        Dim day_count As Integer = intDaysinMonth - visit_day + 1 '来所日の月の日数 - 来所日 + 1 = その月の利用可能残日数
        Dim zeinuki As Decimal = Math.Round((kaihi * (day_count / intDaysinMonth)), MidpointRounding.AwayFromZero) '日割り計算(税抜き)
        Dim zeikomi As Decimal = Math.Round((zeinuki * 1.08), MidpointRounding.AwayFromZero)

        Return CInt(zeikomi)

    End Function

    Private Sub Campaign()

        Try

            Dim con As New SqlConnection

            ''DB接続
            'con.ConnectionString = "Data Source=hokutos-tester.database.windows.net;Initial Catalog=hokutos-tester;Persist Security Info=True;User ID=Komaikou;Password=Tanstaafl2045"

            ''コース選択処理-------------------------------------------------------------------------------
            ''新しいテーブルの生成ストアドプロシージャ
            'Dim strSql As String = "SELECT * FROM GymCampaignInfo WHERE StoreID = @StoreID And CONVERT(DATE,EndDate) >= CONVERT(date, getdate())"
            ''SQLCommand設定
            'Dim sqlcom As SqlCommand = New SqlCommand(strSql, con)

            ''パラメータ設定
            'sqlcom.Parameters.Add(New SqlParameter("StoreID", SqlDbType.VarChar)).Value = Session("facility_id")

            ''SQLCommand実行
            'Dim dt As New DataTable()
            'Dim sda As New SqlDataAdapter(sqlcom)
            'sda.Fill(dt)

            'DB接続
            con.ConnectionString = ConfigurationManager.ConnectionStrings("renaissDBConnectionString").ConnectionString

            '新しいテーブルの生成ストアドプロシージャ
            Dim strSql As String = "SELECT * FROM Ｍ商品ＷＥＢ WHERE 商品ＣＤ = @商品ＣＤ And 店舗ＣＤ = @店舗ＣＤ And 支払区分 = @支払区分 And 会員区分 = @会員区分 And 開始日 <= @本日 And (終了日 >= @本日 Or 終了日 Is null) ORDER BY 年月"
            'SQLCommand設定
            Dim sqlcom As SqlCommand = New SqlCommand(strSql, con)

            'パラメータ設定
            sqlcom.Parameters.Add(New SqlParameter("店舗ＣＤ", SqlDbType.VarChar)).Value = Session("facility_id")
            sqlcom.Parameters.Add(New SqlParameter("商品ＣＤ", SqlDbType.NVarChar)).Value = Session("plan_productcd").ToString
            sqlcom.Parameters.Add(New SqlParameter("支払区分", SqlDbType.VarChar)).Value = Session("Payment")
            sqlcom.Parameters.Add(New SqlParameter("会員区分", SqlDbType.NVarChar)).Value = "1" 'フィットネス
            Dim localNow2 As DateTime = DateTime.Now
            Dim toUtc2 As DateTime = localNow2.ToUniversalTime()
            toUtc2 = toUtc2.AddHours(9)
            sqlcom.Parameters.Add(New SqlParameter("本日", SqlDbType.DateTime)).Value = toUtc2.ToString("yyyy/MM/dd HH:mm:ss") 'DEL_2018/07/05 TOS163 'ADD_2020/02/06 TOS163
            'sqlcom.Parameters.Add(New SqlParameter("来店日", SqlDbType.DateTime)).Value = Session("date_visit").ToString & " 00:00:00" 'ADD_2018/07/05 TOS163 'DEL_2020/02/06 TOS163

            'SQLCommand実行
            Dim dt As New DataTable()
            Dim sda As New SqlDataAdapter(sqlcom)
            sda.Fill(dt)

            '入会費
            celAdmissionFee.Text = CInt(Session("facility_admissionfee").ToString).ToString("#,0")
            If Session("date_visit") >= STRNDate Then
                celAdmissionFee.Text = CInt(Session("facility_admissionfee2").ToString).ToString("#,0")
            End If

            '登録手数料
            celRegistrationFee.Text = CInt(Session("facility_registrationfee").ToString).ToString("#,0") 'ADD_2017/12/26 TOS163
            If Session("date_visit") >= STRNDate Then
                celRegistrationFee.Text = CInt(Session("facility_registrationfee2").ToString).ToString("#,0")
            End If


            If dt.Rows.Count = 0 Then
                'd2017/09/11---start---いったん削除キャンペーン終了後修正-------------------------------------------------------------------
                ''入会費
                'celAdmissionFee.Text = CInt(Session("facility_admissionfee").ToString).ToString("#,0")
                ''登録手数料
                'celRegistrationFee.Text = CInt(Session("facility_registrationfee").ToString).ToString("#,0")
                ''今月の月会費
                'celMonthlyDues1.Text = DailyCalculation().ToString("#,0")
                'celWaribikiName_M1.Text = "日割り"
                'celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                ''来月の月会費
                'celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                'celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                'd2017/09/11---end---------------------------

                'a2017/09/11---start---暫定処理--------------------------------------------------------

                'DEL_start-------------------------------------------------------------------------------------2018/05/08 TOS163
                '今月の月会費
                'If celMonth1.Text = "03月分" Then

                '    If Session("Payment") <> 0 Then
                '        'Dim month10due As Decimal = CDec(CInt(Session("plan_monthlydues").ToString) / 2) - 500
                '        'celMonthlyDues1.Text = month10due.ToString("#,0")
                '        celMonthlyDues1.Text = 0
                '        celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                '        celWaribikiName_M2.Text = "0円"
                '    Else
                '        'Dim month10due As Decimal = CDec(CInt(Session("plan_monthlydues").ToString) / 2)
                '        Dim month10due As Decimal = CDec(Session("plan_monthlydues").ToString)
                '        celMonthlyDues1.Text = month10due.ToString("#,0")
                '        celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                '    End If

                'celMonthlyDues1.Text = DailyCalculation().ToString("#,0")
                '    celWaribikiName_M1.Text = "日割り"
                '    celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")

                '    '決済金額の再来月分表示 'ADD_2018/03/05 TOS163
                '    celMonthlyDues3.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                '    celMonthlyDues3_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                'Else
                '    celMonthlyDues2.Text = CDec(CInt(Session("plan_monthlydues").ToString))
                '    celWaribikiName_M2.Text = ""
                '    celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                'End If


                'If celMonth2.Text = "12月分" Then
                '来月の月会費
                'If Session("Payment") <> 0 Then
                '    Dim month10due As Decimal = CDec(CInt(Session("plan_monthlydues").ToString)) - 500
                '    celMonthlyDues2.Text = month10due.ToString("#,0")
                '    celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")

                '    celWaribikiName_M2.Text = "-500円引き"
                'Else
                'Dim month10due As Decimal = CDec(CInt(Session("plan_monthlydues").ToString))
                'celMonthlyDues2.Text = month10due.ToString("#,0")
                'celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                'End If
                'A2017/09/11---END---------------------------

                'Else
                '    '来月の月会費
                '    'celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                '    celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                '    Dim month1due As Decimal = CDec(CInt(Session("plan_monthlydues_t").ToString)) - 3000
                '    Dim Zei As Decimal = month1due * 0.08
                '    month1due = month1due + Zei
                '    celMonthlyDues2.Text = month1due.ToString("#,0")
                '    celWaribikiName_M2.Text = "月会費-3000円引き"
                'End If

                'a2017/09/11---end----------------------------------------


                ''ADD_start----------------------------------------------------2018/03/08 TOS163
                'If Mid(Session("date_visit").ToString, 9, 2) = "01" Then '来店日が1日だった場合
                '    '今月の月会費
                '    celMonthlyDues1.Text = CDec(CInt(Session("Campaign_monthlydues").ToString))
                '    celWaribikiName_M1.Text = "ｷｬﾝﾍﾟｰﾝ"
                '    celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                '    '来月の月会費
                '    celMonthlyDues2.Text = CDec(CInt(Session("Campaign_monthlydues").ToString))
                '    celWaribikiName_M2.Text = "ｷｬﾝﾍﾟｰﾝ"
                '    celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                '    '再来月の月会費（非表示）
                '    celMonthlyDues3.Text = 0
                '    celMonthlyDues3_O.Text = 0
                '    Session("NEXTNEXTM") = 0
                'Else
                '    '今月の月会費
                '    celMonthlyDues1.Text = CDec(CInt(Session("Campaign_monthlydues").ToString))
                '    celWaribikiName_M1.Text = "ｷｬﾝﾍﾟｰﾝ"
                '    celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                '    '来月の月会費
                '    celMonthlyDues2.Text = CDec(CInt(Session("Campaign_monthlydues").ToString))
                '    celWaribikiName_M2.Text = "ｷｬﾝﾍﾟｰﾝ"
                '    celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                '    '再来月の月会費
                '    Dim day As String = Session("date_visit").ToString
                '    Dim addMonth As DateTime = CDate(day).AddMonths(2)
                '    Dim straddMonth As String = addMonth.ToString("yyyy/MM/dd")
                '    celMonthlyDues3.Text = DailyCalculation2(straddMonth).ToString("#,0")
                '    celWaribikiName_M3.Text = "日割り"
                '    celMonthlyDues3_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")

                '    cel3.Visible = True
                'End If
                ''ADD_end------------------------------------------------------2018/03/08 TOS163
                'DEL_end---------------------------------------------------------------------------------------2018/05/08 TOS163

                'ADD_start----------------------------------------------------2018/03/31 TOS163
                Dim localNow As DateTime = DateTime.Now
                Dim toUtc As DateTime = localNow.ToUniversalTime()
                toUtc = toUtc.AddHours(9)
                Dim HonzituMonth As String = toUtc.ToString("MM")
                Dim HonzituDay As String = toUtc.ToString("dd")

                '通常時（ｷｬﾝﾍﾟｰﾝなし）
                If CInt(HonzituMonth) = CInt(Mid(Session("date_visit").ToString, 6, 2)) Then '来店日の月が今月の場合

                    If CInt(HonzituDay) < 99 Then 'WEB登録日が1日～10までの場合
                        '今月の月会費
                        celMonthlyDues1.Text = DailyCalculation().ToString("#,0")
                        celWaribikiName_M1.Text = "日割り"
                        celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                        '来月の月会費
                        '                        celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")      'D2019/08/31
                        celMonthlyDues2.Text = CInt(Session("plan_monthlydues2").ToString).ToString("#,0")                              'A2019/08/31
                        celWaribikiName_M2.Text = ""
                        celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                        '再来月の月会費(非表示)
                        celMonthlyDues3.Text = 0
                        celWaribikiName_M3.Text = ""
                        celMonthlyDues3_O.Text = 0
                        Session("NEXTNEXTM") = 0
                    Else
                        '今月の月会費
                        celMonthlyDues1.Text = DailyCalculation().ToString("#,0")
                        celWaribikiName_M1.Text = "日割り"
                        celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                        '来月の月会費
                        '                        celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")          'D2019/08/31
                        celMonthlyDues2.Text = CInt(Session("plan_monthlydues2").ToString).ToString("#,0")                                  'A2019/08/31
                        celWaribikiName_M2.Text = ""
                        celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                        '再来月の月会費
                        cel3.Visible = True
                        '                        celMonthlyDues3.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")          'D2019/08/31
                        celMonthlyDues3.Text = CInt(Session("plan_monthlydues2").ToString).ToString("#,0")                                  'A2019/08/31
                        celWaribikiName_M3.Text = ""
                        celMonthlyDues3_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                    End If

                Else
                    '来店日の月が翌日の場合は1日付の計算となる
                    '今月の月会費
                    celMonthlyDues1.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                    celWaribikiName_M1.Text = ""
                    celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                    '来月の月会費
                    celMonthlyDues2.Text = CInt(Session("plan_monthlydues2").ToString).ToString("#,0")
                    celWaribikiName_M2.Text = ""
                    celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                    '再来月の月会費(非表示)
                    celMonthlyDues3.Text = 0
                    celWaribikiName_M3.Text = ""
                    celMonthlyDues3_O.Text = 0
                    Session("NEXTNEXTM") = 0
                End If
                'ADD_end------------------------------------------------------2018/03/31 TOS163

            Else
                'DEL_start-------------------------------------------------------------------------------------2018/05/08 TOS163
                '入会費
                'If dt.Rows(0)("AdmissionFee").ToString = "" Then
                'celAdmissionFee.Text = CInt(Session("facility_admissionfee").ToString).ToString("#,0")
                'Else
                '    celAdmissionFee.Text = CInt(dt.Rows(0)("AdmissionFee")).ToString("#,0")
                '    celWaribikiName_A.Text = dt.Rows(0)("CampaignName").ToString
                'End If
                '登録手数料
                'If dt.Rows(0)("RegistrationFee").ToString = "" Then
                'celRegistrationFee.Text = CInt(Session("facility_registrationfee").ToString).ToString("#,0")
                'Else
                '    celRegistrationFee.Text = CInt(dt.Rows(0)("RegistrationFee")).ToString("#,0")
                '    celWaribikiName_R.Text = dt.Rows(0)("CampaignName").ToString
                'End If
                ''今月の月会費
                'If dt.Rows(0)("MonthlyDues").ToString = "" Then
                '    celMonthlyDues1.Text = DailyCalculation().ToString("#,0")
                '    celWaribikiName_M1.Text = "日割り"
                'Else
                '    celMonthlyDues1.Text = CInt(dt.Rows(0)("MonthlyDues")).ToString("#,0")
                '    celWaribikiName_M1.Text = dt.Rows(0)("CampaignName").ToString
                'End If
                ''今月の月会費(オプション)
                'If dt.Rows(0)("MonthlyDues_O").ToString = "" Then
                '    celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                'Else
                '    celMonthlyDues1_O.Text = CInt(dt.Rows(0)("MonthlyDues_O")).ToString("#,0")
                '    celWaribikiName_O.Text = dt.Rows(0)("CampaignName").ToString
                'End If

                'If dt.Rows(0)("Months") = 2 Then
                '    '来月の月会費
                '    If dt.Rows(0)("MonthlyDues").ToString <> "" Then
                '        celMonthlyDues2.Text = CInt(dt.Rows(0)("MonthlyDues")).ToString("#,0")
                '        celWaribikiName_M2.Text = dt.Rows(0)("CampaignName").ToString
                '    Else
                '        celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                '    End If
                '    '来月の月会費(オプション)
                '    If dt.Rows(0)("MonthlyDues_O").ToString <> "" Then
                '        celMonthlyDues2_O.Text = CInt(dt.Rows(0)("MonthlyDues_O")).ToString("#,0")
                '        celWaribikiName_O2.Text = dt.Rows(0)("CampaignName").ToString
                '    Else
                '        celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                '    End If
                'Else
                '    celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                '    celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                'End If
                'DEL_end---------------------------------------------------------------------------------------2018/05/08 TOS163

                'ADD_start-------------------------------------------------------------------------------------2018/05/24 TOS163
                Dim localNow As DateTime = DateTime.Now
                Dim toUtc As DateTime = localNow.ToUniversalTime()
                toUtc = toUtc.AddHours(9)
                Dim HonzituMonth As String = toUtc.ToString("MM")
                Dim HonzituDay As String = toUtc.ToString("dd")
                Dim NextMonth As String = toUtc.AddMonths(1).ToString("MM") '翌月

                Dim kaisiday As DateTime = CDate(dt.Rows(0).Item(10).ToString)
                Dim kaisiMonth As String = kaisiday.ToString("MM") '開始日の月
                Dim kaisiNextMonth As String = kaisiday.AddMonths(1).ToString("MM") '開始日の翌月

                Select Case dt.Rows(0).Item(13).ToString
                    Case "1" '最初の２カ月まとめて○○円
                        Session("Campaign_monthlydues1") = dt.Rows(0).Item(1).ToString
                        Session("Campaign_monthlydues2") = dt.Rows(1).Item(1).ToString
                        Session("Campaign_name") = "ｷｬﾝﾍﾟｰﾝ価格"

                        If CInt(HonzituMonth) = CInt(Mid(Session("date_visit").ToString, 6, 2)) Then '来店日の月が今月の場合

                            If CInt(HonzituDay) < 99 Then 'WEB登録日が1日～10日だった場合
                                '今月の月会費
                                celMonthlyDues1.Text = CInt(Session("Campaign_monthlydues1").ToString).ToString("#,0")
                                celWaribikiName_M1.Text = Session("Campaign_name").ToString
                                celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '来月の月会費
                                celMonthlyDues2.Text = CInt(Session("Campaign_monthlydues2").ToString).ToString("#,0")
                                celWaribikiName_M2.Text = Session("Campaign_name").ToString
                                celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '再来月の月会費（非表示）
                                celMonthlyDues3.Text = 0
                                celMonthlyDues3_O.Text = 0
                                Session("NEXTNEXTM") = 0
                            Else
                                '今月の月会費
                                celMonthlyDues1.Text = CInt(Session("Campaign_monthlydues1").ToString).ToString("#,0")
                                celWaribikiName_M1.Text = Session("Campaign_name").ToString
                                celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '来月の月会費
                                celMonthlyDues2.Text = CInt(Session("Campaign_monthlydues2").ToString).ToString("#,0")
                                celWaribikiName_M2.Text = Session("Campaign_name").ToString
                                celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '再来月の月会費
                                If dt.Rows(0).Item(14).ToString = "3" Then 'ADD_2018/06/26 TOS163
                                    '表示
                                    Dim day As String = Session("date_visit").ToString
                                    Dim addMonth As DateTime = CDate(day).AddMonths(2)
                                    Dim straddMonth As String = addMonth.ToString("yyyy/MM/dd")
                                    celMonthlyDues3.Text = DailyCalculation2(straddMonth).ToString("#,0")
                                    celWaribikiName_M3.Text = "日割り"
                                    celMonthlyDues3_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                    cel3.Visible = True
                                Else
                                    '非表示
                                    celMonthlyDues3.Text = 0
                                    celMonthlyDues3_O.Text = 0
                                    Session("NEXTNEXTM") = 0
                                End If
                            End If

                        Else
                            '来店日の月が翌日の場合は1日付の計算となる
                            '今月の月会費
                            celMonthlyDues1.Text = CInt(Session("Campaign_monthlydues1").ToString).ToString("#,0")
                            celWaribikiName_M1.Text = Session("Campaign_name").ToString
                            celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                            '来月の月会費
                            celMonthlyDues2.Text = CInt(Session("Campaign_monthlydues2").ToString).ToString("#,0")
                            celWaribikiName_M2.Text = Session("Campaign_name").ToString
                            celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                            '再来月の月会費(非表示)
                            celMonthlyDues3.Text = 0
                            celWaribikiName_M3.Text = ""
                            celMonthlyDues3_O.Text = 0
                            Session("NEXTNEXTM") = 0

                        End If


                    Case "2" '初月分0円
                        Session("Campaign_monthlydues1") = dt.Rows(0).Item(1).ToString
                        Session("Campaign_name") = "0円"

                        If CInt(HonzituMonth) = CInt(Mid(Session("date_visit").ToString, 6, 2)) Then '来店日の月が今月の場合

                            If CInt(HonzituDay) < 99 Then 'WEB登録日が1日～10日だった場合
                                '今月の月会費
                                celMonthlyDues1.Text = CInt(Session("Campaign_monthlydues1").ToString).ToString("#,0")
                                celWaribikiName_M1.Text = Session("Campaign_name").ToString
                                celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '来月の月会費
                                celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                                celWaribikiName_M2.Text = ""
                                celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '再来月の月会費（非表示）
                                celMonthlyDues3.Text = 0
                                celMonthlyDues3_O.Text = 0
                                Session("NEXTNEXTM") = 0
                            Else
                                '今月の月会費
                                celMonthlyDues1.Text = CInt(Session("Campaign_monthlydues1").ToString).ToString("#,0")
                                celWaribikiName_M1.Text = Session("Campaign_name").ToString
                                celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '来月の月会費
                                celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                                celWaribikiName_M2.Text = ""
                                celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '再来月の月会費
                                If dt.Rows(0).Item(14).ToString = "3" Then 'ADD_2018/06/26 TOS163
                                    '表示
                                    celMonthlyDues3.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                                    celWaribikiName_M3.Text = ""
                                    celMonthlyDues3_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                    cel3.Visible = True
                                Else
                                    '非表示
                                    celMonthlyDues3.Text = 0
                                    celMonthlyDues3_O.Text = 0
                                    Session("NEXTNEXTM") = 0
                                End If
                            End If

                        Else
                            '来店日の月が翌日の場合は1日付の計算となる
                            '今月の月会費
                            celMonthlyDues1.Text = CInt(Session("Campaign_monthlydues1").ToString).ToString("#,0")
                            celWaribikiName_M1.Text = Session("Campaign_name").ToString
                            celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                            '来月の月会費
                            celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                            celWaribikiName_M2.Text = ""
                            celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                            '再来月の月会費(非表示)
                            celMonthlyDues3.Text = 0
                            celWaribikiName_M3.Text = ""
                            celMonthlyDues3_O.Text = 0
                            Session("NEXTNEXTM") = 0

                        End If


                    Case "3" '最初の1ヶ月0円
                        Session("Campaign_monthlydues1") = dt.Rows(0).Item(1).ToString
                        Session("Campaign_name") = "0円"

                        If CInt(HonzituMonth) = CInt(Mid(Session("date_visit").ToString, 6, 2)) Then '来店日の月が今月の場合

                            If CInt(HonzituDay) < 99 Then 'WEB登録日が1日～10日だった場合
                                '今月の月会費
                                celMonthlyDues1.Text = CInt(Session("Campaign_monthlydues1").ToString).ToString("#,0")
                                celWaribikiName_M1.Text = Session("Campaign_name").ToString
                                celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '来月の月会費
                                Dim day As String = Session("date_visit").ToString
                                Dim addMonth As DateTime = CDate(day).AddMonths(1)
                                Dim straddMonth As String = addMonth.ToString("yyyy/MM/dd")
                                celMonthlyDues2.Text = DailyCalculation2(straddMonth).ToString("#,0")
                                celWaribikiName_M2.Text = "日割り"
                                celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '再来月の月会費（非表示）
                                celMonthlyDues3.Text = 0
                                celMonthlyDues3_O.Text = 0
                                Session("NEXTNEXTM") = 0
                            Else
                                '今月の月会費
                                celMonthlyDues1.Text = CDec(CInt(Session("Campaign_monthlydues1").ToString))
                                celWaribikiName_M1.Text = Session("Campaign_name").ToString
                                celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                If dt.Rows(0).Item(14).ToString = "3" Then 'ADD_2018/06/26 TOS163
                                    If dt.Rows(0).Item(15).ToString = "翌月度" Then
                                        '来月の月会費
                                        Dim day As String = Session("date_visit").ToString
                                        Dim addMonth As DateTime = CDate(day).AddMonths(1)
                                        Dim straddMonth As String = addMonth.ToString("yyyy/MM/dd")
                                        celMonthlyDues2.Text = DailyCalculation2(straddMonth).ToString("#,0")
                                        celWaribikiName_M2.Text = "日割り"
                                        celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                        '再来月の月会費
                                        celMonthlyDues3.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                                        celWaribikiName_M3.Text = ""
                                        celMonthlyDues3_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                        cel3.Visible = True
                                    Else
                                        '来月の月会費
                                        celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                                        celWaribikiName_M2.Text = ""
                                        celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                        '再来月の月会費
                                        Dim day As String = Session("date_visit").ToString
                                        Dim addMonth As DateTime = CDate(day).AddMonths(2)
                                        Dim straddMonth As String = addMonth.ToString("yyyy/MM/dd")
                                        celMonthlyDues3.Text = DailyCalculation2(straddMonth).ToString("#,0")
                                        celWaribikiName_M3.Text = "日割り"
                                        celMonthlyDues3_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                        cel3.Visible = True
                                    End If
                                Else
                                    '来月の月会費
                                    Dim day As String = Session("date_visit").ToString
                                    Dim addMonth As DateTime = CDate(day).AddMonths(1)
                                    Dim straddMonth As String = addMonth.ToString("yyyy/MM/dd")
                                    celMonthlyDues2.Text = DailyCalculation2(straddMonth).ToString("#,0")
                                    celWaribikiName_M2.Text = "日割り"
                                    celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                    '再来月の月会費(非表示)
                                    celMonthlyDues3.Text = 0
                                    celMonthlyDues3_O.Text = 0
                                    Session("NEXTNEXTM") = 0
                                End If
                            End If

                        Else
                            '来店日の月が翌日の場合は1日付の計算となる
                            '今月の月会費
                            celMonthlyDues1.Text = CInt(Session("Campaign_monthlydues1").ToString).ToString("#,0")
                            celWaribikiName_M1.Text = Session("Campaign_name").ToString
                            celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                            '来月の月会費
                            celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                            celWaribikiName_M2.Text = ""
                            celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                            '再来月の月会費（非表示）
                            celMonthlyDues3.Text = 0
                            celMonthlyDues3_O.Text = 0
                            Session("NEXTNEXTM") = 0

                        End If


                    Case "4" '〇月分と〇月分0円
                        Session("Campaign_monthlydues1") = dt.Rows(0).Item(1).ToString
                        Session("Campaign_monthlydues2") = dt.Rows(1).Item(1).ToString
                        Session("Campaign_name") = "0円"

                        If CInt(kaisiMonth) = CInt(Mid(Session("date_visit").ToString, 6, 2)) Then '来店日の月が開始日月の場合
                            If CInt(HonzituDay) < 99 Then 'WEB登録日が1日～10日だった場合
                                '今月の月会費
                                celMonthlyDues1.Text = CInt(Session("Campaign_monthlydues1").ToString).ToString("#,0")
                                celWaribikiName_M1.Text = Session("Campaign_name").ToString
                                celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '来月の月会費
                                celMonthlyDues2.Text = CInt(Session("Campaign_monthlydues2").ToString).ToString("#,0")
                                celWaribikiName_M2.Text = Session("Campaign_name").ToString
                                celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '再来月の月会費（非表示）
                                celMonthlyDues3.Text = 0
                                celMonthlyDues3_O.Text = 0
                                Session("NEXTNEXTM") = 0
                            Else
                                '今月の月会費
                                celMonthlyDues1.Text = CDec(CInt(Session("Campaign_monthlydues1").ToString))
                                celWaribikiName_M1.Text = Session("Campaign_name").ToString
                                celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '来月の月会費
                                celMonthlyDues2.Text = CInt(Session("Campaign_monthlydues2").ToString).ToString("#,0")
                                celWaribikiName_M2.Text = Session("Campaign_name").ToString
                                celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '再来月の月会費
                                If dt.Rows(0).Item(14).ToString = "3" Then 'ADD_2018/06/26 TOS163
                                    '表示
                                    celMonthlyDues3.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                                    celWaribikiName_M3.Text = ""
                                    celMonthlyDues3_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                    cel3.Visible = True
                                Else
                                    '非表示
                                    celMonthlyDues3.Text = 0
                                    celMonthlyDues3_O.Text = 0
                                    Session("NEXTNEXTM") = 0
                                End If
                            End If

                        ElseIf CInt(kaisiNextMonth) = CInt(Mid(Session("date_visit").ToString, 6, 2)) Then '来店日の月が開始日翌月の場合
                            If CInt(HonzituDay) < 99 Then 'WEB登録日が1日～10日だった場合
                                '今月の月会費
                                celMonthlyDues1.Text = CInt(Session("Campaign_monthlydues1").ToString).ToString("#,0")
                                celWaribikiName_M1.Text = Session("Campaign_name").ToString
                                celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '来月の月会費
                                celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                                celWaribikiName_M2.Text = ""
                                celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '再来月の月会費（非表示）
                                celMonthlyDues3.Text = 0
                                celMonthlyDues3_O.Text = 0
                                Session("NEXTNEXTM") = 0
                            Else
                                '今月の月会費
                                celMonthlyDues1.Text = CDec(CInt(Session("Campaign_monthlydues1").ToString))
                                celWaribikiName_M1.Text = Session("Campaign_name").ToString
                                celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '来月の月会費
                                celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                                celWaribikiName_M2.Text = ""
                                celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '再来月の月会費
                                If dt.Rows(0).Item(14).ToString = "3" Then 'ADD_2018/06/26 TOS163
                                    '表示
                                    celMonthlyDues3.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                                    celWaribikiName_M3.Text = ""
                                    celMonthlyDues3_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                    cel3.Visible = True
                                Else
                                    '非表示
                                    celMonthlyDues3.Text = 0
                                    celMonthlyDues3_O.Text = 0
                                    Session("NEXTNEXTM") = 0
                                End If
                            End If

                        Else
                            'キャンペーン期間で申込しても、来店日が範囲内外であれば通常料金
                            '今月の月会費
                            celMonthlyDues1.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                            celWaribikiName_M1.Text = ""
                            celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                            '来月の月会費
                            celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                            celWaribikiName_M2.Text = ""
                            celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                            '再来月の月会費(非表示)
                            celMonthlyDues3.Text = 0
                            celWaribikiName_M3.Text = ""
                            celMonthlyDues3_O.Text = 0
                            Session("NEXTNEXTM") = 0

                        End If

                    Case "5" '〇月分0円
                        Session("Campaign_monthlydues2") = dt.Rows(0).Item(1).ToString
                        Session("Campaign_name") = "0円"

                        If CInt(kaisiMonth) = CInt(Mid(Session("date_visit").ToString, 6, 2)) Then '来店日の月が開始日月の場合
                            If CInt(HonzituDay) < 99 Then 'WEB登録日が1日～10日だった場合
                                '今月の月会費
                                celMonthlyDues1.Text = DailyCalculation().ToString("#,0")
                                celWaribikiName_M1.Text = "日割り"
                                celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '来月の月会費
                                celMonthlyDues2.Text = CInt(Session("Campaign_monthlydues2").ToString).ToString("#,0")
                                celWaribikiName_M2.Text = Session("Campaign_name").ToString
                                celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                '再来月の月会費（非表示）
                                celMonthlyDues3.Text = 0
                                celMonthlyDues3_O.Text = 0
                                Session("NEXTNEXTM") = 0
                            Else
                                If dt.Rows(0).Item(14).ToString = "3" Then 'ADD_2018/06/26 TOS163
                                    If dt.Rows(0).Item(15).ToString = "初月度" Then
                                        '今月の月会費
                                        celMonthlyDues1.Text = DailyCalculation().ToString("#,0")
                                        celWaribikiName_M1.Text = "日割り"
                                        celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                        '来月の月会費
                                        celMonthlyDues2.Text = CInt(Session("Campaign_monthlydues2").ToString).ToString("#,0")
                                        celWaribikiName_M2.Text = Session("Campaign_name").ToString
                                        celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                        '再来月の月会費
                                        celMonthlyDues3.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                                        celWaribikiName_M3.Text = ""
                                        celMonthlyDues3_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                        cel3.Visible = True
                                    Else
                                        '今月の月会費
                                        celMonthlyDues1.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                                        celWaribikiName_M1.Text = ""
                                        celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                        '来月の月会費
                                        celMonthlyDues2.Text = CInt(Session("Campaign_monthlydues2").ToString).ToString("#,0")
                                        celWaribikiName_M2.Text = Session("Campaign_name").ToString
                                        celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                        '再来月の月会費
                                        Dim day As String = Session("date_visit").ToString
                                        Dim addMonth As DateTime = CDate(day).AddMonths(2)
                                        Dim straddMonth As String = addMonth.ToString("yyyy/MM/dd")
                                        celMonthlyDues3.Text = DailyCalculation2(straddMonth).ToString("#,0")
                                        celWaribikiName_M3.Text = "日割り"
                                        celMonthlyDues3_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                        cel3.Visible = True
                                    End If
                                Else
                                    '今月の月会費
                                    celMonthlyDues1.Text = DailyCalculation().ToString("#,0")
                                    celWaribikiName_M1.Text = "日割り"
                                    celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                    '来月の月会費
                                    celMonthlyDues2.Text = CInt(Session("Campaign_monthlydues2").ToString).ToString("#,0")
                                    celWaribikiName_M2.Text = Session("Campaign_name").ToString
                                    celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                                    '再来月の月会費
                                    celMonthlyDues3.Text = 0
                                    celMonthlyDues3_O.Text = 0
                                    Session("NEXTNEXTM") = 0
                                End If
                            End If

                            'DEL_start-------------------------2020/02/14 TOS163
                            'ElseIf CInt(kaisiNextMonth) = CInt(Mid(Session("date_visit").ToString, 6, 2)) Then '来店日の月が開始日翌月の場合
                            '    If CInt(HonzituDay) < 99 Then 'WEB登録日が1日～10日だった場合
                            '        '今月の月会費
                            '        celMonthlyDues1.Text = CInt(Session("Campaign_monthlydues2").ToString).ToString("#,0")
                            '        celWaribikiName_M1.Text = Session("Campaign_name").ToString
                            '        celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                            '        '来月の月会費
                            '        celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                            '        celWaribikiName_M2.Text = ""
                            '        celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                            '        '再来月の月会費（非表示）
                            '        celMonthlyDues3.Text = 0
                            '        celMonthlyDues3_O.Text = 0
                            '        Session("NEXTNEXTM") = 0
                            '    Else
                            '        '今月の月会費
                            '        celMonthlyDues1.Text = CDec(CInt(Session("Campaign_monthlydues2").ToString))
                            '            celWaribikiName_M1.Text = Session("Campaign_name").ToString
                            '            celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                            '            '来月の月会費
                            '            celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                            '            celWaribikiName_M2.Text = ""
                            '            celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                            '        '再来月の月会費
                            '        If dt.Rows(0).Item(14).ToString = "3" Then 'ADD_2018/06/26 TOS163
                            '            '表示
                            '            celMonthlyDues3.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                            '            celWaribikiName_M3.Text = ""
                            '            celMonthlyDues3_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                            '            cel3.Visible = True
                            '        Else
                            '            '非表示
                            '            celMonthlyDues3.Text = 0
                            '            celMonthlyDues3_O.Text = 0
                            '            Session("NEXTNEXTM") = 0
                            '        End If
                            '    End If
                            'DEL_end---------------------------2020/02/14 TOS163

                        Else
                            'キャンペーン期間で申込しても、来店日が範囲内外であれば通常料金
                            '今月の月会費
                            celMonthlyDues1.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                            celWaribikiName_M1.Text = ""
                            celMonthlyDues1_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                            '来月の月会費
                            celMonthlyDues2.Text = CInt(Session("plan_monthlydues").ToString).ToString("#,0")
                            celWaribikiName_M2.Text = ""
                            celMonthlyDues2_O.Text = CInt(Session("option_monthlydues").ToString).ToString("#,0")
                            '再来月の月会費(非表示)
                            celMonthlyDues3.Text = 0
                            celWaribikiName_M3.Text = ""
                            celMonthlyDues3_O.Text = 0
                            Session("NEXTNEXTM") = 0

                        End If

                End Select
                'ADD_end---------------------------------------------------------------------------------------2018/05/24 TOS163

            End If
            'celMonthlyDues1.Text = 10       'テスト
            'celMonthlyDues2.Text = 10       'テスト

            Session("Monthlydues1") = celMonthlyDues1.Text      'a2017/09/11
            Session("Monthlydues2") = celMonthlyDues2.Text      'a2017/09/11
            Session("Monthlydues3") = celMonthlyDues3.Text      'a2018/03/05 TOS163

            Session("celAdmissionFee") = celAdmissionFee.Text 'ADD_2017/09/27 TOS163

        Catch ex As Exception
            MsgBox("システムエラーが起きました。")
            Exit Sub
        End Try


    End Sub

    Private Sub LogInsert() 'ADD_2017/09/29 TOS163

        Dim con As New SqlConnection

        'DB接続
        con.ConnectionString = ConfigurationManager.ConnectionStrings("renaissDBConnectionString").ConnectionString
        con.Open()

        '===Ｍ会員_FITNESSのINSERT文==========================================================================================================
        'コース選択処理-------------------------------------------------------------------------------
        '新しいテーブルの生成ストアドプロシージャ
        'Dim strSql As String = "DECLARE @MAX decimal(8)"
        'strSql = strSql & " SELECT @MAX = MAX(RIGHT(会員ＣＤ, 5)) FROM LOG_Ｍ会員_FITNESS WHERE LEFT(会員ＣＤ, 3) = @Head3Num" '会員ＣＤ列の左3桁が@Head3Num(下記のパラメータ設定の11行参照)に等しいものを選択し、その右5桁が最大のものを@MAXに代入
        'strSql = strSql & " SET @MAX ="
        'strSql = strSql & " CASE WHEN @MAX IS NULL THEN '00001'"
        'strSql = strSql & " ELSE @MAX + 1 END"
        Dim strSql As String = ""
        strSql = strSql & " INSERT INTO LOG_Ｍ会員_FITNESS (会員種別, 会員名, 会員カナ名, 誕生日, 性別, 郵便番号, 住所, 住所1, 住所2, 住所3, 住所4, 携帯番号, ＰＣＭＡＩＬ, 入会日, 画像, 会員区分, 職業, 職業ＣＤ, 店舗ＣＤ, 申込日, 基本入金額, 入会金, 登録区分, 支払区分, 審査パスＦ, 現金Ｆ, バス利用Ｆ, 利用区分, 申込担当者ＣＤ, 携帯番号１, 携帯番号２, 携帯番号３, Timestamp)"
        strSql = strSql & " VALUES (@会員種別, @会員名, @会員カナ名, @誕生日, @性別, @郵便番号, @住所, @住所1, @住所2, @住所3, @住所4, @携帯番号, @ＰＣＭＡＩＬ, @入会日, @画像, @会員区分, @職業, @職業ＣＤ, @店舗ＣＤ, @申込日, @基本入金額, @入会金, @登録区分, @支払区分, @審査パスＦ, @現金Ｆ, @バス利用Ｆ, @利用区分, @申込担当者ＣＤ, @携帯番号１, @携帯番号２, @携帯番号３, DATEADD(hour, 9, GETUTCDATE()))" '@Head3Numと0埋めした@MAXをともに文字列化してから連結"
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
        sqlcom.Parameters.Add(New SqlParameter("申込日", SqlDbType.Char)).Value = Today.ToString("yyyyMMdd") 'DEL_2017/10/23 TOS163
        sqlcom.Parameters.Add(New SqlParameter("基本入金額", SqlDbType.Decimal)).Value = Session("plan_monthlydues") 'A2017/09/14 セッションの売上単価に変更
        sqlcom.Parameters.Add(New SqlParameter("入会金", SqlDbType.Decimal)).Value = Session("celAdmissionFee") 'A2017/09/14
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


#Region "年齢計算" 'ADD_2017/12/19 TOS163
    ''' <summary>
    ''' 生年月日から年齢を計算する
    ''' </summary>
    ''' <param name="birthDate">生年月日</param>
    ''' <param name="today">現在の日付</param>
    ''' <returns>年齢</returns>
    Public Shared Function GetAge(ByVal birthDate As DateTime,
                                  ByVal today As DateTime) As Integer
        Dim age As Integer = today.Year - birthDate.Year
        '誕生日がまだ来ていなければ、1引く
        If today.Month < birthDate.Month OrElse
            (today.Month = birthDate.Month AndAlso
                today.Day < birthDate.Day) Then
            age -= 1
        End If

        Return age
    End Function
#End Region

End Class