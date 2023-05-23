<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Order.DebugInfos" Codebehind="DebugInfos.aspx.cs" %>

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
        function sendMsg(uid) {
            window.showModelessDialog("SendMsg.aspx?uid=" + uid, window, "dialogWidth=800px;dialogHeight=500px;");
        }
        function sendInfo(id) {
            window.open("../User/UserEdit.aspx?id=" + id, "查看用户信息", "Width=800px;Height=350px;");
        }
        function showInfo(id) {
            window.open("DebugInfoShow.aspx?id=" + id, "查看详细信息", "Width=800px;Height=350px;");
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
                交易调试日志
            </td>
        </tr>
        <tr>
            <td>
                用户订单号
                <asp:TextBox ID="txtuserOrder" runat="server"></asp:TextBox>
                用户ID
                <asp:TextBox ID="txtuserId" runat="server"></asp:TextBox>
                开始：
                <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                截止：
                <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" 查 询 " OnClick="btnSearch_Click">
                </asp:Button>
                <asp:Button ID="btnDel" runat="server" CssClass="button" Text=" 删 除 " />
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                    <asp:Repeater ID="rptTrades" EnableViewState="false" runat="server" OnItemDataBound="rptUsersItemDataBound">
                        <HeaderTemplate>
                            <tr style="background-color: #507CD1; color: #fff; height: 22;">
                                <td style="width:5%">
                                    <input id="chkAll" type="checkbox">
                                </td>
                                <td style="width:10%">
                                    交易号
                                </td>
                                <td style="width:10%">
                                    商户ID
                                </td>
                                <td style="width:10%">
                                    用户名
                                </td>
                                <td style="width:10%">
                                    类型
                                </td>
                                <td style="width:25%">
                                    提交地址
                                </td>
                                <td style="width:10%"> 
                                    错误代号
                                </td>
                                <td style="width:10%">
                                    错误信号
                                </td>
                                <td style="width:10%">
                                    时间
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB" ondblclick="javascript:showInfo('<%# Eval("id")%>')">
                                <td>
                                    <input id="chkItem" type="checkbox" runat="server" value='<%#Eval("id")%>' name="chkItem" />
                                </td>
                                <td>
                                    <%# Eval("userorder")%>
                                </td>
                                <td>
                                    <%# Eval("userid")%>
                                </td>
                                <td>
                                    <a href="javascript:sendInfo('<%# Eval("userid")%>')">
                                        <%#Eval("UserName")%>
                                    </a>
                                </td>
                                <td>
                                    <%#Enum.GetName(typeof(viviapi.Model.Sys.debugtypeenum), Eval("bugtype"))%>
                                </td>
                                <td style="word-break:break-all">
                                    <%# Eval("url")%>
                                </td>
                                <td>
                                    <%# Eval("errorcode")%>
                                </td>
                                <td>
                                    <%# Eval("errorinfo")%>
                                </td>
                                <td>
                                    <%# Eval("addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff">
                                <tr style="background-color: #EFF3FB" ondblclick="javascript:showInfo('<%# Eval("id")%>')">
                                    <td>
                                        <input id="chkItem" type="checkbox" runat="server" value='<%#Eval("id")%>' name="chkItem" />
                                    </td>
                                    <td>
                                        <%# Eval("userorder")%>
                                    </td>
                                    <td>
                                        <%# Eval("userid")%>
                                    </td>
                                    <td>
                                        <a href="javascript:sendInfo('<%# Eval("userid")%>')">
                                            <%#Eval("UserName")%>
                                        </a>
                                    </td>
                                    <td>
                                        <%#Enum.GetName(typeof(viviapi.Model.Sys.debugtypeenum), Eval("bugtype"))%>
                                    </td>
                                    <td style="word-break:break-all">
                                        <%# Eval("url")%>
                                    </td>
                                    <td>
                                        <%# Eval("errorcode")%>
                                    </td>
                                    <td>
                                        <%# Eval("errorinfo")%>
                                    </td>
                                    <td>
                                        <%# Eval("addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #EFEFEF">
                        <td style="height: 16px;">
                            <aspxc:AspNetPager ID="Pager1" runat="server" OnPageChanging="Pager1_PageChanging"
                                AlwaysShow="True" CustomInfoHTML="总记录数：%RecordCount%&nbsp;总页数：%PageCount%&nbsp;当前页：%CurrentPageIndex%&nbsp;"
                                CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                                NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                                PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到" Width="100%" Height="30px">
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
