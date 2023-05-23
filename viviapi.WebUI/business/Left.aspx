<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Business.Left" Codebehind="Left.aspx.cs" %>

<html xmlns="">
<head>
    <title>导航</title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" href="style/left1a.css" type="text/css" />
    <link rel="stylesheet" href="style/left1b.css" type="text/css" />
    <style type="text/css">
.left_color { text-align:left; }
.left_color a {text-indent:40px; background: url(style/images/item.gif) 18px 1px no-repeat;color: #083772; text-decoration: none; font-size:12px; display:block !important; width:150px !important; width:150px; height:22px; line-height:22px;}
.left_color a:hover { color: #1075bd; width:149px;background:#d6e3ef url(style/images/item.gif) 18px 1px no-repeat; height:22px;line-height:22px;}
img { float:none; vertical-align:middle; }
#on { background:#fff url("images/menubg_on.gif") right no-repeat; color:#f20; font-weight:bold; }
hr { width:90%; text-align:left; size:0; height:0px; border-top:1px solid #46A0C8;}
</style>

    <script type="text/javascript">
	function disp(n){
		for (var i=0;i<6;i++){
			//if (!document.getElementById("left"+i)) return;			
			document.getElementById("left"+i).style.display="none";
		}
		document.getElementById("left"+n).style.display="block";
	}
	  
    function ShowMenu(strValue){
	    document.getElementById("left1").style.display="block";
    }
    </script>

</head>
<body style="margin-top: 0px;">
    <div class="columncontent" style="margin: 0px;">
        <table width="150" border="0" cellpadding="0" cellspacing="0">
            <tr class="tdbg">
                <td valign="top" class="left_color" id="menubar">
                    <div id="left0" style="display: ">
                        <div class="lefttab">日常使用</div>
                        <div style="padding-top: 10px"></div>
                        <div style="padding-top: 10px"></div>                    
                        <a id="a1" runat="server" target="rightframe" href="Order/BankOrderList.aspx">网银订单查询</a>
                        <a id="a2" runat="server" target="rightframe" href="Order/CardOrderList.aspx">点卡订单查询</a>
                        <a id="a8" runat="server" target="rightframe" href="orderreport2.aspx">统计分析</a>
                        <a id="a3" runat="server" target="rightframe" href="User/UserList.aspx?UserStatus=2">商户列表</a> 
                        <a id="a4" runat="server" target="rightframe" href="User/UserList.aspx?UserStatus=4">已锁定商户</a> 
                        <a id="a5" runat="server" target="rightframe" href="User/UserList.aspx?UserStatus=1">未审核商户</a>                        
                        <a id="a6" runat="server" target="rightframe" href="Contact.aspx" >团队联系页面</a>                                             
                        <a id="a7" runat="server" target="rightframe" href="stat.aspx">员工业务统计</a>   
                        <a id="a9" runat="server" target="rightframe" href="Index.aspx">员工推广连接获取</a>
                        <a id="a10" runat="server" target="rightframe" href="ManageLoginLog.aspx">管理员登录日志</a>     
                    </div>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
