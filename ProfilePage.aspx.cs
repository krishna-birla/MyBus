using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class ProfilePage : BasePage
{
    static string cons = ConfigurationManager.ConnectionStrings["MyBusConnectionString"].ConnectionString;
    private string SortDirection1
    {
        get
        { 
            return(ViewState["SortDirection1"] != null ? ViewState["SortDirection1"].ToString() : "ASC"); 
        }
        set 
        { 
            ViewState["SortDirection1"] = value;
        }
    }

    private string SortDirection2
    {
        get
        {
            return (ViewState["SortDirection2"] != null ? ViewState["SortDirection2"].ToString() : "ASC");
        }
        set
        {
            ViewState["SortDirection2"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!this.IsPostBack)
        {
            if(Session["Logged"] == null || Session["Logged"].ToString() == "None")
            {
                Response.Redirect("HomePage.aspx");
            }
            if(Session["Logged"].ToString() == "Admin")
            {
                TextBox1.Visible = true;
                TextBox2.Visible = true;
                TextBox3.Visible = true;
                TextBox4.Visible = true;
                TextBox5.Visible = true;
                TextBox6.Visible = true;
                TextBox7.Visible = true;
                TextBox8.Visible = true;
                TextBox9.Visible = true;
                TextBox10.Visible = true;
                buttonInsert.Visible = true;
                TextBoxArrival.Visible = true;
                TextBoxBusOperator.Visible = true;
                TextBoxDeparture.Visible = true;
                TextBoxPrice.Visible = true;
                TextBoxDestination.Visible = true;
                TextBoxSeatsInsert.Visible = true;
                TextBoxSource.Visible = true;
                TextBoxSleeperType.Visible = true;
                TextBoxACType.Visible = true;
                GridView1.Visible = false;
                GridView2.Visible = true;
                GridView3.Visible = true;
                string sqlcmd = "SELECT * FROM Bookings";
                SqlConnection con = new SqlConnection(cons);
                SqlDataAdapter ad = new SqlDataAdapter(sqlcmd, con);
                DataSet ds = new DataSet();
                con.Open();
                ad.Fill(ds);
                GridView2.DataSource = ds;
                GridView2.DataBind();
                GridView3.DataBind();
                con.Close();
            }
            else
            {
                TextBox1.Visible = false;
                TextBox2.Visible = false;
                TextBox3.Visible = false;
                TextBox4.Visible = false;
                TextBox5.Visible = false;
                TextBox6.Visible = false;
                TextBox7.Visible = false;
                TextBox8.Visible = false;
                TextBox9.Visible = false;
                TextBox10.Visible = false;
                buttonInsert.Visible = false;
                TextBoxArrival.Visible = false;
                TextBoxBusOperator.Visible = false;
                TextBoxDeparture.Visible = false;
                TextBoxPrice.Visible = false;
                TextBoxDestination.Visible = false;
                TextBoxSeatsInsert.Visible = false;
                TextBoxSource.Visible = false;
                TextBoxSleeperType.Visible = false;
                TextBoxACType.Visible = false;
                GridView2.Visible = false;
                GridView3.Visible = false;
                GridView1.Visible = true;
                string sqlcmd = "SELECT Bookings.Id, Routes.Source, Routes.Destination, Routes.Departure, " +
                "Routes.Arrival, Routes.Price, Routes.BusOperator, Bookings.Seats, Routes.ACType, Routes.SleeperType " +
                "FROM Bookings INNER JOIN Routes ON Routes.Id = Bookings.RouteId WHERE Bookings.Username = '" +
                Session["Username"].ToString() + "'";
                SqlConnection con = new SqlConnection(cons);
                SqlDataAdapter ad = new SqlDataAdapter(sqlcmd, con);
                DataSet ds = new DataSet();
                con.Open();
                ad.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();
                con.Close();
            }
            SortDirection1 = "ASC";
            SortDirection2 = "ASC";
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        seatsValidate.Visible = true;
        labelCancel.Text = "Cancellation for booking ID: ";
        labelCancel.Text = labelCancel.Text + GridView1.SelectedRow.Cells[1].Text;
        labelCancel.Visible = true;
        textboxSeats.Visible = true;
        labelTickets.Visible = true;
        buttonBack.Visible = true;
        buttonCancelTickets.Visible = true;
    }

    protected void buttonBack_Click(object sender, EventArgs e)
    {
        seatsValidate.Visible = false;
        labelCancel.Visible = false;
        textboxSeats.Visible = false;
        labelTickets.Visible = false;
        buttonBack.Visible = false;
        buttonCancelTickets.Visible = false;
    }

    protected void buttonCancelTickets_Click(object sender, EventArgs e)
    {
        if(!Page.IsValid)
        {
            return;
        }
        SqlConnection con = new SqlConnection(cons);
        if (int.Parse(textboxSeats.Text) == int.Parse(GridView1.SelectedRow.Cells[10].Text))
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Bookings WHERE Id=@bid", con);
            cmd.Parameters.AddWithValue("@bid", GridView1.SelectedRow.Cells[1].Text);
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
        }
        else
        {
            SqlCommand cmd = new SqlCommand("UPDATE Bookings SET Seats=@seat WHERE Id=@bid", con);
            cmd.Parameters.AddWithValue("@bid", GridView1.SelectedRow.Cells[1].Text);
            cmd.Parameters.AddWithValue("@seat", (int.Parse(GridView1.SelectedRow.Cells[10].Text) - int.Parse(textboxSeats.Text)).ToString());
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
        }
        string sqlcmd = "SELECT Bookings.Id, Routes.Source, Routes.Destination, Routes.Departure, " +
                "Routes.Arrival, Routes.Price, Routes.BusOperator, Bookings.Seats, Routes.ACType, Routes.SleeperType " +
                "FROM Bookings INNER JOIN Routes ON Routes.Id = Bookings.RouteId WHERE Bookings.Username = '" +
                Session["Username"].ToString() + "'";
        SqlDataAdapter ad = new SqlDataAdapter(sqlcmd, con);
        DataSet ds = new DataSet();
        con.Open();
        ad.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
        con.Close();
        float labelAmount = float.Parse(textboxSeats.Text) * float.Parse(GridView1.SelectedRow.Cells[9].Text);
        Application["TotalTickets"] = (int.Parse(Application["TotalTickets"].ToString()) - int.Parse(textboxSeats.Text)).ToString();
        Application["TotalRevenue"] = (float.Parse(Application["TotalRevenue"].ToString()) - labelAmount).ToString();
        seatsValidate.Visible = false;
        labelCancel.Visible = false;
        textboxSeats.Visible = false;
        labelTickets.Visible = false;
        buttonBack.Visible = false;
        buttonCancelTickets.Visible = false;
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sqlcmd = "SELECT Bookings.Id, Routes.Source, Routes.Destination, Routes.Departure, " +
                "Routes.Arrival, Routes.Price, Routes.BusOperator, Bookings.Seats, Routes.ACType, Routes.SleeperType " +
                "FROM Bookings INNER JOIN Routes ON Routes.Id = Bookings.RouteId WHERE Bookings.Username = '" +
                Session["Username"].ToString() + "' ORDER BY " + e.SortExpression + " " + SortDirection1;
        SqlConnection con = new SqlConnection(cons);
        SqlDataAdapter ad = new SqlDataAdapter(sqlcmd, con);
        DataSet ds = new DataSet();
        SortDirection1 = (SortDirection1 == "ASC" ? "DESC" : "ASC");
        con.Open();
        ad.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
        con.Close();
    }

    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sqlcmd = "SELECT * FROM Bookings ORDER BY " + e.SortExpression + " " + SortDirection2;
        SortDirection2 = (SortDirection2 == "ASC" ? "DESC" : "ASC");
        SqlConnection con = new SqlConnection(cons);
        SqlDataAdapter ad = new SqlDataAdapter(sqlcmd, con);
        DataSet ds = new DataSet();
        con.Open();
        ad.Fill(ds);
        GridView2.DataSource = ds;
        GridView2.DataBind();
        con.Close();
    }

    protected void Unnamed_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if(textboxSeats.Text != "" && int.Parse(textboxSeats.Text) <= int.Parse(GridView1.SelectedRow.Cells[10].Text))
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }

    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    protected void buttonInsert_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(cons);
        SqlCommand cmd2 = new SqlCommand("INSERT INTO Routes (Source, Destination," +
            "BusOperator, ACType, SleeperType," +
            "Arrival, Departure, Price, Seats) VALUES (@Source, @Destination," +
            "@BusOperator, @ACType, @SleeperType," +
            "@Arrival, @Departure, @Price, @Seats)", con);
        cmd2.Parameters.AddWithValue("@Source", TextBoxSource.Text);
        cmd2.Parameters.AddWithValue("@Destination", TextBoxDestination.Text);
        cmd2.Parameters.AddWithValue("@Price", TextBoxPrice.Text);
        cmd2.Parameters.AddWithValue("@Seats", TextBoxSeatsInsert.Text);
        cmd2.Parameters.AddWithValue("@BusOperator", TextBoxBusOperator.Text);
        cmd2.Parameters.AddWithValue("@Arrival", TextBoxArrival.Text);
        cmd2.Parameters.AddWithValue("@Departure", TextBoxDeparture.Text);
        cmd2.Parameters.AddWithValue("@ACType", TextBoxACType.Text);
        cmd2.Parameters.AddWithValue("@SleeperType", TextBoxSleeperType.Text);
        try
        {
            con.Open();
            cmd2.ExecuteNonQuery();
        }
        catch (Exception err)
        {
            //Do something
        }
        finally
        {
            con.Close();
        }
        GridView3.DataBind();
    }
}