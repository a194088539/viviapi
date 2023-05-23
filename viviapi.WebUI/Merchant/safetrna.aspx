<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="safetrna.aspx.cs" Inherits="viviapi.WebUI.Merchant.safetrna" %>

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
                    ① 系统不支持一代身份证实名认证；<br />
② 实名认证的真实姓名必须与银行卡的户名一致,以免影响提现；<br />
③ 通过实名认证后，账户才能进行提现操作。
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    真实姓名:
                </td>
                <td align="center" class="line_01">
                    <input id="txtpername" runat="server" type="text" class="txt_02" size="100" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr id="tr_pernumber" runat="server">
                <td height="39" align="left" class="line_01">
                    身份证号:
                </td>
                <td align="center" class="line_01">
                    <input id="txtpernumber" runat="server" type="text" class="txt_02" size="100"  />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr id="tr_repernumber" runat="server">
                <td height="39" align="left" class="line_01">
                    确认身份证号:
                </td>
                <td align="center" class="line_01">
                    <input id="txtrpernumber" runat="server" type="text" class="txt_02" size="100"  />
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
                &nbsp;<asp:Label ID="lblMessage" runat="server" Visible="False" Font-Bold="True" 
                          ForeColor="Red"></asp:Label>
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
