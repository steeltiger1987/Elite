<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="Elite.Logon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Logon</title>
<link rel="shortcut icon" href="favicon.ico">

<!-- CSS Global -->
<link href="/global/css/global.css"rel="stylesheet" type="text/css" />

<!-- CSS Local -->
<link href="/local/css/local.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" media="print" href="/local/css/print.css" />
</head>
<body>
<!-- Begin Header -->
<div id="wrapper"> <!-- Wrapper div creates sticky footer -->
	<div id="header">
		<table width="900" border="0" align="center" cellpadding="0" cellspacing="0">
			<tr>
			<td width="226">&nbsp;</a>
			</td>
			<td width="364" align="center">
				<div id="sid"></div>
			</td>
			<td width="211">
				<div id="ccast"></div>
				</td></tr>
		</table>
	</div>
<!-- End Header -->

<!-- Begin Nav & Search -->
	<div id="nav_bar">
		
		<div id="nav">
			<!-- Quick menu moved to local folder to support different color schemes -->
		</div>
		
		<div id="nav_search">			
            <a href="/help/index.htm?context=80" target="_blank" class="help">Help</a>
  		</div>
	</div>
<!-- End Nav & Search -->

<!-- Begin Content -->

<div style="width:600px;margin:0 auto;clear:both;height:300px;">
			
	<h1>Logon</h1>
		
	<div style="display:block;postion:relative;width:400px;margin:0 auto;">

    <form id="frmLogon" runat="server">
    <div>
	<table width="100%" border="0" cellspacing="0" cellpadding="5">
<%
    if (Request.QueryString["logon"] != null && Request.QueryString["logon"] != "") {
%>
		<tr>
		  <td colspan="2" class="AlertTagRed">Incorrect Logon Username/Password. Please see your Casino Manager or System Administrator to have your password reset.</a></td>
		</tr>
<%
    }
%>

<%
    if (Request.QueryString["EmailPassword"] != null && Request.QueryString["EmailPassword"] != "") {
%>
        <tr>
          <td colspan="2"><%="strDisplayMessage"%></td>
		</tr>
<%
    }
%>

    
		<tr>
			<td style="vertical-align:middle;"><h2>Username</h2></td>
			<td style="vertical-align:middle;"><input name="tbxUsername" type="text" class="formField" id="tbxUsername" style="font-family:Arial, Helvetica, sans-serif;font-size:18px; padding: 7px; height:26px;" value="<%=MM_valUsername%>" /></td>
		</tr>
		<tr>
			<td style="vertical-align:middle;"><h2>Password</h2></td>
			<td style="vertical-align:middle;"><input type="password" name="tbxPassword" id="tbxPassword" class="formField" style="font-family:Arial, Helvetica, sans-serif;font-size:18px; padding: 7px; height:26px;" /></td>
		</tr>
		<tr>
			<td colspan="2" align="center"><input type="checkbox" name="chkKeepLogin" id="chkKeepLogin" />
			Keep me logged into CAS on this computer until I log off (Not recommended for public or shared computers)</td>
		</tr>
		<tr>
			<td>&nbsp;</td>
			<td><asp:Button id="btnLogon" runat="server" Text="Logon" OnClick="btnLogon_Click" /></td>
		</tr>
	</table>
    </div>
	</form>

</div>
</div>
<!-- End Content -->

<!-- Begin Footer -->

	<div id="push"></div> <!-- Push for sticky footer -->

</div><!-- End Wrapper -->
	
	<div id="footer">
	
		<div id="footer_inside">
			<p><a href="Logon.aspx">Logon</a></p>
		</div>
</div>

<!-- End Footer -->
    
</body>
</html>
