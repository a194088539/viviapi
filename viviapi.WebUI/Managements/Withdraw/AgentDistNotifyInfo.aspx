<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Withdraw.AgentDistNotifyInfo"
    CodeBehind="AgentDistNotifyInfo.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
    <link href="../style/page_show.css" type="text/css" rel="stylesheet" />

    <script src="../../js/common.js" type="text/javascript"></script>

    <style type="text/css">
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" class="htitle">
                ��ϸ��Ϣ�鿴
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="td2">
                ��� ��
            </td>
            <td class="td1">
                <asp:Label ID="lblid" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                ֪ͨID ��
            </td>
            <td class="td1">
                <asp:Label ID="lblnotify_id" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                �û�ID ��
            </td>
            <td class="td1">
                <asp:Label ID="lbluserid" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                ����� ��
            </td>
            <td class="td1">
                <asp:Label ID="lbltrade_no" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                �̻������ ��
            </td>
            <td class="td1">
                <asp:Label ID="lblout_trade_no" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                ״̬ ��
            </td>
            <td class="td1">
                <asp:Label ID="lblnotifystatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                ֪ͨUrl ��
            </td>
            <td class="td1">
                <asp:Label ID="lblnotifyurl" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                ���� ��
            </td>
            <td class="td1">
                <asp:Label ID="lblresText" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                ����ʱ�� ��
            </td>
            <td class="td1">
                <asp:Label ID="lbladdTime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                ����ʱ�� ��
            </td>
            <td class="td1">
                <asp:Label ID="lblresTime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                ext1 ��
            </td>
            <td class="td1">
                <asp:Label ID="lblext1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                ext2 ��
            </td>
            <td class="td1">
                <asp:Label ID="lblext2" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                ext3 ��
            </td>
            <td class="td1">
                <asp:Label ID="lblext3" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                ˵�� ��
            </td>
            <td class="td1">
                <asp:Label ID="lblremark" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
