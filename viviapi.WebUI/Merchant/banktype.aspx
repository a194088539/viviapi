<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="banktype.aspx.cs" Inherits="viviapi.WebUI.Merchant.banktype" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            网银类型&nbsp;
            <img id="loadimg" width="0" height="0" src="/style/008.gif" />
        </div>
        <div id="search">
            <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
                <div id="msgdiv">
                </div>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <!--工具栏-->
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            class="font2">
            <!--列标题-->
            <tr>
                <td height="34" align="center" background="/merchant/static/style/image/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    序号
                </td>
                <td height="34" align="center" background="/merchant/static/style/image/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    网银名称
                </td>
                <td height="34" align="center" background="/merchant/static/style/image/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    网银代码
                </td>
            </tr>
            <!--列内容-->
            <asp:Repeater ID="rptcardtypes" runat="server">
                <ItemTemplate>
                    <tr>
                        <tr>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#((Pager1.CurrentPageIndex-1)*20)+Container.ItemIndex +1%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#Eval("modeName")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#Eval("code")%>
                            </td>
                        </tr>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <!--合计-->
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
            <tr>
                <td height="10" colspan="3">
                </td>
            </tr>
            <tr>
                <!--按钮-->
                <td height="22" align="left" class="font8">
                    <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="False" CustomInfoHTML="共%PageCount%页/%RecordCount%条"
                        CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                        NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="TextBox"
                        PageSize="10" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Right"
                        ShowPageIndexBox="Never" SubmitButtonText="GO&gt;&gt;" TextAfterPageIndexBox="页"
                        TextBeforePageIndexBox="跳到" Width="100%" Height="30px" OnPageChanged="Pager1_PageChanged"
                        CustomInfoSectionWidth="20%" PageIndexBoxClass="Pager1_input" PageIndexBoxStyle="width:10px"
                        CurrentPageButtonStyle="button_01">
                    </aspxc:AspNetPager>
                </td>
            </tr>
            <tr>
                <td height="10" colspan="3">
                </td>
            </tr>
        </table>
    </div>
    <!--右部表单结束-->
</asp:Content>
