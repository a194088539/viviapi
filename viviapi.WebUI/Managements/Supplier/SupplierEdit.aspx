<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.SupplierEdit" Codebehind="SupplierEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建编辑供应商</title>
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
table { FONT-WEIGHT:normal;line-height:170%;FONT-FAMILY:Arial}
A:link {COLOR:#237C04;TEXT-DECORATION: none}
td {height:20px; line-height:20px; font-size:12px;padding:0px; }
.td_title,th {height:20px;line-height:22px;font-weight:bold;border:0px solid #fff;text-align:left;}
.td1 {padding-right:3px;padding-left:3px;color:#999999;padding-bottom:0px;padding-top:5px;height:25px;}
.td2 {padding-right:3px;padding-left:8px;padding-top:5px;color:#083772;background:#EFF3FB;font-size:12px;text-align:right;}
.td3 {padding:1px 1px 0 0px;color:#083772;background:#EFF3FB;font-size:12px;text-align:center;}
.moban {padding-top:0px;border:0px}
input { border:1px solid #999;padding:3px;margin-left:10px;font:12px tahoma;ling-height:16px}
.input4 {border:1px solid #999;padding:3px;margin-left:10px;font:11px tahoma;ling-height:16px;height:45px;}
.button {color: #135294; border:1px solid #666; height:21px; line-height:21px;}
.nrml{background-color:#eeeeee;font-weight: bold;}
.radio { border:none; }
.checkbox { border:none; }
.addnew {font-size: 12px;color: #FF0000;}
a.servername{height:470px;width: 527px;color:#E54202;cursor:hand;}
.current {border:#ff6600 1px solid;}
a:hover {height:470px;width: 527px;color:#E54202;cursor:hand;}
#nav LI A.noncurrent {/*border:#DC171E 3px solid;*/}
#nav UL {PADDING-BOTTOM: 0px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 0px}
#nav LI {DISPLAY: inline; padding-left:10px;}
#nav LI a:hover {border:#B6E000 1px solid;}
#nav li A:visited {border:#ff0000 1px solid;}
img{border:#CCCCCC 1px solid;padding:0 5px}
#tplPreview {
position: absolute;
top:0px;
left:0px;
background:#ffffff;
border:1px solid #333;
font-size:12px;
color:#4B4B4B;
padding:12px 15px 15px 15px;
}
</style>

    <script type="text/javascript">
function backreturn(){
    location.href='SupplierList.aspx';
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td class="td2">
                        供应商编号：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtcode" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        供应商名称：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtname" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        网址地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpurl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
<!--
                <tr>
                    <td class="td2">
                        Logo图片：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtlogourl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
-->
                <tr>
                    <td class="td2">
                        支持种类：</td>
                    <td class="td1">
                        <asp:CheckBox ID="chkisbank" Text="在线" runat="server" Checked="True" />
                        <asp:CheckBox ID="chkiscard" Text="卡类" runat="server" Checked="True" />
                        <asp:CheckBox ID="chkissms" Text="短信" runat="server" Checked="True" />
                        <asp:CheckBox ID="chkissx" Text="声讯" runat="server" Checked="True" />
                        <asp:CheckBox ID="chkisdistribution" Text="代付" runat="server" Checked="True" />
                        <asp:CheckBox ID="chkiwap" Text="手机网银" runat="server" Checked="True" />
                        <asp:CheckBox ID="chkiali" Text="手机支付宝" runat="server" Checked="True" />
                        <asp:CheckBox ID="chkiwx" Text="手机微信" runat="server" Checked="True" />
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        账号：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserid" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        密钥：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserkey" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        用户名称：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpusername" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        账号1：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserid1" runat="server" Width="50%"></asp:TextBox>
                        快钱 神州行账号
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        密钥1：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserkey1" runat="server" Width="50%"></asp:TextBox>
                        快钱 神州行密钥
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        账号2：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserid2" runat="server" Width="50%"></asp:TextBox>
                          快钱 联通充值卡账号
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        密钥2：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserkey2" runat="server" Width="50%"></asp:TextBox>
                          快钱 联通充值卡账号
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        账号3：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserid3" runat="server" Width="50%"></asp:TextBox>
                        快钱 电信账号
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        密钥3：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserkey3" runat="server" Width="50%"></asp:TextBox>
                        快钱  电信密钥
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        账号4：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserid4" runat="server" Width="50%"></asp:TextBox> 快钱 骏网一卡通账号
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        密钥4：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserkey4" runat="server" Width="50%"></asp:TextBox> 快钱 骏网一卡通密钥
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        账号5：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserid5" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        密钥5：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpuserkey5" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        网址返回地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpbakurl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        卡类返回地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtCardbakUrl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        中转域名：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtJumpUrl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        网银提交地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpostBankUrl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        卡类提交地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpostCardUrl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="td2">
                        卡类查询地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtQueryCardUrl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        短信息提交地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtpostSMSUrl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        代付款提交地址：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtdistributionUrl" runat="server" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        说明：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtdesc" runat="server" Width="400px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        排序：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtsort" runat="server" Width="50%">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        是否系统：</td>
                    <td class="td1">
                        <asp:CheckBox ID="chkissys" Text="issys" runat="server" Checked="False" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; height: 30px;">
                        <asp:Button ID="btnSave" runat="server" Text="保 存" OnClick="btnSave_Click">
                        </asp:Button>
                        <input type="button" value="返 回" onclick="backreturn()" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
