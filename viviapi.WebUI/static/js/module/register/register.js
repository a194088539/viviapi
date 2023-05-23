$(function() {
$.dialog.tips(json.text, 1, 'success', function() {
    window.location = '/profile.aspx?id=' + json.id;
});

	//判断密码强度
	$("#password").keyup(function (e) {
		var strongRegex = new RegExp("^(?=.{8,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*$", "g");
		var mediumRegex = new RegExp("^(?=.{7,})(((?=.*[A-Z])(?=.*[a-z]))|((?=.*[A-Z])(?=.*[0-9]))|((?=.*[a-z])(?=.*[0-9]))).*$", "g");
		var weakRegex = new RegExp("(?=.{6,}).*", "g");
		var $pwdLevelBar = $(".pwd-strength li");
		var $pwdLevelTxt = $(".show-pwd-level");
		if ($(this).val()) {
			if (false == weakRegex.test($(this).val())) {
				$pwdLevelBar.removeClass();
				$pwdLevelTxt.addClass("hide");
				$(".pwd-short").removeClass("hide");
			} else if (strongRegex.test($(this).val())) {
				$pwdLevelBar.removeClass();
				$pwdLevelBar.eq(0).addClass("levelbar-strong");
				$pwdLevelBar.eq(1).addClass("levelbar-strong");
				$pwdLevelBar.eq(2).addClass("levelbar-strong");
				$pwdLevelTxt.addClass("hide");
				$(".pwd-strong").removeClass("hide");
			} else if (mediumRegex.test($(this).val())) {
				$pwdLevelBar.removeClass();
				$pwdLevelBar.eq(0).addClass("levelbar-medium");
				$pwdLevelBar.eq(1).addClass("levelbar-medium");
				$pwdLevelTxt.addClass("hide");
				$(".pwd-medium").removeClass("hide");
			} else {
				$pwdLevelBar.removeClass();
				$pwdLevelBar.eq(0).addClass("levelbar-weak");
				$pwdLevelTxt.addClass("hide");
				$(".pwd-weak").removeClass("hide");
			}
			return true;
		} else {
			$pwdLevelBar.removeClass();
			$pwdLevelTxt.addClass("hide");
			$(".pwd-null").removeClass("hide");
		}

	});

	$("#agreementCheckbox").change(function () {
		var checkboxTrigger = $("#checkboxTrigger");
		var submitBtn = $("#submitBtn");
		if ($(this).prop("checked")) {
			checkboxTrigger.addClass("checkbox-icon-checked");
			submitBtn.addClass("reg-submit").attr("disabled", false);
		} else {
			checkboxTrigger.removeClass("checkbox-icon-checked");
			submitBtn.removeClass("reg-submit").attr("disabled", true);
		}
	});

	//表单验证\
	var config = {
		passwordMin:6,
		passwordMax:20
	};
	$.formValidator.initConfig({
		formID: "register", theme: "ArrowSolidBox", debug: false, submitOnce: true, onSuccess: function () {
			$(".reg-submit").attr("disabled", true);
			var tips = core.tips("正在提交，请稍后……", 60, "loading");
			$("#register").ajaxSubmit({
				success: function (data) {

					tips.close();
					var json = (typeof data == "string") ? eval('(' + data + ')') : data;
					if (json.result) {
						$.dialog.tips(json.text, 1, 'success', function () {
						window.location = '/profile.aspx?id=' + json.id;
						});
					} else {
						$.dialog.tips(json.text, 2, 'error', function () {
							$("#submitBtn").attr("disabled", false);
						});
					}
				}
			});

		},
		submitAfterAjaxPrompt: '有数据正在异步验证，请稍等...'
	});
	$("#mobile").formValidator({
		onShow: "手机号码可用于登录、激活账号、密码找回",
		onFocus: "手机号码可用于登录、激活账号、密码找回",
		onCorrect: "填写正确"
	}).inputValidator({min: 11, max: 11, onError: "手机号码填写错误"}).regexValidator({
		regExp: "^1[34578][0-9]{9}$",
		onError: "你输入的手机号码不正确"
	}).ajaxValidator({
		dataType: "html",
		async: true,
		//手机号码是否可用验证接口
		url: "/register/ajaxcheckmobile.ashx",
		success: function (data) {
			if (data == '1') {
				return true;
			} else {
				return false;
			}
			return false;
		},
		error: function (jqXHR, textStatus, errorThrown) {
			$.dialog({
				title: "错误提示",
				content: "服务器没有返回数据！",
				icon: "error"
			});
		},
		onError: function () {
			return "该手机号码不可用";
		},
		onWait: "请稍候..."
	});
	$("#password").formValidator({
		onShow: "密码由" + config.passwordMin + "-" + config.passwordMax + "个任意字符组成",
		onFocus: "密码由" + config.passwordMin + "-" + config.passwordMax + "个任意字符组成",
		onCorrect: "密码填写正确"
	}).inputValidator({min: config.passwordMin, max: config.passwordMax, onError: "密码填写错误"});
	$("#password2").formValidator({onShow: "请再次输入密码", onFocus: "请再次输入密码", onCorrect: "填写正确"}).inputValidator({
		min: 1,
		onError: "请再次输入密码"
	}).compareValidator({desID: "password", operateor: "=", onError: "两次输入的密码不一致"});
	$("#mobilecode").formValidator({onShow:"请输入收到的6位短信效验码",onFocus:"请输入收到的6位短信效验码",onCorrect:"短信效验码填写正确"}).inputValidator({min:6,max:6,onError:"短信效验码填写错误"}).ajaxValidator({
		dataType : "html",
		async : true,
		//手机短信验证码接口
		url : "/register/ajaxcheckmobilecode.ashx",
		success : function(data){
			if(data=='1'){
				return true;
			}else{
				return false;
			}
			return false;
		},
		buttons: $("#button"),
		error: function(jqXHR, textStatus, errorThrown){alert("服务器没有返回数据！");},
		onError : "短信效验码错误",
		onWait : "请稍候..."
	});

	var sendCodeBtn = $("#sendcode");
	//	短信发送后倒计方法
	var sendCountdown = function(countdownTime){

		if(countdownTime > 0){
			sendCodeBtn.attr("disabled", true);
			sendCodeBtn.addClass("send-captcha-disabled");
			sendCodeBtn.val((countdownTime--) + "秒后重新发送");
			var timer = setTimeout(function(){
				sendCountdown(countdownTime);
			},1000);

		}else{
			sendCodeBtn.attr("disabled", false);
			sendCodeBtn.removeClass("send-captcha-disabled");
			sendCodeBtn.val("点击再次发送");
			countdownTime = 60;
		}
	};
	var sendtime = $("#sendtime").val();

	//当剩余时间大于0时页面保持倒计时
	if(sendtime > 0) {
		sendCountdown(sendtime);
	}
	sendCodeBtn.click(function(){
		if (!$("#mobileTip").hasClass("onCorrect")){
			$.dialog.tips(
				"请先填写您的手机号码",
				1.5,
				"alert"
			);
			return false;
		}else{
			//验证码页面
			var inputCaptchaBox = $.dialog({
				content:$("#captchaCheck")[0],
				title:'请输入验证码',
				width: 380,
				height: 110,
				okVal:'确认发送',
				ok:function(){
					var captchaCodeVal = $("#captcha").val();
					var mobileNum = $("#mobile").val();
					$.ajax({
						//验证码验证接口
						url:"/register/sendMobileCode.ashx?mobile=" + mobileNum + "&captcha=" + captchaCodeVal,
						cache:false,
						dataType:"json",
						success:function(data){
							//如果验证码正确
							if(data.result){
								inputCaptchaBox.close();
								$.dialog.tips(
									data.text,
									2,
									"success"
								);
								//	短信发送后倒计时60s
								sendCountdown(60);
							}else{
								$.dialog.tips(
									data.text,
									2,
									"error"
								);
							}
						},
						error:function(){
							$.dialog({
								title:'系统繁忙',
								content:"请重试",
								icon:'alert',
								ok:true
							});
						}
					});
					return false;
				},
				cancel:true
			});
		}
	});
});

