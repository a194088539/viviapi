<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modiemail.aspx.cs" Inherits="viviapi.WebUI.Userlogin.safety.modiemail" %>

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
        &nbsp;&gt;&nbsp; <span>修改邮箱</span>
    </div>
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            修改邮箱&nbsp;<img id="loading" width="0" height="0" src="/Userlogin/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01">
                    ① 注：当前邮箱如果已经认证过,修改时系统会给您原邮箱地址发送确认邮件,确认后才能修改成功；<br />
                    ② 修改新邮箱成功后需重新进行认证。
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    当前邮箱:
                </td>
                <td align="center" class="line_01" align="left">
                    <input id="txtemail" runat="server" type="text" class="txt_01" size="100" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    新邮箱:
                </td>
                <td align="center" class="line_01" align="left">
                    <input id="txtnewemail" runat="server" type="text" class="txt_01" size="100" />
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
                    <asp:Button ID="btnSave" runat="server" Text="确认提交" CssClass="btn btn-primary" OnClick="btnSave_Click" />&nbsp;
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
