<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="safeques.aspx.cs" Inherits="viviapi.WebUI.Merchant.safeques" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            基本信息&nbsp;<img id="loading" width="0" height="0" src="/merchant/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01">
                    您还未设置密码保护！请立即设置以便以后在找回密码等时使用。<br />
问题或答案最多可输入15个字。请保证您的密码问题不出现泄密，以保障您的隐私。<br />
如果设置答案的时候有带符号、空格等的，验证时也都必须输入完整<br />
示例：问题：我最喜欢说的一句话？ <br />
答案：你赢了<br />

                </td>
            </tr>
            <tr id="p_oldans" runat="server">
                <td height="39" align="left" class="line_01">
                    旧问题:
                </td>
                <td align="center" class="line_01">
                    <input id="txtoldques" runat="server" type="text" class="txt_02" size="80"/>
                </td>
                <td height="39" align="left" class="line_01">
                    
                </td>
            </tr>
            <tr id="p_oldques" runat="server">
                <td height="39" align="left" class="line_01">
                    旧答案:
                </td>
                <td align="center" class="line_01">
                    <input id="txtoldans" runat="server" type="text" class="txt_02" size="80"/>
                </td>
                <td height="39" align="left" class="line_01">
                    
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    新问题:
                </td>
                <td align="center" class="line_01">
                    <input id="txtnewques" runat="server" type="text" class="txt_02" size="80" value="" />
                </td>
                <td height="39" align="left" class="line_01">
                    
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    新答案:
                </td>
                <td align="center" class="line_01">
                    <input id="txtnewans" runat="server" type="text" class="txt_02" size="80"/>
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
                   <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn btn-primary" 
                        onclick="btnSave_Click" />
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
