<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="tools.aspx.cs" Inherits="viviapi.WebUI.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server" Width="80%"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="加密" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="MD5加密" /><br />
        <asp:TextBox ID="TextBox2" runat="server" Rows="6" TextMode="MultiLine" Width="80%"></asp:TextBox></div>
    </div>
    </form>
</body>
</html>
