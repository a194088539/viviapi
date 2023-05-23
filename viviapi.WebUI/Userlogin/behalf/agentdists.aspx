<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agentdists.aspx.cs" Inherits="viviapi.WebUI.Userlogin.behalf.agentdists" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/behalf/index.aspx'">对私代发</a>
        &nbsp;&gt;&nbsp; <span>接口代发</span>
    </div>
    <input name="v$id" type="hidden" value="costlog" />
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            对私代发&nbsp;
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
                        系统交易号
                        <input id="txttrade_no" runat="server" type="text" maxlength="30" class="search_txt_01" />
                        商户交易号
                        <input id="txtout_trade_no" runat="server" type="text" maxlength="30" class="search_txt_01" />
                        <asp:DropDownList ID="ddlbankCode" runat="server" CssClass="search_txt_01">
                            <asp:ListItem Value="">--收款银行--</asp:ListItem>
                            <asp:ListItem Value="0002">支付宝</asp:ListItem>
                            <asp:ListItem Value="0003">财付通</asp:ListItem>
                            <asp:ListItem Value="1002">中国工商银行</asp:ListItem>
                            <asp:ListItem Value="1005">中国农业银行</asp:ListItem>
                            <asp:ListItem Value="1003">中国建设银行</asp:ListItem>
                            <asp:ListItem Value="1026">中国银行</asp:ListItem>
                            <asp:ListItem Value="1001">招商银行</asp:ListItem>
                            <asp:ListItem Value="1006">民生银行</asp:ListItem>
                            <asp:ListItem Value="1020">交通银行</asp:ListItem>
                            <asp:ListItem Value="1025">华夏银行</asp:ListItem>
                            <asp:ListItem Value="1009">兴业银行</asp:ListItem>
                            <asp:ListItem Value="1027">广发银行</asp:ListItem>
                            <asp:ListItem Value="1004">浦发银行</asp:ListItem>
                            <asp:ListItem Value="1022">光大银行</asp:ListItem>
                            <asp:ListItem Value="1021">中信银行</asp:ListItem>
                            <asp:ListItem Value="1010">平安银行</asp:ListItem>
                            <asp:ListItem Value="1066">中国邮政储蓄银行</asp:ListItem>
                        </asp:DropDownList>
                        收款账户：<asp:TextBox ID="txtAccount" runat="server" Width="120px" CssClass="search_txt_01"></asp:TextBox>
                        收款人：<asp:TextBox ID="txtbankAccountName" runat="server" Width="80px" CssClass="search_txt_01"></asp:TextBox>
                        &nbsp;<asp:DropDownList ID="ddlaudit_status" runat="server" CssClass="search_txt_01">
                            <asp:ListItem Value="">--审核状态--</asp:ListItem>
                            <asp:ListItem Value="1">等待审核</asp:ListItem>
                            <asp:ListItem Value="2">审核通过</asp:ListItem>
                            <asp:ListItem Value="3">审核拒绝</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlpayment_status" runat="server" CssClass="search_txt_01">
                            <asp:ListItem Value="">--付款状态--</asp:ListItem>
                            <asp:ListItem Value="1">未知</asp:ListItem>
                            <asp:ListItem Value="4">付款中</asp:ListItem>
                            <asp:ListItem Value="2">成功</asp:ListItem>
                            <asp:ListItem Value="3">失败</asp:ListItem>
                        </asp:DropDownList>
                        <label>
                            &nbsp;
                            <asp:Button ID="b_search" runat="server" Text="搜索" CssClass="btn btn-primary" OnClick="b_search_Click" />
                        </label>
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            class="font2">
            <!--列标题-->
            <tr>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    序号
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    系统单号
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    商户单号
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    收款信息
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    申请金额
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    手续费
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    实付金额
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    审核状态
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    付款状态
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    是否确认
                </td>
                <td>
                    操作
                </td>
            </tr>
            <!--列内容-->
            <asp:Repeater ID="rptDetails" runat="server" OnItemDataBound="rptDetails_ItemDataBound"
                OnItemCommand="rptDetails_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#((Pager1.CurrentPageIndex-1)*20)+Container.ItemIndex +1%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("trade_no")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("out_trade_no")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("bankName")%>
                            <br />
                            <%# Eval("bankBranch")%>
                            <br />
                            <%# Eval("bankAccountName")%>
                            <br />
                            <%# Eval("bankAccount")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("amount","{0:f2}")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("charge", "{0:f2}")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# (Convert.ToDecimal(Eval("amount")) + Convert.ToDecimal(Eval("charge"))).ToString("f2")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#stlAgtBLL.GetAuditStatusText(Eval("audit_status"))%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#stlAgtBLL.GetPaymentStatusText(Eval("payment_status"))%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#stlAgtBLL.GetSureText(Eval("issure"))%>
                        </td>
                        <td>
                            <asp:Button ID="btnSure" runat="server" Text="确认" CommandArgument='<%# Eval("trade_no")%>'
                                CommandName="sure" Visible="false" />
                            <asp:Button ID="btnCancel" runat="server" Text="取消" CommandArgument='<%# Eval("trade_no")%>'
                                CommandName="cancel" Visible="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Literal ID="litfoot" runat="server"></asp:Literal>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <!--合计-->
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
            <tr>
                <td height="10" colspan="3">
                </td>
            </tr>
            <tr>
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
    </form>
</body>
</html>
