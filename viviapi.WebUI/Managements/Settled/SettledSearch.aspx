<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.SettledSearch" Codebehind="SettledSearch.aspx.cs" %>

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
                <td align="center" style="font-weight: bold; font-size: 14px; background-image: url(Images/topbg.gif);
                    color: teal; background-repeat: repeat-x; height: 28px">
                    �����¼
                </td>
            </tr>
            <tr>
                <td>
                    <span style="float: left; margin-left: 2px"><span style="float: left">������</span><asp:DropDownList
                        ID="StatusList" runat="server">
                    </asp:DropDownList>
                        ������
                        <asp:DropDownList ID="SeachType" runat="server">
                            <asp:ListItem Value="">�����ޡ�</asp:ListItem>
                            <asp:ListItem Value="Userid">�̻�ID</asp:ListItem>
                            <asp:ListItem Value="UserName">�û���</asp:ListItem>
                            <asp:ListItem Value="ID">����ID</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="KeyWords" runat="server"></asp:TextBox>
                        ��ʼ��
                        <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                        ��ֹ��
                        <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                        </asp:Button></span>
                </td>
            </tr>
            <tr>
                <td>
                    �����ܶ<%=TotalMoney %>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                        <asp:Repeater ID="rptList" runat="server">
                            <HeaderTemplate>
                                <tr height="22" style="background-color: #507CD1; color: #fff">
                                    <td>
                                        ���
                                    </td>
                                    <td>
                                        �̻�ID
                                    </td>
                                    <td>
                                        �̻���
                                    </td>
                                    <td>
                                        ����
                                    </td>
                                    <td>
                                        ����
                                    </td>
                                    <td>
                                        ������
                                    </td>
                                    <td>
                                        ʵ�����
                                    </td>
                                    <td>
                                        ��˰
                                    </td>
                                    <td>
                                        ������
                                    </td>
                                    <td>
                                        ����ʱ��
                                    </td>
                                    <td>
                                        ״̬
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="#EFF3FB">
                                    <td>
                                        <%# Eval("ID")%>
                                    </td>
                                    <td>
                                        <a href="?action=paylistbyid&userid=<%#Eval("userid")%>">
                                            <%#Eval("userid")%>
                                        </a>
                                    </td>
                                    <td>
                                        <a href="javascript:sendInfo('<%# Eval("userid")%>')">
                                            <%#Eval("UserName")%>
                                        </a>
                                    </td>
                                    <td>
                                        <%# Eval("PayeeName")%>
                                    </td>
                                    <td>
                                        <%# Eval("Account")%>
                                    </td>
                                    <td>
                                        <%# Eval("amount","{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# (Convert.ToDecimal(Eval("amount")) - Convert.ToDecimal(Eval("Charges"))).ToString("f2")%>
                                    </td>
                                    <td>
                                        <%# Eval("Tax")%>
                                    </td>
                                    <td>
                                        <%# Eval("Charges")%>
                                    </td>
                                    <td>
                                        <%# Eval("PayTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%# Eval("StatusText")%>
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
                    <aspxc:AspNetPager ID="Pager1" runat="server" OnPageChanged="Pager1_PageChanged">
                    </aspxc:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
