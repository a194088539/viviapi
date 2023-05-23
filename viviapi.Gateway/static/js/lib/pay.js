(function($){
    var PopUpWin = function(ele,opts){
        opts = $.extend({
            id:'',
            content:undefined,//内容
            closeCallback:undefined//关闭时调用的方法
        },opts);
        this.init(ele,opts);
    }

    PopUpWin.prototype = {
        template:'<div class="pop-wraper" id="{id}">\
                <div class="pop-outer">\
                    <div class="pop-inner">\
                        <div class="pop-content">\
                            {content}\
                        </div>\
                        <div class="btn btn_cancel"><i class="ico_cancel"></i></div>\
                    </div>\
                </div>\
            </div>',
        init:function(ele,opts){
            this.render(ele,opts);
            this.initEvent(ele,opts);
        },
        initEvent:function(ele,opts){
            var self = this;
            ele.find('.btn_cancel').click(function(){
                ele.find('#'+self.id).remove();
                if(opts.closeCallback !== undefined && $.isFunction(opts.closeCallback)){
                    opts.closeCallback();
                }
            });
        },
        elId:function(){//自动生成7位8进制DOM元素ID
            return 'win-xxx'.replace(/[x]/g,function(c){
                var r = Math.random() * 16|0, v = c === 'x' ? r : (r&0x3|0x8);
                return v.toString(8);
            }).toLocaleLowerCase();
        },
        render:function(ele,opts){
            if(ele === undefined){
                ele = $('body');
            }
            
            var content = opts.content;
            this.id = this.elId();
            
            if($.isFunction(content)){
                content  = content(this);
            }
                tpl = this.template.replace(/\{id\}/,this.id).replace(/\{content\}/,content);
            ele.append(tpl);
        }
    };

    $.fn.popUpWin = function(opts){
        return this.each(function(){
             var that = $(this);
             var popUp = new PopUpWin(that,opts);
        });
    };

})(jQuery);

//二维码处理
(function(win,$){
    win.QRLogin = {};
    win.code = 408;
    

    var QRCode = function(opts){
        opts = $.extend({
            qrCodeData:{
                _:(''+Math.random() * 10).substr(2)
            },
            qrUrl:'',//请求UUID的URL地址
            qrImgUrl:'',//请求验证码的URL地址
            statusUrl:'',//请求状态验证的URL地址
            uuid:'',
            msg:'',
            tilMsg:{
                'qr_default':'请使用微信扫描<br/>二维码以完成支付',
                'qr_succ':'扫描成功<br/>请在手机确认支付',
                'pay_error':'无法支付<br/>商品金额大于银行卡快捷支付限额',
                'pay_succ':'购买成功'
            },
            qrCodeClose:false
        },opts);
        this.init(opts);
    };

    QRCode.prototype = {
        qrTimeout:null,
        init:function(opts){
            var param = this.urlParam(opts.qrUrl), self = this;
            this.opts = opts;
            this.appid = param['appid'];
            this.req_key = param['req_key'];
            this.changeQrcode();
        },
        urlParam:function(query){
            var result = {};
            query.replace(/(\w+)=(\w+)/ig,function(a,b,c){
                if(b !== undefined)
                    result[b] = c;
            });
            return result;
        },
        changeQrcode:function(){
            var self = this;
            $.ajax({
                url:self.opts.qrUrl,
                type:'GET',
                dataType:'script',
                data:{
                    _:self.random()
                },
                cache:false,
                success:function(){
                    var _code = win.QRLogin.code;
                    if(_code == 200 && win.QRLogin.uuid && self.opts.qrCodeClose === false){
                        self.opts.uuid = win.QRLogin.uuid;
                        var src = self.opts.qrImgUrl+'&uuid='+self.opts.uuid+'&_='+self.random();
                        if(self.popWin === undefined){
                            self.popWin = $('body').popUpWin({
                                content:function(){
                                    return '<img src="'+src+'" /><div class="msg_default_box"><i class="icon60_qr pngFix"></i><p>请使用微信扫描<br>二维码以完成支付</p></div>';
                                },
                                closeCallback:function(){
                                    self.popWin = undefined;
                                    self.opts.qrCodeClose = true;
                                }
                            });
                        }else{
                            $('div.pop-wraper img').attr('src',src);
                        }
                        self._poll();
                    }else{
                        self.restart();
                    }
                },
                error:self.restart
            });
        },
        restart:function(){
            var self = this;
            clearTimeout(this.qrTimeout);
            this.qrTimeout = setTimeout(function(){
                $.proxy(self.changeQrcode, self);
            },10 * 1000);
        },
        _poll:function(){
            var self = this, pollUUID = self.opts.uuid;
            window.debug = self.succCallback;
            $.ajax({
                url:self.opts.statusUrl,
                type:'GET',
                dataType:'script',
                data:{
                    uuid:pollUUID,
                    tip: 1,
                    _:self.random(),
                    code:win.code
                },
                cache:false,
                timeout:33 * 1000,//后台是30s
                success:function () {
                    if(!win.code){
                        self.repoll.call(self,[pollUUID]);
                        return;
                    }
                    self.succCallback(win.code,pollUUID);
                },
                error:function(){
                    self.repoll.call(self,[pollUUID]);
                }
            });
        },
        succCallback:function(code,pollUUID){
            var self = this;
            switch(code){
                    case 408://扫描未知
                    case 200:
                        self.repoll(pollUUID);
                        break;
                    case 203://扫描成功
                        //clearInterval(self.qrInterval);
                        self.changePayInfo('qr_succ');
                        self.repoll(pollUUID);
                        break;
                    case 205://扫描成功——取消
                        self.changePayInfo('qr_default');
                        self.repoll(pollUUID);
                        break;
                    case 204://支付未知
                        self.repoll(pollUUID);
                        break;
                    case 201://支付成功
                        self.changePayInfo('pay_succ');
                        //这里可以写成功的业务
                        break;
                    case 202://支付失败
                        self.changePayInfo('pay_error');
                        break;
                    case 400://uuid失效
                        self.changeQrcode();
                        break;
                    default:
                        self.repoll(pollUUID);
            }
        },
        repoll:function(pollUUID){
            var self = this;
            if(pollUUID !== self.opts.uuid){
                return;
            }
            if(self.opts.qrCodeClose === true){
                return;
            }
            setTimeout(function(){
                self._poll.call(self);
            }, 1000);
        },
        changePayInfo:function(clazz){
            var codeMsgWrapper = $('div.pop-wraper .msg_default_box');
            if(codeMsgWrapper.size() > 0 && clazz !== undefined){
                codeMsgWrapper.attr('class','msg_default_box '+clazz);
                codeMsgWrapper.find('p').html(this.opts.tilMsg[clazz]);
            }
        },
        random:function(){
            return (''+Math.random() * 10).substr(2);
        }
    };

    win.QRCode = QRCode || {};
    $.fn.load = function( url, params, callback ) {
        if ( typeof url !== "string" && _load ) {
            return _load.apply( this, arguments );
        }

        var selector, type, response,
            self = this,
            off = url.indexOf(" ");

        if ( off >= 0 ) {
            selector = jQuery.trim( url.slice( off ) );
            url = url.slice( 0, off );
        }

        if ( jQuery.isFunction( params ) ) {
            callback = params;
            params = undefined;

        } else if ( params && typeof params === "object" ) {
            type = "POST";
        }

        if ( self.length > 0 ) {
            jQuery.ajax({
                url: url,
                type: type,
                dataType: "html",
                async:true,
                data: params
            }).done(function( responseText ) {
                response = arguments;
                self.html( selector ?
                    jQuery("<div>").append( jQuery.parseHTML( responseText ) ).find( selector ) :
                    responseText );

            }).complete( callback && function( jqXHR, status ) {
                self.each( callback, response || [ jqXHR.responseText, status, jqXHR ] );
            });
        }

        return this;
    };
})(window,jQuery);

(function(win,$,h){
    $(document).ready(function(){
        var routeUrl = {
            'orderInfo':'order.html',//订单提交页面
            'orderInfo_method':'submitOrderInfo',//订单提交action方法
            'queryOrder':'queryOrder.html',
            'queryOrder_method':'queryOrder',
            'refundTest':'refundTest.html',
            'refundTest_method':'submitRefund',
            'queryRefund':'queryRefund.html',
            'queryRefund_method':'queryRefund'
        }, validateField = {//需要验证的字段
            'orderInfo':['out_trade_no','body','total_fee','mch_create_ip'],//字段名
            'orderInfo_msg':['商户订单号','商品描述','总金额','终端IP'],//字段对应的中文名
            'refundTest':['out_refund_no','total_fee','refund_fee'],
            'refundTest_msg':['商户退款单号','总金额','退款金额']
        }, loadHtml = function (url, suffix) {
            $('#auto_center').empty().load(url+' #'+suffix,function(){
                if(suffix === 'orderInfo'){
                    $('input[name=out_trade_no]').val((''+Math.random() * 10).substr(2));
                }else if(suffix === 'refundTest'){
                    $('input[name=out_refund_no]').val((''+Math.random() * 10).substr(2));
                }
            });
        }, curPage = 'orderInfo';
        //初始化加载的页面
        loadHtml(routeUrl['orderInfo'],'orderInfo');
        
        

        $('div.menu li').bind('click',function(e){
            var curTarget = $(e.currentTarget), href = curTarget.attr('href'),suffix = href.substring(href.lastIndexOf('\.'));
            curTarget.addClass('cur').siblings('.cur').removeClass('cur');
            loadHtml(routeUrl[href],suffix);
            curPage = suffix;
        });

        $('#pay_platform').delegate('span','click',function(e){
            if(e.target.className.indexOf('submit') === -1){
                return;
            }

            var input = $('div.form_wrap').find('input,select'), param = {method:'submitOrderInfo'}, vField = validateField[curPage];
            input.each(function(i,item){
                item = $(item);
                var vType = item.attr('vtype'), ind = 0;
                param[item.attr('name')] = item.val();
            });

            //判断不能为空的字段
            if(vField !== undefined){
                for(var i=0, field='', msg = ''; i<vField.length; i++){
                    field = vField[i];
                    msg = validateField[curPage+'_msg'][i];
                    if(param[field] === ''){
                        $('body').popUpWin({
                            content:msg+'不能为空！'
                        });
                        return;
                    }
                }
            }
            //设计提交方法
            param['method']=routeUrl[curPage+'_method'];

            var mask = $('<div class="mask"></div>');
                $('body').append(mask);
                $.post('/ashx/request.ashx', param, function (res) {
                $('body').find('.mask').remove();
                if(typeof(res) === 'string'){
                    res = JSON.parse(res);
                }

                if(res.status === 500){
                    _content = res.msg;
                    $('body').popUpWin({
                        content:res.msg
                    });
                }else{
                    if(curPage === 'orderInfo'){
                        console.log(res);
                        new QRCode({
                            qrUrl:res.code_url,
                            qrImgUrl:res.code_img_url,
                            statusUrl:res.code_status
                        });
                    }else{
                        $('body').popUpWin({
                            content:res.msg
                        });
                    }
                }

                
            });
        });
    });
})(window,jQuery);