<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.business.User.UserLoginLog" Codebehind="UserLoginLog.aspx.cs" %>
 <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
    <script src="../../Js/ControlDate/WdatePicker.js" type="text/javascript"></script>
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
                <td align="center" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif); color: teal; background-repeat: repeat-x; height: 24px">
                    商户登录日志</td>
            </tr>
            <tr>
                <td>
                    搜索
                    <asp:DropDownList ID="SeachType" runat="server" EnableViewState="false">
                        <asp:ListItem Value="">—不限—</asp:ListItem>
                        <asp:ListItem Value="UserName">用户名</asp:ListItem>
                        <asp:ListItem Value="UserId">用户ID</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="KeyWordBox" runat="server" EnableViewState="false"></asp:TextBox>
                    开始：<asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                    截止：<asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" 查 询 " OnClick="btnSearch_Click">
                    </asp:Button>
                    <asp:Button ID="btnDel" runat="server" Text="删 除" onclick="btnDel_Click" />
                    &nbsp; &nbsp;
                </td>
            </tr>           
            <tr>
                <td align="center">
                    <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">                        
                        <asp:Repeater ID="rptUsers" EnableViewState="false" runat="server">
                            <HeaderTemplate>
                                <tr style="background-color: #507CD1;color: #fff; height:22;">
                                    <td style="width:5%">
                                        <input id="chkAll" type="checkbox">
                                    </td>
                                    <td>
                                        商户ID</td>
                                    <td>
                                        用户名</td>
                                    <td>
                                        姓名</td>
                                    <td>
                                        登录时间</td>
                                    <td>
                                        登录IP</td>
                                    <td>
                                        登录地点</td>
                                    <td>
                                        备注</td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color:#EFF3FB">
                                    <td>
                                        <input id="chkItem" type="checkbox" value='<%#Eval("id")%>' name="chkItem"/>
                                    </td>
                                    <td>
                                        <%# Eval("userID")%>
                                    </td>
                                    <td>
                                        <%# Eval("userName")%></a>
                                    </td>
                                    <td>
                                        <%# Eval("payeeName")%></a>
                                    </td>
                                     <td>
                                        <%# Eval("lastTime","{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td> 
                                    <td>
                                        <%# Eval("lastIP")%>
                                    </td>                                                                
                                    <td>
                                        <%# Eval("address")%>
                                    </td>
                                    <td>
                                        <%# Eval("remark")%>
                                    </td>                                    
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="background-color:#ffffff">
                                   <td>
                                        <input id="chkItem" type="checkbox" value='<%#Eval("id")%>' name="chkItem"/>
                                    </td>
                                    <td>
                                        <%# Eval("userID")%>
                                    </td>
                                    <td>
                                        <%# Eval("userName")%></a>
                                    </td>
                                    <td>
                                        <%# Eval("payeeName")%></a>
                                    </td>
                                     <td>
                                        <%# Eval("lastTime","{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td> 
                                    <td>
                                        <%# Eval("lastIP")%>
                                    </td>                                                                
                                    <td>
                                        <%# Eval("address")%>
                                    </td>
                                    <td>
                                        <%# Eval("remark")%>
                                    </td>                   
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr style="background-color:#EFEFEF">
                            <td  style="height:16px;">
                                <aspxc:AspNetPager ID="Pager1" runat="server" OnPageChanging="Pager1_PageChanging" AlwaysShow="True" CustomInfoHTML="总记录数：%RecordCount%&nbsp;总页数：%PageCount%&nbsp;当前页：%CurrentPageIndex%&nbsp;"
                CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到"
                Width="100%" Height="30px"></aspxc:AspNetPager>
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
