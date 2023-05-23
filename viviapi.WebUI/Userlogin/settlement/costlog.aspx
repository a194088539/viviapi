<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="costlog.aspx.cs" Inherits="viviapi.WebUI.Userlogin.settlement.costlog" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/settlement/index.aspx'">结算提现</a>
        &nbsp;&gt;&nbsp; <span>结算记录</span>
    </div>
    <input name="v$id" type="hidden" value="costlog" />
    <!--右部表单开始-->
    <div id="list_content" style=" padding-top:0px;">
       <h2>
            结算记录
        </h2>
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
                             <asp:Button ID="b_search" runat="server" Text="搜索" CssClass="btn btn-primary" onclick="b_search_Click" />
                        </label>
                    </td>
                </tr>
            </table>
        </div>
       <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#dddddd"
            id="dataTable" class="table table-bordered table-striped centered dataTable"
            aria-describedby="dataTable_info">
            <!--列标题-->
            <thead>
                <tr role="row">
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                    序号
                </th>
                <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1" colspan="1" style="width: 100px; text-align: center;">
                    结算方式
                </th>
                <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                    结算金额
                </th>
               <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                    收款人
                </th>
                <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                    开户行
                </th>
                <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                    银行卡号
                </th>
                <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                    提现状态
                </th>
                <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                    提交时间
                </th>
            </tr>
             </thead><%if (this.Pager1.RecordCount > 0)
                                          { %>
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
         <%}
                                          else
                                          { %>
                                           <tbody role="alert" aria-live="polite" aria-relevant="all">
            <!--列内容-->
            
                    
                   </table>
                
            <!--合计-->
             
                <tr class="odd">
                    <td valign="top" colspan="3" class="dataTables_empty">
                        没有符合条件的记录
                    </td>
                </tr>
                
            </tbody><%} %>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="font2">
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
