<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.DataBackup" Codebehind="DataBackup.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>后台管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="style/union.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                <tr>
                    <td align="center" colspan="3" style="font-weight: bold; font-size: 14px; background-image: url(../Images/topbg.gif);color: teal; background-repeat: repeat-x; height: 28px">
                        数据备份</td>
                </tr>
                <tr>
                    <td class="jfontItem" align="right" style="width: 125px">
                        指定目录：</td>
                    <td align="left">
                        <asp:TextBox ID="txtfilepath" runat="server" Width="200px" CssClass="label" Text="d:\BACKUP\Pay\"
                            MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="jfontItem" align="right" style="width: 125px">
                        备份文件名：</td>
                    <td align="left">
                        <asp:TextBox ID="txtfilname" runat="server" Width="200px" CssClass="label" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right" class="jfontItem" style="width: 125px; height: 40px;">
                    </td>
                    <td align="left" style="height: 40px">
                        <asp:Button CssClass="button" ID="bt_sub" runat="server" Text="确定备份 " OnClick="bt_sub_Click">
                        </asp:Button><asp:Label ID="lbmsg" runat="server" ForeColor="Red"></asp:Label></td>
                </tr>
                <tr>
                    <td align="right" class="jfontItem" style="width: 125px; height: 40px;">
                    </td>
                    <td align="left" style="height: 40px">
                        <asp:Button CssClass="button" ID="btnClear" runat="server" Text="历史数据清理" OnClick="btnClear_Click">
                        </asp:Button><asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label><br />
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
