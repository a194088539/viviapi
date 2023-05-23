<%@ page language="C#" autoeventwireup="True" Inherits="viviapi.WebUI.Managements.Default" Codebehind="Default.aspx.cs" %>
<html xmlns="">
<head runat="server">
<title>支付平台-后台管理系统</title>
<meta http-equiv="x-ua-compatible" content="ie=7" />
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<script type="text/javascript">
function changeDisplayMode(){
	if(document.getElementById("bottomframes").cols == "150,7,*"){
		document.getElementById("bottomframes").cols="0,7,*"; 
		document.getElementById("separator").contentWindow.document.getElementById('ImgArrow').src="style/images/separator2.gif"
	}else{
		document.getElementById("bottomframes").cols="150,7,*"
		document.getElementById("separator").contentWindow.document.getElementById('ImgArrow').src="style/images/separator1.gif"
	}
}
</script>

</head>
<frameset rows="58,*" cols="*" frameborder="no" border="0" framespacing="0">
<frame src="Top.aspx" name="topFrame" scrolling="No" noresize="noresize" id="topFrame">
<frameset id="bottomframes" framespacing="0" border="false" cols="150,7,*" frameborder="0" scrolling="yes">
<frame src="left.aspx" name="left" scrolling="auto" frameborder="0" marginheight="0" noresize="noresize" noresize />
<frame id="separator" name="separator" src="separator.html" noresize scrolling="no" />
<frame src="index.aspx" name="rightframe" id="rightframe"/>
</frameset>
</frameset>
<noframes><body><script type="text/javascript">
<!--
document.getElementById("dvbbsannounce").innerHTML = document.getElementById("dvbbsannounce_true").innerHTML;
//-->
</script>
</body>
</noframes>
</html>
