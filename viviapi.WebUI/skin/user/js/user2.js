$(function() {


    $("button.ajaxSubmit,input.ajaxSubmit").live("click", function() {

        var button = $(this);
        var form = $(this).parents("form");

        if (!button.attr("disabled")) {
            if (form.length > 0) {
                var confirm = form.attr("confirm");
                if (confirm) {
                    $.dialog({
                        title: "确认？",
                        content: confirm,
                        icon: 'confirm',
                        ok: function() {
                            if (form.attr("validate") && form.attr("validate").length > 0) {
                                user.validate({ items: form.attr("validate"), button: button });
                            } else {
                                form.ajaxform({
                                    "button": button
                                });
                            }
                        },
                        cancel: true
                    });
                } else {
                    if (form.attr("validate") && form.attr("validate").length > 0) {
                        user.validate({ items: form.attr("validate"), button: button });
                    } else {
                        form.ajaxform({
                            "button": button
                        });
                    }
                }


            }
        }
    });




});







var user = {

    validate: function(opts) {

        var form = opts.form;
        var button = opts.button;
        var url = opts.url;
        var data = opts.data;
        var items = opts.items;
        var callback = opts.callback;
        var okVal = opts.okVal;

        okVal = okVal ? okVal : '验证并提交';

        if (items instanceof Array) {
            items = items.join(",");
        } else if (!items) {
            items = '';
        }

        return $.dialog.open('/user/validate/frame.aspx?items=' + items, {
            title: '操作保护',
            width: 400,
            height: 215,
            okVal: okVal,
            ok: function() {



                var iframe = this.iframe.contentWindow;
                var verifytype = $(iframe.document.body).find("input[name='verifytype']").val();
                var verifycode = '';


                switch (verifytype) {

                    case 'email':
                        verifycode = $(iframe.document.body).find("input[name='email_code']").val();
                        break;

                    case 'mobile':
                        verifycode = $(iframe.document.body).find("input[name='sms_code']").val();
                        break;

                    case 'protection':
                        verifycode = $(iframe.document.body).find("input[name='answer_code']").val();
                        break;

                    default:

                        break;

                }


                if (button) {
                    form = button.parents("form");
                }



                if (url) {

                    data = $.extend({}, data, {
                        verifytype: verifytype,
                        verifycode: verifycode
                    });

                    core.ajaxload({
                        type: 'POST',
                        url: url,
                        data: data,
                        success: callback
                    });

                } else {


                    if (form && form.length > 0) {


                        var newverifytype = form.find("input[name='verifytype']");
                        if (newverifytype.length == 0) {
                            form.append('<input type="hidden" name="verifytype" value="' + verifytype + '" />');
                        } else {
                            newverifytype.val(verifytype);
                        }
                        var newverifycode = form.find("input[name='verifycode']");
                        if (newverifycode.length == 0) {
                            form.append('<input type="hidden" name="verifycode" value="' + verifycode + '" />');
                        } else {
                            newverifycode.val(verifycode);
                        }


                        //form.submit();
                        //return false;

                        form.ajaxform({
                            button: button,
                            success: callback
                        });
                    }

                }

                return false;
            },
            cancel: true
        });
    }












};