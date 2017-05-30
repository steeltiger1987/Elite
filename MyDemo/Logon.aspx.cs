using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Elite
{
    public partial class Logon : System.Web.UI.Page
    {
        string connectionString = @"Server=(localdb)\MSSQLLocalDB; Integrated Security=true; Initial Catalog=Elite; Trusted_Connection=yes; connection timeout=150;";
        //string connectionString = @"Server=ccastweb.com; Database=Elite; User ID=AChen; Password=Andrew1;";

        string MM_LoginAction;
        protected string MM_valUsername;
        bool bolLoginSuccess;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogon_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.ToString() != "")
                MM_LoginAction = MM_LoginAction + "?" + Server.HtmlEncode(Request.QueryString.ToString());

            if (Request.Form["tbxUsername"] != "")
                MM_valUsername = Request.Form["tbxUsername"].ToUpper().Trim();
            else
                MM_valUsername = Request.QueryString["EmailPassword"].Trim();

            //'Response.Write("MM_LoginAction = " & MM_LoginAction & "<br />")
            //'Response.Write("MM_valUsername = " & MM_valUsername & "<br />")
            //'Response.Write("Request.QueryString = " & Request.QueryString & "<br />")
            //'Response.Write("Request.Form(tbxUsername) = " & Request.Form("tbxUsername") & "<br />")

            if (MM_valUsername != "")
            {
                string MM_redirectLoginSuccess;
                string MM_redirectLoginFailed;
                bool bolValidUser;
                int intPositionID = -1;
                long lngUserID = -1;
                string strLandingPage = "";
                string strEmailAddress;
                string strPassword;

                bolLoginSuccess = false;

                MM_redirectLoginFailed = "logon.aspx";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.ConnectionString = connectionString;
                        SqlCommand cmd = new SqlCommand("SELECT UserID, UserName, PositionID, Password, EmailAddress, PageAddress AS LandingPage, BillingCustomerID FROM Users INNER JOIN Pages ON Pages.PageID = Users.LandingPageID WHERE (UserName = @UserName) AND Users.Active = 1", connection);
                        cmd.Parameters.AddWithValue("@UserName", MM_valUsername);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        connection.Open();
                        da.Fill(ds);
                        connection.Close();

                        if (!object.Equals(ds.Tables[0], null))
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                bolValidUser = true;
                                intPositionID = (int)(ds.Tables[0].Rows[0]["PositionID"]);
                                lngUserID = (long)(ds.Tables[0].Rows[0]["UserID"]);
                                //'Response.Write("MM_User BillingCustomerID = " & MM_rsUser.Fields.Item("BillingCustomerID").Value & "<br />")

                                if (bolValidUser)
                                {
                                    //' username esixts - this is a valid user
                                    //'Add user password if none currently exists
                                    Response.Write("Password = " + ds.Tables[0].Rows[0]["Password"] + "<br />");
                                    Response.Write("tbxPassword = " + Request.Form["tbxPassword"] + "<br />");

                                    strLandingPage = ds.Tables[0].Rows[0]["LandingPage"].ToString();


                                    if (false) //If MM_rsUser.Fields.Item("Password").Value = "" OR IsNull(MM_rsUser.Fields.Item("Password").Value) Then
                                    {
                                        /*
                                        MM_rsUser_cmd.CommandText = "UPDATE Users SET Password = ? WHERE UserID = ?"

                                       MM_rsUser_cmd.Parameters.Delete 0

                                       MM_rsUser_cmd.Parameters.Append MM_rsUser_cmd.CreateParameter("param2", 200, 1, 255, Request.Form("tbxPassword")) ' adVarChar

                                       MM_rsUser_cmd.Parameters.Append MM_rsUser_cmd.CreateParameter("param1", 5, 1, -1, lngUserID) ' adVarChar

                                       MM_rsUser_cmd.Prepared = true

                                       MM_rsUser_cmd.Execute

                                       bolLoginSuccess = True
                                       */
                                    }
                                    else
                                    {
                                        //'Authenticate valid user with correct password
                                        strEmailAddress = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                                        strPassword = ds.Tables[0].Rows[0]["Password"].ToString();

                                        if (strPassword == Request.Form["tbxPassword"])
                                            bolLoginSuccess = true;
                                        else
                                            MM_redirectLoginFailed = "logon.aspx?logon=" + MM_valUsername;

                                    }

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                    }
                }
                if (bolLoginSuccess)
                {

                    Session["MM_Username"] = MM_valUsername;

                    Session["UserID"] = lngUserID;

                    Session["PositionID"] = intPositionID;

                    Response.SetCookie(new HttpCookie("MM_Username", MM_valUsername));

                    Response.Cookies["MM_Username"].Expires = DateTime.Now;

                    Response.SetCookie(new HttpCookie("UserID", lngUserID.ToString()));

                    Response.Cookies["UserID"].Expires = DateTime.Now;

                    Response.SetCookie(new HttpCookie("PositionID", intPositionID.ToString()));

                    Response.Cookies["PositionID"].Expires = DateTime.Now;

                    if (Request.Form["chkKeepLogin"] == "true")
                    {

                        Response.SetCookie(new HttpCookie("MM_UsernameLongTerm", MM_valUsername));

                        Response.Cookies["MM_UsernameLongTerm"].Expires = DateTime.Now.AddDays(365);

                        Response.SetCookie(new HttpCookie("UserIDLongTerm", lngUserID.ToString()));

                        Response.Cookies["UserIDLongTerm"].Expires = DateTime.Now.AddDays(365);

                    }

                    MM_redirectLoginSuccess = strLandingPage;
                    MM_redirectLoginSuccess = MM_redirectLoginSuccess.Replace("asp", "aspx");

                    if (Request.QueryString["accessdenied"] != null && Request.QueryString["accessdenied"] != "")
                        MM_redirectLoginSuccess = Request.QueryString["accessdenied"];

                    Session["intLogonAttempts"] = 0;
                    Response.Redirect(MM_redirectLoginSuccess);
                    ///'Response.Write("Session BillingCustomerID = " & Session("BillingCustomerID"))
                }

                if (Request.QueryString["EmailPassword"] == "")
                    Response.Redirect(MM_redirectLoginFailed);
            }

        }
    }
}