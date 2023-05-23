<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayResult.aspx.cs" Inherits="viviapi.gateway.links.PayResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>提交结果</title>
    <style type="text/css">
        body
        {
            font-size: 12px;
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
        .STYLE1
        {
            color: #2179DD;
        }
    </style>
</head>
<body bgcolor="#ffffff">
    <form id="form1" runat="server">
    <table width="100%" height="34" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="34">
                <img src="../images/pic_1.gif" width="69" height="60" />
            </td>
            <td width="100%" background="img/pic_3.gif" bgcolor="#2179DD">
                <img src="../images/pic_4.gif" width="40" height="40" />充值提交结果
            </td>
            <td width="13" height="34">
                <img src="../images/pic_2.gif" width="69" height="60" />
            </td>
        </tr>
    </table>
    <br />
    <table width="864" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#5c9acf" class="mytable">
        <tr>
            <td width="100%" height="88" bgcolor="#FFFFFF">
                <br />
                <table width="500" border="0" align="center" cellpadding="1" cellspacing="1" class="table_main">
                    <tr>
                        <td height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">系统订单：</span>
                        </td>
                        <td>
                            <asp:Literal ID="litSysOrderId" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">商户订单：</span>
                        </td>
                        <td>
                            <asp:Literal ID="litUserOrderId" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td width="178" height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">商户ID：</span>
                        </td>
                        <td width="315" bgcolor="#FFFFFF">
                            <asp:Literal ID="litUserId" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">充值类别：</span>
                        </td>
                        <td>
                            <asp:Literal ID="litTypeId" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr id="tr_cardId" style="display: block;">
                        <td height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">卡号：</span>
                        </td>
                        <td>
                            <asp:Literal ID="LitcardNo" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr id="tr_cardPass" style="display: block;">
                        <td height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">卡密：</span>
                        </td>
                        <td>
                            <asp:Literal ID="litCardPwd" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">支付面值：</span>
                        </td>
                        <td>
                            <asp:Literal ID="litMoney" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">提交状态：</span>
                        </td>
                        <td>
                            <asp:Literal ID="litStatus" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
