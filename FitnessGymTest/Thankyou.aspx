<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Thankyou.aspx.vb" Inherits="FitnessGymTest.Thankyou" %>
<%@ Import Namespace="System.Web.Security" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>ルネッス-来店受付完了</title>
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
    <%--<div class="container webwidth2" style="padding-top: 60px;">--%>
    <div class="webtitle" style="padding-top: 50px;">
        <div class="container webwidth">
            <h4><strong>WEB入会完了</strong></h4>
        </div>
    </div>

    <div class="container webwidth">
        <%--<h4 style="background:#63c2ba;">お申し込みありがとうございました。</h4>--%>
        <h3>お申し込みありがとうございました。</h3>
        <%--<div>下記の通り受付いたしました。</div>--%>
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

                             <tr>
                                <td class="auto-style13">４．</td>
                                <td class="auto-style14">
                                    <div class="auto-style12">
                                        <div class="row">
                                            ご入会諸費用　<asp:Label ID="lblHiyou" runat="server" Text=""></asp:Label>    
                                        </div>
                                    </div>
                                </td>
                            </tr>
  
                        </tbody>
                    </table>

                </div>
            </div>

            <div style="margin-top: 20px;">
                初回来店日には以下の手続きを行います。
<%--                <div>１．口座振替用紙の記入・捺印（月会費の引落用）</div>
                <div>２．トレサポ利用登録</div>
                ※トレサポ＝ルネッスオリジナルの運動サポート--%>
                <ul>
                    <li>１．入会諸費用の精算</li>
                    <li>２．口座振替用紙の記入・捺印（月会費の引落用）</li>
                    <li>３．トレサポ利用登録</li>
                    <li>※トレサポ＝ルネッスオリジナルの運動サポート</li>
                    <li>４．会員証用顔写真および本人確認書類の撮影</li>
                </ul>
            </div>

            <div>
                <h1>ご来店、心よりお待ちしております。</h1>
            </div>


            <div class="button-group">
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
