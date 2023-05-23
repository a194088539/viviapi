<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feedbackview.aspx.cs" Inherits="viviapi.WebUI.LongBao.merchant.feedbackview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
body {margin:0px;font-family:tahoma,宋体,fantasy;font-size:12px;}
input {border: 1px solid #ccc;font-family:tahoma,宋体,fantasy;color: #333333;background:#FFF;font-size:11px;height:16px;line-height:16px;padding-left:3px}
.button {font-size:9pt;height:20px;border-width:1px;cursor:hand;background:url(/merchant/images/cxbg.gif);}
.Mir_List{width:100%;height:auto;margin-top:5px}
.Mir_List .M_Item{width:100%;height:25px;overflow:hidden;background:#F5F5F5;}
.Mir_List .Pub{background:#ffffff;}
.Mir_List .Head{color:#fff;font-weight: bold;}
.Mir_List .M_Item ul{padding:0px;margin:0px;}
.Mir_List li{margin:2px;height:25px;line-height:25px;float:left;list-style-type:none;}
.Mir_List .li_0{text-align:right;width:82px;overflow:hidden;}
.Mir_List .li_1{overflow:hidden;}
.Mir_List .li_2{text-align:right;width:100px;overflow:hidden;}
.Mir_List .li_3{overflow:hidden;}
</style>
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
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                   <tr>
                                     <td align="left" valign="top" style="font-family:'微软雅黑'; color:#333333; font-size:13px;">类型：<asp:Literal 
                                             ID="lit_typeid" runat="server"></asp:Literal>
                                       </td>
                                     <td align="left" valign="top" style="font-family:'微软雅黑'; color:#999999;">问题或建议：<asp:Literal 
                                             ID="lit_title" runat="server"></asp:Literal></td>
                                   </tr>
                                   <tr>
                                     <td align="left" valign="top" style="font-family:'微软雅黑'; color:#333333; font-size:13px;">具体描述：<asp:Literal 
                                             ID="lit_cont" runat="server"></asp:Literal>
                                       </td>
                                     <td align="left" valign="top" style="font-family:'微软雅黑'; color:#999999;">IP：<asp:Literal 
                                             ID="lit_clientip" runat="server"></asp:Literal></td>
                                   </tr>
                                   <tr>
                                     <td colspan="2" align="left" valign="top" style="font-family:'微软雅黑'; color:#333333;">管理员回复：
                                     <asp:Literal 
                                             ID="lit_reply" runat="server"></asp:Literal>
                                     </td>
                                   </tr>
                                 </table>
    </form>
</body>
</html>

