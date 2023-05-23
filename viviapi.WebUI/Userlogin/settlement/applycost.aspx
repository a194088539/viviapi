<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="applycost.aspx.cs" Inherits="viviapi.WebUI.Userlogin.settlement.applycost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/usermodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/usermodule/settlement/index.aspx'">结算提现</a>
        &nbsp;&gt;&nbsp; <span>申请提现</span>
    </div>
    <input name="v$id" type="hidden" value="applycost" />
    <input name="v$fid" type="hidden" value="ruili" />
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            申请提现&nbsp;<img id="loading" width="0" height="0" src="/merchant/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="6" align="left" class="line_01">
                    即时提现申请,如果是按天结算客户,不必提交申请,财务会定时进行转款,谢谢合作
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 15%">
                    商户名称:
                </td>
                <td align="left" class="line_01" style="width: 35%">
                    <input id="txtUserName" runat="server" type="text" class="txt_02" size="25" />
                </td>
                <td height="39" align="left" class="line_01" colspan="4">
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01">
                    总余额:
                </td>
                <td align="left" class="line_01">
                    <input id="txtBalance" runat="server" type="text" class="txt_02" size="25" />
                </td>
                <td height="39" align="left" class="line_01" style="width: 15%" colspan="4">
                    <asp:Literal ID="litBalance" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 15%">
                    网银提现:
                </td>
                <td align="left" class="line_01" style="width: 35%">
                    T+<asp:Literal ID="litbankdetentiondays" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left" class="line_01" style="width: 15%">
                    网银不可提现金额
                </td>
                <td align="left" class="line_01" style="width: 35%">
                    <asp:Literal ID="litbankdetentionAmt" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01">
                    点卡提现:
                </td>
                <td align="left" class="line_01">
                    T+<asp:Literal ID="litcarddetentiondays" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left" class="line_01">
                    点卡不可提现金额
                </td>
                <td align="left" class="line_01">
                    <asp:Literal ID="litcarddetentionAmt" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01">
                    其它提现:
                </td>
                <td align="left" class="line_01">
                    T+<asp:Literal ID="litotherdetentiondays" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left" class="line_01">
                    其它不可提现金额
                </td>
                <td align="left" class="line_01">
                    <asp:Literal ID="litotherdetentionAmt" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01">
                    申请提现金额:
                </td>
                <td align="left" class="line_01">
                    <input id="txtApplyMoney" runat="server" type="text" class="txt_01" size="25" value="" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01">
                    提现密码:
                </td>
                <td align="left" class="line_01">
                    <asp:TextBox ID="txtcashpwd" runat="server" CssClass="txt_01" TextMode="Password"
                        MaxLength="25"></asp:TextBox>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
<!-- 
            <tr>
                <td height="39" align="right" class="line_01">
                    手机验证码:
                </td>
                <td align="left" class="line_01">
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="txt_01" TextMode="Password"
                        MaxLength="25"></asp:TextBox>
                </td>
                <td height="39" align="left" class="line_01"></td>
            </tr>
 -->
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
            <tr>
                <td height="22" align="left" class="font8">
                    <asp:Button ID="btnpost" runat="server" Text="申请" CssClass="btn btn-primary" OnClick="btnpost_Click" />&nbsp;
                    <input name="b$close" type="submit" class="btn btn-primary" value="关闭" onclick="javascript:window.parent.TINY.box.hide();" />
                    &nbsp;<asp:Label ID="lblMessage" runat="server" Visible="False" Font-Bold="True"
                        ForeColor="#FF3300"></asp:Label>
                    <td align="right">
                        &nbsp;
                    </td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
