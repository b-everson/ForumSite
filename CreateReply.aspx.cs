using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateReply : System.Web.UI.Page
{
    int responseToID = -1;
    Reply.ResponsesToTable responseToTable = Reply.ResponsesToTable.Post;
    Post originPost = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            responseToID = Convert.ToInt32(Request.QueryString["responseToID"]);
            string table = Request.QueryString["responseToTable"].ToLower();
            if (table == "post")
            {
                responseToTable = Reply.ResponsesToTable.Post;
            }
            else if (table == "message")
            {
                responseToTable = Reply.ResponsesToTable.Message;
            }
            else if (table == "reply")
            {
                responseToTable = Reply.ResponsesToTable.Reply;
            }
        }
        catch (Exception)
        {
            ErrorWithRedirect();
        }

        Object replyParent = null;

        if (responseToTable == Reply.ResponsesToTable.Post)
        {
            pnlReply.CssClass = "invisble";
            replyParent = PostDB.GetPost(responseToID);
            if (replyParent != null)
            {
                PopulatePostPanel((Post)replyParent);
            }
        }
        else if (responseToTable == Reply.ResponsesToTable.Reply)
        {
            replyParent = ReplyDB.GetReply(responseToID);
                      
            if (replyParent != null)
            {
                originPost = PostDB.GetParentPost((Reply)replyParent);
                if (originPost != null)
                {
                    pnlReply.CssClass = "replyPanel";
                    PopulatePostPanel(originPost);
                }

                lblReplyContent.Text = ((Reply)replyParent).Content;
                ForumUser author = ForumUserDB.GetUser(((Reply)replyParent).Sender);
                lblReplyAuthor.Text = author.UserName;
                lblReplyTime.Text = ((Reply)replyParent).TimePosted.ToString();
            }
        }

        else
        {
            ErrorWithRedirect();
        }

    }

    private void PopulatePostPanel(Post post)
    {
        ForumUser user = ForumUserDB.GetUser(post.UserID);
        lblPostAuthor.Text = user.UserName;
        lblPostContent.Text = post.Content;
        lblPostTime.Text = post.TimePosted.ToString();
        lblPostTitle.Text = post.Title;
    }

    private void ErrorWithRedirect()
    {
        Response.Write("<script language='javascript'>alert('Invalid page, redirecting to main forum page.'); setTimeout(function(){ document.location = '" + ResolveUrl("~/Forums.aspx") + "'} ,100)</script>");
    }

    protected void CreateClient(object sender, EventArgs e)
    {
        int userID = Convert.ToInt32(Session["userID"]);
        ReplyDB.CreateReply(taContent.Value, DateTime.Now, userID, responseToID, responseToTable);
        string url = "";
        if (originPost != null)
        {
            url = "~/Posts.aspx?PostID=" + originPost.PostID;
        }
        else if (responseToTable == Reply.ResponsesToTable.Post)
        {
            url = "~/Posts.aspx?PostID=" + responseToID;
        }
        else if (responseToTable == Reply.ResponsesToTable.Message)
        {
            url = "~/NoUrl.derp";
        }
        else if (responseToTable == Reply.ResponsesToTable.Reply)
        {
            url = "~/NoUrl.derp";
        }
        Response.Redirect(url);
    }

}