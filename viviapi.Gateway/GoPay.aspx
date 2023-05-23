<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoPay.aspx.cs" Inherits="viviapi.gateway.GoPay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>正在连接银行</title>
    <link href="/css/css.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <%=SubForm%>
    <table width="950" border="0" align="center" cellpadding="5" cellspacing="0" style="border: 2px #ccc solid;margin-top: 100px;">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border-top: 3px #ffba00 solid;
                    margin-top: 40px;">
                    <tr>
                        <td valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="text-align: center;
                                line-height: 40px;">
                                <tr>
                                    <td height="60">
                                        <img src="/images/icon_tdwaiting.gif" width="32" height="32" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="STYLE1">本站郑重提醒：请仔细阅读下列声明，同意有方可点击“我已了解，继续付款”。系统也将在 10 秒后自动跳转，自动跳转后本网关也认为您同意下列声明。</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p class="STYLE4">
                                            本充值连接仅支持游戏币直接到账业务！若遇到网站会员充值、激活、帐号解封<br />
                                            淘宝购买物品、实物购买、手机话费充值，担保交易、缴纳押金等，均为诈骗！！切勿继续交易，否则后果自负！<br />
                                            禁止赌博、色情、钓鱼等非法信息接入，如您发现可疑信息，请进行举报！
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="button" name="button1" onclick="javascript:window.location='JuBao.aspx'" id="button1" value="点我举报" style="height: 30px; border: 1px solid #ccc; color: Red;" /><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            <input type="submit" name="button" onclick="document.forms[0].submit();" id="button" value="我已了解，继续付款。[5秒后自动跳转]" style="height: 30px; border: 1px solid #ccc;" disabled="disabled"/></label> <!---->
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="STYLE4">在付款操作完成之前，请不要关闭浏览器！</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var times = 5;
        function fun() {
            if (times > 1) {
                document.getElementById("button").value = "我已了解，继续付款。[" + times + "秒后自动跳转]";
            } else {
                document.forms[0].submit();
                //document.getElementById("button").value = "我已了解，点击继续";
            }
            times -= 1;
            if (times <= 0) {
                window.clearInterval(doTEST);
                document.getElementById("button").disabled = false;
            }
        }
        var doTEST;
        window.clearInterval(doTEST);
        doTEST = window.setInterval("fun()", 1000); 
                   </script>
</body>
</html>
