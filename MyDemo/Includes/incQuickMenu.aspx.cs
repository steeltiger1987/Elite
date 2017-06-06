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
    public partial class incQuickMenu : System.Web.UI.Page
    {
        //string connectionString = @"Server=(localdb)\MSSQLLocalDB; Integrated Security=true; Initial Catalog=Elite; Trusted_Connection=yes; connection timeout=150;";
        string connectionString = @"Server=ccastweb.com; Database=Elite; User ID=AChen; Password=Andrew1;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadMenu();
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
                    cmd.Parameters.AddWithValue("@PositionID", Session["PositionID"]);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    connection.Open();
                    da.Fill(ds);
                    connection.Close();

                    if (!object.Equals(ds.Tables[0], null))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            int IsNewGroup = 1;
                            int intGroupPageID = 0;

                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (IsNewGroup == 1)
                                {
                                    IsNewGroup = 0;
                                    intGroupPageID = (int)(ds.Tables[0].Rows[i]["PageGroupID"]);

                                    sb.Append(@"<li><a class=""qmparent"" href=""javascript:void(0);"">" + ds.Tables[0].Rows[i]["GroupName"] + "</a>");
                                    sb.Append("<ul>");
                                }
                                sb.Append(@"<li><a href=""" + ds.Tables[0].Rows[i]["PageAddress"] +@""" title=""" + ds.Tables[0].Rows[i]["PageAddress"] + @""">" + ds.Tables[0].Rows[i]["PageTitle"] + "</a></li>");
                                sb.Append("<ul>");

                                if (intGroupPageID != (int)(ds.Tables[0].Rows[i]["PageGroupID"]))
                                {
                                    IsNewGroup = 1;
                                    sb.Append("</ul>");
                                    sb.Append("</li>");
                                }

                            }
                            Response.Write("</ul></li>");
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