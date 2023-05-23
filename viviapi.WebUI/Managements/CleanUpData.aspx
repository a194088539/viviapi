<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.Console_CleanUpData" Codebehind="CleanUpData.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�������� - �̻���̨</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="style/union.css" type="text/css" rel="stylesheet" />

    <script src="../../js/ControlDate/WdatePicker.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellspacing="1" cellpadding="2" border="0" align="center">
            <tr align="left">
                <td align="center" colspan="3" style="font-weight: bold; font-size: 14px; background-image: url(Images/topbg.gif);
                    color: teal; background-repeat: repeat-x; height: 25px">
                    ��������
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellpadding="3" cellspacing="1">
            <tr bgcolor="#F7F3F7">
                <td style="width: 15%; text-align: right">
                    ҵ�����ͣ�
                </td>
                <td>
                    <asp:CheckBoxList ID="cbl_clearType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="order">���׶���</asp:ListItem>
                        <asp:ListItem Value="settled">�����¼</asp:ListItem>
                        <asp:ListItem Value="stat">�ۺ�ͳ��</asp:ListItem>                        
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr bgcolor="#F7F3F7">
                <td style="width: 15%; text-align: right">
                    �������ͣ�
                </td>
                <td>
                    <asp:CheckBoxList ID="cb_where" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="bank">��������</asp:ListItem>
                        <asp:ListItem Value="card">�㿨����</asp:ListItem>
                        <asp:ListItem Value="sms">���Ž���</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr bgcolor="#F7F3F7">
                <td style="width: 15%; text-align: right">
                    ״̬ѡ��
                </td>
                <td>
                    <asp:CheckBoxList ID="cb_stat" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="2">�ѳɹ�</asp:ListItem>
                        <asp:ListItem Value="1">������</asp:ListItem>
                        <asp:ListItem Value="4">ʧ��</asp:ListItem>
                        <asp:ListItem Value="8">����</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr bgcolor="#F7F3F7">
                <td colspan="6">
                    ��������󲻿ɻָ�������ʹ�ã�
                </td>
            </tr>
            <tr bgcolor="#F7F3F7">
                <td style="width: 15%; text-align: right">
                    ѡ�����ڣ�
                </td>
                <td colspan="6">
                    <asp:TextBox ID="EtimeBox" runat="server" Width="80px"></asp:TextBox>
                    <span>�����������֮ǰ����������</span>
                </td>
            </tr>
            <tr bgcolor="#F7F3F7">
                <td style="width: 15%; text-align: right">
                    �������룺
                </td>
                <td colspan="6" bgcolor="#F7F3F7">
                    <asp:TextBox ID="txtcaozuo" runat="server" Width="80px" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr bgcolor="#F7F3F7">
                <td style="width: 15%; text-align: right">
                    
                </td>
                <td >
                    <asp:Button ID="btnCleanUp" runat="server" Text=" ȷ������ " CssClass="button" OnClick="btndel_Click" />
                    <asp:Label ID="lbmsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
