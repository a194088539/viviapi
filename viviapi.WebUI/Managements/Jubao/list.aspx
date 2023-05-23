<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Jubao.List" Codebehind="list.aspx.cs" %>

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
            window.open("modi.aspx?id=" + id, "���", "height=500,width=600");
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
                �ٱ�Ͷ��
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="StatusList" runat="server" EnableViewState="false">
                    <asp:ListItem Value="">��״̬��</asp:ListItem>
                    <asp:ListItem Value="1">�ȴ��ظ�</asp:ListItem>
                    <asp:ListItem Value="2">�ѻظ�</asp:ListItem>
                </asp:DropDownList>
                �û�ID��<asp:TextBox ID="txtUserId" runat="server" EnableViewState="false"></asp:TextBox>
                �û�����<asp:TextBox ID="txtUserName" runat="server" EnableViewState="false"></asp:TextBox>
                &nbsp&nbsp��ʼ��
                <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                &nbsp&nbsp��ֹ��
                <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                </asp:Button>
                <asp:Button ID="btnDelete" runat="server" CssClass="button" Text=" ɾ ��" OnClientClick="return Del_Confirm();"
                    OnClick="btnDelete_Click"></asp:Button>
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
                                    ����
                                </td>
                                <td>
                                    �ʼ�
                                </td>
                                <td>
                                    ����ʱ��
                                </td>
                                 <td>
                                    ����
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    ״̬
                                </td>
                                <td>
                                    �ظ���
                                </td>
                                <td>
                                    �ظ�
                                </td>
                                <td>
                                   
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB" ondblclick="javascript:sendInfo('<%# Eval("id")%>')">
                                <td>
                                    <input id="chkItem" type="checkbox" value='<%#Eval("id")%>' name="chkItem" />
                                </td>
                                <td>
                                    <%# Eval("name")%>
                                </td>
                                <td>
                                    <%# Eval("email")%></a>
                                </td>
                                 <td>
                                    <%# Eval("addtime","{0:yyyy-MM-dd HH:ss:mm}")%>
                                </td>
                                <td>
                                    <%# Enum.GetName(typeof(viviapi.Model.JuBaoEnum), Eval("type"))%>
                                </td> 
                                <td>
                                     <%# cutword(Eval("remark"))%>
                                </td>   
                                <td>
                                    <%# Enum.GetName(typeof(viviapi.Model.JuBaoStatusEnum),Eval("status"))%>
                                </td>
                                <td>
                                    <%# Eval("check")%>
                                </td>
                                <td>
                                    <%# cutword(Eval("checkremark"))%>
                                </td>                                
                                <td>
                                    <asp:Label ID="labagcmd" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff" ondblclick="javascript:sendInfo('<%# Eval("id")%>')">
                               <td>
                                    <input id="chkItem" type="checkbox" value='<%#Eval("id")%>' name="chkItem" />
                                </td>
                                <td>
                                    <%# Eval("name")%>
                                </td>
                                <td>
                                    <%# Eval("email")%></a>
                                </td>
                                 <td>
                                    <%# Eval("addtime","{0:yyyy-MM-dd HH:ss:mm}")%>
                                </td>
                                <td>
                                    <%# Enum.GetName(typeof(viviapi.Model.JuBaoEnum), Eval("type"))%>
                                </td> 
                                <td>
                                     <%# cutword(Eval("remark"))%>
                                </td>   
                                <td>
                                    <%# Enum.GetName(typeof(viviapi.Model.JuBaoStatusEnum),Eval("status"))%>
                                </td>
                                <td>
                                    <%# Eval("check")%>
                                </td>
                                <td>
                                    <%# cutword(Eval("checkremark"))%>
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
                            <aspxc:AspNetPager ID="Pager1" runat="server" OnPageChanging="Pager1_PageChanging"
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
                        <td>
                            <span style="color: Red">����˵����˫���е�����ϸ</span>
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
