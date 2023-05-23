<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.User.BankForUser" Codebehind="BankForUser.aspx.cs" %>

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
        function isselect() {
            var result = false;
            $("input[type='checkbox']").each(function() {            
                if ($(this).attr("id") != "chkAll") {
                    if ($(this).attr('checked') == true) {
                        result = true;
                    }
                }
            });
            return result;
        }
        function check() {
            if (!isselect()) {
                alert('����ѡ��һ����¼');
                return false;
            }
            return true;
        }
        function checkAll() {
            if ($("#txtPassWord").val() =="") {
                alert('�������������');
                return false;
            }
            return true;
        }
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
            <td align="center" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 24px">
                �̻����� 
            </td>
        </tr>
        <tr>
            <td>
                �û�ID
                <asp:TextBox ID="txtuserId" runat="server"></asp:TextBox>
                ��<asp:TextBox ID="txtbalance" runat="server" Text="100"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" �� ѯ " OnClick="btnSearch_Click">
                </asp:Button>                
                <asp:Button ID="btnBatchSettle" runat="server" CssClass="button" Text="��������" 
                    onclick="btnBatchSettle_Click" OnClientClick="return check();">
                </asp:Button>
                �������룺<asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox>         
                <asp:Button ID="btnAllSettle" runat="server" CssClass="button" Text="ȫ������" 
                    onclick="btnAllSettle_Click" OnClientClick="return checkAll();">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                    <asp:Repeater ID="rptUsers" runat="server" 
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
                                    ��¼�û�
                                </td>                                
                                <td>
                                    ���
                                </td>
                                <td>
                                    ������
                                </td>
                                <td>
                                    ����
                                </td>
                                <td>
                                    �տ���
                                </td>
                                <td>
                                    ������
                                </td>
                                <td>
                                    ���п���
                                </td>
                                <td>
                                    ������ַ
                                </td>
                                <td>
                                    ����ģʽ
                                </td>
                                <td>
                                    ��Ѻ���
                                </td>
                                <td>
                                    ������
                                </td>
                                <td>
                                    ����
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB">
                                <asp:HiddenField ID="hfuserid" runat="server" Value='<%# Eval("id")%>' />
                                <td>
                                    <input id="chkItem" type="checkbox" runat="server" value='<%#Eval("id")%>' name="chkItem" />                                    
                                </td>
                                <td>
                                    <%# Eval("id")%>
                                    
                                </td> 
                                <td>
                                    <%# Eval("username")%>
                                </td>                              
                                <td>
                                    <%# Eval("balance", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("unpayment", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("Freeze", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("payeeName")%>
                                </td>
                                 <td>
                                    <%#viviapi.BLL.SettledFactory.GetSettleBankName(Eval("payeeBank").ToString())%>
                                </td>
                                <td>
                                    <%# Eval("account")%>
                                </td>
                                <td>
                                    <%# Eval("bankAddress")%>
                                </td>
                                <td>
                                    T+<%# Eval("settles")%>
                                </td>
                                <td>
                                    <asp:Literal ID="litTodayIncome" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpayAmt" runat="server" Width="80px"></asp:TextBox>
                                </td>                               
                                <td>
                                    <asp:Button ID="btnSettled" runat="server" CommandName="Settled" CommandArgument='<%#Eval("id")%>' Text="����" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff">
                                <asp:HiddenField ID="hfuserid" runat="server" Value='<%# Eval("id")%>' />
                                <td>
                                    <input id="chkItem" type="checkbox" runat="server" value='<%#Eval("id")%>' name="chkItem" />
                                </td>
                                <td>
                                    <%# Eval("id")%>                                   
                                </td> 
                                <td>
                                    <%# Eval("username")%>
                                </td>                              
                                <td>
                                    <%# Eval("balance", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("unpayment", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("Freeze", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("payeeName")%>
                                </td>
                                 <td>
                                    <%#viviapi.BLL.SettledFactory.GetSettleBankName(Eval("payeeBank").ToString())%>
                                </td>
                                <td>
                                    <%# Eval("account")%>
                                </td>
                                 <td>
                                    <%# Eval("bankAddress")%>
                                </td>
                                <td>
                                    T+<%# Eval("settles")%>
                                </td>
                                <td>
                                    <asp:Literal ID="litTodayIncome" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpayAmt" runat="server" Width="80px"></asp:TextBox>
                                </td>                               
                                <td>
                                    <asp:Button ID="btnSettled" runat="server" CommandName="Settled" CommandArgument='<%#Eval("id")%>' Text="����" />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #EFEFEF">
                        <td style="height: 16px;">
                            <aspxc:AspNetPager ID="Pager1" runat="server" 
                                onpagechanged="Pager1_PageChanged"
                                
                                AlwaysShow="True" CustomInfoHTML="�ܼ�¼����%RecordCount%&nbsp;��ҳ����%PageCount%&nbsp;��ǰҳ��%CurrentPageIndex%&nbsp;"
                                CustomInfoTextAlign="Left" FirstPageText="��ҳ" HorizontalAlign="Right" LastPageText="ĩҳ"
                                NavigationToolTipTextFormatString="��ת{0}ҳ" NextPageText="��һҳ" PageIndexBoxType="DropDownList"
                                PageSize="20" PrevPageText="��һҳ" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="ҳ" TextBeforePageIndexBox="����" Width="100%" 
                                Height="30px" 
                                >
                            </aspxc:AspNetPager>
                        </td>
                    </tr>
                    <tr>
                    <td>
                        ˵�������ǰ���ƽ̨�涨���������̸��̻���� <br />��ѯ�����е�����ǡ��̻�ʵ������ȥ�������л�δ��ˡ��͡�������Ľ�
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
