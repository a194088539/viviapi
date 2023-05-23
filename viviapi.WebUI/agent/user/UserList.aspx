<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.agent.User.UserList" Codebehind="UserList.aspx.cs" %>

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
        var btnDeleteId = "#<%=btnDelete.ClientID%>";
        $(btnDeleteId).click(function(){
            return confirm("ȷ��Ҫɾ����Щ�̻���?");
        });     
    })
    function sendMsg(uid){
        window.showModelessDialog("SendMsg.aspx?uid="+uid,window,"dialogWidth=800px;dialogHeight=500px;");
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
                    �̻�����</td>
            </tr>
            <tr>
                <td>
                    ����
                    <asp:DropDownList ID="StatusList" runat="server" EnableViewState="false">                        
                        <asp:ListItem Value="2">����</asp:ListItem>                        
                    </asp:DropDownList>                    
                     �û�����<asp:TextBox ID="txtuserName" runat="server"></asp:TextBox> 
                    �û�ID��<asp:TextBox ID="txtuserId" runat="server"></asp:TextBox>                    
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                    </asp:Button>
                    <asp:Button ID="btnDelete" runat="server" CssClass="button" Text=" ɾ ��" 
                        onclick="btnDelete_Click">
                    </asp:Button>
                    <asp:Button ID="btnCashTo" runat="server" CssClass="button" Text="�� ��" OnClientClick="return GetMoney_Confirm();" Visible="false">
                    </asp:Button>
                    <asp:Button ID="btn_allgetmoney" runat="server" CssClass="button" Text="һ������" Visible="false"></asp:Button>
                    <asp:Button ID="btn_Msg" runat="server" CssClass="button" Text="�ڲ���Ϣ" 
                        onclick="btn_Msg_Click"></asp:Button>
                    <input type="button" class="button" value="�� ��" onclick="setupwin();" Visible="false"/>
                    <input type="button" class="button" value="�����ֻ�����" onclick="sentsmswin();" /><br />
                    
                    QQ���룺<asp:TextBox ID="txtQQ" runat="server"></asp:TextBox>
                    �ֻ���<asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
                    Email��<asp:TextBox ID="txtMail" runat="server"></asp:TextBox>  
                    ������<asp:TextBox ID="txtfullname" runat="server"></asp:TextBox> 
                   
                </td>
            </tr>
            <tr style="display:none">
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
                                        ǩԼ����</td>
                                    <td>
                                        �̻�ID</td>
                                    <td>
                                        �û���</td>
                                    <td>
                                        <asp:HyperLink ID="hlinkOrderby" runat="server" NavigateUrl="?orderby=balance&type=asc" CssClass="rptheadlink">����</asp:HyperLink>
                                    </td>
                                    <td>
                                        ����¼</td>
                                    <td>
                                        ����</td>
                                    <td>
                                        ״̬</td>
                                    <td>
                                        ����</td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: #EFF3FB">                                    
                                    <td>
                                        <%#viviapi.BLL.User.UserFactory.GetClassViewName(Eval("classid"))%>
                                    </td>
                                    <td>
                                        <%# Eval("id")%>
                                    </td>
                                    <td>
                                        <%# Eval("userName")%>
                                    </td>
                                    <td>
                                        <%# Eval("balance")%>
                                    </td>
                                    <td>
                                        <%# Eval("lastLoginTime","{0:yyyy-MM-dd HH:ss:mm}")%>
                                    </td>
                                    <td>
                                        <%# Eval("full_name")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUserStat" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUserSettle" runat="server"></asp:Label>
                                        <a href="UserPayRateEdit.aspx?ID=<%# Eval("id")%>">
                                            ����</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="background-color: #ffffff">
                                   <td>
                                        <%#viviapi.BLL.User.UserFactory.GetClassViewName(Eval("classid"))%>
                                    </td>
                                    <td>
                                        <%# Eval("id")%>
                                    </td>
                                    <td>
                                        <%# Eval("userName")%>
                                    </td>
                                    <td>
                                        <%# Eval("balance")%>
                                    </td>
                                    <td>
                                        <%# Eval("lastLoginTime","{0:yyyy-MM-dd HH:ss:mm}")%>
                                    </td>
                                    <td>
                                        <%# Eval("full_name")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUserStat" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUserSettle" runat="server"></asp:Label>
                                        <a href="UserPayRateEdit.aspx?ID=<%# Eval("id")%>">
                                            ����</a>
                                    </td>                                     
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr style="background-color: #EFEFEF">
                            <td style="height: 16px;">
                                <aspxc:AspNetPager ID="Pager1" runat="server" OnPageChanged="Pager1_PageChanged"
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
