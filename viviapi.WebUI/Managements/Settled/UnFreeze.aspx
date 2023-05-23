<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.User.UnFreeze" Codebehind="UnFreeze.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .rptheadlink{color: White;font-family: ����;font-size: 12px;}
         ;</style>
    <script src="../../js/common.js" type="text/javascript"></script>
    <script src="../../js/ControlDate/WdatePicker.js" type="text/javascript"></script>
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
            <td align="center" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);color: teal; background-repeat: repeat-x; height: 24px">
                �ⶳ���� 
            </td>
        </tr>
        <tr>
            <td>
                �û�ID
                <asp:TextBox ID="txtuserId" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                    <asp:Repeater ID="rptData" runat="server" 
                        OnItemDataBound="rptUsersItemDataBound" onitemcommand="rptUsers_ItemCommand">
                        <HeaderTemplate>
                            <tr style="background-color: #507CD1; color: #fff; height: 22;">
                                <td>
                                    <input id="chkAll" type="checkbox">
                                </td>
                                <td>
                                    �̻�ID
                                </td> 
                                 <td>
                                    �û���
                                </td>   
                                 <td>
                                    ��ʵ����
                                </td>                                     
                                <td>
                                    �������
                                </td>
                                <td>
                                    ����ԭ��
                                </td>
                                <td>
                                    �Ƿ�ⶳ
                                </td>
                                <td>
                                    �ⶳ��ʽ
                                </td>
                                <td>
                                    ����ʱ��
                                </td>
                                <td>
                                    ����
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB">
                                <td>
                                    <input id="chkItem" type="checkbox" runat="server" value='<%#Eval("id")%>' name="chkItem" />
                                </td>
                                <td>
                                     <%# Eval("userid")%>
                                </td>  
                                  <td>
                                    <%# Eval("userName")%>
                                </td>  
                                <td>
                                    <%# Eval("full_name")%>
                                </td>                                
                                <td>
                                     <%# Eval("freezeAmt", "{0:f2}")%>
                                </td>
                                <td>
                                     <%# Eval("why")%>
                                </td>
                                <td>
                                     <%#Enum.GetName(typeof(viviapi.Model.Settled.AmtFreezeInfoStatus),Eval("status"))%>
                                </td>
                                <td>
                                    <%#Enum.GetName(typeof(viviapi.Model.Settled.AmtunFreezeMode), Eval("unfreezemode"))%>                                     
                                </td>
                                <td>
                                     <%# Eval("addtime","{0:yyyy-MM-dd HH:mm:ss}")%>
                                </td>
                                <td>
                                    <asp:Button ID="btn_unfreeze1" runat="server" CommandName="unfreeze1" CommandArgument='<%#Eval("id")%>' Text="�ⶳ�����" />
                                    <asp:Button ID="btn_unfreeze2" runat="server" CommandName="unfreeze2" CommandArgument='<%#Eval("id")%>' Text="�ⶳ���۳�" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                                <td>
                                    <input id="chkItem" type="checkbox" runat="server" value='<%#Eval("id")%>' name="chkItem" />
                                </td>
                                <td>
                                     <%# Eval("userid")%>
                                </td>  
                                  <td>
                                    <%# Eval("userName")%>
                                </td>  
                                <td>
                                    <%# Eval("full_name")%>
                                </td>                                
                                <td>
                                     <%# Eval("freezeAmt", "{0:f2}")%>
                                </td>
                                <td>
                                     <%# Eval("why")%>
                                </td>
                                <td>
                                     <%#Enum.GetName(typeof(viviapi.Model.Settled.AmtFreezeInfoStatus),Eval("status"))%>
                                </td>
                                <td>
                                    <%#Enum.GetName(typeof(viviapi.Model.Settled.AmtunFreezeMode), Eval("unfreezemode"))%>                                              
                                </td>
                                <td>
                                     <%# Eval("addtime","{0:yyyy-MM-dd HH:mm:ss}")%>
                                </td>
                                <td>
                                    <asp:Button ID="btn_unfreeze1" runat="server" CommandName="unfreeze1" CommandArgument='<%#Eval("id")%>' Text="�ⶳ�����" />
                                    <asp:Button ID="btn_unfreeze2" runat="server" CommandName="unfreeze2" CommandArgument='<%#Eval("id")%>' Text="�ⶳ���۳�" />
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
                    <tr>
                    <td>
                        ˵����
                        �ⶳ�����(�Ѷ���Ŀ�����̻��������)
                        �ⶳ���۳�(�Ѷ���Ŀ�������Ϊ�ѽⶳ������������̻�)
                    </td>
                    </tr>
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
