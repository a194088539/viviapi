<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="incomestat.aspx.cs" Inherits="viviapi.WebUI.Merchant.incomestat" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="/merchant/static/js/date.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input name="v$id" type="hidden" value="moneychange" />
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            收益明细&nbsp;
            <img id="loadimg" width="0" height="0" src="/merchant/static/style/008.gif" />
        </div>
        <div id="search">
            <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
                <div id="msgdiv">
                </div>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <!--工具栏-->
                        &nbsp;日期从:<input id="sdate" type="text" runat="server" class="search_txt_01" onfocus="HS_setDate(this)"
                            size="12" />
                        至:<input id="edate" type="text" runat="server" class="search_txt_01" onfocus="HS_setDate(this)"
                            size="12" />
                        &nbsp;支付方式:
                        <asp:DropDownList ID="ddlChannelType" runat="server" CssClass="search_txt_01">
                            <asp:ListItem Value="">--所有方式--</asp:ListItem>
                            <asp:ListItem Value="102">网银</asp:ListItem>
                            <asp:ListItem Value="101">支付宝</asp:ListItem>
                            <asp:ListItem Value="100">财付通</asp:ListItem>
                            <asp:ListItem Value="103">移动充值卡</asp:ListItem>
                            <asp:ListItem Value="106">骏网一卡通</asp:ListItem>
                            <asp:ListItem Value="108">联通充值卡</asp:ListItem>
                            <asp:ListItem Value="104">盛大一卡通</asp:ListItem>
                            <asp:ListItem Value="210">盛付通卡</asp:ListItem>
                            <asp:ListItem Value="111">完美一卡通</asp:ListItem>
                            <asp:ListItem Value="112">搜狐一卡通</asp:ListItem>
                            <asp:ListItem Value="105">征途一卡通</asp:ListItem>
                            <asp:ListItem Value="109">久游一卡通</asp:ListItem>
                            <asp:ListItem Value="110">网易一卡通</asp:ListItem>
                            <asp:ListItem Value="115">光宇一卡通</asp:ListItem>
                            <asp:ListItem Value="114">电信充值卡</asp:ListItem>
                            <asp:ListItem Value="117">纵游一卡通</asp:ListItem>
                            <asp:ListItem Value="118">天下一卡通</asp:ListItem>
                            <asp:ListItem Value="107">腾讯一卡通</asp:ListItem>
                            <asp:ListItem Value="119">天宏一卡通</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                        <label>
                            &nbsp;
                            <asp:Button ID="b_search" runat="server" Text="搜索" CssClass="search_button_01" OnClick="b_search_Click" />
                        </label>
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            class="font2">
            <!--列标题-->
            <tr>
                <td height="34" align="center" background="images/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    序号
                </td>
                <td height="34" align="center" background="images/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    日期
                </td>
                <td height="34" align="center" background="images/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    通道
                </td>
                <td height="34" align="center" background="images/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    面值
                </td>
                <td height="34" align="center" background="images/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    提成比例
                </td>
                <td height="34" align="center" background="images/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    条数
                </td>
                <td height="34" align="center" background="images/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    小计
                </td>
            </tr>
            <asp:Repeater ID="rptOrders" runat="server">
                <ItemTemplate>
                    <tr>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#((Pager1.CurrentPageIndex-1)*20)+Container.ItemIndex +1%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("mydate")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("modetypename")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("faceValue","{0:f2}")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("payrate", "{0:p2}")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("s_num")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("sumpay","{0:f2}")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
            <tr>
                <td height="10" colspan="3">
                </td>
            </tr>
            <tr>
                <!--按钮-->
                <td height="22" align="left" class="font8">
                    本页订单数：<%=pageordercount%>条 │ 本页金额小计：<%=pagesumpay%>元 │ 订单数总计：<%=totalordercount%>条
                    │ 金额总计：<%=totalsumpay%>元
                </td>
            </tr>
            <tr>
                <!--按钮-->
                <td height="22" align="left" class="font8">
                    <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="False" CustomInfoHTML="共%PageCount%页/%RecordCount%条"
                        CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                        NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="TextBox"
                        PageSize="10" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Right"
                        ShowPageIndexBox="Never" SubmitButtonText="GO&gt;&gt;" TextAfterPageIndexBox="页"
                        TextBeforePageIndexBox="跳到" Width="100%" Height="30px" OnPageChanged="Pager1_PageChanged"
                        CustomInfoSectionWidth="20%" PageIndexBoxClass="Pager1_input" PageIndexBoxStyle="width:10px"
                        CurrentPageButtonStyle="button_01">
                    </aspxc:AspNetPager>
                </td>
            </tr>
            <tr>
                <td height="10" colspan="3">
                </td>
            </tr>
        </table>
    </div>
    <!--右部表单结束-->
</asp:Content>
