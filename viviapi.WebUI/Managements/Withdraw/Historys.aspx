<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Historys"
    CodeBehind="Historys.aspx.cs" %>

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
                <td align="center" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                    color: teal; background-repeat: repeat-x; height: 28px">
                    �����¼
                </td>
            </tr>
            <tr>
                <td>
                    <span style="float: left; margin-left: 2px">
                        <asp:DropDownList ID="ddlStatusList" runat="server">
                        </asp:DropDownList>
                        �̻�ID��<asp:TextBox ID="txtUserId" runat="server" Width="80px"></asp:TextBox>
                        ����ID��<asp:TextBox ID="txtItemInfoId" runat="server" Width="80px"></asp:TextBox>
                        <asp:DropDownList ID="ddlbankName" runat="server">
                            <asp:ListItem Value="">--�տ�����--</asp:ListItem>
                            <asp:ListItem Value="0002">֧����</asp:ListItem>
                            <asp:ListItem Value="0003">�Ƹ�ͨ</asp:ListItem>
                            <asp:ListItem Value="1002">�й���������</asp:ListItem>
                            <asp:ListItem Value="1005">�й�ũҵ����</asp:ListItem>
                            <asp:ListItem Value="1003">�й���������</asp:ListItem>
                            <asp:ListItem Value="1026">�й�����</asp:ListItem>
                            <asp:ListItem Value="1001">��������</asp:ListItem>
                            <asp:ListItem Value="1006">��������</asp:ListItem>
                            <asp:ListItem Value="1020">��ͨ����</asp:ListItem>
                            <asp:ListItem Value="1025">��������</asp:ListItem>
                            <asp:ListItem Value="1009">��ҵ����</asp:ListItem>
                            <asp:ListItem Value="1027">�㷢����</asp:ListItem>
                            <asp:ListItem Value="1004">�ַ�����</asp:ListItem>
                            <asp:ListItem Value="1022">�������</asp:ListItem>
                            <asp:ListItem Value="1021">��������</asp:ListItem>
                            <asp:ListItem Value="1010">ƽ������</asp:ListItem>
                            <asp:ListItem Value="1066">�й�������������</asp:ListItem>
                        </asp:DropDownList>
                        �տ��˻���<asp:TextBox ID="txtAccount" runat="server" Width="80px"></asp:TextBox>
                        �տ��ˣ�<asp:TextBox ID="txtpayeeName" runat="server" Width="80px"></asp:TextBox>
                        <asp:DropDownList ID="ddlmode" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSupplier" runat="server">
                        </asp:DropDownList>
                        ��ʼ��
                        <asp:TextBox ID="txtStimeBox" runat="server" Width="65px"></asp:TextBox>
                        ��ֹ��
                        <asp:TextBox ID="txtEtimeBox" runat="server" Width="65px"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                        </asp:Button>
                        <asp:Button ID="btnExport" runat="server" CssClass="button" Text="����" OnClick="btnExport_Click"/>
                        </span>
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
                                <tr style="background-color: #507CD1; color: #fff; height: 22px">
                                    <td>
                                        ���
                                    </td>
                                    <td>
                                        �̻�
                                    </td>
                                    <td>
                                        �տ���Ϣ
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
                                            <%#Eval("UserName")%>(#<%#Eval("userid")%>)
                                        </a>
                                    </td>
                                    <td>
                                        <%# viviapi.BLL.SettledFactory.GetSettleBankName(Eval("PayeeBank").ToString())%> <%# Eval("Payeeaddress")%> <br />
                                        <%# Eval("payeeName")%> <br />
                                        <%# Eval("Account")%>                                      
                                    </td>
                                    <td>
                                        <%# Eval("amount","{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# (Convert.ToDecimal(Eval("amount")) - Convert.ToDecimal(Eval("Charges"))).ToString("f2")%>
                                    </td>
                                    <td>
                                        <%# Eval("Tax", "{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# Eval("Charges", "{0:f2}")%>
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
