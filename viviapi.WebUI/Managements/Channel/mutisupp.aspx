<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.channel.mutisupp"
    CodeBehind="mutisupp.aspx.cs" %>

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
            font-family: 宋体;
            font-size: 12px;
        }
         ;
        .style4
        {
            height: 24px;
        }
    </style>

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
                alert('至少选择一条记录');
                return false;
            }
            return true;
        }
        function checkAll() {
            if ($("#txtPassWord").val() == "") {
                alert('请输入二级密码');
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
            window.open("../User/UserEdit.aspx?id=" + id, "查看用户信息", "'height=700, width=1000, top=0, left=0, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=n o, status=no");
        }
    </script>

</head>
<body class="yui-skin-sam">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="1" class="table1">
        <tr>
            <td colspan="2" align="center" style="font-weight: bold; font-size: 14px; background-image: url('../style/images/topbg.gif');
                color: teal; background-repeat: repeat-x;" class="style4">
                销卡管理
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            </td>
            <td>
                <asp:Button ID="btnsave" runat="server" Text="保存设置" OnClick="btnsave_Click" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            </td>
            <td>
                通道类型：<asp:TextBox ID="txttypename" runat="server" Width="200px"></asp:TextBox>
                <asp:HiddenField ID="hftypeid" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                    <asp:Repeater ID="rptsupp" runat="server" OnItemCommand="rptsupp_ItemCommand" OnItemDataBound="rptsupp_ItemDataBound">
                        <HeaderTemplate>
                            <tr style="background-color: #507CD1; color: #fff; height: 22;">
                                <td>
                                    <input id="chkAll" type="checkbox">
                                </td>
                                <td>
                                    通道商代码
                                </td>
                                <td>
                                    通道商名称
                                </td>
                                <td>
                                    显示名称
                                </td>
                                <td>
                                    费率
                                </td>
                                <td>
                                    默认通道
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB">
                                <asp:HiddenField ID="hfsuppid" runat="server" Value='<%# Eval("code")%>' />
                                <td>
                                    <input id="chkItem" type="checkbox" runat="server"  name="chkItem" />
                                </td>
                                <td>
                                    <%# Eval("code")%>
                                </td>
                                <td>
                                    <%# Eval("name")%>
                                </td>
                                <td>
                                    <%# Eval("name1", "{0:f2}")%>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpayrate" runat="server" Width="80px" Text='<%# Eval("payrate","{0:f2}")%>'></asp:TextBox>%
                                </td>
                                 <td>
                                    <input id="chkisdefault" type="checkbox" runat="server"  name="chkisdefault" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff">
                                 <asp:HiddenField ID="hfsuppid" runat="server" Value='<%# Eval("code")%>' />
                                <td>
                                    <input id="chkItem" type="checkbox" runat="server"  name="chkItem" />
                                </td>
                                <td>
                                    <%# Eval("code")%>
                                </td>
                                <td>
                                    <%# Eval("name")%>
                                </td>
                                <td>
                                    <%# Eval("name1", "{0:f2}")%>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpayrate" runat="server" Width="80px" Text='<%# Eval("payrate","{0:f2}")%>'></asp:TextBox>%
                                </td>
                                 <td>
                                    <input id="chkisdefault" type="checkbox" runat="server"  name="chkisdefault" />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
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
