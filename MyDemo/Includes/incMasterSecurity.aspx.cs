using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Elite.Includes
{
    public partial class incMasterSecurity : System.Web.UI.Page
    {
        public string MM_authFailedURL;
        string connectionString = @"Server=(localdb)\MSSQLLocalDB; Integrated Security=true; Initial Catalog=Elite; Trusted_Connection=yes; connection timeout=150;";
        //string connectionString = @"Server=ccastweb.com; Database=Elite; User ID=AChen; Password=Andrew1;";
        protected void Page_Load(object sender, EventArgs e)
        {
            // *** Restrict Access To Page: Grant or deny access to this page
            MM_authFailedURL = "logon.asp";
            bool MM_grantAccess = false;
            bool bolEmailNeedsUpdate = false;
            //'Response.Write("MM_Username = " & Session("MM_Username") & "<br />")
            if (Session["MM_Username"].ToString() != "")
                MM_grantAccess = true;
            else
            {
	            //'Response.Write("PositionID = " & Request.Cookies("PositionID") & "<br />")

	            if (Request.Cookies["UserID"].ToString() != "")
	            {
	                Session["MM_Username"] = Request.Cookies["MM_Username"];
	                Session["UserID"] = Request.Cookies["UserID"];
	                Session["PositionID"] = Request.Cookies["PositionID"];
	                Session["BillingCustomerID"] = Request.Cookies["BillingCustomerID"];
	                //'Response.Write("Cookie BillingCustomerID = " & Request.Cookies("BillingCustomerID") & "<br />")

	                MM_grantAccess = true;
	            }
	            else
	            {
		            //'Response.Write("MM_UsernameLongTerm = " & Request.Cookies("MM_UsernameLongTerm") & "<br />")

		            if (this.Request.Cookies["UserIDLongTerm"].ToString() == "")
		                MM_grantAccess = false;
		            else
		            {
			            MM_grantAccess = true;


			            using (SqlConnection connection = new SqlConnection(connectionString))
			            {
			                try
			                {
			                    connection.ConnectionString = connectionString;
			                    SqlCommand cmd = new SqlCommand("SELECT UserID, UserName, PositionID, BillingCustomerID FROM Users WHERE (UserID = @UserID) AND Active = 1", connection);
			                    cmd.Parameters.AddWithValue("@UserID", Int64.Parse(Request.Cookies["UserIDLongTerm"].ToString()));
			                    SqlDataAdapter da = new SqlDataAdapter(cmd);
			                    DataSet ds = new DataSet();
			                    connection.Open();
			                    da.Fill(ds);
			                    connection.Close();

			                    if (!object.Equals(ds.Tables[0], null))
			                    {
			                        if (ds.Tables[0].Rows.Count > 0)
			                        {
			                            //'Response.Write("Security BillingCustomerID = " & rstSecurityOne.Fields.Item("BillingCustomerID").Value & "<br />")
			                            Session["PositionID"] = ds.Tables[0].Rows[0]["PositionID"];
			                            Session["MM_Username"] = ds.Tables[0].Rows[0]["UserName"];
			                            Session["UserID"] = ds.Tables[0].Rows[0]["UserID"];
			                            Response.SetCookie(new HttpCookie("PositionID", ds.Tables[0].Rows[0]["PositionID"].ToString()));
			                            Response.SetCookie(new HttpCookie("UserID", ds.Tables[0].Rows[0]["UserID"].ToString()));
			                            Response.SetCookie(new HttpCookie("MM_Username", ds.Tables[0].Rows[0]["UserName"].ToString()));
			                        }
			                        else
			                        {
			                        }
			                    }
			                    else
			                    {
			                    }
			                }
			                catch (Exception ex)
			                {
			                    Response.Write(ex.ToString());
			                }
			            }
		            }
	            }
            }

            //'Response.Write("MM_Username = " & Session("MM_Username") & "<br />")
            //'Response.Write("PositionID = " & Session("PositionID") & "<br />")
            //'Response.Write("MM_UsernameLongTerm = " & Request.Cookies("MM_UsernameLongTerm") & "<br />")
            //'Response.Write("MM_grantAccess = " & MM_grantAccess & "<br />")

            int level;
            bool bolDeveloperViewGranted;
            bool bolDeveloperEditGranted;
            bool bolDeveloperAddGranted;
            bool bolDeveloperDeleteGranted;
            bool bolDeveloperFullGranted;

            bool bolSecurityViewGranted;
            bool bolSecurityEditGranted;
            bool bolSecurityAddGranted;
            bool bolSecurityDeleteGranted;
            bool bolSecurityFullGranted;

            bool bolProductsViewGranted;
            bool bolProductsEditGranted;
            bool bolProductsAddGranted;
            bool bolProductsDeleteGranted;
            bool bolProductsFullGranted;

            bool bolQuotesViewGranted;
            bool bolQuotesEditGranted;
            bool bolQuotesAddGranted;
            bool bolQuotesDeleteGranted;
            bool bolQuotesFullGranted;

            string strPageUrl = Request.ServerVariables["URL"].Substring(2);
            if (MM_grantAccess)
            {
                string strPageTitle;
                int intHelpContextID;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.ConnectionString = connectionString;
                        SqlCommand cmd = new SqlCommand("SELECT Grants.GrantLevelID, Pages.PageTitle, Pages.HelpContextID, Elements.ElementName FROM ((Grants INNER JOIN Elements ON Grants.ElementID = Elements.ElementID) INNER JOIN PageElements ON Elements.ElementID = PageElements.ElementID) INNER JOIN Pages ON PageElements.PageID = Pages.PageID WHERE (Grants.PositionID= @PositionID  AND Pages.PageAddress= @PageUrl  AND Pages.Active = 1) GROUP BY Grants.GrantLevelID, Pages.PageTitle, Pages.HelpContextID, Elements.ElementName ORDER BY Elements.ElementName", connection);
                        cmd.Parameters.AddWithValue("@PositionID", Int64.Parse(Session["PositionID"].ToString()));
                        cmd.Parameters.AddWithValue("@PageUrl", strPageUrl);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        connection.Open();
                        da.Fill(ds);
                        connection.Close();

                        if (!object.Equals(ds.Tables[0], null))
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strPageTitle = ds.Tables[0].Rows[0]["PageTitle"].ToString();
                                intHelpContextID = (int)(ds.Tables[0].Rows[0]["HelpContextID"]);

                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    level = (int)(ds.Tables[0].Rows[i]["GrantLevelID"]);
                                    switch (ds.Tables[0].Rows[0]["ElementName"].ToString())
                                    {
                                        case "Support":
                                            if (level > 0)
                                                bolDeveloperViewGranted = true;

                                            if (level > 1)
                                                bolDeveloperEditGranted = true;

                                            if (level > 2)
                                                bolDeveloperAddGranted = true;

                                            if (level > 3)
                                                bolDeveloperDeleteGranted = true;

                                            if (level > 4)
                                                bolDeveloperFullGranted = true;
                                            break;

                                        case "Security":
                                            if (level > 0)
                                                bolSecurityViewGranted = true;

                                            if (level > 1)
                                                bolSecurityEditGranted = true;

                                            if (level > 2)
                                                bolSecurityAddGranted = true;

                                            if (level > 3)
                                                bolSecurityDeleteGranted = true;

                                            if (level > 4)
                                                bolSecurityFullGranted = true;
                                            break;

                                        case "Users":
                                            /*
                                            if (level > 0)
                                                bolUsersViewGranted = true;

                                            if (level > 1)
                                                bolUsersEditGranted = true;

                                            if (level > 2)
                                                bolUsersAddGranted = true;

                                            if (level > 3)
                                                bolUsersDeleteGranted = true;

                                            if (level > 4)
                                                bolUsersFullGranted = true;
                                            */
                                            break;

                                        case "Developer":
                                            if (level > 0)
                                                bolDeveloperViewGranted = true;

                                            if (level > 1)
                                                bolDeveloperEditGranted = true;

                                            if (level > 2)
                                                bolDeveloperAddGranted = true;

                                            if (level > 3)
                                                bolDeveloperDeleteGranted = true;

                                            if (level > 4)
                                                bolDeveloperFullGranted = true;
                                            break;

                                        case "Projects":
                                            /*
                                            if (level > 0)
                                                bolProjectsViewGranted = true;

                                            if (level > 1)
                                                bolProjectsEditGranted = true;

                                            if (level > 2)
                                                bolProjectsAddGranted = true;

                                            if (level > 3)
                                                bolProjectsDeleteGranted = true;

                                            if (level > 4)
                                                bolProjectsFullGranted = true;
                                                */
                                            break;

                                        case "Products":
                                            if (level > 0)
                                                bolProductsViewGranted = true;

                                            if (level > 1)
                                                bolProductsEditGranted = true;

                                            if (level > 2)
                                                bolProductsAddGranted = true;

                                            if (level > 3)
                                                bolProductsDeleteGranted = true;

                                            if (level > 4)
                                                bolProductsFullGranted = true;
                                            break;

                                        case "Quotes":
                                            if (level > 0)
                                                bolQuotesViewGranted = true;

                                            if (level > 1)
                                                bolQuotesEditGranted = true;

                                            if (level > 2)
                                                bolQuotesAddGranted = true;

                                            if (level > 3)
                                                bolQuotesDeleteGranted = true;

                                            if (level > 4)
                                                bolQuotesFullGranted = true;
                                            break;

                                        default:
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                strPageTitle = "No Security Set for this page";
                                intHelpContextID = 120;
                            }
                        }
                        else
                        {
                            strPageTitle = "No Security Set for this page";
                            intHelpContextID = 120;
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                    }
                }

                DateTime dteAccessGranted = DateTime.Now;
            }
	        else
	        {
	            string MM_qsChar = "?";
	            if (MM_authFailedURL.IndexOf("?") >= 1) MM_qsChar = "&";
	            string MM_referrer = Request.ServerVariables["URL"];
	            if (Request.QueryString.ToString().Length > 0) MM_referrer = MM_referrer + "?" + Request.QueryString.ToString();
	            MM_authFailedURL = MM_authFailedURL + MM_qsChar + "accessdenied=" + Server.UrlEncode(MM_referrer);
	            Response.Redirect(MM_authFailedURL);
	            //'Response.Write("MM_Username = " & Session("MM_Username") & "<br />" & "PositionID = |" & Request.Cookies("PositionID") & "|<br />" & "MM_UsernameLongTerm = " & Request.Cookies("MM_UsernameLongTerm") & "<br />")
            }
        }
    }
}