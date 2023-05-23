<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.business.PayRate" Codebehind="PayRate.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>费率调整</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
table {FONT-WEIGHT: normal; FONT-SIZE:12px;LINE-HEIGHT: 170%;}
td{height:11px;}
A:link {COLOR: #02418a; TEXT-DECORATION: none}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="modelPanel" style="background: #F2F2F2">
        </div>
        <table width="100%" border="0" cellspacing="1" cellpadding="0">
            <tr>
                <td align="center" colspan="3" style="font-weight: bold; font-size: 14px; background-image: url(Images/topbg.gif);color: teal; background-repeat: repeat-x; height: 28px">
                    费率调整 
                    <input id="btnAdd" type="button" value="新 增" onclick="location.href='PayRateEdit.aspx'" class="button"/></td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td align="center">
                        <table width="99%" border="0" cellpadding="1" cellspacing="1" bgcolor="#cccccc" id="table2">
                            <tbody bgcolor="#cccccc">
                                <asp:Repeater ID="repRate" runat="server">
                                    <HeaderTemplate>
                                        <tr style="height: 22px; background: #507CD1; color: #fff; font-size: 10px;">
                                            <td>
                                                费率组</td>
                                            <td>
                                                网银</td>
                                            <td>
                                                支付宝</td>
                                            <td>
                                                财付通</td>
                                            <td>
                                                微信</td>
                                            <td>
                                                QQ钱包</td>
                                            <td>
                                                京东钱包</td>
                                            <td>
                                                银联钱包</td>
                                            <td>
                                                手机网银</td>
											<td>
                                                手机支付宝</td>
                                            <td>
                                                手机微信</td>
                                            <td>
                                                手机QQ</td>
                                            <td>
                                                手机京东</td>
                                            <td>
                                                微信H5</td>
                                            <td>
                                                百度钱包</td>
                                            <td>
                                                支付宝H5</td>
                                            <td>
                                                QQ钱包H5</td>
                                            <td>
                                                神州行</td>
                                            <td>
                                                浙江</td>
                                            <td>
                                                江苏</td>
                                            <td>
                                                辽宁</td>
                                             <td>
                                                福建</td>
                                            <td>
                                                盛大卡</td>
                                            <td>
                                                征途卡</td>
                                            <td>
                                                骏网卡</td>
                                            <td>
                                                Q币卡</td>
                                            <td>
                                                联通卡</td>
                                            <td>
                                                久游卡</td>
                                            <td>
                                                网易卡</td>
                                            <td>
                                                完美卡</td>
                                            <td>
                                                搜狐卡</td>
                                            <td>
                                                电信卡</td>
                                            <td>
                                                光宇卡</td>
                                            <td>
                                                金山</td>
                                            <td>
                                                纵游</td>
                                            <td>
                                                天下</td>
                                            <td>
                                                天宏</td>                                            
                                            <td>
                                                魔兽</td>
                                             <td>   类型</td>
                                            <td>
                                                操作</td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr bgcolor="#ffffff">
                                            <td>
                                                <%# Eval("levName")%>
                                            </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p102"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p101"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p100"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p99"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p98"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p97"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p91"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p96"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p95")) * 100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p94")) * 100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p93"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p92"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p89"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p88"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p87"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p86"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p103"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p200")) * 100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p201")) * 100%>
                                                %</td>
                                             <td>
                                                <%# Convert.ToDouble(Eval("p202")) * 100%>
                                                %</td>
                                              <td>
                                                <%# Convert.ToDouble(Eval("p203")) * 100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p104"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p105"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p106"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p107"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p108"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p109"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p110"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p111"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p112"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p113"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p115"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p116"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p117"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p118"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p119"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p118"))*100%>
                                                %</td>
                                            <td>
                                                <%# Eval("usetypename")%>
                                            </td>
                                            <td>
                                                <a href="PayRateEdit.aspx?id=<%# Eval("id") %>" class='ljbg'>编辑</a></td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr bgcolor="#f0f6fc">
                                            <td>
                                                <%# Eval("levName")%>
                                            </td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p102"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p101"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p100"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p99"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p98"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p97"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p91"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p96"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p95")) * 100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p94")) * 100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p93"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p92"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p89"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p88"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p87"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p86"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p103"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p200")) * 100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p201")) * 100%>
                                                %</td>
                                             <td>
                                                <%# Convert.ToDouble(Eval("p202")) * 100%>
                                                %</td>
                                              <td>
                                                <%# Convert.ToDouble(Eval("p203")) * 100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p104"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p105"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p106"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p107"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p108"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p109"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p110"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p111"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p112"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p113"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p115"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p116"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p117"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p118"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p119"))*100%>
                                                %</td>
                                            <td>
                                                <%# Convert.ToDouble(Eval("p118"))*100%>
                                                %</td>
                                            <td>
                                                <%# Eval("usetypename")%>
                                            </td>
                                            <td>
                                                <a href="PayRateEdit.aspx?id=<%# Eval("id") %>" class='ljbg'>编辑</a></td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                                <tr bgcolor="#F0F6FC" height="30" align="center" style="display:none">
                                    <td>
                                        <asp:TextBox ID="txtlevName" runat="server" Width="50px"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtp102" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp101" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp100" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp99" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp98" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp97" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp91" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp96" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp95" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp94" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp93" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp92" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp89" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp88" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp87" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp86" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp103" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp104" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp105" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp106" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp107" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp108" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp109" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp110" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp111" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp112" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp113" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp115" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp116" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp117" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td>
                                        <asp:TextBox ID="txtp118" runat="server" Width="25px"></asp:TextBox>%</td>
                                    <td colspan="2">
                                        <asp:Button ID="btn_save" runat="server" Text="保存" OnClick="btn_save_Click" />
                                        <a href="#" onclick="ymPrompt.win('ViewPrice.aspx?Pri_Type=<%# Eval("Pri_Type") %>',600,230,'用户信息',handler,null,null,{id:'a'})">
                                        </a>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>

        <script type="text/javascript">
 function handler(tp){
 }
        </script>

    </form>

    <script type="text/javascript" language="JavaScript">var table=document.getElementById("table_zyads");if (table){for(i=0;i<table.rows.length;i++){if(i%2==0){table.rows[i].bgColor="ffffff";}else{table.rows[i].bgColor="E6EEFF"}}}var mytr =  document.getElementById("table2").getElementsByTagName("tr");for(var i=1;i<mytr.length;i++){mytr[i].onmouseover= function(){ var rows = this.childNodes.length;for(var row=0;row<rows;row++){this.childNodes[row].style.backgroundColor='#E6EEFF';}};mytr[i].onmouseout= function(){var rows = this.childNodes.length;for(var row=0;row<rows;row++){this.childNodes[row].style.backgroundColor='';}};}</script>

</body>
</html>
