<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuoteInput.aspx.cs" Inherits="MyDemo.QuoteInput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quote Input</title>
    <link rel="shortcut icon" href="favicon.ico" />

    <!-- CSS Global -->
    <link href="/global/css/global.css"rel="stylesheet" type="text/css" />

    <!-- CSS Local -->
    <link href="/local/css/local.css" rel="stylesheet" type="text/css" />

    <!-- Print Stylesheet -->
    <link rel="stylesheet" type="text/css" href="/global/css/print.css" media="print" />

    <!-- InstanceBeginEditable name="Head" -->
    <!-- jQuery UI -->
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.0/themes/base/jquery-ui.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.0/jquery-ui.min.js"></script>
    <!-- Datepicker -->
    <script type="text/javascript" charset="utf-16">
    $(function() {
	    $("#dteFromDate").datepicker();
    });
    </script>

</head>
<body>
    <!-- Begin Header -->
    <div id="wrapper"> <!-- Wrapper div creates sticky footer -->
	    <div id="header">
		    <table width="900" border="0" align="center" cellpadding="0" cellspacing="0">
			    <tr>
			    <td width="256">&nbsp;</td>
			    <td width="364" align="center">
				    <div id="sid"></div></td>
			    <td width="211">
                  <div align="right" id="ccast">
			      <p><a href="MyAccount.asp"><%= Session["MM_Username"] %> Profile</a> | <a href="logoff.asp">Log Out</a></p>
                  </div></td>
			    </tr>
		    </table>
	    </div>
        <!-- End Header -->

        <!-- Begin Nav & Search -->
	    <div id="nav_bar">
		
		    <div id="nav">
			    <!-- Quick menu moved to local folder to support different color schemes -->

<!--%%%%%%%%%%%% QuickMenu Styles [Keep in head for full validation!] %%%%%%%%%%%-->
<style type="text/css">


/*!!!!!!!!!!! QuickMenu Core CSS [Do Not Modify!] !!!!!!!!!!!!!*/
/*[START-QCC]*/.qmmc .qmdivider{display:block;font-size:1px;border-width:0px;border-style:solid;position:relative;z-index:1;}.qmmc .qmdividery{float:left;width:0px;}.qmmc .qmtitle{display:block;cursor:default;white-space:nowrap;position:relative;z-index:1;}.qmclear {font-size:1px;height:0px;width:0px;clear:left;line-height:0px;display:block;float:none !important;}.qmmc {position:relative;zoom:1;z-index:10;}.qmmc a, .qmmc li {float:left;display:block;white-space:nowrap;position:relative;z-index:1;}.qmmc div a, .qmmc ul a, .qmmc ul li {float:none;}.qmsh div a {float:left;}.qmmc div{visibility:hidden;position:absolute;}.qmmc .qmcbox{cursor:default;display:inline-block;position:relative;z-index:1;}.qmmc .qmcbox a{display:inline;}.qmmc .qmcbox div{float:none;position:static;visibility:inherit;left:auto;}.qmmc li {z-index:auto;}.qmmc ul {left:-10000px;position:absolute;z-index:10;}.qmmc, .qmmc ul {list-style:none;padding:0px;margin:0px;}.qmmc li a {float:none;}.qmmc li:hover>ul{left:auto;}/*[END-QCC]*//*[START-QCC0]*/#qm0 ul {top:100%;}#qm0 ul li:hover>ul{top:0px;left:100%;}/*[END-QCC0]*/


/*!!!!!!!!!!! QuickMenu Styles [Please Modify!] !!!!!!!!!!!*/


	/*"""""""" (MAIN) Container""""""""*/	
	#qm0	
	{	
		width:600px;
		background-color:transparent;
		z-index: 60;
	}


	/*"""""""" (MAIN) Items""""""""*/	
	#qm0 a	
	{
	background-color:#091436;
	border:none;
	font-family:Arial;
	font-size:13px;
	margin:2px -1px 0 0;
	padding:13px 52px 7px 8px;
	text-align:left;
	text-decoration:none;
	color: #fefefe;
	height: 22px;
	width: 60px;
	border-left: 1px solid #042106;
	}


	/*"""""""" (MAIN) Hover State""""""""*/	
	#qm0 a:hover	
	{
	background-color:#fefefe;
	text-decoration:none;
	color:#091436;
	font-size:16px;
	cursor:default;
	font-weight: bold;
	background-image:url(../local/images/menu-bg.gif);
	border-left: 1px solid #042106;
	}


	/*"""""""" (MAIN) Hover State - (duplicated for pure CSS)""""""""*/	
	#qm0 li:hover>a	
	{
	background-color:#fefefe;
	text-decoration:none;
	color:#091436;
	font-size:16px;
	cursor:default;
	font-weight: bold;
	background-image:url(../local/images/menu-bg.gif);
	border: 1px solid #042106;
	border-bottom: none;
	}


	/*"""""""" (MAIN) Active State""""""""*/	
	body #qm0 .qmactive, body #qm0 .qmactive:hover	
	{
	background-color:#fefefe;
	text-decoration:none;
	color:#091436;
	font-size:16px;
	cursor:default;
	font-weight: bold;
	background-image:url(../local/images/menu-bg.gif);
	}


	/*"""""""" (SUB) Container""""""""*/	
	#qm0 div, #qm0 ul	
	{
	padding:5px;
	margin:0 0px 0px 0px;
	background-color:#fefefe;
	background-image:url(../local/images/menu-bg.gif);
	background-position: 0 -45px;
	color:#091436;
	border-top:none;
	border-right: 1px solid #042106;
	border-bottom: 1px solid #042106;
	border-left: 1px solid #042106;
	z-index: 100;
	}


	/*"""""""" (SUB) Items""""""""*/	
	#qm0 div a, #qm0 ul a	
	{
	padding: 0px 40px 0px 5px;
	background-color:transparent;
	cursor:pointer;
	color:#091436;
	font-size: 11px;
	border-bottom: 1px dotted #042106;
	width: 210px;
	height:19px;
	margin:0 0;
	padding:5px 40px 0 5px;
	border-left: none !important;
	z-index: 110;
}


	/*"""""""" (SUB) Hover State""""""""*/	
	#qm0 div a:hover	
	{
	text-decoration:none;
	cursor:pointer;
	color:#091436;
	border-bottom: 1px dotted #042106;
	font-size: 11px;
	width: 210px;
	height:19px;
	margin:0 0;
	padding:5px 40px 0 5px;
	background: none;
	}

	/*"""""""" (SUB) Hover State - (duplicated for pure CSS)""""""""*/	
	#qm0 ul li:hover>a	
	{
	text-decoration:none;
	cursor:pointer;
	color:#091436;
	border-bottom: 1px dotted #042106;
	font-size: 11px;
	height:19px;
	margin:0 0;
	padding:5px 40px 0 5px;
	background: none;
	}


	/*"""""""" (SUB) Active State""""""""*/	
	body #qm0 div .qmactive, body #qm0 div .qmactive:hover	
	{	
		background-color:transparent;
		cursor:pointer;
		font-size: 11px;
	}


	/*"""""""" Individual Titles""""""""*/	
	#qm0 .qmtitle	
	{	
		cursor:default;
		padding:3px 0px 3px 4px;
		color:#444444;
		font-family:arial;
		font-size:11px;
		font-weight:bold;
	}


	/*"""""""" Individual Horizontal Dividers""""""""*/	
	#qm0 .qmdividerx	
	{	
		border-top-width:1px;
		margin:4px 0px 4px 0px;
		border-color:#BFBFBF;
	}


	/*"""""""" Individual Vertical Dividers""""""""*/	
/*	#qm0 .qmdividery	
	{	
		border-left-width:1px;
		height:15px;
		margin:4px 2px 0px 2px;
		border-color:#BFBFBF;
	}

*/
	/*"""""""" Custom Rule""""""""*/	
/*	ul#qm0 ul li:hover > a.qmparent	
	{	
		background-image:url(file:///C:/Program%20Files/OpenCube/Visual%20CSS%20QuickMenu/chrome/content/qmimages/cssalt1_arrow_right_hover.gif);
	}

*/
	/*"""""""" Custom Rule""""""""*/	
/*	ul#qm0 ul .qmparent	
	{	
		background-image:url(file:///C:/Program%20Files/OpenCube/Visual%20CSS%20QuickMenu/chrome/content/qmimages/cssalt1_arrow_right.gif);
	}
*/

	/*"""""""" Custom Rule""""""""*/	

/*	ul#qm0 li:hover > a.qmparent	
	{	
		background-image:url(file:///C:/Program%20Files/OpenCube/Visual%20CSS%20QuickMenu/chrome/content/qmimages/cssalt1_arrow_down_hover.gif);
		text-decoration:underline;
	}
*/

	/*"""""""" Custom Rule""""""""*/	
/*	ul#qm0 .qmparent	
	{	
		background-image:url(file:///C:/Program%20Files/OpenCube/Visual%20CSS%20QuickMenu/chrome/content/qmimages/cssalt1_arrow_down.gif);
		background-repeat:no-repeat;
		background-position:97% 50%;
	}
*/	
	/*[END-QS0]*/


</style>

<!-- Add-On Core Code (Remove when not using any add-on's) -->
<!--[START-QZ]--><style type="text/css">.qmfv{visibility:visible !important;}.qmfh{visibility:hidden !important;}</style><script type="text/javascript">if (!window.qmad){qmad=new Object();qmad.binit="";qmad.bvis="";qmad.bhide="";}</script><!--[END-QZ]-->

	<!-- Add-On Settings -->
<script type="text/JavaScript">

		/*******  Menu 0 Add-On Settings *******/
		var a = qmad.qm0 = new Object();

		// Item Bullets (CSS - Imageless) Add On
		a.ibcss_apply_to = "parent";
		a.ibcss_main_type = "arrow-head-v";
		a.ibcss_main_direction = "down";
		a.ibcss_main_size = 5;
		a.ibcss_main_bg_color = "transparent";
		a.ibcss_main_border_color = "#444444";
		a.ibcss_main_border_color_hover = "#dd3300";
		a.ibcss_main_position_x = -19;
		a.ibcss_main_position_y = -4;
		a.ibcss_main_align_x = "right";
		a.ibcss_main_align_y = "middle";
		a.ibcss_sub_type = "arrow-head-v";
		a.ibcss_sub_direction = "right";
		a.ibcss_sub_size = 5;
		a.ibcss_sub_bg_color = "transparent";
		a.ibcss_sub_border_color = "#444444";
		a.ibcss_sub_border_color_hover = "#dd3300";
		a.ibcss_sub_position_x = -16;
		a.ibcss_sub_align_x = "right";
		a.ibcss_sub_align_y = "middle";

		// Drop Shadow Add On
		a.shadow_offset = 3;
		a.shadow_color = "#000000";
		a.shadow_opacity = ".3";

		/*[END-QA0]*/

	</script>

<!-- Core QuickMenu Code -->
<script type="text/javascript">/* <![CDATA[ */qmv_iisv=1;qmv7=true;var qm_si,qm_lo,qm_tt,qm_ts,qm_la,qm_ic,qm_ff,qm_sks;var qm_li=new Object();var qm_ib='';var qp="parentNode";var qc="className";var qm_t=navigator.userAgent;var qm_o=qm_t.indexOf("Opera")+1;var qm_s=qm_t.indexOf("afari")+1;var qm_s2=qm_s&&qm_t.indexOf("ersion/2")+1;var qm_s3=qm_s&&qm_t.indexOf("ersion/3")+1;var qm_n=qm_t.indexOf("Netscape")+1;var qm_v=parseFloat(navigator.vendorSub);var qm_ie8=qm_t.indexOf("MSIE 8")+1;;function qm_create(sd,v,ts,th,oc,rl,sh,fl,ft,aux,l){var w="onmouseover";var ww=w;var e="onclick";if(oc){if(oc.indexOf("all")+1||(oc=="lev2"&&l>=2)){w=e;ts=0;}if(oc.indexOf("all")+1||oc=="main"){ww=e;th=0;}}if(!l){l=1;sd=document.getElementById("qm"+sd);if(window.qm_pure)sd=qm_pure(sd);sd[w]=function(e){try{qm_kille(e)}catch(e){}};if(oc!="all-always-open")document[ww]=qm_bo;if(oc=="main"){qm_ib+=sd.id;sd[e]=function(event){qm_ic=true;qm_oo(new Object(),qm_la,1);qm_kille(event)};}sd.style.zoom=1;if(sh)x2("qmsh",sd,1);if(!v)sd.ch=1;}else  if(sh)sd.ch=1;if(oc)sd.oc=oc;if(sh)sd.sh=1;if(fl)sd.fl=1;if(ft)sd.ft=1;if(rl)sd.rl=1;sd.th=th;sd.style.zIndex=l+""+1;var lsp;var sp=sd.childNodes;for(var i=0;i<sp.length;i++){var b=sp[i];if(b.tagName=="A"){lsp=b;b[w]=qm_oo;if(w==e)b.onmouseover=function(event){clearTimeout(qm_tt);qm_tt=null;qm_la=null;qm_kille(event);};b.qmts=ts;if(l==1&&v){b.style.styleFloat="none";b.style.cssFloat="none";}}else  if(b.tagName=="DIV"){if(window.showHelp&&!window.XMLHttpRequest)sp[i].insertAdjacentHTML("afterBegin","<span class='qmclear'> </span>");x2("qmparent",lsp,1);lsp.cdiv=b;b.idiv=lsp;if(qm_n&&qm_v<8&&!b.style.width)b.style.width=b.offsetWidth+"px";new qm_create(b,null,ts,th,oc,rl,sh,fl,ft,aux,l+1);}}if(l==1&&window.qmad&&qmad.binit){ eval(qmad.binit);}};function qm_bo(e){e=e||event;if(e.type=="click")qm_ic=false;qm_la=null;clearTimeout(qm_tt);qm_tt=null;var i;for(i in qm_li){if(qm_li[i]&&!((qm_ib.indexOf(i)+1)&&e.type=="mouseover"))qm_tt=setTimeout("x0('"+i+"')",qm_li[i].th);}};function qm_co(t){var f;for(f in qm_li){if(f!=t&&qm_li[f])x0(f);}};function x0(id){var i;var a;var a;if((a=qm_li[id])&&qm_li[id].oc!="all-always-open"){do{qm_uo(a);}while((a=a[qp])&&!qm_a(a));qm_li[id]=null;}};function qm_a(a){if(a[qc].indexOf("qmmc")+1)return 1;};function qm_uo(a,go){if(!go&&a.qmtree)return;if(window.qmad&&qmad.bhide)eval(qmad.bhide);a.style.visibility="";x2("qmactive",a.idiv);};function qm_oo(e,o,nt){try{if(!o)o=this;if(qm_la==o&&!nt)return;if(window.qmv_a&&!nt)qmv_a(o);if(window.qmwait){qm_kille(e);return;}clearTimeout(qm_tt);qm_tt=null;qm_la=o;if(!nt&&o.qmts){qm_si=o;qm_tt=setTimeout("qm_oo(new Object(),qm_si,1)",o.qmts);return;}var a=o;if(a[qp].isrun){qm_kille(e);return;}while((a=a[qp])&&!qm_a(a)){}var d=a.id;a=o;qm_co(d);if(qm_ib.indexOf(d)+1&&!qm_ic)return;var go=true;while((a=a[qp])&&!qm_a(a)){if(a==qm_li[d])go=false;}if(qm_li[d]&&go){a=o;if((!a.cdiv)||(a.cdiv&&a.cdiv!=qm_li[d]))qm_uo(qm_li[d]);a=qm_li[d];while((a=a[qp])&&!qm_a(a)){if(a!=o[qp]&&a!=o.cdiv)qm_uo(a);else break;}}var b=o;var c=o.cdiv;if(b.cdiv){var aw=b.offsetWidth;var ah=b.offsetHeight;var ax=b.offsetLeft;var ay=b.offsetTop;if(c[qp].ch){aw=0;if(c.fl)ax=0;}else {if(c.ft)ay=0;if(c.rl){ax=ax-c.offsetWidth;aw=0;}ah=0;}if(qm_o){ax-=b[qp].clientLeft;ay-=b[qp].clientTop;}if((qm_s2&&!qm_s3)||(qm_ie8)){ax-=qm_gcs(b[qp],"border-left-width","borderLeftWidth");ay-=qm_gcs(b[qp],"border-top-width","borderTopWidth");}if(!c.ismove){c.style.left=(ax+aw)+"px";c.style.top=(ay+ah)+"px";}x2("qmactive",o,1);if(window.qmad&&qmad.bvis)eval(qmad.bvis);c.style.visibility="inherit";qm_li[d]=c;}else  if(!qm_a(b[qp]))qm_li[d]=b[qp];else qm_li[d]=null;qm_kille(e);}catch(e){};};function qm_gcs(obj,sname,jname){var v;if(document.defaultView&&document.defaultView.getComputedStyle)v=document.defaultView.getComputedStyle(obj,null).getPropertyValue(sname);else  if(obj.currentStyle)v=obj.currentStyle[jname];if(v&&!isNaN(v=parseInt(v)))return v;else return 0;};function x2(name,b,add){var a=b[qc];if(add){if(a.indexOf(name)==-1)b[qc]+=(a?' ':'')+name;}else {b[qc]=a.replace(" "+name,"");b[qc]=b[qc].replace(name,"");}};function qm_kille(e){if(!e)e=event;e.cancelBubble=true;if(e.stopPropagation&&!(qm_s&&e.type=="click"))e.stopPropagation();}if(window.name=="qm_copen"&&!window.qmv){document.write('<scr'+'ipt type="text/javascript" src="file:///C:/Program Files/OpenCube/Visual CSS QuickMenu/chrome/content/qm_visual.js"></scr'+'ipt>')};function qa(a,b){return String.fromCharCode(a.charCodeAt(0)-(b-(parseInt(b/2)*2)));};;function qm_pure(sd){if(sd.tagName=="UL"){var nd=document.createElement("DIV");nd.qmpure=1;var c;if(c=sd.style.cssText)nd.style.cssText=c;qm_convert(sd,nd);var csp=document.createElement("SPAN");csp.className="qmclear";csp.innerHTML=" ";nd.appendChild(csp);sd=sd[qp].replaceChild(nd,sd);sd=nd;}return sd;};function qm_convert(a,bm,l){if(!l)bm[qc]=a[qc];bm.id=a.id;var ch=a.childNodes;for(var i=0;i<ch.length;i++){if(ch[i].tagName=="LI"){var sh=ch[i].childNodes;for(var j=0;j<sh.length;j++){if(sh[j]&&(sh[j].tagName=="A"||sh[j].tagName=="SPAN"))bm.appendChild(ch[i].removeChild(sh[j]));if(sh[j]&&sh[j].tagName=="UL"){var na=document.createElement("DIV");var c;if(c=sh[j].style.cssText)na.style.cssText=c;if(c=sh[j].className)na.className=c;na=bm.appendChild(na);new qm_convert(sh[j],na,1)}}}}}/* ]]> */</script>

<!-- Add-On Code: Drop Shadow -->
<script type="text/javascript">/* <![CDATA[ */qmad.shadow=new Object();if(qmad.bvis.indexOf("qm_drop_shadow(b.cdiv);")==-1)qmad.bvis+="qm_drop_shadow(b.cdiv);";if(qmad.bhide.indexOf("qm_drop_shadow(a,1);")==-1)qmad.bhide+="qm_drop_shadow(a,1);";;function qm_drop_shadow(a,hide,force){var z;if(!hide&&((z=window.qmv)&&(z=z.addons)&&(z=z.drop_shadow)&&!z["on"+qm_index(a)]))return;if((!hide&&!a.hasshadow)||force){var ss;if(!a.settingsid){var v=a;while((v=v.parentNode)){if(v.className.indexOf("qmmc")+1){a.settingsid=v.id;break;}}}ss=qmad[a.settingsid];if(!ss)return;if(isNaN(ss.shadow_offset))return;qmad.shadow.offset=ss.shadow_offset;var f=document.createElement("SPAN");x2("qmshadow",f,1);var fs=f.style;fs.position="absolute";fs.display="block";fs.backgroundColor="#999999";fs.visibility="inherit";var sh;if((sh=ss.shadow_opacity)){f.style.opacity=sh;f.style.filter="alpha(opacity="+(sh*100)+")";}if((sh=ss.shadow_color))f.style.backgroundColor=sh;f=a.parentNode.appendChild(f);a.hasshadow=f;}var c=qmad.shadow.offset;var b=a.hasshadow;if(b){if(hide)b.style.visibility="hidden";else {b.style.width=a.offsetWidth+"px";b.style.height=a.offsetHeight+"px";var ft=0;var fl=0;if(qm_o){ft=b[qp].clientTop;fl=b[qp].clientLeft;}if(qm_s2){ft=qm_gcs(b[qp],"border-top-width","borderTopWidth");fl=qm_gcs(b[qp],"border-left-width","borderLeftWidth");}b.style.top=a.offsetTop+c-ft+"px";b.style.left=a.offsetLeft+c-fl+"px";b.style.visibility="inherit";}}}/* ]]> */</script>


<!-- QuickMenu Structure [Menu 0] -->

<ul id="qm0" class="qmmc">
    <asp:PlaceHolder id="MenuPlaceHolder" runat="server"></asp:PlaceHolder>
<li class="qmclear"> </li></ul>

<!-- Create Menu Settings: (Menu ID, Is Vertical, Show Timer, Hide Timer, On Click (options: 'all' * 'all-always-open' * 'main' * 'lev2'), Right to Left, Horizontal Subs, Flush Left, Flush Top) -->
<script type="text/javascript">qm_create(0,false,0,500,false,false,false,false,false);</script><!--[END-QM0]-->

			    
		    </div>
		
		    <div id="nav_search">			
                <a href="/help/index.htm?context=<%= "intHelpContextID" %>" target="_blank" class="help">Help</a>
  		    </div>
	    </div>
        <!-- End Nav & Search -->

        <!-- Begin Content -->
	
	    <div id="content">
            <!-- #BeginEditable "content" -->
	        <h1>Quote Input</h1>	
            <form id="frmEdit" runat="server">
        <table border="0" cellspacing="0" cellpadding="0" class="box">
            <tr>
                <td>&nbsp;</td>
                <td colspan="2"><asp:Label ID="ErrorMsg" runat="server" ></asp:Label></td>
                <td>&nbsp;</td>
            </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Client</strong></td>
          <td>
              <asp:DropDownList ID="cbxClientID" runat="server"></asp:DropDownList>
          </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Project</strong></td>
          <td><asp:TextBox ID="tbxProject" runat="server" Columns="15"></asp:TextBox></td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Quote Number</strong></td>
          <td><asp:TextBox ID="tbxQuoteNum" runat="server" Columns="15"></asp:TextBox></td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Valid</strong></td>
          <td><asp:TextBox ID="tbxValid" runat="server" Columns="15"></asp:TextBox> Days</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Item Number</strong></td>
          <td><asp:TextBox ID="tbxItemNum" runat="server" Columns="15"></asp:TextBox></td>
          <td>&nbsp;</td>
        </tr>
        <tr>
            <td width="10">&nbsp;</td>
            <td align="right"><strong>Product Name</strong></td>
          <td><asp:TextBox ID="tbxName" runat="server" Columns="35"></asp:TextBox></td>
		<td>&nbsp;</td>
		</tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Description</strong></td>
          <td><asp:TextBox ID="tbxDescription" runat="server" TextMode="MultiLine" Rows="3" Columns="45"></asp:TextBox></td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Production Times</strong></td>
          <td><asp:TextBox ID="tbxProdTimeLo" runat="server" Columns="5"></asp:TextBox>
-
  <asp:TextBox ID="tbxProdTimeHi" runat="server" Columns="5"></asp:TextBox></td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Packaging</strong></td>
          <td><asp:TextBox ID="tbxPackaging" runat="server"></asp:TextBox></td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Carton Length - Width - Height</strong></td>
          <td><asp:TextBox ID="tbxCartonL" runat="server" Columns="5"></asp:TextBox> 
            - 
            <asp:TextBox ID="tbxCartonW" runat="server" Columns="5"></asp:TextBox> 
            - 
            <asp:TextBox ID="tbxCartonH" runat="server" Columns="5"></asp:TextBox> 
            in cm</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Carton Weight</strong></td>
          <td><asp:TextBox ID="tbxWeightPerCarton" runat="server" Columns="6"></asp:TextBox></td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Units Per Carton</strong></td>
          <td><asp:TextBox ID="tbxUnitsPerCarton" runat="server" Columns="6"></asp:TextBox></td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Exchange Rate (RMB/USD)</strong></td>
          <td><asp:TextBox ID="tbxExchangeRate" runat="server" Columns="6"></asp:TextBox></td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Margin</strong></td>
          <td><asp:TextBox ID="tbxMargin" runat="server" Columns="6"></asp:TextBox></td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>HTS Code / Duty Rate</strong></td>
          <td><asp:TextBox ID="tbxDutyRate" runat="server" Columns="6"></asp:TextBox></td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>FOB Port</strong></td>
          <td>
              <asp:DropDownList ID="cbxFOBPort" runat="server"></asp:DropDownList>
          </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Setup</strong></td>
          <td>
              <asp:TextBox ID="tbxSetup" runat="server"></asp:TextBox>
          </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Mold fee</strong></td>
          <td>
              <asp:TextBox ID="tbxMoldFee" runat="server"></asp:TextBox>
          </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Testing</strong></td>
          <td>
              <asp:TextBox ID="tbxTesting" runat="server"></asp:TextBox>
          </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Pre-Pro</strong></td>
          <td>
              <asp:TextBox ID="tbxPrePro" runat="server"></asp:TextBox>
          </td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><strong>Pre-Pro Time</strong></td>
          <td>
              <asp:TextBox ID="tbxPreProTime" runat="server"></asp:TextBox>
          </td>
          <td>&nbsp;</td>
        </tr>
       <tr>
            <td>&nbsp;</td>
            <td colspan="2">
              <table border="0" cellspacing="0" cellpadding="0" class="box box5">
                <tr>
                  <th colspan="3"><h2>Costs</h2></th>
                </tr>
                <tr>
                  <th align="left"><h4>RMB Price</h4></th>
                  <th align="left"><h4>QTY</h4></th>
                  <th align="left"><h4>Lead Time</h4></th>
                </tr>
                <asp:PlaceHolder id="CostDataPlaceHolder" runat="server"></asp:PlaceHolder>
              </table>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td width="10">&nbsp;</td>
            <td>&nbsp;</td>
            <td><asp:Button id="btnEdit" runat="server" Text="Generate Quote" OnClick="btnEdit_Click" /></td>
            <td>&nbsp;</td>
        </tr>
      <tr>
        <td width="10">&nbsp;</td>
            <td>
                <asp:Button id="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
      </tr>
                </table>
                <asp:HiddenField id="htbxReturnPath" runat="server" />
                <asp:HiddenField id="MM_mode" runat="server" />
                <asp:HiddenField id="MM_recordId" runat="server" />
            </form>
            <!-- #EndEditable -->

        </div>
        <!-- End Content -->

        <!-- Begin Footer -->
        <div id="push"></div> <!-- Push for sticky footer -->

    </div><!-- End Wrapper -->
<!--#includedd file="Includes/incFooter.aspx" -->
	<div id="footer">
	
		<div id="footer_inside">
		  <p><a href="Logoff.asp">Log Out</a> | Produced at <%= DateTime.Now %></p>
		</div>
	</div>
        <script type="text/javascript">
    </script>
</body>
</html>
