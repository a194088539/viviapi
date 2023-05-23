<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="importlist.aspx.cs" Inherits="viviapi.WebUI.Merchant.importlist" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #tinybox
        {
            position: absolute;
            display: none;
            padding: 10px;
            background: #ffffff url(../image/preload.gif) no-repeat 50% 50%;
            border: 10px solid #e3e3e3;
            z-index: 2000;
        }
        #tinymask
        {
            position: absolute;
            display: none;
            top: 0;
            left: 0;
            height: 100%;
            width: 100%;
            background: #000000;
            z-index: 1500;
        }
        #tinycontent
        {
            background: #ffffff;
            font-size: 1.1em;
        }
    </style>
     <link rel="stylesheet" type="text/css" href="/merchant/static/js/ejs/skin/simple_gray/ymPrompt.css" />
    <script src="/merchant/static/js/app/jquery.artDialog.js" type="text/javascript"></script>   
    <script src="/merchant/static/js/app/jquery.artDialog.source.js?skin=simple" type="text/javascript"></script>
    <script  src="/merchant/static/js/ejs/ymPrompt.js" type="text/javascript"></script>
    <script type="text/javascript" src="/merchant/static/js/date.js"></script>
    <script type="text/javascript">
        function replenish(orderid) {
        $.get("/Merchant/Ajax/replenish.ashx?t="+Math.random(), { type: "1", order: orderid },
        function(data, textStatus) {
            if (data == "true") {
                $.dialog({ title: lktitle, resize: false, content: '�����ɹ�', ok: function() { }, close: function() { }, icon: 'succeed', width: '250px', height: '90px', time: 30000 });
            } else {
                $.dialog({ title: lktitle, resize: false, content: '����ʧ��', ok: function() { }, close: function() { }, icon: 'wrong', width: '250px', height: '90px', time: 30000 });
            }
        })
    }
    function ordermore(orderid) {
        ymPrompt.win('orderview.aspx?orderid=' + orderid, 600, 380, '������ϸ��Ϣ', handler, null, null, { id: 'a' })
    }
    function handler(tp) {
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input name="v$id" type="hidden" value="orderquery" />
    <!--�Ҳ�����ʼ-->
    <div id="list_content">
        <div id="title">
            ������¼&nbsp;
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
                        &nbsp;���ڴ�:<input id="sdate" runat="server" type="text" class="search_txt_01"
                            onfocus="HS_setDate(this)" size="12" />
                        ��:<input id="edate" runat="server" type="text" class="search_txt_01"
                            onfocus="HS_setDate(this)" size="12" />
                        &nbsp;
                                                       
                   
                        <label>
                            &nbsp;
                            <asp:Button ID="b_search" runat="server" Text="����" CssClass="search_button_01" OnClick="b_search_Click" />
                        </label>
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd" class="font2">
            <!--�б���-->
            <tr>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    ���κ�
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    Ӧ��������
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    �ɹ�����
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    Ӧ�������
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    �ɹ����
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    Ӧ��������
                </td>
                 <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    ʵ��������
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    Ӧ�����ϼ�
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    ʵ�����ϼ�
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    ״̬
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    ����
                </td>
            </tr>
            <asp:Repeater ID="rptrecharges" runat="server" 
                OnItemDataBound="rptDetails_ItemDataBound" 
                onitemcommand="rptrecharges_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                          <a href="importitems.aspx?lotno=<%# Eval("lotno")%>"><%# Eval("lotno")%></a>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("qty")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("succqty")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("amt","{0:f2}")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                             <%# Eval("succamt", "{0:f2}")%>
                        </td>   
                         <td height="30" align="center" bgcolor="#FFFFFF">
                             <%# Eval("fee", "{0:f2}")%>
                        </td>  
                         <td height="30" align="center" bgcolor="#FFFFFF">
                             <%# Eval("realfee", "{0:f2}")%>
                        </td>    
                         <td height="30" align="center" bgcolor="#FFFFFF">
                             <%# Eval("totalamt", "{0:f2}")%>
                        </td>   
                        <td height="30" align="center" bgcolor="#FFFFFF">
                             <%# Eval("totalsuccamt", "{0:f2}")%>
                        </td> 
                        <td height="30" align="center" bgcolor="#FFFFFF">
                             <%#_bll.GetAuditStatusText(Eval("audit_status"))%>
                        </td> 
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <asp:Button ID="btnSure" runat="server"  Text="�ύ���" CommandArgument='<%# Eval("lotno")%>' CommandName="sure" Visible="false" />
                            <asp:Button ID="btnCancel" runat="server" Text="ȡ������" CommandArgument='<%# Eval("lotno")%>' CommandName="cancel"  Visible="false"  />
                        </td>                         
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <!--������-->            
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
            <tr>
                <td height="10" colspan="3">
                </td>
            </tr>
            <tr>
                <!--��ť-->
                <td height="22" align="left" class="font8">
                   <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="true" CustomInfoHTML="��%PageCount%ҳ/%RecordCount%��"
                                CustomInfoTextAlign="Left" FirstPageText="��ҳ" HorizontalAlign="Right" LastPageText="ĩҳ"
                                NavigationToolTipTextFormatString="��ת{0}ҳ" NextPageText="��һҳ" PageIndexBoxType="TextBox"
                                PageSize="20" PrevPageText="��һҳ" ShowBoxThreshold="50" 
                                 ShowCustomInfoSection="Right" ShowPageIndexBox="Never" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="ҳ" TextBeforePageIndexBox="����" Width="100%" Height="30px"
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
    <!--�Ҳ�������-->
</asp:Content>
