<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="viviapi.WebUI.login" %>

<!DOCTYPE html>
<!-- saved from url=(0033)/login.aspx -->
<html lang="en">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="keywords" content="app支付,支付接口,支付API,支付宝接口,微信支付接口,微信支付,支付宝支付&quot;">
    <link rel="shortcut icon" type="image/x-icon" href="/favicon.ico" media="screen">
    <link rel="stylesheet" href="css/base.css">
    <title>云上支付 |让支付更简单</title>
    <link href="css/polyPay.css" rel="stylesheet">
	<link rel="stylesheet" href="css/form.css">
	<link rel="stylesheet" href="css/layer.css" id="layui_layer_skinlayercss">
</head>

<body ng-app="bcLoginControllers" ng-controller="bcLoginCtrl">
<div class="header">
    <!--[if lt IE 9]>
<style type="text/css">
    .header .nav > .logo {
        background-image: url("img/logo-160x36.png");
    }

    .header .nav > .logo.christmas {
        background-image: url("img/logo-christmas-small.png");
    }
</style>
<![endif]-->
<div class="fast-enter">
    <ul class="common-container">
        <li><a href="#" target="_blank">帮助中心</a><i class="separator"></i>
        </li>
        <li><a href="regedit.aspx">免费注册</a> <i class="separator"></i></li>
        <li><a href="login.aspx">登录</a></li>
    </ul>
</div>
<div class="nav common-container">
    <a href="" class="logo christmas"></a>
    <ul>
        <li><a href="" class="on">首页</a></li>
        <!--<li class="more">-->
        <!--<a href="javascript:void(0);" class="">公司产品<i class="common-icon icon-arrow-down"></i></a>-->
        <!--<div class="sub-nav">-->
        <!--<ul class="group">-->
        <!--<li>-->
        <!--<span class="title">支付接入</span>-->
        <!--<div class="group-list">-->
        <!--<a href="/Home_Products_sdk.html">支付SDK</a>-->
        <!--<a href="/Home_Products_jsbtn.html">秒支付Button</a>-->
        <!--<a href="/Home_Products_plugin.html">H5APP插件</a>-->
        <!--</div>-->
        <!--</li>-->
        <!--<li>-->
        <!--<span class="title">解决方案</span>-->
        <!--<div class="group-list">-->
        <!--<a href="/Home_Products_transfer.html">企业打款</a>-->
        <!--<a href="/Home_Products_subscribe.html">订阅支付</a>-->
        <!--<a href="/Home_Products_huami.html">花蜜付</a>-->
        <!--<a href="/Home_Products_cross.html">跨境收款</a>-->
        <!--</div>-->
        <!--</li>-->
        <!--<li>-->
        <!--<span class="title">商业智能</span>-->
        <!--<div class="group-list">-->
        <!--<a href="/Home_Products_platform.html">客户与营销</a>-->
        <!--</div>-->
        <!--</li>-->
        <!--<li>-->
        <!--<span class="title">辅助功能</span>-->
        <!--<div class="group-list">-->
        <!--<a href="/Home_Products_auth.html">实名认证接口</a>-->
        <!--</div>-->
        <!--</li>-->
        <!--</ul>-->
        <!--</div>-->
        <!--</li>-->
        <li><a href="download.aspx" class="">SDK下载</a></li>
        <li>
            <a href="#" class="more">开发文档</a>
            <!--<div class="sub-nav">-->
            <!--<ul>-->
            <!--&lt;!&ndash;<li><a href="/Home_Apply_index.html">快速开始</a></li>&ndash;&gt;-->
            <!--<li><a href="#">开发文档</a></li>-->
            <!--&lt;!&ndash;<li><a href="https://blog.beecloud.cn/" target="_blank">博客</a></li>&ndash;&gt;-->
            <!--</ul>-->
            <!--</div>-->
        </li>
        <!--<li><a href="/Home_Price_index.html" class="">支付体验</a></li>-->
        <!-- <li><a href="http://ceshi.yunsonpay.com/pay/demo" target="_blank" class="">支付体验</a></li> -->
        <li><a href="login.aspx">管理中心</a></li>
        <!-- <li><a href="/life.html">去生活</a></li> -->
    </ul>
</div>
</div>
<div class="common-wrap" style="padding-top: 90px;">
    <div class="common-banner login-banner"></div>
    <div class="login-wrap ">
        <div class="form-title">登录</div>
        <div class="login-main">

<form id="form1" runat="server">

            <b class="ng-binding loginerr" style="color: rgb(255, 108, 44);display: none;">1111</b>

<input type="hidden" id="needChkCode" value="0"/> <!-- 是否需要验证码 -->
    <input type="hidden" name="ping" id="ping" />
    <input type="hidden" name="xsrf" value="1"/>

            <div class="form-box">
                <i class="form-icon icon-account"></i>
                <i class="form-separator"></i>
                <input type="text" class="form-input" id="username" name="username" runat="server" placeholder="请输入您的账号" autofocus required/>
            </div>
            <div class="form-box">
                <i class="form-icon icon-password"></i>
                <i class="form-separator"></i>
                <asp:TextBox TextMode="Password" name="password" class="form-input" id="password" runat="server" maxlength="20" placeholder="请输入您的密码" required/></asp:TextBox>
            </div>

            <div class="form-box">
                <i class="form-icon icon-password"></i>
                <i class="form-separator"></i>
                <input type="text" class="form-input" id="imycode" name="imycode" required="true" autocomplete="off" placeholder="请输入验证码">
            </div>
            <div class="form-box">
                <label class="userverification col-sm-8">
                    <img id='imgValidateCode' src="/vercode.aspx" width="77" height="38" />
                    <a href="javascript:refreshValidateCode('imgValidateCode','/vercode.aspx');"></a>
                </label>
            </div>

            <div class="form-box">
                <asp:ImageButton ID="ImageButton2" class="form-btn" runat="server" ImageUrl="~/img/dl_tjsub.png" onclick="ImageButton2_Click" />
            </div>
</form>
            <p class="clearfix" style="margin: -10px 0 30px;">
<!--                <span class="remember-me fl"><input type="checkbox" name="remember-me" id="remember-me"><label for="remember-me">记住我</label></span>-->
                <!--<a href="../retrieve/" class="fr forgot">忘记密码?</a>-->
            </p>
            <p class="other-enter clearfix dologin">
                <span class="fl">还没有注册账号？</span>
                <a href="regedit.aspx" class="link-go fr">立即注册</a>
            </p>
        </div>
    </div>
</div>

<div class="footer">

    <div class="con clearfix">
        <dl>
            <dt><i class="common-icon icon-about-us"></i><span>关于我们</span></dt>
            <!--<dd><a href="/Home_About_index.html">创始人</a></dd>-->
            <dd><a>创始人</a></dd>
            <!--<dd><a href="/Home_About_index.html#intro">公司简介</a></dd>-->
            <dd><a>公司简介</a></dd>
            <!--<dd><a href="/Home_About_index.html#honor">公司荣誉</a></dd>-->
            <dd><a>公司荣誉</a></dd>
            <!--<dd><a href="/Home_About_contact.html">联系我们</a></dd>-->
            <dd><a>联系我们</a></dd>
        </dl>
        <dl>
            <dt><i class="common-icon icon-doc"></i><span>文档</span></dt>
            <dd><a>快速开始</a></dd>
            <dd><a href="#">开发文档</a></dd>
            <!--<dd><a href="/Home_Video_index.html">视频教程</a></dd>-->
            <dd><a>视频教程</a></dd>
            <!--<dd><a href="https://blog.beecloud.cn">博客</a></dd>-->
        </dl>
        <!--        <dl>-->
        <!--            <dt><i class="common-icon icon-job"></i><span>招聘</span></dt>-->
        <!--            <dd><a href="/job">福利待遇</a></dd>-->
        <!--            <dd><a href="/job">招聘职位</a></dd>-->
        <!--        </dl>-->
        <dl>
            <dt><i class="common-icon icon-coor"></i><span>合作</span></dt>
            <!--            <dd><a href="/partner/">合作信息</a></dd>-->
            <dd><a>合作伙伴</a></dd>
        </dl>
        <dl>
            <dt><i class="common-icon icon-media"></i><span>媒体</span></dt>
            <!--<dd><a href="/Home_Media_index.html">媒体报道</a></dd>-->
            <!--<dd><a href="/Home_Media_index.html#download">媒体资源</a></dd>-->
        </dl>
        <dl>
            <dt><i class="common-icon icon-agreement"></i><span>合作协议</span></dt>
            <dd><a href="dashboard.html">用户协议</a></dd>
        </dl>
        <dl class="last">
            <dd>
                如果，你觉得<br>
                世界辣么大，你想换个工作环境看看<br>
                那么，就来 云上支付吧<br>
                这里是互联网支付领域里的先锋<br>
                更是极客与大牛的集中营
            </dd>
            <p>
                <!--<a href="/Home_Job_index.html" class="common-btn join-us-btn">加入我们</a>-->
                <a class="common-btn join-us-btn">加入我们</a>
            </p>
        </dl>
    </div>
    <div class="copyright-box common-container" style="color: black">
        <p><b>一站式支付解决方案提供商</b></p>
        <p>Copyright©2014-2017 @云上支付 yunsonpay.com </p>
    </div>
</div>

<script type="text/javascript" src="js/jquery.min.js"></script>
<script src="js/layer.min.js"></script>

<script type="text/javascript">
    //登陆
    function loginButtonClick() {
        var username = $("#username").val().trim();
        var password = $("#password").val().trim();
        var varification = $("#verification").val().trim();
        var ret_username = /^\w{3,20}$/;
        var ret_varify = /^\w{3,20}$/;
        //前端验证
        if (!ret_username.test(username)) {
            return showerror('用户名格式不正确');
        }
        if (password == '') {
            return showerror('密码不能为空');
        }
        if (!ret_varify.test(varification)) {
            return showerror('请输入正确的验证码');
        }
        //提交接口
        var index = layer.load(1, {
            shade: [0.1, '#fff'] //0.1透明度的白色背景
        });
        $.ajax({
            type: 'POST',
            url: 'dologin',
            data: {
                username: username,
                password: password,
                varification: varification
            },
            dataType: 'json',
            beforeSend: function (b) {
                $('.dologin').prop('disabled', true);
            },
            success: function (data) {
                if (data.status) {
                    layer.msg(data.info, {
                        icon: 6,
                        time: 1000
                    }, function () {
                        window.location.href = data.url;
                    });
                } else {
                    layer.msg(data.info, {
                        icon: 2,
                        time: 2000
                    }, function () {
                        $('.dologin').prop('disabled', false);
                    });
                }
            },
            complete: function (c) {
                layer.close(index);
                $(".verifyimg").attr("src", "/verify");
            }
        });
    }

    function showerror(msg) {
        $('.loginerr').html(msg);
        $('.loginerr').css('display', 'block');
        return false;
    }
</script>


</body></html>
