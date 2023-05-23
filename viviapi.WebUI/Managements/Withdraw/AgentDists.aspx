<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Withdraw.AgentDists" CodeBehind="AgentDists.aspx.cs" %>
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
            window.open("AgentDistsInfo.aspx?id=" + id, "�鿴����", "height=500,width=800");
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
                    ��˽����
                </td>
            </tr>
            <tr>
                <td>
                    �̻�ID��<asp:TextBox ID="txtUserId" runat="server" Width="80px"></asp:TextBox>
                    ���ţ�<asp:TextBox ID="txtLotno" runat="server" Width="120px"></asp:TextBox>
                    ϵͳ���׺ţ�<asp:TextBox ID="txttrade_no" runat="server" Width="120px"></asp:TextBox>
                    �̻����׺ţ�<asp:TextBox ID="txtout_trade_no" runat="server" Width="120px"></asp:TextBox>
                    <asp:DropDownList ID="ddlbankCode" runat="server">
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
                    �տ��˻���<asp:TextBox ID="txtAccount" runat="server" Width="120px"></asp:TextBox>
                    �տ��ˣ�<asp:TextBox ID="txtbankAccountName" runat="server" Width="80px"></asp:TextBox>
                    <asp:DropDownList ID="ddlSupplier" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>   
                     <asp:DropDownList ID="ddlmode" runat="server">
                        <asp:ListItem Value="">--�ύģʽ--</asp:ListItem>
                        <asp:ListItem Value="1">API�ύ</asp:ListItem>
                        <asp:ListItem Value="2">�ϴ��ļ�</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlaudit_status" runat="server">
                        <asp:ListItem Value="">--���״̬--</asp:ListItem>
                        <asp:ListItem Value="1">�ȴ����</asp:ListItem>
                        <asp:ListItem Value="2">���ͨ��</asp:ListItem>
                        <asp:ListItem Value="3">��˾ܾ�</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlpayment_status" runat="server">
                        <asp:ListItem Value="">--����״̬--</asp:ListItem>
                        <asp:ListItem Value="1">δ֪</asp:ListItem>
                        <asp:ListItem Value="4">������</asp:ListItem>
                        <asp:ListItem Value="2">�ɹ�</asp:ListItem>
                        <asp:ListItem Value="3">ʧ��</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlis_cancel" runat="server">
                        <asp:ListItem Value="">--�Ƿ�ȡ��--</asp:ListItem>
                        <asp:ListItem Value="0" Selected="True">δȡ��</asp:ListItem>
                        <asp:ListItem Value="1" >��ȡ��</asp:ListItem>
                    </asp:DropDownList> 
                    <asp:DropDownList ID="ddl_issure" runat="server">
                        <asp:ListItem Value="">--�û�ȷ��--</asp:ListItem>
                        <asp:ListItem Value="1" Selected="True">�ȴ�ȷ��</asp:ListItem>
                        <asp:ListItem Value="2" >��ȷ��</asp:ListItem>
                        <asp:ListItem Value="3" >��ȡ��</asp:ListItem>
                    </asp:DropDownList>  
                     <asp:DropDownList ID="ddlnotifystatus" runat="server">
                        <asp:ListItem Value="">--֪ͨ״̬--</asp:ListItem>
                        <asp:ListItem Value="0">����ʧ��</asp:ListItem>
                        <asp:ListItem Value="1">������</asp:ListItem>
                        <asp:ListItem Value="2" >�ѳɹ�</asp:ListItem>
                    </asp:DropDownList>               
                    ��ʼ��
                    <asp:TextBox ID="txtStimeBox" runat="server" Width="65px"></asp:TextBox>
                    ��ֹ��
                    <asp:TextBox ID="txtEtimeBox" runat="server" Width="65px"></asp:TextBox>
                    
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                    </asp:Button>
                    <asp:Button ID="btnExport" runat="server" CssClass="button" Text="����"
                            OnClick="btnExport_Click"></asp:Button>
                    <div id="divmoney">
                    <span style="color: #ff0000; text-align: left">�������<% = total_amount%></span> 
                    <span style="color: #ff0000; text-align: left;">�����ѣ�<% = total_charge%></span>
                    <span style="color: #ff0000; text-align: left;">ʵ��֧����<% = total_paymoney%></span>
                </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                        <asp:Repeater ID="rptList" runat="server" 
                            onitemdatabound="rptList_ItemDataBound" 
                            onitemcommand="rptList_ItemCommand">
                            <HeaderTemplate>
                                <tr style="background-color: #507CD1; color: #fff; height: 22px">
                                    <td style="width:3%">
                                        ���
                                    </td>
                                    <td style="width:6%">
                                        �ύģʽ
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
                                    <td style="width:10%">
                                        �տ���Ϣ
                                    </td>
                                    <td style="width:5%">
                                        ������
                                    </td>
                                    <td style="width:5%">
                                        ������
                                    </td>
                                    <td style="width:5%">
                                        ʵ�����<br />
                                    </td>
                                    <td style="width:5%">
                                        ���״̬
                                    </td>
                                    <td style="width:5%">
                                        ����ӿ�
                                    </td>
                                    <td style="width:5%">
                                        ����״̬
                                    </td>                                     
                                    <td style="width:8%">
                                        ����ʱ��
                                    </td>  
                                    <td style="width:5%">
                                        �û�ȷ��
                                    </td> 
                                    <td style="width:4%">
                                        ȡ��
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
                                        <%#stlAgtBLL.GetModeText(Eval("mode"))%>
                                    </td>
                                    <td>
                                        <%# Eval("trade_no")%>
                                    </td>
                                    <td>
                                        <%# Eval("out_trade_no")%>
                                    </td>
                                    <td>
                                        <a href="?action=paylistbyid&userid=<%#Eval("userid")%>">
                                            <%#Eval("UserName")%>(#<%#Eval("userid")%>) </a>
                                    </td>
                                    <td>
                                        <%# Eval("bankName")%>
                                        <br />
                                        <%# Eval("bankBranch")%>
                                        <br />
                                        <%# Eval("bankAccountName")%>
                                        <br />
                                        <%# Eval("bankAccount")%>
                                    </td>
                                    <td>
                                        <%# Eval("amount","{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# Eval("charge", "{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# (Convert.ToDecimal(Eval("amount")) + Convert.ToDecimal(Eval("charge"))).ToString("f2")%>
                                    </td>
                                    <td>
                                        <%#stlAgtBLL.GetAuditStatusText(Eval("audit_status"))%>
                                    </td>
                                    <td>
                                        <%# Eval("tranApi")%>
                                    </td>
                                    <td>
                                        <%#stlAgtBLL.GetPaymentStatusText(Eval("payment_status"))%>
                                    </td>
                                     <td>
                                        <%# Eval("processingTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                     <td>
                                        <%#stlAgtBLL.GetIsSureText(Eval("issure"))%>
                                    </td>
                                    <td>
                                         <%#stlAgtBLL.GetCancelText(Eval("is_cancel"))%>
                                    </td>
                                    <td>
                                         <asp:Button ID="btnCancel" runat="server" Text="ȡ��" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="Cancel" />
                                         <asp:Button ID="btnAudits" runat="server" Text="���" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="Audit"/> 
                                         <asp:Button ID="btnRefuse" runat="server" Text="�ܾ�" Visible="false" CommandArgument='<%# Eval("trade_no")%>' CommandName="Refuse"/>
                                         <asp:Button ID="btnReissue" runat="server" Text="����"  CommandArgument='<%# Eval("trade_no")%>' CommandName="Reissue"/>
                                         <asp:Button ID="btnResendToApi" runat="server" Text="�ύ���ӿ�" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="ResendToApi" />
                                         <asp:Button ID="btnpaysuccess" runat="server" Text="����ɹ�" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="paysuccess"/>
                                         <asp:Button ID="btnpayfail" runat="server" Text="����ʧ��" Visible="false" CommandArgument='<%# Eval("trade_no")%>' CommandName="payfail"/>
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
