﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="excha.aspx.cs" Inherits="viviapi.WebUI.Userlogin.order.excha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="http://pic.ofcard.com/4pay-new/css/themes/default/0066CC/frame.css">
<script src="/js/lib/jquery-1.4.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/xiaoka_new.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/order/index.aspx'">订单管理</a> &nbsp;&gt;&nbsp;
        <span>批量销卡</span>
    </div>
    <div id="list_content">
        <div id="title">
            批量销卡&nbsp;<img id="loading" width="0" height="0" src="/Userlogin/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="39" align="right" class="line_01">
                    卡种选择：
                </td>
                <td align="left" class="line_01">
                    <asp:DropDownList ID="ddlcardType" runat="server" CssClass="search_txt_01">
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
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 25%">
                    输入卡密：
                </td>
                <td style="width: 55%" align="left" class="line_01">
                    <asp:TextBox ID="txtCards" runat="server" Width="50%" Rows="20" TextMode="MultiLine"
                        CssClass="mutitxt_03"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="line_01">
                    注意事项：
                </td>
                <td class="line_01">
                    1. 每行卡信息支持格式为：“卡号,密码,金额” 或者 “卡号 密码 金额”
                    <br>
                    2. 每行卡信息分割符号仅支持空格和逗号，以其它符号分割将提示错误；
                    <br>
                    3. 每次提交订单数不超过20行,数量订单数量超过20，系统会自动取前20条
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 25%">
                </td>
                <td style="width: 75%">
                    <input id="btnXiaoKa" type="button" value="提交销卡" class="btn btn-primary" />&nbsp;
                    <button class="btn btn-primary" id="queryorder" style="margin-right: 0" type="button" onclick="queryOrder();">
                        刷新列表</button>
                    <asp:Button ID="btnSubmit" runat="server" Text="提交销卡" CssClass="btn btn-primary" OnClick="btnSubmit_Click"
                        Visible="false" />
                    已经输入 <span id="Groupscount" class="txtc" style="font-size: 18px">0</span>
                    <div class="b_m_t txtr dis-n" id="Groupsinfo">
                    </div>
                    <div id="Groupsload">
                    </div>
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 25%">
                </td>
                <td style="width: 75%" align="left" class="line_01">
                    <div id="Groupsinfo_01" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_02" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_03" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_04" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_05" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_06" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_07" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_08" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_09" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_10" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_11" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_12" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_13" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_14" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_15" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_16" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_17" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_18" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_19" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_20" style="margin-bottom: 5px">
                    </div>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            class="font2">
            <!--列标题-->
            <tr>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    卡号
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    提交金额
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    成功金额
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    订单状态
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    说明
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    提交时间
                </td>
                <td height="34" align="center" bgcolor="#E2E8F2"
                    class="list_title">
                    操作
                </td>
            </tr>
            <tbody id="toporder">
                <asp:Repeater ID="rptorders" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="30" align="center" bgcolor="#ffffff">
                                <%# Eval("cardNo")%>
                            </td>
                            <td height="30" align="center" bgcolor="#ffffff">
                                <%#Eval("refervalue","{0:0.00}")%>
                            </td>
                            <td id="paymoney<%#Eval("ID")%>" height="30" align="center" bgcolor="#ffffff">
                                <%#GetViewSuccessAmt(Eval("status"),Eval("realvalue"))%>
                            </td>
                            <td id="orderzt<%#Eval("ID")%>" height="30" align="center" bgcolor="#ffffff">
                                <%#GetViewStatusName(Eval("status"))%>
                            </td>
                            <td id="errorMsg<%#Eval("ID")%>" height="30" align="center" bgcolor="#ffffff">
                                <%#Eval("msg")%>
                            </td>
                            <td height="30" align="center" bgcolor="#ffffff">
                                <%#Eval("addtime","{0:yyyy-MM-dd HH:mm}")%>
                            </td>
                            <td height="30" align="center" bgcolor="#ffffff">
                                <button class="button_01" id="sub<%#Eval("ID")%>" style="margin-right: 0" type="button"
                                    onclick="checkflag('<%#Eval("ID")%>')">
                                    刷新</button>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
