<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bankreport.aspx.cs" Inherits="viviapi.WebUI.Userlogin.order.bankreport" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
   
<link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
    <script type="text/javascript">
        function switchstate(url) { window.open(url, "", ""); }
    </script>

    <script src="/Userlogin/static/js/lib/jquery-1.4.2.js" type="text/javascript"></script>

    <script src="/Userlogin/static/js/app/jquery.artDialog.js" type="text/javascript"></script>

    <script src="/Userlogin/static/js/app/jquery.artDialog.source.js?skin=simple" type="text/javascript"></script>

    <script type="text/javascript" src="/Userlogin/static/js/date.js"></script>

    <script type="text/javascript">
        function replenish(orderid) {
            $.get("/Userlogin/Ajax/replenish_new.ashx?t=" + Math.random(), { type: "2", order: orderid },
        function(data, textStatus) {
            if (data == "true") {
                $.dialog({ title: lktitle, resize: false, content: '操作成功', ok: function() { }, close: function() { }, icon: 'succeed', width: '250px', height: '90px', time: 30000 });
            } else {
                $.dialog({ title: lktitle, resize: false, content: '操作失败', ok: function() { }, close: function() { }, icon: 'wrong', width: '250px', height: '90px', time: 30000 });
            }
        })
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/order/index.aspx'">订单管理</a> &nbsp;&gt;&nbsp;
        <span>网银通知</span>
    </div>
    <input name="v$id" type="hidden" value="orderquery" />
    <!--右部表单开始-->
     <div id="list_content" style="padding-top: 0px;">
        <h2>
            网银订单查询</h2>
        <div id="search">
            <table id="msgtable" width="100%" border="0" cellspacing="0" cellpadding="0">
                <div id="msgdiv">
                </div>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        订单状态:
                        <select name="Success" id="Success" runat="server" class="search_txt_01">
                            <option value="0">所有</option>
                            <option value="2">成功</option>
                            <option value="4">失败</option>
                            <option value="1">处理中</option>
                        </select>
                        &nbsp;其它:<select id="select_field" runat="server" class="search_txt_01">
                            <option value="1">商户订单号</option>
                        </select>
                        =
                        <input name="okey" type="text" id="okey" runat="server" maxlength="30" value="" class="txt_01" />
                        <label>
                            &nbsp;
                            <asp:Button ID="b_search" runat="server" Text="搜索" CssClass="btn btn-primary" OnClick="b_search_Click" />
                        </label>
                    </td>
                </tr>
            </table>
        </div>
         <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            id="dataTable" class="table table-bordered table-striped centered dataTable"
            aria-describedby="dataTable_info">
<thead>
            <tr role="row">
                 <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                    商户订单号
                </th>
                 <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                    订单状态
                </th>
                <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                    报告状态
                </th>
                <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                    发送次数
                </th>
                 <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                    操作
                </th>
            </tr>
</thead> 
<%if (this.Pager1.RecordCount > 0)
                                          { %>
            <asp:Repeater ID="rptOrders" runat="server" OnItemDataBound="rptDetails_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%# Eval("userorder")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#GetViewStatusName(Eval("status"))%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Enum.GetName(typeof(viviapi.Model.Order.OrderStatusEnum), Eval("notifystat"))%>
                            <%#Eval("notifycontext")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <%#Eval("notifycount")%>
                        </td>
                        <td height="30" align="center" bgcolor="#FFFFFF">
                            <asp:Literal ID="litdo" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
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
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
            <tr>
                <td height="10" colspan="3">
                </td>
            </tr>
            <tr>
                <!--按钮-->
                <td height="22" align="left" class="font8">
                    <aspxc:AspNetPager ID="Pager1" runat="server" CssClass="paginator" CurrentPageButtonClass="cpb"  AlwaysShow="true" CustomInfoHTML="共%PageCount%页/%RecordCount%条"
                        CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                        NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="TextBox"
                        PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50"  ShowCustomInfoSection="Right"
                        ShowPageIndexBox="Never" SubmitButtonText="GO&gt;&gt;" TextAfterPageIndexBox="页"
                        TextBeforePageIndexBox="跳到" Width="650px" Height="30px" OnPageChanged="Pager1_PageChanged"
                        CustomInfoSectionWidth="10%" PageIndexBoxClass="Pager1_input" PageIndexBoxStyle="width:10px;">
                    </aspxc:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
    <!--右部表单结束-->
    </form>
</body>
</html>
