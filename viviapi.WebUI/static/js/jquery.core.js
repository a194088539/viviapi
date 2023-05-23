var core = {

	// 确认
	confirm:function(opts){

		var options = $.extend({
			title:'',
			content:'',
			ok:'',
			cancel:true,
			tips:'请稍后……',
			onSuccess:function(){
				location.reload();
			},
			onError:function(){
				
			},
			parent:null
		}, opts);


		return $.dialog({
			id : 'adminconfirm',
			title: options.title,
			content: options.content,
			icon: 'confirm',
			ok: function(){
				if(typeof(options.ok)=="function"){
					options.ok.call(this);
				}else{
					this.close();
					options.url = options.ok;
					delete options.ok;
					core.ajaxload(options);
				}
				return false;
			},
			cancel: options.cancel
		});
	},


	// AJAX加载
	ajaxload:function(opts){

		var loading;

		var options = $.extend({
			tips:'请稍后……',
			type:'GET',
			dataType:"html",
			data:null,
			success:null
		}, opts);

		loading = $.dialog.tips(options.tips, 600, 'loading');

		$.ajax({
			type: options.type,
			cache: false,
			dataType: options.dataType,
			url: this.rndurl(options.url),
			data:options.data,
			success: function(data, textStatus){

				var json = eval('(' + data + ')');

				loading.close();

				delete options.url;

				var json = $.extend(options, json);

				core.ajaxcallback(json);

			},
			error: function(XMLHttpRequest, textStatus, errorThrown){
				
			}
		});
	},



	// 处理AJAX返回
	ajaxcallback:function(options){

		var icon = (options.result) ? 'success' : 'error';
		var success;

		if (options.ajaxreload){
			success = function(){
				core.ajaxreload()
			};
		}

		if (options.reload){
			success = function(){
				location.reload()
			};
		}

		if (options.confirm){

			$.dialog.confirm(options.confirm.text, options.confirm.yes, options.confirm.no);

		}else{

			if (options.text){

				if (options.time==undefined){

					if(typeof(options.ok)=='boolean'){
						success = options.ok;
					}else{
						if(options.ok != undefined){
							if(options.ok.length>0){
								if (options.ok.substring(0,1)=="#"){
									success = function (){
										window.location = $(options.ok).val();
									}
								}else{
									success = function (){
										window.location = core.rndurl(options.ok);
									}
								}
							}
						}
					}

					if(options.no!=undefined){
						if(typeof(options.no)=='boolean'){
							if(options.no.length>0){
								if (options.no.substring(0,1)=="#"){
									options.cancel = function (){
										window.location = $(options.no).val();
									}
								}else{
									options.cancel = function (){
										window.location = core.rndurl(options.no);
									}
								}
							}else{
								options.cancel = true;
							}
						}else{
							options.cancel = options.no;
						}
					}else{
						options.cancel = false;
					}


					if (options.result){
						if (options.success && typeof(options.success)=="function") {
							success = function(){
								options.success.call(options.success, options);
							};
						}
					}


					$.dialog({
						title: "消息提示",
						lock: true, 
						content: options.text, 
						icon: icon,
						ok: success,
						cancel: options.cancel,
						parent: options.parent,
						close:options.error
					});

				}else{

					if (options.result){

						if (options.success && typeof(options.success)=="function") {

							success = options.success;

						}else{

							if(options.url != undefined){
								if(options.url.length>0){
									if (options.url.substring(0,1)=="#"){
										success = function (){
											window.location = $(options.url).val();
										}
									}else{
										success = function (){
											window.location = options.url;
										}
									}
								}
							}

						}

						$.dialog.tips(options.text, options.time, icon, function(){

							setTimeout(function(){
								success.call(success, options);
							}, 100);

						});

					}else{

						$.dialog.tips(options.text, options.time, icon, options.error);

					}

				}

			}else{

				var success = options.success;
				if (success){
					success.call(success, options);
				}

			}

		}

	},




	rndurl:function(url){
		if (url.indexOf("?")<=0){
			url += "?r=" + Math.round(Math.random() * 10000);
		}else{
			url += "&r=" + Math.round(Math.random() * 10000);
		}
		return url;
	},

	alert:function(content, title, ok){
		if (!title){
			title = "提示信息";
		}
		if (!ok){
			ok = true;
		}
		$.dialog({
			title:title,
			content:content,
			icon:'alert',
			ok:ok
		});
	},

	error:function(content){
		$.dialog({
			title:'错误提示',
			content:content,
			icon:'error',
			ok:true
		});
	},


	tips:function(content, time, icon, callback){
		return $.dialog.tips(content, time, icon, callback);
	},


	parseMoney:function(val){
		if (val==undefined || isNaN(val)){
			return '';
		}
		val = parseFloat(val);
		val = Math.round(val * 100) / 100;
		return val;
	},


	loginStatus:function(options){

		var opts = $.extend({
			async:false,
			url:'/login/check.html?rnd=' + Math.random()
		}, options);

		$.ajax(opts);

	},


	// 登录窗
	login:function(options){

		var opts = $.extend({}, options, {
			success:function(data){

				if (data!='1'){
					var loginBox = $.dialog.open('/login/?type=frame', {
						title:'会员登录',
						width:455,
						height:220,
						okVal:'登录并提交',
						ok:function(){

							var box = this;
							var iframe = this.iframe.contentWindow;
							var iframe_form = $(iframe.document).find("form");

							var loginSuccess = options.success;

							options.success = function(){
								box.close();
								loginSuccess.call(loginSuccess, {});
							};

							iframe_form.ajaxform(options);

							return false;

						},
						cancel:true
					});
				}else{
					options.success.call(options.success, data);
				}
			}

		});

		this.loginStatus(opts);
	},



	request:function(name) {
		var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
		var r = window.location.search.substr(1).match(reg);
		if (r != null) return unescape(r[2]);
		return '';
	}



};




$.fn.ajaxform = function(opts){




	var loading = $.dialog.tips("提交中，请稍后……", 60, "loading");


	var that = this;
	var button = opts.button;


	if(button){
		var button_val = button.val();
		button.attr("disabled", true).val("请稍后...");
	}



	var options = {

		contentType: "application/x-www-form-urlencoded;charset=utf-8",

		success:function(data){

			loading.close();

			if(button){
				button.attr("disabled", false).val(button_val);	
			}

			var json = eval('json = ' + data);

			opts = $.extend({}, opts, json);

			core.ajaxcallback(opts);

		},

		error:function(XMLHttpRequest, textStatus, errorThrown){

		}
	};

	this.ajaxSubmit(options);

}; 







