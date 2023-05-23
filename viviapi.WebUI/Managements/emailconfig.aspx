<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.emailconfig" ValidateRequest="false" Codebehind="emailconfig.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>站点设置</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="style/admin.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
table { FONT-WEIGHT:normal;line-height:170%;FONT-FAMILY:Arial}
A:link {COLOR:#237C04;TEXT-DECORATION: none}
td {height:20px; line-height:20px; font-size:12px;padding:0px; }
.td_title,th {height:20px;line-height:22px;font-weight:bold;border:0px solid #fff;text-align:left;}
.td1 {padding-right:3px;padding-left:3px;color:#999999;padding-bottom:0px;padding-top:5px;height:25px;}
.td2 {padding-right:3px;padding-left:8px;padding-top:5px;color:#083772;background:#EFF3FB;font-size:12px;text-align:right; width:35%}
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
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" style="font-weight: bold; font-size: 14px; background: url(style/images/topbg.gif) repeat-x;
                    color: teal; height: 28px">
                    邮件设置</td>
            </tr>
            <tr>
                <td width="10%" class="td2">
                    发送邮件服务器：</td>
                <td width="90%" colspan="3" class="td1">
                    <asp:TextBox ID="txtEmailServerAddress" runat="server" Width="227px"></asp:TextBox>邮件服务器地址 格式如: smtp.gmail.com</td>
            </tr>
            <tr>
                <td class="td2">
                    端口：</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtEmailServerAddressPort" runat="server" Width="50px"></asp:TextBox>如：端口 465
                    </td>
            </tr>
            <tr>
                <td class="td2">
                    邮件的地址名称：</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtServerUserName" runat="server" Width="227px"></asp:TextBox>
                    格式如: ggp@gmail.com</td>
            </tr>
            <tr>
                <td class="td2">
                    密码：</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtServerUserPass" runat="server" TextMode="Password"  Width="227px"></asp:TextBox>
                    格式如: ggp@gmail.com</td>
            </tr>
            <tr>
                <td class="td2">
                    显示邮件来自：</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtmailfrom" runat="server"   Width="227px"></asp:TextBox>
                    格式如: ttpay@service.com</td>
            </tr>
            <tr>
                <td class="td2">
                    显示名称：</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtMailDisplayName" runat="server"   Width="227px"></asp:TextBox>
                    格式如: 天天支付平台客服</td>
            </tr>
             <tr>
                <td class="td2">
                    是否Ssl连接：</td>
                <td colspan="3" class="td1">
                    <asp:CheckBox ID="ckb_ssl" runat="server" Text="是否Ssl连接" /></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="td1" colspan="3">
                    <span style="padding-left: 3px; height: 40px">
                        <asp:Button ID="btn_Update" runat="server" Text="确认更新" OnClick="btnUpdate_Click" OnClientClick="allQQ()" />
                    </span>
                </td>
            </tr>
        </table>
        
        <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" style="font-weight: bold; font-size: 14px; background: url(style/images/topbg.gif) repeat-x;
                    color: teal; height: 28px">
                    邮件测试</td>
            </tr>
            <tr>
                <td class="td2">
                    收信邮箱：</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtmailto" runat="server"   Width="227px">2601401814@qq.com</asp:TextBox>
                    </td>
            </tr>
             <tr>
                <td class="td2">
                    内容：</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="4"  Width="600px">test</asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="td1" colspan="3">
                    <span style="padding-left: 3px; height: 40px">
                        <asp:Button ID="btnSend" runat="server" Text="发送" 
                        onclick="btnSend_Click"  />
                    </span>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
