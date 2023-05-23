<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.settledAgentSummarys" CodeBehind="settledAgentSummarys.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�������</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
    <script src="../../js/ControlDate/WdatePicker.js" type="text/javascript"></script>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; height: 100%;">
            <tr>
                <td align="center" class="headtitle">
                    ����(ͨ���ϴ��ļ�)���
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                    �̻�ID��<asp:TextBox ID="txtUserId" runat="server" Width="80px"></asp:TextBox>
                    ���κţ�<asp:TextBox ID="txtLotno" runat="server" Width="120px"></asp:TextBox>
                    �ϴ�ʱ��ӣ�
                            <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                            &nbsp&nbsp����
                            <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                            
                             <asp:DropDownList ID="ddlaudit_status" runat="server" Visible="false">
                        <asp:ListItem Value="">--���״̬--</asp:ListItem>
                        <asp:ListItem Value="1">�ȴ����</asp:ListItem>
                        <asp:ListItem Value="2">���ͨ��</asp:ListItem>
                        <asp:ListItem Value="3">��˾ܾ�</asp:ListItem>
                    </asp:DropDownList>
                            
                    <asp:DropDownList ID="ddlstatus" runat="server">                        
                        <asp:ListItem value="">--����״̬--</asp:ListItem>
                        <asp:ListItem value="1">�ȴ�����</asp:ListItem>
                        <asp:ListItem value="2">�������</asp:ListItem>
                        <asp:ListItem value="3">ȫ�����</asp:ListItem>
                    </asp:DropDownList>                                       
                   
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                    </asp:Button>
                    <asp:Button ID="btnPass" runat="server" CssClass="button" Width="90px" Text="����ͨ�����"
                        OnClick="btnPass_Click" Visible="false"></asp:Button>
                    <asp:Button ID="btnAllPass" runat="server" CssClass="button" Width="90px" Text="ȫ��ͨ�����"
                        OnClick="btnAllPass_Click" Visible="false"></asp:Button>
                    <asp:Button ID="btnallfail" runat="server" CssClass="button" Width="90px" Text="ȫ���ܾ�"
                        OnClick="btnallfail_Click" Visible="false"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                        <asp:Repeater ID="rptApply" runat="server" onitemdatabound="rptApply_ItemDataBound" onitemcommand="rptApply_ItemCommand">
                            <HeaderTemplate>
                                <tr style="background-color: #507CD1; color: #fff; height:22px">
                                    <td style="width: 3%"></td>
                                    <td style="width: 3%">���</td>
                                    <td style="width: 6%">�̻�</td>
                                    <td style="width: 10%">���κ�</td>
                                    <td style="width: 6%">Ӧ��������</td>
                                    <td style="width: 6%">�ɹ�����</td>
                                    <td style="width: 7%">Ӧ�������</td>
                                    <td style="width: 7%">�ɹ����</td>
                                    <td style="width: 7%">Ӧ��������</td>
                                    <td style="width: 7%">ʵ��������</td>
                                    <td style="width: 7%">Ӧ�����ϼ�</td> 
                                    <td style="width: 7%">ʵ�����ϼ�</td>                                   
                                    <td style="width: 6%">����״̬</td>
                                    <td>����</td>                                     
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="#EFF3FB">
                                    <td><asp:Literal ID="litimg" runat="server"></asp:Literal></td>
                                    <td><%# Eval("ID")%></td>
                                    <td>
                                        <a href="javascript:sendInfo('<%# Eval("userid")%>')"><%#Eval("UserName")%></a>
                                    </td>
                                    <td><%# Eval("lotno")%></td>
                                    <td><%# Eval("qty")%></td>
                                    <td><%# Eval("succqty")%></td>
                                    <td><%# Eval("amt","{0:f2}")%></td>
                                    <td><%# Eval("succamt", "{0:f2}")%></td>
                                    <td><%# Eval("fee", "{0:f2}")%></td>
                                    <td><%# Eval("realfee", "{0:f2}")%></td>
                                    <td><%# Eval("totalamt", "{0:f2}")%></td>
                                    <td><%# Eval("totalsuccamt", "{0:f2}")%></td>                                    
                                    <td><%#_bll.GetStatus(Eval("status"))%><%# Eval("remark")%></td>
                                    <td>
                                        <asp:Button ID="btnAudit" runat="server" Text="ͨ��" CommandName="Pass" CommandArgument='<%# Eval("ID") %>'  Visible="false"/>
                                        <asp:Button ID="btnRefuse" runat="server" Text="�ܾ�" CommandName="Refuse" CommandArgument='<%# Eval("ID") %>'  Visible="false"/>
                                    </td>                                    
                                </tr>
                                <tr id="tr_detail" runat="server" style="display:none">
                                        <td colspan="20">
                                            <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand" OnItemDataBound="rptList_ItemDataBound">
                                                <HeaderTemplate>
                                                    <table align="center" cellpadding="0" cellspacing="0" width="98%" class="zb" style="background-color: #f1fef1;margin: 8px;">
                                                        <tr class="style3">
                                                            <td>���</td>
                                                            <td>ϵͳ����</td>
                                                            <td>�տ���Ϣ</td>
                                                            <td>������</td>
                                                            <td>������</td>
                                                            <td>ʵ��֧��</td>
                                                            <td>���״̬</td>
                                                            <td>����ӿ�</td>
                                                            <td>����״̬</td>
                                                            <td>ȡ��</td>
                                                            <td>˵��</td>
                                                            <td height="30">����ʱ��</td>
                                                            <td height="30">
                                                                ����
                                                            </td>                                                  
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#c4d6fc'"onmouseout='this.style.backgroundColor=c;'>
                                                        <td><%# Eval("serial")%></td>
                                                        <td><%# Eval("trade_no")%></td>
                                                        <td><%# Eval("bankName")%>
                                                            <br />
                                                            <%# Eval("bankBranch")%>
                                                            <br />
                                                            <%# Eval("bankAccountName")%>
                                                            <br />
                                                            <%# Eval("bankAccount")%></td>
                                                        <td><%# Eval("amount","{0:f2}")%></td>
                                                        <td><%# Eval("charge", "{0:f2}")%></td>
                                                        <td><%# (Convert.ToDecimal(Eval("amount")) + Convert.ToDecimal(Eval("charge"))).ToString("f2")%></td>
                                                        <td><%#stlAgtBLL.GetAuditStatusText(Eval("audit_status"))%></td>
                                                        <td><%#Eval("tranApi")%></td>
                                                        <td><%#stlAgtBLL.GetPaymentStatusText(Eval("payment_status"))%></td>
                                                        <td><%#stlAgtBLL.GetCancelText(Eval("is_cancel"))%></td>
                                                        <td><%#Eval("ext1")%></td>
                                                        <td height="30"><%# Eval("processingTime", "{0:yyyy-MM-dd HH:mm:ss}")%></td>
                                                        <td height="30">
                                                             <asp:Button ID="btnCancel" runat="server" Text="ȡ��" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="Cancel" />
                                                             <asp:Button ID="btnAudits" runat="server" Text="���" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="Audit"/> 
                                                             <asp:Button ID="btnRefuse" runat="server" Text="�ܾ�" Visible="false" CommandArgument='<%# Eval("trade_no")%>' CommandName="Refuse"/>                                                             
                                                             <asp:Button ID="btnResendToApi" runat="server" Text="�ύ���ӿ�" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="ResendToApi" />
                                                             <asp:Button ID="btnpaysuccess" runat="server" Text="����ɹ�" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="paysuccess"/>
                                                             <asp:Button ID="btnpayfail" runat="server" Text="����ʧ��" Visible="false" CommandArgument='<%# Eval("trade_no")%>' CommandName="payfail"/>
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
                                <tr bgcolor="#f9f9f9">  
                                    <td><asp:Literal ID="litimg" runat="server"></asp:Literal></td>                                  
                                    <td><%# Eval("ID")%></td>
                                    <td>
                                        <a href="javascript:sendInfo('<%# Eval("userid")%>')"><%#Eval("UserName")%></a>
                                    </td>
                                    <td><%# Eval("lotno")%></td>
                                    <td><%# Eval("qty")%></td>
                                    <td><%# Eval("succqty")%></td>
                                    <td><%# Eval("amt","{0:f2}")%></td>
                                    <td><%# Eval("succamt", "{0:f2}")%></td>
                                    <td><%# Eval("fee", "{0:f2}")%></td>
                                    <td><%# Eval("realfee", "{0:f2}")%></td>
                                    <td><%# Eval("totalamt", "{0:f2}")%></td>
                                    <td><%# Eval("totalsuccamt", "{0:f2}")%></td>                                    
                                    <td><%#_bll.GetStatus(Eval("status"))%><%# Eval("remark")%></td>
                                    <td>
                                        <asp:Button ID="btnAudit" runat="server" Text="ͨ��" CommandName="Pass" CommandArgument='<%# Eval("ID") %>'  Visible="false"/>
                                        <asp:Button ID="btnRefuse" runat="server" Text="�ܾ�" CommandName="Refuse" CommandArgument='<%# Eval("ID") %>'  Visible="false"/>
                                    </td>    
                                </tr>
                                <tr id="tr_detail" runat="server" style="display:none">
                                        <td colspan="20">
                                            <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand" OnItemDataBound="rptList_ItemDataBound">
                                                <HeaderTemplate>
                                                    <table align="center" cellpadding="0" cellspacing="0" width="98%" class="zb" style="background-color: #f1fef1;margin: 8px;">
                                                        <tr class="style3">
                                                            <td>���</td>
                                                            <td>ϵͳ����</td>
                                                            <td>�տ���Ϣ</td>
                                                            <td>������</td>
                                                            <td>������</td>
                                                            <td>ʵ�����</td>
                                                            <td>���״̬</td>
                                                            <td>����ӿ�</td>
                                                            <td>����״̬</td>
                                                            <td>ȡ��</td>
                                                            <td>˵��</td>
                                                            <td height="30">����ʱ��</td>
                                                            <td height="30">
                                                                ����
                                                            </td>                                                  
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#c4d6fc'"onmouseout='this.style.backgroundColor=c;'>
                                                        <td><%# Eval("serial")%></td>
                                                        <td><%# Eval("trade_no")%></td>
                                                        <td><%# Eval("bankName")%>
                                                            <br />
                                                            <%# Eval("bankBranch")%>
                                                            <br />
                                                            <%# Eval("bankAccountName")%>
                                                            <br />
                                                            <%# Eval("bankAccount")%></td>
                                                        <td><%# Eval("amount","{0:f2}")%></td>
                                                        <td><%# Eval("charge", "{0:f2}")%></td>
                                                        <td><%# (Convert.ToDecimal(Eval("amount")) + Convert.ToDecimal(Eval("charge"))).ToString("f2")%></td>
                                                        <td><%#stlAgtBLL.GetAuditStatusText(Eval("audit_status"))%></td>
                                                        <td><%#Eval("tranApi")%></td>
                                                        <td><%#stlAgtBLL.GetPaymentStatusText(Eval("payment_status"))%></td>
                                                        <td><%#stlAgtBLL.GetCancelText(Eval("is_cancel"))%></td>
                                                        <td><%#Eval("ext1")%></td>
                                                        <td height="30"><%# Eval("processingTime", "{0:yyyy-MM-dd HH:mm:ss}")%></td>
                                                        <td height="30">
                                                             <asp:Button ID="btnCancel" runat="server" Text="ȡ��" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="Cancel" />
                                                             <asp:Button ID="btnAudits" runat="server" Text="���" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="Audit"/> 
                                                             <asp:Button ID="btnRefuse" runat="server" Text="�ܾ�" Visible="false" CommandArgument='<%# Eval("trade_no")%>' CommandName="Refuse"/>                                                             
                                                             <asp:Button ID="btnResendToApi" runat="server" Text="�ύ���ӿ�" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="ResendToApi" />
                                                             <asp:Button ID="btnpaysuccess" runat="server" Text="����ɹ�" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="paysuccess"/>
                                                             <asp:Button ID="btnpayfail" runat="server" Text="����ʧ��" Visible="false" CommandArgument='<%# Eval("trade_no")%>' CommandName="payfail"/>
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
                        <tr>
                            <td colspan="20">
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
    </div>
    </form>
</body>
</html>

<script type="text/javascript" language="JavaScript">
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
    var table = document.getElementById("table_zyads"); if (table) { for (i = 0; i < table.rows.length; i++) { if (i % 2 == 0) { table.rows[i].bgColor = "ffffff"; } else { table.rows[i].bgColor = "f3f9fe" } } } var mytr = document.getElementById("table2").getElementsByTagName("tr"); for (var i = 1; i < mytr.length; i++) { mytr[i].onmouseover = function() { var rows = this.childNodes.length; for (var row = 0; row < rows; row++) { this.childNodes[row].style.backgroundColor = '#DFE8F6'; } }; mytr[i].onmouseout = function() { var rows = this.childNodes.length; for (var row = 0; row < rows; row++) { this.childNodes[row].style.backgroundColor = ''; } }; }</script>

