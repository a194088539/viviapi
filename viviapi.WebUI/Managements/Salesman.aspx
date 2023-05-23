<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.User.Salesman" Codebehind="Salesman.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="style/union.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .rptheadlink
        {
            color: White;
            font-family: 宋体;
            font-size: 12px;
        }
         ;</style>

    <script src="../js/common.js" type="text/javascript"></script>
    <script src="../js/ControlDate/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        $().ready(function() {
            $("#chkAll").click(function() {
                $("input[type='checkbox']").each(function() {
                    if ($("#chkAll").attr('checked') == true) {
                        $(this).attr("checked", true);
                    }
                    else
                        $(this).attr("checked", false);
                });
            });
        })
        function sendMsg(uid) {
            window.showModelessDialog("SendMsg.aspx?uid=" + uid, window, "dialogWidth=800px;dialogHeight=500px;");
        }
    
    </script>

    <script type="text/javascript">
        function sendInfo(id) {
            window.open("../User/UserEdit.aspx?id=" + id, "查看用户信息", "'height=700, width=1000, top=0, left=0, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=n o, status=no");
        }
    </script>

</head>
<body class="yui-skin-sam">
    <form id="form1" runat="server">
    <div id="modelPanel" style="background-color: #F2F2F2">
    </div>
    <input id="selectedUsers" runat="server" type="hidden" />
    <table width="100%" border="0" cellspacing="1" cellpadding="1" class="table1">
        <tr>
            <td align="center" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 24px">
                业务员业务统计 
            </td>
        </tr>
        <tr>
            <td>
                业务ID
                <asp:TextBox ID="txtmanageId" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" 查 询 " OnClick="btnSearch_Click">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                    <asp:Repeater ID="rptmanage" runat="server" 
                        OnItemDataBound="rptUsersItemDataBound" onitemcommand="rptUsers_ItemCommand">
                        <HeaderTemplate>
                            <tr style="background-color: #507CD1; color: #fff; height: 22;">
                                <td>
                                    <input id="chkAll" type="checkbox">
                                </td>
                                <td>
                                    业务ID
                                </td>  
                                <td>
                                    姓名
                                </td>                                
                                <td>
                                    余额
                                </td>
                                <td>
                                    总商户数
                                </td>
                                <td>
                                    提成类型
                                </td>
                                <td>
                                    提成比例
                                </td>
                                <td>
                                    本月收入
                                </td>
                                <td>
                                    本日收入
                                </td>  
                                <td>
                                    本月已结算
                                </td>                                
                                <td>
                                    结算金额
                                </td>
                                <td>
                                    操作
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB">
                                <td>
                                    <input id="chkItem" type="checkbox" runat="server" value='<%#Eval("id")%>' name="chkItem" />
                                </td>
                                <td>
                                    <%# Eval("id")%>
                                </td> 
                                <td>
                                    <%# Eval("relname")%>
                                </td>                              
                                <td>
                                    <%# Eval("Balance", "{0:f2}")%>
                                </td>
                                <td>
                                    <asp:Literal ID="litUserCount" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <%# Eval("Commissiontypeview")%>
                                </td>
                                <td>
                                    <%# Eval("Commission", "{0:f5}")%>
                                </td>
                                <td>
                                    <asp:Literal ID="litMonthIncome" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litDayIncome" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litMonthSetted" runat="server"></asp:Literal>
                                </td>
                                <td>
                                     <asp:TextBox ID="txtpayAmt" runat="server" Width="80px"></asp:TextBox>
                                </td>                                                             
                                <td>
                                    <asp:Button ID="btnSettled" runat="server" CommandName="Settled" CommandArgument='<%#Eval("id")%>' Text=" 结 算 " />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff">
                                <td>
                                    <input id="chkItem" type="checkbox" runat="server" value='<%#Eval("id")%>' name="chkItem" />
                                </td>
                                <td>
                                    <%# Eval("id")%>
                                </td> 
                                <td>
                                    <%# Eval("relname")%>
                                </td>                              
                                <td>
                                    <%# Eval("Balance", "{0:f2}")%>
                                </td>
                                <td>
                                    <asp:Literal ID="litUserCount" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <%# Eval("Commissiontypeview")%>
                                </td>
                                <td>
                                    <%# Eval("Commission", "{0:f5}")%>
                                </td>
                                <td>
                                    <asp:Literal ID="litMonthIncome" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litDayIncome" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Literal ID="litMonthSetted" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpayAmt" runat="server" Width="80px"></asp:TextBox>
                                </td>                                                             
                                <td>
                                    <asp:Button ID="btnSettled" runat="server" CommandName="Settled" CommandArgument='<%#Eval("id")%>' Text=" 结 算 " />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </table>                
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript">
        function handler(tp) {
        }

        var mytr = document.getElementById("tab").getElementsByTagName("tr");
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
