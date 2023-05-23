<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Order.BankOrderShow" Codebehind="BankOrderShow.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style/union.css" type="text/css" rel="stylesheet" />

    <script src="../../js/common.js" type="text/javascript"></script>

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
            width: 35%;
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
            width: 15%;
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
        .lable
        {
            border: 1px solid #999;
            padding: 3px;
            margin-left: 10px;
            font: 12px tahoma;
            ling-height: 16px;
        }
        select
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
            <td colspan="4" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 24px">
                网银订单信息查看
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="td2">
                订单编号：
            </td>
            <td class="td1">
                <asp:Label ID="lblid" runat="server"></asp:Label>
            </td>
            <td class="td2">
                系统订单号 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblorderid" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                订单类别 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblordertype" runat="server"></asp:Label>
            </td>
            <td class="td2">
                用户信息 ：
            </td>
            <td class="td1">
                <asp:Label ID="lbluserid" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                通道类型 ：
            </td>
            <td class="td1">
                <asp:Label ID="lbltypeId" runat="server"></asp:Label>
            </td>
            <td class="td2">
                银行名称 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblpaymodeId" runat="server"></asp:Label>
            </td>
        </tr>
        <tr >
            <td class="td2">
                商户订单号 ：
            </td>
            <td class="td1">
                <asp:Label ID="lbluserorder" runat="server"></asp:Label>
            </td>
            <td class="td2">
                用户提交金额 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblrefervalue" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="display:none">
            <td class="td2">
                下行异步通知地址 ：
            </td>
            <td class="td1">
                
            </td>
            <td class="td2">
                下行异步通知地址 ：
            </td>
            <td class="td1">
                
            </td>
        </tr>
        <tr>
            <td class="td2">
                异步通知总次数 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblnotifycount" runat="server"></asp:Label>
            </td>
            <td class="td2">
                通知状态 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblnotifystat" runat="server"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="td2">
                接口 ：
            </td>
            <td class="td1" colspan="4">
                <asp:Label ID="lblversion" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                异步返回内容 ：
            </td>
            <td class="td1" colspan="4">
                <asp:Label ID="lblnotifycontext" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                备注消息 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblattach" runat="server"></asp:Label>
            </td>
            <td class="td2">
                支付者IP ：
            </td>
            <td class="td1">
                <asp:Label ID="lblpayerip" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                传送IP ：
            </td>
            <td class="td1">
                <asp:Label ID="lblclientip" runat="server"></asp:Label>
            </td>
            <td class="td2">
                新增时间 ：
            </td>
            <td class="td1">
                <asp:Label ID="lbladdtime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>            
            <td class="td2">
                通道厂商 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblsupplierId" runat="server"></asp:Label>
            </td>
             <td class="td2">
                通道商订单号 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblsupplierOrder" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>           
            <td class="td2">
                订单状态：
            </td>
            <td class="td1">
                <asp:Label ID="lblstatus" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
            </td>
             <td class="td2">
                实际金额：
            </td>
            <td class="td1">
                <asp:Label ID="lblrealvalue" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>           
            <td class="td2">
                商家费率：
            </td>
            <td class="td1">
                <asp:Label ID="lblpayRate" runat="server"></asp:Label>
            </td>
            <td class="td2">
                商家金额 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblpayAmt" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                 平台费率 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblsupplierRate" runat="server"></asp:Label>
            </td>
            <td class="td2">
                平台金额 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblsupplierAmt" runat="server"></asp:Label>
            </td>           
        </tr>
        <tr>
             <td class="td2">
                代理费率 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblpromRate" runat="server"></asp:Label>
            </td>
            <td class="td2">
                代理金额 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblpromAmt" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>            
            <td class="td2">
                平台利润 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblprofits" runat="server"></asp:Label>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td class="td2">
                提交的服务器 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblserver" runat="server"></asp:Label>
            </td>
            <td class="td2">
                完成时间 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblcompletetime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">提交地址 ：
            </td>
            <td colspan="4" class="td1">
                <asp:Label ID="lblreferUrl" runat="server"></asp:Label>
        </tr>
        <tr>
            <td class="td2">异步通知 ：
            </td>
            <td colspan="4" class="td1">
                <asp:Label ID="lblnotifyurl" runat="server"></asp:Label>
        </tr>
         <tr>
            <td class="td2">同步返回 ：
            </td>
            <td colspan="4" class="td1">
                <asp:Label ID="lblreturnurl" runat="server"></asp:Label>
        </tr>
        <tr>
            <td class="td2">异步通知 ：
            </td>
            <td colspan="4" class="td1" style="word-wrap:break-word;">
                <asp:Literal ID="litNotify" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td colspan="4" style="height: 20px">
                <div align="center">
                <br />                    
                    <input type="button" value="关 闭" onclick="javascript:window.close()" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
