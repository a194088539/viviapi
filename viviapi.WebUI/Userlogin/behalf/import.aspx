<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="import.aspx.cs" Inherits="viviapi.WebUI.Userlogin.behalf.import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            border-bottom: 1px solid #E2E8F2;
            font-size: 13px;
            font-family: "微软雅黑";
            width: 26%;
        }
    </style>
      <script src="/Userlogin/static/js/lib/jquery-1.4.2.js" type="text/javascript"></script>
<!--
    <script type="text/javascript">
        jQuery(document).ready(function() {
            $("#yphoneputbox").fadeOut();
            $("#phoneputbox").fadeOut();
            if ($("#ctl00_ContentPlaceHolder1_IsPhonePass").val() == "1")
                $("#phonecodebox").fadeOut();

            $("a#sendmsg").click(function() {
                $("#callinfo").html("正在发送验证码");
                $.get("/Userlogin/ajax/SendVerifyCode.ashx?t=" + Math.random(), $("#aspnetForm").serialize(),
        function(data, textStatus) {
            if (data == "true") {
                $("#callinfo").html("验证码发送成功!")
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
-->
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/behalf/index.aspx'">对私代发</a>
        &nbsp;&gt;&nbsp; <span>代发上传</span>
    </div>
    <div id="list_content">
        <div id="title">
            文件上传&nbsp;<img id="loading" width="0" height="0" src="/Userlogin/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="right" class="style1">
                    代发需知：
                </td>
                <td class="line_01">
                    1、请参照代发模板文件，按格式填写本批次的代发明细信息。 <a href="download/templetfile.xlsx">下载</a>代发模板文件
                    <br />
                    2、目前支持的收款银行有16家，分别为：工商银行、农业银行、建设银行、交通银行、招商银行、中国银行、邮政储蓄银行、民生银行、华夏银 行、兴业银行、广发银行、浦发银行、光大银行、中信银行、平安银行、杭州银行。
                    <br />
                    3、关于单笔代发限额：工、农、建、交四家收款银行最高20万元，其他银行最高5万元。
                    <br />
                    4、按照反洗钱规定，代发金额超过1万元的，请自行保存代发目标用户的真实有效身份信息，要做到随时可查。代发金额超过5万元的，请跟工作人员联系。工作人员据此审核这些代发明细。
                    <br />
                    5、账户余额应>=代发金额合计+代发手续费合计。 查看当前费率设置
                </td>
            </tr>
            <tr>
                <td z align="right" class="style1">
                </td>
                <td align="left" class="line_01">
                    <asp:CheckBox ID="cbx_sure" runat="server" Text="以上内容我已知悉" AutoPostBack="true" OnCheckedChanged="cbx_sure_CheckedChanged" />
                </td>
            </tr>
<!--
            <tr>
                <td height="39" align="right" class="style1">
                    验证码：</td>
                <td style="width: 75%" align="left" class="line_01">
                     <asp:TextBox ID="TextEmail" runat="server" CssClass="txt_01" TextMode="Password"
                        MaxLength="25"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:;" id="sendmsg">发送验证码</a>
                </td>
            </tr>
-->
            <tr>
                <td height="39" align="right" class="style1">
                    提现密码：</td>
                <td style="width: 75%" align="left" class="line_01">
                    <asp:TextBox ID="txtcashpwd" runat="server" CssClass="txt_01" TextMode="Password"
                        MaxLength="25"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td height="39" align="right" class="style1">
                </td>
                <td style="width: 75%" align="left" class="line_01">
                    <asp:FileUpload ID="file_data" runat="server" class="mutitxt_03" Width="80%" />
                
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="style1">
                </td>
                <td style="width: 75%">
                    <asp:Button ID="btnupload" runat="server" Text="确定上传" CssClass="btn btn-primary" OnClick="btnupload_Click" />
                   &nbsp;&nbsp; <span class="txtr" id="callinfo" runat="server" style="color: Red; font-weight: bold"></span>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
