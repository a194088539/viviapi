<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="repassword.aspx.cs" Inherits="viviapi.WebUI.Merchant.repassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            修改密码&nbsp;<img id="loading" width="0" height="0" src="/merchant/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01">
                    定期修改密码,有助于加强账户安全
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    商户ID:
                </td>
                <td align="center" class="line_01">
                    <input id="txtuserid" runat="server" type="text" class="txt_02" size="25" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    登录名称:
                </td>
                <td align="center" class="line_01">
                    <input id="txtusername" runat="server" type="text" class="txt_02" size="25"  />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    原密码:
                </td>
                <td align="center" class="line_01">
                    <input id="txtoldpassword" runat="server" type="password" class="txt_01" size="25" value="" />
                </td>
                <td height="39" align="left" class="line_01">
                    请输入原密码
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    新密码:
                </td>
                <td align="center" class="line_01">
                    <input id="txtnewpassword" runat="server" type="password" class="txt_01" size="25" value="" />
                </td>
                <td height="39" align="left" class="line_01">
                    请输入新密码,6-32位字符与数字组合
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    重复密码:
                </td>
                <td align="center" class="line_01">
                    <input id="txtrepassword" runat="server" type="password" class="txt_01" size="25" value="" />
                </td>
                <td height="39" align="left" class="line_01">
                    请再次输入一遍新密码
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
                     <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-primary" 
                        onclick="btnSave_Click" />&nbsp;
                    <input name="b$close" type="submit" class="button_01" value="关闭" onclick="javascript:window.parent.TINY.box.hide();" />
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
</asp:Content>
