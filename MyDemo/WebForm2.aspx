<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="MyDemo.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%= testc() %>
        <br />
        <%
            int i = 10;
            while (i-- > 0)
            {
        %>
            <p><%= i %></p>
        <%
            }
        %>
        <%= x %>
    </div>
    </form>
</body>
</html>
