<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.business.User.UserIdImgLists" Codebehind="UserIdImgList.aspx.cs" %>

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
            window.open("UserImageCheck.aspx?id=" + id, "审核", "height=400,width=900");
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
                证件审核
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="StatusList" runat="server" EnableViewState="false">
                    <asp:ListItem Value="">—审核状态—</asp:ListItem>
                    <asp:ListItem Value="1">待审核</asp:ListItem>
                    <asp:ListItem Value="2">已审核</asp:ListItem>
                    <asp:ListItem Value="4">审核失败</asp:ListItem>
                </asp:DropDownList>
                用户ID：<asp:TextBox ID="txtUserId" runat="server" EnableViewState="false"></asp:TextBox>
                用户名：<asp:TextBox ID="txtUserName" runat="server" EnableViewState="false"></asp:TextBox>
                &nbsp&nbsp开始：
                <asp:TextBox ID="StimeBox" runat="server" Width="65px"></asp:TextBox>
                &nbsp&nbsp截止：
                <asp:TextBox ID="EtimeBox" runat="server" Width="65px"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" 查 询 " OnClick="btnSearch_Click">
                </asp:Button>
                <asp:Button ID="btnDelete" runat="server" CssClass="button" Text=" 删 除" OnClientClick="return Del_Confirm();"
                    OnClick="btnDelete_Click"></asp:Button>
                <asp:Button ID="btnCashTo" runat="server" CssClass="button" Text="审 核" OnClientClick="return GetMoney_Confirm();"
                    OnClick="btnCashTo_Click"></asp:Button>
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
                                    商户ID
                                </td>
                                <td>
                                    用户名
                                </td>
                                <td>
                                    真实名
                                </td>
                                <td>
                                    证件号码
                                </td>
                                <td>
                                    提交时间
                                </td>
                                <td>
                                    正面
                                </td>
                                <td>
                                    反面
                                </td>
                                <td>
                                    审核状态
                                </td>
                                <td>
                                    审核时间
                                </td>
                                <td>
                                    说 明
                                </td>
                                <td>
                                    审核人
                                </td>
                                <td>
                                    审 核
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB" ondblclick="javascript:sendInfo('<%# Eval("id")%>')">
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
                                    <%# Eval("payeeName")%>
                                </td>
                                <td>
                                    <%# Eval("IdCard")%>
                                </td>
                                <td>
                                    <%# Eval("addtime","{0:yyyy-MM-dd HH:ss:mm}")%>
                                </td>
                                <td>
                                    <a href="idimg.aspx?id=<%#Eval("id")%>&show=on" target="_blank">
                                        <img id="img1" src="idimg.aspx?id=<%#Eval("id")%>&show=on" width="180px" height="120px"
                                            style="border: 0" /></a>
                                </td>
                                <td>
                                    <a href="idimg.aspx?id=<%#Eval("id")%>&show=down" target="_blank">
                                        <img id="img2" src="idimg.aspx?id=<%#Eval("id")%>&show=down" width="180" height="120px"
                                            style="border: 0" /></a>
                                </td>
                                <td>
                                    <%# Enum.GetName(typeof(viviapi.Model.User.IdImageStatus),Eval("status"))%>
                                </td>
                                <td>
                                    <%# Eval("checktime","{0:yyyy-MM-dd HH:ss:mm}")%>
                                </td>
                                <td>
                                    <%# Eval("why")%>
                                </td>
                                <td>
                                    <%# Eval("admin")%>
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
                                    <%# Eval("userId")%>
                                </td>
                                <td>
                                    <a href='UserEdit.aspx?ID=<%# Eval("ID") %>'><strong>
                                        <%# Eval("userName")%>
                                    </strong></a>
                                </td>
                                <td>
                                    <%# Eval("payeeName")%>
                                </td>
                                <td>
                                    <%# Eval("IdCard")%>
                                </td>
                                <td>
                                    <%# Eval("addtime","{0:yyyy-MM-dd HH:ss:mm}")%>
                                </td>
                                <td>
                                    <a href="idimg.aspx?id=<%#Eval("id")%>&show=on" target="_blank">
                                        <img id="img1" src="idimg.aspx?id=<%#Eval("id")%>&show=on" width="180px" height="120px"
                                            style="border: 0" /></a>
                                </td>
                                <td>
                                    <a href="idimg.aspx?id=<%#Eval("id")%>&show=down" target="_blank">
                                        <img id="img2" src="idimg.aspx?id=<%#Eval("id")%>&show=down" width="180" height="120px"
                                            style="border: 0" /></a>
                                </td>
                                <td>
                                    <%# Enum.GetName(typeof(viviapi.Model.User.IdImageStatus),Eval("status"))%>
                                </td>
                                <td>
                                    <%# Eval("checktime","{0:yyyy-MM-dd HH:ss:mm}")%>
                                </td>
                                <td>
                                    <%# Eval("why")%>
                                </td>
                                <td>
                                    <%# Eval("admin")%>
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
                                AlwaysShow="True" CustomInfoHTML="总记录数：%RecordCount%&nbsp;总页数：%PageCount%&nbsp;当前页：%CurrentPageIndex%&nbsp;"
                                CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                                NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                                PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到" Width="100%" Height="30px">
                            </aspxc:AspNetPager>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span style="color: Red">操作说明：双击行弹出明细</span>
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
