<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ordercards.aspx.cs" Inherits="viviapi.WebUI.Userlogin.order.ordercards" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
   
    </style>
    <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
    <link rel="stylesheet" type="text/css" href="/Userlogin/static/js/ejs/skin/vista/ymPrompt.css" />

    <script src="/Userlogin/static/js/ejs/ymPrompt.js" type="text/javascript"></script>

    <script src="/Userlogin/static/js/lib/jquery-1.4.2.js" type="text/javascript"></script>

    <script src="/Userlogin/static/js/app/jquery.artDialog.js" type="text/javascript"></script>

    <script src="/Userlogin/static/js/app/jquery.artDialog.source.js?skin=simple" type="text/javascript"></script>

    <script type="text/javascript" src="/Userlogin/static/js/date.js"></script>

    <script type="text/javascript">
        function replenish(orderid) {
            $.get("/Userlogin/ajax/replenish_new.ashx?v=" + Math.random(), { type: "2", order: orderid },
        function(data, textStatus) {
            alert("返回：" + data);
        })
        }
        function ordermore(orderid) {
            ymPrompt.win('cardorderview.aspx?orderid=' + orderid, 600, 380, '订单详细信息', handler, null, null, { id: 'a' })
        }
        function handler(tp) {
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/order/index.aspx'">订单管理</a> &nbsp;&gt;&nbsp;
        <span>卡类订单</span>
    </div>
    <input name="v$id" type="hidden" value="orderquery" />
    <!--右部表单开始-->
   <div id="list_content" style="padding-top: 0px;">
        <h2>
            卡类订单查询</h2>
        <div id="search">
            <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
                <div id="msgdiv">
                </div>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <!--工具栏-->
                        &nbsp;日期从:<input id="sdate" runat="server" type="text" class="search_txt_01" onfocus="HS_setDate(this)"
                            size="12" />
                        从:<input id="edate" runat="server" type="text" class="search_txt_01" onfocus="HS_setDate(this)"
                            size="12" />
                        &nbsp;类型:
                        <select id="channelId" runat="server" class="search_txt_01">
                            <option value="0">所有通道</option>
                            <option value="103">移动充值卡</option>
                            <option value="106">骏网一卡通</option>
                            <option value="108">联通充值卡</option>
                            <option value="104">盛大一卡通</option>
                            <option value="210">盛付通卡</option>
                            <option value="111">完美一卡通</option>
                            <option value="112">搜狐一卡通</option>
                            <option value="105">征途一卡通</option>
                            <option value="109">久游一卡通</option>
                            <option value="110">网易一卡通</option>
                            <option value="115">光宇一卡通</option>
                            <option value="114">电信充值卡</option>
                            <option value="117">纵游一卡通</option>
                            <option value="118">天下一卡通</option>
                            <option value="107">腾讯一卡通</option>
                            <option value="119">天宏一卡通</option>
                        </select>
                        &nbsp;状态:
                        <select name="Success" id="Success" runat="server" class="search_txt_01">
                            <option value="0">所有</option>
                            <option value="2" selected="selected">成功</option>
                            <option value="4">失败</option>
                            <option value="1">处理中</option>
                        </select>
                        &nbsp;下发状态:
                        <asp:DropDownList ID="ddlNotifyStatus" runat="server" Width="95px" class="search_txt_01">
                            <asp:ListItem>所有</asp:ListItem>
                            <asp:ListItem Value="1">处理中</asp:ListItem>
                            <asp:ListItem Value="2">已成功</asp:ListItem>
                            <asp:ListItem Value="4">失败</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;其它:<select id="select_field" runat="server" class="search_txt_01">
                            <option value="1">商户订单号</option>
                            <option value="2">卡号</option>
                            <option value="3">平台订单号</option>
                        </select>
                        =
                        <input name="okey" type="text" id="okey" runat="server" maxlength="30" value="" class="txt_01" />
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
                        商户订单
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        卡号
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        支付方式
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        提交金额
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        成功金额
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        订单状态
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        提交时间
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        操作
                    </th>
                </tr>
            </thead> <%if (this.Pager1.RecordCount > 0)
                                          { %>
            <asp:Repeater ID="rptOrders" runat="server" OnItemDataBound="rptDetails_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("userorder")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("cardno")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("modetypename")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("refervalue", "{0:f0}")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#GetViewSuccessAmt(Eval("status"),Eval("realvalue"))%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#GetViewStatusName(Eval("status"))%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <a href="javascript:ordermore('<%#Eval("orderid")%>');">&laquo; 查看</a>
                            <asp:Literal ID="litdo" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
                    <%}
                                          else
                                          { %>
      
           
<tbody role="alert" aria-live="polite" aria-relevant="all">
                
                <!--列内容-->
                
                <tr class="odd">
                    <td valign="top" colspan="8" class="dataTables_empty">
                        没有符合条件的记录
                    </td>
                </tr>
                 <%} %>
                <!--合计-->
                <tr>
                   <td height="30" align="center" bgcolor="#FFFFFF" colspan="12">
                    本页提交订单：<%=pageordertotal%>笔 │ 本页成功订单：<%=pageordersucctotal%>笔 │ 总提交订单 ：<%=totalordertotal%>笔
                    │ 总成功订单：
                    <%=totalsuccordertotal%>笔 本页提交金额：<%=pagerefervalue%>元 │ 本页成功金额：<%=pagerealvalue%>元
                    │ 总提交金额 ：<%=totalrefervalue%>元 │ 总成功金额：
                    <%=totalrealvalue%>元
                </tr>
            </tbody>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
            <tr>
                <td height="10" colspan="3">
                </td>
            </tr>
            <tr>
                <!--按钮-->
                <td height="22" align="left" class="font8">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
            <tr>
                <td height="10" colspan="3">
                </td>
            </tr>
            <tr>
                <!--按钮-->
                <td height="22" align="left" class="font8">
                    <aspxc:AspNetPager ID="Pager1" runat="server" CssClass="paginator" CurrentPageButtonClass="cpb"  AlwaysShow="true" CustomInfoHTML="共%PageCount%页/%RecordCount%条"
                        CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                        NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="TextBox"
                        PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50"  ShowCustomInfoSection="Right"
                        ShowPageIndexBox="Never" SubmitButtonText="GO&gt;&gt;" TextAfterPageIndexBox="页"
                        TextBeforePageIndexBox="跳到" Width="650px" Height="30px" OnPageChanged="Pager1_PageChanged"
                        CustomInfoSectionWidth="20%" PageIndexBoxClass="Pager1_input" PageIndexBoxStyle="width:10px;">
                    </aspxc:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
    <!--右部表单结束-->
    </form>
</body>
</html>
