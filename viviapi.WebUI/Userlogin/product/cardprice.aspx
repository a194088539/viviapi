<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cardprice.aspx.cs" Inherits="viviapi.WebUI.Userlogin.product.cardprice" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../css/mytablelist.css" /></head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i> 首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/product/index.aspx'">商品管理</a> &nbsp;&gt;&nbsp;
        <span>卡类面值</span>
    </div>
    <input name="v$id" type="hidden" value="cardprice" />
    <!--右部表单开始-->
     <div id="list_content" style="padding-top: 0px;">
        <h2>
            卡类面值</h2>
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
                        <input id="txtfacevalue" runat="server" type="text" class="search_txt_01" value=""
                            size="19" />
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
            <!--列标题-->
            <thead>
                <tr role="row">
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 5%; text-align: center;">
                        序号
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        大类ID
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        小类ID
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        类型名称
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        支持面值
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="dataTable" rowspan="1"
                        colspan="1" style="width: 100px; text-align: center;">
                        状态
                    </th>
                </tr>
            </thead><%if (this.Pager1.RecordCount > 0)
                                          { %>
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
