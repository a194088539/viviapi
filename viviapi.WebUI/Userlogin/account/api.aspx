<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="api.aspx.cs" Inherits="viviapi.WebUI.Userlogin.account.api" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/page.css" />
    <style>
        .alert
        {
            padding: 8px 10px 8px 14px;
            margin-top: 10px;
            text-shadow: 0 1px 0 rgba(255,255,255,0.5);
            background-color: #fcf8e3;
            border: 1px solid #fbeed5;
            -webkit-border-radius: 2px;
            -moz-border-radius: 2px;
            border-radius: 2px;
            color: #c09853;
        }
        .alert-info
        {
            background-color: #d9edf7;
            border-color: #bce8f1;
            color: #3a87ad;
        }
        p
        {
            margin: 0 0 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/account/index.aspx'">我的账户</a>
        &nbsp;&gt;&nbsp; <span>接口信息</span>
    </div>
    <!--右部表单开始-->
    <div id="list_content">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="50" colspan="3" align="center" class="line_01" valign="top">
                    <img alt="" src="/Userlogin/images/apitop.png" />
                </td>
            </tr>
            <tr>
                <td height="50" align="right" class="line_01" style="font-weight: bold; font-size: 16px;
                    color: Black; width: 150px;">
                    商户ID号：
                </td>
                <td align="left" class="line_01" style="padding-left: 20px;">
                    <span style="background-color: #488a4c; color: White; font-weight: bold; padding: 2px 5px 2px 5px;">
                        <%= getuserid %></span>
                </td>
                <td height="50" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="50" align="right" class="line_01" style="font-weight: bold; font-size: 16px;
                    color: Black; width: 150px;">
                    商户密钥(KEY)：
                </td>
                <td align="left" class="line_01" style="padding-left: 20px;">
                    <span style="background-color: #fd960b; color: White; font-weight: bold; padding: 2px 5px 2px 5px;">
                        <%= getapikey %></span>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnModiKey" runat="server" Text="重置密钥" CssClass="btn btn-primary"
                        OnClick="btnModiKey_Click" />
                </td>
                <td height="50" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="50" align="right" class="line_01" style="font-weight: bold; font-size: 16px;
                    color: Black; width: 150px; border: none;">
                    接口文档：
                </td>
                <td align="left" class="line_01" style="padding-left: 20px; border: none;">
                    <a href="Demo.zip" target="_blank" class="btn btn-primary">网银接口说明文档【点击下载】</a> <a href="Demo.zip"
                        target="_blank" class="btn btn-primary">卡类接口说明文档【点击下载】</a>
                </td>
                <td height="50" align="left" class="line_01" style="border: none;">
                </td>
            </tr>
        </table>
        <h4 style="font-size: 14px; line-height: 20px;">
            注意事项：</h4>
        <div class="alert alert-info">
            <p>
                1.API接口提交卡需要对接本平台支付接口</p>
            <p>
                2.安全码是对接唯一的加密密钥，请妥善保管</p>
            <p>
                3.在接口对接时，如果遇到问题可联系客服人员</p>
            <p>
                4.接口一旦接入成功，提交面值请按照真实面值提交</p>
            <p>
                5.在卡已经提交的情况下，充值未结束时请不要再次提交该卡</p>
            <p>
                6.提交卡时使用的商户订单号请不要重复</p>
        </div>
    </div>
    </form>
</body>
</html>
