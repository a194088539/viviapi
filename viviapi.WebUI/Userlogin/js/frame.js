 window["headerHeight"] = '82';
        $(function(){
            $(".mainbar .module,.mainframe,.sidebar .mbody,.sideBar-trigger").css("height",$(window).height() - headerHeight);
            $(".sidebar .mbody").css("height",$(window).height() - headerHeight - 30);
            $(".sideNav-content").css("height",$(window).height() - headerHeight - 64);
            $(".sideBar-trigger span").css("margin-top",($(window).height() - headerHeight) / 2 - 15);
            $(".sideBar-trigger").click(function(){
                $("#frame-content").toggleClass('trigged','false');
            });
            $(".hoverToggle-wrapper").each(function(){
                $(this).hoverDelay({
                hoverEvent:function(){
                    $(this).children(".hoverToggle").show();
                },outEvent:function(){
                    $(this).children(".hoverToggle").hide();
                }
                });
            });
            $(".clickToggle-wrapper .trigger").click(function(){
                $(this).next(".clickToggle").toggle();
            });
            $(".close").click(function(){
                $(this).parents(".clickToggle").hide();
            });
            //引导遮罩层自适应窗口高度
            $("#guide").css('height',$(window).height());  
            $(".table-wrapper").delegate('tr','click',function(){$(this).addClass('hovered');});
            $(".cart-list tr:last").addClass('last-item');
        });
        $(window).resize(function(){
            $(".mainbar .module,.mainframe,.sidebar .mbody,.sideBar-trigger").css("height",$(window).height() - headerHeight);
            $(".sidebar .mbody").css("height",$(window).height() - headerHeight - 30);
            $(".sideNav-content").css("height",$(window).height() - headerHeight - 64);
            $(".sideBar-trigger span").css("margin-top",($(window).height() - headerHeight) / 2 - 15);
            //引导遮罩层自适应窗口高度
            $("#guide").css('height',$(window).height());
        });
        