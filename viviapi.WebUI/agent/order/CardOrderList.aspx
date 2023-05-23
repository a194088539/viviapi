<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.agent.Order.CardOrderList" Codebehind="CardOrderList.aspx.cs" %>

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
            window.open("CardOrderShow.aspx?id=" + id, "�鿴���ඩ����ϸ", "");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="0" class="table1">
        <tr>
            <td align="center" colspan="3" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);color: teal; background-repeat: repeat-x; height: 28px">
                ���ඩ��
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td colspan="3">
                            �̻�ID��
                            <asp:TextBox ID="txtuserid" runat="server" Width="60px"></asp:TextBox>
                             &nbsp&nbsp<asp:DropDownList ID="ddlChannelType" runat="server" Width="95px">
                                <asp:ListItem Value="">--ͨ������--</asp:ListItem>
                                <asp:ListItem Value="103">�����г�ֵ��</asp:ListItem>
                                <asp:ListItem Value="104">ʢ��һ��ͨ</asp:ListItem>
                                 <asp:ListItem Value="210">ʢ��ͨ��</asp:ListItem>
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
                         
                            &nbsp&nbsp
                            <asp:DropDownList ID="ddlmange" runat="server" Visible="false"></asp:DropDownList>
                            &nbsp&nbsp���ţ�<asp:TextBox ID="txtCardNo" runat="server" Width="120px"></asp:TextBox>
                            &nbsp&nbsp��ʼ��
                            <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                            &nbsp&nbsp��ֹ��
                            <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                            <asp:Button ID="btn_Search" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btn_Search_Click">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            �����ţ�<asp:TextBox ID="txtOrderId" runat="server" Width="150px"></asp:TextBox>
                            &nbsp&nbsp�̻������ţ�<asp:TextBox ID="txtUserOrder" runat="server" Width="150px"></asp:TextBox>
                            &nbsp&nbsp�ӿ��̶����ţ�<asp:TextBox ID="txtSuppOrder" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td align="left" bgcolor="#F9F9F9">
                            <div runat="server" id="divmoney">
                                <span style="color: #ff0000; text-align: left">�ܶ<% = TotalTranATM %></span>                                    
                                <span style="color: #ff0000; text-align: left;">��������ɣ�<% = TotalCommission %></span>
                            </div>
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
                                <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand" OnItemDataBound="rptOrders_ItemDataBound">
                                    <HeaderTemplate>
                                        <tr height="22" style="background-color: #507CD1; color: #fff">
                                            <td></td>
                                            <td>
                                                �̻�ID
                                            </td>
                                             <td>
                                                �ӿ�
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
                                                ���
                                            </td>                                         
                                            <td>
                                                ��������
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
                                        <tr bgcolor="#EFF3FB" ondblclick="javascript:sendInfo('<%# Eval("id")%>')">
                                            <td><%# getversionName(Eval("ismulticard"))%> 
                                                 <asp:Literal ID="litimg" runat="server"></asp:Literal></td>
                                            <td>
                                                <a href="?action=userpay&userid=<%# Eval("userid") %>&status=2">
                                                    <%# Eval("userid")%>
                                                </a>
                                            </td>
                                             <td>
                                                <%# Eval("version")%>
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
                                                <%# CutWord(Eval("cardNo").ToString())%>
                                            </td>                                           
                                            <td>
                                                <%# Eval("refervalue", "{0:f2}")%>
                                            </td>                                            
                                            <td>
                                                <%# Eval("promAmt", "{0:f2}")%>
                                            </td>                                           
                                            <td>
                                                <%# Eval("completetime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                            </td>
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum),Eval("status"))%> [<%#Eval("msg")%>]
                                            </td>                                            
                                                                                    
                                        </tr>
                                        <tr id="tr_carddetail" runat="server" style="display:none">
                                            <td colspan="20">
                                                <asp:Repeater ID="rptcardDetail" runat="server" OnItemCommand="rptcardDetail_ItemCommand" OnItemDataBound="rptcardDetail_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <table align="center" cellpadding="0" cellspacing="0" width="98%" class="zb" style="background-color: #f1fef1;margin: 8px;">
                                                            <tr class="style3">
                                                                <td>
                                                                    ���
                                                                </td>
                                                                <td>
                                                                    ������
                                                                </td>
                                                                <td>
                                                                    ����
                                                                </td>
                                                                <td>
                                                                    ����
                                                                </td>
                                                                <td>
                                                                    ���ύ��ֵ
                                                                </td>
                                                                <td>
                                                                    ��ʵ����ֵ
                                                                </td>
                                                                <td>
                                                                    �ӿ���
                                                                </td>
                                                                <td>
                                                                    ״̬
                                                                </td>
                                                                <td>
                                                                    ��ʾ��Ϣ
                                                                </td>
                                                                <td height="30">
                                                                    ���ʱ��
                                                                </td>
                                                                <td height="30">
                                                                    ����
                                                                </td>                                                  
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#c4d6fc'"onmouseout='this.style.backgroundColor=c;'>
                                                <td>
                                                    &nbsp;<%# Eval("serial")%>
                                                </td>
                                                <td>
                                                    &nbsp;<%# Eval("porderid").ToString() + Eval("serial").ToString()%>
                                                </td>
                                                <td>
                                                    &nbsp;<%# Eval("cardno")%>
                                                </td>
                                                <td>
                                                    &nbsp;<%# Eval("cardpwd")%>
                                                </td> 
                                                <td>
                                                    &nbsp;<%# Eval("refervalue","{0:f0}")%>
                                                </td>  
                                                <td>
                                                    &nbsp;<%# Eval("realvalue", "{0:f0}")%>
                                                </td> 
                                                <td>
                                                    &nbsp;<%# viviapi.WebUI.WebUtility.GetsupplierName(Eval("suppid"))%>
                                                </td>  
                                                <td>
                                                    &nbsp;<%# Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum),Eval("status"))%>
                                                </td>  
                                                <td>
                                                    &nbsp;<%# Eval("msg")%>
                                                </td> 
                                                <td>
                                                    &nbsp;<%# Eval("completetime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnitemRest" runat="server" Text="����" CommandName="ResetOrder" CommandArgument='<%#GetParm(Eval("porderid").ToString() + Eval("serial").ToString(),Eval("suppid"),Eval("refervalue"))%>' />
                                                </td>                                             
                                            </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
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
                                                <%# Eval("version")%>
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
                                               <%# CutWord(Eval("cardNo").ToString())%>
                                            </td>                                          
                                            <td>
                                                <%# Eval("refervalue", "{0:f2}")%>
                                            </td>                                            
                                            <td>
                                                <%# Eval("promAmt", "{0:f2}")%>
                                            </td>                                            
                                            <td>
                                                <%# Eval("completetime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                            </td>
                                            <td>
                                                <%# Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum),Eval("status"))%> [<%#Eval("msg")%>]
                                            </td>                                           
                                        </tr>
                                        <tr id="tr_carddetail" runat="server" style="display:none">
                                            <td colspan="20">
                                                <asp:Repeater ID="rptcardDetail" runat="server" OnItemCommand="rptcardDetail_ItemCommand" OnItemDataBound="rptcardDetail_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <table align="center" cellpadding="0" cellspacing="0" width="98%" class="zb" style="background-color: #f1fef1;margin: 8px;">
                                                            <tr class="style3">
                                                                <td>
                                                                    ���
                                                                </td>
                                                                <td>
                                                                    ������
                                                                </td>
                                                                <td>
                                                                    ����
                                                                </td>
                                                                <td>
                                                                    ����
                                                                </td>
                                                                <td>
                                                                    ���ύ��ֵ
                                                                </td>
                                                                <td>
                                                                    ��ʵ����ֵ
                                                                </td>
                                                                <td>
                                                                    �ӿ���
                                                                </td>
                                                                <td>
                                                                    ״̬
                                                                </td>
                                                                <td>
                                                                    ��ʾ��Ϣ
                                                                </td>
                                                                <td height="30">
                                                                    ���ʱ��
                                                                </td>
                                                                <td height="30">
                                                                    ����
                                                                </td>                                                  
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#c4d6fc'"onmouseout='this.style.backgroundColor=c;'>
                                                <td>
                                                    &nbsp;<%# Eval("serial")%>
                                                </td>
                                                <td>
                                                    &nbsp;<%# Eval("porderid").ToString() + Eval("serial").ToString()%>
                                                </td>
                                                <td>
                                                    &nbsp;<%# Eval("cardno")%>
                                                </td>
                                                <td>
                                                    &nbsp;<%# Eval("cardpwd")%>
                                                </td> 
                                                <td>
                                                    &nbsp;<%# Eval("refervalue","{0:f0}")%>
                                                </td>  
                                                <td>
                                                    &nbsp;<%# Eval("realvalue", "{0:f0}")%>
                                                </td> 
                                                <td>
                                                    &nbsp;<%# viviapi.WebUI.WebUtility.GetsupplierName(Eval("suppid"))%>
                                                </td>  
                                                <td>
                                                    &nbsp;<%# Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum),Eval("status"))%>
                                                </td>  
                                                <td>
                                                    &nbsp;<%# Eval("msg")%>
                                                </td> 
                                                <td>
                                                    &nbsp;<%# Eval("completetime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnitemRest" runat="server" Text="����" CommandName="ResetOrder" CommandArgument='<%#GetParm(Eval("porderid").ToString() + Eval("serial").ToString(),Eval("suppid"),Eval("refervalue"))%>' />
                                                </td>                                             
                                            </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
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
        function collapse(img, objName) {
            var obj = document.getElementById(objName);
            if (img.src.indexOf('open') != -1) {
                img.src = img.src.replace('open', 'close');
                obj.style.display = 'none';
            }
            else {
                img.src = img.src.replace('close', 'open');
                obj.style.display = '';
            }
        }
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
