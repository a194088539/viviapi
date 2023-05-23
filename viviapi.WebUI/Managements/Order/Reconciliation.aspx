<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.Reconciliation" ValidateRequest="false" Codebehind="Reconciliation.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>对账查询</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
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
                    对账查询</td>
            </tr> 
            <tr>
                <td class="td2">
                    接口商：</td>
                <td colspan="3" class="td1">
                    <asp:DropDownList ID="ddlsupp" runat="server">
                        <asp:ListItem Value="80">欧飞</asp:ListItem>
                        <asp:ListItem Value="60866">60866</asp:ListItem>
                        <asp:ListItem Value="81">汇元</asp:ListItem>
                        <asp:ListItem Value="70">70card</asp:ListItem>
                        <asp:ListItem Value="700">龙宝</asp:ListItem>
                        <asp:ListItem Value="86">神州付</asp:ListItem>
                    </asp:DropDownList>
                    </td>
            </tr>            
            <tr>
                <td class="td2">
                    订单号：</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtorders" runat="server" Width="600px" TextMode="MultiLine" Height="200px"></asp:TextBox></td>
            </tr>              
            <tr>
                <td>
                    &nbsp;</td>
                <td class="td1" colspan="3">
                    <span style="padding-left: 3px; height: 40px">
                        <asp:Button ID="btn_search" runat="server" Text="提交查询" 
                        onclick="btn_search_Click"/>
                    </span>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="1" cellpadding="0" class="table1">
       
        <tr>
            <td bgcolor="#ffffff">                
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="center">
                            <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                                <asp:Repeater ID="rptOrders" runat="server" >
                                    <HeaderTemplate>
                                        <tr height="22" style="background-color: #507CD1; color: #fff">
                                            <td>
                                                订单号
                                            </td>
                                            <td>
                                                流水号
                                            </td>
                                            <td>
                                                支付金额
                                            </td>
                                            <td>
                                                查询结果
                                            </td>
                                            <td>
                                                支付状态
                                            </td>
                                            <td>
                                                交易币种
                                            </td>
                                            <td>
                                                卡种
                                            </td>                                           
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr bgcolor="#EFF3FB" >
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>
                                            <td>
                                                <%# Eval("supporder")%>
                                            </td>
                                            <td>
                                                <%# Eval("realamt")%>                                               
                                            </td>
                                            <td>
                                                <%# Eval("result")%>
                                            </td>                                           
                                            <td>
                                                <%# Eval("status")%>
                                            </td>
                                            <td>
                                                <%# Eval("coin")%>
                                            </td>
                                            <td>
                                                <%# Eval("cardtype")%>
                                            </td>                                            
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr bgcolor="#ffffff" >
                                            <td>
                                                <%# Eval("orderid")%>
                                            </td>
                                            <td>
                                                <%# Eval("supporder")%>
                                            </td>
                                            <td>
                                                <%# Eval("realamt")%>                                               
                                            </td>
                                            <td>
                                                <%# Eval("result")%>
                                            </td>                                           
                                            <td>
                                                <%# Eval("status")%>
                                            </td>
                                            <td>
                                                <%# Eval("coin")%>
                                            </td>
                                            <td>
                                                <%# Eval("cardtype")%>
                                            </td>       
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
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
