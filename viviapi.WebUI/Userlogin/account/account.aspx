<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="account.aspx.cs" Inherits="viviapi.WebUI.Userlogin.account.account" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的账户 － 账户首页</title>
    <link rel="stylesheet" href="../css/panel.css" />
    <link rel="stylesheet" type="text/css" href="../css/datatable.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="panel-wrapper">
        <div id="panel-head">
            <h2>
                您好，欢迎您的到来</h2>
            <div class="panel-notify">
            </div>
        </div>
        <div id="panel-content">
            <div id="columnA" class="column">
                <div class="portlet account-portlet">
                    <div class="module">
                        <div class="portlet-header">
                            <h3>
                                账户信息</h3>
                        </div>
                        <div class="portlet-content">
                            <div class="account-info">
                                <h4>
                                    下午好，
                                     <%=getnm %></h4>
                                <ul class="horizonal">
                                   <li> <%=getemail %>&nbsp<a href="javascript:void(0)" id="a_verify" class="font12 weight-n"><%=getemailbtn %></li></a><li>会员编号：<strong class="red"><%=getnid %></strong> </li>
                                </ul>
                                <p class="login-info">
                                    上次登录时间：<span><%=getlastm %></span>&nbsp;&nbsp;&nbsp;于&nbsp;<span id="location"><%=getlastip %></span>
                                </p>
                            </div>
                            <div class="balance-info">
                                <h4>
                                    账户余额</h4>
                                <ul class="horizonal">
                                    <li>
                                        <p class="num">
                                            <span class="red">
                                                <asp:Literal ID="litbalance" runat="server"></asp:Literal></span> 元</p>
                                    </li>
                                    <li><a href="" onclick="parent.location.href=&#39;/Userlogin/recharg/index.aspx&#39;"
                                        id="refresh" class="btn btn-primary">充值</a> <a href="" onclick="parent.location.href=&#39;/Userlogin/settlement/index.aspx&#39;"
                                            id="cash" class="btn">提现</a> </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="portlet notifycation-portlet">
                    <div class="module">
                        <div class="portlet-header">
                            <h3>
                                系统通知</h3>
                        </div>
                        <div class="portlet-content">
                            <h4 id="title" class="red">
                                寄售平台改版啦！</h4>
                            <p id="createtime">
                                2014-05-21 12:00:00</p>
                            <p id="content">
                                尊敬的卡寄售用户，我们的卡寄售平台全新改版啦！改版试用期间，老版本卡寄售会与新版本卡寄售并行运行。欢迎新老客户对我们新卡寄售平台提供宝贵意见。祝所有用户生意兴隆、财源广进！</p>
                        </div>
                    </div>
                </div>
            </div>
            <div id="columnB" class="column columnRight">
                <div class="portlet business-portlet">
                    <div class="module">
                        <div class="portlet-header">
                            <h3>
                                今日统计</h3>
                        </div>
                        <div id="stat" class="portlet-content">
                            <table class="table table-striped centered">
                                <thead>
                                    <tr>
                                        <th>
                                            交易笔数
                                        </th>
                                        <th>
                                            交易金额（元）
                                        </th>
                                        <th>
                                            成功笔数
                                        </th>
                                        <th>
                                            成功金额（元）
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <span id="ordercount" class="num">
                                                <%=totalordertotal %></span>笔
                                        </td>
                                        <td>
                                            <span id="totalmoney" class="num success">
                                                <%=totalrefervalue %></span>元
                                        </td>
                                        <td>
                                            <span id="succordercount" class="num warning">
                                                <%=totalsuccordertotal %></span>笔
                                        </td>
                                        <td>
                                            <span id="succtotalmoney" class="num red">
                                                <%=totalrealvalue %></span>元
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="portlet records-portlet">
                    <div class="module">
                        <div class="portlet-header">
                            <h3>
                                当日网银交易记录</h3>
                        </div>
                        <div class="portlet-content">
                            <div id="dataTable_wrapper" class="dataTables_wrapper" role="grid" style="text-align: center;">
                                <table id="dataTable" class="table table-bordered table-striped centered dataTable"
                                    aria-describedby="dataTable_info">
                                    <thead>
                                        <tr role="row">
                                            <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                                                colspan="1">
                                                商户订单号
                                            </th>
                                            <th class="sorting_desc" role="columnheader" tabindex="0" aria-controls="dataTable"
                                                rowspan="1">
                                                订单时间
                                            </th>
                                            <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                                                colspan="1">
                                                运营商
                                            </th>
                                            <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                                                colspan="1">
                                                提交金额
                                            </th>
                                            <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                                                colspan="1">
                                                实际金额
                                            </th>
                                            <th class="sorting_disabled" role="columnheader" rowspan="1" colspan="1">
                                                订单状态
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody role="alert" aria-live="polite" aria-relevant="all">
                                        <asp:Repeater ID="rporderbank" runat="server">
                                            <ItemTemplate>
                                                <tr role="row">
                                                    <td>
                                                        <%# Eval("userorder")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("modeName")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("refervalue", "{0:f0}")%>
                                                    </td>
                                                    <td>
                                                        <%#GetViewSuccessAmt(Eval("status"),Eval("realvalue"))%>
                                                    </td>
                                                    <td>
                                                        <%#GetViewStatusName(Eval("status"))%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <%if (this.Pager1.RecordCount > 0)
                                          { %>
                                        <tr class="odd">
                                            <td valign="middle" colspan="6" class="dataTables_empty">
                                                <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="true" CustomInfoHTML="共%PageCount%页/%RecordCount%条"
                                                    CustomInfoTextAlign="Left" FirstPageText="首页" LastPageText="末页" NavigationToolTipTextFormatString="跳转{0}页"
                                                    NextPageText="下一页" PageIndexBoxType="TextBox" PageSize="6" PrevPageText="上一页"
                                                    ShowBoxThreshold="50" ShowCustomInfoSection="Right" ShowPageIndexBox="Never"
                                                    SubmitButtonText="GO&gt;&gt;" TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到"
                                                    Width="100%" Height="30px" OnPageChanged="Pager1_PageChanged" CustomInfoSectionWidth="20%"
                                                    PageIndexBoxClass="Pager1_input" PageIndexBoxStyle="width:10px" NumericButtonCount="5">
                                                </aspxc:AspNetPager>
                                            </td>
                                        </tr>
                                        <%}
                                          else
                                          { %>
                                        <tr class="odd">
                                            <td valign="top" colspan="6" class="dataTables_empty">
                                                没有符合条件的记录
                                            </td>
                                        </tr>
                                        <%} %>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="portlet-header">
                            <h3>
                                当日点卡交易记录</h3>
                        </div>
                        <div id="dataTable_wrapper1" class="dataTables_wrapper" role="grid">
                            <table id="Table1" class="table table-bordered table-striped centered dataTable"
                                aria-describedby="dataTable_info">
                                <thead>
                                    <tr role="row">
                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                                            colspan="1">
                                            商户订单号
                                        </th>
                                        <th class="sorting_desc" role="columnheader" tabindex="0" aria-controls="dataTable"
                                            rowspan="1">
                                            订单时间
                                        </th>
                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                                            colspan="1">
                                            充值卡号
                                        </th>
                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                                            colspan="1">
                                            运营商
                                        </th>
                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                                            colspan="1">
                                            提交金额
                                        </th>
                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                                            colspan="1">
                                            实际金额
                                        </th>
                                        <th class="sorting_disabled" role="columnheader" rowspan="1" colspan="1">
                                            订单状态
                                        </th>
                                    </tr>
                                </thead>
                                <tbody role="alert" aria-live="polite" aria-relevant="all">
                                    <asp:Repeater ID="rpordercard" runat="server">
                                        <ItemTemplate>
                                            <tr role="row">
                                                <td>
                                                    <%# Eval("userorder")%>
                                                </td>
                                                <td>
                                                    <%# Eval("cardno")%>
                                                </td>
                                                <td>
                                                    <%# Eval("addtime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("modeName")%>
                                                </td>
                                                <td>
                                                    <%# Eval("refervalue", "{0:f0}")%>
                                                </td>
                                                <td>
                                                    <%#GetViewSuccessAmt(Eval("status"),Eval("realvalue"))%>
                                                </td>
                                                <td>
                                                    <%#GetViewStatusName(Eval("status"))%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <%if (this.Pager2.RecordCount > 0)
                                      { %>
                                    <tr class="odd">
                                        <td valign="middle" colspan="7" class="dataTables_empty">
                                            <aspxc:AspNetPager ID="Pager2" runat="server" AlwaysShow="true" CustomInfoHTML="共%PageCount%页/%RecordCount%条"
                                                CustomInfoTextAlign="Left" FirstPageText="首页" LastPageText="末页" NavigationToolTipTextFormatString="跳转{0}页"
                                                NextPageText="下一页" PageIndexBoxType="TextBox" PageSize="6" PrevPageText="上一页"
                                                ShowBoxThreshold="50" ShowCustomInfoSection="Right" ShowPageIndexBox="Never"
                                                SubmitButtonText="GO&gt;&gt;" TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到"
                                                Width="100%" Height="30px" OnPageChanged="Pager2_PageChanged" CustomInfoSectionWidth="20%"
                                                PageIndexBoxClass="Pager1_input" PageIndexBoxStyle="width:10px" NumericButtonCount="5">
                                            </aspxc:AspNetPager>
                                        </td>
                                    </tr>
                                    <%}
                                      else
                                      { %>
                                    <tr class="odd">
                                        <td valign="top" colspan="7" class="dataTables_empty">
                                            没有符合条件的记录
                                        </td>
                                    </tr>
                                    <%} %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
