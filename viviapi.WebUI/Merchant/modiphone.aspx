<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="modiphone.aspx.cs" Inherits="viviapi.WebUI.Merchant.modiphone" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    jQuery(document).ready(function() {
        $("#yphoneputbox").fadeOut();
        $("#phoneputbox").fadeOut();
        if ($("#ctl00_ContentPlaceHolder1_IsPhonePass").val() == "1")
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
            $("#callinfo").html(ldico + "正在发送验证码");
            $.get("/merchant/ajax/phonevalid.ashx?t=" + Math.random(), $("#aspnetForm").serialize(),
        function(data, textStatus) {
            if (data == "true") {
                $("#callinfo").html(okico + "验证码发送成功!")
            } else {
                $("#callinfo").css({
                    color: "red"
                });
                $("#callinfo").html(errico + data + "")
            }
        })
        });
    }); 
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="IsPhonePass" runat="server" value="0" type="hidden" />  
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            手机认证&nbsp;<img id="loading" width="0" height="0" src="/merchant/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01">
                    手机认证流程 输入手机号码 - 发送验证码 - 输入验证码确认 - 认证成功
                </td>
            </tr>
            <tr id="yphoneputbox">
                <td height="39" align="left" class="line_01">
                    原手机号码:
                </td>
                <td align="left" class="line_01">
                    <input id="yphone" runat="server" type="text" class="txt_02" size="50" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    手机号码:
                </td>
                <td align="left" class="line_01">
                    <asp:Literal ID="litphone" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
             <tr id="phonecodebox">
                <td height="39" align="left" class="line_01">
                    手机验证码:
                </td>
                <td align="left" class="line_01">
                    <input id="phonecode" runat="server" type="text" class="txt_02" size="20" />
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
                     <asp:Button ID="btnSave" runat="server" Text="确认提交" CssClass="btn btn-primary" 
                        onclick="btnSave_Click" />&nbsp;
                &nbsp;<span class="txtr" id="callinfo"></span>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
