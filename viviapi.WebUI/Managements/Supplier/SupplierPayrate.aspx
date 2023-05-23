<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.SupplierPayrate" Codebehind="SupplierPayrate.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>供应商费率</title>
     <link href="../style/admin.css" type="text/css" rel="stylesheet" />
     <link href="../style/page.css" type="text/css" rel="stylesheet" />

    <script src="../../js/common.js" type="text/javascript"></script>
    <script type="text/javascript">
     $().ready(function(){
        var sup = $("#hfsupplierid").val();
        switch(sup){
           
            default:
            $(".bank").show();
            $(".card").show();
            break;
        }
     });
function backreturn(){
    location.href='SupplierList.aspx';
}
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="hfsupplierid" runat="server" />
            <table cellspacing="0" cellpadding="0" width="100%" border="0">                
                <tr >
                    <td class="td2">
                        名称 ：</td>
                    <td class="td1">
                        <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr class="bank" style="display:none">
                    <td class="td2">
                        网上银行：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp102" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="alipay" >
                    <td class="td2">
                        支付宝：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp101" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tenpay" style="display:none">
                    <td class="td2">
                        财付通：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp100" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        微信：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp99" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        QQ钱包：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp98" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        京东钱包：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp97" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        银联钱包：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp91" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        手机支付宝：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp95" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        手机微信：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp94" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        手机网银：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp96" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        手机QQ：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp93" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        手机京东：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp92" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        微信H5：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp89" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        百度钱包：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp88" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        支付宝H5：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp87" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        QQ钱包H5：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp86" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        神州行充值卡：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp103" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" >
                    <td class="td2">
                        神州行浙江卡：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp200" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                  <tr class="card" >
                    <td class="td2">
                        神州行江苏卡：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp201" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                  <tr class="card" >
                    <td class="td2">
                        神州行辽宁卡：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp202" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                  <tr class="card" >
                    <td class="td2">
                        神州行福建卡：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp203" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        盛大一卡通：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp104" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        盛付通卡：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp210" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        征途支付卡：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp105" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        骏网一卡通：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp106" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        腾讯Q币卡：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp107" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        联通充值卡：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp108" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        久游一卡通：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp109" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        网易一卡通：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp110" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        完美一卡通：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp111" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        搜狐一卡通：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp112" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        电信充值卡：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp113" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        声讯卡：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp114" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        光宇一卡通：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp115" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        金山一卡通：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp116" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        纵游一卡通：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp117" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        天下一卡通：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp118" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" >
                    <td class="td2">
                        天下一卡通专项 ：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp209" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        天宏一卡通：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp119" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                 <tr class="card" >
                    <td class="td2">
                        魔兽卡：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp204" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" >
                    <td class="td2">
                        联华卡：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp205" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" style="display:none">
                    <td class="td2">
                        短信：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp90" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="card" >
                    <td class="td2">
                        殴飞一卡通：</td>
                    <td class="td1">
                        <asp:TextBox ID="txtp208" runat="server" Width="200px"></asp:TextBox>
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
