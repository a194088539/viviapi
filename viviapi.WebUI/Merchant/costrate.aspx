<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="costrate.aspx.cs" Inherits="viviapi.WebUI.Merchant.costrate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            结算费率&nbsp;
            <img id="loadimg" width="0" height="0" src="/merchant/static/style/008.gif" />
        </div>
        <div id="search">
            <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
                <div id="msgdiv">
                </div>
            </table>            
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            class="font2">
            <!--列标题-->
            <tr>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    序号
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    大类ID
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    类型名称
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    结算费率
                </td>
            </tr>
            <!--列内容-->
            <asp:Repeater ID="rpt_paymode" runat="server">
                <ItemTemplate>
                    <tr>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Container.ItemIndex +1%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("code")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("modetypename")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("payrate","{0:0.00}")%>%
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <!--右部表单结束-->
</asp:Content>
