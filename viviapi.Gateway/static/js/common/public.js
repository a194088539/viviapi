/*通用脚本*/

$(document).ready(function () {
	// topbar左上角用户信息展示
	var API_HOST = "/";
	var paramString = "m=api&c=user&a=status";
	$.getJSON(API_HOST + "?" + paramString + "&jsoncallback=?", function (responseData) {
		if (!responseData.status) {
			return;
		}

		var $_userinfoContainer = $("#userinfoContainer");
		var $_usernameText = $("#usernameText");
		var $_usernameShow = $("#usernameShow");
		var $_userId = $("#userId");

		$_usernameShow.html(responseData.name);
		$_userId.html("(ID:" + responseData.id + ")");
		$("#serverCount").html(responseData.servercount);
		$("#orderCount").html(responseData.ordercount);
		$("#msgCount").html(responseData.msgcount);

		var money = responseData.money || "0";
		if (String(money).indexOf(".") == -1) {
			money += ".00";
		}
		$("#userMoney").html(money);

		var verifyClassNameMap = {
			"0": "auth-icon-unauth",
			"1": "auth-icon-personal",
			"2": "auth-icon-company"
		};
		$_usernameText.addClass(verifyClassNameMap[responseData.verify]);

		// 初始化数据之后，显示 用户面板
		$_userinfoContainer.addClass("welcome-user");

		// 动画效果
		var PANEL_EXTEND_CLASSNAME = "userinfo-container--extend";
		var EXTEND_DURATION = 300;
		var $_usernameContainer = $("#usernameContainer");
		var $_userPanel = $("#userPanel");
		var panelHeight = $_userPanel.css("height");
		$_userinfoContainer.on("mouseenter", ".userinfo-container", function () {
			$_userinfoContainer.addClass(PANEL_EXTEND_CLASSNAME);

			// 取消动画列队，不完成当前动画
			$_userPanel.stop(true, false);
			$_userPanel.css("height", 0);
			$_userPanel.animate({height: panelHeight}, EXTEND_DURATION);
		});
		$_userinfoContainer.on("mouseleave", ".userinfo-container", function () {
			// 取消动画列队，不完成当前动画
			$_userPanel.stop(true, false);
			$_userPanel.animate({height: 0}, EXTEND_DURATION, function () {
				$_userinfoContainer.removeClass(PANEL_EXTEND_CLASSNAME);
			});
		});
	});

	//返回顶部按钮
	var $toTop = $("#toTop");
	$toTop.hide();
	$(window).scroll(function () {
		if ($(window).scrollTop() > 100) {
			if($toTop.is(":hidden")) {
				$toTop.stop().fadeIn(500);
			}
		}
		else {
			if($toTop.is(":visible")) {
				$toTop.stop().fadeOut(500);
			}
		}
	});
	$toTop.click(function () {
		$('body,html').stop().animate({scrollTop: 0}, 300);
		return false;
	});

	//二级菜单交互效果
	var $headerNavUl = $(".header-nav>ul");
	$(".header-nav-li").mouseenter(function(){
		$headerNavUl.removeClass();
		$headerNavUl.addClass("nav-bar-"+($(this).index()+1));
		$(this).addClass("header-nav-li--active").siblings().removeClass("header-nav-li--active");
	});
	$(".header-nav").mouseleave(function(){
		$(".header-nav-li").removeClass("header-nav-li--active");
		$headerNavUl.removeClass();
	});
	//“首页”不触发二级菜单
	$(".nav-1").hover(function(){
		$(".header-nav").addClass("hide-pop-list");
	}, function () {
		$(".header-nav").removeClass("hide-pop-list");
	});

	/*footer部分的脚本*/
	//侧边栏弹出
	$(".suspension-tel, .suspension-qrcode").hover(function(){
		$(this).children(".pop-detail").fadeIn(300);
	},function(){
		$(this).children(".pop-detail").fadeOut(100);
	});

	$(".official-plat ul li:first-child").hover(function () {
		$(".weixin").show();
		$(".weibo").hide();
	});
	$("li[title='点击打开官方微博']").hover(function () {
		$(".weixin").hide();
		$(".weibo").show();
	});

	//href="#a_null"的统一设置为无效链接
	$("a[href='#a_null']").click(function(){
		return false;
	});

	//全图可点型banner添加打开链接方法
	$(".link-banner").each(function(){
		var $_self = $(this);
		var openURL = $_self.data("url");

		if(openURL) {
			$_self.click(function(){
				var openTarget = $_self.data("target") || "self";
				window.open(openURL,openTarget);
			});
		}
	});
	//PIE 统一设置
	if (window.PIE) {
		$('.pie-for-element').each(function () {
			PIE.attach(this);
		});
		$('.pie-for-children').children().each(function () {
			PIE.attach(this);
		});
		$('.pie-for-tree').find("*").each(function () {
			PIE.attach(this);
		});
	}
	//验证码切换
	var showCaptcha = $(".show-captcha");
	var refreshCaptcha = $(".refresh-captcha");
	var captchaSrc = "/CodeImage.aspx?";
	showCaptcha.attr("src", captchaSrc + '&rnd='+ Math.random());
	showCaptcha.click(function(){
		this.src= captchaSrc+ '&rnd='+ Math.random();
	});
	refreshCaptcha.click(function(){
		$(this).parent().find(".show-captcha").attr("src", captchaSrc+ '&rnd='+ Math.random());
	});
});

// 封装工具类方法
$(function () {
	window.NY = window.NY || {};

	// add feedback tips: warn/success/error
	if ($.dialog && $.dialog.tips) {
		var DEFAULT_TIPS_SHOW_DURATION = 3000;
		var tipsTypeList = ["warn", "success", "error"];
		var tipsTypeMap = {
			warn: "alert"
		};

		$.each(tipsTypeList, function (i, tipsType) {
			var basicMethodType = tipsTypeMap[tipsType] || tipsType;

			window.NY[tipsType] = function (text, duration) {
				duration = duration || DEFAULT_TIPS_SHOW_DURATION;

				$.dialog.tips(text, duration / 1000, basicMethodType);
			};
		});
	}

	// 作为ajax请求失败时 提示使用
	if (NY.warn) {
		NY.showBusy = function (duration) {
			NY.warn("服务器繁忙，请稍后重试！", duration);
		};
	}

	// 显示登录框
	if ($.dialog) {
		var loginHost = "/";
		var loginFrameURL = loginHost + "login/?type=frame";
		var loginActionURL = loginHost + "login/login.html";

		// loginSuccessCallback 仅登录成功后会调用
		NY.showLoginDialog = function (loginSuccessCallback, dialogConfig) {
			var loginDialog = null;
			var defaultDialogConfig = {
				title: "会员登录",
				okVal: "登录",
				width: 455,
				height: 220,
				ok: function () {
					var iframe = this.iframe.contentWindow;
					var iframe_form = $(iframe.document).find("form");
					var param = {};
					iframe_form.find("[name]").each(function () {
						var $_input = $(this);

						param[$_input.prop("name")] = $_input.val();
					});

					$.ajax({
						type: "post",
						url: loginActionURL,
						data: param,
						success: function (dataString) {
							var responseData = eval("(" + dataString + ")");

							if (!responseData.result) {
								NY.warn(responseData.text);

								return;
							}

							loginDialog.close();

							loginSuccessCallback.call(loginDialog, responseData);
						},
						error: function() {
							NY.showBusy();
						}
					});

					return false;
				},
				cancel: true
			};

			// 统一代理登录请求，不接收ok按钮事件重载
			delete dialogConfig.ok;
			var config = $.extend(defaultDialogConfig, dialogConfig);

			loginDialog = $.dialog.open(loginFrameURL, config);

			return loginDialog;
		};
	}
});

// 埋点统计
$(function () {
	var deleteCookie = function (name) {
		document.cookie = name + "=;path=/;expires=" + (new Date().toUTCString());
	};

	// #id=richu-xxx[-yyy]
	var hash = location.hash.slice(1);
	var matchGroups = hash.match(/(\-\d+)/g);
	if (!matchGroups || (matchGroups.length > 2)) {
		deleteCookie("channelID");
		deleteCookie("channelType");
		return;
	}

	var channelType = (matchGroups[0] || "").slice(1);
	var channelID = (matchGroups[1] || "").slice(1);
	if (!channelID) {
		channelID = channelType;
		deleteCookie("channelType");
	}
	else {
		document.cookie = "channelType=" + channelType + ";path=/";
	}
	document.cookie = "channelID=" + channelID + "; path=/";
});
