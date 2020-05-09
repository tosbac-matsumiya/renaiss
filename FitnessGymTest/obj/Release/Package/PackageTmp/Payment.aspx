<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Payment.aspx.vb" Inherits="FitnessGymTest.Payment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="Content/css/StyleSheet1.css" />
    <link href="css/font-awesome.min.css" rel="stylesheet"/>
    <link href="css/main.css" rel="stylesheet"/>
    <link href="css/webentry.css" rel="stylesheet"/>
    <style type="text/css">
        body {
            background-image: url(Content/img/wall.jpg); 
            margin: 0;
            padding: 0;
        }
        h3{
            padding: 5px 0;
            margin: 0;
        }

        .iframe-content {
           position: relative;
         width: 100%;
            padding: 75% 0 0 0;
        }
        .iframe-content iframe {
            position: absolute;
            top: 0;
            left: 0;
            max-width: 440px;
            width: 100%;
            height: 440px;
        }

    </style>

    <script type="text/javascript">
        var sum = '<%=Session("celsum")%>'    //消す　Sessionをjs変数に引き渡す
    </script>

</head>
<body>

    <div class="webtitle">
        <div class="container webwidth">
            <h3><strong>安全な決済について</strong></h3>
        </div>
    </div>

    <div class="container webwidth" >

        <form action="https://www.sandbox.paypal.com/cgi-bin/webscr" method="post" target="_top" runat="server">
         <div class="sample1 jumbotron">
             <%--<h1>安全な決済について</h1>--%>
             <h2>お客様の決済金額は<asp:Label ID="lblSum" runat="server" Text="〇〇" Font-Underline="True" ForeColor="Red"></asp:Label>円です。</h2>
             <div>お支払い方法はPayPal決済とクレジットカード決済からお選びいただけます。</div>
         </div>

        </form>

<!--ここからカード決済ユニット。valueにセッションの値を渡すものと渡さないものがある。name要素は変更したら死ぬ-->
        <div class="pannel panel-primary">
        
            <div class="iframe-content">
                <iframe name="hss_iframe"></iframe><%-- width="440" height="440"--%>
                <form style="display: none" target="hss_iframe" name="form_iframe" method="post" action="https://securepayments.paypal.com/webapp/HostedSoleSolutionApp/webflow/sparta/hostedSoleSolutionProcess">
                    <input type="hidden" name="cmd" value="_hosted-payment" /><!--変数なし-->
                    <input type="hidden" name="subtotal" value='<%=Session("celsum")%>' /><!--合計金額。セッションで渡してください-->
                    <input type="hidden" name="business" value="TRT7PCDHJRCKE" /><!--セキュアキー。変更しないでね-->
                    <input type="hidden" name="paymentaction" value="sale" /><!--カードポジション。変更しないでね-->
                    <input type="hidden" name="currency_code" value="JPY" /><!--決済通貨。SandBoxだとUSDしか機能しないよ。本番はJPYへ-->
                    <input type="hidden" name="billing_first_name" value='<%=Session("name_kanafirst")%>' /><!--お客様の名。セッションで渡してね-->
                    <input type="hidden" name="billing_last_name" value='<%=Session("name_kanalast")%>' /><!--お客様の氏。セッションで渡してね-->
                    <input type="hidden" name="billing_zip" value='<%=Session("zipcode3-4")%>' /><!--お客様の郵便番号。セッションで渡してね-->
                    <input type="hidden" name="billing_country" value="JP" /><!--お客様のカード登録国。固定してもいいけど、本当はフォームから取得してセッションで渡すべき-->
                    <input type="hidden" name="billing_state" value='<%=Session("address0")%>' /><!--お客様の都道県。セッションで渡してね-->
                    <input type="hidden" name="billing_city" value='<%=Session("address1")%>' /><!--お客様の市町村。セッションで渡してね-->
                    <input type="hidden" name="billing_address1" value='<%=Session("address2+3")%>' /><!--お客様の市町村以下の住所。セッションで渡してね-->
                    <input type="hidden" name="night_phone_a" value="81" /><!--お客様の電話番語句国別コード。決裁上必須ではない-->
                    <input type="hidden" name="night_phone_b" value='<%=Session("phonenumber")%>' /><!--お客様の電話番号。決裁上必須ではない-->
                    <input type="hidden" name="email" value='<%=Session("email")%>' /><!--お客様のメールアドレス。決裁上必須ではない-->
                    <input type="hidden" name="template" value="templateD" /><!--Paypalに引き渡す変数。固定で頼む-->
                    <input type="hidden" name="return" value="https://renaiss-welcome.azurewebsites.net/ThankyouForPayment.aspx" /><!--決済後の戻りページ-->
                </form>
            </div>

        <div class="col-lg-2"></div>
    </div>
    </div>

    <script type="text/javascript">
        document.form_iframe.submit();

        //-- ADD_start---2017/09/27 TOS163
        // ブラウザ戻るボタンを制御
        history.pushState(null, null, null);
        window.addEventListener("popstate", function () {
            history.pushState(null, null, null);
        });
        //-- ADD_end-----2017/09/27 TOS163 

    </script>

    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/jquery-1.9.1.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/bootstrap.js"></script>
</body>
</html>
