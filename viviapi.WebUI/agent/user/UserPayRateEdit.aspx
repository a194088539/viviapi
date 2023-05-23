<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.web.Agent.User.UserPayRateEdit"
    CodeBehind="UserPayRateEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        .td1a
        {
            padding-right: 3px;
            padding-left: 3px;
            color: #999999;
            padding-bottom: 0px;
            padding-top: 5px;
            height: 25px;
        }
        .td2a
        {
            padding-right: 3px;
            padding-left: 8px;
            padding-top: 5px;
            color: #083772;
            background: #EFF3FB;
            font-size: 12px;
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td colspan="10">
                用户ID:<asp:Label ID="lblUserId" runat="server" Text="Label" Font-Bold="True" ForeColor="Red"></asp:Label>
                <asp:CheckBox ID="ckb_isopen" runat="server" Visible="false" Text="启用单独配置" />
                &nbsp;<asp:Button ID="btnCopy" runat="server" Visible="false"  Text="我的费率" OnClick="btnCopy_Click" />
            </td>
        </tr>
        <tr>
            <td class="td2a">
                财付通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp100" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                支付宝：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp101" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                网上银行：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp102" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                微信：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp99" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td>
            <td class="td2a">
                QQ钱包：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp98" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td>
        </tr>
        <tr>
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
            <td>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                支付宝APP：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp95" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                网银APP：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp96" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td2a">
                微信APP：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp94" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td1a">
            </td>
        </tr>
        <tr>
            <td class="td2a">
                QQ钱包APP：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp93" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td1a">
            </td>
            <td class="td2a">
                京东钱包APP：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp92" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="td1a">
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

        <tr style="display:none;">
            <td colspan="2" style="text-align: center; height: 30px;">
                <asp:Button ID="btnSave" runat="server" Text="保 存" OnClick="btnSave_Click"></asp:Button>
                <input type="button" value="返 回" onclick="backreturn()" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="1" cellpadding="0" style="display:none;">
        <tr>
            <td align="center" colspan="3" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 28px">
                费率设置范围
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0" style="display:none;">
        <tr>
            <td class="td2a" style="width: 9%">
            </td>
            <td class="td1a" style="width: 8%">
                最低
            </td>
            <td class="td1a" style="width: 8%">
                最高
            </td>
            <td class="td2a" style="width: 9%">
            </td>
            <td class="td1a" style="width: 8%">
                最低
            </td>
            <td class="td1a" style="width: 8%">
                最高
            </td>
            <td class="td2a" style="width: 9%">
            </td>
            <td class="td1a" style="width: 8%">
                最低
            </td>
            <td class="td1a" style="width: 8%">
                最高
            </td>
            <td class="td2a" style="width: 9%">
            </td>
            <td class="td1a" style="width: 8%">
                最低
            </td>
            <td class="td1a" style="width: 8%">
                最高
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
                <asp:TextBox ID="txtp1030" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1031" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                神州行浙江卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2000" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2001" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                神州行江苏卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2010" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2011" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                神州行辽宁卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2020" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2021" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                神州行福建卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2030" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2031" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                盛大一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1040" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1041" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                征途支付卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1050" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1051" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                骏网一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1060" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1061" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                腾讯Q币卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1070" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1071" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                联通充值卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1080" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1081" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                久游一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1090" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1091" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                网易一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1100" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1101" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                完美一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1110" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1111" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                搜狐一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1120" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1121" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                电信充值卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1130" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1131" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                声讯卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1140" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1141" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                光宇一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1150" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1151" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                金山一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1160" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1161" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                纵游一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1170" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1171" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                天下一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1180" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1181" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                天宏一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1190" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp1191" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                魔兽卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2040" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2041" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                联华卡：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2050" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2051" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                短信：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp900" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp901" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2a">
                天下一卡通专项：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2090" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2091" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
                殴飞一卡通：
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2080" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td1a">
                <asp:TextBox ID="txtp2081" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td2a">
            </td>
            <td class="td1a">
            </td>
            <td class="td1a">
            </td>
            <td class="td2a">
            </td>
            <td class="td1a">
            </td>
            <td class="td1a">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
