<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.Left" Codebehind="Left.aspx.cs" %>

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
		for (var i=0;i<8;i++){
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
                        <a target="rightframe" href="Order/BankOrderList.aspx?status=2">成功订单</a>
                        <%--<a target="rightframe" href="DayCount.aspx">综合统计</a>--%>
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=1">商户列表</a>
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=0">商户审核</a>
                        <a target="rightframe" href="News/NewsEdit.aspx">新闻发布</a>
                        <a target="rightframe" href="Withdraw/Audits.aspx">结算审核</a>
                    </div>
                    <div id="left1" style="display: none;">
                        <div class="lefttab">常规设置</div>
                        <div style="padding-top: 10px"></div>                    
                        <a target="rightframe" href="News/NewsList.aspx" > 新闻管理</a>
                        <a target="rightframe" href="News/NewsEdit.aspx" >新闻发布</a>                                             
                        <a target="rightframe" href="SiteInfo.aspx">站点设置</a> 
                        <a target="rightframe" href="emailconfig.aspx">邮件配置</a>   
                        <%--<a target="rightframe" href="User/Questions.aspx">问题管理</a>--%>   
                        <a target="rightframe" href="Manage.aspx">管理员列表</a>
                        <a target="rightframe" href="ManageLoginLog.aspx">管理员登录日志</a>
                        <a target="rightframe" href="CahceManage.aspx" >清理系统缓存</a>
                         <a target="rightframe" href="GwCahceManage.aspx" >网关缓存管理</a>
                        <a target="rightframe" href="DataBackup.aspx" >数据备份</a>
                        <a target="rightframe" href="CleanUpData.aspx" >数据清理</a> 
                        <a target="rightframe" href="Salesman.aspx" >业务业绩</a>
                        <a target="rightframe" href="Trades.aspx" >结算记录</a>  
                        <a target="rightframe" href="Template/Configuration.aspx" >短信模板</a>                         
                    </div>
                    <div id="left2" style="display: none">
                        <div class="lefttab">订单管理</div>
                        <div style="padding-top: 10px"></div>
                        <a target="rightframe" href="Order/BankOrderList.aspx?status=2" >网银订单</a>
                        <a target="rightframe" href="Order/CardOrderList.aspx?status=2" >点卡订单</a>                      
                        <a target="rightframe" href="Order/SmsOrderList.aspx" >短信订单</a> 
                        <a target="rightframe" href="Order/BankOrderList.aspx?status=8" >网银扣量</a>
                        <a target="rightframe" href="Order/CardOrderList.aspx?status=8" >点卡扣量</a>
                        <a target="rightframe" href="Order/BankReportList.aspx" >网银状态报告</a>
                        <a target="rightframe" href="Order/CardReportList.aspx" >点卡状态报告</a> 
                        <a target="rightframe" href="Order/SmsReportList.aspx" >短信状态报告</a>                        
                        <a target="rightframe" href="Order/ResetOrder.aspx" >补单功能</a>
                        <a target="rightframe" href="Order/Reconciliation2.aspx" >接口补单</a>
                        <a target="rightframe" href="Stat/OrderReport.aspx" >综合统计</a>
                        <a target="rightframe" href="Order/DebugInfos.aspx" >交易日志</a>
                        <a target="rightframe" href="Order/Reconciliation.aspx" >对账查询</a>
                        <a target="rightframe" href="Md5.aspx" >MD5加密工具</a>
                        <a target="rightframe" href="Settled/recharges.aspx" >充值记录</a>                 
                    </div>    
                    <div id="left6" style="display: none">
                        <div class="lefttab">统计分析</div>
                        <div style="padding-top: 10px"></div>
                        <a target="rightframe" href="stat/orderreport7.aspx" >利润分析</a> 
                        <a target="rightframe" href="stat/orderreport2.aspx" >对账统计</a>
                        <a target="rightframe" href="stat/orderreport3.aspx" >收支统计</a>
                        <a target="rightframe" href="stat/orderreport4.aspx" >代理收益</a>
                        <a target="rightframe" href="stat/usersOrderIncomes.aspx" >商户收益</a>
                        <a target="rightframe" href="stat/orderreport5.aspx" >风控管理</a> 
                        <a target="rightframe" href="stat/orderreport6.aspx" >业务统计</a>          
                    </div>                     
                    <div id="left3" style="display: none">
                        <div class="lefttab">接口管理</div>
                        <div style="padding-top: 10px">
                        </div>                        
                        <a target="rightframe" href="Supplier/SupplierList.aspx" >接口商列表</a>
                        <a target="rightframe" href="Channel/ChannelTypeList.aspx" >通道类型</a> 
                        <a target="rightframe" href="Channel/ChannelList.aspx" >通道管理</a>
                        <a target="rightframe" href="Channel/WithdrawChannels.aspx" >结算通道</a>
                        <%--<a target="rightframe" href="Channel/CodeMappinglList.aspx" >代码映射</a>--%>  
                        <%--<a target="rightframe" href="PayPriceConv.aspx">转化设置</a>--%>
                    </div>
                    <div id="left4" style="display: none">
                        <div class="lefttab">商户&代理管理</div>
                        <div style="padding-top: 10px">
                        </div>
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=2">商户列表</a> 
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=4">已锁定商户</a> 
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=1">未审核商户</a>
                        <a target="rightframe" href="User/UserIdImgList.aspx?s=1">证件审核</a> 
                        <a target="rightframe" href="User/UserHosts.aspx">链接管理</a>
                        <%--<a target="rightframe" href="User/PromUserList.aspx">代理列表</a>--%> 
                        <a target="rightframe" href="User/UserPayAccts.aspx">账户审核</a>
                        <a target="rightframe" href="User/PayRate.aspx">费率设置</a>                        
                        <a target="rightframe" href="User/SMSLogList.aspx">短信记录</a>
                        <a target="rightframe" href="User/UserLoginLog.aspx">登录日志</a> 
                        <a target="rightframe" href="User/UserUpdateLog.aspx">修改日志</a> 
                        <a target="rightframe" href="feedback/list.aspx">反馈处理</a> 
                        <a target="rightframe" href="Jubao/list.aspx" >投诉处理</a>
                    </div>
                    <div id="left5" style="display: none">
                        <div class="lefttab"> 财务管理</div>
                        <div style="padding-top: 10px"></div>                       
                        <a target="rightframe" href="Withdraw/Audits.aspx" >提现审核</a>
                        <a target="rightframe" href="Withdraw/Pays.aspx" >付款操作</a>
                        <a target="rightframe" href="Withdraw/PayingByApi.aspx" >付款中（API）</a>
                        <a target="rightframe" href="Withdraw/Distributions.aspx" >接口付款</a>
                        <a target="rightframe" href="Withdraw/Historys.aspx" >结算记录</a>
                        <a target="rightframe" href="Withdraw/AgentDists.aspx" >对私代发</a>
                        
                        <a target="rightframe" href="Settled/Trades.aspx" >交易记录</a>
                        <a target="rightframe" href="Settled/transfers.aspx" >转账记录</a>
                        <a target="rightframe" href="Settled/IncreaseAmts.aspx" >加款扣款</a>
                        <a target="rightframe" href="Settled/Freeze.aspx" >冻结款项</a>
                        <a target="rightframe" href="Settled/unFreeze.aspx" >解冻款项</a>
                        <a target="rightframe" href="Settled/BankForUser.aspx" >商户结算</a>
                        <a target="rightframe" href="Settled/TocashSchemes.aspx" >提现方案</a>
                        <a target="rightframe" href="Settled/TransferschemeModi.aspx" >转账规则</a>                        
                    </div>
                    <div id="left7" style="display: none">
                        <div class="lefttab"> 对私代发</div>
                        <div style="padding-top: 10px"></div>    
                        <a target="rightframe" href="Withdraw/settledAgentSummarys.aspx" >代发(上传)</a>              
                        <a target="rightframe" href="Withdraw/AgentDists.aspx?audit_status=1" >代发审核</a>
                        <a target="rightframe" href="Withdraw/AgentDists.aspx?audit_status=2" >等待付款</a>
                        <a target="rightframe" href="Withdraw/Distributions.aspx" >接口付款</a>
                        <a target="rightframe" href="Withdraw/AgentDists.aspx?payment_status=4" >代发付款中</a>
                        <a target="rightframe" href="Withdraw/AgentDists.aspx?audit_status=2&payment_status=2" >代发成功</a>
                        <a target="rightframe" href="Withdraw/AgentDistNotifys.aspx" >结果通知</a>
                        <a target="rightframe" href="Withdraw/AgentDists.aspx" >对私代发</a> 
                        <a target="rightframe" href="Withdraw/Distributions.aspx" >接口付款</a> 
                         <a target="rightframe" href="Withdraw/AgentDistsSchemes.aspx" >代发规则</a>                     
                        <!--<a href="GetDayMoney.aspx" target="rightframe">自动结算</a>-->
                    </div>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
