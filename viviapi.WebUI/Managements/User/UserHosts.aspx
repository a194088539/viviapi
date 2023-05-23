<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.User.UserHosts" Codebehind="UserHosts.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .rptheadlink
        {
            color: White;
            font-family: ����;
            font-size: 12px;
        }
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
        function sendInfo(id) {
            window.open("UserImageCheck.aspx?id=" + id, "���", "height=400,width=900");
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
                �û�֧�����ӵ�ַ����
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="StatusList" runat="server" EnableViewState="false">
                    <asp:ListItem Value="">��״̬��</asp:ListItem>
                    <asp:ListItem Value="0">δ֪</asp:ListItem>
                    <asp:ListItem Value="1">�ѿ���</asp:ListItem>
                    <asp:ListItem Value="2">�ѹر�</asp:ListItem>
                </asp:DropDownList>
                �û�ID��<asp:TextBox ID="txtUserId" runat="server" EnableViewState="false"></asp:TextBox>
                �û�����<asp:TextBox ID="txtUserName" runat="server" EnableViewState="false"></asp:TextBox>
                &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                </asp:Button>
                <asp:Button ID="btnDelete" runat="server" CssClass="button" Text=" ɾ ��" OnClientClick="return Del_Confirm();"
                    OnClick="btnDelete_Click"></asp:Button>
                <asp:Button ID="btnOpen" runat="server" CssClass="button" Text="�� ��" 
                    OnClientClick="return GetMoney_Confirm();" onclick="btnOpen_Click"
                    ></asp:Button>
                 <asp:Button ID="btnClose" runat="server" CssClass="button" Text="�� ��" 
                    OnClientClick="return GetMoney_Confirm();" onclick="btnClose_Click"
                    ></asp:Button>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                    <asp:Repeater ID="rptIamges" EnableViewState="false" runat="server" OnItemDataBound="rptUsersItemDataBound">
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
                                    ��·վ��
                                </td>
                                <td>
                                    ��·����
                                </td>
                                <td>
                                    ֧����ַ
                                </td>
                                <td>
                                    ״̬
                                </td>                               
                                <td>
                                    ����
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB">
                                <td>
                                    <input id="chkItem" type="checkbox" value='<%#Eval("id")%>' name="chkItem" />
                                </td>
                                <td>
                                    <%# Eval("userId")%>
                                </td>
                                <td>
                                    <a href='UserEdit.aspx?ID=<%# Eval("ID") %>'><strong>
                                        <%# Eval("userName")%>
                                    </strong></a>
                                </td>
                                <td>
                                    <%# Eval("hostName")%>
                                </td>                                
                                <td>
                                   <a href='<%# Eval("hostUrl")%>' target="_blank"><%# Eval("hostUrl")%></a>
                                </td>
                                 <td>
                                    <a href='<%# GetPaymentUrl( Eval("ID"))%>' target="_blank"><%#GetPaymentUrl( Eval("ID"))%></a>
                                </td>
                                <td>
                                    <%# Enum.GetName(typeof(viviapi.Model.User.userHostStatus),Eval("status"))%>
                                </td>
                                <td>
                                    <asp:Label ID="labagcmd" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff">
                                 <td>
                                    <input id="chkItem" type="checkbox" value='<%#Eval("id")%>' name="chkItem" />
                                </td>
                                <td>
                                    <%# Eval("userId")%>
                                </td>
                                <td>
                                    <a href='UserEdit.aspx?ID=<%# Eval("ID") %>'><strong>
                                        <%# Eval("userName")%>
                                    </strong></a>
                                </td>
                                <td>
                                    <%# Eval("hostName")%>
                                </td>
                                <td>
                                   <a href='<%# Eval("hostUrl")%>' target="_blank"><%# Eval("hostUrl")%></a>
                                </td>
                                <td>
                                    <a href='<%# GetPaymentUrl( Eval("ID"))%>' target="_blank"><%#GetPaymentUrl( Eval("ID"))%></a>
                                </td>
                                <td>
                                    <%# Enum.GetName(typeof(viviapi.Model.User.userHostStatus),Eval("status"))%>
                                </td>
                                <td>
                                    <asp:Label ID="labagcmd" runat="server"></asp:Label>
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
