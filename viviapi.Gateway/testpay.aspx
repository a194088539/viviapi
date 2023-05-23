<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testpay.aspx.cs" Inherits="viviapi.gateway.testpay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>收银台</title>
    <link id="linkWebCss" href="/App_Themes/Cashier/Web.min.css" rel="stylesheet" />
    <link id="linkWeixinCss" href="/App_Themes/Cashier/Weixin.css" rel="stylesheet" />
    <link id="linkPaymentDialogCss" href="/App_Themes/Cashier/PaymentDialog.css" rel="stylesheet" />
    <link href="/static/demo/css/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/static/demo/js/jquery-1.7.2.js"></script>
    <script type="text/javascript" src="/static/demo/js/zzsc.js"></script>
    <meta name="keywords" content="" />
</head>
<body>
    <form id="payfrom" action="PayInterface.aspx" method="post" target="_blank">
    <input type="hidden" name="bankCardType" value="00" />
	<!-- 
    <input type="text" placeholder="输入金额" name="totalAmount" value="1.0" />
	 -->
    <div id="divTitle" class="Header">
        <div class="Wrap1000">
            <div class="Logo">
                <span></span>
            </div>
        </div>
    </div>
    <div id="divLine" style="border-bottom: 3px solid #A2AABB; margin-top: 10px;">
    </div>
    <div class="w1280">
        <div class="pay">
            <div class="title">
                <span>支付号：测试支付 </span><span>商户：测试支付</span><span>商品名称：充值体验</span><span>金额：<em><input type="text" name="totalAmount" value="10.0" /></em>元</span></div>
            <div class="bank">
                <div class="demo1">
                    <ul class="tab_menu">
                        <li class="current">网银类</li>
                        <li>第三方</li>
                        <li>点卡类</li>
                    </ul>
                    <div class="tab_box">
                        <div class="">
                            <div class="zhifu">
                                <label>
                                    <input type="radio" name="bankCode" id="b_abc" value="964" checked="checked" /><img src="/static/demo/images/bank/ABC.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_boc" value="963" /><img src="/static/demo/images/bank/BOC.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_ccb" value="965" /><img src="/static/demo/images/bank/CCB.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_icbc" value="967" /><img src="/static/demo/images/bank/ICBC.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_cmb" value="970" /><img src="/static/demo/images/bank/cmb.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_psbc" value="971" /><img src="/static/demo/images/bank/psbc.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_bcom" value="981" /><img src="/static/demo/images/bank/bcom.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_spdb" value="977" /><img src="/static/demo/images/bank/spdb.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_cib" value="972" /><img src="/static/demo/images/bank/cib.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_citic" value="962" /><img src="/static/demo/images/bank/ECITIC.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_gdb" value="985" /><img src="/static/demo/images/bank/gdb.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_pab" value="978" /><img src="/static/demo/images/bank/pab.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_shb" value="975" /><img src="/static/demo/images/bank/shb.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_ceb" value="986" /><img src="/static/demo/images/bank/cebb.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_cmbc" value="980" /><img src="/static/demo/images/bank/cmbc.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_hxb" value="982" /><img src="/static/demo/images/bank/hxb.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_bob" value="989" /><img src="/static/demo/images/bank/bob.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_nbcb" value="998" /><img src="/static/demo/images/bank/nbb.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_hzb" value="983" /><img src="/static/demo/images/bank/hzb.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_wapbank" value="1005" /><img src="/static/demo/images/bank/wapbankpay.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_kjbank" value="1000" /><img src="/static/demo/images/bank/kjbankpay.png" width="144" height="44" />
                                </label>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="qita">
                            </div>
                            <div class="car">
                                付款卡种：<label><input type="radio" name="cardType" id="cardType1" value="radio" />储蓄卡</label>
                                          <label><input type="radio" name="cardType" id="cardType2" value="radio" />信用卡</label>
                            </div>
                            <div class="btn">
                                <a href="javascript:sub()">下一步</a></div>
                        </div>

                        <div class="hide">
                            <div class="zhifu">
                            <!-- 支付宝 -->
                                <label>
                                    <input type="radio" name="bankCode" id="b_3fa" value="992" checked="checked"/>扫码<img src="/static/demo/images/bank/zfb.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_3fb" value="1006" />WAP<img src="/static/demo/images/bank/zfb.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_3fc" value="2007" />APP<img src="/static/demo/images/bank/zfb.png" width="144" height="44" />
                                </label>
                            <!-- 京东 -->
                                <label>
                                    <input type="radio" name="bankCode" id="b_3fj" value="2001" />扫码<img src="/static/demo/images/bank/jdpay.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_3fk" value="2002" />H5<img src="/static/demo/images/bank/jdpay.png" width="144" height="44" />
                                </label>
                            <!-- 微信 -->
                                <label>
                                    <input type="radio" name="bankCode" id="b_3fd" value="1004" />扫码<img src="/static/demo/images/bank/wxpay.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_3fe" value="1007" />WAP<img src="/static/demo/images/bank/wxpay.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_3ff" value="2005" />APP<img src="/static/demo/images/bank/wxpay.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_3f2" value="2005" />JSAPI<img src="/static/demo/images/bank/wxpay.png" width="144" height="44" />
                                </label>
                            <!-- 银联 -->
                                <label>
                                    <input type="radio" name="bankCode" id="b_3fl" value="2003" />扫码<img src="/static/demo/images/bank/ylpay.png" width="144" height="44" />
                                </label>
                            <!-- 百度 -->
                                <label>
                                    <input type="radio" name="bankCode" id="b_3fm" value="2006" />扫码<img src="/static/demo/images/bank/bdpay.png" width="144" height="44" />
                                </label>
                            <!-- QQ -->
                                <label>
                                    <input type="radio" name="bankCode" id="b_3fg" value="1008" />扫码<img src="/static/demo/images/bank/qqpay.png" width="144" height="44" />
                                </label>
                                <label>
                                    <input type="radio" name="bankCode" id="b_3fh" value="1009" />H5<img src="/static/demo/images/bank/qqpay.png" width="144" height="44" />
                                </label>
                            </div>

                            <div class="btn">
                                <a href="javascript:sub()">下一步</a></div>
                        </div>
												
                        <div class="hide">
                            <div class="zhifu">
                                <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" style="border-spacing: 0;">
                                    <tr height="30px">
                                        <td>
                                            充值金额(元)
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;<input size="50" type="text" name="facevalue" id="facevalue" value="100" />&nbsp;<span style="color: #FF0000; font-weight: 100;">*</span>
                                        </td>
                                    </tr>
                                    <tr id="trcardNum" height="30px">
                                        <td>
                                            卡号
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;<input size="50" type="text" name="cardNo" id="cardNo" value="" />&nbsp;<span style="color: #FF0000; font-weight: 100;">*</span>
                                        </td>
                                    </tr>
                                    <tr id="trcardPwd" height="30px">
                                        <td>
                                            卡密
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;<input size="50" type="text" name="cardPwd" id="cardPwd" value="" />&nbsp;<span style="color: #FF0000; font-weight: 100;">*</span>
                                        </td>
                                    </tr>
                                </table>
                                <div style="width:100%;float:left;">
                                    <div class="ra-img2" style="width:200px;height:44px;margin-top:20px;float:left;">
                                        <input type="radio" name="Channel" id="Radio10" value="12">
                                        <img src="images/dianka/DXGK.png" />
                                    </div>
                                    <div class="ra-img2" style="width:200px;height:44px;margin-top:20px;float:left;">
                                        <input type="radio" name="Channel" id="Radio11" value="13">
                                        <img src="images/dianka/YDSZX.png" />
                                    </div>
                                    <div class="ra-img2" style="width:200px;height:44px;margin-top:20px;float:left;">
                                        <input type="radio" name="Channel" id="Radio12" value="14">
                                        <img src="images/dianka/LTYKT.png" />
                                    </div>
                                    <div class="ra-img2" style="width:200px;height:44px;margin-top:20px;float:left;">
                                        <input type="radio" name="Channel" id="Radio5" value="6">
                                        <img src="images/dianka/SHYKT.png" />
                                    </div>
                                    <div class="ra-img2" style="width:200px;height:44px;margin-top:20px;float:left;">
                                        <input type="radio" name="Channel" id="Radio6" value="7">
                                        <img src="images/dianka/ZTYKT.png" />
                                    </div>
                                    <div class="ra-img2" style="width:200px;height:44px;margin-top:20px;float:left;">
                                        <input type="radio" name="Channel" id="Radio8" value="8">
                                        <img src="images/dianka/JYYKT.png" />
                                    </div>
                                    <div class="ra-img2" style="width:200px;height:44px;margin-top:20px;float:left;">
                                        <input type="radio" name="Channel" id="Radio9" value="9">
                                        <img src="images/dianka/WYYKT.png" />
                                    </div>
                                    <div class="ra-img2" style="width:200px;height:44px;margin-top:20px;float:left;">
                                        <input type="radio" name="Channel" id="Channel1" value="1">
                                        <img src="images/dianka/QBCZK.png" />
                                    </div>
                                    <div class="ra-img2" style="width:200px;height:44px;margin-top:20px;float:left;">
                                        <input type="radio" name="Channel" id="Radio2" value="2">
                                        <img src="images/dianka/SDYKT.png" />
                                    </div>
                                    <div class="ra-img2" style="width:200px;height:44px;margin-top:20px;float:left;">
                                        <input type="radio" name="Channel" id="Radio3" value="3">
                                        <img src="images/dianka/JWYKT.png" />
                                    </div>
                                    <div class="ra-img2" style="width:200px;height:44px;margin-top:20px;float:left;">
                                        <input type="radio" name="Channel" id="Radio4" value="5">
                                        <img src="images/dianka/WMYKT.png" />
                                    </div>
                                    <div class="ra-img2" style="width:200px;height:44px;margin-top:20px;float:left;">
                                        <input type="radio" name="Channel" id="Radio17" value="22">
                                        <img src="images/dianka/THYKT.png" />
                                    </div>
                                    <div class="ra-img2" style="width:200px;height:44px;margin-top:20px;float:left;">
                                        <input type="radio" name="Channel" id="Radio18" value="23">
                                        <img src="images/dianka/ZYYKT.png" />
                                    </div>
                                    <div class="ra-img2" style="width:200px;height:44px;margin-top:20px;float:left;">
                                        <input type="radio" name="Channel" id="Radio20" value="28">
                                        <img src="images/dianka/SFYKT.png" />
                                    </div>
                                    <div class="ra-img2" style="width:200px;height:44px;margin-top:20px;float:left;">
                                        <input type="radio" name="Channel" id="Radio16" value="21">
                                        <img src="images/dianka/TXYKT.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="btn">
                                <a href="javascript:sub()">下一步</a></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
    <script src="/static/demo/js/jquery.tabs.js"></script>
    <script>
        $(function () {
            //$('.demo1').Tabs({
            //	event:'click',
            //	switchBtn : true
            //});

            function tabcallback() {
                var index = $(".current").index();
                if (index == 0) {
                    $("#b_abc").attr("checked", true);
                    $("#cardType1").attr("checked", true);
                }
                else if (index == 1) {
                    $("#b_3f").attr("checked", true);
                }
/*
                if (index == 3) {
                    $("#bankCardType").val("01");
                    $("#Channel1").attr("checked", true);
                } else {
                    $("#bankCardType").val("00");
                }

                if (index == 4) {
                    $("#bankCardType").val("01");
                    $("#Channel1").attr("checked", true);
                } else {
                    $("#bankCardType").val("00");
                }
*/
            }

            $('.demo1').Tabs({
                event: 'click',
                callback: tabcallback
            });


            $(".tab_menu > li").onclick(function (e) {
                // var index = $.inArray(this, tabs);
                alert("ok");
            });
        });

        function sub() {
            var index = $(".current").index();
            if (index == 8) {//
                var cardno = $("#cardNo").val();
                if (!cardno) {
                    alert("请输入卡号");
                    return;
                }
                var cardPwd = $("#cardPwd").val();
                if (!cardPwd) {
                    alert("请输入卡密");
                    return;
                }
                var facevalue = $("#facevalue").val();
                if (!facevalue) {
                    alert("请输入面值");
                    return;
                }
            }
            $("#payfrom").submit();
        }
    </script>
    </form>
    <div id="divFooter" class="Footer" style="margin-top: 100px;">
        <div class="ClearFloat">
        </div>
        <div class="Footer_part2">
            <div style="border: 1px solid #CFCFCF;">
            </div>
            <div class="footerD">
            </div>
        </div>
    </div>
</body>
</html>
