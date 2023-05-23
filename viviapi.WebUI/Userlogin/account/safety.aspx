<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="safety.aspx.cs" Inherits="viviapi.WebUI.Userlogin.account.safety" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/datatable.css" />
    <link rel="stylesheet" type="text/css" href="../css/page.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/account/index.aspx'">我的账户</a>
        &nbsp;&gt;&nbsp; <span>安全设置</span>
    </div>
    <!--右部表单开始-->
    <div id="list_content">
        <div class="page-wrapper">
            <h2>
                保护资金安全</h2>
            <div class="s-center">
                <table class="table">
                    <tbody>
                        <tr>
                            <td style="width: 350px">
                                <h3>
                                    登录密码
                                </h3>
                                <span class="label label-success">已设置</span>
                            </td>
                            <td>
                                <span class="help-inline">上次登录时间：<%= currentUser.LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss") %></span>
                            </td>
                            <td style="width: 100px;" class="tcenter">
                                <a href="/Userlogin/safety/repassword.aspx" class="btn btn-primary">修改</a>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 350px">
                                <h3>
                                    提现密码
                                </h3>
                                <%=gettxpass %>
                            </td>
                            <td>
                                <span class="help-inline">提现时账户信息时输入，保护账户资金安全。（如果未设置，默认为登录密码）</span>
                            </td>
                            <td style="width: 100px;" class="tcenter">
                                <a href="/Userlogin/safety/cashpass.aspx" class="btn btn-primary">
                                    <%=gettxbtn %></a>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 350px">
                                <h3>
                                    密保问题
                                </h3>
                                <%=getmb %>
                            </td>
                            <td>
                                <span class="help-inline">密码保护问题方便找回密码</span>
                            </td>
                            <td style="width: 100px;" class="tcenter">
                                <a href="/Userlogin/safety/safeques.aspx" class="btn btn-primary">
                                    <%=getmbbtn %></a>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 350px">
                                <h3>
                                    手机认证
                                </h3>
                                <%=getphone %>
                            </td>
                            <td>
                                <span class="help-inline">手机认证流程：输入手机号 - 发送验证码 - 输入验证码确认 - 认证成功</span>
                            </td>
                            <td style="width: 100px;" class="tcenter">
                                <a href="/Userlogin/safety/modiphone.aspx" class="btn btn-primary">
                                    <%=getphonebtn %></a>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 350px">
                                <h3>
                                    邮箱认证
                                </h3>
                                <%=getemail %>
                            </td>
                            <td>
                                <span class="help-inline">①当前邮箱如果已经认证，修改时系统会给您原邮箱地址发送确认邮件，确认才能修改成功 ②修改新邮箱成功后需要重新进行认证</span>
                            </td>
                            <td style="width: 100px;" class="tcenter">
                                <a href="/Userlogin/safety/modiemail.aspx" class="btn btn-primary">
                                    <%=getemailbtn %></a>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 350px">
                                <h3>
                                    实名认证
                                </h3>
                                <%=getidcard %>
                            </td>
                            <td>
                                <span class="help-inline">①系统不支持一代身份证实名认证；②实名认证的真实姓名必须与银行卡的用户名一致，以免影响提现；③通过实名认证后，账户才能进行体现操作</span>
                            </td>
                            <td style="width: 100px;" class="tcenter">
                                <a href="/Userlogin/safety/safetrna.aspx" class="btn btn-primary">
                                    <%=getidcardbtn %></a>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
