<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Order.OrderReport" Codebehind="OrderReport.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <script src="../../js/common.js" type="text/javascript"></script>
    <script src="../../Js/ControlDate/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellspacing="1" cellpadding="0" style="width: 100%">
        <tr>
            <td align="center" colspan="3" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 28px">
                每日商户排行
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            开始：
                            <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                            截止：
                            <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                            <asp:Button ID="btn_Search" runat="server" CssClass="button" Text=" 查 询 " OnClick="btn_Search_Click">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            总额：<asp:Label ID="lbmoney" runat="server" Text="0"></asp:Label>
                            总支出：<asp:Label ID="lbchumoney" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                    <tr height="22" style="background-color: #507CD1; color: #fff">
                        <td>
                            商户名
                        </td>
                        <td>
                            总金额
                        </td>
                        <td>
                            充值次数
                        </td>
                        <td>
                            商户所得
                        </td>                       
                    </tr>
                    <asp:Repeater ID="gv_data" runat="server">
                        <ItemTemplate>
                            <tr style="background-color: #fff">
                                <td>
                                    <%#Eval("UserName") %>
                                </td>
                                <td>
                                    <%#Eval("totalAmt")%>
                                </td>
                                <td>
                                    <%#Eval("fillcount")%>
                                </td>
                                <td>
                                    <%#Eval("payAmt")%>
                                </td>                                
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #EBEBEB">
                                 <td>
                                    <%#Eval("UserName") %>
                                </td>
                                <td>
                                    <%#Eval("totalAmt")%>
                                </td>
                                <td>
                                    <%#Eval("fillcount")%>
                                </td>
                                <td>
                                    <%#Eval("payAmt")%>
                                </td>                             
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
    </table>
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
    var mytr = document.getElementById("table2").getElementsByTagName("tr");
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

