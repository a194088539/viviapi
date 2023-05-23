<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feedbackview.aspx.cs" Inherits="viviapi.WebUI.Userlogin.account.feedbackview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/datatable.css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="line-height: 35px;
        padding: 20px;">
        <tr>
            <td align="right" valign="top" style="font-family: '微软雅黑'; color: #333333; font-size: 13px;
                width: 80px; border-bottom: 1px solid #dddddd;">
                类型：
            </td>
            <td align="left" style="border-bottom: 1px solid #dddddd;">
                <asp:Literal ID="lit_typeid" runat="server"></asp:Literal>
            </td>
            <td align="right" valign="top" style="font-family: '微软雅黑'; color: #999999; border-bottom: 1px solid #dddddd;">
                问题或建议：
            </td>
            <td align="left" style="border-bottom: 1px solid #dddddd;">
                <asp:Literal ID="lit_title" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top" style="font-family: '微软雅黑'; color: #333333; font-size: 13px;
                border-bottom: 1px solid #dddddd;">
                具体描述：
            </td>
            <td align="left" style="border-bottom: 1px solid #dddddd;">
                <asp:Literal ID="lit_cont" runat="server"></asp:Literal>
            </td>
            <td align="right" valign="top" style="font-family: '微软雅黑'; color: #999999; border-bottom: 1px solid #dddddd;">
                IP：
            </td>
            <td align="left" style="border-bottom: 1px solid #dddddd;">
                <asp:Literal ID="lit_clientip" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top" style="font-family: '微软雅黑'; color: #333333; border-bottom: 1px solid #dddddd;">
                管理员回复：
            </td>
            <td align="left" colspan="3" style="border-bottom: 1px solid #dddddd;">
                <asp:Literal ID="lit_reply" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
