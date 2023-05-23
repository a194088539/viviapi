<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankSelect.aspx.cs" Inherits="viviapi.gateway.StandardAPI.HuaQi.BankSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>银行充值</title>
    <script type="text/javascript" src="/js/common.js"></script>
    <link rel="stylesheet" type="text/css" href="/images/css.css" />
    <style type="text/css">
        .fm-explain{color:gray;background-position:-125px -419px;}
        .fm-explain1{color:blue;background-position:-125px -419px;}
        .fm-explain2{color:green;background-position:-125px -419px;}
        .fm-explain3{color:#F00;background-position:-125px -419px;}
    </style>
    <script type="text/javascript">
      $(document).ready(function() {
          $("input:[name=pd_FrpId]:radio").each(function() {
              if (this.value == $("#hftypeid").val()) {
                  this.checked = true;
              }
          });
      });

      function chkit(elm) {
          var bank = document.getElementById(elm);
          bank.checked = "checked";
          //alert(bank.value);
      } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hftypeid" runat="server" />
        <div id="main">
        <div id="Div1" class="top_title">
            <div id="Div2" class="top_title_left">
                <img alt="" src="/images/top_title_left.jpg" /></div>
            <div id="Div3" class="top_title_middle">
                <ul>
                    <li class="top_title_middle_wz"><a>1.选择付款银行</a></li>
                    <li class="top_title_middle_ico">
                        <img alt="" src="/images/top_title_ico.jpg" /></li>
                    <li class="top_title_middle_wz_x">2.付款</li>
                    <li class="top_title_middle_ico">
                        <img alt="" src="/images/top_title_ico.jpg" /></li>
                    <li class="top_title_middle_wz">3.付款完成</li>
                </ul>
            </div>
            <div id="Div4" class="top_title_right">
                <img alt="" src="/images/top_title_right.jpg" /></div>
        </div>
        <div id="Div5" class="main_list">
            <table width="920" border="0" cellspacing="0" cellpadding="5">
                <tr class="main_list_title">
                    <td width="400">
                        商户订单号
                    </td>
                    <td width="320">
                        系统流水号
                    </td>
                    <td width="200">
                        订单金额
                    </td>
                </tr>
                <tr class="main_list_nr">
                    <td class="main_list_mc">
                        <span id="lblorderid" runat="server"></span>
                    </td>
                    <td class="main_list_mc">
                        <span id="lblsysorderid" runat="server"></span>
                    </td>
                    <td class="main_list_jine">
                        <span id="lblordermoney" runat="server"></span>&nbsp; 元
                    </td>
                </tr>
                <tr style="height: 2px; background: #acacac; margin: 2px 0 0 0;">
                    <td height="2">
                    </td>
                    <td height="2">
                    </td>
                    <td height="2">
                    </td>
                </tr>
            </table>
        </div>
        <div id="Div7" class="main_ka">
            <div id="Div8" class="main_ka_title">
                <div id="Div9" class="main_ka_title_left">
                    <img alt="" src="/images/main_ka_title_left.jpg" /></div>
                <div id="Div10" class="main_ka_title_middle">
                    <ul>
                        <li class="main_ka_tishi">请您选择银行进行付款：</li>
                        <li id="l5" class="main_ka_zhifu"><a id="c5" class="ka_lei" href="javascript:__doPostBack('c5','')">
                            网银支付</a> </li>
                        <li id="l6" class="main_ka_zhifu" style="display: none"><a id="c6" href="javascript:__doPostBack('c6','')">
                            宝付账户支付</a></li></ul>
                </div>
                <div id="Div11" class="main_ka_title_right">
                    <img alt="" src="/images/main_ka_title_right.jpg" /></div>
            </div>
            <div id="Div12" class="main_ka_nr">
                <div id="Div13" class="main_ka_nr_title">
                    选择您的支付方式：</div>
                <div id="Div14" class="main_ka_nr_middle">
                    <ul id="UL1" runat="server" visible="false">
                        <li id="l_1002" class="yin_a0">
                            <input id="ccb1" name="pd_FrpId" type="radio" value="967" checked="checked" />
                            <img alt="中国工商银行" src="/images/gongsha.jpg" onclick="chkit('ccb1')" /></li>
                        <li id="l_1001" class="yin_a0">
                            <input id="ccb2" type="radio" name="pd_FrpId" value="970" />
                            <img alt="招商银行" src="/images/zhaosha.jpg" onclick="chkit('ccb2')" /></li>
                        <li id="l_1003" class="yin_a0">
                            <input id="ccb3" type="radio" name="pd_FrpId" value="965" />
                            <img alt="中国建设银行 " src="/images/jianhang.jpg" onclick="chkit('ccb3')" /></li>
                        <li id="l_1004" class="yin_a0">
                            <input id="ccb4" type="radio" name="pd_FrpId" value="975" />
                            <img alt="上海浦东发展银行" src="/images/pufa.jpg" onclick="chkit('ccb4')" /></li>
                        <li id="l_1005" class="yin_a0">
                            <input id="ccb5" type="radio" name="pd_FrpId" value="964">
                            <img alt="中国农业银行" src="/images/nongye.jpg" onclick="chkit('ccb5')" /></li>
                        <li id="l_1006" class="yin_a0">
                            <input id="ccb6" type="radio" name="pd_FrpId" value="980" />
                            <img alt="民生银行" src="/images/mingsheng.jpg" onclick="chkit('ccb6')" /></li>
                        <li id="l_1008" class="yin_a0">
                            <input id="ccb7" type="radio" name="pd_FrpId" value="974" />
                            <img alt="深圳发展银行" src="/images/shenfa.jpg" onclick="chkit('ccb7')" /></li>
                        <li id="l_1009" class="yin_a0">
                            <input id="ccb8" type="radio" name="pd_FrpId" value="972" />
                            <img alt="兴业银行" src="/images/xingye.jpg" onclick="chkit('ccb8')" /></li>
                        <li id="l_1032" class="yin_a0">
                            <input id="ccb9" type="radio" name="pd_FrpId" value="989" />
                            <img src="/images/HXBC.jpg" alt="北京银行" width="117" height="34" onclick="chkit('ccb9')" /></li>
                        <li id="l_1020" class="yin_a0">
                            <input id="ccb10" type="radio" name="pd_FrpId" value="981" />
                            <img alt="交通银行" src="/images/jiaotong.jpg" onclick="chkit('ccb10')" /></li>
                        <li id="l_1022" class="yin_a0">
                            <input id="ccb11" type="radio" name="pd_FrpId" value="986" />
                            <img alt="光大银行" src="/images/guangda.jpg" onclick="chkit('ccb11')" /></li>
                        <li id="l_1033" class="yin_a0">
                            <input id="ccb12" type="radio" name="pd_FrpId" value="987" />
                            <img alt="东亚银行" src="/images/dongya.jpg" onclick="chkit('ccb12')" /></li>
                        <li id="l_1034" class="yin_a0">
                            <input id="ccb13" type="radio" name="pd_FrpId" value="988" />
                            <img alt="渤海银行" src="/images/bohai.jpg" onclick="chkit('ccb13')" /></li>
                        <li id="l_1035" class="yin_a0">
                            <input id="ccb14" type="radio" name="pd_FrpId" value="978" />
                            <img alt="平安银行" src="/images/pingan.jpg" onclick="chkit('ccb14')" /></li>
                        <li id="l_1036" class="yin_a0">
                            <input id="ccb15" type="radio" name="pd_FrpId" value="985" />
                            <img alt="广东发展银行" src="/images/guangfa.jpg" onclick="chkit('ccb15')" /></li>
                        <li id="l_1037" class="yin_a0">
                            <input id="ccb16" type="radio" name="pd_FrpId" value="977" />
                            <img alt="上海农村商业银行" src="/images/shnongsha.jpg" onclick="chkit('ccb16')" /></li>
                        <li id="l_1026" class="yin_a0">
                            <input id="ccb17" type="radio" name="pd_FrpId" value="963">
                            <img alt="中国银行" src="/images/zhongguo.jpg" onclick="chkit('ccb17')" /></li>
                        <li id="l_1038" class="yin_a0">
                            <input id="ccb18" type="radio" name="pd_FrpId" value="971" />
                            <img alt="中国邮政储蓄银行" src="/images/youzheng.jpg" onclick="chkit('ccb18')" /></li>
                        <li id="l_1039" class="yin_a0">
                            <input id="ccb19" type="radio" name="pd_FrpId" value="962" />
                            <img alt="中信银行" src="/images/zhongxin.jpg" onclick="chkit('ccb19')" /></li>
                        <li id="l_1057" class="yin_a0">
                            <input id="ccb20" type="radio" name="pd_FrpId" value="997" />
                            <img src="/images/ningbo.jpg" alt="宁波银行" width="131" height="31" onclick="chkit('ccb20')" /></li>
                    </ul>
                    <ul id="UL2" runat="server" visible="false">
                        <li id="Li1" class="yin_a0">
                            <input id="ccbalipay" name="pd_FrpId" type="radio" value="992" checked="checked" />
                            <img alt="支付宝" src="/images/bank/ALIPAY.gif" onclick="chkit('ccbalipay')" /></li>                       
                    </ul>
                     <ul id="UL3" runat="server" visible="false">
                        <li id="Li2" class="yin_a0">
                            <input id="ccbtenpay" name="pd_FrpId" type="radio" value="993" checked="checked" />
                            <img alt="财付通" src="/images/bank/TENPAY.gif" onclick="chkit('ccbtenpay')" /></li>                       
                    </ul>
                </div>
                <div class="main_bank_zhifu" style="display: none">
                    <ul>
                        <li class="bank_zf_tishi"><span style="font-size: 18px; color: red; font-weight: bold;
                            display: none">注意事项:网络繁忙时，连接银行有时会出现连接不上银行或者“非法数据域长度”提示，请再连接多一次，或几次就可以！新增银联在线支付，支持95%银行支付！！</span>
                        </li>
                        <br>
                        <br>
                    </ul>
                </div>
                <div class="main_bank_zhifu">
                </div>
            </div>
        </div>
        <div id="divNextbutton" class="main_bank_btn">
            <br />
            <p><asp:ImageButton ID="ibtnnext" runat="server" BorderWidth="0" 
                    ImageUrl="/images/xiayibu_btn.jpg" onclick="ibtnnext_Click" />
            
                    <img src="/images/js.jpg" width="510" height="20" /><br />
                    <span id="paylimitelink"></span>
                    <br />
                    <br />
        </div>
    </div>
    </form>
</body>
</html>
