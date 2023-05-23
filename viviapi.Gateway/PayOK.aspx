<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayOK.aspx.cs" Inherits="viviapi.Gateway.PayOK" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script src="/js/ui/jquery-1.7.2.min.js"></script>
    <script src="/js/framepay.js"></script>
    <link rel="stylesheet" href="/css/base.css" />
    <link rel="stylesheet" href="/css/framepay.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="ui-dialog">
        <div class="d-top" style="">
            <span class="title">
                <%=LabelJG%></span></div>
        <div class="d-cnt">
            <div class="p30">
                <table width="100%" border="0" cellspacing="10" cellpadding="0" class="ui-box-tipinfo">
                    <tr>
                        <td align="right" valign="top" width="20%">
                            <asp:Label ID="Labelcss" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <div class="ft-14 fb g-msg-title">
                                您已成功付款<%=Labelvalue%>元</div>
                            <ul>
                                <li>交易订单号：<%=txtorderid%></li>
                                <li>交易时间：<%=completetime%></li>
                            </ul>
                            <div class="mt20">
                                <a href="<%=url%>" class="ui-button-mwhite">完成支付</a>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="divcover" style="z-index: 998; width: 100%; height: 100%;">
    </div>
    </form>
</body>
</html>
