<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="excha.aspx.cs" Inherits="viviapi.WebUI.Merchant.excha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="/js/xiaoka.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="list_content">
        <div id="title">
            ��������&nbsp;<img id="loading" width="0" height="0" src="/merchant/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="39" align="right" class="line_01">
                    ����ѡ��
                </td>
                <td align="left" class="line_01">
                    <asp:DropDownList ID="ddlcardType" runat="server" CssClass="search_txt_01">
                        <asp:ListItem Value="103">�ƶ���ֵ��</asp:ListItem>
                        <asp:ListItem Value="106">����һ��ͨ</asp:ListItem>
                        <asp:ListItem Value="108">��ͨ��ֵ��</asp:ListItem>
                        <asp:ListItem Value="104">ʢ��һ��ͨ</asp:ListItem>
                        <asp:ListItem Value="210">ʢ��ͨ��</asp:ListItem>
                        <asp:ListItem Value="111">����һ��ͨ</asp:ListItem>
                        <asp:ListItem Value="112">�Ѻ�һ��ͨ</asp:ListItem>
                        <asp:ListItem Value="105">��;һ��ͨ</asp:ListItem>
                        <asp:ListItem Value="109">����һ��ͨ</asp:ListItem>
                        <asp:ListItem Value="110">����һ��ͨ</asp:ListItem>
                        <asp:ListItem Value="115">����һ��ͨ</asp:ListItem>
                        <asp:ListItem Value="114">���ų�ֵ��</asp:ListItem>
                        <asp:ListItem Value="117">����һ��ͨ</asp:ListItem>
                        <asp:ListItem Value="118">����һ��ͨ</asp:ListItem>
                        <asp:ListItem Value="107">��Ѷһ��ͨ</asp:ListItem>
                        <asp:ListItem Value="119">���һ��ͨ</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 25%">
                    ���뿨�ܣ�
                </td>
                <td style="width: 75%" align="left" class="line_01">
                    <asp:TextBox ID="txtCards" runat="server" Width="80%" Rows="20" TextMode="MultiLine"
                        class="mutitxt_03"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="line_01">
                    ע�����
                </td>
                <td class="line_01">
                    1. ÿ�п���Ϣ֧�ָ�ʽΪ��������,����,�� ���� ������ ���� ��
                    <br>
                    2. ÿ�п���Ϣ�ָ���Ž�֧�ֿո�Ͷ��ţ����������ŷָ��ʾ����
                    <br>
                    3. ÿ���ύ������������20��,����������������20��ϵͳ���Զ�ȡǰ20��
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 25%">
                </td>
                <td style="width: 75%">
                    <input id="btnXiaoKa" type="button" value="�ύ����" class="button_01" />&nbsp;
                    <button class="button_01" id="queryorder" style="margin-right: 0" type="button" onclick="queryOrder();">
                        ˢ���б�</button>
                    <asp:Button ID="btnSubmit" runat="server" Text="�ύ����" CssClass="btn btn-primary" OnClick="btnSubmit_Click"
                        Visible="false" />
                    �Ѿ����� <span id="Groupscount" class="txtc" style="font-size: 18px">0</span>
                    <div class="b_m_t txtr dis-n" id="Groupsinfo">
                    </div>
                    <div id="Groupsload">
                    </div>
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 25%">
                </td>
                <td style="width: 75%" align="left" class="line_01">
                    <div id="Groupsinfo_01" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_02" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_03" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_04" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_05" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_06" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_07" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_08" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_09" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_10" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_11" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_12" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_13" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_14" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_15" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_16" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_17" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_18" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_19" style="margin-bottom: 5px">
                    </div>
                    <div id="Groupsinfo_20" style="margin-bottom: 5px">
                    </div>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            class="font2">
            <!--�б���-->
            <tr>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    ����
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    �ύ���
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    �ɹ����
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    ����״̬
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    ˵��
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    �ύʱ��
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF"
                    class="list_title">
                    ����
                </td>
            </tr>
            <tbody id="toporder">
                <asp:Repeater ID="rptorders" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%# Eval("cardNo")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#Eval("refervalue","{0:0.00}")%>
                            </td>
                            <td id="paymoney<%#Eval("ID")%>" height="30" align="center" bgcolor="#FFFFFF">
                                <%#GetViewSuccessAmt(Eval("status"),Eval("realvalue"))%>
                            </td>
                            <td id="orderzt<%#Eval("ID")%>" height="30" align="center" bgcolor="#FFFFFF">
                                <%#GetViewStatusName(Eval("status"))%>
                            </td>
                            <td id="errorMsg<%#Eval("ID")%>" height="30" align="center" bgcolor="#FFFFFF">
                                <%#Eval("msg")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <%#Eval("addtime","{0:yyyy-MM-dd HH:mm}")%>
                            </td>
                            <td height="30" align="center" bgcolor="#FFFFFF">
                                <button class="button_01" id="sub<%#Eval("ID")%>" style="margin-right: 0" type="button"
                                    onclick="checkflag('<%#Eval("ID")%>')">
                                    ˢ��</button>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>
