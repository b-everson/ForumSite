using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreatePost : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int userID = -1;
        int topicID = -1;
        try
        {
            userID = Convert.ToInt32(Session["userID"]);
            topicID = Convert.ToInt32(Request.QueryString["topicID"]);
        }
        catch (Exception)
        {
            Response.Write("<script language='javascript'>alert('Error loading page, rerouting to home page.'); document.location = '" + ResolveUrl("Forums.aspx") + "'; </script>");
        }
        if (userID < 1)
        {
            btnSubmit.Enabled = false;
            Response.Write("<script language='javascript'>alert('You must be logged in in order to create a post.')</script>");
        }
        if (topicID < 1)
        {
            Response.Write("<script language='javascript'>alert('Error loading page, rerouting to home page.'); document.location = '" + ResolveUrl("~/Forums.aspx") + "'; </script>");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int topicID = Convert.ToInt32(Request.QueryString["topicID"]);
        int userID = Convert.ToInt32(Session["userID"]);
        PostDB.CreatePost(txtTitle.Text, txtContent.Text, userID ,topicID);
        Response.Redirect("~/Topics.aspx?topicId=" + topicID);
    }
}