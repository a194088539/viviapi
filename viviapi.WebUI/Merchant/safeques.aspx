<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="safeques.aspx.cs" Inherits="viviapi.WebUI.Merchant.safeques" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--�Ҳ�����ʼ-->
    <div id="list_content">
        <div id="title">
            ������Ϣ&nbsp;<img id="loading" width="0" height="0" src="/merchant/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01">
                    ����δ�������뱣���������������Ա��Ժ����һ������ʱʹ�á�<br />
��������������15���֡��뱣֤�����������ⲻ����й�ܣ��Ա���������˽��<br />
������ô𰸵�ʱ���д����š��ո�ȵģ���֤ʱҲ��������������<br />
ʾ�������⣺����ϲ��˵��һ�仰�� <br />
�𰸣���Ӯ��<br />

                </td>
            </tr>
            <tr id="p_oldans" runat="server">
                <td height="39" align="left" class="line_01">
                    ������:
                </td>
                <td align="center" class="line_01">
                    <input id="txtoldques" runat="server" type="text" class="txt_02" size="80"/>
                </td>
                <td height="39" align="left" class="line_01">
                    
                </td>
            </tr>
            <tr id="p_oldques" runat="server">
                <td height="39" align="left" class="line_01">
                    �ɴ�:
                </td>
                <td align="center" class="line_01">
                    <input id="txtoldans" runat="server" type="text" class="txt_02" size="80"/>
                </td>
                <td height="39" align="left" class="line_01">
                    
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    ������:
                </td>
                <td align="center" class="line_01">
                    <input id="txtnewques" runat="server" type="text" class="txt_02" size="80" value="" />
                </td>
                <td height="39" align="left" class="line_01">
                    
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    �´�:
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
                   <asp:Button ID="btnSave" runat="server" Text="����" CssClass="btn btn-primary" 
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
