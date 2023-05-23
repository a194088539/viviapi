<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Template.Configuration" Codebehind="Configuration.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/page.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" class="tbtitle">模板设置</td>
            </tr>
            <tr>
                <td width="10%" class="td2">
                    注册内容：</td>
                <td width="90%" colspan="3" class="td1">
                    <asp:TextBox ID="txtRegister" runat="server" Width="800px">尊敬的商户您好，非常感谢您注册{@sitename}。您的注册码是{@authcode}。【@sitename】</asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">认证内容：</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtAuthenticate" runat="server" Width="800px">尊敬的商户{@username}您好，您的认证码是{@authcode}。【@sitename】</asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td class="td2">修改手机：</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtModify" runat="server" Width="800px">尊敬的商户{@username}您好，您的认证码是{@authcode}。【@sitename】</asp:TextBox>              </td>
            </tr>
            <tr>
                <td class="td2">
                    找回密码：</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtFindPwd" runat="server" Width="800px">尊敬的商户{@username}您好，您的认证码是{@authcode}。【@sitename】</asp:TextBox></td>
            </tr>
             <tr>
                <td class="td2">
                    提现操作：</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txttoCash" runat="server" Width="800px">尊敬的商户{@username}您好，您的认证码是{@authcode}。【@sitename】</asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="td1" colspan="3">
                    <span style="padding-left: 3px; height: 40px">
                        <asp:Button ID="btn_Update" runat="server" Text="确认更新" OnClick="btnUpdate_Click" OnClientClick="allQQ()" />
                    </span>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
