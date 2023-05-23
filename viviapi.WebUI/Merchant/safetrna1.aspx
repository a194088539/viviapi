<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="safetrna1.aspx.cs" Inherits="viviapi.WebUI.Merchant.safetrna1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            实名认证&nbsp;<img id="loading" width="0" height="0" src="/merchant/static/style/008.gif" /></div>
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
                    <input id="txtfullname" runat="server" type="text" class="txt_02" size="25"/>
                </td>
                <td height="39" align="left" class="line_01">
                    
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    性别:
                </td>
                <td align="center" class="line_01">
                    <input id="txtmale" runat="server" type="text" class="txt_02" size="25"/>
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
                    <input id="txtlocation" runat="server" type="text" class="txt_02" size="25"/>
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
                    <asp:Button ID="btnSure" runat="server" Text="确认无误" CssClass="btn btn-primary" 
                        onclick="btnSure_Click" /> &nbsp;
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
