<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.APIConfig" Codebehind="APIConfig.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>支付通道配置</title>
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
input {BACKGROUND:#FAFAFA;border:#bdc5ca 1px solid;padding:2px;font-size:11px;font-family:tahoma,}
td{height:18px}
#logoup{padding:0px;margin-top:5px;background:#F8F8F8}
#logoup p{height:22px;padding:2px}
.style4 {font-weight:bold}
.tablegg {border:1px solid #D5DCE6;}
#keysj{border:solid 1px #e5e5e5}
#keysj .keybg{background:#f1f1f1;border:solid 1px #FFF}
#keysj h1{width:150px;font-size:14px;padding:5px 5px 5px 25px}
#list{margin:auto;width:100%;padding:5px 0px;border-bottom:#fff solid 1px}
#list span{width:80px;float:right;color:#666;text-align:left;padding:2px}
#list img{border:1px dotted #DADADA}
#list dl{float:left}
#list dt.na{color:#084173;font-size:12px;text-align:left}
#list .keylogo{width:130px;float:left;padding:0px 25px}
.cvlink {display: inline-table;display: -moz-inline-box;display: inline-block;margin:1px;border-style:solid;border-width: 1px;border-color: #999999;border-top-color: #cccccc;border-left-color:#cccccc;background:#eeeeee;color:#333333;font-family:Verdana, Arial, Helvetica, sans-serif;font-size: 100%;white-space: nowrap; height:22px;line-height:22px;padding:0 4px;}
#GVSupplier:VISITED{text-decoration:none;color:#333;background:#eeeeee}
#GVSupplier:ACTIVE{text-decoration:none;color:#333}
#GVSupplier:HOVER{color:#666;background:#f9f9f9}</style>
</head>
<body>
    <form id="form1" runat="server">
   <div class="rtop">
                <div   style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                        color: teal; background-repeat: repeat-x; height: 28px">
                    通道接口设置</div>
                <div style="width: 80%">
                    通道接口管理:开启、锁定支付接口,编辑接口ID、密钥</div>
            </div>
            <table width="100%" border="0" align="center" cellpadding="3" cellspacing="1" bgcolor="#f9f9f9"
                class="tablegg">
                <tr>
                    <td width="120" align="right" style="height: 32px">
                        默认卡类通道：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddlisurl" runat="server">
                        </asp:DropDownList>
                        <asp:Button ID="purlok" runat="server" CssClass="button" Text=" 提 交 " OnClick="purlok_Click" /></td>
                </tr>
                 <tr>
                    <td width="120" align="right" style="height: 32px">
                        神州行充值卡：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddlszx" runat="server">                           
                        </asp:DropDownList>
                        </td>
                </tr>
                 <tr>
                    <td width="120" align="right" style="height: 32px">
                        盛大一卡通：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddlsd" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                 <tr>
                    <td width="120" align="right" style="height: 32px">
                        征途支付卡：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddlzt" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                 <tr>
                    <td width="120" align="right" style="height: 32px">
                        骏网一卡通：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddljw" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                 <tr>
                    <td width="120" align="right" style="height: 32px">
                        腾讯Q币卡：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddlqq" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                 <tr>
                    <td width="120" align="right" style="height: 32px">
                        &nbsp;联通充值卡：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddllt" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                 <tr>
                    <td width="120" align="right" style="height: 32px">
                        久游一卡通：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddljy" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                 <tr>
                    <td width="120" align="right" style="height: 32px">
                        网易一卡通：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddlwy" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr>
                    <td width="120" align="right" style="height: 32px">
                        完美一卡通：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddlwm" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr>
                    <td width="120" align="right" style="height: 32px">
                        搜狐一卡通：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddlsh" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr>
                    <td width="120" align="right" style="height: 32px">
                        电信充值卡：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddldx" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr>
                    <td width="120" align="right" style="height: 32px">
                        光宇一卡通：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddlonline" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                 <tr>
                    <td width="120" align="right" style="height: 32px">
                        金山一卡通：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddlking" runat="server">                            
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr>
                    <td width="120" align="right" style="height: 32px">
                        魔兽卡：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddlmoko" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr>
                    <td width="120" align="right" style="height: 32px">
                        5173卡：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddl5173" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr>
                    <td width="120" align="right" style="height: 32px">
                        热血卡：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddlrxk" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                 <tr>
                    <td width="120" align="right" style="height: 32px">
                        短信：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddlsms" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr>
                    <td width="120" align="right" style="height: 32px">
                        声讯卡：</td>
                    <td style="height: 32px">
                    <asp:DropDownList ID="ddlsxk" runat="server">
                        </asp:DropDownList>
                        </td>
                </tr>
                <tr>
                    <td align="right" style="height: 32px" width="120">
                        网银：</td>
                    <td style="height: 32px">
                        <asp:DropDownList ID="ddlbankurl" runat="server">                           
                        </asp:DropDownList></td>
                </tr>
            </table>
    </form>
</body>
</html>
