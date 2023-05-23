<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="feedbacks.aspx.cs" Inherits="viviapi.WebUI.Merchant.feedbacks" %>
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
        
    function view(id) {
        ymPrompt.win('feedbackview.aspx?id=' + id, 600, 380, '反馈查看', handler, null, null, { id: 'a' })
    }
    function handler(tp) {
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input name="v$id" type="hidden" value="orderquery" />
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            留言反馈&nbsp;
            <img id="loadimg" width="0" height="0" src="/merchant/static/style/008.gif" />
        </div>
        <div id="search">
            <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
                <div id="msgdiv">
                <a href="feedback.aspx">添加反馈</a>
                </div>
            </table>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            class="font2">
            <!--列标题-->
            <tr>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    类型
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    问题或建议
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    时间
                </td>
                <td height="34" align="center" background="/merchant/static/style/09.jpg" bgcolor="#FFFFFF" class="list_title">
                    管理员回复
                </td>
            </tr>
            <asp:Repeater ID="rptfeedback" runat="server" OnItemDataBound="rptfeedback_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Enum.GetName(typeof(viviapi.Model.feedbacktype), Eval("typeid"))%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <a href="javascript:view('<%#Eval("id")%>');"> <%# Eval("title")%></a>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("addtime","{0:yyyy-MM-dd HH:ss:mm}")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                           <asp:Literal ID="litdetail" runat="server"></asp:Literal> 
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <!--列内容-->
            
        </table>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
            <tr>
                <td height="10" colspan="3">
                </td>
            </tr>
            <tr>
                <!--按钮-->
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
