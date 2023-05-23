<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Audits" CodeBehind="Audits.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�������</title>
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
                    �̻�ID��<asp:TextBox ID="txtUserId" runat="server" Width="80px"></asp:TextBox>
                    ����ID��<asp:TextBox ID="txtItemInfoId" runat="server" Width="80px"></asp:TextBox>
                    <asp:DropDownList ID="ddlbankName" runat="server">                        
                        <asp:ListItem value="">--�տ�����--</asp:ListItem>
                        <asp:ListItem value="0002">֧����</asp:ListItem>
                        <asp:ListItem value="0003">�Ƹ�ͨ</asp:ListItem>
                        <asp:ListItem value="1002">�й���������</asp:ListItem>
                        <asp:ListItem value="1005">�й�ũҵ����</asp:ListItem>
                        <asp:ListItem value="1003">�й���������</asp:ListItem>
                        <asp:ListItem value="1026">�й�����</asp:ListItem>
                        <asp:ListItem value="1001">��������</asp:ListItem>
                        <asp:ListItem value="1006">��������</asp:ListItem>
                        <asp:ListItem value="1020">��ͨ����</asp:ListItem>
                        <asp:ListItem value="1025">��������</asp:ListItem>
                        <asp:ListItem value="1009">��ҵ����</asp:ListItem>
                        <asp:ListItem value="1027">�㷢����</asp:ListItem>
                        <asp:ListItem value="1004">�ַ�����</asp:ListItem>
                        <asp:ListItem value="1022">�������</asp:ListItem>
                        <asp:ListItem value="1021">��������</asp:ListItem>
                        <asp:ListItem value="1010">ƽ������</asp:ListItem>
                        <asp:ListItem value="1066">�й�������������</asp:ListItem>
                    </asp:DropDownList>
                    �տ��˻���<asp:TextBox ID="txtAccount" runat="server" Width="80px"></asp:TextBox>
                    �տ��ˣ�<asp:TextBox ID="txtpayeeName" runat="server" Width="80px"></asp:TextBox>                    
                    <asp:DropDownList ID="ddlmode" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSupplier" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                    </asp:Button>
                    <asp:Button ID="btnPass" runat="server" CssClass="button" Width="90px" Text="����ͨ�����"
                        OnClick="btnPass_Click"></asp:Button>
                    <asp:Button ID="btnAllPass" runat="server" CssClass="button" Width="90px" Text="ȫ��ͨ�����"
                        OnClick="btnAllPass_Click"></asp:Button>
                    <asp:Button ID="btnallfail" runat="server" CssClass="button" Width="90px" Text="ȫ���ܾ�"
                        OnClick="btnallfail_Click"></asp:Button>
                     <asp:Button ID="btnExport" runat="server" CssClass="button" Text="����" OnClick="btnExport_Click">
                </asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                        <asp:Repeater ID="rptApply" runat="server" 
                            onitemdatabound="rptApply_ItemDataBound" 
                            onitemcommand="rptApply_ItemCommand">
                            <HeaderTemplate>
                                <tr style="background-color: #507CD1; color: #fff; height:22px">
                                    <td style="width: 5%">
                                        <input id="Checkboxall" type="checkbox" class="qx" onclick="checkall(this)" />   ȫѡ                                     
                                    </td>
                                    <td style="width: 5%">
                                        ���
                                    </td>
                                    <td style="width: 8%">
                                        �̻���
                                    </td>
                                    <td style="width: 20%">
                                        �տ���Ϣ
                                    </td>
                                    <td style="width: 8%">
                                        ����
                                    </td>
                                    <td style="width: 8%">
                                        ������
                                    </td>
                                    <td style="width: 10%">
                                        ����ʱ��
                                    </td>
                                    <td style="width: 10%">
                                        ֧������
                                    </td>
                                    <td style="width: 8%">
                                        ����ӿ�
                                    </td> 
                                    <td>
                                        ����
                                    </td>                                     
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="#EFF3FB">
                                    <td>
                                        <input id="<%# Eval("ID") %>" type="checkbox" name="ischecked" class="qx" value="<%# Eval("ID") %>" />
                                    </td>
                                    <td>
                                        <%# Eval("ID")%>
                                    </td>
                                    <td>
                                        <a href="javascript:sendInfo('<%# Eval("userid")%>')">
                                            <%#Eval("UserName")%>
                                        </a>
                                    </td>
                                    <td>
                                        <%# viviapi.BLL.SettledFactory.GetSettleBankName(Eval("PayeeBank").ToString())%> <%# Eval("Payeeaddress")%> <br />
                                        <%# Eval("payeeName")%> <br />
                                        <%# Eval("account")%>
                                    </td>                                    
                                    <td style="text-align: right">
                                        <%#Enum.GetName(typeof(viviapi.Model.SettledmodeEnum), Eval("settmode"))%>
                                    </td>
                                    <td style="text-align: right">
                                        <%# Eval("amount","{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# Eval("AddTime","{0:yyyy-MM-dd HH:mm:ss}") %>
                                    </td>
                                    <td>
                                        <%# Eval("required", "{0:yyyy-MM-dd}")%>
                                    </td>
                                    <td>
                                        <%# viviapi.WebUI.WebUtility.GetsupplierName(Eval("tranapi"))%>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAudit" runat="server" Text="ͨ��" CommandName="Pass" CommandArgument='<%# Eval("ID") %>'  />
                                        <asp:Button ID="btnRefuse" runat="server" Text="�ܾ�" CommandName="Refuse" CommandArgument='<%# Eval("ID") %>'  />
                                    </td>                                    
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr bgcolor="#f9f9f9">                                    
                                   <td>
                                        <input id="<%# Eval("ID") %>" type="checkbox" name="ischecked" class="qx" value="<%# Eval("ID") %>" />
                                    </td>
                                    <td>
                                        <%# Eval("ID")%>
                                    </td>
                                    <td>
                                        <a href="javascript:sendInfo('<%# Eval("userid")%>')">
                                            <%#Eval("UserName")%>
                                        </a>
                                    </td>
                                    <td>
                                        <%# viviapi.BLL.SettledFactory.GetSettleBankName(Eval("PayeeBank").ToString())%> <%# Eval("Payeeaddress")%> <br />
                                        <%# Eval("payeeName")%> <br />
                                        <%# Eval("account")%>
                                    </td>                                    
                                    <td style="text-align: right">
                                        <%#Enum.GetName(typeof(viviapi.Model.SettledmodeEnum), Eval("settmode"))%>
                                    </td>
                                    <td style="text-align: right">
                                        <%# Eval("amount","{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# Eval("AddTime","{0:yyyy-MM-dd HH:mm:ss}") %>
                                    </td>
                                    <td>
                                        <%# Eval("required", "{0:yyyy-MM-dd}")%>
                                    </td>
                                    <td>
                                         <%# viviapi.WebUI.WebUtility.GetsupplierName(Eval("tranapi"))%>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAudit" runat="server" Text="ͨ��" CommandName="Pass" CommandArgument='<%# Eval("ID") %>'  />
                                        <asp:Button ID="btnRefuse" runat="server" Text="�ܾ�" CommandName="Refuse" CommandArgument='<%# Eval("ID") %>'  />
                                    </td>    
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                        <tr>
                            <td colspan="10">
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
    var table = document.getElementById("table_zyads"); if (table) { for (i = 0; i < table.rows.length; i++) { if (i % 2 == 0) { table.rows[i].bgColor = "ffffff"; } else { table.rows[i].bgColor = "f3f9fe" } } } var mytr = document.getElementById("table2").getElementsByTagName("tr"); for (var i = 1; i < mytr.length; i++) { mytr[i].onmouseover = function() { var rows = this.childNodes.length; for (var row = 0; row < rows; row++) { this.childNodes[row].style.backgroundColor = '#DFE8F6'; } }; mytr[i].onmouseout = function() { var rows = this.childNodes.length; for (var row = 0; row < rows; row++) { this.childNodes[row].style.backgroundColor = ''; } }; }</script>

