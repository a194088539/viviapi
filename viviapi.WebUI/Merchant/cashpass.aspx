<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="cashpass.aspx.cs" Inherits="viviapi.WebUI.Merchant.cashpass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            提现密码&nbsp;<img id="loading" width="0" height="0" src="/merchant/static/style/008.gif" /></div>
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
                    登录密码:
                </td>
                <td align="center" class="line_01">
                    <input id="txtloginpwd" runat="server" type="password" class="txt_01" size="25" value="" />
                </td>
                <td height="39" align="left" class="line_01">
                    请输入登录密码
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    您的账户邮箱:
                </td>
                <td align="center" class="line_01">
                    <input id="txtmail" runat="server" type="text" class="txt_01" size="25" value="" />
                </td>
                <td height="39" align="left" class="line_01">
                    请输入你的安全邮箱
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    提现密码:
                </td>
                <td align="center" class="line_01">
                    <input id="txtcashpass" runat="server" type="password" class="txt_01" size="25" value="" />
                </td>
                <td height="39" align="left" class="line_01">
                    请输入提现密码
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    确认提现密码:
                </td>
                <td align="center" class="line_01">
                    <input id="txtrecashpass" runat="server" type="password" class="txt_01" size="25" value="" />
                </td>
                <td height="39" align="left" class="line_01">
                    请再次输入提现密码
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
