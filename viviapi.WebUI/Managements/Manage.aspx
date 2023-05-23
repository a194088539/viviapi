<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.Manage" Codebehind="Manage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>��̨����</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="style/union.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="JavaScript">
        function Setchkall(obj) {
            var objs = document.getElementsByName("chk");
            for (i = 0; i < objs.length; i++) {
                objs[i].checked = obj.checked;
            }
        }    </script>

    <style type="text/css">
        body
        {
            margin: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellpadding="1" cellspacing="1" id="table_zyads" style="width: 100%;
            height: 100%; border: #c9ddf0 1px solid; background-color: White;">
            <tr>
                <td align="center" colspan="2" style="font-weight: bold; font-size: 14px; background-image: url(style/images/topbg.gif);
                    color: teal; background-repeat: repeat-x; height: 28px">
                    ����Ա���� <asp:Button ID="btnAdd" runat="server" Text="�� ��" OnClick="btnAdd_Click"  />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" Width="100%" CellSpacing="1" AllowPaging="True"
                        OnPageIndexChanging="GridView1_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="UserName" HeaderText="�û���" />
                            <asp:BoundField DataField="RelName" HeaderText="����" />
                            <asp:BoundField DataField="LevelText" HeaderText="���" />
                            <asp:BoundField DataField="Commissiontypeview" HeaderText="�������" />
                            <asp:BoundField DataField="Commission" HeaderText="�������" />
                            <asp:BoundField DataField="CardCommission" HeaderText="�㿨���" />
                            <asp:BoundField DataField="statusName" HeaderText="״̬" />
                             <asp:BoundField DataField="Balance" HeaderText="���" DataFormatString="{0:f2}"/>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a href="ManageEdit.aspx?cmd=edit&amp;ID=<%# Eval("ID") %>">�༭</a> || 
                                    <a onclick="return confirm('ȷ��Ҫɾ������û���')" href="?cmd=del&amp;ID=<%# Eval("ID") %>">ɾ��</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%; display:none">
                        <tr align="center">
                            <td>
                                �û���
                            </td>
                            <td>
                                ���루���޸������գ�
                            </td>
                            <td>
                                �������루���޸������գ�
                            </td>
                            <td>
                                ����
                            </td>
                            <td>
                                �������
                            </td>
                            <td>
                                �������
                            </td>
                             <td>
                                �������
                            </td>
                            <td>
                                ״̬
                            </td>
                            <td>
                                ���
                            </td>
                            <td align="center">
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:TextBox ID="UserNameBox" runat="server"></asp:TextBox>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="PassWordBox" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="SecPassWordBox" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="RelNameBox" runat="server"></asp:TextBox>
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="ddlCommissionType" runat="server">
                                    <asp:ListItem Value="1">�����̶����</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="2">��֧�����%</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtCommission" runat="server">0</asp:TextBox>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txtCardCommission" runat="server">0</asp:TextBox>
                            </td>
                             <td align="center">
                                <asp:DropDownList ID="ddlStus" runat="server">
                                    <asp:ListItem Value="1">����</asp:ListItem>
                                    <asp:ListItem Value="0">����</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="LevelList" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td align="center">
                                <asp:Button ID="BtnUpdate" runat="server" CssClass="button" Text=" �� �� " OnClick="BtnUpdate_Click">
                                </asp:Button>
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
    var table = document.getElementById("table_zyads");
    if (table) {
        for (i = 0; i < table.rows.length; i++) {
            if (i % 2 == 0) {
                table.rows[i].bgColor = "ffffff";
            } else { table.rows[i].bgColor = "f3f9fe" }
        }
    }
    var mytr = document.getElementById("GridView1").getElementsByTagName("tr");
    for (var i = 1; i < mytr.length; i++) {
        mytr[i].onmouseover = function() {
            var rows = this.childNodes.length;
            for (var row = 0; row < rows; row++) {
                this.childNodes[row].style.backgroundColor = '#B2D3FF';
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

