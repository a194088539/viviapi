<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="viviapi.WebUI.Userlogin.product.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商户中心</title>
    <meta name="description" content="">
    <link href="/favicon.ico" rel="shortcut icon" type="image/ico">
    <link rel="stylesheet" href="../css/frame.css" />

    <script src="../js/jquery-1.8.1.min.js"></script>

    <script src="../js/jquery.hoverdelay.js"></script>

    <script src="../js/frame.js"></script>

    <script type="text/javascript" src="../js/jquery.artDialog.js"></script>

    <!--jQuery qtip-->

    <script src="../js/jquery.qtip.min.js"></script>

    <script type="text/javascript" src="../js/hermes.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="position: absolute; left: -9999em; top: 299px; width: auto; z-index: 1987;"
        class="aui_state_focus">
        <div class="aui_outer">
            <table class="aui_border">
                <tbody>
                    <tr>
                        <td class="aui_nw">
                        </td>
                        <td class="aui_n">
                        </td>
                        <td class="aui_ne">
                        </td>
                    </tr>
                    <tr>
                        <td class="aui_w">
                        </td>
                        <td class="aui_c">
                            <div class="aui_inner">
                                <table class="aui_dialog">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" class="aui_header">
                                                <div class="aui_titleBar">
                                                    <div class="aui_title" style="cursor: move;">
                                                        消息</div>
                                                    <a class="aui_close" href="javascript:/*artDialog*/;" style="">×</a></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="aui_icon" style="display: none;">
                                                <div class="aui_iconBg" style="background-image: none; background-position: initial initial;
                                                    background-repeat: initial initial;">
                                                </div>
                                            </td>
                                            <td class="aui_main" style="width: auto; height: auto;">
                                                <div class="aui_content" style="padding: 20px 25px;">
                                                    <div class="aui_loading">
                                                        <span>loading..</span></div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="aui_footer">
                                                <div class="aui_buttons" style="display: none;">
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </td>
                        <td class="aui_e">
                        </td>
                    </tr>
                    <tr>
                        <td class="aui_sw">
                        </td>
                        <td class="aui_s">
                        </td>
                        <td class="aui_se" style="cursor: se-resize;">
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div id="frame-wrapper">
        <div class="top-warning">
        </div>
        <!-- header -->
        <div id="frame-header">
            <div id="logo">
                <a href="/Userlogin/account/index.aspx">
                    <img src="/img/logo1.png" alt=""></a></div>
            <ul id="headerNav">
                <li class=""><a href="/Userlogin/account/index.aspx">我的账户</a></li>
                <li class=""><a href="/Userlogin/order/index.aspx">订单管理</a></li>
                <li class="current"><a href="/Userlogin/product/index.aspx">商品管理</a></li>
                <li class=""><a href="/Userlogin/settlement/index.aspx">结算提现</a></li>
                <li class=""><a href="/Userlogin/recharg/index.aspx">账户充值</a></li>
                <li class=""><a href="/Userlogin/behalf/index.aspx">对私代发</a></li>
                <li class=""><a href="/Userlogin/safety/index.aspx">安全中心</a></li>
            </ul>
            <ul id="header-info">
               <li class="item user-info hoverToggle-wrapper"><a href="javascript:void(0);" class="hoverToggle-trigger">
                    <%=getnm %>&nbsp;<i class="icon icon-chevron-down"></i> </a>
                    <div class="hoverToggle hoverToggle-left">
                        <div class="mtitle">
                            &nbsp;</div>
                        <ul class="mbody top-user-list">
                            <li class="title">
                                <label for="">
                                    编号：</label>
                                <span class="label label-info">
                                    <%=getnid %></span> </li>
                        </ul>
                    </div>
                </li>
                <li class="item return-desktop"><a href="/Userlogin/account/index.aspx" data-hasqtip="0"
                    oldtitle="返回首页" title=""><i class="icon-top icon-top-desktop"></i></a></li>
                <li class="item messages-top"><a href="/Userlogin/account/messages.aspx" target="mainframe"
                    class="link-message" title="未读信息 0 条"><span id="msghead" class="badge badge-error">0</span><i
                        class="icon-top icon-top-message"></i> </a></li>
                <li class="item top-exit last-item"><a href="/Userlogin/logout.aspx" data-hasqtip="3"
                    oldtitle="退出" title=""><i class="icon-top icon-top-exit"></i></a></li>
            </ul>
        </div>
        <!-- sider & main -->
        <div id="frame-content">
            <div class="mainbar">
                <div class="module" style="height: 823px;">
                    <iframe src="cardprice.aspx" marginwidth="0" marginheight="0" frameborder="0"
                        scrolling="auto" class="mainframe" id="mainframe" name="mainframe" style="height: 823px;">
                    </iframe>
                </div>
            </div>
            <div class="sidebar">
                <div class="module">
                    <a href="javascript:void(0);" class="sideBar-trigger" style="height: 823px;"><span
                        style="margin-top: 396.5px;">&nbsp;</span></a>
                    <div class="mbody" style="height: 793px;">
                        <div class="sideNav-content" style="height: 759px;">
                            <!--文字侧边导航-->
                            <ul id="side-menu" class="text-nav">
                                <li class="current"><a href="cardprice.aspx" target="mainframe"><i class="icon-nav">
                                </i>卡类面值</a></li>
                                <li><a href="banktype.aspx" target="mainframe"><i class="icon-nav"></i>网银种类</a></li>
                                <li><a href="costrate.aspx" target="mainframe"><i class="icon-nav"></i>结算费率</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- footer -->
        <div id="frame-footer">
            <ul class="footer-info">
                <li class="item hoverToggle-wrapper"><a href="javascript:void(0);" class="trigger"><i
                    class="icon icon-user"></i>客服</a>
                    <div class="hoverToggle hoverToggle-bottomRight service-popup">
                        <div class="mbody">
                        <dl>
                                <dt>在线客服：</dt>
                                <dd>
                                   <a target="_blank"
                            href="http://wpa.qq.com/msgrd?v=3&uin=1833366652&site=qq&menu=yes"><img border="0"
                                src="http://wpa.qq.com/pa?p=2:1833366652:51" alt="点击这里给我发消息" title="点击这里给我发消息" /></a>
                                </dd>
                            </dl>
                            <p class="tcenter">
                                <a target="mainframe" href="/Userlogin/account/feedbacks.aspx" class="btn">问题反馈</a></p>
                        </div>
                        <div class="mfooter">
                            &nbsp;</div>
                    </div>
                </li>
                <!--需要通知默认显示时，在hoverToggle-wrapper后加class="active"-->
                <li class="item clickToggle-wrapper last-item"><a href="javascript:void(0);" class="trigger">
                    <i class="icon icon-comment"></i>通知 <span id="msgfoot2" class="badge badge-error">0</span></a>
                    <div class="clickToggle hoverToggle-bottomLeft notify-popup">
                        <a href="javascript:void(0);" class="close">×</a>
                        <div class="mbody">
                            <div class="alert">
                                <a href="/Userlogin/account/messages.aspx" target="mainframe">未读信息</a> <span id="msgfoot"
                                    class="badge badge-error">0</span>
                            </div>
                        </div>
                        <div class="mfooter">
                            &nbsp;</div>
                    </div>
                </li>
            </ul>
        </div>
    </div>

    <script>
        $(function() {
            $("[title]").qtip({
                position: {
                    my: 'top center',
                    at: 'bottom center'
                },
                style: {
                    classes: 'qtip-dark qtip-shadow'
                }
            });
            $(".account-info span.label").qtip({
                position: {
                    my: 'top left',
                    at: 'bottom right'
                },
                style: {
                    classes: 'qtip-dark qtip-shadow'
                }
            });

            $(".top-exit a").qtip({
                position: {
                    my: 'top right',
                    at: 'bottom left'
                },
                style: {
                    classes: 'qtip-dark qtip-shadow'
                }
            });

        });

        $(function() {
            //侧边导航tab点击切换
            $("#side-menu li a").click(function() {
                $("#side-menu li").removeClass("current");
                jQuery(this).parent("li").addClass("current");
            });
        });

        // 探测屏幕分辨率以更改布局
        jQuery(window).bind("resize", changeLayout);
        function changeLayout(e) {
            var winHeight = jQuery(window).height();
            var winWidth = jQuery(window).width();
            if (winWidth < 1000) {
                jQuery('#header-info').addClass("info-lite");
                jQuery('#topSearch input').addClass("input-small");
            } else {
                jQuery('#header-info').removeClass("info-lite");
                jQuery('#topSearch input').removeClass("input-small");
            }
        }
        jQuery(document).ready(function() {
            var winWidth = jQuery(window).width();
            if (winWidth < 1000) {
                jQuery('#header-info').addClass("info-lite");
                jQuery('#topSearch input').addClass("input-small");
            } else {
                jQuery('#header-info').removeClass("info-lite");
                jQuery('#topSearch input').removeClass("input-small");
            }
            unReadNum();
        });

        //弹层方法
        function dialogPOP(url, title1, width1, height1) {
            dialog = art.dialog({
                title: title1,
                width: width1,
                height: height1,
                lock: true,
                background: '#000', // 背景色
                opacity: 0.67	// 透明度
            });
            $.ajax({
                url: url,
                dataType: 'html',
                cache: false,
                success: function(data) {
                    dialog.content(data);
                },
                cache: false
            });
        }
        //关闭弹层
        function closeDialog() {
            try {
                if (timer) {
                    clearInterval(timer);
                }
            } catch (err) { };
            if (dialog) {
                dialog.close();
            }
        }

        // 未读消息数量
        function unReadNum() {
            $.getJSON('../message/unread', function(json) {
                if (json.success) {
                    $("#msghead").html(json.data.unread);
                    $("#msghead").parent().attr("title", "未读信息 " + json.data.unread + " 条");
                    $("#msgfoot").html(json.data.unread);
                    $("#msgfoot2").html(json.data.unread);
                } else {
                    $("#msg").html("0");
                }
            }
        )
        }
</script>

    </form>
</body>
</html>
