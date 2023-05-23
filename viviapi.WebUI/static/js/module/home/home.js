$(document).ready(function () {
	/*banner carousel*/
	var btn = $("#slider-btn li");
	var sliderImg = $("#slider-back p");
	var $bannerTxt = $(".banner-txt");
	var $sliderTxt = $(".slider-txt");
	var $sliderLinkBtn = $(".banner-txt a");
	var iNow = 0;
	btn.each(function (index) {
		$(this).mouseover(function () {
			slide(index);
		});


		$(this).data("index");
	});

	function slide(index) {
		iNow = index;
		btn.eq(index).addClass("slider-active").siblings().removeClass();
		var bannerTxtActive = $bannerTxt.eq(index);
		var slideElements = bannerTxtActive.children();
		bannerTxtActive.siblings(".banner-txt").stop(true).fadeOut(100);
		//初始化
		bannerTxtActive.show();
		slideElements.each(function(){
			var $_self = $(this);
			$_self.css({
				opacity: 0,
				top: $_self.data("start_top")||0,
				left: $_self.data("start_left")||0
			});
			$_self.stop(true).delay(400).animate({
				opacity: 1,
				top: $_self.data("to_top"),
				left: $_self.data("to_left")
			}, 1200);
			if($_self.data("class")!==undefined){
				$_self.removeClass($_self.data("class"));
				setTimeout(function(){
					$_self.addClass($_self.data("class"));
				},0);
			}
		});

		sliderImg.eq(index).siblings().stop().animate({opacity: 0}, 600);
		sliderImg.eq(index).stop().animate({opacity: 1}, 600);

	}

	function autoRun() {
		iNow++;
		if (iNow == btn.length) {
			iNow = 0;
		}
		slide(iNow);
	}

	var timer = setInterval(autoRun, 6000);
	btn.hover(function () {
			clearInterval(timer);
		}, function () {
			timer = setInterval(autoRun, 6000);
		}
	);
	//banner初始化
	slide(0);
	//新闻切换
	var newsParent = $(".news-list");
	var news = newsParent.children("p");
	//news--current
	news.first().css({
		opacity: 1,
		top: 0
	}).siblings().css({
		opacity: 0,
		top: -37
	});

	var newsDown = function () {
		var newsCurrent = newsParent.children(".news--current");
		var nextItem = newsCurrent.next();
		if (nextItem.length == 0) {
			nextItem = newsParent.children().first();
		}

		nextItem.css({
			opacity: 0,
			top: -37
		});


		nextItem.animate({opacity: 1, top: 0}, 300, function () {
			nextItem.addClass("news--current");
		});
		newsCurrent.animate({opacity: 0, top: 25}, 300, function () {
			newsCurrent.removeClass("news--current");
		});
	};
	var newsUp = function () {
		var newsCurrent = newsParent.children(".news--current");
		var nextItem = newsCurrent.prev();
		if (nextItem.length == 0) {
			nextItem = newsParent.children().last();
		}

		nextItem.css({
			opacity: 0,
			top: 25
		});

		nextItem.animate({opacity: 1, top: 0}, 300, function () {
			nextItem.addClass("news--current");
		});
		newsCurrent.animate({opacity: 0, top: -37}, 300, function () {
			newsCurrent.removeClass("news--current");
		});
	};

	var newsIntervalId = null;
	function newsAuto() {
		clearInterval(newsIntervalId);
		newsIntervalId = setInterval(newsDown, 8000);
	}
	newsAuto();
	$(".news-left").hover(function(){
		clearInterval(newsIntervalId);
	},function() {
		newsAuto();
	});

	$("#prev-news").click(function () {
		newsUp();
	}).hover(function(){
		clearInterval(newsIntervalId);
	},function() {
		newsAuto();
	});

	$("#next-news").click(function () {
		newsDown();
	}).hover(function(){
			clearInterval(newsIntervalId);
	},function() {
			newsAuto();
	});


	//解决方案cover进入动画
	$(".solution-block").hover(function () {
		$(this).children(".covers").stop(true, true).delay(300).animate({"z-index": 10}, 10).animate({
			"top": 0,
			opacity: 1
		}, 300);
	}, function () {
		$(this).children(".covers").stop(true, true).animate({
			"top": 279,
		}, 400).animate({"z-index": -5}, 10);
	});

	//页面滚动到视频链接位置时 banner出现动画
	$(function () {
		$(window).scroll(function () {
			var videoBanner = $(".video-link");
			var videoBannerShowPosition = videoBanner.prev().offset().top - window.innerHeight + 200;
			if ($(window).scrollTop() > videoBannerShowPosition) {
				videoBanner.slideDown(300);
				$(".video-text").animate({
					opacity: 1,
					top: 20
				},1000);
				$(".v-btn").animate({
					opacity:1,
					top:100
				},1200);
			}
		});

	});
});