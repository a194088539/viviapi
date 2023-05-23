<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.business.Order.SmsOrderList" Codebehind="SmsOrderList.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />

    <script src="../../js/ControlDate/WdatePicker.js" type="text/javascript"></script>

    <style type="text/css">
        table
        {
            font-weight: normal;
            font-size: 12px;
            line-height: 170%;
        }
        td
        {
            height: 11px;
        }
        A:link
        {
            color: #02418a;
            text-decoration: none;
        }
    </style>

    <script type="text/javascript">
        function sendInfo(id) {
            window.open("SmsOrderShow.aspx?id=" + id, "查看订单", "height=760,width=1000");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="0" class="table1">
        <tr>
            <td align="center" colspan="3" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 28px">
                短信订单查询
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td colspan="2">
                            商户ID：
                            <asp:TextBox ID="txtuserid" runat="server" Width="40px"></asp:TextBox>
                            <%--  代理ID：
                <asp:TextBox ID="txtpromid" runat="server" Width="30px"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlOrderStatus" runat="server" Width="95px">
                                <asp:ListItem>--订单状态--</asp:ListItem>
                                <asp:ListItem Value="1">处理中</asp:ListItem>
                                <asp:ListItem Value="2">已成功</asp:ListItem>
                                <asp:ListItem Value="4">失败</asp:ListItem>
                                <asp:ListItem Value="8">扣量</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlNotifyStatus" runat="server" Width="95px">
                                <asp:ListItem>--下发状态--</asp:ListItem>
                                <asp:ListItem Value="1">处理中</asp:ListItem>
                                <asp:ListItem Value="2">已成功</asp:ListItem>
                                <asp:ListItem Value="4">失败</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlmange" runat="server"></asp:DropDownList>
                            开始：
                            <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                            截止：
                            <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                              &nbsp&nbsp手机号码：<asp:TextBox ID="txtmobile" runat="server" Width="120px"></asp:TextBox>
                            <asp:Button ID="btn_Search" runat="server" CssClass="button" Text=" 查 询 " OnClick="btn_Search_Click">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            订单号：<asp:TextBox ID="txtOrderId" runat="server" Width="160px"></asp:TextBox>
                            商户订单号：<asp:TextBox ID="txtUserOrder" runat="server" Width="160px"></asp:TextBox>
                            接口商订单号：<asp:TextBox ID="txtSuppOrder" runat="server" Width="160px"></asp:TextBox>
                        </td>
                        <td>
                            <div runat="server" id="divmoney">
                                <span style="color: #ff0000; text-align: left">总额：<% = TotalTranATM %></span> <span
                                    style="color: #ff0000; text-align: left;" runat="server" id="spangmmoney">商户所得：<% = TotalUserATM %></span>
                                    <span style="color: #ff0000; text-align: left;">业务总提成：<% = TotalCommission %></span>
                                <span style="color: #ff0000; text-align: left; display: none">代理总提成：<% = TotalPromATM %></span>
                                <span style="color: #ff0000; text-align: left;">平台利润：<% = TotalProfit%></span>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" bgcolor="#F9F9F9">
            </td>
        </tr>
        <tr>
            <td bgcolor="#ffffff">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="center">
                            <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                                <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand"
                                    OnItemDataBound="rptOrders_ItemDataBound">
                                    <HeaderTemplate>
                                        <tr height="22" style="background-color: #507CD1; color: #fff">
                                            <td>
                                                商户ID
                                            </td>
                                            <td>
                                                商户订单号
                                            </td>
                                            <td>
                                                订单号
                                            </td>
                                            <td>
                                                接口商交易号
                                            </td>
                                            <td>
                                                手机号码
                                            </td>
                                            <td>
                                                长号码
                                            </td>                                            
                                            <td>
                                                短信内容
                                            </td>
                                            <td>
                                                金额
                                            </td>
                                            <td>
                                                商户
                                            </td>
                                            <td>
                                                平台
                                            </td>
                                            <td>
                                                业务
                                            </td>
                                             <td>
                                                利润
                                            </td>
                                            <td>
                                                到帐时间
                                            </td>
                                            <td>
                                                状态
                                            </td>
                                            <td>
                                                下发状态
                                            </td>
                                            <td>
                                                接口商
                                            </td>
                                            <td>
                                                服务器
                                            </td>
                                            <td>
                                                补发
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr bgcolor="#EFF3FB" ondblclick="javascript:sendInfo('<%# Eval("id")%>')">
                                            <td>
                                                <a href="?action=userpay&userid=<%# Eval("userid") %>&status=2">
                                                    <%# Eval("userid")%>
                                                </a>
                                            </td>
                                            <td>
                                                <%# Server.HtmlEncode(Eval("userorder").ToString())%>
                                            </td>
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>
                                            <td>
                                                <%# Eval("linkid")%>
                                            </td>
                                            <td>
                                                <%# Eval("mobile")%>
                                            </td>
                                            <td>
                                                <%# Eval("servicenum")%>
                                            </td>                                            
                                            <td>
                                                <%# Eval("message")%>
                                            </td>
                                            <td>
                                                <%# Eval("fee","{0:0}")%>
                                            </td>
                                            <td>
                                                <%# Eval("payAmt", "{0:f2}")%>
                                            </td>
                                            <td>
                                                <%# Eval("supplierAmt", "{0:f2}")%>
                                            </td>
                                            <td>
                                                <%# Eval("commission", "{0:f2}")%>
                                            </td>                                            
                                             <td>
                                                <%# Eval("profits", "{0:f2}")%>
                                            </td>
                                            <td>
                                                <%# Eval("completetime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                            </td>
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum),Eval("status"))%>
                                            </td>
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderNofityStatusEnum), Eval("notifystat"))%>
                                            </td>
                                            <td>
                                                <%# viviapi.WebUI.WebUtility.GetsupplierName(Eval("supplierId"))%>
                                            </td>
                                            <td>
                                                <%# Eval("server")%>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnReissue" runat="server" Text="补发" ToolTip="手动回发" CommandName="Reissue" CommandArgument='<%# Eval("orderid")%>' />
                                                <asp:Button ID="btnRest" runat="server" Text="补单" CommandName="ResetOrder" CommandArgument='<%#GetParm(Eval("orderid"),Eval("supplierId"),Eval("fee"))%>' />
                                                <asp:Button ID="btnDeduct" runat="server" Text="扣"  ToolTip="扣量" CommandName="Deduct" CommandArgument='<%# Eval("orderid")%>' />
                                                <asp:Button ID="btnReDeduct" runat="server" Text="还"  CommandName="ReDeduct" CommandArgument='<%# Eval("orderid")%>' />
                                
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr bgcolor="#ffffff" ondblclick="javascript:sendInfo('<%# Eval("id")%>')">
                                            <td>
                                                <a href="?action=userpay&userid=<%# Eval("userid") %>&status=2">
                                                    <%# Eval("userid")%>
                                                </a>
                                            </td>
                                            <td>
                                                 <%# Server.HtmlEncode(Eval("userorder").ToString())%>
                                            </td>
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>
                                            <td>
                                                <%# Eval("linkid")%>
                                            </td>
                                            <td>
                                                <%# Eval("mobile")%>
                                            </td>
                                            <td>
                                                <%# Eval("servicenum")%>
                                            </td>                                            
                                            <td>
                                                <%# Eval("message")%>
                                            </td>
                                            <td>
                                                <%# Eval("fee","{0:0}")%>
                                            </td>
                                            <td>
                                                <%# Eval("payAmt", "{0:f2}")%>
                                            </td>
                                            <td>
                                                <%# Eval("supplierAmt", "{0:f2}")%>
                                            </td>
                                            <td>
                                                <%# Eval("commission", "{0:f2}")%>
                                            </td>
                                             <td>
                                                <%# Eval("profits", "{0:f2}")%>
                                            </td>
                                            <td>
                                                <%# Eval("completetime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                            </td>
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum),Eval("status"))%>
                                            </td>
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderNofityStatusEnum), Eval("notifystat"))%>
                                            </td>
                                            <td>
                                                <%# viviapi.WebUI.WebUtility.GetsupplierName(Eval("supplierId"))%>
                                            </td>
                                            <td>
                                                <%# Eval("server")%>
                                            </td>
                                            <td>
                                               <asp:Button ID="btnReissue" runat="server" Text="补发" ToolTip="手动回发" CommandName="Reissue" CommandArgument='<%# Eval("orderid")%>' />
                                                <asp:Button ID="btnRest" runat="server" Text="补单" CommandName="ResetOrder" CommandArgument='<%#GetParm(Eval("orderid"),Eval("supplierId"),Eval("fee"))%>' />
                                                <asp:Button ID="btnDeduct" runat="server" Text="扣"  ToolTip="扣量" CommandName="Deduct" CommandArgument='<%# Eval("orderid")%>' />
                                                <asp:Button ID="btnReDeduct" runat="server" Text="还"  CommandName="ReDeduct" CommandArgument='<%# Eval("orderid")%>' />
                                
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr style="background-color: #EBEBEB">
                        <td height="22" colspan="7">
                            <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CustomInfoHTML="总记录数：%RecordCount%&nbsp;总页数：%PageCount%&nbsp;当前页：%CurrentPageIndex%&nbsp;"
                                CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                                NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                                PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到" Width="100%" Height="30px"
                                OnPageChanged="Pager1_PageChanged">
                            </aspxc:AspNetPager>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript">
        function handler(tp) {
        }

        var mytr = document.getElementById("table2").getElementsByTagName("tr");
        for (var i = 1; i < mytr.length; i++) {
            mytr[i].onmouseover = function() {
                var rows = this.childNodes.length;
                for (var row = 0; row < rows; row++) {
                    this.childNodes[row].style.backgroundColor = '#E6EEFF';
                }
            };
            mytr[i].onmouseout = function() {
                var rows = this.childNodes.length;
                for (var row = 0; row < rows; row++) {
                    this.childNodes[row].style.backgroundColor = '';
                }
            };
        }

    </script>

</body>
</html>
