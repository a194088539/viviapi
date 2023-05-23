<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="costlog.aspx.cs" Inherits="viviapi.WebUI.Merchant.costlog" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input name="v$id" type="hidden" value="costlog" />
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            结算记录&nbsp;
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
                        &nbsp;状态<select id="fState" runat="server" class="search_txt_01">
                            <option value="-1">全部</option>
                            <option value="1">审核中</option>
                            <option value="2">支付中</option>
                            <option value="4">无效</option>
                            <option value="0">已取消</option>
                            <option value="8">已支付</option>
                        </select>
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
                    结算方式
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    结算金额
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    收款人
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    开户行
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    银行卡号
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    提现状态
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    处理时间
                </td>
            </tr>
            <!--列内容-->
            <asp:Repeater ID="rptDetails" runat="server" OnItemDataBound="rptDetails_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#((Pager1.CurrentPageIndex-1)*20)+Container.ItemIndex +1%>
                        </td>
                        <td  height="30" align="center" bgcolor="#FFFFFF">
                            银行卡
                        </td>
                        <td  height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("amount", "{0:f1}")%>
                        </td>
                        <td  height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("PayeeName")%>
                        </td>
                        <td  height="30" align="center" bgcolor="#FFFFFF">
                            <%#viviapi.BLL.SettledFactory.GetSettleBankName(Eval("PayeeBank").ToString())%>
                        </td  height="30" align="center" bgcolor="#FFFFFF">
                        <td  height="30" align="center" bgcolor="#FFFFFF">
                            <%#viviLib.Text.Strings.Mark(Eval("Account").ToString())%>
                        </td>
                        <td  height="30" align="center" bgcolor="#FFFFFF">
                            <%# Enum.GetName(typeof(viviapi.Model.SettledStatus), Eval("status"))%>
                        </td>
                        <td  height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("paytime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Literal ID="litfoot" runat="server"></asp:Literal>
                   </table>
                </FooterTemplate>
            </asp:Repeater>
            <!--合计-->
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
            <tr>
                <td height="10" colspan="3">
                </td>
            </tr>
            <tr>
            <td height="22" align="left" class="font8">
               <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="true" CustomInfoHTML="共%PageCount%页/%RecordCount%条"
                                CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                                NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="TextBox"
                                PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" 
                                 ShowCustomInfoSection="Right" ShowPageIndexBox="Never" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到" Width="100%" Height="30px"
                                OnPageChanged="Pager1_PageChanged" CustomInfoSectionWidth="20%" 
                                 PageIndexBoxClass="Pager1_input" PageIndexBoxStyle="width:10px">
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
