<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="baseinfo.aspx.cs" Inherits="viviapi.WebUI.Merchant.baseinfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            基本信息&nbsp;<img id="loading" width="0" height="0" src="/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01">
                    账户基本信息,如需修改,请联系商务
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    商户ID:
                </td>
                <td align="center" class="line_01">
                    <input id="txtuserid" runat="server" type="text" class="txt_02" size="25"/>
                </td>
                <td height="39" align="left" class="line_01">
                    API接口对接的ID
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    商户名称:
                </td>
                <td align="center" class="line_01">
                    <input id="txtusername" runat="server" type="text" class="txt_02" size="25"/>
                </td>
                <td height="39" align="left" class="line_01">
                    平台登录的ID
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    商户级别:
                </td>
                <td align="center" class="line_01">
                    <input id="txtuserlev" runat="server" type="text" class="txt_02" size="25" value="" />
                </td>
                <td height="39" align="left" class="line_01">
                    不同的级别,对应不同的结算费率
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    联系QQ:
                </td>
                <td align="center" class="line_01">
                    <input id="txtqq" runat="server"  name="fQQ" type="text" class="txt_02" size="25" />
                </td>
                <td height="39" align="left" class="line_01">
                    联系人的QQ
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    注册邮箱:
                </td>
                <td align="center" class="line_01">
                    <input id="txtemail" runat="server" type="text" class="txt_02" size="25"/>
                </td>
                <td height="39" align="left" class="line_01">
                    用于找回密码和接收平台邮件
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    平台名称:
                </td>
                <td align="center" class="line_01">
                    <input id="txtsitename" runat="server" type="text" class="txt_02" size="25" />
                </td>
                <td height="39" align="left" class="line_01">
                    用于商户平台备案核实
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    平台网址:
                </td>
                <td align="center" class="line_01">
                    <input id="txtsiteUrl" runat="server" type="text" class="txt_02" size="25"  />
                </td>
                <td height="39" align="left" class="line_01">
                    用于商户平台备案核实
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    联系电话:
                </td>
                <td align="center" class="line_01">
                    <input  id="txtTel" runat="server" type="text" class="txt_02" size="25"   />
                </td>
                <td height="39" align="left" class="line_01">
                    联系人手机或电话
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    联系人:
                </td>
                <td align="center" class="line_01">
                    <input id="txtLinkMan" runat="server" type="text" class="txt_02" size="25"/>
                </td>
                <td height="39" align="left" class="line_01">
                    联系人称谓
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    状态:
                </td>
                <td align="center" class="line_01">
                    <input id="txtStatus" runat="server" type="text" class="txt_02" size="25" />
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
