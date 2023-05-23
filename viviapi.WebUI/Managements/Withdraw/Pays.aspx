<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Pays" Codebehind="Pays.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript">
        function sendInfo(id) {
            window.open("../User/UserEdit.aspx?id=" + id, "查看用户信息", "Width=800px;Height=350px;");
        }
    </script>
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
    <script type="text/javascript">
        function sendInfo(id) {
            window.open("../User/UserEdit.aspx?id=" + id, "查看用户信息", "Width=800px;Height=350px;");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; height: 100%;">
            <tr>
                <td align="center" class="headtitle">
                    付款管理
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                    商户ID：<asp:TextBox ID="txtUserId" runat="server" Width="80px"></asp:TextBox>
                    申请ID：<asp:TextBox ID="txtItemInfoId" runat="server" Width="80px"></asp:TextBox>
                    <asp:DropDownList ID="ddlbankName" runat="server">                        
                        <asp:ListItem value="">--收款银行--</asp:ListItem>
                        <asp:ListItem value="0002">支付宝</asp:ListItem>
                        <asp:ListItem value="0003">财付通</asp:ListItem>
                        <asp:ListItem value="1002">中国工商银行</asp:ListItem>
                        <asp:ListItem value="1005">中国农业银行</asp:ListItem>
                        <asp:ListItem value="1003">中国建设银行</asp:ListItem>
                        <asp:ListItem value="1026">中国银行</asp:ListItem>
                        <asp:ListItem value="1001">招商银行</asp:ListItem>
                        <asp:ListItem value="1006">民生银行</asp:ListItem>
                        <asp:ListItem value="1020">交通银行</asp:ListItem>
                        <asp:ListItem value="1025">华夏银行</asp:ListItem>
                        <asp:ListItem value="1009">兴业银行</asp:ListItem>
                        <asp:ListItem value="1027">广发银行</asp:ListItem>
                        <asp:ListItem value="1004">浦发银行</asp:ListItem>
                        <asp:ListItem value="1022">光大银行</asp:ListItem>
                        <asp:ListItem value="1021">中信银行</asp:ListItem>
                        <asp:ListItem value="1010">平安银行</asp:ListItem>
                        <asp:ListItem value="1066">中国邮政储蓄银行</asp:ListItem>
                    </asp:DropDownList>
                    收款账户：<asp:TextBox ID="txtAccount" runat="server" Width="80px"></asp:TextBox>
                    收款人：<asp:TextBox ID="txtpayeeName" runat="server" Width="80px"></asp:TextBox>                    
                    <asp:DropDownList ID="ddlmode" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSupplier" runat="server">
                    </asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" 查 询 " OnClick="btnSearch_Click">
                        </asp:Button>
                         <asp:RadioButtonList ID="rbl_export_mode" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="1">excel</asp:ListItem>
                    <asp:ListItem Value="2">txt</asp:ListItem>
                </asp:RadioButtonList>
                        <asp:Button ID="btnExport" runat="server" CssClass="button" Text="导出"
                            OnClick="btnExport_Click"></asp:Button>
                            
                             二级密码：<asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox>         
                <asp:Button ID="btnAllSettle" runat="server" CssClass="button" Text="批量支付" 
                    onclick="btnAllSettle_Click" OnClientClick="return checkAll();">
                </asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                        <asp:Repeater ID="rptList" runat="server">
                            <HeaderTemplate>
                                <tr height="22" style="background-color: #507CD1; color: #fff">
                                    <td style="width: 5%">
                                        序号
                                    </td>
                                    <td style="width: 8%">
                                        商户名
                                    </td>
                                    <td style="width: 8%">
                                        收款信息
                                    </td>                                    
                                    <td style="width: 8%">
                                        申请金额
                                    </td>
                                    <td style="width: 8%">
                                        手 续 费
                                    </td>
                                    <td style="width: 8%">
                                        实付金额
                                    </td>
                                    <td style="width: 8%">
                                        类型
                                    </td>
                                    <td style="width: 10%">
                                        申请时间
                                    </td>
                                    <td style="width: 15%">
                                        状态
                                    </td>
                                    <td>
                                        <input id="Checkboxall" type="checkbox" class="qx" onclick="checkall(this)" />
                                        全选
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="#EFF3FB">
                                    <td>
                                        <%# Eval("ID")%>
                                    </td>
                                    <td>
                                        <a href="javascript:sendInfo('<%# Eval("userid")%>')">
                                            <%#Eval("UserName")%>
                                        </a>
                                    </td>
                                    <td>
                                        <%# viviapi.BLL.SettledFactory.GetSettleBankName(Eval("PayeeBank").ToString())%> <%# Eval("Payeeaddress")%> <br />
                                        <%# Eval("payeeName")%> <br />
                                        <%# Eval("Account")%>
                                    </td>                                   
                                    <td style="text-align:right">
                                        <%# Eval("amount", "{0:f2}")%>
                                    </td>
                                    <td style="text-align:right">
                                        <%# Eval("Charges", "{0:f2}")%>
                                    </td>
                                    <td style="text-align:right">
                                        <%# (Convert.ToDecimal(Eval("amount")) - Convert.ToDecimal(Eval("Charges"))).ToString("f2")%>
                                    </td>
                                    <td>
                                        <%#Enum.GetName(typeof(viviapi.Model.SettledmodeEnum), Eval("settmode"))%>
                                    </td>
                                    <td>
                                        <%# Eval("AddTime","{0:yyyy-MM-dd HH:mm:ss}") %>
                                    </td>
                                    <td>
                                        <%# Eval("StatusText")%>
                                    </td>
                                    <td>
                                        <input id="<%# Eval("ID") %>" type="checkbox" name="ischecked" class="qx" value="<%# Eval("ID") %>" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <tr>
                        <td colspan="20">
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
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
