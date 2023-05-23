//window.onload = function() {
//    if (!-[1,]) { // only IE & ver lt 10
//        document.write('<p style="text-align: center">抱歉，我们并不欢迎IE用户！</p>');
//    }
//};

// =============================================
// ------------------- 弹出框 -------------------
// =============================================
/**
 * 标签居中，给jquery增加新工具方法
 * 使用方式$('#div').center(true)
 *
 * isParent 默认为false,true相对父级别窗体大小
 */
$.fn.center = function(isParent) {
    var parent = window;
    if (isParent) {
        parent = this.parent();
    }
    this.css({
        "position": "absolute",
        "top": ((($(parent).height() - this.outerHeight()) / 2) + $(parent).scrollTop() + "px"),
        "left": ((($(parent).width() - this.outerWidth()) / 2) + $(parent).scrollLeft() + "px")
    });
    return this;
};
$.fn.outerHTML = function(s) {
    return s
        ? this.before(s).remove()
        : $("<p>").append(this.eq(0).clone()).html();
};

/**
 * 弹出框，基本文字版
 *
 * div#dialog_simple
 *
 * 调用方法：
 * dialog_simple()
 * dialog_simple({width:200})
 * dialog_simple({html:'添加成功', top: '20px', hide: true, time: 2000})
 * dialog_simple({html:'修改成功', time: 2000})
 *
 * 参数传入map {}
 * content 提示内容，支持html标签
 * top     弹出层居上高度，默认136px
 * hide    是否自动隐藏，默认true,隐藏
 * status  状态,默认成功：success，颜色为绿色，否则为红色
 * time    隐藏延迟时间，默认5秒=5000毫秒
 */
var dialog_simple = function(opts) {
    opts = $.extend({
        content: "恭喜，操作成功！",
        top: '103px',
        hide: true,
        status: 'success', // error
        time: 5000,
        isParent: true
    },opts||{});
    var className = 'msg';
    if (opts.status == 'error') {
        className = 'errmsg';
    }
    var parent = window;
    if (opts.isParent) {
        parent = window.parent;
    }
    parent.$("#dialog_simple").remove();
    var s ='';
    s +='<div id="dialog_simple" style="position: absolute; width: 100%; padding-top: 2px; height: 24px; top: '+opts.top+'; text-align: center;">';
    s +='<span class="'+className+'">'+opts.content+'</span>';
    s +='</div>';
    parent.$("body").prepend(s);
    if (opts.hide) {
        parent.$("#dialog_simple").delay(opts.time).fadeOut();
    }
};

/**
 * 简化提示，操作成功
 * c 内容
 * p 窗体默认true
 */
var dialog_simple_ok = function(c, p, top) {
    if (typeof(c) == "undefined") {
        c = "恭喜，操作成功！";
    }
    var isParent = true;
    if (typeof(p) != "undefined") {
        isParent = p;
    }
    var t = '103px';
    if (typeof(top) != "undefined") {
        t = top
    }
    if (isParent == false && t == '103px') {
        t = '0px';
    }
    dialog_simple({status:'success', content:c, isParent: p, top:t});
}

/**
 * 简化提示，操作失败
 *
 * c 内容
 * p 窗体默认true
 */
var dialog_simple_fail = function(c, p, top) {
    if (typeof(c) == "undefined") {
        c = "抱歉，操作失败！";
    }
    var isParent = true;
    if (typeof(p) != "undefined") {
        isParent = p;
    }
    var t = '103px';
    if (typeof(top) != "undefined") {
        t = top
    }
    if (isParent == false && t == '103px') {
        t = '0px';
    }
    dialog_simple({status:'error', content:c, isParent: p, top:t});
}

/**
 * 弹出框，动态页面
 *
 * 调用方式：
 * dialog_iframe({url:$(this).attr("href"), title:"导航设置", isParent:false});
 * dialog_iframe({url:'http://127.0.0.1:8080/security', title:"安全设置", width:700, height:600});
 *
 * 参数传入：map {}
 * url      请求url，将返回页面至对话框内
 * title    弹出框标题，页面名称
 * width    弹出框宽度
 * height   弹出框高度
 * isParent 遮盖父窗体，默认遮盖父窗体。
 *
 */
var dialog_iframe = function(opts) {
    opts = $.extend({
        url: "/404.jsp",
        title:"标题",
        width: 650,
        height: 500,
        isParent: true,
        closeOnEscape: true,
        resizable: true,
        draggable: true,
        scroll: false     // 窗体内容翻屏
    },opts||{});
    var parent = window;
    if (opts.isParent) {
        parent = window.parent;
    }
    var openfun = function(event, ui) { };
    if (!opts.closeOnEscape) {
        openfun = function(event, ui) { parent.$(".ui-dialog-titlebar-close").hide(); }
    }
    parent.$("#dialog iframe").attr('src',""); // fix:大于第一次请求后iframe存在之前地址，获取$('dialog')对象会自动加载一次src地址
    parent.$("#dialog").remove();
    // parent.$("body").prepend('<div id="dialog"></div>');
    var scollAttr = '';
    if (!opts.scroll) {
        scollAttr = ' frameborder="0" scrolling="no" ';
    }
    parent.$('<div id="dialog"></div>').html('<iframe '+scollAttr+'style="border: 0px; " src="' + opts.url + '" width="100%" height="100%"></iframe>')
        .dialog({
            modal : true,
            height : opts.height,
            closeText : '关闭',
            closeOnEscape: opts.closeOnEscape,
            open: openfun,
            resizable: opts.resizable,
            draggable: opts.draggable,
            width : opts.width,
            title : opts.title
        });
}

/**
 * 弹出框，动态html
 *
 * 调用方式：
 * dialog_html({title:"导航设置", html:"<div>test</div>", isParent:false});
 * dialog_html({title:"安全设置", html:"<div><p>test</p></div>", width:700, height:600});
 *
 * 参数传入：map {}
 * title    弹出框标题
 * html     弹出框内容
 * width    弹出框宽度
 * height   弹出框高度
 * isParent 遮盖父窗体，默认遮盖父窗体。
 *
 */
var dialog_html = function(opts) {
    opts = $.extend({
        title:"标题",
        html: "",
        width: 500,
        height: 400,
        isParent: true
    },opts||{});

    var parent = window;
    if (opts.isParent) {
        parent = window.parent;
    }
    parent.$("#dialog").remove();
    parent.$("body").prepend('<div id="dialog"></div>');
    parent.$("#dialog").html(opts.html).dialog({
        modal : true,
        height : opts.height,
        width : opts.width,
        title : opts.title
    });
}

/**
 * 关闭iframe页面弹出框
 * 调用方式：
 * dialog_close({callBack:function(){
 *    // operating
 * }, isParent:true})
 *
 * 传入参数：map{}
 * callBack  回调方法
 * isParent  参考dialog_iframe.isParent
 */
var dialog_close = function(opts) {
    opts = $.extend({
        callBack: function(){},
        isParent: true
    },opts||{});

    var parent = window;
    if (opts.isParent) {
        parent = window.parent;
    }
    parent.$('#dialog').dialog('close');
    opts.callBack();
}

/**
 * 彈出框，確認對話框
 *
 * 調用方式：
 * dialog_confirm({yes:function(){alert(1)}});
 *
 * 傳入參數：map{}
 * title    对话框标题
 * content  对话框内容
 * width    弹出框宽度
 * height   弹出框高度
 * yes      确认 回调方法
 * no       取消 回调方法
 * isParent 遮盖父窗体，默认不遮盖父窗体。
 *
 */
var dialog_confirm = function(opts) {
    opts = $.extend({
        title:"操作确认",
        content: "真的要这么做？",
        width: 300,
        height: 140,
        yes: function(){},
        no: function(){},
        isParent: false
    },opts||{});

    var parent = window;
    if (opts.isParent) {
        parent = this.parent;
    }
    parent.$("#dialog_confirm").remove();
    parent.$("body").prepend('<div id="dialog_confirm"></div>');
    parent.$("#dialog_confirm").html(opts.content).dialog({
        title : opts.title,
        height: opts.height,
        width: opts.width,
        modal: true,
        buttons: {
            "确认": function() {
                parent.$( this ).dialog( "close" );
                opts.yes();
            },
            "取消": function() {
                parent.$( this ).dialog( "close" );
                opts.no();
            }
        }
    });
};

/**
 * 弹出对话框，用于消息提示
 *
 * 调用方式：
 * dialog_prompt({title:'测试标题',content:'测试内容'})
 * dialog_prompt({title:'测试',yes:function(){alert(1)}});
 *
 * 传入参数：map{}
 * title    消息标题
 * content  消息内容
 * width    对话框宽度
 * height   对话框高度
 * yes      确认 回调方法
 * isParent 遮盖父窗体，默认不遮盖父窗体。
 *
 */
var dialog_prompt = function(opts) {
    opts = $.extend({
        title:"消息",
        content:"内容",
        width: 320,
        height: 140,
        yes: function(){},
        isParent: false
    },opts||{});

    var parent = window;
    if (opts.isParent) {
        parent = this.parent;
    }
    parent.$("#dialog_prompt").remove();
    parent.$("body").prepend('<div id="dialog_prompt"></div>');
    parent.$("#dialog_prompt").html(opts.content).dialog({
        title : opts.title,
        height: opts.height,
        width: opts.width,
        modal: true,
        buttons: {
            "确定": function() {
                parent.$( this ).dialog( "close" );
                opts.yes();
            }
        }
    });
};

/**
 * 封装消息对话框,使其调用更简单
 *
 * 调用方式：
 * dialog_msg("没有选择商品！", 'warning');
 * dialog_msg("事情就这样简单");
 *
 * 传入参数：
 * content  提示框内容
 * type     提示类型，支持 成功：succeed，警告：warning，提示：prompt，错误：error和默认消息：msg
 * title    提示框标题
 *
 */
var dialog_msg = function(content, type, title) {
    type = type || 'msg';
    switch (type) {
        case 'succeed':
            title = title || "成功";
            break;
        case 'warning':
            title = title || "警示";
            break;
        case 'prompt':
            title = title || "提示";
            break;
        case 'error':
            title = title || "错误";
            break;
        default:
            title = title || "消息";
            break;
    }
    content = '<div class="' + type + '">' + content + '</div>';

    dialog_prompt({title:title, content:content, width:320, height:140});
};

/**
 * 用于tab高亮
 * @param pid 父tab id
 * @param sid 子tab id
 */
function bindTab(pid, sid){
    // tab高亮
    $("#tabs").children().removeClass("current");
    $("#"+pid).addClass("current");
    $("#tabs > li").children("div").addClass("hide");
    $("#"+pid+"div").removeClass("hide");
    $("#"+pid+"div").children().removeClass("current");
    $("#"+sid).addClass("current");
}

//全局的ajax访问，处理ajax清求时sesion超时
$.ajaxSetup({
    cache:false,
    contentType:"application/x-www-form-urlencoded;charset=utf-8",
    complete:function(XMLHttpRequest){
        var sessionstatus=XMLHttpRequest.getResponseHeader("sessionstatus"); //通过XMLHttpRequest取得响应头，sessionstatus
        if(sessionstatus=="timeout"){
            //如果超时就处理 ，指定要跳转的页面
            window.location.replace("/login");
        }
    }
});
