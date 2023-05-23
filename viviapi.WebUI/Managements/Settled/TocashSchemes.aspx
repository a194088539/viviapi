<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.TocashScheme" Codebehind="TocashSchemes.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>后台管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />

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
                <td align="left" colspan="2" style="font-weight: bold; font-size: 14px; background-image: url(style/images/topbg.gif);
                    color: teal; background-repeat: repeat-x; height: 28px">
                    提现方案 <asp:Button ID="btnAdd" runat="server" Text="新 增" OnClick="btnAdd_Click"  />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" Width="100%" CellSpacing="1" AllowPaging="True"
                        OnPageIndexChanging="GridView1_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="schemename" HeaderText="方案名称" />
                            <asp:BoundField DataField="bankdetentiondays" HeaderText="网银T+N" />
                            <asp:BoundField DataField="carddetentiondays" HeaderText="点卡T+N" />
                            <asp:BoundField DataField="otherdetentiondays" HeaderText="其它T+N" />
                            <asp:BoundField DataField="minamtlimitofeach" HeaderText="每笔最少提" />
                            <asp:BoundField DataField="maxamtlimitofeach" HeaderText="每笔最多提" />
                            <asp:BoundField DataField="dailymaxtimes" HeaderText="每天最多提次" />
                            <asp:BoundField DataField="dailymaxamt" HeaderText="每天限额" />
                            <asp:BoundField DataField="isdefault" HeaderText="默认" />                           
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a href="TocashSchemeModi.aspx?cmd=edit&amp;ID=<%# Eval("ID") %>">编辑</a> || 
                                    <a onclick="return confirm('确定要删除这个用户？')" href="?cmd=del&amp;ID=<%# Eval("ID") %>">删除</a>
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

