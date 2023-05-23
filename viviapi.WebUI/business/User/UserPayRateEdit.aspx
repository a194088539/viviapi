<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.business.User.UserPayRateEdit" CodeBehind="UserPayRateEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>费率编辑</title>
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <link href="../style/page.css" type="text/css" rel="stylesheet" />
    <script src="../../js/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function backreturn() {
            location.href = 'UserList.aspx?UserStatus=2';
        }
    </script>
<style>
    .td1a {padding-right:3px;padding-left:3px;color:#999999;padding-bottom:0px;padding-top:5px;height:25px;}
    .td2a {padding-right:3px;padding-left:8px;padding-top:5px;color:#083772;background:#EFF3FB;font-size:12px;text-align:right; }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td colspan="10">                    
                   用户ID:<asp:Label ID="lblUserId" runat="server" Text="Label" 
                        Font-Bold="True" ForeColor="Red"></asp:Label>
                        
                    <asp:CheckBox ID="ckb_isopen" runat="server" Text="启用单独配置" />
                    
                    
                &nbsp;<asp:Button ID="btnCopy" runat="server" Text="复制默认比例" 
                        onclick="btnCopy_Click" />
                </td>
            </tr>
        <tr>
            <td class="td2a">
                网上银行：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp102" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                支付宝：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp101" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                财付通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp100" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                微信：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp99" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                QQ钱包：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp98" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                京东钱包：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp97" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                银联钱包：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp91" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                手机支付宝：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp95" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                手机网银：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp96" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                手机微信：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp94" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                手机QQ：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp93" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                手机京东：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp92" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                微信H5：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp89" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                百度钱包：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp88" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                支付宝H5：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp87" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                QQ钱包H5：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp86" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                神州行充值卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp103" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                神州行浙江卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp200" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                神州行江苏卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp201" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                神州行辽宁卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp202" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                神州行福建卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp203" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                盛大一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp104" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                征途支付卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp105" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                骏网一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp106" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                腾讯Q币卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp107" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                联通充值卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp108" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                久游一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp109" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                网易一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp110" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                完美一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp111" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                搜狐一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp112" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                电信充值卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp113" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                声讯卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp114" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                光宇一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp115" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                金山一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp116" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                纵游一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp117" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                天下一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp118" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                天宏一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp119" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                魔兽卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp204" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                联华卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp205" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                短信：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp90" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                天下一卡通专项：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp209" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                殴飞一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp208" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                盛付通卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp210" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
            <tr>
                <td colspan="2" style="text-align: center; height: 30px;">
                    <asp:Button ID="btnSave" runat="server" Text="保 存" OnClick="btnSave_Click"></asp:Button>
                    <input type="button" value="返 回" onclick="backreturn()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
