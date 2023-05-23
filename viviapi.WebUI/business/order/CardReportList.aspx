<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.business.Order.CardReportList" Codebehind="CardReportList.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />

    <script src="../../js/ControlDate/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css"> table {font-weight: normal;font-size: 12px; line-height: 170%;}
        td{ height: 11px; }
        A:link {color: #02418a;text-decoration: none;}
    </style>
    <script type="text/javascript">
        function sendInfo(id) {
            window.open("CardOrderShow.aspx?id=" + id, "查看订单", "height=760,width=800");
        }        
    </script>
    <script type="text/javascript">
        function openuserurl(url) {
            window.open(url, "查看用户信息");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="0" class="table1">
        <tr>
            <td align="center" colspan="3" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 28px">
                卡类状态报告
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td colspan="2">
                            &nbsp&nbsp商户ID：
                            <asp:TextBox ID="txtuserid" runat="server" Width="60px"></asp:TextBox>
                            <%--  代理ID：
                <asp:TextBox ID="txtpromid" runat="server" Width="30px"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlChannelType" runat="server" Width="95px">
                                <asp:ListItem Value="">--通道类型--</asp:ListItem>
                                <asp:ListItem Value="103">神州行充值卡</asp:ListItem>
                                <asp:ListItem Value="104">盛大一卡通</asp:ListItem>
                                <asp:ListItem Value="105">征途支付卡</asp:ListItem>
                                <asp:ListItem Value="106">骏网一卡通</asp:ListItem>
                                <asp:ListItem Value="107">腾讯Q币卡</asp:ListItem>
                                <asp:ListItem Value="108">联通充值卡</asp:ListItem>
                                <asp:ListItem Value="109">久游一卡通</asp:ListItem>
                                <asp:ListItem Value="110">网易一卡通</asp:ListItem>
                                <asp:ListItem Value="111">完美一卡通</asp:ListItem>
                                <asp:ListItem Value="112">搜狐一卡通</asp:ListItem>
                                <asp:ListItem Value="113">电信充值卡</asp:ListItem>
                                <asp:ListItem Value="114">声讯卡</asp:ListItem>
                                <asp:ListItem Value="115">光宇一卡通</asp:ListItem>
                                <asp:ListItem Value="116">金山一卡通</asp:ListItem>
                                <asp:ListItem Value="117">魔兽卡</asp:ListItem>
                                <asp:ListItem Value="118">5173卡</asp:ListItem>
                                <asp:ListItem Value="119">热血卡</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlOrderStatus" runat="server" Width="95px">
                                <asp:ListItem>--订单状态--</asp:ListItem>                                
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
                             &nbsp&nbsp卡号：<asp:TextBox ID="txtCardNo" runat="server" Width="120px"></asp:TextBox>
                            <asp:DropDownList ID="ddlmange" runat="server"></asp:DropDownList>
                            &nbsp&nbsp开始：
                            <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                            &nbsp&nbsp截止：
                            <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                            &nbsp&nbsp&nbsp&nbsp<asp:Button ID="btn_Search" runat="server" CssClass="button" Text=" 查 询 " OnClick="btn_Search_Click">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp订单号：<asp:TextBox ID="txtOrderId" runat="server" Width="160px"></asp:TextBox>
                            &nbsp&nbsp商户订单号：<asp:TextBox ID="txtUserOrder" runat="server" Width="160px"></asp:TextBox>
                            &nbsp&nbsp接口商订单号：<asp:TextBox ID="txtSuppOrder" runat="server" Width="160px"></asp:TextBox>
                        </td>                        
                    </tr>
                </table>
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
                                             <td></td>
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
                                                通道类型
                                            </td>
                                            <td>
                                                银行
                                            </td>
                                             <td>
                                                状态
                                            </td>
                                            <td>
                                                下发状态
                                            </td>
                                            <td>
                                                下发时间
                                            </td>
                                             <td>
                                                下发次数
                                            </td>
                                            <td>
                                                返回内容
                                            </td>                                           
                                            <td>
                                                操作
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr bgcolor="#EFF3FB" ondblclick="javascript:sendInfo('<%# Eval("id")%>')">
                                            <td><%# getversionName(Eval("ismulticard"))%> 
                                                 <asp:Literal ID="litimg" runat="server"></asp:Literal></td>
                                            <td>
                                                <a href="?action=userpay&userid=<%# Eval("userid") %>&status=2">
                                                    <%# Eval("userid")%>
                                                </a>
                                            </td>                                           
                                            <td>
                                                <%# Eval("userorder")%>
                                            </td>
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>                                            
                                            <td>
                                                <%# Eval("modetypename")%>
                                            </td>
                                            <td>
                                                <%# Eval("modeName")%>
                                            </td>  
                                             <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum),Eval("status"))%>
                                            </td>                                  
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderNofityStatusEnum), Eval("notifystat"))%>
                                            </td> 
                                             <td>
                                                <%# Eval("notifytime")%>
                                            </td>
                                            <td>
                                                <%# Eval("notifycount")%>
                                            </td>                                           
                                            <td>
                                                <%# Eval("notifycontext")%>
                                            </td>                                         
                                            <td>
                                                <a href="javascript:openuserurl('<%# Eval("againNotifyUrl")%>')">查看</a>
                                                <asp:Button ID="btnReissue" runat="server" Text="补发" ToolTip="手动回发" CommandName="Reissue" CommandArgument='<%# Eval("orderid")%>' />                                              
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr bgcolor="#ffffff" ondblclick="javascript:sendInfo('<%# Eval("id")%>')">
                                        <td><%# getversionName(Eval("ismulticard"))%> 
                                                 <asp:Literal ID="litimg" runat="server"></asp:Literal></td>
                                              <td>
                                                <a href="?action=userpay&userid=<%# Eval("userid") %>&status=2">
                                                    <%# Eval("userid")%>
                                                </a>
                                            </td>                                           
                                            <td>
                                                <%# Eval("userorder")%>
                                            </td>
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>                                            
                                            <td>
                                                <%# Eval("modetypename")%>
                                            </td>
                                            <td>
                                                <%# Eval("modeName")%>
                                            </td>  
                                             <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum),Eval("status"))%>
                                            </td>                                  
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderNofityStatusEnum), Eval("notifystat"))%>
                                            </td> 
                                             <td>
                                                <%# Eval("notifytime")%>
                                            </td>
                                            <td>
                                                <%# Eval("notifycount")%>
                                            </td>                                           
                                            <td>
                                                <%# Eval("notifycontext")%>
                                            </td>                                         
                                            <td>
                                            <a href="javascript:openuserurl('<%# Eval("againNotifyUrl")%>')">查看</a>
                                                <asp:Button ID="btnReissue" runat="server" Text="补发" ToolTip="手动回发" CommandName="Reissue" CommandArgument='<%# Eval("orderid")%>' />                                              
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
