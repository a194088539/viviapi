﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="viviapi.WebUI.index" %>

<!DOCTYPE html>
<!-- saved from url=(0023)/ -->
<html lang="en">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="keywords" content="app支付,支付接口,支付API,支付宝接口,微信支付接口,微信支付,支付宝支付&quot;">
    <link rel="shortcut icon" type="image/x-icon" href="/favicon.ico" media="screen">
    <link rel="stylesheet" href="css/base.css">
    <title>云上支付 |让支付更简单</title>
    <link href="css/polyPay.css" rel="stylesheet">
	<link rel="stylesheet" href="css/index.css">
</head>

<body>
<!--[if lt IE 9]>
<style type="text/css">
    .header .nav > .logo {
        background-image: url("img/logo-160x36.png");
    }

    .banner .right .circle .c1 .c2 .c3 i:first-child + i {
        background-image: url("img/2.png");
        right: 266px;
        top: 300px;
    }

    .banner .right .circle .c1 .c2 .c3 i:first-child + i + i {
        background-image: url("img/3-2.png");
        right: 245px;
        top: 0px;
    }

    .content.scene ul li:first-child + li dl dt em {
        background-position: -131px -389px;
    }

    .content.scene ul li:first-child + li + li dl dt em {
        background-position: -268px -389px;
    }

    .content.scene ul li:first-child + li + li + li dl dt em {
        background-position: -403px -389px;
    }

    .content.scene ul li:first-child + li + li + li + li dl dt em {
        background-position: -531px -389px;
    }

    .float_bar li:first-child + li i {
        background-position: -847px -312px;
    }

    .float_bar li:first-child + li + li em {
        background-image: url("img/bc_qq_qr.png");
    }

    .float_bar li:first-child + li + li i {
        background-position: -847px -374px;
    }

    .float_bar li:first-child + li + li + li i {
        background-position: -847px -436px;
    }

    .float_bar li:first-child + li + li + li + li i {
        background-position: -847px -498px;
    }

    .float_bar li:first-child + li:hover i {
        background-position: -787px -312px;
    }

    .float_bar li:first-child + li + li:hover i {
        background-position: -787px -374px;
    }

    .float_bar li:first-child + li + li + li:hover i {
        background-position: -787px -436px;
    }

    .float_bar li:first-child + li + li + li + li:hover i {
        background-position: -787px -498px;
    }

    .float_bar li:first-child + li + li:hover em {
        display: inline-block;
    }

    .float_bar li:first-child + li + li + li:hover em {
        display: inline-block;
    }

    .content.product ul li:first-child + li div h4 i {
        background-position: -676px -140px;
    }

    .content.product ul li:first-child + li + li div h4 i {
        background-position: -805px -140px;
    }

    .content.saas .con_r dl:after {
        display: none;
    }

    .safe dl:first-child + dl {
        border-right: 1px solid #e0e0e0;
        border-left: 1px solid #e0e0e0;
        padding-left: 30px;
        margin-right: 30px;
    }

    .safe dl:first-child + dl dt {
        background-position: -617px -318px;
    }

    .safe dl:first-child + dl + dl dt {
        background-position: -700px -318px;
    }
</style>
<![endif]-->
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
<div class="banner">
    <!--    <div class="con con1">-->
    <!--        <div class="index_center" style="height: 500px;">-->
    <!--            <div class="left">-->
    <!--                <h2>创造更安全的<span>支付体验</span></h2>-->
    <!--                <p>BeeCloud全面集成主流支付渠道</p>-->
    <!--                <a href="demo/" class="btn">体验支付</a>-->
    <!--            </div>-->
    <!--            <div class="right">-->
    <!--                <div class="circle">-->
    <!--                    <div class="c1">-->
    <!--                        <div class="c2">-->
    <!--                            <div class="c3">-->
    <!--                                <i></i>-->
    <!--                                <i></i>-->
    <!--                                <i></i>-->
    <!--                                <div class="c4"></div>-->
    <!--                            </div>-->
    <!--                        </div>-->
    <!--                    </div>-->
    <!--                    <div class="safe"></div>-->
    <!--                    <div class="safe safe2"></div>-->
    <!--                </div>-->
    <!--            </div>-->
    <!--        </div>-->
    <!--    </div>-->
    <div class="con con7 christmas" style="opacity: 0; z-index: 10; display: block;">
        <div class="index_center">
            <div class="left">
                <span style="font-size:52px;color:white;">创造更安全的支付体验</span>

                <p><span style="color:ghostwhite">云上支付全面集成主流支付渠道</span></p>
                <div class="a_2" style="">
                    <a href="http://ceshi.yunsonpay.com/pay/demo" target="_blank" style="background-color: #4876FF;font-size: 18px;color: black;border: 0px">体验支付</a>
                </div>
            </div>
        </div>
    </div>
    <!--    <div class="con con2">-->
    <!--        <div class="index_center">-->
    <!--            <div class="left">-->
    <!--                <h2>花蜜付<span>让支付更简单</span></h2>-->
    <!--                <h3><span>支持渠道</span><i></i>·<i></i></h3>-->
    <!--                <p>支持分公司、门店收款业务互相独立、操作员分权限进行管理</p>-->
    <!--                <a href="products/huami.php">了解更多</a>-->
    <!--            </div>-->
    <!--        </div>-->
    <!--    </div>-->

    <!--<div class="con con5">-->
    <!--<div class="index_center">-->
    <!--<div class="left">-->
    <!--<h2 style="font-family:PingFangSC-Medium;">订阅支付</h2>-->
    <!--<p><span>用户根据自己需求，订阅不同的产品服务<br>大大的提升了你的会员持续付费率</p>-->
    <!--<a href="/Home_Products_subscribe.html">了解更多</a>-->
    <!--</div>-->
    <!--</div>-->
    <!--</div>-->
    <div class="con con3" style="opacity: 1; display: block; z-index: 11;">
        <div class="index_center">
            <div class="left">
                <h2 style="font-family:PingFangSC-Medium;"><span>云上支付sdk</span></h2>
                <p>PC支付，手机支付，扫码支付等支持全部场景<br>提供标准化支付产品及全面的支付渠道，只负责信息处理，不参与资金清算</p>
                <!--<a href="#">立即申请</a>-->
            </div>
        </div>
    </div>

    <div class="con con4" style="opacity: 0; display: block; z-index: 10;">
        <div class="index_center">
            <div class="left">
                <h2><span>客户与营销</span>数据管理分析平台</h2>
                <p><span>提供灵活、易用、高效的客户与营销管理功能，浅显易懂的可视化<br>分析，帮助企业完成企业洞察，助力商业决策</span></p>
                <!--<a href="/Home_Products_platform.html">了解更多</a>-->
            </div>
        </div>
    </div>
    <!--<div class="con con6">-->
    <!--<div class="index_center">-->
    <!--<div class="left">-->
    <!--<p>云上支付支持</p>-->
    <!--<h1><span>小程序</span><span>支付啦！</span></h1>-->
    <!--<div class="a_2">-->
    <!--<a href="/Home_Test_loginindex.html">立即申请</a>-->
    <!--</div>-->
    <!--</div>-->
    <!--</div>-->
    <!--</div>-->
    <div class="banner_index" id="banner_slide"><i class="" style="width: 10px;"></i><i class="on" style="width: 20px;"></i><i class="" style="width: 10px;"></i></div>
    <div class="notice_bg">
        <div class="notice">云上支付已经支持支付宝APP和支付宝移动网页新接口，欢迎大家使用!<i></i></div>
    </div>
</div>
<div class="safe">
    <div class="index_center">
        <dl>
            <dt></dt>
            <dd>
                <h3>接入便利&nbsp;</h3>
                全平台 SDK 让你最小化接入支付的时间与人力
            </dd>
        </dl>
        <dl>
            <dt></dt>
            <dd>
                <h3>稳定可靠</h3>
                两地三中心容灾系统，确保服务稳定，最快完成交易
            </dd>
        </dl>
        <dl>
            <dt></dt>
            <dd>
                <h3>不介入资金流&nbsp;</h3>
                云上支付平台，只负责交易处理，不参与资金清算
            </dd>
        </dl>
    </div>
</div>
<div class="content scene">
    <div class="index_center">
        <h1>全收款场景覆盖</h1>
        <p>提供PC、H5、IOS等终端技术解决方案</p>
        <ul>
            <li>
                <dl>
                    <dt>

                        <img src="img/icon-07.png" style="center no-repeat;width:120px;height:auto;">

                        <br>
                        <span>PC网页/扫码</span>
                    </dt>
                    <!--<dd>PC网页支付解决方案、多渠道支付、网页和扫码-->
                        <!--<br><a href="/Home_Products_sdk.html#pc">查看详情 >-->
                            <!--<br><i></i></a></dd>-->
                </dl>
            </li>
            <li>
                <dl>
                    <dt>
                        <img src="img/icon-08.png" style="center no-repeat;width:120px;height:auto;">
                        <br>
                        <span>移动网页/H5</span>
                    </dt>
                    <!--<dd>iOS、Android、HTML5微信公众号<br><a href="/Home_Products_sdk.html#phone">查看详情 ><br><i></i></a>-->
                    <!--</dd>-->
                </dl>
            </li>
            <li>
                <dl>
                    <dt>
                        <img src="img/icon-09.png" style="center no-repeat;width:120px;height:auto;">
                        <br>
                        <span>移动Android APP</span>
                    </dt>
                    <!--<dd>主动扫码、被动扫码、支付宝和微信双渠道<br><a href="/Home_Products_huami.html">查看详情 ><br><i></i></a></dd>-->
                </dl>
            </li>
            <li>
                <dl>
                    <dt>
                        <img src="img/icon-10.png" style="center no-repeat;width:120px;height:auto;">
                        <br>
                        <span>移动IOS APP</span>
                    </dt>
                    <!--<dd>商户向企业/个人帐户打款、到账周期短、费率低<br><a href="/Home_Products_transfer.html">查看详情 ><br><i></i></a></dd>-->
                </dl>
            </li>
        </ul>
    </div>
</div>
<div class="content saas">
    <div class="index_center">
        <ul>
            <li><i><em></em></i></li>
            <li class="on"><i><em></em></i>技术研发</li>
            <li><i><em></em></i>运营管理</li>
            <li><i><em></em></i>渠道安全</li>
            <li><i><em></em></i>开发调试</li>
            <li><i><em></em></i>服务流程</li>
        </ul>
        <h1>企业信赖的商业合作伙伴</h1>
        <p>
            作为支付技术服务商，qujuhe提供标准化支付产品及全面的支付渠道，并坚持阳光支付，只负责信息处理，不参与资金清算，是企业坚实的商业合作伙伴。</p>
        <div class="con_r">
            <dl>
                <dt>技术研发</dt>
                <dd>云上支付拥有一支行业一流的技术团队为您提供便捷、稳定和安全的技术服务。云上支付计费自主研发的聚合支付系统可以提供专业的sdk、api数据服务，从而打造一站式的接口管理、行程简单稳定的聚合支付云服务。
                </dd>
            </dl>
            <div class="img_list">
                <img src="img/saas_img1.png">

            </div>
        </div>
    </div>
</div>
<div class="polyPay product" style="margin-top:5%">
    <div class="mod flow">
        <div class="flow-clock">
            <div class="hour">
            </div>
            <div class="minute">
            </div>
            <div class="second">
            </div>
        </div>
        <div class="flow-content">
            <h2>快速接入流程</h2>
            <p>
                只需4个步骤即可成功接入
            </p>
            <ul>
                <li>商户注册</li>
                <li>线上测试</li>
                <li>参数配置</li>
                <li>完成接入</li>
            </ul>
        </div>
    </div>
</div>
<div class="content" style="">
    <div class="index_center">
        <h1>全面支付渠道支持</h1>
        <div class="channel-main clearfix">
            <div class="channel-con">
                <div class="channel-box">
                    <div class="logo-box">
                        <img src="img/channel_logo_wx.png" alt="" class="logo">
                        <p class="name">微信支付</p>
                    </div>
                    <div class="info-box">
                        <p class="name">微信支付</p>
                        <div class="list-box">
                            公众号支付<br>APP支付<br>
                            H5支付<br>WAP支付<br>小程序
                        </div>
                    </div>
                </div>
            </div>
            <div class="channel-con">
                <div class="channel-box">
                    <div class="logo-box">
                        <img src="img/channel_logo_ali.png" alt="" class="logo">
                        <p class="name">支付宝</p>
                    </div>
                    <div class="info-box">
                        <p class="name">支付宝</p>
                        <div class="list-box">
                            即时到账<br>手机网站<br>
                            APP支付<br>当面付
                        </div>
                    </div>
                </div>
            </div>
            <div class="channel-con current">
                <div class="channel-box">
                    <div class="logo-box">
                        <img src="img/channel_logo_yl.png" alt="" class="logo">
                        <p class="name">银联支付</p>
                    </div>
                    <div class="info-box">
                        <p class="name">银联支付</p>
                        <div class="list-box">
                            网关支付<br>手机支付
                        </div>
                    </div>
                </div>
            </div>
            <div class="channel-con">
                <div class="channel-box">
                    <div class="logo-box">
                        <img src="img/channel_logo_jd.png" alt="" class="logo">
                        <p class="name">京东支付</p>
                    </div>
                    <div class="info-box">
                        <p class="name">京东支付</p>
                        <div class="list-box">
                            PC网页支付<br>移动网页支付
                        </div>
                    </div>
                </div>
            </div>
            <div class="channel-con">
                <div class="channel-box">
                    <div class="logo-box">
                        <img src="img/channel_logo_bd.png" alt="" class="logo">
                        <p class="name">百度钱包</p>
                    </div>
                    <div class="info-box">
                        <p class="name">百度钱包</p>
                        <div class="list-box">
                            APP支付<br>PC网页支付<br>移动网页支付
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--        <div class="channel_logo"></div>-->
        <h1 class="bank_h1">保证资金安全<br><span>( 云上支付与多家银行合作 )</span></h1>
        <div class="channel_logo bank_logo"></div>
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
<script type="text/javascript">
    $(function () {
        $('#logout').click(function () {
            $.ajax({
                type: "get",
                url: "/login/data/logout.php",
                //            data: "",
                success: function (data) {
                    var data = JSON.parse(data);
                    console.log(data);
                    if (data.resultCode == 0) {
                        window.location.href = '/login';
                    } else {
                        alert(data.errMsg);
                    }
                }
            });
        });
    })
</script>
<script type="text/javascript" src="js/index.js"></script>

</body></html>