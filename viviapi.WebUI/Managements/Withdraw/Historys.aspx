<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Historys"
    CodeBehind="Historys.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript">
        function Setchkall(obj) {
            var objs = document.getElementsByName("chk");
            for (i = 0; i < objs.length; i++) {
                objs[i].checked = obj.checked;
            }
        }
        function checkall(obj) {
            var check = document.getElementsByName("ischecked");
            for (i = 0; i < check.length; i++) {
                check[i].checked = obj.checked;
            }
        }
    </script>

    <script type="text/javascript">
        function sendInfo(id) {
            window.open("../User/UserEdit.aspx?id=" + id, "查看用户信息", "Width=800px;Height=350px;");
        }
    </script>

    <style type="text/css">
        table
        {
            font-weight: normal;
            font-size: 12px;
            line-height: 170%;
            font-family: Arial;
        }
        td
        {
            height: 11px;
        }
        A:link
        {
            color: #02418a;
            text-decoration: none;
        }
    </style>

    <script src="../../Js/ControlDate/WdatePicker.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; height: 100%;">
            <tr>
                <td align="center" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                    color: teal; background-repeat: repeat-x; height: 28px">
                    结算记录
                </td>
            </tr>
            <tr>
                <td>
                    <span style="float: left; margin-left: 2px">
                        <asp:DropDownList ID="ddlStatusList" runat="server">
                        </asp:DropDownList>
                        商户ID：<asp:TextBox ID="txtUserId" runat="server" Width="80px"></asp:TextBox>
                        申请ID：<asp:TextBox ID="txtItemInfoId" runat="server" Width="80px"></asp:TextBox>
                        <asp:DropDownList ID="ddlbankName" runat="server">
                            <asp:ListItem Value="">--收款银行--</asp:ListItem>
                            <asp:ListItem Value="0002">支付宝</asp:ListItem>
                            <asp:ListItem Value="0003">财付通</asp:ListItem>
                            <asp:ListItem Value="1002">中国工商银行</asp:ListItem>
                            <asp:ListItem Value="1005">中国农业银行</asp:ListItem>
                            <asp:ListItem Value="1003">中国建设银行</asp:ListItem>
                            <asp:ListItem Value="1026">中国银行</asp:ListItem>
                            <asp:ListItem Value="1001">招商银行</asp:ListItem>
                            <asp:ListItem Value="1006">民生银行</asp:ListItem>
                            <asp:ListItem Value="1020">交通银行</asp:ListItem>
                            <asp:ListItem Value="1025">华夏银行</asp:ListItem>
                            <asp:ListItem Value="1009">兴业银行</asp:ListItem>
                            <asp:ListItem Value="1027">广发银行</asp:ListItem>
                            <asp:ListItem Value="1004">浦发银行</asp:ListItem>
                            <asp:ListItem Value="1022">光大银行</asp:ListItem>
                            <asp:ListItem Value="1021">中信银行</asp:ListItem>
                            <asp:ListItem Value="1010">平安银行</asp:ListItem>
                            <asp:ListItem Value="1066">中国邮政储蓄银行</asp:ListItem>
                        </asp:DropDownList>
                        收款账户：<asp:TextBox ID="txtAccount" runat="server" Width="80px"></asp:TextBox>
                        收款人：<asp:TextBox ID="txtpayeeName" runat="server" Width="80px"></asp:TextBox>
                        <asp:DropDownList ID="ddlmode" runat="server">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSupplier" runat="server">
                        </asp:DropDownList>
                        开始：
                        <asp:TextBox ID="txtStimeBox" runat="server" Width="65px"></asp:TextBox>
                        截止：
                        <asp:TextBox ID="txtEtimeBox" runat="server" Width="65px"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" 查 询 " OnClick="btnSearch_Click">
                        </asp:Button>
                        <asp:Button ID="btnExport" runat="server" CssClass="button" Text="导出" OnClick="btnExport_Click"/>
                        </span>
                </td>
            </tr>
            <tr>
                <td>
                    结算总额：<%=TotalMoney %>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                        <asp:Repeater ID="rptList" runat="server">
                            <HeaderTemplate>
                                <tr style="background-color: #507CD1; color: #fff; height: 22px">
                                    <td>
                                        序号
                                    </td>
                                    <td>
                                        商户
                                    </td>
                                    <td>
                                        收款信息
                                    </td>
                                    <td>
                                        申请金额
                                    </td>
                                    <td>
                                        实付金额
                                    </td>
                                    <td>
                                        扣税
                                    </td>
                                    <td>
                                        手续费
                                    </td>
                                    <td>
                                        结算时间
                                    </td>
                                    <td>
                                        状态
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="#EFF3FB">
                                    <td>
                                        <%# Eval("ID")%>
                                    </td>
                                    <td>
                                        <a href="?action=paylistbyid&userid=<%#Eval("userid")%>">
                                            <%#Eval("UserName")%>(#<%#Eval("userid")%>)
                                        </a>
                                    </td>
                                    <td>
                                        <%# viviapi.BLL.SettledFactory.GetSettleBankName(Eval("PayeeBank").ToString())%> <%# Eval("Payeeaddress")%> <br />
                                        <%# Eval("payeeName")%> <br />
                                        <%# Eval("Account")%>                                      
                                    </td>
                                    <td>
                                        <%# Eval("amount","{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# (Convert.ToDecimal(Eval("amount")) - Convert.ToDecimal(Eval("Charges"))).ToString("f2")%>
                                    </td>
                                    <td>
                                        <%# Eval("Tax", "{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# Eval("Charges", "{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# Eval("PayTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%# Eval("StatusText")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                    <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CustomInfoHTML="总记录数：%RecordCount%&nbsp;总页数：%PageCount%&nbsp;当前页：%CurrentPageIndex%&nbsp;"
                        CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                        NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                        PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                        ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                        TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到" Width="100%" Height="30px"
                        OnPageChanged="Pager1_PageChanged">
                    </aspxc:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
