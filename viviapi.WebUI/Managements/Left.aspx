<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.Left" Codebehind="Left.aspx.cs" %>

<html xmlns="">
<head>
    <title>����</title>
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
                        <div class="lefttab">�ճ�ʹ��</div>
                        <div style="padding-top: 10px"></div>
                        <a target="rightframe" href="Order/BankOrderList.aspx?status=2">�ɹ�����</a>
                        <%--<a target="rightframe" href="DayCount.aspx">�ۺ�ͳ��</a>--%>
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=1">�̻��б�</a>
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=0">�̻����</a>
                        <a target="rightframe" href="News/NewsEdit.aspx">���ŷ���</a>
                        <a target="rightframe" href="Withdraw/Audits.aspx">�������</a>
                    </div>
                    <div id="left1" style="display: none;">
                        <div class="lefttab">��������</div>
                        <div style="padding-top: 10px"></div>                    
                        <a target="rightframe" href="News/NewsList.aspx" > ���Ź���</a>
                        <a target="rightframe" href="News/NewsEdit.aspx" >���ŷ���</a>                                             
                        <a target="rightframe" href="SiteInfo.aspx">վ������</a> 
                        <a target="rightframe" href="emailconfig.aspx">�ʼ�����</a>   
                        <%--<a target="rightframe" href="User/Questions.aspx">�������</a>--%>   
                        <a target="rightframe" href="Manage.aspx">����Ա�б�</a>
                        <a target="rightframe" href="ManageLoginLog.aspx">����Ա��¼��־</a>
                        <a target="rightframe" href="CahceManage.aspx" >����ϵͳ����</a>
                         <a target="rightframe" href="GwCahceManage.aspx" >���ػ������</a>
                        <a target="rightframe" href="DataBackup.aspx" >���ݱ���</a>
                        <a target="rightframe" href="CleanUpData.aspx" >��������</a> 
                        <a target="rightframe" href="Salesman.aspx" >ҵ��ҵ��</a>
                        <a target="rightframe" href="Trades.aspx" >�����¼</a>  
                        <a target="rightframe" href="Template/Configuration.aspx" >����ģ��</a>                         
                    </div>
                    <div id="left2" style="display: none">
                        <div class="lefttab">��������</div>
                        <div style="padding-top: 10px"></div>
                        <a target="rightframe" href="Order/BankOrderList.aspx?status=2" >��������</a>
                        <a target="rightframe" href="Order/CardOrderList.aspx?status=2" >�㿨����</a>                      
                        <a target="rightframe" href="Order/SmsOrderList.aspx" >���Ŷ���</a> 
                        <a target="rightframe" href="Order/BankOrderList.aspx?status=8" >��������</a>
                        <a target="rightframe" href="Order/CardOrderList.aspx?status=8" >�㿨����</a>
                        <a target="rightframe" href="Order/BankReportList.aspx" >����״̬����</a>
                        <a target="rightframe" href="Order/CardReportList.aspx" >�㿨״̬����</a> 
                        <a target="rightframe" href="Order/SmsReportList.aspx" >����״̬����</a>                        
                        <a target="rightframe" href="Order/ResetOrder.aspx" >��������</a>
                        <a target="rightframe" href="Order/Reconciliation2.aspx" >�ӿڲ���</a>
                        <a target="rightframe" href="Stat/OrderReport.aspx" >�ۺ�ͳ��</a>
                        <a target="rightframe" href="Order/DebugInfos.aspx" >������־</a>
                        <a target="rightframe" href="Order/Reconciliation.aspx" >���˲�ѯ</a>
                        <a target="rightframe" href="Md5.aspx" >MD5���ܹ���</a>
                        <a target="rightframe" href="Settled/recharges.aspx" >��ֵ��¼</a>                 
                    </div>    
                    <div id="left6" style="display: none">
                        <div class="lefttab">ͳ�Ʒ���</div>
                        <div style="padding-top: 10px"></div>
                        <a target="rightframe" href="stat/orderreport7.aspx" >�������</a> 
                        <a target="rightframe" href="stat/orderreport2.aspx" >����ͳ��</a>
                        <a target="rightframe" href="stat/orderreport3.aspx" >��֧ͳ��</a>
                        <a target="rightframe" href="stat/orderreport4.aspx" >��������</a>
                        <a target="rightframe" href="stat/usersOrderIncomes.aspx" >�̻�����</a>
                        <a target="rightframe" href="stat/orderreport5.aspx" >��ع���</a> 
                        <a target="rightframe" href="stat/orderreport6.aspx" >ҵ��ͳ��</a>          
                    </div>                     
                    <div id="left3" style="display: none">
                        <div class="lefttab">�ӿڹ���</div>
                        <div style="padding-top: 10px">
                        </div>                        
                        <a target="rightframe" href="Supplier/SupplierList.aspx" >�ӿ����б�</a>
                        <a target="rightframe" href="Channel/ChannelTypeList.aspx" >ͨ������</a> 
                        <a target="rightframe" href="Channel/ChannelList.aspx" >ͨ������</a>
                        <a target="rightframe" href="Channel/WithdrawChannels.aspx" >����ͨ��</a>
                        <%--<a target="rightframe" href="Channel/CodeMappinglList.aspx" >����ӳ��</a>--%>  
                        <%--<a target="rightframe" href="PayPriceConv.aspx">ת������</a>--%>
                    </div>
                    <div id="left4" style="display: none">
                        <div class="lefttab">�̻�&�������</div>
                        <div style="padding-top: 10px">
                        </div>
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=2">�̻��б�</a> 
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=4">�������̻�</a> 
                        <a target="rightframe" href="User/UserList.aspx?UserStatus=1">δ����̻�</a>
                        <a target="rightframe" href="User/UserIdImgList.aspx?s=1">֤�����</a> 
                        <a target="rightframe" href="User/UserHosts.aspx">���ӹ���</a>
                        <%--<a target="rightframe" href="User/PromUserList.aspx">�����б�</a>--%> 
                        <a target="rightframe" href="User/UserPayAccts.aspx">�˻����</a>
                        <a target="rightframe" href="User/PayRate.aspx">��������</a>                        
                        <a target="rightframe" href="User/SMSLogList.aspx">���ż�¼</a>
                        <a target="rightframe" href="User/UserLoginLog.aspx">��¼��־</a> 
                        <a target="rightframe" href="User/UserUpdateLog.aspx">�޸���־</a> 
                        <a target="rightframe" href="feedback/list.aspx">��������</a> 
                        <a target="rightframe" href="Jubao/list.aspx" >Ͷ�ߴ���</a>
                    </div>
                    <div id="left5" style="display: none">
                        <div class="lefttab"> �������</div>
                        <div style="padding-top: 10px"></div>                       
                        <a target="rightframe" href="Withdraw/Audits.aspx" >�������</a>
                        <a target="rightframe" href="Withdraw/Pays.aspx" >�������</a>
                        <a target="rightframe" href="Withdraw/PayingByApi.aspx" >�����У�API��</a>
                        <a target="rightframe" href="Withdraw/Distributions.aspx" >�ӿڸ���</a>
                        <a target="rightframe" href="Withdraw/Historys.aspx" >�����¼</a>
                        <a target="rightframe" href="Withdraw/AgentDists.aspx" >��˽����</a>
                        
                        <a target="rightframe" href="Settled/Trades.aspx" >���׼�¼</a>
                        <a target="rightframe" href="Settled/transfers.aspx" >ת�˼�¼</a>
                        <a target="rightframe" href="Settled/IncreaseAmts.aspx" >�ӿ�ۿ�</a>
                        <a target="rightframe" href="Settled/Freeze.aspx" >�������</a>
                        <a target="rightframe" href="Settled/unFreeze.aspx" >�ⶳ����</a>
                        <a target="rightframe" href="Settled/BankForUser.aspx" >�̻�����</a>
                        <a target="rightframe" href="Settled/TocashSchemes.aspx" >���ַ���</a>
                        <a target="rightframe" href="Settled/TransferschemeModi.aspx" >ת�˹���</a>                        
                    </div>
                    <div id="left7" style="display: none">
                        <div class="lefttab"> ��˽����</div>
                        <div style="padding-top: 10px"></div>    
                        <a target="rightframe" href="Withdraw/settledAgentSummarys.aspx" >����(�ϴ�)</a>              
                        <a target="rightframe" href="Withdraw/AgentDists.aspx?audit_status=1" >�������</a>
                        <a target="rightframe" href="Withdraw/AgentDists.aspx?audit_status=2" >�ȴ�����</a>
                        <a target="rightframe" href="Withdraw/Distributions.aspx" >�ӿڸ���</a>
                        <a target="rightframe" href="Withdraw/AgentDists.aspx?payment_status=4" >����������</a>
                        <a target="rightframe" href="Withdraw/AgentDists.aspx?audit_status=2&payment_status=2" >�����ɹ�</a>
                        <a target="rightframe" href="Withdraw/AgentDistNotifys.aspx" >���֪ͨ</a>
                        <a target="rightframe" href="Withdraw/AgentDists.aspx" >��˽����</a> 
                        <a target="rightframe" href="Withdraw/Distributions.aspx" >�ӿڸ���</a> 
                         <a target="rightframe" href="Withdraw/AgentDistsSchemes.aspx" >��������</a>                     
                        <!--<a href="GetDayMoney.aspx" target="rightframe">�Զ�����</a>-->
                    </div>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
