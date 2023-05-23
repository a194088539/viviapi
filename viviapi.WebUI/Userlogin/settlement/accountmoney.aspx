<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="accountmoney.aspx.cs" Inherits="viviapi.WebUI.Userlogin.settlement.accountmoney" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/account/index.aspx'">结算管理</a>
        &nbsp;&gt;&nbsp; <span>账户余额</span>
    </div>
    <input name="v$id" type="hidden" value="accountmoney" />
    <input name="v$fid" type="hidden" value="ruili" />
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            账户余额&nbsp;<img id="loading" width="0" height="0" src="/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01">
                    成功订单的结算金额将自动累计到当前余额
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    商户ID:
                </td>
                <td align="center" class="line_01">
                    <input id="txtuserid" runat="server" type="text" class="txt_02" size="25" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    账户余额:
                </td>
                <td align="center" class="line_01">
                    <input id="txtBalance" runat="server" type="text" class="txt_02" size="25" />
                </td>
                <td height="39" align="left" class="line_01">
                    交易成功订单并未提现的金额
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    冻结余额:
                </td>
                <td align="center" class="line_01">
                    <input id="txtFreezeAmt" runat="server" type="text" class="txt_02" size="25" readonly="readonly" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
            <tr>
                <td height="22" align="left" class="font8">
                    <td align="right">
                        &nbsp;
                    </td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
