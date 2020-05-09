<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ThankyouForPayment.aspx.vb" Inherits="FitnessGymTest.ThankyouForPayment" %>
<%@ Import Namespace="System.Web.Security" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>ルネッス-来店受付完了</title>
    <!-- Bootstrap core CSS -->
    <link href="css/bootstrap.css" rel="stylesheet"/>
    <link href="css/font-awesome.min.css" rel="stylesheet"/>
    <!-- Custom styles for this template -->
    <link href="css/main.css" rel="stylesheet"/>
    <link href="css/webentry.css" rel="stylesheet"/>
    <%--<link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/redmond/jquery-ui.css"/>--%>

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
    <!-- Bootstrap core JavaScript================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <!--    <script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>-->
    <script src="scripts/jquery-3.1.1.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <style>
       
        h3 {
            text-align:center;
            color: #808080;
        }


        table{
            border:1px solid;
	        font-size:10.5pt;
	        font-family:"Century",serif;
	    }

        td{
            border:1px solid;
            padding:0 10px;
        }

        .space0{
            border-collapse:collapse;
            width:auto
        }
    
        .auto-style9 {
            color: red;
            font-weight: bold;
        }

        .auto-style10 {
            font-size: x-large;
            width: 394px;
            height: 50px;
        }

        .auto-style11 {
            width: 790px;
            font-size: xx-large;
            height: 50px;
        }

        .auto-style12 {
            font-size: x-large;
            margin: 0px 5px;
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 0px;
            padding-bottom: 5px;
        }

        .auto-style13 {
            font-size: x-large;
            width: 50px;
        }

        .auto-style14 {
            width: 750px;
            font-size: x-large;
        }
    
        .auto-style15 {
            width: 700px;
            font-size: xx-large;
        }

        .auto-style16 {
            font-size: x-large;
            width: 300px;
        }
    
    </style>
</head>
<body>
    <!-- Fixed navbar -->
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container webwidth2">
            <div class="navbar-header">
                <a class="navbar-brand" href="fitnessindex.aspx"></a>
            </div>
            <!--/.nav-collapse -->
        </div>
    </div>

    <!--Main部分----------------------------------------------->
    <div class="webtitle" style="padding-top: 50px;">
        <div class="container webwidth">
            <h4><strong>WEB入会完了</strong></h4>
        </div>
    </div>

    <div class="container webwidth">
        <h3>お申し込みありがとうございました。</h3>
        <div style="text-align: center; margin-bottom: 20px;">次の内容は、印刷・画面保存するなど大切にお取り扱いください。</div>
        
        <form id="form1" runat="server">
      
            <div class="row">
                <div class="col-sm-12">
                        
                    <table class ="space0">
                        <tbody>
                            <tr>
                                <td class="auto-style16">受付番号</td>
                                <td class="auto-style15">    
                                    <div class="auto-style12">
                                        <div class="row">
                                            <asp:Label ID="lblMemberCd" runat="server" Text=""></asp:Label>    
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style16">名前</td>
                                <td class="auto-style15">    
                                    <div class="auto-style12">
                                        <div class="row">
                                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>    
                                        </div>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style16">店舗</td>
                                <td class="auto-style15">
                                    <div class="auto-style12">
                                        <div class="row">
                                            <asp:Label ID="lblTenpo" runat="server" Text=""></asp:Label>    
                                        </div>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style16">受付日</td>
                                <td class="auto-style15">
                                    <div class="auto-style12">
                                        <div class="row">
                                            <asp:Label ID="lblUketukebi" runat="server" Text=""></asp:Label>    
                                        </div>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style16">来店予定日時<br /><small>(最終お手続き予定日)</small></td>
                                <td class="auto-style15">
                                    <div class="auto-style12">
                                        <div class="row">
                                            <asp:Label ID="lblRaiten" runat="server" Text=""></asp:Label>    
                                        </div>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style16">会員種別</td>
                                <td class="auto-style15">
                                    <div class="auto-style12">
                                        <div class="row">
                                            <asp:Label ID="lblSyubetu" runat="server" Text=""></asp:Label>    
                                        </div>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style16">ご入会店舗</td>
                                <td class="auto-style15">
                                    <div class="auto-style12">
                                        <div class="row">
                                            <asp:Label ID="lblGoriyokakijo" runat="server" Text=""></asp:Label>    
                                        </div>
                                    </div>
                                </td>
                            </tr>

                        </tbody>
                    </table>
                    <br />

                    <div class="auto-style9">
                        来店日（最終お手続き）の際には以下のものをご持参ください。
                    </div>

                    <table class ="space0">
                        <tbody>

                            <tr>
                                <td class="auto-style13">１．</td>
                                <td class="auto-style14">受付内容を確認できるもの（以下のいずれか一つ）<br />
                                                        <span style="font-size: large;">●受付番号<br />●この画面のプリントアウトまたはスクリーンショット<br />●確認メールの受信画面</span>              
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style13">２．</td>
                                <td class="auto-style14">本人確認書類<br />
                                                    <span style="font-size: large;">免許証や保険証など、氏名、住所を確認できるもの</span>
                                </td>
                            </tr>

                            <tr>
                                <td class="auto-style13">３．</td>
                                <td class="auto-style14">通帳(またはキャッシュカード)、届出印<br />
                                                        <span style="font-size: large;">月会費の口座振替を行う金融機関の通帳(またはキャッシュカード)と届出印</span>
                                </td>
                            </tr>

<%--                             <tr>
                                <td class="auto-style13">４．</td>
                                <td class="auto-style14">
                                    <div class="auto-style12">
                                        <div class="row">
                                            ご入会諸費用　<asp:Label ID="lblHiyou" runat="server" Text=""></asp:Label>    
                                        </div>
                                    </div>
                                </td>
                            </tr>--%>
  
                        </tbody>
                    </table>

                </div>
            </div>

            <div style="margin-top: 20px;">
<%--                初回来店日には以下の手続きを行います。
                <div>１．口座振替用紙の記入・捺印（月会費の引落用）</p>
                <div>２．トレサポ利用登録</p>
                ※トレサポ＝ルネッスオリジナルの運動サポート<br />
                <div style="color: red;">※初回オリエンテーションは１時間ほどかかります。</div>--%>
                <ul>
                    <li>※WEBで顔写真を登録されなかった場合は、来店日（最終お手続き）に会員証の写真撮影を行わせていただきます。</li>
                    <li>※18歳未満の方の入会は親権者の同意が必要です。</li>
                    <li>※入会資格の条件を満たさないと判断した場合、入会をお断りすることがあります。</li>
                </ul>
            </div>


            <asp:panel  runat="server" id ="pnlNishitenuketuke">
                <div style="margin: 50px 0 20px;">
                    ◆ご質問、ご不明な点がございましたら、下記の先行受付会場までご連絡ください。
                </div>

                <div class="row" style="border: 1px solid #808080; margin: 0 3px;">
                    <div class="col-sm-12">
                        【10/21～11/15　先行受付会場】
                    </div>
                    <div class="col-sm-2">会場</div>
                    <div class="col-sm-10">スポーツクラブルネッス福井南店</div>
                    <div class="col-sm-2">ＴＥＬ</div>
                    <div class="col-sm-10">0776-63-5720</div>
                    <div class="col-sm-2">受付時間</div>
                    <div class="col-sm-10">平日・土曜 10:00-21:00、日曜・祝日　10:00-18:00</div>
                    <div class="col-sm-2">休館日</div>
                    <div class="col-sm-10">毎週木曜日</div>
                </div>

                <div class="row" style="border: 1px solid #808080; margin: 10px 3px;">
                    <div class="col-sm-12">
                        【11/16～11/30　先行受付会場】
                    </div>
                    <div class="col-sm-2">会場</div>
                    <div class="col-sm-10">スポーツクラブルネッス福井西店</div>
                    <div class="col-sm-2">ＴＥＬ</div>
                    <div class="col-sm-10">0776-43-1844</div>
                    <div class="col-sm-2">受付時間</div>
                    <div class="col-sm-10">平日・土曜・日曜・祝日　10:00-15:00、17:00-21:00</div>
                    <div class="col-sm-2">休館日</div>
                    <div class="col-sm-10">期間中定休日なし</div>
                </div>

                <div class="row" style="border: 1px solid #808080; margin: 40px 3px 20px;">
                    <div class="col-sm-12">
                        ◆新店舗情報
                    </div>
                    <div class="col-sm-2">店舗名</div>
                    <div class="col-sm-10">スポーツクラブルネッス福井西店</div>
                    <div class="col-sm-2">住所</div>
                    <div class="col-sm-10">〒910-0071福井市文京6-23-2</div>
                    <div class="col-sm-2">電話番号</div>
                    <div class="col-sm-10">0776-43-1844</div>
                    <div class="col-sm-2">営業時間</div>
                    <div class="col-sm-10">平日9:30-22:30/土曜9:30-21:30/日曜・祝日9:30-18:00</div>
                    <div class="col-sm-2">休館日</div>
                    <div class="col-sm-10">毎週火曜日　※夏季特別休館日、年末年始、臨時休業</div>
                </div>
            </asp:panel>

            <div>
                <h1>ご来店、心よりお待ちしております。</h1>
            </div>

            <div class="button-group" style="margin-bottom: 20px;">
                <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="http://www.sports-renaiss.jp/">トップへ戻る</asp:HyperLink>
            </div>
        </form>
    </div>

    <%-- ADD_start---2017/09/27 TOS163 --%>
    <script type="text/javascript">

        // ブラウザ戻るボタンを制御
        history.pushState(null, null, null);
        window.addEventListener("popstate", function () {
            history.pushState(null, null, null);
        });
    </script>
    <%-- ADD_end-----2017/09/27 TOS163 --%>

</body>
</html>
