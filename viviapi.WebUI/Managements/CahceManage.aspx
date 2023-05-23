<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.CahceManage" Codebehind="CahceManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>后台管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="style/union.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellspacing="1" cellpadding="0" style="width: 100%; height: 100%;border: #c9ddf0 1px solid; background-color: White;" id="table_zyads">
                <tr>
                    <td align="center" colspan="2" style="font-weight: bold; font-size: 14px; background-image: url(style/images/topbg.gif);color: teal; background-repeat: repeat-x; height: 28px">
                        缓存管理
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
                                            <td height="30" style="font-size: 14px;">
                                                本地缓存总量：<asp:Label ID="CacheCountLabel" runat="server" Text="没有缓存"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td height="30">
                                                <asp:Button ID="btnDelAll" runat="server" Text="清空全部缓存" 
                                                    OnClick="btnDelAll_Click" /></td>
                                        </tr>
                                        <tr>
                                            <td height="30">
                                                <asp:Label ID="Label1" runat="server" ForeColor="Red">系统依托缓存来提高运行效率。请勿随便清空缓存！</asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td height="30">
                                            按大类清除：                                             
                                                <asp:CheckBoxList ID="cbl_cacheTypeList" runat="server" Width="100%">
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
                                                </asp:CheckBoxList>
                                                <asp:Button ID="btnBigClass" runat="server" Text="清除所选类别缓存" 
                                                    onclick="btnBigClass_Click" />
                                            </td>
                                        </tr>
                                        <tr><td>MemCached缓存:
                                            <asp:GridView ID="gv_cache" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="cacheKey">
                                                <Columns>
                                                     <asp:TemplateField>
                                                     <ItemTemplate>
                                                            <asp:CheckBox ID="item" runat="server" />
                                                     </ItemTemplate>
                                                     </asp:TemplateField>
                                                      <asp:BoundField HeaderText="缓存类别" DataField="cacheType" />
                                                      <asp:BoundField HeaderText="类别名称" DataField="cacheTypeName" />
                                                    <asp:BoundField HeaderText="缓存Key" DataField="cacheKey" />                                                                                                   
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Button ID="btnClear" runat="server" Text="按小类清除" 
                                                onclick="btnClear_Click"  />
                                            </td>
                                        </tr>
                                    </table>
                                </td>                               
                            </tr>                            
                        </table>                  
        </div>
    </form>
</body>
</html>

<script type="text/javascript" language="javascript">
var table=document.getElementById("setpsd");
if (table){
for(i=0;i<table.rows.length;i++){
if(i%2==0){
table.rows[i].bgColor="ffffff";
}else{table.rows[i].bgColor="f3f9fe"}
}
}
</script>

