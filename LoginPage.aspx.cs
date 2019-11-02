using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginPage : BasePage
{
    static string cons = ConfigurationManager.ConnectionStrings["MyBusConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!this.IsPostBack)
        {
            if (Session["Logged"] == null || Session["Logged"].ToString() == "None")
            {
                Session["Logged"] = "None";
            }
            else
            {
                Response.Redirect("HomePage.aspx");
            }
            textPassword.Attributes["type"] = "password";
            HttpCookie cookie = Request.Cookies["Autofill"];
            if (cookie != null)
            {
                textUsername.Text = cookie["Username"];
                textPassword.Text = cookie["Password"];
            }
        }
    }
    protected void buttonSignup_Click(object sender, EventArgs e)
    {
        Response.Redirect("SignUp.aspx");
    }

    protected void buttonLogin_Click(object sender, EventArgs e)
    {
        int temp = -1;
        string sqlcmd = "SELECT Type FROM Customers WHERE Username=@user AND Password=@pass";
        SqlConnection sqlcon = new SqlConnection(cons);
        SqlCommand cmd = new SqlCommand(sqlcmd, sqlcon);
        cmd.Parameters.AddWithValue("@user", textUsername.Text);
        cmd.Parameters.AddWithValue("@pass", textPassword.Text);
        SqlDataReader rd;
        try
        {
            sqlcon.Open();
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                temp = int.Parse(rd["Type"].ToString());
            }
            rd.Close();
        }
        catch (Exception err)
        { 
            //Do something
        }
        finally
        { 
            sqlcon.Close();
        }
        if(temp == -1)
        {
            labelResults.Text = "Wrong Username or Password. Try Again!";
        }
        if(temp == 0)
        {
            Session["Logged"] = "Admin";
            Session["Username"] = textUsername.Text;
            Response.Redirect("HomePage.aspx");
        }
        if (temp == 1)
        {
            Session["Logged"] = "User";
            Session["Username"] = textUsername.Text;
            Response.Redirect("HomePage.aspx");
        }
    }
}