<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.business.SMSLogList" Codebehind="SMSLogList.aspx.cs" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
   <link href="../style/union.css" type="text/css" rel="stylesheet" />
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
                <td align="center" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);color: teal; background-repeat: repeat-x; height: 24px">
                    �ֻ���֤����־</td>
            </tr>
            <tr>
                <td>
                    �ֻ����룺<asp:TextBox ID="txtMobile" runat="server" EnableViewState="false"></asp:TextBox>
                    <asp:Button ID="btn_Search" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btn_Search_Click">
                    </asp:Button></td>
            </tr>
            <tr>
                <td align="center">
                    <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                        <asp:Repeater ID="repSms" EnableViewState="false" runat="server" OnItemDataBound="repSms_ItemDataBound">
                            <HeaderTemplate>
                                <tr height="22" style="background-color: #507CD1; color: #fff">
                                    <td style="width:5%">                                        
                                    </td>
                                    <td style="width:5%">
                                        <input id="chkAll" type="checkbox">
                                    </td>
                                    <td style="width:5%">
                                        ���</td>
                                    <td style="width:20%">
                                        �ֻ�����</td>
                                    <td style="width:20%">
                                        �ѷ����ܴ���</td>
                                    <td style="width:20%">
                                        ����</td>
                                    <td style="width:15%">
                                        ����</td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="#EFF3FB">
                                    <td style="width: 40px;">
                                        <img src="../style/images/folder_close.gif" style="cursor: hand" onclick="collapse(this, '<%#Eval("ID")%>')" alt="" />
                                    </td>
                                    <td>
                                        <input id="chkItem" type="checkbox" runat="server" value='<%#Eval("ID")%>' name="chkItem"/>
                                    </td>
                                    <td>
                                        <%# Eval("ID")%>
                                    </td>
                                    <td>
                                        <%# Eval("phone")%>
                                    </td>                                    
                                    <td>
                                        <%# Eval("count")%>
                                    </td> 
                                    <td>
                                        <%# Eval("enable")%>
                                    </td>  
                                    <td>
                                        <asp:Literal ID="litcmd" runat="server"></asp:Literal>
                                    </td>                                    
                                </tr>
                                <tr id="<%#Eval("ID") %>" style="display:none">
                                    <td colspan="16">
                                        <asp:Repeater ID="rptDetail" runat="server" >
                                        <HeaderTemplate>
                                            <table align="center" cellpadding="0" cellspacing="0" width="98%" class="zb" style="background-color: #f1fef1;margin: 8px;">
                                                <tr class="style3">
                                                    <td>
                                                        ����ʱ��
                                                    </td>
                                                    <td>
                                                        ����IP
                                                    </td>
                                                    <td height="30">
                                                        ��֤��
                                                    </td>                                                  
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#c4d6fc'"onmouseout='this.style.backgroundColor=c;'>
                                                <td>
                                                    &nbsp;
                                                    <%#Convert.ToDateTime(((System.Data.DataRow)Container.DataItem)["sendTime"]).ToString("yyyy-MM-dd mm:ss")%>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <%#((System.Data.DataRow)Container.DataItem)["clientIP"].ToString()%>
                                                </td>
                                                <td>
                                                    &nbsp;<%#((System.Data.DataRow)Container.DataItem)["code"].ToString()%>
                                                </td>                                              
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    </td>
                                </tr>
                            </ItemTemplate>                            
                        </asp:Repeater>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr bgcolor="#EFEFEF">
                            <td height="25">
                                 <aspxc:AspNetPager ID="Pager1" runat="server" OnPageChanged ="Pager1_PageChanged"
                                    AlwaysShow="True" CustomInfoHTML="�ܼ�¼����%RecordCount%&nbsp;��ҳ����%PageCount%&nbsp;��ǰҳ��%CurrentPageIndex%&nbsp;"
                                    CustomInfoTextAlign="Left" FirstPageText="��ҳ" HorizontalAlign="Right" LastPageText="ĩҳ"
                                    NavigationToolTipTextFormatString="��ת{0}ҳ" NextPageText="��һҳ" PageIndexBoxType="DropDownList"
                                    PageSize="20" PrevPageText="��һҳ" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                    ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                    TextAfterPageIndexBox="ҳ" TextBeforePageIndexBox="����" Width="100%" Height="30px">
                                </aspxc:AspNetPager>
                            </td>
                        </tr>
                        <tr>
                            <td height="25">˵���������ޡ�����ǡ�True�� ֻ�ܷ����ڡ�վ�����á������õġ�һ���ֻ������Է�����Ϣ���������������Ϊ��False������һ�������ƣ����Է�����������</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>

    <script type="text/javascript">
    function collapse(img, objName){
	    var obj = document.getElementById(objName);
	    if (img.src.indexOf('open') != -1) {
		    img.src = img.src.replace('open', 'close');
		    obj.style.display = 'none';
	    }
	    else {
		    img.src = img.src.replace('close', 'open');
		    obj.style.display = '';
	    }
    }

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
