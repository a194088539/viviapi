<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="importitems.aspx.cs" Inherits="viviapi.WebUI.Userlogin.behalf.importitems" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
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
    <link rel="stylesheet" type="text/css" href="/Userlogin/static/js/ejs/skin/vista/ymPrompt.css" />
<script src="/Userlogin/static/js/lib/jquery-1.4.2.js" type="text/javascript"></script>
    <script src="/Userlogin/static/js/app/jquery.artDialog.js" type="text/javascript"></script>

    <script src="/Userlogin/static/js/app/jquery.artDialog.source.js?skin=simple" type="text/javascript"></script>

    <script src="/Userlogin/static/js/ejs/ymPrompt.js" type="text/javascript"></script>

    <script type="text/javascript" src="/Userlogin/static/js/date.js"></script>

    <script type="text/javascript">
        function replenish(orderid) {
            $.get("/Userlogin/Ajax/replenish_new.ashx?t=" + Math.random(), { type: "1", order: orderid },
        function(data, textStatus) {
            if (data == "true") {
                $.dialog({ title: lktitle, resize: false, content: '操作成功', ok: function() { }, close: function() { }, icon: 'succeed', width: '250px', height: '90px', time: 30000 });
            } else {
                $.dialog({ title: lktitle, resize: false, content: '操作失败', ok: function() { }, close: function() { }, icon: 'wrong', width: '250px', height: '90px', time: 30000 });
            }
        })
        }
        function ordermore(orderid) {
            ymPrompt.win('orderview.aspx?orderid=' + orderid, 600, 380, '订单详细信息', handler, null, null, { id: 'a' })
        }
        function handler(tp) {
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i> 首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/behalf/index.aspx'">对私代发</a>
        &nbsp;&gt;&nbsp; <span>上传明细</span>
    </div>
    <input name="v$id" type="hidden" value="orderquery" />
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            代发明细列表&nbsp;
            <img id="loadimg" width="0" height="0" src="/Userlogin/static/style/008.gif" />
        </div>
        <div id="search">
            <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
                <div id="msgdiv">
                </div>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        所属批次号:<asp:TextBox ID="txtLotNo" runat="server" CssClass="search_txt_01"></asp:TextBox>
                        <asp:DropDownList ID="ddlaudit_status" runat="server" CssClass="search_txt_01">
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
                        代发金额:<asp:TextBox ID="txtamtfrom" runat="server" CssClass="search_txt_01"></asp:TextBox>~
                        <asp:TextBox ID="txtamtto" runat="server" CssClass="search_txt_01"></asp:TextBox>
                        <label>
                            &nbsp;
                            <asp:Button ID="b_search" runat="server" Text="搜索" CssClass="btn btn-primary" OnClick="b_search_Click" />
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        批次汇总信息
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    总条数：
                                    <%=qty_str%>
                                    条
                                </td>
                                <td>
                                    审核拒绝条数：
                                    <%=qty1_str%>条
                                </td>
                                <td>
                                    应代发条数：
                                    <%=qty2_str%>条
                                </td>
                                <td>
                                    成功条数：
                                    <%=qty3_str%>
                                    条
                                </td>
                                <td>
                                    失败条数：
                                    <%=qty4_str%>
                                    条
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    问题条数：
                                    <%=cancel_qty_str%>元
                                </td>
                                <td>
                                    审核拒绝金额：
                                    <%=qty1_amt_str%>
                                    元
                                </td>
                                <td>
                                    应代发金额：
                                    <%=qty2_amt_str%>元
                                </td>
                                <td>
                                    代发成功金额：
                                    <%=qty3_amt_str%>元
                                </td>
                                <td>
                                    代发失败金额 ：
                                    <%=qty4_amt_str%>
                                    元
                                </td>
                            </tr>
                        </table>
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
                    目标银行
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    开户网点
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    开户名
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    账号
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    代发金额
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    代发用途
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    问题描述
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    代发金额
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    手续费
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    支付金额合计
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    处理状态
                </td>
            </tr>
            <asp:Repeater ID="rptrecharges" runat="server" OnItemDataBound="rptDetails_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("serial")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("bankName")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("bankBranch")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("bankAccountName")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("bankAccount")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("amount", "{0:f2}")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("ext1")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("remark")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("amount", "{0:f2}")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("charge", "{0:f2}")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("totalamt", "{0:f2}")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#stlAgtBLL.GetStatusText(Eval("is_cancel"), Eval("audit_status"), Eval("payment_status"))%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <!--列内容-->
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
    </form>
</body>
</html>
