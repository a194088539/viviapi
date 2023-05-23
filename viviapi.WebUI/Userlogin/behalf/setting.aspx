<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="setting.aspx.cs" Inherits="viviapi.WebUI.Userlogin.behalf.setting2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/behalf/index.aspx'">对私代发</a>
        &nbsp;&gt;&nbsp; <span>代发设置</span>
    </div>
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            代发选项&nbsp;<img id="loading" width="0" height="0" src="/Userlogin/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 30%">
                    接口代发是否需要确认:
                </td>
                <td align="left" class="line_01" style="width: 70%">
                    <asp:RadioButtonList ID="rbl_set" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">需要确认</asp:ListItem>
                        <asp:ListItem Value="0">不需要确认</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                </td>
                <td align="left" class="line_01">
                    <asp:Button ID="btnupdate" runat="server" Text="保存" CssClass="btn btn-primary" OnClick="btnupdate_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
