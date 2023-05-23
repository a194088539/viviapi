<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="viviapi.WebUI.business.User.SendMsg" Codebehind="SendMsg.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发站内信</title>
    <base target="_self" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
        <tr>
            <td align="center" colspan="3" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 24px">
               发送内部消息
            </td>
        </tr>
        <tr>
           <td align="right" class="jfontItem" style="width: 125px">
                收信者：
            </td>
            <td align="left">                
                <asp:TextBox CssClass="label" ID="txtMsgTo" MaxLength="50" runat="server" Width="300px"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" class="jfontItem" style="width: 125px">
                消息标题：<asp:HiddenField ID="NewsID" Value="0" runat="server" />
            </td>
            <td align="left">
                <asp:TextBox CssClass="label" ID="tb_title" MaxLength="50" runat="server" Width="300px"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" class="jfontItem" style="width: 125px">
                消息内容：
            </td>
            <td align="left">
                <iframe id="eWebEditor1" src="../News/DocEditor/ewebeditor.htm?id=content&style=mini"
                    frameborder="0" scrolling="no" style="width: 550px; height: 350px;"></iframe>
            </td>
        </tr>
        <tr>
            <td align="right" class="jfontItem" style="width: 125px; height: 40px;">
            </td>
            <td align="left" style="height: 40px">
                <asp:Button CssClass="button" ID="bt_sub" runat="server" Text=" 发 布 " OnClick="bt_sub_Click">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td align="right" class="jfontItem" style="width: 125px; height: 40px;">
            </td>
            <td align="left" style="height: 40px">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="content" runat="server" />
    </form>
</body>
</html>
