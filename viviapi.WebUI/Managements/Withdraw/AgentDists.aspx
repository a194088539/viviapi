<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Withdraw.AgentDists" CodeBehind="AgentDists.aspx.cs" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript">
        function Setchkall(obj) {
            var objs = document.getElementsByName("chk");
            for (i = 0; i < objs.length; i++) {
                objs[i].checked = obj.checked;
            }
        }
        function checkall(obj) {
            var check = document.getElementsByName("ischecked");
            for (i = 0; i < check.length; i++) {
                check[i].checked = obj.checked;
            }
        }
    </script>

    <script type="text/javascript">
        function sendInfo(id) {
            window.open("../User/UserEdit.aspx?id=" + id, "查看用户信息", "Width=800px;Height=350px;");
        }
        function showDetail(id) {
            window.open("AgentDistsInfo.aspx?id=" + id, "查看订单", "height=500,width=800");
        }
    </script>

    <style type="text/css">
        table
        {
            font-weight: normal;
            font-size: 12px;
            line-height: 170%;
            font-family: Arial;
        }
        td
        {
            height: 11px;
        }
        A:link
        {
            color: #02418a;
            text-decoration: none;
        }
    </style>
    <script src="../../Js/ControlDate/WdatePicker.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; height: 100%;">
            <tr>
                <td align="center" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                    color: teal; background-repeat: repeat-x; height: 28px">
                    对私代发
                </td>
            </tr>
            <tr>
                <td>
                    商户ID：<asp:TextBox ID="txtUserId" runat="server" Width="80px"></asp:TextBox>
                    批号：<asp:TextBox ID="txtLotno" runat="server" Width="120px"></asp:TextBox>
                    系统交易号：<asp:TextBox ID="txttrade_no" runat="server" Width="120px"></asp:TextBox>
                    商户交易号：<asp:TextBox ID="txtout_trade_no" runat="server" Width="120px"></asp:TextBox>
                    <asp:DropDownList ID="ddlbankCode" runat="server">
                        <asp:ListItem Value="">--收款银行--</asp:ListItem>
                        <asp:ListItem Value="0002">支付宝</asp:ListItem>
                        <asp:ListItem Value="0003">财付通</asp:ListItem>
                        <asp:ListItem Value="1002">中国工商银行</asp:ListItem>
                        <asp:ListItem Value="1005">中国农业银行</asp:ListItem>
                        <asp:ListItem Value="1003">中国建设银行</asp:ListItem>
                        <asp:ListItem Value="1026">中国银行</asp:ListItem>
                        <asp:ListItem Value="1001">招商银行</asp:ListItem>
                        <asp:ListItem Value="1006">民生银行</asp:ListItem>
                        <asp:ListItem Value="1020">交通银行</asp:ListItem>
                        <asp:ListItem Value="1025">华夏银行</asp:ListItem>
                        <asp:ListItem Value="1009">兴业银行</asp:ListItem>
                        <asp:ListItem Value="1027">广发银行</asp:ListItem>
                        <asp:ListItem Value="1004">浦发银行</asp:ListItem>
                        <asp:ListItem Value="1022">光大银行</asp:ListItem>
                        <asp:ListItem Value="1021">中信银行</asp:ListItem>
                        <asp:ListItem Value="1010">平安银行</asp:ListItem>
                        <asp:ListItem Value="1066">中国邮政储蓄银行</asp:ListItem>
                    </asp:DropDownList>
                    收款账户：<asp:TextBox ID="txtAccount" runat="server" Width="120px"></asp:TextBox>
                    收款人：<asp:TextBox ID="txtbankAccountName" runat="server" Width="80px"></asp:TextBox>
                    <asp:DropDownList ID="ddlSupplier" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>   
                     <asp:DropDownList ID="ddlmode" runat="server">
                        <asp:ListItem Value="">--提交模式--</asp:ListItem>
                        <asp:ListItem Value="1">API提交</asp:ListItem>
                        <asp:ListItem Value="2">上传文件</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlaudit_status" runat="server">
                        <asp:ListItem Value="">--审核状态--</asp:ListItem>
                        <asp:ListItem Value="1">等待审核</asp:ListItem>
                        <asp:ListItem Value="2">审核通过</asp:ListItem>
                        <asp:ListItem Value="3">审核拒绝</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlpayment_status" runat="server">
                        <asp:ListItem Value="">--付款状态--</asp:ListItem>
                        <asp:ListItem Value="1">未知</asp:ListItem>
                        <asp:ListItem Value="4">付款中</asp:ListItem>
                        <asp:ListItem Value="2">成功</asp:ListItem>
                        <asp:ListItem Value="3">失败</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlis_cancel" runat="server">
                        <asp:ListItem Value="">--是否取消--</asp:ListItem>
                        <asp:ListItem Value="0" Selected="True">未取消</asp:ListItem>
                        <asp:ListItem Value="1" >已取消</asp:ListItem>
                    </asp:DropDownList> 
                    <asp:DropDownList ID="ddl_issure" runat="server">
                        <asp:ListItem Value="">--用户确认--</asp:ListItem>
                        <asp:ListItem Value="1" Selected="True">等待确认</asp:ListItem>
                        <asp:ListItem Value="2" >已确认</asp:ListItem>
                        <asp:ListItem Value="3" >已取消</asp:ListItem>
                    </asp:DropDownList>  
                     <asp:DropDownList ID="ddlnotifystatus" runat="server">
                        <asp:ListItem Value="">--通知状态--</asp:ListItem>
                        <asp:ListItem Value="0">发送失败</asp:ListItem>
                        <asp:ListItem Value="1">处理中</asp:ListItem>
                        <asp:ListItem Value="2" >已成功</asp:ListItem>
                    </asp:DropDownList>               
                    开始：
                    <asp:TextBox ID="txtStimeBox" runat="server" Width="65px"></asp:TextBox>
                    截止：
                    <asp:TextBox ID="txtEtimeBox" runat="server" Width="65px"></asp:TextBox>
                    
                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" 查 询 " OnClick="btnSearch_Click">
                    </asp:Button>
                    <asp:Button ID="btnExport" runat="server" CssClass="button" Text="导出"
                            OnClick="btnExport_Click"></asp:Button>
                    <div id="divmoney">
                    <span style="color: #ff0000; text-align: left">总申请金额：<% = total_amount%></span> 
                    <span style="color: #ff0000; text-align: left;">手续费：<% = total_charge%></span>
                    <span style="color: #ff0000; text-align: left;">实际支付：<% = total_paymoney%></span>
                </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="1" width="100%" id="table2">
                        <asp:Repeater ID="rptList" runat="server" 
                            onitemdatabound="rptList_ItemDataBound" 
                            onitemcommand="rptList_ItemCommand">
                            <HeaderTemplate>
                                <tr style="background-color: #507CD1; color: #fff; height: 22px">
                                    <td style="width:3%">
                                        序号
                                    </td>
                                    <td style="width:6%">
                                        提交模式
                                    </td>
                                    <td style="width:8%">
                                        系统单号
                                    </td>
                                    <td style="width:8%">
                                        商户单号
                                    </td>
                                    <td style="width:7%">
                                        商户
                                    </td>
                                    <td style="width:10%">
                                        收款信息
                                    </td>
                                    <td style="width:5%">
                                        申请金额
                                    </td>
                                    <td style="width:5%">
                                        手续费
                                    </td>
                                    <td style="width:5%">
                                        实付金额<br />
                                    </td>
                                    <td style="width:5%">
                                        审核状态
                                    </td>
                                    <td style="width:5%">
                                        付款接口
                                    </td>
                                    <td style="width:5%">
                                        付款状态
                                    </td>                                     
                                    <td style="width:8%">
                                        付款时间
                                    </td>  
                                    <td style="width:5%">
                                        用户确认
                                    </td> 
                                    <td style="width:4%">
                                        取消
                                    </td>
                                    <td>
                                        操作
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr bgcolor="#EFF3FB" ondblclick="javascript:showDetail('<%# Eval("id")%>')">
                                    <td>
                                        <%# Eval("ID")%>
                                    </td>
                                    <td>
                                        <%#stlAgtBLL.GetModeText(Eval("mode"))%>
                                    </td>
                                    <td>
                                        <%# Eval("trade_no")%>
                                    </td>
                                    <td>
                                        <%# Eval("out_trade_no")%>
                                    </td>
                                    <td>
                                        <a href="?action=paylistbyid&userid=<%#Eval("userid")%>">
                                            <%#Eval("UserName")%>(#<%#Eval("userid")%>) </a>
                                    </td>
                                    <td>
                                        <%# Eval("bankName")%>
                                        <br />
                                        <%# Eval("bankBranch")%>
                                        <br />
                                        <%# Eval("bankAccountName")%>
                                        <br />
                                        <%# Eval("bankAccount")%>
                                    </td>
                                    <td>
                                        <%# Eval("amount","{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# Eval("charge", "{0:f2}")%>
                                    </td>
                                    <td>
                                        <%# (Convert.ToDecimal(Eval("amount")) + Convert.ToDecimal(Eval("charge"))).ToString("f2")%>
                                    </td>
                                    <td>
                                        <%#stlAgtBLL.GetAuditStatusText(Eval("audit_status"))%>
                                    </td>
                                    <td>
                                        <%# Eval("tranApi")%>
                                    </td>
                                    <td>
                                        <%#stlAgtBLL.GetPaymentStatusText(Eval("payment_status"))%>
                                    </td>
                                     <td>
                                        <%# Eval("processingTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                     <td>
                                        <%#stlAgtBLL.GetIsSureText(Eval("issure"))%>
                                    </td>
                                    <td>
                                         <%#stlAgtBLL.GetCancelText(Eval("is_cancel"))%>
                                    </td>
                                    <td>
                                         <asp:Button ID="btnCancel" runat="server" Text="取消" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="Cancel" />
                                         <asp:Button ID="btnAudits" runat="server" Text="审核" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="Audit"/> 
                                         <asp:Button ID="btnRefuse" runat="server" Text="拒绝" Visible="false" CommandArgument='<%# Eval("trade_no")%>' CommandName="Refuse"/>
                                         <asp:Button ID="btnReissue" runat="server" Text="补发"  CommandArgument='<%# Eval("trade_no")%>' CommandName="Reissue"/>
                                         <asp:Button ID="btnResendToApi" runat="server" Text="提交到接口" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="ResendToApi" />
                                         <asp:Button ID="btnpaysuccess" runat="server" Text="付款成功" Visible="false"  CommandArgument='<%# Eval("trade_no")%>' CommandName="paysuccess"/>
                                         <asp:Button ID="btnpayfail" runat="server" Text="付款失败" Visible="false" CommandArgument='<%# Eval("trade_no")%>' CommandName="payfail"/>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                    <aspxc:AspNetPager ID="Pager1" runat="server" AlwaysShow="True" CustomInfoHTML="总记录数：%RecordCount%&nbsp;总页数：%PageCount%&nbsp;当前页：%CurrentPageIndex%&nbsp;"
                        CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                        NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                        PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                        ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                        TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到" Width="100%" Height="30px"
                        OnPageChanged="Pager1_PageChanged">
                    </aspxc:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
