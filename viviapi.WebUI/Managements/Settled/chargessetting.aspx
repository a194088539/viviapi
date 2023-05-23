<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.chargessetting"
    ValidateRequest="false" Codebehind="chargessetting.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>手续费设置</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        table
        {
            font-weight: normal;
            line-height: 170%;
            font-family: Arial;
        }
        A:link
        {
            color: #237C04;
            text-decoration: none;
        }
        td
        {
            height: 20px;
            line-height: 20px;
            font-size: 12px;
            padding: 0px;
        }
        .td_title, th
        {
            height: 20px;
            line-height: 22px;
            font-weight: bold;
            border: 0px solid #fff;
            text-align: left;
        }
        .td1
        {
            padding-right: 3px;
            padding-left: 3px;
            color: #999999;
            padding-bottom: 0px;
            padding-top: 5px;
            height: 25px;
        }
        .td2
        {
            padding-right: 3px;
            padding-left: 8px;
            padding-top: 5px;
            color: #083772;
            background: #EFF3FB;
            font-size: 12px;
            text-align: right;
        }
        .td3
        {
            padding: 1px 1px 0 0px;
            color: #083772;
            background: #EFF3FB;
            font-size: 12px;
            text-align: center;
        }
        .moban
        {
            padding-top: 0px;
            border: 0px;
        }
        input
        {
            border: 1px solid #999;
            padding: 3px;
            margin-left: 10px;
            font: 12px tahoma;
            ling-height: 16px;
        }
        .input4
        {
            border: 1px solid #999;
            padding: 3px;
            margin-left: 10px;
            font: 11px tahoma;
            ling-height: 16px;
            height: 45px;
        }
        .button
        {
            color: #135294;
            border: 1px solid #666;
            height: 21px;
            line-height: 21px;
        }
        .nrml
        {
            background-color: #eeeeee;
            font-weight: bold;
        }
        .radio
        {
            border: none;
        }
        .checkbox
        {
            border: none;
        }
        .addnew
        {
            font-size: 12px;
            color: #FF0000;
        }
        a.servername
        {
            height: 470px;
            width: 527px;
            color: #E54202;
            cursor: hand;
        }
        .current
        {
            border: #ff6600 1px solid;
        }
        a:hover
        {
            height: 470px;
            width: 527px;
            color: #E54202;
            cursor: hand;
        }
        #nav LI A.noncurrent
        {
            /*border:#DC171E 3px solid;*/
        }
        #nav UL
        {
            padding-bottom: 0px;
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 0px;
        }
        #nav LI
        {
            display: inline;
            padding-left: 10px;
        }
        #nav LI a:hover
        {
            border: #B6E000 1px solid;
        }
        #nav li A:visited
        {
            border: #ff0000 1px solid;
        }
        img
        {
            border: #CCCCCC 1px solid;
            padding: 0 5px;
        }
        #tplPreview
        {
            position: absolute;
            top: 0px;
            left: 0px;
            background: #ffffff;
            border: 1px solid #333;
            font-size: 12px;
            color: #4B4B4B;
            padding: 12px 15px 15px 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" style="font-weight: bold; font-size: 14px; background: url(style/images/topbg.gif) repeat-x;
                color: teal; height: 28px">
                T+0 提现
            </td>
        </tr>
        <tr>
            <td width="10%" class="td2">
                最低提现金额限制：
            </td>
            <td width="90%" colspan="3" class="td1">
                <asp:TextBox ID="txtMinimum1" runat="server" Width="227px"></asp:TextBox> 单位：元
            </td>
        </tr>
        <tr>
            <td class="td2">
                最大提现金额限制：
            </td>
            <td colspan="3" class="td1">
                <asp:TextBox ID="txtMaximum1" runat="server" Width="227px"></asp:TextBox> 单位：元
            </td>
        </tr>
        <tr>
            <td class="td2">
                每天最多可提现次数：
            </td>
            <td colspan="3" class="td1">
                <asp:TextBox ID="txtCashTimes1" runat="server" Width="227px"></asp:TextBox> 单位：次/天
        </tr>
        <tr>
            <td class="td2">
                提现手续费：
            </td>
            <td class="td1" colspan="3">
                <asp:TextBox ID="txtCharges1" runat="server" Width="227px"></asp:TextBox> 单位：元
            </td>
        </tr>
        <tr>
            <td colspan="4" style="font-weight: bold; font-size: 14px; background: url(style/images/topbg.gif) repeat-x;
                color: teal; height: 28px">
                T+1 提现
            </td>
        </tr>
        <tr>
            <td width="10%" class="td2">
                最低提现金额限制：
            </td>
            <td width="90%" colspan="3" class="td1">
                <asp:TextBox ID="txtMinimum2" runat="server" Width="227px"></asp:TextBox> 单位：元
            </td>
        </tr>
        <tr>
            <td class="td2">
                最大提现金额限制：
            </td>
            <td colspan="3" class="td1">
                <asp:TextBox ID="txtMaximum2" runat="server" Width="227px"></asp:TextBox> 单位：元
            </td>
        </tr>
        <tr>
            <td class="td2">
                每天最多可提现次数：
            </td>
            <td colspan="3" class="td1">
                <asp:TextBox ID="txtCashTimes2" runat="server" Width="227px"></asp:TextBox>单位：次/天
            </td>
        </tr>
        <tr>
            <td class="td2">
                提现手续费：
            </td>
            <td class="td1" colspan="3">
                <asp:TextBox ID="txtCharges2" runat="server" Width="227px"></asp:TextBox> 单位：元
            </td>
        </tr>
        <tr>
            <td colspan="4" style="font-weight: bold; font-size: 14px; background: url(style/images/topbg.gif) repeat-x;
                color: teal; height: 28px">
                T+7 7天以后到帐：
            </td>
        </tr>
        <tr>
            <td width="10%" class="td2">
                最低提现金额限制：
            </td>
            <td width="90%" colspan="3" class="td1">
                <asp:TextBox ID="txtMinimum3" runat="server" Width="227px"></asp:TextBox> 单位：元
            </td>
        </tr>
        <tr>
            <td class="td2">
                最大提现金额限制：
            </td>
            <td colspan="3" class="td1">
                <asp:TextBox ID="txtMaximum3" runat="server" Width="227px"></asp:TextBox> 单位：元
            </td>
        </tr>
        <tr>
            <td class="td2">
                每天最多可提现次数：
            </td>
            <td colspan="3" class="td1">
                <asp:TextBox ID="txtCashTimes3" runat="server" Width="227px"></asp:TextBox> 单位：次/天
            </td>
        </tr>
        <tr>
            <td class="td2">
                提现手继费：
            </td>
            <td class="td1" colspan="3">
                <asp:TextBox ID="txtCharges3" runat="server" Width="227px"></asp:TextBox> 单位：元
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td class="td1" colspan="3">
                <span style="padding-left: 3px; height: 40px">
                    <asp:Button ID="btn_Update" runat="server" Text="设置" OnClick="btnUpdate_Click" />
                </span>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
