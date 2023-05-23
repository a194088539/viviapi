<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Index.aspx.cs" Inherits="viviapi.gateway.WeiXin.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>扫码支付 </title>
    <link id="linkWebCss" href="/App_Themes/Cashier/Web.min.css" rel="stylesheet" />
    <link id="linkWeixinCss" href="/App_Themes/Cashier/Weixin.css" rel="stylesheet" />
    <link id="linkPaymentDialogCss" href="/App_Themes/Cashier/PaymentDialog.css" rel="stylesheet" />
    <style type="text/css">
        
    </style>
    <script src="/js/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script>
        var times = 1;
        function clock() {
            var result = "error";
            $.ajax({
                type: 'get',
                url: "/tools/QueryOrder.ashx?wxorderid=<%=wxorderid%>", //实际修改为查询我们数据库里面支付结果记录
                success: function (result) {
                    checkResult(result);
                }
            });
        }

        function checkResult(result) {
            if (result == "0") {
                //执行定时请求
                if (times < 50) {
                    setTimeout("clock();", 1000);
                    times++;
                }

            } else if (result == "1") {
                $("#content").hide();
                $("#payok").show();
                setTimeout("jumpUrl();", 1000);
            } else if (result == "ERR1") {
                $("#res .ti").html("参数错误");
                $("#res").show();
            }
            else if (result == "ERR2") {
                $("#res .ti").html("支付记录不存在，请返回商户页面重新提交！");
                $("#res").show();
            }
        }

        function jumpUrl() {
            document.location.href = "/PayOk.aspx?orderid=<%=wxorderid%>"; //商户跳转页面
        }

        $(document).ready(function () {
            setTimeout("clock();", 8000);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divTitle" class="Header">
            <div class="Wrap1000">
                <div class="Logo" style="width: 340px; margin: auto;">
                    <span style="width: 340px;"></span>
                </div>
            </div>
        </div>
        <div id="divLine" style="border-bottom: 3px solid #A2AABB; margin-top: 10px;">
        </div>
        <div id="div1000" class="Wrap1000">
            <div>
                <div id="IsShowBillInfo" class="divShow" style="height: 180px; text-align: center;">
                    <div>
                        <span class="Hint">请您尽快付款，以便订单及时处理！ </span>
                        <br />
                        <span class="Sum">请您在提交订单后<span style="color: #f60; font-weight: bold;">5分钟</span>内完成支付，否则订单会自动取消。</span><br />
                        <span class="Hint">订单号:<asp:Label ID="Labelno" runat="server"></asp:Label></span><br />
                        <br />
                        <span class="Hint">应付金额：<asp:Label ID="LabelAmt" runat="server"></asp:Label>元</span><br />
                        <br />
                        <span class="Hint">订单时间:<asp:Label ID="shijian" runat="server"></asp:Label></span><br />
                    </div>
                    <br />

                </div>
            </div>
            <div style="width: 100%;">
                <div id="divQRCode" class="divQRCode" style="margin: 0 auto;">
                    <p align="center" style="margin-top: -109; margin-left: -109">
                        <asp:Image ID="Image2" runat="server" Style="width: 245px; height: 245px;" />
                </div>
                <div id="imgQRCode" class="codeImg" style="margin: 0 auto;">
                    请使用<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>扫一扫，扫描二维码支付
                </div>
            </div>
        </div>

    </form>
</body>
</html>
