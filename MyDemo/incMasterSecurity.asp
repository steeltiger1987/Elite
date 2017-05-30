<%

' *** Restrict Access To Page: Grant or deny access to this page
MM_authFailedURL="logon.asp"
MM_grantAccess=false
bolEmailNeedsUpdate=false
'Response.Write("MM_Username = " & Session("MM_Username") & "<br />")
If Session("MM_Username") <> "" Then
    MM_grantAccess = true
Else
'Response.Write("PositionID = " & Request.Cookies("PositionID") & "<br />")
	If 	Request.Cookies("UserID") <> "" Then
		Session("MM_Username") = Request.Cookies("MM_Username")
		Session("UserID") = Request.Cookies("UserID")
		Session("PositionID") = Request.Cookies("PositionID")
		Session("BillingCustomerID") = Request.Cookies("BillingCustomerID")
				'Response.Write("Cookie BillingCustomerID = " & Request.Cookies("BillingCustomerID") & "<br />")
		MM_grantAccess = true
	Else
'Response.Write("MM_UsernameLongTerm = " & Request.Cookies("MM_UsernameLongTerm") & "<br />")
		If 	Request.Cookies("UserIDLongTerm") = "" Then
			MM_grantAccess = false
		Else
			MM_grantAccess = true

			Set rstSecurityOne_cmd = Server.CreateObject ("ADODB.Command")
			rstSecurityOne_cmd.ActiveConnection = MM_OBA_STRING
		
			rstSecurityOne_cmd.CommandText = "SELECT UserID, UserName, PositionID, BillingCustomerID FROM Users WHERE (UserID = ?) AND Active = 1"
			rstSecurityOne_cmd.Parameters.Append rstSecurityOne_cmd.CreateParameter("param1", 5, 1, -1, Request.Cookies("UserIDLongTerm")) ' adVarWChar
			Set rstSecurityOne = rstSecurityOne_cmd.Execute
			If rstSecurityOne.EOF or rstSecurityOne.BOF Then
				rstSecurityOne.Close()
				Set rstSecurityOne = Nothing
				Response.Redirect("logoff.asp")

			Else
				'Response.Write("Security BillingCustomerID = " & rstSecurityOne.Fields.Item("BillingCustomerID").Value & "<br />")
				Session("PositionID") = rstSecurityOne.Fields.Item("PositionID").Value
				Session("MM_Username") = rstSecurityOne.Fields.Item("UserName").Value
				Session("UserID") = rstSecurityOne.Fields.Item("UserID").Value
				Response.Cookies("PositionID") = rstSecurityOne.Fields.Item("PositionID").Value
				Response.Cookies("UserID") = rstSecurityOne.Fields.Item("UserID").Value
				Response.Cookies("MM_Username") = rstSecurityOne.Fields.Item("UserName").Value
				
			End If
			Set rstSecurityOne = Nothing
		
		End If
	End If
End If
'Response.Write("MM_Username = " & Session("MM_Username") & "<br />")
'Response.Write("PositionID = " & Session("PositionID") & "<br />")
'Response.Write("MM_UsernameLongTerm = " & Request.Cookies("MM_UsernameLongTerm") & "<br />")
'Response.Write("MM_grantAccess = " & MM_grantAccess & "<br />")

strPageUrl = MID(Request.ServerVariables("URL"),2)
If MM_grantAccess Then

	
	
	
Dim intHelpContextID
Dim rstSecurity
Dim rstSecurity_cmd
Dim rstSecurity_numRows
	Set rstSecurity_cmd = Server.CreateObject ("ADODB.Command")
	
	rstSecurity_cmd.ActiveConnection = MM_OBA_STRING

	rstSecurity_cmd.CommandText = "SELECT Grants.GrantLevelID, Pages.PageTitle, Pages.HelpContextID, Elements.ElementName FROM ((Grants INNER JOIN Elements ON Grants.ElementID = Elements.ElementID) INNER JOIN PageElements ON Elements.ElementID = PageElements.ElementID) INNER JOIN Pages ON PageElements.PageID = Pages.PageID WHERE (Grants.PositionID= ?  AND Pages.PageAddress= ?  AND Pages.Active = 1) GROUP BY Grants.GrantLevelID, Pages.PageTitle, Pages.HelpContextID, Elements.ElementName ORDER BY Elements.ElementName" 
	rstSecurity_cmd.Prepared = true
	rstSecurity_cmd.Parameters.Append rstSecurity_cmd.CreateParameter("param1", 5, 1, -1, Session("PositionID")) ' adVarWChar
	rstSecurity_cmd.Parameters.Append rstSecurity_cmd.CreateParameter("param1", 202, 1, 255, strPageUrl) ' adVarWChar
	Set rstSecurity = rstSecurity_cmd.Execute

Dim strPageTitle
	If rstSecurity.EOF Then
		strPageTitle = "No Security Set for this page"
		intHelpContextID = 120
	Else
		strPageTitle = rstSecurity.Fields.Item("PageTitle").Value
		intHelpContextID = rstSecurity.Fields.Item("HelpContextID").Value
	End If

	Do While Not rstSecurity.EOF

		Select Case rstSecurity.Fields.Item("ElementName").Value
			Case "Support"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolDeveloperViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolDeveloperEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolDeveloperAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolDeveloperDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolDeveloperFullGranted = True
				End If
					
			Case "Security"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolSecurityViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolSecurityEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolSecurityAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolSecurityDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolSecurityFullGranted = True
				End If
		
			Case "Users"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolUsersViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolUsersEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolUsersAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolUsersDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolUsersFullGranted = True
				End If
					
			Case "Developer"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolDeveloperViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolDeveloperEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolDeveloperAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolDeveloperDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolDeveloperFullGranted = True
				End If
					
			Case "Projects"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolProjectsViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolProjectsEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolProjectsAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolProjectsDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolProjectsFullGranted = True
				End If
		
			Case "Vendors"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolVendorsViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolVendorsEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolVendorsAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolVendorsDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolVendorsFullGranted = True
				End If
		
			Case "Customers"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolCustomersViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolCustomersEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolCustomersAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolCustomersDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolCustomersFullGranted = True
				End If
		
			Case "Products"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolProductsViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolProductsEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolProductsAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolProductsDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolProductsFullGranted = True
				End If
		
			Case "Logs"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolLogsViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolLogsEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolLogsAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolLogsDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolLogsFullGranted = True
				End If
		
			Case "Categories"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolCategoriesViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolCategoriesEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolCategoriesAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolCategoriesDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolCategoriesFullGranted = True
				End If
		
			Case "Themes"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolThemesViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolThemesEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolThemesAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolThemesDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolThemesFullGranted = True
				End If
		
			Case "Samples"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolSamplesViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolSamplesEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolSamplesAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolSamplesDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolSamplesFullGranted = True
				End If
		
			Case "Factories"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolFactoriesViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolFactoriesEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolFactoriesAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolFactoriesDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolFactoriesFullGranted = True
				End If
		
			Case "Margins"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolMarginsViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolMarginsEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolMarginsAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolMarginsDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolMarginsFullGranted = True
				End If
		
			Case "Clients"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolClientsViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolClientsEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolClientsAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolClientsDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolClientsFullGranted = True
				End If
		
			Case "Ports"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolPortsViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolPortsEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolPortsAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolPortsDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolPortsFullGranted = True
				End If
		
			Case "Quotes"
				If rstSecurity.Fields.Item("GrantLevelID").Value >  0 Then
					bolQuotesViewGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  1 Then
					bolQuotesEditGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  2 Then
					bolQuotesAddGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  3 Then
					bolQuotesDeleteGranted = True
				End If
				If rstSecurity.Fields.Item("GrantLevelID").Value >  4 Then
					bolQuotesFullGranted = True
				End If		
		
		End Select

		rstSecurity.MoveNext
	Loop

	rstSecurity.Close()
	Set rstSecurity = Nothing
		
	dteAccessGranted = Now()

Else
  MM_qsChar = "?"
  If (InStr(1,MM_authFailedURL,"?") >= 1) Then MM_qsChar = "&"
  MM_referrer = Request.ServerVariables("URL")
  if (Len(Request.QueryString()) > 0) Then MM_referrer = MM_referrer & "?" & Request.QueryString()
  MM_authFailedURL = MM_authFailedURL & MM_qsChar & "accessdenied=" & Server.URLEncode(MM_referrer)
  Response.Redirect(MM_authFailedURL)
'Response.Write("MM_Username = " & Session("MM_Username") & "<br />" & "PositionID = |" & Request.Cookies("PositionID") & "|<br />" & "MM_UsernameLongTerm = " & Request.Cookies("MM_UsernameLongTerm") & "<br />")
End If
%>