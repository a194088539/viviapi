define(function(require,exports,module) {
	var PasswordCtrl = require('PasswordCtrl');
	var loginPasswordCtrl;

	//@yu
	var tickerId;
	var tickerLeft = 60;
	//@yu 2015.8.26  加入此事件主要就是为了密码控件（阻止表单提交）
	$("#loginForm").submit(function() {
		return false;
	});


	$("#loginSubmit").click(function(){

		$("#loginForm").trigger("submit");

		var accountVal = $.trim($('#accountName').val());
		/*@2015-09-17支持um账号，邮箱，手机号登录*/
		/*if( accountVal.length !== 11 || !/^(1[^012][0-9]{9})$/i.test(accountVal) ){
		 $('#accountName').focus();
		 $('#errorMessage').text('请填写正确的遨游支付账户');
		 return false;
		 }*/
		if(!(isPhone(accountVal) || isEmail(accountVal))){
			$('#accountName').focus();
			$('#errorMessage').text('请填写正确的遨游支付账户');
			return false;
		}

		if(loginPasswordCtrl.isInstalled && loginPasswordCtrl.isSupport){
			if(!checkPwdHandler(true)){
				return false;
			}
		}else{
			if($('#pwd').val() === ""){
				$('#pwd').focus();
				$('#errorMessage').text('请填写您的登录密码');
				return false;
			}
		}

		if(loginPasswordCtrl.isInstalled && loginPasswordCtrl.isSupport){
			if(!checkPwdHandler(true)){
				return false;
			}
		}else{
			if($('#pwd').val() === ""){
				$('#pwd').focus();
				$('#errorMessage').text('请填写您的登录密码');
				return false;
			}
		}

		if($('#captcha\\.captchaValue').val() === ""){
			$('#captcha\\.captchaValue').focus();
			$('#errorMessage').text('请输入图片验证码');
			return false;
		}


		var data = $("#loginForm").serialize();
		$.ajax({
			dataType: 'JSON',
			type: 'POST',
			url: "/login/checkPwd",
			data: data
		})
		.done(function (response) {
			if ("000000" === response.code){
				$("#phoneNum").text(response.data.mobile);
				$("#otpToken").val(response.data.token);
				$("#otp\\.otpValue").val();
				$("#pop_tips").show();
			}
			else {
				window.location = "/session?errorCode=" + response.code;
			}
		})
		.fail(function () {
			window.location = "/session";
		});
	});

	$("#otpSend").click(function(){

		var data = {
			token: $("#otpToken").val()
		};

		$.ajax({
			dataType: 'JSON',
			type: 'POST',
			url: "/login/sendLoginOtp",
			data: data
		})
		.done(function (response) {
			if ("000000" == response.code) {
				$("#otp-help-block").text("");
				$("#otp-help-block").addClass("hidden");
				clearTimeout(tickerId);
				tickerId = 0;
				tickerLeft = 60;
				startTicker();

			} else {
				$("#otp-help-block").text("发送短信校验码失败");
				$("#otp-help-block").removeClass("hidden");
			}
		})
		.fail(function () {
			$("#otp-help-block").text("发送短信校验码失败");
			$("#otp-help-block").removeClass("hidden");
		});
	});

	function startTicker() {
		tickerId = setTimeout(function() {

			$("#otpSend").addClass("hidden");
			$("#otp-ticker").text(tickerLeft+"秒");
			$("#otp-ticker").addClass("inlineHidden");
			$("#otp-ticker").removeClass("hidden");
			tickerLeft = tickerLeft - 1;
			if (tickerLeft > 0) {
				startTicker();
				$("#otp-help-block").text("1分钟后若未收到校验码短信，请重新获取");
				$("#otp-help-block").removeClass("hidden");
			} else{
				tickerLeft = 0;
				$("#otpSend").removeClass("hidden");
				$("#otp-ticker").removeClass("inlineHidden");
				$("#otp-ticker").addClass("hidden");
			}
		}, 1000);
	}


	$("#loginDialogBtn").click(function(){

		var data = {
			token: $("#otpToken").val(),
			returnUrl: "/me",
			verifyCode: $("#otp\\.otpValue").val()
		};

		$.ajax({
			dataType: 'JSON',
			type: 'POST',
			url: "/login/validLoginOtp",
			data: data
		})
			.done(function (response) {
				if ("000000" == response.code) {
					window.location = response.data;
				} else {
					$("#otp-help-block").text("短信校验码错误");
					$("#otp-help-block").removeClass("hidden");
				}
			})
			.fail(function () {
				$("#otp-help-block").text("短信校验码错误");
				$("#otp-help-block").removeClass("hidden");
			});
	});

	$("#closeDlg").click(function(){
		clearTimeout(tickerId);
		tickerId = 0;
		tickerLeft = 60;
		$("#otp-help-block").text("");
		$("#otp-help-block").addClass("hidden");
		$("#otpSend").removeClass("hidden");
		$("#otp-ticker").removeClass("inlineHidden");
		$("#otp-ticker").addClass("hidden");
		$("#otp\\.otpValue").val();
		$("#phoneNum").text("");
		$("#otpToken").val("");
		$("#pop_tips").hide();
	});


	//@yu 2015.8.21 新增初始化密码控件
	function passwordInit(){
		loginPasswordCtrl= new PasswordCtrl({
			pwInput:"#pwd",
			pagId:"pwdCtrl",
			showPwStrengthBox: false, 	// 是否显示密码强度判断框
			isInstallMandatory: false, 	// 是否强制安装，默认为false
			showPgeInstall: true, 		// 是否显示'下载密码控件'提示
			checkPwdHandler: checkPwdHandler
		});
	}

	var checkPassWordResult=true;
	function checkPwdHandler(isSubmit) {
		var res = true;

		if (loginPasswordCtrl.pwdLength() == 0) {
			if(isSubmit || !checkPassWordResult){
				$('#errorMessage').text('请填写您的登录密码');
				res = false;
			}
		}

		checkPassWordResult = res;

		return res;
	}

	//@yu 2015.8.21 修改函数名并将初始化老密码控件删除
	function moduleInit(){
		if(!isSupportPlaceholder()) {
		    //input框,除了密码框
		    $('input').not("input[type='password']").each(
		      function() {
		        var self = $(this);
		        var val = self.attr("placeholder");
		        input(self, val);
		      });
		}

		$("#yiZhangTong").on('click',function(){
			$.ajax({
	        	 type : "post",
	             url : '/jointLogin/pinganone',
	             success :  function(response){
	            	 if (response.code == "000000") {
	            		 window.location.href = response.data;
	                 }else if(response.code == "999999"){
	                	 window.location.href = window.staticFileRoot +'/jointLogin/reserrormsg';
	                 }else{
	                	 $('#ajax_error').show();
	                	 $('#ajax_error').html(response.message);
	                 }
	             },
	             fail:function(response){
	            	 $('#ajax_error').show();
	            	 $('#ajax_error').html('系统异常，请稍后刷新页面重试');
	             }
	        });
		});


		$('#loginForm').on('submit',function(){
			var accountVal = $.trim($('#accountName').val());
			/*@2015-09-17支持um账号，邮箱，手机号登录*/
			/*if( accountVal.length !== 11 || !/^(1[^012][0-9]{9})$/i.test(accountVal) ){
				$('#accountName').focus();
				$('#errorMessage').text('请填写正确的遨游支付账户');
				return false;
			}*/
            if(!(isPhone(accountVal) || isEmail(accountVal))){
               $('#accountName').focus();
			   $('#errorMessage').text('请填写正确的遨游支付账户');
			   return false;
            }

			if(loginPasswordCtrl.isInstalled && loginPasswordCtrl.isSupport){
				if(!checkPwdHandler(true)){
					return false;
				}
			}else{
				if($('#pwd').val() === ""){
					$('#pwd').focus();
					$('#errorMessage').text('请填写您的登录密码');
					return false;
				}
			}

			if(loginPasswordCtrl.isInstalled && loginPasswordCtrl.isSupport){
				if(!checkPwdHandler(true)){
					return false;
				}
			}else{
				if($('#pwd').val() === ""){
					$('#pwd').focus();
					$('#errorMessage').text('请填写您的登录密码');
					return false;
				}
			}

			if($('#captcha\\.captchaValue').val() === ""){
				$('#captcha\\.captchaValue').focus();
				$('#errorMessage').text('请输入图片验证码');
				return false;
			}
		});

		//@yu 2015.8.21 密码控件更新（删除）
		//seajs.use(['pwdgrd'], function (PwdGrdModule) {
        //
		//    //prepare options for PwdGrdModule
		//    var ctrls = [];
		//    var ctrl1 = {
		//      id: 'pwd',
		//      hasPlaceHolder: false,
		//      sClass: 'passwordXactioned pafweblib-pwdGrd',
		//      iClass: 'passwordXaction pafweblib-pwdGrd-customized',
		//      nextTabId: 'captcha.captchaValue'
		//    };
		//    ctrls.push(ctrl1);
		//    var preSubmitFunc = function(pgeditorList){
		//        if($('#accountName').val()===''){
		//            $('#accountName').focus();
		//            $('#errorMessage').text('请填写您的遨游支付账户');
		//            return false;
		//        }
		//        if(pgeditorList){
		//            for(var i=0;i<pgeditorList.length;i++){
		//                var pgeditor = pgeditorList[i];
		//                if(0 == pgeditor.pwdLength()){
		//                    $("#" + pgeditor.id).focus();
		//                    $('#errorMessage').text('请填写您的登录密码');
		//                    return false;
		//                }
		//            }
		//        }
		//        if($('#captcha\\.captchaValue').val() === ''){
		//            $('#captcha\\.captchaValue').focus();
		//            $('#errorMessage').text('请输入图片验证码');
		//            return false;
		//        }
		//        return true;
		//    };
        //
		//    //init passguard Ctrl
		//    var pwdGrdModule = new PwdGrdModule({
		//        ctrls: ctrls,
		//        preSubmitFunc: preSubmitFunc,
		//        rootPath: window.contextPath
		//    });
		//});
	}
	
	// 是否支持placeholder属性
	function isSupportPlaceholder() {
	  var input = document.createElement('input');
	  return 'placeholder' in input;
	}

	// val----placeholder
	function input(obj, val) {
	  var $input = obj;
	  var val = val;
	  $input.attr({value:val}).addClass("placeholder");
	  $input.focus(function() {
	    if ($input.val() == val) {
	      $(this).attr({value:""}).removeClass("placeholder");
	    }
	  }).blur(function() {
	    if ($input.val() == "") {
	            $(this).attr({value:val}).addClass("placeholder");
	    }
	  });
	}
	
	function bindInit(){
		var clickValid = true;
		$("#business-updown").click(function(){
			$(this).toggleClass("business-upbtn").parent().toggleClass("business-hidden");;
		})
	        
        $("#refresh_captcha").click(function() {
            var that = this;
            if (clickValid) {
                clickValid = false;
                setTimeout(function(){
                    $.ajax({
                    url: "/CodeImage.aspx",
                        dataType: "json",
                        method: "POST"
                    }).done(function(data) {
                        var $t = $(that), idEl = $("#" + $t.data("id").replace(/[.]/g, "\\.")), imgEl = $t.find("img");
                        imgEl.prop("src", data.img);
                        idEl.prop("value", data.id);
                    }).always(function(){
                        clickValid = true;
                    });
                }, 300)
            }
            return false;
        });
	}

	function showGonggao(){
		var gonggao = require("./announcement");
		gonggao.init();
	}
	/*@2015-09-17支持um账号，邮箱，手机号登录*/
	function isPhone(p) {
        //判断是不是手机号
        var s = p.toString().replace(/\s/g, "");
        // 除 10、11、12外都可支持
        var pattern = /^(1[^012][0-9]{9})$/i;
        return pattern.test(s);
    }
	
	function isEmail(p) {
        //判断是不是邮箱
        var s = p.toString().replace(/\s/g, "");
        var pattern = /^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$/;
        return pattern.test(s);
    }
	
	exports.init = function(){
		moduleInit();
		if($("#pwd").length > 0){
			passwordInit();
		}

		bindInit();//初始化事件
		showGonggao();
	}
})