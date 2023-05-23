<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Withdraw.AgentDistsInfo"
    CodeBehind="AgentDistsInfo.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
    <link href="../style/page_show.css" type="text/css" rel="stylesheet" />
    <script src="../../js/common.js" type="text/javascript"></script>

    <style type="text/css">
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" class="htitle">
                详细信息查看
            </td>
        </tr>
    </table>   
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="td2">
                ID ：
            </td>
            <td class="td1">
                <asp:Label ID="lblid" runat="server"></asp:Label>
            </td>        
            <td class="td2">
                模式 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblmode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                批次号 ：
            </td>
            <td class="td1">
                <asp:Label ID="lbllotno" runat="server"></asp:Label>
            </td>        
            <td class="td2">
                序号 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblserial" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                系统流水号 ：
            </td>
            <td class="td1">
                <asp:Label ID="lbltrade_no" runat="server"></asp:Label>
            </td>        
            <td class="td2">
                商户订单号 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblout_trade_no" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>            
            <td class="td2">
                商户 ：
            </td>
            <td class="td1">
                <asp:Label ID="lbluserid" runat="server"></asp:Label>
            </td>
            <td class="td2">
                当前余额 ：
            </td>
            <td class="td1">
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                sign_type ：
            </td>
            <td class="td1">
                <asp:Label ID="lblsign_type" runat="server"></asp:Label>
            </td>
            <td class="td2">
                service ：
            </td>
            <td class="td1">
                <asp:Label ID="lblservice" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                付款银行 ：
            </td>
            <td class="td1">
                 <asp:Label ID="lblbankName" runat="server"></asp:Label><asp:Label ID="lblbankCode" runat="server"></asp:Label>
                 <br />                 
                 <asp:Label ID="lblbankBranch" runat="server"></asp:Label>
            </td>
            <td class="td2">
                收款人姓名 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblbankAccountName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                收款账号 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblbankAccount" runat="server"></asp:Label>
            </td>
            <td class="td2">
                付款金额 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblamount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                手继费 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblcharge" runat="server"></asp:Label>
            </td>
            <td class="td2">
                添加时间 ：
            </td>
            <td class="td1">
                <asp:Label ID="lbladdTime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                处理时间 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblprocessingTime" runat="server"></asp:Label>
            </td>
            <td class="td2">
                审核状态 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblaudit_status" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                付款状态 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblpayment_status" runat="server"></asp:Label>
            </td>
            <td class="td2">
                是否取消 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblis_cancel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                备注说明 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblremark" runat="server"></asp:Label>
            </td>
            <td class="td2">
                付款接口 ：
            </td>
            <td class="td1">
                <asp:Label ID="lbltranApi" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                通知次数 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblnotifyTimes" runat="server"></asp:Label>
            </td>
            <td class="td2">
                通知状态 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblnotifystatus" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                返回内容 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblcallbackText" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>            
            <td class="td2">
                return_url ：
            </td>
            <td class="td1">
                <asp:Label ID="lblreturn_url" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 20px">
                <div align="center">
                <br />      
                    <asp:Button ID="btn_cancel" runat="server" Text="取消" Visible="false" 
                        onclick="btn_cancel_Click"/>
                    <asp:Button ID="btnAudits" runat="server" Text="审核" Visible="false" 
                        onclick="btnAudits_Click" />
                    <asp:Button ID="btnRefuse" runat="server" Text="拒绝" Visible="false" 
                        onclick="btnRefuse_Click"/>
                    <asp:Button ID="btnpaysuccess" runat="server" Text="付款成功" Visible="false" 
                        onclick="btnpaysuccess_Click"/>
                    <asp:Button ID="btnpayfail" runat="server" Text="付款失败" Visible="false" 
                        onclick="btnpayfail_Click"/>
                                        <asp:Button ID="btnreNotify" runat="server" Text="重新通知" 
    Visible="false"/>
                    
                    <input type="button" value="关 闭" onclick="javascript:window.close();" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
