<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true" CodeBehind="import.aspx.cs" Inherits="viviapi.WebUI.Merchant.import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="list_content">
        <div id="title">
            �ļ��ϴ�&nbsp;<img id="loading" width="0" height="0" src="/merchant/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="right" class="line_01">
                    ������֪��
                </td>
                <td class="line_01">
                    1������մ���ģ���ļ�������ʽ��д�����εĴ�����ϸ��Ϣ�� <a href="download/templetfile.xlsx">����</a>����ģ���ļ� 
<br />2��Ŀǰ֧�ֵ��տ�������16�ң��ֱ�Ϊ���������С�ũҵ���С��������С���ͨ���С��������С��й����С������������С��������С�������
     �С���ҵ���С��㷢���С��ַ����С�������С��������С�ƽ�����С��������С�
<br />3�����ڵ��ʴ����޶����ũ���������ļ��տ��������20��Ԫ�������������5��Ԫ��
<br />4�����շ�ϴǮ�涨����������1��Ԫ�ģ������б������Ŀ���û�����ʵ��Ч�����Ϣ��Ҫ������ʱ�ɲ顣��������5��Ԫ�ģ����򱴸�
     ��������տ��˵����֤��ӡ������д�������������κţ�������룺0571-86584668��������Ա�ݴ������Щ������ϸ��
<br />5���˻����Ӧ>=�������ϼ�+���������Ѻϼơ� �鿴��ǰ��������

                </td>
            </tr>
            <tr>
                <td z align="right" class="line_01">
                </td>
                <td align="left" class="line_01">
                    <asp:CheckBox ID="cbx_sure" runat="server" Text="������������֪Ϥ" AutoPostBack="true" OnCheckedChanged="cbx_sure_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 25%">
                </td>
                <td style="width: 75%" align="left" class="line_01">
                    <asp:FileUpload ID="file_data" runat="server" class="mutitxt_03" Width="80%" />
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 25%">
                </td>
                <td style="width: 75%">
                    <asp:Button ID="btnupload" runat="server" Text="ȷ���ϴ�" CssClass="btn btn-primary" 
                        onclick="btnupload_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
