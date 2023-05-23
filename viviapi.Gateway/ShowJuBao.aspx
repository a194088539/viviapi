<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowJuBao.aspx.cs" Inherits="viviapi.gateway.ShowJuBao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        请输入查询密码：<input name="txtKey" type="text" value="" id="txtKey" runat="server" />&nbsp;&nbsp;
        <asp:Button   ID="btnQuery" runat="server" Text="查询" onclick="btnQuery_Click" />
    </div>
    <div>    
        <span id="lblMsg" runat="server" style="color:Red;"></span>
    </div>

    </form>
</body>
</html>
