using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home_Page : BasePage
{
    static string cons = ConfigurationManager.ConnectionStrings["MyBusConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Logged"] == null)
        {
            Session["Logged"] = "None";
        }
        if(Session["Logged"].ToString() == "None")
        {
            Response.Redirect("LoginPage.aspx");
        }
        else if (Session["Logged"].ToString() == "User")
        {
            labelWelcome.Text = "WELCOME USER";
        }
        else if (Session["Logged"].ToString() == "Admin")
        {
            labelWelcome.Text = "WELCOME ADMIN";
            if (Application["TotalTickets"] == null || Application["TotalRevenue"] == null)
            {
                string sqlcmd = "SELECT SUM(Bookings.Seats) AS Seat, SUM(Bookings.Seats * Routes.Price) AS Revenue " +
                    "FROM Bookings INNER JOIN Routes ON Routes.Id = Bookings.RouteId";
                SqlConnection sqlcon = new SqlConnection(cons);
                SqlCommand cmd = new SqlCommand(sqlcmd, sqlcon);
                SqlDataReader rd;
                try
                {
                    sqlcon.Open();
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        Application["TotalTickets"] = rd["Seat"].ToString();
                        Application["TotalRevenue"] = rd["Revenue"].ToString();
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
            }
            if (Application["TotalTickets"] != null && Application["TotalRevenue"] != null)
            {
                labelWelcome.Text += "<br />Total Tickets Booked = " + Application["TotalTickets"].ToString() +
                "<br />Total Revenue = " + Application["TotalRevenue"].ToString();
            }
        }
    }
}