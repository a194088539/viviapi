<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="smsTest.aspx.cs" Inherits="viviapi.gateway.smsTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        短信内容：<asp:TextBox ID="TextBox1" runat="server" Width="242px"></asp:TextBox><br />
        长 号 码：<asp:TextBox ID="TextBox2" runat="server" Width="244px"></asp:TextBox>
        <br />
        费    用：<asp:TextBox ID="TextBox3" runat="server" Width="244px"></asp:TextBox><br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="分析" 
            style="width: 56px" /><br />
        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
        
    </div>
    
    
    </form>
</body>
</html>
