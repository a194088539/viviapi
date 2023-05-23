<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="orderbank.aspx.cs" Inherits="viviapi.WebUI.Merchant.orderbank" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #tinybox
        {
            position: absolute;
            display: none;
            padding: 10px;
            background: #ffffff url(../image/preload.gif) no-repeat 50% 50%;
            border: 10px solid #e3e3e3;
            z-index: 2000;
        }
        #tinymask
        {
            position: absolute;
            display: none;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            background: #000000;
            z-index: 1500;
        }
        #tinycontent
        {
            background: #ffffff;
            font-size: 1.1em;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="/merchant/static/js/ejs/skin/simple_gray/ymPrompt.css" />

    <script src="/merchant/static/js/app/jquery.artDialog.js" type="text/javascript"></script>

    <script src="/merchant/static/js/app/jquery.artDialog.source.js?skin=simple" type="text/javascript"></script>

    <script src="/merchant/static/js/ejs/ymPrompt.js" type="text/javascript"></script>

    <script type="text/javascript" src="/merchant/static/js/date.js"></script>

    <script type="text/javascript">
        function replenish(orderid) {
            $.get("/merchant/ajax/replenish.ashx?v=" + Math.random(), { type: "1", order: orderid },
        function(data, textStatus) {
            alert("返回：" + data);
        })
        }

        function ordermore(orderid) {
            ymPrompt.win('orderview.aspx?orderid=' + orderid, 600, 380, '订单详细信息', handler, null, null, { id: 'a' })
        }
        function handler(tp) {
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input name="v$id" type="hidden" value="orderquery" />
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            网银订单查询&nbsp;
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
                        &nbsp;日期从:<input id="sdate" runat="server" type="text" class="search_txt_01" onfocus="HS_setDate(this)"
                            size="12" />
                        从:<input id="edate" runat="server" type="text" class="search_txt_01" onfocus="HS_setDate(this)"
                            size="12" />
                        &nbsp;类型:
                        <select id="channelId" runat="server" class="search_txt_01">
                            <option value="0">所有通道</option>
                                <option value="102">网上银行</option>
                                <option value="101">支付宝</option>
                                <option value="100">财付通</option>
                                <option value="99">微信</option>
                                <option value="98">QQ钱包</option>
                                <option value="97">京东钱包</option>
                                <option value="88">百度钱包</option>
                                <option value="96">手机网银</option>
                                <option value="95">手机支付宝</option>
                                <option value="94">手机微信</option>
                                <option value="93">手机QQ</option>
                                <option value="92">手机京东</option>
                                <option value="87">支付宝h5</option>
                                <option value="89">微信h5</option>
                                <option value="86">QQh5</option>
                        </select>
                        &nbsp;状态:
                        <select name="Success" id="Success" runat="server" class="search_txt_01">
                            <option value="0">所有</option>
                            <option value="2" selected="selected">成功</option>
                            <option value="4">失败</option>
                            <option value="1">处理中</option>
                        </select>
                        &nbsp;下发状态:
                        <asp:DropDownList ID="ddlNotifyStatus" runat="server" Width="95px">
                            <asp:ListItem>所有</asp:ListItem>
                            <asp:ListItem Value="1">处理中</asp:ListItem>
                            <asp:ListItem Value="2">已成功</asp:ListItem>
                            <asp:ListItem Value="4">失败</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;其它:<select id="select_field" runat="server" class="search_txt_01">
                            <option value="1">商户订单号</option>
                            <option value="3">平台订单号</option>
                        </select>
                        =
                        <input name="okey" type="text" id="okey" runat="server" maxlength="30" value="" />
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
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    商户订单
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    支付方式
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    提交金额
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    成功金额
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    订单状态
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    提交时间
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    操作
                </td>
            </tr>
            <asp:Repeater ID="rptOrders" runat="server" OnItemDataBound="rptDetails_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("userorder")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("modeName")%>
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
            <!--列内容-->
            <!--合计-->
            <tr>
                <td height="30" align="center" bgcolor="#FFFFFF" colspan="12">
                    本页提交订单：<%=pageordertotal%>笔 │ 成功订单：<%=pageordersucctotal%>笔 │ 总提交订单：<%=totalordertotal%>笔
                    │ 总成功订单：
                    <%=totalsuccordertotal%>笔 本页提交金额：<%=pagerefervalue%>元 │ 本页成功金额：<%=pagerealvalue%>元
                    │ 总提交金额 ：<%=totalrefervalue%>元 │ 总成功金额：
                    <%=totalrealvalue%>元
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
            <tr>
                <td height="10" colspan="3">
                </td>
            </tr>
            <tr>
                <!--按钮-->
                <td height="22" align="left" class="font8">
                    <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="true" CustomInfoHTML="共%PageCount%页/%RecordCount%条"
                        CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                        NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="TextBox"
                        PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Right"
                        ShowPageIndexBox="Never" SubmitButtonText="GO&gt;&gt;" TextAfterPageIndexBox="页"
                        TextBeforePageIndexBox="跳到" Width="100%" Height="30px" OnPageChanged="Pager1_PageChanged"
                        CustomInfoSectionWidth="20%" PageIndexBoxClass="Pager1_input" PageIndexBoxStyle="width:10px">
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
