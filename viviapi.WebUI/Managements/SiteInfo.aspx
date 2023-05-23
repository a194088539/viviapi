<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.SiteInfo" ValidateRequest="false" Codebehind="SiteInfo.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>վ������</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="style/admin.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
table { FONT-WEIGHT:normal;line-height:170%;FONT-FAMILY:Arial}
A:link {COLOR:#237C04;TEXT-DECORATION: none}
td {height:20px; line-height:20px; font-size:12px;padding:0px; }
.td_title,th {height:20px;line-height:22px;font-weight:bold;border:0px solid #fff;text-align:left;}
.td1 {padding-right:3px;padding-left:3px;color:#999999;padding-bottom:0px;padding-top:5px;height:25px;}
.td2 {padding-right:3px;padding-left:8px;padding-top:5px;color:#083772;background:#EFF3FB;font-size:12px;text-align:right; width:35%}
.td3 {padding:1px 1px 0 0px;color:#083772;background:#EFF3FB;font-size:12px;text-align:center;}
.moban {padding-top:0px;border:0px}
input { border:1px solid #999;padding:3px;margin-left:10px;font:12px tahoma;ling-height:16px}
.input4 {border:1px solid #999;padding:3px;margin-left:10px;font:11px tahoma;ling-height:16px;height:45px;}
.button {color: #135294; border:1px solid #666; height:21px; line-height:21px;}
.nrml{background-color:#eeeeee;font-weight: bold;}
.radio { border:none; }
.checkbox { border:none; }
.addnew {font-size: 12px;color: #FF0000;}
a.servername{height:470px;width: 527px;color:#E54202;cursor:hand;}
.current {border:#ff6600 1px solid;}
a:hover {height:470px;width: 527px;color:#E54202;cursor:hand;}
#nav LI A.noncurrent {/*border:#DC171E 3px solid;*/}
#nav UL {PADDING-BOTTOM: 0px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 0px}
#nav LI {DISPLAY: inline; padding-left:10px;}
#nav LI a:hover {border:#B6E000 1px solid;}
#nav li A:visited {border:#ff0000 1px solid;}
img{border:#CCCCCC 1px solid;padding:0 5px}
#tplPreview {
position: absolute;
top:0px;
left:0px;
background:#ffffff;
border:1px solid #333;
font-size:12px;
color:#4B4B4B;
padding:12px 15px 15px 15px;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="JsSave" />
        <asp:HiddenField runat="server" ID="hfnum" />
        <asp:HiddenField runat="server" ID="kfSave" />
        <asp:HiddenField runat="server" ID="kfnum" />
        <asp:HiddenField runat="server" ID="hdTemplate" />
        <table width="100%" border="0" cellspacing="1" cellpadding="3">
            <tr>
                <td colspan="4" style="font-weight: bold; font-size: 14px; background: url(style/images/topbg.gif) repeat-x;
                    color: teal; height: 28px">
                    վ������</td>
            </tr>
            <tr>
                <td width="10%" class="td2">
                    ��վ���ƣ�</td>
                <td width="90%" colspan="3" class="td1">
                    <asp:TextBox ID="txtName" runat="server" Width="227px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    ��վ������</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtDomain" runat="server" Width="227px"></asp:TextBox>
                    ֱ����������,��Ҫ��http://</td>
            </tr>
            <tr>
                <td class="td2">
                    ��ֵ���أ�</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtPayUrl" runat="server" Width="227px"></asp:TextBox>
                    ������������ ��Ҫ��http://</td>
            </tr>
            <tr>
                <td class="td2">
                    ��ϵ�绰��</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtPhone" runat="server" Width="227px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    ����QQ��</td>
                <td colspan="3" class="td1">
                    <%--<a href="#" onclick="addc();">��Ӽ���QQ</a> <a href="#" onclick="delc();">ɾ������QQ</a>--%>
                    <asp:TextBox ID="txtJSQQ" runat="server" Width="400px"></asp:TextBox>
                    ��ʽ:����,QQ����| ���λ����|
                    <div id="jsqqpanle" runat="server">
                    </div>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    �ͷ�QQ��</td>
                <td colspan="3" class="td1">
                    <%-- <a href="#" onclick="addk();">��ӿͻ�QQ</a> <a href="#" onclick="delk();">ɾ���ͻ�QQ</a>--%>
                    <asp:TextBox ID="txtKFQQ" runat="server" Width="400px"></asp:TextBox>
                    ��ʽ:����,QQ����| ���λ����|
                    <div id="kefu" runat="server">
                    </div>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    ��վ�����׺��</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtTitleSuffix" runat="server" Width="800px"></asp:TextBox></td>
            </tr> 
            <tr>
                <td class="td2">
                    ��վ�ؼ��֣�</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtWebSiteKey" runat="server" Width="800px"></asp:TextBox></td>
            </tr> 
            <tr>
                <td class="td2">
                    ��վ������</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtWebSitedescription" runat="server" Width="800px"></asp:TextBox></td>
            </tr> 
            <tr>
                <td class="td2">
                    ��վ��Ȩ��Ϣ��</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="txtFooter" runat="server" Width="400px"></asp:TextBox></td>
            </tr>            
            <tr>
                <td class="td2">
                    �̻�ע���Ƿ���ˣ�</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="ddlstatus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="td2">
                    �������˻��ĵ�¼��ʾ��Ϣ��</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtuserloginMsgForlock" runat="server" Width="400px" Text="����˻������������ܵ�¼������ϵ����Ա"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    δ������˻��ĵ�¼��ʾ��Ϣ��</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtUserloginMsgForUnCheck" runat="server" Width="400px" Text="�����˻�δ����ˣ����ܵ�¼������ϵ����Ա���"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    �˻����ʧ�ܵĵ�¼��ʾ��Ϣ��</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtUserloginMsgForCheckfail" runat="server" Width="400px" Text="�����˻�δ�����ͨ�������ܵ�¼������ϵ����Ա"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    �Ƿ���ע�᣺</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="ddlopen" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    ע���Ƿ���Ҫ�ʼ����</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_ActivationByEmail" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    �Ƿ�����ͨ���ʼ���¼��</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_isUserloginByEmail" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    ��Ҫ�ֻ���֤��</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_mobilval" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    һ���ֻ������Է�����Ϣ������</td>
                <td class="td1" colspan="3">
                    &nbsp;<asp:TextBox ID="txtMobilMaxSendTimes" runat="server" Width="227px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    �Ƿ����ͻ����ֶ������ѣ�</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="RadioButtonPhone" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
             <tr>
                <td class="td2">
                    �Ƿ����̼����ֶ�����֤��</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="RadioButtonshouji" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    �Ƿ����̼�����������֤��</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="RadioButtonemail" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
             <tr>
                <td class="td2">
                    ���ֶ�������-�����ֻ���</td>
                <td colspan="3" class="td1">
                    <asp:TextBox ID="TextPhone" runat="server" Width="227px"></asp:TextBox></td>
            </tr> 
            <tr>
                <td class="td2">
                    ����·վ�������</td>
                <td class="td1" colspan="3">
                    &nbsp;<asp:TextBox ID="txtRefCount" runat="server" Width="227px"></asp:TextBox>
                    ��ÿ�û���</td>
            </tr>
            <tr>
                <td class="td2">
                    ����·����״̬��</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_NoRef" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
            </tr>
            <tr>
                <td class="td2">
                    ���ÿ�����</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rblOpenDeduct" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="td2">
                    �Ƿ�����ƽ̨�����û��������ֲ�����</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_isopenCash" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    ƽ̨��ͣ�����û������ԭ��</td>
                <td class="td1" colspan="3">
                    &nbsp;<asp:TextBox ID="txtclosecashReason" runat="server" Width="227px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td2">
                    �û�Ĭ�Ͽ���������</td>
                <td class="td1" colspan="3">
                    &nbsp;<asp:TextBox ID="txtDefaultCPSDrate" runat="server" Width="227px"></asp:TextBox></td>
            </tr>
             <tr>
                <td class="td2">
                     �û�Ĭ�Ͻ���ģʽ��</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_settledmode" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="T+0" Value="0"></asp:ListItem>
                        <asp:ListItem Text="T+1" Value="1"></asp:ListItem>                        
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="td2">
                    ����������ͳ�ƣ�</td>
                <td class="td1" colspan="3">
                    <asp:TextBox ID="txtCode" runat="server" TextMode="MultiLine" Width="400px" class="input4"></asp:TextBox>
                    ֧��cnzz,51la,51yes����ͳ��.</td>
            </tr>
            <tr>
                <td class="td2">
                    ��¼���״�����־����������</td>
                <td class="td1" colspan="3">
                    <asp:RadioButtonList ID="rbl_debuglog" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
             <tr>
                <td width="10%" class="td2">
                    �����ӿ����ƣ�</td>
                <td width="90%" colspan="3" class="td1">
                    <asp:TextBox ID="txtapibankname" runat="server" Width="227px"></asp:TextBox></td>
            </tr>
             <tr>
                <td width="10%" class="td2">
                    �����ӿڰ汾��</td>
                <td width="90%" colspan="3" class="td1">
                    <asp:TextBox ID="txtapibankversion" runat="server" Width="227px"></asp:TextBox></td>
            </tr>
             <tr>
                <td width="10%" class="td2">
                    ����ӿ����ƣ�</td>
                <td width="90%" colspan="3" class="td1">
                    <asp:TextBox ID="txtapicardname" runat="server" Width="227px"></asp:TextBox></td>
            </tr>
             <tr>
                <td width="10%" class="td2">
                    ����ӿڰ汾��</td>
                <td width="90%" colspan="3" class="td1">
                    <asp:TextBox ID="txtapicardversion" runat="server" Width="227px"></asp:TextBox></td>
            </tr>
             <tr>
                <td width="10%" class="td2">
                    ͨ���޶����ã�</td>
                <td width="90%" colspan="3" class="td1">
                   ����: <asp:TextBox ID="txtbank" runat="server" Width="55px"></asp:TextBox>
                   ΢��: <asp:TextBox ID="txtweixin" runat="server" Width="55px"></asp:TextBox>
                   ֧����: <asp:TextBox ID="txtali" runat="server" Width="55px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="td1" colspan="3">
                    <span style="padding-left: 3px; height: 40px">
                        <asp:Button ID="btn_Update" runat="server" Text="ȷ�ϸ���" OnClick="btnUpdate_Click" OnClientClick="allQQ()" />
                    </span>
                </td>
            </tr>
        </table>

        <script type="text/javascript">
function $d(obj){
   return document.getElementById(obj);
}
   var num=$d("hfnum").value;
   function addc(){			
			$d("jsqqpanle").innerHTML=$d("jsqqpanle").innerHTML + "<BR/>QQ�ţ�<input type='text' name='qqnum"+num+"' id='qqnum"+num+"'>QQ���ƣ�<input type='text' name='qqname"+num+"' id='qqname"+num+"'>";  		 
			num=parseInt(num)+1;//parseInt(document.getElementById("hfnum").value);

		}
		
		function delc(){
			var str=$d("jsqqpanle").innerHTML;			
			str="QQ�ţ�<input type='text' id='qqnum0' name='qqnum0'>QQ���ƣ�<input type='text' name='qqname0' id='qqname0'>";
			$d("jsqqpanle").innerHTML=str;
			$d("JsSave").value="";
			num=1;
		}
		var kfnum=$d("kfnum").value;
		
		function allQQ(){
		   var myAllQQ="" ;
		   for(var i=0;i<num;i++){
		myAllQQ+=$d("qqname"+i).value+","+$d("qqnum"+i).value+"|";
		   }
		   $d("JsSave").value=myAllQQ;
		   var myAllkfQQ="" ;
		   for(var i=0;i<kfnum;i++){
		myAllkfQQ+=$d("kfqqname"+i).value+","+$d("kfqqnum"+i).value+"|";
		   }
		   $d("kfSave").value=myAllkfQQ;
		}
		 
		 function addk(){			
			$d("kefu").innerHTML=$d("kefu").innerHTML + "<BR/>QQ�ţ�<input type='text' name='kfqqnum"+kfnum+"' id='kfqqnum"+kfnum+"'>QQ���ƣ�<input type='text' name='kfqqname"+kfnum+"' id='kfqqname"+kfnum+"'>";  		 
			kfnum=parseInt(kfnum)+1;//parseInt(document.getElementById("hfnum").value);

		}
		
		function delk(){
			var str=$d("kefu").innerHTML;			
			str="QQ�ţ�<input type='text' id='kfqqnum0' name='kfqqnum0'>QQ���ƣ�<input type='text' name='kfqqname0' id='kfqqname0'>";
			$d("kefu").innerHTML=str;
			$d("kfSave").value="";
			kfnum=1;
		}
		
var   rbl   =   document.getElementsByName("RBLtemplate"); 
function hi(){
  
 for(var i=0;i<rbl.length;i++){
 if(rbl[i].checked){
$d("img"+rbl[i].value).className='current';
$d("hdTemplate").value=rbl[i].value;
 }else{
$d("img"+rbl[i].value).className='';
 }
 }
}
function bind(){
var myTemplate = $d("hdTemplate").value;

if(myTemplate!=null){

 for(var i=0;i<rbl.length;i++){

   if(rbl[i].value==myTemplate){
$d("img"+rbl[i].value).className='current';
   rbl[i].checked=true;
  
   }
 }
}
}
bind();
        </script>

    </form>
</body>
</html>
