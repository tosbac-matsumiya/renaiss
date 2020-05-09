<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Registration_1_2.aspx.vb" Inherits="FitnessGymTest.Registration_1_2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>ルネッス</title>
    <!-- Bootstrap core CSS -->
    <link href="css/bootstrap.css" rel="stylesheet"/>
    <link href="css/font-awesome.min.css" rel="stylesheet"/>
    <!-- Custom styles for this template -->
    <link href="css/main.css" rel="stylesheet"/>
    <link href="css/webentry.css" rel="stylesheet"/>
    <link rel="stylesheet" href="//ajax.googleapis.com/ajax/libs/jqueryui/1/themes/redmond/jquery-ui.css"/>

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1/i18n/jquery.ui.datepicker-ja.min.js"></script>
    <style>
		.sunday .ui-state-default {
			color: red;
		}
		
		.saturday .ui-state-default {
			color: blue;
		}
		
		.holiday .ui-state-default {
			color: red;
		}

        .ui-state-active,
		.ui-widget-content .ui-state-active,
		.ui-widget-header .ui-state-active {
		  border: 1px solid #211b0d;
		  background: white;
		  font-weight: normal;
		  color: black;
		}
		/*.ui-state-active a,
		.ui-state-active a:link,
		.ui-state-active a:visited {
		  color: black;
		  text-decoration: none;
		}*/
	</style>
</head>
<body>
    <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>/<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>--%>
    <div class="webtitle">
        <div class="container webwidth">
            <h4>WEB<strong>入会フォーム</strong></h4>
        </div>
    </div>

    <div class="container webwidth">
        <div class="banner">
            <a href="#">
           <!--     <img class="banner_image img-responsive" src="img/button.png"/>-->
            </a>
        </div>
        <nav>
            <ol class="stepBar step3">
		      <li class="step current">STEP 1 <small>ご利用情報入力</small></li>
		      <li class="step">STEP 2 <small>お客様情報入力</small></li>
		      <li class="step">STEP 3 <small>登録内容確認</small></li>
		    </ol>
        </nav>

        <form  id="form1" runat="server">
            
            <div class="backcolor_w">
                <div class="form-group">
                    <label>ご利用店舗を選択してください。</label>
                    <div class="row">
                        <div class="col-sm-4  col-sm-offset-1">
                            <asp:DropDownList ID="ddlFacilityName" runat="server" DataTextField="店舗名" DataValueField="店舗ＣＤ" CssClass="form-control" Font-Size="Medium">
                            </asp:DropDownList>
                            <%--<asp:DropDownList ID="ddlFacilityName" runat="server" DataSourceID="SqlDataSource1" DataTextField="店舗名" DataValueField="店舗ＣＤ" CssClass="form-control" Font-Size="Medium">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT * FROM [Ｍ店舗] WHERE 店舗ＣＤ IN ('1','2','4','5')" ConflictDetection="CompareAllValues"></asp:SqlDataSource>--%> <%--現状は花がたみの郷(3)は対象外--%>
                        </div>
                        <div class="col-sm-4">
                            <asp:Button ID="btnSearch" runat="server" Text="決定" CssClass="btn btn-info" Font-Bold="True" />
                        </div>
                     </div>
                </div>

                <div class="form-group">
                    <label>ご利用コースを選択してください。</label>
                    <span><asp:Label ID="lblerr_plan" runat="server" Text="" ForeColor="Red"></asp:Label></span>  
                    <div class="row">
                        <div class="col-sm-10 col-sm-offset-1">
                            <asp:Label ID="Label3" runat="server" Text="入会金"></asp:Label>&nbsp;
                            <asp:Label ID="lblAdmissionFee" runat="server" Text="0円"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="Label2" runat="server" Text="登録手数料"></asp:Label>&nbsp;
                            <asp:Label ID="lblRegistrationFee" runat="server" Text="0円"></asp:Label>
                            <br /><asp:Label ID="lblTsurugaMsg" runat="server" Text="※ファミリー会員については店頭でのみ受付となります。" Visible="false" Font-Underline="True"></asp:Label>
                            <asp:DataList ID="dltPlan" runat="server" DataKeyField="商品ＣＤ" DataSourceID="SqlDataSource2" CssClass="image_list">
                                <ItemTemplate>
                                    <div class="image_box">
                                        <asp:Image ID="PlanImgLabel" runat="server" CssClass="thumbnail img-responsive" ImageUrl='<%# Eval("画像パス") %>' />
                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="disabled_checkbox" Checked="true" Text="選択"/>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:HiddenField ID="hdnGetsrc_P" runat="server" value="0"/>
                            <asp:HiddenField ID="hdnGetid_P" runat="server" value="0"/>
                
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="IF (@店舗ＣＤ = 1 OR @店舗ＣＤ = 2) SELECT * FROM [Ｍ商品] WHERE ([店舗ＣＤ] = 999) AND ([画像パス] LIKE 'img/Web-%') ELSE SELECT * FROM [Ｍ商品] WHERE ([店舗ＣＤ] = @店舗ＣＤ) AND ([画像パス] LIKE 'img/Web-%')">　<%--Ｍ商品テーブル店舗ＣＤ列=ddlFacilityNameで選択した@店舗ＣＤ　かつ　画像パスに'img/Web-'が先頭に含まれるものをSELECT、@店舗ＣＤは越前(1)・鯖江(2)のとき999に集約する--%>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlFacilityName" Name="店舗ＣＤ" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>              
                </div>

             <!--   <div class="form-group">
                    <label>オプションを選択してください。</label>
                    <span><asp:Label ID="lblerr_option" runat="server" Text="" ForeColor="Red"></asp:Label></span>
                    <div class="row">
                        <div class="col-sm-10 col-sm-offset-1" aria-hidden="True">
                            <asp:DataList ID="dltOption" runat="server" DataKeyField="Id" DataSourceID="SqlDataSource3" CssClass="image_list2" Visible="false">
                                <ItemTemplate>
                                    <div class="image_box2">
                                        <asp:Image ID="OptionImgLabel" runat="server" CssClass="thumbnail2 img-responsive" ImageUrl='<%# Eval("OptionImg") %>' />
                                        <asp:CheckBox ID="CheckBox2" runat="server" CssClass="disabled_checkbox2" Checked="true" Text="選択"/>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                
                            <asp:HiddenField ID="hdnGetsrc_O" runat="server" value="0" Visible="False"/>
                            <asp:HiddenField ID="hdnGetid_O" runat="server" value="0"/>
                
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="SELECT * FROM [GymPlanOption] WHERE ([StoreID] = @StoreID) OR ([StoreID] = 99) ORDER BY [StoreID]">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlFacilityName" Name="StoreID" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                </div>
                 -->
                <div class="form-group">
                    <label>初回ご来所予定日を入力してください。</label>
                    <span><asp:Label ID="lblerr_datepicker" runat="server" Text="" ForeColor="Red"></asp:Label></span>
                    <div class="row">
                        <%--<div class="col-sm-4 col-sm-offset-2">
                            <asp:TextBox ID="datepicker" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <span class="text-danger">※例）2017/01/01</span>
                        </div>--%>
                        <div class="col-sm-4 col-sm-offset-1">
                            <asp:Panel ID="pnlDatepicker" runat="server">
                                <asp:TextBox ID="txtDatepicker" runat="server" CssClass="form-control" ReadOnly="false"></asp:TextBox>
	                            <div id="datepicker"></div>
	                            <div id="datepicker2"></div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <div class="button-group">
                <asp:Button ID="btnNext" runat="server" Text="STEP 3　登録確認画面へもどる" CssClass="btn2 edit-btn" />
                <br />
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/tokuteisyou.aspx" Target="_blank">特定商取引法に基づく表記</asp:HyperLink>
            </div>

        </form>
    </div>

    <script type="text/javascript">

        $(function () {
            var $id = $("#hdnGetid_P").val();

            if ($id != 0) {
                // チェックを入れた状態にします。
                $("#" + $id).addClass('checked');
            }

            var $id_o = $("#hdnGetid_O").val();

            if ($id_o != 0) {
                // チェックを入れた状態にします。
                $("#" + $id_o).addClass('checked2');
            }
        });


        //ADD_start-------------------------------------------------------------2018/05/30 TOS163
        //OKボタンクリックで選択解除
        $('#btnSearch').click(function () {
            var $imageList = $('.image_list');

            // 現在の選択を解除します。
            $imageList.find('img.thumbnail.checked').removeClass('checked');

            $("#hdnGetid_P").val(0);
        });
        //ADD_end---------------------------------------------------------------2018/05/30 TOS163


        //コース選択処理----------------------------------------------------------
        $('img.thumbnail').click(function () {
            var $imageList = $('.image_list');

            // 現在の選択を解除します。
            $imageList.find('img.thumbnail.checked').removeClass('checked');

            // チェックを入れた状態にします。
            $(this).addClass('checked');

            var $val = $(this).attr("src");
            $("#hdnGetsrc_P").val($val);

            var $id = $(this).attr("id");
            $("#hdnGetid_P").val($id);

        });

        // チェックボックスのクリックを無効化します。
        $('.image_box .disabled_checkbox').click(function () {
            return false;
        });


        //オプション選択処理----------------------------------------------------------
        $('img.thumbnail2').click(function () {
            var $imageList = $('.image_list2');

            // 現在の選択を解除します。
            $imageList.find('img.thumbnail2.checked2').removeClass('checked2');

            // チェックを入れた状態にします。
            $(this).addClass('checked2');

            var $val = $(this).attr("src");
            $("#hdnGetsrc_O").val($val);

            var $id = $(this).attr("id");
            $("#hdnGetid_O").val($id);

        });

        // チェックボックスのクリックを無効化します。
        $('.image_box2 .disabled_checkbox2').click(function () {
            return false;
        });


        //カレンダー表示----------------------------------------------------------
        //$(function () {
        //    $("#datepicker").datepicker();
        //});

        $(function () {
            var hiduke = new Date();
            var year = hiduke.getFullYear();
            var nextyear = year + 1;

            // 祝日を配列で確保
            var holidays = [
//                year + '-03-21',
//                year + '-04-30',
 //               year + '-05-03',
//                year + '-05-04',
 //               year + '-05-05'
            ];
            // 休暇
            var kyukas = [
                year + '-12-30',
                year + '-12-31',
                nextyear + '-01-01',
                nextyear + '-01-02',
                nextyear + '-01-03'
            ];
            // 翌月末日をセット
            var date = new Date;
            date.setMonth(date.getMonth() + 2, 0); // 翌々月, 0日（1日の前日）

            var tenpoid = $("#ddlFacilityName").val(); //店舗ID
            var holidayYobi; //定休日の曜日変数
            var sunday; //日曜日が定休日かどうか
            var horiday; //祝日が定休日かどうか
            switch (tenpoid) {
                case "6"://福井西 火曜日   
                    holidayYobi = 2;
                    sunday = true;
                    horiday = true;
                    break;


                case "5"://福井南 木曜日   
                    holidayYobi = 4;
                    sunday = true;
                    horiday = true;
                    break;

                case "1"://越前 金曜日   
                    holidayYobi = 5;
                    sunday = true;
                    horiday = true;
                    break;

                case "2"://鯖江 月曜日   
                    holidayYobi = 1;
                    sunday = true;
                    horiday = true;
                    break;

                case "4"://敦賀 金曜日   
                    holidayYobi = 5;
                    sunday = true;
                    horiday = true;
                    break;

                case "3"://越前花がたみの郷 日曜日   
                    sunday = false;
                    horiday = false;
                    break;
            }

            ////春キャンペーン2018--------------
            //var MaxDate; //選択可能最大値
            //var intMonth = hiduke.getMonth()
            //if (intMonth == 2) { //3月
            //    MaxDate = '2018/04/01'
            //} else if (intMonth == 3) { //4月
            //    MaxDate = '2018/05/01'
            //} else {
            //    MaxDate = date
            //}
            ////春キャンペーン2018--------------

            $("#datepicker").datepicker({
                minDate: '0',
                maxDate: date, 
                //maxDate: MaxDate, //ADD_2018/03/08 TOS163 //DEL_2018/05/26 TOS163
                hideIfNoPrevNext: true,
                firstDay: 1,
                numberOfMonths: [1, 2],
                beforeShowDay: function (date) {
                    // 祝日の判定
                    for (var i = 0; i < holidays.length; i++) {
                        var htime = Date.parse(holidays[i]); // 祝日を 'YYYY-MM-DD' から time へ変換
                        var holiday = new Date();
                        holiday.setTime(htime); // 上記 time を Date へ設定

                        // 祝日
                        if (holiday.getYear() == date.getYear() &&
                            holiday.getMonth() == date.getMonth() &&
                            holiday.getDate() == date.getDate()) {
                            return [horiday, 'holiday'];
                        }
                    }
                    //休暇の判定
                    for (var i = 0; i < kyukas.length; i++) {
                        var ktime = Date.parse(kyukas[i]); // 休暇を 'YYYY-MM-DD' から time へ変換
                        var kyuka = new Date();
                        kyuka.setTime(ktime); // 上記 time を Date へ設定

                        // 休暇
                        if (kyuka.getYear() == date.getYear() &&
                            kyuka.getMonth() == date.getMonth() &&
                            kyuka.getDate() == date.getDate()) {
                            return [false, 'holiday'];
                        }
                    }

                    // 日曜日
                    if (date.getDay() == 0) {
                        return [sunday, 'sunday'];
                    }

                    // 土曜日
                    if (date.getDay() == 6) {
                        return [true, 'saturday'];
                    }
                    // 定休日
                    if (date.getDay() == holidayYobi) {
                        return [false, 'Regularholiday'];
                    }
                    // 平日
                    return [true, ''];
                },
                onSelect: function (dateText, inst) {
                    $("#txtDatepicker").val(dateText);
                }
            });

            $("#datepicker2").datepicker({
                minDate: '0',
                maxDate: date,
                //maxDate: MaxDate, //ADD_2018/03/08 TOS163 //DEL_2018/05/26 TOS163
                hideIfNoPrevNext: true,
                firstDay: 1,
                //				numberOfMonths: [1, 2],
                beforeShowDay: function (date) {
                    // 祝日の判定
                    for (var i = 0; i < holidays.length; i++) {
                        var htime = Date.parse(holidays[i]); // 祝日を 'YYYY-MM-DD' から time へ変換
                        var holiday = new Date();
                        holiday.setTime(htime); // 上記 time を Date へ設定

                        // 祝日
                        if (holiday.getYear() == date.getYear() &&
                            holiday.getMonth() == date.getMonth() &&
                            holiday.getDate() == date.getDate()) {
                            return [horiday, 'holiday'];
                        }
                    }
                    //休暇の判定
                    for (var i = 0; i < kyukas.length; i++) {
                        var ktime = Date.parse(kyukas[i]); // 休暇を 'YYYY-MM-DD' から time へ変換
                        var kyuka = new Date();
                        kyuka.setTime(ktime); // 上記 time を Date へ設定

                        // 休暇
                        if (kyuka.getYear() == date.getYear() &&
                            kyuka.getMonth() == date.getMonth() &&
                            kyuka.getDate() == date.getDate()) {
                            return [false, 'holiday'];
                        }
                    }
                    // 日曜日
                    if (date.getDay() == 0) {
                        return [sunday, 'sunday'];
                    }
                    // 土曜日
                    if (date.getDay() == 6) {
                        return [true, 'saturday'];
                    }
                    // 定休日
                    if (date.getDay() == holidayYobi) {
                        return [false, 'Regularholiday'];
                    }
                    // 平日
                    return [true, ''];
                },
                onSelect: function (dateText, inst) {
                    $("#txtDatepicker").val(dateText);
                }
            });

        });

        //-- ADD_start---2017/09/27 TOS163
        // ブラウザ戻るボタンを制御
        history.pushState(null, null, null);
        window.addEventListener("popstate", function () {
                history.pushState(null, null, null);
        });
        //-- ADD_end-----2017/09/27 TOS163 

    </script>


</body>
</html>
