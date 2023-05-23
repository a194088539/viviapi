<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="smartexcha.aspx.cs" Inherits="viviapi.WebUI.Userlogin.order.smartexcha" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" href="../style/css/style.css"  type="text/css"  media="screen" />
    <link rel="stylesheet" href="../style/css/m_global.css"  type="text/css"  media="screen" />
    <link rel="stylesheet" href="../javascript/plugin/jqtransform.css" type="text/css" media="all" />
<script type="text/javascript">
    function sindex(tid) {
        $(document).ready(function() {
            $.get("?writein=index&tid=" + tid);
            $('#ex_index').html("<a href=javascript:(); style=font-size:12px;font-weight:normal>(首选通道)</a>");
        })
    }
    function cleanup() {
        var cardcontent = $("#arr_content").val();
        if (cardcontent == '') {
            alert('请输入卡号和密码');
            $('#arr_content').focus();
            return false;
        }
        var sSource = '/merchant/ajax/cleanupcardcontent.ashx?t=' + Math.random();
        var postData = 'cardcontent=' + encodeURIComponent(cardcontent);

        $.ajax({
            type: "post",
            dataType: "json",
            timeout: 10000,
            url: sSource,
            data: postData,
            success: function(json) {

                if (json.result == 'ok') {
                    $('#arr_content').val(json.msg);
                } else {
                    //dialog_simple_fail(json.msg);
                    alert(json.msg);
                }
            },
            error: function(a, b) {
            }
        })

        return false;
    }

    function clearchar() {
        var str = $("#arr_content").val();
        str = str.replace(eval('/' + $('#customchar').val() + '/g'), '');
        $("#arr_content").val(str);
        $("#customchar").val('');
    }

</script>
<style type="text/css"> 
.x_radio{float:left;width:86px;}
.x_radio i{line-height:35px; font-style:normal}
.x_radio input{float:left}
.x_radio label{line-height:25px;height:25px; display:block;float:left;padding:0;width:auto}
</style>

<script type="text/javascript" src="/javascript/app_user_commonS.js"></script>
</head>
<body>
<form id="form1" runat="server">
  <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/order/index.aspx'">订单管理</a> &nbsp;&gt;&nbsp;
        <span>批量销卡</span>
    </div>
	  <div class="form-put b_clear" style="min-height:40px;*height:auto;_height:40px;"><a style="vertical-align:middle;" href="/kefu1" target="_blank"><img style="vertical-align:middle;" src="/images/chongz_23.gif"/></a>&nbsp;&nbsp;&nbsp;<font style="font-size:13px;">温馨提示:<strong>若卡密失败，可联系客服核实使用情况</strong></font>&nbsp;&nbsp;&nbsp;<a href="/merchant/order/channel.aspx" target=_blank style="font-size:15px;">查看回收价格</a>&nbsp;&nbsp;&nbsp;<a href="/query.aspx" target=_blank style="font-size:15px;">查看余额</a></div>	  
	   <div class="b_m_t chart-box">
	    <h4 id="m1"><a href="/Merchant/excha.aspx" >普通提交</a></h4><h4 id="m2"  style="left:87px" class="ac">智能提交</h4>
		<div class="clear"></div>
		 <div class="chart-c">
	     <div id="m1-box"  style="display:none">	     
	     <div class="b_r" style="width:170px;background:url(/style/images/onebit_36.png) no-repeat 10px 50px;padding-left:70px;padding-top:50px;line-height:160%;"><span class="font14 txtc" style="font-weight:bold">重要提示：</span><br>⑴ 如果确定卡号、密码、卡面额输入正确，但系统提示错误，请重新输入提交一次；<br><span class="font14 txtr">⑵ 在提交移动、联通卡时如不清楚面额的，请一定选择最大面额进行提交，避免造成损失。</span>
		</div>
        <div id="form_postorder" style="padding-right:15px;margin-right:15px;border-right:1px solid #E1E2E2;width:480px">
        <input value="14" type="hidden" name="ChannelId" id="xk_channelId" runat="server"/>
        <input value="移动充值卡" type="hidden" name="Channelname" id="xk_channelname" runat="server"/>
  		<div class="form-put b_clear" style="min-height:40px;*height:auto;_height:40px;"><label>面值：</label>
  		<div id="facevaluelist" runat="server" class="b_l" style="width:350px"></div></div>
		<div class="form-put b_clear" style="overflow:hidden"><label>确认面额</label><div style="width:320px;padding-left:10px"><em class="txtc font14"><span id="xmoney" style="font-size:18px"><%=defaultvalue%></span> 元</em></div></div>
  		<div class="form-put"><label for="xk_cardId">卡号</label><div style="width:320px;padding-left:10px;"><input  name="CardId" type="text" id="xk_cardId"  class="required" onKeyUp="this.value=this.value.replace(/[^\w\/]/ig,'')" onBlur="this.value=this.value.replace(/[^\w\/]/ig,'')" maxlength="30"/></div></div>
  		<div class="form-put"><label for="xk_cardPass">卡密</label><div style="width:320px;padding-left:10px;"><input style="position:relative;" name="CardPass" type="text" id="xk_cardPass" class="required" onKeyUp="this.value=this.value.replace(/[^\w\/]/ig,'')" onBlur="this.value=this.value.replace(/[^\w\/]/ig,'')" maxlength="30"/></div></div>
  		<div class="form-put"><button type="button" class="button green">提交兑换</button> <button type="reset" class="button">重新填写</button> <span class="txtr" id="callinfo"></span></div>
  </div>          
	  </div>
	     <div id="m2-box">
		<div class="b_r" style="width:170px;background:url(/common/images/onebit_36.png) no-repeat 10px 50px;padding-left:70px;padding-top:35px;line-height:160%;">
<span class="font14 txtc" style="font-weight:bold">智能提交说明：</span>
<br>1. 程序自动判断充值卡类型,无需手动选择；
<br><span class="txtr">2. 程序自动整理卡号卡密,去除非法字符只保留卡密。</span>
<br />3. 面值不同的充值卡，可以一起提交
<br />4. 默认面值为100，大面值注意自己选择面值
<br>5. 自定义非法字符,可以指定批量去除某字符
<br>6. 批量提交一次最多60张卡,系统只提取前60张
<br><span class="txtr">7. 如果智能提交判断错误，请切换到普通提交</span>
		</div>
		</div>
        <div id="form_Groupscard" style="padding-right:15px;margin-right:15px;border-right:1px solid #E1E2E2;width:682px;overflow:hidden">
        <input value="14" type="hidden" name="ChannelId" id="g_channelId" runat="server"/>
        <div>
  		
        
  		
		<textarea style="background-color: rgb(255, 255, 255);font-size:14px;width:660px;height:250px;font-family:Verdana, Arial, Helvetica, sans-serif;line-height:180%;border-radius:5px;border:0px solid #ebebeb;padding:10px;ime-mode:disabled" name="arr_content" id="arr_content" cols="75" rows="16" onBlur="this.value=this.value.replace(/[\u4E00-\u9FA5]/g,'')" onmouseover="this.style.borderColor='#7ECAF1'" onmouseout="this.style.borderColor='#ebebeb'"></textarea>
	<br>
  		<div>
  		<div class="jqTransformInputWrapper jqTransformSafari" style="width: 210px;"><div class="jqTransformInputInner"><div><input id="customchar" name="customchar" type="text" style="width: 530px;" class="jqtranformdone jqTransformInput"></div></div></div>
						<a class="form_btn" href="javascript:" onclick="clearchar();" style="margin-left:3px;/* color: #ffffff; *//* font-size: 12px; */padding: 8px 56px;/* background-color: #FF876C; */ background-image: url(/img/qcgzf.png);"></a>&nbsp;
						<a class="form_btn" href="javascript:" onclick="cleanup();" style=" /* background-color: transparent; */ /* font-size: 8px; */ padding: 8px 56px; /* background-color: #3D9228; *//* color: #ffffff; *//* padding: 8px 55px; *//* background-color: rgba(61, 146, 40, 0); */background-image: url(/img/zlkm.png);/* width: 111px; */ /* height: 32px; */"></a> </div>    
		</div><br>
        <div class="form-put b_clear" style="min-height:40px;*height:auto;_height:40px;"><label style="color:#FF6600;font-size:16px;font-family:'Microsoft YaHei';font-weight:bold">选择面值：</label>
		<div id="facevaluelist_1" runat="server" class="b_l" style="width:550px"></div></div>
		<div class="b_m_t">&nbsp;&nbsp;&nbsp;&nbsp;已经输入 <span id="Groupscount" class="txtc" style="font-size:18px">0</span> 张&nbsp;<span id="mutixmoney" style="font-weight:bold;color:#FF6600;font-size:18px;padding:0px;"><%=defaultvalue%></span>&nbsp; 元面值充值卡，每次最多提交20张，系统只取<font color=red>前20</font>张卡</div>
		<div class="form-put b_clear" style="overflow:hidden"><span style="margin-top:5px;"><button type="button" class="button green" style="font-size:16px;margin-left:5px;width:230px;height:40px;font-weight:bold;">我已经选好面值,确认提交</button> <!--<button type="reset" class="button">重新填写</button>--></span></div>
		 
		<div id="Groupsload"></div><div class="b_m_t txtr dis-n" id="Groupsinfo"></div><div id="Groupsinfo_01" style="margin-bottom:5px"></div><div id="Groupsinfo_02" style="margin-bottom:5px"></div><div id="Groupsinfo_03" style="margin-bottom:5px"></div><div id="Groupsinfo_04" style="margin-bottom:5px"></div><div id="Groupsinfo_05" style="margin-bottom:5px"></div><div id="Groupsinfo_06" style="margin-bottom:5px"></div><div id="Groupsinfo_07" style="margin-bottom:5px"></div><div id="Groupsinfo_08" style="margin-bottom:5px"></div><div id="Groupsinfo_09" style="margin-bottom:5px"></div><div id="Groupsinfo_10" style="margin-bottom:5px"></div><div id="Groupsinfo_11" style="margin-bottom:5px"></div><div id="Groupsinfo_12" style="margin-bottom:5px"></div><div id="Groupsinfo_13" style="margin-bottom:5px"></div><div id="Groupsinfo_14" style="margin-bottom:5px"></div><div id="Groupsinfo_15" style="margin-bottom:5px"></div><div id="Groupsinfo_16" style="margin-bottom:5px"></div><div id="Groupsinfo_17" style="margin-bottom:5px"></div><div id="Groupsinfo_18" style="margin-bottom:5px"></div><div id="Groupsinfo_19" style="margin-bottom:5px"></div><div id="Groupsinfo_20" style="margin-bottom:5px"></div>
		<div id="Groupsinfo_21" style="margin-bottom:5px"></div><div id="Groupsinfo_22" style="margin-bottom:5px"></div><div id="Groupsinfo_23" style="margin-bottom:5px"></div><div id="Groupsinfo_24" style="margin-bottom:5px"></div><div id="Groupsinfo_25" style="margin-bottom:5px"></div><div id="Groupsinfo_26" style="margin-bottom:5px"></div><div id="Groupsinfo_27" style="margin-bottom:5px"></div><div id="Groupsinfo_28" style="margin-bottom:5px"></div><div id="Groupsinfo_29" style="margin-bottom:5px"></div><div id="Groupsinfo_30" style="margin-bottom:5px"></div><div id="Groupsinfo_31" style="margin-bottom:5px"></div><div id="Groupsinfo_32" style="margin-bottom:5px"></div><div id="Groupsinfo_33" style="margin-bottom:5px"></div><div id="Groupsinfo_34" style="margin-bottom:5px"></div><div id="Groupsinfo_35" style="margin-bottom:5px"></div><div id="Groupsinfo_36" style="margin-bottom:5px"></div><div id="Groupsinfo_37" style="margin-bottom:5px"></div><div id="Groupsinfo_38" style="margin-bottom:5px"></div><div id="Groupsinfo_39" style="margin-bottom:5px"></div><div id="Groupsinfo_40" style="margin-bottom:5px"></div><div id="Groupsinfo_41" style="margin-bottom:5px"></div><div id="Groupsinfo_42" style="margin-bottom:5px"></div><div id="Groupsinfo_43" style="margin-bottom:5px"></div><div id="Groupsinfo_44" style="margin-bottom:5px"></div><div id="Groupsinfo_45" style="margin-bottom:5px"></div>
		<div id="Groupsinfo_46" style="margin-bottom:5px"></div><div id="Groupsinfo_47" style="margin-bottom:5px"></div><div id="Groupsinfo_48" style="margin-bottom:5px"></div><div id="Groupsinfo_49" style="margin-bottom:5px"></div><div id="Groupsinfo_50" style="margin-bottom:5px"></div><div id="Groupsinfo_51" style="margin-bottom:5px"></div><div id="Groupsinfo_52" style="margin-bottom:5px"></div><div id="Groupsinfo_53" style="margin-bottom:5px"></div><div id="Groupsinfo_54" style="margin-bottom:5px"></div><div id="Groupsinfo_55" style="margin-bottom:5px"></div><div id="Groupsinfo_56" style="margin-bottom:5px"></div><div id="Groupsinfo_57" style="margin-bottom:5px"></div><div id="Groupsinfo_58" style="margin-bottom:5px"></div><div id="Groupsinfo_59" style="margin-bottom:5px"></div><div id="Groupsinfo_60" style="margin-bottom:5px"></div>
		
		<div class="clear"></div>
		</div><!--/批量提交结束-->
		
		</div>
		</div>	
		卡提交成功后，可在<a href="/Merchant/order/cardlist.aspx" rel="target" style="font-weight:bold;font-size:13px;font-family:'黑体';">订单查询</a>页面查询支付结果。支付结果以订单查询页为准。注：请一定正确选择卡面值,否则造成损失商户自行承担.
		
		<div class="b_m_t chart-box" style="margin-top:10px;"><h4 id="H1" class="ac">最近8条订单</h4>
		   <div class="chart-c">
              <p style="margin-left:150px"><button class="button" id="queryorder" style="margin-right:0" type="button" onClick="queryOrder();">刷新列表</button>&nbsp;&nbsp;&nbsp;&nbsp;<font color=#FF6600>建议：间隔30秒后再刷新，若订单失败，未使用的卡密请核实后重新提交</font></p>
		     <table id="m_orderlist" align="center">
	      <thead>
			<tr>
				<td>卡号</td>
				<td>支付方式</td>
				<td>提交金额</td>
				<td>成功金额</td>
				<td>订单状态</td>
				<td>说明</td>
				<td>提交时间</td>
				<td>操作</td>
			</tr>
			 </thead>
			     <tbody id="toporder">
			<%--  </HeaderTemplate>	--%> 
			<asp:Repeater ID="rptorders" runat="server" 
                     onitemdatabound="rptorders_ItemDataBound">
			    <ItemTemplate>
			        <tr>
				    <td><%#Eval("cardNo")%></td>
				    <td><%#Eval("modetypename")%></td>
				    <td><%#Eval("refervalue","{0:0.00}")%></td>
				    <td id="paymoney<%#Eval("ID")%>"><%#GetViewSuccessAmt(Eval("status"),Eval("realvalue"))%></td>
				    <td id="orderzt<%#Eval("ID")%>"><%#GetViewStatusName(Eval("status"))%></td>
				    <td id="errorMsg<%#Eval("ID")%>"><%#Eval("msg")%></td>
				    <td><%#Eval("addtime","{0:yyyy-MM-dd HH:mm}")%></td>
				    <td><button class="button1 green" id="sub<%#Eval("ID")%>" style="margin-right:0" type="button" onClick="checkflag('<%#Eval("ID")%>')">刷新</button></td>
			    </tr>
			</ItemTemplate>
			<FooterTemplate>
                <asp:Literal ID="litfooter" runat="server"></asp:Literal>
          
			</FooterTemplate>
			</asp:Repeater>	
      </table>
			</tbody>
             <table>
                <tfoot>
                     <tr>
                        <td>
                              <aspxc:aspnetpager ID="Pager1" runat="server" AlwaysShow="False" CustomInfoHTML="共%PageCount%页/%RecordCount%条"
                                CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                                NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="TextBox"
                                PageSize="10" PrevPageText="上一页" ShowBoxThreshold="50" 
                                 ShowCustomInfoSection="Right" ShowPageIndexBox="Never" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到" Width="800px" Height="30px"
                                OnPageChanged="Pager1_PageChanged" CustomInfoSectionWidth="20%" 
                                 PageIndexBoxClass="Pager1_input" PageIndexBoxStyle="width:10px">
                            </aspxc:aspnetpager>
                        </td>
                    </tr>	 
 </tfoot>
            </table>
		   </div>
		</div>
	  </div>
	  </div>
   </div>
   
    </form>
</body>
</html>
