<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GwCahceManage.aspx.cs" Inherits="viviapi.WebUI.LongBao.console.GwCahceManage1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="style/union.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellspacing="1" cellpadding="0" style="width: 100%; height: 100%;border: #c9ddf0 1px solid; background-color: White;" id="table_zyads">
                <tr>
                    <td align="center" colspan="2" style="font-weight: bold; font-size: 14px; background-image: url(style/images/topbg.gif);color: teal; background-repeat: repeat-x; height: 28px">
                        网关缓存管理
                </tr>
                <tr>
                    <td align="center">
                        <table style="border-right: #c9ddf0 1px solid; border-top: #c9ddf0 1px solid; border-left: #c9ddf0 1px solid;
                            border-bottom: #c9ddf0 1px solid" cellspacing="0" cellpadding="0" width="100%"
                            bgcolor="#f3f9fe" border="0">
                            <tr>
                                <td>
                                    <table id="setpsd" style="margin-bottom: 5px" cellspacing="1" cellpadding="3" width="100%">
                                        <tr>
                                            <td style="width:25%;text-align:right;">
                                                网关地址：
                                            </td>
                                            <td style="width:75%;text-align:left;">
                                                <asp:TextBox ID="txtGwUrl" runat="server" Width="80%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:25%;text-align:right;">
                                                选择大类：
                                            </td>
                                            <td style="width:75%;text-align:left;">
                                                 <asp:DropDownList ID="ddlcachetype" runat="server" AutoPostBack="True" 
                                                     onselectedindexchanged="ddlcachetype_SelectedIndexChanged" >
                                                    <asp:ListItem Value="CHANNELS">支付通道</asp:ListItem>
                                                    <asp:ListItem Value="CHANNEL_TYPE">支付通道类别</asp:ListItem>
                                                    <asp:ListItem Value="CHANNEL_TYPE_USER_">用户支付通道设置</asp:ListItem>
                                                    <asp:ListItem Value="NEWS">新闻</asp:ListItem>
                                                    <asp:ListItem Value="SUPPLIER_">接口商</asp:ListItem>
                                                    <asp:ListItem Value="SUPPPAYRATE">供应商费率</asp:ListItem>
                                                    <asp:ListItem Value="USER_">用户缓存</asp:ListItem>
                                                    <asp:ListItem Value="USERHOST_">用户来路网站列表</asp:ListItem>
                                                    <asp:ListItem Value="Question">安全问题列表</asp:ListItem>
                                                    <asp:ListItem Value="WEBINFO_">网站设置</asp:ListItem>
                                                    <asp:ListItem Value="SYSCONFIG">配置信息</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlsubcache" runat="server"  ></asp:DropDownList>
                                                </td>
                                        </tr>
                                         <tr>
                                            <td style="width:25%;text-align:right;">
                                                
                                            </td>
                                            <td style="width:75%;text-align:left;">
                                                <asp:Button ID="BtnRemove" runat="server" Text="删除" onclick="BtnRemove_Click" />
                                            </td>
                                        </tr>
                                        
                                    </table>
                                </td>                               
                            </tr>                            
                        </table>
                        </td>
                        </tr>
                        </table>
    </form>
</body>
</html>
