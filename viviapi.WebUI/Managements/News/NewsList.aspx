<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.NewsList" Codebehind="NewsList.aspx.cs" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellpadding="1" cellspacing="1" class="table1">
                <tr>
                    <td align="center" style="font-weight: bold; font-size: 14px; background-image: url(..style/images/topbg.gif);color: teal; background-repeat: repeat-x; height: 28px">
                        新闻管理</td>
                </tr>
                <tr>
                    <td style="height: 30px">
                        <font class="jfontitem">请选择新闻类别
                            <asp:DropDownList ID="ddl_type" runat="server" Width="128px" CssClass="label">
                                <asp:ListItem Value="" Selected="True">所有类别</asp:ListItem>
                            </asp:DropDownList>&nbsp;<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" 查 询 " OnClick="btnSearch_Click">
                            </asp:Button>
                            <asp:Button ID="btnPublish" runat="server" CssClass="button" Text="一键发布今日结算公告" OnClick="btnPublish_Click"/></font></td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundField DataField="NewsID" HeaderText="ID" />                                
                                <asp:TemplateField HeaderText="类别">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNewsType" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:HyperLinkField DataTextField="NewsTitle" HeaderText="主题" />
                                <asp:BoundField DataField="AddTime" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                                  <asp:BoundField DataField="release" HeaderText="是否发布"/>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <asp:Button ID="btnUpdate" runat="server" Text="编 辑" CommandName="update" CommandArgument='<%# Eval("NewsID") %>'/>
                                        <asp:Button ID="btnDel" runat="server" Text="删 除" OnClientClick="return confirm('您确定要删除这条新闻吗？')" CommandName="del" CommandArgument='<%# Eval("NewsID") %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <aspxc:AspNetPager ID="Pager1" runat="server" OnPageChanging="Pager1_PageChanging" AlwaysShow="True" CustomInfoHTML="总记录数：%RecordCount%&nbsp;总页数：%PageCount%&nbsp;当前页：%CurrentPageIndex%&nbsp;"
                CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到"
                Width="100%" Height="30px"></aspxc:AspNetPager>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script type="text/javascript">
var mytr =  document.getElementById("GridView1").getElementsByTagName("tr");
for(var i=1;i<mytr.length;i++){
  mytr[i].onmouseover= function(){ 
var rows = this.childNodes.length;
for(var row=0;row<rows;row++){
this.childNodes[row].style.backgroundColor='#B2D3FF';
}
};
  mytr[i].onmouseout= function(){ 
var rows = this.childNodes.length;
for(var row=0;row<rows;row++){
this.childNodes[row].style.backgroundColor='';
}
};
}
    </script>
</body>
</html>
