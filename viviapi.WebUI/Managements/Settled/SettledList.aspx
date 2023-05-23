<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.SettledList" Codebehind="SettledList.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript">
        function sendInfo(id) {
            window.open("../User/UserEdit.aspx?id=" + id, "�鿴�û���Ϣ", "Width=800px;Height=350px;");
        }
    </script>
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
    <script type="text/javascript">
        function sendInfo(id) {
            window.open("../User/UserEdit.aspx?id=" + id, "�鿴�û���Ϣ", "Width=800px;Height=350px;");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; height: 100%;">
            <tr>
                <td align="center" class="headtitle">
                    �������
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                    <asp:DropDownList ID="ddlAcctype" runat="server">
                    <asp:ListItem Value="0">���˺����͡�</asp:ListItem>
                     <asp:ListItem Value="1">�����˻�</asp:ListItem>
                    <asp:ListItem Value="2">֧����</asp:ListItem>
                     <asp:ListItem Value="3">�Ƹ�ͨ</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="StatusList" runat="server">
                    </asp:DropDownList>
                        &nbsp;<asp:DropDownList ID="SeachType" runat="server">
                            <asp:ListItem Value="">�����ޡ�</asp:ListItem>
                            <asp:ListItem Value="UserID">�̻�ID</asp:ListItem>
                            <asp:ListItem Value="ID">����ID</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="KeyWords" runat="server"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                        </asp:Button>
                        <asp:Button ID="btnExport" runat="server" CssClass="button" Width="164px" Text="����֧���е�txt�ļ�"
                            OnClick="btnExport_Click"></asp:Button>
                            
                             �������룺<asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox>         
                <asp:Button ID="btnAllSettle" runat="server" CssClass="button" Text="����֧��" 
                    onclick="btnAllSettle_Click" OnClientClick="return checkAll();">
                </asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                        <asp:Repeater ID="rptList" runat="server">
                            <HeaderTemplate>
                                <tr height="22" style="background-color: #507CD1; color: #fff">
                                    <td style="width: 5%">
                                        ���
                                    </td>
                                    <td style="width: 8%">
                                        �̻���
                                    </td>
                                    <td style="width: 8%">
                                        ����
                                    </td>
                                    <td style="width: 12%">
                                        ����
                                    </td>
                                    <td style="width: 8%">
                                        ������
                                    </td>
                                    <td style="width: 8%">
                                        �� �� ��
                                    </td>
                                    <td style="width: 8%">
                                        ʵ�����
                                    </td>
                                    <td style="width: 8%">
                                        ����
                                    </td>
                                    <td style="width: 10%">
                                        ����ʱ��
                                    </td>
                                    <td style="width: 15%">
                                        ״̬
                                    </td>
                                    <td>
                                        <input id="Checkboxall" type="checkbox" class="qx" onclick="checkall(this)" />
                                        ȫѡ
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="#EFF3FB">
                                    <td>
                                        <%# Eval("ID")%>
                                    </td>
                                    <td>
                                        <a href="javascript:sendInfo('<%# Eval("userid")%>')">
                                            <%#Eval("UserName")%>
                                        </a>
                                    </td>
                                    <td>
                                        <%# Eval("PayeeName")%>
                                    </td>
                                    <td >
                                        <%# Eval("Account","{0:f2}")%>
                                    </td>
                                    <td style="text-align:right">
                                        <%# Eval("amount", "{0:f2}")%>
                                    </td>
                                    <td style="text-align:right">
                                        <%# Eval("Charges", "{0:f2}")%>
                                    </td>
                                    <td style="text-align:right">
                                        <%# (Convert.ToDecimal(Eval("amount")) - Convert.ToDecimal(Eval("Charges"))).ToString("f2")%>
                                    </td>
                                    <td>
                                        <%#Enum.GetName(typeof(viviapi.Model.SettledmodeEnum), Eval("settmode"))%>
                                    </td>
                                    <td>
                                        <%# Eval("AddTime","{0:yyyy-MM-dd HH:mm:ss}") %>
                                    </td>
                                    <td>
                                        <%# Eval("StatusText")%>
                                    </td>
                                    <td>
                                        <input id="<%# Eval("ID") %>" type="checkbox" name="ischecked" class="qx" value="<%# Eval("ID") %>" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <tr>
                        <td colspan="20">
                            <aspxc:AspNetPager ID="Pager1" runat="server" OnPageChanging="Pager1_PageChanging"
                                AlwaysShow="True" CustomInfoHTML="�ܼ�¼����%RecordCount%&nbsp;��ҳ����%PageCount%&nbsp;��ǰҳ��%CurrentPageIndex%&nbsp;"
                                CustomInfoTextAlign="Left" FirstPageText="��ҳ" HorizontalAlign="Right" LastPageText="ĩҳ"
                                NavigationToolTipTextFormatString="��ת{0}ҳ" NextPageText="��һҳ" PageIndexBoxType="DropDownList"
                                PageSize="20" PrevPageText="��һҳ" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="ҳ" TextBeforePageIndexBox="����" Width="100%" Height="30px">
                            </aspxc:AspNetPager>
                        </td>
                    </tr>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
