<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.User.distribution.list" Codebehind="list.aspx.cs" %>

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
    
    </script>
     <script type="text/javascript">
         function sendInfo(id) {
             window.open("../User/UserEdit.aspx?id=" + id, "�鿴�û���Ϣ", "'height=700, width=1000, top=0, left=0, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=n o, status=no");
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
                    �ӿڸ����¼</td>
            </tr>
            <tr>
                <td>
                     ���׵���
                    <asp:TextBox ID="txttrade_no" runat="server"></asp:TextBox>
                    �û�ID
                    <asp:TextBox ID="txtuserId" runat="server" Width="80px"></asp:TextBox>
                    ��������:
                    <select id="ddlbankName" runat="server">
                        <option value="">--��ѡ��--</option>
                        <option value="0002">֧����</option>
                        <option value="0003">�Ƹ�ͨ</option>
                        <option value="1002">�й���������</option>
                        <option value="1005">�й�ũҵ����</option>
                        <option value="1003">�й���������</option>
                        <option value="1026">�й�����</option>
                        <option value="1001">��������</option>
                        <option value="1006">��������</option>
                        <option value="1020">��ͨ����</option>
                        <option value="1025">��������</option>
                        <option value="1009">��ҵ����</option>
                        <option value="1027">�㷢����</option>
                        <option value="1004">�ַ�����</option>
                        <option value="1022">�������</option>
                        <option value="1021">��������</option>
                        <option value="1010">ƽ������</option>
                        <option value="1066">�й�������������</option>
                     </select>
                    �˺�
                    <asp:TextBox ID="txtbankAccount" runat="server"></asp:TextBox>
                     ��ʼ��
                         <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                     ��ֹ��
                        <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                    </asp:Button>                    
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                        <asp:Repeater ID="rptTrades" EnableViewState="false" runat="server" 
                            onitemcommand="rptTrades_ItemCommand" onitemdatabound="rptTrades_ItemDataBound">
                            <HeaderTemplate>
                                <tr style="background-color: #507CD1; color: #fff; height: 22;">
                                    <td>
                                        <input id="chkAll" type="checkbox">
                                    </td>                                    
                                    <td>
                                        �����</td>  
                                    <td>
                                        �û�ID</td>
                                    <td>
                                        ������Ϣ</td>                                   
                                    <td>
                                        ��� </td>
                                    <td>
                                        ״̬</td>
                                     <td>
                                        ʱ��</td> 
                                     <td>
                                        �ӿ���</td>                                   
                                     <td>
                                        �ӿ���ˮ��</td>
                                     <td>
                                        ˵��</td>
                                     <td>
                                         
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: #EFF3FB">
                                    <td>
                                        <input id="chkItem" type="checkbox" runat="server" value='<%#Eval("id")%>' name="chkItem" />
                                    </td>
                                    <td>
                                        <%# Eval("trade_no")%>
                                    </td>
                                    <td>
                                         <%# Eval("userid")%>
                                    </td>
                                    <td>
                                        <%# Eval("bankCode")%> <%# Eval("bankBranch")%> <br />
                                        <%# Eval("bankAccountName")%> <br />
                                        <%# Eval("bankAccount")%>
                                    </td>
                                    <td>
                                        <%# Eval("amount", "{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# viviapi.BLL.distribution.GetStatusText(Eval("status"))%>
                                    </td>
                                     <td>
                                        <%# Eval("processingTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%# viviapi.WebUI.WebUtility.GetsupplierName(Eval("suppid"))%>
                                    </td>                                     
                                    <td>
                                        <%# Eval("supp_trade_no")%>
                                    </td>                                    
                                    <td>
                                        <%# Eval("supp_message")%>
                                    </td>  
                                     <td>
                                        <asp:Button ID="btnquery" runat="server" Text="���²�ѯ" CommandName="query" CommandArgument='<%# Eval("trade_no")%>' />
                                    </td>                                     
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="background-color: #ffffff">
                                   <td>
                                        <input id="chkItem" type="checkbox" runat="server" value='<%#Eval("id")%>' name="chkItem" />
                                    </td>
                                    <td>
                                        <%# Eval("trade_no")%>
                                    </td>
                                    <td>
                                         <%# Eval("userid")%>
                                    </td>
                                    <td>
                                        <%# Eval("bankCode")%> <%# Eval("bankBranch")%> <br />
                                        <%# Eval("bankAccountName")%> <br />
                                        <%# Eval("bankAccount")%>
                                    </td>
                                    <td>
                                        <%# Eval("amount", "{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# viviapi.BLL.distribution.GetStatusText(Eval("status"))%>
                                    </td>
                                     <td>
                                        <%# Eval("processingTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%# viviapi.WebUI.WebUtility.GetsupplierName(Eval("suppid"))%>
                                    </td>                                     
                                    <td>
                                        <%# Eval("supp_trade_no")%>
                                    </td>                                    
                                    <td>
                                        <%# Eval("supp_message")%>
                                    </td>  
                                     <td>
                                        <asp:Button ID="btnquery" runat="server" Text="���²�ѯ" CommandName="query" CommandArgument='<%# Eval("trade_no")%>' />
                                    </td>       
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr style="background-color: #EFEFEF">
                            <td style="height: 16px;">
                                <aspxc:AspNetPager ID="Pager1" runat="server"
                                    AlwaysShow="True" CustomInfoHTML="�ܼ�¼����%RecordCount%&nbsp;��ҳ����%PageCount%&nbsp;��ǰҳ��%CurrentPageIndex%&nbsp;"
                                    CustomInfoTextAlign="Left" FirstPageText="��ҳ" HorizontalAlign="Right" LastPageText="ĩҳ"
                                    NavigationToolTipTextFormatString="��ת{0}ҳ" NextPageText="��һҳ" PageIndexBoxType="DropDownList"
                                    PageSize="20" PrevPageText="��һҳ" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                    ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                    TextAfterPageIndexBox="ҳ" TextBeforePageIndexBox="����" Width="100%" 
                                    Height="30px" onpagechanged="Pager1_PageChanged">
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
