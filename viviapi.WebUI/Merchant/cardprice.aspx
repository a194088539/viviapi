<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="cardprice.aspx.cs" Inherits="viviapi.WebUI.Merchant.cardprice" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input name="v$id" type="hidden" value="cardprice" />
    <!--�Ҳ�����ʼ-->
    <div id="list_content">
        <div id="title">
            ������ֵ&nbsp;
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
                        <!--������-->
                        &nbsp;����:
                        <select name="fType" id="fType" runat="server" class="search_txt_01">
                            <option value="0">����ͨ��</option>
                            <option value="103">�ƶ���ֵ��</option>
                            <option value="106">����һ��ͨ</option>
                            <option value="108">��ͨ��ֵ��</option>
                            <option value="104">ʢ��һ��ͨ</option>
                            <option value="210">ʢ��ͨ��</option>
                            <option value="111">����һ��ͨ</option>
                            <option value="112">�Ѻ�һ��ͨ</option>
                            <option value="105">��;һ��ͨ</option>
                            <option value="109">����һ��ͨ</option>
                            <option value="110">����һ��ͨ</option>
                            <option value="115">����һ��ͨ</option>
                            <option value="114">���ų�ֵ��</option>
                            <option value="117">����һ��ͨ</option>
                            <option value="118">����һ��ͨ</option>
                            <option value="107">��Ѷһ��ͨ</option>
                            <option value="119">���һ��ͨ</option>
                        </select>
                        &nbsp;״̬:
                        <select name="fState" id="fState" runat="server" class="search_txt_01">
                            <option value="-1">����ͨ��</option>
                            <option value="1">����</option>
                            <option value="0">����</option>
                        </select>
                        &nbsp;����:<select name="$common_select_field$" class="search_txt_01">
                            <option value="fPrice">��ֵ</option>
                        </select>
                        =
                        <input id="txtfacevalue" runat="server" type="text" class="search_txt_01" value="" size="19" />
                        <label>
                            &nbsp;
                            <asp:Button ID="b_search" runat="server" Text="����" 
                            CssClass="search_button_01" onclick="b_search_Click" />
                        </label>
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            class="font2">
            <!--�б���-->
            <tr>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    ���
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    ����ID
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    С��ID
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    ��������
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    ֧����ֵ
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    ״̬
                </td>
            </tr>
            <!--������-->
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
                <!--��ť-->
                <td height="22" align="left" class="font8">
                    <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="False" CustomInfoHTML="��%PageCount%ҳ/%RecordCount%��"
                        CustomInfoTextAlign="Left" FirstPageText="��ҳ" HorizontalAlign="Right" LastPageText="ĩҳ"
                        NavigationToolTipTextFormatString="��ת{0}ҳ" NextPageText="��һҳ" PageIndexBoxType="TextBox"
                        PageSize="10" PrevPageText="��һҳ" ShowBoxThreshold="50" ShowCustomInfoSection="Right"
                        ShowPageIndexBox="Never" SubmitButtonText="GO&gt;&gt;" TextAfterPageIndexBox="ҳ"
                        TextBeforePageIndexBox="����" Width="100%" Height="30px" OnPageChanged="Pager1_PageChanged"
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
    <!--�Ҳ�������-->
</asp:Content>
