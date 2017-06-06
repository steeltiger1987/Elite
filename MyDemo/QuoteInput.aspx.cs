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
    public partial class QuoteInput : System.Web.UI.Page
    {
        //string connectionString = @"Server=(localdb)\MSSQLLocalDB; Integrated Security=true; Initial Catalog=Elite; Trusted_Connection=yes; connection timeout=150;";
        string connectionString = @"Server=ccastweb.com; Database=Elite; User ID=AChen; Password=Andrew1;";

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
            ErrorMsg.Text = "";
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

            string mode = Request.QueryString["mode"];
            string lngQuoteID = Request.QueryString["lngQuoteID"];
            string lngProductID = Request.QueryString["lngProductID"];
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd;
                    SqlDataAdapter da;

                    cmd = new SqlCommand("SELECT * FROM Clients ORDER BY ClientID", connection);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    sb = new StringBuilder();
                    connection.Open();
                    da.Fill(ds);
                    connection.Close();

                    cbxClientID.Items.Clear();

                    if (!object.Equals(ds.Tables[0], null))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                cbxClientID.Items.Add(new ListItem(ds.Tables[0].Rows[i]["ClientName"].ToString() + " - " + ds.Tables[0].Rows[i]["Company"].ToString(), ds.Tables[0].Rows[i]["ClientID"].ToString()));
                            }
                        }
                    }

                    cmd = new SqlCommand("SELECT * FROM Ports ORDER BY PortID", connection);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    sb = new StringBuilder();
                    connection.Open();
                    da.Fill(ds);
                    connection.Close();

                    cbxFOBPort.Items.Clear();

                    if (!object.Equals(ds.Tables[0], null))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                cbxFOBPort.Items.Add(new ListItem(ds.Tables[0].Rows[i]["PortName"].ToString(), ds.Tables[0].Rows[i]["PortID"].ToString()));
                            }
                        }
                    }

                    if (mode == "edit")
                    {
                        connection.ConnectionString = connectionString;
                        cmd = new SqlCommand("SELECT *, DateDiff(day, Quotes.QuoteDate, Quotes.ValidDate) AS ValidDays FROM Quotes LEFT OUTER JOIN Clients ON Quotes.ClientID = Clients.ClientID LEFT OUTER JOIN Products ON Quotes.ProductID = Products.ProductID WHERE Quotes.QuoteID = @QuoteID", connection);
                        cmd.Parameters.AddWithValue("@QuoteID", Int64.Parse(lngQuoteID));
                        da = new SqlDataAdapter(cmd);
                        ds = new DataSet();
                        connection.Open();
                        da.Fill(ds);
                        connection.Close();

                        if (!object.Equals(ds.Tables[0], null))
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                cbxClientID.SelectedValue = ds.Tables[0].Rows[0]["ClientID"].ToString();
                                tbxProject.Text = ds.Tables[0].Rows[0]["Project"].ToString();
                                tbxQuoteNum.Text = ds.Tables[0].Rows[0]["QuoteNumber"].ToString();
                                tbxItemNum.Text = ds.Tables[0].Rows[0]["ItemNum"].ToString();
                                tbxName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                                tbxDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                                tbxPackaging.Text = ds.Tables[0].Rows[0]["Packaging"].ToString();
                                tbxProdTimeLo.Text = ds.Tables[0].Rows[0]["ProdTimeLo"].ToString();
                                tbxProdTimeHi.Text = ds.Tables[0].Rows[0]["ProdTimeHi"].ToString();
                                tbxWeightPerCarton.Text = ds.Tables[0].Rows[0]["WeightPerCarton"].ToString();
                                tbxUnitsPerCarton.Text = ds.Tables[0].Rows[0]["UnitsPerCarton"].ToString();
                                tbxCartonL.Text = ds.Tables[0].Rows[0]["CartonL"].ToString();
                                tbxCartonW.Text = ds.Tables[0].Rows[0]["CartonW"].ToString();
                                tbxCartonH.Text = ds.Tables[0].Rows[0]["CartonH"].ToString();
                                tbxValid.Text = ds.Tables[0].Rows[0]["ValidDays"].ToString();
                                tbxExchangeRate.Text = ds.Tables[0].Rows[0]["ExchangeRate"].ToString();
                                tbxMargin.Text = ds.Tables[0].Rows[0]["Margin"].ToString();
                                tbxDutyRate.Text = ds.Tables[0].Rows[0]["DutyRate"].ToString();
                                cbxFOBPort.SelectedValue = ds.Tables[0].Rows[0]["PortID"].ToString();
                                tbxSetup.Text = ds.Tables[0].Rows[0]["Setup"].ToString();
                                tbxMoldFee.Text = ds.Tables[0].Rows[0]["MoldFee"].ToString();
                                tbxTesting.Text = ds.Tables[0].Rows[0]["Testing"].ToString();
                                tbxPrePro.Text = ds.Tables[0].Rows[0]["PrePro"].ToString();
                                tbxPreProTime.Text = ds.Tables[0].Rows[0]["PreProTime"].ToString();
                            }
                            else
                            {
                                sb.Append(@"<tr>");
                                sb.Append(@"<td colspan=""9"">Viewing this list requires certain &quot;Products&quot; permissions</td>");
                                sb.Append(@"</tr>");
                            }
                        }

                        cmd = new SqlCommand("SELECT RMBPrice, Quantity, LeadTime FROM dbo.QuoteDetails WHERE QuoteID = @QuoteID ORDER BY SubID", connection);
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
                                    sb.Append(@"<td><input name=""tbxRMBPrice[]"" type=""text"" id=""tbxRMBPrice" + i + @""" value=""" + ds.Tables[0].Rows[i]["RMBPrice"] + @""" /></td>");
                                    sb.Append(@"<td><input name=""tbxQty[]"" type=""text"" id=""tbxQty" + i + @""" value=""" + ds.Tables[0].Rows[i]["Quantity"] + @""" /></td>");
                                    sb.Append(@"<td><input name=""tbxLeadTime[]"" type=""text"" id=""tbxLeadTime" + i + @""" value=""" + ds.Tables[0].Rows[i]["LeadTime"] + @""" /></td>");
                                    sb.Append(@"</tr>");
                                }
                            }
                        }
                        for (; i < 5; i++)
                        {
                            sb.Append(@"<tr>");
                            sb.Append(@"<td><input name=""tbxRMBPrice[]"" type=""text"" id=""tbxRMBPrice" + i + @""" value="""" /></td>");
                            sb.Append(@"<td><input name=""tbxQty[]"" type=""text"" id=""tbxQty" + i + @""" value="""" /></td>");
                            sb.Append(@"<td><input name=""tbxLeadTime[]"" type=""text"" id=""tbxLeadTime" + i + @""" value="""" /></td>");
                            sb.Append(@"</tr>");
                        }
                        CostDataPlaceHolder.Controls.Add(new Literal { Text = sb.ToString() });
                        MM_mode.Value = "edit";
                        MM_recordId.Value = lngQuoteID;
                        btnEdit.Text = "Update";
                    }
                    else
                    {
                        connection.ConnectionString = connectionString;
                        cmd = new SqlCommand("SELECT Products.*, Quotes.* FROM Products, Quotes WHERE Products.ProductID = @ProductID AND Products.ProductID = Quotes.ProductID", connection);
                        cmd.Parameters.AddWithValue("@ProductID", Int64.Parse(lngProductID));
                        da = new SqlDataAdapter(cmd);
                        ds = new DataSet();
                        connection.Open();
                        da.Fill(ds);
                        connection.Close();

                        if (!object.Equals(ds.Tables[0], null))
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                cbxClientID.SelectedIndex = -1;
                                tbxItemNum.Text = ds.Tables[0].Rows[0]["ItemNum"].ToString();
                                tbxName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                                tbxDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                                tbxPackaging.Text = ds.Tables[0].Rows[0]["Packaging"].ToString();
                                tbxProdTimeLo.Text = ds.Tables[0].Rows[0]["ProdTimeLo"].ToString();
                                tbxProdTimeHi.Text = ds.Tables[0].Rows[0]["ProdTimeHi"].ToString();
                                tbxWeightPerCarton.Text = ds.Tables[0].Rows[0]["WeightPerCarton"].ToString();
                                tbxUnitsPerCarton.Text = ds.Tables[0].Rows[0]["UnitsPerCarton"].ToString();
                                tbxCartonL.Text = ds.Tables[0].Rows[0]["CartonL"].ToString();
                                tbxCartonW.Text = ds.Tables[0].Rows[0]["CartonW"].ToString();
                                tbxCartonH.Text = ds.Tables[0].Rows[0]["CartonH"].ToString();
                                tbxValid.Text = "30";
                                tbxExchangeRate.Text = "6.2";
                                tbxMargin.Text = "0.7";
                                tbxDutyRate.Text = "5.3";
                                cbxFOBPort.SelectedIndex = -1;
                            }
                            else
                            {
                                sb.Append(@"<tr>");
                                sb.Append(@"<td colspan=""9"">Viewing this list requires certain &quot;Products&quot; permissions</td>");
                                sb.Append(@"</tr>");
                            }
                        }

                        cmd = new SqlCommand("SELECT RMBPrice, Quantity, LeadTime  FROM dbo.ProductCosts WHERE ProductID = @ProductID ORDER BY Quantity", connection);
                        cmd.Parameters.AddWithValue("@ProductID", Int64.Parse(lngProductID));
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
                                    sb.Append(@"<td><input name=""tbxRMBPrice[]"" type=""text"" id=""tbxRMBPrice" + i + @""" value=""" + ds.Tables[0].Rows[i]["RMBPrice"] + @""" /></td>");
                                    sb.Append(@"<td><input name=""tbxQty[]"" type=""text"" id=""tbxQty" + i + @""" value=""" + ds.Tables[0].Rows[i]["Quantity"] + @""" /></td>");
                                    sb.Append(@"<td><input name=""tbxLeadTime[]"" type=""text"" id=""tbxLeadTime" + i + @""" value=""" + ds.Tables[0].Rows[i]["LeadTime"] + @""" /></td>");
                                    sb.Append(@"</tr>");
                                }
                            }
                        }
                        for (; i < 5; i++)
                        {
                            sb.Append(@"<tr>");
                            sb.Append(@"<td><input name=""tbxRMBPrice[]"" type=""text"" id=""tbxRMBPrice" + i + @""" value="""" /></td>");
                            sb.Append(@"<td><input name=""tbxQty[]"" type=""text"" id=""tbxQty" + i + @""" value="""" /></td>");
                            sb.Append(@"<td><input name=""tbxLeadTime[]"" type=""text"" id=""tbxLeadTime" + i + @""" value="""" /></td>");
                            sb.Append(@"</tr>");
                        }
                        CostDataPlaceHolder.Controls.Add(new Literal { Text = sb.ToString() });

                        btnDelete.Visible = false;
                        MM_mode.Value = "add";
                        MM_recordId.Value = lngProductID;
                    }

                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string mode = Request.Form["MM_mode"];
            string lngQuoteID = Request.Form["MM_recordId"];
            long lngNewQuoteID = -1;

            long FOBPort = Int64.Parse(cbxFOBPort.SelectedValue);
            DataSet ports = new DataSet();
            SqlCommand cmd1;
            SqlDataAdapter da1;
            string sql;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                cmd1 = new SqlCommand("SELECT * FROM Ports  WHERE PortID = @PortID", connection);
                cmd1.Parameters.AddWithValue("@PortID", FOBPort);
                da1 = new SqlDataAdapter(cmd1);
                connection.Open();
                da1.Fill(ports);
                connection.Close();
            }

            try
            {
                if (mode == "edit")
                {
                    sql = "UPDATE dbo.Quotes SET ClientID = @ClientID, Project = @Project, QuoteNumber = @QuoteNumber, QuoteDate = GETDATE(), ValidDate = GETDATE() + @Valid, ExchangeRate = @ExchangeRate, Margin = @Margin, DutyRate = @DutyRate, PortID = @PortID, Setup = @Setup, MoldFee = @MoldFee, Testing = @Testing, PrePro = @PrePro, PreProTime = @PreProTime WHERE QuoteID = @QuoteID";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@ClientID", Int64.Parse(cbxClientID.SelectedValue));
                            cmd.Parameters.AddWithValue("@Project", tbxProject.Text);
                            cmd.Parameters.AddWithValue("@QuoteNumber", tbxQuoteNum.Text);
                            cmd.Parameters.AddWithValue("@Valid", Int32.Parse(tbxValid.Text));
                            cmd.Parameters.AddWithValue("@ExchangeRate", double.Parse(tbxExchangeRate.Text));
                            cmd.Parameters.AddWithValue("@Margin", double.Parse(tbxMargin.Text));
                            cmd.Parameters.AddWithValue("@DutyRate", double.Parse(tbxDutyRate.Text));
                            cmd.Parameters.AddWithValue("@PortID", Int64.Parse(cbxFOBPort.SelectedValue));
                            cmd.Parameters.AddWithValue("@QuoteID", Int64.Parse(lngQuoteID));
                            cmd.Parameters.AddWithValue("@Setup", double.Parse(tbxSetup.Text));
                            cmd.Parameters.AddWithValue("@MoldFee", double.Parse(tbxMoldFee.Text));
                            cmd.Parameters.AddWithValue("@Testing", double.Parse(tbxTesting.Text));
                            cmd.Parameters.AddWithValue("@PrePro", double.Parse(tbxPrePro.Text));
                            cmd.Parameters.AddWithValue("@PreProTime", Int64.Parse(tbxPreProTime.Text));

                            connection.Open();
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
             
                    sql = "DELETE FROM QuoteDetails WHERE QuoteID = @QuoteID";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@QuoteID", Int64.Parse(lngQuoteID));

                            connection.Open();
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                }

                string[] strRMBPrice = Request.Form["tbxRMBPrice[]"].Split(',');
                string[] strQty = Request.Form["tbxQty[]"].Split(',');
                string[] strLeadTime = Request.Form["tbxLeadTime[]"].Split(',');

                bool bMasterInserted = false;
                int i = 0;

                for (; i < strRMBPrice.Length; i++)
                {
                    if (strRMBPrice[i] == "" || strQty[i] == "")
                        break;

                    double dblRMBPrice = double.Parse(strRMBPrice[i]);

                    long intQty = Int64.Parse(strQty[i]);

                    string intLeadTime = strLeadTime[i].Trim();

                    if (dblRMBPrice <= 0 || intQty <= 0)
                    {

                    }
                    else
                    {
                        double dblExchangeRate = double.Parse(tbxExchangeRate.Text);
                        double dblFOBPrice = dblRMBPrice / dblExchangeRate;
                        int intPcsPerCtn = int.Parse(tbxUnitsPerCarton.Text);
                        long intNumberOfCtns = (long)(Math.Ceiling((double)intQty / intPcsPerCtn));
                        double dblCtnSizeL = double.Parse(tbxCartonL.Text);
                        double dblCtnSizeW = double.Parse(tbxCartonW.Text);
                        double dblCtnSizeH = double.Parse(tbxCartonH.Text);
                        double dblCtnWgt = double.Parse(tbxWeightPerCarton.Text);
                        double dblCBM = Math.Round(dblCtnSizeL * dblCtnSizeW * dblCtnSizeH / 1000000 * intNumberOfCtns + 0.049, 1);
                        double dblValueOfOrder = dblFOBPrice * intQty;
                        double dblMargin = double.Parse(tbxMargin.Text);
                        double dblFOBChinaSellPrice = dblValueOfOrder / dblMargin;
                        double dblSellPrice = Math.Round(dblFOBChinaSellPrice / intQty + 0.000049, 4);
                        double dblDutyRate = double.Parse(tbxDutyRate.Text) / 100;
                        double dblTotalDutyPaid = dblValueOfOrder * dblDutyRate;
                        double dblDuties = Math.Round(dblTotalDutyPaid / intQty + 0.000049, 4);
                        double dblSetup = double.Parse(tbxSetup.Text);
                        double dblMoldFee = double.Parse(tbxMoldFee.Text);
                        double dblTesting = double.Parse(tbxTesting.Text);
                        double dblPrePro = double.Parse(tbxPrePro.Text);
                        double dblPreProTime = double.Parse(tbxPreProTime.Text);
                        
                        int intFreightRateCode;

                        if (dblCBM <= 1)
                            intFreightRateCode = 1;
                        else if (dblCBM <= 5)
                            intFreightRateCode = 2;
                        else if (dblCBM <= 10)
                            intFreightRateCode = 3;
                        else if (dblCBM <= 28)
                            intFreightRateCode = 4;
                        else if (dblCBM <= 56)
                            intFreightRateCode = 5;
                        else
                            intFreightRateCode = 6;

                        double dblRateCBM = 0.0;
                        if (!object.Equals(ports.Tables[0], null) && ports.Tables[0].Rows.Count > 0)
                        {
                            dblRateCBM = double.Parse(ports.Tables[0].Rows[0]["Zone" + intFreightRateCode].ToString());
                        }
                        else
                        {
                            Response.Write("Error: Retrieving Rate/CBM data failed.");
                        }

                        double dblFreight = Math.Round(dblRateCBM * dblCBM / intQty + 0.000049, 4);
                        double dblFees;
                        dblFees = 50; // bank fee
                        dblFees = dblFees + 200; // inspection fee
                        dblFees = dblFees + 22.5; // messenger fee
                        dblFees = dblFees + 120; // ocean import entry
                        dblFees = dblFees + dblCBM * 10; // pier pass fee
                        dblFees = dblFees + 35; // outlay fee
                        dblFees = dblFees + 30; // ISF
                        dblFees = dblFees + dblCBM * 5; // terminal in/out fee
                        dblFees = dblFees + (dblCBM < 1 ? 50 : dblCBM * 10); // FSC fee ($50 minimum)
                        dblFees = dblFees + 25; // warehouse inbound fee per billing of lading
                        dblFees = dblFees + Math.Round(dblCBM / 1.5 + 0.49, 0) * 20; // pallet fee per pallet
                        dblFees = dblFees + (dblValueOfOrder < 20000 ? 25 : dblValueOfOrder * 0.00125); // habor maintenance fee ($25 minimum)
                        dblFees = dblFees + (dblValueOfOrder < 7225 ? 25 : dblValueOfOrder * 0.00346); // merchandise processing fee ($25 minimum)
                        dblFees = Math.Round(dblFees / intQty + 0.000049, 4);

                        double dblTotal = dblSellPrice + dblDuties + dblFreight + dblFees;

                        if (mode == "add" && !bMasterInserted)
                        {
                            sql = "INSERT INTO Quotes (ClientID, Project, QuoteNumber, QuoteDate, ValidDate, ProductID, ExchangeRate, Margin, DutyRate, PortID, Setup, MoldFee, Testing, PrePro, PreProTime) VALUES (@ClientID, @Project, @QuoteNumber, GETDATE(), GETDATE() + @Valid, @ProductID, @ExchangeRate, @Margin, @DutyRate, @PortID, @Setup, @MoldFee, @Testing, @PrePro, @PreProTime)";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                using (SqlCommand cmd = new SqlCommand(sql, connection))
                                {
                                    cmd.Parameters.AddWithValue("@ClientID", Int64.Parse(cbxClientID.SelectedValue));
                                    cmd.Parameters.AddWithValue("@Project", tbxProject.Text);
                                    cmd.Parameters.AddWithValue("@QuoteNumber", tbxQuoteNum.Text);
                                    cmd.Parameters.AddWithValue("@Valid", Int32.Parse(tbxValid.Text));
                                    cmd.Parameters.AddWithValue("@ProductID", lngQuoteID); // MM_recordId is ProductID
                                    cmd.Parameters.AddWithValue("@ExchangeRate", double.Parse(tbxExchangeRate.Text));
                                    cmd.Parameters.AddWithValue("@Margin", double.Parse(tbxMargin.Text));
                                    cmd.Parameters.AddWithValue("@DutyRate", double.Parse(tbxDutyRate.Text));
                                    cmd.Parameters.AddWithValue("@PortID", Int64.Parse(cbxFOBPort.SelectedValue));
                                    cmd.Parameters.AddWithValue("@Setup", double.Parse(tbxSetup.Text));
                                    cmd.Parameters.AddWithValue("@MoldFee", double.Parse(tbxMoldFee.Text));
                                    cmd.Parameters.AddWithValue("@Testing", double.Parse(tbxTesting.Text));
                                    cmd.Parameters.AddWithValue("@PrePro", double.Parse(tbxPrePro.Text));
                                    cmd.Parameters.AddWithValue("@PreProTime", Int64.Parse(tbxPreProTime.Text));

                                    connection.Open();
                                    cmd.ExecuteNonQuery();
                                    connection.Close();
                                }
                            }

                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                SqlCommand cmd;
                                SqlDataAdapter da;
                                DataSet ds;

                                cmd = new SqlCommand("SELECT TOP 1 QuoteID AS LastID FROM dbo.Quotes ORDER BY QuoteID DESC", connection);
                                da = new SqlDataAdapter(cmd);
                                ds = new DataSet();
                                connection.Open();
                                da.Fill(ds);
                                connection.Close();

                                if (!object.Equals(ds.Tables[0], null))
                                {
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        lngNewQuoteID = Int64.Parse(ds.Tables[0].Rows[0]["LastID"].ToString());
                                    }
                                }
                            }

                            bMasterInserted = true;
                        }

                        sql = "INSERT INTO QuoteDetails (QuoteID, SubID, RMBPrice, FOBPrice, Quantity, Package, PcsPerCtn, CtnWgtInKg, CtnSizeInCML, " +
                            "CtnSizeInCMW, CtnSizeInCMH, NumberOfCtns, CBM, LeadTime, SellPrice, Duties, Freight, Fees, TotalPrice) VALUES " +
                            "(@QuoteID, @SubID, @RMBPrice, @FOBPrice, @Quantity, @Package, @PcsPerCtn, @CtnWgtInKg, @CtnSizeInCML, " +
                            "@CtnSizeInCMW, @CtnSizeInCMH, @NumberOfCtns, @CBM, @LeadTime, @SellPrice, @Duties, @Freight, @Fees, @TotalPrice)";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand(sql, connection))
                            {
                                if (mode == "edit")
                                    cmd.Parameters.AddWithValue("@QuoteID", lngQuoteID);
                                else
                                    cmd.Parameters.AddWithValue("@QuoteID", lngNewQuoteID);
                                cmd.Parameters.AddWithValue("@SubID", i + 1);
                                cmd.Parameters.AddWithValue("@RMBPrice", dblRMBPrice);
                                cmd.Parameters.AddWithValue("@FOBPrice", dblFOBPrice);
                                cmd.Parameters.AddWithValue("@Quantity", intQty);
                                cmd.Parameters.AddWithValue("@Package", Request.Form["tbxPackaging"]);
                                cmd.Parameters.AddWithValue("@PcsPerCtn", intPcsPerCtn);
                                cmd.Parameters.AddWithValue("@CtnWgtInKg", dblCtnWgt);
                                cmd.Parameters.AddWithValue("@CtnSizeInCML", double.Parse(tbxCartonL.Text));
                                cmd.Parameters.AddWithValue("@CtnSizeInCMW", double.Parse(tbxCartonW.Text));
                                cmd.Parameters.AddWithValue("@CtnSizeInCMH", double.Parse(tbxCartonH.Text));
                                cmd.Parameters.AddWithValue("@NumberOfCtns", intNumberOfCtns);
                                cmd.Parameters.AddWithValue("@CBM", dblCBM);
                                cmd.Parameters.AddWithValue("@LeadTime", intLeadTime);
                                cmd.Parameters.AddWithValue("@SellPrice", dblSellPrice);
                                cmd.Parameters.AddWithValue("@Duties", dblDuties);
                                cmd.Parameters.AddWithValue("@Freight", dblFreight);
                                cmd.Parameters.AddWithValue("@Fees", dblFees);
                                cmd.Parameters.AddWithValue("@TotalPrice", dblTotal);

                                connection.Open();
                                cmd.ExecuteNonQuery();
                                connection.Close();
                            }
                        }
                    }
                }

                if (mode == "edit")
                {
                    Response.Redirect("QuoteInformation.aspx?lngQuoteID=" + lngQuoteID);
                }
                else
                {
                    Response.Redirect("QuoteInformation.aspx?lngQuoteID=" + lngNewQuoteID);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg.Text = ex.Message;
                LoadProductsData();
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string lngQuoteID = Request.Form["MM_recordId"];
            string sql = "DELETE FROM Quotes WHERE QuoteID = @QuoteID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@QuoteID", Int64.Parse(lngQuoteID));

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            sql = "DELETE FROM QuoteDetails WHERE QuoteID = @QuoteID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@QuoteID", Int64.Parse(lngQuoteID));

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            Response.Redirect("Quotes.aspx");
        }
    }
}