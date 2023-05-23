
function toggle(targetid) {
    if (document.getElementById) {
        target = document.getElementById(targetid);
        if (target.style.display == "block") {
            target.style.display = "none";
        } else {
            target.style.display = "block";
        }
    }
}

function loadXMLDoc(dname) {
    var xmlDoc;
    if (window.ActiveXObject) {
        xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
    }
    else if (document.implementation && document.implementation.createDocument) {
        xmlDoc = document.implementation.createDocument("", "", null);
    }
    else {
        alert('Your browser cannot handle this script');
    }
    xmlDoc.async = false;
    xmlDoc.load(dname);
    return (xmlDoc);
}

function getObject(objectId) {
    if (document.getElementById && document.getElementById(objectId)) {
        return document.getElementById(objectId);
    }
    else if (document.all && document.all(objectId)) {
        return document.all(objectId);
    }
    else if (document.layers && document.layers[objectId]) {
        return document.layers[objectId];
    }
    else {
        return false;
    }
} 
function setCookie(name, value, expires, path, domain, secure) {
    document.cookie = name + "=" + escape(value) +
                ((expires) ? "; expires=" + expires : "") +
                ((path) ? "; path=" + path : "") +
                ((domain) ? "; domain=" + domain : "") +
                ((secure) ? "; secure" : "");
}

function getCookieVal(offset) {
    var endstr = document.cookie.indexOf(";", offset);
    if (endstr == -1) {
        endstr = document.cookie.length;
    }
    return unescape(document.cookie.substring(offset, endstr));
}

function getCookie(name) {
    var arg = name + "=";
    var alen = arg.length;
    var clen = document.cookie.length;
    var i = 0;
    while (i < clen) {
        var j = i + alen;
        if (document.cookie.substring(i, j) == arg) {
            return getCookieVal(j);
        }
        i = document.cookie.indexOf(" ", i) + 1;
        if (i == 0) break;
    }
    return "";
}

function DelCookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    document.cookie = name + "=" + cval + "; expires=" + exp.toGMTString();
}

function SplitFormData(formData, field) {
    var aa = formData.split("&");
    for (i = 0; i < aa.length; i++) {
        if (aa[i].indexOf(field + "=") >= 0) {
            return aa[i].substring((field + "=").length);
        }
    }
    return "";
}
function BindForm(formID, cookieName) {
    var form = document.getElementById(formID);
    var formData = getCookie(cookieName);
    for (var i = 0; i < form.length; i++) {
        if (form.elements[i].name != "") {
            var valueData = SplitFormData(formData, form.elements[i].name);
            form.elements[i].value = valueData;
        }
    }
}

//清空file类型元素内容
function clearFileInput(file) {
    var form = document.createElement('form');
    document.body.appendChild(form);
    //记住file在旧表单中的的位置
    var pos = file.nextSibling;
    form.appendChild(file);
    form.reset();
    pos.parentNode.insertBefore(file, pos);
    document.body.removeChild(form);
}

//日期控件
function rule(id) {
	if (id == datetime) {
		var v = $("#"+endtime).val();
		if (v == "") {
			return null;
		}
		else {
			var d = v.match(/^(\d{1,4})(-|\/|.)(\d{1,2})\2(\d{1,2})$/);
			if (d != null) {
				var nd = new Date(parseInt(d[1], 10), parseInt(d[3], 10) - 1, parseInt(d[4], 10));
				return { enddate: nd };
			}
			else {
				return null;
			}
		}
	}
	else {
		var v = $("#"+datetime).val();
		if (v == "") {
			return null;
		}
		else {
			var d = v.match(/^(\d{1,4})(-|\/|.)(\d{1,2})\2(\d{1,2})$/);
			if (d != null) {
				var nd = new Date(parseInt(d[1], 10), parseInt(d[3], 10) - 1, parseInt(d[4], 10));
				return { startdate: nd };
			}
			else {
				return null;
			}
		}

	}
}
//数组去重
function unique(data) {
    data = data || [];
    var a = {};
    for (var i = 0; i < data.length; i++) {
        var v = data[i];
        if (typeof (a[v]) == 'undefined') {
            a[v] = 1;
        }
    };
    data.length = 0;
    for (var i in a) {
        data[data.length] = i;
    }
    return data;
}
//全选
function checkAll(name) {
    var names = document.getElementsByName(name);
    var len = names.length;
    if (len > 0) {
        var i = 0;
        for (i = 0; i < len; i++)
            names[i].checked = true;
    }
}
//反选
function reserveCheck(name) {
    var names = document.getElementsByName(name);
    var len = names.length;
    if (len > 0) {
        var i = 0;
        for (i = 0; i < len; i++) {
            if (names[i].checked)
                names[i].checked = false;
            else
                names[i].checked = true;
        }
    }
}
// 复制
function CopyTxt(maintext) {
    if (window.clipboardData) {
        window.clipboardData.setData("Text", maintext);
    } else if (window.netscape) {
        try {
            netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
        } catch (e) {
            alert("该浏览器不支持一键复制！请手工复制文本框内容！");
        }

        var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
        if (!clip) return;
        var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
        if (!trans) return;
        trans.addDataFlavor('text/unicode');
        var str = new Object();
        var len = new Object();
        var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
        var copytext = maintext;
        str.data = copytext;
        trans.setTransferData("text/unicode", str, copytext.length * 2);
        var clipid = Components.interfaces.nsIClipboard;
        if (!clip) return false;
        clip.setData(trans, null, clipid.kGlobalClipboard);
    }
    alert("以下内容已经复制到剪贴板：\n\n" + maintext);
}
/*加入收藏*/
function AddFavorite(sURL, sTitle) {
    try {
        window.external.addFavorite(sURL, sTitle);
    }
    catch (e) {
        try {
            window.sidebar.addPanel(sTitle, sURL, "");
        }
        catch (e) {
            alert("加入收藏失败，请使用Ctrl+D进行添加");
        }
    }
}
/*设为首页*/
function SetHome(obj, vrl) {
    try {
        obj.style.behavior = 'url(#default#homepage)'; obj.setHomePage(vrl);
    }
    catch (e) {
        if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            }
            catch (e) {
                alert("此操作被浏览器拒绝！\n请在浏览器地址栏输入“about:config”并回车\n然后将 [signed.applets.codebase_principal_support]的值设置为'true',双击即可。");
            }
            var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
            prefs.setCharPref('browser.startup.homepage', vrl);
        }
    }
}
/*获取文件名*/
function getFileName() {
    var url = this.location.href
    var pos = url.lastIndexOf("/");
    if (pos == -1) {
        pos = url.lastIndexOf("\\")
    }
    var filename = url.substr(pos + 1);
    filename = filename.split("?")[0];
    return filename;
}