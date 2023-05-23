<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Withdraw.AgentDistNotifys" CodeBehind="AgentDistNotifys.aspx.cs" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript">
        function Setchkall(obj) {
            var objs = document.getElementsByName("chk");
            for (i = 0; i < objs.length; i++) {
                objs[i].checked = obj.checked;
            }
        }
        function checkall(obj) {
            var check = document.getElementsByName("ischecked");
            for (i = 0; i < check.length; i++) {
                check[i].checked = obj.checked;
            }
        }
    </script>

    <script type="text/javascript">
        function sendInfo(id) {
            window.open("../User/UserEdit.aspx?id=" + id, "�鿴�û���Ϣ", "Width=800px;Height=350px;");
        }
        function showDetail(id) {
            window.open("AgentDistNotifyInfo.aspx?id=" + id, "�鿴����", "height=500,width=800");
        }
    </script>

    <style type="text/css">
        table
        {
            font-weight: normal;
            font-size: 12px;
            line-height: 170%;
            font-family: Arial;
        }
        td
        {
            height: 11px;
        }
        A:link
        {
            color: #02418a;
            text-decoration: none;
        }
    </style>
    <script src="../../Js/ControlDate/WdatePicker.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; height: 100%;">
            <tr>
                <td align="center" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                    color: teal; background-repeat: repeat-x; height: 28px">
                    ��˽����(���֪ͨ)
                </td>
            </tr>
            <tr>
                <td>
                    �̻�ID��<asp:TextBox ID="txtUserId" runat="server" Width="80px"></asp:TextBox>
                    ϵͳ���׺ţ�<asp:TextBox ID="txttrade_no" runat="server" Width="120px"></asp:TextBox> 
                    �̻����׺ţ�<asp:TextBox ID="txtout_trade_no" runat="server" Width="120px"></asp:TextBox>                   
                    <asp:DropDownList ID="ddlnotifystatus" runat="server">
                        <asp:ListItem Value="">--״̬--</asp:ListItem>
                        <asp:ListItem Value="0">֪ͨʧ��</asp:ListItem>
                        <asp:ListItem Value="1">������</asp:ListItem>
                        <asp:ListItem Value="2">֪ͨ�ɹ�</asp:ListItem>
                    </asp:DropDownList>
                     ��ʼ��
                    <asp:TextBox ID="txtStimeBox" runat="server" Width="65px"></asp:TextBox>
                    ��ֹ��
                    <asp:TextBox ID="txtEtimeBox" runat="server" Width="65px"></asp:TextBox>
                    
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                    </asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                        <asp:Repeater ID="rptList" runat="server" >
                            <HeaderTemplate>
                                <tr style="background-color: #507CD1; color: #fff; height: 22px">
                                    <td style="width:3%">
                                        ���
                                    </td>
                                    <td style="width:8%">
                                        ֪ͨID
                                    </td>
                                    <td style="width:8%">
                                        ϵͳ����
                                    </td>
                                    <td style="width:8%">
                                        �̻�����
                                    </td>
                                    <td style="width:7%">
                                        �̻�
                                    </td>
                                    <td style="width:7%">
                                        ״̬
                                    </td>
                                    <td style="width:10%">
                                        ��������
                                    </td>
                                    <td style="width:10%">
                                        ʱ��
                                    </td>                                    
                                    <td>
                                        ����
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="#EFF3FB" ondblclick="javascript:showDetail('<%# Eval("id")%>')">
                                    <td>
                                        <%# Eval("ID")%>
                                    </td>
                                    <td>
                                        <%# Eval("notify_id")%>
                                    </td>
                                    <td>
                                        <%# Eval("trade_no")%>
                                    </td>
                                    <td>
                                        <%# Eval("out_trade_no")%>
                                    </td>
                                    <td>
                                        <a href="?action=paylistbyid&userid=<%#Eval("userid")%>">
                                           <%#Eval("userid")%></a>
                                    </td>
                                    <td>
                                        <%#notifyBLL.GetNotifyStatusText(Eval("notifystatus"))%>
                                    </td>
                                     <td>
                                        <%#Server.HtmlDecode(Eval("resText").ToString())%>
                                    </td>
                                    <td>
                                        <%# Eval("addTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                         
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                    <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CustomInfoHTML="�ܼ�¼����%RecordCount%&nbsp;��ҳ����%PageCount%&nbsp;��ǰҳ��%CurrentPageIndex%&nbsp;"
                        CustomInfoTextAlign="Left" FirstPageText="��ҳ" HorizontalAlign="Right" LastPageText="ĩҳ"
                        NavigationToolTipTextFormatString="��ת{0}ҳ" NextPageText="��һҳ" PageIndexBoxType="DropDownList"
                        PageSize="20" PrevPageText="��һҳ" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                        ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                        TextAfterPageIndexBox="ҳ" TextBeforePageIndexBox="����" Width="100%" Height="30px"
                        OnPageChanged="Pager1_PageChanged">
                    </aspxc:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
