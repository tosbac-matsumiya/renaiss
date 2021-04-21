<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="tokuteisyou.aspx.vb" Inherits="FitnessGymTest.tokuteisyou" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ルネッス</title>
    <!-- Bootstrap core CSS -->
    <link href="css/bootstrap.css" rel="stylesheet"/>
    <link href="css/font-awesome.min.css" rel="stylesheet"/>
    <!-- Custom styles for this template -->
    <link href="css/main.css" rel="stylesheet"/>
    <link href="css/webentry.css" rel="stylesheet"/>
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>

<style type="text/css">

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
        width = 50
    }


    </style>
</head>
<body>
    
    <form id="form1" runat="server">
     <div class="container webwidth2" style="padding-top: 50px;">
        <p>   <span>特定商取引法に基づく表記<span lang="EN-JP"><o:p></o:p></span></span></p>
            

        <table class ="space0">
            <tbody>
                <tr>
                    <td >販売業者</td>
                    <td>株式会社エル・ローズ</td>
                </tr>
                <tr>
                    <td>運営統括責任者氏名</td>
                    <td>福田　忠義</td>
                </tr>

                <tr>
                    <td>所在地・連絡先</td>
                    <td>〒910-0033<br />
                        福井県福井市三郎丸4-200<br />
                        TEL：0776-27-3131　／　FAX：0776-27-3130<br />
                    </td>
                </tr>

                <tr>
                    <td>ホームページ</td>
                    <td>http://www.sports-renaiss.jp/</td>
                </tr>

                <tr>
                    <td>販売価格</td>
                    <td>各店舗の料金案内をご確認ください</td>
                </tr>

                <tr>
                    <td>支払方法</td>
                    <td>クレジットカード決済</td>
                </tr>

                <tr>
                    <td>利用開始日</td>
                    <td>申込当月の任意の日または申込翌月1日</td>
                </tr>

                <tr>
                    <td>申込の撤回について</td>
                    <td>本人確認書類(※)をご持参のうえ、ご利用開始日の前営業日まで<br />
                        に施設で直接手続きをお願い致します。<br />
                        ※本人確認書類は運転免許証、保険証、パスポート、住民基本台帳カード、<br />
                        住民票写し、在留カード、特別永住者証明書、身体障害者手帳、<br />
                        マイナンバーカード（顔写真付き）のいずれか<br />
                        　尚、入会申し込みを頂き、お客様都合による申込の撤回については、<br />
                        キャンセル料2,000円（税抜）を申し受けます。
                    </td>
                </tr>

                <tr>
                    <td>営業時間・休館日</td>
                    <td>各店舗のページをご確認ください</td>
                </tr>
  
            </tbody>
        </table>


        <p class="text-center" style="margin-top: 20px;">
            <%--<asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Registration_1.aspx">トップへ戻る</asp:HyperLink>--%>
        </p>
    </div>
  </form>
</body>
</html>
