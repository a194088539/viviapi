<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.business.User.UserChannel" Codebehind="UserChannel.aspx.cs" %>

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
        $().ready(function () {
            $("#chkAll").click(function () {
                $("input[type='checkbox']").each(function () {
                    if ($("#chkAll").attr('checked') == true) {
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
                    �̻�ͨ������ <asp:Button ID="btnAllOpen" runat="server" Text="ȫ������" OnClick="btnAllOpen_Click"/>  <asp:Button ID="btnAllColse" runat="server" Text="ȫ���ر�" OnClick="btnAllColse_Click"/>  
                    <asp:Button ID="btnReSet" runat="server" Text="�ָ�Ĭ��" onclick="btnReSet_Click"/>
                    
                                            <asp:Button ID="btnSave" runat="server" Text="��������" 
                        onclick="btnSave_Click" />
                    </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblInfo" runat="server"></asp:Label>
                </td>
            </tr>  
            <tr>
                <td align="center">
                    <table width="100%"  border="0" align="center" cellpadding="2" cellspacing="1">
                        <asp:Repeater ID="rpt_paymode" runat="server" 
                            onitemdatabound="rpt_paymode_ItemDataBound" 
                            onitemcommand="rpt_paymode_ItemCommand">
                                <HeaderTemplate>
                                    <table id="tab" width="100%" border="0" align="center" cellpadding="1" cellspacing="1">
                                        <tr style="background-color: #507CD1; color: #fff; height: 22;">
                                            <td width="101" height="25">
                                                ͨ������</td>
                                            <td width="156">
                                                ͨ������</td>
                                            <td width="172">
                                                ��ɱ���</td>
                                            <td width="152">
                                                ͨ��״̬</td>
                                            <td width="144">
                                                �û�����</td>                                           
                                            <td width="144">
                                                ����״̬</td>
                                            <td width="144">
                                                ͨ����</td>                                            
                                            <td width="149">
                                                ����</td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: #EFF3FB">
                                        <td style="height:25" >
                                            <%#Eval("typeId")%><asp:HiddenField ID="hftypeId" runat="server" Value='<%#Eval("typeId")%>' />
                                        </td>
                                        <td>
                                            <%#Eval("modetypename")%></td>
                                        <td>
                                            <%#Eval("payrate","{0:0.00}")%>%</td>
                                        <td>
                                            <img id="imgstus<%#Eval("id")%>" src="../style/images/<%#Eval("type_status")%>.png" alt=""/>
                                        </td>
                                        <td>
                                            <img  src="../style/images/<%#Eval("user_setting")%>.png" alt="" />
                                        </td>
                                        <td>
                                            <img  src="../style/images/<%#Eval("sys_setting")%>.png" alt="" />
                                        </td> 
                                        <td>
                                            <asp:DropDownList ID="ddlsupp" runat="server">
                                            </asp:DropDownList>
                                        </td>                                        
                                        <td>
                                            <div class="height40">                                                                                                
                                                <asp:Button ID="btn_open" runat="server" Text="����" CommandName="open" CommandArgument='<%#Eval("typeId")%>' />
                                                <asp:Button ID="btn_close" runat="server" Text="�ر�" CommandName="close" CommandArgument='<%#Eval("typeId")%>'/>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr style="background-color: #ffffff">
                                        <td style="height:25" >
                                            <%#Eval("typeId")%></td>
                                        <td>
                                            <%#Eval("modetypename")%></td>
                                        <td>
                                            <%#Eval("payrate","{0:0.00}")%>%</td>
                                        <td>
                                            <img id="imgstus<%#Eval("id")%>" src="../style/images/<%#Eval("type_status")%>.png" alt="" />
                                        </td>
                                        <td>
                                            <img  src="../style/images/<%#Eval("user_setting")%>.png" alt="" />
                                        </td>
                                        <td>
                                            <img  src="../style/images/<%#Eval("sys_setting")%>.png" alt="" />
                                        </td>  
                                        <td>
                                           <asp:DropDownList ID="ddlsupp" runat="server">
                                            </asp:DropDownList>
                                        </td>    
                                        <td>
                                            <div class="height40">                                                                                                
                                                  <asp:Button ID="btn_open" runat="server" Text="����" CommandName="open" CommandArgument='<%#Eval("typeId")%>' />
                                                <asp:Button ID="btn_close" runat="server" Text="�ر�" CommandName="close" CommandArgument='<%#Eval("typeId")%>'/>
                                            </div>
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                             <tr>
                            <td style="height: 20px">
                                <div align="center">                                    
                                    <input type="button" value="�� ��" onclick="backreturn()" />
                                </div>
                            </td>
                        </tr>
                    </table>                    
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="puser" runat="server" />
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".bottonstyle02").each(function () {
                $(this).click(function () {
                    var oId = $(this).attr("id");
                    var txt = $("#" + oId).text();
                    var msg = "", cmd = "", isopen = true;
                    if (txt == "�ر�") {
                        msg = "ȷ��Ҫ�رմ�֧��ͨ����";
                        cmd = "close";
                        isopen = false;
                    }
                    else {
                        msg = "ȷ��Ҫ������֧��ͨ����";
                        cmd = "open";
                    }
                    if (confirm(msg)) {
                        var modeid = $(this).attr("id");
                        $.get("UserChannel.aspx", { userId: $("#puser").val(), id: modeid, cmd: cmd, t: Math.random() },
					    function (data) {
					        if (data == 'success') {
					            alert(txt + '�ɹ���');
					            if (isopen) {
					                $("#imgstus" + oId).attr("src", "/images/right.png");
					                $("#" + oId).attr("innerText", "�ر�");
					            }
					            else {
					                $("#imgstus" + oId).attr("src", "/images/wrong.png");
					                $("#" + oId).attr("innerText", "����");
					            }
					        } else {
					            alert(txt + 'ʧ�ܣ�');
					        }
					    });
                    }
                });
            })
        });
    </script>
    <script type="text/javascript">
        function backreturn() {
            history.go(-1);
        }
        function handler(tp) {
        }

        var mytr = document.getElementById("tab").getElementsByTagName("tr");
        for (var i = 1; i < mytr.length; i++) {
            mytr[i].onmouseover = function () {
                var rows = this.childNodes.length;
                for (var row = 0; row < rows; row++) {
                    this.childNodes[row].style.backgroundColor = '#E6EEFF';
                }
            };
            mytr[i].onmouseout = function () {
                var rows = this.childNodes.length;
                for (var row = 0; row < rows; row++) {
                    this.childNodes[row].style.backgroundColor = '';
                }
            };
        }

    </script>

</body>
</html>
