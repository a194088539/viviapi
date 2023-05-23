<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.ChannelList" Codebehind="ChannelList.aspx.cs" %>

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
                <td align="center" colspan="2" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                    color: teal; background-repeat: repeat-x; height: 28px">
                    通道管理
                    </td>
            </tr>
            <tr>
            <td>
               类别：<asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                </asp:DropDownList><asp:Button ID="btnSearch" runat="server" Text="查 找" OnClick="btnSearch_Click" /> <asp:Button ID="btnAdd" runat="server" Text="新 增" OnClick="btnAdd_Click" />
            </td>
            </tr>
            <tr>             
                <td align="center">
                    <asp:GridView ID="GVChannel" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" Width="100%" CellSpacing="1" 
                        OnRowDataBound="GVChannel_RowDataBound" onrowcommand="GVChannel_RowCommand">
                        <Columns>    
                             <asp:BoundField DataField="modetypename" HeaderText="类别">
                                <ControlStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="code" HeaderText="代码">
                                <ControlStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="modeEnName" HeaderText="英文代码">
                                <ControlStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="modeName" HeaderText="名称">
                                <ControlStyle Width="8%" />
                            </asp:BoundField>                       
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    面值
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("faceValue")%>
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:BoundField DataField="usingSupplierName" HeaderText="通道接口商">
                                <ControlStyle Width="8%" />
                            </asp:BoundField> 
                             <asp:TemplateField>
                                <HeaderTemplate>
                                    通道费率
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("supprate","{0:p2}")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="typesupp" HeaderText="通道类别接口商">
                                <ControlStyle Width="8%" />
                            </asp:BoundField> 
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    通道类别费率
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("typesupprate", "{0:p2}")%>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    开启状态
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal ID="litopen" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>                          
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <input type="button" value="设 置" onclick="javascript:location.href='ChannelEdit.aspx?id=<%#Eval("id")%>'" /> 
                                    <asp:Button ID="btnDel" runat="server" Text="删除" CommandName="del" CommandArgument='<%#Eval("id")%>' OnClientClick="javascript:return confirm('您确认要删除此通道吗？');" />                                   
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
