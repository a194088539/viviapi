<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="msgview.aspx.cs" Inherits="viviapi.WebUI.Userlogin.account.msgview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: white;">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 20px;
        line-height: 35px;">
        <tr>
            <td align="right" valign="top" style="font-family: '微软雅黑'; color: #333333; font-size: 13px;
                width: 100px;">
                标题：
            </td>
            <td align="left">
                <asp:Literal ID="lit_title" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top" style="font-family: '微软雅黑'; color: #999999;">
                时间：
            </td>
            <td align="left">
                <asp:Literal ID="lit_addtime" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top" style="font-family: '微软雅黑'; color: #333333;">
                内容：
            </td>
            <td align="left">
                <asp:Literal ID="lit_content" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
