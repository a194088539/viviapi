<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="applycost.aspx.cs" Inherits="viviapi.WebUI.Merchant.applycost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input name="v$id" type="hidden" value="applycost" />
    <input name="v$fid" type="hidden" value="ruili" />
    <!--�Ҳ�����ʼ-->
    <div id="list_content">
        <div id="title">
            ��������&nbsp;<img id="loading" width="0" height="0" src="/merchant/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="6" align="left" class="line_01">
                    ��ʱ��������,����ǰ������ͻ�,�����ύ����,����ᶨʱ����ת��,лл����
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width:15%">
                    �̻�����:
                </td>
                <td align="left" class="line_01" style="width:35%">
                    <input id="txtUserName" runat="server" type="text" class="txt_02" size="25"/>
                </td>
                <td height="39" align="left" class="line_01" colspan="4">
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01">
                    �����:
                </td>
                <td align="left" class="line_01">
                    <input id="txtBalance" runat="server" type="text" class="txt_02" size="25" />
                </td>
                <td height="39" align="left" class="line_01" style="width:15%" colspan="4">
                    <asp:Literal ID="litBalance" runat="server"></asp:Literal>     
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width:15%">
                    ��������:
                </td>
                <td align="left" class="line_01" style="width:35%">
                    T+<asp:Literal ID="litbankdetentiondays" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left" class="line_01" style="width:15%">
                    �����������ֽ��
                </td>
                 <td align="left" class="line_01" style="width:35%">
                    <asp:Literal ID="litbankdetentionAmt" runat="server"></asp:Literal>
                </td>
            </tr>            
            <tr>
                <td height="39" align="right" class="line_01">
                    �㿨����:
                </td>
                <td align="left" class="line_01">
                    T+<asp:Literal ID="litcarddetentiondays" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left" class="line_01">
                    �㿨�������ֽ��
                </td>
                 <td align="left" class="line_01">
                    <asp:Literal ID="litcarddetentionAmt" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01">
                    ��������:
                </td>
                <td align="left" class="line_01">
                    T+<asp:Literal ID="litotherdetentiondays" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left" class="line_01">
                    �����������ֽ��
                </td>
                 <td align="left" class="line_01">
                    <asp:Literal ID="litotherdetentionAmt" runat="server"></asp:Literal>
                </td>
            </tr>            
            <tr>
                <td height="39" align="right" class="line_01">
                    �������ֽ��:
                </td>
                <td align="left" class="line_01">
                    <input id="txtApplyMoney"  runat="server" type="text" class="txt_01" size="25" value="" />
                </td>
                <td height="39" align="left" class="line_01">
                    
                </td>
            </tr> 
            <tr>
                <td height="39" align="right" class="line_01">
                    ��������:
                </td>
                <td align="left" class="line_01">
                    <asp:TextBox ID="txtcashpwd" runat="server" CssClass="txt_02" TextMode="Password" MaxLength="25"></asp:TextBox>                    
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
                    <asp:Button ID="btnpost" runat="server" Text="����" CssClass="btn btn-primary" 
                        onclick="btnpost_Click" />&nbsp;
                    <input name="b$close" type="submit" class="button_01" value="�ر�" onclick="javascript:window.parent.TINY.box.hide();" />
                &nbsp;<asp:Label ID="lblMessage" runat="server" Visible="False" Font-Bold="True" 
                        ForeColor="#FF3300"></asp:Label>
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
