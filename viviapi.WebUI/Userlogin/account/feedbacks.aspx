<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feedbacks.aspx.cs" Inherits="viviapi.WebUI.Userlogin.account.feedbacks" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
   
    <link rel="stylesheet" type="text/css" href="/Userlogin/static/js/ejs/skin/vista/ymPrompt.css" />

    <script src="/Userlogin/static/js/lib/jquery-1.4.2.js" type="text/javascript"></script>

    <script src="/Userlogin/static/js/app/jquery.artDialog.js" type="text/javascript"></script>

    <script src="/Userlogin/static/js/app/jquery.artDialog.source.js?skin=simple" type="text/javascript"></script>

    <script src="/Userlogin/static/js/ejs/ymPrompt.js" type="text/javascript"></script>

    <script type="text/javascript" src="/Userlogin/static/js/date.js"></script>

    <script type="text/javascript">

        function view(id) {
            ymPrompt.win('feedbackview.aspx?id=' + id, 600, 380, '反馈查看', handler, null, null, { id: 'a' })
        }
        function infeedback() {
            ymPrompt.win('feedback.aspx', 600, 420, '留言反馈', handler, null, null, { id: 'a' })
        }
        function handler(tp) {
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/account/index.aspx'">我的账户</a>
        &nbsp;&gt;&nbsp; <span>留言反馈</span>
    </div>
    <input name="v$id" type="hidden" value="orderquery" />
    <!--右部表单开始-->
<div id="list_content" style="padding-top: 0px;">
        <h2>
            留言反馈
        </h2>
        <div id="search">
            <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
                <div id="msgdiv">
                    <a href="javascript:void(0);" class="btn btn-primary" onclick="infeedback()">添加反馈</a>
                </div>
            </table>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            class="table table-bordered table-striped centered dataTable" aria-describedby="dataTable_info">
            <!--列标题-->
            <thead>
                <tr role="row">
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        类型
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        问题或建议
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        时间
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        管理员回复
                    </th>
                </tr>
            </thead>  <%if (this.Pager1.RecordCount > 0)
                                          { %>
            <asp:Repeater ID="rptfeedback" runat="server" OnItemDataBound="rptfeedback_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Enum.GetName(typeof(viviapi.Model.feedbacktype), Eval("typeid"))%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <a href="javascript:view('<%#Eval("id")%>');">
                                <%# Eval("title")%></a>
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
         <%}
                                          else
                                          { %>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">

                 <tr class="odd">
                    <td valign="top" colspan="4" class="dataTables_empty">
                        没有符合条件的记录
                    </td>
                </tr>   <%} %>
</table>
          <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <aspxc:AspNetPager ID="Pager1" runat="server" CssClass="paginator" CurrentPageButtonClass="cpb"  AlwaysShow="true" CustomInfoHTML="共%PageCount%页/%RecordCount%条"
                        CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                        NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="TextBox"
                        PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50"  ShowCustomInfoSection="Right"
                        ShowPageIndexBox="Never" SubmitButtonText="GO&gt;&gt;" TextAfterPageIndexBox="页"
                        TextBeforePageIndexBox="跳到" Width="650px" Height="30px" OnPageChanged="Pager1_PageChanged"
                        CustomInfoSectionWidth="50%" PageIndexBoxClass="Pager1_input" PageIndexBoxStyle="width:10px;">
                    </aspxc:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
    <!--右部表单结束-->
    </form>
</body>
</html>
