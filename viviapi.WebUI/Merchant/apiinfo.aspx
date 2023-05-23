<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="apiinfo.aspx.cs" Inherits="viviapi.WebUI.Merchant.apiinfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            接口信息&nbsp;<img id="loading" width="0" height="0" src="/merchant/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01">
                    API接口对接信息,可以联系技术修改
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    商户ID:
                </td>
                <td align="center" class="line_01">
                    <input id="txtuserid" runat="server" type="text" class="txt_02" size="50" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    接口密钥(KEY):
                </td>
                <td align="center" class="line_01">
                    <input id="txtapikey" runat="server" type="text" class="txt_02" size="50"/>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>            
            <tr style="display:none">
                <td height="39" align="left" class="line_01">
                    备用通知地址:
                </td>
                <td align="center" class="line_01">
                    <input id="txtReturnUrl" runat="server" name="fReturnUrl" type="text" class="txt_01" size="50" value="" />
                </td>
                <td height="39" align="left" class="line_01">
                    当接口参数传入的通知地址无法访问时,会使用此地址重试
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
                        onclick="btnSave_Click" Visible="false" />&nbsp;
                    <asp:Button ID="btnModiKey" runat="server" Text="重置密钥" CssClass="btn btn-primary" 
                        onclick="btnModiKey_Click" />
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
