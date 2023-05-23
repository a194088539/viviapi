<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Order.usersOrderIncomes"
    CodeBehind="usersOrderIncomes.aspx.cs" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />

    <script src="../../js/common.js" type="text/javascript"></script>

    <script src="../../Js/ControlDate/WdatePicker.js" type="text/javascript"></script>

    <style type="text/css">
        .style4
        {
            width: 737px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellspacing="1" cellpadding="0" style="width: 100%">
        <tr>
            <td align="center" colspan="3" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 28px">
                商户各通道收益比例分析
            </td>
        </tr>
        <tr>
            <td colspan="3">
                商户ID：<asp:TextBox ID="txtuserid" runat="server" Width="65px"></asp:TextBox>
                <asp:DropDownList ID="ddlChannelType" runat="server" Width="95px">
                    <asp:ListItem Value="">--支付类型--</asp:ListItem>
                     <asp:ListItem Value="102">网上银行</asp:ListItem>
                    <asp:ListItem Value="101">支付宝</asp:ListItem>
                    <asp:ListItem Value="100">财付通</asp:ListItem>
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
                面值从：
                <asp:TextBox ID="txtvaluefrom" runat="server" Width="65px"></asp:TextBox>
                至<asp:TextBox ID="txtvalueto" runat="server" Width="65px"></asp:TextBox>
                开始：
                <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                截止：
                <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                <asp:Button ID="btn_Search" runat="server" CssClass="button" Text=" 查 询 " OnClick="btn_Search_Click">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                    <tr height="22" style="background-color: #507CD1; color: #fff">
                        <td>
                            日期
                        </td>
                        <td>
                            商户名(#ID)
                        </td>
                        <td>
                            真实姓名
                        </td>
                        <td>
                            充值类别
                        </td>
                        <td>
                            面值
                        </td>
                        <td>
                            结算比例
                        </td>
                        <td>
                            总条数
                        </td>
                        <td>
                            总金额
                        </td>
                    </tr>
                    <asp:Repeater ID="gv_data" runat="server">
                        <ItemTemplate>
                            <tr style="background-color: #fff">
                                <td>
                                    <%#Eval("mydate")%>
                                </td>
                                <td>
                                    <%#Eval("Username")%>
                                </td>
                                <td>
                                    <%#Eval("full_name")%>
                                </td>
                                <td>
                                    <%#Eval("modetypename")%>
                                </td>
                                <td>
                                    <%#Eval("faceValue")%>
                                </td>
                                <td>
                                    <%#Eval("payrate","{0:p2}")%>
                                </td>
                                <td>
                                    <%#Eval("s_num")%>
                                </td>
                                <td>
                                    <%#Eval("sumpay")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #EBEBEB">
                                <td>
                                    <%#Eval("mydate")%>
                                </td>
                                <td>
                                    <%#Eval("Username")%>
                                </td>
                                <td>
                                    <%#Eval("full_name")%>
                                </td>
                                <td>
                                    <%#Eval("modetypename")%>
                                </td>
                                <td>
                                    <%#Eval("faceValue")%>
                                </td>
                                <td>
                                    <%#Eval("payrate","{0:p2}")%>
                                </td>
                                <td>
                                    <%#Eval("s_num")%>
                                </td>
                                <td>
                                    <%#Eval("sumpay")%>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                     <tr style="background-color: #EBEBEB">
                        <td height="22" colspan="10">
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
</body>
</html>

<script type="text/javascript" language="JavaScript">
    var table = document.getElementById("table_zyads");
    if (table) {
        for (i = 0; i < table.rows.length; i++) {
            if (i % 2 == 0) {
                table.rows[i].bgColor = "ffffff";
            } else { table.rows[i].bgColor = "f3f9fe" }
        }
    }
    var mytr = document.getElementById("table2").getElementsByTagName("tr");
    for (var i = 1; i < mytr.length; i++) {
        mytr[i].onmouseover = function() {
            var rows = this.childNodes.length;
            for (var row = 0; row < rows; row++) {
                this.childNodes[row].style.backgroundColor = '#B2D3FF';
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

