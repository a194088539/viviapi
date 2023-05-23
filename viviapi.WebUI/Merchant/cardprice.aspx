<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="cardprice.aspx.cs" Inherits="viviapi.WebUI.Merchant.cardprice" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input name="v$id" type="hidden" value="cardprice" />
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            卡类面值&nbsp;
            <img id="loadimg" width="0" height="0" src="/merchant/static/style/008.gif" />
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
                        &nbsp;类型:
                        <select name="fType" id="fType" runat="server" class="search_txt_01">
                            <option value="0">所有通道</option>
                            <option value="103">移动充值卡</option>
                            <option value="106">骏网一卡通</option>
                            <option value="108">联通充值卡</option>
                            <option value="104">盛大一卡通</option>
                            <option value="210">盛付通卡</option>
                            <option value="111">完美一卡通</option>
                            <option value="112">搜狐一卡通</option>
                            <option value="105">征途一卡通</option>
                            <option value="109">久游一卡通</option>
                            <option value="110">网易一卡通</option>
                            <option value="115">光宇一卡通</option>
                            <option value="114">电信充值卡</option>
                            <option value="117">纵游一卡通</option>
                            <option value="118">天下一卡通</option>
                            <option value="107">腾讯一卡通</option>
                            <option value="119">天宏一卡通</option>
                        </select>
                        &nbsp;状态:
                        <select name="fState" id="fState" runat="server" class="search_txt_01">
                            <option value="-1">所有通道</option>
                            <option value="1">启用</option>
                            <option value="0">禁用</option>
                        </select>
                        &nbsp;其它:<select name="$common_select_field$" class="search_txt_01">
                            <option value="fPrice">面值</option>
                        </select>
                        =
                        <input id="txtfacevalue" runat="server" type="text" class="search_txt_01" value="" size="19" />
                        <label>
                            &nbsp;
                            <asp:Button ID="b_search" runat="server" Text="搜索" 
                            CssClass="search_button_01" onclick="b_search_Click" />
                        </label>
                    </td>
                </tr>
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
                    小类ID
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    类型名称
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    支持面值
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    状态
                </td>
            </tr>
            <!--列内容-->
            <asp:Repeater ID="rptcardtypes" runat="server">
                <ItemTemplate>
                    <tr>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#((Pager1.CurrentPageIndex-1)*20)+Container.ItemIndex +1%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#GetTogTypeCode(Eval("code"))%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("code")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("modetypename")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("facevalue")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#GetStautsName(Eval("chanelstatus"))%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
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
                        CustomInfoSectionWidth="20%" PageIndexBoxClass="Pager1_input" 
                        PageIndexBoxStyle="width:10px" CurrentPageButtonStyle="button_01">
                    </aspxc:AspNetPager>
                    &nbsp;
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
