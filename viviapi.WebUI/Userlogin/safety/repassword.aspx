<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="repassword.aspx.cs" Inherits="viviapi.WebUI.Userlogin.safety.repassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/safety/index.aspx'">安全中心</a>
        &nbsp;&gt;&nbsp; <span>密码修改</span>
    </div>
    <!--右部表单开始-->
     <div id="Div2">
        <div id="Div3" style="color: Black;">
            <h2>
                登陆密码修改</h2>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left"  style="color: Black; border: none;">
                    定期修改密码,有助于加强账户安全
                </td>
            </tr>
            <tr>
                <td height="45" align="right"  style="border: none; font-weight: bold;
                    color: Black;" width="150">
                    商户ID：
                </td>
                <td align="left"  style="padding-left: 15px; border: none; width: 240px;">
                   <input id="txtuserid" runat="server" type="text" class="txt_02" size="25" />
                </td>
                <td height="45" align="left"  style="border: none;">
                </td>
            </tr>
            <tr>
                <td height="45" align="right"  style="border: none; font-weight: bold;
                    color: Black;">
                    登录名称：
                </td>
                <td align="left"  style="padding-left: 15px; border: none;">
                    <input id="txtusername" runat="server" type="text" class="txt_02" size="25" />
                </td>
                <td height="45" align="left"  style="border: none;">
                </td>
            </tr>
            <tr>
                <td height="45" align="right"  style="border: none; font-weight: bold;
                    color: Black;">
                    原密码：
                </td>
                <td align="left"  style="padding-left: 15px; border: none;">
                     <input id="txtoldpassword" runat="server" type="password" class="txt_01" size="25"
                        value="" />
                </td>
                <td height="45" align="left"  style="border: none;">
                    请输入原密码
                </td>
            </tr>
            <tr>
                <td height="45" align="right"  style="border: none; font-weight: bold;
                    color: Black;">
                    新密码：
                </td>
                <td align="left"  style="padding-left: 15px; border: none;">
                    <input id="txtnewpassword" runat="server" type="password" class="txt_01" size="25"
                        value="" />
                </td>
                <td height="45" align="left"  style="border: none;">
                    请输入新密码,6-32位字符与数字组合
                </td>
            </tr>
            <tr>
                <td height="45" align="right"  style="border: none; font-weight: bold;
                    color: Black;">
                    重复密码：
                </td>
                <td align="left"  style="padding-left: 15px; border: none;">
                    <input id="txtrepassword" runat="server" type="password" class="txt_01" size="25"
                        value="" />
                </td>
                <td height="45" align="left"  style="border: none;">
                    请再次输入一遍新密码
                </td>
            </tr>
            <tr>
                <td height="45" align="right"  style="border: none;">
                </td>
                <td align="left"  style="padding-left: 15px; border: none;">
                     <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-primary" OnClick="btnSave_Click" />&nbsp;
                    <input name="b$close" type="submit" class="btn btn-primary" value="关闭" onclick="javascript:window.parent.TINY.box.hide();" />
                </td>
                <td height="45" align="left"  style="border: none;">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
