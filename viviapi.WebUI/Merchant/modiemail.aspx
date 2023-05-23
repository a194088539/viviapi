<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="modiemail.aspx.cs" Inherits="viviapi.WebUI.Merchant.modiemail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            修改邮箱&nbsp;<img id="loading" width="0" height="0" src="/merchant/static/style/008.gif" /></div>
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
                <td align="center" class="line_01">
                    <input id="txtemail" runat="server" type="text" class="txt_02" size="100" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    新邮箱:
                </td>
                <td align="center" class="line_01">
                    <input id="txtnewemail" runat="server" type="text" class="txt_02" size="100"  />
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
                     <asp:Button ID="btnSave" runat="server" Text="确认提交" CssClass="btn btn-primary" 
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
