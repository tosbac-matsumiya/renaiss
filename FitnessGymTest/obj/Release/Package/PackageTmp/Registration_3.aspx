<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Registration_3.aspx.vb" Inherits="FitnessGymTest.Registration_3" %>

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
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>

   <style type="text/css">
      *.logbox
      {
         border: solid 1px #808080;
         width: 100%;
         max-width: 810px;
         height: 200px;
         padding: 5px 10px 1px;
         overflow: auto;
         background:#fff;
         border-radius: 5px;

      }
      .q  { color: #008000; }
   </style>

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
		      <li class="step visited">STEP 2 <small>お客様情報入力</small></li>
		      <li class="step current">STEP 3 <small>登録内容確認</small></li>
		    </ol>
        </nav>
        <form id="form1" runat="server">

            <div class="backcolor_w">
                <div class="kakunin">
                    
                   <div style="text-align: center;">
                       <asp:Label ID="lblItido" runat="server" Text="" Visible="false">一度登録された可能性があります。店舗にお問い合わせください。</asp:Label> 
                       <br /><asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/fitnessindex.aspx" Visible="false" >トップへ戻る</asp:HyperLink>
                   </div>
                    
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-3 text-center">
                                <div class="item">入会店舗</div>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lblFacilityName" runat="server" Text="Label"></asp:Label>
                            </div>
                            <div class="col-sm-3 text-center">
                                <div class="item">コース</div>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lblPlanName" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-3 text-center">
                                <div class="item">オプション名</div>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lblOptionName" runat="server" Text="Label"></asp:Label>
                            </div>
                            <div class="col-sm-3 text-center">
                                <div class="item">来店日付</div>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lblDateVisit" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-3 text-center">
                                <div class="item">決済金額</div>
                            </div>
                            <div class="col-sm-6">
                                <asp:Table ID="Table1" runat="server" Width="100%" class="table table-condensed">
                                    <asp:TableRow runat="server" Font-Size="Smaller" HorizontalAlign="Center">
                                        <asp:TableCell runat="server">項目</asp:TableCell>
                                        <asp:TableCell runat="server">価格</asp:TableCell>
                                        <%--<asp:TableCell runat="server">割引</asp:TableCell>--%>
                                        <asp:TableCell runat="server">備考</asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server" Font-Size="Smaller">
                                        <asp:TableCell runat="server">入会金</asp:TableCell>
                                        <asp:TableCell ID="celAdmissionFee" runat="server" HorizontalAlign="Right"></asp:TableCell>
                                        <%--<asp:TableCell ID="celWaribiki_A" runat="server"></asp:TableCell>--%>
                                        <asp:TableCell ID="celWaribikiName_A" runat="server" Font-Size="Smaller" ForeColor="Red" HorizontalAlign="Center"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server" Font-Size="Smaller">
                                        <asp:TableCell runat="server">登録手数料</asp:TableCell>
                                        <asp:TableCell ID="celRegistrationFee" runat="server" HorizontalAlign="Right"></asp:TableCell>
                                        <%--<asp:TableCell ID="celWaribiki_R" runat="server"></asp:TableCell>--%>
                                        <asp:TableCell ID="celWaribikiName_R" runat="server" Font-Size="Smaller" ForeColor="Red" HorizontalAlign="Center"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server" Font-Size="Smaller">
                                        <asp:TableCell ID="celMonth1" runat="server"></asp:TableCell>
                                        <asp:TableCell ID="celMonthlyDues1" runat="server" HorizontalAlign="Right"></asp:TableCell>
                                        <%--<asp:TableCell ID="celWaribiki_M1" runat="server"></asp:TableCell>--%>
                                        <asp:TableCell ID="celWaribikiName_M1" runat="server" Font-Size="Smaller" ForeColor="Red" HorizontalAlign="Center"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server" Font-Size="Smaller" Visible="false">
                                        <asp:TableCell runat="server">オプション</asp:TableCell>
                                        <asp:TableCell ID="celMonthlyDues1_O" runat="server" HorizontalAlign="Right"></asp:TableCell>
                                        <%--<asp:TableCell ID="celWaribiki_O" runat="server"></asp:TableCell>--%>
                                        <asp:TableCell ID="celWaribikiName_O" runat="server" Font-Size="Smaller" ForeColor="Red" HorizontalAlign="Center"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server" Font-Size="Smaller">
                                        <asp:TableCell ID="celMonth2" runat="server"></asp:TableCell>
                                        <asp:TableCell ID="celMonthlyDues2" runat="server" HorizontalAlign="Right"></asp:TableCell>
                                        <%--<asp:TableCell ID="celWaribiki_M2" runat="server"></asp:TableCell>--%>
                                        <asp:TableCell ID="celWaribikiName_M2" runat="server" Font-Size="Smaller" ForeColor="Red" HorizontalAlign="Center"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server" Font-Size="Smaller" Visible="false">
                                        <asp:TableCell runat="server">オプション</asp:TableCell>
                                        <asp:TableCell ID="celMonthlyDues2_O" runat="server" HorizontalAlign="Right"></asp:TableCell>
                                        <%--<asp:TableCell ID="celWaribiki_O2" runat="server"></asp:TableCell>--%>
                                        <asp:TableCell ID="celWaribikiName_O2" runat="server" Font-Size="Smaller" ForeColor="Red" HorizontalAlign="Center"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server" Font-Size="Smaller" ID="cel3" Visible="false">
                                        <asp:TableCell ID="celMonth3" runat="server"></asp:TableCell>
                                        <asp:TableCell ID="celMonthlyDues3" runat="server" HorizontalAlign="Right"></asp:TableCell>
                                        <asp:TableCell ID="celWaribikiName_M3" runat="server" Font-Size="Smaller" ForeColor="Red" HorizontalAlign="Center"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server" Font-Size="Smaller" ID="cel3_O" Visible="false">
                                        <asp:TableCell runat="server">オプション</asp:TableCell>
                                        <asp:TableCell ID="celMonthlyDues3_O" runat="server" HorizontalAlign="Right"></asp:TableCell>
                                        <asp:TableCell ID="celWaribikiName_O3" runat="server" Font-Size="Smaller" ForeColor="Red" HorizontalAlign="Center"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow runat="server">
                                        <asp:TableCell ID="celsum_text" runat="server" ColumnSpan="2">初回お支払い金額</asp:TableCell>
                                        <asp:TableCell ID="celsum" runat="server" ColumnSpan="2" HorizontalAlign="Right"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </div>
                        </div>
                    </div>
                    <div class="text-center">
                        <asp:Button ID="btnReturn1" runat="server" Text="ご利用情報登録画面へ戻る" CssClass="btn btn-primary" />
                        <asp:Label ID="lblerr_Return1" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </div>
                </div>

                <div class="kakunin">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-3 text-center">
                                <div class="item">お名前</div>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lblNameKanji" runat="server" Text="Label"></asp:Label>
                            </div>
                            <div class="col-sm-3 text-center">
                                <div class="item">フリガナ</div>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lblNameKana" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-3 text-center">
                                <div class="item">性別</div>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lblSex" runat="server" Text="Label"></asp:Label>
                            </div>
                            <div class="col-sm-3 text-center">
                                <div class="item">生年月日</div>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lblBirthDay" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-3 text-center">
                                <div class="item">電話番号</div>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lblPhoneNumber" runat="server" Text="Label"></asp:Label>
                            </div>
                            <div class="col-sm-3 text-center">
                                <div class="item">職業</div>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lblJob" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-3 text-center">
                                <div class="item">住所</div>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblZipCode" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-9 col-sm-offset-3">
                                <asp:Label ID="lblAddress" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-3 text-center">
                                <div class="item">メールアドレス</div>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblEmail" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                    </div>
<!--消す
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-3 text-center">
                                <div class="item">お支払方法</div>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblPayment" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                    </div>
-->
                    <div class="form-group">
                        <div class="row">
                            <div class="col-sm-3 text-center">
                                <div class="item">会員証画像</div>
                            </div>
                            <div class="col-sm-9">
                                <div class="photocenter">
                                    <asp:Image ID="imgPhoto" runat="server"/>
                                </div>               
                            </div>
                        </div>
                    </div>
                    <div class="text-center">
                        <asp:Button ID="btnReturn2" runat="server" Text="会員情報登録画面へ戻る" CssClass="btn btn-primary"/>
                    </div>
                </div>

<!--消す            
                <asp:Panel ID="Panel1" runat="server" Height="100%" Visible="false">
                    <div class="kakunin">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-3 text-center">
                                    <div class="item">カードの種類</div>
                                </div>
                                <div class="col-sm-4">
                                    <asp:Label ID="lblCreditName" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 5px;">
                                <div class="col-sm-3 text-center">
                                    <div class="item">有効期限</div>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Label ID="lblCreditDate" runat="server" Text="Label"></asp:Label>
                                </div>
                                <div class="col-sm-3 text-center">
                                    <div class="item">カード番号</div>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Label ID="lblCreditNumber" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="text-center">
                        <asp:Button ID="btnReturn3" runat="server" Text="クレジット情報入力画面へ戻る" CssClass="btn btn-primary"/>
                        </div>
                    </div>
                </asp:Panel>
-->
            </div>

                        <div class="backcolor_w" style="margin-top:10px;" >
                <div class="logbox">
                <div class="p">規約の同意</div>
                 <div style="padding: 0px 10px; font-size: 15px;">
                    　重要事項をご確認いただき、該当項目にチェックをお願いします。<br />
                    株式会社エル・ローズ（以下、「会社」）が運営するスポーツクラブルネッス（以下「本クラブ」）では、<br />
                    会員の皆様に安全で快適にご利用頂く為に、ご入会にあたり下記の事項についてご確認をいただいております。<br />
                    下記事項をご確認の上、チェックを付けてください。<br />
                   
                    <div class="p">[1] 入会資格 </div>
                     <ul>
                        <li>
                            □16歳以上であり、18歳未満の場合には親権者の同意を得ています。
                        </li>
                        <li>
                            □下記の項目すべてに該当しません。</li>
                        </ul>
                        <ul>
                            <li>
                                ●運動を医師より禁止されている
                            </li>
                            <li>
	                            ●刺青・タトゥがある
                            </li>
                            <li>
	                            ●暴力団関係者
                            </li>
                            <li>
	                            ●薬物常用者
                            </li>
                            <li>
	                            ●妊娠している
                            </li>
                            <li>
	                            ●伝染及び感染する恐れのある疾患を有している
                            </li>
                        </ul> 
                        <ul>
                            <li>
                                □本クラブの定める規約等ルール、マナー等を守って他人に迷惑をかけることなく利用します。
                            </li>
                       </ul>
                    
                     <div class="p">[2] 会費のお支払いについて</div>
                     <ul>
                         <li>
                            □会費は、毎月指定日に翌月分を指定口座より自動振替いたします。</li>
                         <li>
                            □原則として一旦納入した入会金、会費及び諸費用は返還しません。</li>
                         <li>
                            □施設の利用がない場合であっても、会費の支払い義務が発生します。</li>
                         <li>
                            □会社に対し、会費の支払いを3ヵ月以上滞納した場合は除名となることを確認しました。</li>
                     </ul>
                     <div class="p">[3] 各種お手続</div>
                     <ul>
                         <li>
                            □各種届出（変更・退会）については、本人が直接フロントで手続をします。<br />（代理人、電話、FAX、口頭、メール等での受付は行っておりません）</li>
                         <li>
                            □各種手続をする場合は、毎月10日（10日が休館日の場合は、前営業日）までに所定の用紙を提出します。<br />内容によっては費用が発生する場合があります。</li>
                     </ul>

                    <div class="p"> [4] 駐車場利用について</div>
                     <ul>
                         <li>
                            □駐車場は本クラブ利用時のみ利用できます。<br/>
                            利用が混雑する曜日や時間帯など、満車時はお待ちいただく場合があります。
                         </li>
                     </ul>

                     <div class="p">[5] 解約について</div>
                     <ul>
                         <li>
                             □入会日前の解約申出の場合に限り、支払った全額を返金します。                        
                         </li>
                         <li>
                            □その場合、解約に伴う諸手数料（解約手数料）として2,000円（税別）を別途申し受けます。
                         </li>

                     </ul>


                     <div class="p">[6] 免責</div>
                     <ul>
                         <li>
                             □本クラブ内及び駐車場内で発生した人的、物的事故並びに盗難、紛失及びその他の事故については、<br />
                             会社に故意又は重大な過失がある場合を除き、会社に対し責任を追求しません。
                         </li>

                     </ul>


                 </div>           
                </div>
            </div>


            <div class="text-center">
                <div class="chkbox">
                    <asp:CheckBox ID="chkDoisyo2" runat="server" />
	                <label for="chkDoisyo2">すべて同意します。</label>
                </div>
                <asp:Label ID="lblerr_chkdoisyo2" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>


            <div class="backcolor_w" style="margin-top:10px;">
                <div class="logbox"  >
                <div class="p">個人情報保護指針</div>
                 <div style="padding: 0px 10px; font-size: 15px;">
                     株式会社エル・ローズが運営するスポーツクラブルネッスでは、皆様のプライバシーを尊重し、個人情報を保護する<br />
                     ために細心の注意を払っています。このプライバシーポリシーでは、スポーツクラブルネッスのWEB入会フォーム内で、<br />
                     皆様の個人情報をどのように取り扱うかについての基本的な方針をお知らせします。<br />
                     下記の事項をご確認の上、チェックを付けてください。<br /><br />
                      <div>[1] 個人情報の管理について </div>
                     スポーツクラブルネッスは、プライバシー尊重の観点から、個人情報は保護すべき重要な情報であると認識し、<br />
                     お客様の個人情報を収集した際には、厳重に管理いたします。スポーツクラブルネッスは、個人情報への不正アクセス、<br />
                     紛失、破壊、改ざんおよび漏洩などに関し予防措置を講ずるとともに、万一の発生時には速やかな是正措置を実施いたします。<br /><br />

                     <div class="p">[2] 情報の収集について </div>
                     スポーツクラブルネッスは、必要に応じてお客様のお名前、住所、メールアドレス、その他の個人情報のご提供を任意に<br />
                     お願いする場合があります。スポーツクラブルネッスがお尋ねする個人情報は、当社のサービスをご利用いただくため、<br />
                     及び当社より発信する情報を提供するために必要なものに限られています。<br />
                     <%-- ADD_start---2018/06/04 TOS163 --%>
                     また、本サイトにおいてはサービス向上のためGoogle, Inc.のGoogle Analyticsを利用してサイトの計測を行っております。
                     これに付随して生成されるテキストファイル「Cookie」を通じて分析を行うことがありますが、この際、IPアドレス等の
                     ユーザ様情報の一部が、Google, Inc.に収集される可能性があります。サイト利用状況の分析、サイト運営者へのレポートの作成、
                     その他のサービスの提供目的に限りこれを使用します。利用者は、本サイトを利用することで、上記方法および目的において
                     Googleが行うこうしたデータ処理につき許可を与えたものとみなします。<br />
                     ※なお、「Cookie」は、ユーザー側のブラウザ操作により拒否することも可能です。ただしその際、本サイトの機能が一部利用できなくなる可能性があります。<br /><br />
                     <%-- ADD_end-----2018/06/04 TOS163 --%>

                    <div class="p">[3] 情報の利用</div>
                    スポーツクラブルネッスは、原則として下記以外の目的で個人情報は利用いたしません。<br />
                    （1）申込み内容の確認、対応、代金決済のため<br />
                    （2）新商品、新サービス、その他営業に関する案内のため<br />
                    （3）サービス向上に向けたマーケティング調査、アンケート調査のため<br />
                    （4）その他、円滑なクラブ運営に必要な範囲<br /><br />

                    <div class="p">[4] 情報の開示</div>
                    スポーツクラブルネッスは、正当な理由がある場合を除き、本人の同意なく第三者に情報提供、開示いたしません。<br />
                     ただし、あらかじめ当社との間で秘密保持契約を締結している業務委託先に提供する場合、裁判所、警察またはこれらに<br />
                     準じる公的機関より適法に開示を要請された場合、お客様の生命、身体または財産の保護のために緊急に必要がある場合は、<br />
                     この限りではありません。<br /><br />

                    <div class="p">[5] 通信の暗号化について</div>
                    このホームページでは、お客様情報をご提供いただく際には「SSL（Secure Socket Layer）」を使用し、お客様との通信を暗号化しております。<br />
                    SSL（Secure Socket Layer）とは、インターネット上の通信において、サーバとお客様とのPC間で機密性の高い情報を安全にやり取りするための<br />
                    セキュリティ機能付きのHTTPプロトコルです。このSSLにより、お客様がご入力いただいたデータを暗号化し、第三者に傍受されることを防いでいます。<br />
                    当社では、このSSLをWEB予約フォームにて使用しています。<br /><br />

                    <div class="p">[6] 見直し</div>
                    よりよくお客様の個人情報保護を図るため、及び法令等の変更に対応するために、個人情報保護方針を改定することがあります。<br /><br />

                    <div class="p">[7] お客様の個人情報についてのお問い合せ</div>
                    お客様ご自身の個人情報についての照会や修正、削除、利用停止等のご依頼などをお申し出いただく場合は、下記までお問い合せください。<br />
                    株式会社エル・ローズ<br />
                    〒910-0033<br />
                    福井県福井市三郎丸4-200<br />
                    TEL：0776-27-3131　／　FAX：0776-27-3130<br />

                    </div>           


                </div>
            </div>
            <div class="text-center">
                <div class="chkbox">
                    <asp:CheckBox ID="chkDoisyo1" runat="server" />
	                <label for="chkDoisyo1">確認しました。</label>
                </div>
                <asp:Label ID="lblerr_chkdoisyo1" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>


            <div class="backcolor_w" style="margin-top:10px;">
                <div class="doisyo">
                    <h3 class="text-center" style="margin-top:0px;">同 意 書</h3>
                    <div class="top">スポーツクラブルネッス　殿</div>
                    <div style="padding: 0px 10px; font-size: 15px;">
						私は貴施設規約に従うことを契約するとともに施設内において万一事故・その他異常が生じた場合、私の責任において一切意義申し立ていたしません。
						また、申し込み用紙に記入した個人情報については貴社情報管理に則り会員証の作成、指導のカルテ作成、連絡事項の案内等に使用されることに同意します。
					</div>
                    <div class="text-center">
                        <div class="chkbox">
                            <asp:CheckBox ID="chkDoisyo" runat="server" />
	                        <label for="chkDoisyo">同意します</label>
                        </div>
                        <asp:Label ID="lblerr_chkdoisyo" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="button-group">
                <%--<asp:TableCell runat="server">割引</asp:TableCell>--%>
                <asp:Button ID="BtnOK" runat="server" Text="この内容で申し込み、お支払い手続きへ進む" CssClass="btn2 edit-btn"/>
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
