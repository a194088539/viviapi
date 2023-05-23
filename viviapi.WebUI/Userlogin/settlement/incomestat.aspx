<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="incomestat.aspx.cs" Inherits="viviapi.WebUI.Userlogin.settlement.incomestat" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="/Userlogin/static/js/date.js"></script>
 <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/settlement/index.aspx'">结算提现</a>
        &nbsp;&gt;&nbsp; <span>收益明细</span>
    </div>
    <input name="v$id" type="hidden" value="moneychange" />
    <!--右部表单开始-->
   <div id="list_content" style="padding-top: 0px;">
        <h2>
            收益明细</h2>
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
                            <asp:Button ID="b_search" runat="server" Text="搜索" CssClass="btn btn-primary" OnClick="b_search_Click" />
                        </label>
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            id="dataTable" class="table table-bordered table-striped centered dataTable"
            aria-describedby="dataTable_info">
            <!--列标题-->
            <thead>
                <tr role="row">
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        序号
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        日期
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        通道
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        面值
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        提成比例
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        条数
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        小计
                    </th>
                </tr>
            </thead><%if (this.Pager1.RecordCount > 0)
                                          { %>
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
         <%}
                                          else
                                          { %>

<tbody role="alert" aria-live="polite" aria-relevant="all">
                 <tr class="odd">
                    <td valign="top" colspan="7" class="dataTables_empty">
                        没有符合条件的记录
                    </td>
                </tr>   <%} %>
       
            <tr>
                <!--按钮-->
                <td height="22" align="left" class="font8" colspan="7">
                    本页订单数：<%=pageordercount%>条 │ 本页金额小计：<%=pagesumpay%>元 │ 订单数总计：<%=totalordercount%>条
                    │ 金额总计：<%=totalsumpay%>元
                </td>
            </tr>
</tbody>
        </table>  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
            <tr>
                <!--按钮-->
                <td height="22" align="left" class="font8">
                    <aspxc:AspNetPager ID="Pager1" runat="server" CssClass="paginator" CurrentPageButtonClass="cpb"  AlwaysShow="true" CustomInfoHTML="共%PageCount%页/%RecordCount%条"
                        CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                        NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="TextBox"
                        PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50"  ShowCustomInfoSection="Right"
                        ShowPageIndexBox="Never" SubmitButtonText="GO&gt;&gt;" TextAfterPageIndexBox="页"
                        TextBeforePageIndexBox="跳到" Width="650px" Height="30px" OnPageChanged="Pager1_PageChanged"
                        CustomInfoSectionWidth="10%" PageIndexBoxClass="Pager1_input" PageIndexBoxStyle="width:10px;">
                    </aspxc:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
    <!--右部表单结束-->
    </form>
</body>
</html>
