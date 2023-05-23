<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsEdit.aspx.cs" Inherits="viviapi.WebUI.LongBao.console.news.WebForm1"
    ValidateRequest="false" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />
    <meta name="keywords" content="" />
    <meta name="description" content="">
    <title></title>

    <script type="text/javascript" charset="utf-8">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>

    <script type="text/javascript" charset="utf-8" src="../../ueditor/ueditor.config.js"></script>

    <script type="text/javascript" charset="utf-8" src="../../ueditor/ueditor.all.js"></script>

    <script src="../../js/common.js" type="text/javascript"></script>

    <script src="../../Js/ControlDate/WdatePicker.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hfcontent" runat="server" />
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" style="font-weight: bold; font-size: 14px; background: url(../style/images/topbg.gif) repeat-x;
                color: teal; height: 28px">
                新闻信息编辑
            </td>
        </tr>
        <tr>
            <td>
                <table style="border: 1px;" cellpadding="0" cellspacing="0" class="table01">
                    <tr>
                        <td align="center" style="width: 118px; height: 40px;">
                            标题：
                        </td>
                        <td align="left" style="width: 550px; height: 40px;">
                            <asp:TextBox ID="txtTitle" runat="server" Width="220px"></asp:TextBox>
                            <asp:CheckBox ID="cb_red" runat="server" Text="加红" CssClass="label" Visible="false">
                            </asp:CheckBox>
                            <asp:CheckBox ID="cb_bold" runat="server" Text="加粗" CssClass="label"></asp:CheckBox>
                            <asp:CheckBox ID="cb_top" runat="server" Text="置顶" CssClass="label"></asp:CheckBox>
                            <asp:CheckBox ID="cb_pop" runat="server" Text="弹出" CssClass="label"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 118px; height: 40px;">
                            标题颜色：
                        </td>
                        <td align="left" style="width: 550px; height: 40px;">
                            <asp:RadioButtonList ID="rbColor" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="FF0000">&lt;span style=&quot; color:#FF0000&quot;&gt;#FF0000&lt;/span&gt;</asp:ListItem>
                                <asp:ListItem Value="990000">&lt;span style=&quot; color:#990000&quot;&gt;#990000&lt;/span&gt;</asp:ListItem>
                                <asp:ListItem Value="FF0066">&lt;span style=&quot; color:#FF0066&quot;&gt;#FF0066&lt;/span&gt;</asp:ListItem>
                                <asp:ListItem Value="FF3300">&lt;span style=&quot; color:#FF3300&quot;&gt;#FF3300&lt;/span&gt;</asp:ListItem>
                            </asp:RadioButtonList>
                            <br />
                            颜色代码：<asp:TextBox ID="txtColorCode" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 118px; height: 40px;">
                            类型：
                        </td>
                        <td align="left" style="width: 550px; height: 40px;">
                            <asp:DropDownList ID="ddl_type" runat="server" Width="93px">
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            内容：
                        </td>
                        <td align="left">

                            <script type="text/plain" id="editor"><%=newscontent%></script>

                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 118px; height: 40px;">
                            添加日期：
                        </td>
                        <td align="left" style="width: 550px; height: 40px;">
                            <input id="txtDate" runat="server" readonly="readonly" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 118px; height: 40px;">
                            是否发布：
                        </td>
                        <td align="left" style="width: 550px; height: 40px;">
                            <asp:RadioButtonList ID="rbl_Release" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 118px; height: 25px;">
                        </td>
                        <td align="left" style="width: 550px; height: 25px;">
                            <asp:Button ID="BtnSubmit" runat="server" Text="提  交" OnClick="BtnSubmit_Click" OnClientClick="return getContent();" />
                            <input type="button" value="返  回" onclick="javascript:location.href='NewsList.aspx'" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript">
        $().ready(function() {
            var color = $("input[name$='txtColorCode']").val();
            if (color != null && color.length > 0) {
                $("input[name$='txtTitle']").css("color", color);
            }
            var isbold = $("#cb_bold").attr("checked");
            if (isbold == true) {
                $("input[name$='txtTitle']").css("font-weight", "bold");
            }
            $("input[name='rbColor']").click(function() {
                $("input[name$='txtColorCode']").val("#" + $(this).val());
                $("input[name$='txtTitle']").css("color", "#" + $(this).val());
            })
            $("#cb_bold").click(function() {
                var isbold = $(this).attr("checked");
                if (isbold == true) {
                    $("input[name$='txtTitle']").css("font-weight", "bold");
                }
                else {
                    $("input[name$='txtTitle']").css("font-weight", "");
                }
            })
            // $("#editor").text($("#hfcontent").val());
        });
        function backreturn() {
            location.href = 'SupplierList.aspx';
        }
    </script>

    <script type="text/javascript">
        //实例化编辑器
        var options = {
            lang: /^zh/.test(navigator.language || navigator.browserLanguage || navigator.userLanguage) ? 'zh-cn' : 'en',
            langPath: UEDITOR_HOME_URL + "lang/",

            webAppKey: "9HrmGf2ul4mlyK8ktO2Ziayd",
            initialFrameWidth: 860,
            initialFrameHeight: 420,
            focus: true
        };
        var ue = UE.getEditor('editor', options);
        var domUtils = UE.dom.domUtils;

        ue.addListener("ready", function() {
            ue.focus(true);
        });
        function setLanguage(obj) {
            var value = obj.value,
                opt = {
                    lang: value
                };
            UE.utils.extend(opt, options, true);

            UE.delEditor("editor");
            //清空语言
            if (!UE.I18N[opt.lang]) {
                UE.I18N = {};
            }
            UE.getEditor('editor', opt);
        }
        function createEditor() {
            enableBtn();
            UE.getEditor('editor', {
                initialFrameWidth: "100%"
            })
        }
        function getAllHtml() {
            alert(UE.getEditor('editor').getAllHtml())
        }
        function getContent() {
            //        var arr = [];
            //        arr.push("使用editor.getContent()方法可以获得编辑器的内容");
            //        arr.push("内容为：");
            //        arr.push(UE.getEditor('editor').getContent());
            //        alert(arr.join("\n"));
            var _val = UE.getEditor('editor').getContent();
            $("#hfcontent").val(_val);
            return true;
        }
        function getPlainTxt() {
            var arr = [];
            arr.push("使用editor.getPlainTxt()方法可以获得编辑器的带格式的纯文本内容");
            arr.push("内容为：");
            arr.push(UE.getEditor('editor').getPlainTxt());
            alert(arr.join('\n'))
        }
        function setContent() {
            var arr = [];
            arr.push("使用editor.setContent('欢迎使用ueditor')方法可以设置编辑器的内容");
            UE.getEditor('editor').setContent('欢迎使用ueditor');
            alert(arr.join("\n"));
        }
        function setDisabled() {
            UE.getEditor('editor').setDisabled('fullscreen');
            disableBtn("enable");
        }

        function setEnabled() {
            UE.getEditor('editor').setEnabled();
            enableBtn();
        }

        function getText() {
            //当你点击按钮时编辑区域已经失去了焦点，如果直接用getText将不会得到内容，所以要在选回来，然后取得内容
            var range = UE.getEditor('editor').selection.getRange();
            range.select();
            var txt = UE.getEditor('editor').selection.getText();
            alert(txt)
        }

        function getContentTxt() {
            var arr = [];
            arr.push("使用editor.getContentTxt()方法可以获得编辑器的纯文本内容");
            arr.push("编辑器的纯文本内容为：");
            arr.push(UE.getEditor('editor').getContentTxt());
            alert(arr.join("\n"));
        }
        function hasContent() {
            var arr = [];
            arr.push("使用editor.hasContents()方法判断编辑器里是否有内容");
            arr.push("判断结果为：");
            arr.push(UE.getEditor('editor').hasContents());
            alert(arr.join("\n"));
        }
        function setFocus() {
            UE.getEditor('editor').focus();
        }
        function deleteEditor() {
            disableBtn();
            UE.getEditor('editor').destroy();
        }
        function disableBtn(str) {
            var div = document.getElementById('btns');
            var btns = domUtils.getElementsByTagName(div, "input");
            for (var i = 0, btn; btn = btns[i++]; ) {
                if (btn.id == str) {
                    domUtils.removeAttributes(btn, ["disabled"]);
                } else {
                    btn.setAttribute("disabled", "true");
                }
            }
        }
        function enableBtn() {
            var div = document.getElementById('btns');
            var btns = domUtils.getElementsByTagName(div, "input");
            for (var i = 0, btn; btn = btns[i++]; ) {
                domUtils.removeAttributes(btn, ["disabled"]);
            }
        }
    </script>

</body>
</html>
