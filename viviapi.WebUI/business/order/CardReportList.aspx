<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.business.Order.CardReportList" Codebehind="CardReportList.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />

    <script src="../../js/ControlDate/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css"> table {font-weight: normal;font-size: 12px; line-height: 170%;}
        td{ height: 11px; }
        A:link {color: #02418a;text-decoration: none;}
    </style>
    <script type="text/javascript">
        function sendInfo(id) {
            window.open("CardOrderShow.aspx?id=" + id, "�鿴����", "height=760,width=800");
        }        
    </script>
    <script type="text/javascript">
        function openuserurl(url) {
            window.open(url, "�鿴�û���Ϣ");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="0" class="table1">
        <tr>
            <td align="center" colspan="3" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 28px">
                ����״̬����
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td colspan="2">
                            &nbsp&nbsp�̻�ID��
                            <asp:TextBox ID="txtuserid" runat="server" Width="60px"></asp:TextBox>
                            <%--  ����ID��
                <asp:TextBox ID="txtpromid" runat="server" Width="30px"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlChannelType" runat="server" Width="95px">
                                <asp:ListItem Value="">--ͨ������--</asp:ListItem>
                                <asp:ListItem Value="103">�����г�ֵ��</asp:ListItem>
                                <asp:ListItem Value="104">ʢ��һ��ͨ</asp:ListItem>
                                <asp:ListItem Value="105">��;֧����</asp:ListItem>
                                <asp:ListItem Value="106">����һ��ͨ</asp:ListItem>
                                <asp:ListItem Value="107">��ѶQ�ҿ�</asp:ListItem>
                                <asp:ListItem Value="108">��ͨ��ֵ��</asp:ListItem>
                                <asp:ListItem Value="109">����һ��ͨ</asp:ListItem>
                                <asp:ListItem Value="110">����һ��ͨ</asp:ListItem>
                                <asp:ListItem Value="111">����һ��ͨ</asp:ListItem>
                                <asp:ListItem Value="112">�Ѻ�һ��ͨ</asp:ListItem>
                                <asp:ListItem Value="113">���ų�ֵ��</asp:ListItem>
                                <asp:ListItem Value="114">��Ѷ��</asp:ListItem>
                                <asp:ListItem Value="115">����һ��ͨ</asp:ListItem>
                                <asp:ListItem Value="116">��ɽһ��ͨ</asp:ListItem>
                                <asp:ListItem Value="117">ħ�޿�</asp:ListItem>
                                <asp:ListItem Value="118">5173��</asp:ListItem>
                                <asp:ListItem Value="119">��Ѫ��</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlOrderStatus" runat="server" Width="95px">
                                <asp:ListItem>--����״̬--</asp:ListItem>                                
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
                             &nbsp&nbsp���ţ�<asp:TextBox ID="txtCardNo" runat="server" Width="120px"></asp:TextBox>
                            <asp:DropDownList ID="ddlmange" runat="server"></asp:DropDownList>
                            &nbsp&nbsp��ʼ��
                            <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                            &nbsp&nbsp��ֹ��
                            <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                            &nbsp&nbsp&nbsp&nbsp<asp:Button ID="btn_Search" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btn_Search_Click">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp�����ţ�<asp:TextBox ID="txtOrderId" runat="server" Width="160px"></asp:TextBox>
                            &nbsp&nbsp�̻������ţ�<asp:TextBox ID="txtUserOrder" runat="server" Width="160px"></asp:TextBox>
                            &nbsp&nbsp�ӿ��̶����ţ�<asp:TextBox ID="txtSuppOrder" runat="server" Width="160px"></asp:TextBox>
                        </td>                        
                    </tr>
                </table>
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
                                             <td></td>
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
                                                ͨ������
                                            </td>
                                            <td>
                                                ����
                                            </td>
                                             <td>
                                                ״̬
                                            </td>
                                            <td>
                                                �·�״̬
                                            </td>
                                            <td>
                                                �·�ʱ��
                                            </td>
                                             <td>
                                                �·�����
                                            </td>
                                            <td>
                                                ��������
                                            </td>                                           
                                            <td>
                                                ����
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr bgcolor="#EFF3FB" ondblclick="javascript:sendInfo('<%# Eval("id")%>')">
                                            <td><%# getversionName(Eval("ismulticard"))%> 
                                                 <asp:Literal ID="litimg" runat="server"></asp:Literal></td>
                                            <td>
                                                <a href="?action=userpay&userid=<%# Eval("userid") %>&status=2">
                                                    <%# Eval("userid")%>
                                                </a>
                                            </td>                                           
                                            <td>
                                                <%# Eval("userorder")%>
                                            </td>
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>                                            
                                            <td>
                                                <%# Eval("modetypename")%>
                                            </td>
                                            <td>
                                                <%# Eval("modeName")%>
                                            </td>  
                                             <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum),Eval("status"))%>
                                            </td>                                  
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderNofityStatusEnum), Eval("notifystat"))%>
                                            </td> 
                                             <td>
                                                <%# Eval("notifytime")%>
                                            </td>
                                            <td>
                                                <%# Eval("notifycount")%>
                                            </td>                                           
                                            <td>
                                                <%# Eval("notifycontext")%>
                                            </td>                                         
                                            <td>
                                                <a href="javascript:openuserurl('<%# Eval("againNotifyUrl")%>')">�鿴</a>
                                                <asp:Button ID="btnReissue" runat="server" Text="����" ToolTip="�ֶ��ط�" CommandName="Reissue" CommandArgument='<%# Eval("orderid")%>' />                                              
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr bgcolor="#ffffff" ondblclick="javascript:sendInfo('<%# Eval("id")%>')">
                                        <td><%# getversionName(Eval("ismulticard"))%> 
                                                 <asp:Literal ID="litimg" runat="server"></asp:Literal></td>
                                              <td>
                                                <a href="?action=userpay&userid=<%# Eval("userid") %>&status=2">
                                                    <%# Eval("userid")%>
                                                </a>
                                            </td>                                           
                                            <td>
                                                <%# Eval("userorder")%>
                                            </td>
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>                                            
                                            <td>
                                                <%# Eval("modetypename")%>
                                            </td>
                                            <td>
                                                <%# Eval("modeName")%>
                                            </td>  
                                             <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum),Eval("status"))%>
                                            </td>                                  
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderNofityStatusEnum), Eval("notifystat"))%>
                                            </td> 
                                             <td>
                                                <%# Eval("notifytime")%>
                                            </td>
                                            <td>
                                                <%# Eval("notifycount")%>
                                            </td>                                           
                                            <td>
                                                <%# Eval("notifycontext")%>
                                            </td>                                         
                                            <td>
                                            <a href="javascript:openuserurl('<%# Eval("againNotifyUrl")%>')">�鿴</a>
                                                <asp:Button ID="btnReissue" runat="server" Text="����" ToolTip="�ֶ��ط�" CommandName="Reissue" CommandArgument='<%# Eval("orderid")%>' />                                              
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
