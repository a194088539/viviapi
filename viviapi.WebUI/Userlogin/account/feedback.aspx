<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feedback.aspx.cs" Inherits="viviapi.WebUI.Userlogin.account.feedback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/page.css" />
</head>
<body style="background: white;">
    <form id="form1" runat="server">
    <%-- <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/account/index.aspx'">我的账户</a>
        &nbsp;&gt;&nbsp; <span>留言反馈</span>
    </div>--%>
    <!--右部表单开始-->
    <div id="list_content">
        <%-- <div id="title">
            留言反馈&nbsp;<img id="loading" width="0" height="0" src="/style/008.gif" /></div>--%>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="10" colspan="3" align="left">
                </td>
            </tr>
            <tr>
                <td height="39" align="right">
                    类型:
                </td>
                <td align="left">
                    &nbsp;<select id="ddltypeid" runat="server" class="txt_01"><option value="1">BUG反馈</option>
                        <option value="2">意见建议</option>
                        <option value="3">产品咨询</option>
                        <option value="4">其他</option>
                    </select>
                </td>
                <td height="39" align="left">
                </td>
            </tr>
            <tr>
                <td height="39" align="right">
                    问题或建议:
                </td>
                <td align="left">
                    &nbsp;<input id="txttitle" runat="server" type="text" class="txt_01" maxlength="50" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="right">
                    具体描述:
                </td>
                <td align="left">
                    &nbsp;<textarea id="txtcontent" runat="server" class="txt_02_1" cols="60" rows="8"
                        style="margin-right: auto; width: 360px;"></textarea>
                </td>
                <td height="39" align="left">
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
            <tr>
                <td height="22" align="center" class="font8">
                    <asp:Button ID="b_save" runat="server" Text="保存" CssClass="btn btn-primary" OnClick="b_save_Click" />
                </td>
                &nbsp;
                <td align="right">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
