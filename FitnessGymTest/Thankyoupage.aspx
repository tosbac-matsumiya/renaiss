<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Thankyoupage.aspx.vb" Inherits="FitnessGymTest.Thankyoupage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
    <%--<script src="scripts/main.js"></script>--%>
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
            width: 121px;
        }
        .auto-style14 {
            width: 681px;
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
        
        <form id="form1" runat="server">

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


            <div class="button-group" style="margin-bottom: 20px;">
                <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/fitnessindex.aspx">トップへ戻る</asp:HyperLink>
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
