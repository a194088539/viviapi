<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="modiphone.aspx.cs" Inherits="viviapi.WebUI.Userlogin.safety.modiphone" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/page.css" />
    <script src="/Userlogin/static/js/lib/jquery-1.4.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function() {
            $("#yphoneputbox").fadeOut();
            $("#phoneputbox").fadeOut();
            if ($("#IsPhonePass").val() == "1")
                $("#phonecodebox").fadeOut();

            $("#phoneinput a").click(function() {
                $("#phoneinput").fadeOut();
                $("#phoneputbox").fadeIn();
                $("#yphoneputbox").fadeIn();
                $("#phonecodebox").fadeIn();
                $('#phonetxt').text("新手机号码");
                $('#formflag').html("")
                $('#action').val("modiphone"); //add by vivisoft
            });
            $("#phoneinput_close a").click(function() {
                $("#phoneinput").fadeIn();
                $("#phoneputbox").fadeOut();
                $("#yphoneputbox").fadeOut();
                $("#phonecodebox").fadeOut();
                $('#phonetxt').text("手机号码");
                $('#formflag').html("")
                $('#action').val("renew"); //add by vivisoft
            });
            var okico = "";
            var errico = "";
            var ldico = "";

            $("a#sendmsg").click(function() {
                var phoneno = $("#phone").val();
                if (phoneno == null || phoneno == "") {
                    $("#lblMessage").html("请输入手机号码");
                    return;
                }
                $("#lblMessage").html(ldico + "正在发送验证码");
                $.get("/Userlogin/Ajax/PhoneValid_new.ashx?t=" + Math.random(), { phone: phoneno },
                    function(data, textStatus) {
                        if (data == "true") {
                            $("#lblMessage").html("验证码发送成功!");
                        } else {
                            $("#lblMessage").css({
                                color: "red"
                            });
                            $("#lblMessage").html(data + "");
                        }
                    });
            });
        }); 
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        //<![CDATA[
        var theForm = document.forms['form1'];
        if (!theForm) {
            theForm = document.form1;
        }
        function __doPostBack(eventTarget, eventArgument) {
            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                theForm.__EVENTTARGET.value = eventTarget;
                theForm.__EVENTARGUMENT.value = eventArgument;
                theForm.submit();
            }
        }
        //]]>
</script>
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/usermodule/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/usermodule/account/index.aspx'">我的账户</a>
        &nbsp;&gt;&nbsp; <span>手机认证</span>
    </div>
   <input id="IsPhonePass" runat="server" value="0" type="hidden" /> 
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title" style="color: Black;">
            <h2>
                手机认证</h2>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01" style="color: Black; border: none;">
                    手机认证流程 输入手机号码 - 发送验证码 - 输入验证码确认 - 认证成功
                </td>
            </tr>
            <tr id="yphoneputbox">
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;
                    color: Black;" width="150">
                    原手机号码：
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none; width: 240px;">
                        <input id="yphone" runat="server" type="text" class="txt_01" size="50" />
                </td>
                <td height="45" align="left" class="line_01" style="border: none;">
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;
                    color: Black;">
                    手机号码：
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                   <asp:Literal ID="litphone" runat="server"></asp:Literal>
                </td>
                <td height="45" align="left" class="line_01" style="border: none;">
                </td>
            </tr>
            <tr id="phonecodebox">
                <td height="45" align="right" class="line_01" style="border: none; font-weight: bold;
                    color: Black;">
                    手机验证码：
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                   <input id="phonecode" runat="server" type="text" class="txt_01" size="20" />
                </td>
                <td height="45" align="left" class="line_01" style="border: none;">
                    
                </td>
            </tr>
            <tr>
                <td height="45" align="right" class="line_01" style="border: none;">
                </td>
                <td align="left" class="line_01" style="padding-left: 15px; border: none;">
                    <asp:Button ID="btnSave" runat="server" Text="确认提交" CssClass="btn btn-primary" 
                        onclick="btnSave_Click" />&nbsp;
                    <a href="/usermodule/account/safety.aspx" target="mainframe" class="btn btn-primary">
                        取消</a>
                    <span id="lblMessage"></span>
                </td>
                <td height="45" align="left" class="line_01" style="border: none;">
                </td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
