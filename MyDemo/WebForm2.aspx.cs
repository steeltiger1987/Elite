using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;

namespace MyDemo
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=ccastweb.com; Integrated Security=SSPI; User ID=AChen; Password=Andrew1; Initial Catalog=Elite; Trusted_Connection=yes; connection timeout=150;";
            //string connectionString = @"Server=(localdb)\MSSQLLocalDB; Integrated Security=true; Initial Catalog=Elite; Trusted_Connection=yes; connection timeout=150;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataReader myReader = null;
                    SqlCommand myCommand = new SqlCommand("SELECT * FROM QUOTES", connection);
                    myCommand.Connection.Open();
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        Response.Write(myReader["Project"].ToString() + "/" + myReader["QuoteNumber"].ToString());
                        Response.Write("<br/>");
                    }
                    myCommand.Connection.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }

            x = "This is expression!";
        }
        public string testc()
        {
            return "Hello";
        }

        public string x;
    }
}