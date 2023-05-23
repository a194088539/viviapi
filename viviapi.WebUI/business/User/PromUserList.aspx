<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.business.User.PromUserList" Codebehind="PromUserList.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
      .rptheadlink{color: White; font-family: ����; font-size: 12px};
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
                    �����б�</td>
            </tr>
            <tr>
                <td>
                    ����
                    <asp:DropDownList ID="StatusList" runat="server" EnableViewState="false">
                        <asp:ListItem Value="">���û�״̬��</asp:ListItem>
                        <asp:ListItem Value="1">�����</asp:ListItem>
                        <asp:ListItem Value="2">����</asp:ListItem>
                        <asp:ListItem Value="4">����</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="SeachType" runat="server" EnableViewState="false">
                        <asp:ListItem Value="">�����ޡ�</asp:ListItem>
                        <asp:ListItem Value="UserName">�û���</asp:ListItem>
                        <asp:ListItem Value="UserId">�û�ID</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="KeyWordBox" runat="server" EnableViewState="false"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                    </asp:Button>
                    <asp:Button ID="btnDelete" runat="server" CssClass="button" Text=" ɾ ��" OnClientClick="return Del_Confirm();">
                    </asp:Button>
                    <asp:Button ID="btnCashTo" runat="server" CssClass="button" Text="�� ��" OnClientClick="return GetMoney_Confirm();">
                    </asp:Button>
                    <asp:Button ID="btn_allgetmoney" runat="server" CssClass="button" Text="һ������"></asp:Button>
                    <input type="button" class="button" value="�� ��" onclick="setupwin();" />
                    <input type="button" class="button" value="�����ֻ�����" onclick="sentsmswin();" />
                </td>
            </tr>
            <tr>
                <td>
                    �ѽ����ܶ�:<%=yzfmoney %>
                    δ�����ܶ�:<%=wzfmoney %>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                        <asp:Repeater ID="rptUsers" EnableViewState="false" runat="server" OnItemDataBound="rptUsersItemDataBound">
                            <HeaderTemplate>
                                <tr style="background-color: #507CD1; color: #fff; height: 22;">
                                    <td>
                                        �̻�ID</td>
                                    <td>
                                        �û���</td>
                                    <td>
                                        ���</td>
                                    <td>
                                        ����</td>
                                    <td>
                                        ����</td>
                                    <td>
                                        ���ʵȼ�</td>
                                    <td>
                                        ״̬</td>
                                    <td>
                                        ����</td>
                                    <td>
                                        ��������</td>
                                    <td>
                                        ����</td>
                                    <td>
                                        �ƹ�����</td>
                                    <td>
                                        �����̻�</td>
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
                                        <a href='UserList.aspx?proid=<%# Eval("ID") %>' class='ljbg'>�鿴</a></td>
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
                                        <a href='AgexiaUser.aspx?proid=<%# Eval("ID") %>' class='ljbg'>�鿴</a></td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr style="background-color: #EFEFEF">
                            <td style="height: 16px;">
                                <aspxc:AspNetPager ID="Pager1" runat="server" OnPageChanged = "Pager1_PageChanged"
                                    AlwaysShow="True" CustomInfoHTML="�ܼ�¼����%RecordCount%&nbsp;��ҳ����%PageCount%&nbsp;��ǰҳ��%CurrentPageIndex%&nbsp;"
                                    CustomInfoTextAlign="Left" FirstPageText="��ҳ" HorizontalAlign="Right" LastPageText="ĩҳ"
                                    NavigationToolTipTextFormatString="��ת{0}ҳ" NextPageText="��һҳ" PageIndexBoxType="DropDownList"
                                    PageSize="20" PrevPageText="��һҳ" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                    ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                    TextAfterPageIndexBox="ҳ" TextBeforePageIndexBox="����" Width="100%" Height="30px">
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
