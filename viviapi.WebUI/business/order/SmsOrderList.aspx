<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.business.Order.SmsOrderList" Codebehind="SmsOrderList.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />

    <script src="../../js/ControlDate/WdatePicker.js" type="text/javascript"></script>

    <style type="text/css">
        table
        {
            font-weight: normal;
            font-size: 12px;
            line-height: 170%;
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
            window.open("SmsOrderShow.aspx?id=" + id, "�鿴����", "height=760,width=1000");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="0" class="table1">
        <tr>
            <td align="center" colspan="3" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 28px">
                ���Ŷ�����ѯ
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td colspan="2">
                            �̻�ID��
                            <asp:TextBox ID="txtuserid" runat="server" Width="40px"></asp:TextBox>
                            <%--  ����ID��
                <asp:TextBox ID="txtpromid" runat="server" Width="30px"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlOrderStatus" runat="server" Width="95px">
                                <asp:ListItem>--����״̬--</asp:ListItem>
                                <asp:ListItem Value="1">������</asp:ListItem>
                                <asp:ListItem Value="2">�ѳɹ�</asp:ListItem>
                                <asp:ListItem Value="4">ʧ��</asp:ListItem>
                                <asp:ListItem Value="8">����</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlNotifyStatus" runat="server" Width="95px">
                                <asp:ListItem>--�·�״̬--</asp:ListItem>
                                <asp:ListItem Value="1">������</asp:ListItem>
                                <asp:ListItem Value="2">�ѳɹ�</asp:ListItem>
                                <asp:ListItem Value="4">ʧ��</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlmange" runat="server"></asp:DropDownList>
                            ��ʼ��
                            <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                            ��ֹ��
                            <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                              &nbsp&nbsp�ֻ����룺<asp:TextBox ID="txtmobile" runat="server" Width="120px"></asp:TextBox>
                            <asp:Button ID="btn_Search" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btn_Search_Click">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            �����ţ�<asp:TextBox ID="txtOrderId" runat="server" Width="160px"></asp:TextBox>
                            �̻������ţ�<asp:TextBox ID="txtUserOrder" runat="server" Width="160px"></asp:TextBox>
                            �ӿ��̶����ţ�<asp:TextBox ID="txtSuppOrder" runat="server" Width="160px"></asp:TextBox>
                        </td>
                        <td>
                            <div runat="server" id="divmoney">
                                <span style="color: #ff0000; text-align: left">�ܶ<% = TotalTranATM %></span> <span
                                    style="color: #ff0000; text-align: left;" runat="server" id="spangmmoney">�̻����ã�<% = TotalUserATM %></span>
                                    <span style="color: #ff0000; text-align: left;">ҵ������ɣ�<% = TotalCommission %></span>
                                <span style="color: #ff0000; text-align: left; display: none">��������ɣ�<% = TotalPromATM %></span>
                                <span style="color: #ff0000; text-align: left;">ƽ̨����<% = TotalProfit%></span>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" bgcolor="#F9F9F9">
            </td>
        </tr>
        <tr>
            <td bgcolor="#ffffff">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="center">
                            <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                                <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand"
                                    OnItemDataBound="rptOrders_ItemDataBound">
                                    <HeaderTemplate>
                                        <tr height="22" style="background-color: #507CD1; color: #fff">
                                            <td>
                                                �̻�ID
                                            </td>
                                            <td>
                                                �̻�������
                                            </td>
                                            <td>
                                                ������
                                            </td>
                                            <td>
                                                �ӿ��̽��׺�
                                            </td>
                                            <td>
                                                �ֻ�����
                                            </td>
                                            <td>
                                                ������
                                            </td>                                            
                                            <td>
                                                ��������
                                            </td>
                                            <td>
                                                ���
                                            </td>
                                            <td>
                                                �̻�
                                            </td>
                                            <td>
                                                ƽ̨
                                            </td>
                                            <td>
                                                ҵ��
                                            </td>
                                             <td>
                                                ����
                                            </td>
                                            <td>
                                                ����ʱ��
                                            </td>
                                            <td>
                                                ״̬
                                            </td>
                                            <td>
                                                �·�״̬
                                            </td>
                                            <td>
                                                �ӿ���
                                            </td>
                                            <td>
                                                ������
                                            </td>
                                            <td>
                                                ����
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr bgcolor="#EFF3FB" ondblclick="javascript:sendInfo('<%# Eval("id")%>')">
                                            <td>
                                                <a href="?action=userpay&userid=<%# Eval("userid") %>&status=2">
                                                    <%# Eval("userid")%>
                                                </a>
                                            </td>
                                            <td>
                                                <%# Server.HtmlEncode(Eval("userorder").ToString())%>
                                            </td>
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>
                                            <td>
                                                <%# Eval("linkid")%>
                                            </td>
                                            <td>
                                                <%# Eval("mobile")%>
                                            </td>
                                            <td>
                                                <%# Eval("servicenum")%>
                                            </td>                                            
                                            <td>
                                                <%# Eval("message")%>
                                            </td>
                                            <td>
                                                <%# Eval("fee","{0:0}")%>
                                            </td>
                                            <td>
                                                <%# Eval("payAmt", "{0:f2}")%>
                                            </td>
                                            <td>
                                                <%# Eval("supplierAmt", "{0:f2}")%>
                                            </td>
                                            <td>
                                                <%# Eval("commission", "{0:f2}")%>
                                            </td>                                            
                                             <td>
                                                <%# Eval("profits", "{0:f2}")%>
                                            </td>
                                            <td>
                                                <%# Eval("completetime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                            </td>
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum),Eval("status"))%>
                                            </td>
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderNofityStatusEnum), Eval("notifystat"))%>
                                            </td>
                                            <td>
                                                <%# viviapi.WebUI.WebUtility.GetsupplierName(Eval("supplierId"))%>
                                            </td>
                                            <td>
                                                <%# Eval("server")%>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnReissue" runat="server" Text="����" ToolTip="�ֶ��ط�" CommandName="Reissue" CommandArgument='<%# Eval("orderid")%>' />
                                                <asp:Button ID="btnRest" runat="server" Text="����" CommandName="ResetOrder" CommandArgument='<%#GetParm(Eval("orderid"),Eval("supplierId"),Eval("fee"))%>' />
                                                <asp:Button ID="btnDeduct" runat="server" Text="��"  ToolTip="����" CommandName="Deduct" CommandArgument='<%# Eval("orderid")%>' />
                                                <asp:Button ID="btnReDeduct" runat="server" Text="��"  CommandName="ReDeduct" CommandArgument='<%# Eval("orderid")%>' />
                                
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr bgcolor="#ffffff" ondblclick="javascript:sendInfo('<%# Eval("id")%>')">
                                            <td>
                                                <a href="?action=userpay&userid=<%# Eval("userid") %>&status=2">
                                                    <%# Eval("userid")%>
                                                </a>
                                            </td>
                                            <td>
                                                 <%# Server.HtmlEncode(Eval("userorder").ToString())%>
                                            </td>
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>
                                            <td>
                                                <%# Eval("linkid")%>
                                            </td>
                                            <td>
                                                <%# Eval("mobile")%>
                                            </td>
                                            <td>
                                                <%# Eval("servicenum")%>
                                            </td>                                            
                                            <td>
                                                <%# Eval("message")%>
                                            </td>
                                            <td>
                                                <%# Eval("fee","{0:0}")%>
                                            </td>
                                            <td>
                                                <%# Eval("payAmt", "{0:f2}")%>
                                            </td>
                                            <td>
                                                <%# Eval("supplierAmt", "{0:f2}")%>
                                            </td>
                                            <td>
                                                <%# Eval("commission", "{0:f2}")%>
                                            </td>
                                             <td>
                                                <%# Eval("profits", "{0:f2}")%>
                                            </td>
                                            <td>
                                                <%# Eval("completetime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                            </td>
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum),Eval("status"))%>
                                            </td>
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderNofityStatusEnum), Eval("notifystat"))%>
                                            </td>
                                            <td>
                                                <%# viviapi.WebUI.WebUtility.GetsupplierName(Eval("supplierId"))%>
                                            </td>
                                            <td>
                                                <%# Eval("server")%>
                                            </td>
                                            <td>
                                               <asp:Button ID="btnReissue" runat="server" Text="����" ToolTip="�ֶ��ط�" CommandName="Reissue" CommandArgument='<%# Eval("orderid")%>' />
                                                <asp:Button ID="btnRest" runat="server" Text="����" CommandName="ResetOrder" CommandArgument='<%#GetParm(Eval("orderid"),Eval("supplierId"),Eval("fee"))%>' />
                                                <asp:Button ID="btnDeduct" runat="server" Text="��"  ToolTip="����" CommandName="Deduct" CommandArgument='<%# Eval("orderid")%>' />
                                                <asp:Button ID="btnReDeduct" runat="server" Text="��"  CommandName="ReDeduct" CommandArgument='<%# Eval("orderid")%>' />
                                
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    <tr style="background-color: #EBEBEB">
                        <td height="22" colspan="7">
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
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript">
        function handler(tp) {
        }

        var mytr = document.getElementById("table2").getElementsByTagName("tr");
        for (var i = 1; i < mytr.length; i++) {
            mytr[i].onmouseover = function() {
                var rows = this.childNodes.length;
                for (var row = 0; row < rows; row++) {
                    this.childNodes[row].style.backgroundColor = '#E6EEFF';
                }
            };
            mytr[i].onmouseout = function() {
                var rows = this.childNodes.length;
                for (var row = 0; row < rows; row++) {
                    this.childNodes[row].style.backgroundColor = '';
                }
            };
        }

    </script>

</body>
</html>
