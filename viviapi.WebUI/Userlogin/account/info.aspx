<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="info.aspx.cs" Inherits="viviapi.WebUI.Userlogin.account.info" %>

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
                onclick="parent.location.href='/Userlogin/account/index.aspx'">我的账户</a>
        &nbsp;&gt;&nbsp; <span>基本信息</span>
    </div>
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            基本信息&nbsp;<img id="loading" width="0" height="0" src="/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="color: Black;">
            <tr>
                <td height="42" colspan="2" align="left" class="line_01" style="font-size: 16px;
                    line-height: 22px;">
                    <h3>
                        基本资料</h3>
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="width: 150px; text-align: right;
                    border: none; font-weight: bold;">
                    商户ID：
                </td>
                <td align="left" class="line_01" style="padding-left: 16px; border: none; font-weight: bold;">
                    <input id="txtuserid" runat="server" type="text" size="25" style="font-weight: bold;" />
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;">
                    登陆名称：
                </td>
                <td align="left" class="line_01" style="padding-left: 16px; border: none;">
                    <input id="txtusername" runat="server" type="text" size="25" />
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;">
                    真实姓名：
                </td>
                <td align="left" class="line_01" style="padding-left: 16px; border: none;">
                    <input id="txtLinkMan" runat="server" type="text" size="25" />
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;">
                    身份证号：
                </td>
                <td align="left" class="line_01" style="padding-left: 16px; border: none;">
                    <%=getidcard %>
                </td>
            </tr>
            <tr>
                <td height="42" colspan="2" align="left" class="line_01" style="font-size: 16px;
                    line-height: 22px;">
                    <h3>
                        联系方式</h3>
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;">
                    手机号码：
                </td>
                <td align="left" class="line_01" style="padding-left: 16px; border: none;">
                    <%=gettel %>
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;">
                    E-mail：
                </td>
                <td align="left" class="line_01" style="padding-left: 16px; border: none;">
                    <%=getemail %>
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;">
                    QQ号码：
                </td>
                <td align="left" class="line_01" style="padding-left: 16px; border: none;">
                    <input id="txtqq" runat="server" name="fQQ" type="text" size="25" />
                </td>
            </tr>
            <tr>
                <td height="45" colspan="2" align="left" class="line_01" style="font-size: 16px;
                    line-height: 22px;">
                    <h3>
                        其它资料</h3>
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;">
                    平台名称：
                </td>
                <td align="left" class="line_01" style="padding-left: 16px; border: none;">
                    <input id="txtsitename" runat="server" type="text" size="25" />
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;">
                    官方网址：
                </td>
                <td align="left" class="line_01" style="padding-left: 16px; border: none;">
                    <input id="txtsiteUrl" runat="server" type="text" size="25" />
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;">
                    商户级别：
                </td>
                <td align="left" class="line_01" style="padding-left: 16px; border: none;">
                    <input id="txtuserlev" runat="server" type="text" size="25" value="" />
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style=" font-weight: bold;">
                    商户状态：
                </td>
                <td align="left" class="line_01" style="padding-left: 16px;">
                    <input id="txtStatus" runat="server" type="text" size="25" />
                </td>
            </tr>
            <tr>
                <td height="45" align="left" class="line_01" style="border: none; padding-left:50px; line-height:50px;" colspan="2">
                    如需修改信息资料，请联系QQ：1833366652
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
