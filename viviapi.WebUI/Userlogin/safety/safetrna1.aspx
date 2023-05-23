<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="safetrna1.aspx.cs" Inherits="viviapi.WebUI.Userlogin.safety.safetrna1" %>

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
                onclick="parent.location.href='/Userlogin/safety/index.aspx'">安全中心</a>
        &nbsp;&gt;&nbsp; <span>实名认证</span>
    </div>
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            实名认证&nbsp;<img id="loading" width="0" height="0" src="/Userlogin/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01">
                    再次确认需要实名认证的信息
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    真实姓名:
                </td>
                <td align="center" class="line_01">
                    <input id="txtfullname" runat="server" type="text" class="txt_02" size="25" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    性别:
                </td>
                <td align="center" class="line_01">
                    <input id="txtmale" runat="server" type="text" class="txt_02" size="25" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    出生年月:
                </td>
                <td align="center" class="line_01">
                    <input id="txtbirthday" runat="server" type="text" class="txt_02" size="25" value="" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    身份证地址:
                </td>
                <td align="center" class="line_01">
                    <input id="txtlocation" runat="server" type="text" class="txt_02" size="25" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    身份证号:
                </td>
                <td align="center" class="line_01">
                    <input id="txtIdcard" runat="server" type="text" class="txt_02" size="25" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
            <tr>
                <td height="22" align="left" class="font8">
                    <asp:Button ID="btnSure" runat="server" Text="确认无误" CssClass="btn btn-primary" OnClick="btnSure_Click" />
                    &nbsp;
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
