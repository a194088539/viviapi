$(function() {



    var tree = $("#tree");

    var li = tree.children("li");

    var defaultIndex = $.cookie("niaoyun_user_tree");

    li.each(function(i) {
        if (defaultIndex == i) {
            li.eq(i).children("ul").show();
        } else {
            li.eq(i).children("ul").hide();
        }
    });




    li.children("span").click(function(i) {

        var parent = $(this).parent();
        var index = parent.index();

        if (parent.children("ul").is(":hidden")) {

            li.children("ul").hide(100);
            parent.children("ul").show(100);

            $.cookie("niaoyun_user_tree", index, { expires: 7, path: '/' });

        } else {

            li.children("ul").hide(100);
            $.cookie("niaoyun_user_tree", null, { expires: 7, path: '/' });
        }


    });


    var task = function(data) {
        // 浠诲姟

    };




});