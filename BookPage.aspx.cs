using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookPage : BasePage
{
    string source, destination, date, seats;
    static string cons = ConfigurationManager.ConnectionStrings["MyBusConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if(Session["Logged"] == null || Session["Logged"].ToString() == "None")
            {
                Response.Redirect("HomePage.aspx");
            }
            if (Request.QueryString["From"] == null || Request.QueryString["To"] == null
                || Request.QueryString["Date"] == null || Request.QueryString["Seats"] == null)
            {
                Response.Redirect("HomePage.aspx");
            }
            dropdownBus.Items.Add("Any");
            source = HttpUtility.UrlDecode(Request.QueryString["From"]);
            destination = HttpUtility.UrlDecode(Request.QueryString["To"]);
            date = HttpUtility.UrlDecode(Request.QueryString["Date"]);
            seats = HttpUtility.UrlDecode(Request.QueryString["Seats"]);
            SqlConnection con = new SqlConnection(cons);
            string sqlcmd = "SELECT DISTINCT BusOperator FROM Routes WHERE Source='" + source + "' " +
                "AND Destination='" + destination + "' " +
                "AND Seats>=" + seats + " AND CAST(Departure AS DATE)='" + date + "'";
            SqlCommand cmd = new SqlCommand(sqlcmd, con);
            SqlDataReader rd;
            try
            {
                con.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    dropdownBus.Items.Add(rd["BusOperator"].ToString());
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
            dropdownBus.SelectedIndex = 0;
            buttonBook.Enabled = false;
            labelBook.Text = "Select a route";
            labelAmount.Text = "Select a route";
            labelFrom.Text = source;
            labelTo.Text = destination;
            labelSeats.Text = seats;
            labelDate.Text = date;
            sqlcmd = "SELECT * FROM Routes WHERE Source='" + source + "' " +
                "AND Destination='" + destination + "' " +
                "AND Seats>=" + seats + " AND CAST(Departure AS DATE)='" + date + "'";
            con = new SqlConnection(cons);
            SqlDataAdapter ad = new SqlDataAdapter(sqlcmd, con);
            DataSet ds = new DataSet();
            con.Open();
            ad.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            con.Close();
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["Logged"].ToString() == "Admin")
        {
            return;
        }
        buttonBook.Enabled = true;
        labelBook.Text = GridView1.SelectedRow.Cells[1].Text;
        source = HttpUtility.UrlDecode(Request.QueryString["From"]);
        destination = HttpUtility.UrlDecode(Request.QueryString["To"]);
        date = HttpUtility.UrlDecode(Request.QueryString["Date"]);
        seats = HttpUtility.UrlDecode(Request.QueryString["Seats"]);
        labelAmount.Text = (float.Parse(seats) * float.Parse(GridView1.SelectedRow.Cells[9].Text)).ToString();
    }

    protected void buttonBook_Click(object sender, EventArgs e)
    {
        source = HttpUtility.UrlDecode(Request.QueryString["From"]);
        destination = HttpUtility.UrlDecode(Request.QueryString["To"]);
        date = HttpUtility.UrlDecode(Request.QueryString["Date"]);
        seats = HttpUtility.UrlDecode(Request.QueryString["Seats"]);
        SqlConnection con = new SqlConnection(cons);
        string sqlcmd = "INSERT INTO Bookings (RouteId, Username, Seats) VALUES (@rid, @user, @seat)";
        SqlCommand cmd = new SqlCommand(sqlcmd, con);
        cmd.Parameters.AddWithValue("@rid", GridView1.SelectedRow.Cells[1].Text);
        cmd.Parameters.AddWithValue("@user", Session["Username"].ToString());
        cmd.Parameters.AddWithValue("@seat", seats);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception err)
        {
            //Do something
        }
        finally
        {
            con.Close();
        }
        Application["TotalTickets"] = (int.Parse(Application["TotalTickets"].ToString()) + int.Parse(seats)).ToString();
        Application["TotalRevenue"] = (float.Parse(Application["TotalRevenue"].ToString()) + float.Parse(labelAmount.Text)).ToString();
        Response.Redirect("ProfilePage.aspx");
    }

    protected void filter_Changed(object sender, EventArgs e)
    {
        int temp1 = 0, temp2 = 0;
        float minval = -1, maxval = -1;
        string bus = "";
        source = HttpUtility.UrlDecode(Request.QueryString["From"]);
        destination = HttpUtility.UrlDecode(Request.QueryString["To"]);
        date = HttpUtility.UrlDecode(Request.QueryString["Date"]);
        seats = HttpUtility.UrlDecode(Request.QueryString["Seats"]);
        temp1 = (checkAC.Checked ? 1 : 0);
        temp2 = (checkSleeper.Checked ? 1 : 0);
        string sqlcmd = "SELECT * FROM Routes WHERE Source='" + source + "' " +
                "AND Destination='" + destination + "' " +
                "AND Seats>=" + seats + " AND CAST(Departure AS DATE)='" + date + "'";
        sqlcmd += ((temp1 == 1) ? (" AND ACType=" + temp1.ToString()) : "");
        sqlcmd += ((temp2 == 1) ? (" AND SleeperType=" + temp2.ToString()) : "");
        if (float.TryParse(textboxMinPrice.Text, out minval) && minval > 0.0)
        {
            if (float.TryParse(textboxMaxPrice.Text, out maxval) && maxval >= minval)
            {
                sqlcmd += " AND Price>=" + textboxMinPrice.Text;
                sqlcmd += " AND Price<=" + textboxMaxPrice.Text;
            }
        }
        bus = dropdownBus.SelectedItem.Text;
        if (bus != "Any")
        {
            sqlcmd += " AND BusOperator='" + bus + "'";
        }
        SqlConnection con = new SqlConnection(cons);
        SqlDataAdapter ad = new SqlDataAdapter(sqlcmd, con);
        DataSet ds = new DataSet();
        con.Open();
        ad.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
        con.Close();
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
}