<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.agent.Index" Codebehind="Index.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" href="style/left1b.css" type="text/css" />
    <style type="text/css">
p {clear:both;margin:1px;padding:2px 8px;background:#f5f5f5;}
td,input,button,select,body {font-family:"lucida Grande",Verdana;font-size:12px;}
.welcomeinfo {font:bold 16px "lucida Grande",Verdana;height:39px;margin:0 0 0 118px;}
.t_left1 {margin-left:17px;}
.nowrap {white-space:nowrap; text-decoration:none}
.f_size,.f_sizetd {font-size:11px;}
.normal {font-weight:normal}
img {border:none}
a {text-decoration:none;cursor:pointer;}
.level {margin-bottom:6px;margin-left:1px;font:normal 12px "lucida Grande",Verdana;clear:both;}
.level_no {margin-top:8px;margin-bottom:17px;}
.leftpanel {margin:0 0 14px 0;padding:0;list-style:none;}
.left {float:left}
.right {float:right}
.todaybody {overflow:auto;overflow-x:hidden}
ul.tipbook {border-top:1px solid #d8dee5;margin:0 13px;padding:18px 0 0 2px;list-style:none;}
ul.tipbook li {height:28px;}
.tipstitle_title {font:normal 12px Verdana;padding-left:13px;line-height:20px}
.ico_input {border:none;padding:0;margin:0;width:16px;height:16px;}
.ico_helpmin {background:url(Images/help.gif) no-repeat;margin:0 6px 1px 0}
.ico_bbsmin {background:url(Images/bbs.gif) no-repeat;margin:0 6px 1px 0}
body,td,th {color: #083772;}
</style>

    <script type="text/javascript">

function shouquan()
{
  alert(fsdf);
//   if(document.getElementById("paysoudiv").style.display==none)
//   {
//  document.getElementById("paysoudiv").style.display=="";
//   }
//   else
//   {
// document.getElementById("paysoudiv").style.display==none;
//   }
}
    </script>

</head>
<body class="todaybody">
    <form id="form1" runat="server">
        <ul class="leftpanel" style="margin-bottom: 0; border: none;">
            <li style="padding-right: 200px; height: auto" id="TodayWelcome" class="welcomeinfo t_left1">
                <script type="text/javascript">
		var hour = (new Date()).getHours();
		if (hour < 4) {
			hello = "夜深了，";
		}
		else if (hour < 7) {
			hello = "早安，";
		}
		else if (hour < 9) {
			hello = "早上好，"; 
		}
		else if (hour < 12) {
			hello = "上午好，";
		}
		else if (hour < 14) {
			hello = "中午好，";
		}
		else if (hour < 17) {
			hello = "下午好，";
		}
		else if (hour < 19) {
			hello = "您好，";
		}
		else if (hour < 22) {
			hello = "晚上好，";
		}
		else {
			hello = "夜深了，";
		}
		document.write(hello);
                </script>

                <%=username %>
                <span class="f_size normal addrtitle" id="spanGreeting">。</span>
                <div class="oneheight">
                </div>
                <div class="level level_no" style="width: 500px; line-height: 20px; font-size: 11px">
                    <div style="color: #009900">
                        <br />
                        <span runat="server" id="paysouid" style="color: Red;"></span>
                    </div>
                </div>
            </li>
        </ul>
        <div style="width: 99%; margin: 5px; height: 170px;" id="TodayPartNotice" class="bd column">
            <div class="tipstitle_title bd settingtable bold columntitle" style="border-width: 0 0 1px 0; padding-top: 1px; *padding-top: 1px; height: 20px">统计信息</div>
            <div id="contentBulletin_">
                <div style="height: 70px; margin: 4px">
                    <div>
                        <div class="left b_size" style="margin: 1px; width: 99%">
                            <div class="addrtitle f_size">
                                <p>账上余额：<%=balance%>元</p>
                                <p>商户总数：<%=userscount%>个</p>
                                <p>商户今日充值量：<%=todaytotalAmt%>元</p>
                                <p>商户当月充值量：<%=monthtotalAmt%>元</p>
                                <p>商户当年充值量：<%=yeartotalAmt%>元</p>
                                <p>代理当月提成：<%=monthcommission%>元</p>
                                <p>推广连接获取：http://www.mcfl.cn/regedit.aspx<asp:Literal ID="litlinks" runat="server"></asp:Literal></p>
                            </div>
                        </div>
                    </div>
                </div>                          
                <div id="loader_update" style="padding: 5px; padding-left: 25px;">
                    
                </div>
            </div>
        </div>
        <div class="clr" style="height: 23px; margin: 8px 0 0 10px">
            <div class="left" style="margin: 0px">
                上次登录时间：<%=logintime %>
                上次登录IP：<%=loginip %></div>
        </div>
        <div style="height: 23px; margin: 8px 0 0 10px; color: #666666; font-size: 11px">
            Copyright &copy; . All Rights Reserved</div>
    </form>
</body>
</html>
