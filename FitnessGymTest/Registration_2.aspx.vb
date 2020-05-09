Imports System.IO
Imports System.Data.SqlClient
Public Class Registration_2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ddlBirthYear.SelectedValue = "1990"
        End If

        'a2019/03/08---START-------------------------------------------------------
        Dim localNow As DateTime = DateTime.Now
        Dim toUtc As DateTime = localNow.ToUniversalTime()
        toUtc = toUtc.AddHours(9)

        If toUtc.ToString("yyyy/MM/dd HH:mm:ss") >= "2019/06/15 00:00:00" And toUtc.ToString("yyyy/MM/dd HH:mm:ss") < "2019/08/01 00:00:00" Then
            lblNebiki.Text = "※クレジットカード決済なら翌々月分会費500円引き！"
        Else
            lblNebiki.Text = ""
        End If
        'a2019/03/08---END-------------------------------------------------------

    End Sub

    Protected Sub BtnNext_Click(sender As Object, e As EventArgs) Handles BtnNext.Click

        Try
            Session("photodata2") = fupPhoto2.Text '切取後の画像URL a2017/8/03_TOS163
            '入力チェック
            If DetailInputCheck() = False Then Exit Sub

            Session("name_kanjifirst") = txtFirstNameKanji.Text
            Session("name_kanjilast") = txtLastNameKanji.Text
            Session("name_kanji") = txtLastNameKanji.Text & " " & txtFirstNameKanji.Text '名前（漢字）
            Session("name_kanafirst") = txtFirstNameKana.Text
            Session("name_kanalast") = txtLastNameKana.Text
            Session("name_kana") = txtLastNameKana.Text & " " & txtFirstNameKana.Text '名前（カナ）
            Session("sex") = rblSex.SelectedValue '性別
            Session("birthday") = ddlBirthYear.SelectedValue & "/" & ddlBirthMonth.SelectedValue & "/" & ddlBirthDay.SelectedValue '誕生日
            Session("job") = ddlJob.SelectedItem.Text '職業
            Session("job_cd") = ddlJob.SelectedItem.Value '職業ＣＤ
            Session("phonenumber") = txtPhone.Text '電話番号
            Session("zipcode") = txtZipCode.Text '郵便番号
            Session("zipcode3-4") = Left(txtZipCode.Text, 3) & "-" & Right(txtZipCode.Text, 4)
            Session("address0") = ddlPrefecture.Text
            Session("address1") = txtAddress1.Text
            Session("address2") = txtAddress2.Text
            Session("address3") = txtAddress3.Text
            Session("address") = ddlPrefecture.Text & txtAddress1.Text & txtAddress2.Text & " " & txtAddress3.Text '住所
            Session("address2+3") = txtAddress2.Text & " " & txtAddress3.Text
            Session("email") = txtEmail.Text 'メールアドレス
            Session("payment") = rblPayment.SelectedItem.Value '支払方法 0:初回来所時に現金支払い、1:現金支払い済み、2:クレジット支払い、3:クレジット支払い確認済み
            '消すSession("credit_id") = rblPayment.SelectedIndex '支払方法id

            'Session("photodata1") = fupPhoto.FileBytes '画像データ
            Dim binaryData(fupPhoto.PostedFile.InputStream.Length) As Byte
            fupPhoto.PostedFile.InputStream.Read(binaryData, 0, fupPhoto.PostedFile.InputStream.Length)
            'Command.Parameters.Add("@file", SqlDbType.VarBinary).Value = binaryDa
            Session("photodata") = binaryData

            Session("datatype") = Path.GetFileName(fupPhoto.PostedFile.ContentType) 'データタイプ
            Session("photodata2") = fupPhoto2.Text '切取後の画像データ
            'Session("photodata3") = System.Convert.FromBase64String(fupPhoto3.Text) '切取後の画像データ 'a2017/08/03_TOS163 d2017/08/07_TOS163

            Session("photodata3") = fupPhoto3.Text '切取後の画像データ 'a2017/08/07_TOS163


            'sdsRegistration.InsertParameters.Add("NameKanji", txtLastNameKanji.Text & " " & txtFirstNameKanji.Text)
            'sdsRegistration.InsertParameters.Add("NameKana", txtLastNameKana.Text & " " & txtFirstNameKana.Text)
            'sdsRegistration.InsertParameters.Add("Sex", rblSex.SelectedValue)
            'sdsRegistration.InsertParameters.Add("BirthDay", ddlBirthYear.SelectedValue & "/" & ddlBirthMonth.SelectedValue & "/" & ddlBirthDay.SelectedValue)
            'sdsRegistration.InsertParameters.Add("Job", ddlJob.SelectedValue)
            'sdsRegistration.InsertParameters.Add("PhoneNumber", txtPhone.Text)
            'sdsRegistration.InsertParameters.Add("ZipCode", txtZipCode.Text)
            'sdsRegistration.InsertParameters.Add("Address", txtAddress1.Text & txtAddress2.Text & " " & txtAddress3.Text)
            'sdsRegistration.InsertParameters.Add("Email", txtEmail.Text)
            'sdsRegistration.InsertParameters.Add("Payment", rblPayment.SelectedValue)
            'sdsRegistration.InsertParameters.Add("DataType", Path.GetFileName(fupPhoto.PostedFile.ContentType))
            'sdsRegistration.InsertParameters.Add("PhotoData2", fupPhoto2.Text)

            'sdsRegistration.InsertParameters.Add("FacilityName", Session("facility_name").ToString)
            'sdsRegistration.InsertParameters.Add("MembershipPlanID", Session("plan_id").ToString)
            'sdsRegistration.InsertParameters.Add("PlanoptionID", Session("option_id").ToString)
            'sdsRegistration.InsertParameters.Add("DateVisit", Session("date_visit").ToString)


            'sdsRegistration.Insert()

            'Response.Redirect("Thankyou.aspx")


            LogInsert() 'renaissDBに登録　ADD_2017/09/29 TOS163

        Catch ex As Exception
        MsgBox("システムエラーが起きました。")
        Exit Sub
        End Try

        Response.Redirect("Registration_3.aspx")

    End Sub

    'Protected Sub rblPhoto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblPhoto.SelectedIndexChanged
    '    If rblPhoto.Items(1).Selected Then
    '        fupPhoto.Visible = True
    '    Else
    '        fupPhoto.Visible = False
    '    End If
    'End Sub

    Private Function DetailInputCheck() As Boolean

        '--- 名前（姓）(必須) ---
        If txtLastNameKanji.Text = "" Then
            'MsgBox("お名前の'姓'を入力してください。")
            lblerr_kanji.Text = "※お名前の'姓'を入力してください。"
            txtLastNameKanji.Focus()
            Return False
        Else
            lblerr_kanji.Text = ""
        End If

        '--- 名前（名）(必須) ---
        If txtFirstNameKanji.Text = "" Then
            'MsgBox("お名前の'名'を入力してください。")
            lblerr_kanji.Text = "※お名前の'名'を入力してください。"
            txtFirstNameKanji.Focus()
            Return False
        Else
            lblerr_kana.Text = ""
        End If

        '--- フリガナ（セイ）(必須) ---
        If txtLastNameKana.Text = "" Then
            'MsgBox("フリガナの'セイ'を入力してください。")
            lblerr_kana.Text = "※フリガナの'セイ'を入力してください。"
            txtLastNameKana.Focus()
            Return False
        Else
            If Not Regex.IsMatch(txtLastNameKana.Text, "^[ァ-ー]+$") Then
                ' カタカナエラー
                'MsgBox("全角カタカナで入力してください")
                lblerr_kana.Text = "※全角カタカナで入力してください"
                txtLastNameKana.Focus()
                Return False
            End If
            lblerr_kana.Text = ""
        End If

        '--- フリガナ（メイ）(必須) ---
        If txtFirstNameKana.Text = "" Then
            'MsgBox("フリガナの'メイ'を入力してください。")
            lblerr_kana.Text = "※フリガナの'メイ'を入力してください。"
            txtFirstNameKana.Focus()
            Return False
        Else
            If Not Regex.IsMatch(txtFirstNameKana.Text, "^[ァ-ー]+$") Then
                ' カタカナエラー
                'MsgBox("全角カタカナで入力してください")
                lblerr_kana.Text = "※全角カタカナで入力してください"
                txtFirstNameKana.Focus()
                Return False
            End If
            lblerr_kana.Text = ""
        End If

        '--- 性別の選択(必須) ---
        If rblSex.SelectedValue = "" Then
            'MsgBox("性別を選択してください。")
            lblerr_sex.Text = "※性別を選択してください。"
            rblSex.Focus()
            Return False
        Else
            lblerr_sex.Text = ""
        End If

        '--- 生年月日の選択(必須) ---
        Dim birth As String = ddlBirthYear.SelectedValue & ddlBirthMonth.SelectedValue & ddlBirthDay.SelectedValue
        If birth.Length <> 8 Then
            MsgBox("生年月日を選択してください。")
            ddlBirthYear.Focus()
            Return False
        End If

        '--- 職業の選択(必須) ---
        If ddlJob.SelectedValue = "" Then
            MsgBox("職業を選択してください。")
            ddlJob.Focus()
            Return False
        End If

        '--- 電話番号(必須) ---
        If txtPhone.Text = "" Then
            'MsgBox("電話番号を入力してください。")
            lblerr_phone.Text = "※電話番号を入力してください。"
            txtPhone.Focus()
            Return False
        Else
            lblerr_phone.Text = ""
        End If

        '--- 郵便番号(必須) ---
        If txtZipCode.Text = "" Then
            'MsgBox("郵便番号を入力してください。")
            lblerr_zipcode.Text = "※郵便番号を入力してください。"
            txtZipCode.Focus()
            Return False
        Else
            lblerr_zipcode.Text = ""
        End If

        '--- 都道府県の選択(必須) ---
        If ddlPrefecture.SelectedValue = "選択して下さい" Then
            'MsgBox("都道府県を選択してください。")
            lblerr_address.Text = "※都道府県を選択してください。"
            ddlPrefecture.Focus()
            Return False
        Else
            lblerr_address.Text = ""
        End If

        '--- 市町村(必須) ---
        If txtAddress1.Text = "" Then
            'MsgBox("市町村を入力してください。")
            lblerr_address.Text = "※市町村を入力してください。"
            txtAddress1.Focus()
            Return False
        Else
            lblerr_address.Text = ""
        End If

        '--- 丁目・番地・号(必須) ---
        If txtAddress2.Text = "" Then
            'MsgBox("丁目・番地・号を入力してください。")
            lblerr_address.Text = "※丁目・番地・号を入力してください。"
            txtAddress2.Focus()
            Return False
        Else
            lblerr_address.Text = ""
        End If

        '--- メールアドレス(必須) ---
        If txtEmail.Text = "" Then
            'MsgBox("メールアドレスを入力してください。")
            lblerr_email.Text = "※メールアドレスを入力してください。"
            txtEmail.Focus()
            Return False
        Else
            '半角英数字に一致しているかチェック
            Dim r As New System.Text.RegularExpressions.Regex(“^[a-zA-Z0-9]+$”)
            If r.IsMatch(txtEmail.Text) = True Then
                'MsgBox(“半角英数字で入力してください。”)
                lblerr_email.Text = "※半角英数字で入力してください。"
                txtEmail.Focus()
                Return False
            End If
            lblerr_email.Text = ""
        End If

        '--- メールアドレス（確認用）(必須) ---
        If txtEmail2.Text = "" Then
            'MsgBox("メールアドレス（確認用）を入力してください。")
            lblerr_email.Text = "※メールアドレス（確認用）を入力してください。"
            txtEmail2.Focus()
            Return False
        Else
            If txtEmail.Text <> txtEmail2.Text Then
                'MsgBox("メールアドレスと（確認用）が一致しません。")
                lblerr_email.Text = "※メールアドレスと（確認用）が一致しません。"
                txtEmail.Focus()
                Return False
            End If
            lblerr_email.Text = ""
        End If

        '--- お支払い方法の選択(必須) ---
        If rblPayment.SelectedValue = "" Then
            'MsgBox("お支払い方法を選択してください。")
            lblerr_Payment.Text = "※お支払い方法を選択してください。"
            rblPayment.Focus()
            Return False
        Else
            lblerr_Payment.Text = ""
        End If

        Return True

    End Function


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
        strSql = strSql & " INSERT INTO LOG2_Ｍ会員_FITNESS (会員種別, 会員名, 会員カナ名, 誕生日, 性別, 郵便番号, 住所, 住所1, 住所2, 住所3, 住所4, 携帯番号, ＰＣＭＡＩＬ, 入会日, 画像, 会員区分, 職業, 職業ＣＤ, 店舗ＣＤ, 申込日, 基本入金額, 入会金, 登録区分, 支払区分, 審査パスＦ, 現金Ｆ, バス利用Ｆ, 利用区分, 申込担当者ＣＤ, 携帯番号１, 携帯番号２, 携帯番号３, Timestamp)"
        strSql = strSql & " VALUES (@会員種別, @会員名, @会員カナ名, @誕生日, @性別, @郵便番号, @住所, @住所1, @住所2, @住所3, @住所4, @携帯番号, @ＰＣＭＡＩＬ, @入会日, @画像, @会員区分, @職業, @職業ＣＤ, @店舗ＣＤ, @申込日, @基本入金額, 0, @登録区分, @支払区分, @審査パスＦ, @現金Ｆ, @バス利用Ｆ, @利用区分, @申込担当者ＣＤ, @携帯番号１, @携帯番号２, @携帯番号３, DATEADD(hour, 9, GETUTCDATE()))" '@Head3Numと0埋めした@MAXをともに文字列化してから連結"
        strSql = strSql & " SELECT SCOPE_IDENTITY()"

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

        'IDの取得をする場合
        'Dim res As Integer = CInt(sqlcom.ExecuteScalar())
        'Session("LOGid") = res

    End Sub

End Class