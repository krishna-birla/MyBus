using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : BasePage
{
    static string cons = ConfigurationManager.ConnectionStrings["MyBusConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Session["Logged"] == null || Session["Logged"].ToString() == "None")
            {
                buttonSearch.Enabled = false;
                buttonSearch.Text = "Please Login First";
            }
            else
            {
                buttonSearch.Enabled = true;
                buttonSearch.Text = "Search";
            }
            string sqlcmd = "SELECT DISTINCT Source FROM Routes";
            SqlConnection sqlcon = new SqlConnection(cons);
            SqlCommand cmd = new SqlCommand(sqlcmd, sqlcon);
            SqlDataReader rd;
            try
            {
                sqlcon.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    dropdownFrom.Items.Add(rd["Source"].ToString());
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
            sqlcmd = "SELECT DISTINCT Destination FROM Routes";
            sqlcon = new SqlConnection(cons);
            cmd = new SqlCommand(sqlcmd, sqlcon);
            try
            {
                sqlcon.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    dropdownTo.Items.Add(rd["Destination"].ToString());
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
            dropdownFrom.SelectedIndex = 0;
            dropdownTo.SelectedIndex = 1;
            textboxSeats.Text = "1";
        }
    }

    protected void buttonSearch_Click(object sender, EventArgs e)
    {
        if(!Page.IsValid)
        {
            return;
        }
        string url = "BookPage.aspx?" + "From=" + HttpUtility.UrlEncode(dropdownFrom.Text) + "&To=";
        url = url + HttpUtility.UrlEncode(dropdownTo.Text) + "&Date=" + HttpUtility.UrlEncode(textboxDate.Text) + "&Seats=" + HttpUtility.UrlEncode(textboxSeats.Text);
        Response.Redirect(url);
    }

    protected void dropdownFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(dropdownFrom.SelectedIndex == dropdownTo.SelectedIndex)
        {
            dropdownTo.SelectedIndex = (dropdownTo.SelectedIndex + 1) % dropdownTo.Items.Count;
        }
    }

    protected void dropdownTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropdownTo.SelectedIndex == dropdownFrom.SelectedIndex)
        {
            dropdownFrom.SelectedIndex = (dropdownFrom.SelectedIndex + 1) % dropdownFrom.Items.Count;
        }
    }

    protected void dateValid_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (DateTime.TryParseExact(textboxDate.Text, @"yyyy-MM-dd", null, DateTimeStyles.None, out _))
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }
}