/*根据元素ID查找元素*/
function $() {
	var elements = new Array();
	for (var i = 0; i < arguments.length; i++) {
		var element = arguments[i];
		if (typeof element == 'string')
			element = document.getElementById(element);	
	    if (arguments.length == 1)
			return element;
		elements.push(element);
  }
  return elements;
}

/*根据元素ID取得元素*/
function $E(elemid) {
	return document.getElementById(elemid);
}

/*根据元素ID取得元素的value*/
function $Vo(elemid) {
	return document.getElementById(elemid).value;
}

/*根据元素ID取得元素的innerHTML*/
function $H(elemid) {
	return document.getElementById(elemid).innerHTML;
}

/*根据元素ID隐藏该元素*/
function $Hide(id) {
    document.getElementById(id).style.display = 'none';
}

/*根据元素ID显示该元素*/
function $Display(id) {
    document.getElementById(id).style.display = 'block';
}

/*根据元素ID设置该元素为空*/
function $Dlose(id) {
    document.getElementById(id).innerHTML='';
}

/*网页转向*/
function $U(url){
	window.location.href=url;
}

/*取字符长度，一个中文字符为两个字节*/
function $Len(str){
	//return (''+str).replace(/[^\x0000-\xFF00]/gi,'xx').length;
	return str.length;
}

//用正则表达式将前后空格用空字符串替代。
function trim(strSrc)
{
	return strSrc.replace(/(^\s*)|(\s*$)/g, "");
}

//onkeypress时根据输入类型控制输入字符,"F":浮点型 "I":整型 "D":日期型
function filterKey(sType){
	var iKey = window.event.keyCode;
	
	if(sType == "F"){ //浮点型
	
		if(iKey != 45 && iKey != 46 && iKey != 13 && iKey != 11 && !(iKey>=48 && iKey<=57))
			window.event.keyCode = 0
		else{
			if(iKey == 46){
				var obj = window.event.srcElement;
				if(obj.value.indexOf(".")>=0)
					window.event.keyCode = 0;
			}
			if(iKey == 45){
				var obj = window.event.srcElement;
				if(obj.value.indexOf("-")>=0)
					window.event.keyCode = 0;
				else{
					window.event.keyCode = 0;
					obj.value = "-" + obj.value;
					if(obj.onchange != null){
						obj.onchange();
					}
				}
			}
		}
	} //end "F"
	else if(sType == "I"){ //整型
		if(iKey != 13 && iKey != 45 && iKey != 11 && !(iKey>=48 && iKey<=57))
			window.event.keyCode = 0;
		else if(iKey == 45){
			var obj = window.event.srcElement;
			if(obj.value.indexOf("-")>=0)
				window.event.keyCode = 0;
			else{
				window.event.keyCode = 0;
				obj.value = "-" + obj.value;
				if(obj.onchange != null){
					obj.onchange();
				}
			}
		}
	}	// end "I"
	else if(sType == "D"){ //日期型
		var obj = window.event.srcElement;
		var strDate = obj.value;
		
		if(strDate.length>=10){
			window.event.keyCode = 0;
			return;
		}
		else if(strDate.length<4){ //年
			if(iKey != 13 && iKey != 11 && !(iKey>=48 && iKey<=57))
				window.event.keyCode = 0;
		}
		else if(strDate.length == 4){ //分隔符
			if(iKey != 45 && iKey != 47)
				window.event.keyCode = 0;
		}
		else if(strDate.length>=5){
			if( strDate.indexOf("-") > 0 && strDate.indexOf("-") == strDate.lastIndexOf("-")){ //正输入月份
				if(strDate.length>=7 && iKey != 45){ //如果长度过长，则退出
					window.event.keyCode = 0;
					return;
				}
				
				if(iKey>=48 && iKey<=57){
					var iPos = strDate.indexOf("-");
					
					var iMonth = parseInt("" + strDate.substr(iPos+1,strDate.length-iPos-1) + (parseInt(iKey) - 48));
					
					if(strDate.length>=6 && (iMonth <1 || iMonth > 12)){
						window.event.keyCode = 0;
						return;
					}
				}
			} // end if("-")
			else if( strDate.indexOf("/") > 0 && strDate.indexOf("/") == strDate.lastIndexOf("/")){ //正输入月份
				if(strDate.length>=7 && iKey != 47){ //如果长度过长，则退出
					window.event.keyCode = 0;
					return;
				}
				
				if(iKey>=48 && iKey<=57){
					var iPos = strDate.indexOf("/");
					
					var iMonth = parseInt("" + strDate.substr(iPos+1,strDate.length-iPos-1) + (parseInt(iKey) - 48));
					
					if(strDate.length >= 6 && (iMonth <1 || iMonth > 12)){
						window.event.keyCode = 0;
						return;
					}
				}
			} // end if("/")
			else if( strDate.indexOf("-") > 0 && strDate.indexOf("-") != strDate.lastIndexOf("-")){ //正输入日期
				if(iKey>=48 && iKey<=57){
					var iPos = strDate.lastIndexOf("-");
					if(strDate.length - iPos > 2){
						window.event.keyCode = 0;
						return;
					}
					
					var iDay = parseInt("" + strDate.substr(iPos+1,strDate.length-iPos-1) + (parseInt(iKey) - 48));
					
					if(iDay > 31){
						window.event.keyCode = 0;
						return;
					}
				}
			}
			else if( strDate.indexOf("/") > 0 && strDate.indexOf("/") != strDate.lastIndexOf("/")){ //正输入日期
				if(iKey>=48 && iKey<=57){
					var iPos = strDate.lastIndexOf("/");
					if(strDate.length - iPos > 2){
						window.event.keyCode = 0;
						return;
					}
					
					var iDay = parseInt("" + strDate.substr(iPos+1,strDate.length-iPos-1) + (parseInt(iKey) - 48));
					
					if(iDay > 31){
						window.event.keyCode = 0;
						return;
					}
				}
			}
			
		}
		
		if(strDate.charAt(strDate.length-1) == "-" || strDate.charAt(strDate.length-1) == "/"){
			if(iKey != 13 && iKey != 11 && !(iKey>=48 && iKey<=57))
				window.event.keyCode = 0;
		}
		
		if(iKey == 47 || iKey == 45){
			if(strDate.indexOf("-") > 0 && iKey == 47)
				window.event.keyCode = 0;
			if(strDate.indexOf("/") > 0 && iKey == 45)
				window.event.keyCode = 0;
		}
	} // end "D"
	
	
}

/*自适应大小*/ 
function DrawImage(ImgD,_width,_height){
	if(!_width) _width=120;
	if(!_height) _height=120;
	var flag=false;
	var image=new Image();
	image.src=ImgD.src;
	if(image.width>0&&image.height>0){
		flag=true;
		if(image.width/image.height>=_width/_height){//120/120
			if(image.width>_width){   
				ImgD.width=_width;
				ImgD.height=(image.height*_width)/image.width;
			}else{
				ImgD.width=image.width;   
				ImgD.height=image.height;
			}
			ImgD.alt=image.width+"X"+image.height;
		}
		else{
			if(image.height>_height){   
				ImgD.height=_height;
				ImgD.width=(image.width*_height)/image.height;   
			}else{
				ImgD.width=image.width;   
				ImgD.height=image.height;
			}
			ImgD.alt=image.width+"X"+image.height;
		}
	}
}  

/*Cookie*/
function setCookie(name,value)
{
    var Days = 365;
    var exp  = new Date();    //new Date("December 31, 9998");
        exp.setTime(exp.getTime() + Days*24*60*60);
        document.cookie = name + "="+ escape (value) + ";expires=" + exp.toGMTString();
}
/*secure--ture for ssl*/
function setCookie(name,value,secure)
{
    var Days = 365;
    var exp  = new Date();    //new Date("December 31, 9998");
        exp.setTime(exp.getTime() + Days*24*60*60);
        document.cookie = name + "="+ escape (value) + ";expires=" + exp.toGMTString()+((secure==true) ? "; secure" : "");;

}
function getCookie(name)
{
    var arr,reg=new RegExp("(^| )"+name+"=([^;]*)(;|$)");
	if(arr=document.cookie.match(reg)) 
		return unescape(arr[2]);
	else 
		return null;
}
function delCookie(name)
{
    var exp = new Date();
        exp.setTime(exp.getTime() - 1);
    var cval=getCookie(name);
        if(cval!=null) 
        	document.cookie= name + "="+cval+";expires="+exp.toGMTString();
}

/*验证数字*/
function isNumber(e){
	var number = "1234567890";
	for(var i=0; i<e.length; i++){
		if (number.indexOf(e.charAt(i))<0) {
			return false;
		}
	}
	return true;
}

/*验证数字*/
function isAllDigits(argvalue) {
    argvalue = argvalue.toString();
    var validChars = "0123456789";
    var startFrom = 0;
    if (argvalue.substring(0, 2) == "0x") {
       validChars = "0123456789abcdefABCDEF";
       startFrom = 2;
    } else if (argvalue.charAt(0) == "0") {
       validChars = "01234567";
       startFrom = 1;
    } else if (argvalue.charAt(0) == "-") {
        startFrom = 1;
    }
    
    for (var n = startFrom; n < argvalue.length; n++) {
        if (validChars.indexOf(argvalue.substring(n, n+1)) == -1) return false;
    }
    return true;
}

/*检查Email是否合法*/
function isEmail(s){
    if (s.length<7||s.length > 50){
            return false;
    }
     var regu = "^(([0-9a-zA-Z]+)|([0-9a-zA-Z]+[_.0-9a-zA-Z-]*[0-9a-zA-Z]+))@([a-zA-Z0-9-]+[.])+([a-zA-Z]{2}|net|NET|com|COM|gov|GOV|mil|MIL|org|ORG|edu|EDU|int|INT)$"
     var re = new RegExp(regu);
     if (s.search(re) != -1) {
           return true;
     } else {
           return false;
     }
}

/*检查字符串是否为Null*/
function isNull(s){
    if (s == null || s.length <= 0 || s.trim() == ""){
            return true;
    }
    return false;
}

/*检查字符串是否为空*/
function isEmpty(s){
    if (s == null || s.length <= 0 || s.trim() == ""){
            return true;
    }
    return false;
}

/*检查日期是否合法*/
function isValidDate(day, month, year) {
    if (month < 1 || month > 12) {
            return false;
        }
        if (day < 1 || day > 31) {
            return false;
        }
        if ((month == 4 || month == 6 || month == 9 || month == 11) &&
            (day == 31)) {
            return false;
        }
        if (month == 2) {
            var leap = (year % 4 == 0 &&
                       (year % 100 != 0 || year % 400 == 0));
            if (day>29 || (day == 29 && !leap)) {
                return false;
            }
        }
        return true;
    }

/*获得Radio的值*/
function getRadioValue(name){	
	var radios = document.getElementsByName(name);
	var i;   
	if (null == radios.length){
	  	if(radios.checked) {
	  		return radios.value;
	  	}
	}
    for(i = 0; i < radios.length; i++){
       if(radios[i].checked){
     		return radios[i].value;
       }
    }
    return 0;
}

/*设置Radio的值*/
function setRadioValue(name,value){	
	var radios = document.getElementsByName(name);
	var i;
	if (null == radios.length){
	  	if(radios.checked) {
	  		radios.checked = "checked";
	  	}
	}
    for(i=0;i<radios.length;i++){
       if(value == radios[i].value){
     		radios[i].checked = "checked";
       }
    }
    return 0;
}

/*获得CheckBox的值,多个为数组*/
function getCheckBoxValues(name){	
	var values = new Array();
	var cbs = document.getElementsByName(name);
	var i;   
	if (null == cbs) return values;	  
	if (null == cbs.length){
	  	if(cbs.checked) {
	  		values[values.length] = cbs.value;
	  	}
	  	return values;
	}	    
	var count = 0 ;  	
	for(i = 0; i<cbs.length; i++){
		if(cbs[i].checked){
			values[values.length] = cbs[i].value;
		}
	}
	return values;
}

/*设置CheckBox的值*/
function setCheckBoxValue(name,value){
	var cbs = document.getElementsByName(name);
	var i;
    if (null == cbs) return 0 ;
  	if (null == cbs.length){
  		cbs.checked = value;
  		return 0;
  	}
	for(i=0;i<cbs.length;i++){
  		cbs[i].checked = value;
  	}
  	return 0;
}

/*设置CheckBox选中状态*/
function setCheckBoxs(name,value){
	var cbs = document.getElementsByName(name);
	var i;
    if (null == cbs) return 0 ;
  	if (null == cbs.length){
  		cbs.checked = true;
  		return 0;
  	}
	for(i=0;i<cbs.length;i++){
		if(cbs[i].value == value){
			cbs[i].checked = true;
		}
  	}
  	return 0;
}

function htmlEncode(text) {
	return text.replace(/&/g, '&amp;').replace(/"/g, '&quot;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
}

var Request = new Object();
Request.send = function(url, method, callback, data, urlencoded) {
    var req;
    if (window.XMLHttpRequest) {
        req = new XMLHttpRequest();
        if (req.overrideMimeType) {
			req.overrideMimeType('text/xml');
		}
    } else if (window.ActiveXObject) {
        req = new ActiveXObject("Microsoft.XMLHTTP");
    }
    req.onreadystatechange = function() {
        if (req.readyState == 4) {
            if (req.status < 400) {
                (method=="POST") ? callback(req) : callback(req,data);
            } else {
            }
        }
    }
    if (method=="POST") {
        req.open("POST", url, true);
        if (urlencoded) req.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        req.send(data);
    } else {
        req.open("GET", url, true);
        req.send(null);
    }
   // return req;
}

Request.sendRawPOST = function(url, data, callback) {
    Request.send(url, "POST", callback, data, false);
}
Request.sendPOST = function(url, data, callback) {
    Request.send(url, "POST", callback, data, true);
}
Request.sendGET = function(url,callback) {
    Request.send(url, "GET", callback, null, null);
}

/*分析status文件的内容*/
function parseResponseStatus(documentElement){
     var dataobj = new Object();
	 //parse status
	 var status = documentElement.getElementsByTagName("status");
	 if(status && status.length > 0){
		 if(status[0].firstChild){
			dataobj.status = status[0].firstChild.nodeValue;			 
		 }
		else{
			dataobj.status = "";			 
		}
	 }
	 return dataobj;	
}

//分页
function page(datastr){
	eval("var obj = "+datastr+";");
	dojo.io.bind({
		url: parseUrl(obj.url),
		load: function(type, data, evt) {
			$E(obj.id).innerHTML = data;
			loadScripts(data);
		},
		error: function(type, error) { 
			alert("error");
		},
		mimetype: "text/plain"	
	});
}

//提交form，把得到的数据放在指定的ID上.
function ajaxFormRequest(datastr) {
	try{

		eval("var obj = "+datastr+";");
		
		if (obj.id == null)
	  		obj.id = "paginationResult";

		dojo.io.bind({
		url: parseUrl(obj.url),
		useCache: false,
		preventCache: false,
		encoding:'UTF-8',
		load: function(type, data, evt) {
			
			if(document.getElementById(obj.id) == null){
		  		return;
		  	}
		  	
			$E(obj.id).innerHTML = data;
			
			loadScripts(data);
			
			enableBtn();
		},
		
		error: function(type, error) { alert("error");},
		mimetype: "text/plain",
		formNode: $E(obj.form)
		});
		
	}catch(e){
		
	}
}

//提交form，把得到的数据放在指定的ID上.
//给"我的快钱"显示站内短信使用,当取不到短信时也不alert,因为取短信的请求是不停刷新的
function ajaxFormRequestNoAlert(datastr) {
	eval("var obj = "+datastr+";");
	if (obj.id == null)
  		obj.id = "paginationResult";  		
	dojo.io.bind({
	url: parseUrl(obj.url),
	useCache: false,
	preventCache: false,
	encoding:'UTF-8',
	load: function(type, data, evt) {
		$E(obj.id).innerHTML = data;
		loadScripts(data);
	},
	error: function(type, error) { },
	mimetype: "text/plain",
	formNode: $E(obj.form)
	});
}


//Ajax请求
function ajaxRequest(obj) {
	dojo.io.bind({
	url: parseUrl(obj.url),
	useCache: false,
	preventCache: false,
	load: function(type, data, evt) {
	document.getElementById(obj.id).innerHTML=data;
	loadScripts(data);
	//dlg.show();
	},
	error: function(type, error) { alert("没有找到该会员");},
	mimetype: "text/plain"
	// and many more options!
	});
}

//将显示分页的结果
function pagination(obj)
{
  if (obj.id == null||obj.id=="null")
  obj.id = "paginationResult";
  document.getElementById(obj.id).innerHTML='<div align="center"><img src="/seashell/website/img/button/tiao.gif" alt="进度条" width="161" height="20" />处理中，请稍候...<br /></div>';
  this.disabled = true;
  ajaxRequest(obj); 
}
//分页时,请求到第几页数据
function goPage(dataObject)
{
   var page=1;
   if (document.getElementById('gopage').value != null &&document.getElementById('gopage').value != '')	
     page=document.getElementById('gopage').value;   
   dataObject.url=dataObject.url+'&page='+page;
   pagination(dataObject);
}	

//执行文本字符中的<script>对.
function loadScripts(text) {
	match = new RegExp('(?:<script.*?>)((\n|.)*?)(?:<\/script>)', 'im'); 
	var scripts  = text.match(match); 
	if(scripts && scripts.length){
		for (var i = 0; i < scripts.length; i++){ 
			if(scripts[i] && scripts[i].match(match) && scripts[i].match(match)[1]){
		    	eval(scripts[i].match(match)[1]); 
		    }
		}   
	}  
}

function AjaxCall(location,callback) { // give as many args as you want
	var args = arguments;
	dojo.io.bind({
    	url: parseUrl(location),
    	useCache: false,
		preventCache: false,
    	load: function(type, data, http) {
      		var newArgs = [type, data, http];
      		for(var i = 1; i < args.length; i++) {
        		newArgs.push(args[i]);
      		}
      	callback.apply(this, newArgs);
    }
	});
}


function ajaxNoCacheRequest(obj) {
	dojo.io.bind({
	url: parseUrl(obj.url),
	//disable cache
	useCache: false,
	preventCache: false,
	load: function(type, data, evt) {
	document.getElementById(obj.id).innerHTML=data;
	loadScripts(data);
	//dlg.show();
	},
	error: function(type, error) { alert("connection error.");},
	mimetype: "text/plain"
	// and many more options!
	});
}

//利用ajax删除列表
function listRemove(objs) {
	if (!confirm('您真要要进行删除吗?'))
	  return; 
	dojo.io.bind({
    	url: parseUrl(objs.url),
    	mimetype: "text/xml",
    	useCache: false,
		preventCache: false,
    	load: function(type, data, httpreq) {    		  
    	var rss = data.documentElement;
    	var header = {      
            status:0
        };          
      	var i=data.getElementsByTagName('status')[0].firstChild.data;	      	
      	if (i=="1")      	
      	   $Hide(objs.delID);      	         	
      	else
      	 alert(i);       	
      	}
    
	});
}

//提交form，把得到的数据放在指定的ID上.
function ajaxFormRequestReversendMoney(datastr) {
	try{
		eval("var obj = "+datastr+";");
		
		if (obj.id == null)
	  		obj.id = "ReversendMoneyResult";

		dojo.io.bind({
		url: parseUrl(obj.url),
		useCache: false,
		preventCache: false,
		encoding:'UTF-8',
		load: function(type, data, evt) {
			
			if(document.getElementById(obj.id) == null){
		  		return;
		  	}
		  	
			$E(obj.id).innerHTML = data;
			
			loadScripts(data);
		},
		
		error: function(type, error) { alert("error");},
		mimetype: "text/plain",
		formNode: $E(obj.form)
		});
	  
	}catch(e){
		
	}
}

//打开一个模式窗口
function openWindow(url) {
if (window.showModalDialog)
  window.showModalDialog(url,'','unadorned:yes;dialogWidth:755px;dialogHeight:550px');
else 
  window.open(url,'','width=755,height=550,toolbar=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes ,modal=yes');
}
// 获取XML某个节点中的文本
// 因为IE和Gecko对节点文本的实现方法不同，所以用这个函数进行封装
// IE: node.text ; Gecko: node.textContent
function getXmlNodeText(node) {
   var undefined;
   if (node !== null) {  // 判断节点不为空才获取
       if (undefined !== node.text) {
           return node.text;
       } else if (undefined !== node.textContent) {
           return node.textContent;
       }
   }
   else
     alert('null');   
   return undefined;
}
function getXmlData(xml, obj) {

  for (o in obj) {

    if ("object" == typeof(obj[o])) {            
      getXmlData(xml.getElementsByTagName(o), obj[o]);
    } else {
      obj[o] = getXmlNodeText(xml.getElementsByTagName(o).item(0));

    }

  }

}
function setDate(objId,dojoId){
	document.getElementById(objId).value=dojo.widget.byId(dojoId).inputNode.value;
}

/*金额标签转换函数*/
function money_convert(name){
	
	var formatName = name  + "_format";
	var strValue = document.getElementById(formatName).value;
    strValue=strValue.replace(",","");
    var regex = /^(0(\.\d{0,2})?|([1-9]+[0]*)+(\.\d{0,2})?)$/;
    if(!regex.test(strValue)){
    	//alert("金额格式不正确!");
    	
    	document.getElementById(name).value=0;
    	document.getElementById(formatName).value=0;
    }else{
      
	 strValue = Math.round(parseFloat(strValue)*1000+0.001);
      document.getElementById(name).value=strValue;
    }
}

/*金额标签转换函数
* 获得焦点时,去掉金额中的','符号
*/
function prepare(obj){
	var strValue = obj.value;
	strValue=strValue.replaceAll(",","");
	obj.value = strValue;
	obj.select();
}

String.prototype.replaceAll  = function(s1,s2){    
return this.replace(new RegExp(s1,"gm"),s2);    
}   

/*日期标签转换函数*/
function setDate(name){
    var func=function(){
		var formatName = "format_" + name;
		var tempDate = document.getElementById(name).value;
		var validateDate = dojo.widget.byId(formatName).inputNode.value;
		var regex = /^[1-9]{1}\d{3}-\d{1,2}-\d{1,2}$/;
		if(regex.test(validateDate)){
			var dateString = validateDate.split("-");
			var year = dateString[0];
			var month = dateString[1];
			var day = dateString[2];
			if(testMonth(month)&&testDay(year,month,day)){
				document.getElementById(name).value=validateDate;
				return;
			}
		}
		
		if(validateDate==""){
			tempDate="";
		}
		dojo.widget.byId(formatName).inputNode.value=tempDate;
		document.getElementById(name).value=tempDate;
	}
	window.setTimeout(func,100);
}

function testMonth(month){
	if (month < 1 || month > 12)   
    {                
        alert("月份应该为1到12的整数");
        return false;   
    } 
    return true;
}
function testDay(year,month,day){
    if (day < 1 || day > 31)   
    {   
        alert("每个月的天数应该为1到31的整数");
        return false;   
    }        
    if ((month==4 || month==6 || month==9 || month==11) && day==31)   
    {   
        alert("该月不存在31号");  
        return false;   
    }        
    if (month==2)   
    {   
        var isleap=(year % 4==0 && (year % 100 !=0 || year % 400==0));   
        if (day>29)   
        {                  
            alert("2月最多有29天");  
            return false;   
        }   
        if ((day==29) && (!isleap))   
        {                  
            alert("闰年2月才有29天");  
            return false;   
        }   
    }   
    return true;  
}
//比较两个日期是否有效(第一个日期不能在大于第二个日期);第一个日期可以为空,当为空时不进行验证
function compareTwoDate(startDate,endDate,s){
var a=startDate;
var b=endDate;
if(startDate==""){
	return true;
}
if(((Number(a.substring(0,4))-Number(b.substring(0,4)))*356+
       (Number(a.substring(5,7))-Number(b.substring(5,7)))*31+
    (Number(a.substring(8,10))-Number(b.substring(8,10))))>0){
  alert(s);
  //startDate.focus();
  return false;
 }
 return true;
}
//比较两个日期时间是否有效(第一个日期时间不能在大于第二个日期时间);第一个日期时间可以为空,当为空时不进行验证
function compareTwoDateTime(startDate,endDate,startTime,endTime,s){
var a=startDate;
var b=endDate;
var at = startTime;
var bt = endTime;
if(startDate==""){
	return true;
}
if(((Number(a.substring(0,4))-Number(b.substring(0,4)))*356*24*3600+
       (Number(a.substring(5,7))-Number(b.substring(5,7)))*31*24*3600+
    (Number(a.substring(8,10))-Number(b.substring(8,10)))*24*3600+
    (Number(at.substring(0,2))-Number(bt.substring(0,2)))*3600+
    (Number(at.substring(3,5))-Number(bt.substring(3,5)))*60+
    (Number(at.substring(6))-Number(bt.substring(6)))>0)){
  alert(s);
  return false;
  //startDate.focus();
 }
 return true;
}
//验证时间是否合法.
function testTime(time){
	var regex = /^[0-2]{1}[0-9]{1}:[0-5]{1}[0-9]{1}:[0-5]{1}[0-9]{1}$/;
	if(!regex.test(time)){
		alert("您输入的时间格式不正确!");
		return false;
	}
	var hour = time.substring(0,2); 
	var minute = time.substring(3,5); 
	var second = time.substring(6);
	if(hour>23 || hour <0){
		alert("小时的值应该在0-23之间!");
		return false;
	}
	if(minute > 60 ||minute < 0){
		alert("分钟的值应该在0-59之间!");
		return false;
	}
	if(second > 60 ||second < 0){
		alert("秒钟的值应该在0-59之间!");
		return false;
	}
	return true;
}
	String.prototype.trim=function(){
	        return this.replace(/(^\s*)|(\s*$)/g, "");
	}

		
	function isUrlValidate(url){
		var regx = /^(\s)*(http(s)?:\/\/)?([\w-]+\.)+[\w-]+(:(\d{1,4}))?(\/[\w-.\/?%&=]*)?(\s)*$/;
		return regx.test(url);
	}
	
	function isDateValidate(strDate){
		var regx = /^([1-2]\d{3})[\/|\-](0?[1-9]|10|11|12)[\/|\-]([1-2]?[0-9]|0[1-9]|30|31)$/;
		return regx.test(strDate);
	}
	
	function isEmailValidate(email){
		//var regx = /^(\s)*([\w]+([-_.][\w]+)*@[\w]+([.][\w]+)*\.[\w]+([.][\w]+)*)(\s)*$/;
		//var regx = /^([a-z0-9a-z]+[-|\.]?)+[a-z0-9a-z]@([a-z0-9a-z]+(-[a-z0-9a-z]+)?\.)+[a-za-z]{2,}$/;
		var regx = /^([a-z0-9A-Z]+(-|_)*[\.]?)+[a-z0-9A-Z]@([a-z0-9A-Z]+(-|_)*([a-z0-9A-Z]+)?\.)+[a-zA-Z]{2,}$/;
		return regx.test(email);
	}
	
	function isPhoneValidate(tel){
		var regx = /^(\s)*((1\d{10})|((0\d{2,3}\-){1}[1-9]{1}\d{6,7}(\-\d{1,4})?))(\s)*$/;
	
		return regx.test(tel);
	}
	
	function isMobileValidate(tel){
		var regx = /^(\s)*(1\d{10})(\s)*$/;
	
		return regx.test(tel);
	}

function securityLevel(objValue){
	var divObj = document.getElementById("securityDiv");
	divObj.innerHTML=="";
	var SecrityGif = new Array;
	SecrityGif[5] = "/seashell/website/img/common/passcolor/iWeak.gif";
	SecrityGif[4] = "/seashell/website/img/common/passcolor/iWeak.gif";
	SecrityGif[0] = "/seashell/website/img/common/passcolor/iWeak.gif";
	SecrityGif[1] = "/seashell/website/img/common/passcolor/iMedium.gif";
	SecrityGif[2] = "/seashell/website/img/common/passcolor/iStrong.gif";
	
	var SecrityAlt=new Array;
	SecrityAlt[5] = "密码强度弱";
	SecrityAlt[4] = "密码输入非法";
	SecrityAlt[0] = "密码强度弱";
	SecrityAlt[1] = "密码强度中";
	SecrityAlt[2] = "密码强度高";
	
	var SecrityMessage=new Array;
	SecrityMessage[5]="密码至少要输入6位。";
	SecrityMessage[4]="密码只能由字母，下划线和数字组成。";
	SecrityMessage[0]="密码安全性较弱，建议使用大小写字母与数字混合设置。";
	SecrityMessage[1]="密码安全性为中，建议使用大小写字母与数字混合设置。";
	SecrityMessage[2]="密码安全性为高，请牢记密码。";
	
	var result = '<ul><li class="userInfoL"></li><li class="userInfoThreeM"><img src="{0}" alt="{1}"/></li><li class="userInfoThreeR"><span class="text12 textRed">{2}</span></li></ul>';
	var r1 = /^([a-zA-Z0-9_])*$/;
	var r2 = /^([a-zA-Z])*$/; 
	var r3 = /^([0-9])*$/;
	var r4 = /^([_])*$/;
	if(r1.test(objValue)== false){
	 	result = result.replace('{0}',SecrityGif[4]);
		result = result.replace('{1}',SecrityAlt[4]);
		result = result.replace('{2}',SecrityMessage[4]);
		divObj.innerHTML=result;
		return ;
	}
	
	if(objValue.length < 6 || objValue.length > 32){
		result = result.replace('{0}',SecrityGif[5]);
		result = result.replace('{1}',SecrityAlt[5]);
		result = result.replace('{2}',SecrityMessage[5]);
		divObj.innerHTML=result;
		return ;
	}
		
	/*不允许使用重复字符*/
	if(this.isRepetitive(objValue)){
		result = result.replace('{0}',SecrityGif[0]);
		result = result.replace('{1}',SecrityAlt[0]);
		result = result.replace('{2}',SecrityMessage[0]);
		divObj.innerHTML=result;
		return ;
		
	} 	
	/*不允许使用顺序序列*/
	if(this.isOrder(objValue)||this.isReverseOrder(objValue)){
		result = result.replace('{0}',SecrityGif[0]);
		result = result.replace('{1}',SecrityAlt[0]);
		result = result.replace('{2}',SecrityMessage[0]);
		divObj.innerHTML=result;
		return ;
	}	
	
	if(r2.test(objValue)||r3.test(objValue)||r4.test(objValue)){
		result = result.replace("{0}",SecrityGif[1]);
		result = result.replace("{1}",SecrityAlt[1]);
		result = result.replace("{2}",SecrityMessage[1]);
		divObj.innerHTML=result;
		return ;
	}
	
	result = result.replace("{0}",SecrityGif[2]);
	result = result.replace("{1}",SecrityAlt[2]);
	result = result.replace("{2}",SecrityMessage[2]);
	divObj.innerHTML=result;
	return ;
}


function isRepetitive(objValue){
	var temp = objValue.charAt(0);
	for(var i = 0; i < objValue.length;i++){
		if(temp != objValue.charAt(i)){
			return false;
		} 
	} 
	return true;
}
function isOrder(objValue)
 { 
 				
	for(var i = 1; i < objValue.length;i++){
	if(objValue.charCodeAt(i-1) + 1 != objValue.charCodeAt(i)){
			return false;
		} 
	} 
	return true;
}
function isReverseOrder(objValue){
	for(var i = 1; i < objValue.length;i++){
	if(objValue.charCodeAt(i-1) - 1 != objValue.charCodeAt(i)){
			return false;
		} 
	} 
	return true;
}
function respond(obj){
	if(window.event.keyCode==13){
		obj.onblur();
	}	
}
function refreshValidateCode(_id,url){
	document.getElementById(_id).src = url+"?date="+new Date();
}

function parseUrl(url){
	
	var uniqueKey = new Date();
	if(url != null && url.indexOf("?") > 0){
		url += "&uniqueKey=" + uniqueKey;
	}
	else{
		url += "?uniqueKey=" + uniqueKey;
	}
	return url;
}

String.prototype.trim=function(){
        return this.replace(/(^\s*)|(\s*$)/g, "");
}

	function toHttpsURL(urlHead,url){
				
		var _temp= new String(window.location);
		_temp = _temp.substr(0,_temp.lastIndexOf("/"));
		var _url = new String(url);
		if(_url.indexOf("/")!=0){
			_url =  "/"+ _url;
		}
		_temp += _url;
		_temp = _temp.replace(urlHead,"https");
		return _temp;
		
	}

	function disableBtn(){
		var oBtns = document.getElementsByName("BTN_QRY");
	
		if(oBtns != null && oBtns.length > 0){
			for(var i=0; i<oBtns.length; i++){
				oBtns[i].disabled = true;
			}
		}
	}
	
	function enableBtn(){

		var oBtns = document.getElementsByName("BTN_QRY");

		if(oBtns != null && oBtns.length > 0){
			for(var i=0; i<oBtns.length; i++){
				oBtns[i].disabled = false;
			}
		}
	}
