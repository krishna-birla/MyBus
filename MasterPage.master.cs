using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["Username"] == null)
        {
            Session["Username"] = "Please Log In";
        }
        labelCurrentUser.Text = Session["Username"].ToString();
    }

    protected void buttonHome_Click(object sender, EventArgs e)
    {
        if(Session["Logged"] == null)
        {
            Session["Logged"] = "None";
        }
        if(Session["Logged"].ToString() == "None")
        {
            Response.Redirect("LoginPage.aspx");
        }
        else if(Session["Logged"].ToString() == "User")
        {
            Response.Redirect("HomePage.aspx");
        }
        else if(Session["Logged"].ToString() == "Admin")
        {
            Response.Redirect("HomePage.aspx");
        }
    }

    protected void buttonLogout_Click(object sender, EventArgs e)
    {
        Session["Logged"] = "None";
        Session["Username"] = "Please Log In";
        HttpCookie cookie = Request.Cookies["Autofill"];
        if(cookie != null)
        {
            cookie.Expires = DateTime.Now.AddMinutes(0);
        }
        Response.Redirect("LoginPage.aspx");
    }

    protected void buttonSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Search.aspx");
    }

    protected void buttonProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProfilePage.aspx");
    }

    protected void buttonTheme_Click(object sender, EventArgs e)
    {
        if (Session["MyTheme"].ToString() == "LightSkin")
        {
            Session["MyTheme"] = "DarkSkin";
        }
        else
        {
            Session["MyTheme"] = "LightSkin";
        }
        Server.Transfer(Request.FilePath);
    }
}
