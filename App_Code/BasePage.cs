using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class BasePage : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["MyTheme"] == null)
        {
            Session["MyTheme"] = "DarkSkin";
        }
        Page.Theme = Session["MyTheme"].ToString();
    }
}