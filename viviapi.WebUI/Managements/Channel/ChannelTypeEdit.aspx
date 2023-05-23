<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.ChannelTypeEdit" Codebehind="ChannelTypeEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建编辑供应商</title>
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <script src="../../js/common.js" type="text/javascript"></script>

    <script type="text/javascript">
function backreturn(){
    location.href='ChannelTypeList.aspx';
}
    </script>
    
    <script type="text/javascript">
        $().ready(function() {
            statechange();

            $("input[name='rblrunmode']").click(function() {
                statechange();
            });
        })

        function statechange() {
            var runmode = $("input[name='rblrunmode']:checked").val();
            if (runmode == "0") {
                $("#ddlSupplier").show();
                $("#cblSupplier").hide();
            }
            else if (runmode == "1") {
                $("#cblSupplier").show();
                $("#ddlSupplier").hide();
            }
        }  
function backreturn(){
    history.go(-1);
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">                
                <tr>
                    <td class="td2">
                        名称 ：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtmodetypename" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        ID ：</td>
                    <td class="td1">
                        <asp:TextBox ID="txttypeId" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        英文代码 ：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtCode" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        通道类型 ：</td>
                    <td class="td1">
                        <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1">在线</asp:ListItem>
                            <asp:ListItem Value="2">充值卡</asp:ListItem>
                            <asp:ListItem Value="4">声讯</asp:ListItem>
                            <asp:ListItem Value="8">短信</asp:ListItem>
                            <asp:ListItem Value="6">手机网银</asp:ListItem>
                            <asp:ListItem Value="9">手机支付宝</asp:ListItem>
                            <asp:ListItem Value="10">手机微信</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td class="td2">
                        开启状态 ：</td>
                    <td class="td1">      
                        <asp:DropDownList ID="ddlOpen" runat="server" >   
                            <asp:ListItem Value="2">全部开启</asp:ListItem>                         
                            <asp:ListItem Value="1">全部关闭</asp:ListItem>                            
                            <asp:ListItem Value="8">按配置(默认开启)</asp:ListItem>
                            <asp:ListItem Value="4">按配置(默认关闭)</asp:ListItem>                            
                        </asp:DropDownList><span style="color:Red"> <br />注意按配置 的意思是如果用户设置了通道的开关状态就按用户的设置的状态 
                                            <br />如果没有 就看具体通道有没有设置通道状态 如果设置就按具体通道的状态
                                            <br />如果没有就默认为关闭 或才开启</span></td>
                </tr>
                <tr>
                    <td class="td2">
                        接口模式 ：</td>
                    <td class="td1">
                        <asp:RadioButtonList ID="rblrunmode" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" onselectedindexchanged="rblrunmode_SelectedIndexChanged">
                            <asp:ListItem Value="0" Selected="True">单独</asp:ListItem>
                            <asp:ListItem Value="1">轮询</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="tr_runmode_1" runat="server" visible="false">
                    <td class="td2">
                        轮询模式 ：</td>
                    <td class="td1">
                        <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                         <asp:Repeater ID="rptsupp" runat="server" OnItemCommand="rptsupp_ItemCommand" OnItemDataBound="rptsupp_ItemDataBound">
                        <HeaderTemplate>
                            <tr style="background-color: #507CD1; color: #fff; height: 22;">
                                <td>
                                    <input id="chkAll" type="checkbox" value="是否参与">
                                </td>
                                <td>
                                    通道商代码
                                </td>
                                <td>
                                    通道商名称
                                </td>
                                <td>
                                    权重(1-9)
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB">
                                <asp:HiddenField ID="hfsuppid" runat="server" Value='<%# Eval("code")%>' />
                                <td>
                                    <input id="chkItem" type="checkbox" runat="server"  name="chkItem" />
                                </td>
                                <td>
                                    <%# Eval("code")%>
                                </td>
                                <td>
                                    <%# Eval("name")%>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtweight" runat="server" Width="80px" Text='<%# Eval("weight")%>'></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff">
                                <asp:HiddenField ID="hfsuppid" runat="server" Value='<%# Eval("code")%>' />
                                <td>
                                    <input id="chkItem" type="checkbox" runat="server"  name="chkItem"/>
                                </td>
                                <td>
                                    <%# Eval("code")%>
                                </td>
                                <td>
                                    <%# Eval("name")%>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtweight" runat="server" Width="80px" Text='<%# Eval("weight")%>'></asp:TextBox>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>                       
                    </asp:Repeater>
                     </table>
                    </td>
                </tr>
                <tr id="tr_runmode_0" runat="server">
                    <td class="td2">
                        接口商 ：</td>
                    <td class="td1">
                        <asp:DropDownList ID="ddlSupplier" runat="server"></asp:DropDownList>
                        
                        <asp:CheckBoxList ID="cblSupplier" runat="server" RepeatDirection="Horizontal" Visible="false">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        排序 ：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtsort" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td2">
                        是否显示 ：</td>
                    <td class="td1">                        
                        <asp:RadioButtonList ID="rblRelease" runat="server" RepeatDirection="horizontal">
                            <asp:ListItem Selected="true" Value="1">显示</asp:ListItem>
                            <asp:ListItem Value="0">不显示</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="td2">                        
                    </td>
                    <td class="td1">
                        <asp:Button ID="btnSave" runat="server" Text="保 存" OnClick="btnSave_Click"></asp:Button>
                        <input type="button" value="返 回" onclick="backreturn()" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
