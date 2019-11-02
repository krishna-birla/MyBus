using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SignUp : BasePage
{
    static string cons = ConfigurationManager.ConnectionStrings["MyBusConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        textboxPassword.Attributes["type"] = "password";
        textboxConfirmPassword.Attributes["type"] = "password";
    }

    protected void buttonSignup_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(cons);
        SqlCommand cmd1 = new SqlCommand("SELECT Type FROM Customers WHERE Username=@user", con);
        cmd1.Parameters.AddWithValue("@user", textboxUsername.Text);
        SqlDataReader rd;
        int flag = 0;
        try
        {
            con.Open();
            rd = cmd1.ExecuteReader();
            if (!rd.HasRows)
            {
                flag = 1;
            }
            rd.Close();

        }
        catch (Exception err)
        {
            //Do something
        }
        finally
        {
            con.Close();
        }
        if (flag == 1)
        {
            SqlCommand cmd2 = new SqlCommand("INSERT INTO Customers VALUES (@user, @pass, @name, 1)", con);
            cmd2.Parameters.AddWithValue("@user", textboxUsername.Text);
            cmd2.Parameters.AddWithValue("@pass", textboxPassword.Text);
            cmd2.Parameters.AddWithValue("@name", textboxName.Text);
            try
            {
                con.Open();
                cmd2.ExecuteNonQuery();
            }
            catch (Exception err) { }
            finally
            { 
                con.Close(); 
            }
            HttpCookie cookie = new HttpCookie("Autofill");
            cookie["Username"] = textboxUsername.Text;
            cookie["Password"] = textboxPassword.Text;
            Response.Cookies.Add(cookie);
            cookie.Expires = DateTime.Now.AddSeconds(10);
            Response.Redirect("LoginPage.aspx");
        }
        else
        {
            labelResult.Text = "Username already taken!";
        }
    }
}