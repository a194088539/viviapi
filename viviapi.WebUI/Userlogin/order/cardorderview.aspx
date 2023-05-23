<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cardorderview.aspx.cs"
    Inherits="viviapi.WebUI.Userlogin.order.cardorderview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            margin: 0px;
            font-family: tahoma,宋体,fantasy;
            font-size: 12px;
        }
        input
        {
            border: 1px solid #ccc;
            font-family: tahoma,宋体,fantasy;
            color: #333333;
            background: #FFF;
            font-size: 11px;
            height: 16px;
            line-height: 16px;
            padding-left: 3px;
        }
        .button
        {
            font-size: 9pt;
            height: 20px;
            border-width: 1px;
            cursor: hand;
            background: url(/merchant/static/style/images/cxbg.gif);
        }
        .Mir_List
        {
            width: 100%;
            height: auto;
            margin-top: 5px;
        }
        .Mir_List .M_Item
        {
            width: 100%;
            height: 25px;
            overflow: hidden;
            background: #F5F5F5;
        }
        .Mir_List .Pub
        {
            background: #ffffff;
        }
        .Mir_List .Head
        {
            color: #fff;
            font-weight: bold;
        }
        .Mir_List .M_Item ul
        {
            padding: 0px;
            margin: 0px;
        }
        .Mir_List li
        {
            margin: 2px;
            height: 25px;
            line-height: 25px;
            float: left;
            list-style-type: none;
        }
        .Mir_List .li_0
        {
            text-align: right;
            width: 82px;
            overflow: hidden;
        }
        .Mir_List .li_1
        {
            overflow: hidden;
        }
        .Mir_List .li_2
        {
            text-align: right;
            width: 100px;
            overflow: hidden;
        }
        .Mir_List .li_3
        {
            overflow: hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class='Mir_List'>
        <div class='M_Item Pub'>
            <ul>
                <li class='li_0'>系统订单号：</li>
                <li class='li_1'>
                    <asp:Label ID="lblorderid" runat="server" Width="160px"></asp:Label></li>
                <li class='li_2'>订单类别：</li>
                <li class='li_3'>
                    <asp:Label ID="lblordertype" runat="server" Width="160px"></asp:Label></li>
            </ul>
        </div>
        <div class='M_Item'>
            <ul>
                <li class='li_0'>通道类型：</li>
                <li class='li_1'>
                    <asp:Label ID="lbltypeId" runat="server" Width="160px"></asp:Label></li>
                <li class='li_2'>银行名称：</li>
                <li class='li_3'>
                    <asp:Label ID="lblpaymodeId" runat="server" Width="160px"></asp:Label></li>
            </ul>
        </div>
        <div class='M_Item Pub'>
            <ul>
                <li class='li_0'>商户订单号：</li>
                <li class='li_1'>
                    <asp:Label ID="lbluserorder" runat="server" Width="160px"></asp:Label></li>
                <li class='li_2'>用户提交金额：</li>
                <li class='li_3'>
                    <asp:Label ID="lblrefervalue" runat="server" Width="160px"></asp:Label></li>
            </ul>
        </div>
        <div class='M_Item'>
            <ul>
                <li class='li_0'>通知总次数：</li>
                <li class='li_1'>
                    <asp:Label ID="lblnotifycount" runat="server" Width="160px"></asp:Label></li>
                <li class='li_2'>通知状态：</li>
                <li class='li_3'>
                    <asp:Label ID="lblnotifystat" runat="server" Width="160px"></asp:Label></li>
            </ul>
        </div>
        <div class='M_Item Pub'>
            <ul>
                <li class='li_0'>接口：</li>
                <li class='li_1'>
                    <asp:Label ID="lblversion" runat="server" Width="160px"></asp:Label></li>
                <li class='li_2'>异步返回内容：</li>
                <li class='li_3'>
                    <asp:Label ID="lblnotifycontext" runat="server"></asp:Label></li>
            </ul>
        </div>
        <div class='M_Item'>
            <ul>
                <li class='li_0'>备注：</li>
                <li class='li_1'>
                    <asp:Label ID="lblattach" runat="server" Width="160px"></asp:Label>
                </li>
                <li class='li_2'>支付者IP：</li>
                <li class='li_3'>
                    <asp:Label ID="lblpayerip" runat="server" Width="160px"></asp:Label></li>
            </ul>
        </div>
        <div class='M_Item Pub'>
            <ul>
                <li class='li_0'>新增时间：</li>
                <li class='li_1'>
                    <asp:Label ID="lbladdtime" runat="server" Width="160px"></asp:Label></li>
                <li class='li_2'>订单状态：</li>
                <li class='li_3'>
                    <asp:Label ID="lblstatus" runat="server" Width="160px"></asp:Label></li>
            </ul>
        </div>
        <div class='M_Item'>
            <ul>
                <li class='li_0'>实际金额：</li>
                <li class='li_1'>
                    <asp:Label ID="lblrealvalue" runat="server" Width="160px"></asp:Label></li>
                <li class='li_2'>商家费率：</li>
                <li class='li_3'>
                    <asp:Label ID="lblpayRate" runat="server" Width="160px"></asp:Label></li>
            </ul>
        </div>
        <div class='M_Item Pub'>
            <ul>
                <li class='li_0'>商家金额：</li>
                <li class='li_1'>
                    <asp:Label ID="lblpayAmt" runat="server" Width="160px"></asp:Label></li>
                <li class='li_2'>完成时间：</li>
                <li class='li_3'>
                    <asp:Label ID="lblcompletetime" runat="server" Width="160px"></asp:Label></li>
            </ul>
        </div>
        <div class='M_Item'>
            <ul>
                <li class='li_0'>提交地址：</li>
                <li class='li_1'><a href="<%=referUrl%>" target="_blank">
                    <%=CutWord(referUrl,30)%></a>&nbsp;<input value="复制地址" type="button" class="button"
                        onclick="copyToClipboard('<%=referUrl%>')" /></li>
            </ul>
        </div>
        <div class='M_Item Pub'>
            <ul>
                <li class='li_0'>异步通知：</li>
                <li class='li_1'><a href="<%=notifyurl%>" target="_blank">
                    <%=CutWord(notifyurl, 30)%></a>&nbsp;<input value="复制地址" type="button" class="button"
                        onclick="copyToClipboard('<%=notifyurl%>')" /></li>
            </ul>
        </div>
        <div class='M_Item Pub'>
            <ul>
                <li class='li_0'>卡号：</li>
                <li class='li_1'>
                    <asp:Label ID="lblcardno" runat="server" Width="160px"></asp:Label></li>
                <li class='li_2'>卡密：</li>
                <li class='li_3'>
                    <asp:Label ID="lblcardpwd" runat="server" Width="160px"></asp:Label></li>
            </ul>
        </div>
        <div class='M_Item'>
            <ul>
                <li class='li_0'>备注：</li>
                <li class='li_1'>
                    <li class='li_1'>
                        <asp:Label ID="lblmessage" runat="server" Width="300px"></asp:Label></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>

<script type="text/javascript" language="JavaScript">
    var table = document.getElementById("table_zyads");
    if (table) {
        for (i = 0; i < table.rows.length; i++) {
            if (i % 2 == 0) {
                table.rows[i].bgColor = "ffffff";
            } else { table.rows[i].bgColor = "f5f5f5" }
        }
    }
    function copyToClipboard(txt) {
        if (window.clipboardData) {
            window.clipboardData.clearData();
            window.clipboardData.setData("Text", txt);
            alert("成功复制到剪贴板！");
        } else if (navigator.userAgent.indexOf("Opera") != -1) {
            window.location = txt;
        } else if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            } catch (e) {
                alert("被浏览器拒绝！\n请在浏览器地址栏输入'about:config'并回车\n然后将'signed.applets.codebase_principal_support'设置为'true'");
            }
            var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
            if (!clip)
                return;
            var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
            if (!trans)
                return;
            trans.addDataFlavor('text/unicode');
            var str = new Object();
            var len = new Object();
            var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
            var copytext = txt;
            str.data = copytext;
            trans.setTransferData("text/unicode", str, copytext.length * 2);
            var clipid = Components.interfaces.nsIClipboard;
            if (!clip)
                return false;
            clip.setData(trans, null, clipid.kGlobalClipboard);
            alert("成功复制到剪贴板！");
        }
    }

</script>

