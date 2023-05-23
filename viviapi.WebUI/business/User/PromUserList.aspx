<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.business.User.PromUserList" Codebehind="PromUserList.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
      .rptheadlink{color: White; font-family: 宋体; font-size: 12px};
    </style>

    <script src="../../js/common.js" type="text/javascript"></script>

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
                    代理列表</td>
            </tr>
            <tr>
                <td>
                    搜索
                    <asp:DropDownList ID="StatusList" runat="server" EnableViewState="false">
                        <asp:ListItem Value="">—用户状态—</asp:ListItem>
                        <asp:ListItem Value="1">待审核</asp:ListItem>
                        <asp:ListItem Value="2">正常</asp:ListItem>
                        <asp:ListItem Value="4">锁定</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="SeachType" runat="server" EnableViewState="false">
                        <asp:ListItem Value="">—不限—</asp:ListItem>
                        <asp:ListItem Value="UserName">用户名</asp:ListItem>
                        <asp:ListItem Value="UserId">用户ID</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="KeyWordBox" runat="server" EnableViewState="false"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" 查 询 " OnClick="btnSearch_Click">
                    </asp:Button>
                    <asp:Button ID="btnDelete" runat="server" CssClass="button" Text=" 删 除" OnClientClick="return Del_Confirm();">
                    </asp:Button>
                    <asp:Button ID="btnCashTo" runat="server" CssClass="button" Text="提 现" OnClientClick="return GetMoney_Confirm();">
                    </asp:Button>
                    <asp:Button ID="btn_allgetmoney" runat="server" CssClass="button" Text="一键提现"></asp:Button>
                    <input type="button" class="button" value="设 置" onclick="setupwin();" />
                    <input type="button" class="button" value="发送手机短信" onclick="sentsmswin();" />
                </td>
            </tr>
            <tr>
                <td>
                    已结算总额:<%=yzfmoney %>
                    未结算总额:<%=wzfmoney %>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                        <asp:Repeater ID="rptUsers" EnableViewState="false" runat="server" OnItemDataBound="rptUsersItemDataBound">
                            <HeaderTemplate>
                                <tr style="background-color: #507CD1; color: #fff; height: 22;">
                                    <td>
                                        商户ID</td>
                                    <td>
                                        用户名</td>
                                    <td>
                                        余额</td>
                                    <td>
                                        扣率</td>
                                    <td>
                                        姓名</td>
                                    <td>
                                        费率等级</td>
                                    <td>
                                        状态</td>
                                    <td>
                                        类型</td>
                                    <td>
                                        代理域名</td>
                                    <td>
                                        操作</td>
                                    <td>
                                        推广设置</td>
                                    <td>
                                        下属商户</td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: #EFF3FB">
                                    <td>
                                        <%# Eval("ID")%>
                                    </td>
                                    <td bgcolor="#EFF3FB">
                                           <a href='UserEdit.aspx?ID=<%# Eval("ID") %>'>
                                                <strong>
                                                    <%# Eval("userName")%>
                                                </strong>
                                            </a>
                                    </td>
                                    <td>
                                        <%# Eval("balance")%>
                                    </td>
                                    <td>
                                        <%# Eval("CPSDrate")%>
                                    </td>
                                    <td>
                                        <%# Eval("PayeeName")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUserLevel" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUserStat" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUserType" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <%# Eval("prourl")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="labcmd" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="labagcmd" runat="server"></asp:Label></td>
                                    <td>
                                        <a href='UserList.aspx?proid=<%# Eval("ID") %>' class='ljbg'>查看</a></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="background-color: #ffffff">
                                    <td>
                                        <%# Eval("ID")%>
                                    </td>
                                    <td bgcolor="#EFF3FB">
                                           <a href='UserEdit.aspx?ID=<%# Eval("ID") %>'>
                                                <strong>
                                                    <%# Eval("userName")%>
                                                </strong>
                                            </a>
                                    </td>
                                    <td>
                                        <%# Eval("balance")%>
                                    </td>
                                    <td>
                                        <%# Eval("CPSDrate")%>
                                    </td>
                                    <td>
                                        <%# Eval("PayeeName")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUserLevel" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUserStat" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUserType" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <%# Eval("prourl")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="labcmd" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="labagcmd" runat="server"></asp:Label></td>
                                    <td>
                                        <a href='AgexiaUser.aspx?proid=<%# Eval("ID") %>' class='ljbg'>查看</a></td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr style="background-color: #EFEFEF">
                            <td style="height: 16px;">
                                <aspxc:AspNetPager ID="Pager1" runat="server" OnPageChanged = "Pager1_PageChanged"
                                    AlwaysShow="True" CustomInfoHTML="总记录数：%RecordCount%&nbsp;总页数：%PageCount%&nbsp;当前页：%CurrentPageIndex%&nbsp;"
                                    CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                                    NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                                    PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                    ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                    TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到" Width="100%" Height="30px">
                                </aspxc:AspNetPager>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>

    <script type="text/javascript">
 function handler(tp){
 }

var mytr =  document.getElementById("tab").getElementsByTagName("tr");
for(var i=1;i<mytr.length;i++){
  mytr[i].onmouseover= function(){ 
var rows = this.childNodes.length;
for(var row=0;row<rows;row++){
this.childNodes[row].style.backgroundColor='#E6EEFF';
}
};
  mytr[i].onmouseout= function(){ 
var rows = this.childNodes.length;
for(var row=0;row<rows;row++){
this.childNodes[row].style.backgroundColor='';
}
};
}

    </script>

</body>
</html>
