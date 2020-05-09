<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Registration_0.aspx.vb" Inherits="FitnessGymTest.Registration_0" %>

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

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media
        queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1/i18n/jquery.ui.datepicker-ja.min.js"></script>
    <style>

        .margin-top {
            margin-top: 50px;
        }

        .form-group h4 {
            margin-left: 20px;
        }

        @keyframes loading {
          0%, 100% {
            transform: translateY(0);
          }
          50% {
            transform: translateY(15px);
          }
        }

    </style>
</head>
<body>
    <div class="webtitle">
        <div class="container webwidth">
            <h4>WEB<strong>入会フォーム-店舗選択</strong></h4>
        </div>
    </div>

    <div class="container webwidth">
        <form  id="form1" runat="server">
            <div id="JSNG" style="padding: 20px; text-align: center; font-size: large;">
                <div>WEB入会をご利用するには、JavaScriptを有効にする必要があります。<br />
                    このブラウザでは、JavaScriptが無効になっているか、サポートされていないようです。<br />
                    ブラウザのオプションを変更して、JavaScriptを有効にし、もう一度お試しください。
                </div>
                <div class="button-group">
                    <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/fitnessindex.aspx">トップへ戻る</asp:HyperLink>
                </div>
            </div>

            <script>  document.getElementById("JSNG").style.display = "none";</script>

            <div id="JSOK" class="backcolor_w" style="display:none;">
                 <h4>ご利用する店舗のボタンをクリックしてください。</h4>
                 <div class="form-group margin-top">
                    <h4>福井西店の方</h4>
                    <div class="row">
                        <div class="col-sm-6 col-sm-offset-3">
                            <%--<a class="btn btn-warning btn-lg btn-block" href="Registration-ns_1.aspx"><strong>福井西店&nbsp;&nbsp;<span style="font-size: x-small; color:black;">12/1（金）OPEN！！</span></strong></a>--%>
                             <asp:Button ID="btnNishi" runat="server" Text="福井西店" CssClass="btn btn-warning btn-lg btn-block" Font-Bold="True" />
                        </div>
                    </div>
                </div>                
                <div class="form-group margin-top"> 
                    <h4>福井西店以外の方</h4>
                    <div class="row">
                        <div class="col-sm-6 col-sm-offset-3">
                            <%--<a class="btn btn-success btn-lg btn-block" href="Registration_1.aspx"><strong>福井西店以外の方</strong></a>--%>
                            <asp:Button ID="btnEchisaba" runat="server" Text="福井西店以外の方" CssClass="btn btn-success btn-lg btn-block" Font-Bold="True" />
                        </div>
                    </div>
                </div>
            </div>

            <script> document.getElementById("JSOK").style.display = "block";</script>

        </form>
    </div>


</body>
</html>
