<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.WithdrawChannels" Codebehind="WithdrawChannels.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
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
    <form id="Form1" runat="server">
        <table width="100%" border="0" cellpadding="1" cellspacing="1" id="table_zyads" style="width: 100%;
            height: 100%; border: #c9ddf0 1px solid; background-color: White;">
            <tr>
                <td align="center" colspan="2" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);color: teal; background-repeat: repeat-x; height: 28px">
                    结算通道
                 </td>
            </tr>
            <tr>             
                <td align="center">
                    <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                    <asp:Repeater ID="rptChnls" runat="server" onitemcommand="rptChnls_ItemCommand" 
                            onitemdatabound="rptChnls_ItemDataBound" >
                        <HeaderTemplate>
                            <tr style="background-color: #507CD1; color: #fff; height: 22;">                                
                                <td>
                                    结算银行
                                </td>  
                                <td>
                                    银行代码
                                </td>                                
                                <td>
                                    通道
                                </td>
                                <td>
                                    操作
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB">                                
                                <td>
                                    <%# Eval("bankName")%>
                                </td>                              
                                <td>
                                    <%# Eval("bankCode")%>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlsupp" runat="server"></asp:DropDownList>
                                </td>                                          
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" Text="保存" CommandName="update" CommandArgument='<%#Eval("id")%>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff">
                                <td>
                                    <%# Eval("bankName")%>
                                </td>                              
                                <td>
                                    <%# Eval("bankCode")%>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlsupp" runat="server"></asp:DropDownList>
                                </td>                                          
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" Text="保存" CommandName="update" CommandArgument='<%#Eval("id")%>' />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
