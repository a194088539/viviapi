//refresh verify code
function m_reload(url){
    document.getElementById("loginImg").src= url + "?times=" + new Date().getTime();
}

////切换账号到密码框的焦点
//function nameToPassFocus(event){
//	if(event.keyCode==9){
//		event.preventDefault(); 
//	}
//	
//	var e = event || window.event || arguments.callee.caller.arguments[0];
//	if((e && e.keyCode==13) || (e && e.keyCode==9)){
//		//var powerobj = document.getElementById(getObjID("powerpass"));
//		//powerobj.focus();
//		$("#j_username").blur();
//		if (!isIE()){
//			$("#powerpass_noie").focus();
//		}else{
//			$("#powerpass_ie").focus();
//		}
//	}
//}

//function loadPwd(){
	//if ( navigator.appName != 'Microsoft Internet Explorer'){
//	if (!isIE()) {
		//如果是非IE浏览器，则调用此函数，为控件添加事件处理函数。
//		doAdd();
//	}
//}

//function doAdd(){	
		 //获取对象
//	var powerpass = document.getElementById("powerpass_noie");
		//添加Password控件的Tab事件，如果收到此事件，则触发OnPassEventTab()函数
//	powerpass.addEventListener("EventTab",OnPassEventTab,false);
//	powerpass.addEventListener("EventReturn",OnPassEventTab,false);
		//addEvent(powerpass, "EventTab",OnPassEventTab);
//}

//function addEvent(obj, name, func){
//	obj.addEventListener(name, func, false); 
//}

//function OnPassEventTab(){
	//在收到Password控件上的Tab事件时，将焦点放在id为j_captcha_response的标签上。
//	document.getElementById("powerpass_noie").blur();
//	$("#j_username").focus();

//}

//在验证码框按回车焦点跳到登录按钮
function enterOnCapatcha(event){
	
    if(event.keyCode==13){
   	 $("#loginBtn").focus();
	}
}

//=========================================表单验证=============================================
//检查账号
//function checkUserName() {
//	var data = document.getElementById("j_username");
//	if (data == null || data.value == null || data.value == "") {
//		$("#messageBox").html("账户名不能为空");
//		$("#j_username").focus();
//		return false;
//	} else {
//		var rule =  /^[a-zA-Z0-9_]{4,16}$/;
//		if (!rule.test(data.value)) {
//			$("#messageBox").html("账号长4-16位，由字母、数字或下划线组成");
//			$("#j_username").focus();
//			return false;
//		}else{
//			return true;
//		}
//	}
//}

////检查密码
//function checkPassword(ts) {
//	var password = getIBSInput("powerpass", ts, "messageBox","密码");
//	if(password==null){
//		var powerobj = document.getElementById(getObjID("powerpass"));
//		powerobj.focus();
//		return false;
//	}else{
//		document.getElementById("messageBox").innerHTML ="";
//		document.getElementById("j_password").value=password;
//		return true;				
//	}
//}

////检查验证码
//function checkCapatcha() {
//	var data = document.getElementById("j_captcha_response");
//	if (data == null || data.value == null || data.value == "") {
//		$("#messageBox").html("验证码不能为空！");
//		$("#j_captcha_response").focus();
//		return false;
//	} else {
//		var rule = /^[a-zA-Z0-9]{4}$/;
//		if (!rule.test(data.value)){
//			$("#messageBox").html("验证码不正确！");
//			$("#j_captcha_response").focus();
//			return false;
//		}
//	}
//	return true;
//}


//====================================================================================
//防止密码控件浮动在导航层上，将其隐藏
function setPwdVisiable(){
	
	var top_h = $("#securityPwd").offset().top-$(window).scrollTop();
	
	if(top_h < 100){
		if (!isIE())
		    $("#powerpass_noie").css("visibility","hidden");
		else
			$("#powerpass_ie").css("visibility","hidden");
	}else{
		if (!isIE())
		    $("#powerpass_noie").css("visibility","visible");
		else
			$("#powerpass_ie").css("visibility","visible");
	}
}

//重设登录窗口位置
function resetloginPosition() {
	var top = ($(window).height() - $("#loginWindow").height()) / 2;
	var right = ($(window).width() - $("#loginWindow").width()) / 2;
	$("#loginWindow").css({
		top : top,
		right : right * 0.4
	});
}

$(document).ready(function() {
	//重置登录窗口位置
	resetloginPosition();
	//给按钮绑定登录方法
	$("#loginBtn").click(function(){
		login();
	});		
});

$(window).resize(function() {
	resetloginPosition();
	setPwdVisiable();
});

//悬停提示
$(function () {
	  $('[data-toggle="tooltip"]').tooltip()
});

$(window).scroll(function() {
	setPwdVisiable();
});

//fix method
function downlist_fix_pwd_hide(){
	if (!isIE())
	    $("#powerpass_noie").css("visibility","hidden");
	else
		$("#powerpass_ie").css("visibility","hidden");
}

function downlist_fix_pwd_show(){
	if (!isIE()){
		 $("#powerpass_noie").css("visibility","visible");
		// $("#powerpass_noie").focus();
	}
	else{
		$("#powerpass_ie").css("visibility","visible");
		//$("#powerpass_ie").focus();
	}
}
