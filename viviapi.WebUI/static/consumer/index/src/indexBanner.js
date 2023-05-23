define(function(require,exports,module){
	function imgInit(){
		var pafFocus = require('focusBeta');
		var imgUrl = window.cportalPathRoot + '/cportal/banner.jsonp';
		window.cportalPathRoot == "" ? imgUrl = '/static/consumer/index/examples/banner.js' : '';
		$.ajax({
			url: imgUrl,
			dataType:'jsonp',
			jsonp: "callback",
			jsonpCallback:"cportalIndexBanner",
			type:'get',
			cache: false,
            data: {},
			success: function(json){
				var imgData = [];
				if(json.length > 0){
					for(var i=0;i<json.length;i++){
						var flag = isShow(pafCurTime ,json[i].startTime , json[i].endTime);
						if(flag){
							imgData.push(json[i]);
						}
					}
					new pafFocus({gap: 5000,box: '#imgDiv',imgs: imgData});
				}
			},
			error: function(e){
				window.console && console.log(e.toString())
			}
		});
	}
	//当前服务器时间毫秒，开始时间，结束时间
	function isShow(curT,starT,endT){
		var flag = false;
		if(starT == ""){
			flag = false;
		}else if(endT != ""){
			if(curT > starT && curT < endT){
				flag = true;
			}else{
				flag = false;
			}
		}else{
			flag = true;
		}
		return flag;
	}
	//返回当前网络时间毫秒
	function getCurTime(){
		var T = this;
		T.time = "";
		$.ajax({
			url:'',
			type:'get',
			cache: false
		}).complete(function(xhr,data){
			T.time = (new Date(xhr.getResponseHeader("Date"))) || Date.parse(new Date());
		});
	}
	exports.init = function(){
		$.ajax({
			url:'',
			type:'get',
			cache: false
		}).complete(function(xhr,data){
			window.pafCurTime = (new Date(xhr.getResponseHeader("Date"))).getTime() || Date.parse(new Date());
			imgInit();//slide初始化
		});
	}
})