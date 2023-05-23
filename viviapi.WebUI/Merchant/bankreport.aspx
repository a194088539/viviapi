<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true" CodeBehind="bankreport.aspx.cs" Inherits="viviapi.WebUI.Merchant.bankreport" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #tinybox
        {
            position: absolute;
            display: none;
            padding: 10px;
            background: #ffffff url(/merchant/static/style/images/preload.gif) no-repeat 50% 50%;
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
    <script type="text/javascript">        function switchstate(url) { window.open(url, "", ""); }</script>
    <script src="/merchant/static/js/app/jquery.artDialog.js" type="text/javascript"></script>   
    <script src="/merchant/static/js/app/jquery.artDialog.source.js?skin=simple" type="text/javascript"></script>
    <script type="text/javascript" src="/merchant/static/js/date.js"></script>
    <script type="text/javascript">
        function replenish(orderid) {
        $.get("/Merchant/Ajax/replenish.ashx?t="+Math.random(), { type: "2", order: orderid },
        function(data, textStatus) {
            if (data == "true") {
                $.dialog({ title: lktitle, resize: false, content: '操作成功', ok: function() { }, close: function() { }, icon: 'succeed', width: '250px', height: '90px', time: 30000 });
            } else {
                $.dialog({ title: lktitle, resize: false, content: '操作失败', ok: function() { }, close: function() { }, icon: 'wrong', width: '250px', height: '90px', time: 30000 });
            }
        })
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input name="v$id" type="hidden" value="orderquery" />
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            网银订单通知结果&nbsp;
            <img id="loadimg" width="0" height="0" src="/style/008.gif" />
        </div>
        <div id="search">
            <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
                <div id="msgdiv">
                </div>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>订单状态:
                        <select name="Success" id="Success" runat="server" class="search_txt_01">
                            <option value="0">所有</option>
                            <option value="2">成功</option>
                            <option value="4">失败</option>
                            <option value="1">处理中</option>
                        </select>
                        &nbsp;其它:<select id="select_field" runat="server" class="search_txt_01">
                            <option value="1">商户订单号</option>
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
                <td height="34" align="center" background="/merchant/static/style/image/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    商户订单号
                </td>
                <td height="34" align="center" background="/merchant/static/style/image/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    订单状态
                </td>
                <td height="34" align="center" background="/merchant/static/style/image/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    报告状态
                </td>
                <td height="34" align="center" background="/merchant/static/style/image/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    发送次数
                </td>
                <td height="34" align="center" background="/merchant/static/style/image/09.jpg" bgcolor="#FFFFFF" class="list_title">
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
                            <%#GetViewStatusName(Eval("status"))%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum), Eval("notifystat"))%> <%#Eval("notifycontext")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("notifycount")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <asp:Literal ID="litdo" runat="server"></asp:Literal>
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
                   <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="true" CustomInfoHTML="共%PageCount%页/%RecordCount%条"
                                CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                                NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="TextBox"
                                PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" 
                                 ShowCustomInfoSection="Right" ShowPageIndexBox="Never" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到" Width="100%" Height="30px"
                                OnPageChanged="Pager1_PageChanged" CustomInfoSectionWidth="20%" 
                                 PageIndexBoxClass="Pager1_input" PageIndexBoxStyle="width:10px">
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
