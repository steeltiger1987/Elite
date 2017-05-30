using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MyDemo
{
    public partial class QuoteInformation : System.Web.UI.Page
    {
        string connectionString = @"Server=(localdb)\MSSQLLocalDB; Integrated Security=true; Initial Catalog=Elite; Trusted_Connection=yes; connection timeout=150;";
        //string connectionString = @"Server=ccastweb.com; Database=Elite; User ID=AChen; Password=Andrew1;";

        string MM_authFailedURL;
        bool MM_grantAccess;
        bool bolEmailNeedsUpdate;

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

        string strPageUrl;
        string strPageTitle;
        int intHelpContextID;
        DateTime dteAccessGranted;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadProductsData();
            }
        }

        private void LoadSecurity()
        {
            MM_authFailedURL = "logon.aspx";
            MM_grantAccess = false;
            bolEmailNeedsUpdate = false;

            //'Response.Write("MM_Username = " & Session("MM_Username") & "<br />")
            if (Session["MM_Username"] != null && Session["MM_Username"].ToString() != "")
                MM_grantAccess = true;
            else
            {
                //'Response.Write("PositionID = " & Request.Cookies("PositionID") & "<br />")

                if (Request.Cookies["UserID"] != null && Request.Cookies["UserID"].ToString() != "")
                {
                    Session["MM_Username"] = Request.Cookies["MM_Username"];
                    Session["UserID"] = Request.Cookies["UserID"];
                    Session["PositionID"] = Request.Cookies["PositionID"];
                    Session["BillingCustomerID"] = Request.Cookies["BillingCustomerID"];
                    //'Response.Write("Cookie BillingCustomerID = " & Request.Cookies("BillingCustomerID") & "<br />")

                    MM_grantAccess = false;
                }
                else
                {
                    //'Response.Write("MM_UsernameLongTerm = " & Request.Cookies("MM_UsernameLongTerm") & "<br />")

                    if (Request.Cookies["UserIDLongTerm"] == null || Request.Cookies["UserIDLongTerm"].ToString() == "")
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
            strPageUrl = Request.ServerVariables["URL"].Substring(1).Replace("aspx", "asp");

            if (MM_grantAccess)
            {
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

                dteAccessGranted = DateTime.Now;
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

        private void LoadMenu()
        {
            long lngVisitorID;
            //if (lngVisitorID == "")
            lngVisitorID = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    SqlCommand cmd = new SqlCommand("SELECT PageGroups.GroupName, PageGroups.TabOrder, Pages.PageTitle, Pages.PageAddress, PageGroups.PageGroupID, Pages.PageID FROM PageGroups INNER JOIN Pages ON PageGroups.PageGroupID = Pages.PageGroupID INNER JOIN PageElements ON Pages.PageID = PageElements.PageID INNER JOIN  Grants ON PageElements.ElementID = Grants.ElementID WHERE (Pages.NavigationPage = 1) AND (Pages.Active = 1) AND (Grants.PositionID = @PositionID) GROUP BY PageGroups.GroupName, PageGroups.TabOrder, Pages.PageTitle, Pages.PageAddress, PageGroups.PageGroupID, Pages.PageID ORDER BY PageGroups.TabOrder, Pages.PageTitle", connection);
                    cmd.Parameters.AddWithValue("@PositionID", Int64.Parse(Session["PositionID"].ToString()));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    connection.Open();
                    da.Fill(ds);
                    connection.Close();

                    StringBuilder sb = new StringBuilder();
                    if (!object.Equals(ds.Tables[0], null))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            int IsNewGroup = 1;
                            int intGroupPageID = 0;

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (IsNewGroup == 1)
                                {
                                    IsNewGroup = 0;
                                    intGroupPageID = (int)(ds.Tables[0].Rows[i]["PageGroupID"]);

                                    sb.Append(@"<li><a class=""qmparent"" href=""javascript:void(0);"">" + ds.Tables[0].Rows[i]["GroupName"] + "</a>");
                                    sb.Append("<ul>");
                                }
                                string pageAddress = ds.Tables[0].Rows[i]["PageAddress"].ToString();
                                pageAddress = pageAddress.Replace("asp", "aspx");
                                sb.Append(@"<li><a href=""" + pageAddress + @""" title=""" + pageAddress + @""">" + ds.Tables[0].Rows[i]["PageTitle"] + "</a></li>");

                                if (i == ds.Tables[0].Rows.Count - 1)
                                    sb.Append("</ul></li>");
                                else
                                {
                                    if (intGroupPageID != (int)(ds.Tables[0].Rows[i + 1]["PageGroupID"]))
                                    {
                                        IsNewGroup = 1;
                                        sb.Append("</ul>");
                                        sb.Append("</li>");
                                    }
                                }

                            }
                            MenuPlaceHolder.Controls.Add(new Literal { Text = sb.ToString() });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }

        }

        private void LoadProductsData()
        {
            LoadSecurity();
            LoadMenu();

            string lngQuoteID = Request.QueryString["lngQuoteID"];
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();
            string imgurl = "";
            string itemNum = "";
            string ProductName = "";
            string Setup = "";
            string MoldFee = "";
            string Testing = "";
            string PrePro = "";
            string PreProTime = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Quotes LEFT OUTER JOIN Clients ON Quotes.ClientID = Clients.ClientID LEFT OUTER JOIN Products ON Quotes.ProductID = Products.ProductID WHERE Quotes.QuoteID = @QuoteID", connection);
                    cmd.Parameters.AddWithValue("@QuoteID", Int64.Parse(lngQuoteID));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    connection.Open();
                    da.Fill(ds);
                    connection.Close();

                    if (!object.Equals(ds.Tables[0], null))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblClientName.Text = ds.Tables[0].Rows[0]["ClientName"].ToString();
                            lblCompany.Text = ds.Tables[0].Rows[0]["Company"].ToString();
                            lblProject.Text = ds.Tables[0].Rows[0]["Project"].ToString();
                            lblQuoteDate.Text = String.Format("{0:d}", ds.Tables[0].Rows[0]["QuoteDate"]);
                            lblPhone.Text = ds.Tables[0].Rows[0]["Phone"].ToString();
                            lblValidDate.Text = String.Format("{0:d}", ds.Tables[0].Rows[0]["ValidDate"]);
                            lblEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                            lblQuoteNumber.Text = ds.Tables[0].Rows[0]["QuoteNumber"].ToString();
                            if (ds.Tables[0].Rows[0]["HasImage"].ToString() == "Y")
                                imgurl = @"    <img src=""UploadFiles/ProductImages/" + ds.Tables[0].Rows[0]["ProductID"] + @".png"" alt=""some text"" style=""width: 100px; height: auto; "" />";
                            else
                                imgurl = @"    <img src=""UploadFiles/ProductImages/NoImage.png"" alt=""some text"" style=""width: 100px; height: auto; "" />";
                            itemNum = ds.Tables[0].Rows[0]["ItemNum"].ToString();
                            ProductName = ds.Tables[0].Rows[0]["Name"].ToString();
                            Setup = ds.Tables[0].Rows[0]["Setup"].ToString();
                            MoldFee = ds.Tables[0].Rows[0]["MoldFee"].ToString();
                            Testing = ds.Tables[0].Rows[0]["Testing"].ToString();
                            PrePro = ds.Tables[0].Rows[0]["PrePro"].ToString();
                            PreProTime = ds.Tables[0].Rows[0]["PreProTime"].ToString();
                        }
                        else
                        {
                        }
                    }

                    cmd = new SqlCommand("SELECT * FROM QuoteDetails WHERE QuoteID = @QuoteID ORDER BY SubID", connection);
                    cmd.Parameters.AddWithValue("@QuoteID", Int64.Parse(lngQuoteID));
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    sb = new StringBuilder();
                    connection.Open();
                    da.Fill(ds);
                    connection.Close();

                    int i = 0;
                    if (!object.Equals(ds.Tables[0], null))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (; i < ds.Tables[0].Rows.Count; i++)
                            {
                                sb.Append(@"<tr>");

                                if (i == 0)
                                {
                                    sb.Append(@"<td rowspan=""5"">");
                                    sb.Append(@"    <div style=""width: 100px; height: 100px; display: table - cell; vertical - align:middle"" >");
                                    sb.Append(imgurl);
                                    sb.Append(@"    </div>");
                                    sb.Append(@"</td>");
                                    sb.Append(@"<td rowspan=""5"">");
                                    sb.Append(@"    <p>ITEM: " + itemNum + @"</p>");
                                    sb.Append(@"    <p>NAME: " + ProductName + @"</p>");
                                    sb.Append(@"    <p>Setup: $ " + Setup + @"</p>");
                                    sb.Append(@"    <p>Mold Fee: $ " + MoldFee + @"</p>");
                                    sb.Append(@"    <p>Testing: $ " + Testing + @"</p>");
                                    sb.Append(@"    <p>Pre-Pro: $ " + PrePro + @"</p>");
                                    sb.Append(@"    <p>Pre-Pro Time: " + PreProTime + @"days</p>");
                                    sb.Append(@"</td>");
                                }
                                sb.Append(@"<td>" + String.Format("{0:N0}", ds.Tables[0].Rows[i]["Quantity"])  + "</td>");
                                sb.Append(@"<td>" + String.Format("{0}", ds.Tables[0].Rows[i]["Package"]) + "</td>");
                                sb.Append(@"<td>" + String.Format("{0:N0}", ds.Tables[0].Rows[i]["PcsPerCtn"]) + "</td>");
                                sb.Append(@"<td>" + String.Format("{0:F1}", ds.Tables[0].Rows[i]["CtnWgtInKg"]) + "</td>");
                                sb.Append(@"<td>" + String.Format("{0:F1}", ds.Tables[0].Rows[i]["CtnSizeInCML"]) + "</td>");
                                sb.Append(@"<td>" + String.Format("{0:F1}", ds.Tables[0].Rows[i]["CtnSizeInCMW"]) + "</td>");
                                sb.Append(@"<td>" + String.Format("{0:F1}", ds.Tables[0].Rows[i]["CtnSizeInCMH"]) + "</td>");
                                sb.Append(@"<td>" + String.Format("{0:N0}", ds.Tables[0].Rows[i]["NumberOfCtns"]) + "</td>");
                                sb.Append(@"<td>" + String.Format("{0:F1}", ds.Tables[0].Rows[i]["CBM"]) + "</td>");
                                if (ds.Tables[0].Rows[i]["LeadTime"].ToString() == "")
                                    sb.Append(@"<td></td>");
                                else
                                    sb.Append(@"<td>" + ds.Tables[0].Rows[i]["LeadTime"] + " days after approval</td>");
                                sb.Append(@"<td>" + String.Format("{0:C2}", ds.Tables[0].Rows[i]["SellPrice"]) + "</td>");
                                sb.Append(@"<td>" + String.Format("{0:C2}", ds.Tables[0].Rows[i]["Duties"]) + "</td>");
                                sb.Append(@"<td>" + String.Format("{0:C2}", ds.Tables[0].Rows[i]["Freight"]) + "</td>");
                                sb.Append(@"<td>" + String.Format("{0:C2}", ds.Tables[0].Rows[i]["Fees"]) + "</td>");
                                sb.Append(@"<td>" + String.Format("{0:C2}", ds.Tables[0].Rows[i]["TotalPrice"]) + "</td>");
                                sb.Append(@"</tr>");  
                            }
                        }
                    }

                    for (; i < 5; i++)
                    {
                        sb.Append(@"<tr>");

                        if (i == 0)
                        {
                            sb.Append(@"<td rowspan=""5"">");
                            sb.Append(@"    <div style=""width: 100px; height: 100px; display: table - cell; vertical - align:middle"" >");
                            sb.Append(imgurl);
                            sb.Append(@"    </div>");
                            sb.Append(@"</td>");
                            sb.Append(@"<td rowspan=""5"">");
                            sb.Append(@"    <p>ITEM: " + itemNum + @"</p>");
                            sb.Append(@"    <p>NAME: " + ProductName + @"</p>");
                            sb.Append(@"    <p>Setup: $ " + Setup + @"</p>");
                            sb.Append(@"    <p>Mold Fee: $ " + MoldFee + @"</p>");
                            sb.Append(@"    <p>Testing: $ " + Testing + @"</p>");
                            sb.Append(@"    <p>Pre-Pro: $ " + PrePro + @"</p>");
                            sb.Append(@"    <p>Pre-Pro Time: $ " + PreProTime + @"</p>");
                            sb.Append(@"</td>");
                        }
                        sb.Append(@"<td></td>");
                        sb.Append(@"<td></td>");
                        sb.Append(@"<td></td>");
                        sb.Append(@"<td></td>");
                        sb.Append(@"<td></td>");
                        sb.Append(@"<td></td>");
                        sb.Append(@"<td></td>");
                        sb.Append(@"<td></td>");
                        sb.Append(@"<td></td>");
                        sb.Append(@"<td></td>");
                        sb.Append(@"<td></td>");
                        sb.Append(@"<td></td>");
                        sb.Append(@"<td></td>");
                        sb.Append(@"<td></td>");
                        sb.Append(@"<td></td>");
                        sb.Append(@"</tr>");
                    }

                    QuoteDataPlaceHolder.Controls.Add(new Literal { Text = sb.ToString() });
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }

        }
    }
}