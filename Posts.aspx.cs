using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Posts : System.Web.UI.Page
{
    Post post = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        int postID = -1;
        try
        {
            postID = Convert.ToInt32(Request.QueryString["postID"]);
        }catch(InvalidCastException){
            ErrorWithRedirect();
        }
        post = PostDB.GetPost(postID);
        if (post != null)
        {
            lblPostTitle.Text = post.Title;
            lblPostContent.Text = post.Content;
        }
        else
        {
            ErrorWithRedirect();
        }
    }

    private void ErrorWithRedirect()
    {
        Response.Write("<script language='javascript'>alert('Invalid page, redirecting to main forum page.'); setTimeout(function(){ document.location = '" + ResolveUrl("~/Forums.aspx") + "'} ,100)</script>");
    }
}