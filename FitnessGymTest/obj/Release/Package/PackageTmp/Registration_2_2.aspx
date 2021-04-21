<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Registration_2_2.aspx.vb" Inherits="FitnessGymTest.Registration_2_2" %>

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
    <link href="css/cropper.css" rel="stylesheet" type="text/css" media="all" />
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <script src="https://ajaxzip3.github.io/ajaxzip3.js" charset="UTF-8"></script>
    <script src="scripts/cropper.min.js"></script>
</head>
<body>
    <div class="webtitle">
        <div class="container webwidth">
            <h4>WEB<strong>入会フォーム</strong></h4>
        </div>
    </div>

    <div class="container webwidth" >
        <nav style="margin: 5px 0px">
            <ol class="stepBar step3">
		      <li class="step visited">STEP 1 <small>ご利用情報入力</small></li>
		      <li class="step current">STEP 2 <small>お客様情報入力</small></li>
		      <li class="step">STEP 3 <small>登録内容確認</small></li>
		    </ol>
        </nav>
        <form id="form1" runat="server">

            <div class="backcolor_w">
                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-3">
                            <label>お名前</label>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtLastNameKanji" runat="server" CssClass="form-control" placeholder="姓"></asp:TextBox>
                            <asp:Label ID="lblerr_kanji" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtFirstNameKanji" runat="server" CssClass="form-control" placeholder="名"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-3">
                            <label>フリガナ</label>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtLastNameKana" runat="server" CssClass="form-control" placeholder="セイ"></asp:TextBox>
                            <asp:Label ID="lblerr_kana" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtFirstNameKana" runat="server" CssClass="form-control" placeholder="メイ"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-3">
                            <label>性別</label>
                        </div>
                        <div class="col-sm-8">
                            <asp:RadioButtonList ID="rblSex" runat="server" RepeatLayout="UnorderedList">
                                <asp:ListItem>男性</asp:ListItem>
                                <asp:ListItem>女性</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Label ID="lblerr_sex" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-3">
                            <label>生年月日</label>
                        </div>
                        <div class="col-sm-8">
                            <div class="row">
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlBirthYear" runat="server" CssClass="form-control">
                                        <asp:ListItem>1910</asp:ListItem>
                                        <asp:ListItem>1911</asp:ListItem>
                                        <asp:ListItem>1912</asp:ListItem>
                                        <asp:ListItem>1913</asp:ListItem>
                                        <asp:ListItem>1914</asp:ListItem>
                                        <asp:ListItem>1915</asp:ListItem>
                                        <asp:ListItem>1916</asp:ListItem>
                                        <asp:ListItem>1917</asp:ListItem>
                                        <asp:ListItem>1918</asp:ListItem>
                                        <asp:ListItem>1919</asp:ListItem>
                                        <asp:ListItem>1920</asp:ListItem>
                                        <asp:ListItem>1921</asp:ListItem>
                                        <asp:ListItem>1922</asp:ListItem>
                                        <asp:ListItem>1923</asp:ListItem>
                                        <asp:ListItem>1924</asp:ListItem>
                                        <asp:ListItem>1925</asp:ListItem>
                                        <asp:ListItem>1926</asp:ListItem>
                                        <asp:ListItem>1927</asp:ListItem>
                                        <asp:ListItem>1928</asp:ListItem>
                                        <asp:ListItem>1929</asp:ListItem>
                                        <asp:ListItem>1930</asp:ListItem>
                                        <asp:ListItem>1931</asp:ListItem>
                                        <asp:ListItem>1932</asp:ListItem>
                                        <asp:ListItem>1933</asp:ListItem>
                                        <asp:ListItem>1934</asp:ListItem>
                                        <asp:ListItem>1935</asp:ListItem>
                                        <asp:ListItem>1936</asp:ListItem>
                                        <asp:ListItem>1937</asp:ListItem>
                                        <asp:ListItem>1938</asp:ListItem>
                                        <asp:ListItem>1939</asp:ListItem>
                                        <asp:ListItem>1940</asp:ListItem>
                                        <asp:ListItem>1941</asp:ListItem>
                                        <asp:ListItem>1942</asp:ListItem>
                                        <asp:ListItem>1943</asp:ListItem>
                                        <asp:ListItem>1944</asp:ListItem>
                                        <asp:ListItem>1945</asp:ListItem>
                                        <asp:ListItem>1946</asp:ListItem>
                                        <asp:ListItem>1947</asp:ListItem>
                                        <asp:ListItem>1948</asp:ListItem>
                                        <asp:ListItem>1949</asp:ListItem>
                                        <asp:ListItem>1950</asp:ListItem>
                                        <asp:ListItem>1951</asp:ListItem>
                                        <asp:ListItem>1952</asp:ListItem>
                                        <asp:ListItem>1953</asp:ListItem>
                                        <asp:ListItem>1954</asp:ListItem>
                                        <asp:ListItem>1955</asp:ListItem>
                                        <asp:ListItem>1956</asp:ListItem>
                                        <asp:ListItem>1957</asp:ListItem>
                                        <asp:ListItem>1958</asp:ListItem>
                                        <asp:ListItem>1959</asp:ListItem>
                                        <asp:ListItem>1960</asp:ListItem>
                                        <asp:ListItem>1961</asp:ListItem>
                                        <asp:ListItem>1962</asp:ListItem>
                                        <asp:ListItem>1963</asp:ListItem>
                                        <asp:ListItem>1964</asp:ListItem>
                                        <asp:ListItem>1965</asp:ListItem>
                                        <asp:ListItem>1966</asp:ListItem>
                                        <asp:ListItem>1967</asp:ListItem>
                                        <asp:ListItem>1968</asp:ListItem>
                                        <asp:ListItem>1969</asp:ListItem>
                                        <asp:ListItem>1970</asp:ListItem>
                                        <asp:ListItem>1971</asp:ListItem>
                                        <asp:ListItem>1972</asp:ListItem>
                                        <asp:ListItem>1973</asp:ListItem>
                                        <asp:ListItem>1974</asp:ListItem>
                                        <asp:ListItem>1975</asp:ListItem>
                                        <asp:ListItem>1976</asp:ListItem>
                                        <asp:ListItem>1977</asp:ListItem>
                                        <asp:ListItem>1978</asp:ListItem>
                                        <asp:ListItem>1979</asp:ListItem>
                                        <asp:ListItem>1980</asp:ListItem>
                                        <asp:ListItem>1981</asp:ListItem>
                                        <asp:ListItem>1982</asp:ListItem>
                                        <asp:ListItem>1983</asp:ListItem>
                                        <asp:ListItem>1984</asp:ListItem>
                                        <asp:ListItem>1985</asp:ListItem>
                                        <asp:ListItem>1986</asp:ListItem>
                                        <asp:ListItem>1987</asp:ListItem>
                                        <asp:ListItem>1988</asp:ListItem>
                                        <asp:ListItem>1989</asp:ListItem>
                                        <asp:ListItem>1990</asp:ListItem>
                                        <asp:ListItem>1991</asp:ListItem>
                                        <asp:ListItem>1992</asp:ListItem>
                                        <asp:ListItem>1993</asp:ListItem>
                                        <asp:ListItem>1994</asp:ListItem>
                                        <asp:ListItem>1995</asp:ListItem>
                                        <asp:ListItem>1996</asp:ListItem>
                                        <asp:ListItem>1997</asp:ListItem>
                                        <asp:ListItem>1998</asp:ListItem>
                                        <asp:ListItem>1999</asp:ListItem>
                                        <asp:ListItem>2000</asp:ListItem>
                                        <asp:ListItem>2001</asp:ListItem>
                                        <asp:ListItem>2002</asp:ListItem>
                                        <asp:ListItem>2003</asp:ListItem>
                                        <asp:ListItem>2004</asp:ListItem>
                                        <asp:ListItem>2005</asp:ListItem>
                                        <asp:ListItem>2006</asp:ListItem>
                                        <asp:ListItem>2007</asp:ListItem>
                                        <asp:ListItem>2008</asp:ListItem>
                                        <asp:ListItem>2009</asp:ListItem>
                                        <asp:ListItem>2010</asp:ListItem>
                                        <asp:ListItem>2011</asp:ListItem>
                                        <asp:ListItem>2012</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1 text_position">
                                    年
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlBirthMonth" runat="server" CssClass="form-control">
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1 text_position">
                                    月
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlBirthDay" runat="server" CssClass="form-control">
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                        <asp:ListItem>24</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>26</asp:ListItem>
                                        <asp:ListItem>27</asp:ListItem>
                                        <asp:ListItem>28</asp:ListItem>
                                        <asp:ListItem>29</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>31</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1 text_position">
                                    日
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-3">
                            <label>職業</label>
                        </div>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddlJob" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1">自営業</asp:ListItem>
                                <asp:ListItem Value="2">会社員</asp:ListItem>
                                <asp:ListItem Value="3">無職</asp:ListItem>
                                <asp:ListItem Value="4">公務員</asp:ListItem>
                                <asp:ListItem Value="5">学生</asp:ListItem>
                                <asp:ListItem Value="6">パート</asp:ListItem>
                                <asp:ListItem Value="7">その他</asp:ListItem>
                                <asp:ListItem Value="8">主婦・主夫</asp:ListItem>
                                <asp:ListItem Value="9">会社役員</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-3">
                            <label>電話番号</label>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtPhone" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblerr_phone" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-sm-6">
                            <span class="text-danger">※ハイフン（-）なしで入力してください。</span>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-3">
                            <label>郵便番号</label>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtZipCode" runat="server" TextMode="Number" CssClass="form-control" maxlength="8" onkeyup="AjaxZip3.zip2addr(this,'','ddlPrefecture','txtAddress1');"></asp:TextBox>
                            <asp:Label ID="lblerr_zipcode" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-sm-6">
                            <span class="text-danger">※ハイフン（-）なしで入力してください。</span>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-3">
                            <label>住所</label>
                        </div>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddlPrefecture" runat="server" CssClass="form-control">
                                <asp:ListItem>選択して下さい</asp:ListItem>
                                <asp:ListItem>北海道</asp:ListItem>
                                <asp:ListItem>青森県</asp:ListItem>
                                <asp:ListItem>岩手県</asp:ListItem>
                                <asp:ListItem>宮城県</asp:ListItem>
                                <asp:ListItem>秋田県</asp:ListItem>
                                <asp:ListItem>山形県</asp:ListItem>
                                <asp:ListItem>福島県</asp:ListItem>
                                <asp:ListItem>茨城県</asp:ListItem>
                                <asp:ListItem>栃木県</asp:ListItem>
                                <asp:ListItem>群馬県</asp:ListItem>
                                <asp:ListItem>埼玉県</asp:ListItem>
                                <asp:ListItem>千葉県</asp:ListItem>
                                <asp:ListItem>東京都</asp:ListItem>
                                <asp:ListItem>神奈川県</asp:ListItem>
                                <asp:ListItem>新潟県</asp:ListItem>
                                <asp:ListItem>富山県</asp:ListItem>
                                <asp:ListItem>石川県</asp:ListItem>
                                <asp:ListItem>福井県</asp:ListItem>
                                <asp:ListItem>山梨県</asp:ListItem>
                                <asp:ListItem>長野県</asp:ListItem>
                                <asp:ListItem>岐阜県</asp:ListItem>
                                <asp:ListItem>静岡県</asp:ListItem>
                                <asp:ListItem>愛知県</asp:ListItem>
                                <asp:ListItem>三重県</asp:ListItem>
                                <asp:ListItem>滋賀県</asp:ListItem>
                                <asp:ListItem>京都府</asp:ListItem>
                                <asp:ListItem>大阪府</asp:ListItem>
                                <asp:ListItem>兵庫県</asp:ListItem>
                                <asp:ListItem>奈良県</asp:ListItem>
                                <asp:ListItem>和歌山県</asp:ListItem>
                                <asp:ListItem>鳥取県</asp:ListItem>
                                <asp:ListItem>島根県</asp:ListItem>
                                <asp:ListItem>岡山県</asp:ListItem>
                                <asp:ListItem>広島県</asp:ListItem>
                                <asp:ListItem>山口県</asp:ListItem>
                                <asp:ListItem>徳島県</asp:ListItem>
                                <asp:ListItem>香川県</asp:ListItem>
                                <asp:ListItem>愛媛県</asp:ListItem>
                                <asp:ListItem>高知県</asp:ListItem>
                                <asp:ListItem>福岡県</asp:ListItem>
                                <asp:ListItem>佐賀県</asp:ListItem>
                                <asp:ListItem>長崎県</asp:ListItem>
                                <asp:ListItem>熊本県</asp:ListItem>
                                <asp:ListItem>大分県</asp:ListItem>
                                <asp:ListItem>宮崎県</asp:ListItem>
                                <asp:ListItem>鹿児島県</asp:ListItem>
                                <asp:ListItem>沖縄県</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-4">
                            (都道府県)
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4 col-sm-offset-3">
                            <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            (市町村)
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4 col-sm-offset-3">
                            <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            (丁目・番地・号)
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4 col-sm-offset-3">
                            <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblerr_address" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-sm-4">
                            (建物名)
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row">
                        <div class="col-sm-3">
                            <label>メールアドレス</label>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4  col-sm-offset-3">
                            <asp:TextBox ID="txtEmail2" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblerr_email" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-sm-4">
                            (確認用)
                        </div>
                    </div>
                </div>
<!--必要かわからない
               <div class="form-group">
                    <div class="row">
                        <div class="col-sm-3">
                            <label>パスワード</label>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4  col-sm-offset-3">
                            <asp:TextBox ID="txtPassword2" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblerr_password" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-sm-4">
                            (確認用)
                        </div>
                    </div>
                </div>
-->
                <div class="form-group">
                    <h4>会員証用写真</h4>
                    <div class="row">
                        <div class="col-sm-6">
                            <%--<img src="#" class="img-responsive"/>--%><%--会員証イメージ画像--%>
                            <%--<img src="#" class="photo-crop-preview2"/>d2017/08/03_TOS163--%>
                            <asp:Image ID="imgPhoto" runat="server" CssClass="photo-crop-preview2" /><%--A2017/08/03_TOS163--%>
                        </div>
                        <div class="col-sm-6">
                            <div>会員証の完成イメージは左記の通りです。<br/>店頭での撮影する場合はそのままお進みください。</div>
                            <!-- <asp:RadioButtonList ID="rblPhoto" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">初回訪問時に撮影</asp:ListItem>
                                <asp:ListItem>お手持ちの画像をアップロード</asp:ListItem>
                            </asp:RadioButtonList> -->
                            <br/>
                            <!-- モーダルウィンドウを呼び出すボタン -->
                            <button type="button" id="img_enter" class="btn btn-primary" data-toggle="modal" data-target="#myModal">写真を登録する</button>
                            <button type="button" id="img_enter2" class="btn btn-warning" data-toggle="modal" data-target="#myModal" style="display: none;">再度選択・調整する</button>
                            <button type="button" id="img_delete" class="btn btn-denger">削除する</button><%--a2017/08/03_TOS163--%>
                        </div>
                    </div>
                </div>
                <!-- モーダルウィンドウの中身 -->
                <div class="modal fade" id="myModal" tabindex="-1">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title">写真を登録する
                                    <span>
                                        <asp:TextBox ID="fupPhoto2" runat="server" Visible="true" BackColor="White" BorderColor="White" BorderStyle="None" ForeColor="White" Height="0" Width="0"></asp:TextBox>
                                    </span>
                                    <!-- a2017/08/03_START_TOS163 -->
                                    <span>
                                        <asp:TextBox ID="fupPhoto3" runat="server" Visible="true" BackColor="White" BorderColor="White" BorderStyle="None" ForeColor="White" Height="0" Width="0"></asp:TextBox>
                                    </span>
                                    <!-- a2017/08/03_END_TOS163 -->
                                </h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-6 col-sm-7">
                                        <div class="photo-crop-preview-wrapper">
                                            <img class="photo-crop-preview"/>
                                        </div>
                                    </div>
                                    <div class="col-lg-5 col-sm-5">
                                        
                                        <div class="photo-crop-button" id="fupPhoto_2">
                                            <asp:FileUpload ID="FileUpload1" runat="server" class="photo-crop-button__file-select"/>
                                        </div>

                                        <h4>写真に関する注意事項</h4>

                                        <ul class="list-group">
                                            <li>正面から撮影されており、ご本人と確認できるもの</li>
                                            <li>複数人で写っていないもの</li>
                                            <li>サングラス、帽子等を着用していないもの</li>
                                            <li>アプリ等で加工されていないもの</li>
                                            <li>半年以内に撮影されたもの</li>
                                            <li>データサイズは最大8MBまで</li>
                                        </ul>
                                        <div style="font-size: small;">※現像写真及び写真データを店頭へ持ち込みいただいても会員証作成はできません。</div>
                                        <div class="border">
                                            <h4>写真調整</h4>
                                            <div>顔が枠の中心に来るようにサイズ・位置を調整してください。</div>
                                            <div>※写真をドラッグして位置を調整できます。</div>
                                            <div>※スクロールして拡大・縮小ができます。</div>
                                        </div>
                                        <%--<div id="holidaysc_web_entry_entry_photo">
                                            <input type="hidden" id="holidaysc_web_entry_entry_photo_photo" name="holidaysc_web_entry_entry_photo[photo]" />
                                        </div>--%>
                                        <div class="photo-crop-button" id="fupPhoto_1">
                                            <asp:FileUpload ID="fupPhoto" runat="server" class="photo-crop-button__file-select"/>
                                        </div>
                                        <div class="photo-crop-button--submit">
                                            <button id="OKbtn" class="btn btn-success">この写真に決定する</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary" data-dismiss="modal">閉じる</button>
                            </div>
                        </div>
                    </div>
                </div>
                </div>

           　 <div class="form-group">
                    <div class="row">
                        <div class="col-sm-3">
                            <label>お支払い方法</label>
                        </div>

                        <asp:Label ID="lblNebiki" runat="server" Text="" ForeColor="Red"></asp:Label>

                        <div class="col-sm-8">
                            <asp:RadioButtonList ID ="rblPayment" runat="server"  RepeatLayout="UnorderedList">
                                <asp:ListItem Text="クレジットカード　" Value="3"></asp:ListItem>
                                <asp:ListItem Text="現金支払い(初回ご来所時)" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Label ID="lblerr_Payment" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
            
            
            <div class="button-group">
                <asp:Button ID="btnNext" runat="server" Text="STEP 3　登録確認画面へもどる" CssClass="btn2 edit-btn" />
            </div>

        </form>

    </div>

    <script type="text/javascript">
        $(function () {
            var cropper;
            //var $photoInput = $('#holidaysc_web_entry_entry_photo_photo');
            var $photoInput = $('#fupPhoto2');
            var $imagePreview = $('.photo-crop-preview');
            var $imagePreview2 = $('.photo-crop-preview2');

            $('.photo-crop-button__file-select').change(function (e) {

                var file = this.files[0];

                if (file.type != 'image/jpeg') {
                    alert('jpeg画像のみアップロード可能です');

                } else if (file.size > 8388608) {
                    alert('画像は8MB以内のファイルを選択してください。'); //画像サイズﾁｪｯｸ ADD_2020/02/11 TOS163

                } else {

                    var reader = new FileReader();

                    reader.readAsDataURL(file);
                    reader.onload = (function (e) {

                        $imagePreview.attr('src', reader.result);

                        if (cropper) {
                            cropper.destroy();
                        }

                        cropper = new Cropper($imagePreview.get(0), {
                            autoCropArea: 0.65,
                            aspectRatio: 496 / 624,
                            viewMode: 1,
                            dragMode: 'move',
                            cropBoxResizable: false,
                            cropBoxMovable: false
                        });
                    })
                }
            });


            $('#OKbtn').click(function (e) {

                e.preventDefault();

                if (!cropper) {
                    return;
                }

                //切取後の画像データを取得
                var canvas = cropper.getCroppedCanvas({
                    //width: 496,
                    //height: 624
                    width: 93,
                    height: 117
                });

                //parent.$.fancybox.close();
                $('#myModal').modal('hide');//モーダルを閉じる

                var base64 = canvas.toDataURL('image/jpeg');
                $imagePreview2.attr('src', base64);
                $photoInput.val(base64);

                //<!-- a2017/08/03_START_TOS163 -- >
                // Base64からバイナリへ変換
                var bin = atob(base64.replace(/^.*,/, ''));
                var buffer = new Uint8Array(bin.length);
                for (var i = 0; i < bin.length; i++) {
                    buffer[i] = bin.charCodeAt(i);
                }
                $('#fupPhoto3').val(base64.replace(/^.*,/, ''));
                //<!-- a2017/08/03_END_TOS163 -- >


                $('#img_enter').css('display', 'none');
                //$('#img_enter2').css('display', 'block');//<!-- d2017/08/03_END_TOS163 -- >
                $('#img_enter2').css('display', 'inline-block');//<!-- a2017/08/03_END_TOS163 -- >
                $('#lblerr_imgPhoto').css('display', 'none'); //<!-- a2017/08/03_END_TOS163 -- >



                //postAjax(function () {
                alert('写真を登録しました');
                //});

                return false;
            });


            //$('.js-web-entry-photo-crop-remove-button').click(function (e) { //<!-- d2017/08/03_END_TOS163 -- >
            $('#img_delete').click(function (e) { //<!-- a2017/08/03_END_TOS163 -- >

                e.preventDefault();

                $photoInput.val("");
                $('#fupPhoto3').val(""); //<!-- a2017/08/03_END_TOS163 -- >
                $imagePreview2.attr('src', "") //<!-- a2017/08/03_END_TOS163 -- >
                //postAjax(function () {//<!-- d2017/08/03_END_TOS163 -- >
                alert("写真を削除しました");
                //});//<!-- d2017/08/03_END_TOS163 -- >
            });
        });

        //-- ADD_start---2017/09/27 TOS163
        // ブラウザ戻るボタンを制御
        history.pushState(null, null, null);
        window.addEventListener("popstate", function () {
            history.pushState(null, null, null);
        });
        //-- ADD_end-----2017/09/27 TOS163 


        //$('#rblPayment_0').click(function () {
        //    $('#btnNext').val("STEP 2　クレジット情報入力へ");
        //});

        //$('#rblPayment_1').click(function () {
        //    $('#btnNext').val("STEP 3　登録内容確認へ");
        //});


        //<!--a2017/12/15_TOS163 -->
        $(function () {
            if ($('#fupPhoto2').val() == "") {
            } else {
                $('.photo-crop-preview2').attr('src', $('#fupPhoto2').val());
            }
        });


    </script>

</body>
</html>
