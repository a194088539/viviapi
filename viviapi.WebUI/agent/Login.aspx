<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.agent.Login" Codebehind="login.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=gb2312" />
    <title>管理员登录</title>
    <link rel="stylesheet" href="style/style.css" type="text/css" />
    <link rel="stylesheet" href="style/login.css" type="text/css" />
    <link rel="stylesheet" href="style/before_login.css" type="text/css"/>    
    <style type="text/css">
body {margin:20px auto auto 6px;text-align:center;padding:0;line-height:22px;}
img {border:0}
.tip {padding:4px 0 6px 46px;color:#999;}
#Logo {width:760px;margin:auto;text-align:left;}
#Logo .lg {position:absolute;top:22px}
#Logo .nav {float:right;margin-right:5px;color:#1A82D2}
#Main {width:770px;margin:auto;text-align:center;}
#Bot {width:750px;clear:both;margin:auto;text-align:center;line-height:18px;border-top:1px solid #DADADA;padding-top:13px;margin-top:25px;}
#Bot a {color:#494949;}
#Banner {width:503px;height:170px;float:left;margin-top:30px;}
#Banner .conn_left{float:left;width:3px;height:170px;background:#208DE1}
#Banner .conn_img {margin-top:164px;}
#Banner .index_banner {float:left;background:#208DE1;width:216px;height:170px;}
#Banner .index_bg {float:left;background:url(style/images/index_login_bg.gif) repeat-y;width:280px;height:145px!important; height /**/: 170px;padding:25px 0 0 4px;font:normal 12px tahoma;line-height:24px;text-align:left;color:#fff;}
#Banner .color {clear:both;width:503px;height:16px;background:#D4ECFF;border-top:2px solid #fff;}
#Banner ul {list-style:none;margin:12px 0 0 6px;}
#Banner ul li {text-align:left;height:48px;}
.txt {color:#1274BA;}
.txt_ {font-family:tahoma;}
.txt1 {color:#F86B2D;}
.txt2 {font-size:11px!important; font-size /**/:8pt;font-family:tahoma;}
.left {float:left;}
.right {float:right;}
#Login {width:255px;float:left;font-family:tahoma;color:#494949}
#Login .top {height:4px;background:url(style/images/login_top_bg.gif) repeat-x;}
#Login .login_bg {height:270px;background:#F9F9F9;border-right:1px solid #8A8A8A;border-left:1px solid #8A8A8A;}
#Login .lg_title {text-align:left;height:23px;margin:0 11px 0px 11px;padding-top:3px;padding-left:4px;border-bottom:1px solid #B5B5B5;}
#Login .lg_title1 {text-align:left;margin:5px 11px;padding-top:3px;padding-left:4px;border-bottom:1px solid #B5B5B5;}
#Login .lg_title2 {text-align:left;margin:0 11px;padding-top:3px;padding-left:4px;color:#ff0000;}
#Login .input_id {text-align:left;margin:0px 0 0 26px;}
#Login .input_pwd {text-align:left;margin:6px 0 0 26px;}
#Login .input_vc {text-align:left;margin:6px 0 0 14px;}
#Login .input_post {text-align:left;margin:8px 0 0 75px;}
#Login .input_fpwd {text-align:left;margin:5px 0 0 32px;}
#Login .bottom {height:4px;background:url(style/images/login_bottom_bg.gif) repeat-x;}
#Login .intro_txt {color:#959595;text-align:left;margin-left:62px;}
#Login .txt3 {text-align:left;margin:10px 0 0 22px; clear:both}
.input_n {width:110px;height:15px !important; height /**/:20px;border-style:inset;padding:2px 0 0 2px;font:normal 12px tahoma;}
.input_s {width:62px;height:27px;padding-top:2px;font-weight:bold;border-style:outset;}
#Right {margin-top:30px;float:left;width:12px;height:170px;background:#A5D3F7;}
#Right .color {margin-top:170px;width:12px;height:16px;background:#D4ECFF;border-top:2px solid #fff;}
</style>  
    <script type="text/javascript">
	function $(arg) {
		var r_obj = null;
		if(typeof(arg) == 'object') {
			return arg;
		}
		r_obj = document.getElementById(arg);
		if (r_obj == null) {
			r_obj = document.getElementsByName(arg)[0];
		}
		return r_obj;
	}

	try {
	  document.oncontextmenu = new Function('return false;');
	} catch (e) {}

	var useSoftBoard = false;
	window.onload = function() {
		//优化软键盘 start
		var funX = $('button12').onclick;
		$('button12').onclick = function () {
			funX();
			//useSoftBoard = true;
			$('pas').readOnly = false;
			//$('login_3').className = 'tip_off';
		}
		// end
	}
    </script>
    <script type="text/javascript">
if (top.location != location) top.location.href = location.href;
self.moveTo(0,0);
self.resizeTo(screen.availWidth,screen.availHeight);
function checkform(){
  if (document.all.UserNameBox.value==""){
	  alert("请输入用户名")
	  document.all.UserNameBox.focus();
	  return false
		}
  if (document.all.pas.value==""){
	  alert("请输入密码");
	  document.all.pas.focus();
	  return false
} 
if (document.all.CCode.value==""){
alert("请输入验证码");
document.all.CCode.focus();
return false
}
return true
}
//重新生成验证码
function ChangeMap(obj){
    obj.src = "/vercode.aspx?code=" + Math.random();
}
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return checkform()">
        <div id="Logo">
            <div style="float: left">
                <div class="lg">
                    <a href="#" target="_blank">
                        <img src="style/images/logo.gif" border="0" alt="" /></a></div>
            </div>
            <div class="nav">
                <a href="#" target="_blank">产品网站</a>&nbsp;-&nbsp;<a href="#">系统帮助</a></div>
            <div style="clear: both">
            </div>
        </div>
        <div id="Main">
            <div id="Banner">
                <div class="conn_left">
                    <img src="style/images/index_conn_left.gif" width="3" height="3" alt=""/>
                    <img src="style/images/index_conn_left_bottom.gif" width="3" height="3" class="conn_img" alt=""/></div>
                <div class="index_banner">
                    <img src="style/images/login.gif" alt=""/></div>
                <div class="index_bg">
                    我们拥有强大的技术团队力量为您服务<br />
                    最简单化、最便捷化的服务器端一键安装配置使用<br />
                    系统稳定、程序安全、是我们追求的目标<br />
                    精确化建设、协作化管理、流程化控制<br />
                    降低总体成本、降低实施风险</div>
                <div class="color">
                </div>
                <div style="margin-top:6px;text-align:left;">&nbsp;&nbsp;&nbsp;</div>
            </div>
            <div id="Login">
                <div class="top">
                    <div class="left">
                        <img src="style/images/login_conn_left.gif" width="4" height="4" alt=""/></div>
                    <div class="right">
                        <img src="style/images/login_conn_right.gif" width="4" height="4" alt=""/></div>
                </div>
                <div class="login_bg">
                    <div class="lg_title">
                        <b class="txt">管理员登录</b></div>
                    <div class="lg_title2">
                        <span id="MessageLabel" style="display:inline;width: 100%;font-size:12px"></span>
                        <br />
                    </div>
                    <div class="input_id">
                        帐 号：<input class="colorblur" id="UserNameBox" type="text" name="UserNameBox" value="" /></div>
                    <div class="input_pwd">
                        密 码：<input class="colorblur" id="pas" type="password" name="pas" value="" style="width: 86px"
                            onchange="$('Calc').password.value=this.value;keyjiami=0;jiami = 0" onfocus="if($('softkeyboard').style.display == 'none') { if(isShowKeyboard()) { this.value = ''; password1=this; showkeyboard(); this.readOnly=1; $('Calc').password.value=''; } if(useSoftBoard) { this.value = '';useSoftBoard = false; } }" /><span
                                id="span0" title="使用软键盘输入" style="cursor: pointer" onclick="if (true) { $('pas').value = ''; password1 = $('pas'); showkeyboard(); $('pas').readOnly = 1; $('Calc').password.value = ''; }; ">
                                <img height="18" alt="" src="style/images/softkeyboard.gif" width="56" border="0"/></span></div>
                    <div class="input_pwd">
                        验证码：<input class="colorblur" onblur="this.style;" maxlength="5" size="6" name="CCode"
                            id="CCode" />
                        <img src="/vercode.aspx" alt="看不清，点击更换图片。" name="codeimg" width="63" height="22"
                            class="mid" id="codeimg" style="margin-bottom: -4px; margin-left: 2px; cursor: pointer"
                            onclick="ChangeMap(this)" /></div>
                    <div class="input_post">
                        <input type="submit" class="button" value=" 登 录 " name="Submit" /></div>
                    <div class="input_fpwd">
                        <input id="RememberMe" type="checkbox" name="RememberMe" checked="checked" /><label
                            for="RememberMe">在此计算机上保留登录帐号</label></div>
                    <div class="lg_title1">
                    </div>
                    <div class="txt3">
                        .NET 版本：2.0<br />
                        数据库：Microsoft SQL Server 2008
                    </div>
                </div>
                <div class="bottom">
                    <div class="left">
                        <img src="style/images/login_conn_left_b.gif" width="4" height="4" /></div>
                    <div class="right">
                        <img src="style/images/login_conn_right_b.gif" width="4" height="4" /></div>
                </div>
            </div>
            <div id="Right">
                <div class="right">
                    <img src="style/images/index_conn_right.gif" width="3" height="3" /></div>
                <div class="color">
                </div>
            </div>
            <div style="clear: both">
            </div>
        </div>
        <div id="Bot">
            <span>Copyright &copy; 2012 <a href="#" target="_blank"></a>.All Rights Reserved</span></div>
    </form>
    <script type="text/javascript" src="style/ccbsoftkeyboard.htm" charset="gbk"></script>
</body>
</html>
