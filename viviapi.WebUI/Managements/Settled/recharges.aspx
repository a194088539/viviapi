﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recharges.aspx.cs" Inherits="viviapi.WebUI.Managements.Settled.Recharges" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />

     <script src="../../js/common.js" type="text/javascript"></script>

  <script src="../../js/ControlDate/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
    $().ready(function(){
         $("#chkAll").click(function(){
            $("input[type='checkbox']").each(function(){
               if ($("#chkAll").attr('checked') == true){
                   $(this).attr("checked", true);
               }
               else 
                   $(this).attr("checked", false);
            });
        });      
    })
    function sendMsg(uid){
    window.showModelessDialog("SendMsg.aspx?uid="+uid,window,"dialogWidth=800px;dialogHeight=500px;");
    }
    function sendInfo(id) {
     window.open("../User/UserEdit.aspx?id=" + id, "查看用户信息", "'height=700, width=1000, top=0, left=0, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=n o, status=no");
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="modelPanel" style="background-color: #F2F2F2">
    </div>
    <input id="selectedUsers" runat="server" type="hidden" />
    <table width="100%" border="0" cellspacing="1" cellpadding="1" class="table1">
        <tr>
            <td align="center" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 24px">
                充值记录
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlstatus" runat="server">
                    <asp:ListItem Value="" Selected="true">支付状态</asp:ListItem>
                    <asp:ListItem Value="1">未付款</asp:ListItem>
                    <asp:ListItem Value="2">完成付款</asp:ListItem>
                    <asp:ListItem Value="4">支付失败</asp:ListItem>
                </asp:DropDownList>
                会员：
                <asp:TextBox ID="txtuserId" runat="server"></asp:TextBox>
                开始：
                <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                截止：
                <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" 查 询 " OnClick="btnSearch_Click">
                </asp:Button><span style="color: #ff0000; text-align: left;">总充值金额：<%=wzfmoney%></span> 
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                    <asp:Repeater ID="recharges"  runat="server" 
                        onitemcommand="recharges_ItemCommand" 
                        onitemdatabound="recharges_ItemDataBound" >
                        <HeaderTemplate>
                            <tr style="background-color: #507CD1; color: #fff; height: 22;">
                                <td>
                                    <input id="chkAll" type="checkbox">
                                </td>
                                <td>
                                    <asp:LinkButton ID="iBtnid" runat="server" CommandName="id">ID</asp:LinkButton>
                                </td>
                                <td>
                                    交易号
                                </td>
                                <td>
                                    会员名（#ID)
                                </td>
                                <td>
                                    充值金额
                                </td>
                                <td>
                                    <asp:LinkButton ID="iBtnrealPayAmt" runat="server" CommandName="realPayAmt">到账金额</asp:LinkButton>
                                </td>
                                <td>
                                    结余
                                </td>
                                <td>
                                    <asp:LinkButton ID="iBtnprocesstime" runat="server" CommandName="processtime">充值时间</asp:LinkButton>
                                </td>
                                <td>
                                    状态
                                </td>
                                <td>
                                    备注
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
                                    <%# Eval("orderid")%>
                                </td>
                                <td>
                                    <%# Eval("account")%>(#<%# Eval("userid")%>)
                                </td>
                                <td>
                                    <%# Eval("rechargeAmt", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("realPayAmt", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("Balance", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("processtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                </td>
                                <td>
                                    <%# GetStatusName(Eval("status"))%>
                                </td>
                                <td>
                                    <%# Eval("remark")%>
                                </td>
                                <td>
                                    <asp:Button ID="btnFreeze" runat="server" Text="冻结" CommandName="Freeze" CommandArgument='<%#Eval("userid")+","+Eval("realPayAmt")+","+Eval("orderid")%>' />
                                    <asp:Button ID="btnReplenish" runat="server" Text="补" CommandName="btnReplenish" CommandArgument='<%#Eval("orderid")%>' />
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
                                    <%# Eval("orderid")%>
                                </td>
                                <td>
                                    <%# Eval("account")%>(#<%# Eval("userid")%>)
                                </td>
                                <td>
                                    <%# Eval("rechargeAmt", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("realPayAmt", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("Balance", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("processtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                </td>
                                <td>
                                    <%# GetStatusName(Eval("status"))%>
                                </td>
                                <td>
                                    <%# Eval("remark")%>
                                </td>
                                <td>
                                   <asp:Button ID="btnFreeze" runat="server" Text="冻结" CommandName="Freeze" CommandArgument='<%#Eval("userid")+","+Eval("realPayAmt")+","+Eval("orderid")%>' />
                                   <asp:Button ID="btnReplenish" runat="server" Text="补" CommandName="btnReplenish" CommandArgument='<%#Eval("orderid")%>' />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>                    
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #EFEFEF">
                        <td style="height: 16px;">
                            <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CustomInfoHTML="总记录数：%RecordCount%&nbsp;总页数：%PageCount%&nbsp;当前页：%CurrentPageIndex%&nbsp;"
                                CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                                NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                                PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到" Width="100%" Height="30px"
                                OnPageChanged="Pager1_PageChanged">
                            </aspxc:AspNetPager>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

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

    </form>
</body>
</html>
