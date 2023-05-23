<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.Console_CleanUpData" Codebehind="CleanUpData.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据清理 - 商户后台</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="style/union.css" type="text/css" rel="stylesheet" />

    <script src="../../js/ControlDate/WdatePicker.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellspacing="1" cellpadding="2" border="0" align="center">
            <tr align="left">
                <td align="center" colspan="3" style="font-weight: bold; font-size: 14px; background-image: url(Images/topbg.gif);
                    color: teal; background-repeat: repeat-x; height: 25px">
                    数据清理
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellpadding="3" cellspacing="1">
            <tr bgcolor="#F7F3F7">
                <td style="width: 15%; text-align: right">
                    业务类型：
                </td>
                <td>
                    <asp:CheckBoxList ID="cbl_clearType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="order">交易订单</asp:ListItem>
                        <asp:ListItem Value="settled">结算记录</asp:ListItem>
                        <asp:ListItem Value="stat">综合统计</asp:ListItem>                        
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr bgcolor="#F7F3F7">
                <td style="width: 15%; text-align: right">
                    交易类型：
                </td>
                <td>
                    <asp:CheckBoxList ID="cb_where" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="bank">网银交易</asp:ListItem>
                        <asp:ListItem Value="card">点卡交易</asp:ListItem>
                        <asp:ListItem Value="sms">短信交易</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr bgcolor="#F7F3F7">
                <td style="width: 15%; text-align: right">
                    状态选择：
                </td>
                <td>
                    <asp:CheckBoxList ID="cb_stat" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="2">已成功</asp:ListItem>
                        <asp:ListItem Value="1">处理中</asp:ListItem>
                        <asp:ListItem Value="4">失败</asp:ListItem>
                        <asp:ListItem Value="8">扣量</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr bgcolor="#F7F3F7">
                <td colspan="6">
                    数据清理后不可恢复，谨慎使用！
                </td>
            </tr>
            <tr bgcolor="#F7F3F7">
                <td style="width: 15%; text-align: right">
                    选择日期：
                </td>
                <td colspan="6">
                    <asp:TextBox ID="EtimeBox" runat="server" Width="80px"></asp:TextBox>
                    <span>清理这个日期之前的所有数据</span>
                </td>
            </tr>
            <tr bgcolor="#F7F3F7">
                <td style="width: 15%; text-align: right">
                    二级密码：
                </td>
                <td colspan="6" bgcolor="#F7F3F7">
                    <asp:TextBox ID="txtcaozuo" runat="server" Width="80px" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr bgcolor="#F7F3F7">
                <td style="width: 15%; text-align: right">
                    
                </td>
                <td >
                    <asp:Button ID="btnCleanUp" runat="server" Text=" 确认清理 " CssClass="button" OnClick="btndel_Click" />
                    <asp:Label ID="lbmsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
